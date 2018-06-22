using Newtonsoft.Json;

namespace jr.common.Jira.Models
{
    public class TempoWorkItem
    {
        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("billableSeconds")]
        public long BilledSeconds { get; set; }

        [JsonProperty("issue")]
        public TempoIssue TempoIssue { get; set; }

        [JsonProperty("jiraWorklogId")]
        public long JiraWorklogId { get; set; }

        [JsonProperty("timeSpentSeconds")]
        public long TimeSpentSeconds { get; set; }
    }

    public class TempoIssue
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class Author
    {
        [JsonProperty("username")]
        public string Name { get; set; }
    }
}