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
using CommonLib.DlkControls;
using TEMobileLib.DlkFunctions;

namespace TEMobileLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]    
    public static class DlkTEMobileFunctionHandler
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
                    case "NativeInputPIN":
                        NativeInputPIN(Parameters[0]);
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
                                DlkCP7Login.Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                                break;
                            case "TestLoginConnection":
                                DlkCP7Login.TestLoginConnection(Parameters[0], Parameters[1], Parameters[2]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Query":
                        switch (Keyword)
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
                        switch (Keyword)
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
                            case "Cancel":
                                DlkAutoComplete.Cancel();
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
                    case "OpenApplications":
                        switch (Keyword)
                        {
                            case "Close":
                                DlkOpenApplications.Close(Parameters[0]);
                                break;
                            case "CollapseOrExpandTreeView":
                                DlkOpenApplications.CollapseOrExpandTreeView(Parameters[0]);
                                break;
                            case "SwitchToApplication":
                                DlkOpenApplications.SwitchToApplication(Parameters[0]);
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
                    DlkTEMobileTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                        ? string.Format("{0:n0}",Convert.ToInt32(myAmount[0])) + "." + myAmount[1] 
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
                var driver = DlkEnvironment.AutoDriver;
                // get the current active window
                string parentHandle = driver.CurrentWindowHandle;

                // open new window

                // switch to the new window
                foreach (string handle in driver.WindowHandles)
                {
                    if (!handle.Equals(parentHandle))
                    {
                        driver.SwitchTo().Window(handle);
                    }
                }


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
            try
            {
                SwitchToWindow2("WELCOME");
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.CurrentWindowHandle);

                //DlkEnvironment.AutoDriver.SwitchTo().Window("handle");


                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[0]);
                String sActualBrowserTitle = DlkEnvironment.AutoDriver.Title;
                DlkAssert.AssertEqual("VerifyWindowTitle()", ExpectedWindowTitle, sActualBrowserTitle);
                DlkLogger.LogInfo("VerifyWindowTitle() passed.");
            }
            catch (Exception e)
            {
                throw new Exception($"VerifyWindowTitle(): {e.Message}");
            }
            
        }

        /// <summary>
        /// Function to input numeric value 
        /// </summary>
        /// <param name="PINValue">Value of PIN to press on native screen</param>
        [Keyword("NativeInputPIN", new String[] { "1|text|Expected Window Title|Help" })]
        public static void NativeInputPIN(String PINValue)
        {
            try
            {
                if (!int.TryParse(PINValue, out int num))
                {
                    throw new Exception("NativeInputPIN failed : parameter is an invalid PIN value");
                }
                DlkEnvironment.mIsMobile = true;
                DlkEnvironment.SetContext("NATIVE");
                DlkMobileControl.EnterPIN(PINValue);
                DlkLogger.LogInfo("NativeInputPIN() passed");
            }
            catch (Exception e)
            {
                throw new Exception($"NativeInputPIN(): {e.Message}");
            }
            finally
            {
                DlkEnvironment.SetContext("WEBVIEW");
                DlkEnvironment.mIsMobile = false;
            }
        }

        public static void SwitchToWindow2(string title)
        {
            foreach (string handle in DlkEnvironment.AutoDriver.WindowHandles)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(handle);
                if (DlkEnvironment.AutoDriver.Title == title)
                {
                    return;
                }
            }

            throw new ArgumentException("Unable to find window with condition:");
        }
    }
}
