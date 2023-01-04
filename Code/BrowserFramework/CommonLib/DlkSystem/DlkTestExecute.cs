using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLib.DlkSystem
{
    /// <summary>
    /// This is the core shared execution logic. It executes a test on a given browser
    /// </summary>
    public class DlkTestExecute
    {
        public static bool IsBrowserOpen = false;
        public static bool IsPlayBack = false;
        public static bool IsSetupPassed = true;
        public static DlkLoginConfigRecord OverrideLoginConfigRecord = null;
        public static string OverrideBrowser = null;
        private bool IsBlacklisted = false;
        private string CurrentURL = "";
        public bool IsAdHocRun { get; set; } = false;

        protected string mCtorErrorMsg = string.Empty;

        public DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        /// <summary>
        /// Stored information from our LoginConfig.xml
        /// </summary>
        public DlkLoginConfigHandler mLoginConfigHandler;

        /// <summary>
        /// The test we are running loaded into memory as a record
        /// </summary>
        public DlkTest mTest { get; set; }

        /// <summary>
        /// Manages the test step we are executing
        /// </summary>
        public int mCurrentTestStep { get; set; }

        /// <summary>
        /// Constuctor initializes the environment data needed to run a test
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="LoginConfig"></param>
        /// <param name="TestPath"></param>
        /// <param name="TestInstance"></param>
        /// <param name="Browser"></param>
        public DlkTestExecute(String Product, String LoginConfig, String TestPath, String TestInstance, String Browser, String KeepBrowserOpen)
        {
            // all information logged before the first step is tagged with step -999
            mCurrentTestStep = -9999;

            try
            {
                mCtorErrorMsg = string.Empty;

                DlkLogger.ClearLogger();

                if (!string.IsNullOrEmpty(OverrideBrowser))
                {
                    Browser = OverrideBrowser;
                }

                DlkEnvironment.InitializeEnvironment(Product, LoginConfig, TestPath, TestInstance, Browser, KeepBrowserOpen);

                DlkDynamicObjectStoreHandler.Initialize(true);

                mTest = new DlkTest(DlkEnvironment.mDirTests + TestPath);
                mTest.UpdateTestStatus("failed");
                mTest.UpdateTestStart(DateTime.Now);
                mTest.UpdateTestInstanceExecuted(Convert.ToInt32(TestInstance));

                if (OverrideLoginConfigRecord != null)
                {
                    mLoginConfigHandler = new DlkLoginConfigHandler(
                        OverrideLoginConfigRecord.mUrl,
                        OverrideLoginConfigRecord.mUser,
                        OverrideLoginConfigRecord.mPassword,
                        OverrideLoginConfigRecord.mDatabase,
                        OverrideLoginConfigRecord.mPin,
                        OverrideLoginConfigRecord.mMetaData);
                }
                else if (DlkEnvironment.mLoginConfig == "skip")
                {
                    mLoginConfigHandler = null;
                }
                else
                {
                    mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
                }

                /* Log system info */
                DlkLoginConfigRecord login = null;
                try
                {
                    if (OverrideLoginConfigRecord != null)
                    {
                        login = new DlkLoginConfigRecord(
                            OverrideLoginConfigRecord.mID,
                            OverrideLoginConfigRecord.mUrl,
                            OverrideLoginConfigRecord.mUser,
                            OverrideLoginConfigRecord.mPassword,
                            OverrideLoginConfigRecord.mDatabase,
                            OverrideLoginConfigRecord.mPin,
                            OverrideLoginConfigRecord.mConfigFile
                            );
                        // encrypt information in override login record
                        DlkEncryptionHelper helper = new DlkEncryptionHelper();
                        login.mUser = Convert.ToBase64String(helper.EncryptStringToByteArray(OverrideLoginConfigRecord.mUser));
                        login.mPassword = Convert.ToBase64String(helper.EncryptStringToByteArray(OverrideLoginConfigRecord.mPassword));
                    }
                    else
                    {
                        login = DlkLoginConfigHandler.GetLoginConfigInfo(DlkEnvironment.mLoginConfigFile, LoginConfig);
                    }
                    IsBlacklisted = DlkEnvironment.URLBlacklist.Any(x => DlkEnvironment.IsSameURL(login.mUrl, x));
                    if (IsBlacklisted)
                    {
                        CurrentURL = login.mUrl;
                        throw new Exception();
                    }
                }
                catch
                {
                    login = null;
                    throw;
                }
                finally
                {
                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("SETUP"));
                    DlkLogger.LogSystemInfo(Browser, login);
                }
            }
            catch
            {
                mCtorErrorMsg = IsBlacklisted ? "Current URL \'" + CurrentURL + "\' is a blacklisted URL and cannot be used for execution. Please change this test's environment URL in Edit>Settings>Environment tab." :
                    mLoginConfigHandler == null ? "Missing environment: " + LoginConfig + ". Please check available environment(s) on Edit>Settings>Environment tab in Test Runner." :
                    "Unable to initialize test environment with these parameters. Product: " + Product +
                    ", LoginConfig: " + LoginConfig + ", TestPath: " + TestPath + ", TestInstance: " + TestInstance + ", Browser: " + Browser;
                DlkLogger.LogScreenCapture("ExceptionImg");
            }
        }

        /// <summary>
        /// This executes the test and returns the path of the generated results directory
        /// </summary>
        /// <returns></returns>
        public virtual String ExecuteTest()
        {
            TestSetup();
            if (IsSetupPassed)
            {
                TestStepsExecute();
            }
            TestTeardown();

            /* update test logs */
            mTest.UpdateTestEnd(DateTime.Now);
            mTest.WriteTestToTestResults();

            //Generates .txt file if the run type is AdHoc
            if (IsAdHocRun)
                mTest.WriteAdHocFile();

            /* revert setup results flag to initial state */
            IsSetupPassed = true;

            return DlkEnvironment.mDirTestResultsCurrent;
        }

        /// <summary>
        /// TestSetup runs at the begining of the test to prepare the test for execution
        /// </summary>
        public virtual void TestSetup()
        {
            // ensure that DlkTestExecute ctor did not fail */
            if (string.IsNullOrEmpty(mCtorErrorMsg))
            {
                if (!IsBrowserOpen)
                {
                    DlkEnvironment.StartBrowser();
                }
                // Do all data iteration substitutions here
                DlkData.SubstituteExecuteDataVariables(mTest);
                DlkData.SubstituteDataVariables(mTest);
            }
            else
            {
                throw new Exception(mCtorErrorMsg);
            }
        }

        /// <summary>
        /// TestExecute executes the test steps
        /// </summary>
        public virtual void TestStepsExecute()
        {
            DlkLogger.ClearLogger();
        }

        /// <summary>
        /// TestTeardown performs steps to cleanup after the test steps execute
        /// </summary>
        public virtual void TestTeardown()
        {
            try
            {
                DlkLogger.ClearLogger();
                mCurrentTestStep = 9999;
                if (!DlkEnvironment.mKeepBrowserOpen)
                {
                    IsBrowserOpen = false;
                    DlkEnvironment.CloseSession();
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
        }
    }
}
