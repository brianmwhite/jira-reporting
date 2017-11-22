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
        public string JiraURL { get; set; }
    }
}