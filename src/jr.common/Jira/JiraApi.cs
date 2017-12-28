using System.Collections.Generic;
using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public interface IJiraApi
    {
        JiraIssue GetJiraIssue(long issueId);
        JiraProject GetJiraProject(long projectId);
        IEnumerable<JiraIssueResults> GetJiraIssuesFromProject(string project);
        IEnumerable<TempoWorkItem> GetTempoWorkItems(string dateFrom, string dateTo, string accountKey);
    }

    public class JiraApi : IJiraApi
    {
        //TODO: use repository pattern / dependency injection?

        private readonly string _pwd;
        private readonly string _url;
        private readonly string _user;

        public JiraApi(string url, string user, string pwd)
        {
            _pwd = pwd;
            _user = user;
            _url = url;
        }

        public JiraIssue GetJiraIssue(long issueId)
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

        public JiraProject GetJiraProject(long projectId)
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

        public IEnumerable<JiraIssueResults> GetJiraIssuesFromProject(string project)
        {
            var resultsCollection = new List<JiraIssueResults>();

            string jql = $"project={project}";
            JiraIssueResults firstResult = GetIssueSearchResultsPaginated(jql, 0, 0);
            int maxResults = firstResult.MaxResults;

            var startAtPages = JiraServices.GetPagingStartPageNumbers(firstResult.Total, maxResults);

            resultsCollection.Add(firstResult);

            foreach (var item in startAtPages)
            {
                var result = GetIssueSearchResultsPaginated(jql, item, maxResults);
                resultsCollection.Add(result);
            }

            return resultsCollection;
        }

        private JiraIssueResults GetIssueSearchResultsPaginated(string jql, int startAt, int maxResults)
        {
            var client = new RestClient(_url + "/rest/api/2") {Authenticator = new HttpBasicAuthenticator(_user, _pwd)};
            var request = new RestRequest("search", Method.GET);
            request.AddParameter("jql", jql);
            request.AddParameter("fields",
                "id,key,summary,issuetype,priority,created,fixVersions,status,customfield_10008,customfield_11600,customfield_10004");
            request.AddParameter("startAt", startAt);
            if (maxResults > 0) request.AddParameter("maxResults", maxResults);

            var response = client.Execute(request);

            var tp = JsonConvert.DeserializeObject<JiraIssueResults>(response.Content,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            return tp;
        }

        public IEnumerable<TempoWorkItem> GetTempoWorkItems(string dateFrom, string dateTo, string accountKey)
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
    }
}