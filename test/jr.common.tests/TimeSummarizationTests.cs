using System.Collections.Generic;
using Xunit;
using System.Linq;
using jr.common.Models;

namespace jr.common.tests
{
    public class TimeSummarizationTests
    {
        [Fact]
        public void SummarizeWorkItems_GroupByProject_Test()
        {
            List<WorkItem> wi = SetupData.CreateWorkItemList();
            
            string[] mgmt = { "bwall" };
            TimeSummarization timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
                , groupByString: "project"
            );
            
            List<SummarizedItem> si = timeSummarizationData.SummarizeWorkItems(wi);
            Assert.Equal(4, si.Count);
            Assert.Equal(92, si.Sum(x => x.dev_hours));
            Assert.Equal(5.5, si.Sum(x => x.mgmt_hours));
            Assert.Equal(97.5, si.Sum(x => x.total_hours));
        }
        
        [Fact]
        public void SummarizeWorkItem_GroupByIssue_Test()
        {
            List<WorkItem> wi = SetupData.CreateWorkItemList();
            
            string[] mgmt = { "bwall" };
            TimeSummarization timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Issue,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
                , groupByString: "issue"
            );
            
            List<SummarizedItem> si = timeSummarizationData.SummarizeWorkItems(wi);
            
            Assert.Equal(29, si.Count);
            Assert.Equal(92, si.Sum(x => x.dev_hours));
            Assert.Equal(5.5, si.Sum(x => x.mgmt_hours));
            Assert.Equal(97.5, si.Sum(x => x.total_hours));
        }

        [Fact]
        public void SummarizedItem_GroupByProject_ToStringArray_Test()
        {
            List<SummarizedItem> si = SetupData.CreateSummarizedItem_List_GroupByProject_WithTotals();
            
            string[] mgmt = { "bwall" };
            TimeSummarization timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
                , groupByString: "project"
            );
            
            List<string[]> strOutput = timeSummarizationData.GenerateOutputData(si);

            List<string[]> expectedOutput = SetupData.CreateStringArrayList_GroupByProject();
            Assert.Equal(expectedOutput, strOutput);
        }
        
        [Fact]
        public void SummarizedItem_GroupyByIssue_ToStringArray_Test()
        {
            List<SummarizedItem> si = SetupData.CreateSummarizedItemList_GroupByIssue_WithTotals();
            
            string[] mgmt = { "bwall" };
            TimeSummarization timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Issue,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
                , groupByString: "issue"
            );
            
            List<string[]> strOutput = timeSummarizationData.GenerateOutputData(si);

            List<string[]> expectedOutput = SetupData.CreateStringArrayList_GroupByIssue();
            Assert.Equal(expectedOutput, strOutput);
        }

        [Fact]
        public void AddSummarizedTotalTest()
        {
            var ts = new TimeSummarization(10,20,null,false,string.Empty);
            var rows = new List<SummarizedItem>
            {
                new SummarizedItem {project = "project name", dev_hours = 100, mgmt_hours = 5, issue = string.Empty},
                new SummarizedItem {project = "project name", dev_hours = 200, mgmt_hours = 10, issue = string.Empty},
                new SummarizedItem {project = "project name", dev_hours = 50, mgmt_hours = 40, issue = string.Empty},
                new SummarizedItem {project = "project name", dev_hours = 350, mgmt_hours = 25, issue = string.Empty},
                new SummarizedItem {project = "project name", dev_hours = 300, mgmt_hours = 20, issue = string.Empty}
            };

            var totalRow = ts.AddSummarizedTotal(rows);
            var expectedTotalRow = new SummarizedItem
            {
                project = "project name",
                dev_hours = 1000,
                mgmt_hours = 100,
                dev_rate = 10,
                mgmt_rate = 20,
                issue = string.Empty
            };
            
            Assert.Equal(expectedTotalRow.dev_hours, totalRow.dev_hours);
            Assert.Equal(expectedTotalRow.dev_amount, totalRow.dev_amount);
            Assert.Equal(expectedTotalRow.mgmt_hours, totalRow.mgmt_hours);
            Assert.Equal(expectedTotalRow.mgmt_amount, totalRow.mgmt_amount);
        }
    }
}
