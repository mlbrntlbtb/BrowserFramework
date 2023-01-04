using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CostpointLib.DlkFunctions;
using OpenQA.Selenium;
using System.Diagnostics;

namespace CostpointLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]    
    public static class DlkCostpointFunctionHandler
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
                    case "PerformMathOperation":
                        PerformMathOperation(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "PerformMathOperationUnrounded":
                        PerformMathOperationUnrounded(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "FormatAmount":
                        FormatAmount(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "SwitchToWindow":
                        SwitchToWindow(Convert.ToInt16(Parameters[0]));
                        break;
                    case "VerifyWindowTitle":
                        VerifyWindowTitle(Parameters[0]);
                        break;
                    case "OpenNewBrowserTab":
                        OpenNewBrowserTab();
                        break;
                    case "SwitchFocusToNewBrowserTab":
                        SwitchFocusToNewBrowserTab();
                        break;
                    case "SwitchFocusToPreviousBrowserTab":
                        SwitchFocusToPreviousBrowserTab();
                        break;
                    case "GoBackInBrowser":
                        GoBackInBrowser();
                        break;
                    case "GetCurrentUrl":
                        GetCurrentUrl(Parameters[0]);
                        break;
                    case "GoToUrl":
                        GoToUrl(Parameters[0]);
                        break;
                    case "ClearCache":
                        ClearCache();
                        break;
                    case "EnableOfflineModeInBrowser":
                        EnableOfflineModeInBrowser();
                        break;
                    case "GetCurrentBrowser":
                        GetCurrentBrowser(Parameters[0]);
                        break;
                    case "EnableOnlineModeInBrowser":
                        EnableOnlineModeInBrowser();
                        break;
                    case "TextToSpeech":
                        TextToSpeech(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "DisconnectInternetAccess":
                        DisconnectInternetAccess();
                        break;
                    case "ConnectToInternet":
                        ConnectToInternet();
                        break;
                    case "PercentageValueToString":
                        PercentageValueToString(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "VerifyAutoCompleteValue":
                        VerifyAutoCompleteValue(Parameters[0]);
                        break;
                    case "GetAutoCompleteValue":
                        GetAutoCompleteValue(Parameters[0], Parameters[1]);
                        break;
                    case "SelectAutoCompleteValue":
                        SelectAutoCompleteValue(Parameters[0]);
                        break;
                    case "RoundHours":
                        RoundHours(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
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
                    case "CP7Login":
                        switch (Keyword)
                        {
                            case "Login":
                                DlkCP7Login.Login(Parameters[0], Parameters[1], Parameters[2]);
                                break;
                            case "TestLoginConnection":
                                DlkCP7Login.TestLoginConnection(Parameters[0], Parameters[1], Parameters[2]);
                                break;
                            case "VerifyLoginErrorMessage":
                                DlkCP7Login.VerifyLoginErrorMessage(Parameters[0]);
                                break;
                            case "VerifyPartialLoginErrorMessage":
                                DlkCP7Login.VerifyPartialLoginErrorMessage(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Query":
                        switch(Keyword)
                        {
                            case "SetSearchCriterion":
                                DlkQuery.SetSearchCriterion(Parameters[0], Parameters[1], Parameters[2]);
                                break;
                            case "AddQueryCondition":
                                DlkQuery.AddQueryCondition(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                break;
                            case "VerifyTitle":
                                DlkQuery.VerifyTitle(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Dialog":
                        switch (Keyword)
                        {
                            case "ClickOkDialogWithMessage":
                                DlkDialog.ClickOkDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickCancelDialogWithMessage":
                                DlkDialog.ClickCancelDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickOkDialogIfExists":
                                DlkDialog.ClickOkDialogIfExists(Parameters[0]);
                                break;
                            case "ClickOkDialogWithMessagePart":
                                DlkDialog.ClickOkDialogWithMessagePart(Parameters[0]);
                                break;
                            case "FileDownload":
                                DlkDialog.FileDownload(Parameters[0], Parameters[1]);
                                break;
                            case "FileUpload":
                                DlkDialog.FileUpload(Parameters[0], Parameters[1]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "ProcessProgress":
                        switch(Keyword)
                        {
                            case "WaitForProcessFinished":
                                DlkProcessProgress.WaitForProcessFinished(Parameters[0]);
                                break;
                            case "WaitForProcessDialogClosed":
                                DlkProcessProgress.WaitForProcessDialogClosed(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "DatePicker":
                        switch (Keyword)
                        {
                            case "ClickDay":
                                DlkDatePicker.ClickDay(Parameters[0]);
                                break;
                            case "VerifySelectedDay":
                                DlkDatePicker.VerifySelectedDay(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "AutoComplete":
                        switch (Keyword)
                        {
                            case "Select":
                                DlkAutoComplete.Select(Parameters[0]);
                                break;
                            case "VerifyAvailableInList":
                                DlkAutoComplete.VerifyAvailableInList(Parameters[0], Parameters[1]);
                                break;
                            case "VerifyExists":
                                DlkAutoComplete.VerifyExists(Parameters[0]);
                                break;
                            case "VerifyList":
                                DlkAutoComplete.VerifyList(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "BrowseFilePopup":
                        switch (Keyword)
                        {
                            case "ClickBrowse":
                                DlkBrowseFilePopup.ClickBrowse();
                                break;
                            case "VerifyFilename":
                                DlkBrowseFilePopup.VerifyFilename(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "ShowHideScreenControls":
                        switch (Keyword)
                        {
                            case "SetAlwaysHideValue":
                                DlkShowHideScreenControls.SetAlwaysHideValue(Parameters[0], Parameters[1]);
                                break;
                            case "VerifyAlwaysHideValue":
                                DlkShowHideScreenControls.VerifyAlwaysHideValue(Parameters[0], Parameters[1]);
                                break;
                            case "VerifyTableRowExist":
                                DlkShowHideScreenControls.VerifyTableRowExist(Parameters[0], Parameters[1]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "ArrangeTableColumns":
                        switch (Keyword)
                        {
                            case "SelectRow":
                                DlkArrangeTableColumns.SelectRow(Parameters[0]);
                                break;
                            case "SetWidth":
                                DlkArrangeTableColumns.SetWidth(Parameters[0], Parameters[1]);
                                break;
                            case "VerifyWidth":
                                DlkArrangeTableColumns.VerifyWidth(Parameters[0], Parameters[1]);
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
                        if (VariableValue.Replace("\r","") == ValueToTest)
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
                    DlkCostpointTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }
        [Keyword("PerformMathOperation", new String[] { @"100|5|+|Sum" })]
        public static void PerformMathOperation(String FirstOperand, String SecondOperand, String Operator, String VariableName)
        {
            try
            {
                Decimal res = 0;
                Decimal num1 = 0;
                Decimal num2 = 0;

                if (!Decimal.TryParse(FirstOperand, out num1))
                {
                    throw new Exception("PerformMathOperation(): Invalid Input1 Parameter: " + FirstOperand);
                }
                if (!Decimal.TryParse(SecondOperand, out num2))
                {
                    throw new Exception("PerformMathOperation(): Invalid Input2 Parameter: " + SecondOperand);
                }

                /* Determine number of decimal places of output by getting the max decimal places between Operand1 and Operand2 */
                int firstOperandNumDecimal = FirstOperand.Contains('.') ? (FirstOperand.Length - 1) - FirstOperand.IndexOf('.') : 0;
                int secondOperandNumDecimal = SecondOperand.Contains('.') ? (SecondOperand.Length - 1) - SecondOperand.IndexOf('.') : 0;
                int numDecimalPlaces = Math.Max(firstOperandNumDecimal, secondOperandNumDecimal);

                int firstOperandWholeNumString = FirstOperand.Contains('.') ? FirstOperand.IndexOf('.') : FirstOperand.Length;
                int secondOperandWholeNumString = SecondOperand.Contains('.') ? SecondOperand.IndexOf('.') : SecondOperand.Length;
                int numberWholeNumber = Math.Max(firstOperandWholeNumString, secondOperandWholeNumString);

                string fmtWhole = string.Empty;
                string fmtDecimal = string.Empty;
                string fmt = string.Empty;

                if (FirstOperand.StartsWith("0") && !(FirstOperand.Contains('.'))) // To handle special cases like for serial numbers (e.g. 0000012345)
                {
                    for (int i = 0; i < numberWholeNumber; i++)
                    {
                        fmtWhole += "0";
                    }

                    for (int i = 0; i < numDecimalPlaces; i++)
                    {
                        fmtDecimal += "0";
                    }
                    fmt = !fmtDecimal.Equals("") ? string.Format("{0}.{1}", fmtWhole, fmtDecimal) : fmtWhole;
                }

                switch (Operator)
                {
                    case "+":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] + [" + SecondOperand + "].");
                        res = num1 + num2;
                        break;
                    case "-":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] - [" + SecondOperand + "].");
                        res = num1 - num2;
                        break;
                    case "*":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] * [" + SecondOperand + "].");
                        res = num1 * num2;
                        break;
                    case "/":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] / [" + SecondOperand + "].");
                        res = num1 / num2;
                        break;
                    default:
                        throw new Exception("PerformMathOperation(): Unsupported operator " + Operator);
                }
                /* Round UP the output to numDecimalPlaces */
                decimal resDec = decimal.Round(res, numDecimalPlaces, MidpointRounding.AwayFromZero);
                String resString = resDec.ToString(fmt);

                DlkVariable.SetVariable(VariableName, resString);
                DlkLogger.LogInfo("Successfully executed PerformMathOperation(). Variable:[" + VariableName + "], Value:[" + resString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMathOperation() failed: " + ex.Message);
            }
        }

        [Keyword("PerformMathOperationUnrounded", new String[] { @"100|5|+|Sum" })]
        public static void PerformMathOperationUnrounded(String FirstOperand, String SecondOperand, String Operator, String VariableName)
        {
            try
            {
                Decimal res = 0;
                Decimal num1 = 0;
                Decimal num2 = 0;

                if (!Decimal.TryParse(FirstOperand, out num1))
                {
                    throw new Exception("PerformMathOperationUnrounded(): Invalid Input1 Parameter: " + FirstOperand);
                }
                if (!Decimal.TryParse(SecondOperand, out num2))
                {
                    throw new Exception("PerformMathOperationUnrounded(): Invalid Input2 Parameter: " + SecondOperand);
                }

                /* Number of decimal places is fixed as requested for tax computation */
                int numDecimalPlaces = 6;

                int firstOperandWholeNumString = FirstOperand.Contains('.') ? FirstOperand.IndexOf('.') : FirstOperand.Length;
                int secondOperandWholeNumString = SecondOperand.Contains('.') ? SecondOperand.IndexOf('.') : SecondOperand.Length;
                int numberWholeNumber = Math.Max(firstOperandWholeNumString, secondOperandWholeNumString);

                string fmtWhole = string.Empty;
                string fmtDecimal = string.Empty;
                string fmt = string.Empty;

                if (FirstOperand.StartsWith("0") && !(FirstOperand.Contains('.'))) // To handle special cases like for serial numbers (e.g. 0000012345)
                {
                    for (int i = 0; i < numberWholeNumber; i++)
                    {
                        fmtWhole += "0";
                    }

                    for (int i = 0; i < numDecimalPlaces; i++)
                    {
                        fmtDecimal += "0";
                    }
                    fmt = !fmtDecimal.Equals("") ? string.Format("{0}.{1}", fmtWhole, fmtDecimal) : fmtWhole;
                }

                switch (Operator)
                {
                    case "+":
                        DlkLogger.LogInfo("PerformMathOperationUnrounded(). Operation:[" + FirstOperand + "] + [" + SecondOperand + "].");
                        res = num1 + num2;
                        break;
                    case "-":
                        DlkLogger.LogInfo("PerformMathOperationUnrounded(). Operation:[" + FirstOperand + "] - [" + SecondOperand + "].");
                        res = num1 - num2;
                        break;
                    case "*":
                        DlkLogger.LogInfo("PerformMathOperationUnrounded(). Operation:[" + FirstOperand + "] * [" + SecondOperand + "].");
                        res = num1 * num2;
                        break;
                    case "/":
                        DlkLogger.LogInfo("PerformMathOperationUnrounded(). Operation:[" + FirstOperand + "] / [" + SecondOperand + "].");
                        res = num1 / num2;
                        break;
                    default:
                        throw new Exception("PerformMathOperationUnrounded(): Unsupported operator " + Operator);
                }
                /* Round UP the output to numDecimalPlaces */
                decimal resDec = decimal.Round(res, numDecimalPlaces, MidpointRounding.AwayFromZero);
                String resString = resDec.ToString(fmt);

                DlkVariable.SetVariable(VariableName, resString);
                DlkLogger.LogInfo("Successfully executed PerformMathOperationUnrounded(). Variable:[" + VariableName + "], Value:[" + resString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMathOperationUnrounded() failed: " + ex.Message);
            }
        }

        [Keyword("FormatAmount", new String[] { @"123.5|True|2|MyVariable" })]
        public static void FormatAmount(String Amount, String UseSeparator, String DecimalPlaces, String VariableName)
        {
            try
            {
                Decimal amt = 0;
                bool useSeparator = false;
                int decimalPlaces = 0;

                if (!Decimal.TryParse(Amount, out amt))
                {
                    throw new Exception("FormatAmount(): Invalid Input1 Parameter: " + Amount);
                }
                if (!Boolean.TryParse(UseSeparator, out useSeparator))
                {
                    throw new Exception("FormatAmount(): Invalid Input2 Parameter: " + UseSeparator);
                }
                if (!Int32.TryParse(DecimalPlaces, out decimalPlaces))
                {
                    throw new Exception("FormatAmount(): Invalid Input3 Parameter: " + DecimalPlaces);
                }

                String resString = amt.ToString();
                amt = decimal.Round(amt, decimals: decimalPlaces, mode: MidpointRounding.AwayFromZero);

                if (useSeparator)
                {
                    var myAmount = amt.ToString().Split('.');
                    resString = decimalPlaces > 0
                        ? string.Format("{0:n0}", Convert.ToInt32(myAmount[0])) + "." + myAmount[1]
                        : string.Format("{0:n0}", Convert.ToInt32(myAmount[0]));
                }
                else
                {
                    resString = amt.ToString().Replace(",", "");
                }

                DlkVariable.SetVariable(VariableName, resString);
                DlkLogger.LogInfo("Successfully executed FormatAmount(). Variable:[" + VariableName + "], Value:[" + resString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("FormatAmount() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Function to switch control to another browser window
        /// </summary>
        /// <param name="Index">Index of the browser window opened (zero based)</param>
        [Keyword("SwitchToWindow", new String[] { "1|int|index|1" })]
        public static void SwitchToWindow(int Index)
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[Index]);
                DlkLogger.LogInfo("Successfully executed SwitchToWindow().");
            }
            catch (Exception e)
            {
                throw new Exception("SwitchToWindow() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function to verify the title of the browser window
        /// </summary>
        /// <param name="ExpectedWindowTitle">Expected title of the browser window</param>
        [Keyword("VerifyWindowTitle", new String[] { "1|text|Expected Window Title|Help" })]
        public static void VerifyWindowTitle(String ExpectedWindowTitle)
        {
            String sActualBrowserTitle = DlkEnvironment.AutoDriver.Title;
            DlkAssert.AssertEqual("VerifyWindowTitle()", ExpectedWindowTitle, sActualBrowserTitle);
            DlkLogger.LogInfo("VerifyWindowTitle() passed.");
        }

        /// <summary>
        ///  Function to open a new tab in the current browser. Does not work for IE browser.
        /// </summary>
        [Keyword("OpenNewBrowserTab")]
        public static void OpenNewBrowserTab()
        {
            try
            {
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            javascript.ExecuteScript("window.open();");
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            Thread.Sleep(500);
            DlkLogger.LogInfo("Successfully executed OpenNewBrowserTab().");
            }
            catch (Exception e)
            {
                throw new Exception("OpenNewBrowserTab() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function to switch focus to a new tab in the current browser. Does not work for IE browser.
        /// </summary>
        [Keyword("SwitchFocusToNewBrowserTab")]
        public static void SwitchFocusToNewBrowserTab()
        {
            try
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.SendKeys(Keys.Control + Keys.Tab).KeyUp(Keys.Control).Build().Perform();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
                Thread.Sleep(500);
                DlkLogger.LogInfo("Successfully executed SwitchFocusToNewBrowserTab().");
            }
            catch (Exception e)
            {
                throw new Exception("SwitchFocusToNewBrowserTab() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function to switch to the previous tab in the current browser. Does not work for IE browser.
        /// </summary>
        [Keyword("SwitchFocusToPreviousBrowserTab")]
        public static void SwitchFocusToPreviousBrowserTab()
        {
            string previousWindow = "";
            try
            {
                foreach (String winhandle in DlkEnvironment.AutoDriver.WindowHandles)
                {
                    if (winhandle == DlkEnvironment.AutoDriver.CurrentWindowHandle)
                    {
                        previousWindow = DlkEnvironment.AutoDriver.WindowHandles[(DlkEnvironment.AutoDriver.WindowHandles.IndexOf(winhandle)) - 1];
                    }
                }
                /* To show tab switch to user */
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.SendKeys(Keys.Control + Keys.Shift + Keys.Tab).KeyUp(Keys.Shift).KeyUp(Keys.Control).Build().Perform();
                DlkEnvironment.AutoDriver.SwitchTo().Window(previousWindow);
                Thread.Sleep(500);
                DlkLogger.LogInfo("Successfully executed SwitchFocusToPreviousBrowserTab().");
            }
            catch (Exception e)
            {
                throw new Exception("SwitchFocusToPreviousBrowserTab() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function that performs a click on the Back button in the current browser.
        /// </summary>
        [Keyword("GoBackInBrowser")]
        public static void GoBackInBrowser()
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().Back();
                Thread.Sleep(500);
                DlkLogger.LogInfo("Successfully executed GoBackInBrowser().");
            }
            catch (Exception e)
            {
                throw new Exception("GoBackInBrowser() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function to get the current URL and store it in a variable.
        /// </summary>
        /// <param name="VariableName">Variable name where the URL will be stored</param>
        [Keyword("GetCurrentUrl")]
        public static void GetCurrentUrl(string VariableName)
        {
            try
            {
                string sURL = DlkEnvironment.AutoDriver.Url;
                DlkVariable.SetVariable(VariableName, sURL);
                DlkLogger.LogInfo("Successfully executed GetCurrentUrl(). URL: " + sURL);
            }
            catch (Exception e)
            {
                throw new Exception("GetCurrentUrl() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function to set a specified URL in the browser or new tab.
        /// </summary>
        /// <param name="URL">URL to set in the browser</param>
        [Keyword("GoToUrl")]
        public static void GoToUrl(string URL)
        {
            try
            {
                DlkEnvironment.AutoDriver.Url = URL;
                DlkLogger.LogInfo("Successfully executed GoToUrl(). URL: " + URL);
            }
            catch (Exception e)
            {
                throw new Exception("GoToUrl() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Switch browser to offline mode
        /// </summary>
        [Keyword("EnableOfflineModeInBrowser")]
        public static void EnableOfflineModeInBrowser()
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                {
                    ((OpenQA.Selenium.Chrome.ChromeDriver)DlkEnvironment.AutoDriver).NetworkConditions = new OpenQA.Selenium.Chrome.ChromeNetworkConditions() 
                    { 
                        IsOffline = true, 
                        DownloadThroughput = 5000, 
                        UploadThroughput = 5000, 
                        Latency = TimeSpan.FromMilliseconds(5) 
                    };
                    DlkEnvironment.AutoDriver.Navigate().Refresh();

                    new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(DlkEnvironment.mLongWaitMs)).Until(
                            d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                    DlkLogger.LogInfo("Successfully executed EnableOfflineModeInBrowser()");
                }
                else
                    throw new Exception($"{DlkEnvironment.mBrowser} browser not supported.");
            }
            catch (Exception e)
            {
                throw new Exception("EnableOfflineModeInBrowser() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function that clears chrome browsing data.
        /// </summary>
        /// 
        [Keyword("ClearCache")]
        public static void ClearCache()
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                {
                    IWebDriver driver = DlkEnvironment.AutoDriver;
                    IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
                    jse.ExecuteScript("window.open()");
                    string originalHandle = DlkEnvironment.AutoDriver.CurrentWindowHandle;
                    driver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[1]);
                    driver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
                    Thread.Sleep(2000);
                    IWebElement element = (IWebElement)jse.ExecuteScript(
                            "return document.querySelector('settings-ui').shadowRoot.querySelector('settings-main').shadowRoot.querySelector('settings-basic-page')." +
                            "shadowRoot.querySelector('settings-section > settings-privacy-page').shadowRoot.querySelector('settings-clear-browsing-data-dialog')." +
                            "shadowRoot.querySelector('#clearBrowsingDataDialog').querySelector('#clearBrowsingDataConfirm');");
                    element.Click();
                    Thread.Sleep(3000);
                    driver.Close();
                    driver.SwitchTo().Window(originalHandle);
                    DlkLogger.LogInfo("Successfully executed ClearCache().");
                }
                else
                    throw new Exception($"{DlkEnvironment.mBrowser} browser not supported.");
            }
            catch (Exception e)
            {
                throw new Exception("ClearCache() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Function to save current browser into a variable
        /// </summary>
        /// <param name="VariableName">Variable to save the browser name in</param>
        [Keyword("GetCurrentBrowser")]
        public static void GetCurrentBrowser(string VariableName)
        {
            try
            {
                DlkVariable.SetVariable(VariableName, DlkEnvironment.mBrowser);
                DlkLogger.LogInfo("Successfully executed GetCurrentBrowser(). Variable:[" + VariableName + "], Value:[" + DlkEnvironment.mBrowser + "].");
            }
            catch (Exception e)
            {
                throw new Exception("GetCurrentBrowser() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Switch browser to online mode
        /// </summary>
        [Keyword("EnableOnlineModeInBrowser")]
        public static void EnableOnlineModeInBrowser()
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                {
                    ((OpenQA.Selenium.Chrome.ChromeDriver)DlkEnvironment.AutoDriver).NetworkConditions = new OpenQA.Selenium.Chrome.ChromeNetworkConditions()
                    {
                        IsOffline = false,
                        DownloadThroughput = 5000,
                        UploadThroughput = 5000,
                        Latency = TimeSpan.FromMilliseconds(5)
                    };
                    DlkEnvironment.AutoDriver.Navigate().Refresh();

                    new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(DlkEnvironment.mLongWaitMs)).Until(
                            d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                    DlkLogger.LogInfo("Successfully executed EnableOnlineModeInBrowser()");
                }
                else
                    throw new Exception($"{DlkEnvironment.mBrowser} browser not supported.");
            }
            catch (Exception e)
            {
                throw new Exception("EnableOnlineModeInBrowser() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Synthesize text command
        /// </summary>
        /// <param name="Text">Text command</param>
        [Keyword("TextToSpeech", new String[] { "1|text|Text|Hey Deltek!", "2|text|True" })]
        public static void TextToSpeech(string Text, string WaitResponse, string MaleVoice)
        {
            try
            {
                bool maleVoice = true;
                if (!bool.TryParse(WaitResponse, out bool waitResponse))
                    throw new Exception($"Incorrect parameter value for 'WaitResponse'");

                if(!string.IsNullOrEmpty(MaleVoice) && !bool.TryParse(MaleVoice, out maleVoice))
                    throw new Exception($"Incorrect parameter value for 'MaleVoice'");

                DlkUtility.DlkVoiceCommand.Listen();
                DlkUtility.DlkVoiceCommand.ExecuteVoiceCommand(Text, maleVoice);

                if (waitResponse && !DlkUtility.DlkVoiceCommand.Responding())
                    throw new Exception("Voice recognition did not respond.");

                DlkUtility.DlkVoiceCommand.Wait(true);
                DlkUtility.DlkCostpointCommon.WaitLoadingFinished();

                DlkLogger.LogInfo("TextToSpeech() Passed");
            }
            catch (Exception e)
            {
                throw new Exception("TextToSpeech() failed : " + e.Message, e);
            }
            finally
            {
                DlkUtility.DlkVoiceCommand.Stop();
            }
        }

        /// <summary>
        /// Disconnects machines internet access by releasing it's IP from router (Note: Do not run this keyword on VM)
        /// </summary>
        [Keyword("DisconnectInternetAccess")]
        public static void DisconnectInternetAccess()
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "ipconfig.exe";
                    process.StartInfo.Arguments = "/release";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();
                }
                Thread.Sleep(1000);
                DlkEnvironment.AutoDriver.Navigate().Refresh();

                new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(DlkEnvironment.mLongWaitMs)).Until(
                        d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                DlkLogger.LogInfo("DisconnectInternetAccess() Passed");
            }
            catch (Exception e)
            {
                throw new Exception("DisconnectInternetAccess() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Renew IP from router to reconnect internet access (Note: Do not run this keyword on VM)
        /// </summary>
        [Keyword("ConnectToInternet")]
        public static void ConnectToInternet()
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "ipconfig.exe";
                    process.StartInfo.Arguments = "/renew";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();
                }
                Thread.Sleep(1000);
                DlkEnvironment.AutoDriver.Navigate().Refresh();

                new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(DlkEnvironment.mLongWaitMs)).Until(
                        d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                DlkLogger.LogInfo("ConnectToInternet() Passed");
            }
            catch (Exception e)
            {
                throw new Exception("ConnectToInternet() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Convert percentage value to string and round decimal places to specified parameter
        /// </summary>
        /// <param name="PercentValue">Percentage value. Ex 25.600%</param>
        /// <param name="DecimalPlaces">Number of decimal places</param>
        /// <param name="VariableName">Variable which the string value will be assigned</param>
        [Keyword("PercentageValueToString", new String[] { "PercentValue|Text|8.2560%", "DecimalPlaces|Text|2", "VariableName|Text|PercentVar" })]
        public static void PercentageValueToString(string PercentValue, string DecimalPlaces, string VariableName)
        {
            try
            {
                string[] values = PercentValue.Split('%');
                if (values.Length > 2 || !string.IsNullOrEmpty(values[1]))
                    throw new Exception($"Invalid parameter for PercentValue: '{PercentValue}'");

                if (int.TryParse(DecimalPlaces, out int decimalPlaces))
                {
                    string result = Math.Round(decimal.Parse(values[0]), decimalPlaces).ToString();
                    DlkVariable.SetVariable(VariableName, result);
                }
                else
                    throw new Exception($"Invalid parameter for DecimalPlaces: '{DecimalPlaces}'");
            }
            catch (Exception e)
            {
                throw new Exception("PercentageValueToString() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectAutoCompleteValue", new String[] {"1|text|Value|Offline value: Sample"})]
        public static void SelectAutoCompleteValue(string Value)
        {
            try
            {
                if (AutoCompleteDisplayed(out List<IWebElement> items))
                {
                    bool found = false;
                    foreach (var item in items)
                    {
                        if (item.Text == Value)
                        {
                            Thread.Sleep(500);
                            item.Click();
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        throw new Exception($"Autocomplete value '{Value}' not found");
                    else
                        DlkLogger.LogInfo("SelectAutoCompleteValue() successfully executed");
                }
                else
                    throw new Exception("Autocomplete not found");
            }
            catch (Exception e)
            {
                throw new Exception("SelectAutoCompleteValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetAutoCompleteValue", new String[] {"1|text|Indices|1~2",
                                                    "2|text|VariableName|SampleVar"})]
        public static void GetAutoCompleteValue(string Indices, string VariableName)
        {
            try
            {
                string result = "";
                List<int> indexItems = new List<int>();
                foreach (string item in Indices.Split('~'))
                {
                    if (int.TryParse(item, out int index))
                    {
                        indexItems.Add(index);
                    }
                    else
                        throw new Exception($"{Indices} contains invalid integer parameter");
                }

                if (AutoCompleteDisplayed(out List<IWebElement> autoCompleteValues))
                {
                    indexItems.ForEach(index =>
                    {
                        if (autoCompleteValues.Count >= index)
                        {
                            result += (result != "" ? "~" : "") + autoCompleteValues[index - 1].Text;
                        }
                        else
                            throw new Exception($"Autocomplete value index: '{index}' does not exists");
                    });
                }
                else
                    throw new Exception("Autocomplete not found");

                DlkVariable.SetVariable(VariableName, result);
                DlkLogger.LogInfo("GetAutoCompleteValue() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("GetAutoCompleteValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAutoCompleteValue", new String[] {"1|text|ExpectedValues|Value1~Value2"})]
        public static void VerifyAutoCompleteValue(string ExpectedValues)
        {
            try
            {
                List<string> expectedValues = new List<string>(ExpectedValues.Split('~'));

                if(AutoCompleteDisplayed(out List<IWebElement> items))
                {
                    List<string> autoCompleteValues = items.Select(item => item?.Text).ToList();
                    List<string> foundValues = expectedValues.Intersect(autoCompleteValues).Distinct().ToList();
                    DlkAssert.AssertEqual("VerifyAutoCompleteValue()", expectedValues.ToArray(), foundValues.ToArray());
                    DlkLogger.LogInfo("VerifyAutoCompleteValue() successfully executed");
                }
                else
                    throw new Exception("Autocomplete not found");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAutoCompleteValue() failed : " + e.Message, e);
            }
        }

        [Keyword("RoundHours", new String[] { "1|text|Hours|6.80", "2|text|RoundUpOrDown|Up", 
                                              "3|text|RoundingIncrements|Half", "4|text|VariableName|SampleVar" })]
        public static void RoundHours(string Hours, string RoundUpOrDown, string RoundingIncrements, string VariableName)
        {
            try
            {
                if (!decimal.TryParse(Hours, out decimal hours))
                    throw new Exception($"Invalid parameter value for Hours: '{Hours}'");

                if(!new string[] { "up", "down"}.Contains(RoundUpOrDown.ToLower()))
                    throw new Exception($"Invalid parameter value for RoundUpOrDown: '{RoundUpOrDown}'");

                bool up = RoundUpOrDown.ToLower() == "up";
                string result = "";
                
                switch (RoundingIncrements.ToLower())
                {
                    case "whole":
                        result = Microsoft.VisualBasic.Conversion.Int(hours).ToString("0.00");
                        break;
                    case "half":
                        result = MRound(hours, 0.5M, up);
                        break;
                    case "quarter":
                        result = MRound(hours, 0.25M, up);
                        break;
                    case "tenths":
                        result = ExcelRound(hours, 1, up);
                        break;
                    case "hundredths":
                        result = ExcelRound(hours, 2, up);
                        break;
                    default:
                        throw new Exception($"Invalid parameter value for RoundingIncrements: '{RoundingIncrements}'");
                }
                DlkLogger.LogInfo($"Rounded value = '{result}'");
                DlkVariable.SetVariable(VariableName, result);
                DlkLogger.LogInfo("RoundHours() sucessfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Message, e);
            }
                
        }

        /// <summary>
        /// Performs excel MROUND formula
        /// </summary>
        /// <param name="number">Number of hours</param>
        /// <param name="significance">Significant decimal value used to round</param>
        /// <param name="up">UP=true;DOWN=false</param>
        /// <returns>Formatted in 2 decimal places string</returns>
        private static string MRound(decimal number, decimal significance, bool up)
        {
            decimal result;
            if (up)
                result = Math.Round(number / significance) * significance;
            else
                result = Math.Floor(number / significance) * significance;

            return result.ToString("0.00");
        }

        /// <summary>
        /// Performs excel ROUNDUP/ROUNDDOWN formula
        /// </summary>
        /// <param name="number">Number of hours</param>
        /// <param name="precision">Number of decimal</param>
        /// <param name="up">UP=true;DOWN=false</param>
        /// <returns>Formatted in 2 decimal places string</returns>
        private static string ExcelRound(decimal number, int precision, bool up)
        {
            double result;
            if (up)
                result = Math.Ceiling(Convert.ToDouble(number) * Math.Pow(10, precision)) / Math.Pow(10, precision);
            else
                result = Math.Floor(Convert.ToDouble(number) * Math.Pow(10, precision)) / Math.Pow(10, precision);

            return result.ToString("0.00");
        }

        /// <summary>
        /// Checks if autocomplete is displayed
        /// </summary>
        /// <param name="items">List of autocomplete values</param>
        /// <returns>DISPLAYED=true;OTHERWISE=false</returns>
        private static bool AutoCompleteDisplayed(out List<IWebElement> items)
        {
            List<IWebElement> autoCompleteItems = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='fldAutoCompleteDiv' and contains(@style,'display: block')]/*[contains(@style,'display: block')]")).ToList();
            items = autoCompleteItems;
            return autoCompleteItems.Count > 0;
        }
    }
}
