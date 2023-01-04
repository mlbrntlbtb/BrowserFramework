using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using StormWebLib.System;
using StormWebLib.DlkControls;
using StormWebLib.DlkFunctions;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using CommonLib.DlkControls;
using System.Diagnostics;
using CommonLib.DlkUtility;

namespace StormWebLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]    
    public static class DlkStormWebFunctionHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

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
                    case "GetUrl":
                        GetUrl(Parameters[0]);
                        break;
                    case "GoToUrl":
                        GoToUrl(Parameters[0]);
                        break;
                    case "CurrentDateTime":
                        CurrentDateTime(Parameters[0]);
                        break; 
                    case "VerifyPageOpens":
                        VerifyPageOpens(Parameters[0]);
                        break;
                    case "NavigateTo":
                        NavigateTo(Parameters[0]);
                        break;
                    case "WaitForSaving":
                        WaitForSaving();
                        break;
                    case "PerformMathOperationWithCustomDecimal":
                        PerformMathOperationWithCustomDecimal(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4], Parameters[5]);
                        break;
                    case "ConvertToProperNumberFormat":
                        ConvertToProperNumberFormat(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "VerifyPrintPreviewPage":
                        VerifyPrintPreviewPage(Parameters[0]);
                        break;
                    case "SwitchFocusToNewTab":
                        SwitchFocusToNewTab();
                        break;
                    case "MaximizeNewWindow":
                        MaximizeNewWindow();
                        break;
                    case "SwitchFocusToPreviousTab":
                        SwitchFocusToPreviousTab();
                        break;
                    case "VerifyURL":
                        VerifyURL(Parameters[0]);
                        break;
                    case "VerifyURLContains":
                        VerifyURLContains(Parameters[0]);
                        break;
                    case "VerifyTabTitle":
                        VerifyTabTitle(Parameters[0]);
                        break;
                    case "VerifyCursorBusyState":
                        VerifyCursorBusyState(Parameters[0]);
                        break;
                    case "WaitForCursorReadyState":
                        WaitForCursorReadyState();
                        break;
                    case "WaitScreenGetsReady":
                        WaitScreenGetsReady();
                        break;
                    case "CloseChromeTab":
                        CloseChromeTab();
                        break;
                    case "RefreshPage":
                        RefreshPage();
                        break;
                    case "GetMachineNameAndAssignDB":
                        GetMachineNameAndAssignDB(Parameters[0]);
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
                    case "Dialog":
                        switch(Keyword)
                        {
                            case "FileDownload":
                                DlkDialog.FileDownload(Parameters[0], Parameters[1]);
                                break;
                            case "FileUpload":
                                DlkDialog.FileUpload(Parameters[0], Parameters[1]);
                                break;
                            case "MultipleFileUpload":
                                DlkDialog.MultipleFileUpload(Parameters[0], Parameters[1]);
                                break;
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);                                
                        }
                        break;
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }

        /// <summary>
        /// Closes the newly opened tab.
        /// </summary>
        /// <param name="URL"></param>
        [Keyword("CloseChromeTab")]
        public static void CloseChromeTab()
        {
            try
            {
                // switch focus to newly opened tab. (tab count = 2)
                SwitchFocusToNewTab ();
                Thread.Sleep(1000);
                // close the second tab so only 1 tab will be left open
                DlkEnvironment.AutoDriver.Close();
                // switch focus to the remaining tab
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            catch (Exception ex)
            {
                throw new Exception("CloseChromeTab() failed: " + ex.Message);
            }
        }

        [Keyword("GoToUrl")]
        public static void GoToUrl(string URL)
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().GoToUrl(URL);
            }
            catch (Exception ex)
            {
                throw new Exception("GoToUrl() failed: " + ex.Message);
            }
        }

        [Keyword("GetUrl")]
        public static void GetUrl(string VariableName)
        {
            try
            {
                DlkVariable.SetVariable(VariableName, DlkEnvironment.AutoDriver.Url);
                DlkLogger.LogInfo("GetUrl() URL:" + DlkEnvironment.AutoDriver.Url + " assigned.");
            }
            catch (Exception ex)
            {
                throw new Exception("GetUrl() failed: " + ex.Message);
            }
        }

        [Keyword("WaitScreenGetsReady")]
        public static void WaitScreenGetsReady()
        {
            try
            {
                IWebElement basePage;
                var pageDialogs = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@role='dialog']")).Where(x => x.Displayed).ToList();
                if (pageDialogs.Count > 0)
                {
                    basePage = pageDialogs.Last();
                }
                else
                {
                    basePage = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*"));
                }

                long ctr = 0;
                //all spinners excluding the following due to investigation results:
                // - spinnercontainer - It is always existing in timesheet control
                // - spinner-area - It is always existing in Activity Reminder Dialog. [JAM: 5/29/17]
                // - grid-spinner - always displayed since it is inheriting display:block from parent causing performance issues. Observe if this hotfix will not break anything
                // - added has-spinner, spinner-control, spinner-up, spinner-down classes due to Reporting screen
                var spinners = basePage.FindElements(By.XPath(".//*[contains(@class,'spinner')][not(contains(@class,'spinnercontainer'))][not(contains(@class,'spinnerContainer'))][not(contains(@class,'spinner-area'))][not(contains(@class,'grid-spinner'))][not(contains(@class,'has-spinner'))][not(contains(@class,'spinner-control'))][not(contains(@class,'spinner-up'))][not(contains(@class,'spinner-down'))]"));
                var blockers = basePage.FindElements(By.XPath(".//*[contains(@id,'application-blocker')]"));
                var mainPageLoading = basePage.FindElements(By.XPath(".//*[contains(@class,'src-core-workbench-navigation-menu')]/ancestor::*[contains(@class,'ngcrm en-US')]/*[@id='applicationLoadingOverlay']"));
                var reportViewerSpinner = basePage.FindElements(By.XPath(".//*[@id='sqlrsReportViewer_AsyncWait_Wait']"));

                Action<IReadOnlyCollection<IWebElement>> CheckSpinners = (collection) =>
                {
                    ctr = 0;
                    int retryLimit = 3, retry = 0, maxWaitTime = 180000;

                    while (retry < retryLimit)
                    {
                        retry++;
                        Stopwatch mWatch = Stopwatch.StartNew();
                        mWatch.Start();

                        while ((mWatch.ElapsedMilliseconds < maxWaitTime/retryLimit) && (collection.Count(x => x.Displayed == true) > 0))
                        {
                            DlkLogger.LogInfo("Waiting for page to load completely...");
                            Thread.Sleep(1000);
                        }
                        ctr += mWatch.ElapsedMilliseconds;
                        mWatch.Stop();

                        //if anything is still displayed, retry after 1 second...
                        if (collection.Count(x => x.Displayed == true) > 0)
                        {
                            DlkLogger.LogInfo("It's been " + (ctr / 1000).ToString() + " seconds.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            retry = 3;
                        }
                    }

                    if (collection.Count(x => x.Displayed == true) > 0)
                    {
                        DlkLogger.LogInfo("Page not loaded within 180 seconds. Proceeding...");
                    }
                };
                
                List<IWebElement> itemsForWait = new List<IWebElement>();
                itemsForWait.AddRange(mainPageLoading);
                itemsForWait.AddRange(spinners);
                itemsForWait.AddRange(blockers);
                itemsForWait.AddRange(reportViewerSpinner);

                DlkLogger.LogInfo("Checking for spinners, blockers and page loading...");
                CheckSpinners(itemsForWait);
                DlkLogger.LogInfo("Slept for " + (ctr / 1000).ToString() + " seconds");

            }
            catch (StaleElementReferenceException)
            {
                //do nothing for now, page is expected to refresh so Stale Element Reference Exception is expected.
            }
            catch (Exception ex)
            {
                throw new Exception("WaitScreenGetsReady() failed : " + ex.Message);
            }
        }

        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
            VariableValue = VariableValue.ToLower();
            ValueToTest = ValueToTest.ToLower();
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

                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case >
                case ">":

                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                       // if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                      //  {
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
                      //  }
                      //  else
                      //  {
                       //     throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                      //  }
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
                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case <
                case "<":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                       // if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                       // {
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
                       // }
                       // else
                      //  {
                      //      throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                      //  }
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
                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case >=
                case ">=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                       // if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                      //  {
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
                      //  }
                    //    else
                     //   {
                     //       throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                     //   }
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
                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region contains
                case "contains":
                    if (VariableValue.Contains(ValueToTest))
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    
                    DlkStormWebTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("PerformMathOperationWithCustomDecimal", new String[] { @"1|text|Comment|This is a comment" })]
        public static void PerformMathOperationWithCustomDecimal(String FirstOperand, String SecondOperand, String Operator, String VariableName, String DecimalPlaces, String IsDecimalComma)
        {
            try
            {
                Decimal res = 0;
                Decimal num1 = 0;
                Decimal num2 = 0;

                if(Convert.ToBoolean(IsDecimalComma))
                {
                    if (FirstOperand.Contains(".") || SecondOperand.Contains(".")) //for number formats like 1.000,01.
                    {
                        FirstOperand = FirstOperand.Replace(".","");
                        SecondOperand = SecondOperand.Replace(".", "");
                    }
                    FirstOperand = FirstOperand.Replace(",",".");
                    SecondOperand = SecondOperand.Replace(",", ".");
                }

                if (!Decimal.TryParse(FirstOperand, out num1))
                {
                    throw new Exception("PerformMathOperationWithCustomDecimal(): Invalid Input1 Parameter: " + FirstOperand);
                }
                if (!Decimal.TryParse(SecondOperand, out num2))
                {
                    throw new Exception("PerformMathOperationWithCustomDecimal(): Invalid Input2 Parameter: " + SecondOperand);
                }

                switch (Operator)
                {
                    case "+":
                        DlkLogger.LogInfo("PerformMathOperationWithCustomDecimal(). Operation:[" + FirstOperand + "] + [" + SecondOperand + "].");
                        res = num1 + num2;
                        break;
                    case "-":
                        DlkLogger.LogInfo("PerformMathOperationWithCustomDecimal(). Operation:[" + FirstOperand + "] - [" + SecondOperand + "].");
                        res = num1 - num2;
                        break;
                    case "*":
                        DlkLogger.LogInfo("PerformMathOperationWithCustomDecimal(). Operation:[" + FirstOperand + "] * [" + SecondOperand + "].");
                        res = num1 * num2;
                        break;
                    case "/":
                        DlkLogger.LogInfo("PerformMathOperationWithCustomDecimal(). Operation:[" + FirstOperand + "] / [" + SecondOperand + "].");
                        res = num1 / num2;
                        break;
                    default:
                        throw new Exception("PerformMathOperationWithCustomDecimal(): Unsupported operator " + Operator);
                }

                int numDecimalPlaces = int.Parse(DecimalPlaces);

                decimal resDec = decimal.Round(res, numDecimalPlaces, MidpointRounding.AwayFromZero);
                String resString = resDec.ToString("F" + numDecimalPlaces, CultureInfo.InvariantCulture);

                if (Convert.ToBoolean(IsDecimalComma))
                {
                    //es-ES is used to format number into swapping decimal points with comma (i.e., 1,100.00 -> 1.100,00)
                    resString = resDec.ToString("F" + numDecimalPlaces, CultureInfo.CreateSpecificCulture("es-ES"));
                }

                DlkVariable.SetVariable(VariableName, resString);
                DlkLogger.LogInfo("Successfully executed PerformMathOperationWithCustomDecimal(). Variable:[" + VariableName + "], Value:[" + resString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMathOperationWithCustomDecimal() failed: " + ex.Message);
            }
        }

        [Keyword("ConvertToProperNumberFormat", new String[] { @"1|text|Comment|This is a comment" })]
        public static void ConvertToProperNumberFormat(String VariableName, String Number, String numDecimalPlaces, String IsDecimalComma)
        {
            try
            {
                Decimal num = 0;
                Boolean isComma = false;
                int decPlaceNum = 0;

                if (!Boolean.TryParse(IsDecimalComma, out isComma))
                {
                    throw new Exception("ConvertToProperNumberFormat(): Invalid IsDecimalComma Parameter: " + IsDecimalComma);
                }

                if (!Int32.TryParse(numDecimalPlaces, out decPlaceNum))
                {
                    throw new Exception("ConvertToProperNumberFormat(): Invalid numDecimalPlaces Parameter: " + numDecimalPlaces);
                }

                if (!Decimal.TryParse(Number, out num))
                {
                    throw new Exception("ConvertToProperNumberFormat(): Invalid Number Parameter: " + Number);
                }

                // Result is 1,234.00. Decimal places are customizable.
                String numString = num.ToString("N" + numDecimalPlaces, CultureInfo.CreateSpecificCulture("en-US"));
                
                if (isComma)
                {
                    //es-ES is used to format number into swapping decimal points with comma (i.e., 1,100.00 -> 1.100,00)
                    numString = num.ToString("N" + numDecimalPlaces, CultureInfo.CreateSpecificCulture("es-ES"));
                }

                DlkVariable.SetVariable(VariableName, numString);
                DlkLogger.LogInfo("Successfully executed ConvertToProperNumberFormat(). Variable:[" + VariableName + "], Value:[" + numString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("ConvertToProperNumberFormat() failed: " + ex.Message);
            }
        }
      
        //[Keyword("NavigateTo")]      
        //public static void NavigateTo(String ProductName)
        //{
        //    DlkLoginConfigHandler mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
           
        //    switch (ProductName.ToLower())
        //    {
        //        case "crm":
        //            DlkStormWebFunctionHandler.ExecuteFunction("Login", "Function", "Login",
        //            new String[] { "http://ashapt15vs/CRM", mLoginConfigHandler.mUser, 
        //            mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
        //            break;
        //        case "navigator_crm":
        //            DlkStormWebFunctionHandler.ExecuteFunction("Login", "Function", "Login",
        //            new String[] { "http://ashapt15vs/Navigator", mLoginConfigHandler.mUser, 
        //            mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
        //            DlkStormWebKeywordHandler.ExecuteKeyword("Navigator_MainHeader", "WorkspaceSwitcher", "Click", new String[] { "" });
        //            DlkStormWebKeywordHandler.ExecuteKeyword("Navigator_MainHeader", "Dropdown_ConextMenu", "Select", new String[] { "Business Development" });
        //            break;
        //        case "navigator":
        //            DlkStormWebFunctionHandler.ExecuteFunction("Login", "Function", "Login",
        //            new String[] { "http://ashapt15vs/Navigator", mLoginConfigHandler.mUser, 
        //            mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
        //            break;
        //        default:
        //            throw new Exception("Unsupported product: " + ProductName); // not going to spend time on this... likely never happen as we will control via front end;
        //    }
        //}

        [Keyword("NavigateTo")]
        public static void NavigateTo(String ProductName)
        {
            switch (ProductName.ToLower())
            {
                case "crm":
                    // detect if currently in crm if not, do the neccesary steps
                    List<IWebElement> elems = DlkEnvironment.AutoDriver.FindElements(By.Id("workspace-container")).ToList();
                    if (elems.Count == 0)
                    {
                        throw new Exception("Cannot navigate to '" + ProductName + "' because workspace container was not found.");
                    }
                    if (!elems.First().GetAttribute("class").Contains("crm"))
                    {
                        DlkLogger.LogInfo("Navigating to '" + ProductName + "'");
                        DlkImage switcher = new DlkImage("WorkspaceSwitcher", "ID", "frame-title");
                        switcher.Click();
                        DlkMenu items = new DlkMenu("ConextMenu", "ID", "workspace-switcher");
                        items.Select("Business Development");
                    }
                    else
                    {
                        DlkLogger.LogInfo("Already in '" + ProductName + "'. No need to navigate.");
                    }
                    break;
                default:
                    throw new Exception("Unsupported product: " + ProductName); // not going to spend time on this... likely never happen as we will control via front end;
            }
        }

        [Keyword("VerifyPageOpens")]
        public static void VerifyPageOpens(String PageTitle)
        {
            
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            Thread.Sleep(800);
            try
            {

                IWebElement mWindowElement = DlkEnvironment.AutoDriver.FindElement(By.ClassName("page-title"));
                DlkAssert.AssertEqual("Window Title Check", PageTitle, mWindowElement.GetAttribute("textContent"));
            }
            catch
            {
                //if page-title element doesn't exist, use this instead...
                IWebElement mWindowElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//title[1]"));
                DlkAssert.AssertEqual("Window Title Check", PageTitle, (new DlkBaseControl("Window Title",mWindowElement)).GetValue());
            }
            DlkEnvironment.AutoDriver.Close();
            Thread.Sleep(500);
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
        }

        [Keyword("VerifyURL")]
        public static void VerifyURL(String URL)
        {                 
                DlkAssert.AssertEqual("Window URL Check", URL, DlkEnvironment.AutoDriver.Url);           
        }

        [Keyword("VerifyURLContains")]
        public static void VerifyURLContains(String Value)
        {
            DlkAssert.AssertEqual("Window URL Check Contains", true, DlkEnvironment.AutoDriver.Url.Contains(Value));
        }

        [Keyword("VerifyTabTitle")]
        public static void VerifyTabTitle(String TabTitle)
        {
            DlkAssert.AssertEqual("Window TabTitle Check", TabTitle, DlkEnvironment.AutoDriver.Title);
        }

        [Keyword("VerifyPrintPreviewPage")]
        public static void VerifyPrintPreviewPage(String PageTitle)
        {

            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            Thread.Sleep(800);
            DlkAssert.AssertEqual("VerifyPrintPreviewPage", true, DlkEnvironment.AutoDriver.Title.Contains(PageTitle));          
            DlkEnvironment.AutoDriver.Close();
            Thread.Sleep(500);
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
        }

        [Keyword("SwitchFocusToNewTab")]
        public static void SwitchFocusToNewTab()
        {
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.SendKeys(Keys.Control + Keys.Tab).KeyUp(Keys.Control).Build().Perform();
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);   
            Thread.Sleep(800);           
        }

        [Keyword("MaximizeNewWindow")]
        public static void MaximizeNewWindow()
        {
            //AJM: For IE wherein new tab opens as new window instead by default.
            DlkEnvironment.AutoDriver.Manage().Window.Maximize();
            Thread.Sleep(800);
        }

        [Keyword("SwitchFocusToPreviousTab")]
        public static void SwitchFocusToPreviousTab()
        {
            string previousWindow = "";
            foreach (String winhandle in DlkEnvironment.AutoDriver.WindowHandles)
            {
                if (winhandle == DlkEnvironment.AutoDriver.CurrentWindowHandle)
                {
                    previousWindow = DlkEnvironment.AutoDriver.WindowHandles[(DlkEnvironment.AutoDriver.WindowHandles.IndexOf(winhandle))-1];
                }
            }
            /* To show tab switch to user */
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.SendKeys(Keys.Control + Keys.Shift + Keys.Tab).KeyUp(Keys.Shift).KeyUp(Keys.Control).Build().Perform();
            DlkEnvironment.AutoDriver.SwitchTo().Window(previousWindow);
            Thread.Sleep(800);
            DlkLogger.LogInfo("Successfully executed SwitchFocusToPreviousTab()");

        }

        [Keyword("WaitForSaving")]
        public static void WaitForSaving()
        {
            IWebElement mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='header-subtitle']/div[@class='progress-area general-progress-area']"));          
               
            for (int i = 1; i <= 5; i++)
                {
                    if (mElement.GetAttribute("style") == "display: block;")
                    {
                        DlkLogger.LogInfo("Spinner is still spinning.");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        DlkLogger.LogInfo("Spinner has completed.");
                        break;
                    }
                
            }
            
            
        }

        [Keyword("VerifyCursorBusyState")]
        public static void VerifyCursorBusyState(String TrueOrFalse)
        {
            try
            {
                string cursorXpath = "//*[@id='application-blocker']";
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(cursorXpath)).Count() > 0)
                {
                    IWebElement mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(cursorXpath));
                    string cursorStatus = mElement.GetCssValue("cursor");
                    bool ActualValue;
                    if (cursorStatus.ToLower() == "wait")
                        ActualValue = true;
                    else
                        ActualValue = false;

                    bool ExpectedValue = false;
                    if (Boolean.TryParse(TrueOrFalse, out ExpectedValue))
                    {
                        DlkAssert.AssertEqual("VerifyCursorBusyState() : ", ExpectedValue, ActualValue);
                        DlkLogger.LogInfo("VerifyCursorBusyState() completed.");
                    }
                    else
                    {
                        throw new Exception("VerifyCursorBusyState() : Invalid input [" + TrueOrFalse + "]");
                    }                    
                }
                else
                {
                    throw new Exception("VerifyCursorBusyState() : Unable to find cursor style.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCursorBusyState() failed.", ex);
            }
        }

        [Keyword("WaitForCursorReadyState")]
        public static void WaitForCursorReadyState()
        {
            try
            {
                string cursorXpath = "//*[@id='application-blocker']";
                bool wait = false;
                long elapsed = 0;

                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(cursorXpath)).Count() > 0)
                {
                    Stopwatch mWatch = Stopwatch.StartNew();
                    mWatch.Start();
                    do
                    {
                        IWebElement mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(cursorXpath));
                        string cursorStatus = mElement.GetCssValue("cursor");

                        if (cursorStatus.ToLower() == "wait")
                            wait = true;
                        else
                            wait = false;

                        elapsed = mWatch.ElapsedMilliseconds;

                    } while (wait && elapsed < 30000);

                    if (elapsed < 30000)
                    {
                        DlkLogger.LogInfo("WaitForCursorReadyState() : Cursor is now on ready state. Elapsed : " + (elapsed.ToString()) + " milliseconds");
                    }
                    else
                    {
                        throw new Exception("WaitForCursorReadyState() : Cursor exceeded the limit of 30 seconds waiting time.");
                    }
                }
                else
                {
                    throw new Exception("WaitForCursorReadyState() : Unable to find cursor style.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("WaitForCursorReadyState() failed.", ex);
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
            DlkEnvironment.AutoDriver.Url = Url;

            //Additional wait time before logging in while Storm server has yet to stabilize
            int waitLoadingFinished = 300;
            DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "Login");
            DlkButton loginBtn = new DlkButton(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
            for (int i = 0; i < waitLoadingFinished; i++)
            {
                if (!loginBtn.Exists())
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    DlkLogger.LogInfo(string.Format("Login button was found, page loading was finished in {0} seconds", i.ToString()));
                    break;
                }
            }
            // use the object store definitions 
            if(User != "")
            {
                DlkStormWebKeywordHandler.ExecuteKeyword("Login", "UserID", "Set", new String[] { User });
                DlkStormWebKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                DlkStormWebKeywordHandler.ExecuteKeyword("Login", "Database", "Select", new String[] { Database });
                Thread.Sleep(250);
                DlkStormWebKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });

                //Wait for screen to load before checking
                WaitScreenGetsReady();

                //Wait for Navigation to appear (denotes that the main page has loaded)
                DlkObjectStoreFileControlRecord osRec = DlkDynamicObjectStoreHandler.GetControlRecord("Main", "Navigation");
                DlkSideBar sideBar = new DlkSideBar(osRec.mKey, osRec.mSearchMethod, osRec.mSearchParameters);
                if (!sideBar.Exists(60))
                {
                    DlkLogger.LogWarning("Login() : Main page not loaded within timeout.");
                }
                else
                {
                    Thread.Sleep(8000);
                    DlkLogger.LogInfo("Login() passed.");
                }

            }
        }

        [Keyword("CurrentDateTime")]
        public static void CurrentDateTime(String VariableName)
        {
            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());

            string dtValue = DlkString.GetDateAsText(dtNow,"file");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo("Successfully executed CurrentDateTime(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
        }

        [Keyword("RefreshPage")]
        public static void RefreshPage()
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().Refresh();
                DlkLogger.LogInfo("Successfully executed RefreshPage().");
            }
           catch(Exception e)
            {
                throw new Exception("RefreshPage() failed : " + e.Message);
            }
        }

        // Exclusive for Storm. Gets machine name and assigns a database into variable for auto-restore feature.
        [Keyword("GetMachineNameAndAssignDB")]
        public static void GetMachineNameAndAssignDB(String VariableName)
        {
            try
            {
                string machineName = Environment.MachineName;
                string vmconfigfile = Path.Combine(DlkEnvironment.mDirConfigs, "vmconfig.xml");

                //Find if machineName has a matching database in the vmconfig xml
                var vmlist = XDocument.Load(vmconfigfile);
                string dbName = vmlist.Root.Elements()
                .Where(a => (string)a.Attribute("name") == machineName.ToUpper())
                .Select(a => (string)a.Attribute("database"))
                .FirstOrDefault();

                if (String.IsNullOrEmpty(dbName))
                {
                    throw new Exception("Unidentified machine: " + machineName + ". Kindly contact your team lead to register this machine.");
                }
                DlkVariable.SetVariable(VariableName, dbName);
                DlkLogger.LogInfo(String.Format("GetMachineNameAndAssignDB() executed successfully. Assigned {0} to {1}", dbName, VariableName));
            }
            catch (Exception e)
            {
                throw new Exception("GetMachineNameAndAssignDB() failed : " + e.Message);
            }
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

    }
}