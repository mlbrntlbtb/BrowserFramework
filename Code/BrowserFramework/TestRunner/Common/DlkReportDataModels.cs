using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner.Common
{
    public class DlkReportContainer
    {
        public DlkReportContainer(DlkBaseReport _Report)
        {
            Report = _Report;
        }

        public DlkBaseReport Report { get; private set; }
    }
    public class DlkBaseReport
    {

    }

    public class DlkHistoryReport: DlkBaseReport
    {
        public class HistoryRecord
        {
            public string Date { get; set; }
            public ObservableCollection<ResultRecord> HistoryResults { get; set; }
        }

        public string ReportDate { get; set; }
        public ObservableCollection<HistoryRecord> HistoryRecords { get; set; }

        public DlkHistoryReport()
        {
            ReportDate = DateTime.Today.ToShortDateString();
            HistoryRecords = new ObservableCollection<HistoryRecord>();
            List<DlkTestSuiteResultsManifestRecord> lstSuiteResults = DlkTestSuiteResultsManifestHandler.GetSuiteResultsManifestRecords();
            IEnumerable<string> executionDates = lstSuiteResults.GroupBy(rec => new { rec.executiondate }).Select(g => g.First().executiondate);
            foreach(string date in executionDates)
            {
                var results = from res in lstSuiteResults
                              where DateTime.Parse(res.executiondate).CompareTo(DateTime.Parse(date)) <= 0
                              select res;
                Dictionary<string, DlkTestSuiteResultsManifestRecord> dictUniqueResults = new Dictionary<string, DlkTestSuiteResultsManifestRecord>();
                foreach(DlkTestSuiteResultsManifestRecord res in results)
                {
                    if(dictUniqueResults.ContainsKey(res.suite))
                    {
                        dictUniqueResults[res.suite] = res;
                    }
                    else
                    {
                        dictUniqueResults.Add(res.suite, res);
                    }
                }
                int TotalPassed = 0;
                int TotalFailed = 0;
                int TotalNotRun = 0;
                for(int i=0; i < dictUniqueResults.Count; i++)
                {
                    DlkTestSuiteResultsManifestRecord res = dictUniqueResults.ElementAt(i).Value;
                    TotalPassed = TotalPassed + res.passed;
                    TotalFailed = TotalFailed + res.failed;
                    TotalNotRun = TotalNotRun + res.notrun;
                }
                HistoryRecord newHistory = new HistoryRecord();
                newHistory.Date = date;
                newHistory.HistoryResults = new ObservableCollection<ResultRecord>();
                newHistory.HistoryResults.Add(new ResultRecord("Passed", TotalPassed));
                newHistory.HistoryResults.Add(new ResultRecord("Failed", TotalFailed));
                newHistory.HistoryResults.Add(new ResultRecord("NotRun", TotalNotRun));
                HistoryRecords.Add(newHistory);
            }
        }
    }

    public class DlkSummaryReport : DlkBaseReport
    {
        

        public class SuiteResultRecord
        {
            public string Suite { get; set; }
            public int Passed 
            {
                get
                {
                    return SuiteResults.Single(res => res.Status == "Passed").Number;
                }
            }
            public int Failed
            {
                get
                {
                    return SuiteResults.Single(res => res.Status == "Failed").Number;
                }
            }
            public int NotRun
            {
                get
                {
                    return SuiteResults.Single(res => res.Status == "NotRun").Number;
                }
            }
            public ObservableCollection<ResultRecord> SuiteResults { get; set; }
            public ObservableCollection<TestResultRecord> TestResults { get; set; }
        }

        public class TestResultRecord
        {
            public string TestName { get; set; }
            public string TestInstance { get; set; }
            public string Status { get; set; }
        }

        public string SummaryDate { get; set; }
        public ObservableCollection<ResultRecord> SummaryResults { get; set; }
        public ObservableCollection<SuiteResultRecord> SuitesResults { get; set; }

        public DlkSummaryReport()
        {
            XDocument docSummary = DlkSummaryResultsHandler.GenerateSummaryResults();
            SummaryResults = new ObservableCollection<ResultRecord>();
            var res = from doc in docSummary.Descendants("suiteresults")
                        select new
                        {
                            date = doc.Attribute("date").Value,
                            passed = doc.Attribute("passed").Value,
                            failed = doc.Attribute("failed").Value,
                            notrun = doc.Attribute("notrun").Value
                        };
            SummaryDate = res.First().date;
            SummaryResults.Add(new ResultRecord("Passed", int.Parse(res.First().passed)));
            SummaryResults.Add(new ResultRecord("Failed", int.Parse(res.First().failed)));
            SummaryResults.Add(new ResultRecord("NotRun", int.Parse(res.First().notrun)));

            SuitesResults = new ObservableCollection<SuiteResultRecord>();
            var suiteResults = from doc in docSummary.Descendants("suite")
                                select new
                                {
                                    name = doc.Attribute("name").Value,
                                    passed = doc.Attribute("passed").Value,
                                    failed = doc.Attribute("failed").Value,
                                    notrun = doc.Attribute("notrun").Value,
                                    testresults = doc.Descendants("test")
                                };
            foreach(var suiteResRec in suiteResults)
            {
                SuiteResultRecord newSuiteResRec = new SuiteResultRecord();
                newSuiteResRec.Suite = Path.GetFileNameWithoutExtension(suiteResRec.name);
                newSuiteResRec.SuiteResults = new ObservableCollection<ResultRecord>();
                newSuiteResRec.SuiteResults.Add(new ResultRecord("Passed", int.Parse(suiteResRec.passed)));
                newSuiteResRec.SuiteResults.Add(new ResultRecord("Failed", int.Parse(suiteResRec.failed)));
                newSuiteResRec.SuiteResults.Add(new ResultRecord("NotRun", int.Parse(suiteResRec.notrun)));
                newSuiteResRec.TestResults = new ObservableCollection<TestResultRecord>();
                foreach(var testResRec in suiteResRec.testresults)
                {   
                    TestResultRecord newTestResRec = new TestResultRecord();
                    newTestResRec.TestName = testResRec.Attribute("name").Value;
                    newTestResRec.TestInstance = testResRec.Attribute("instance").Value;
                    newTestResRec.Status = testResRec.Attribute("status").Value;
                    newSuiteResRec.TestResults.Add(newTestResRec);
                }

                SuitesResults.Add(newSuiteResRec);
            }
                

        }
    }

    public class ResultRecord
    {
        public string Status { get; set; }
        public int Number { get; set; }

        public ResultRecord(string Status, int Number)
        {
            this.Status = Status;
            this.Number = Number;
        }
    }

}
