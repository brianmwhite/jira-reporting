using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExcelDataReader;
using McMaster.Extensions.CommandLineUtils;
using jr.common;

//quicktype -o Options.cs --namespace "jr" sample.json

namespace jr
{
    class Program
    {
        public static int Main(string[] args)
        {
            Options userOptions = new Options();

            var app = new CommandLineApplication();

            app.HelpOption();
            var optionConfigFileLocation = app.Option("-c|--config <CONFIG_FILE>", "Configuration file location", CommandOptionType.SingleValue);
            var optionInputSource = app.Option("-i|--input <INPUT_FILE>", "Input file location", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                try
                {
                    //get json config location from command line argument
                    string configFileLocation = optionConfigFileLocation.HasValue()
                        ? optionConfigFileLocation.Value()
                        : string.Empty;

                    //check to see if the file exists
                    if (string.IsNullOrEmpty(configFileLocation))
                    {
                        throw new ArgumentNullException("ERROR: A config file is required");
                    }
                    else if (!File.Exists(configFileLocation))
                    {
                        Console.WriteLine(Path.GetFullPath(configFileLocation));
                        throw new FileNotFoundException();
                    }

                    //deserialize json
                    string configJson = File.ReadAllText(configFileLocation);
                    userOptions = Options.FromJson(configJson);

                    //get source file location from command line argument
                    string inputSourceLocation = optionInputSource.HasValue()
                        ? optionInputSource.Value()
                        : string.Empty;

                    //check to see if the input file exists
                    if (!string.IsNullOrEmpty(inputSourceLocation) && !File.Exists(inputSourceLocation))
                    {
                        Console.WriteLine(Path.GetFullPath(inputSourceLocation));
                        throw new FileNotFoundException();
                    }

                    string output = GenerateSummaryFromFile(userOptions, inputSourceLocation);
                    Console.WriteLine(output);

                    return 0;
                }
                catch (Exception ex) when
                    (ex is ArgumentNullException || ex is FileNotFoundException)
                {
                    Console.WriteLine(ex.Message);
                    return 1;
                }
            });

            return app.Execute(args);
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
