using System;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using PIMMobileLib.DlkFunctions;
using CommonLib.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using OpenQA.Selenium.Appium;

namespace PIMMobileLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkPIMMobileFunctionHandler
    {
        private const int INT_SWIPE_WAIT_MS = 800;

        /// <summary>
        /// this logic handles functions which are to be used when keywords do not provide the required flexibility
        /// </summary>
        /// <param name="Screen"></param>
        /// <param name="ControlName"></param>
        /// <param name="Keyword"></param>
        /// <param name="Parameters"></param>
        public static void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "VerifySyncing":
                        VerifySyncing(Parameters[0]);
                        break;
                    case "Swipe":
                        Swipe(Parameters[0]);
                        break;
                    default:
                        DlkFunctionHandler.ExecuteFunction(Keyword, Parameters);
                        break;
                }
            }
            else
            {
                switch (Screen)
                {
                    case "Database":
                        DlkDatabaseHandler.ExecuteDatabase(Keyword, Parameters);
                        break;
                    case "Login":
                        switch (Keyword)
                        {
                            case "Login":
                                Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Dialog":
                        switch (Keyword)
                        {
                            case "ClickDialogButton":
                                DlkDialog.ClickDialogButton(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }


        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        private static void Login(String Url, String User, String Password, String Database, String Pin)
        {
            int iSleep = 1000;
            
            // use the object store definitions 
            if (!string.IsNullOrEmpty(Database))
            {
                DlkPIMMobileKeywordHandler.ExecuteKeyword("Login", "ClientCode", "Set", new String[] { Database });
                DlkPIMMobileKeywordHandler.ExecuteKeyword("Login", "CompleteSetup", "Tap", new String[] { "" });
                Thread.Sleep(iSleep);


                DlkPIMMobileKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkPIMMobileKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                DlkPIMMobileKeywordHandler.ExecuteKeyword("Login", "SignIn", "Tap", new String[] { "" });
                Thread.Sleep(iSleep);

                WaitForSpinnerToFinishLoading(iSleep);
            }
        }
        
        [Keyword("VerifySyncing")]
        /// <summary>
        /// Verifies if sync screen exissts
        /// </summary>
        /// <param name="IsSyncing"></param>
        public static void VerifySyncing(string TrueOrFalse)
        {
            try
            {
                int iSleep = 1000;
                DlkBaseControl syncDialog = new DlkBaseControl("SyncDialog", "XPATH", "//*[@resource-id='android:id/progress']/ancestor::*[@resource-id='android:id/body']");
                bool isSyncing = Convert.ToBoolean(TrueOrFalse);
                int retryLimit = 3;
                int curRetry = 0;
                bool bFound = false;
                while (curRetry++ <= retryLimit)
                {
                    bFound = syncDialog.Exists();
                    if (bFound)
                        break;
                }

                DlkAssert.AssertEqual("VerifySyncing() : " + syncDialog.mControlName, isSyncing, bFound);

                while (syncDialog.Exists())
                {
                    //wait for sync dialog to disappear
                    Thread.Sleep(10000);
                    DlkLogger.LogInfo("Waiting for sync to finish.");
                }

                WaitForSpinnerToFinishLoading(iSleep);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySyncing() failed : " + e.Message, e);
            }

        }

        [Keyword("Swipe")]
        public static void Swipe(string Direction)
        {
            try
            {
                OpenQA.Selenium.Appium.AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                DlkEnvironment.SetContext("NATIVE");
                switch (Direction.ToLower())
                {
                    case "up":
                        appiumDriver.Swipe(DlkEnvironment.mDeviceWidth / 2, DlkEnvironment.mDeviceHeight / 2, DlkEnvironment.mDeviceWidth / 2, 5, INT_SWIPE_WAIT_MS);
                        break;
                    case "down":
                        appiumDriver.Swipe(DlkEnvironment.mDeviceWidth / 2, DlkEnvironment.mDeviceHeight / 2, DlkEnvironment.mDeviceWidth / 2, DlkEnvironment.mDeviceHeight - 5, INT_SWIPE_WAIT_MS);
                        break;
                    case "left":
                        appiumDriver.Swipe(DlkEnvironment.mDeviceWidth / 2, DlkEnvironment.mDeviceHeight / 2, 5, DlkEnvironment.mDeviceHeight / 2, INT_SWIPE_WAIT_MS);
                        break;
                    case "right":
                        appiumDriver.Swipe(DlkEnvironment.mDeviceWidth / 2, DlkEnvironment.mDeviceHeight / 2, DlkEnvironment.mDeviceWidth - 5, DlkEnvironment.mDeviceHeight / 2, INT_SWIPE_WAIT_MS);
                        break;
                    default:
                        throw new Exception("Unsupported swipe direction = '" + Direction + "'");
                }
                DlkEnvironment.SetContext("WEBVIEW");
                DlkLogger.LogInfo("Swipe() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Swipe() failed : " + e.Message, e);
            }
        }

        #region METHODS

        private static void WaitForSpinnerToFinishLoading(int iSleep)
        {
            for (int sleep = 0; sleep < 30; sleep++)
            {
                DlkBaseControl spin = new DlkBaseControl("Spinner", "XPATH", "//*[@class='android.widget.ProgressBar']");
                if (spin.Exists())
                {
                    //if current spinner is visible, sleep 1 sec                                    
                    DlkLogger.LogInfo("Waiting for page to load...");
                    Thread.Sleep(iSleep);
                }
                else
                {
                    break;
                }

            }
        }
        #endregion
    }
}
