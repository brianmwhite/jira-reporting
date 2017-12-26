using System;
using System.Collections.Generic;
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

        [Fact]
        public void GetTimePeriodOption_YTD_Test()
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption("ytd", today);
            Assert.Equal("2017-01-01", startDate);
            Assert.Equal("2017-12-26", endDate);
        }
        
        [Fact]
        public void GetTimePeriodOption_MONTH_Test()
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption("month", today);
            Assert.Equal("2017-12-01", startDate);
            Assert.Equal("2017-12-26", endDate);
        }
        
        [Fact]
        public void GetTimePeriodOption_LASTMONTH_Test()
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption("lastmonth", today);
            Assert.Equal("2017-11-01", startDate);
            Assert.Equal("2017-11-30", endDate);
        }

        [Fact]
        public void GetTimePeriodOption_WEEK_Test()
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption("week", today);
            Assert.Equal("2017-12-24", startDate);
            Assert.Equal("2017-12-26", endDate);
        }

        [Fact]
        public void GetTimePeriodOption_LASTWEEK_Test()
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption("lastweek", today);
            Assert.Equal("2017-12-17", startDate);
            Assert.Equal("2017-12-23", endDate);
        }

        [Fact]
        public void GetTimePeriodOption_GIBBERISH_Test()
        {
            var today = new DateTime(2017,12,26);
            (string startDate, string endDate) = OutputUtils.GetTimePeriodOption("gibberish", today);
            Assert.Equal("2017-12-01", startDate);
            Assert.Equal("2017-12-26", endDate);
        }
        
        
    }
}