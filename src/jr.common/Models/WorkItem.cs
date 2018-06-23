namespace jr.common.Models
{
    public class WorkItem
    {
        public string project { get; set; }
        public double billedHours { get; set; }
        public string userName { get; set; }
        public string issueKey { get; set; }
        public string issueName { get; set; }
        public string label { get; set; }
        public string combinedIssueName
        {
            get
            {
                return string.Format("[{0}] {1}", this.issueKey, this.issueName);
            }
        }

    }
}