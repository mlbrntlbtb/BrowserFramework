using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using SBCLib.DlkControls;
using SBCLib.DlkFunctions;

namespace SBCLib.DlkSystem
{
    [ControlType("Function")]
    public class DlkSBCFunctionHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }
        private const int WAIT_LIMIT = 60;
        private const int WAIT_TO_EXIST = 1;

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
                    case "ScrollUp":
                        ScrollUp();
                        break;
                    case "ScrollDown":
                        ScrollDown();
                        break;
                    case "SwitchFocusToNewTab":
                        SwitchFocusToNewTab();
                        break;
                    case "SwitchFocusToPreviousTab":
                        SwitchFocusToPreviousTab();
                        break;
                    case "ExtractNumberFromText":
                        ExtractNumberFromText(Parameters[0], Parameters[1]);
                        break;
                    case "VerifyFontSize":
                        VerifyFontSize(Parameters[0]);
                        break;
                    case "WaitLoadingFinished":
                        WaitLoadingFinished();
                        break;
                    case "DateTimeNow":
                        DateTimeNow(Parameters[0]);
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
                            case "MultipleFileUpload":
                                DlkDialog.MultipleFileUpload(Parameters[0], Parameters[1]);
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
        [Keyword("GetFormattedDateToday", new String[] { "" })]
        public static void GetFormattedDateToday(String VariableName, String DateFormat)
        {
            DlkVariable.SetVariable(VariableName, DateTime.Now.ToString(DateFormat));
        }

        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            if (!String.IsNullOrEmpty(User))
            {
                DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "Login");
                DlkButton loginBtn = new DlkButton(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
                int waitTime = 0;
                /* wait for login screen to be ready */
                while (waitTime++ <= WAIT_LIMIT && !loginBtn.Exists(WAIT_TO_EXIST))
                {
                    continue; /* Do nothing. Call to Exist() takes 1s */
                }

                new DlkTextBox("username", "ID", DlkDynamicObjectStoreHandler.GetControlRecord("Login", "Username").mSearchParameters).Set(User);
                new DlkTextBox("password", "ID", DlkDynamicObjectStoreHandler.GetControlRecord("Login", "Password").mSearchParameters).Set(Password);
                loginBtn.Click();
            }
        }

        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;

            switch (Operator)
            {
                case "=":
                case ">=":
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
                    DlkSBCTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
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

                    DlkSBCTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("PerformMathOperation", new String[] { @"1|text|Comment|This is a comment" })]
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
                for (int i = 0; i < numberWholeNumber; i++)
                {
                    fmtWhole += "0";
                }

                for (int i = 0; i < numDecimalPlaces; i++)
                {
                    fmtDecimal += "0";
                }

                string fmt = !fmtDecimal.Equals("") ? string.Format("{0}.{1}", fmtWhole, fmtDecimal) : fmtWhole;

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
                DlkLogger.LogInfo($"Successfully executed PerformMathOperation(). Variable:[{VariableName}], Value:[{resString}].");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMathOperation() failed: " + ex.Message);
            }
        }

        [Keyword("ScrollUp")]
        public static void ScrollUp()
        {
            try
            {          
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();
                DlkLogger.LogInfo("Successfully executed ScrollUp()");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollUp() failed : " + e.Message);
            }
        }

        [Keyword("ScrollDown")]
        public static void ScrollDown()
        {
            try
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();
                DlkLogger.LogInfo("Successfully executed ScrollDown()");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollDown() failed : " + e.Message);
            }
        }

        [Keyword("SwitchFocusToNewTab")]
        public static void SwitchFocusToNewTab()
        {
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.SendKeys(Keys.Control + Keys.Tab).KeyUp(Keys.Control).Build().Perform();
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
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
                    previousWindow = DlkEnvironment.AutoDriver.WindowHandles[(DlkEnvironment.AutoDriver.WindowHandles.IndexOf(winhandle)) - 1];
                }
            }
            /* To show tab switch to user */
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.SendKeys(Keys.Control + Keys.Shift + Keys.Tab).KeyUp(Keys.Shift).KeyUp(Keys.Control).Build().Perform();
            DlkEnvironment.AutoDriver.SwitchTo().Window(previousWindow);
            Thread.Sleep(800);
            DlkLogger.LogInfo("Successfully executed SwitchFocusToPreviousTab()");

        }

        [Keyword("ExtractNumberFromText")]
        public static void ExtractNumberFromText(String Text, String VariableName)
        {
            try
            {
                if ( !DlkString.HasNumericChar(Text)) { throw new Exception("Specified text has no numeric value."); }
                string res = new String(Text.Where(x => char.IsDigit(x)).ToArray());
                DlkVariable.SetVariable(VariableName, res);
                DlkLogger.LogInfo($"Successfully executed ExtractNumberFromText(). Variable:[{VariableName}], Value:[{res}].");
            }
            catch(Exception e)
            {
                throw new Exception($"ExtractNumberFromText() failed: {e.Message}");
            }
        }

        [Keyword("VerifyFontSize")]
        public static void VerifyFontSize(String ExpectedValue)
        {
            try
            {
                string actualFontSize = string.Empty;
                IWebElement page = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'current_font')]")).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@class,'current_font')]")) : 
                    DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='canvasAction']//parent::body"));
                string pageStyle = page.GetAttribute("style") ;
                switch (pageStyle.ToLower())
                {
                    case string small when small.Contains("1em"):
                    case string small2 when small2.Contains("0.625vw"):
                        actualFontSize = "1em";
                        break;
                    case string medium when medium.Contains("1.25em"):
                    case string medium2 when medium2.Contains("0.7815vw"):
                        actualFontSize = "1.25em";
                        break;
                    case string large when large.Contains("1.5em"):
                    case string large2 when large2.Contains("0.9375vw"):
                        actualFontSize = "1.5em";
                        break;
                }
                DlkAssert.AssertEqual("VerifyFontSize()", ExpectedValue.ToLower(), actualFontSize);
            }
            catch (Exception e)
            {
                throw new Exception($"VerifyFontSize() failed: {e.Message}");
            }
        }

        [Keyword("DateTimeNow")]
        public static void DateTimeNow(String VariableName)
        {
            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());
            string dtValue = DlkString.GetDateAsText(dtNow, @"m/dd/yy hh:mm tt");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo("Successfully executed DateTimeNow(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
        }

        [Keyword("WaitLoadingFinished")]
        public static void WaitLoadingFinished()
        {
            try
            {
                int count = 0, maxWaitTime = 180;

                //List XPaths of loaders/spinners
                string canvasloaderXPath = "//div[@id='canvasAction']//span[@id='lblMessage']";
                string loaderXPath = "//div[contains(@class,'displayloader')]";

                Stopwatch mWatch = Stopwatch.StartNew();
                mWatch.Start();
                DlkLogger.LogInfo($"Searching for loaders...");
                IWebElement basePage = DlkEnvironment.AutoDriver.FindElement(By.XPath("//body"));
                IList<IWebElement> loaders = basePage.FindWebElementsCoalesce(true, By.XPath(canvasloaderXPath), By.XPath(loaderXPath));
                
                if(loaders != null)
                {
                    DlkLogger.LogInfo($"Loaders found. Waiting...");
                    while (loaders.Any(x=> x.Displayed) && count < maxWaitTime)
                    {
                        Thread.Sleep(1000);
                    }
                    mWatch.Stop();
                    DlkLogger.LogInfo($"Total loading time: {Math.Round(Convert.ToDecimal(mWatch.ElapsedMilliseconds)/1000,2)} seconds");
                }
                else
                {
                    mWatch.Stop();
                    DlkLogger.LogInfo($"No loaders found. Proceeding...");
                }
                if (count >= maxWaitTime) { DlkLogger.LogInfo($"Loading time exceeded maximum wait time of 180 seconds. Proceeding to next step..."); }                
            }
            catch (Exception ex)
            {
                DlkLogger.LogInfo($"WaitLoadingFinished() failed: {ex.Message}");
            }
        }

        #region PRIVATE METHODS
        private static DateTime SetDateToUSCultureInfo(String dateToSet)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime dt = DateTime.Parse(dateToSet, cultureInfo); //parse date to en-US date format
            return dt;
        }
        #endregion
    }
}
