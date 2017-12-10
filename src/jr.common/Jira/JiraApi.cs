using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

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
    }
}