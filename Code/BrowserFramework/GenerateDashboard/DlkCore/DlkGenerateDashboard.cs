using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using GenerateDashboard.DlkRecords;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenerateDashboard.DlkCore
{
    /// <summary>
    /// this program generates the html pages for a local Dashboard
    /// </summary>
    static class DlkGenerateDashboard
    {
        public static void Execute()
        {
            // initializtion
            DlkObjectStoreHandler.Initialize(true);

            // generate the Summary Page
            List<DlkTestSuiteStatsRecord> mSuiteStatsRecords = DlkGenerateSummary.Execute();

            // generate the Suite Details Pages
            DlkGenerateSuiteDetails.Execute(mSuiteStatsRecords);

            // publish
            Publish();
        }

        /// <summary>
        /// remove old files and add new ones
        /// </summary>
        private static void Publish()
        {
            try
            {
                DlkEnvironment.EmptyFolder(DlkEnvironment.mDirDataFrameworkDashboardRepositoryPublished);
                FileInfo[] mFiles = new DirectoryInfo(DlkEnvironment.mDirDataFrameworkDashboardRepositoryWorking).GetFiles();
                foreach (FileInfo mFile in mFiles)
                {
                    mFile.CopyTo(DlkEnvironment.mDirDataFrameworkDashboardRepositoryPublished + mFile.Name, true);
                }
                Thread.Sleep(1000);
            }
            catch(Exception ex) 
            {
                DlkLogger.LogToFile("[Generate Dashboard] Error in publishing file", ex);
            }
        }

        /// <summary>
        /// Get date string from input absolute path
        /// </summary>
        /// <param name="AbsolutePath"></param>
        /// <returns></returns>
        public static string GetDateStringFromPath(string AbsolutePath)
        {
            string ret = string.Empty;
            if (Directory.Exists(AbsolutePath))
            {
                String mDisplay = new DirectoryInfo(AbsolutePath).Name;
                // 20130718123423

                mDisplay = mDisplay.Insert(4, "-");
                // 2013-0718123423

                mDisplay = mDisplay.Insert(7, "-");
                // 2013-07-18123423

                mDisplay = mDisplay.Insert(10, " ");
                // 2013-07-18 123423

                mDisplay = mDisplay.Insert(13, ":");
                // 2013-07-18 12:3423

                mDisplay = mDisplay.Insert(16, ":");
                // 2013-07-18 12:34:23

                ret = mDisplay;
            }
            return ret;
        }
    }
}
