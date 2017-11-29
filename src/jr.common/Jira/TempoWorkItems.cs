using Newtonsoft.Json;

namespace jr.common.Jira
{
    public class TempoWorkItems
    {
        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("billedSeconds")]
        public long BilledSeconds { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("dateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("dateStarted")]
        public string DateStarted { get; set; }

        [JsonProperty("dateUpdated")]
        public string DateUpdated { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("issue")]
        public Issue Issue { get; set; }

        [JsonProperty("jiraWorklogId")]
        public long? JiraWorklogId { get; set; }

        [JsonProperty("tempoWorklogId")]
        public long TempoWorklogId { get; set; }

        [JsonProperty("timeSpentSeconds")]
        public long TimeSpentSeconds { get; set; }
    }

    public class Issue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("issueType")]
        public WorkItemIssueType IssueType { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("projectId")]
        public long ProjectId { get; set; }

        [JsonProperty("remainingEstimateSeconds")]
        public long? RemainingEstimateSeconds { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public class WorkItemIssueType
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Author
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
