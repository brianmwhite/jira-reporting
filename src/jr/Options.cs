namespace jr
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class Options
    {
        [JsonProperty("advanced")]
        public Advanced Advanced { get; set; }

        [JsonProperty("billing-setup")]
        public BillingSetup BillingSetup { get; set; }

        [JsonProperty("filtering")]
        public Filtering Filtering { get; set; }

        [JsonProperty("jira-setup")]
        public JiraSetup JiraSetup { get; set; }

        [JsonProperty("output")]
        public Output Output { get; set; }

        [JsonProperty("tempo-setup")]
        public TempoSetup TempoSetup { get; set; }
    }

    public partial class TempoSetup
    {
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }

    public partial class Output
    {
        [JsonProperty("col")]
        public string Col { get; set; }

        [JsonProperty("pretty")]
        public bool Pretty { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }
    }

    public partial class JiraSetup
    {
        [JsonProperty("jira_password")]
        public string JiraPassword { get; set; }

        [JsonProperty("jira_url")]
        public string JiraUrl { get; set; }

        [JsonProperty("jira_user")]
        public string JiraUser { get; set; }
    }

    public partial class Filtering
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("date_end")]
        public string DateEnd { get; set; }

        [JsonProperty("date_start")]
        public string DateStart { get; set; }

        [JsonProperty("groupby")]
        public string Groupby { get; set; }

        [JsonProperty("hide-non-billable")]
        public bool HideNonBillable { get; set; }

        [JsonProperty("month")]
        public double Month { get; set; }
    }

    public partial class BillingSetup
    {
        [JsonProperty("dev_rate")]
        public double DevRate { get; set; }

        [JsonProperty("mgmt_rate")]
        public double MgmtRate { get; set; }

        [JsonProperty("mgmt_usernames")]
        public string[] MgmtUsernames { get; set; }
    }

    public partial class Advanced
    {
        [JsonProperty("split-po")]
        public bool SplitPo { get; set; }

        [JsonProperty("trim")]
        public string Trim { get; set; }
    }

    public partial class Options
    {
        public static Options FromJson(string json) => JsonConvert.DeserializeObject<Options>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Options self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
