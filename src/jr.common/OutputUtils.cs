using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterConsoleTables;
using FluentDateTime;

namespace jr.common
{
    public class DateRange
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
    
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
                        .Select(x => string.Format("\"{0}\"", EscapeQuotes(x)))
                        .ToList());
                output.AppendLine(newList);
            }
            return output.ToString();
        }

        private static string EscapeQuotes(string input)
        {
            return input.Replace("\"","\"\"");
        }

        public static (string datestart, string dateend) GetTimePeriodOption(string timePeriodString)
        {
            return GetTimePeriodOption(timePeriodString, DateTime.Now);
        }

        public static (string datestart, string dateend) GetTimePeriodOption(string timePeriodString, DateTime today)
        {
            const string dateFormatPattern = "yyyy-MM-dd";
            var dateRange = new DateRange();
            switch (timePeriodString)
            {
                case "ytd":
                    dateRange.DateStart = today.FirstDayOfYear();
                    dateRange.DateEnd = today;
                    break;
                case "lastmonth":
                    dateRange.DateStart = today.PreviousMonth().FirstDayOfMonth();
                    dateRange.DateEnd = today.PreviousMonth().LastDayOfMonth();
                    break;
                case "week":
                    dateRange.DateStart = today.FirstDayOfWeek();
                    dateRange.DateEnd = today;
                    break;
                case "lastweek":
                    dateRange.DateStart = today.WeekEarlier().FirstDayOfWeek();
                    dateRange.DateEnd = today.WeekEarlier().LastDayOfWeek();
                    break;
                case "month":
                default:
                    dateRange.DateStart = today.FirstDayOfMonth();
                    dateRange.DateEnd = today;
                    break;
            }
            return (dateRange.DateStart.ToString(dateFormatPattern), dateRange.DateEnd.ToString(dateFormatPattern));
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