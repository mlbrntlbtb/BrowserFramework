using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using WebTimeClockLib.DlkControls;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Globalization;

namespace WebTimeClockLib.DlkSystem
{
    [ControlType("Function")]
    public static class DlkWebTimeClockFunctionHandler
    {
        private const int LOGIN_PROGRESS_WAIT_LIMIT = 60;
        private const int LOGIN_PROGRESS_WAIT_TO_EXIST = 1;

        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }
        public static void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
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
        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "BadgeNumber");
            DlkTextBox badgeTextbox = new DlkTextBox(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
            if (badgeTextbox.Exists(60))
            {
                DlkLogger.LogInfo("Badge number textbox was found. Proceeding to login action.");
            }
            else
            {
                throw new Exception("Loading login page failed within maximum limit (60s).");
            }

            if (User != "")
            {
                DlkWebTimeClockKeywordHandler.ExecuteKeyword("Login", "BadgeNumber", "Set", new String[] { User });
                DlkWebTimeClockKeywordHandler.ExecuteKeyword("Login", "OK", "Click", new String[] { "" });
                Thread.Sleep(3000);

                string invalidBadgeXPath = "//div[@class='message'][contains(text(),'Invalid Badge')]";
                if(DlkEnvironment.AutoDriver.FindElements(By.XPath(invalidBadgeXPath)).Count == 0)
                {
                    DlkLogger.LogInfo("Login passed. Proceeding to main page... ");
                }
                else
                {
                    throw new Exception("Invalid badge number. Login failed.");
                }
            }
            else
            {
                DlkLogger.LogInfo("Badge number not set. Stopping to login page.");
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
                                   "M/d/yy",
                                   "d/M/yy",
                                   "dd-MM-yy",
                                   "dd.MM.y",
                                   "dd.MM.yy",
                                   "dd/MM/y",
                                   "y-MM-dd"
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

                    DlkWebTimeClockTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkWebTimeClockTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkWebTimeClockTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkWebTimeClockTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkWebTimeClockTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkWebTimeClockTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }
    }
}
