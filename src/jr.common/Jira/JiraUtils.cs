using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using jr.common.Jira.Models;
using jr.common.Models;
using Newtonsoft.Json;

namespace jr.common.Jira
{
    public static class JiraUtils
    {
        public static double ConvertSecondsToHours(long seconds)
        {
            return seconds > 0 ? seconds / 60.0 / 60.0 : 0;
        }

        public static int[] GetPagingStartPageNumbers(int total, int maxResults)
        {
            var results = new List<int>();
            for (var x = maxResults; x < total; x += maxResults)
            {
                results.Add(x);
            }
            return results.ToArray();
        }

        public static string ExtractSprintName(string sprintObject)
        {
            const string pattern = @",name=(.*)?,goal";
            string sprintName = string.Empty;
        
            var m = Regex.Match(sprintObject, pattern);
            if (m.Success)
            {
                sprintName = m.Groups[1].Value;
            }
            return sprintName;
        }

        public static JiraIssueResults DeserializeJiraIssueResults(string json)
        {
            var tp = JsonConvert.DeserializeObject<JiraIssueResults>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            return tp;
        }

        public static (string IssueKey, string IssueName) GetParentIssueFields(JiraIssue ji)
        {
            var issueKey = string.Empty;
            var issueName = string.Empty;

            if (ji.Fields.Parent?.Fields != null)
            {
                issueKey = ji.Fields.Parent.Key;
                issueName = ji.Fields.Parent.Fields.Summary;
            }

            return (IssueKey: issueKey, IssueName: issueName);
        }

        public static IEnumerable<Issue> ConvertJiraIssueResults(IEnumerable<JiraIssueResults> jir) {
            var jrItems = new List<Issue>();
            foreach (var jiraResults in jir)
            {
                foreach (var jiraItem in jiraResults.Issues) {
                    var jrItem = new Issue
                    {
                        IssueKey = jiraItem.Key,
                        IssueName = jiraItem.Fields?.Summary,
                        IssueType = jiraItem.Fields?.Issuetype?.Name,
                        Epic = jiraItem.Fields?.EpicName,
                        StoryPoints = jiraItem.Fields?.StoryPoints,
                        Team = jiraItem.Fields?.Team,
                        Version = jiraItem.Fields?.FixVersions?.FirstOrDefault()?.Name ?? string.Empty,
                        Sprint = ExtractSprintName(jiraItem.Fields?.SprintObjects?.FirstOrDefault() ?? string.Empty),
                        Status = jiraItem.Fields?.Status.Name
                    };
                    jrItems.Add(jrItem);
                }
            }
            return jrItems;
        }
    }
}