using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;

namespace WorkBookLib.DlkSystem
{
    public class DlkWorkBookTestExecute : DlkTestExecute
    {
        private DlkWorkBookReleaseConfigHandler mReleaseConfigHandler;
        private DlkWorkBookRerunConfigHandler mRerunConfigHandler;
        public DlkWorkBookTestExecute(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBrowserOpen)
            : base(Product, LoginConfig, TestPath, TestInstance, Browser, KeepBrowserOpen)
        {
        }

        /// <summary>
        /// Override the base ExecuteTest as needed
        /// </summary>
        /// <returns></returns>
        public override string ExecuteTest()
        {
            return base.ExecuteTest();
        }

        /// <summary>
        /// Executes the Test Setup
        /// </summary>
        public override void TestSetup()
        {
            try
            {
                base.TestSetup(); // common setup for a test

                if (!IsBrowserOpen)
                {
                    bool bSuccess = false;
                    int retrySetupLimit = 3;
                    int retrySetup = 0;

                    while(!bSuccess & retrySetup <= retrySetupLimit)
                    {
                        try
                        {
                            LoadURL(mLoginConfigHandler.mUrl, mLoginConfigHandler.mDatabase);
                            Login(IsBrowserOpen.ToString());
                            GetBuildVersionAfterLogin();
                            bSuccess = true;
                        }
                        catch (Exception ex)
                        {
                            if (!bSuccess) //Retry if page load setup failed due to exception
                            {
                                DlkLogger.LogWarning("WebDriver encountered a fatal error during page load: " + ex.Message);
                                DlkEnvironment.CloseSession();

                                if (retrySetup == retrySetupLimit)
                                    throw new Exception("Retrying page load setup has reached its limit. Setup failed.");
                                else
                                {
                                    retrySetup++;
                                    DlkLogger.LogWarning("Re-launching browser after page load setup failure ...[" + retrySetup + "]");
                                    DlkEnvironment.StartBrowser();
                                }
                            }
                        }
                    }
                }
                else
                {
                    DisplayBuildVersion();
                }
            }
            catch (Exception ex)
            {
                IsSetupPassed = false;
                DlkLogger.LogError(ex);
            }
            finally
            {
                mTest.UpdateTestSetupLogMessages(DlkLogger.mLogRecs);
            }
        }

        public static int mGoToStep { get; set; }

        /// <summary>
        /// Executes the Test Steps
        /// </summary>
        public override void TestStepsExecute()
        {
            base.TestStepsExecute(); // common setup for test step execution

            DateTime mStart = DateTime.Now;
            DateTime mEnd;
            Boolean bFailed = false;
            mGoToStep = -1; // if this property gets changed we will skip the execution of steps
            mTest.UpdateTestStatus("passed");
            int retries = 0;
            int maxRetries = 1;

            for (int i = 0; i < mTest.mTestSteps.Count; i++) // for each test step
            {
                try
                {
                    while (DlkTestRunnerApi.mExecutionPaused)
                    {
                        Thread.Sleep(500);
                    }
                    DlkLogger.ClearLogger(); // the logger should only have messages for the current step; so, we clear it each time
                    mStart = DateTime.Now;
                    mCurrentTestStep = mTest.mTestSteps[i].mStepNumber;

                    if (bFailed) // the test failed, but we still need to fill in the results for the unexecuted test steps
                    {
                        mEnd = DateTime.Now;
                        mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "not run", new List<DlkLoggerRecord>());
                        continue;
                    }

                    DlkTestRunnerApi.CurrentRunningStep = mCurrentTestStep;

                    if (mGoToStep >= 0) // if-then-else logic has been used to alter test flow
                    {
                        for (int j = i; j < mGoToStep; j++)
                        {
                            mStart = DateTime.Now;
                            mCurrentTestStep = mTest.mTestSteps[j].mStepNumber;
                            mEnd = DateTime.Now;
                            mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "not run", new List<DlkLoggerRecord>());

                            if ((mGoToStep - 1) == j)
                            {
                                i = mGoToStep;
                                mStart = DateTime.Now;
                                mCurrentTestStep = mTest.mTestSteps[i].mStepNumber;
                            }
                        }
                        mGoToStep = -1;
                    }

                    // the parameters we use are based on the test instance (subtract 1 to account for zero based data internally)
                    String[] mTestParams = mTest.mTestSteps[i].mParameters[mTest.mTestInstanceExecuted - 1].Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);

                    // substitute any live variables that have been defined along the way (e.g. %DateTime% for the real date time)
                    DlkVariable.SetVariable("DateTime", DlkString.GetDateAsText("file"));
                    DlkVariable.SetVariable("DateTimeShort", DlkString.GetDateAsText("yymmddhhmmss"));
                    DlkVariable.SetVariable("DateCurrentMonthYear", DlkString.GetDateAsText("monthyear"));
                    DlkVariable.SetVariable("DateCurrentStartOfMonth", DlkString.GetDateAsText("startofmonth"));
                    DlkVariable.SetVariable("DateCurrentEndOfMonth", DlkString.GetDateAsText("endofmonth"));

                    // make all variable substitutions
                    mTestParams = DlkVariable.SubstituteVariables(mTestParams);

                    // check if execute value is valid (due to data-driven values enabled in this field)
                    bool execute = false;
                    if (!Boolean.TryParse(mTest.mTestSteps[i].mExecute, out execute))
                    {
                        DlkLogger.LogInfo("Invalid value supplied for Execute field: [" + mTest.mTestSteps[i].mExecute + "]");
                        mTest.mTestSteps[i].mExecute = "False";
                    }

                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader(string.Format("STEP {0}", mCurrentTestStep)));
                    DlkLogger.LogToOutputDisplay(string.Format("EXECUTE\t\t:\t{0}\r\nSCREEN\t\t:\t{1}\r\nCONTROL\t:\t{2}\r\nKEYWORD\t:\t{3}\r\nPARAM\t\t:\t{4}",
                        mTest.mTestSteps[i].mExecute, mTest.mTestSteps[i].mScreen, mTest.mTestSteps[i].mControl, mTest.mTestSteps[i].mKeyword,
                        string.Join("|", mTestParams)));
                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("+-+-+-+"));

                    /* Check if step should be turned-off from a prior call to TurnOffTestStep function call */
                    if (DlkFunctionHandler.mTurnedOffSteps.Contains(mTest.mTestSteps[i].mStepNumber.ToString()))
                    {
                        mEnd = DateTime.Now;
                        mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "not run", DlkLogger.mLogRecs);
                        DlkLogger.LogInfo("Step was turned off by a prior call to TurnOffTestSteps. Skipping test step...");
                    }
                    else if (execute)
                    {
                        if ((!string.IsNullOrEmpty(mTest.mTestSteps[i].mScreen) && string.IsNullOrEmpty(mTest.mTestSteps[i].mControl))
                            || ((mTest.mTestSteps[i].mScreen.ToLower() == "function") || (mTest.mTestSteps[i].mControl.ToLower() == "function"))
                            )
                        {
                            DlkWorkBookFunctionHandler.ExecuteFunction
                                (
                                mLoginConfigHandler.mDatabase,
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                        }
                        else
                        {
                            DlkWorkBookKeywordHandler.ExecuteKeyword(
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                        }
                        if (mTest.mTestSteps[i].mStepDelay > 0)
                        {
                            DlkLogger.LogInfo("Test Step delay found. Sleeping: " + mTest.mTestSteps[i].mStepDelay.ToString() + "(s)");
                            Thread.Sleep((mTest.mTestSteps[i].mStepDelay * 1000));
                        }
                        mEnd = DateTime.Now;
                        mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "passed", DlkLogger.mLogRecs);
                    }
                    else
                    {
                        mEnd = DateTime.Now;
                        mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "not run", DlkLogger.mLogRecs);
                        DlkLogger.LogInfo("Execution is false. Skipping test step...");
                    }
                    retries = 0;
                }
                catch (Exception ex) // the test failed during this step execution
                {
                    bool isStepFunction = mTest.mTestSteps[i].mScreen.ToLower().Contains("function") ||
                       mTest.mTestSteps[i].mScreen.ToLower().Contains("database") ||
                       mTest.mTestSteps[i].mScreen.ToLower().Contains("dialog"); //Ignore retry for steps using function keywords

                    if (!isStepFunction & retries <= maxRetries)
                    {
                        retries++;
                        int currentStep = i + 1;
                        i--;
                        DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("+-+-+-+"));
                        DlkLogger.LogWarning("Current step has failed. Retrying from step #" + currentStep.ToString() + "... ");
                        DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("+-+-+-+"));
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        DlkEnvironment.mKeepBrowserOpen = false;
                        DlkLogger.LogError(ex); // this is the only place you need to use LogError --> otherwise throw new Exception
                        mEnd = DateTime.Now;
                        mTest.UpdateTestFailedAtStep(mCurrentTestStep);
                        mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "failed", DlkLogger.mLogRecs);
                        mTest.UpdateTestStatus("failed");

                        /* Proceed to next step execution if continue on error is enabled */
                        if (mTest.mContinueOnError)
                        {
                            continue;
                        }
                        else
                        {
                            bFailed = true; // this forces the end of executing the other steps
                                            /* Flush turned off steps cache right after the test */
                            DlkFunctionHandler.mTurnedOffSteps.Clear();
                        }
                    }
                }
            }
            /* Flush turned off steps cache right after the test */
            DlkFunctionHandler.mTurnedOffSteps.Clear();
        }

        /// <summary>
        /// Executes the Test Teardown logic
        /// </summary>
        public override void TestTeardown()
        {
            base.TestTeardown(); // common teardown for a test

            mTest.UpdateTestTeardownLogMessages(DlkLogger.mLogRecs);
        }

        private void Login(string IsBrowserOpen)
        {
            DlkWorkBookFunctionHandler.ExecuteFunction("Database","Login", "Function", "Login",
                    new String[] { IsBrowserOpen, mLoginConfigHandler.mUrl, mLoginConfigHandler.mUser,
                    mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
        }

        /// <summary>
        /// Loads current url release site then selects latest build url
        /// </summary>
        /// <param name="Url">Target URL of script to execute</param>
        /// <param name="Database">Target database of the script to execute</param>
        public void LoadURL(string Url, string Database)
        {
            //Check if URL is a release site or specific version
            string validateURL1 = "cphv0017.ads.deltek.com";
            string validateURL2 = "workbookreleases.deltek.com:12345";

            if (!Url.Contains(validateURL1) && !Url.Contains(validateURL2))
            {
                UpdateBuildInfoConfigBeforeRun("NO", "");
                LoadSpecificVersion(Url);
            }
            else
            {
                DlkEnvironment.AutoDriver.Manage().Window.Size = new System.Drawing.Size(1296, 696); //Default screen resolution
                DlkEnvironment.AutoDriver.Url = Url;
                String windowSize = DlkEnvironment.AutoDriver.Manage().Window.Size.ToString();
                DlkLogger.LogInfo("Current window size is: " + windowSize);
                WaitForReleaseSiteToLoad(Url);

                string links_XPath;
                IList<IWebElement> activeLinks;

                string releaseOption = "";
                const string releaseOne = "releaseOne";
                const string releaseTwo = "releaseTwo";

                if (Url.Contains(validateURL1))
                {
                    releaseOption = releaseOne;
                }
                else if (Url.Contains(validateURL2))
                {
                    releaseOption = releaseTwo;
                }

                switch (releaseOption)
                {
                    //Release Site #1
                    case releaseOne:
                        links_XPath = "//div[contains(@class,'container body-content')]//li";
                        activeLinks = DlkEnvironment.AutoDriver.FindElements(By.XPath(links_XPath)).Count > 0 ?
                            DlkEnvironment.AutoDriver.FindElements(By.XPath(links_XPath)).Where(x => x.Displayed).ToList() :
                            throw new Exception("Active links not found.");
                        break;
                    //Release Site #2
                    case releaseTwo:
                        links_XPath = "//table[@id='siteList']//td";
                        activeLinks = DlkEnvironment.AutoDriver.FindElements(By.XPath(links_XPath)).Count > 0 ?
                            DlkEnvironment.AutoDriver.FindElements(By.XPath(links_XPath)).Where(x => x.Displayed).ToList() :
                            throw new Exception("Active links not found.");
                        var reversedList = (List<IWebElement>)activeLinks;
                        reversedList.Reverse();
                        activeLinks = (IList<IWebElement>)reversedList;
                        break;
                    default:
                        throw new Exception("Unknown release site.");
                }

                bool lFound = false;
                foreach (IWebElement link in activeLinks)
                {
                    string databaseBuildVersion = link.Text.Trim();
                    if (databaseBuildVersion.ToLower().Contains(Database.ToLower()) && !databaseBuildVersion.ToLower().Contains("rhino"))
                    {
                        IsRerunEnable(databaseBuildVersion);
                        UpdateBuildInfoConfigBeforeRun("YES", databaseBuildVersion);
                        bool isMismatchBuild = IsMismatchBuildInfoConfig();

                        if (isMismatchBuild)
                        {
                            continue;
                        }
                        else
                        {
                            //Select latest version
                            UpdateBuildInfoConfigAfterRun(databaseBuildVersion);
                            IWebElement linkHREF = link.FindElements(By.TagName("a")).Count > 0 ? link.FindElement(By.TagName("a")) :
                                throw new Exception("HREF link not found in target version");
                            string HREF_Url = linkHREF.GetAttribute("href");

                            //Load href url from target version
                            DlkLogger.LogInfo("Url: [" + HREF_Url + "]");
                            DlkEnvironment.AutoDriver.Url = HREF_Url;

                            GetBuildVersion();
                            lFound = true;
                            break;
                        }   
                    }
                }
                if (!lFound)
                    throw new Exception("Latest run build not found. It must have been removed or renamed.");
            }
        }

        /// <summary>
        /// Loads specific build url
        /// </summary>
        /// <param name="Url">Target URL of the script to execute</param>
        public static void LoadSpecificVersion(string Url)
        {
            DlkEnvironment.AutoDriver.Manage().Window.Size = new System.Drawing.Size(1296, 696); //Default screen resolution
            DlkEnvironment.AutoDriver.Url = Url;
            String windowSize = DlkEnvironment.AutoDriver.Manage().Window.Size.ToString();
            DlkLogger.LogInfo("Current window size is: " + windowSize);

            GetBuildVersion();
        }

        /// <summary>
        /// Waits for the release site to load
        /// </summary>
        /// <param name="Url">Target URL of the script to execute</param>
        public static void WaitForReleaseSiteToLoad(string Url)
        {
            //Wait for workbook page to load
            int waitPageLoad = 1;
            int waitPageLoadLimit = 61;
            string activeSite_XPath = "";

            if (Url.Contains("cphv0017.ads.deltek.com"))
            {
                activeSite_XPath = "//div[contains(@class,'container body-content')]";
            }
            else if (Url.Contains("workbookreleases.deltek.com:12345"))
            {
                activeSite_XPath = "//table[@id='siteList']";
            }
            else
            {
                throw new Exception("Unknown release site.");
            }

            while (waitPageLoad != waitPageLoadLimit)
            {
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(activeSite_XPath)).Count == 0)
                {
                    DlkLogger.LogInfo("Waiting for Release Site to load... [" + waitPageLoad.ToString() + "]s");
                    Thread.Sleep(1000);
                    waitPageLoad++;
                    continue;
                }
                else
                    break;
            }

            if (waitPageLoad == waitPageLoadLimit)
                throw new Exception("Waiting for Release Site to load has reached its limit (60s). Setup failed.");

        }

        /// <summary>
        /// Retrieves current build version from login page
        /// </summary>
        public static void GetBuildVersion()
        {
            //Store info in custom info for summary suite results
            if (DlkEnvironment.CustomInfo.Count == 0)
            {
                //Retrieve build version
                string buildVersion_XPath = "//div[@class='VersionNumber']";
                IWebElement mBuildVersion = null;

                //Wait for build version to appear
                int wait = 1;
                int waitLimit = 61;

                while (wait != waitLimit)
                {
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(buildVersion_XPath)).Count == 0)
                    {
                        DlkLogger.LogInfo("Waiting for build version to appear... [" + wait.ToString() + "]s");
                        Thread.Sleep(1000);
                        wait++;
                        continue;
                    }
                    else
                    {
                        mBuildVersion = DlkEnvironment.AutoDriver.FindElement(By.XPath(buildVersion_XPath));
                        string buildVersion = mBuildVersion.Text.Trim();

                        if (String.IsNullOrEmpty(buildVersion))
                        {
                            Thread.Sleep(1000);
                            buildVersion = mBuildVersion.Text.Trim();
                        }

                        if (!String.IsNullOrEmpty(buildVersion))
                        {
                            DlkEnvironment.CustomInfo.Add("buildversion", new string[] { "Build Version", buildVersion});
                            DlkLogger.LogInfo("Build Version: [" + buildVersion + "]");
                        }
                        else
                        {
                            DlkLogger.LogInfo("Build version not found.");
                        }
                        break;
                    }
                }

                //if (wait == waitLimit)
                //    throw new Exception("Waiting for build version to appear has reached its limit (60s). Setup failed.");
            }
        }

        /// <summary>
        /// Displays currently stored build version to logs
        /// </summary>
        public static void DisplayBuildVersion()
        {
            if (DlkEnvironment.CustomInfo.ContainsKey("buildversion"))
                DlkLogger.LogInfo("Build Version: [" + DlkEnvironment.CustomInfo["buildversion"].ElementAt(1) + "]");
            else
                DlkLogger.LogInfo("Build version not found.");
        }

        /// <summary>
        /// Retrieves current build version after logging in
        /// </summary>
        public static void GetBuildVersionAfterLogin()
        {
            if (!DlkEnvironment.CustomInfo.ContainsKey("buildversion"))
            {
                //Retrieve build version
                string buildVersion_XPath = "//*[@class='contentBarVersion']";
                IWebElement mBuildVersion = null;

                //Wait for build version to appear
                int wait = 1;
                int waitLimit = 21;

                while (wait != waitLimit)
                {
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(buildVersion_XPath)).Count == 0)
                    {
                        DlkLogger.LogInfo("Waiting for build version after login to appear... [" + wait.ToString() + "]s");
                        Thread.Sleep(1000);
                        wait++;
                        continue;
                    }
                    else
                    {
                        mBuildVersion = DlkEnvironment.AutoDriver.FindElement(By.XPath(buildVersion_XPath));
                        string[] contentBarVersion = mBuildVersion.Text.Split('/');
                        string buildVersion = contentBarVersion.FirstOrDefault().Replace("Version ","").Trim();

                        if (String.IsNullOrEmpty(buildVersion))
                        {
                            Thread.Sleep(1000);
                            buildVersion = mBuildVersion.Text.Trim();
                        }

                        if (!String.IsNullOrEmpty(buildVersion))
                        {
                            DlkEnvironment.CustomInfo.Add("buildversion", new string[] { "Build Version", buildVersion });
                            DlkLogger.LogInfo("Build Version: [" + buildVersion + "]");
                        }
                        else
                        {
                            DlkLogger.LogInfo("Build version also not found after login.");
                        }
                        break;
                    }
                }
            }
                
        }

        public void UpdateBuildInfoConfigBeforeRun(string IsDefaultEnv, string LatestBuild)
        {
            string IsInitialScript = String.Equals(DlkTestRunnerApi.CurrentRunningTest,"1") || 
                String.IsNullOrEmpty(DlkTestRunnerApi.CurrentRunningTest) ? "YES" : "NO";
            mReleaseConfigHandler = new DlkWorkBookReleaseConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\ReleaseConfig.xml"));
            mReleaseConfigHandler.UpdateConfigValue("isdefaultenv", IsDefaultEnv);
            mReleaseConfigHandler.UpdateConfigValue("isinitialscript", IsInitialScript);
            mReleaseConfigHandler.UpdateConfigValue("latestbuild", LatestBuild);
        }

        public void UpdateBuildInfoConfigAfterRun(string LatestRunBuild)
        {
            mReleaseConfigHandler.UpdateConfigValue("latestrunbuild", LatestRunBuild);
            mReleaseConfigHandler.UpdateConfigValue("latestrundate", DateTime.Now.ToString("MM/dd/yyyy"));
            mReleaseConfigHandler.UpdateConfigValue("latestruntime", DateTime.Now.ToString("hh:mm tt"));
        }

        public bool IsMismatchBuildInfoConfig()
        {
            string isDefaultEnv = mReleaseConfigHandler.GetConfigValue("isdefaultenv");
            string isInitialScript = mReleaseConfigHandler.GetConfigValue("isinitialscript");
            string latestBuild = mReleaseConfigHandler.GetConfigValue("latestbuild");
            string latestRunBuild = mReleaseConfigHandler.GetConfigValue("latestrunbuild");
            string latestRunDate = mReleaseConfigHandler.GetConfigValue("latestrundate");
            string latestRunTime = mReleaseConfigHandler.GetConfigValue("latestruntime");

            if(isDefaultEnv.Equals("YES") && isInitialScript.Equals("NO"))
            {
                DlkLogger.LogInfo("Succeding script in suite detected. Verifying mismatch builds... ");
                if (!String.IsNullOrEmpty(latestRunBuild) && !latestBuild.Equals(latestRunBuild))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                DlkLogger.LogInfo("Initial script in suite detected. Verifying mismatch of build ignored.");
                return false;
            }
        }

        public void IsRerunEnable(string buildID)
        {
            string IsInitialScript = String.Equals(DlkTestRunnerApi.CurrentRunningTest, "1") ||
                String.IsNullOrEmpty(DlkTestRunnerApi.CurrentRunningTest) ? "YES" : "NO";

            if (IsInitialScript.Equals("YES") && DlkTestRunnerApi.mTestScheduled)
            {
                mRerunConfigHandler = new DlkWorkBookRerunConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\RerunConfig.xml"), buildID);

                if (mRerunConfigHandler.isInitialRun)
                {
                    DlkLogger.LogInfo("Initial run of test suite. Re-run config created.");
                    DlkLogger.LogInfo("Build: [" + buildID + "] saved on re-run config. Proceeding executing test... ");
                }
                else
                {
                    if (mRerunConfigHandler.ConfigBuildExists())
                    {
                        DlkLogger.LogInfo("Build: [" + buildID + "] has already been executed in re-run config. Re-running of suite is cancelled. ");
                        DlkTestRunnerApi.mAbortScheduledRun = true;
                    }
                    else
                    {
                        DlkLogger.LogInfo("Build: [" + buildID + "] not found on re-run config. Proceeding executing test... ");
                        mRerunConfigHandler.UpdateRerunConfig();
                    }
                    
                }
            }
        }
    }
}
