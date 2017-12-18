using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Issue = jr.common.Models.Issue;

namespace jr.common.Jira
{
    public class JiraApi
    {
        protected readonly string Pwd;
        protected readonly string Url;
        protected readonly string User;

        public JiraApi(string url, string user, string pwd)
        {
            Pwd = pwd;
            User = user;
            Url = url;
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

        public string _GetJiraIssueJson(long issueId)
        {
            var client = new RestClient(Url + "/rest/api/2");
            client.Authenticator = new HttpBasicAuthenticator(User, Pwd);

            var request = new RestRequest("issue/{issueId}", Method.GET);
            request.AddUrlSegment("issueId", issueId);

            var response = client.Execute(request);
            return response.Content;
        }

        private static JiraIssue _DeserializeJiraIssue(string json)
        {
            var tp = JsonConvert.DeserializeObject<JiraIssue>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                }
            );
            return tp;
        }
        
        public JiraIssue GetJiraIssue(long issueId)
        {
            var json = _GetJiraIssueJson(issueId);
            var ji = _DeserializeJiraIssue(json);
            return ji;
        }

        public (string IssueKey, string IssueName) GetParentIssue(long issueId)
        {
            var ji = GetJiraIssue(issueId);
            return _GetParentIssueFields(ji);
        }

        public (string IssueKey, string IssueName) _GetParentIssueFields(JiraIssue ji)
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
        
        public string _GetJiraProjectJson(long projectId)
        {
            var client = new RestClient(Url + "/rest/api/2/");
            client.Authenticator = new HttpBasicAuthenticator(User, Pwd);

            var request = new RestRequest("project/{projectIdOrKey}", Method.GET);
            request.AddUrlSegment("projectIdOrKey", projectId);

            var response = client.Execute(request);
            return response.Content;
        }
        
        public static JiraProject _DeserializeJiraProject(string json)
        {
            var tp = JsonConvert.DeserializeObject<JiraProject>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            return tp;
        }
        
        public JiraProject GetJiraProject(long projectId)
        {
            var json = _GetJiraProjectJson(projectId);
            var jp = _DeserializeJiraProject(json);
            return jp;
        }

        public string GetProjectName(long projectId)
        {
            var jp = GetJiraProject(projectId);
            var projectName = jp.Name;
            return projectName;
        }

        public List<Issue> GetItems(string project)
        {
            var jir = _GetJiraIssueResults(project);
            var ir = _ConvertJiraIssueResults(jir);
            return ir;
        }
        
        public List<Issue> _ConvertJiraIssueResults(IEnumerable<JiraIssueResults> jir) {
            var jrItems = new List<Issue>();
            foreach (var jiraResults in jir)
            {
                foreach (var jiraItem in jiraResults.Issues) {
                    var jrItem = new Issue();
                    jrItem.IssueKey = jiraItem.Key;
                    jrItem.IssueName = jiraItem.Fields?.Summary;
                    jrItem.IssueType = jiraItem.Fields?.Issuetype?.Name;
                    jrItem.Epic = jiraItem.Fields?.EpicName;
                    jrItem.StoryPoints = jiraItem.Fields?.StoryPoints;
                    jrItem.Team = jiraItem.Fields?.Team;
                    jrItem.Version = jiraItem.Fields?.FixVersions?.FirstOrDefault()?.Name ?? string.Empty;
                    jrItem.Sprint = ExtractSprintName(jiraItem.Fields?.SprintObjects?.FirstOrDefault() ?? string.Empty);
                    jrItem.Status = jiraItem.Fields?.Status.Name;
                    jrItems.Add(jrItem);
                }
            }
            return jrItems;
        }

        public static string ExtractSprintName(string sprintObject)
        {
            const string pattern = @",name=(.*)?,goal";
            string sprintName = string.Empty;
        
            var m = Regex.Match(sprintObject, pattern);
            if (m.Success)
            {
                sprintName = m.Groups[1]?.Value;
            }
            return sprintName;
        }

        public List<JiraIssueResults> _GetJiraIssueResults(string project) {
            var resultsCollection = new List<JiraIssueResults>();

            string jql = $"project={project}";
            string initialJson = _GetIssueResultsJson(jql, 0, 0);
            JiraIssueResults firstResult = _DeserializeJiraIssueResults(initialJson);
            int maxResults = firstResult.MaxResults;

            var startAtPages = GetPagingStartPageNumbers(firstResult.Total, maxResults);

            resultsCollection.Add(firstResult);

            foreach (var item in startAtPages)
            {
                var json = _GetIssueResultsJson(jql, item, maxResults);
                var result = _DeserializeJiraIssueResults(json);
                resultsCollection.Add(result);
            }

            return resultsCollection;
        }

        public static JiraIssueResults _DeserializeJiraIssueResults(string json)
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

        public string _GetIssueResultsJson(string jql, int startAt, int maxResults)
        {
            var client = new RestClient(Url + "/rest/api/2");
            client.Authenticator = new HttpBasicAuthenticator(User, Pwd);
            var request = new RestRequest("search", Method.GET);
            request.AddParameter("jql", jql);
            request.AddParameter("fields", "id,key,summary,issuetype,priority,created,fixVersions,status,customfield_10008,customfield_11600,customfield_10004");
            request.AddParameter("startAt", startAt);
            if (maxResults > 0) 
            {
                request.AddParameter("maxResults", maxResults);
            }
            var response = client.Execute(request);
            return response.Content;
        }
    }
}