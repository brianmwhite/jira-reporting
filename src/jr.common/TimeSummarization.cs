using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace jr.common
{
    public class TimeSummarization
    {
        private List<WorkItem> workItems;
        private double devRate;
        private double mgmtRate;
        private string[] mgmtUsers;
        private bool splitPO;
        private string projectTextToTrim;
        private List<SummarizedItem> _summarizedWorkItems;

        public TimeSummarization(List<WorkItem> _workItems, double _devRate, double _mgmtRate, string[] _mgmtUsers, bool _splitPO, string _projectTextToTrim)
        {
            this.workItems = _workItems;
            this.devRate = _devRate;
            this.mgmtRate = _mgmtRate;
            if (_mgmtUsers is null)
            {
                this.mgmtUsers = new string[] { };
            }
            else
            {
                this.mgmtUsers = _mgmtUsers;
            }
            this.splitPO = _splitPO;
            this.projectTextToTrim = _projectTextToTrim;
        }

        public void SummarizeWorkItems()
        {
            _summarizedWorkItems = workItems
                .Where(x => x.billedHours > 0)
                .GroupBy(x => x.project)
                .Select(x => new SummarizedItem(splitPO, projectTextToTrim)
                {
                    project = x.First().project,
                    dev_rate = devRate,
                    mgmt_rate = mgmtRate,
                    dev_hours = x.Sum(z => !mgmtUsers.Contains(z.userName) ? z.billedHours : 0),
                    mgmt_hours = x.Sum(z => mgmtUsers.Contains(z.userName) ? z.billedHours : 0),
                }
                )
                .OrderBy(x => x.billing_code)
                .ThenBy(x => x.project)
                .ToList();

            SummarizedItem totalRow = new SummarizedItem()
            {
                project = "Total",
                dev_rate = devRate,
                mgmt_rate = mgmtRate,
                dev_hours = _summarizedWorkItems.Sum(x => x.dev_hours),
                mgmt_hours = _summarizedWorkItems.Sum(x => x.mgmt_hours),
            };

            _summarizedWorkItems.Add(totalRow);
        }
        public List<string[]> GenerateOutputData(string columnMapString = "") {
            if (string.IsNullOrEmpty(columnMapString))
            {
                columnMapString = "Project,Code,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount";
            }

            OrderedDictionary columnMap = new OrderedDictionary();
            string[] columnFields = columnMapString.Split(',');

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
            foreach (var row in _summarizedWorkItems)
            {
                int dataRowNumber = 0;
                string[] values = new string[columnMap.Values.Count];
                foreach (DictionaryEntry item in columnMap)
                {
                    switch (item.Key)
                    {
                        case ("Project"):
                            values[dataRowNumber] = string.Format("{0}", row.billing_project);
                            break;
                        case ("Code"):
                            values[dataRowNumber] = string.Format("{0}", row.billing_code);
                            break;
                        case ("Dev_Hours"):
                            values[dataRowNumber] = string.Format("{0:N2}", row.dev_hours);
                            break;
                        case ("Dev_Amount"):
                            values[dataRowNumber] = string.Format("{0:C}", row.dev_amount);
                            break;
                        case ("Mgmt_Hours"):
                            values[dataRowNumber] = string.Format("{0:N2}", row.mgmt_hours);
                            break;
                        case ("Mgmt_Amount"):
                            values[dataRowNumber] = string.Format("{0:C}", row.mgmt_amount);
                            break;
                        case ("Total_Hours"):
                            values[dataRowNumber] = string.Format("{0:N2}", row.total_hours);
                            break;
                        case ("Total_Amount"):
                            values[dataRowNumber] = string.Format("{0:C}", row.total_amount);
                            break;
                        default:
                            break;
                    }
                    dataRowNumber++;
                }
                dataTable.Add(values);
            }            
            return dataTable;
        }

        public string GenerateSeparatedValueTextOutput(char fieldSeparator, string columnMapString = "")
        {
            var dataTable = GenerateOutputData(columnMapString);
            StringBuilder output = new StringBuilder();
            for (int rowNum = 0;rowNum<dataTable.Count;rowNum++) {
                string newList = string
                    .Join(fieldSeparator,dataTable[rowNum]
                    .Select(x => string.Format("\"{0}\"", x))
                    .ToList());
                output.AppendLine(newList);
            }
            return output.ToString();
        }
    }
}