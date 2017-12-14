using Newtonsoft.Json;

namespace jr.common.Jira.Models
{
    public class JiraIssue
    {
        [JsonProperty("fields")]
        public IssueFields Fields { get; set; }

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

        [JsonProperty("issuetype")]
        public Issuetype Issuetype { get; set; }

        [JsonProperty("parent")]
        public Parent Parent { get; set; }

        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public class Project
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Parent
    {
        [JsonProperty("fields")]
        public ParentFields Fields { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class ParentFields
    {
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
}