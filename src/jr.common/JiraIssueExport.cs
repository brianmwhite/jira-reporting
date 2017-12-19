using System.Collections.Generic;
using jr.common.Models;

namespace jr.common
{
    public class JiraIssueExport
    {
        public static string GenerateSummaryText(IEnumerable<Issue> issues, OutputUtils.OutputFormat format)
        {
            var dataTable = GenerateOutputData(issues);
            return OutputUtils.CreateOutputString(format, dataTable);
        }
        
        public static List<string[]> GenerateOutputData(IEnumerable<Issue> issues)
        {
            var stringCollection = new List<string[]>();
            foreach (var issue in issues)
            {
                var row = new List<string>
                {
                    issue.Epic,
                    issue.IssueKey,
                    issue.IssueName,
                    issue.CombinedIssueName,
                    issue.IssueType,
                    issue.Status,
                    issue.Team,
                    issue.Version,
                    issue.Sprint,
                    issue.StoryPoints.ToString()
                };
                stringCollection.Add(row.ToArray());
            }
            return stringCollection;
        }
    }
}