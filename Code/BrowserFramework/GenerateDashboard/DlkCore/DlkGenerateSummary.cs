using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using GenerateDashboard.DlkRecords;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GenerateDashboard.DlkCore
{
    static class DlkGenerateSummary
    {
        /// <summary>
        /// This holds the statistics per test suite
        /// </summary>
        private static List<DlkTestSuiteStatsRecord> mSuiteStatsRecords { get; set; }

        /// <summary>
        /// This holds the latest suite results
        /// </summary>
        private static List<DlkTestSuiteStatsRecord> mLatestSuiteStatsRecords { get; set; }

        private static List<DlkTestSuiteStatsRecord> mAllSuiteStatsRecords { get; set; }


        /// <summary>
        /// this holds the overall summary, totals across suites
        /// </summary>
        private static DlkSummaryStatsRecord mSummaryRec { get; set; }

        /// <summary>
        /// this is the XML file that is generated containing the statistics for the dashboard
        /// </summary>
        private static String mSummaryWorkingFilePath
        {
            get
            {
                return DlkEnvironment.mDirDataFrameworkDashboardRepositoryWorking + "Summary.xml";
            }
        }

        /// <summary>
        /// generate the summary xml and return the latest suite result records for use in the details
        /// </summary>
        /// <returns></returns>
        public static List<DlkTestSuiteStatsRecord> Execute()
        {
            // clean out old data
            DlkEnvironment.EmptyFolder(DlkEnvironment.mDirDataFrameworkDashboardRepositoryWorking);

            // read data and generate stats
            GenerateStatRecords();

            // create xml file with stats
            WriteStats();

            return mAllSuiteStatsRecords;
        }

        /// <summary>
        /// read the stored result files and then generate global statistic records
        /// </summary>
        private static void GenerateStatRecords()
        {
            // init
            int mTotalTestCount = 0;
            int mTestCount = 0;
            int mTotalTestsExecuted = 0;
            int mTestsExecuted = 0;
            int mTotalTestsPassed = 0;
            int mTotalTestsFailed=0;
            mSuiteStatsRecords = new List<DlkTestSuiteStatsRecord>();
            mLatestSuiteStatsRecords = new List<DlkTestSuiteStatsRecord>();
            mAllSuiteStatsRecords = new List<DlkTestSuiteStatsRecord>();
            Dictionary<string, DlkTestSuiteResultsManifestRecord> mLatestSuiteDict = new Dictionary<string, DlkTestSuiteResultsManifestRecord>();

            List<DlkTestSuiteResultsManifestRecord> lstSuiteResults = DlkTestSuiteResultsManifestHandler.GetSuiteResultsManifestRecords();

            if (lstSuiteResults != null)
            {
                lstSuiteResults = lstSuiteResults.OrderByDescending(x => x.resultsdirectory).ToList();
            }

            /* Determoine latest suite executions */
            Dictionary<string, string> latestTracker = new Dictionary<string, string>();
            foreach (DlkTestSuiteResultsManifestRecord allrec in lstSuiteResults)
            { 
                if(!Directory.Exists(allrec.resultsdirectory))
                {
                    DlkLogger.LogToFile(string.Format("[Generate Dashboard] Folder '{0}' does not exist", allrec.resultsdirectory));
                    continue;
                }

                if (latestTracker.ContainsKey(allrec.suitepath))
                {
                    if (latestTracker[allrec.suitepath].CompareTo(allrec.resultsdirectory) < 0)
                    {
                        latestTracker[allrec.suitepath] = allrec.resultsdirectory;
                    }
                }
                else
                {
                    latestTracker.Add(allrec.suitepath, allrec.resultsdirectory);
                }
            }

            /* Add all */
            foreach (DlkTestSuiteResultsManifestRecord allrec in lstSuiteResults)
            {
                if (!Directory.Exists(allrec.resultsdirectory))
                {
                    continue;
                }

                mTestsExecuted = allrec.passed + allrec.failed;
                mTestCount = mTestsExecuted + allrec.notrun;  
                DlkTestSuiteStatsRecord mSuiteStatsRecord = new DlkTestSuiteStatsRecord(
                    allrec.resultsdirectory, allrec.suitepath, allrec.suite,
                    mTestCount, mTestsExecuted, allrec.passed, allrec.failed, DlkGenerateDashboard.GetDateStringFromPath(allrec.resultsdirectory),
                    latestTracker[allrec.suitepath] == allrec.resultsdirectory);
                mAllSuiteStatsRecords.Add(mSuiteStatsRecord);
            }

            /* Compute summary stats */
            foreach (DlkTestSuiteStatsRecord stat in mAllSuiteStatsRecords.FindAll(x => x.mIsLatest))
            {
                mTotalTestsExecuted += stat.mTestsExecuted;
                mTotalTestCount += stat.mTestCount;
                mTotalTestsPassed += stat.mTestsPassed;
                mTotalTestsFailed += stat.mTestsFailed;
            }

            mSummaryRec = new DlkSummaryStatsRecord(latestTracker.Count, mTotalTestCount, mTotalTestsExecuted, mTotalTestsPassed, mTotalTestsFailed);
        }

        /// <summary>
        /// write the summary statistics to a file
        /// </summary>
        private static void WriteStats()
        {
            try
            {
                List<XElement> mElms = new List<XElement>();
                int mId = 0;

                foreach (DlkTestSuiteStatsRecord mRec in mAllSuiteStatsRecords)
                {
                    mId++;
                    XElement mElm = new XElement("suiteresult",
                        new XAttribute("suitedetails", "File_" + mId.ToString() + ".xml"),
                        new XAttribute("suitename", mRec.mTestSuiteName),
                        new XAttribute("suitepath", mRec.mTestSuitePath),
                        new XAttribute("suiteresultpath", mRec.mTestSuiteResultPath),
                        new XAttribute("testcount", mRec.mTestCount),
                        new XAttribute("testsexecuted", mRec.mTestsExecuted),
                        new XAttribute("testspassed", mRec.mTestsPassed),
                        new XAttribute("testsfailed", mRec.mTestsFailed),
                        new XAttribute("testsfailedpercentage", mRec.mTestsFailedPercentage),
                        new XAttribute("executiondate", mRec.mExecutionDate),
                        new XAttribute("islatest", mRec.mIsLatest.ToString().ToLower())
                        );
                    mElms.Add(mElm);
                }

                XElement mAllSuiteResultsRoot = new XElement("allsuiteresults", mElms);

                XElement mSummaryStatsRoot = new XElement("summarystatistics",
                    new XAttribute("totalsuitecount", mSummaryRec.mSuiteCount),
                    new XAttribute("totaltestcount", mSummaryRec.mTestCount),
                    new XAttribute("totaltestsexecuted", mSummaryRec.mTestsExecuted),
                    new XAttribute("totaltestspassed", mSummaryRec.mTestsPassed),
                    new XAttribute("totaltestsfailed", mSummaryRec.mTestsFailed),
                    new XAttribute("totaltestsfailedpercent", mSummaryRec.mTestsFailedPercentage)
                    );

                XElement mRoot = new XElement("summary", mSummaryStatsRoot, mAllSuiteResultsRoot);

                XDocument mDoc = new XDocument(mRoot);
                mDoc.AddFirst(new XProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Path.Combine(DlkEnvironment.mDirProductsRoot, "Common\\Dashboard") + "\\Summary.xsl\""));
                mDoc.Save(mSummaryWorkingFilePath);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile("[Generate Dashboard] Error in creating summary file", ex);
            }
        }
    }
}
