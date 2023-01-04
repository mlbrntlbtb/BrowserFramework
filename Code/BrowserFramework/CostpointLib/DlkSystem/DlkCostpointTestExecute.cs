using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using CostpointLib.DlkUtility;
using CostpointLib.DlkFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using CostpointLib.DlkRecords;
using OSH_Client;
using System.IO;

namespace CostpointLib.System
{
    /// <summary>
    /// This is the Navigator specific test execution program. 
    /// It utilites a base class with shared code
    /// It can also be customized as needed
    /// </summary>
    public class DlkCostpointTestExecute : DlkTestExecute
    {
        public DlkCostpointTestExecute(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBrowserOpen)
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
                        DlkEnvironment.GoToUrl(mLoginConfigHandler.mUrl);
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
                        DlkEnvironment.GoToUrl(mLoginConfigHandler.mUrl);
                        Login();
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
                DlkCostpointFunctionHandler.ExecuteFunction("CP7Login", "", funcName,
                    new String[] { mLoginConfigHandler.mUser, mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase});
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
            Dictionary<int, string> screens = GetScreenNames();
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
                    String[] maskedTestParams = mTestParams;
                    List<int> maskedIndexes = new List<int>();
                    // substitute any live variables that have been defined along the way (e.g. %DateTime% for the real date time)
                    DlkVariable.SetVariable("DateTime", DlkString.GetDateAsText("file"));
                    DlkVariable.SetVariable("DateTimeShort", DlkString.GetDateAsText("yymmddhhmmss"));
                    DlkVariable.SetVariable("DateCurrentMonthYear", DlkString.GetDateAsText("monthyear"));
                    DlkVariable.SetVariable("DateCurrentStartOfMonth", DlkString.GetDateAsText("startofmonth"));
                    DlkVariable.SetVariable("DateCurrentEndOfMonth", DlkString.GetDateAsText("endofmonth"));

                    if (mTest.mTestSteps[i].mPasswordParameters != null)
                        GetMaskedIndex(mTest.mTestSteps[i], ref maskedIndexes);

                    // make all variable substitutions
                    mTestParams = DlkVariable.SubstituteVariables(mTestParams, maskedIndexes.ToArray());
                    maskedTestParams = new List<string>(mTestParams).ToArray();

                    // check if execute value is valid (due to data-driven values enabled in this field)
                    bool execute = false;
                    if (!Boolean.TryParse(mTest.mTestSteps[i].mExecute, out execute))
                    {
                        DlkLogger.LogInfo("Invalid value supplied for Execute field: [" + mTest.mTestSteps[i].mExecute + "]");
                        mTest.mTestSteps[i].mExecute = "False";
                    }

                    if (mTest.mTestSteps[i].mPasswordParameters != null)
                    {
                        GetMaskedText(mTest.mTestSteps[i], ref maskedTestParams);
                    }

                    string _screen = mTest.mTestSteps[i].mScreen;
                    if (screens != null)
                        _screen = screens[mTest.mTestSteps[i].mStepNumber];

                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader(string.Format("STEP {0}", mCurrentTestStep)));
                    DlkLogger.LogToOutputDisplay(string.Format("EXECUTE\t\t:\t{0}\r\nSCREEN\t\t:\t{1}\r\nCONTROL\t:\t{2}\r\nKEYWORD\t:\t{3}\r\nPARAM\t\t:\t{4}",
                        mTest.mTestSteps[i].mExecute, _screen, mTest.mTestSteps[i].mControl, mTest.mTestSteps[i].mKeyword,
                        string.Join("|", maskedTestParams ?? mTestParams)));
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
                        DlkCostpointCommon.CurrentControl = mTest.mTestSteps[i].mControl;
                        DlkCostpointCommon.CurrentComponent = mTest.mTestSteps[i].mScreen;

                        if ((!string.IsNullOrEmpty(mTest.mTestSteps[i].mScreen) && string.IsNullOrEmpty(mTest.mTestSteps[i].mControl))
                            || ((mTest.mTestSteps[i].mScreen.ToLower() == "function") && (mTest.mTestSteps[i].mControl.ToLower() == "function"))
                            )
                        {
                            DlkPreviousControlRecord.ClearPreviousControlRecord();
                            DlkCostpointFunctionHandler.ExecuteFunction
                                (
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                        }
                        else
                        {
                            DlkCostpointKeywordHandler.ExecuteKeyword(
                                mTest.mTestSteps[i].mScreen,
                                mTest.mTestSteps[i].mControl,
                                mTest.mTestSteps[i].mKeyword,
                                mTestParams
                                );
                            DlkPreviousControlRecord.SetPreviousControl();
                        }
                        if (mTest.mTestSteps[i].mStepDelay > 0)
                        {
                            DlkLogger.LogInfo("Test Step delay found. Sleeping: " + mTest.mTestSteps[i].mStepDelay.ToString() + "(s)");
                            Thread.Sleep((mTest.mTestSteps[i].mStepDelay * 1000));
                        }
                        if (IsActionKeyword(mTest.mTestSteps[i].mKeyword))
                        {
                            // Handles loading if step triggers action
                            DlkCostpointCommon.WaitLoadingFinished(DlkCostpointCommon.IsComponentModal(DlkCostpointCommon.CurrentComponent, mTest.mTestSteps[i].mScreen));
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
                    if (DlkCostpointCommon.IsSystemError())
                    {
                        throw new Exception("System Error was encountered. Please refer to the exception image.");
                    }
                }
                catch (Exception ex) // the test failed during this step execution
                {
                    DlkPreviousControlRecord.ClearPreviousControlRecord();

                    if(!DlkTestExecute.IsPlayBack)
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
#if DEBUG
            string configFilePath = Path.Combine(DlkEnvironment.mDirTools, @"TestRunner\OSHConfig.xml");
            OSH.Push(configFilePath, () =>
            {
                List<ControlRecord> controlRecords = mTest.mTestSteps
                .Where(step => step.mStepStatus.ToLower() == "passed")
                .Select(step => new ControlRecord
                {
                    ProductName = DlkEnvironment.mProductFolder,
                    Screen = step.mScreen,
                    Control = step.mControl
                }).ToList();

                return controlRecords;
            });
#endif

            base.TestTeardown(); // common teardown for a test

            mTest.UpdateTestTeardownLogMessages(DlkLogger.mLogRecs);
        }

        /// <summary>
        /// Determines which keyword behaviors have actions that trigger loading in the webpage.
        /// </summary>
        private bool IsActionKeyword(String Keyword)
        {
            // Non-action keywords
            if (Keyword.ToLower().StartsWith("verify")) { return false; }
            if (Keyword.ToLower().StartsWith("assign")) { return false; }
            if (Keyword.ToLower().StartsWith("get")) { return false; }
            if (Keyword.ToLower().StartsWith("wait")) { return false; }

            //Non-action functions
            if (Keyword.ToLower().StartsWith("text")) { return false; }
            if (Keyword.ToLower().StartsWith("date")) { return false; }
            if (Keyword.ToLower().Equals("logcomment")) { return false; }
            if (Keyword.ToLower().Equals("computenumberofdays")) { return false; }
            if (Keyword.ToLower().Equals("capturescreenshot")) { return false; }
            if (Keyword.ToLower().Equals("startofweek")) { return false; }
            if (Keyword.ToLower().Equals("endofweek")) { return false; }
            if (Keyword.ToLower().Equals("startofmonth")) { return false; }
            if (Keyword.ToLower().Equals("endofmonth")) { return false; }
            if (Keyword.ToLower().Equals("setdelayretries")) { return false; }
            if (Keyword.ToLower().Equals("lettercase")) { return false; }
            if (Keyword.ToLower().Equals("movefile")) { return false; }
            if (Keyword.ToLower().Equals("copyfile")) { return false; }
            if (Keyword.ToLower().Equals("emptyfolder")) { return false; }
            if (Keyword.ToLower().Equals("turnoffteststeps")) { return false; }

            //CP-specific functions
            if (Keyword.ToLower().StartsWith("performmathoperation")) { return false; }
            if (Keyword.ToLower().Equals("ifthenelse")) { return false; }
            if (Keyword.ToLower().Equals("formatamount")) { return false; }

            return true;
        }

        /// <summary>
        /// Masked parameters with substituted value
        /// </summary>
        /// <param name="step">current step</param>
        /// <param name="parameters">keyword parameters value</param>
        private void GetMaskedText(DlkTestStepRecord step, ref string[] parameters)
        {
            string maskedPassword = "";
            for (int i = 0; i < step.mPasswordParameters.Count(); i++)
            {
                if(DlkPasswordMaskedRecord.IsMaskedParameter(step, i))
                {                    
                    parameters[i].ToList().ForEach(item => maskedPassword += DlkPasswordMaskedRecord.PasswordChar);
                    parameters[i] = !String.IsNullOrWhiteSpace(maskedPassword) ? maskedPassword : DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE; ;
                }
            }
        }

        /// <summary>
        /// Get password parameter indexes. Needed in substitution of variables
        /// </summary>
        /// <param name="step">current step</param>
        /// <param name="maskedIndexes">keyword parameters value</param>
        private void GetMaskedIndex(DlkTestStepRecord step, ref List<int> maskedIndexes)
        {
            for (int i = 0; i < step.mPasswordParameters.Count(); i++)
            {
                if (DlkPasswordMaskedRecord.IsMaskedParameter(step, i))
                    maskedIndexes.Add(i);
            }
        }

        /// <summary>
        /// Replace screen names to alias and store to dictionary to show alias in log
        /// </summary>
        /// <returns>Dictionary with key step number and value alias</returns>
        private Dictionary<int, string> GetScreenNames()
        {
            if (DlkEnvironment.IsShowAppNameProduct && DlkEnvironment.IsShowAppNameEnabled())
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                foreach (DlkTestStepRecord step in mTest.mTestSteps)
                {
                    if (step.mScreen == "")
                        continue;
                    
                    int index = DlkDynamicObjectStoreHandler.Alias.IndexOf(step.mScreen);
                    string appId = DlkDynamicObjectStoreHandler.Screens[index];
                    step.mScreen = appId;
                    result.Add(step.mStepNumber, DlkDynamicObjectStoreHandler.Alias[index]);
                }
                return result;
            }
            else
                return null;
        }
    }
}
