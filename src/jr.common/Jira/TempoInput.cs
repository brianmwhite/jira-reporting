using System.Collections.Generic;
using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public class TempoInput
    {
        private readonly string _url;
        private readonly string _user;
        private readonly string _pwd;
        public TempoInput(string url, string user, string pwd)
        {
            _pwd = pwd;
            _user = user;
            _url = url;
        }

        public string GetWorkItemsJsonFromTempo(string dateFrom, string dateTo, string accountKey)
        {
            RestClient client = new RestClient(_url + "/rest/tempo-timesheets/3/");
            client.Authenticator = new HttpBasicAuthenticator(_user, _pwd);

            RestRequest request = new RestRequest("worklogs", Method.GET);
            request.AddQueryParameter("dateFrom", dateFrom);
            request.AddQueryParameter("dateTo", dateTo);
            request.AddQueryParameter("accountKey", accountKey);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string GetProjectJsonFromJira(long projectId)
        {
            RestClient client = new RestClient(_url + "/rest/api/2/");
            client.Authenticator = new HttpBasicAuthenticator(_user, _pwd);

            RestRequest request = new RestRequest("project/{projectIdOrKey}", Method.GET);
            request.AddUrlSegment("projectIdOrKey", projectId);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public (string IssueKey, string IssueName) GetParentIssue(long issueId)
        {
            string json = GetIssueJsonFromJira(issueId);
            return GetParentIssueFromJson(json);
        }

        public string GetIssueJsonFromJira(long issueId)
        {
            RestClient client = new RestClient(_url + "/rest/api/2");
            client.Authenticator = new HttpBasicAuthenticator(_user, _pwd);

            RestRequest request = new RestRequest("issue/{issueId}", Method.GET);
            request.AddUrlSegment("issueId", issueId);

            IRestResponse response = client.Execute(request);
            return response.Content;            
        }
        
        public (string IssueKey, string IssueName) GetParentIssueFromJson(string json)
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
            string issueKey = string.Empty;
            string issueName = string.Empty;

            if (tp.Fields.Parent?.Fields != null)
            {
                issueKey = tp.Fields.Parent.Key;
                issueName = tp.Fields.Parent.Fields.Summary;
            }
            
            return (IssueKey: issueKey, IssueName: issueName);
        }

        public static string GetTempoProjectNameFromJson(string json)
        {
            var tp = JsonConvert.DeserializeObject<TempoProject>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            return tp.Name;
        }

        public static IEnumerable<TempoWorkItem> ConvertJsonToTempoWorkItemList(string json)
        {
            var twi = new List<TempoWorkItem>();

            var arr = JsonConvert.DeserializeObject<TempoWorkItem[]>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            
            twi.AddRange(arr);

            return twi;
        }

        private string GetProject(long projectId)
        {
            string json = GetProjectJsonFromJira(projectId);
            string projectName = GetTempoProjectNameFromJson(json);
            return projectName;
        }

        public static double ConvertSecondsToHours(long seconds)
        {
            return seconds > 0 ? seconds / 60.0 / 60.0 : 0;
        }
        
        public IEnumerable<WorkItem> ConvertTempoWorkItemListToWorkItems(IEnumerable<TempoWorkItem> twi, bool getParentIssue = false)
        {
            var wi = new List<WorkItem>();
            var projectLookup = new Dictionary<long, string>();
            foreach (TempoWorkItem item in twi)
            {
                var w = new WorkItem();
                w.issueKey = item.Issue.Key;
                w.issueName = item.Issue.Summary;
                w.billedHours = ConvertSecondsToHours(item.BilledSeconds);
                w.userName = item.Author.Name;
                
                if (projectLookup.ContainsKey(item.Issue.ProjectId))
                {
                    w.project = projectLookup.GetValueOrDefault(item.Issue.ProjectId);
                }
                else
                {
                    string projectName = GetProject(item.Issue.ProjectId);
                    projectLookup.Add(item.Issue.ProjectId, projectName);
                    w.project = projectName;
                }

                if (getParentIssue && item.Issue.IssueType.Name == "Sub-task")
                {
                    (string parentKey, string parentName) = GetParentIssue(item.Issue.Id);
                    w.issueKey = parentKey;
                    w.issueName = parentName;
                }
                
                wi.Add(w);
            }
            return wi;
        }
    }
}
