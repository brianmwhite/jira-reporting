using System.Collections.Generic;
using jr.common.Jira.Models;
using jr.common.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public class JiraApi
    {
        private readonly string _pwd;
        private readonly string _url;
        private readonly string _user;

        public JiraApi(string url, string user, string pwd)
        {
            _pwd = pwd;
            _user = user;
            _url = url;
        }

        private JiraIssue GetJiraIssue(long issueId)
        {
            var client = new RestClient(_url + "/rest/api/2")
            {
                Authenticator = new HttpBasicAuthenticator(_user, _pwd)
            };

            var request = new RestRequest("issue/{issueId}", Method.GET);
            request.AddUrlSegment("issueId", issueId);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<JiraIssue>(response.Content,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                }
            );
        }

        private JiraProject GetJiraProject(long projectId)
        {
            var client = new RestClient(_url + "/rest/api/2/")
            {
                Authenticator = new HttpBasicAuthenticator(_user, _pwd)
            };

            var request = new RestRequest("project/{projectIdOrKey}", Method.GET);
            request.AddUrlSegment("projectIdOrKey", projectId);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<JiraProject>(response.Content,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
        }

        private IEnumerable<JiraIssueResults> GetJiraIssuesFromProject(string project)
        {
            var resultsCollection = new List<JiraIssueResults>();

            string jql = $"project={project}";
            string initialJson = GetIssueSearchResultsPaginated(jql, 0, 0);
            JiraIssueResults firstResult = JiraUtils.DeserializeJiraIssueResults(initialJson);
            int maxResults = firstResult.MaxResults;

            var startAtPages = JiraUtils.GetPagingStartPageNumbers(firstResult.Total, maxResults);

            resultsCollection.Add(firstResult);

            foreach (var item in startAtPages)
            {
                var json = GetIssueSearchResultsPaginated(jql, item, maxResults);
                var result = JiraUtils.DeserializeJiraIssueResults(json);
                resultsCollection.Add(result);
            }

            return resultsCollection;
        }

        private string GetIssueSearchResultsPaginated(string jql, int startAt, int maxResults)
        {
            var client = new RestClient(_url + "/rest/api/2") {Authenticator = new HttpBasicAuthenticator(_user, _pwd)};
            var request = new RestRequest("search", Method.GET);
            request.AddParameter("jql", jql);
            request.AddParameter("fields",
                "id,key,summary,issuetype,priority,created,fixVersions,status,customfield_10008,customfield_11600,customfield_10004");
            request.AddParameter("startAt", startAt);
            if (maxResults > 0)
            {
                request.AddParameter("maxResults", maxResults);
            }

            var response = client.Execute(request);
            return response.Content;
        }

        public IEnumerable<WorkItem> GetWorkItems(string dateFrom, string dateTo, string accountKey,
            bool getParentIssue = false)
        {
            var twi = GetTempoWorkItems(dateFrom, dateTo, accountKey);
            return ConvertTempoWorkItems(twi, getParentIssue);
        }

        private IEnumerable<TempoWorkItem> GetTempoWorkItems(string dateFrom, string dateTo, string accountKey)
        {
            var client = new RestClient(_url + "/rest/tempo-timesheets/3/")
            {
                Authenticator = new HttpBasicAuthenticator(_user, _pwd)
            };

            var request = new RestRequest("worklogs", Method.GET);
            request.AddQueryParameter("dateFrom", dateFrom);
            request.AddQueryParameter("dateTo", dateTo);
            request.AddQueryParameter("accountKey", accountKey);

            var response = client.Execute(request);

            var arr = JsonConvert.DeserializeObject<TempoWorkItem[]>(response.Content,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );

            var twi = new List<TempoWorkItem>();
            twi.AddRange(arr);
            return twi;
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
                    billedHours = JiraUtils.ConvertSecondsToHours(item.BilledSeconds),
                    userName = item.Author.Name
                };

                if (projectLookup.ContainsKey(item.TempoIssue.ProjectId))
                {
                    w.project = projectLookup.GetValueOrDefault(item.TempoIssue.ProjectId);
                }
                else
                {
                    string projectName = GetJiraProject(item.TempoIssue.ProjectId)?.Name ?? string.Empty;
                    projectLookup.Add(item.TempoIssue.ProjectId, projectName);
                    w.project = projectName;
                }

                if (getParentIssue && item.TempoIssue.IssueType.Name == "Sub-task")
                {
                    var ji = GetJiraIssue(item.TempoIssue.Id);
                    (w.issueKey, w.issueName) = JiraUtils.GetParentIssueFields(ji);
                }

                wi.Add(w);
            }

            return wi;
        }

        public IEnumerable<Issue> GetIssuesFromProject(string project)
        {
            return JiraUtils.ConvertJiraIssueResults(GetJiraIssuesFromProject(project));
        }
    }
}