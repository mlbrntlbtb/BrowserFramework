using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Globalization;
using MaconomyNavigatorLib.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using ControlType = System.Windows.Automation.ControlType;

namespace MaconomyNavigatorLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [CommonLib.DlkSystem.ControlType("Function")]
    public static class DlkMaconomyNavigatorFunctionHandler
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
                    case "WaitLoadingFinished":
                        WaitLoadingFinished(Parameters[0]);
                        break;
                    case "RunProgram":
                        RunProgram(Parameters[0], Parameters[1]);
                        break;
                    case "GetWeekNumber":
                        GetWeekNumber(Parameters[0], Parameters[1]);
                        break;
                    case "SSOButtonClickIfWindowIsPresent":
                        SSOButtonClickIfWindowIsPresent(Parameters[0]);
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
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);


        [Keyword("SSOButtonClickIfWindowIsPresent")]
        public static void SSOButtonClickIfWindowIsPresent(string ButtonText)
        {
            try
            {
                switch (DlkEnvironment.mBrowser.ToLower())
                {
                    case "ie":
                        PerformSSO("Internet Explorer", ButtonText, "Windows Security");
                        break;
                    case "firefox":
                        //unsupported by SSO
                        break;
                    case "chrome":
                        PerformSSO("Google Chrome", ButtonText, "Authentication Required");
                        break;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method that performs the clicking of Cancel button in SSO. Keyword parameter varies per browser.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="keyword"></param>
        private static void PerformSSO(string browser,string btnText, string keyword)
        {
            try
            {
                AutomationElement aeDesktop = AutomationElement.RootElement;
                Condition controlCondition = Automation.ControlViewCondition;
                TreeWalker controlWalker = new TreeWalker(controlCondition);

                AutomationElement browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "iAccess for Maconomy");
                if (browserWindow == null)
                {
                    browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, browser);
                }
                AutomationElement ssoWindow = DlkMSUIAutomationHelper.FindWindow(browserWindow, controlWalker, keyword);
                if (ssoWindow == null)
                {
                    DlkLogger.LogInfo("SSO window was not present.");
                    return;
                }
                var buttonName = btnText;
                AutomationElement cancelBtn = ssoWindow.FindFirst(TreeScope.Descendants,
                   new PropertyCondition(AutomationElement.NameProperty, buttonName));
                Thread.Sleep(300);
                //click button with text equivalent to desired text
                ((InvokePattern)cancelBtn.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                DlkLogger.LogInfo("SSO window was  present and "+btnText+" was clicked. Escaped out of SSO.");
            }
            catch (Exception)
            {
                //ignore as per request of Alex
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
                        throw new Exception("CompareValues(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                        //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
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
                        throw new Exception("CompareValues(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
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
                                throw new Exception("CompareValues(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
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
                                throw new Exception("CompareValues(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
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
                                throw new Exception("CompareValues(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
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
                                throw new Exception("CompareValues(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
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
                    if (String.Compare(VariableValue, ValueToTest) == 0)
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkMaconomyNavigatorTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case !=
                case "!=":
                    if (String.Compare(VariableValue, ValueToTest) != 0)
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkMaconomyNavigatorTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkMaconomyNavigatorTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkMaconomyNavigatorTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
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
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
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
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) >= 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
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
                    DlkMaconomyNavigatorTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
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
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
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
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) <= 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
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
                    DlkMaconomyNavigatorTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("WaitLoadingFinished")]
        public static void WaitLoadingFinished(String TimeOutInSeconds)
        {
            try
            {
                int tOut = int.Parse(TimeOutInSeconds);
                int count = 0;
                // Wait for 5s for good measure
                Thread.Sleep(5000);
                IWebElement spinnr = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@class='cg-busy-default-spinner nav-spinner']"));

                while (spinnr.Displayed && (count < tOut))
                {
                    DlkLogger.LogInfo("Page is still loading ... Waiting " + (++count).ToString() + "s");
                    Thread.Sleep(1000);
                }
                DlkLogger.LogInfo("Successfully executed WaitLoadingFinished()");
            }
            catch (Exception)
            {

            }
        }

        [Keyword("RunProgram")]
        public static void RunProgram(string ProgramPath, string SpaceDelimitedArguments)
        {
            try
            {
                if (!File.Exists(ProgramPath))
                {
                    if (File.Exists(Path.Combine(DlkEnvironment.mDirUserData, Path.GetFileName(ProgramPath))))
                    {
                        ProgramPath = Path.Combine(DlkEnvironment.mDirUserData, Path.GetFileName(ProgramPath));
                    }
                    else
                    {
                        throw new Exception("Program file path does not exist.");
                    }
                }
                DlkProcess.RunProcess(ProgramPath, SpaceDelimitedArguments, Path.GetDirectoryName(ProgramPath), false, 60);
                DlkLogger.LogInfo("Successfully executed RunProgram()");
            }
            catch(Exception e)
            {
                throw new Exception("RunProgram() failed " + e.Message, e);
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
            
            for(int i=0; i<formats.Length;i++)
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
        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.GoToUrl(Url);

            //IF SSO ENABLED:
            /*
             *1. Add controls for language selection for sso window handling 
             *2. Select English
             *3. Click Login
             *4. Cancel SSO Window
             *
             */
            try
            {
                var elem = DlkEnvironment.AutoDriver.FindElement(By.Id("language"));

                if (elem.FindElements(By.XPath(".//ul[@class='dropdown-menu inner']")).Count > 0 && elem.Displayed)
                {
                    elem.Click();
                    var dropdown = elem.FindElement(By.XPath(".//ul[@class='dropdown-menu inner']"));
                    //always select english
                    var english = dropdown.FindElement(By.XPath(".//*[text()='English (United States)']"));
                    english.Click();
                }
                var login = DlkEnvironment.AutoDriver.FindElement(By.XPath("//button[text()='Log In']"));
                login.Click();
                DlkMaconomyNavigatorFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");   
            }
            catch (Exception)
            {
               //throw;
            }


            //check if sso window is present after language selection (chrome)
            //or click cancel asap because the sso window appears immediately after navigating to the url (IE)
            DlkMaconomyNavigatorFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");

            DlkObjectStoreFileControlRecord btnCookieOkRecords = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Main", "CookiePolicyOkButton");
            DlkButton btnCookieOk = new DlkButton("CookiePolicyOkButton", btnCookieOkRecords.mSearchMethod, btnCookieOkRecords.mSearchParameters);
            
                
            //click cookie if user and password is not null
            if (btnCookieOk.Exists()//if the button is visible,
                &(User!=string.Empty&Password!=string.Empty))//and the username and password is not empty
            {
                btnCookieOk.Click();
            }

            //check if sso window is present
            DlkMaconomyNavigatorFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");

            // use the object store definitions 
            if(User != "")
            {
                DlkMaconomyNavigatorKeywordHandler.ExecuteKeyword("Login", "UserName", "Set", new String[] { User });
                DlkMaconomyNavigatorKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                Thread.Sleep(250);
                DlkMaconomyNavigatorKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                //if incorrect credentials, 
                //sso will pop up again
                //check if sso window is present
                DlkMaconomyNavigatorFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");
            }
        }

        
    }
}
