using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using CommonLib.DlkControls;
using CPTouchLib.DlkFunctions;
using CPTouchLib.DlkControls;
using CPTouchLib.DlkUtility;

namespace CPTouchLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkCPTouchFunctionHandler
    {
        public const int WAIT_TIMEOUT_SEC = 30;
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
                    case "ScrollIntoView":
                        ScrollIntoView(Parameters[0], Parameters[1]);
                        break;
                    case "SwipeUpToBottom":
                        SwipeUpToBottom();
                        break;
                    case "SwipeDownToTop":
                        SwipeDownToTop();
                        break;
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "FormatNumericText":
                        FormatNumericText(Parameters[0], Parameters[1]);
                        break;
                    case "TapPushNotification":
                        TapPushNotification(Parameters[0], Parameters[1]);
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

        [Keyword("ScrollIntoView")]
        public static void ScrollIntoView(string Screen, string ControlName)
        {
            var osInfo = DlkDynamicObjectStoreHandler.Instance.GetControlRecord(Screen, ControlName);
            var target = new DlkMobileControl(ControlName, osInfo.mSearchMethod, osInfo.mSearchParameters);
            if (target.Exists())
            {
                target.ScollIntoViewUsingWebView();
            }
            else
            {
                DlkLogger.LogWarning("Cannot scroll control into view: Cannot find control");
            }
        }

        [Keyword("SwipeUpToBottom")]
        public static void SwipeUpToBottom()
        {
            try
            {
                DlkMobileControl.Swipe(DlkBaseControl.SwipeDirection.Up);
                DlkLogger.LogInfo("SwipeUpToBottom() successfully executed");
            }
            catch (Exception ex)
            {
                throw new Exception("SwipeUpToBottom failed: " + ex.Message);
            }
        }

        [Keyword("SwipeDownToTop")]
        public static void SwipeDownToTop()
        {
            try
            {
                DlkMobileControl.Swipe(DlkBaseControl.SwipeDirection.Down);
                DlkLogger.LogInfo("SwipeDownToTop() successfully executed");
            }
            catch (Exception ex)
            {
                throw new Exception("SwipeDownToTop failed: " + ex.Message);
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
                    bool bValueToTest, bVariableValue;
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
                    else if (bool.TryParse(VariableValue, out bVariableValue))
                    {
                        if (bool.TryParse(ValueToTest, out bValueToTest)) // both are boolean, so compare the boolean values
                        {
                            if (bVariableValue == bValueToTest)
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
                    DlkCPTouchTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("FormatNumericText")]
        public static void FormatNumericText(String VariableName, String OriginalText)
        {
            if (!double.TryParse(OriginalText, out double originalNum)) throw new Exception("Invalid format for {0}, please make sure to input correct numeric input without non-numeric symbols (excluding '.').");
            String strResult = String.Format("{0:N}", originalNum);
            DlkVariable.SetVariable(VariableName, strResult);
            DlkLogger.LogInfo("Successfully executed FormatNumericText(). Variable:[" + VariableName + "], Value:[" + strResult + "].");
        }

        [Keyword("TapPushNotification")]
        public static void TapPushNotification(String NotificationName, String NotificationButton)
        {
            try
            {
                DlkMobileControl.ShowNotificationBar();
                DlkMobileControl.TapNotificationHeader(NotificationName);
                DlkMobileControl.AcceptNotification(NotificationName, NotificationButton);
                DlkMobileControl.CloseNotificationBar();
                DlkLogger.LogInfo("Successfully executed TapPushNotification().");
            }
            catch(Exception ex)
            {
                throw new Exception("TapPushNotification failed: " + ex.Message);
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
            /*Get status bar height*/
            DlkEnvironment.mStatusBarHeight = DlkEnvironment.GetStatusBarHeight();

            if (!string.IsNullOrEmpty(Database))
            {
                bool proceedToLogin = true;

                if (DlkEnvironment.mBrowser.ToLower() == "ios")
                {
                    var checkControlRecord = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PIN", "Accept");
                    var checkBtn = new DlkButton(checkControlRecord.mKey, checkControlRecord.mSearchMethod, checkControlRecord.mSearchParameters);

                    DlkLogger.LogInfo("Verifying PIN setup...");
                    if (checkBtn.Exists())
                    {
                        AutomatedPinSetup(Pin);
                        proceedToLogin = false;
                    }
                    else
                        DlkLogger.LogInfo("PIN Setup not found. Proceeding to login...");
                }

                if (proceedToLogin)
                {
                    DlkLogger.LogInfo("Tapping Login...");
                    var loginControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "Login");
                    var login = new DlkButton(loginControlRec.mKey, loginControlRec.mSearchMethod, loginControlRec.mSearchParameters);

                    if (!login.Exists())
                    {
                        DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);
                        /* Hard-coded wait , avoid intermittent */
                        Thread.Sleep(DlkEnvironment.mMediumWaitMs);

                        DlkLogger.LogInfo("Setting ServerUrl...");
                        var serverUrlControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "ServerUrl");
                        var serverUrl = new DlkTextBox(serverUrlControlRec.mKey, serverUrlControlRec.mSearchMethod, serverUrlControlRec.mSearchParameters);
                        serverUrl.Set(Url);

                        DlkLogger.LogInfo("Tapping Connect...");
                        var connectControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "Connect");
                        var connect = new DlkButton(connectControlRec.mKey, connectControlRec.mSearchMethod, connectControlRec.mSearchParameters);
                        TapButton(connect);
                    }

                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);
                    /* Hard-coded wait , avoid intermittent */
                    Thread.Sleep(DlkEnvironment.mMediumWaitMs);

                    DlkLogger.LogInfo("Setting Username...");
                    var userNameControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "Username");
                    var userName = new DlkTextBox(userNameControlRec.mKey, userNameControlRec.mSearchMethod, userNameControlRec.mSearchParameters);
                    userName.Set(User);

                    DlkLogger.LogInfo("Setting Password...");
                    var passwordControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "Password");
                    var password = new DlkTextBox(passwordControlRec.mKey, passwordControlRec.mSearchMethod, passwordControlRec.mSearchParameters);
                    password.Set(Password);

                    DlkLogger.LogInfo("Setting System...");
                    var systemControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "System");
                    var system = new DlkTextBox(systemControlRec.mKey, systemControlRec.mSearchMethod, systemControlRec.mSearchParameters);
                    system.Set(Database);

                    TapButton(login);
                    Thread.Sleep(DlkEnvironment.mMediumWaitMs);

                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);

                    DlkLogger.LogInfo("Tapping TermsAndUseOfService_Show...");
                    var termsControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "TermsAndUseOfService_Show");
                    var terms = new DlkButton(termsControlRec.mKey, termsControlRec.mSearchMethod, termsControlRec.mSearchParameters);
                    TapButton(terms);
                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);

                    DlkLogger.LogInfo("Tapping TermsAndUseOfService_IAcceptTheseTerms...");
                    var termsAcceptControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "TermsAndUseOfService_IAcceptTheseTerms");
                    var termsAccept = new DlkButton(termsAcceptControlRec.mKey, termsAcceptControlRec.mSearchMethod, termsAcceptControlRec.mSearchParameters);

                    if (DlkEnvironment.mBrowser.ToLower() == "ios")
                        DlkMobileControl.SwipeFromBottomToUp();

                    TapButton(termsAccept);
                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);
                    /* Hard-coded wait , avoid intermittent */
                    Thread.Sleep(DlkEnvironment.mMediumWaitMs);

                    DlkLogger.LogInfo("Tapping UsageStatisticsTracking_IAcceptTheseTerms...");
                    var termsUSTAcceptControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "UsageStatisticsTracking_IAcceptTheseTerms");
                    var termsUSTAccept = new DlkButton(termsUSTAcceptControlRec.mKey, termsUSTAcceptControlRec.mSearchMethod, termsUSTAcceptControlRec.mSearchParameters);
                    TapButton(termsUSTAccept);
                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);

                    AutomatedPinSetup(Pin);

                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);

                    /* Re-enter */
                    AutomatedPinSetup(Pin);

                    DlkCPTouchCommon.WaitForSpinnerToFinishLoading(WAIT_TIMEOUT_SEC);
                    DlkLogger.LogInfo("Auto-login successful!");
                }
            }
        }

        #region METHODS

        /// <summary>
        /// Automated pin setup.
        /// </summary>
        private static void AutomatedPinSetup(string pin)
        {
            try
            {
                DlkLogger.LogInfo("Attempting to configure PIN...");
                var customizedPin = pin.ToCharArray();

                DlkLogger.LogInfo("Configuring PIN: " + pin);
                //var pinButtons = new List<Point>();
                foreach (var p in customizedPin)
                {
                    //store Key control record in PINSetup screen from Object store file to a local variable
                    DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PIN", "Key" + p);
                    //construct a control from the record
                    DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);
                    if (!btnKey.Exists(3))
                    {
                        throw new Exception("Ensure digit: " + p + " is in the PIN Setup Screen.");
                    }

                    TapButton(btnKey);
                }
                var checkControlRecord = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PIN", "Accept");
                var checkBtn = new DlkButton(checkControlRecord.mKey, checkControlRecord.mSearchMethod, checkControlRecord.mSearchParameters);

                TapButton(checkBtn);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("Automated PIN setup was successful!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Click or tap buttons during login process (to replace base.Tap() calls)
        private static void TapButton(DlkButton button)
        {
            if (DlkEnvironment.mBrowser.ToLower() == "ios")
            {
                button.Initialize();
                button.mElement.Click();
            }
            else
                button.Tap();
        }

        #endregion
    }
}
