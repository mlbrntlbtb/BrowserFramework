using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner.Common
{
    public static class DlkSummaryResultsHandler
    {
        public static XDocument GenerateSummaryResults()
        {
            
            XElement suite;

            int TotalPassed = 0;
            int TotalFailed = 0;
            int TotalNotRun = 0;

            XElement root = new XElement("suiteresults");
            string[] suiteResults = Directory.GetDirectories(DlkEnvironment.mDirSuiteResults);
            for (int i = 0; i < suiteResults.Count(); i++)
            {
                suite = new XElement("suite");
                suite.Add(new XAttribute("name", suiteResults[i]));

                string resultFolder = "";
                List<DlkExecutionQueueRecord> results = DlkTestSuiteResultsFileHandler.GetLatestSuiteResults(Path.Combine(DlkEnvironment.mDirSuiteResults, suiteResults[i]), out resultFolder);
                int NotRun = 0;
                int Passed = 0;
                int Failed = 0;
                for(int j=0; j < results.Count; j++)
                {
                    XElement testresult = new XElement("test");
                    testresult.Add(new XAttribute("name", results[j].name));
                    testresult.Add(new XAttribute("instance", results[j].instance));
                    testresult.Add(new XAttribute("status", results[j].teststatus));
                    switch(results[j].teststatus)
                    {
                        case "Passed":
                            Passed++;
                            break;
                        case "Failed":
                            Failed++;
                            break;
                        default:
                            NotRun++;
                            break;
                    }

                    suite.Add(testresult);

                }

                suite.Add(new XAttribute("passed", Passed.ToString()));
                suite.Add(new XAttribute("failed", Failed.ToString()));
                suite.Add(new XAttribute("notrun", NotRun.ToString()));

                root.Add(suite);
                TotalPassed = TotalPassed + Passed;
                TotalFailed = TotalFailed + Failed;
                TotalNotRun = TotalNotRun + NotRun;
            }

            root.Add(new XAttribute("machine", Environment.MachineName));
            root.Add(new XAttribute("date", DateTime.Today.ToShortDateString()));
            root.Add(new XAttribute("passed", TotalPassed.ToString()));
            root.Add(new XAttribute("failed", TotalFailed.ToString()));
            root.Add(new XAttribute("notrun", TotalNotRun.ToString()));

            XDocument doc = new XDocument(root);
            return doc;
        }
    }
}
