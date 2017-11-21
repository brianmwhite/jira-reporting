namespace jr.common
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class TempoWorkItems
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

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("origin")]
        public object Origin { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("tempoWorklogId")]
        public long TempoWorklogId { get; set; }

        [JsonProperty("timeSpentSeconds")]
        public long TimeSpentSeconds { get; set; }

        [JsonProperty("workAttributeValues")]
        public WorkAttributeValue[] WorkAttributeValues { get; set; }

        [JsonProperty("worklogAttributes")]
        public WorklogAttribute[] WorklogAttributes { get; set; }
    }

    public partial class WorklogAttribute
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class WorkAttributeValue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("workAttribute")]
        public WorkAttribute WorkAttribute { get; set; }

        [JsonProperty("worklogId")]
        public long WorklogId { get; set; }
    }

    public partial class WorkAttribute
    {
        [JsonProperty("externalUrl")]
        public string ExternalUrl { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("staticListValues")]
        public object StaticListValues { get; set; }

        [JsonProperty("type")]
        public PurpleType Type { get; set; }
    }

    public partial class PurpleType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("systemType")]
        public bool SystemType { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Issue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("internalIssue")]
        public bool InternalIssue { get; set; }

        [JsonProperty("issueType")]
        public IssueType IssueType { get; set; }

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

    public partial class IssueType
    {
        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public partial class TempoWorkItems
    {
        public static TempoWorkItems[] FromJson(string json) => JsonConvert.DeserializeObject<TempoWorkItems[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TempoWorkItems[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
