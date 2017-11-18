using System.Collections.Generic;
using Xunit;

namespace jr.common.tests
{
    public class ClassPropertyTests
    {
        [Fact]
        public void WorkItemCombinedIssueNameTest()
        {
            WorkItem wi = new WorkItem();
            wi.issueKey = "ABC-123";
            wi.issueName = "This is my abc issue";
            Assert.Equal("[ABC-123] This is my abc issue", wi.combinedIssueName);
        }

        [Fact]
        public void SummarizedItemPropertyTest()
        {
            SummarizedItem si = new SummarizedItem(_splitPO: true, _projectTextToTrim: "Trim this - ");
            si.project = "Trim this - Project 1 #ABC123";
            Assert.Equal("Project 1 #ABC123", si.project);
            Assert.Equal("Project 1", si.billing_project);
            Assert.Equal("ABC123", si.billing_code);

            si.dev_rate = 4;
            si.mgmt_rate = 5;

            si.dev_hours = 23;
            si.mgmt_hours = 4;

            Assert.Equal(92, si.dev_amount);
            Assert.Equal(20, si.mgmt_amount);

            Assert.Equal(27, si.total_hours);
            Assert.Equal(112, si.total_amount);
        }
    }
}
