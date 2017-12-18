namespace jr.common.Models
{
    public class Issue
    {
        public string IssueKey { get; set; }
        
        public string IssueName { get; set; }
        
        public string CombinedIssueName => string.Format("[{0}] {1}", IssueKey, IssueName);
        
        public string Epic { get; set; }
        
        public string Version { get; set; }
        
        public string Team { get; set; }
        
        public string IssueType { get; set; }
        
        public double? StoryPoints { get; set; }
        
        public string Sprint { get; set; }
        
        public string Status { get; set; }
    }
}