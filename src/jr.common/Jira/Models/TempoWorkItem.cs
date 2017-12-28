using Newtonsoft.Json;

namespace jr.common.Jira.Models
{
    public class TempoWorkItem
    {
        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("billedSeconds")]
        public long BilledSeconds { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("issue")]
        public TempoIssue TempoIssue { get; set; }

        [JsonProperty("timeSpentSeconds")]
        public long TimeSpentSeconds { get; set; }
    }

    public class TempoIssue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("issueType")]
        public WorkItemIssueType IssueType { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("projectId")]
        public long ProjectId { get; set; }

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
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
