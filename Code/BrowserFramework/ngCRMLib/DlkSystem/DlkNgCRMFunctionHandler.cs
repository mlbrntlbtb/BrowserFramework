using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using ngCRMLib.System;
using ngCRMLib.DlkControls;
using ngCRMLib.DlkFunctions;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using CommonLib.DlkControls;
using System.Diagnostics;

namespace ngCRMLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]    
    public static class DlkNgCRMFunctionHandler
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
                    case "VerifyPageOpens":
                        VerifyPageOpens(Parameters[0]);
                        break;
                    case "NavigateTo":
                        NavigateTo(Parameters[0]);
                        break;
                    case "WaitForSaving":
                        WaitForSaving();
                        break;
                    case "VerifyPrintPreviewPage":
                        VerifyPrintPreviewPage(Parameters[0]);
                        break;
                    case "SwitchFocusToNewTab":
                        SwitchFocusToNewTab();
                        break;
                    case "SwitchFocusToPreviousTab":
                        SwitchFocusToPreviousTab();
                        break;
                    case "VerifyURL":
                        VerifyURL(Parameters[0]);
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
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);                                
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

                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    
                    DlkNgCRMTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

      
        //[Keyword("NavigateTo")]      
        //public static void NavigateTo(String ProductName)
        //{
        //    DlkLoginConfigHandler mLoginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
           
        //    switch (ProductName.ToLower())
        //    {
        //        case "crm":
        //            DlkNgCRMFunctionHandler.ExecuteFunction("Login", "Function", "Login",
        //            new String[] { "http://ashapt15vs/CRM", mLoginConfigHandler.mUser, 
        //            mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
        //            break;
        //        case "navigator_crm":
        //            DlkNgCRMFunctionHandler.ExecuteFunction("Login", "Function", "Login",
        //            new String[] { "http://ashapt15vs/Navigator", mLoginConfigHandler.mUser, 
        //            mLoginConfigHandler.mPassword, mLoginConfigHandler.mDatabase });
        //            DlkNgCRMKeywordHandler.ExecuteKeyword("Navigator_MainHeader", "WorkspaceSwitcher", "Click", new String[] { "" });
        //            DlkNgCRMKeywordHandler.ExecuteKeyword("Navigator_MainHeader", "Dropdown_ConextMenu", "Select", new String[] { "Business Development" });
        //            break;
        //        case "navigator":
        //            DlkNgCRMFunctionHandler.ExecuteFunction("Login", "Function", "Login",
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
            DlkAssert.AssertEqual("VerifyPrintPreviewPage", true, DlkEnvironment.AutoDriver.Url.Contains(PageTitle));          
            DlkEnvironment.AutoDriver.Close();
            Thread.Sleep(500);
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
        }

        [Keyword("SwitchFocusToNewTab")]
        public static void SwitchFocusToNewTab()
        {

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
                    previousWindow = DlkEnvironment.AutoDriver.WindowHandles[(DlkEnvironment.AutoDriver.WindowHandles.IndexOf(winhandle))-1];
                }
            }
            /* To show tab switch to user */
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.SendKeys(Keys.Control + Keys.Shift + Keys.Tab).Build().Perform();
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


            // use the object store definitions 
            if(User != "")
            {
                DlkNgCRMKeywordHandler.ExecuteKeyword("Login", "UserID", "Set", new String[] { User });
                DlkNgCRMKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                DlkNgCRMKeywordHandler.ExecuteKeyword("Login", "Database", "Select", new String[] { Database });
                Thread.Sleep(250);
                DlkNgCRMKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });

                //Wait for Sidebar to appear (denotes that the main page has loaded)
                DlkObjectStoreFileControlRecord osRec = DlkDynamicObjectStoreHandler.GetControlRecord("Main", "SideBar");
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

            //if(Url.Contains("Navigator"))
            //{
            //    DlkNgCRMKeywordHandler.ExecuteKeyword()
            //}
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
