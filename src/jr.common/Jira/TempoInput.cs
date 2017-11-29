using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public class TempoInput
    {
        private readonly string url;
        private readonly string user;
        private readonly string pwd;
        public TempoInput(string url, string user, string pwd)
        {
            this.pwd = pwd;
            this.user = user;
            this.url = url;
        }

        public string GetWorkItemsJsonFromTempo(string dateFrom, string dateTo, string accountKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            RestClient client = new RestClient(url + "/rest/tempo-timesheets/3/");
            client.Authenticator = new HttpBasicAuthenticator(user, pwd);

            RestRequest request = new RestRequest("worklogs", Method.GET);
            request.AddQueryParameter("dateFrom", dateFrom);
            request.AddQueryParameter("dateTo", dateTo);
            request.AddQueryParameter("accountKey", accountKey);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string GetProjectJsonFromJira(long projectId)
        {
            RestClient client = new RestClient(url + "/rest/api/2/");
            client.Authenticator = new HttpBasicAuthenticator(user, pwd);

            RestRequest request = new RestRequest("project/{projectIdOrKey}", Method.GET);
            request.AddUrlSegment("projectIdOrKey", projectId);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string GetTempoProjectNameFromJson(string json)
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

        public List<TempoWorkItems> ConvertJsonToTempoWorkItemList(string json)
        {
            var twi = new List<TempoWorkItems>();

            var arr = JsonConvert.DeserializeObject<TempoWorkItems[]>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            
            twi.AddRange(arr);

            return twi;
        }

        public string GetProject(long projectId)
        {
            string json = this.GetProjectJsonFromJira(projectId);
            string projectName = this.GetTempoProjectNameFromJson(json);
            return projectName;
        }

        public static double ConvertSecondsToHours(long seconds)
        {
            return seconds > 0 ? seconds / 60.0 / 60.0 : 0;
        }
        
        public List<WorkItem> ConvertTempoWorkItemListToWorkItems(List<TempoWorkItems> twi)
        {
            List<WorkItem> wi = new List<WorkItem>();
            var projectLookup = new Dictionary<long, string>();
            foreach (TempoWorkItems item in twi)
            {
                WorkItem w = new WorkItem();
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
                    string projectName = this.GetProject(item.Issue.ProjectId);
                    projectLookup.Add(item.Issue.ProjectId, projectName);
                    w.project = projectName;
                }
                wi.Add(w);
            }
            return wi;
        }
    }
}
