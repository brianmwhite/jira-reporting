using System;
using System.Collections.Generic;
using System.IO;
using FluentDateTime;
using McMaster.Extensions.CommandLineUtils;
using jr.common;
using jr.common.Excel;
using jr.common.Jira;
using Newtonsoft.Json;

//quicktype -o Options.cs --namespace "jr" sample.json

namespace jr
{
    public static class Program
    {
        //TODO: add consistent handling of errors, colorize output
        //TODO: split out non console specific functionality to jr.common
        
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication(false) {Name = "jr"};

            //TODO: show help if no commands are specified
            
            app.HelpOption();

            app.OnExecute(() => 0);

            app.Command("login", Login, false);
            
            //TODO: try refactoring timesummary and login to named method
            
            app.Command("timesummary", (command) =>
                {
                    Options userOptions;

                    var optionSrcOption = command.Option("-s|--src <SRC>"
                        , "Source option: web [default], excel",
                        CommandOptionType.SingleValue);
                    
                    var optionConfigFileLocation = command.Option("-c|--config <CONFIG_FILE>",
                        "Configuration file location"
                        , CommandOptionType.SingleValue);
                    
                    var optionInputSource = command.Option("-i|--input <INPUT_FILE>"
                        , "Input file location",
                        CommandOptionType.SingleValue);
                    
                    var optionTimePeriod = command.Option("-t|--timeperiod <TIME_PERIOD>",
                        "Time period: lastmonth, month, ytd"
                        , CommandOptionType.SingleValue);
                    
                    var optionAccount = command.Option("-a|--account <ACCOUNT>", "Account key",
                        CommandOptionType.SingleValue);
                    
                    var optionDateRange = command.Option("-d|--daterange <dates>", "Date Range",
                        CommandOptionType.MultipleValue);

                    var optionGroupBy = command.Option("-g|--groupby <groupby>", "Group by <project>, <issue>",
                        CommandOptionType.SingleValue);

                    var optionSeparatorOutput = command.Option("-o|--output <output>", "csv,tab,pretty"
                        , CommandOptionType.SingleValue);

                    command.OnExecute(() =>
                    {
                        userOptions = GetUserOptions(optionConfigFileLocation);

                        if (optionSrcOption.HasValue())
                        {
                            string src = optionSrcOption.Value();
                            switch (src)
                            {
                                case "web":
                                    
                                    //TODO: check for local credentials first
                                    
                                    var jc = LocalProfileInfo.LoadJiraCredentials();
                                    var ti = new TempoInput(jc.JiraURL, jc.JiraUser, jc.JiraPassword);

                                    //TODO: parse/catch invalid date strings
                                    
                                    if (optionDateRange.HasValue())
                                    {
                                        userOptions.Filtering.DateStart = optionDateRange.Values[0];
                                        userOptions.Filtering.DateEnd = optionDateRange.Values[1];
                                    }
                                    else if (optionTimePeriod.HasValue())
                                    {
                                        (userOptions.Filtering.DateStart, userOptions.Filtering.DateEnd) 
                                            = TimeSummarization.GetTimePeriodOption(optionTimePeriod.Value());
                                    }
                                    if (optionAccount.HasValue())
                                    {
                                        userOptions.Filtering.Account = optionAccount.Value();
                                    }
                                    
                                    if (optionGroupBy.HasValue())
                                    {
                                        string groupString = optionGroupBy.Value().ToLower();
                                        if (groupString == "project" || groupString == "issue")
                                        {
                                            userOptions.Filtering.Groupby = groupString;
                                        }
                                        else
                                        {
                                            userOptions.Filtering.Groupby = "project";
                                        }
                                    }
                                    if (optionSeparatorOutput.HasValue())
                                    {
                                        userOptions.Output.Separator = optionSeparatorOutput.Value();
                                    }
                                    
                                    bool getParentIssue = userOptions.Filtering.Groupby == "issue";
                                    
                                    var wi = ti.GetWorkItems(
                                        userOptions.Filtering.DateStart, userOptions.Filtering.DateEnd, 
                                        userOptions.Filtering.Account, getParentIssue);

                                    var tempoOutput = GenerateSummaryText(userOptions, wi);
                                    Console.WriteLine(tempoOutput);

                                    break;
                                case "excel":
                                    //excel
                                    if (optionInputSource.HasValue())
                                    {
                                        string inputSourceLocation = optionInputSource.Value();
                                        //check to see if the input file exists
                                        if (!string.IsNullOrEmpty(inputSourceLocation) &&
                                            !File.Exists(inputSourceLocation))
                                        {
                                            Console.Error.WriteLine(string.Format($"Input file not found: {Path.GetFullPath(inputSourceLocation)}"));
                                        }

                                        var workItems = ExcelInput.ExtractWorkItemsFromExcel(inputSourceLocation);
                                        var output = GenerateSummaryText(userOptions, workItems);
                                        Console.WriteLine(output);
                                    }
                                    break;
                            }
                        }
                    });
                }
                , false);

            return app.Execute(args);
        }

        private static Options GetUserOptions(CommandOption optionConfigFileLocation)
        {
            Options userOptions = new Options();
            //get json config location from command line argument
            string configFileLocation = optionConfigFileLocation.HasValue()
                ? optionConfigFileLocation.Value()
                : string.Empty;

            //check to see if the file exists
            if (string.IsNullOrEmpty(configFileLocation))
            {
                Console.Error.WriteLine("Config file (-c or --config) is required");
            } 
            else if (!File.Exists(configFileLocation))
            {
                Console.Error.WriteLine(
                    string.Format($"Config file not found: {Path.GetFullPath(configFileLocation)}"));
            }
            else
            {
                //deserialize json
                string configJson = File.ReadAllText(configFileLocation);
                userOptions = Options.FromJson(configJson);
            }
            return userOptions;
        }

        private static void Login(CommandLineApplication command)
        {
            //TODO: test login before saving
            
            command.Description = "Provide login credentials to jira";
            command.HelpOption();
            command.OnExecute(() =>
            {
                Console.WriteLine("-- Enter Jira login info ---");
                Console.Write("jira url (https://companyname.atlassian.net): ");
                var url = Console.ReadLine();

                Console.Write("login: ");
                var login = Console.ReadLine();

                Console.Write("password: ");
                var password = Console.ReadLine();

                var jc = new JiraCredentials
                {
                    JiraURL = url,
                    JiraUser = login,
                    JiraPassword = password
                };
                var json = JsonConvert.SerializeObject(jc);
                LocalProfileInfo.CreateJiraCredentialsFile(json);
            });
        }

        private static string GenerateSummaryText(Options userOptions, IEnumerable<WorkItem> workItems)
        {
            var ts = new TimeSummarization(
                userOptions.BillingSetup.DevRate
                , userOptions.BillingSetup.MgmtRate
                , userOptions.BillingSetup.MgmtUsernames
                , userOptions.Advanced.SplitPo
                , userOptions.Advanced.Trim
                , userOptions.Output.Col
                , userOptions.Filtering.Groupby
            );

            var outputformat = TimeSummarization.OutputFormat.Pretty;
            switch (userOptions.Output.Separator.ToLower())
            {
                case "csv":
                    outputformat = TimeSummarization.OutputFormat.Csv;
                    break;
                case "tab":
                    outputformat = TimeSummarization.OutputFormat.Tab;
                    break;
            }
            return ts.GenerateSummaryText(workItems, outputformat);
        }
    }
}