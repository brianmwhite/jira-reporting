using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using jr.common.Jira.Models;
using jr.common.Models;
using Newtonsoft.Json;

namespace jr.common.Jira
{
    public interface IJiraServices
    {
        IEnumerable<WorkItem> GetWorkItems(string dateFrom, string dateTo, string accountKey, bool getParentIssue = false);
        IEnumerable<Issue> GetIssuesFromProject(string project);
    }

    public class JiraServices : IJiraServices
    {
        private IJiraApi JiraApi;

        public JiraServices(IJiraApi jiraApi)
        {
            JiraApi = jiraApi;
        }

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

        
        public IEnumerable<WorkItem> GetWorkItems(string dateFrom, string dateTo, string accountKey, bool getParentIssue = false)
        {
            return ConvertTempoWorkItems(JiraApi.GetTempoWorkItems(dateFrom, dateTo, accountKey), getParentIssue);
        }
        
        public IEnumerable<Issue> GetIssuesFromProject(string project)
        {
            return ConvertJiraIssueResults(JiraApi.GetJiraIssuesFromProject(project));
        }
        
        private IEnumerable<WorkItem> ConvertTempoWorkItems(IEnumerable<TempoWorkItem> twi, bool getParentIssue = false)
        {
            var wi = new List<WorkItem>();
            var projectLookup = new Dictionary<long, string>();
            foreach (TempoWorkItem item in twi)
            {
                var w = new WorkItem
                {
                    issueKey = item.TempoIssue.Key,
                    issueName = item.TempoIssue.Summary,
                    billedHours = JiraServices.ConvertSecondsToHours(item.BilledSeconds),
                    userName = item.Author.Name
                };

                if (projectLookup.ContainsKey(item.TempoIssue.ProjectId))
                {
                    w.project = projectLookup.GetValueOrDefault(item.TempoIssue.ProjectId);
                }
                else
                {
                    string projectName = JiraApi.GetJiraProject(item.TempoIssue.ProjectId)?.Name ?? string.Empty;
                    projectLookup.Add(item.TempoIssue.ProjectId, projectName);
                    w.project = projectName;
                }

                if (getParentIssue && item.TempoIssue.IssueType.Name == "Sub-task")
                {
                    var ji = JiraApi.GetJiraIssue(item.TempoIssue.Id);
                    if (ji.Fields.Parent?.Fields != null)
                    {
                        w.issueKey = ji.Fields.Parent.Key;
                        w.issueName = ji.Fields.Parent.Fields.Summary;
                    }
                }

                wi.Add(w);
            }

            return wi;
        }

        private IEnumerable<Issue> ConvertJiraIssueResults(IEnumerable<JiraIssueResults> jir) {
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
