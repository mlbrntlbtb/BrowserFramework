using System;
using System.Globalization;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using TrafficLiveLib.DlkFunctions;
using TrafficLiveLib.DlkControls;

namespace TrafficLiveLib.DlkSystem
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkTrafficLiveFunctionHandler
    {
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
                    case "CompareValues":
                        CompareValues(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "GetWeekNumber":
                        GetWeekNumber(Parameters[0], Parameters[1]);
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
                            case "VerifyDialogExistsWithMessage":
                                DlkDialog.VerifyDialogExistsWithMessage(Parameters[0], Parameters[1]);
                                break;
                            case "ClickDialogButton":
                                DlkDialog.ClickDialogButton(Parameters[0]);
                                break;
                            case "ClickDialogButtonIfExists":
                                DlkDialog.ClickDialogButtonIfExists(Parameters[0]);
                                break;
                            case "VerifyDialogButtonExists":
                                DlkDialog.VerifyDialogButtonExists(Parameters[0], Parameters[1]);
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
                    DlkTrafficLiveTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        /// <summary>
        /// Similar to IfThenElse but it only compares the variables
        /// </summary>
        /// <param name="VariableValue"></param>
        /// <param name="Operator"></param>
        /// <param name="ValueToTest"></param>
        [Keyword("CompareValues")]
        public static void CompareValues(String VariableValue, String Operator, String ValueToTest)
        {
            double dValueToTest = -1, dVariableValue = -1;
            DateTime dtVariableValue = DateTime.MinValue, dtValueToTest = DateTime.MinValue;
            switch (Operator)
            {
                #region =
                case "=":
                    if (String.Compare(VariableValue, ValueToTest) == 0)
                    {
                        DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                    }
                    else
                    {
                        DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues().");
                    break;
                #endregion
                #region !=
                case "!=":
                    if (String.Compare(VariableValue, ValueToTest) != 0)
                    {
                        DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                    }
                    else
                    {
                        DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues()");
                    break;
                #endregion
                #region case >
                case ">":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        {
                            if (dVariableValue > dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) > 0)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date
                    {
                        throw new Exception("CompareValues(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues()");
                    break;
                #endregion
                #region case <
                case "<":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        {
                            if (dVariableValue < dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) < 0)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("CompareValues(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues().");
                    break;
                #endregion
                #region case >=
                case ">=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        {
                            if (dVariableValue >= dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) >= 0)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("CompareValues(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues().");
                    break;
                #endregion
                #region case <=
                case "<=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        {
                            if (dVariableValue <= dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) <= 0)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("CompareValues(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues().");
                    break;
                #endregion
                default:
                    throw new Exception("CompareValues(): Unsupported operator " + Operator);
            }
        }

        [Keyword("GetWeekNumber", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public static void GetWeekNumber(String InputDate, String VariableName)
        {
            try
            {                
                CultureInfo mCI = new CultureInfo("en-US");
                Calendar mCal = mCI.Calendar;
                string dtValue = mCal.GetWeekOfYear(Convert.ToDateTime(InputDate), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday).ToString();
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed GetWeekNumber(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("GetWeekNumber() failed : " + e.Message, e);
            }
        }

        private static int getDateFormat(string inputDate)
        {
            string[] formats = {"MM/dd/yyyy",
                                   "M/d/yyyy",
                                   "MM/dd/yy",
                                   "dd/MM/yyyy",
                                   "d/M/yyyy",
                                   "dd/MM/yy",
                                   "yyyyMMdd",
                                   "yyyy-MM-dd",
                                   "yyMMdd",
                                   "yy-MM-dd",
                                   "yyMMddHHmmss",
                                   "yy-MM-dd HH:mm:ss",
                                   "yyyyMMddHHmmss",
                                   "yyyy-MM-dd HH:mm:ss",
                                   "yyyyMMddhhmmsstt",
                                   "yyyy-MM-dd hh:mm:ss tt",
                                   "MMM yy",
                                   "1-MMM-yy",
                                   "MMM-yy}",
                                   "MMddyy",
                                   "ddMMyy",
                                   "dddd, MMMM dd, yyyy",
                                   "M/dd",
                                   "dd-MMM",
                                   "MMM dd",
                                   "dd-MMM-yy",
                                   "MMM-dd",
                                   "MMM dd, yyyy",
                                   "dd-MMM-yyyy",
                                   "M/dd/yy h:mm tt",
                                   "M/dd/yy H:mm",
                                   "dddd",
                                   "MMMM",
                                   "yyyy-MM-dd hh:mm:ss tt"
                               };
            DateTime dateValue = DateTime.MinValue;

            for (int i = 0; i < formats.Length; i++)
                foreach (string dateStringFormat in formats)
                {
                    if (DateTime.TryParseExact(inputDate, dateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                                                   out dateValue))
                        return i;
                }

            return -1;
        }

        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        private static void Login(String Url, String User, String Password, String Database, String Pin)
        {
            if (string.IsNullOrEmpty(Url) && string.IsNullOrEmpty(User))
                return;

            int iSleep = 1000;
            int iSecToWait = 10;
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
            //Wait until Terms of Use/Login/URL page exists
            for (int i = 0; i < 300; i++)
            {
                if (btnTermsAndUseOfService.Exists() || txtUsername.Exists() || txtServerUrl.Exists() || btnKey1.Exists())
                {
                    break;
                }
                Thread.Sleep(iSecToWait * iSleep);
            }

           

            if (!txtUsername.Exists())
            {
                    //Wait until Terms and Use of Service is visible
                    for (int i = 0; i < 300; i++)
                    {
                        if (btnTermsAndUseOfService.Exists() || btnKey1.Exists())
                            break;
                        Thread.Sleep(iSecToWait * iSleep);
                    }

                    if (!string.IsNullOrEmpty(Url))
                    {
                        Thread.Sleep(5000); //for the fading effect
                        if (btnTermsAndUseOfService.Exists())
                        {
                            //Terms and Use -> Accept Terms -> Usage Statistics
                            //Click each button on each page to proceed to PIN Setup
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService", "Click", new String[] { "" });
                            Thread.Sleep(iSecToWait * iSleep);
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService_IAcceptTheseTerms", "Click", new String[] { "" });
                            Thread.Sleep(iSecToWait * iSleep);
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("Terms", "UsageStatisticsTracking_IAcceptTheseTerms", "Click", new String[] { "" });
                            Thread.Sleep(iSecToWait * iSleep);
                        }
                        else if (btnKey1.Exists())
                        {
                            DlkLogger.LogInfo("Landing page was PIN setup screen.");
                            DlkObjectStoreFileControlRecord cu = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "ChangeUser");
                            //construct a control from the record
                            DlkButton cuCtrl = new DlkButton("Change User", cu.mSearchMethod, cu.mSearchParameters);
                            cuCtrl.Click();
                        }
                        DlkObjectStoreFileControlRecord mServerURLRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "ServerURL");
                        DlkButton btnServerURL = new DlkButton("ServerURL", mServerURLRec.mSearchMethod, mServerURLRec.mSearchParameters);
                        if (btnServerURL.Exists())
                        {
                            btnServerURL.Click();
                        }
                        DlkDialog.ClickDialogButtonIfExists("OK");
                        //Wait until Terms of Use/Login/URL page exists
                        for (int i = 0; i < 60; i++)
                        {
                            if (txtServerUrl.Exists())
                            {
                                break;
                            }
                            Thread.Sleep(iSecToWait * iSleep);
                        }

                        if (Url != "")
                        {
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("Start", "ServerURL", "Set", new String[] { Url });
                            Thread.Sleep(iSecToWait * iSleep);
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("Start", "Connect", "Click", new String[] { "" });
                            Thread.Sleep(iSecToWait * iSleep);
                        }

                    }
            }

            // use the object store definitions 
            if (User != "")
            {
                DlkTrafficLiveKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkTrafficLiveKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                Thread.Sleep(iSecToWait * iSleep);
                DlkTrafficLiveKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(iSecToWait * iSleep);


                //store SkipPINSetup control record in PINSetup screen from Object store file to a local variable
                DlkObjectStoreFileControlRecord mSkipPinSetupControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "SkipPINSetup");
                //construct a button from the record
                DlkButton btnSkipPinSetup = new DlkButton("SkipPINSetup", mSkipPinSetupControlRec.mSearchMethod, mSkipPinSetupControlRec.mSearchParameters);

                for (int i = 0; i < 60; i++)
                {
                    /*
                     * look if the SkipPINSetup button exists
                     * then confirm pin
                     */
                    if (btnSkipPinSetup.Exists())
                    {
                        DlkTrafficLiveKeywordHandler.ExecuteKeyword("PINSetup", "SkipPINSetup", "Click", new String[] { "" });
                        Thread.Sleep(iSecToWait * iSleep);
                        break;
                    }
                    else if (!btnSkipPinSetup.Exists() && !String.IsNullOrEmpty(Pin))
                    {
                        AutomatedPinSetup(Pin);
                        break;
                    }
                    Thread.Sleep(iSecToWait * iSleep);
                }
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
                DlkObjectStoreFileControlRecord enter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Enter");
                //construct a control from the record
                DlkButton lblEnter = new DlkButton("Enter", enter.mSearchMethod, enter.mSearchParameters);
                //get label control record
                DlkObjectStoreFileControlRecord reEnter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "ReEnter");
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
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("PINSetup", "Key" + p, "Click", new String[] { "" });
                            Thread.Sleep(3 * iSleep);
                        }
                        else
                        {
                            throw new Exception("Not in the PINSetup Screen.");
                        }
                    }
                }

                //click check button if reenter does not exist
                DlkObjectStoreFileControlRecord check = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Check");
                DlkButton checkBtn = new DlkButton("Check", check.mSearchMethod, check.mSearchParameters);
                if (checkBtn.Exists())
                {
                    checkBtn.Click();
                    Thread.Sleep(3 * iSleep);
                }

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
                            DlkTrafficLiveKeywordHandler.ExecuteKeyword("PINSetup", "Key" + p, "Click", new String[] { "" });
                            Thread.Sleep(3 * iSleep);
                        }
                        else
                        {
                            throw new Exception("Not in the PINSetup Screen.");
                        }
                    }
                }

                //click check button if to proceed
                if (checkBtn.Exists())
                {
                    checkBtn.Click();
                    Thread.Sleep(3 * iSleep);
                }

                DlkLogger.LogInfo("Automated PIN setup was successful!");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
