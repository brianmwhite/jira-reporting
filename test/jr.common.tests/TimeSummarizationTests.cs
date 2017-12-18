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
        public void GenerateSeparatedValueTextOutput_GroupByProject_Test()
        {
            List<string[]> strOutput = SetupData.CreateStringArrayList_GroupByProject();
            string output = OutputUtils.GenerateSeparatedValueTextOutput(strOutput, '\t');
            string expectedOutput = TestUtils.GetTextFromResource("sample-tab-output-project.txt");

            Assert.Equal(expectedOutput, output);
        }
        
        [Fact]
        public void GenerateSeparatedValueTextOutput_GroupByIssue_Test()
        {
            List<string[]> strOutput = SetupData.CreateStringArrayList_GroupByIssue();
            string output = OutputUtils.GenerateSeparatedValueTextOutput(strOutput, '\t');
            string expectedOutput = TestUtils.GetTextFromResource("sample-tab-output-issue.txt");

            Assert.Equal(expectedOutput, output);
        }
    }
}
