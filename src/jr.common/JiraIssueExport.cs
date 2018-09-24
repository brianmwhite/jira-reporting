using System.Collections.Generic;
using System.Linq;
using jr.common.Models;

namespace jr.common
{
    public static class JiraIssueExport
    {
        public static string GenerateSummaryText(IEnumerable<Issue> issues, OutputUtils.OutputFormat format)
        {
            var outputData = GenerateOutputData(issues);
            return OutputUtils.CreateOutputString(format, outputData);
        }

        public static List<string[]> GenerateOutputData(IEnumerable<Issue> issues)
        {
            var outputData = new List<string[]>
            {
                AddHeaderRow()
            };

            outputData.AddRange(issues.Select(AddIssueRow));

            return outputData;
        }

        private static string[] AddHeaderRow()
        {
            return new[]
            {
                "Epic", "Issue Key", "Issue Summary", "Issue", "Issue Type", "Status", "Team", "Version", "Sprint",
                "Story Points"
            };
        }
        
        private static string[] AddIssueRow(Issue issue)
        {
            var itemRow = new List<string>
            {
                issue.Epic ?? string.Empty,
                issue.IssueKey ?? string.Empty,
                issue.IssueName ?? string.Empty,
                issue.CombinedIssueName ?? string.Empty,
                issue.IssueType ?? string.Empty,
                issue.Status ?? string.Empty,
                issue.Team ?? string.Empty,
                issue.Version ?? string.Empty,
                issue.Sprint ?? string.Empty,
                issue.StoryPoints?.ToString() ?? string.Empty
            };
            return itemRow.ToArray();
        }
    }
}