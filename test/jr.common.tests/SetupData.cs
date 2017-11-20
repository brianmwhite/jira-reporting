using System.Collections.Generic;

namespace jr.common.tests
{
    public class SetupData
    {
        public static List<WorkItem> CreateWorkItemList()
        {
            // Used in the ExcelInput.cs to generate data
            // testOutput.AppendLine(string.Format("workItems.Add(new WorkItem() {{project = \"{0}\", billedHours = {1}, userName = \"{2}\", issueKey = \"{3}\", issueName = \"{4}\"}});",reader.GetString(project_column_index),reader.GetDouble(billed_hours_column_index),reader.GetString(username_column_index),reader.GetString(issue_key_name_index),reader.GetString(issue_column_name_index)));
            // System.Diagnostics.Debug.Write(testOutput.ToString());

            List<WorkItem> workItems = new List<WorkItem>();
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "amorrison", issueKey = "ABC-462", issueName = "placerat eget," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 1.5, userName = "sward", issueKey = "CDE-905", issueName = "tincidunt. Donec vitae erat vel" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 3, userName = "bgrant", issueKey = "CDE-905", issueName = "tincidunt. Donec vitae erat vel" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "amorrison", issueKey = "ABC-461", issueName = "Donec tempus, lorem fringilla" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1.5, userName = "dramos", issueKey = "ABC-461", issueName = "Donec tempus, lorem fringilla" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "amorrison", issueKey = "ABC-461", issueName = "Donec tempus, lorem fringilla" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "vburgess", issueKey = "ABC-461", issueName = "Donec tempus, lorem fringilla" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 1, userName = "sward", issueKey = "CDE-906", issueName = "Nunc ullamcorper," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 3, userName = "bgrant", issueKey = "CDE-906", issueName = "Nunc ullamcorper," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 2, userName = "bgrant", issueKey = "CDE-906", issueName = "Nunc ullamcorper," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 4, userName = "amorrison", issueKey = "ABC-463", issueName = "non, luctus sit amet, faucibus" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 0.5, userName = "amorrison", issueKey = "BCD-16", issueName = "ut, molestie in, tempus" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 4, userName = "dramos", issueKey = "ABC-444", issueName = "hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 2, userName = "dramos", issueKey = "ABC-444", issueName = "hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 2, userName = "dramos", issueKey = "ABC-444", issueName = "hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1.5, userName = "dramos", issueKey = "ABC-444", issueName = "hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 3, userName = "dramos", issueKey = "ABC-444", issueName = "hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "vburgess", issueKey = "ABC-445", issueName = "dolor. Quisque tincidunt pede ac urna. Ut" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "vburgess", issueKey = "ABC-448", issueName = "Aliquam adipiscing lobortis risus. In mi pede, nonummy" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "vburgess", issueKey = "CDE-844", issueName = "risus. Donec egestas. Aliquam nec" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 1.5, userName = "vburgess", issueKey = "BCD-250", issueName = "pellentesque a, facilisis non, bibendum" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 2, userName = "dramos", issueKey = "BCD-250", issueName = "pellentesque a, facilisis non, bibendum" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 2, userName = "vburgess", issueKey = "BCD-250", issueName = "pellentesque a, facilisis non, bibendum" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 5, userName = "amorrison", issueKey = "ABC-446", issueName = "semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 2.5, userName = "amorrison", issueKey = "ABC-446", issueName = "semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "amorrison", issueKey = "ABC-446", issueName = "semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "vburgess", issueKey = "ABC-446", issueName = "semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 2, userName = "dramos", issueKey = "ABC-443", issueName = "enim nisl" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1.5, userName = "amorrison", issueKey = "ABC-443", issueName = "magna tellus faucibus leo, in lobortis tellus justo" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "dramos", issueKey = "ABC-456", issueName = "at lacus. Quisque purus sapien," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 6, userName = "dramos", issueKey = "ABC-456", issueName = "at lacus. Quisque purus sapien," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "vburgess", issueKey = "CDE-872", issueName = "sagittis. Nullam vitae diam. Proin" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "vburgess", issueKey = "CDE-897", issueName = "ac metus vitae velit egestas lacinia. Sed congue," });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "vburgess", issueKey = "ABC-415", issueName = "fames ac turpis" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1.5, userName = "amorrison", issueKey = "ABC-415", issueName = "fames ac turpis" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 1, userName = "vburgess", issueKey = "CDE-886", issueName = "dignissim. Maecenas ornare egestas" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "vburgess", issueKey = "ABC-459", issueName = "vitae velit egestas lacinia." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "amorrison", issueKey = "ABC-459", issueName = "vitae velit egestas lacinia." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "amorrison", issueKey = "ABC-460", issueName = "tempor lorem, eget mollis lectus" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "amorrison", issueKey = "ABC-460", issueName = "tempor lorem, eget mollis lectus" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 2, userName = "bgrant", issueKey = "CDE-903", issueName = "sociis natoque" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 2, userName = "bgrant", issueKey = "CDE-903", issueName = "sociis natoque" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "vburgess", issueKey = "CDE-903", issueName = "sociis natoque" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 2, userName = "bgrant", issueKey = "CDE-904", issueName = "erat. Vivamus nisi. Mauris nulla. Integer" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "vburgess", issueKey = "CDE-904", issueName = "erat. Vivamus nisi. Mauris nulla. Integer" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 0.5, userName = "vburgess", issueKey = "BCD-253", issueName = "cursus, diam at pretium aliquet, metus urna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 1, userName = "dramos", issueKey = "BCD-253", issueName = "cursus, diam at pretium aliquet, metus urna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "bwall", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "bwall", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "bwall", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "bwall", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "bwall", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 0.5, userName = "sward", issueKey = "ABC-345", issueName = "orci. Donec nibh." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "amorrison", issueKey = "ABC-419", issueName = "Nullam ut nisi a odio semper cursus." });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "bwall", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 2, userName = "sward", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "bwall", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 1, userName = "sward", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 1, userName = "sward", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 0.5, userName = "bwall", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 3 #ABC00008", billedHours = 1, userName = "sward", issueKey = "CDE-263", issueName = "dictum augue malesuada malesuada. Integer id magna" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 2, userName = "dramos", issueKey = "BCD-106", issueName = "turpis egestas. Aliquam fringilla" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 2 #ABC00006", billedHours = 2.5, userName = "dramos", issueKey = "BCD-106", issueName = "turpis egestas. Aliquam fringilla" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 1 #ABC00001", billedHours = 1, userName = "vburgess", issueKey = "ABC-423", issueName = "lacus. Aliquam rutrum lorem" });
            workItems.Add(new WorkItem() { project = "Alpha Beta - Project 4 #ABC00004", billedHours = 0.5, userName = "vburgess", issueKey = "EFG-26", issueName = "faucibus ut," });
            return workItems;
        }
    }
}