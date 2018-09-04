using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterConsoleTables;
using FluentDateTime;

namespace jr.common
{
    public static class OutputUtils
    {
        public enum OutputFormat
        {
            Csv,
            Tab,
            Pretty
        }

        public static string GeneratePrettyOutput(List<string[]> dataTable)
        {
            var table = new Table();
            table.Config = TableConfiguration.Markdown();
            
            var header = dataTable[0];
            table.AddColumns(header);
            
            //remove header row
            dataTable.RemoveAt(0);
            table.AddRows(dataTable);
            
            return table.ToString();
        }

        public static string GenerateSeparatedValueTextOutput(IEnumerable<string[]> dataTable, char fieldSeparator)
        {
            var output = new StringBuilder();
            foreach (var t in dataTable)
            {
                var newList = string.Join(fieldSeparator, t
                        .Select(x => string.Format("\"{0}\"", x.Replace("\"","\"\"")))
                        .ToList());
                output.AppendLine(newList);
            }
            return output.ToString();
        }

        public static (string datestart, string dateend) GetTimePeriodOption(string timePeriodString)
        {
            return GetTimePeriodOption(timePeriodString, DateTime.Now);
        }

        public static (string datestart, string dateend) GetTimePeriodOption(string timePeriodString, DateTime today)
        {
            string datestart;
            string dateend;
            switch (timePeriodString)
            {
                case "ytd":
                    datestart = today.FirstDayOfYear()
                        .ToString("yyyy-MM-dd");
                    dateend = today.ToString("yyyy-MM-dd");
                    break;
                case "month":
                    datestart = today.FirstDayOfMonth()
                        .ToString("yyyy-MM-dd");
                    dateend = today.ToString("yyyy-MM-dd");
                    break;
                case "lastmonth":
                    datestart = today.PreviousMonth()
                        .FirstDayOfMonth().ToString("yyyy-MM-dd");
                    dateend = today.PreviousMonth()
                        .LastDayOfMonth().ToString("yyyy-MM-dd");
                    break;
                case "week":
                    datestart = today.FirstDayOfWeek()
                        .ToString("yyyy-MM-dd");
                    dateend = today.ToString("yyyy-MM-dd");
                    break;
                case "lastweek":
                    datestart = today.WeekEarlier()
                        .FirstDayOfWeek().ToString("yyyy-MM-dd");
                    dateend = today.WeekEarlier()
                        .LastDayOfWeek().ToString("yyyy-MM-dd");
                    break;
                default:
                    datestart = today.FirstDayOfMonth()
                        .ToString("yyyy-MM-dd");
                    dateend = today.ToString("yyyy-MM-dd");
                    break;
            }
            return (datestart, dateend);
        }

        public static string CreateOutputString(OutputFormat format, List<string[]> dataTable)
        {
            string output;
            switch (format)
            {
                case OutputFormat.Csv:
                    output = GenerateSeparatedValueTextOutput(dataTable, ',');
                    break;
                case OutputFormat.Tab:
                    output = GenerateSeparatedValueTextOutput(dataTable, '\t');
                    break;
                case OutputFormat.Pretty:
                default:
                    output = GeneratePrettyOutput(dataTable);
                    break;
            }
            return output;
        }

        public static OutputFormat ConvertOutputFormat(string formatString)
        {
            OutputFormat outputformat;
            switch (formatString.ToLower())
            {
                case "csv":
                    outputformat = OutputFormat.Csv;
                    break;
                case "tab":
                    outputformat = OutputFormat.Tab;
                    break;
                case "pretty":
                default:
                    outputformat = OutputFormat.Pretty;
                    break;
            }
            return outputformat;
        }
    }
}