using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using jr.common.Jira.Models;
using jr.common.Models;

namespace jr.common.Jira
{
    public interface IJiraServices
    {
        IEnumerable<WorkItem> GetWorkItems(string dateFrom, string dateTo, string accountKey, string[] labelWhiteList,
            bool getParentIssue = false);
        IEnumerable<Issue> GetIssuesFromProject(string project);
    }

    public class JiraServices : IJiraServices
    {
        private readonly IJiraApi _jiraApi;

        public JiraServices(IJiraApi jiraApi)
        {
            _jiraApi = jiraApi;
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

        public static string ExtractSingleLabel(string[] whiteList, string[] labels)
        {
            if (whiteList == null || labels == null) return string.Empty;
            foreach (string s in whiteList)
            {
                string result = Array.Find(labels, x => x.Equals(s));
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }
            return string.Empty;
        }
        
        public IEnumerable<WorkItem> GetWorkItems(string dateFrom, string dateTo, string accountKey,
            string[] labelWhiteList, bool getParentIssue = false)
        {
            return ConvertTempoWorkItems(_jiraApi.GetTempoWorkItems(dateFrom, dateTo, accountKey), labelWhiteList, getParentIssue);
        }
        
        public IEnumerable<Issue> GetIssuesFromProject(string project)
        {
            return ConvertJiraIssueResults(_jiraApi.GetJiraIssuesFromProject(project));
        }
        
        public IEnumerable<WorkItem> ConvertTempoWorkItems(IEnumerable<TempoWorkItemResults> twr, string[] labelWhiteList, bool getParentIssue = false)
        {
            var wi = new List<WorkItem>();
            var projectLookup = new Dictionary<string, string>();
            var issueLookup = new Dictionary<string, JiraIssue>();
            foreach (TempoWorkItemResults twi in twr)
            {
                foreach (TempoWorkItem item in twi.WorkItems)
                {
                    JiraIssue issue;
                    if (issueLookup.ContainsKey(item.TempoIssue.Key))
                    {
                        issue = issueLookup.GetValueOrDefault(item.TempoIssue.Key);
                    }
                    else
                    {
                        issue = _jiraApi.GetJiraIssue(item.TempoIssue.Key);
                        issueLookup.Add(issue.Key, issue);
                    }
                
                    var w = new WorkItem
                    {
                        issueKey = item.TempoIssue.Key,
                        issueName = issue.Fields.Summary,
                        billedHours = ConvertSecondsToHours(item.BilledSeconds),
                        userName = item.Author.Name,
                        label = ExtractSingleLabel(labelWhiteList, issue.Fields.Labels)
                    };

                    if (projectLookup.ContainsKey(issue.Fields.Project.Id))
                    {
                        w.project = projectLookup.GetValueOrDefault(issue.Fields.Project.Id);
                    }
                    else
                    {
                        string projectName = _jiraApi.GetJiraProject(issue.Fields.Project.Id)?.Name ?? string.Empty;
                        projectLookup.Add(issue.Fields.Project.Id, projectName);
                        w.project = projectName;
                    }

                    if (getParentIssue && issue.Fields.Issuetype.Name == "Sub-task")
                    {
                        if (issue.Fields.Parent?.Fields != null)
                        {
                            w.issueKey = issue.Fields.Parent.Key;
                            w.issueName = issue.Fields.Parent.Fields.Summary;
                            w.label = ExtractSingleLabel(labelWhiteList, issue.Fields.Labels);
                        }
                    }
                    wi.Add(w);
                }
            }
            return wi;
        }

        public IEnumerable<Issue> ConvertJiraIssueResults(IEnumerable<JiraIssueResults> jir) {
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
