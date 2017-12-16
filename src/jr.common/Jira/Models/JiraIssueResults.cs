using Newtonsoft.Json;

namespace jr.common.Jira.Models
{
    public class JiraIssueResults
    {
        [JsonProperty("issues")]
        public JiraIssue[] Issues { get; set; }

        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty("startAt")]
        public int StartAt { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
