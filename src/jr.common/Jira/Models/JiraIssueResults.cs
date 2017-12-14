using Newtonsoft.Json;

namespace jr.common.Jira.Models
{
    public class JiraIssueResults
    {
        [JsonProperty("issues")]
        public JiraIssue[] Issues { get; set; }

        [JsonProperty("maxResults")]
        public long MaxResults { get; set; }

        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
