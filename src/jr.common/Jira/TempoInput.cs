using System.Collections.Generic;
using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public class TempoInput : JiraApi
    {
        public TempoInput(string url, string user, string pwd) : base(url, user, pwd)
        {
        }

        public IEnumerable<WorkItem> GetWorkItems(string dateFrom, string dateTo, string accountKey, bool getParentIssue = false)
        {
            string json = _GetTempoWorkItemJson(dateFrom, dateTo, accountKey);
            IEnumerable<TempoWorkItem> twi = _DeserializeTempoWorkItems(json);
            IEnumerable<WorkItem> wi = _ConvertTempoWorkItems(twi, getParentIssue);
            return wi;
        }
       
        public string _GetTempoWorkItemJson(string dateFrom, string dateTo, string accountKey)
        {
            RestClient client = new RestClient(Url + "/rest/tempo-timesheets/3/");
            client.Authenticator = new HttpBasicAuthenticator(User, Pwd);

            RestRequest request = new RestRequest("worklogs", Method.GET);
            request.AddQueryParameter("dateFrom", dateFrom);
            request.AddQueryParameter("dateTo", dateTo);
            request.AddQueryParameter("accountKey", accountKey);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public static IEnumerable<TempoWorkItem> _DeserializeTempoWorkItems(string json)
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

        public IEnumerable<WorkItem> _ConvertTempoWorkItems(IEnumerable<TempoWorkItem> twi, bool getParentIssue = false)
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
                    string projectName = GetProjectName(item.Issue.ProjectId);
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
