using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Globalization;
using MaconomyiAccessLib.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Linq;
using ControlType = System.Windows.Automation.ControlType;
using System.Collections.Generic;
using CommonLib.DlkControls;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;
using System.Collections;

namespace MaconomyiAccessLib.DlkSystem
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [CommonLib.DlkSystem.ControlType("Function")]
    public static class DlkMaconomyiAccessFunctionHandler
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
                    case "GetDayOfWeek":
                        GetDayOfWeek(Parameters[0], Parameters[1]);
                        break;
                    case "SSOButtonClickIfWindowIsPresent":
                        SSOButtonClickIfWindowIsPresent(Parameters[0]);
                        break;
                    case "TimeToday":
                        TimeToday(Parameters[0]);
                        break;
                    case "FileUpload":
                        FileUpload(Parameters[0], Parameters[1]);
                        break;
                    case "FileDownload":
                        FileDownload(Parameters[0], Parameters[1]);
                        break;
                    case "Randomizer":
                        Randomizer(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "RefreshPage":
                        RefreshPage();
                        break;
                    case "GetSplitWeekNumber":
                        GetSplitWeekNumber(Parameters[0], Parameters[1]);
                        break;
                    case "GetDateRangeFromWeekNumber":
                        GetDateRangeFromWeekNumber(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "ChangeNumberFormat":
                        ChangeNumberFormat(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "VerifyFileExists":
                        VerifyFileExists(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "ScrollWizardDown":
                        ScrollWizardDown(Parameters[0]);
                        break;
                    case "TextContains":
                        TextContains(Parameters[0], Parameters[1]);
                        break;
                    case "VerifyDownloadedFile":
                        VerifyDownloadedFile(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "VerifyDownloadedFileContains":
                        VerifyDownloadedFileContains(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "VerifyToastNotifExists":
                        VerifyToastNotifExists(Parameters[0]);
                        break;
                    case "ClickIfDialogExists":
                        ClickIfDialogExists(Parameters[0]);
                        break;
                    case "GetEnvironmentDetails":
                        GetEnvironmentDetails(Parameters[0], Parameters[1]);
                        break;
                    case "MultipleIfThenElse":
                        MultipleIfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4], Parameters[5]);
                        break;
                    case "IfDialogMessageExists":
                        IfDialogMessageExists(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "GetStringLength":
                        GetStringLength(Parameters[0], Parameters[1]);
                        break;
                    case "WaitIframeLoading":
                        WaitIframeLoading();
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
        /// retrieve the content of a text file
        /// </summary>
        /// <param name="DefaultFolderOrPath">file directory</param>
        /// <param name="FileName">filename of the text file</param>
        /// <returns></returns>
        private static string GetFileContent(string DefaultFolderOrPath, string FileName)
        {
            try
            {
                string defaultDownloadDirectory = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\Downloads";

                if (DefaultFolderOrPath == "")
                    DefaultFolderOrPath = defaultDownloadDirectory;
                else if (!Directory.Exists(DefaultFolderOrPath))
                    throw new ArgumentException($"GetFileContent() failed: Directory {DefaultFolderOrPath} does not exists.");

                DefaultFolderOrPath = defaultDownloadDirectory;
                var fileNames = new DirectoryInfo(defaultDownloadDirectory).GetFiles("*.txt").OrderByDescending(o => o.CreationTime).ToList();

                foreach (FileInfo _file in fileNames)
                {
                    string[] splitFile = _file.Name.Replace(".txt", "").Split('(');
                    if (splitFile[0].TrimEnd() == FileName.Replace(".txt","").TrimEnd() && (splitFile.Count() == 1 || (splitFile.Count() == 2 && splitFile[1].TrimEnd().EndsWith(")"))))
                    {
                        FileName = _file.Name;
                        break;
                    }
                }

                if (FileName.Split('.').Last() != "txt")
                    throw new ArgumentException($"GetFileContent() failed: File extension not supported");

                string file = Path.Combine(DefaultFolderOrPath, FileName);

                if (File.Exists(file))
                {
                    DlkLogger.LogInfo($"GetFileContent() : Selected file {file}");
                    string fileContent = DlkString.RemoveCarriageReturn(File.ReadAllText(file)).Trim().ToLower();
                    DlkLogger.LogInfo($"Actual Text Value: [{fileContent}]");
                    return fileContent;
                }
                else
                    throw new Exception($"GetFileContent() failed: File {file} does not exists.");
            }
            catch
            {
                throw;
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
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
                            if (dVariableValue > dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                            }
                            else
                            {
                                throw new Exception("CompareValues(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                            }
                        //}
                        //else
                        //{
                        //    throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
                            if (dVariableValue < dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                            }
                            else
                            {
                                throw new Exception("CompareValues(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                            }
                        //}
                        //else
                        //{
                        //    throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
                            if (dVariableValue >= dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                            }
                            else
                            {
                                throw new Exception("CompareValues(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                            }
                        //}
                        //else
                        //{
                        //    throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
                            if (dVariableValue <= dValueToTest)
                            {
                                DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                            }
                            else
                            {
                                throw new Exception("CompareValues(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                //DlkLogger.LogInfo("CompareValues(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                            }
                        //}
                        //else
                        //{
                        //    throw new Exception("CompareValues(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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

                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case >
                case ">":

                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
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
                        //}
                        //else
                        //{
                        //    throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case <
                case "<":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
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
                        //}
                        //else
                        //{
                        //    throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case >=
                case ">=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
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
                        //}
                        //else
                        //{
                        //    throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case <=
                case "<=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //{
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
                        //}
                        //else
                        //{
                        //    throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //}
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
                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region contains
                case "contains":
                    if (VariableValue.Contains(ValueToTest))
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] contains [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] does not contains [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region not contains
                case "not contains":
                    if (!VariableValue.Contains(ValueToTest))
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not contains [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] contains [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("MultipleIfThenElse")]
        public static void MultipleIfThenElse(String ConditionSeperator, String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            string[] VariableValueList = VariableValue.Split('~');
            string[] OperatorList = Operator.Split('~'); ;
            string[] ValueToTestList = ValueToTest.Split('~');
            ArrayList ResultBooleanList = new ArrayList();
            int iGoToStep = 0;

            for (int v = 0; v <= ValueToTestList.Count() - 1; v++)
            {
                VariableValue = VariableValueList.Count() == 1 ? VariableValue : VariableValueList[v].ToString();
                Operator = OperatorList.Count() == 1 ? Operator : OperatorList[v].ToString();
                ValueToTest = ValueToTestList.Count() == 1 ? ValueToTest : ValueToTestList[v].ToString();
                bool resultBool = false;

                int cond = v + 1;
                iGoToStep = -1;
                double dValueToTest = -1, dVariableValue = -1;
                DateTime dtVariableValue = DateTime.MinValue, dtValueToTest = DateTime.MinValue;

                switch (Operator)
                {
                    #region case =
                    case "=":
                        if (String.Compare(VariableValue, ValueToTest) == 0)
                        {
                            DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                            //iGoToStep = Convert.ToInt32(IfGoToStep);
                            resultBool = true;
                        }
                        else
                        {
                            DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                            //iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }

                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    #region case !=
                    case "!=":
                        if (String.Compare(VariableValue, ValueToTest) != 0)
                        {
                            DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                            //iGoToStep = Convert.ToInt32(IfGoToStep);
                            resultBool = true;
                        }
                        else
                        {
                            DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                            //iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }

                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    #region case >
                    case ">":

                        if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                        {
                            //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                            //{
                            if (dVariableValue > dValueToTest)
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] greater than [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(IfGoToStep);
                                resultBool = true;
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                            //}
                            //else
                            //{
                            //    throw new Exception("MultipleIfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                            //}
                        }
                        else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                        {
                            if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                            {
                                if (DateTime.Compare(dtVariableValue, dtValueToTest) > 0)
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] greater than [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(IfGoToStep);
                                    resultBool = true;
                                }
                                else
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(ElseGoToStep);
                                }
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else //if not numeric or date
                        {
                            throw new Exception("Statement #[" + cond + "]: Cannot compare input values using " + Operator + " operator.");
                        }
                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    #region case <
                    case "<":
                        if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                        {
                            //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                            //{
                            if (dVariableValue < dValueToTest)
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] less than [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(IfGoToStep);
                                resultBool = true;
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                            //}
                            //else
                            //{
                            //    throw new Exception("MultipleIfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                            //}
                        }
                        else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                        {
                            if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                            {
                                if (DateTime.Compare(dtVariableValue, dtValueToTest) < 0)
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] less than [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(IfGoToStep);
                                    resultBool = true;
                                }
                                else
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(ElseGoToStep);
                                }
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else //if not numeric or date, throw an exception
                        {
                            throw new Exception("Statement #[" + cond + "]: Cannot compare input values using " + Operator + " operator.");
                        }
                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    #region case >=
                    case ">=":
                        if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                        {
                            //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                            //{
                            if (dVariableValue >= dValueToTest)
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(IfGoToStep);
                                resultBool = true;
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                            //}
                            //else
                            //{
                            //    throw new Exception("MultipleIfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                            //}
                        }
                        else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                        {
                            if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                            {
                                if (DateTime.Compare(dtVariableValue, dtValueToTest) >= 0)
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(IfGoToStep);
                                    resultBool = true;
                                }
                                else
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(ElseGoToStep);
                                }
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else //if not numeric or date, throw an exception
                        {
                            throw new Exception("Statement #[" + cond + "]:: Cannot compare input values using " + Operator + " operator.");
                        }
                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    #region case <=
                    case "<=":
                        if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                        {
                            //if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                            //{
                            if (dVariableValue <= dValueToTest)
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(IfGoToStep);
                                resultBool = true;
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                            //}
                            //else
                            //{
                            //    throw new Exception("MultipleIfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                            //}
                        }
                        else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                        {
                            if (getDateFormat(VariableValue) == getDateFormat(ValueToTest))
                            {
                                if (DateTime.Compare(dtVariableValue, dtValueToTest) <= 0)
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(IfGoToStep);
                                    resultBool = true;
                                }
                                else
                                {
                                    DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                    //iGoToStep = Convert.ToInt32(ElseGoToStep);
                                }
                            }
                            else
                            {
                                DlkLogger.LogInfo("Statement #[" + cond + "]: Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                                //iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else //if not numeric or date, throw an exception
                        {
                            throw new Exception("Statement #[" + cond + "]: Cannot compare input values using " + Operator + " operator.");
                        }
                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    #region contains
                    case "contains":
                        if (VariableValue.Contains(ValueToTest))
                        {
                            DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] contains [" + ValueToTest + "].");
                            //iGoToStep = Convert.ToInt32(IfGoToStep);
                            resultBool = true;
                        }
                        else
                        {
                            DlkLogger.LogInfo("Statement #[" + cond + "]: [" + VariableValue + "] does not contain [" + ValueToTest + "].");
                            //iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }

                        //DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                        //DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep:" + iGoToStep.ToString());
                        break;
                    #endregion
                    default:
                        throw new Exception("MultipleIfThenElse(): Unsupported operator " + Operator);
                }
                ResultBooleanList.Add(resultBool);
            }

            switch(ConditionSeperator.ToLower())
            {
                #region OR
                case "||":
                case "or":
                    if (ResultBooleanList.Contains(true))
                    {
                        iGoToStep = Convert.ToInt32(IfGoToStep);

                        string logInfo = "";
                        int resultCount = 1;
                        foreach(bool result in ResultBooleanList)
                        {
                            if (result)
                                logInfo = logInfo + "Statement #[" + resultCount + "] is True. ";
                            resultCount++;
                        }
                        logInfo = logInfo + "Therefore condition [" + ConditionSeperator.ToUpper() + "] is True.";
                        DlkLogger.LogInfo(logInfo);
                    }
                    else
                    {
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                        DlkLogger.LogInfo("All statements are False. Therefore Condition [" + ConditionSeperator.ToUpper() + "] is False.");
                    }
                    break;
                #endregion
                #region AND
                case "&&":
                case "and":
                    if (ResultBooleanList.Contains(false))
                    {
                        iGoToStep = Convert.ToInt32(ElseGoToStep);

                        string logInfo = "";
                        int resultCount = 1;
                        foreach (bool result in ResultBooleanList)
                        {
                            if (!result)
                                logInfo = logInfo + "Statement #[" + resultCount + "] is False. ";
                            resultCount++;
                        }
                        logInfo = logInfo + "Therefore Condition [" + ConditionSeperator.ToUpper() + "] is False.";
                        DlkLogger.LogInfo(logInfo);
                    }
                    else
                    {
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                        DlkLogger.LogInfo("All statements are True. Therefore condition [" + ConditionSeperator.ToUpper() + "] is True.");
                    }
                    break;
                #endregion
                #region NONE
                default:
                    if (String.IsNullOrEmpty(ConditionSeperator) && ResultBooleanList.Count == 1)
                    {
                        bool result = Convert.ToBoolean(ResultBooleanList[0].ToString());
                        if (result)
                        {
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                            DlkLogger.LogInfo("Statement is True.");
                        }
                        else
                        {
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                            DlkLogger.LogInfo("Statement is False.");
                        }
                        break;
                    }
                    else
                    {
                        throw new Exception("Invalid condition seperator. It should only be OR [||] or AND [&&]");
                    }
                    #endregion
            }

            DlkMaconomyiAccessTestExecute.mGoToStep = (iGoToStep - 1);
            DlkLogger.LogInfo("Successfully executed MultipleIfThenElse(). GoToStep: [" + iGoToStep.ToString() + "]");
        }

        [Keyword("WaitLoadingFinished")]
        public static void WaitLoadingFinished(String TimeOutInSeconds)
        {
            try
            {
                int tOut = int.Parse(TimeOutInSeconds);
                int count = 0;
                // Wait for 5s for good measure
                Thread.Sleep(2000);

                string spinner1_XPath = "//*[@class='cg-busy-default-spinner nav-spinner']";
                string spinner2_XPath = "//ul[contains(@class,'navbar-nav')]//dm-spinner//div[@class='spinner']";
                IWebElement spinnr1 = DlkEnvironment.AutoDriver.FindElement(By.XPath(spinner1_XPath));
                IWebElement spinner2 = DlkEnvironment.AutoDriver.FindElement(By.XPath(spinner2_XPath));

                while (spinnr1.Displayed && (count < tOut))
                {
                    DlkLogger.LogInfo("[1] Page is still loading ... Waiting " + (++count).ToString() + "s");
                    Thread.Sleep(1000);
                }

                while (spinner2.Displayed && (count < tOut))
                {
                    DlkLogger.LogInfo("[2] Page is still loading ... Waiting " + (++count).ToString() + "s");
                    Thread.Sleep(1000);
                }
                DlkLogger.LogInfo("Successfully executed WaitLoadingFinished()");
            }
            catch (Exception)
            {

            }
        }

        [Keyword("WaitIframeLoading")]
        public static void WaitIframeLoading()
        {
            try
            {
                string iFrameSpinner_XPath = "//table[contains(@class,'dxlpLoadingPanel')]";
                int wait = 0;
                int waitLimit = 60;
                while(DlkEnvironment.AutoDriver.FindElements(By.XPath(iFrameSpinner_XPath)).
                    Where(x => x.Displayed).ToList().Count > 0 && wait != waitLimit)
                {
                    DlkLogger.LogInfo("Loading spinner found. Waiting for loading to finished... ");
                    Thread.Sleep(1000);
                    wait++;
                }

                if (wait == waitLimit)
                    throw new Exception("Waiting for loading spinner to finished has reached the limit.");

                DlkLogger.LogInfo("WaitIframeLoading() passed");
            }
            catch (Exception ex)
            {
                throw new Exception("WaitIframeLoading() failed: " + ex.Message);
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
                int actualWeekNumber = GetWeekNumber(InputDate);
                string actualWeekValue = actualWeekNumber.ToString();
                DlkVariable.SetVariable(VariableName, actualWeekValue);
                DlkLogger.LogInfo("Successfully executed GetWeekNumber(). Variable:[" + VariableName + "], Value:[" + actualWeekValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("GetWeekNumber() failed : " + e.Message, e);
            }
        }

        [Keyword("GetDayOfWeek", new String[] { "1|text|01/01/2020","2|text|SampleVar" })]
        public static void GetDayOfWeek(String InputDate, String VariableName)
        {
            try
            {
                if (DateTime.TryParse(InputDate, out DateTime result))
                {
                    string dayOfWeek = result.DayOfWeek.ToString();
                    DlkVariable.SetVariable(VariableName, dayOfWeek);
                    DlkLogger.LogInfo("Successfully executed GetDayOfWeek(). Variable:[" + VariableName + "], Value:[" + dayOfWeek + "].");
                }
                else
                {
                    throw new Exception("[" + InputDate + "] is not a valid date format.");
                }               
            }
            catch (Exception e)
            {
                throw new Exception("GetDayOfWeek() failed : " + e.Message, e);
            }
        }

        [Keyword("TimeToday")]
        public static void TimeToday(String VariableName)
        {
            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());
            string dtValue = dtNow.ToString("hh:mm:ss tt");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo(String.Format("Successfully executed DateToday(). Variable:[ {0} ], Value:[ {1} ]",VariableName, dtValue));
        }

        [Keyword("FileUpload", new String[] {"1|text|File Name|C:\\test.txt",
                                                "2|text|Wait Time(secs)|30"})]
        public static void FileUpload(String Filename, String WaitTime)
        {
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.FileUpload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }

        [Keyword("FileDownload", new String[] {"1|text|File Name|C:\\test.txt",
                                                "2|text|Wait Time(secs)|30"})]
        public static void FileDownload(String Filename, String WaitTime)
        {
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.FileDownload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }

        [Keyword("Randomizer", new String[] {"1|text|Minimum Range|1", "2|text|Maximum Range|10", "3|variable|SampleVar"})]
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
            catch(Exception e)
            {
                throw new Exception("Randomizer() failed : " + e.Message, e);
            }
            
        }

        [Keyword("RefreshPage")]
        public static void RefreshPage()
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().Refresh();
                DlkLogger.LogInfo("Successfully executed RefreshPage().");
            }
            catch (Exception e)
            {
                throw new Exception("RefreshPage() failed : " + e.Message);
            }
        }

        [Keyword("VerifyDownloadedFileContains", new String[] { "1|text|Folder Directory|Default User Downloads", "2|text|sample.txt", "3|text|sample text" })]
        public static void VerifyDownloadedFileContains(string DefaultFolderOrPath, string FileName, string TextToVerify)
        {
            try
            {
                if (FileName == "")
                {
                    throw new Exception("VerifyDownloadedFileContains() failed : File name is required.");
                }
                else
                {
                    string fileContent = GetFileContent(DefaultFolderOrPath, FileName);
                    if (TextToVerify != "") //auto pass if no text to verify
                    {
                        DlkAssert.AssertEqual("VerifyDownloadedFileContains() : ", true, fileContent.Contains(TextToVerify.ToLower()));
                    }
                    DlkLogger.LogInfo("VerifyDownloadedFileContains() passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDownloadedFileContains() failed : " + e.Message);
            }
        }

        [Keyword("VerifyDownloadedFile", new String[] { "1|text|Folder Directory|Default User Downloads","2|text|sample.txt","3|text|sample text" })]
        public static void VerifyDownloadedFile(string DefaultFolderOrPath, string FileName, string TextToVerify)
        {
            try
            {
                if (FileName == "")
                {
                    throw new Exception("VerifyDownloadedFile() failed : File name is required.");
                }
                else
                {
                    string fileContent = GetFileContent(DefaultFolderOrPath, FileName);
                    if (TextToVerify != "")//auto pass if no text to verify
                    {
                        DlkAssert.AssertEqual("VerifyDownloadedFile() : ", true, fileContent == TextToVerify.ToLower());
                    }
                    DlkLogger.LogInfo("VerifyDownloadedFile() passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDownloadedFile() failed : " + e.Message);
            }
        }

        [Keyword("VerifyFileExists")]
        public static void VerifyFileExists(String DefaultFolderOrPath, String FileName, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                string verifyPath = "";

                if (Path.IsPathRooted(DefaultFolderOrPath))
                    verifyPath = DefaultFolderOrPath;
                else
                {
                    string currentUserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    verifyPath = Path.Combine(currentUserPath, DefaultFolderOrPath);
                }

                bool actualValue = File.Exists(Path.Combine(verifyPath, FileName));

                DlkAssert.AssertEqual("VerifyFileExists()", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyFileExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFileExists() failed : " + e.Message);
            }
        }

        [Keyword("GetSplitWeekNumber", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public static void GetSplitWeekNumber(String InputDate, String VariableName)
        {
            try
            {
                string actualSplitWeekValue = GetSplitWeekNumber(InputDate);
                DlkVariable.SetVariable(VariableName, actualSplitWeekValue);
                DlkLogger.LogInfo("Successfully executed GetSplitWeekNumber(). Variable:[" + VariableName + "], Value:[" + actualSplitWeekValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("GetSplitWeekNumber() failed : " + e.Message, e);
            }
        }

        [Keyword("GetDateRangeFromWeekNumber", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public static void GetDateRangeFromWeekNumber(String WeekNumber, String Year, String DateFormat, String VariableName)
        {
            try
            {
                string weekNumberChar = new string(WeekNumber.Where(Char.IsDigit).ToArray());
                string splitWeekChar = new string(WeekNumber.Where(Char.IsLetter).ToArray());

                int weekNumber = 0;
                if (!int.TryParse(weekNumberChar, out weekNumber))
                    throw new Exception("[" + weekNumberChar + "] is not a valid input for parameter WeekNumber.");

                if (!String.IsNullOrEmpty(splitWeekChar))
                {
                    switch (splitWeekChar.ToLower())
                    {
                        case "a":
                            //do nothing
                            break;
                        case "b":
                            //do nothing
                            break;
                        default:
                            throw new Exception("SplitWeekChar: [" + splitWeekChar + "] is not a valid split week character. It should be 'A' or 'B' only.");

                    }
                }

                int yearNumber = Convert.ToInt32(Year);
                DateTime initialDate = new DateTime(yearNumber, 1, 1);
                int actualWeekNumber = 0;
                string actualWeekNumberText = "";

                //Add (1) one week to initial date to reach actual date of week number
                while (weekNumber != actualWeekNumber)
                {
                    actualWeekNumber = GetWeekNumber(initialDate.ToString());
                    if(weekNumber != actualWeekNumber)
                    {
                        initialDate = initialDate.AddDays(7);
                    }
                    else
                    {
                        DateTime startDate = initialDate;
                        DateTime endDate = initialDate;

                        //Get date ranges of the target week number
                        if (String.IsNullOrEmpty(splitWeekChar))
                        {
                            int prevDateWeekNumber = actualWeekNumber;
                            int nextDateWeekNumber = actualWeekNumber;

                            //Get start date starting from the actual date
                            while (prevDateWeekNumber == actualWeekNumber)
                            {
                                startDate = startDate.AddDays(-1);
                                prevDateWeekNumber = GetWeekNumber(startDate.ToString());
                                if (prevDateWeekNumber != actualWeekNumber)
                                {
                                    startDate = startDate.AddDays(1);
                                    break;
                                }
                            }
                            //Get end date starting from the actual date
                            while (nextDateWeekNumber == actualWeekNumber)
                            {
                                endDate = endDate.AddDays(1);
                                nextDateWeekNumber = GetWeekNumber(endDate.ToString());
                                if (nextDateWeekNumber != actualWeekNumber)
                                {
                                    endDate = endDate.AddDays(-1);
                                    break;
                                }
                            }
                            actualWeekNumberText = actualWeekNumber.ToString();
                        }

                        //Get date range of target split week number
                        else
                        {
                            string actualSplitWeekNumber = GetSplitWeekNumber(initialDate.ToString());
                            if(actualSplitWeekNumber.ToLower().Contains("a") || actualSplitWeekNumber.ToLower().Contains("b"))
                            {
                                //Check if split week char parameter is same as current split week number
                                if (splitWeekChar.ToLower().Equals("a") && !actualSplitWeekNumber.ToLower().Contains("a"))
                                {
                                    string subtractSplitWeekNumber = actualSplitWeekNumber;
                                    while (subtractSplitWeekNumber == actualSplitWeekNumber)
                                    {
                                        initialDate = initialDate.AddDays(-1);
                                        subtractSplitWeekNumber = GetSplitWeekNumber(initialDate.ToString());
                                        if (subtractSplitWeekNumber != actualSplitWeekNumber)
                                            break;
                                    }
                                }

                                if (splitWeekChar.ToLower().Equals("b") && !actualSplitWeekNumber.ToLower().Contains("b"))
                                {
                                    string addSplitWeekNumber = actualSplitWeekNumber;
                                    while (addSplitWeekNumber == actualSplitWeekNumber)
                                    {
                                        initialDate = initialDate.AddDays(1);
                                        addSplitWeekNumber = GetSplitWeekNumber(initialDate.ToString());
                                        if (addSplitWeekNumber != actualSplitWeekNumber)
                                            break;
                                    }
                                }

                                actualSplitWeekNumber = GetSplitWeekNumber(initialDate.ToString());
                                startDate = initialDate;
                                endDate = initialDate;
                                string prevDateSplitWeekNumber = actualSplitWeekNumber;
                                string nextDateSplitWeekNumber = actualSplitWeekNumber;

                                //Get start date starting from the actual date
                                while (prevDateSplitWeekNumber == actualSplitWeekNumber)
                                {
                                    startDate = startDate.AddDays(-1);
                                    prevDateSplitWeekNumber = GetSplitWeekNumber(startDate.ToString());
                                    if (prevDateSplitWeekNumber != actualSplitWeekNumber)
                                    {
                                        startDate = startDate.AddDays(1);
                                        break;
                                    }
                                }
                                //Get end date starting from the actual date
                                while (nextDateSplitWeekNumber == actualSplitWeekNumber)
                                {
                                    endDate = endDate.AddDays(1);
                                    nextDateSplitWeekNumber = GetSplitWeekNumber(endDate.ToString());
                                    if (nextDateSplitWeekNumber != actualSplitWeekNumber)
                                    {
                                        endDate = endDate.AddDays(-1);
                                        break;
                                    }
                                }
                                actualWeekNumberText = actualSplitWeekNumber;
                            }
                            else
                            {
                                throw new Exception("Week number is not a valid split week. Set SplitWeekChar parameter to blank or select a valid split week number.");
                            }
                        }

                        //Change date format
                        string startDateText = DlkString.GetDateAsText(startDate, DateFormat);
                        string endDateText = DlkString.GetDateAsText(endDate, DateFormat);
                        string dateRangeFromWeekNumberText = startDateText + " - " + endDateText;

                        DlkLogger.LogInfo("Week Number: [" + actualWeekNumberText + "] Start Date: [" + startDateText + "] End Date: [" + endDateText + "]");
                        DlkLogger.LogInfo("Value [" + dateRangeFromWeekNumberText + "] assigned to Variable Name: [" + VariableName + "]");
                        DlkVariable.SetVariable(VariableName, dateRangeFromWeekNumberText);
                        break;
                    }
                }
                DlkLogger.LogInfo("GetDateRangeFromWeekNumber() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetDateRangeFromWeekNumber() failed : " + e.Message, e);
            }
        }

        [Keyword("ChangeNumberFormat", new String[] { "1|number|1234.53", "2|digit symbol|,", "3|decimal symbol|.", "4|variable|SampleVar" })]
        public static void ChangeNumberFormat(String NumberValue, String DigitGroupSymbol, String DecimalSymbol, String VariableName)
        {
            try
            {
                if (!double.TryParse(NumberValue, out double num))
                    throw new Exception("[" + NumberValue + "] is not a valid input for parameter NumberValue.");

                string ConvertedValue = "";
                string digitSymbol = DigitGroupSymbol.ToLower();
                string decimalSymbol = DecimalSymbol.ToLower();
                Boolean decimalIsPeriod = false;
                
                //Convert value depending on desired decimal symbol
                switch (decimalSymbol)
                {
                    case "period":
                    case ".":
                        decimalIsPeriod = true;
                        ConvertedValue = num.ToString("N", new CultureInfo("en-US"));
                        break;
                    case "comma":
                    case ",":
                        ConvertedValue = num.ToString("N", new CultureInfo("is-IS"));
                        break;
                    default:
                        throw new Exception("Invalid Decimal Symbol. Provide a valid Decimal Symbol.");
                }
                
                //Convert value depending on desired digit grouping symbol
                switch (digitSymbol)
                {
                    case "apostrophe":
                    case "'":
                        if (decimalIsPeriod)
                            ConvertedValue = ConvertedValue.Replace(",", "'");
                        else
                            ConvertedValue = ConvertedValue.Replace(".", "'");
                        break;
                    case "space":
                    case " ":
                        if (decimalIsPeriod)
                            ConvertedValue = ConvertedValue.Replace(",", " ");
                        else
                            ConvertedValue = ConvertedValue.Replace(".", " ");
                        break;
                    case "period":
                    case ".":
                        if(decimalIsPeriod)
                            throw new Exception("Digit Group Symbol [" + DigitGroupSymbol + "] cannot be the same as Decimal Symbol [" + DecimalSymbol + "]");
                        break;
                    case "comma":
                    case ",":
                        if(!decimalIsPeriod)
                            throw new Exception("Digit Group Symbol [" + DigitGroupSymbol + "] cannot be the same as Decimal Symbol [" + DecimalSymbol + "]");
                        break;
                    default:
                        throw new Exception("Invalid Digit Grouping Symbol. Provide a valid Digit Grouping Symbol.");
                }

                DlkVariable.SetVariable(VariableName,ConvertedValue);
                DlkLogger.LogInfo("[" + ConvertedValue + "] stored to variable [" + VariableName + "]");
                DlkLogger.LogInfo("ChangeNumberFormat() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ChangeNumberFormat() failed : " + e.Message, e);
            }

        }

        [Keyword("ScrollWizardDown")]
        public static void ScrollWizardDown(String NumberOfScrolls)
        {
            int scrolls = 0;
            if (!int.TryParse(NumberOfScrolls, out scrolls) || scrolls == 0)
                throw new Exception("[" + NumberOfScrolls + "] is not a valid input for parameter NumberOfScrolls.");

            try
            {
                IWebElement wizard = DlkEnvironment.AutoDriver.FindElements(By.XPath("//dm-wizard/div")).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath("//dm-wizard/div")) : throw new Exception("Wizard not found.");
                IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                for (int s = 1; s <= scrolls; s++)
                {
                    js.ExecuteScript("arguments[0].scrollTop += 1000", wizard);
                    DlkLogger.LogInfo("Scrolling wizard down... [" + s.ToString() + "]");
                }

                DlkLogger.LogInfo("ScrollWizardDown() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollWizardDown() failed : " + e.Message);
            }
        }

        [Keyword("VerifyToastNotifExists", new String[] { "1|text|Expected Value|TRUE" })]
        public static void VerifyToastNotifExists(string ExpectedResult)
        {
            try
            {
                if (bool.TryParse(ExpectedResult, out bool result))
                {
                    int toastCount = DlkEnvironment.AutoDriver.FindElements(By.XPath("//dm-toast|//div[contains(@class,'toast-text')]")).Count();
                    DlkLogger.LogInfo($"Actual toast notification count : {toastCount}");
                    DlkAssert.AssertEqual("VerifyToastNotifExists() : ", result, toastCount > 0);
                    DlkLogger.LogInfo("VerifyToastNotifExists() Passed");
                }
                else
                    throw new ArgumentException($"VerifyToastNotifExists() failed : Invalid TrueOrFalse parameter {ExpectedResult}");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyToastNotifExists() failed : " + e.Message);
            }
        }

        [Keyword("TextContains")]
        public static void TextContains(String ValueInput, String Contains)
        {
            try
            {
                DlkAssert.AssertEqual("TextContains()", Contains.ToLower() , ValueInput.ToLower(), true);
                DlkLogger.LogInfo("Value input contains [" + Contains + "]");
                DlkLogger.LogInfo("TextContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("TextContains() failed: " + e.Message);
            }
        }

        [Keyword("ClickIfDialogExists", new String[] { "1|text|Expected Value|TRUE" })]
        public static void ClickIfDialogExists(String WaitTime)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(WaitTime, out wait) || wait == 0)
                    throw new Exception("[" + WaitTime + "] is not a valid input for parameter WaitTime.");

                bool dFound = false;
                int waitDialogLoad = 1;
                int waitDialogLoadLimit = wait;
                string dialogXPath = "//dm-message-dialog//*[contains(@class,'modal-content')]";
                string dialogButtonXPath = "//dm-message-dialog//button[contains(@class,'primary')] | //dm-wizard//div[contains(@class,'modal-footer')]//button[contains(@class,'primary')]";

                while (waitDialogLoad != waitDialogLoadLimit)
                {
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(dialogXPath)).Count == 0)
                    {
                        DlkLogger.LogInfo("Waiting for dialog to appear... [" + waitDialogLoad.ToString() + "]s");
                        Thread.Sleep(1000);
                        waitDialogLoad++;
                        continue;
                    }
                    else
                    {
                        DlkLogger.LogInfo("Dialog found. Executing click on OK button... ");
                        IWebElement dialogButton = DlkEnvironment.AutoDriver.FindElements(By.XPath(dialogButtonXPath)).Count > 0 ?
                            DlkEnvironment.AutoDriver.FindElement(By.XPath(dialogButtonXPath)) : throw new Exception("Dialog button not found");
                        dialogButton.Click();
                        dFound = true;
                        DlkLogger.LogInfo("Dialog OK Button has been successfully clicked... ");
                        break;
                    }
                }

                if (!dFound)
                    DlkLogger.LogInfo("Dialog did not found. No dialog button has been clicked... ");

                DlkLogger.LogInfo("ClickIfDialogExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickIfDialogExists() failed : " + e.Message);
            }
        }

        [Keyword("IfDialogMessageExists", new String[] { "1|text|Expected Value|TRUE" })]
        public static void IfDialogMessageExists(String TitleContains, String MessageContains, String ClickButtonCaption, String WaitTime)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(WaitTime, out wait) || wait == 0)
                    throw new Exception("[" + WaitTime + "] is not a valid input for parameter WaitTime.");

                int waitDialogLoad = 1;
                int waitDialogLoadLimit = wait;
                string dialogXPath = "//dm-message-dialog//*[contains(@class,'modal-content')]";
                bool dFound = false;

                while (waitDialogLoad != waitDialogLoadLimit)
                {
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(dialogXPath)).Count == 0)
                    {
                        DlkLogger.LogInfo("Waiting for dialog to appear... [" + waitDialogLoad.ToString() + "]s");
                        Thread.Sleep(1000);
                        waitDialogLoad++;
                        continue;
                    }
                    else
                    {
                        DlkLogger.LogInfo("Dialog title found. Verifying text value... ");
                        DlkMaconomyiAccessKeywordHandler.ExecuteKeyword("DialogMessageBox", "Title_Label", "VerifyTextContains", new String[] { TitleContains });
                        DlkLogger.LogInfo("Dialog message found. Verifying text value... ");
                        DlkMaconomyiAccessKeywordHandler.ExecuteKeyword("DialogMessageBox", "MessageText_Label", "VerifyTextContains", new String[] { MessageContains });

                        DlkObjectStoreFileControlRecord mControl;
                        switch (ClickButtonCaption.ToLower())
                        {
                            case "ok":
                                DlkLogger.LogInfo("Button [" + ClickButtonCaption + "] found. Executing click action... ");
                                mControl = DlkDynamicObjectStoreHandler.GetControlRecord("DialogMessageBox", "Ok_Button");
                                break;
                            case "cancel":
                                DlkLogger.LogInfo("Button [" + ClickButtonCaption + "] found. Executing click action... ");
                                mControl = DlkDynamicObjectStoreHandler.GetControlRecord("DialogMessageBox", "Cancel_Button");
                                break;
                            case "x":
                            case "close":
                                DlkLogger.LogInfo("Button [" + ClickButtonCaption + "] found. Executing click action... ");
                                mControl = DlkDynamicObjectStoreHandler.GetControlRecord("DialogMessageBox", "X_Button");
                                break;
                            case "delete":
                                DlkLogger.LogInfo("Button [" + ClickButtonCaption + "] found. Executing click action... ");
                                mControl = DlkDynamicObjectStoreHandler.GetControlRecord("DialogMessageBox", "Delete_Button");
                                break;
                            default:
                                DlkLogger.LogInfo("Button [" + ClickButtonCaption + "] not found. Executing click action on default button... ");
                                mControl = DlkDynamicObjectStoreHandler.GetControlRecord("DialogMessageBox", "Ok_Button");
                                break;
                        }
                        DlkButton mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                        mButton.Click();
                        dFound = true;
                        break;
                    }
                }
                if(!dFound)
                    DlkLogger.LogInfo("Dialog does not exist. No actions executed.");

                DlkLogger.LogInfo("IfDialogMessageExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("IfDialogMessageExists() failed : " + e.Message);
            }
        }

        [Keyword("GetEnvironmentDetails", new String[] { "Username | SampleVar" })]
        public static void GetEnvironmentDetails(String Detail, String VariableName)
        {
            try
            {
                DlkLoginConfigRecord loginConfigRecord = DlkLoginConfigHandler.GetLoginConfigInfo(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
                DlkDecryptionHelper decryptor = new DlkDecryptionHelper();

                string detailValue = "";
                switch (Detail.ToLower())
                {
                    case "url":
                        detailValue = loginConfigRecord.mUrl;
                        break;
                    case "username":
                        detailValue = decryptor.IsDecrpytable(loginConfigRecord.mUser) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(loginConfigRecord.mUser)) : loginConfigRecord.mUser;
                        break;
                    case "password":
                        detailValue = decryptor.IsDecrpytable(loginConfigRecord.mPassword) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(loginConfigRecord.mPassword)) : loginConfigRecord.mPassword;
                        break;
                    case "database":
                        detailValue = decryptor.IsDecrpytable(loginConfigRecord.mDatabase) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(loginConfigRecord.mDatabase)) : loginConfigRecord.mDatabase;
                        break;
                    default:
                        throw new Exception("Invalid input for detail. Detail should either be username, password or database.");
                }

                DlkVariable.SetVariable(VariableName, detailValue);
                DlkLogger.LogInfo("[" + detailValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetEnvironmentDetails() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetEnvironmentDetails() failed : " + e.Message, e);
            }
        }

        [Keyword("GetStringLength")]
        public static void GetStringLength(String InputString, String VariableName)
        {
            try
            {
                string lengthString = InputString.Length.ToString();
                DlkVariable.SetVariable(VariableName, lengthString);
                DlkLogger.LogInfo("[" + lengthString + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetStringLength() passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("GetStringLength() failed: " + ex.Message);
            }
        }

        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        private static int GetWeekNumber(String InputDate)
        {
            DateTime dateValue = Convert.ToDateTime(InputDate);
            string inputYear = dateValue.ToString("yyyy");
            string inputMonth = dateValue.ToString("MM");
            CultureInfo mCI = new CultureInfo("en-US");
            Calendar mCal = mCI.Calendar;

            //Check first week if equivalent to 1 to verify if needed weeknumber needs to be adjusted

            int firstWeekNumber = mCal.GetWeekOfYear(Convert.ToDateTime("1/1/" + inputYear), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            bool isFirstWeekOne = firstWeekNumber == 1 ? true : false;
            int weekNumber = mCal.GetWeekOfYear(dateValue, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            //Change value of actual week number by its first week depending on split week number
            int actualWeekNumber = weekNumber;

            //Change last week of december value to 1 if it falls to first week of january next year
            if (inputMonth == "12")
            {
                if (!isFirstWeekOne)
                    actualWeekNumber += 1;

                DayOfWeek checkDay = dateValue.DayOfWeek == DayOfWeek.Monday ? DayOfWeek.Sunday : DayOfWeek.Monday;
                DateTime newDate = dateValue;

                do
                {
                    newDate = newDate.AddDays(1);
                    if (newDate.ToString("yyyy") != inputYear && !IsolateYearNumber(inputYear))
                    {
                        actualWeekNumber = 1;
                        break;
                    }
                }
                while (newDate.DayOfWeek != checkDay);
            }
            else
            {
                if (actualWeekNumber != firstWeekNumber && !isFirstWeekOne)
                    actualWeekNumber += 1;
                else if (actualWeekNumber == firstWeekNumber && !isFirstWeekOne)
                    actualWeekNumber = 1;
            }

            actualWeekNumber = IsolateDeductWeekNumber(actualWeekNumber, inputYear);
            return actualWeekNumber;
        }

        private static string GetSplitWeekNumber(String InputDate)
        {
            DateTime dateValue = Convert.ToDateTime(InputDate);
            int actualWeekNumber = GetWeekNumber(InputDate);
            string actualWeekValue = actualWeekNumber.ToString();

            // Add split characters 'A' for first half and 'B' for second half if the week splits within two months
            string inputMonth = dateValue.ToString("MM");
            DateTime splitDate;
            DayOfWeek splitCheckDay = dateValue.DayOfWeek;

            //Checks for split week first half
            if (splitCheckDay != DayOfWeek.Sunday)
            {
                splitDate = dateValue;
                do
                {
                    splitDate = splitDate.AddDays(1);
                    if (splitDate.ToString("MM") != inputMonth)
                    {
                        actualWeekValue = actualWeekValue + "A";
                        break;
                    }
                }
                while (splitDate.DayOfWeek != DayOfWeek.Sunday);
            }

            //Checks for split week end half
            if (!actualWeekValue.Contains("A") && splitCheckDay != DayOfWeek.Monday)
            {
                splitDate = dateValue;
                do
                {
                    splitDate = splitDate.AddDays(-1);
                    if (splitDate.ToString("MM") != inputMonth)
                    {
                        actualWeekValue = actualWeekValue + "B";
                        break;
                    }
                }
                while (splitDate.DayOfWeek != DayOfWeek.Monday);
            }

            return actualWeekValue;
        }

        private static int IsolateDeductWeekNumber(int actualWeekNumber, string inputYear)
        {
            //Isolated request for year 2021 - 2023, first week does not start with 1 but starts with last split week number
            if (inputYear.Equals("2021") || inputYear.Equals("2022") || inputYear.Equals("2023"))
            {
                actualWeekNumber -= 1;
                if (actualWeekNumber.Equals(0))
                {
                    int prevYear = Convert.ToInt32(inputYear) - 1;
                    actualWeekNumber = GetWeekNumber("12/31/" + prevYear.ToString());
                }
                    
            }
            return actualWeekNumber;
        }

        private static bool IsolateYearNumber (string inputYear)
        {
            //Isolate changing of last week of december value to 1 if year is not 2020 - 2022
            bool isolateYear = false;
            if (inputYear.Equals("2020") || inputYear.Equals("2021") || inputYear.Equals("2022"))
                isolateYear = true;

            return isolateYear;
        }

        private static DateTime SetDateToUSCultureInfo(String dateToSet)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime dt = DateTime.Parse(dateToSet, cultureInfo); //parse date to en-US date format
            return dt;
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
        /// Performs a login to iAccess
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        private static void Login(String Url, String User, String Password, String Database)
        {
            try
            {
                //Get mobile info if mobile environment is used
                GetMobileInfo();

                //Get remote info if remote info is used
                GetRemoteInfo();

                //Get platform info
                GetPlatformInfo();

                //Loads URL to current browser session
                LoadURL(Url);

                //Reset zoom level to 100 for IE
                ResetZoomLevel100();

                //Adjusting zoom level to 150 for IE browser because of Click issues
                ZoomInBrowserForIE(2);

                DlkLink lnkOverride = new DlkLink("Override Link", "XPATH_DISPLAY", "//a[@id='overridelink']");

                if (lnkOverride.Exists(1))
                {
                    lnkOverride.Click();
                }

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
                    var elem = DlkEnvironment.AutoDriver.FindElement(By.TagName("dm-language-selector"));
                    elem.Click();
                    if (elem.FindElements(By.XPath(".//ul[contains(@class,'dropdown-menu')]")).Count > 0)
                    {
                        var dropdown = elem.FindElement(By.XPath(".//ul[contains(@class,'dropdown-menu')]"));
                        //always select english
                        var english = dropdown.FindElement(By.XPath(".//*[text()='English (United States)']"));
                        english.Click();
                    }
                    var login = DlkEnvironment.AutoDriver.FindElement(By.Id("login"));
                    //Changed all click function to Selenium click because of Javascript issues
                    login.Click();
                    DlkMaconomyiAccessFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");
                }
                catch (Exception)
                {
                    //throw;
                }


                //check if sso window is present after language selection (chrome)
                //or click cancel asap because the sso window appears immediately after navigating to the url (IE)
                DlkMaconomyiAccessFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");

                //Wait for login page to finish loading
                WaitForLoginPage();
                //Close toast text pop up if displayed
                CloseToastMessages();

                //Verify cookies button then close
                bool isCookiesClosed = VerifyCookies(User, Password);

                //check if sso window is present
                DlkMaconomyiAccessFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");

                //Get version info from before login - Version, Build Type, FPU
                GetVersionInfo();

                // use the object store definitions 
                if (User != "")
                {
                    DlkMaconomyiAccessKeywordHandler.ExecuteKeyword("Login", "UserName", "Set", new String[] { User });
                    DlkMaconomyiAccessKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                    Thread.Sleep(250);
                    var login = DlkEnvironment.AutoDriver.FindElement(By.XPath("//dm-login-button"));
                    //Changed all click function to Selenium click because of Javascript issues
                    try
                    {
                        login.Click();
                    }
                    catch (ElementNotInteractableException)
                    {
                        login = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@dmdataid='dm-login-button']"));
                        login.Click();
                    }
                    //if incorrect credentials, 
                    //sso will pop up again
                    //check if sso window is present
                    DlkMaconomyiAccessFunctionHandler.SSOButtonClickIfWindowIsPresent("Cancel");
                    //waiting time for loading application. Sidebar is the last component which loads in the page after login.
                    while (DlkEnvironment.AutoDriver.FindElements(By.XPath("//dm-menu//div[@class='content']")).Count < 0)
                    {
                        //sleep for 1s
                        Thread.Sleep(1000);
                    }

                    //Get additional version info after login - TPU & APU
                    GetAdditionalVersionInfo();
                }

                //Verify again cookies button then close
                if(!isCookiesClosed)
                    VerifyCookies(User, Password);

            }
            catch (Exception e)
            {
                ZoomOutBrowserForIE(2);
                throw new Exception(e.ToString());
            }
        }

        private static void LoadURL(string Url, bool ReOpened = false)
        {
            bool isURLSuccessLoad = false;

            //Set pageload timeout to 90 seconds to verify page load failure instance
            DlkEnvironment.AutoDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(90);
            var task = Task.Run(() => DlkEnvironment.AutoDriver.Navigate().GoToUrl(Url));

            try
            {
                int waitTime = 90;
                if (DlkEnvironment.mBrowser.ToLower() == "safari")
                {
                    waitTime = 180;
                }
                if (!task.Wait(TimeSpan.FromSeconds(waitTime)))
                {
                    //Close current session if page load exceeds 90 seconds
                    DlkEnvironment.CloseSession();
                    throw new Exception("Page loading has failed. Closing current session ...");
                }
                else
                {
                    //Reset pageload timeout to indefinte time to avoid closing if page load is successful
                    DlkEnvironment.AutoDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(9999999);
                    DlkLogger.LogInfo("Page loaded successfully. Executing login steps ...");
                    isURLSuccessLoad = true;
                }
            }
            catch
            {
                if (!task.IsCompleted)
                {
                    //Close current session if page load exceeds 90 seconds
                    DlkLogger.LogWarning("Page loading exceeded maximum waiting time (90s).");
                    if (!isURLSuccessLoad & !ReOpened)
                    {
                        DlkLogger.LogWarning("Page loading has failed. Retrying to start session ...");
                        DlkEnvironment.StartBrowser();
                        LoadURL(Url, true);
                    }
                    else
                    {
                        DlkEnvironment.CloseSession();
                        throw new Exception("Page loading has failed after retrying current session ...");
                    }
                }
                else
                {
                    //Reset pageload timeout to indefinte time to avoid closing if page load is successful                       
                    DlkEnvironment.AutoDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(9999999);
                    DlkLogger.LogWarning("Page loading reached maximum waiting time (90s).");
                    DlkLogger.LogInfo("Page loaded successfully. Executing login steps ...");
                    isURLSuccessLoad = true;
                }
            }
        }

        private static bool VerifyCookies(string User, string Password)
        {
            bool isCookiesClosed = false;
            if (!String.IsNullOrEmpty(User) || !String.IsNullOrEmpty(Password))
            {
                DlkObjectStoreFileControlRecord btnCookieOkRecords = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Main", "CookiePolicyOkButton");
                DlkButton btnCookieOk = new DlkButton("CookiePolicyOkButton", btnCookieOkRecords.mSearchMethod, btnCookieOkRecords.mSearchParameters);

                if (btnCookieOk.Exists(10))
                {
                    btnCookieOk.Click();
                    isCookiesClosed = true;
                }
            }
            return isCookiesClosed;
        }

        public static void CloseToastMessages()
        {
            if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='toasty']")).Count > 0)
            {
                DlkLogger.LogInfo("Toast text found.");
                IWebElement closeButton = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='toasty']//*[contains(@class,'close')]")).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='toasty']//*[contains(@class,'close')]")) : null;
                if (closeButton != null)
                {
                    DlkLogger.LogInfo("Closing toast text... ");
                    closeButton.Click();
                }
            }
        }

        public static void WaitForLoginPage()
        {
            //Wait for workbook page to load
            int waitPageLoad = 1;
            int waitPageLoadLimit = 61;
            string userNameField_XPath = "//input[@id='username']";

            while (waitPageLoad != waitPageLoadLimit)
            {
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(userNameField_XPath)).Count == 0)
                {
                    DlkLogger.LogInfo("Waiting for login screen to load... [" + waitPageLoad.ToString() + "]s");
                    Thread.Sleep(1000);
                    waitPageLoad++;
                    continue;
                }
                else
                    break;
            }

            if (waitPageLoad == waitPageLoadLimit)
                throw new Exception("Waiting for login screen to load has reached its limit (60s). Setup failed.");

        }

        public static void ResetZoomLevel100()
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    int zoomLevel = 0;
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    zoomLevel = Convert.ToInt32(jse.ExecuteScript("return window.screen.deviceXDPI"));

                    while(zoomLevel != 96)
                    {
                        if (zoomLevel > 96)
                        {
                            //Zoom out
                            var body = DlkEnvironment.AutoDriver.FindElement(By.TagName("html"));
                            body.SendKeys(Keys.Control + Keys.Subtract);
                            DlkLogger.LogInfo("Decreasing zoom level... ");
                        }

                        if (zoomLevel < 96)
                        {
                            //Zoom in
                            var body = DlkEnvironment.AutoDriver.FindElement(By.TagName("html"));
                            body.SendKeys(Keys.Control + Keys.Add);
                            DlkLogger.LogInfo("Increasing zoom level... ");
                        }

                        zoomLevel = Convert.ToInt32(jse.ExecuteScript("return window.screen.deviceXDPI"));
                    }
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        public static void ZoomInBrowserForIE(int zoomTimes)
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    int zoomLevel = 0;
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    zoomLevel = Convert.ToInt32(jse.ExecuteScript("return window.screen.deviceXDPI"));

                    if (zoomLevel <= 96 && zoomLevel < 110)
                    {
                        //Zoom in
                        var body = DlkEnvironment.AutoDriver.FindElement(By.TagName("html"));
                        for (int z = 0; z < zoomTimes; z++)
                        {
                            body.SendKeys(Keys.Control + Keys.Add);
                            DlkLogger.LogInfo("Increasing zoom level... ");
                        }
                    }
                }
            }
            catch(Exception)
            {
                //Do nothing
            }
        }

        public static void ZoomOutBrowserForIE(int zoomTimes)
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    int zoomLevel = 0;
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    zoomLevel = Convert.ToInt32(jse.ExecuteScript("return window.screen.deviceXDPI"));

                    if (zoomLevel >= 144 && zoomLevel < 160)
                    {
                        //Zoom in
                        var body = DlkEnvironment.AutoDriver.FindElement(By.TagName("html"));
                        for (int z = 0; z < zoomTimes; z++)
                        {
                            body.SendKeys(Keys.Control + Keys.Subtract);
                            DlkLogger.LogInfo("Decreasing zoom level... ");
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        public static void GetVersionInfo()
        {
            try
            {
                //Store info in custom info for summary suite results
                if (!DlkEnvironment.CustomInfo.ContainsKey("revision"))
                {
                    //Wait for copyright container to appear
                    string copyrightContainer_XPath = "//small[contains(@class,'copyright')] | //dm-copyright";
                    int wait = 1;
                    int waitLimit = 61;

                    while (wait != waitLimit)
                    {
                        if (DlkEnvironment.AutoDriver.FindElements(By.XPath(copyrightContainer_XPath)).Count == 0)
                        {
                            DlkLogger.LogInfo("Waiting for copyright to appear... [" + wait.ToString() + "]s");
                            Thread.Sleep(1000);
                            wait++;
                            continue;
                        }
                        else
                        {
                            //Retrieve tooltip info
                            string toolTip_XPath = "//div[contains(@class,'tooltip-inner')]";
                            IWebElement copyRightContainer = null;
                            IWebElement toolTip = null;

                            copyRightContainer = DlkEnvironment.AutoDriver.FindElement(By.XPath(copyrightContainer_XPath));
                            Actions actions = new Actions(DlkEnvironment.AutoDriver);

                            actions.MoveToElement(copyRightContainer).Perform();
                            //actions.MoveToElement(copyRightContainer).Click().Build().Perform();
                            toolTip = DlkEnvironment.AutoDriver.FindElements(By.XPath(toolTip_XPath)).Count > 0 ?
                                 DlkEnvironment.AutoDriver.FindElement(By.XPath(toolTip_XPath)) : null;

                            if (toolTip != null) // Old version get info
                            {
                                string tooltipInfo = !String.IsNullOrEmpty(toolTip.Text) ? toolTip.Text.Trim() :
                                    new DlkBaseControl("Tooltip", toolTip).GetValue().Trim();
                                string revision = tooltipInfo.ToString().Replace(":", "").Replace("Revision", "").Trim();
                                DlkEnvironment.CustomInfo.Add("revision", new string[] { "Revision", revision });

                                string[] infos = revision.Split('-');
                                if (infos.Length >= 2)
                                {
                                    string version = infos[0].ToString().Replace(":", "").Replace("Revision","").Trim();
                                    string build = infos[2].ToString();
                                    DlkEnvironment.CustomInfo.Add("version", new string[] { "Version", version });
                                    DlkEnvironment.CustomInfo.Add("build", new string[] { "Build", build });
                                }
                                else
                                {
                                    DlkLogger.LogInfo("Version info is not in proper format [revision: version- -build]. No version info retrieved.");
                                }
                            }
                            else  // New version get info
                            {
                                DlkLogger.LogInfo("Tooltip info not found. No version info retrieved for old version.");

                                string copyrightInner_XPath = "//dm-copyright//small";
                                IWebElement revisionInfo = DlkEnvironment.AutoDriver.FindElements(By.XPath(copyrightInner_XPath)).Count > 0 ?
                                 DlkEnvironment.AutoDriver.FindElement(By.XPath(copyrightInner_XPath)) : null;
                                string revisionValue = !String.IsNullOrEmpty(revisionInfo.Text) ? revisionInfo.Text.Trim() :
                                    new DlkBaseControl("Tooltip", revisionInfo).GetValue().Trim();

                                if (revisionInfo != null)
                                {
                                    string revision = revisionValue.ToString().Replace(":", "").Replace("Revision", "").Trim();
                                    DlkEnvironment.CustomInfo.Add("revision", new string[] { "Revision", revision });

                                    string[] infos = revision.Split('-');
                                    string version = infos.FirstOrDefault().ToString().Replace(":", "").Replace("Revision", "").Trim();
                                    string build = infos.LastOrDefault().ToString();
                                    DlkEnvironment.CustomInfo.Add("version", new string[] { "Version", version });
                                    DlkEnvironment.CustomInfo.Add("build", new string[] { "Build", build });
                                }
                                else
                                {
                                    DlkLogger.LogInfo("Revision info not found. No version info retrieved for new version.");
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch
            {
                DlkLogger.LogInfo("Something went wrong while retrieving version info. No version info retrieved.");
            }
        }
       
        public static void GetAdditionalVersionInfo()
        {
            string settingsIcon_XPath = "//a[contains(@class,'settings')]";
            string aboutInfoList_XPath = "//dm-about//ul[contains(@class,'list-unstyled')]";
            try
            {
                if (!DlkEnvironment.CustomInfo.ContainsKey("tpu") || !DlkEnvironment.CustomInfo.ContainsKey("apu"))
                {
                    //Open settings and about dialog
                    int settingsWait = 1;
                    int settingsWaitLimit = 121;
                    while (settingsWait != settingsWaitLimit)
                    {
                        if (DlkEnvironment.AutoDriver.FindElements(By.XPath(settingsIcon_XPath)).Count > 0)
                        {
                            IWebElement settings = DlkEnvironment.AutoDriver.FindElement(By.XPath(settingsIcon_XPath));
                            settings.Click();
                            DlkMaconomyiAccessKeywordHandler.ExecuteKeyword("Main", "SettingsList", "Select", new String[] { "About iAccess for Maconomy" });
                            break;
                        }
                        else
                        {
                            settingsWait++;
                            continue;
                        }
                    }

                    if (settingsWait == settingsWaitLimit)
                        throw new Exception("Waiting time for settings icon to appear has been reached. Skipping getting additional info.");

                    int wait = 1;
                    int waitLimit = 121;

                    while (wait != waitLimit)
                    {
                        if (DlkEnvironment.AutoDriver.FindElements(By.XPath(aboutInfoList_XPath)).Count == 0)
                        {
                            DlkLogger.LogInfo("Waiting for about info list to appear... [" + wait.ToString() + "]s");
                            Thread.Sleep(1000);
                            wait++;
                            continue;
                        }
                        else if (!DlkEnvironment.AutoDriver.FindElement(By.XPath(aboutInfoList_XPath)).Displayed)
                        {
                            DlkLogger.LogInfo("Waiting for about info list to appear... [" + wait.ToString() + "]s");
                            Thread.Sleep(1000);
                            wait++;
                            continue;
                        }
                        else
                        {
                            //Retrieve TPU and APU info
                            IWebElement aboutInfoList = DlkEnvironment.AutoDriver.FindElement(By.XPath(aboutInfoList_XPath));
                            IList<IWebElement> aboutListItems = aboutInfoList.FindElements(By.TagName("li")).Count > 0 ?
                                aboutInfoList.FindElements(By.TagName("li")).Where(x => x.Displayed).ToList() : null;

                            if(aboutListItems != null)
                            {
                                IWebElement TPUAPU_Element = aboutListItems.ElementAt(1);
                                string tpuapu = !String.IsNullOrEmpty(TPUAPU_Element.Text) ? TPUAPU_Element.Text.Trim() :
                                    new DlkBaseControl("TPU and APU", TPUAPU_Element).GetValue().Trim();

                                string[] tpuapuItems = tpuapu.Split('|');
                                string TPU = tpuapuItems[0].Replace(":","").Replace("TPU", "").Trim();
                                string APU = tpuapuItems[1].Replace(":", "").Replace("APU", "").Trim();

                                DlkEnvironment.CustomInfo.Add("tpu", new string[] { "TPU", TPU });
                                DlkEnvironment.CustomInfo.Add("apu", new string[] { "APU", APU });
                            }
                            else
                            {
                                DlkLogger.LogInfo("About info list items not found. No additional version info retrieved.");
                            }
                            break;
                        }
                    }

                    //Close about dialog
                    DlkEnvironment.AutoDriver.SwitchTo().ActiveElement().SendKeys(Keys.Escape);
                    Thread.Sleep(1000);

                    if (DlkEnvironment.AutoDriver.FindElement(By.XPath(aboutInfoList_XPath)).Displayed)
                    {
                        Thread.Sleep(1000);
                        DlkLogger.LogInfo("Sending escape keys did not close about dialog. Trying close button instead.. ");
                        string buttonClose_XPath = "//dm-about//button[@class='close']";
                        DlkEnvironment.AutoDriver.FindElement(By.XPath(buttonClose_XPath)).Click();
                    }
                    
                    if (wait == waitLimit)
                        DlkLogger.LogInfo("Waiting time for about info list to appear has been reached. Skipping getting additional info.");
                }
            }
            catch
            {
                DlkLogger.LogInfo("Something went wrong while retrieving additional version info. No additional version info retrieved.");
                
                DlkEnvironment.AutoDriver.SwitchTo().ActiveElement().SendKeys(Keys.Escape);
                Thread.Sleep(1000);

                if (DlkEnvironment.AutoDriver.FindElement(By.XPath(aboutInfoList_XPath)).Displayed)
                {
                    Thread.Sleep(1000);
                    DlkLogger.LogInfo("Sending escape keys did not close about dialog. Trying close button instead.. ");
                    string buttonClose_XPath = "//dm-about//button[@class='close']";
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(buttonClose_XPath)).Click();
                }
            }
        }

        public static void GetMobileInfo()
        {
            try
            {
                if (!DlkEnvironment.CustomInfo.ContainsKey("mobileid"))
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
            }
            catch
            {
                DlkLogger.LogInfo("Something went wrong while retrieving mobile info. No mobile info retrieved.");
            }
        }

        public static void GetRemoteInfo()
        {
            try
            {
                if (!DlkEnvironment.CustomInfo.ContainsKey("remoteid"))
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
            }
            catch
            {
                DlkLogger.LogInfo("Something went wrong while retrieving remote info. No remote info retrieved.");
            }
        }

        public static void GetPlatformInfo()
        {
            try
            {
                if (!DlkEnvironment.CustomInfo.ContainsKey("platform"))
                {
                    DlkMobileRecord mobileDevice = DlkMobileHandler.GetRecord(DlkEnvironment.mBrowserID);

                    if (mobileDevice != null)
                    {
                        //Get platform info from mobile environment
                        DlkEnvironment.CustomInfo.Add("platform", new string[] { "Platform", mobileDevice.MobileType });
                    }
                    else
                    {
                        //Get platform info from current browser
                        IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        string platform = js.ExecuteScript("return window.navigator.platform").ToString();
                        DlkEnvironment.CustomInfo.Add("platform", new string[] { "Platform", platform });
                    }
                }
            }
            catch
            {
                DlkLogger.LogInfo("Something went wrong while retrieving platform info. No platform info retrieved.");
            }
        }
    }
}
