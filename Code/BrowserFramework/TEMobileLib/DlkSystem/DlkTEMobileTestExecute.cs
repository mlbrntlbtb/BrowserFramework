//#define ATK_RELEASE

using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TEMobileLib.DlkUtility;
using TEMobileLib.DlkFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace TEMobileLib.System
{
    /// <summary>
    /// This is the Navigator specific test execution program. 
    /// It utilites a base class with shared code
    /// It can also be customized as needed
    /// </summary>
    public class DlkTEMobileTestExecute : DlkTestExecute
    {
        public DlkTEMobileTestExecute(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBrowserOpen)
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
                if(DlkEnvironment.mIsMobile)
                {
                    base.TestSetup();
                    DlkEnvironment.SetContext("WEBVIEW");
                    Login();
                }
                else
                {
                    // ensure that DlkTestExecute ctor did not fail */
                    if (string.IsNullOrEmpty(mCtorErrorMsg))
                    {
                        if (!IsBrowserOpen)
                        {
                            DlkEnvironment.StartBrowser(true);
                        }
                        // Do all data iteration substitutions here
                        DlkData.SubstituteExecuteDataVariables(mTest);
                        DlkData.SubstituteDataVariables(mTest);
                    }
                    else
                    {
                        throw new Exception(mCtorErrorMsg);
                    }

                    bool bCont = true;
                    if (DlkTestExecute.IsBrowserOpen && !DlkTestExecute.IsPlayBack)
                    {
                        bCont = false;
                    }

                    if (bCont)
                    {
                        if (DlkEnvironment.mIsDeviceEmulator)
                        {
                            var mAddress = mLoginConfigHandler.mUrl.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            IPAddress[] mIPAddressList = null;
                            String ipAdd = "";
                            for (int i = 0; i < mAddress.Count; i++)
                            {
                                if (mAddress[i].Contains("http"))
                                {
                                    ipAdd = mAddress[i] + "//";

                                    if (mAddress[i + 1].Contains(":")) //for URLS that have specific ports (i.e, ashapt62vs:7009)
                                    {
                                        String[] ipAddress = mAddress[i + 1].Split(':');
                                        mIPAddressList = Dns.GetHostAddresses(ipAddress[0]);
                                        foreach (IPAddress add in mIPAddressList)
                                        {
                                            mAddress[i + 1] = add.ToString() + ":" + ipAddress[1];
                                        }
                                    }
                                    else
                                    {
                                        mIPAddressList = Dns.GetHostAddresses(mAddress[i + 1]);
                                        foreach (IPAddress add in mIPAddressList)
                                        {
                                            mAddress[i + 1] = add.ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (i < mAddress.Count - 1)
                                    {
                                        ipAdd = ipAdd + mAddress[i] + "/";
                                    }
                                    else
                                    {
                                        ipAdd = ipAdd + mAddress[i];
                                    }
                                }
                            }
                            DlkEnvironment.AutoDriver.Url = ipAdd;
                        }
                        else
                        {
                            DlkEnvironment.AutoDriver.Url = mLoginConfigHandler.mUrl;
                        }

                        bool bFailed = false;
                        try
                        {
                            Login();
                        }
                        catch (OpenQA.Selenium.WebDriverException)
                        {
                            DlkLogger.LogWarning("WebDriver encountered a fatal error during login. Retrying...");
                            bFailed = true;
                            DlkEnvironment.CloseSession();
                        }
                        if (bFailed) /* If login threw a generic webdriver exception, retry once */
                        {
                            DlkEnvironment.StartBrowser();
                            DlkEnvironment.AutoDriver.Url = mLoginConfigHandler.mUrl;
                            Login();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                IsSetupPassed = false;
                DlkLogger.LogError(e);
            }
            finally
            {
                mTest.UpdateTestSetupLogMessages(DlkLogger.mLogRecs);
            }
        }

        private void Login()
        {
            DlkCP7Login.SetUserInterface(mLoginConfigHandler.mMetaData ?? string.Empty);
            if (!String.IsNullOrEmpty(mLoginConfigHandler.mUser) && !String.IsNullOrEmpty(mLoginConfigHandler.mPassword))
            {
                if (DlkEnvironment.mIsMobileBrowser)
                {
                    Thread.Sleep(3000);
                }
                string funcName = mTest.mTestPath.Contains(DlkEnvironment.STR_TEST_CONNECT_FILE) ? "TestLoginConnection" : "Login";
                DlkTEMobileFunctionHandler.ExecuteFunction("CP7Login", "", funcName,
                    new String[] { mLoginConfigHandler.mUser, mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase, mLoginConfigHandler.mUrl, mLoginConfigHandler.mPin});
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

                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader(string.Format("STEP {0}",mCurrentTestStep)));
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
                        // Handles loading
                        DlkTEMobileCommon.CurrentControl = mTest.mTestSteps[i].mControl;
                        if (DlkTEMobileCommon.CurrentControl != "Function" && mTest.mTestSteps[i].mKeyword != "ClickWhileLoading")
                        {
                            DlkTEMobileCommon.WaitLoadingFinished(DlkTEMobileCommon.IsComponentModal(DlkTEMobileCommon.CurrentComponent, mTest.mTestSteps[i].mScreen));
                        }
                        DlkTEMobileCommon.CurrentComponent = mTest.mTestSteps[i].mScreen;

                        if ((!string.IsNullOrEmpty(mTest.mTestSteps[i].mScreen) && string.IsNullOrEmpty(mTest.mTestSteps[i].mControl)) 
                            || ((mTest.mTestSteps[i].mScreen.ToLower() == "function") && (mTest.mTestSteps[i].mControl.ToLower() == "function"))
                            )
                        {
                            DlkTEMobileFunctionHandler.ExecuteFunction
                                (
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                        }
                        else
                        {
                            DlkTEMobileKeywordHandler.ExecuteKeyword(
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
                    // after each step, perform a System Error check - if there is a system error, fail this step.
                    if (DlkTEMobileCommon.IsSystemError())
                    {
                        throw new Exception("System Error was encountered. Please refer to the exception image.");
                    }
                }
                catch (Exception ex) // the test failed during this step execution
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
    }
}
