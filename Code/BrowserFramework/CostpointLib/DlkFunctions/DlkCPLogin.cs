using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CostpointLib.System;
using CostpointLib.DlkUtility;
using CommonLib.DlkControls;
using OpenQA.Selenium.Remote;

namespace CostpointLib.DlkFunctions
{
    [Component("CP7Login")]
    public static class DlkCP7Login
    {
        static int INT_LOGIN_TIMEOUT = 60;
        static int INT_TARGET_URL_LOAD_TIMEOUT = 120;

        [Keyword("Login", new string[] {"1|text|User|CPSUPERUSER",
                                        "2|text|Password|CPSUPERUSER",
                                        "3|text|System|OCP71Q1"})]
        public static void Login(string sUser, string sPassword, string sSystem)
        {
            int pageLoadThreshold = 0;
            while (DlkEnvironment.AutoDriver.FindElements(By.Id("loginBtn")).Count == 0 && ++pageLoadThreshold <= INT_TARGET_URL_LOAD_TIMEOUT)
            {
                DlkLogger.LogInfo("Waiting for " + DlkEnvironment.AutoDriver.Url + " to load... " + pageLoadThreshold + "s");
                Thread.Sleep(300);
                if (pageLoadThreshold % 60 == 0)
                {
                    DlkLogger.LogWarning("Page did not load correctly after " + pageLoadThreshold + "s. Reloading " + DlkEnvironment.AutoDriver.Url + "...");
                    DlkEnvironment.AutoDriver.FindElement(By.TagName("html")).SendKeys(Keys.F5);
                }
            }
            if (DlkEnvironment.AutoDriver.FindElements(By.Id("loginBtn")).Count == 0 && pageLoadThreshold >= INT_TARGET_URL_LOAD_TIMEOUT)
            {
                throw new Exception("Login cannot proceed. " + DlkEnvironment.AutoDriver.Url
                    + " did not load as expected after " + INT_TARGET_URL_LOAD_TIMEOUT + "s. Please refer to the error image.");
            }
            try
            {
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "ShowAdditionalCriteria", "Click", new String[] { "" });
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "UserID", "Set", new String[] { sUser });
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "Password", "Set", new String[] { sPassword });
                if (!string.IsNullOrEmpty(sSystem))
                {
                    DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "System", "Set", new String[] { sSystem });
                }
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "Login", "Click", new String[] { "" });

                DlkBaseControl errorHeader = new DlkBaseControl("Error", "ID", "errMsgHeader");
                if (errorHeader.Exists(1))
                {
                    DlkBaseControl errorText = new DlkBaseControl("Error", "ID", "errMsgText");
                    throw new Exception(errorText.GetValue());
                }
                DlkLogger.LogInfo("Successfully performed Login steps...");

                // For security page of Leidos
                DlkBaseControl acknowledgeBtn = new DlkBaseControl("AckButton", "ID", "ackBtn");
                if (acknowledgeBtn.Exists(3))
                {
                    acknowledgeBtn.Click();
                    DlkLogger.LogInfo("Successfully clicked Acknowledge button.");
                }

                DlkCostpointCommon.WaitLoadingFinished();
            }
            catch (Exception e)
            {
                throw new Exception("Login cannot proceed. " + e.Message);
            }

            DlkBaseControl navCtl = new DlkBaseControl("NavMenu", "ID", "navCont");
            try
            {
                navCtl.FindElement(INT_LOGIN_TIMEOUT);
            }
            catch
            {
                throw new Exception("Costpoint took too long to Login. The app navigation menu was not reached after "
                    + INT_LOGIN_TIMEOUT + "s. Please refer to the error image.");
            }
        }

        /// <summary>
        /// Verify login error message
        /// </summary>
        /// <param name="ErrorMessage">Error message</param>
        [Keyword("VerifyLoginErrorMessage", new string[] {"1|text|Invalid login"})]
        public static void VerifyLoginErrorMessage(string ErrorMessage)
        {
            try 
            {
                IWebElement errorMsgText = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='errorMessageDiv']/div[@id='errMsgText']")).FirstOrDefault();

                if (errorMsgText != null)
                {
                    DlkAssert.AssertEqual("VerifyLoginErrorMessage()", ErrorMessage.ToLower(), errorMsgText.Text.ToLower());
                    DlkLogger.LogInfo("VerifyLoginErrorMessage() : Passed.");
                }
                else
                {
                    throw new Exception("Login error message not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLoginErrorMessage() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verify part of login error message
        /// </summary>
        /// <param name="ErrorMessage">Part of error message</param>
        [Keyword("VerifyPartialLoginErrorMessage", new string[] { "1|text|Invalid login" })]
        public static void VerifyPartialLoginErrorMessage(string ErrorMessage)
        {
            try
            {
                IWebElement errorMsgText = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='errorMessageDiv']/div[@id='errMsgText']")).FirstOrDefault();

                if (errorMsgText != null)
                {
                    if (errorMsgText.Text.ToLower().Contains(ErrorMessage.ToLower()))
                    {
                        DlkLogger.LogInfo("VerifyPartialLoginErrorMessage() : Passed.");
                    }
                    else
                    {
                        throw new Exception("Actual login error text [" + errorMsgText.Text + "] does not contain [" + ErrorMessage + "].");
                    }
                }
                else
                {
                    throw new Exception("Login error message not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialLoginErrorMessage() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Login Test for connection test purposes
        /// Version of login routine with shorter tolerance for unresponsive application server
        /// Exclusive for Costpoint FW for external client purposes
        /// </summary>
        /// <param name="sUser">User ID</param>
        /// <param name="sPassword">Password (Unencrypted)</param>
        /// <param name="sSystem">Database</param>
        /// <param name="pageLoadWaitInSec">Optional wait time when reaching URL</param>
        /// <param name="navMenuLoadWait">Optional wait time when login page transitions to main page</param>
        public static void TestLoginConnection(string sUser, string sPassword, string sSystem, int pageLoadWaitInSec = 5, int navMenuLoadWait = 10)
        {
            int pageLoadThreshold = 0;
            while (DlkEnvironment.AutoDriver.FindElements(By.Id("loginBtn")).Count == 0 && ++pageLoadThreshold <= pageLoadWaitInSec)
            {
                DlkLogger.LogInfo("Waiting for " + DlkEnvironment.AutoDriver.Url + " to load... " + pageLoadThreshold + "s");
                Thread.Sleep(1000);
            }
            if (DlkEnvironment.AutoDriver.FindElements(By.Id("loginBtn")).Count == 0 && pageLoadThreshold >= pageLoadWaitInSec)
            {
                throw new Exception("Login cannot proceed. " + DlkEnvironment.AutoDriver.Url
                    + " did not load as expected after " + INT_TARGET_URL_LOAD_TIMEOUT + "s. Please refer to the error image.");
            }
            try
            {
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "ShowAdditionalCriteria", "Click", new String[] { "" });
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "UserID", "Set", new String[] { sUser });
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "Password", "Set", new String[] { sPassword });
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "System", "Set", new String[] { sSystem });
                DlkCostpointKeywordHandler.ExecuteKeyword("CP7Login", "Login", "Click", new String[] { "" });

                DlkBaseControl errorHeader = new DlkBaseControl("Error", "ID", "errMsgHeader");
                if (errorHeader.Exists(1))
                {
                    DlkBaseControl errorText = new DlkBaseControl("Error", "ID", "errMsgText");
                    throw new Exception(errorText.GetValue());
                }
                DlkLogger.LogInfo("Successfully performed Login steps...");

                // For security page of Leidos
                DlkBaseControl acknowledgeBtn = new DlkBaseControl("AckButton", "ID", "ackBtn");
                if (acknowledgeBtn.Exists(3))
                {
                    acknowledgeBtn.Click();
                    DlkLogger.LogInfo("Successfully clicked Acknowledge button.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Login cannot proceed. " + e.Message);
            }
            DlkBaseControl navCtl = new DlkBaseControl("NavMenu", "ID", "navCont");
            try
            {
                navCtl.FindElement(navMenuLoadWait);
            }
            catch
            {
                throw new Exception("Costpoint took too long to Login. The app navigation menu was not reached after "
                    + navMenuLoadWait + "s. Please refer to the error image.");
            }
        }

        /// <summary>
        /// Set user interface preference
        /// </summary>
        /// <param name="userInterface">UI preference</param>
        public static void SetUserInterface(string userInterface)
        {
            /* Choose Interace or Do nothing */
            switch (userInterface.ToLower())
            {
                case "new":
                    DlkBaseControl newRadio = new DlkBaseControl("new", "ID", "newui");
                    if (newRadio.Exists())
                    {
                        newRadio.Click();
                        DlkLogger.LogInfo("User Interface set to NEW VERSION");
                    }
                    break;
                case "classic":
                    DlkBaseControl classicRadio = new DlkBaseControl("classic", "ID", "oldui");
                    if (classicRadio.Exists())
                    {
                        classicRadio.Click();
                        DlkLogger.LogInfo("User Interface set to CLASSIC VERSION");
                    }
                    break;
                case "":
                default:
                    DlkLogger.LogInfo("No action taken to set User Interface.");
                    break;
            }
        }
    }
}
