using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.AdvancedScheduler.View;
using System.Text.RegularExpressions;

namespace TestRunner.AdvancedScheduler.Presenter
{    
    /// <summary>
    /// Presentation logic of IResultsDetailsView
    /// </summary>
    public class ResultsDetailsPresenter
    {
        #region PRIVATE MEMBERS
        private IResultsDetailsView mView;
        private Dictionary<string, string> mResultsManifest = new Dictionary<string, string>();
        private string mScreenShotPath = string.Empty;
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">Required view instance to use this presenter class</param>
        public ResultsDetailsPresenter(IResultsDetailsView view)
        {
            mView = view;
        }

        /// <summary>
        /// Load target suite
        /// </summary>
        public void Load()
        {
            string summaryPath = Path.Combine(mView.SuiteResultsPath, "summary.dat");
            if (!File.Exists(summaryPath))
            {
                mView.DisplayError("An error was encountered locating summary results data file.");
                return;
            }

            try
            {
                /* Suite Info */
                Dictionary<string, string> suiteInfo = DlkTestSuiteResultsFileHandler.GetSummaryAttributeValues(summaryPath);

                mView.Description = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.DESCRIPTION)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.DESCRIPTION] : string.Empty;
                mView.UserName = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.USERNAME)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.USERNAME] : string.Empty;
                mView.OperatingSystem = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.OPERATINGSYSTEM)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.OPERATINGSYSTEM] : string.Empty;
                mView.MachineName = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.MACHINENAME)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.MACHINENAME] : string.Empty;
                mView.Duration = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.ELAPSED)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.ELAPSED] : string.Empty;
                string folderName = new DirectoryInfo(mView.SuiteResultsPath).Name;
                mView.ExecutionDate = GetFormattedDate(folderName);

                /* Suite Statistics */
                mView.Passed = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.PASSED)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.PASSED] : string.Empty;
                mView.Failed = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.FAILED)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.FAILED] : string.Empty;
                mView.NotRun = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.NOTRUN)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.NOTRUN] : string.Empty;
                mView.Total = suiteInfo.ContainsKey(DlkTestSuiteInfoAttributes.TOTAL)
                    ? suiteInfo[DlkTestSuiteInfoAttributes.TOTAL] : string.Empty;
                mView.PassRate = GetPassRate(mView.Passed, mView.Total, mView.NotRun);
                mView.CompletionRate = GetCompletionRate(mView.Passed, mView.Failed, mView.Total);

                /* Suite Results */
                string[] arrProduct = mView.SuitePath.Split('\\');
                bool flag = true;

                while(flag)
                { 
                    if(arrProduct[0] != "BrowserFramework")
                    {
                        arrProduct = arrProduct.Where(x => x != arrProduct[0]).ToArray();
                    }
                    else
                    {
                        flag = false;
                    }
                }

                Regex regex = new Regex("Products\\\\.*\\\\Tests");
                string path = regex.Replace(DlkEnvironment.mDirTests, $"Products\\{arrProduct[2]}\\Tests");

                List<DlkExecutionQueueRecord> actual = DlkTestSuiteXmlHandler.Load(mView.SuitePath, path);
                List<DlkExecutionQueueRecord> result = DlkTestSuiteResultsFileHandler.GetResults(mView.SuitePath, folderName);
                UpdateExecutionQueueRecords(actual, result, DlkTestSuiteResultsFileHandler.GetTests(summaryPath));
                mView.ExecutionQueueRecords = actual;
            }
            catch
            {
                throw; // re-throw to View
            }
        }

        /// <summary>
        /// Load logs of current test
        /// </summary>
        public void LoadLogs()
        {
            try
            {
                string currentlyDisplayedLog = GetLogName();

                /* Clear logs, set to null */
                mView.Logs = null;

                if (File.Exists(currentlyDisplayedLog))
                {
                    DlkTest currentTest = new DlkTest(currentlyDisplayedLog);
                    // update parameter count
                    foreach (DlkTestStepRecord step in currentTest.mTestSteps)
                    {
                        step.mCurrentInstance = currentTest.mTestInstanceExecuted;
                    }

                    DlkTestStepRecord setup = new DlkTestStepRecord
                    {
                        mExecute = "true",
                        mScreen = "Test Setup",
                        mControl = "",
                        mKeyword = "",
                        mStepDelay = 0,
                        mStepStatus = "",
                        mStepLogMessages = currentTest.mTestSetupLogMessages,
                        mStepStart = new DateTime(),
                        mStepEnd = new DateTime(),
                        mStepElapsedTime = "",
                        mParameters = new List<string>()
                    };

                    setup.mStepStatus = setup.mStepLogMessages.FindAll(x => x.mMessageType.ToLower().Contains(
                        "exception")).Count > 0 ? "failed" : "passed";

                    for (int idx = 0; idx < currentTest.mInstanceCount; idx++)
                    {
                        setup.mParameters.Add("");
                    }
                    currentTest.mTestSteps.Insert(0, setup);

                    MaskedParameters(currentTest.mTestSteps);
                    mView.Logs = currentTest.mTestSteps;
                }
                else
                {
                    currentlyDisplayedLog = string.Empty;
                }

                /* Load screenshot */
                mView.ErrorScreenShot = LoadErrorScreenShot(currentlyDisplayedLog);
            }
            catch
            {
                throw; // re-throw to View
            }
        }

        /// <summary>
        /// Display error screenshot in MSPaint
        /// </summary>
        public void DisplayScreenshot()
        {
            try
            {
                if (mView.ErrorScreenShot == null)
                {
                    return;
                }
                Uri path = mView.ErrorScreenShot.UriSource;

                if (path != null)
                {
                    string originaPath = path.OriginalString;
                    if (File.Exists(originaPath))
                    {
                        DlkProcess.RunProcess("OpenMspaint.bat", "\"" + originaPath + "\"", AppDomain.CurrentDomain.BaseDirectory, true, false);
                    }
                }
            }
            catch
            {
                throw; // re-throw to View
            }
        }

        /// <summary>
        /// Display error link image from logs grid
        /// </summary>
        /// <param name="path"></param>
        public void DisplayLinkedImage(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    DlkProcess.RunProcess("OpenMspaint.bat", "\"" + path + "\"", AppDomain.CurrentDomain.BaseDirectory, true, false);
                }
            }
            catch
            {
                mView.DisplayError("An error was encountered displaying linked image.");
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Masked control parameters for selected test steps
        /// </summary>
        /// <param name="steps">current selected test steps</param>
        private void MaskedParameters(List<DlkTestStepRecord> steps)
        {
            foreach (DlkTestStepRecord step in steps)
            {
                if (step.mPasswordParameters != null)
                {
                    for (int index = 0; index < step.mParameters.Count(); index++)
                    {
                        string[] arrParameters = step.mParameters[index].Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                        for (int i = 0; i < arrParameters.Count(); i++)
                        {
                            if (DlkPasswordMaskedRecord.IsMaskedParameter(step, i) && !DlkData.IsDataDrivenParam(step.mPasswordParameters[i]))
                            {
                                string maskedText = "";
                                foreach (char item in arrParameters[i])
                                    maskedText += DlkPasswordMaskedRecord.PasswordChar;

                                arrParameters[i] = !String.IsNullOrWhiteSpace(maskedText) ? maskedText : DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE;
                            }
                        }
                        step.mParameters[index] = string.Join(DlkTestStepRecord.globalParamDelimiter, arrParameters);
                    }
                }
            }
        }

        /// <summary>
        /// Get pass rate of test suite result
        /// </summary>
        /// <param name="passed">number of passed tests</param>
        /// <param name="total">number of total tests</param>
        /// <returns>pass rate</returns>
        private string GetPassRate(string passed, string total, string notrun)
        {
            int dPassed, dTotal, dNotRun;

            if (!int.TryParse(passed, out dPassed) || !int.TryParse(total, out dTotal) || !int.TryParse(notrun, out dNotRun))
            {
                return string.Empty;
            }

            return (dNotRun == dTotal) ? "0%" : string.Format("{0:F0}", (dPassed * 100 /(dTotal - dNotRun))) + "%";
        }

        /// <summary>
        /// Get completion rate of test suite result
        /// </summary>
        /// <param name="passed">number of passed tests</param>
        /// <param name="failed">number of failed tests</param>
        /// <param name="total">number of total tests</param>
        /// <returns>pass rate</returns>
        private string GetCompletionRate(string passed, string failed, string total)
        {
            int dPassed, dFailed, dTotal;

            if (!int.TryParse(passed, out dPassed) || !int.TryParse(failed, out dFailed) || !int.TryParse(total, out dTotal))
            {
                return string.Empty;
            }

            return string.Format("{0:F0}", ((dPassed + dFailed) * 100 / dTotal)) + "%";
        }

        /// <summary>
        /// Update input test list with results info
        /// </summary>
        /// <param name="act">Test list</param>
        /// <param name="res">Results info</param>
        /// <param name="testInfo">Test info</param>
        private void UpdateExecutionQueueRecords(List<DlkExecutionQueueRecord> act, List<DlkExecutionQueueRecord> res, Dictionary<string, Dictionary<string, string>> testInfo)
        {
            for (int idx = 0; idx < act.Count; idx++)
            {
                // match test and result based on ID
                if (res.FindAll(x => x.identifier == act[idx].identifier).Count > 0)
                {
                    DlkExecutionQueueRecord hit = res.Find(x => x.identifier == act[idx].identifier);
                    act[idx].executedsteps = hit.executedsteps;
                    act[idx].teststatus = hit.teststatus;
                    act[idx].duration = hit.duration;

                    //get browser and environment data
                    if (testInfo.ContainsKey(act[idx].identifier))
                    {
                        act[idx].Browser.Name = testInfo[act[idx].identifier]["browser"];
                        act[idx].environment = testInfo[act[idx].identifier]["environment"];
                    }

                    if (!mResultsManifest.ContainsKey(act[idx].identifier))
                    {
                        mResultsManifest.Add(act[idx].identifier, hit.file);
                    }
                }
                else
                {
                    //Updates the record browser to the overriden browser from the unexecuted tests in the suite resutls.
                    if (act[idx].Browser.Name != testInfo[act[idx].identifier]["browser"])
                    {
                        act[idx].Browser.Name = testInfo[act[idx].identifier]["browser"];
                    }
                }
            }
        }

        /// <summary>
        /// Get formatted date from raw results folder name
        /// </summary>
        /// <param name="date">Raw folder name</param>
        /// <returns>Formatted date</returns>
        private string GetFormattedDate(string date)
        {
            date = date.Insert(4, "-");
            // 2013-0718123423

            date = date.Insert(7, "-");
            // 2013-07-18123423

            date = date.Insert(10, " ");
            // 2013-07-18 123423

            date = date.Insert(13, ":");
            // 2013-07-18 12:3423

            date = date.Insert(16, ":");
            // 2013-07-18 12:34:23

            string inputFormat = "yyyy-MM-dd HH:mm:ss";
            string outputFormat = "yyyy-MM-dd hh:mm:ss tt";
            DateTime suiteDate = DateTime.ParseExact(date, inputFormat, System.Globalization.CultureInfo.InvariantCulture);
            date = suiteDate.ToString(outputFormat, System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }

        /// <summary>
        /// Get log name of test
        /// </summary>
        /// <returns></returns>
        private String GetLogName()
        {
            String mLog = string.Empty;

            if (mView.SelectedTest != null && mResultsManifest.ContainsKey(mView.SelectedTest.identifier))
            {
                mLog = Path.Combine(mView.SuiteResultsPath, mResultsManifest[mView.SelectedTest.identifier]);

                if (!File.Exists(mLog))
                {
                    mLog = string.Empty;
                }
            }
            return mLog;
        }

        /// <summary>
        /// Gets error screenshot
        /// </summary>
        /// <returns>Error screenshot</returns>
        private BitmapImage LoadErrorScreenShot(string logName)
        {
            BitmapImage ret = null;

            try
            {
                if (mView.SelectedTest == null)
                {
                    return ret;
                }

                if (File.Exists(logName))
                {
                    DlkTest currentTest = new DlkTest(logName);
                    int targetIndex = currentTest.mTestFailedAtStep - 1;
                    if (targetIndex >= 0)
                    {
                        foreach (DlkLoggerRecord log in currentTest.mTestSteps[targetIndex].mStepLogMessages)
                        {
                            if (log.mMessageType == "EXCEPTIONIMG")
                            {
                                string myLog = log.mMessageDetails.Substring(log.mMessageDetails.IndexOf(' ') + 1);
                                ret = new BitmapImage(new Uri(Path.Combine(Path.GetDirectoryName(logName), 
                                    Path.GetFileName(myLog))));
                                break;
                            }
                        }
                    }
                    else // check if it is SETUP
                    {
                        foreach (DlkLoggerRecord log in currentTest.mTestSetupLogMessages)
                        {
                            if (log.mMessageType == "EXCEPTIONIMG")
                            {
                                string myLog = log.mMessageDetails.Substring(log.mMessageDetails.IndexOf(' ') + 1);
                                ret = new BitmapImage(new Uri(Path.Combine(Path.GetDirectoryName(logName), 
                                    Path.GetFileName(myLog))));
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Do nothing if getting screenshot failed
            }

            return ret;
        }
        #endregion
    }
}
