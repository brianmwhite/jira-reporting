using jr.common.Models;
using Xunit;

namespace jr.common.tests
{
    public class IssueTests
    {
        [Fact]
        public void IssueCombinedIssueNamePropertyTest()
        {
            var i = new Issue
            {
                IssueKey = "ABC-123",
                IssueName = "This is my issue name"
            };
            const string expectedCombinedIssueName = "[ABC-123] This is my issue name";
            Assert.Equal(expectedCombinedIssueName, i.CombinedIssueName);
        }
    }
}