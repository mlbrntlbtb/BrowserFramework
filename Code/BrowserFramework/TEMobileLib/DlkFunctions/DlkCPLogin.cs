using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using TEMobileLib.System;
using TEMobileLib.DlkUtility;
using CommonLib.DlkControls;
using OpenQA.Selenium.Remote;
using CommonLib.DlkRecords;
using TEMobileLib.DlkControls;
using CommonLib.DlkHandlers;
using OpenQA.Selenium.Support.UI;

namespace TEMobileLib.DlkFunctions
{
    [Component("CP7Login")]
    public static class DlkCP7Login
    {
        static int INT_LOGIN_TIMEOUT = 60;
        static int INT_TARGET_URL_LOAD_TIMEOUT = 120;

        /// <summary>
        /// Automated pin setup.
        /// </summary>
        private static void AutomatedPinSetup(string pin)
        {
            try
            {
                //get label control record
                DlkObjectStoreFileControlRecord enter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Enter");
                //construct a control from the record
                DlkButton lblEnter = new DlkButton("Enter", enter.mSearchMethod, enter.mSearchParameters);
                //get label control record
                DlkObjectStoreFileControlRecord reEnter = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "ReEnter");
                //construct a control from the record
                DlkButton lblReEnter = new DlkButton("ReEnter", reEnter.mSearchMethod, reEnter.mSearchParameters);
                DlkLogger.LogInfo("Attempting to configure PIN...");


                if (lblEnter.Exists())
                {
                    DlkLogger.LogInfo("Configuring PIN: " + pin);
                    EnterPIN(pin);
                }

                //ReEnter
                if (lblReEnter.Exists())
                {
                    DlkLogger.LogInfo("Configuring Re-Enter PIN: " + pin);
                    EnterPIN(pin);
                }

                DlkLogger.LogInfo("Automated PIN setup was successful!");

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void EnterPIN(String pin)
        {
            var customizedPin = pin.ToCharArray();

            foreach (var p in customizedPin)
            {
                //store Key control record in PINSetup screen from Object store file to a local variable
                DlkObjectStoreFileControlRecord mKey = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key" + p);
                //construct a control from the record
                DlkButton btnKey = new DlkButton("Key" + p, mKey.mSearchMethod, mKey.mSearchParameters);
                btnKey.Click();
            }

            DlkObjectStoreFileControlRecord check = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Check");
            DlkButton checkBtn = new DlkButton("Check", check.mSearchMethod, check.mSearchParameters);
            checkBtn.Click();
        }

        [Keyword("Login", new string[] {"1|text|User|CPSUPERUSER",
                                        "2|text|Password|CPSUPERUSER",
                                        "3|text|System|OCP71Q1"})]
        public static void Login(String sUser, String sPassword, String sSystem, String Url="", String Pin="")
        {
            int pageLoadThreshold = 0;

            DlkTEMobileCommon.WaitForScreenToLoad();

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
                //This is to retrieve build number and other mobile settings to be added to custom info
                GetMobileInfo(sSystem);
              
                DlkBaseControl mobileCriteriaLink = new DlkBaseControl("MobileLink", "ID", "extraParmsLink");

                if (mobileCriteriaLink.Exists())
                {
                    mobileCriteriaLink.Click();
                }
                else
                {
                    DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "ShowAdditionalCriteria", "Click", new String[] { "" });
                }

                DlkTEMobileCommon.WaitLoadingFinished(false, true); //wait for screen to load first before checking for errors/pin setup
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "SmartphoneMode", "Set", new String[] { "TRUE" });
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "UserID", "Set", new String[] { sUser });
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "Password", "Set", new String[] { sPassword });

                if (!string.IsNullOrEmpty(sSystem))
                {
                    DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "System", "Set", new String[] { sSystem });
                }

                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(1000);//add pause

                DlkTEMobileCommon.WaitLoadingFinished(false, true); //wait for screen to load first before checking for errors
                DlkBaseControl errorHeader = new DlkBaseControl("Error", "ID", "errMsgHeader");

                if (errorHeader.Exists())
                {
                    DlkBaseControl errorText = new DlkBaseControl("Error", "ID", "errMsgText");
                    throw new Exception(errorText.GetValue());
                }

                DlkObjectStoreFileControlRecord mKey1 = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "Key1");
                DlkButton btnKey1 = new DlkButton("Key1", mKey1.mSearchMethod, mKey1.mSearchParameters);

                DlkTEMobileCommon.WaitLoadingFinished(false, true); //wait for screen to load first before checking for pin setup
                if (btnKey1.ExistsAndEnabled(2))
                {
                    DlkLogger.LogInfo("Landing page was PIN setup screen.");
                    DlkObjectStoreFileControlRecord skipPin = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("PINSetup", "SkipPIN");
                    DlkButton btnSkipPin = new DlkButton("Key1", skipPin.mSearchMethod, skipPin.mSearchParameters);
                    btnSkipPin.Click();

                    // AutomatedPinSetup(Pin); --- Enter PIN when PIN is successful.
                }

                DlkLogger.LogInfo("Successfully performed Login steps...");
                DlkTEMobileCommon.WaitLoadingFinished(false, true);
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
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "ShowAdditionalCriteria", "Click", new String[] { "" });
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "UserID", "Set", new String[] { sUser });
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "Password", "Set", new String[] { sPassword });
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "System", "Set", new String[] { sSystem });
                DlkTEMobileKeywordHandler.ExecuteKeyword("CP7Login", "Login", "Click", new String[] { "" });

                DlkBaseControl errorHeader = new DlkBaseControl("Error", "ID", "errMsgHeader");
                if (errorHeader.Exists(1))
                {
                    DlkBaseControl errorText = new DlkBaseControl("Error", "ID", "errMsgText");
                    throw new Exception(errorText.GetValue());
                }
                DlkLogger.LogInfo("Successfully performed Login steps...");
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
                    IWebElement newRadio = DlkEnvironment.AutoDriver.FindElements(By.Id("newui")).FirstOrDefault();
                    if (newRadio != null)
                    {
                        newRadio.Click();
                        DlkLogger.LogInfo("User Interface set to NEW VERSION");
                    }
                    break;
                case "classic":
                    IWebElement classicRadio = DlkEnvironment.AutoDriver.FindElements(By.Id("oldui")).FirstOrDefault();
                    if (classicRadio != null)
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

        /// <summary>
        /// Retrieve build number of Costpoint and other mobile settings to be then added to custom info
        /// </summary>
        /// <param name="system">Database used</param>
        public static void GetMobileInfo(string system)
        {
            if (DlkEnvironment.CustomInfo.Count == 0)
            {
                try
                {
                    DlkMobileRecord mobileDevice = DlkMobileHandler.GetRecord(DlkEnvironment.mBrowserID);
                    string buildversion = DlkEnvironment.AutoDriver.FindElement(By.XPath("//input[@id='buildNum']")).GetAttribute("value");

                    if (mobileDevice != null)
                    {
                        DlkEnvironment.CustomInfo.Add("buildversion", new string[] { "Build Version", buildversion.Remove(buildversion.Length - 2) });
                        DlkEnvironment.CustomInfo.Add("database", new string[] { "Database", system });
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
                catch
                {
                    DlkLogger.LogInfo("Failed to pull out data");
                }
            }
        }

    }
}
