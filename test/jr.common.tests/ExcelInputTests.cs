using System.Collections.Generic;
using jr.common.Excel;
using Xunit;

namespace jr.common.tests
{
    public class ExcelInputTests
    {
        [Fact]
        public void XlsConvertToWorkItemsCorrectNumRowsTest()
        {
            ExcelInput excel = new ExcelInput();
            List<WorkItem> workItems = excel.ReadExcel(TestUtils.GetStreamFromResource("sample.xls"));
            Assert.Equal(73, workItems.Count);
        }
    }
}
