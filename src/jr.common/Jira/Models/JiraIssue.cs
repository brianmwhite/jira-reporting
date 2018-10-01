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
        public long? TotalEstimate { get; set; }

        [JsonProperty("aggregatetimeoriginalestimate")]
        public long? TotalOriginalEstimate { get; set; }

        [JsonProperty("aggregatetimespent")]
        public long? TotalTimeSpent { get; set; }

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

        [JsonProperty("customfield_10008")]
        public string EpicName {get; set;}
        
        [JsonProperty("customfield_10004")]
        public long? StoryPoints {get; set;}
        
//TODO: deal with Team property, was a string but now is a complex object, at least for subtasks
        /*
        "customfield_11600": {
            "isEditable": false,
            "nonEditableReason": {
                "message": "The team for a sub-task is inherited from the parent issue.",
                "reason": "CANNOT_SET_ON_SUBTASK"
            }
        },
         */
//        [JsonProperty("customfield_11600")]
        public string Team {get; set;}
        
        [JsonProperty("customfield_10006")]
        public string[] SprintObjects { get; set; }
        
        [JsonProperty("fixVersions")]
        public FixVersion[] FixVersions { get; set; }
        
        [JsonProperty("labels")]
        public string[] Labels { get; set; }
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

    public class FixVersion
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("archived")]
        public bool Archived { get; set; }
        
        [JsonProperty("released")]
        public bool Released { get; set; }
        
        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }
    }
}