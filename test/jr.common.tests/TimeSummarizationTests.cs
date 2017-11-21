using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace jr.common.tests
{
    public class TimeSummarizationTests : IDisposable
    {
        private TimeSummarization _timeSummarizationData;
        public TimeSummarizationTests()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            string[] mgmt = { "bwall" };
            _timeSummarizationData = new TimeSummarization(
                _devRate: 0
                , _mgmtRate: 0
                , _mgmtUsers: mgmt
                , _splitPO: false
                , _projectTextToTrim: ""
                , _outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
            );
        }

        public void Dispose()
        {
            _timeSummarizationData = null;
        }

        [Fact]
        public void SummarizeWorkItemRowsTest()
        {
            List<WorkItem> wi = SetupData.CreateWorkItemList();
            List<SummarizedItem> si = _timeSummarizationData.SummarizeWorkItems(wi);
            Assert.Equal(4, si.Count);
            Assert.Equal(92, si.Sum(x => x.dev_hours));
            Assert.Equal(5.5, si.Sum(x => x.mgmt_hours));
            Assert.Equal(97.5, si.Sum(x => x.total_hours));
        }

        [Fact]
        public void SummarizedItemsToStringArrayTest()
        {
            List<SummarizedItem> si = SetupData.CreateSummarizedItemListWithTotals();
            List<string[]> strOutput = _timeSummarizationData.GenerateOutputData(si);

            List<string[]> expectedOutput = SetupData.CreateStringArrayList();
            Assert.Equal(expectedOutput, strOutput);
        }

        [Fact]
        public void TabSeparatedOutputTest()
        {
            List<string[]> strOutput = SetupData.CreateStringArrayList();
            string output = TimeSummarization.GenerateSeparatedValueTextOutput(strOutput, '\t');
            string expectedOutput = TestUtils.GetTextFromResource("sample-tab-output.txt");

            Assert.Equal(expectedOutput, output);
        }
    }
}
