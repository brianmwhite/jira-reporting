//TODO: add isolated tests, create test data

//using System;
//using System.Collections.Generic;
//using jr.common.Jira;
//using Xunit;
//
//namespace jr.common.tests
//{
//    public class TempoTests : IDisposable
//    {
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
//
//        public void Dispose()
//        {
//            _tempoInput = null;
//        }
//    }
//}