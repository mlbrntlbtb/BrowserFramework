using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using OpenQA.Selenium;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using MaconomyTouchLib.System;
using MaconomyTouchLib.DlkControls;
using MaconomyTouchLib.DlkFunctions;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;

namespace MaconomyTouchLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkMaconomyTouchFunctionHandler
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
                    case "VerifyModule":
                        VerifyModule(Parameters[0]);
                        break;
                    case "GetWeekNumber":
                        GetWeekNumber(Parameters[0], Parameters[1]);
                        break;
                    case "SplitWeekPeriod":
                        SplitWeekPeriod(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "SkipPINSetupIfAvailable":
                        SkipPINSetupIfAvailable();
                        break;
                    case "GetStringLength":
                        GetStringLength(Parameters[0], Parameters[1]);
                        break;
                    case "TimeFormat":
                        TimeFormat(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "VerifySlidingMenuItems":
                        VerifySlidingMenuItems(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                     case "EnterPIN":
                        EnterPIN();
                        break;
                    case "TimeToday":
                        TimeToday(Parameters[0]);
                        break;
                    case "Randomizer":
                        Randomizer(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "ScrollIntoView":
                        ScrollIntoView(Parameters[0], Parameters[1]);
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
                            case "VerifyDialogExistsAndStoreToVar":
                                DlkDialog.VerifyDialogExistsAndStoreToVar(Parameters[0]);
                                break;
                            case "VerifyIfDialogButtonIsReadOnly":
                                DlkDialog.VerifyIfDialogButtonIsReadOnly(Parameters[0],Parameters[1]);
                                break;
                            case "RepeatedlyTypeAndClickOnDialogBox":
                                DlkDialog.RepeatedlyTypeAndClickOnDialogBox(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
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

        [Keyword("EnterPIN")]
        public static void EnterPIN()
        {
            var mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
            var customizedPin = mLoginConfigHandler.mPin.ToCharArray();

            try
            {
                int iSleep = 1000;
                DlkLogger.LogInfo("Attempting to enter PIN...");                                                                     

                foreach (var p in customizedPin)
                {
                    //store Key control record in PINSetup screen from Object store file to a local variable
                    DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key" + p);
                    //construct a control from the record
                    DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);

                    if (btnKey.Exists())
                    {
                        //click Key button
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PINSetup", "Key" + p, "Click", new String[] { "" });
                        Thread.Sleep(3 * iSleep);
                    }
                    else
                    {
                        throw new Exception("Not in the PINSetup Screen.");
                    }
                }

                DlkObjectStoreFileControlRecord check = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Check");
                DlkButton checkBtn = new DlkButton("Check", check.mSearchMethod, check.mSearchParameters);
                if (checkBtn.Exists())
                {
                    checkBtn.Click();
                    Thread.Sleep(3 * iSleep);
                }

                DlkLogger.LogInfo("EnterPIN(): "+ customizedPin +" - Passed.");

            }
            catch (Exception)
            {
                throw new Exception("EnterPIN(): " + customizedPin + " - Failed.");
            }
        }

        [Keyword("GetStringLength")]
        public static void GetStringLength(String InputString, String Variable)
        {
            try
            {
                DlkVariable.SetVariable(Variable, InputString.Length.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetStringLength() failed: " + ex.Message);
            }
        }

        [Keyword("SkipPINSetupIfAvailable")]
        public static void SkipPINSetupIfAvailable()
        {
            //store SkipPINSetup control record in PINSetup screen from Object store file to a local variable
            DlkObjectStoreFileControlRecord mSkipPinSetupControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "SkipPINSetup");
            //construct a button from the record
            DlkButton btnSkipPinSetup = new DlkButton("SkipPINSetup", mSkipPinSetupControlRec.mSearchMethod, mSkipPinSetupControlRec.mSearchParameters);
            if (!btnSkipPinSetup.Exists())
            {
                var mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
                AutomatedPinSetup(mLoginConfigHandler.mPin);
            }
            else
            {
                DlkLogger.LogInfo("SkipPINSetup is visible. Cannot automatically configure PIN.");
            }
        }
        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
            double dValueToTest = -1, dVariableValue = -1;
            DateTime dtVariableValue = DateTime.MinValue, dtValueToTest = DateTime.MinValue;

            switch (Operator)
            {
                #region case =
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
                    DlkMaconomyTouchTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
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
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) > 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    else //if not numeric or date
                    {
                        throw new Exception("IfThenElse(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkMaconomyTouchTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
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
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) < 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("IfThenElse(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkMaconomyTouchTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case !=
                case "!=":
                    int iExpectedValue = -1, iActualValue = -1;
                    if (int.TryParse(VariableValue, out iActualValue))
                    {
                        if (int.TryParse(ValueToTest, out iValueToTest)) // both are numbers, so compare the numbers
                        {
                            if (iActualValue != iExpectedValue)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else // both are not numbers, so compare the values
                        {
                            if (VariableValue != ValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                    }
                    else // both are not numbers, so compare the values
                    {
                        if (VariableValue != ValueToTest)
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    DlkMaconomyTouchTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
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
                        DlkLogger.LogInfo("CompareValues()Failed: [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                        throw new Exception("CompareValues()Failed: [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
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
                        DlkLogger.LogInfo("CompareValues()Failed: [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                        throw new Exception("CompareValues()Failed: [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues()");
                    break;
                #endregion
                #region case >
                case ">":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        if (!(VariableValue.StartsWith("0")) /*&& !(ValueToTest.StartsWith("0"))*/)
                        {
                            if (dVariableValue > dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                            }
                            else
                            {
                                DlkLogger.LogInfo("CompareValues()Failed: [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                throw new Exception("CompareValues()Failed: [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
                            throw new Exception("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            throw new Exception("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date
                    {
                        DlkLogger.LogInfo("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
                        throw new Exception("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not less than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
                            throw new Exception("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not less than [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            throw new Exception("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        DlkLogger.LogInfo("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
                        throw new Exception("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
                            throw new Exception("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            throw new Exception("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        DlkLogger.LogInfo("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
                        throw new Exception("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
                            throw new Exception("CompareValues() Failed: Cannot compare string input values using " + Operator + " operator.");
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
                                DlkLogger.LogInfo("CompareValues() Failed: [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                throw new Exception("CompareValues() Failed: [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            throw new Exception("CompareValues() Failed: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        DlkLogger.LogInfo("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
                        throw new Exception("CompareValues() Failed: Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkLogger.LogInfo("Successfully executed CompareValues().");
                    break;
                #endregion
                default:
                    DlkLogger.LogInfo("CompareValues() Failed: Unsupported operator " + Operator);
                    throw new Exception("CompareValues() Failed: Unsupported operator " + Operator);
            }
        }

        [Keyword("VerifyModule", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public static void VerifyModule(String ExpectedModule = "")
        {
            try
            {
                DlkObjectStoreFileControlRecord mSlidingMenu = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Main", "ShowMenu");
                DlkButton btnSlidingMenu = new DlkButton("SlidingMenu", mSlidingMenu.mSearchMethod, mSlidingMenu.mSearchParameters);
                btnSlidingMenu.Click();
                Thread.Sleep(1000);
                IWebElement mSelected = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@id, 'maconomytimeslidingmenu')]//span[contains(@class,'dltk_mn_item_highlight')]"));
                String ActModule = mSelected.Text;
                btnSlidingMenu.Click();
                DlkAssert.AssertEqual("VerifyModule(): ", ExpectedModule, ActModule);
                DlkLogger.LogInfo("VerifyModule() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyModule() failed : " + e.Message, e);
            }
        }

        [Keyword("GetWeekNumber", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public static void GetWeekNumber(String InputDate, String VariableName)
        {
            try
            {                
                CultureInfo mCI = new CultureInfo("en-US");
                Calendar mCal = mCI.Calendar;
                int dtValue = mCal.GetWeekOfYear(Convert.ToDateTime(InputDate), CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday) + 1;
                DlkVariable.SetVariable(VariableName, dtValue.ToString());
                DlkLogger.LogInfo("Successfully executed GetWeekNumber(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("GetWeekNumber() failed : " + e.Message, e);
            }
        }


        [Keyword("SplitWeekPeriod", new String[] { "Sample Text | - | Var1 | Var2" })]
        public static void SplitWeekPeriod(String WeekPeriod, String Splitter, String VariableName1, String VariableName2)
        {
            try
            {
                WeekPeriod = WeekPeriod.Trim();
                WeekPeriod = WeekPeriod.Replace(" ", "");
                var splitText = WeekPeriod.Split(Convert.ToChar(Splitter)).ToList();

                DlkFunctionHandler.AssignToVariable(VariableName1, splitText.First());
                DlkFunctionHandler.AssignToVariable(VariableName2, splitText.Last());
                DlkLogger.LogInfo("SplitWeekPeriod() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("SplitWeekPeriod() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySlidingMenuItems", new String[] { "Sample Text | - | Var1 | Var2" })]
        public static void VerifySlidingMenuItems(String MenuName, String MenuItems, String TrueOrFalse)
        {
            try
            {
                bool expectedResult;

                if (String.IsNullOrEmpty(MenuName)) throw new Exception("Invalid MenuName.");
                if (!Boolean.TryParse(TrueOrFalse, out expectedResult)) throw new Exception("Invalid TrueOrFalse value.");

                string actualMenuItems = string.Empty;
                bool result;
                //store SkipPINSetup control record in PINSetup screen from Object store file to a local variable
                DlkObjectStoreFileControlRecord mShowMenu = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Main", "ShowMenu");
                //construct a button from the record
                DlkButton btnShowMenu = new DlkButton("ShowMenu", mShowMenu.mSearchMethod, mShowMenu.mSearchParameters);
                
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@id, 'maconomytimeslidingmenu')]")).Count < 1)
                {
                    btnShowMenu.FindElement();
                    btnShowMenu.Click();
                }

                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@id, 'maconomytimeslidingmenu')]//div[contains(@id, 'menuheader')][.='" + MenuName + "']")).Count < 0)
                {
                    throw new Exception("Unable to find MenuName [" + MenuName + "]");
                }

                IWebElement mSlidingMenuName = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@id, 'maconomytimeslidingmenu')]//div[contains(@id, 'menuheader')][.='" + MenuName + "']"));
                List<IWebElement> followingItems = mSlidingMenuName.FindElements(By.XPath(".//following-sibling::div")).ToList();
                
                foreach(IWebElement item in followingItems)
                {
                    if(item.GetAttribute("id").Contains("menuitem"))
                    {
                        if (item.Displayed)
                        {
                            IWebElement mItemText = item.FindElements(By.XPath(".//*[contains(@class, 'item_text')]")).Count > 0 ?
                                item.FindElement(By.XPath(".//*[contains(@class, 'item_text')]")) : item;
                            actualMenuItems += (mItemText.Text + "~");
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                actualMenuItems = actualMenuItems.TrimEnd('~');
                DlkLogger.LogInfo("Menu Items: " + MenuItems);
                DlkLogger.LogInfo("Actual Items: " + actualMenuItems);

                result = String.Equals(MenuItems, actualMenuItems, StringComparison.InvariantCultureIgnoreCase);
                DlkAssert.AssertEqual("VerifySlidingMenuItems", result, expectedResult);
               
            }
            catch (Exception e)
            {
                throw new Exception("VerifySlidingMenuItems() failed : " + e.Message, e);
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
                                   "yyyy-MM-dd hh:mm:ss tt",
                                   "yyyy.MM.dd"
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

        public static void GetMobileInfo()
        {
            DlkMobileRecord mobileDevice = DlkMobileHandler.GetRecord(DlkEnvironment.mBrowserID);

            if (mobileDevice != null)
            {
                DlkEnvironment.CustomInfo.Add("mobileid", new string[] { "Mobile ID", mobileDevice.MobileId });
                DlkEnvironment.CustomInfo.Add("mobileappiumserverurl", new string[] { "Mobile Appium Server URL", mobileDevice.MobileUrl });
                DlkEnvironment.CustomInfo.Add("mobiledevicename", new string[] { "Mobile Device Name", mobileDevice.DeviceName });
                DlkEnvironment.CustomInfo.Add("mobiletype", new string[] { "Mobile Type", mobileDevice.MobileType });
                DlkEnvironment.CustomInfo.Add("mobileversion", new string[] { "Mobile Version", mobileDevice.DeviceVersion });
                DlkEnvironment.CustomInfo.Add("mobileapporbrowser", new string[] { "Mobile App or Browser", mobileDevice.Path == "" ? "Browser" : "App" });
                DlkEnvironment.CustomInfo.Add("mobilepackagepath", new string[] { "Mobile Package Path", mobileDevice.Path });
            }
            else
            {
                DlkLogger.LogInfo("Mobile info not used in execution. No mobile info retrieved.");
            }
        }

        public static void GetRemoteInfo()
        {
            DlkRemoteBrowserRecord remoteInfo = DlkRemoteBrowserHandler.GetRecord(DlkEnvironment.mBrowserID);

            if (remoteInfo != null)
            {
                DlkEnvironment.CustomInfo.Add("remoteid", new string[] { "Remote ID", remoteInfo.Id });
                DlkEnvironment.CustomInfo.Add("remoteurl", new string[] { "Remote URL", remoteInfo.Url });
                DlkEnvironment.CustomInfo.Add("remotebrowser", new string[] { "Remote Browser", remoteInfo.Browser });
            }
            else
            {
                DlkLogger.LogInfo("Remote info not used in execution. No remote info retrieved.");
            }
        }

        /// <summary>
        /// Automated pin setup.
        /// </summary>
        private static void AutomatedPinSetup(string pin)
        {
            try
            {
                int iSleep = 1;

                /*Handler for the PIN Setup automation when Skip Pin is not available (MaconomyTime 2.0)*/

                //get label control record
                DlkObjectStoreFileControlRecord enter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Enter");
                
                //construct a control from the record
                DlkButton lblEnter = new DlkButton("Enter", "XPATH_DISPLAY", "//form[contains(@id,'pin')]//div[contains(@class,'pin')]//div[contains(text(),'Enter')]");

                //get label control record
                
                //construct a control from the record
                DlkButton lblReEnter = new DlkButton("ReEnter", "XPATH_DISPLAY", "//form[contains(@id,'pin')]//div[contains(@class,'pin')]//div[contains(text(),'Re-enter')]");
                DlkLogger.LogInfo("Attempting to configure PIN...");

                var customizedPin = pin.ToCharArray();

                if (lblEnter.Exists())
                {
                    DlkLogger.LogInfo("Configuring PIN: " + pin);
                    var pinButtons = new List<Point>();
                    foreach (var p in customizedPin)
                    {
                        //store Key control record in PINSetup screen from Object store file to a local variable
                        DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key" + p);
                        //construct a control from the record
                        DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);
                        if (!btnKey.Exists(1))
                        {
                            throw new Exception("Ensure digit: " + p + " is in the PIN Setup Screen.");
                        }
                        if (DlkEnvironment.mIsMobile)
                        {
                            pinButtons.Add(btnKey.GetNativeViewCenterCoordinates());
                        }
                        else
                        {
                            try
                            {
                                btnKey.Click();
                            }
                            catch (Exception e)
                            {
                                throw new Exception(e.Message + ". Ensure digit: " + p + " is in the PIN Setup Screen.");
                            }
                        }
                    }
                    if (DlkEnvironment.mIsMobile)
                    {
                        try
                        {
                            DlkBaseControl.TapInSuccession(pinButtons);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }

                //click check button if reenter does not exist
                DlkButton checkBtn = new DlkButton("Check", "XPATH_DISPLAY", "//div[contains(@id,'pin')]/input[@id='_back']");
                if (checkBtn.Exists())
                {
                    checkBtn.Click(iSleep);
                }

                //ReEnter
                if (lblReEnter.Exists())
                {
                    DlkLogger.LogInfo("Configuring Re-Enter PIN: " + pin);
                    var pinButtons = new List<Point>();
                    foreach (var p in customizedPin)
                    {
                        //store Key control record in PINSetup screen from Object store file to a local variable
                        DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key" + p);
                        //construct a control from the record
                        DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);
                        if (!btnKey.Exists(1))
                        {
                            throw new Exception("Ensure digit: " + p + " is in the PIN Setup Screen.");
                        }
                        if (DlkEnvironment.mIsMobile)
                        {
                            pinButtons.Add(btnKey.GetNativeViewCenterCoordinates());
                        }
                        else
                        {
                            try
                            {
                                btnKey.Click();
                            }
                            catch (Exception e)
                            {
                                throw new Exception(e.Message + ". Ensure digit: " + p + " is in the PIN Setup Screen.");
                            }
                        }
                    }
                    if (DlkEnvironment.mIsMobile)
                    {
                        try
                        {
                            DlkBaseControl.TapInSuccession(pinButtons);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }

                //click check button if to proceed
                if (checkBtn.Exists())
                {
                    checkBtn.Click(iSleep);
                }

                DlkLogger.LogInfo("Automated PIN setup was successful!");
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        public static void Login(String Url, String User, String Password, String Database, String Pin)
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
            var waiter = new WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(300));
            waiter.IgnoreExceptionTypes(new Type[] { typeof(NoSuchElementException) });
            DlkObjectStoreFileControlRecord mTermsAndUseOfServiceControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Terms", "TermsAndUseOfService");
            DlkButton btnTermsAndUseOfService = new DlkButton("TermsAndUseOfService", mTermsAndUseOfServiceControlRec.mSearchMethod, mTermsAndUseOfServiceControlRec.mSearchParameters);
            DlkObjectStoreFileControlRecord mUsernameControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "Username");
            DlkTextBox txtUsername = new DlkTextBox("Username", mUsernameControlRec.mSearchMethod, mUsernameControlRec.mSearchParameters);
            DlkObjectStoreFileControlRecord mServerUrlControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Start", "ServerURL");
            DlkTextBox txtServerUrl = new DlkTextBox("ServerURL", mServerUrlControlRec.mSearchMethod, mServerUrlControlRec.mSearchParameters);
            DlkObjectStoreFileControlRecord mKey1 = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key1");
            DlkButton btnKey1 = new DlkButton("Key1", mKey1.mSearchMethod, mKey1.mSearchParameters);

            DlkObjectStoreFileControlRecord mAzureLoginButton = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "AzureLogin");
            DlkToggle btnAzureLogin = new DlkToggle("AzureLoginButtonForSSO", mAzureLoginButton.mSearchMethod, mAzureLoginButton.mSearchParameters);
            //Wait until Terms of Use/Login/URL page exists
            for (int i = 0; i < 60; i++)
            {
                if (txtServerUrl.Exists() || btnTermsAndUseOfService.Exists() || txtUsername.Exists() || btnKey1.Exists())
                {
                    break;
                }
                Thread.Sleep(iSecToWait * iSleep);
            }

            if (!txtUsername.Exists())
            {
                if (DlkEnvironment.mIsMobile)
                {
                    DlkObjectStoreFileControlRecord mServerURLRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Login", "ServerURL");
                    DlkButton btnServerURL = new DlkButton("ServerURL", mServerURLRec.mSearchMethod, mServerURLRec.mSearchParameters);
                    DlkObjectStoreFileControlRecord mChangeUserRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "ChangeUser");
                    DlkButton btnChangeUser = new DlkButton("ChangeUser", mChangeUserRec.mSearchMethod, mChangeUserRec.mSearchParameters);

                    if (btnServerURL.Exists())
                    {
                        btnServerURL.Click();
                    }
                    else if (btnChangeUser.Exists())
                    {
                        btnChangeUser.Click();
                    }
                    
                    if (Url != "" && txtServerUrl.Exists())
                    {
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Start", "ServerURL", "Set", new String[] { Url });
                        Thread.Sleep(iSecToWait * iSleep);
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Start", "Connect", "Click", new String[] { "" });
                        
                        //Workaround for using Selenium ExpectedConditions since it is now obselete
                        waiter.Until(condition =>
                            {
                                try
                                {
                                    IWebElement termsAndUse = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='x-button-label'][contains(., 'Terms and Use of Service')]"));
                                    return termsAndUse.Displayed;
                                }
                                catch (StaleElementReferenceException)
                                {
                                    return false;
                                }
                                catch(NoSuchElementException)
                                {
                                    return false;
                                }
                            });

                        Thread.Sleep(iSecToWait * iSleep);
                    }
                }

                //Wait until Terms and Use of Service is visible
                for (int i = 0; i < 60; i++)
                {
                    if (btnTermsAndUseOfService.Exists())
                    {
                        break;
                    }
                    Thread.Sleep(iSleep);
                }

                if (!string.IsNullOrEmpty(Url))
                {
                    //Terms and Use -> Accept Terms -> Usage Statistics
                    if (btnTermsAndUseOfService.Exists())
                    {
                        //Click each button on each page to proceed to PIN Setup
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService", "Click", new String[] { "" });
                        Thread.Sleep(iSleep);
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Terms", "TermsAndUseOfService_IAcceptTheseTerms", "Click", new String[] { "" });
                        Thread.Sleep(iSleep);
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Terms", "UsageStatisticsTracking_IAcceptTheseTerms", "Click", new String[] { "" });
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
                    
                    //Workaround for using Selenium ExpectedConditions since it is now obselete
                    waiter.Until(condition =>
                    {
                        try
                        {
                            IWebElement login = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='x-button-label'][contains(., 'Log In')]"));
                            return login.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            return false;
                        }
                        catch (NoSuchElementException)
                        {
                            return false;
                        }
                    });

                    // toggle sso to off
                    if (btnAzureLogin.Exists())
                    {
                        DlkLogger.LogInfo("Azure button exists.");
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Login", "AzureLogin", "Set", new String[] { "Off" });
                        DlkLogger.LogInfo("Azure button was toggled to 'OFF'.");
                    }
                }
            }

            // use the object store definitions 
            if (User != "")
            {
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                Thread.Sleep(iSleep);
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                // for slow test systems. add wait for 60 seconds or until the pin setup screen appears;
                Thread.Sleep(iSleep);
                for (int sleep = 0; sleep < 60; sleep++)
                {
                    DlkBaseControl spin = new DlkBaseControl("Spinner", "XPATH", "//div[contains(@class,'x-loading-spinner')]");
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

                //After login, retrieve and store info in custom info for summary suite results
                if (DlkEnvironment.CustomInfo.Count == 0)
                {
                    try
                    {
                        //Get mobile info if mobile environment is used
                        GetMobileInfo();
                        //Get remote info if remote info is used
                        GetRemoteInfo();

                        IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        //Retrieve settings to be added in custom info
                        DlkMobileRecord mobileDevice = DlkMobileHandler.GetRecord(DlkEnvironment.mBrowserID);

                        //Retrieving build version and web service version through javascript function provided by Touch
                        string buildversion = javascript.ExecuteScript("return Deltek.common.DeltekTouchServerVersion.getAppVersionInfo().versionInfo.version").ToString() == "" ? javascript.ExecuteScript("return Deltek.common.DeltekTouchServerVersion.getAppVersionInfo().version").ToString() :
                                              javascript.ExecuteScript("return Deltek.common.DeltekTouchServerVersion.getAppVersionInfo().versionInfo.version").ToString();
                        string webService = javascript.ExecuteScript("return Deltek.common.DeltekVersionManager.arrayCurApiVersions[0].version.version").ToString();
                        string deviceType = javascript.ExecuteScript("return Ext.os.deviceType").ToString();
                        string os = javascript.ExecuteScript("return Ext.os.name").ToString();
                        string maconomyVersion = javascript.ExecuteScript("return DeltekMaconomy.common.Globals.coreVersion").ToString();
                        string touchServer = javascript.ExecuteScript("return Deltek.common.DeltekTouchServerVersion.serverVersion.versionInfo.version").ToString();
                        string nativeApp = buildversion.Remove(buildversion.LastIndexOf('.'));

                        if (buildversion != "" && webService != "" && deviceType != "" && os != "")
                        {
                            //Removing the last character in buildversion. Javascript returns a . in build version
                            DlkEnvironment.CustomInfo.Add("buildversion", new string[] { "Build Version", buildversion.Remove(buildversion.Length - 1) });
                            DlkEnvironment.CustomInfo.Add("webservice", new string[] { "Web Service", webService.Contains("-") ? webService.Remove(webService.IndexOf("-")) : webService });
                            DlkEnvironment.CustomInfo.Add("devicetype", new string[] { "Device Type", deviceType });
                            DlkEnvironment.CustomInfo.Add("operatingsystem", new string[] { "Operating System", os });
                            DlkEnvironment.CustomInfo.Add("maconomyversion", new string[] { "Maconomy Version", maconomyVersion.Contains("-") ? maconomyVersion.Remove(maconomyVersion.IndexOf("-")) : maconomyVersion });
                            DlkEnvironment.CustomInfo.Add("touchserverversion", new string[] { "Touch Server Version", touchServer.Contains("-") ? touchServer.Remove(touchServer.IndexOf("-")).Remove(touchServer.Length - 1) : touchServer.Remove(touchServer.Length - 1) });
                            DlkEnvironment.CustomInfo.Add("nativeappbuild", new string[] { "Native App Build Version", buildversion.Remove(buildversion.LastIndexOf(".")) });
                            DlkEnvironment.CustomInfo.Add("nativeapp", new string[] { "Native App Version", nativeApp.Remove(nativeApp.LastIndexOf('.')) });
                         
                            DlkLogger.LogInfo("Build Version: [" + buildversion.ToString() + "]");
                            DlkLogger.LogInfo("Web Service Version: [" + webService.ToString() + "]");
                            DlkLogger.LogInfo("Device Type: [" + deviceType.ToString() + "]");
                            DlkLogger.LogInfo("Operating System: [" + os.ToString() + "]");
                            DlkLogger.LogInfo("Maconomy Version: [" + maconomyVersion.ToString() + "]");
                            DlkLogger.LogInfo("Touch Server Version: [" + touchServer.ToString() + "]");
                            DlkLogger.LogInfo("Native App Build Version: [" + buildversion.ToString() + "]");
                        }
                    }
                    catch
                    {
                        DlkLogger.LogInfo("Failed to pull out data");
                    }
                }

                //store SkipPINSetup control record in PINSetup screen from Object store file to a local variable
                DlkObjectStoreFileControlRecord mSkipPinSetupControlRec = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "SkipPINSetup");
                //construct a button from the record
                DlkButton btnSkipPinSetup = new DlkButton("SkipPINSetup", mSkipPinSetupControlRec.mSearchMethod, mSkipPinSetupControlRec.mSearchParameters);
             
                for (int i = 0; i < 60; i++)
                {
                    /*
                     * look if the SkipPINSetup button exists
                     * if exists, skip pin setup by clicking the button (for 1.6.1)
                     * else, click pin (for 2.0)
                     * then confirm pin
                     */
                    if (btnSkipPinSetup.Exists())
                    {
                        DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PINSetup", "SkipPINSetup", "Click", new String[] { "" });
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

        [Keyword("TimeFormat", new String[] { "1|text|InputDateTime|01/01/2001 01:21PM", "2|text|Format|HHmm", "3|text|VariableName|MyVariable" })]
        public static void TimeFormat(String InputDateTime, String Format, String VariableName)
        {
            try
            {
                DateTime dateTime;
                if (!DateTime.TryParse(InputDateTime, out dateTime)) throw new Exception("Invalid InputDateTime value.");
                
                switch(Format)
                {
                    case "HH:mm":
                        DlkFunctionHandler.AssignToVariable(VariableName, dateTime.ToString("HH:mm"));
                        break;
                    case "hh:mm":
                        DlkFunctionHandler.AssignToVariable(VariableName, dateTime.ToShortTimeString());
                        break;
                    default:
                        throw new Exception("Format [" + Format + "] not supported.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetHHMMDateFormat() failed : " + e.Message, e);
            }
        }

        [Keyword("TimeToday")]
        public static void TimeToday(String VariableName)
        {
            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());
            string dtValue = dtNow.ToString("hh:mm:ss tt");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo(String.Format("Successfully executed DateToday(). Variable:[ {0} ], Value:[ {1} ]", VariableName, dtValue));
        }

        private static DateTime SetDateToUSCultureInfo(String dateToSet)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime dt = DateTime.Parse(dateToSet, cultureInfo); //parse date to en-US date format
            return dt;
        }

        [Keyword("Randomizer", new String[] { "1|text|Minimum Range|1", "2|text|Maximum Range|10", "3|variable|SampleVar" })]
        public static void Randomizer(String LowestRange, String HighestRange, String VariableName)
        {
            try
            {
                int low = 0;
                if (!int.TryParse(LowestRange, out low))
                    throw new Exception("[" + LowestRange + "] is not a valid input for parameter LowestRange.");
                int high = 0;
                if (!int.TryParse(HighestRange, out high))
                    throw new Exception("[" + HighestRange + "] is not a valid input for parameter HighestRange.");
                if (low > high)
                    throw new Exception("LowestRange [" + LowestRange + "] cannot be greater than HighestRange [" + HighestRange + "]");
                Random rand = new Random();
                string randNum = rand.Next(low, high).ToString();
                DlkVariable.SetVariable(VariableName, randNum);
                DlkLogger.LogInfo("[" + randNum + "] stored to variable [" + VariableName + "]");
                DlkLogger.LogInfo("Randomizer() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("Randomizer() failed : " + e.Message, e);
            }

        }

        [Keyword("ScrollIntoView")]
        public static void ScrollIntoView(string Screen, string ControlName)
        {
            var osInfo = DlkDynamicObjectStoreHandler.Instance.GetControlRecord(Screen, ControlName);
            var target = new DlkBaseControl(ControlName, osInfo.mSearchMethod, osInfo.mSearchParameters);
            target.ScrollIntoView();
        }
    }
}