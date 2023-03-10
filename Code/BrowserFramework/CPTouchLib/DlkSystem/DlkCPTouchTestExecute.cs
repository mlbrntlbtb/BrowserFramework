#define NATIVE_MAPPING

using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using CPTouchLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CPTouchLib.System
{
    /// <summary>
    /// This is the Navigator specific test execution program. 
    /// It utilites a base class with shared code
    /// It can also be customized as needed
    /// </summary>
    public class DlkCPTouchTestExecute : DlkTestExecute
    {
        public DlkCPTouchTestExecute(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBrowserOpen)
            : base(Product, LoginConfig, TestPath, TestInstance, Browser, KeepBrowserOpen)
        {
        }

        #region CONSTANTS
        /// <summary>
        /// Default standard wait : 3s
        /// </summary>
        public const int STANDARD_WAIT = 3000;
        #endregion

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
                DlkEnvironment.InitialAppActivity = "com.deltek.costpoint.te.MainActivity";
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
#if NATIVE_MAPPING
                /* ensure cotext is NATIVE for duration of session */
                DlkEnvironment.SetContext("NATIVE", true);
#else
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
#endif
                if (bCont)
                {
                    DlkCPTouchFunctionHandler.ExecuteFunction("Login", "Function", "Login",
                        new String[] { mLoginConfigHandler.mUrl, mLoginConfigHandler.mUser,
                    mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase, mLoginConfigHandler.mPin });
                }

                /* old */
                //base.TestSetup(); // common setup for a test

                ///* ensure cotext is NATIVE for duration of session */
                //DlkEnvironment.SetContext("NATIVE", true);

                //DlkCPTouchFunctionHandler.ExecuteFunction("Login", "Function", "Login",
                //   new String[] { mLoginConfigHandler.mUrl, mLoginConfigHandler.mUser,
                //    mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase, mLoginConfigHandler.mPin });
            }
            catch (Exception ex)
            {
                DlkEnvironment.mKeepBrowserOpen = false;
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
                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(DlkCPTouchFunctionHandler.WAIT_TIMEOUT_SEC);

                    /* Check if step should be turned-off from a prior call to TurnOffTestStep function call */
                    if (DlkFunctionHandler.mTurnedOffSteps.Contains(mTest.mTestSteps[i].mStepNumber.ToString()))
                    {
                        mEnd = DateTime.Now;
                        mTest.UpdateTestStepExecutionData(mCurrentTestStep, mStart, mEnd, "not run", DlkLogger.mLogRecs);
                        DlkLogger.LogInfo("Step was turned off by a prior call to TurnOffTestSteps. Skipping test step...");
                    }
                    else if (execute)
                    {
                        DlkCPTouchCommon.WaitForSpinnerToFinishLoading(DlkCPTouchFunctionHandler.WAIT_TIMEOUT_SEC);
                        if ((!string.IsNullOrEmpty(mTest.mTestSteps[i].mScreen) && string.IsNullOrEmpty(mTest.mTestSteps[i].mControl))
                            || ((mTest.mTestSteps[i].mScreen.ToLower() == "function") || (mTest.mTestSteps[i].mControl.ToLower() == "function"))
                            )
                        {
                            DlkCPTouchFunctionHandler.ExecuteFunction
                                (
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                        }
                        else
                        {
                            DlkCPTouchKeywordHandler.ExecuteKeyword(
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
            try
            {
                DlkLogger.ClearLogger();
                mCurrentTestStep = 9999;
                if (!DlkEnvironment.mKeepBrowserOpen)
                {
                    IsBrowserOpen = false;
                    DlkEnvironment.CloseSession();
                    //allot 3 seconds before closing nodejs to give time to close app 
                    Thread.Sleep(STANDARD_WAIT);
                    DlkEnvironment.KillProcessByName("node");
                    DlkEnvironment.KillProcessByName("adb");
                    DlkEnvironment.KillProcessByName("chromedriver");
                    DlkEnvironment.KillProcessByName(DlkEnvironment.STR_AVD_PROCESS);
                }
                else
                {
                    IsBrowserOpen = true;
                }
            }
            catch (Exception ex)
            {
                IsBrowserOpen = false;
                DlkLogger.LogError(ex);
                DlkEnvironment.CloseSession();
            }
            mTest.UpdateTestTeardownLogMessages(DlkLogger.mLogRecs);

            /* old */
            //base.TestTeardown(); // common teardown for a test
            ////allot 3 seconds before closing nodejs to give time to close app 
            //Thread.Sleep(STANDARD_WAIT); 
            //DlkEnvironment.KillProcessByName("node");
            //DlkEnvironment.KillProcessByName("adb");
            //DlkEnvironment.KillProcessByName("chromedriver");
            //DlkEnvironment.KillProcessByName(DlkEnvironment.STR_AVD_PROCESS);
            //mTest.UpdateTestTeardownLogMessages(DlkLogger.mLogRecs);
        }
    }
}