using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using jr.common.Jira.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace jr.common.Jira
{
    public interface IJiraApi
    {
        JiraIssue GetJiraIssue(long issueId);
        JiraIssue GetJiraIssue(string issueKey);
        JiraProject GetJiraProject(string projectId);
        IEnumerable<JiraIssueResults> GetJiraIssuesFromProject(string project);
        IEnumerable<TempoWorkItemResults> GetTempoWorkItems(string dateFrom, string dateTo, string accountKey);
    }

    public class JiraApi : IJiraApi
    {
        //TODO: use repository pattern / dependency injection?

        private readonly string _pwd;
        private readonly string _url;
        private readonly string _user;
        private readonly string _tempoUrl;
        private readonly string _tempoApiToken;

        [ExcludeFromCodeCoverage]
        public JiraApi(string url, string user, string pwd, string tempoUrl, string tempoApiToken)
        {
            _pwd = pwd;
            _user = user;
            _url = url;
            _tempoUrl = tempoUrl;
            _tempoApiToken = tempoApiToken;
        }
        
//TODO: consolidate GetJiraIssue methods, shouldn't have two almost exact methods between issueId and issueKey        
        [ExcludeFromCodeCoverage]
        public JiraIssue GetJiraIssue(long issueId)
        {
            var client = new RestClient(_url + "/rest/api/2")
            {
                Authenticator = new HttpBasicAuthenticator(_user, _pwd)
            };

            var request = new RestRequest("issue/{issueId}", Method.GET);
            request.AddUrlSegment("issueId", issueId);

            var response = client.Execute(request);
            Debug.WriteLine($"QUERY: Get issue: Issue:{issueId}");

            if (response.IsSuccessful)
            {
                try
                {
                    var issue = JsonConvert.DeserializeObject<JiraIssue>(response.Content,
                        new JsonSerializerSettings
                        {
                            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                            DateParseHandling = DateParseHandling.None,
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        }
                    );
                    return issue;
                }
                catch (Exception e)
                {
                    throw new Exception($"Issue ID={issueId}, Json={response.Content}, Exception={e.Message}{e.StackTrace}");
                }
            }
            else
            {
                throw new Exception($"Issue ID={issueId}, Response Status={response.StatusDescription}");
            }

        }
        
        [ExcludeFromCodeCoverage]
        public JiraIssue GetJiraIssue(string issueKey)
        {
            var client = new RestClient(_url + "/rest/api/2")
            {
                Authenticator = new HttpBasicAuthenticator(_user, _pwd)
            };

            var request = new RestRequest("issue/{issueKey}", Method.GET);
            request.AddUrlSegment("issueKey", issueKey);

            var response = client.Execute(request);
            Debug.WriteLine($"QUERY: Get issue: Issue:{issueKey}");

            if (response.IsSuccessful)
            {
                try
                {
                    var issue = JsonConvert.DeserializeObject<JiraIssue>(response.Content,
                        new JsonSerializerSettings
                        {
                            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                            DateParseHandling = DateParseHandling.None,
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        }
                    );
                    return issue;
                }
                catch (Exception e)
                {
                    throw new Exception($"Issue ID={issueKey}, Json={response.Content}, Exception={e.Message}{e.StackTrace}");
                }
            }
            else
            {
                throw new Exception($"Issue ID={issueKey}, Response Status={response.StatusDescription}");
            }

        }

        
        [ExcludeFromCodeCoverage]
        public JiraProject GetJiraProject(string projectId)
        {
            var client = new RestClient(_url + "/rest/api/2/")
            {
                Authenticator = new HttpBasicAuthenticator(_user, _pwd)
            };

            var request = new RestRequest("project/{projectIdOrKey}", Method.GET);
            request.AddUrlSegment("projectIdOrKey", projectId);

            var response = client.Execute(request);
            Debug.WriteLine($"QUERY: Get project: Project:{projectId}");
            
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<JiraProject>(response.Content,
                    new JsonSerializerSettings
                    {
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                        DateParseHandling = DateParseHandling.None
                    }
                );
            }
            else
            {
                throw new Exception(response.StatusDescription);
            }
        }

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
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
            Debug.WriteLine($"QUERY: Get Issues:{jql}, offset:{startAt}, maxResults:{maxResults}");
            
            if (response.IsSuccessful)
            {
                var tp = JsonConvert.DeserializeObject<JiraIssueResults>(response.Content,
                    new JsonSerializerSettings
                    {
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                        DateParseHandling = DateParseHandling.None
                    }
                );
                return tp;
            }
            else
            {
                throw new Exception(response.StatusDescription);
            }
            
        }

        [ExcludeFromCodeCoverage]
        public IEnumerable<TempoWorkItemResults> GetTempoWorkItems(string dateFrom, string dateTo, string accountKey)
        {
            var resultsCollection = new List<TempoWorkItemResults>();

            string next = string.Empty;
            
            TempoWorkItemResults firstResult = GetTempoSearchResultsPaginated(accountKey, dateFrom, dateTo, 0, 0);
            resultsCollection.Add(firstResult);
            if (!string.IsNullOrEmpty(firstResult.Metadata.Next))
            {
                next = firstResult.Metadata.Next;
                while (!string.IsNullOrEmpty(next))
                {
                    TempoWorkItemResults results = GetTempoSearchResultsPaginated(next);
                    resultsCollection.Add(results);
                    next = results.Metadata.Next;
                }
            }

            return resultsCollection;

        }

        private TempoWorkItemResults GetTempoSearchResultsPaginated(string url)
        {
            var client = new RestClient(url)
            {
                Authenticator = new JwtAuthenticator(_tempoApiToken)
            };
            
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            Debug.WriteLine($"QUERY: Get worklogs: URL:{url}");
            
            if (response.IsSuccessful)
            {
                var tp = JsonConvert.DeserializeObject<TempoWorkItemResults>(response.Content,
                    new JsonSerializerSettings
                    {
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                        DateParseHandling = DateParseHandling.None
                    }
                );
                return tp;
            }
            else
            {
                throw new Exception(response.StatusDescription);
            }
        }

        private TempoWorkItemResults GetTempoSearchResultsPaginated(string accountKey, string dateFrom, string dateTo, int startAt, int maxResults)
        {
            var client = new RestClient(_tempoUrl + "/core/3/")
            {
                Authenticator = new JwtAuthenticator(_tempoApiToken)
            };
            
            var request = new RestRequest("worklogs/account/{accountKey}", Method.GET);
            request.AddUrlSegment("accountKey",accountKey);
            request.AddQueryParameter("from", dateFrom);
            request.AddQueryParameter("to", dateTo);
            request.AddParameter("offset", startAt);
            if (maxResults > 0) request.AddParameter("limit", maxResults);

            var response = client.Execute(request);
            Debug.WriteLine($"QUERY: Get worklogs: Account:{accountKey},F:{dateFrom},T:{dateTo},offset:{startAt},limit:{maxResults}");
            
            if (response.IsSuccessful)
            {
                var tp = JsonConvert.DeserializeObject<TempoWorkItemResults>(response.Content,
                    new JsonSerializerSettings
                    {
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                        DateParseHandling = DateParseHandling.None
                    }
                );
                return tp;
            }
            else
            {
                throw new Exception(response.StatusDescription);
            }
        }
    }
}