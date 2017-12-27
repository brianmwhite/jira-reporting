using System.Collections.Generic;
using jr.common.Excel;
using jr.common.Models;
using jr.common.tests.TestSetup;
using Xunit;

namespace jr.common.tests.Excel
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
