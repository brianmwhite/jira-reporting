//TODO: add isolated tests, create test data

using System.Collections.Generic;
using jr.common.Jira;
using Xunit;
//
namespace jr.common.tests
{
    public class JiraApiTests
    {
        [Fact]
        public void ConvertSecondsToHoursTest()
        {
            const double expectedHours = 5.25;
            double actualHours = JiraApi.ConvertSecondsToHours(18900);
            Assert.Equal(expectedHours, actualHours);
        }
        
        [Fact]
        public void ConvertSecondsToHoursZeroTest()
        {
            const double expectedHours = 0;
            double actualHours = JiraApi.ConvertSecondsToHours(0);
            Assert.Equal(expectedHours, actualHours);
        }
        
        [Fact]
        public void ConvertSecondsToHoursNegativeTest()
        {
            const double expectedHours = 0;
            double actualHours = JiraApi.ConvertSecondsToHours(-1000);
            Assert.Equal(expectedHours, actualHours);
        }
        
//        private readonly string _dateTo;
//        private readonly string _dateFrom;
//        private readonly string _accountKey;
//        private readonly long _projectId;
//        private TempoInput _tempoInput;
//
//        public TempoTests()
//        {
//            var jiraUser = "";
//            var jiraPwd = "";
//            var jiraUrl = "";
//            long _projectIdString = 0;
//            _dateTo = "";
//            _dateFrom = "";
//            _accountKey = "";
//            
//            _tempoInput = new TempoInput(jiraUrl, jiraUser, jiraPwd);
//        }
//
//        [Fact]
//        public void GetTempoJson()
//        {
//            string outputJson = _tempoInput.GetWorkItemsJsonFromTempo(_dateFrom, _dateTo, _accountKey);
//            List<TempoWorkItems> twi = _tempoInput.ConvertJsonToTempoWorkItemList(outputJson);
//            Assert.NotEqual(string.Empty, outputJson);
//        }
//
//        [Fact]
//        public void GetTempoProjectJson()
//        {
//            string outputJson = _tempoInput.GetProjectJsonFromJira(_projectId);
//            Assert.NotEqual(string.Empty, outputJson);
//        }
//
//        [Fact]
//        public void GetTempoToOutput()
//        {
//            string outputJson = _tempoInput.GetWorkItemsJsonFromTempo(_dateFrom, _dateTo, _accountKey);
//            List<TempoWorkItems> twi = _tempoInput.ConvertJsonToTempoWorkItemList(outputJson);
//            List<WorkItem> wi = _tempoInput.ConvertTempoWorkItemListToWorkItems(twi);
//            TimeSummarization timeSummarizationData = new TimeSummarization(
//                _devRate: 0
//                , _mgmtRate: 0
//                , _mgmtUsers: null
//                , _splitPO: false
//                , _projectTextToTrim: ""
//                , _outputColumns: "Project,Dev_Hours,Dev_Amount,Mgmt_Hours,Mgmt_Amount,Total_Hours,Total_Amount"
//            );
//            var si = timeSummarizationData.SummarizeWorkItems(wi);
//            Assert.NotEqual(string.Empty, outputJson);
//        }

        [Fact]
        public void GetNumberPagesTest()
        {
            const int total = 64;
            const int maxResults = 50;
            
            var pages = JiraApi.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {50};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesBigNumberTest()
        {
            const int total = 1203;
            const int maxResults = 50;
            
            var pages = JiraApi.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {50,100,150,200,250,300,350,400,450,500,550,600,650
                                   ,700,750,800,850,900,950,1000,1050,1100,1150,1200};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesSmallNumberTest()
        {
            const int total = 32;
            const int maxResults = 50;
            
            var pages = JiraApi.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesSameAsPageSizeTest()
        {
            const int total = 50;
            const int maxResults = 50;
            
            var pages = JiraApi.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesTriplePageSizeTest()
        {
            const int total = 150;
            const int maxResults = 50;
            
            var pages = JiraApi.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {50,100};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesNoResultsTest()
        {
            const int total = 0;
            const int maxResults = 50;
            
            var pages = JiraApi.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {};
            Assert.Equal(expectedPages, pages);
        }
    }
}