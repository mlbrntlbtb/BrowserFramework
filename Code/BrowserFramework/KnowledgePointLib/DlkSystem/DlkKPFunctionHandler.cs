using System;
using System.Linq;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using KnowledgePointLib.DlkControls;
using CommonLib.DlkControls;
using KnowledgePointLib.DlkFunctions;

namespace KnowledgePointLib.DlkSystem
{
    [ControlType("Function")]
    public class DlkKPFunctionHandler
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
                    case "ClickOkOnAlert":
                        ClickOkOnAlert();
                        break;
                    case "ClickCancelOnAlert":
                        ClickCancelOnAlert();
                        break;
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "GoToUrl":
                        GoToUrl(Parameters[0]);
                        break;
                    case "GetCurrentUrl":
                        GetCurrentUrl(Parameters[0]);
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
                    case "VerifyAlertText":
                        VerifyAlertText(Parameters[0]);
                        break;
                    case "VerifyPreloaderDisplayed":
                        VerifyPreloaderDisplayed(Parameters[0]);
                        break;
                    case "VerifyJSBundle":
                        VerifyJSBundle(Parameters[0]);
                        break;
                    case "VerifyForbiddenPage":
                        VerifyForbiddenPage(Parameters[0]);
                        break;
                    case "VerifyURL":
                        VerifyURL(Parameters[0]);
                        break;
                    case "VerifyURLContains":
                        VerifyURLContains(Parameters[0]);
                        break;
                    case "Verify404Page":
                        Verify404Page(Parameters[0]);
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
                            case "ClickButtonIfDialogExists":
                                DlkDialog.ClickButtonIfDialogExists(Parameters[0]);
                                break;
                            case "ClickButtonOnDialogWithMessage":
                                DlkDialog.ClickButtonOnDialogWithMessage(Parameters[0], Parameters[1]);
                                break;
                            case "VerifyDialogExists":
                                DlkDialog.VerifyDialogExists(Parameters[0]);
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
            if (!String.IsNullOrEmpty(User))
            {
                //Wait for Token
                Thread.Sleep(5000);
                //new DlkButton("signin", "xpath", DlkDynamicObjectStoreHandler.GetControlRecord("Login", "SignIn").mSearchParameters).Click();
                
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
        [Keyword("GoToUrl")]
        public static void GoToUrl(String URL)
        {
            DlkEnvironment.AutoDriver.Navigate().GoToUrl(URL);
            DlkLogger.LogInfo("Navigating to : " + URL);
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
                    DlkKPTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkKPTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                int firstOperandNumString = FirstOperand.Contains(',') ? FirstOperand.IndexOf(',') : FirstOperand.Length;
                if (FirstOperand.Contains(',')) firstOperandWholeNumString = firstOperandWholeNumString - 1;

                int secondOperandWholeNumString = SecondOperand.Contains('.') ? SecondOperand.IndexOf('.') : SecondOperand.Length;
                int secondOperandNumString = SecondOperand.Contains(',') ? SecondOperand.IndexOf(',') : SecondOperand.Length;
                if (SecondOperand.Contains(',')) secondOperandWholeNumString = secondOperandWholeNumString - 1;
                int numberWholeNumber = Math.Max(firstOperandWholeNumString, secondOperandWholeNumString);

                string fmtWhole = string.Empty;
                string fmtDecimal = string.Empty;
                for (int i = 0; i < numberWholeNumber; i++)
                {
                    if (i == firstOperandNumString - 1)
                        fmtWhole += "0" + ",";
                    else
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
                DlkLogger.LogInfo("Successfully executed PerformMathOperation(). Variable:[" + VariableName + "], Value:[" + resString + "].");
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

        [Keyword("VerifyPreloaderDisplayed")]
        public static void VerifyPreloaderDisplayed(String TrueOrFalse)
        {
            IWebElement preLoaderIndicator = DlkEnvironment.AutoDriver.FindElements(By.XPath("//header[contains(@class,'jss15')]")).FirstOrDefault(); // autogenerated jss1-jss14 is used by preloader. Hence, if header contains jss15, preloader has been showed
            bool hasBeenDisplayed = false;
            if (preLoaderIndicator != null)
                hasBeenDisplayed = true;
            DlkAssert.AssertEqual("Verify if preloader has been displayed ", hasBeenDisplayed, Convert.ToBoolean(TrueOrFalse));
        }

        [Keyword("VerifyAlertText")]
        public static void VerifyAlertText(String ExpectedAlertText)
        {
            if (DlkAlert.DoesAlertExist())
            {
                DlkAlert.VerifyAlertText(ExpectedAlertText);
                DlkLogger.LogInfo("Successfully executed Ok on alert.");
            }
        }
        [Keyword("ClickOkOnAlert")]
        public static void ClickOkOnAlert()
        {
            IWebElement muiDialogOkButton = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'MuiDialog-paper')]//button[@data-testid='specpointAlertConfirmBtn']")).FirstOrDefault();
            if (DlkAlert.DoesAlertExist())
            {
                DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                DlkLogger.LogInfo("Successfully executed Ok on alert.");
            }
            else if (muiDialogOkButton.Displayed)
            {
                muiDialogOkButton.Click();
                DlkLogger.LogInfo("Successfully executed Ok on alert.");
            }
        }

        [Keyword("ClickCancelOnAlert")]
        public static void ClickCancelOnAlert()
        {
            IWebElement muiDialogCancelButton = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'MuiDialog-paper')]//button[@data-testid='specpointAlertCancelBtn']")).FirstOrDefault();
            if (DlkAlert.DoesAlertExist())
            {
                DlkEnvironment.AutoDriver.SwitchTo().Alert().Dismiss();
                DlkLogger.LogInfo("Successfully executed Cancel on alert.");
            }
            else if (muiDialogCancelButton.Displayed)
            {
                muiDialogCancelButton.Click();
                DlkLogger.LogInfo("Successfully executed Cancel on alert.");
            }
        }


        [Keyword("VerifyURL")]
        public static void VerifyURL(String URL)
        {
            DlkAssert.AssertEqual("Window URL Check", URL, DlkEnvironment.AutoDriver.Url);
        }

        [Keyword("VerifyURLContains")]
        public static void VerifyURLContains(String URL)
        {
            bool urlContains = DlkEnvironment.AutoDriver.Url.Contains(URL);
            DlkAssert.AssertEqual("Window URLContains Check ", true, urlContains);

            DlkLogger.LogInfo("Successfully executed VerifyURLContains()");
        }


        [Keyword("VerifyForbiddenPage")]
        public static void VerifyForbiddenPage(String TrueOrFalse)
        {
            IWebElement forbiddenElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//img[contains(@src,'security-nobubbles.') and contains(@alt,'not allowed in here.')]"));
            DlkAssert.AssertEqual("Verifying Forbidden Page", Convert.ToBoolean(TrueOrFalse), forbiddenElement.Displayed);
        }
        
        [Keyword("VerifyJSBundle")]
        public static void VerifyJSBundle(String CDN)
        {
            int scriptCdn = DlkEnvironment.AutoDriver.FindElements(By.XPath("//script[contains(@src,'"+ CDN + "')]")).Count;
            bool bExists = false;
            if (scriptCdn > 0)
                bExists = true;

            DlkAssert.AssertEqual("Verifying JS Bundle", true, bExists);
        }

        [Keyword("Verify404Page")]
        public static void Verify404Page(String TrueOrFalse)
        {
            IWebElement forbiddenElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[contains(text(),'Oops! 404')]"));
            DlkAssert.AssertEqual("Verifying 404 Page", Convert.ToBoolean(TrueOrFalse), forbiddenElement.Displayed);
        }
    }
}
