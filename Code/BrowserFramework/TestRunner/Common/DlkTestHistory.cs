using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;

namespace TestRunner.Common
{
    public class DlkTestHistory
    {
        String mSuite;
        DlkExecutionQueueRecord mSelectedTest;
        public List<DlkExecutionQueueRecord> mTestHistory;


        public DlkTestHistory(String TestSuite, DlkExecutionQueueRecord SelectedTest)
        {
            mSuite = TestSuite;
            mSelectedTest = SelectedTest;
            mTestHistory = new List<DlkExecutionQueueRecord>();
        }

        /// <summary>
        /// Load test history of target test
        /// </summary>
        public void LoadHistory()
        {
            List<String> mPossibleDates = DlkTestSuiteResultsFileHandler.GetExecutionDatesForSuite(mSuite);
            foreach (String mPossibleDate in mPossibleDates)
            {
                List<DlkExecutionQueueRecord> mRecs = DlkTestSuiteResultsFileHandler.GetResults(mSuite, mPossibleDate);
                foreach (DlkExecutionQueueRecord mRec in mRecs)
                {
                    if (mSelectedTest.identifier == mRec.identifier)
                    {
                        mTestHistory.Add(mRec);
                    }
                }
            }
        }
    }
}
