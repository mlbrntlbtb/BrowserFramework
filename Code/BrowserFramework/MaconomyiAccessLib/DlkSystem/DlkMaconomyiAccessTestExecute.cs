using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaconomyiAccessLib.DlkSystem
{
    /// <summary>
    /// This is the iAccess specific test execution program. 
    /// It utilites a base class with shared code
    /// It can also be customized as needed
    /// </summary>
    public class DlkMaconomyiAccessTestExecute : DlkTestExecute
    {
        public DlkMaconomyiAccessTestExecute(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBrowserOpen)
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

                //only for mobile
                DlkEnvironment.SetContext("WEBVIEW");

                if (!IsBrowserOpen)
                {
                    bool bFailed = false;
                    int loginRetry = 0;
                    int maxRetry = 3;
                    
                    //Execute login function with maximum of 3 retries
                    while(loginRetry != maxRetry)
                    {
                        if (loginRetry == maxRetry)
                            throw new Exception("Retrying login setup has reached its limit. Setup failed.");

                        try
                        {
                            //Login();
                            DisplayMobileRemoteInfo();
                            DisplayVersionInfo();
                        }
                        catch (WebDriverTimeoutException)
                        {
                            bFailed = true;
                            loginRetry++;
                        }

                        if (bFailed)
                        {
                            DlkLogger.LogWarning("Re-launching browser after page loading failure ...[" + loginRetry + "]");
                            DlkEnvironment.StartBrowser();
                            Thread.Sleep(1000);
                            bFailed = false;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    DisplayMobileRemoteInfo();
                    DisplayVersionInfo();
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

        /// <summary>
        /// Executes the Login Setup
        /// </summary>
        private void Login()
        {
            DlkMaconomyiAccessFunctionHandler.ExecuteFunction("Login", "Function", "Login",
                      new String[] { mLoginConfigHandler.mUrl, mLoginConfigHandler.mUser,
                    mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
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

                    // check if execute value is valid (due to data-driven values enabled in this field)
                    bool execute = false;
                    if (!Boolean.TryParse(mTest.mTestSteps[i].mExecute, out execute))
                    {
                        DlkLogger.LogInfo("Invalid value supplied for Execute field: [" + mTest.mTestSteps[i].mExecute + "]");
                        mTest.mTestSteps[i].mExecute = "False";
                    }
                    else if(execute)
                    {
                        // make all variable substitutions
                        mTestParams = DlkVariable.SubstituteVariables(mTestParams);
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
                        int iSleep = 1000;

                        //Anticipate spinner pop-up delay
                        Thread.Sleep(3 * iSleep);

                        //If spinner still appears after wait, check until spinner disappears.
                        //Maximum of 5 minutes wait.
                        for (int sleep = 0; sleep < 300; sleep++)
                        {
                            DlkBaseControl spin = new DlkBaseControl("Spinner", "XPATH", "//div[contains(@class,'spinner')]");
                            if (spin.Exists())
                            {
                                //if current spinner is visible, sleep 1 sec                                    
                                DlkLogger.LogInfo("Waiting for page to load...");
                                Thread.Sleep(iSleep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("Page loaded. Continuing to perform keyword...");
                                break;
                            }
                        }

                        if ((!string.IsNullOrEmpty(mTest.mTestSteps[i].mScreen) && string.IsNullOrEmpty(mTest.mTestSteps[i].mControl))
                            || ((mTest.mTestSteps[i].mScreen.ToLower() == "function") || (mTest.mTestSteps[i].mControl.ToLower() == "function"))
                            )
                        {
                            DlkMaconomyiAccessFunctionHandler.ExecuteFunction
                                (
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                        }
                        else
                        {
                            DlkMaconomyiAccessKeywordHandler.ExecuteKeyword(
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
                    if (retries <= maxRetries)
                    {
                        retries++;
                        int currentStep = i + 1;
                        i--;
                        DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("+-+-+-+"));
                        DlkLogger.LogWarning("Current step has failed. Retrying from step #" + currentStep.ToString() + "... ");
                        DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("+-+-+-+"));
                        Thread.Sleep(10000);
                        continue;
                    }
                    else
                    {
                        ZoomOutBrowserForIE(2);
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
            ZoomOutBrowserForIE(2);
        }

        /// <summary>
        /// Executes the Test Teardown logic
        /// </summary>
        public override void TestTeardown()
        {
            base.TestTeardown(); // common teardown for a test

            mTest.UpdateTestTeardownLogMessages(DlkLogger.mLogRecs);
        }

        private void ZoomOutBrowserForIE(int zoomTimes)
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    int zoomLevel = 0;
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    zoomLevel = Convert.ToInt32(jse.ExecuteScript("return window.screen.deviceXDPI"));

                    if (zoomLevel >= 144 && zoomLevel < 160)
                    {
                        //Zoom in
                        var body = DlkEnvironment.AutoDriver.FindElement(By.TagName("html"));
                        for (int z = 0; z < zoomTimes; z++)
                        {
                            body.SendKeys(Keys.Control + Keys.Subtract);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        public static void DisplayVersionInfo()
        {
            if (DlkEnvironment.CustomInfo.ContainsKey("platform"))
            {
                DlkLogger.LogInfo("Platform: [" + DlkEnvironment.CustomInfo["platform"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("Platform not found.");

            if (DlkEnvironment.CustomInfo.ContainsKey("revision"))
            {
                DlkLogger.LogInfo("Revision: [" + DlkEnvironment.CustomInfo["revision"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("Revision not found.");

            if (DlkEnvironment.CustomInfo.ContainsKey("version"))
            {
                DlkLogger.LogInfo("Version: [" + DlkEnvironment.CustomInfo["version"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("Version info not found.");

            if (DlkEnvironment.CustomInfo.ContainsKey("build"))
            {
                DlkLogger.LogInfo("Build: [" + DlkEnvironment.CustomInfo["build"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("Build not found.");

            if (DlkEnvironment.CustomInfo.ContainsKey("tpu"))
            {
                DlkLogger.LogInfo("TPU: [" + DlkEnvironment.CustomInfo["tpu"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("TPU not found.");

            if (DlkEnvironment.CustomInfo.ContainsKey("apu"))
            {
                DlkLogger.LogInfo("APU: [" + DlkEnvironment.CustomInfo["apu"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("APU not found.");
        }

        public static void DisplayMobileRemoteInfo()
        {
            if (DlkEnvironment.CustomInfo.ContainsKey("mobileid"))
            {
                DlkLogger.LogInfo("Mobile ID: [" + DlkEnvironment.CustomInfo["mobileid"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Mobile Appium Server URL: [" + DlkEnvironment.CustomInfo["mobileappiumserverurl"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Mobile Device Name: [" + DlkEnvironment.CustomInfo["mobiledevicename"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Mobile Type: [" + DlkEnvironment.CustomInfo["mobiletype"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Mobile Version: [" + DlkEnvironment.CustomInfo["mobileversion"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Mobile App or Browser: [" + DlkEnvironment.CustomInfo["mobileapporbrowser"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Mobile Package Path: [" + DlkEnvironment.CustomInfo["mobilepackagepath"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("Mobile info not found.");

            if (DlkEnvironment.CustomInfo.ContainsKey("remoteid"))
            {
                DlkLogger.LogInfo("Remote ID: [" + DlkEnvironment.CustomInfo["remoteid"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Remote URL: [" + DlkEnvironment.CustomInfo["remoteurl"].ElementAt(1) + "]");
                DlkLogger.LogInfo("Remote Browser: [" + DlkEnvironment.CustomInfo["remotebrowser"].ElementAt(1) + "]");
            }
            else
                DlkLogger.LogInfo("Remote info not found.");
        }
    }
}
