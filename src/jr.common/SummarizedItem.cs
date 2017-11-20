namespace jr.common
{
    public class SummarizedItem
    {
        // public SummarizedItem(double _devRate, double _mgmtRate): 
        //     this(_devRate, _mgmtRate, false, string.Empty) {}

        public SummarizedItem(bool _splitPO = false, string _projectTextToTrim = "")
        {
            this.split_po = _splitPO;
            this.ProjectTextToTrim = _projectTextToTrim;
        }

        private string BillingCodeSplitCharacter = "#";
        private string ProjectTextToTrim = "";
        private bool split_po = false;

        private string _project = "";

        public string project
        {
            get
            {
                if (!string.IsNullOrEmpty(ProjectTextToTrim))
                {
                    return this._project.Replace(ProjectTextToTrim, string.Empty).Trim();
                }
                else
                {
                    return this._project.Trim();
                }
            }
            set
            {
                this._project = value;
            }
        }
        public string billing_project
        {
            get
            {
                if (this.split_po && this.project.Contains(this.BillingCodeSplitCharacter))
                {
                    return this.project.Split(this.BillingCodeSplitCharacter)[0].Trim();
                }
                else
                {
                    return this.project;
                }
            }
        }
        public string billing_code
        {
            get
            {
                if (this.split_po && this.project.Contains(this.BillingCodeSplitCharacter))
                {
                    return this.project.Split(this.BillingCodeSplitCharacter)[1].Trim();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string issue { get; set; }
        public double dev_hours { get; set; }
        public double dev_rate { get; set; }
        public double mgmt_hours { get; set; }
        public double mgmt_rate { get; set; }

        public double total_hours
        {
            get
            {
                return this.dev_hours + this.mgmt_hours;
            }
        }

        public double mgmt_amount
        {
            get
            {
                return this.mgmt_hours * this.mgmt_rate;
            }
        }

        public double dev_amount
        {
            get
            {
                return this.dev_hours * this.dev_rate;
            }
        }

        public double total_amount
        {
            get
            {
                return this.dev_amount + this.mgmt_amount;
            }
        }

    }
}