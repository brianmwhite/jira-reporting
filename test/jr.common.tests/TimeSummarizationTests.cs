using Xunit;
using System.Linq;

namespace jr.common.tests
{
    public class TimeSummarizationTests
    {
        [Fact]
        public void SummarizeWorkItemRowsGroupByProjectTest()
        {
            var wi = SetupData.CreateWorkItemList();
            
            string[] mgmt = { "bwall" };
            var timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
            );
            
            var si = timeSummarizationData.SummarizeWorkItems(wi);
            Assert.Equal(4, si.Count);
            Assert.Equal(92, si.Sum(x => x.dev_hours));
            Assert.Equal(5.5, si.Sum(x => x.mgmt_hours));
            Assert.Equal(97.5, si.Sum(x => x.total_hours));
        }
        
        [Fact]
        public void SummarizeWorkItemRowsGroupByIssueTest()
        {
            var wi = SetupData.CreateWorkItemList();
            
            string[] mgmt = { "bwall" };
            var timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Issue,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
                , groupByString: "issue"
            );
            
            var si = timeSummarizationData.SummarizeWorkItems(wi);
            Assert.Equal(29, si.Count);
            Assert.Equal(92, si.Sum(x => x.dev_hours));
            Assert.Equal(5.5, si.Sum(x => x.mgmt_hours));
            Assert.Equal(97.5, si.Sum(x => x.total_hours));
        }

        [Fact]
        public void SummarizedItemsToStringArrayTest()
        {
            var si = SetupData.CreateSummarizedItemListWithTotals();
            
            string[] mgmt = { "bwall" };
            var timeSummarizationData = new TimeSummarization(
                devRate: 0
                , mgmtRate: 0
                , mgmtUsers: mgmt
                , splitPo: false
                , projectTextToTrim: ""
                , outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
            );
            
            var strOutput = timeSummarizationData.GenerateOutputData(si);

            var expectedOutput = SetupData.CreateStringArrayListWithProjectGrouping();
            Assert.Equal(expectedOutput, strOutput);
        }

        [Fact]
        public void TabSeparatedOutputTest()
        {
            var strOutput = SetupData.CreateStringArrayListWithProjectGrouping();
            var output = TimeSummarization.GenerateSeparatedValueTextOutput(strOutput, '\t');
            var expectedOutput = TestUtils.GetTextFromResource("sample-tab-output.txt");

            Assert.Equal(expectedOutput, output);
        }
    }
}
