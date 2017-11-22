using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using ExcelDataReader;
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
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication(false);

            app.HelpOption();
            app.Command("login", login, false);
            app.Command("time", SummarizeTime, false);

            app.OnExecute(() => 0);

            return app.Execute(args);
        }

        private static void SummarizeTime(CommandLineApplication command)
        {
            var userOptions = new Options();
            
            var optionConfigFileLocation = command.Option("-c|--config <CONFIG_FILE>", "Configuration file location", CommandOptionType.SingleValue);
            var optionInputSource = command.Option("-i|--input <INPUT_FILE>", "Input file location", CommandOptionType.SingleValue);
            var optionTimePeriod = command.Option("-t|--timeperiod <TIME_PERIOD>", "Time period: lastmonth, month, ytd", CommandOptionType.SingleValue);
            
            command.OnExecute(() =>
            {
                userOptions = GetUserOptions(optionConfigFileLocation);

                if (optionInputSource.HasValue() && !string.IsNullOrEmpty(optionInputSource.Value()))
                {
                    string inputSourceLocation = optionInputSource.Value();
                    //check to see if the input file exists
                    if (!string.IsNullOrEmpty(inputSourceLocation) && !File.Exists(inputSourceLocation))
                    {
                        Console.WriteLine(Path.GetFullPath(inputSourceLocation));
                        throw new FileNotFoundException();
                    }

                    string output = GenerateSummaryFromFile(userOptions, inputSourceLocation);
                    Console.WriteLine(output);
                }
                else
                {
                    var jc = LocalProfileInfo.LoadJiraCredentials();
                    var ti = new TempoInput(jc.JiraURL, jc.JiraUser, jc.JiraPassword);
                    string dateFrom;
                    string dateTo;

                    if (optionTimePeriod.HasValue() && !string.IsNullOrEmpty(optionTimePeriod.Value()))
                    {
                        switch (optionTimePeriod.Value())
                        {
                            case "ytd":
                                dateFrom = DateTime.Now.FirstDayOfYear().ToString("yyyy-MM-dd");
                                dateTo = DateTime.Now.ToString("yyyy-MM-dd");
                                break;
                            case "month":
                                dateFrom = DateTime.Now.FirstDayOfMonth().ToString("yyyy-MM-dd");
                                dateTo = DateTime.Now.ToString("yyyy-MM-dd");
                                break;
                            case "lastmonth":
                                dateFrom = DateTime.Now.PreviousMonth().FirstDayOfMonth().ToString("yyyy-MM-dd");
                                dateTo = DateTime.Now.PreviousMonth().LastDayOfMonth().ToString("yyyy-MM-dd");
                                break;
                        }
                    }
                }
            });
        }

        private static Options GetUserOptions(CommandOption optionConfigFileLocation)
        {
            Options userOptions;
            //get json config location from command line argument
            string configFileLocation = optionConfigFileLocation.HasValue()
                ? optionConfigFileLocation.Value()
                : string.Empty;

            //check to see if the file exists
            if (string.IsNullOrEmpty(configFileLocation))
            {
                throw new ArgumentNullException("ERROR: A config file is required");
            }
            if (!File.Exists(configFileLocation))
            {
                Console.WriteLine(Path.GetFullPath(configFileLocation));
                throw new FileNotFoundException();
            }

            //deserialize json
            string configJson = File.ReadAllText(configFileLocation);
            userOptions = Options.FromJson(configJson);
            return userOptions;
        }

        private static void login (CommandLineApplication command)
        {
            command.Description = "Provide login credentials to jira";
            command.HelpOption();
            command.OnExecute(() =>
            {
                Console.WriteLine("-- Enter Jira login info ---");
                Console.Write("jira url (https://companyname.atlassian.net): ");
                var url= Console.ReadLine();
                
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

        private static string GenerateSummaryFromFile(Options userOptions, string inputSourceLocation)
        {
            List<WorkItem> workItems = ExtractWorkItemsFromExcel(inputSourceLocation);

            TimeSummarization ts = new TimeSummarization(
                  userOptions.BillingSetup.DevRate
                , userOptions.BillingSetup.MgmtRate
                , userOptions.BillingSetup.MgmtUsernames
                , userOptions.Advanced.SplitPo
                , userOptions.Advanced.Trim
                , userOptions.Output.Col
            );

            return ts.GenerateSummaryText(workItems);
        }

        private static List<WorkItem> ExtractWorkItemsFromExcel(string inputSourceLocation)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            //if the input file has an excel extension parse it with ExcelDataReader
            if (inputSourceLocation.EndsWith(".xls") || inputSourceLocation.EndsWith(".xlsx"))
            {
                ExcelInput excel = new ExcelInput();
                workItems = excel.ReadExcel(inputSourceLocation);
            }

            return workItems;
        }
    }
}
