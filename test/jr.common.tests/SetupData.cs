using System.Collections.Generic;

namespace jr.common.tests
{
    public static class SetupData
    {
        public static List<string[]>  CreateStringArrayList_GroupByProject() {
            List<string[]> s = new List<string[]>();
            s.Add(new[] {"Project","Dev Hours","Dev Amount","Mgmt Hours","Mgmt Amount","Total Hours","Total Amount"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","54.50","$0.00","4.00","$0.00","58.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 2 #ABC00006","12.00","$0.00","0.00","$0.00","12.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","25.00","$0.00","1.50","$0.00","26.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 4 #ABC00004","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"","92.00","$0.00","5.50","$0.00","97.50","$0.00"});
            return s;
        }
        
        public static List<string[]>  CreateStringArrayList_GroupByIssue() {
            List<string[]> s = new List<string[]>();
            s.Add(new[] {"Project","Issue","Dev Hours","Dev Amount","Mgmt Hours","Mgmt Amount","Total Hours","Total Amount"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-345] orci. Donec nibh.","5.00","$0.00","4.00","$0.00","9.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-415] fames ac turpis","2.50","$0.00","0.00","$0.00","2.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-419] Nullam ut nisi a odio semper cursus.","1.00","$0.00","0.00","$0.00","1.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-423] lacus. Aliquam rutrum lorem","1.00","$0.00","0.00","$0.00","1.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-443] enim nisl","3.50","$0.00","0.00","$0.00","3.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-444] hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit","12.50","$0.00","0.00","$0.00","12.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-445] dolor. Quisque tincidunt pede ac urna. Ut","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-446] semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam","9.00","$0.00","0.00","$0.00","9.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-448] Aliquam adipiscing lobortis risus. In mi pede, nonummy","1.00","$0.00","0.00","$0.00","1.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-456] at lacus. Quisque purus sapien,","7.00","$0.00","0.00","$0.00","7.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-459] vitae velit egestas lacinia.","1.00","$0.00","0.00","$0.00","1.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-460] tempor lorem, eget mollis lectus","1.50","$0.00","0.00","$0.00","1.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-461] Donec tempus, lorem fringilla","4.00","$0.00","0.00","$0.00","4.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-462] placerat eget,","1.00","$0.00","0.00","$0.00","1.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 1 #ABC00001","[ABC-463] non, luctus sit amet, faucibus","4.00","$0.00","0.00","$0.00","4.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 2 #ABC00006","[BCD-106] turpis egestas. Aliquam fringilla","4.50","$0.00","0.00","$0.00","4.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 2 #ABC00006","[BCD-16] ut, molestie in, tempus","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 2 #ABC00006","[BCD-250] pellentesque a, facilisis non, bibendum","5.50","$0.00","0.00","$0.00","5.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 2 #ABC00006","[BCD-253] cursus, diam at pretium aliquet, metus urna","1.50","$0.00","0.00","$0.00","1.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-263] dictum augue malesuada malesuada. Integer id magna","5.00","$0.00","1.50","$0.00","6.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-844] risus. Donec egestas. Aliquam nec","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-872] sagittis. Nullam vitae diam. Proin","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-886] dignissim. Maecenas ornare egestas","1.00","$0.00","0.00","$0.00","1.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-897] ac metus vitae velit egestas lacinia. Sed congue,","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-903] sociis natoque","4.50","$0.00","0.00","$0.00","4.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-904] erat. Vivamus nisi. Mauris nulla. Integer","2.50","$0.00","0.00","$0.00","2.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-905] tincidunt. Donec vitae erat vel","4.50","$0.00","0.00","$0.00","4.50","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 3 #ABC00008","[CDE-906] Nunc ullamcorper,","6.00","$0.00","0.00","$0.00","6.00","$0.00"});
            s.Add(new[] {"Alpha Beta - Project 4 #ABC00004","[EFG-26] faucibus ut,","0.50","$0.00","0.00","$0.00","0.50","$0.00"});
            s.Add(new[] {"","","92.00","$0.00","5.50","$0.00","97.50","$0.00"});
            return s;
        }

        public static List<SummarizedItem> CreateSummarizedItemList_GroupByIssue_WithTotals()
        {
            List<SummarizedItem> si = new List<SummarizedItem>();
            AddSummarizedItems_GroupByIssue(si);
            CreateSummarizedItem_TotalRow(si);
            return si;
        }

        private static void CreateSummarizedItem_TotalRow(List<SummarizedItem> si)
        {
            si.Add(new SummarizedItem()
            {
                project = "",
                issue = "",
                dev_rate = 0,
                mgmt_rate = 0,
                dev_hours = 92,
                mgmt_hours = 5.5
            });
        }

        public static List<SummarizedItem> CreateSummarizedItem_List_GroupByProject_WithTotals()
        {
            List<SummarizedItem> si = new List<SummarizedItem>();
            AddSummarizedItems_GroupByProject(si);
            CreateSummarizedItem_TotalRow(si);
            return si;
        }

        private static void AddSummarizedItems_GroupByProject(List<SummarizedItem> si)
        {
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", dev_rate = 0, mgmt_rate = 0, dev_hours = 54.5, mgmt_hours = 4 });
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 2 #ABC00006", dev_rate = 0, mgmt_rate = 0, dev_hours = 12, mgmt_hours = 0.00 });
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", dev_rate = 0, mgmt_rate = 0, dev_hours = 25, mgmt_hours = 1.50 });
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 4 #ABC00004", dev_rate = 0, mgmt_rate = 0, dev_hours = 0.5, mgmt_hours = 0.00 });
        }

        private static void AddSummarizedItems_GroupByIssue(List<SummarizedItem> si)
        {
            //sample output example
            //output.AppendLine($"si.Add(new SummarizedItem() {{ project = \"{item.project}\", issue = \"{item.issue}\", dev_hours = {item.dev_hours}, mgmt_hours = {item.mgmt_hours}, dev_rate = 0, mgmt_rate = 0}});");
            
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-345] orci. Donec nibh.", dev_hours = 5, mgmt_hours = 4, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-415] fames ac turpis", dev_hours = 2.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-419] Nullam ut nisi a odio semper cursus.", dev_hours = 1, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-423] lacus. Aliquam rutrum lorem", dev_hours = 1, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-443] enim nisl", dev_hours = 3.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-444] hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit", dev_hours = 12.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-445] dolor. Quisque tincidunt pede ac urna. Ut", dev_hours = 0.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-446] semper cursus. Integer mollis. Integer tincidunt aliquam arcu. Aliquam", dev_hours = 9, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-448] Aliquam adipiscing lobortis risus. In mi pede, nonummy", dev_hours = 1, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-456] at lacus. Quisque purus sapien,", dev_hours = 7, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-459] vitae velit egestas lacinia.", dev_hours = 1, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-460] tempor lorem, eget mollis lectus", dev_hours = 1.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-461] Donec tempus, lorem fringilla", dev_hours = 4, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-462] placerat eget,", dev_hours = 1, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 1 #ABC00001", issue = "[ABC-463] non, luctus sit amet, faucibus", dev_hours = 4, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 2 #ABC00006", issue = "[BCD-106] turpis egestas. Aliquam fringilla", dev_hours = 4.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 2 #ABC00006", issue = "[BCD-16] ut, molestie in, tempus", dev_hours = 0.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 2 #ABC00006", issue = "[BCD-250] pellentesque a, facilisis non, bibendum", dev_hours = 5.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 2 #ABC00006", issue = "[BCD-253] cursus, diam at pretium aliquet, metus urna", dev_hours = 1.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-263] dictum augue malesuada malesuada. Integer id magna", dev_hours = 5, mgmt_hours = 1.5, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-844] risus. Donec egestas. Aliquam nec", dev_hours = 0.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-872] sagittis. Nullam vitae diam. Proin", dev_hours = 0.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-886] dignissim. Maecenas ornare egestas", dev_hours = 1, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-897] ac metus vitae velit egestas lacinia. Sed congue,", dev_hours = 0.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-903] sociis natoque", dev_hours = 4.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-904] erat. Vivamus nisi. Mauris nulla. Integer", dev_hours = 2.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-905] tincidunt. Donec vitae erat vel", dev_hours = 4.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 3 #ABC00008", issue = "[CDE-906] Nunc ullamcorper,", dev_hours = 6, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
            si.Add(new SummarizedItem() { project = "Alpha Beta - Project 4 #ABC00004", issue = "[EFG-26] faucibus ut,", dev_hours = 0.5, mgmt_hours = 0, dev_rate = 0, mgmt_rate = 0});
        }

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