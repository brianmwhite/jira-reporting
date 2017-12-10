using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public class JiraApi
    {

        protected string _url;
        protected string _user;
        protected string _pwd;

        public JiraApi(string url, string user, string pwd)
        {
            _pwd = pwd;
            _user = user;
            _url = url;
        }

        public static double ConvertSecondsToHours(long seconds)
        {
            return seconds > 0 ? seconds / 60.0 / 60.0 : 0;
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

        public (string IssueKey, string IssueName) GetParentIssue(long issueId)
        {
            string json = GetIssueJsonFromJira(issueId);
            return GetParentIssueFromJson(json);
        }
        
        public static string GetJiraProjectNameFromJson(string json)
        {
            var tp = JsonConvert.DeserializeObject<JiraProject>(json,
                new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                }
            );
            return tp.Name;
        }

        public string GetProject(long projectId)
        {
            string json = GetProjectJsonFromJira(projectId);
            string projectName = GetJiraProjectNameFromJson(json);
            return projectName;
        }
    }
}