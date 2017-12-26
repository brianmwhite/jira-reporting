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
    }
}