using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using BetterConsoleTables;
using FluentDateTime;
using jr.common.Models;

namespace jr.common
{
    public class TimeSummarization
    {
        private readonly double _devRate;
        private readonly double _mgmtRate;
        private readonly string[] _mgmtUsers;
        private readonly bool _splitPo;
        private readonly string _projectTextToTrim;
        private readonly bool _groupByIssue;
        private readonly string _outputColumns;

        public TimeSummarization(double devRate, double mgmtRate, string[] mgmtUsers ,bool splitPo
                                ,string projectTextToTrim ,string outputColumns = "", string groupByString = "project")
        {
            _devRate = devRate;
            _mgmtRate = mgmtRate;
            _mgmtUsers = mgmtUsers is null ? new string[] { } : mgmtUsers;

            _splitPo = splitPo;
            _projectTextToTrim = projectTextToTrim;

            _groupByIssue = groupByString == "issue";
            
            _outputColumns = string.IsNullOrEmpty(outputColumns) 
                ? "Project,Code,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount" 
                : outputColumns;
        }

        public string GenerateSummaryText(IEnumerable<WorkItem> workItems, OutputUtils.OutputFormat format)
        {
            var si = SummarizeWorkItems(workItems);
            si.Add(AddSummarizedTotal(si));
            List<string[]> dataTable = GenerateOutputData(si);
            return OutputUtils.CreateOutputString(format, dataTable);
        }

        public List<SummarizedItem> SummarizeWorkItems(IEnumerable<WorkItem> workItems)
        {
            var summarizedWorkItems = workItems
                .Where(x => x.billedHours > 0)
                .GroupBy(x => _groupByIssue ? x.issueKey : x.project)
                .Select(x => new SummarizedItem(_splitPo, _projectTextToTrim)
                    {
                        project = x.First().project,
                        issue = x.First().combinedIssueName,
                        dev_rate = _devRate,
                        mgmt_rate = _mgmtRate,
                        dev_hours = x.Sum(z => GetHoursBasedOnRole(z, HourType.Developer)),
                        mgmt_hours = x.Sum(z => GetHoursBasedOnRole(z, HourType.Manager)),
                    }
                )
                .OrderBy(x => x.billing_code)
                .ThenBy(x => x.project)
                .ThenBy(x => x.issue)
                .ToList();

            return summarizedWorkItems;
        }

        public enum HourType
        {
            Manager,
            Developer
        }
        
        public double GetHoursBasedOnRole(WorkItem item, HourType hourType)
        {
            switch (hourType)
            {
                case HourType.Manager:
                    return _mgmtUsers.Contains(item.userName) ? item.billedHours : 0;
                case HourType.Developer:
                default:
                    return !_mgmtUsers.Contains(item.userName) ? item.billedHours : 0;
            }
        }

        private SummarizedItem AddSummarizedTotal(IReadOnlyCollection<SummarizedItem> summarizedWorkItems)
        {
            var totalRow = new SummarizedItem()
            {
                dev_rate = _devRate,
                mgmt_rate = _mgmtRate,
                dev_hours = summarizedWorkItems.Sum(x => x.dev_hours),
                mgmt_hours = summarizedWorkItems.Sum(x => x.mgmt_hours),
            };

            return totalRow;
        }

        public List<string[]> GenerateOutputData(IEnumerable<SummarizedItem> summarizedWorkItems)
        {
            OrderedDictionary columnMap = new OrderedDictionary();
            string[] columnFields = _outputColumns.Split(',');
            CultureInfo culture = new CultureInfo("en-US");
            
            List<string[]> dataTable = new List<string[]>();

            foreach (string col in columnFields)
            {
                string[] fields = col.Split('=');
                string fieldName = fields[0];
                string fieldLabel = string.Format("{0}", fieldName.Replace("_", " "));
                if (fields.Length > 1)
                {
                    fieldLabel = string.Format("{0}", fields[1]);
                }
                columnMap.Add(fieldName, fieldLabel);
            }
            string[] headers = new string[columnMap.Values.Count];
            columnMap.Values.CopyTo(headers, 0);
            dataTable.Add(headers);

            //build data rows
            foreach (var row in summarizedWorkItems)
            {
                int dataRowNumber = 0;
                string[] values = new string[columnMap.Values.Count];
                foreach (DictionaryEntry item in columnMap)
                {
                    switch (item.Key)
                    {
                        case ("Project"):
                            values[dataRowNumber] = string.Format(culture, "{0}", row.billing_project);
                            break;
                        case ("Code"):
                            values[dataRowNumber] = string.Format(culture, "{0}", row.billing_code);
                            break;
                        case ("Issue"):
                            values[dataRowNumber] = string.Format(culture, "{0}", row.issue);
                            break;
                        case ("Dev_Hours"):
                            values[dataRowNumber] = string.Format(culture, "{0:N2}", row.dev_hours);
                            break;
                        case ("Dev_Amount"):
                            values[dataRowNumber] = string.Format(culture, "{0:C}", row.dev_amount);
                            break;
                        case ("Mgmt_Hours"):
                            values[dataRowNumber] = string.Format(culture, "{0:N2}", row.mgmt_hours);
                            break;
                        case ("Mgmt_Amount"):
                            values[dataRowNumber] = string.Format(culture, "{0:C}", row.mgmt_amount);
                            break;
                        case ("Total_Hours"):
                            values[dataRowNumber] = string.Format(culture, "{0:N2}", row.total_hours);
                            break;
                        case ("Total_Amount"):
                            values[dataRowNumber] = string.Format(culture, "{0:C}", row.total_amount);
                            break;
                        default:
                            values[dataRowNumber] = string.Empty;
                            break;
                    }
                    dataRowNumber++;
                }
                dataTable.Add(values);
            }
            return dataTable;
        }
    }
}