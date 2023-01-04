using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using CommonLib;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using VisionTimeLib.System;
using VisionTimeLib.DlkControls;

namespace VisionTimeLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkVisionTimeFunctionHandler
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
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
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
                                Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
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
        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
            switch (Operator)
            {
                case "=":
                    int iValueToTest = -1, iVariableValue = -1;
                    if (int.TryParse(VariableValue, out iVariableValue))
                    {
                        if (int.TryParse(ValueToTest, out iValueToTest)) // both are numbers, so compare the numbers
                        {
                            if (iVariableValue == iValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else // both are not numbers, so compare the values
                        {
                            if (VariableValue == ValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                    }
                    else // both are not numbers, so compare the values
                    {
                        if (VariableValue == ValueToTest)
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    DlkTouchCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("Swipe")]
        public static void Swipe(string Direction)
        {
            try
            {
                AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
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
                        throw new Exception("Unsupported swiupe direction = '" + Direction + "'");
                }
                DlkEnvironment.SetContext("WEBVIEW");
                DlkLogger.LogInfo("Swipe() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Swipe() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        private static void Login(String Url, String User, String Password, String Database)
        {
            int iSleep = 1000;
            if (DlkEnvironment.mIsMobile)
            {
                iSleep = 250;
            }
            else
            {
                DlkEnvironment.GoToUrl(Url);
            }
            DlkObjectStoreFileControlRecord mTermsAndUseOfServiceControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Terms", "TermsAndUseOfService");
            DlkButton btnTermsAndUseOfService = new DlkButton("TermsAndUseOfService", mTermsAndUseOfServiceControlRec.mSearchMethod, mTermsAndUseOfServiceControlRec.mSearchParameters);
            DlkObjectStoreFileControlRecord mUsernameControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "Username");
            DlkTextBox txtUsername = new DlkTextBox("Username", mUsernameControlRec.mSearchMethod, mUsernameControlRec.mSearchParameters);
            DlkObjectStoreFileControlRecord mServerUrlControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Start", "ServerURL");
            DlkTextBox txtServerUrl = new DlkTextBox("ServerURL", mServerUrlControlRec.mSearchMethod, mServerUrlControlRec.mSearchParameters);
            DlkObjectStoreFileControlRecord mKey1 = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key1");
            DlkButton btnKey1 = new DlkButton("Key1", mKey1.mSearchMethod, mKey1.mSearchParameters);

            for (int i = 0; i < 60; i++ )
            {
                if (btnTermsAndUseOfService.Exists() || txtUsername.Exists() || txtServerUrl.Exists())
                {
                    break;
                }
                Thread.Sleep(500);
            }

            if (!string.IsNullOrEmpty(Url))
            {
                if (btnTermsAndUseOfService.Exists())
                {
                    DlkVisionTimeKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                    DlkVisionTimeKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService_IAcceptTheseTerms", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                    DlkVisionTimeKeywordHandler.ExecuteKeyword("Terms", "UsageStatisticsTracking_IAcceptTheseTerms", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                }
                else if (btnKey1.Exists())
                {
                    DlkLogger.LogInfo("Landing page was PIN setup screen.");
                    DlkObjectStoreFileControlRecord cu = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "ChangeUser");
                    //construct a control from the record
                    DlkButton cuCtrl = new DlkButton("Change User", cu.mSearchMethod, cu.mSearchParameters);
                    cuCtrl.Click();
                }

                if (DlkEnvironment.mIsMobile)
                {
                    DlkObjectStoreFileControlRecord mServerURLRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "ServerURLBack");
                    DlkButton btnServerURL = new DlkButton("ServerURL", mServerURLRec.mSearchMethod, mServerURLRec.mSearchParameters);
                    if (btnServerURL.Exists())
                    {
                        btnServerURL.Click();
                    }
                    DlkVisionTimeKeywordHandler.ExecuteKeyword("Start", "ServerURL", "Set", new String[] { Url });
                    DlkVisionTimeKeywordHandler.ExecuteKeyword("Start", "Connect", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                }
            }

            // use the object store definitions 
            if (User != "")
            {
                DlkVisionTimeKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkVisionTimeKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                Thread.Sleep(iSleep);
                DlkVisionTimeKeywordHandler.ExecuteKeyword("Login", "Database", "Click", new String[] { "" });
                Thread.Sleep(iSleep);
                DlkVisionTimeKeywordHandler.ExecuteKeyword("Login", "DatabasePicker", "Select", new String[] { Database });
                DlkVisionTimeKeywordHandler.ExecuteKeyword("Login", "Database_Done", "Click", new String[] { "" });
                Thread.Sleep(iSleep);
                DlkVisionTimeKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(iSleep);
                //DlkVisionTimeKeywordHandler.ExecuteKeyword("PINSetup", "SkipPINSetup", "Click", new String[] { "" });
                //Thread.Sleep(iSleep);
            }
        }
    }
}
