#define ATK_RELEASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using CommonLib.DlkUtility;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.Devices;

namespace CommonLib.DlkHandlers
{
    public static class DlkTestSuiteResultsFileHandler
    {
        public static String mSuiteDescription = "";
        public static String mSuiteOwner = "";
        public static String mBrowser = "";
        public static String mEnvironment = "";
        private static DlkTestSuiteInfoRecord mSuiteInfo = null;

        /// <summary>
        /// Saves the test suite results from TestQueue to a xml file
        /// </summary>
        /// <param name="SuitePath"></param>
        /// <param name="Results"></param>
        public static string SaveSuiteResults(String SuitePath, string ResultsPath)
        {
            try
            {
                String mDir = DlkString.GetDateAsText("file");
                DirectoryInfo suiteResults = Directory.CreateDirectory(Path.Combine(DlkEnvironment.mDirSuiteResults,
                    Path.GetFileNameWithoutExtension(SuitePath), mDir));

                FileInfo[] mFiles = new DirectoryInfo(ResultsPath).GetFiles();
                foreach (FileInfo mFile in mFiles)
                {
                    File.Copy(mFile.FullName, Path.Combine(suiteResults.FullName, mFile.Name));
                }

                return suiteResults.FullName;

                //Store suite results in manifest
                //AddSuiteResultsManifestEntry(SuitePath, ResultsPath);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile("[Suite Results] Unable to create suite results file.", ex);
                return "";
            }
        }

        public static void CreateSuiteResultsSummary(String SuitePath, String SuiteResultsFolder, List<DlkExecutionQueueRecord> AllTests, int RunNumber, int NumberOfRuns, String BrowserOverride, String EnvironmentOverride, String ScheduledStart)
        {
            try
            {
                string outputFile = Path.Combine(SuiteResultsFolder, "summary.dat");
                string suiteName = new DirectoryInfo(SuiteResultsFolder).Parent.ToString();
                FileInfo[] resultFiles = new DirectoryInfo(SuiteResultsFolder).GetFiles("*.xml");
                TimeSpan timeElapsed = TimeSpan.Zero;
                //int totalsteps = 0;
                //int executedsteps = 0;

                // Get all tags on suite
                XElement elemTags = GetTags(mSuiteInfo.Tags);

                // Create collection of DLkTest -> List<DlkTest>
                List<DlkTest> testFile = new List<DlkTest>();
                int failedTestCount = 0;
                int passedTestCount = 0;

                /* Add all tests with results */
                foreach (FileInfo fi in resultFiles)
                {
                    DlkTest tfile = new DlkTest(fi.FullName);
                    testFile.Add(tfile);
                }

                /* Need to include in summary, tests that did not run */
                foreach (DlkExecutionQueueRecord eqr in AllTests)
                {
                    if (testFile.FindAll(x => x.mIdentifier == eqr.identifier).Count == 0)
                    {
                        DlkTest testToAdd = new DlkTest(Path.Combine(DlkEnvironment.mDirTests.Trim('\\'), eqr.folder.Trim('\\'), eqr.file));
                        testToAdd.mIdentifier = eqr.identifier;
                        testToAdd.mTestInstanceExecuted = int.Parse(eqr.instance);
                        testToAdd.mTestStatus = "not run";
                        testFile.Add(testToAdd);
                    }
                }

                // Save collection to "summary.xml"
                // create elements
                List<XElement> Elms = new List<XElement>();
                foreach (DlkExecutionQueueRecord eqr in AllTests)
                {
                    DlkTest writeTest = testFile.Find(x => x.mIdentifier == eqr.identifier);
                    int i = 0;
                    int executedsteps = writeTest.mExecutedTestSteps == null ? 0 : writeTest.mExecutedTestSteps.Count;
                    string err_message = "";
                    string err_outputfile = "";
                    string err_stack = "";
                    string err_img = "";
                    string err_step = "";
                    string err_lastInfoMsg = "";

                    // Get all tags on test
                    XElement eTestTags = GetTags(writeTest.mTags);

                    // Retrieve browser and env ID from test script
                    DlkExecutionQueueRecord rec = DlkTestSuiteXmlHandler.GetExecutionQueueRecordByScriptTestID(writeTest.mIdentifier);
                    mBrowser = rec.Browser.Name;
                    mEnvironment = rec.environment;
                    //check if there are overrides, if yes - use them
                    if (!string.IsNullOrEmpty(BrowserOverride))
                    {
                        mBrowser = "*" + BrowserOverride;
                    }
                    if (!string.IsNullOrEmpty(EnvironmentOverride))
                    {
                        mEnvironment = EnvironmentOverride;
                    }

                    TimeSpan elapsed;
                    if (TimeSpan.TryParse(writeTest.mTestElapsed, out elapsed))
                    {
                        timeElapsed = timeElapsed.Add(elapsed);
                    }

                    if (writeTest.mTestStatus == "failed")
                    {
                        MaskedTestSteps(writeTest.mTestSteps);
                        failedTestCount++;

                        if (writeTest.mTestSetupLogMessages.FindAll(x => x.mMessageType.ToLower().Contains("exception")).Count > 0)
                        {
                            if (writeTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONMSG").Count > 0)
                            {
                                err_message = writeTest.mTestSetupLogMessages.FindAll(
                                    x => x.mMessageType == "EXCEPTIONMSG").First().mMessageDetails;
                            }
                            if (writeTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "OUTPUTFILE").Count > 0)
                            {
                                err_outputfile = writeTest.mTestSetupLogMessages.FindAll(
                                   x => x.mMessageType == "OUTPUTFILE").First().mMessageDetails;
                            }
                            if (writeTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONSTACK").Count > 0)
                            {
                                err_stack = writeTest.mTestSetupLogMessages.FindAll(
                                    x => x.mMessageType == "EXCEPTIONSTACK").First().mMessageDetails;
                            }
                            if (writeTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONIMG").Count > 0)
                            {
                                err_img = writeTest.mTestSetupLogMessages.FindAll(
                                    x => x.mMessageType == "EXCEPTIONIMG").First().mMessageDetails;
                            }
                        }

                        else
                        {
                            i = writeTest.mTestFailedAtStep - 1;
#if ATK_RELEASE
                            if (!writeTest.mContinueOnError)
                            {
                                executedsteps -= 1;
                            }
#else
                        executedsteps -= 1;
#endif

                            for (int r = 0; r < writeTest.mTestSteps[i].mStepLogMessages.Count; r++)
                            {
                                if (writeTest.mTestSteps[i].mStepLogMessages[r].mMessageType == "EXCEPTIONMSG")
                                {
                                    err_message = "[ERROR] " + writeTest.mTestSteps[i].mStepLogMessages[r].mMessageDetails;
                                }
                                if (writeTest.mTestSteps[i].mStepLogMessages[r].mMessageType == "OUTPUTFILE")
                                {
                                    string msg = writeTest.mTestSteps[i].mStepLogMessages[r].mMessageDetails;
                                    msg = msg.Contains("outputfile:") ? msg.Substring(msg.LastIndexOf("outputfile:") + 12) : msg;
                                    err_outputfile = "[OUTPUTFILE] " + msg;
                                    
                                }
                                if (writeTest.mTestSteps[i].mStepLogMessages[r].mMessageType == "EXCEPTIONSTACK")
                                {
                                    err_stack = writeTest.mTestSteps[i].mStepLogMessages[r].mMessageDetails;
                                }
                                if (writeTest.mTestSteps[i].mStepLogMessages[r].mMessageType == "EXCEPTIONIMG")
                                {
                                    err_img = writeTest.mTestSteps[i].mStepLogMessages[r].mMessageDetails;
                                }
                                if (writeTest.mTestSteps[i].mStepLogMessages[r].mMessageType == "INFO")
                                {
                                    err_lastInfoMsg = "[INFO] " + writeTest.mTestSteps[i].mStepLogMessages[r].mMessageDetails;
                                }
                                string screenAliasString = "";
                                if (DlkEnvironment.IsShowAppNameProduct)
                                {
                                    int index = -1;
                                    if (DlkEnvironment.IsShowAppNameEnabled())
                                    {
                                        index = DlkDynamicObjectStoreHandler.Instance.Alias.IndexOf(writeTest.mTestSteps[i].mScreen);
                                        screenAliasString = " (" + DlkDynamicObjectStoreHandler.Instance.Screens[index] + ")";
                                        //Do not add screen alias string if app name already contains the app ID
                                        if (writeTest.mTestSteps[i].mScreen.Contains(screenAliasString)) { screenAliasString = string.Empty; }
                                    }
                                    else
                                    {
                                        index = DlkDynamicObjectStoreHandler.Instance.Screens.IndexOf(writeTest.mTestSteps[i].mScreen);
                                        screenAliasString = " (" + DlkDynamicObjectStoreHandler.Instance.Alias[index] + ")";
                                        //Remove app ID from screen alias string if it already contains the app ID
                                        string appID = " ("+ writeTest.mTestSteps[i].mScreen + ")";
                                        if (screenAliasString.Contains(appID)) { screenAliasString = screenAliasString.Replace(appID,""); }

                                    }
                                }
                               err_step = "[INFO] Step: " + (i + 1).ToString() + " Screen: " + writeTest.mTestSteps[i].mScreen + screenAliasString
                                    + " Control: " + writeTest.mTestSteps[i].mControl + " Keyword: " + writeTest.mTestSteps[i].mKeyword +
                                        " Parameters: " + writeTest.mTestSteps[i].mParameterString;
                            }
                        }
                    }
                    else if (writeTest.mTestStatus == "passed")
                    {
                        passedTestCount++;
                    }

                    XElement mrow = new XElement("test",
                        new XElement("name", writeTest.mTestName),
                        new XElement("filepath", writeTest.GetOriginalTestPath()),
                        new XElement("id", writeTest.mIdentifier),
                        new XElement("instance", writeTest.mTestInstanceExecuted.ToString()),
                        new XElement("browser", mBrowser),
                        new XElement("environment", mEnvironment),
                        new XElement("description", writeTest.mTestDescription),
                        new XElement("start_time", writeTest.mTestStart.ToString()),
                        new XElement("end_time", writeTest.mTestEnd.ToString()),
                        new XElement("elapsed", writeTest.mTestElapsed),
                        new XElement("status", writeTest.mTestStatus),
                        new XElement("step_desc", err_step),
                        new XElement("step_info", err_lastInfoMsg),
                        new XElement("errormessage", err_message),
                        new XElement("erroroutputfile", err_outputfile),
                        new XElement("errorstack", err_stack),
                        new XElement("errorscreenshot", err_img),
                        new XElement("executedsteps", executedsteps),
                        new XElement("totalsteps", writeTest.mTestSteps.Count),
                        eTestTags
                        );

                    Elms.Add(mrow);
                }

                XElement ElmRoot = new XElement("summary",
                    new XAttribute("name", suiteName),
                    new XAttribute("path", SuitePath),
                    new XAttribute("description", mSuiteDescription),
                    new XAttribute("owner", mSuiteOwner),
                    new XAttribute("elapsed", timeElapsed.ToString(@"hh\:mm\:ss\.ff")),
                    new XAttribute("total", testFile.Count),
                    new XAttribute("failed", failedTestCount),
                    new XAttribute("passed", passedTestCount),
                    new XAttribute("notrun", testFile.Count - (failedTestCount + passedTestCount)),
                    new XAttribute("machinename", Environment.MachineName),
                    new XAttribute("username", Environment.UserName),
                    new XAttribute("operatingsystem", new ComputerInfo().OSFullName),
                    new XAttribute("runnumber", RunNumber.ToString()),
                    new XAttribute("numberofruns", NumberOfRuns.ToString()),
                    new XAttribute("scheduledstart", ScheduledStart),
                    AddCustomInfoToRootElement(),
                    elemTags,
                    Elms);

                XDocument xDoc = new XDocument(ElmRoot);

                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }
                xDoc.Save(outputFile);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile("[Suite Results] Unable to create suite results summary file.", ex);
            }
        }

        /// <summary>
        /// Get the list of tag that have been used during suite run
        /// </summary>
        /// <param name="Tags">list of tags to add on xml element</param>
        /// <returns></returns>
        private static XElement GetTags(List<DlkTag> Tags)
        {
            List<XElement> tag = new List<XElement>();
            for (int j = 0; j < Tags.Count(); j++)
            {
                XElement _tag = new XElement("tag",
                        new XAttribute("id", Tags[j].Id),
                        new XAttribute("name", Tags[j].Name));

                tag.Add(_tag);
            }
            XElement eTags = new XElement("tags",
                tag);

            return eTags;
        }

        /// <summary>
        /// Masked password parameters
        /// </summary>
        /// <param name="steps">list of steps</param>
        private static void MaskedTestSteps(List<DlkTestStepRecord> steps)
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
                            if ((step.mPasswordParameters[i] != "" && !DlkData.IsDataDrivenParam(step.mPasswordParameters[i])) ||
                                (step.mPasswordParameters[i] != "" && !DlkData.IsOutPutVariableParam(step.mPasswordParameters[i])) ||
                                 (step.mPasswordParameters[i] != "" && !DlkData.IsGlobalVarParam(step.mPasswordParameters[i])))
                            {
                                string maskedText = "";
                                foreach (var item in arrParameters[i])
                                    maskedText += DlkPasswordMaskedRecord.PasswordChar;

                                arrParameters[i] = maskedText;
                            }
                        }
                        step.mParameters[index] = string.Join(DlkTestStepRecord.globalParamDelimiter, arrParameters);
                    }
                }
            }
        }

        /// <summary>
        /// Adds custom info to suite summary root element
        /// </summary>
        private static XElement AddCustomInfoToRootElement()
        {
            if (DlkEnvironment.CustomInfo != null)
            {
                XElement customElm = new XElement("custominfo");
                foreach (var info in DlkEnvironment.CustomInfo)
                {
                    string tagName = info.Key;
                    string attributeName = info.Value[0];
                    string infoValue = info.Value[1];

                    XElement custominfo = new XElement(tagName, 
                        new XAttribute("display", attributeName), infoValue);
                    customElm.Add(custominfo);
                }
                return customElm;
            }
            else
            {
                return null;
            }
        }

        private static string GetTestIDFromFileName(string Filename)
        {
            string ret = string.Empty;
            try
            {
                int targetIndex = Filename.LastIndexOf("_");

                if (targetIndex >= 0)
                {
                    ret = Filename.Substring(targetIndex + 1);
                }
            }
            catch
            {
                // just return an empty string
            }
            return ret;
        }

        /// <summary>
        /// Gets value of used Environment for whole suite if uniform, MIXED otherwise
        /// </summary>
        /// <param name="SummaryFileXDoc">Loaded summary.dat</param>
        /// <returns>Value of environment used, MIXED if not uniform</returns>
        private static string GetSummaryEnvironment(XDocument SummaryFileXDoc)
        {
            string ret = "--";
            var envUsed = SummaryFileXDoc.Descendants(DlkTestSuiteInfoAttributes.ENVIRONMENT).Select(x => x.Value);
            if (envUsed.Any())
            {
                var first = envUsed.First();
                ret = envUsed.Count(x => x == first) == envUsed.Count() ? first : "MIXED";
            }
            return ret.Trim('*');
        }

        /// <summary>
        /// Gets value of used Browser for whole suite if uniform, MIXED otherwise
        /// </summary>
        /// <param name="SummaryFileXDoc">Loaded summary.dat</param>
        /// <returns>Value of browser used, MIXED if not uniform</returns>
        private static string GetSummaryBrowser(XDocument SummaryFileXDoc)
        {
            string ret = "--";
            var browsersUsed = SummaryFileXDoc.Descendants(DlkTestSuiteInfoAttributes.BROWSER).Select(x => x.Value);
            if (browsersUsed.Any())
            {
                var first = browsersUsed.First();
                ret = browsersUsed.Count(x => x == first) == browsersUsed.Count() ? first : "MIXED";
            }
            return ret.Trim('*');
        }

        public static void AddSuiteResultsManifestEntry(string SuitePath)
        {
            string mResFile = "";
            List<DlkExecutionQueueRecord> res = GetLatestSuiteResults(SuitePath, out mResFile);
            int Passed = 0;
            int Failed = 0;
            int NotRun = 0;
            for(int i=0; i < res.Count; i++)
            {
                switch(res[i].teststatus)
                {
                    case "Passed":
                        Passed++;
                        break;
                    case "Failed":
                        Failed++;
                        break;
                    case "NotRun":
                        NotRun++;
                        break;
                }
            }
            DlkTestSuiteResultsManifestRecord resultManifestRec = new DlkTestSuiteResultsManifestRecord(Path.GetFileNameWithoutExtension(SuitePath), SuitePath, DateTime.Today.ToShortDateString(), Passed, Failed, NotRun, mResFile);
            DlkTestSuiteResultsManifestHandler.AddSuiteResultsManifestRecord(resultManifestRec);
        }

        /// <summary>
        /// Return a list of execution dates
        /// </summary>
        /// <param name="AbsoluteSuitePath"></param>
        /// <returns></returns>
        public static List<String> GetExecutionDatesForSuite(String AbsoluteSuitePath)
        {
            List<String> mResults = new List<string>();
            string path = Path.Combine(DlkEnvironment.mDirSuiteResults, Path.GetFileNameWithoutExtension(AbsoluteSuitePath));
            if (Directory.Exists(path))
            {
                foreach (DirectoryInfo dir in new DirectoryInfo(path).GetDirectories())
                {
                    String mDisplay = dir.Name;
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

                    mResults.Add(mDisplay);
                }
                mResults.Sort();
            }
            return mResults;
        }

        /// <summary>
        /// Return a list of execution dates
        /// </summary>
        /// <param name="SuitePath"></param>
        /// <returns></returns>
        public static List<String> GetAllExecutionDates()
        {
            List<String> mResults = new List<string>();
            if (Directory.Exists(DlkEnvironment.mDirTestResults))
            {
                foreach (DirectoryInfo dir in new DirectoryInfo(DlkEnvironment.mDirTestResults).GetDirectories())
                {
                    String mDisplay = dir.Name;
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

                    mResults.Add(mDisplay);
                }
                mResults.Sort();
            }
            return mResults;
        }

        /// <summary>
        /// Return a list of execution dates
        /// </summary>
        /// <param name="SuitePath"></param>
        /// <returns></returns>
        public static List<String> GetAllExecutionDirectories()
        {
            List<String> mResults = new List<string>();
            if (Directory.Exists(DlkEnvironment.mDirTestResults))
            {
                foreach (DirectoryInfo dir in new DirectoryInfo(DlkEnvironment.mDirTestResults).GetDirectories())
                {
                    mResults.Add(dir.Name);
                }
                mResults.Sort();
            }
            return mResults;
        }

        /// <summary>
        /// Returns the result records from the stored SuiteResults file
        /// </summary>
        /// <param name="ExecutionDate"></param>
        /// <returns></returns>
        public static List<DlkExecutionQueueRecord> GetResults(String AbsoluteSuitePath, String ExecutionDate)
        {
            string[] arrProduct = AbsoluteSuitePath.Split('\\');
            bool flag = true;

            while (flag)
            {
                if (arrProduct[0] != "BrowserFramework")
                {
                    arrProduct = arrProduct.Where(x => x != arrProduct[0]).ToArray();
                }
                else
                {
                    flag = false;
                }
            }

            Regex regex = new Regex("Products\\\\.*\\\\Framework");
            string path = regex.Replace(DlkEnvironment.mDirSuiteResults, $"Products\\{arrProduct[2]}\\Framework");

            List<DlkExecutionQueueRecord> mRes = new List<DlkExecutionQueueRecord>();
            String mExecDate = ExecutionDate.Replace("-", "").Replace(":", "").Replace(" ", "");
            String mDir = Path.Combine(path, Path.GetFileNameWithoutExtension(AbsoluteSuitePath), mExecDate);
            if (Directory.Exists(mDir))
            {
                DirectoryInfo suiteResultsDir = new DirectoryInfo(mDir);
                List<DlkTest> testList = new List<DlkTest>();
                foreach (FileInfo fi in suiteResultsDir.GetFiles("*.xml"))
                {
                    DlkTest test = new DlkTest(fi.FullName);
                    testList.Add(test);
                }
                testList = testList.OrderBy(x => x.mTestStart).ToList();
                foreach (DlkTest currTest in testList)
                {
                    DlkData.SubstituteExecuteDataVariables(currTest);
                    int executeTestCount = currTest.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count;

                    DlkExecutionQueueRecord eqr = new DlkExecutionQueueRecord(
                        currTest.mIdentifier, 
                        "",
#if ATK_RELEASE
                            currTest.mExecutedTestSteps.Count() + "/" + executeTestCount,
#else
                            currTest.mTestStatus == "failed" ?
                                Math.Max(0, currTest.mExecutedTestSteps.Count() - 1) + "/" + executeTestCount
                                : currTest.mExecutedTestSteps.Count() + "/" + executeTestCount,
#endif
                        DlkString.ToUpperIndex(currTest.mTestStatus, 0), 
                        "", 
                        currTest.mTestName,
                        currTest.mTestDescription, 
                        Path.GetFileName(currTest.mTestPath), 
                        currTest.mTestInstanceExecuted.ToString(), 
                        "", 
                        "", 
                        "", 
                        currTest.mTestElapsed, 
                        ExecutionDate, 
                        "", 
                        "", 
                        "", 
                        "",
                        currTest.mTestSteps.Count().ToString() 
                        );
                    mRes.Add(eqr);
                }
            }
            return mRes;
        }

        /// <summary>
        /// Returns the result records from the stored SuiteResults file
        /// </summary>
        /// <param name="ExecutionDate"></param>
        /// <returns></returns>
        public static List<DlkExecutionQueueRecord> GetLatestSuiteResults(String AbsoluteSuitePath, out String ResultFolderPath)
        {
            List<DlkExecutionQueueRecord> mRes = new List<DlkExecutionQueueRecord>();
            List<string> resultsFolders = GetExecutionDatesForSuite(AbsoluteSuitePath);
            ResultFolderPath = "";
            if (resultsFolders.Count > 0)
            {
                String ExecutionDate = resultsFolders.Last();
                String mExecDate = ExecutionDate.Replace("-", "").Replace(":", "").Replace(" ", "");
                String mDir = Path.Combine(DlkEnvironment.mDirSuiteResults, Path.GetFileNameWithoutExtension(AbsoluteSuitePath), mExecDate);
                ResultFolderPath = mDir;
                if (Directory.Exists(mDir))
                {
                    DirectoryInfo suiteResultsDir = new DirectoryInfo(mDir);

                    List<DlkTest> testList = new List<DlkTest>();
                    foreach (FileInfo fi in suiteResultsDir.GetFiles("*.xml"))
                    {
                        DlkTest test = new DlkTest(fi.FullName);
                        testList.Add(test);
                    }
                    testList = testList.OrderBy(x => x.mTestStart).ToList();
                    foreach (DlkTest currTest in testList)
                    {
                        DlkData.SubstituteExecuteDataVariables(currTest);
                        int executeTestCount = currTest.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count;

                        DlkExecutionQueueRecord eqr = new DlkExecutionQueueRecord(
                            currTest.mIdentifier, 
                            "",
#if ATK_RELEASE
                            currTest.mExecutedTestSteps.Count() + "/" + executeTestCount,
#else
                            currTest.mTestStatus == "failed" ?
                                Math.Max(0, currTest.mExecutedTestSteps.Count() - 1) + "/" + executeTestCount
                                : currTest.mExecutedTestSteps.Count() + "/" + executeTestCount,
#endif
                            DlkString.ToUpperIndex(currTest.mTestStatus, 0),  
                            "", 
                            currTest.mTestName,
                            currTest.mTestDescription, 
                            Path.GetFileName(currTest.mTestPath), 
                            currTest.mTestInstanceExecuted.ToString(), 
                            "", 
                            "", 
                            "", 
                            currTest.mTestElapsed, 
                            ExecutionDate, 
                            "", 
                            "", 
                            "", 
                            "",
                            currTest.mTestSteps.Count().ToString());
                        mRes.Add(eqr);
                    }
                }
            }
            return mRes;
        }

        /// <summary>
        /// Returns the result records from the stored SuiteResults file
        /// </summary>
        /// <param name="ExecutionDate"></param>
        /// <returns></returns>
        public static List<DlkExecutionQueueRecord> GetLatestResults(out string Folder)
        {
            List<DlkExecutionQueueRecord> mRes = new List<DlkExecutionQueueRecord>();
            List<string> resultsFolders = GetAllExecutionDates();
            Folder = string.Empty;
            if (resultsFolders.Count > 0)
            {
                String ExecutionDate = resultsFolders.Last();
                String mExecDate = ExecutionDate.Replace("-", "").Replace(":", "").Replace(" ", "");
                String mDir = Path.Combine(DlkEnvironment.mDirTestResults, mExecDate);
                Folder = mDir;
                if (Directory.Exists(mDir))
                {
                    DirectoryInfo suiteResultsDir = new DirectoryInfo(mDir);
                    List<DlkTest> testList = new List<DlkTest>();
                    foreach (FileInfo fi in suiteResultsDir.GetFiles("*.xml"))
                    {
                        DlkTest test = new DlkTest(fi.FullName);
                        testList.Add(test);
                    }
                    testList = testList.OrderBy(x => x.mTestStart).ToList();
                    foreach (DlkTest currTest in testList)
                    {
                        DlkData.SubstituteExecuteDataVariables(currTest);
                        int executeTestCount = currTest.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count;

                        DlkExecutionQueueRecord eqr = new DlkExecutionQueueRecord(
                            currTest.mIdentifier, 
                            "",
#if ATK_RELEASE
                            currTest.mExecutedTestSteps.Count() + "/" + executeTestCount,
#else
                            currTest.mTestStatus == "failed" ?
                                Math.Max(0, currTest.mExecutedTestSteps.Count() - 1) + "/" + executeTestCount
                                : currTest.mExecutedTestSteps.Count() + "/" + executeTestCount,
#endif
                            DlkString.ToUpperIndex(currTest.mTestStatus, 0), 
                            "", 
                            currTest.mTestName,
                            currTest.mTestDescription, 
                            Path.GetFileName(currTest.mTestPath), 
                            currTest.mTestInstanceExecuted.ToString(), 
                            "", 
                            "", 
                            "", 
                            currTest.mTestElapsed, 
                            ExecutionDate, 
                            "", 
                            "", 
                            "", 
                            "",
                            currTest.mTestSteps.Count().ToString());
                        mRes.Add(eqr);
                    }
                }
            }
            return mRes;
        }

        /// <summary>
        /// Gets the test suite description and owner for use in the summary report
        /// </summary>
        /// <param name="SuitePath"></param>
        /// <returns></returns>
        public static void GetTestSuiteDescription(string suitePath)
        {
            mSuiteInfo = DlkTestSuiteXmlHandler.GetTestSuiteInfo(suitePath);
            mSuiteDescription = mSuiteInfo.Description;
            mSuiteOwner = mSuiteInfo.Owner;
        }

        /// <summary>
        /// Get result using test id
        /// </summary>
        /// <param name="ResultsPath"></param>
        /// <param name="TestId"></param>
        /// <returns></returns>
        public static string GetTestResultUsingId(string ResultsPath, string TestId)
        {
            string ret = "not run";
            DirectoryInfo di = new DirectoryInfo(ResultsPath);

            foreach (FileInfo fi in di.GetFiles("*.xml"))
            {
                try
                {
                    DlkTest test = new DlkTest(fi.FullName);
                    if (test.mIdentifier == TestId)
                    {
                        ret = test.mTestStatus;
                        break;
                    }
                }
                catch
                {
                    continue; //swallow. just attempt to find from the rest of list.
                }
            }
            return ret;
        }

        /// <summary>
        /// Get suite attributes from summary.dat
        /// </summary>
        /// <param name="inputXML">Path of summary.dat</param>
        /// <returns>Dictionary of values</returns>
        public static Dictionary<string, string> GetSummaryAttributeValues(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);

            var summary = from itm in input.Descendants("summary")
                          select new
                          {
                              name = itm.Attribute(DlkTestSuiteInfoAttributes.NAME) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.NAME).Value : string.Empty,
                              path = itm.Attribute(DlkTestSuiteInfoAttributes.PATH) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.PATH).Value : string.Empty,
                              description = itm.Attribute(DlkTestSuiteInfoAttributes.DESCRIPTION) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.DESCRIPTION).Value : string.Empty,
                              username = itm.Attribute(DlkTestSuiteInfoAttributes.USERNAME) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.USERNAME).Value : string.Empty,
                              operatingsystem = itm.Attribute(DlkTestSuiteInfoAttributes.OPERATINGSYSTEM) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.OPERATINGSYSTEM).Value : string.Empty,
                              machinename = itm.Attribute(DlkTestSuiteInfoAttributes.MACHINENAME) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.MACHINENAME).Value : string.Empty,
                              total = itm.Attribute(DlkTestSuiteInfoAttributes.TOTAL) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.TOTAL).Value : string.Empty,
                              notrun = itm.Attribute(DlkTestSuiteInfoAttributes.NOTRUN) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.NOTRUN).Value : string.Empty,
                              passed = itm.Attribute(DlkTestSuiteInfoAttributes.PASSED) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.PASSED).Value : string.Empty,
                              failed = itm.Attribute(DlkTestSuiteInfoAttributes.FAILED) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.FAILED).Value : string.Empty,
                              elapsed = itm.Attribute(DlkTestSuiteInfoAttributes.ELAPSED) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.ELAPSED).Value : string.Empty,
                              runnumber = itm.Attribute(DlkTestSuiteInfoAttributes.RUNNUMBER) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.RUNNUMBER).Value : "1",
                              numberofruns = itm.Attribute(DlkTestSuiteInfoAttributes.NUMBEROFRUNS) != null
                                ? itm.Attribute(DlkTestSuiteInfoAttributes.NUMBEROFRUNS).Value : "1",
                          };

            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret.Add(DlkTestSuiteInfoAttributes.NAME, summary.First().name);
            ret.Add(DlkTestSuiteInfoAttributes.PATH, summary.First().path);
            ret.Add(DlkTestSuiteInfoAttributes.DESCRIPTION, summary.First().description);
            ret.Add(DlkTestSuiteInfoAttributes.USERNAME, summary.First().username);
            ret.Add(DlkTestSuiteInfoAttributes.OPERATINGSYSTEM, summary.First().operatingsystem);
            ret.Add(DlkTestSuiteInfoAttributes.MACHINENAME, summary.First().machinename);
            ret.Add(DlkTestSuiteInfoAttributes.TOTAL, summary.First().total);
            ret.Add(DlkTestSuiteInfoAttributes.NOTRUN, summary.First().notrun);
            ret.Add(DlkTestSuiteInfoAttributes.PASSED, summary.First().passed);
            ret.Add(DlkTestSuiteInfoAttributes.FAILED, summary.First().failed);
            ret.Add(DlkTestSuiteInfoAttributes.ELAPSED, summary.First().elapsed);
            ret.Add(DlkTestSuiteInfoAttributes.RUNNUMBER, summary.First().runnumber);
            ret.Add(DlkTestSuiteInfoAttributes.NUMBEROFRUNS, summary.First().numberofruns);
            ret.Add(DlkTestSuiteInfoAttributes.BROWSER, GetSummaryBrowser(input));
            ret.Add(DlkTestSuiteInfoAttributes.ENVIRONMENT, GetSummaryEnvironment(input));

            return ret;
        }

        /// <summary>
        /// Get tests from input XML
        /// </summary>
        /// <param name="inputXML">Path of summary.dat</param>
        /// <returns>Dictionary of values</returns>
        public static Dictionary<string, Dictionary<string, string>> GetTests(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);
            Dictionary<string, Dictionary<string, string>> ret = new Dictionary<string, Dictionary<string, string>>();

            var tests = from itm in input.Descendants("test")
                        select new
                        {
                            name = itm.Element(DlkTestAttributes.NAME).Value != null
                            ? itm.Element(DlkTestAttributes.NAME).Value : string.Empty,
                            filepath = itm.Element(DlkTestAttributes.FILEPATH).Value != null
                            ? itm.Element(DlkTestAttributes.FILEPATH).Value : string.Empty,
                            id = itm.Element(DlkTestAttributes.ID).Value != null
                            ? itm.Element(DlkTestAttributes.ID).Value : string.Empty,
                            instance = itm.Element(DlkTestAttributes.INSTANCE).Value != null
                            ? itm.Element(DlkTestAttributes.INSTANCE).Value : string.Empty,
                            browser = itm.Element(DlkTestAttributes.BROWSER).Value != null
                            ? itm.Element(DlkTestAttributes.BROWSER).Value : string.Empty,
                            environment = itm.Element(DlkTestAttributes.ENVIRONMENT).Value != null
                            ? itm.Element(DlkTestAttributes.ENVIRONMENT).Value : string.Empty,
                            description = itm.Element(DlkTestAttributes.DESCRIPTION).Value != null
                            ? itm.Element(DlkTestAttributes.DESCRIPTION).Value : string.Empty,
                            starttime = itm.Element(DlkTestAttributes.START_TIME).Value != null
                            ? itm.Element(DlkTestAttributes.START_TIME).Value : string.Empty,
                            endtime = itm.Element(DlkTestAttributes.END_TIME).Value != null
                            ? itm.Element(DlkTestAttributes.END_TIME).Value : string.Empty,
                            elapsed = itm.Element(DlkTestAttributes.ELAPSED).Value != null
                            ? itm.Element(DlkTestAttributes.ELAPSED).Value : string.Empty,
                            status = itm.Element(DlkTestAttributes.STATUS).Value != null
                            ? itm.Element(DlkTestAttributes.STATUS).Value : string.Empty,
                            stepdesc = itm.Element(DlkTestAttributes.STEP_DESC).Value != null
                            ? itm.Element(DlkTestAttributes.STEP_DESC).Value : string.Empty,
                            stepinfo = itm.Element(DlkTestAttributes.STEP_INFO).Value != null
                            ? itm.Element(DlkTestAttributes.STEP_INFO).Value : string.Empty,
                            errormessage = itm.Element(DlkTestAttributes.ERRORMESSAGE).Value != null
                            ? itm.Element(DlkTestAttributes.ERRORMESSAGE).Value : string.Empty,
                            erroroutputfile = itm.Element(DlkTestAttributes.ERROROUTPUTFILE).Value != null
                            ? itm.Element(DlkTestAttributes.ERROROUTPUTFILE).Value : string.Empty,
                            errorstack = itm.Element(DlkTestAttributes.ERRORSTACK).Value != null
                            ? itm.Element(DlkTestAttributes.ERRORSTACK).Value : string.Empty,
                            errorscreenshot = itm.Element(DlkTestAttributes.ERRORSCREENSHOT).Value != null
                            ? itm.Element(DlkTestAttributes.ERRORSCREENSHOT).Value : string.Empty,
                            executedsteps = itm.Element(DlkTestAttributes.EXECUTEDSTEPS).Value != null
                            ? itm.Element(DlkTestAttributes.EXECUTEDSTEPS).Value : string.Empty,
                            totalsteps = itm.Element(DlkTestAttributes.TOTALSTEPS).Value != null
                            ? itm.Element(DlkTestAttributes.TOTALSTEPS).Value : string.Empty
                        };

            foreach (var test in tests)
            {
                if (!ret.ContainsKey(test.id))
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    values.Add(DlkTestAttributes.NAME, test.name);
                    values.Add(DlkTestAttributes.FILEPATH, test.filepath);
                    values.Add(DlkTestAttributes.ID, test.id);
                    values.Add(DlkTestAttributes.INSTANCE, test.instance);
                    values.Add(DlkTestAttributes.BROWSER, test.browser);
                    values.Add(DlkTestAttributes.ENVIRONMENT, test.environment);
                    values.Add(DlkTestAttributes.DESCRIPTION, test.description);
                    values.Add(DlkTestAttributes.START_TIME, test.starttime);
                    values.Add(DlkTestAttributes.END_TIME, test.endtime);
                    values.Add(DlkTestAttributes.ELAPSED, test.elapsed);
                    values.Add(DlkTestAttributes.STATUS, test.status);
                    values.Add(DlkTestAttributes.STEP_DESC, test.stepdesc);
                    values.Add(DlkTestAttributes.STEP_INFO, test.stepinfo);
                    values.Add(DlkTestAttributes.ERRORMESSAGE, test.errormessage);
                    values.Add(DlkTestAttributes.ERROROUTPUTFILE, test.erroroutputfile);
                    values.Add(DlkTestAttributes.ERRORSTACK, test.errorstack);
                    values.Add(DlkTestAttributes.ERRORSCREENSHOT, test.errorscreenshot);
                    values.Add(DlkTestAttributes.EXECUTEDSTEPS, test.executedsteps);
                    values.Add(DlkTestAttributes.TOTALSTEPS, test.totalsteps);
                    ret.Add(test.id, values);
                }
            }

            return ret;
        }
    }


    /// <summary>
    /// Static class to store summary.dat suite info attributes
    /// </summary>
    public static class DlkTestSuiteInfoAttributes
    {
        public const string NAME = "name";
        public const string PATH = "path";
        public const string DESCRIPTION = "description";
        public const string USERNAME = "username";
        public const string OPERATINGSYSTEM = "operatingsystem";
        public const string MACHINENAME = "machinename";
        public const string TOTAL = "total";
        public const string NOTRUN = "notrun";
        public const string PASSED = "passed";
        public const string FAILED = "failed";
        public const string ELAPSED = "elapsed";
        public const string RUNNUMBER = "runnumber";
        public const string NUMBEROFRUNS = "numberofruns";
        public const string ID = "id";
        public const string BROWSER = "browser";
        public const string ENVIRONMENT = "environment";
    }

    /// <summary>
    /// Static class to store tests values from summary.dat
    /// </summary>
    public static class DlkTestAttributes
    {
        public const string NAME = "name";
        public const string FILEPATH = "filepath";
        public const string ID = "id";
        public const string INSTANCE = "instance";
        public const string BROWSER = "browser";
        public const string ENVIRONMENT = "environment";
        public const string DESCRIPTION = "description";
        public const string START_TIME = "start_time";
        public const string END_TIME = "end_time";
        public const string ELAPSED = "elapsed";
        public const string STATUS = "status";
        public const string STEP_DESC = "step_desc";
        public const string STEP_INFO = "step_info";
        public const string ERRORMESSAGE = "errormessage";
        public const string ERROROUTPUTFILE = "erroroutputfile";
        public const string ERRORSTACK = "errorstack";
        public const string ERRORSCREENSHOT = "errorscreenshot";
        public const string EXECUTEDSTEPS = "executedsteps";
        public const string TOTALSTEPS = "totalsteps";
    }
}
