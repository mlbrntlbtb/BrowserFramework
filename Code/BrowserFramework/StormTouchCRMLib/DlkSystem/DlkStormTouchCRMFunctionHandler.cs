using System;
using System.Globalization;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using StormTouchCRMLib.DlkFunctions;
using OpenQA.Selenium.Appium;
using StormTouchCRMLib.DlkControls;
using CommonLib.DlkControls;

namespace StormTouchCRMLib.DlkSystem
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkStormTouchCRMFunctionHandler
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
                    case "DayToday":
                        DayToday(Parameters[0]);
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
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }

        [Keyword("DayToday")]
        public static void DayToday(String VariableName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(VariableName)) throw new ArgumentException("VariableName must not be empty.");
                var day = DateTime.Now.DayOfWeek.ToString();
                DlkVariable.SetVariable(VariableName, day);
                DlkLogger.LogInfo(String.Format("DayToday() passed. Stored '{0}' to variable '{1}'", day, VariableName));

            }
            catch (Exception ex)
            {
                throw new Exception("DayToday() failed. " + ex.Message);
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
                    DlkStormTouchCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        private static void Login(String Url, String User, String Password, String Database, String Pin)
        {
            int iSleep = 1000;
            int iSecToWait = 5;
            if (DlkEnvironment.mIsMobile)
            {
                iSleep = 250;
                iSecToWait = 1;
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
            for (int i = 0; i < 60; i++)
            {
                if (btnTermsAndUseOfService.Exists() || txtUsername.Exists() || txtServerUrl.Exists())
                {
                    break;
                }
                Thread.Sleep(iSleep);
            }

            if (!string.IsNullOrEmpty(Url))
            {
                if (btnTermsAndUseOfService.Exists())
                {
                    Thread.Sleep(5000); // wait for the loading effect, btnTermsAndUseOfService exists but is still not interactable after a few seconds
                    DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                    DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService_IAcceptTheseTerms", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                    DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Terms", "UsageStatisticsTracking_IAcceptTheseTerms", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);

                }

                if (DlkEnvironment.mIsMobile)
                {
                    DlkObjectStoreFileControlRecord mServerURLRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "ServerURLBack");
                    DlkButton btnServerURL = new DlkButton("ServerURL", mServerURLRec.mSearchMethod, mServerURLRec.mSearchParameters);
                    if (btnServerURL.Exists())
                    {
                        btnServerURL.Click();
                    }
                    DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Start", "ServerURL", "Set", new String[] { Url });
                    DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Start", "Connect", "Click", new String[] { "" });
                    Thread.Sleep(iSleep);
                }
            }

            // use the object store definitions 
            if (User != "")
            {
                DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                Thread.Sleep(iSleep);
                DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Login", "DatabaseClick", "Click", new String[] { "" });
                Thread.Sleep(iSleep);
                DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Login", "DatabasePicker", "Select", new String[] { Database });
                DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Login", "Database_Done", "Click", new String[] { "" });
                Thread.Sleep(iSleep);
                DlkStormTouchCRMKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(iSleep);
                for (int sleep = 0; sleep < 60; sleep++)
                {
                    DlkBaseControl spin = new DlkBaseControl("Spinner", "XPATH", "//div[@class='x-loading-spinner-outer']");
                    if (spin.Exists())
                    {
                        //if current spinner is visible, sleep 1 sec                                    
                        DlkLogger.LogInfo("Waiting for page to load...");
                        Thread.Sleep(iSleep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("Page loaded. Checking if PIN Setup exists...");
                        break;
                    }
                }
                AutomatedPinSetup(Pin);
                Thread.Sleep(iSecToWait * iSleep);


            }
        }

        /// <summary>
        /// Automated pin setup.
        /// </summary>
        private static void AutomatedPinSetup(string pin)
        {
            try
            {
                int iSleep = 1000;
                /*Handler for the PIN Setup automation when Skip Pin is not available (MaconomyTime 2.0)*/

                //get label control record
                DlkObjectStoreFileControlRecord enter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "EnterCode");
                //construct a control from the record
                DlkButton lblEnter = new DlkButton("Enter", enter.mSearchMethod, enter.mSearchParameters);
                //get label control record
                DlkObjectStoreFileControlRecord reEnter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "ReEnterCode");
                //construct a control from the record
                DlkButton lblReEnter = new DlkButton("ReEnter", reEnter.mSearchMethod, reEnter.mSearchParameters);
                DlkLogger.LogInfo("Attempting to configure PIN...");

                var customizedPin = pin.ToCharArray();

                if (lblEnter.Exists())
                {
                    DlkLogger.LogInfo("Configuring PIN: " + pin);
                    foreach (var p in customizedPin)
                    {
                        //store Key control record in PINSetup screen from Object store file to a local variable
                        DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key" + p);
                        //construct a control from the record
                        DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);

                        if (btnKey.Exists())
                        {
                            //click Key button
                            DlkStormTouchCRMKeywordHandler.ExecuteKeyword("PINSetup", "Key" + p, "Click", new String[] { "" });
                            Thread.Sleep(3 * iSleep);
                        }
                        else
                        {
                            throw new Exception("Not in the PINSetup Screen.");
                        }
                    }
                }

                //click check button if reenter does not exist
                //DlkObjectStoreFileControlRecord check = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Check");
                //DlkButton checkBtn = new DlkButton("Check", check.mSearchMethod, check.mSearchParameters);
                //if (checkBtn.Exists())
                //{
                //    checkBtn.Click();
                //    Thread.Sleep(3 * iSleep);
                //}

                //ReEnter
                if (lblReEnter.Exists())
                {
                    DlkLogger.LogInfo("Configuring Re-Enter PIN: " + pin);
                    foreach (var p in customizedPin)
                    {
                        //store Key control record in PINSetup screen from Object store file to a local variable
                        DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key" + p);
                        //construct a control from the record
                        DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);

                        if (btnKey.Exists())
                        {
                            //click Key button
                            DlkStormTouchCRMKeywordHandler.ExecuteKeyword("PINSetup", "Key" + p, "Click", new String[] { "" });
                            Thread.Sleep(3 * iSleep);
                        }
                        else
                        {
                            throw new Exception("Not in the PINSetup Screen.");
                        }
                    }
                }

                //click check button if to proceed
                //if (checkBtn.Exists())
                //{
                //    checkBtn.Click();
                //    Thread.Sleep(3 * iSleep);
                //}

                DlkLogger.LogInfo("Automated PIN setup was successful!");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
