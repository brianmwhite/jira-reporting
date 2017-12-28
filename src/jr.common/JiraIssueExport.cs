using System.Collections.Generic;
using jr.common.Models;

namespace jr.common
{
    public static class JiraIssueExport
    {
        public static string GenerateSummaryText(IEnumerable<Issue> issues, OutputUtils.OutputFormat format)
        {
            var dataTable = GenerateOutputData(issues);
            return OutputUtils.CreateOutputString(format, dataTable);
        }

        public static List<string[]> GenerateOutputData(IEnumerable<Issue> issues)
        {
            var stringCollection = new List<string[]>();

            stringCollection.Add(new[]
            {
                "Epic", "Issue Key", "Issue Summary", "Issue", "Issue Type", "Status", "Team", "Version", "Sprint",
                "Story Points"
            });

            foreach (var issue in issues)
            {
                var row = new List<string>
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
                stringCollection.Add(row.ToArray());
            }

            return stringCollection;
        }
    }
}