// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using jr.common;
//
//    var data = TempoProject.FromJson(jsonString);
//
namespace jr.common.TempoProjectObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class TempoProject
    {
        [JsonProperty("assigneeType")]
        public string AssigneeType { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("components")]
        public Component[] Components { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("expand")]
        public string Expand { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("issueTypes")]
        public IssueType[] IssueTypes { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("lead")]
        public Lead Lead { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("projectCategory")]
        public ProjectCategory ProjectCategory { get; set; }

        [JsonProperty("projectTypeKey")]
        public string ProjectTypeKey { get; set; }

        [JsonProperty("roles")]
        public Roles Roles { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("versions")]
        public Version[] Versions { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("projectId")]
        public long ProjectId { get; set; }

        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonProperty("released")]
        public bool Released { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("userReleaseDate")]
        public string UserReleaseDate { get; set; }
    }

    public partial class Roles
    {
        [JsonProperty("Administrators")]
        public string Administrators { get; set; }

        [JsonProperty("All Users")]
        public string AllUsers { get; set; }

        [JsonProperty("atlassian-addons-project-access")]
        public string AtlassianAddonsProjectAccess { get; set; }

        [JsonProperty("Engagement Managers")]
        public string EngagementManagers { get; set; }

        [JsonProperty("Project Managers")]
        public string ProjectManagers { get; set; }

        [JsonProperty("Quality Assurance")]
        public string QualityAssurance { get; set; }

        [JsonProperty("Tempo Project Managers")]
        public string TempoProjectManagers { get; set; }
    }

    public partial class ProjectCategory
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public partial class Lead
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public partial class IssueType
    {
        [JsonProperty("avatarId")]
        public long AvatarId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("subtask")]
        public bool Subtask { get; set; }
    }

    public partial class Component
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isAssigneeTypeValid")]
        public bool IsAssigneeTypeValid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public partial class AvatarUrls
    {
        [JsonProperty("16x16")]
        public string The16x16 { get; set; }

        [JsonProperty("24x24")]
        public string The24x24 { get; set; }

        [JsonProperty("32x32")]
        public string The32x32 { get; set; }

        [JsonProperty("48x48")]
        public string The48x48 { get; set; }
    }

    public partial class TempoProject
    {
        public static TempoProject FromJson(string json) => JsonConvert.DeserializeObject<TempoProject>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TempoProject self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
