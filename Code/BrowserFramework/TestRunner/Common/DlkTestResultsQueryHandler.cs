using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;


namespace TestRunner.Common
{
    public static class DlkTestResultsQueryHandler
    {
        public static List<DlkExecutionQueueRecord> GetResultsFromQuery(DlkReportQueryRecord queryRec)
        {
            return GetResultsFromQuery(queryRec.tag, queryRec.date, queryRec.status);
        }

        public static List<DlkExecutionQueueRecord> GetResultsFromQuery(String Tags, String Date, String Status)
        {
            List<DlkExecutionQueueRecord> QueryResults = new List<DlkExecutionQueueRecord>();
            //List<DlkTestSuiteManifestRecord> resultManifest = DlkTestSuiteResultsFileHandler.ResultManifestRecords;
            //var data = from resRec in resultManifest
            //           where ContainsTags(resRec.tag, Tags) && resRec.resultsdirectory.Substring(0, 8) == Date
            //           select new
            //            {
            //                resultdir = resRec.resultsdirectory
            //            };
            //foreach (var val in data)
            //{
            //    List<DlkExecutionQueueRecord> res = DlkTestSuiteResultsFileHandler.GetResults(val.resultdir);
            //    foreach (DlkExecutionQueueRecord executionRec in res)
            //    {
            //        if (executionRec.teststatus == Status)
            //        {
            //            QueryResults.Add(executionRec);
            //        }
            //    }
            //}

            return QueryResults;
        }

        private static Boolean ContainsTags(String ActualTags, String ExpectedTags)
        {
            Boolean res = false;
            ExpectedTags = ExpectedTags.Replace(" ", "").ToLower();
            ActualTags = ActualTags.ToLower();
            foreach (String tag in ExpectedTags.Split(','))
            {
                if(ActualTags.Contains(tag))
                {
                    res = true;
                    break;
                }
            }

            return res;

        }
    }
}
