using System.Collections.Generic;
using jr.common.Models;
using Xunit;

namespace jr.common.tests
{
    public class JiraIssueExportTests
    {
        [Fact]
        public void GenerateOutputDataTest()
        {
            var issues = new List<Issue>
            {
                new Issue
                {
                    Epic = "epic", IssueKey = "issuekey", IssueName = "issuename", IssueType = "issuetype", 
                    Status = "status",  Team = "team", Version = "version", Sprint = "sprint", StoryPoints = 3
                },
                new Issue
                {
                    Epic = "epic", IssueKey = "issuekey", IssueName = "issuename", IssueType = "issuetype", 
                    Status = "status", Team = "team", Version = "version", Sprint = "sprint", StoryPoints = 5
                },
                new Issue
                {
                    Epic = "epic", IssueKey = "issuekey", IssueName = "issuename", IssueType = "issuetype", 
                    Status = "status", Team = "team", Version = "version", Sprint = "sprint", StoryPoints = 8
                },
                new Issue
                {
                    Epic = "epic", IssueKey = "issuekey", IssueName = "issuename", IssueType = "issuetype", 
                    Status = "status", Team = "team", Version = "version", Sprint = "sprint", StoryPoints = 13
                },
                new Issue
                {
                    Epic = "epic", IssueKey = "issuekey", IssueName = "issuename", IssueType = "issuetype", 
                    Status = "status", Team = "team", Version = "version", Sprint = "sprint", StoryPoints = 21
                }
            };

            List<string[]> output = JiraIssueExport.GenerateOutputData(issues);
            var expectedOutput = new List<string[]>
            {
                new[] {"Epic", "Issue Key", "Issue Summary", "Issue", "Issue Type", "Status", "Team", "Version", "Sprint", "Story Points"},
                new[] {"epic", "issuekey", "issuename", "[issuekey] issuename", "issuetype", "status", "team", "version", "sprint", "3"},
                new[] {"epic", "issuekey", "issuename", "[issuekey] issuename", "issuetype", "status", "team", "version", "sprint", "5"},
                new[] {"epic", "issuekey", "issuename", "[issuekey] issuename", "issuetype", "status", "team", "version", "sprint", "8"},
                new[] {"epic", "issuekey", "issuename", "[issuekey] issuename", "issuetype", "status", "team", "version", "sprint", "13"},
                new[] {"epic", "issuekey", "issuename", "[issuekey] issuename", "issuetype", "status", "team", "version", "sprint", "21"}
            };

            Assert.Equal(expectedOutput, output);
        }
    }
}