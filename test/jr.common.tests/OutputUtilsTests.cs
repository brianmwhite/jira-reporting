using System;
using System.Collections.Generic;
using jr.common.tests.TestSetup;
using Xunit;

namespace jr.common.tests
{
    public class OutputUtilsTests
    {
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

        [Fact]
        public void CreateOutputString_Tab_Test()
        {
            string output = OutputUtils.CreateOutputString(OutputUtils.OutputFormat.Tab, SetupData.CreateStringArrayList_GroupByProject());
            string expectedOutput = TestUtils.GetTextFromResource("sample-tab-output-project.txt");
            Assert.Equal(expectedOutput, output);
        }
        
        [Fact]
        public void CreateOutputString_CSV_Test()
        {
            string output = OutputUtils.CreateOutputString(OutputUtils.OutputFormat.Csv, SetupData.CreateStringArrayList_GroupByProject());
            string expectedOutput = TestUtils.GetTextFromResource("sample-csv-output-project.csv");
            Assert.Equal(expectedOutput, output);
        }

//TODO: test fails due to 1) unsupportedplatformexception in Console.WindowWidth & potentially some whitespace issues
//        [Fact]
//        public void GeneratePrettyOutputTest()
//        {
//            var data = new List<string[]>
//            {
//                new[] {"C1", "C2", "C3"},
//                new[] {"R1C1", "R1C2", "R1C3"},
//                new[] {"R2C1", "R2C2", "R2C3"},
//                new[] {"R3C1", "R3C2", "R3C3"}
//            };
//            string output = OutputUtils.GeneratePrettyOutput(data);
//            string expectedOutput = TestUtils.GetTextFromResource("sample-pretty-output.txt");
//            Assert.Equal(expectedOutput, output);
//        }

        [Theory]
        [InlineData("ytd", "2017-01-01", "2017-12-26")]
        [InlineData("month", "2017-12-01", "2017-12-26")]
        [InlineData("lastmonth", "2017-11-01", "2017-11-30")]
        [InlineData("week", "2017-12-24", "2017-12-26")]
        [InlineData("lastweek", "2017-12-17", "2017-12-23")]
        [InlineData("gibberish", "2017-12-01", "2017-12-26")]
        public void GetTimePeriodOption_YTD_Test(string timePeriod, string expectedStartDate, string expectedEndDate)
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption(timePeriod, today);
            Assert.Equal(expectedStartDate, startDate);
            Assert.Equal(expectedEndDate, endDate);
        }

        [Fact]
        public void ConvertOutputFormatTest()
        {
            Assert.Equal(OutputUtils.OutputFormat.Csv, OutputUtils.ConvertOutputFormat("csv"));
            Assert.Equal(OutputUtils.OutputFormat.Tab, OutputUtils.ConvertOutputFormat("tab"));
            Assert.Equal(OutputUtils.OutputFormat.Pretty, OutputUtils.ConvertOutputFormat("pretty"));
            Assert.Equal(OutputUtils.OutputFormat.Pretty, OutputUtils.ConvertOutputFormat("gibberish"));
        }
    }
}