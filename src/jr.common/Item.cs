namespace jr.common.Jira
{
    public class Item
    {
        public string IssueKey { get; set; }
        public string IssueName { get; set; }
        public string CombinedIssueName => string.Format("[{0}] {1}", IssueKey, IssueName);
        
        public string EpicKey { get; set; }
        public string EpicName { get; set; }
        public string CombinedEpicName => string.Format("[{0}] {1}", EpicKey, EpicName);
        
        public string Version { get; set; }
        public string Team { get; set; }
        
        public string IssueType { get; set; }
        
        public float StoryPoints { get; set; }
    }
}