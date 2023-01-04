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
using WorkBookLib.DlkControls;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using WorkBookLib.DlkFunctions;
using OpenQA.Selenium.Interactions;

namespace WorkBookLib.DlkSystem
{
    [ControlType("Function")]
    public static class DlkWorkBookFunctionHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        #region PUBLIC METHODS

        public static void ExecuteFunction(String Database, String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "ReLogin":
                        ReLogin(Parameters[0], Parameters[1]);
                        break;
                    case "WaitScreenGetsReady":
                        WaitScreenGetsReady();
                        break;
                    case "ShowPagePathElement":
                        ShowPagePathElement();
                        break;
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "CreateNewFolder":
                        CreateNewFolder(Parameters[0], Parameters[1]);
                        break;
                    case "RefreshPage":
                        RefreshPage();
                        break;
                    case "YearToday":
                        YearToday(Parameters[0]);
                        break;
                    case "ExecuteDBQuery":
                        ExecuteDBQuery(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "GetDBFolderName":
                        GetDBFolderName(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "MaximizeWindow":
                        MaximizeWindow();
                        break;
                    case "ResizeWindow":
                        ResizeWindow(Parameters[0], Parameters[1]);
                        break;
                    case "FormatAmount":
                        FormatAmount(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                        break;
                    case "OpenNewTab":
                        OpenNewTab();
                        break;
                    case "CloseCurrentTab":
                        CloseCurrentTab();
                        break;
                    case "SwitchFocusToNewTab":
                        SwitchFocusToNewTab();
                        break;
                    case "SwitchFocusToPreviousTab":
                        SwitchFocusToPreviousTab();
                        break;
                    default:
                        DlkFunctionHandler.ExecuteFunction(Keyword, Parameters);
                        break;
                }
            }
            else if (Screen == "Database")
            {
                switch (Keyword)
                {
                    case "ExecuteQuery":
                        DlkDatabaseHandler.ExecuteQuery(Parameters[0], Parameters[1], Parameters[2], Parameters[3],
                            Parameters[4], Parameters[5], Parameters[6], Parameters[7]);
                        break;
                    case "ExecuteQueryAssignToVariable":
                        DlkDatabaseHandler.ExecuteQueryAssignToVariable(Parameters[0], Parameters[1], Parameters[2], Parameters[3],
                            Parameters[4], Parameters[5], Parameters[6], Parameters[7]);
                        break;
                    default:
                        throw new Exception("Unknown Database Function: [" + Keyword + "]");
                }
            }
            else if (Screen == "Dialog")
            {
                switch (Keyword)
                {
                    case "FileUpload":
                        DlkDialog.FileUpload(Parameters[0], Parameters[1]);
                        break;
                    case "MultipleFileUpload":
                        DlkDialog.MultipleFileUpload(Parameters[0], Parameters[1]);
                        break;
                    default:
                        throw new Exception("Unkown Dialog Function.[" + Keyword + "]");
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
                                Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
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
        
        public static void UserPassLoginExecute(string User, string Pass)
        {
            string UserID_XPath = "//input[@id='login']";
            bool isUserFieldDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(UserID_XPath)).Where(x => x.Displayed).Any();
            if (isUserFieldDisplayed)
            {
                DlkWorkBookKeywordHandler.ExecuteKeyword("Login", "UserID", "Set", new String[] { User });
                DlkWorkBookKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Pass });
                DlkWorkBookKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                DlkLogger.LogInfo("Executing click to login button... ");

                WaitForLoginEnable();
                ResetPasswordIfExpired(Pass);
                WaitForLoginToFinishLoad();
                VerifyInvalidLoginPrompt();
            }
            else
                throw new Exception("Login page not found.");
        }
        
        public static void WaitForWorkBookToLoad()
        {
            //Wait for workbook page to load
            int waitPageLoad = 1;
            int waitPageLoadLimit = 301;
            string loginDisplay_XPath = "//button[contains(@class,'LoginButton')] | //input[contains(@id,'login')]";
            bool isLoginDisplayed = false;

            while (waitPageLoad != waitPageLoadLimit)
            {
                isLoginDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(loginDisplay_XPath)).Where(x => x.Displayed).Any();
                if (!isLoginDisplayed)
                {
                    DlkLogger.LogInfo("Waiting for WorkBook to load... [" + waitPageLoad.ToString() + "]s");
                    Thread.Sleep(1000);
                    waitPageLoad++;
                }
                else
                    break;
            }

            if (!isLoginDisplayed)
                throw new Exception("Waiting for WorkBook to load has reached its limit (300s). Setup failed.");

            ShowPagePathOnLoad();
            DlkLogger.LogInfo("WorkBook page has loaded.");
        }

        public static void ShowPagePathOnLoad()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            jse.ExecuteScript("WorkBook.showNavigationPagePath = true");
        }

        public static void WaitForLoginEnable()
        {
            //Verify if clicking login button is still disabled or loading
            string loginButtonDisabledXPath = "//button[contains(@class,'LoginButton')][contains(@class,'disabled')]";
            int waitLoginButton = 1;
            int waitLoginButtonLimit = 301;
            bool isLoginDisabled = true;

            while (waitLoginButton != waitLoginButtonLimit)
            {
                isLoginDisabled = DlkEnvironment.AutoDriver.FindElements(By.XPath(loginButtonDisabledXPath)).Count > 0;
                if (isLoginDisabled)
                {
                    DlkLogger.LogInfo("Waiting for login button to be enabled... [" + waitLoginButton.ToString() + "]s");
                    Thread.Sleep(1000);
                    waitLoginButton++;
                }
                else
                    break;
            }
            if (isLoginDisabled)
                throw new Exception("Waiting for login button to be enabled or finished loading has reached its limit (300s). Setup failed.");

            DlkLogger.LogInfo("Login button enabled.");
        }

        public static void ResetPasswordIfExpired(string Password)
        {
            //Verify if password expired or requires resetting
            string newPassContainerXPath = "//*[@class='NewPasswordContainer']";
            string currentPassXPath = "//*[@id='newPasswordCurrentPassword']";
            string newPassXPath = "//*[@id='newPassword']";
            string repeatPassXPath = "//*[@id='newPasswordRepeat']";
            string newButtonXPath = "//button[contains(@class,'SendNewPassword')]";
            bool isNewPassDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(newPassContainerXPath)).Where(x=>x.Displayed).Any();
            if (isNewPassDisplayed)
            {
                DlkLogger.LogInfo("Password has expired. Resetting new password... ");
                IWebElement newPass = DlkEnvironment.AutoDriver.FindElement(By.XPath(newPassXPath));
                IWebElement repeatPass = DlkEnvironment.AutoDriver.FindElement(By.XPath(repeatPassXPath));
                IWebElement newButton = DlkEnvironment.AutoDriver.FindElement(By.XPath(newButtonXPath));

                //Verify if current password field is displayed
                bool isCurrentPassDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(currentPassXPath)).Where(x => x.Displayed).Any();
                if (isCurrentPassDisplayed)
                {
                    IWebElement currentPass = DlkEnvironment.AutoDriver.FindElement(By.XPath(currentPassXPath));
                    new DlkTextBox("Current Password", currentPass).Set(Password);
                }

                new DlkTextBox("New Password", newPass).Set(Password);
                new DlkTextBox("Repeat Password", repeatPass).Set(Password);
                newButton.Click();
                DlkLogger.LogInfo("Proceeding with new password... ");

                //Verify if clicking new password button is still disabled or loading
                string passButtonDisabledXPath = "//button[contains(@class,'SendNewPassword')][contains(@class,'disabled')]";
                int waitPassButton = 1;
                int waitPassButtonLimit = 301;
                bool isPassButtonDisabled = true;
                while (waitPassButton != waitPassButtonLimit)
                {
                    isPassButtonDisabled = DlkEnvironment.AutoDriver.FindElements(By.XPath(passButtonDisabledXPath)).Where(x => x.Displayed).Any();
                    if (isPassButtonDisabled)
                    {
                        DlkLogger.LogInfo("Waiting for new password button to finish loading... [" + waitPassButton.ToString() + "]s");
                        Thread.Sleep(1000);
                        waitPassButton++;
                    }
                    else
                        break;
                }
                if (isPassButtonDisabled)
                    throw new Exception("Waiting for new password button to be enabled or finished loading has reached its limit (300s). Setup failed.");
                else
                    DlkLogger.LogInfo("New password button enabled.");

                //Check if alert dialogs appear during refresh
                try
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                }
                catch
                {
                    //Do nothing
                }

                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("WorkBook.showNavigationPagePath = true");
                DlkLogger.LogInfo("Show navigation page path set to true.");
            }
        }

        public static void VerifyInvalidLoginPrompt()
        {
            //Verify if invalid login prompt appeared
            string invalidXPath = "//span[@id='errorText'][contains(text(),'Invalid')]";
            bool isInvalidLogin = DlkEnvironment.AutoDriver.FindElements(By.XPath(invalidXPath)).Where(x => x.Displayed).Any();
            if (isInvalidLogin)
                throw new Exception("Invalid username/password prompt appeared. Setup failed.");
            else
                DlkLogger.LogInfo("Proceeding to logging in...");
        }

        public static void WaitForLoginToFinishLoad()
        {
            //Wait for login progress bar to finish loading
            string loginProgress_XPath = "//*[@id='loginProgressBar'] | //*[@id='loginProgressBarPercentage']";
            int waitLoginBar = 1;
            int waitLoginBarLimit = 301;
            bool isLoginStillLoading = true;

            while (waitLoginBar != waitLoginBarLimit)
            {
                isLoginStillLoading = DlkEnvironment.AutoDriver.FindElements(By.XPath(loginProgress_XPath)).Where(x => x.Displayed).Any();
                if (isLoginStillLoading)
                {
                    DlkLogger.LogInfo("Waiting for login bar to finish loading... [" + waitLoginBar.ToString() + "]s");
                    ShowPagePathOnLoad();
                    Thread.Sleep(1000);
                    waitLoginBar++;
                }
                else
                    break;
            }

            if (isLoginStillLoading)
                throw new Exception("Waiting for login bar to finish loading has reached its limit (300s). Setup failed.");

            ShowPagePathOnLoad();
            DlkLogger.LogInfo("Login bar has finished loading.");
        }

        public static void WaitForMainMenuDisplay()
        {
            //Wait for main menu to be displayed
            string loginButtonProgressBar_XPath = "//button[contains(@class,'LoginButton')] | //*[@id='loginProgressBar']";
            string mainMenuXPath = "//div[@id='leftSideMenuButtonContainer']";
            int waitMainMenu = 1;
            int waitMainMenuLimit = 301;
            bool isloginStillDisplayed = true;
            bool isMainMenuDisplayed = false;

            while (waitMainMenu != waitMainMenuLimit)
            {
                isloginStillDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(loginButtonProgressBar_XPath)).Count > 0;
                isMainMenuDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(mainMenuXPath)).Count > 0;
                if (isloginStillDisplayed || !isMainMenuDisplayed)
                {
                    DlkLogger.LogInfo("Waiting for main menu to be displayed... [" + waitMainMenu.ToString() + "]s");
                    ShowPagePathOnLoad();
                    Thread.Sleep(1000);
                    waitMainMenu++;
                }
                else
                    break;
            }

            if (isloginStillDisplayed || !isMainMenuDisplayed)
                throw new Exception("Waiting for main menu to be displayed has reached its limit (300s). Setup failed.");

            ShowPagePathOnLoad();
            DlkLogger.LogInfo("Main page has been displayed.");
        }

        public static string GetCurrentDatabaseVersion()
        {
            string Database = "";
            string WB_Branch = "wb8.7.0_9.";
            string WB_DB_Indication = "_BuildUpdate_Release";
            string DBVersion_XPath = "//span[@class='contentBarVersion']";
            IWebElement dbaseVersion = DlkEnvironment.AutoDriver.FindElements(By.XPath(DBVersion_XPath)).Where(x=>x.Displayed).Any() ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(DBVersion_XPath)) : throw new Exception("Database version not found.");

            Database = dbaseVersion.Text.Trim();
            Database = Database.Replace("Version ", "");
            string[] d = Database.Split(' ');
            Database = d[0];
            Database = WB_Branch + Database;
            Database = Database + WB_DB_Indication;
            return Database;
        }

        #endregion

        #region PRIVATE METHODS

        private static void Login(String IsBrowserOpen, String Url, String User, String Password, String Database)
        {
            try
            {
                bool isBrowserOpen = Convert.ToBoolean(IsBrowserOpen);
                
                //Login to workbook
                if (!String.IsNullOrEmpty(User))
                {
                    DlkEnvironment.AutoDriver.Navigate().Refresh();
                    Thread.Sleep(5000);

                    //Check if alert dialogs appear during refresh
                    try
                    {
                        DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                    }
                    catch
                    {
                        //Do nothing
                    }

                    WaitForWorkBookToLoad();
                    UserPassLoginExecute(User, Password);
                    WaitForLoginToFinishLoad();
                    WaitForMainMenuDisplay();

                    DlkLogger.LogInfo("Login successful. Proceeding with test steps... ");
                }
                else
                    DlkLogger.LogInfo("No login credentials set. Login not executed... ");
            }
            catch (WebDriverException)
            {
                throw new WebDriverException();
            }
            catch (Exception e)
            {
                throw new Exception("Login setup failed: " + e.Message, e);
            }
        }

        private static int GetDateFormat(string inputDate)
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

        private static DateTime SetDateToUSCultureInfo(String dateToSet)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime dt = DateTime.Parse(dateToSet, cultureInfo); //parse date to en-US date format
            return dt;
        }

        #endregion

        #region KEYWORDS

        [Keyword("ReLogin")]
        public static void ReLogin(String User, String Pass)
        {
            try
            {
                if (String.IsNullOrEmpty(User))
                    throw new Exception("Username is empty. Provide a valid username.");
                if (String.IsNullOrEmpty(Pass))
                    throw new Exception("Password is empty. Provide a valid password.");

                WaitForWorkBookToLoad();
                UserPassLoginExecute(User, Pass);
                WaitForLoginToFinishLoad();
                WaitForMainMenuDisplay();
                DlkLogger.LogInfo("ReLogin passed()");
            }
            catch (Exception e)
            {
                throw new Exception("ReLogin() failed : " + e.Message, e);
            }
        }

        [Keyword("WaitScreenGetsReady")]
        public static void WaitScreenGetsReady()
        {
            try
            {
                DlkLogger.LogInfo("Checking for loading indicator(s) or connection timeout prompt(s)... ");

                int wait = 1;
                int waitLimit = 301;
                string loadingDialogsXPath = "//*[@class='GenericLoadingOverlay']//*[@class='Progress'] | " +
                    "//*[@class='LoadingIndicator']//svg[contains(@class,'loadingRotation')] | " +
                    "//*[@id='busyOverLay'] | " +
                    "//*[@role='dialog']//span[contains(text(),'Connection timeout')] | " +
                    "//*[@role='dialog']//span[contains(text(),'Everything is a-ok')] | " +
                    "//*[@role='dialog']//span[contains(text(),'Forbindelses timeout')] | " +
                    "//*[@role='dialog']//span[contains(text(),'Alt er iorden')]";
                bool isLoadingDialogDisplayed = true;

                while (wait != waitLimit)
                {
                    isLoadingDialogDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(loadingDialogsXPath)).Where(x => x.Displayed).Any();
                    if(isLoadingDialogDisplayed)
                    {
                        DlkLogger.LogInfo("Waiting for WorkBook to reconnect to server or finish loading... [" + wait.ToString() + "]s");
                        Thread.Sleep(1000);
                        wait++;
                        continue;
                    }
                    else
                        break;
                }

                if (isLoadingDialogDisplayed)
                    throw new Exception("Waiting for WorkBook to reconnect to server or finish loading has reached its limit (300s).");
            }
            catch (Exception e)
            {
                throw new Exception("WaitScreenGetsReady() failed : " + e.Message, e);
            }
        }

        [Keyword("ShowPagePathElement")]
        public static void ShowPagePathElement()
        {
            try
            {
                string loginButtonXPath = "//button[contains(@class,'LoginButton')]";
                IWebElement loginButton = DlkEnvironment.AutoDriver.FindElements(By.XPath(loginButtonXPath)).Where(x => x.Displayed).Any() ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(loginButtonXPath)) : null;

                if (loginButton == null)
                    throw new Exception("This function can only be executed during login page. Login page not found.");

                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("WorkBook.showNavigationPagePath = true");
                DlkLogger.LogInfo("ShowPagePathElement() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ShowPagePathElement() failed : " + e.Message, e);
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

                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
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
                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
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
                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
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
                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
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
                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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

                    DlkWorkBookTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("CreateNewFolder")]
        public static void CreateNewFolder(String FolderPath, String FolderName)
        {
            try
            {
                if (!String.IsNullOrEmpty(FolderPath) && !String.IsNullOrEmpty(FolderName))
                {
                    if (!Directory.Exists(FolderPath))
                        throw new Exception("Folder path does not exist. Provide a valid folder path.");

                    string folderPathName = Path.Combine(FolderPath, FolderName);
                    if (Directory.Exists(folderPathName))
                        DlkLogger.LogInfo("Folder already exists in provided path. Skipping CreateNewFolder() function ...");
                    else
                    {
                        Directory.CreateDirectory(folderPathName);
                        if (!Directory.Exists(folderPathName))
                            throw new Exception("Creating new folder has failed. Provide a valid folder path and name.");
                    }
                    DlkLogger.LogInfo("CreateNewFolder() passed.");
                }
                else
                    throw new Exception("Provide a valid folder path or folder name.");                
            }
            catch (Exception e)
            {
                throw new Exception("CreateNewFolder() failed : " + e.Message, e);
            }
        }

        [Keyword("RefreshPage")]
        public static void RefreshPage()
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().Refresh();

                //Check if alert dialogs appear during refresh
                try
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                }
                catch
                {
                    //Do nothing
                }

                WaitForLoginToFinishLoad();
                WaitForMainMenuDisplay();
                DlkLogger.LogInfo("RefreshPage() passed");
            }
            catch (Exception e)
            {
                throw new Exception("RefreshPage() failed : " + e.Message);
            }
        }

        [Keyword("YearToday")]
        public static void YearToday(String VariableName)
        {
            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());

            string dtValue = dtNow.ToString("yyyy");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo("Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            DlkLogger.LogInfo("YearToday() passed");
        }

        [Keyword("ExecuteDBQuery")]
        public static void ExecuteDBQuery(String Query, String Server, String Database, String User, String Password)
        {
            try
            {
                //constant variables for accessing database
                string DB_Server = !String.IsNullOrEmpty(Server) ? Server : "cphv0018.ads.deltek.com";
                string DB_Type = "mss";
                string DB_Port = "1433";
                string WB_Branch = "wb8.7.0_9.";
                string WB_DB_Indication = "_BuildUpdate_Release";

                if (String.IsNullOrEmpty(Database))
                {
                    Database = GetCurrentDatabaseVersion();
                }
                else
                {
                    if (!Database.Contains(WB_Branch) && !Database.Contains(WB_DB_Indication))
                        throw new Exception("Invalid database version.");
                }

                DlkDatabaseHandler.ExecuteQueryAssignToVariable(DB_Type, DB_Server, DB_Port, User, Password, Database, Query, "Database_Var");
                DlkLogger.LogInfo("ExecuteDBQuery() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ExecuteDBQuery() failed : " + e.Message);
            }
        }

        [Keyword("GetDBFolderName")]
        public static void GetDBFolderName(String Database, String EMorRQ, String VariableName)
        {
            try
            {
                string currentBranch = "wb8.7.0_9.";
                string WB_DB_Indication = "_BuildUpdate_Release";

                if (String.IsNullOrEmpty(Database))
                {
                    Database = GetCurrentDatabaseVersion();
                }
                else
                {
                    if (!Database.Contains(currentBranch) && !Database.Contains(WB_DB_Indication))
                        throw new Exception("Invalid database version.");
                }

                if (!String.IsNullOrEmpty(Database) && Database.Contains(currentBranch))
                {
                    string originalName;
                    string folderName;

                    //Remove unnecessary trailing characters
                    originalName = Database.Replace(currentBranch, "");
                    originalName = originalName.Replace(WB_DB_Indication, "");
                    string[] d = originalName.Split('.');

                    //Add additional zero if version is below 3-digit
                    if (d[2].Length < 3)
                        d[2] = "0" + d[2];
                    
                    switch (EMorRQ.ToLower())
                    {
                        case "em":
                            folderName = d[0] + d[1] + d[2] + "_EM";
                            break;
                        case "rq":
                            folderName = d[0] + d[1] + d[2] + "_RQ";
                            break;
                        default:
                            throw new Exception("Trailing folder name should only be 'EM' or 'RQ'");
                    }

                    DlkVariable.SetVariable(VariableName, folderName);
                    DlkLogger.LogInfo("[" + folderName + "] assigned to variable name: [" + VariableName +"]");
                }
                else
                    throw new Exception("Database not declared or invalid. Provide a valid database on environment config or as parameter.");
                
                DlkLogger.LogInfo("GetDBFolderName() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetDBFolderName() failed : " + e.Message);
            }
        }

        [Keyword("MaximizeWindow")]
        public static void MaximizeWindow()
        {
            try
            {
                DlkEnvironment.AutoDriver.Manage().Window.Maximize();
                Thread.Sleep(1000);
                DlkLogger.LogInfo("MaximizeWindow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("MaximizeWindow() failed : " + e.Message);
            }
           
        }

        [Keyword("ResizeWindow")]
        public static void ResizeWindow(String Width, String Height)
        {
            try
            {
                int width = 0;
                if (!int.TryParse(Width, out width) || width <= 0)
                    throw new Exception("[" + Width + "] is not a valid input for parameter Width.");

                int height = 0;
                if (!int.TryParse(Height, out height) || height <= 0)
                    throw new Exception("[" + Height + "] is not a valid input for parameter Heigth.");

                DlkEnvironment.AutoDriver.Manage().Window.Size = new System.Drawing.Size(width, height);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("ResizeWindow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ResizeWindow() failed : " + e.Message);
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

        [Keyword("SwitchFocusToNewTab")]
        public static void SwitchFocusToNewTab()
        {
            try
            {
                Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                mAction.SendKeys(Keys.Control + Keys.Tab).KeyUp(Keys.Control).Build().Perform();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("SwitchFocusToNewTab() passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SwitchFocusToNewTab() failed: " + ex.Message);
            }
        }

        [Keyword("SwitchFocusToPreviousTab")]
        public static void SwitchFocusToPreviousTab()
        {
            try
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
                Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                mAction.SendKeys(Keys.Control + Keys.Shift + Keys.Tab).KeyUp(Keys.Shift).KeyUp(Keys.Control).Build().Perform();
                DlkEnvironment.AutoDriver.SwitchTo().Window(previousWindow);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("SwitchFocusToPreviousTab() passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SwitchFocusToPreviousTab() failed: " + ex.Message);
            }
        }

        [Keyword("OpenNewTab")]
        public static void OpenNewTab()
        {
            try
            {
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open();");
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("OpenNewTab() passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("OpenNewTab() failed: " + ex.Message);
            }
        }

        [Keyword("CloseCurrentTab")]
        public static void CloseCurrentTab()
        {
            try
            {
                Thread.Sleep(1000);
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
                DlkLogger.LogInfo("CloseCurrentTab() passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("CloseCurrentTab() failed: " + ex.Message);
            }
        }

        #endregion
    }
}
