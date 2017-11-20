using System.Collections.Generic;
using Xunit;

namespace jr.common.tests
{
    public class TimeSummarizationTests
    {
            // TODO: split apart this test into multiple once code is refactored
            [Fact]
            public void ExcelToTextOutputTest()
            {
                ExcelInput excel = new ExcelInput();
                List<WorkItem> workItems = excel.ReadExcel(TestUtils.GetStreamFromResource("sample.xls"));
                TimeSummarization ts = new TimeSummarization(
                  _devRate: 0
                , _mgmtRate: 0
                , _mgmtUsers: null
                , _splitPO: false
                , _projectTextToTrim: ""
                , _outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
            );
            string output = ts.GenerateSummaryText(workItems, ts);
            string expectedOutput = TestUtils.GetTextFromResource("sample-tab-output.txt");
            Assert.Equal(expectedOutput, output);
            }
    }
}