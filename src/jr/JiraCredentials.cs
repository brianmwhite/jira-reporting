using Newtonsoft.Json;

namespace jr
{
    public class JiraCredentials
    {
        [JsonProperty("jiraUser")]
        public string JiraUser { get; set; }
        
        [JsonProperty("jiraPassword")]
        public string JiraPassword { get; set; }
        
        [JsonProperty("jiraURL")]
        public string JiraUrl { get; set; }
        
        [JsonProperty("tempoURL")]
        public string TempoUrl { get; set; }
        
        [JsonProperty("tempoToken")]
        public string TempoToken { get; set; }
    }
}