using Newtonsoft.Json;

namespace jr.common.Jira.Models
{
    public class TempoWorkItemResults
    {
        [JsonProperty("metadata")]
        public TempoMetadata Metadata { get; set; }

        [JsonProperty("results")]
        public TempoWorkItem[] WorkItems { get; set; }
    
        public class TempoMetadata
        {
            [JsonProperty("limit")]
            public int MaxResults { get; set; }

            [JsonProperty("offset")]
            public int StartAt { get; set; }

            [JsonProperty("count")]
            public int Total { get; set; }

            [JsonProperty("next")]
            public string Next { get; set; }
        }
    }
}