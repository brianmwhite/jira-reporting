using Newtonsoft.Json;

namespace jr.common.Jira
{
    public class JiraIssue
    {
        [JsonProperty("fields")]
        public IssueFields Fields { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class IssueFields
    {
        [JsonProperty("aggregatetimeestimate")]
        public long TotalEstimate { get; set; }

        [JsonProperty("aggregatetimeoriginalestimate")]
        public long TotalOriginalEstimate { get; set; }

        [JsonProperty("aggregatetimespent")]
        public long TotalTimeSpent { get; set; }

        [JsonProperty("assignee")]
        public Assignee Assignee { get; set; }

        [JsonProperty("customfield_10400")]
        public Account Account { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("issuetype")]
        public Issuetype Issuetype { get; set; }

        [JsonProperty("parent")]
        public Parent Parent { get; set; }

        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("resolution")]
        public Priority Resolution { get; set; }

        [JsonProperty("resolutiondate")]
        public string ResolutionDate { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("timeestimate")]
        public long Estimate { get; set; }

        [JsonProperty("timeoriginalestimate")]
        public long OriginalEstimate { get; set; }

        [JsonProperty("timespent")]
        public long TimeSpent { get; set; }
    }

    public class Project
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("projectCategory")]
        public Priority ProjectCategory { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public class Parent
    {
        [JsonProperty("fields")]
        public ParentFields Fields { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public class ParentFields
    {
        [JsonProperty("issuetype")]
        public Issuetype Issuetype { get; set; }

        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public class Status
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Priority
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Issuetype
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Account
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Assignee
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("displayName")]
        public string FullName { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("key")]
        public string Username { get; set; }
    }
}