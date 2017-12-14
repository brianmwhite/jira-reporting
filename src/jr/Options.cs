namespace jr
{
    using Newtonsoft.Json;

    public class Options
    {
        public Options()
        {
            Advanced = new Advanced();
            BillingSetup = new BillingSetup();
            Filtering = new Filtering();
            Output = new Output();
        }

        public static Options FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Options>(json, new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            });
        }

        [JsonProperty("advanced")]
        public Advanced Advanced { get; set; }

        [JsonProperty("billing-setup")]
        public BillingSetup BillingSetup { get; set; }

        [JsonProperty("filtering")]
        public Filtering Filtering { get; set; }

        [JsonProperty("output")]
        public Output Output { get; set; }
    }


    public class Output
    {
        [JsonProperty("col")]
        public string Col { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("separator")]
        public string Separator { get; set; } = string.Empty;
    }

    public class Filtering
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

        [JsonProperty("time_period")]
        public string TimePeriod { get; set; }
    }

    public class BillingSetup
    {
        [JsonProperty("dev_rate")]
        public double DevRate { get; set; }

        [JsonProperty("mgmt_rate")]
        public double MgmtRate { get; set; }

        [JsonProperty("mgmt_usernames")]
        public string[] MgmtUsernames { get; set; }
    }

    public class Advanced
    {
        [JsonProperty("split-po")]
        public bool SplitPo { get; set; }

        [JsonProperty("trim")]
        public string Trim { get; set; }
    }
}