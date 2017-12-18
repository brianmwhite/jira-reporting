using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using jr.common.Models;

namespace jr.common.Excel
{
    public class ExcelInput
    {
        public ExcelInput()
        {
            /*
            https://github.com/ExcelDataReader/ExcelDataReader

            By default, ExcelDataReader throws a NotSupportedException "No data is available for encoding 1252." on .NET Core.

            To fix, add a dependency to the package System.Text.Encoding.CodePages and then add code to register the code 
            page provider during application initialization (f.ex in Startup.cs):

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            This is required to parse strings in binary BIFF2-5 Excel documents encoded with DOS-era code pages. 
            These encodings are registered by default in the full .NET Framework, but not on .NET Core.
            */
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public List<WorkItem> ReadExcel(string inputSourceLocation)
        {
            var stream = File.Open(inputSourceLocation, FileMode.Open, FileAccess.Read);
            return this.ReadExcel(stream);
        }
        public List<WorkItem> ReadExcel(Stream stream)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            string billed_hours_column_name = "Billed Hours";
            string project_column_name = "Project Name";
            string username_column_name = "Username";
            string issue_column_name = "Issue summary";
            string issue_key_name = "Issue Key";

            int username_column_index = -1;
            int project_column_index = -1;
            int billed_hours_column_index = -1;
            int issue_column_name_index = -1;
            int issue_key_name_index = -1;

            // Auto-detect format, supports:
            //  - Binary Excel files (2.0-2003 format; *.xls)
            //  - OpenXml Excel files (2007 format; *.xlsx)
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                //read first row
                int row = 0;
                while (reader.Read())
                {
                    if (row == 0)
                    {
                        //header row
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnnValue = reader.GetString(i);
                            if (columnnValue == billed_hours_column_name) { billed_hours_column_index = i; }
                            else if (columnnValue == project_column_name) { project_column_index = i; }
                            else if (columnnValue == username_column_name) { username_column_index = i; }
                            else if (columnnValue == issue_column_name) { issue_column_name_index = i; }
                            else if (columnnValue == issue_key_name) { issue_key_name_index = i; }
                        }
                    }
                    else
                    {
                        workItems.Add(new WorkItem()
                        {
                            project = reader.GetString(project_column_index),
                            billedHours = reader.GetDouble(billed_hours_column_index),
                            userName = reader.GetString(username_column_index),
                            issueKey = reader.GetString(issue_key_name_index),
                            issueName = reader.GetString(issue_column_name_index)
                        });
                    }
                    row++;
                }
            }
            return workItems;
        }
        public static List<WorkItem> ExtractWorkItemsFromExcel(string inputSourceLocation)
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