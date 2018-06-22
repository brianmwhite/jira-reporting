using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using jr.common.Jira;
using jr.common.Jira.Models;
using jr.common.Models;
using Moq;
using Xunit;

namespace jr.common.tests.Jira
{
    public class JiraServicesTests
    {
        private const string SampleSprintString = "com.atlassian.greenhopper.service.sprint.Sprint@215201cd[id=348,rapidViewId=176,state=CLOSED,name=Sprint 15,goal=<null>,startDate=2016-04-18T14:40:01.457Z,endDate=2016-04-29T14:40:00.000Z,completeDate=2016-04-29T14:52:52.537Z,sequence=341]";

        [Theory]
        [InlineData(5.25, 18900)]
        [InlineData(0, 0)]
        [InlineData(0, -1000)]
        public void ConvertSecondsToHoursTest(double expectedHours, long seconds)
        {
            double actualHours = JiraServices.ConvertSecondsToHours(seconds);
            Assert.Equal(expectedHours, actualHours);
        }

        [Fact]
        public void ExtractSprintNameTest()
        {
            const string sprintString = SampleSprintString;
            string sprintName = JiraServices.ExtractSprintName(sprintString);
            Assert.Equal("Sprint 15", sprintName);
        }
        
        [Fact]
        public void ExtractSprintNameEmptyTest()
        {
            const string sprintString = "";
            string sprintName = JiraServices.ExtractSprintName(sprintString);
            Assert.Equal("", sprintName);
        }

        [Fact]
        public void ConvertTempoWorkItemsTest()
        {
            var mockJiraApi = new Mock<IJiraApi>();
            mockJiraApi
                .Setup(x => x.GetJiraProject(It.IsAny<string>()))
                .Returns(new JiraProject()
                {
                    Name = "My Project",
                    Id = "1"
                });

            mockJiraApi
                .Setup(x => x.GetJiraIssue(It.Is<string>(i=>i.Equals("ABC-123"))))
                .Returns(new JiraIssue
                {
                    Key = "ABC-123",
                    Fields = new IssueFields
                    {
                        Summary = "This is my issue summary",
                        Project = new Project
                        {
                            Id = "1",
                        },
                        Issuetype = new Issuetype()
                        {
                            Name = "Story"
                        }
                    }
                });

            mockJiraApi
                .Setup(x => x.GetJiraIssue(It.Is<string>(i=>i.Equals("BCD-123"))))
                .Returns(new JiraIssue
                {
                    Key = "BCD-123",
                    Fields = new IssueFields
                    {
                        Summary = "This is my issue summary",
                        Project = new Project
                        {
                            Id = "1",
                        },
                        Issuetype = new Issuetype()
                        {
                            Name = "Sub-task"
                        },
                        Parent = new Parent
                        {
                            Key = "ABC-123",
                            Fields = new ParentFields()
                            {
                                Summary = "My parent issue summary"
                            }
                        }
                    }
                });
            
            mockJiraApi
                .Setup(x => x.GetJiraIssue(It.Is<string>(i=>i.Equals("CDE-123"))))
                .Returns(new JiraIssue
                {
                    Key = "CDE-123",
                    Fields = new IssueFields
                    {
                        Summary = "This is my second issue summary",
                        Project = new Project
                        {
                            Id = "1",
                        },
                        Issuetype = new Issuetype()
                        {
                            Name = "Story"
                        },
                        Parent = new Parent
                        {
                            Key = "ABC-123",
                            Fields = new ParentFields()
                            {
                                Summary = "My parent issue summary"
                            }
                        }
                    }
                });
            
            var tempoWorkItemList = new List<TempoWorkItem>
            {
                new TempoWorkItem
                {
                    BilledSeconds = 900,
                    TempoIssue = new TempoIssue
                    {
                        Key = "BCD-123"
                    },
                    Author = new Author
                    {
                        Name = "John Doe"
                    },
                },
                new TempoWorkItem
                {
                    BilledSeconds = 3600,
                    TempoIssue = new TempoIssue
                    {
                        Key = "CDE-123",
                    },
                    Author = new Author
                    {
                        Name = "Jane Doe"
                    },
                }
            };

            var tempoResults = new TempoWorkItemResults {WorkItems = tempoWorkItemList.ToArray()};

            var js = new JiraServices(mockJiraApi.Object);
            var convertedTempoWorkItems = js.ConvertTempoWorkItems(new List<TempoWorkItemResults> {tempoResults}, true);

            var expectedWorkItem = new WorkItem
            {
                billedHours = .25,
                issueKey = "ABC-123",
                issueName= "My parent issue summary",
                userName = "John Doe",
                project = "My Project"
            };
            
            var expectedWorkItemSecond = new WorkItem
            {
                billedHours = 1,
                issueKey = "CDE-123",
                issueName= "This is my second issue summary",
                userName = "Jane Doe",
                project = "My Project"
            };

            var tempoWorkItems = convertedTempoWorkItems.ToList();
            Assert.Equal(2, tempoWorkItems.Count);
            
            var firstConvertedTempoWorkItem = tempoWorkItems[0];
            var secondConvertedTempoWorkItem = tempoWorkItems[1];
            
            Assert.Equal(expectedWorkItem.billedHours, firstConvertedTempoWorkItem.billedHours);
            Assert.Equal(expectedWorkItem.issueKey, firstConvertedTempoWorkItem.issueKey);
            Assert.Equal(expectedWorkItem.issueName, firstConvertedTempoWorkItem.issueName);
            Assert.Equal(expectedWorkItem.userName, firstConvertedTempoWorkItem.userName);
            Assert.Equal(expectedWorkItem.project, firstConvertedTempoWorkItem.project);
            
            Assert.Equal(expectedWorkItemSecond.billedHours, secondConvertedTempoWorkItem.billedHours);
            Assert.Equal(expectedWorkItemSecond.issueKey, secondConvertedTempoWorkItem.issueKey);
            Assert.Equal(expectedWorkItemSecond.issueName, secondConvertedTempoWorkItem.issueName);
            Assert.Equal(expectedWorkItemSecond.userName, secondConvertedTempoWorkItem.userName);
            Assert.Equal(expectedWorkItemSecond.project, secondConvertedTempoWorkItem.project);
        }
        
        [Fact]
        public void ConvertJiraIssueResultsTest()
        {
            var jiraIssueResultsList = new List<JiraIssueResults>
            {
                new JiraIssueResults
                {
                    Issues = new[]
                    {
                        new JiraIssue
                        {
                            Key = "ABC-123",
                            Fields = new IssueFields
                            {
                                Summary = "This is my issue summary",
                                Issuetype = new Issuetype
                                {
                                    Name = "Story"
                                },
                                EpicName = "My Epic",
                                StoryPoints = 13,
                                Team = "My Team",
                                FixVersions = new[] {new FixVersion {Name = "My Version"}},
                                SprintObjects = new[] {SampleSprintString},
                                Status = new Status
                                {
                                    Name = "New"
                                }
                            }
                        }
                    }
                }
            };


            var expectedIssue = new Issue
            {
                IssueKey = "ABC-123",
                IssueName = "This is my issue summary",
                IssueType = "Story",
                Epic = "My Epic",
                StoryPoints = 13,
                Team = "My Team",
                Version = "My Version",
                Sprint = "Sprint 15",
                Status = "New",
            };

            var js = new JiraServices(new JiraApi(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            var convertedIssue = js.ConvertJiraIssueResults(jiraIssueResultsList).ToList()[0];

            Assert.Equal(expectedIssue.IssueKey, convertedIssue.IssueKey);
            Assert.Equal(expectedIssue.IssueName, convertedIssue.IssueName);
            Assert.Equal(expectedIssue.CombinedIssueName, convertedIssue.CombinedIssueName);
            Assert.Equal(expectedIssue.IssueType, convertedIssue.IssueType);
            Assert.Equal(expectedIssue.Epic, convertedIssue.Epic);
            Assert.Equal(expectedIssue.StoryPoints, convertedIssue.StoryPoints);
            Assert.Equal(expectedIssue.Team, convertedIssue.Team);
            Assert.Equal(expectedIssue.Version, convertedIssue.Version);
            Assert.Equal(expectedIssue.Sprint, convertedIssue.Sprint);
            Assert.Equal(expectedIssue.Status, convertedIssue.Status);
        }
        
//        [Fact]
//        public void TestJiraIssueSearchTemp()
//        {
//            JiraApi ja = new JiraApi("","","");
//            var jir = ja._GetJiraIssueResults("");
//            var ir = ja._ConvertJiraIssueResults(jir);
//            Assert.Equal(453, ir.Count);
//        }
//        
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

//        [Fact]
//        public void GetTempoJson()
//        {
//            var ja = new JiraApi("https://smallfootprint.atlassian.net", "bwhite@smallfootprint.com","aDwMnc3p/B");
//            var twi = ja.GetTempoWorkItems("2018-06-01", "2018-06-30", "NG001");
//            
////            string outputJson = TempoInput.GetWorkItemsJsonFromTempo(_dateFrom, _dateTo, _accountKey);
////            List<TempoWorkItems> twi = _tempoInput.ConvertJsonToTempoWorkItemList(outputJson);
//            Assert.NotNull(twi);
//        }

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
            
            var pages = JiraServices.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {50};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesBigNumberTest()
        {
            const int total = 1203;
            const int maxResults = 50;
            
            var pages = JiraServices.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {50,100,150,200,250,300,350,400,450,500,550,600,650
                                   ,700,750,800,850,900,950,1000,1050,1100,1150,1200};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesSmallNumberTest()
        {
            const int total = 32;
            const int maxResults = 50;
            
            var pages = JiraServices.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesSameAsPageSizeTest()
        {
            const int total = 50;
            const int maxResults = 50;
            
            var pages = JiraServices.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesTriplePageSizeTest()
        {
            const int total = 150;
            const int maxResults = 50;
            
            var pages = JiraServices.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {50,100};
            Assert.Equal(expectedPages, pages);
        }
        
        [Fact]
        public void GetNumberPagesNoResultsTest()
        {
            const int total = 0;
            const int maxResults = 50;
            
            var pages = JiraServices.GetPagingStartPageNumbers(total, maxResults);

            int[] expectedPages = {};
            Assert.Equal(expectedPages, pages);
        }
    }
}