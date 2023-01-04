using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using METestHarness.Sys;
using System.Reflection;

namespace METestHarness.Common
{
    public static class CPCommon
    {
        static int INT_LOGIN_TIMEOUT = 60;
        static int INT_TARGET_URL_LOAD_TIMEOUT = 120;
        private const int INT_SHORT_WAIT_SEC = 3;
        private const int mLongWaitMs = 900000;

        public static string CurrentComponent = string.Empty;

        #region COMMON METHODS
        public static bool Login(string EnvId, out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            Environment myEnv = Environments.First(x => x.Id == EnvId);

            Driver.Instance.Url = myEnv.Url;
            int pageLoadThreshold = 0;
           
            Control loginButton = new Control("loginBtn", "id", "loginBtn");
            while (!loginButton.Exists() && ++pageLoadThreshold <= INT_TARGET_URL_LOAD_TIMEOUT)
            {
                
                Driver.SessionLogger.WriteLine("Waiting for " + Driver.Instance.Url + " to load... " + pageLoadThreshold + "s");
                Thread.Sleep(300);
                if (pageLoadThreshold % 60 == 0)
                {
                    Driver.SessionLogger.WriteLine("Page did not load correctly after " + pageLoadThreshold + "s. Reloading " + Driver.Instance.Url + "...", Logger.MessageType.INF);
                    Driver.Instance.FindElement(By.TagName("html")).SendKeys(Keys.F5);
                }
            }
            if (Driver.Instance.FindElements(By.Id("loginBtn")).Count == 0 && pageLoadThreshold >= INT_TARGET_URL_LOAD_TIMEOUT)
            {
                ret = false;
                throw new Exception("Login cannot proceed. " + Driver.Instance.Url
                    + " did not load as expected after " + INT_TARGET_URL_LOAD_TIMEOUT + "s. Please refer to the error image.");
            }
            try
            {
                try
                {
                    Control ShowAdditionalCriteria = new Control("ShowAdditionalCriteria", "id", "showAdditionalCriteria");
                    ShowAdditionalCriteria.Click();
                }
                catch
                {
                    // swallow. no big deal if not clicked
                }

                Control UserID = new Control("UserID", "id", "USER");
                UserID.SendKeys(myEnv.UserName);

                Control Password = new Control("Password", "id", "CLIENT_PASSWORD");
                Password.SendKeys(myEnv.Password);

                Control System = new Control("System", "id", "DATABASE");
                System.SendKeys(myEnv.Database);

                loginButton.Click();
               
                Control errorHeader = new Control("Error", "ID", "errMsgHeader");
                if (errorHeader.Exists(1))
                {
                    Control errorText = new Control("Error", "ID", "errMsgText");
                    throw new Exception(errorText.GetValue());
                }
                Driver.SessionLogger.WriteLine("Successfully performed Login steps...", Logger.MessageType.INF);
                WaitLoadingFinished();
            }
            catch (Exception e)
            {
                ret = false;
                throw new Exception("Login cannot proceed. " + e.Message);
            }

            Control navCtl = new Control("NavMenu", "ID", "navCont");
            try
            {
                navCtl.FindElement(INT_LOGIN_TIMEOUT);
            }
            catch
            {
                throw new Exception("Costpoint took too long to Login. The app navigation menu was not reached after "
                    + INT_LOGIN_TIMEOUT + "s. Please refer to the error image.");
            }
            
            return ret;
        }

        public static void WaitControlDisplayed(Control Ctrl, int WaitLoadingFinished = 300)
        {
            for (int i = 0; i < WaitLoadingFinished; i++)
            {
                if (!Ctrl.Exists(1))
                {
                    // do nothing: Exist is already 1 second wait
                }
                else
                {
                    Driver.SessionLogger.WriteLine(string.Format("{0} found, control found in {1} seconds."
                        , Ctrl.mControlName, i.ToString()), Logger.MessageType.INF);
                    break;
                }
            }
        }

        public static void WaitLoadingFinished(Boolean IsComponentModal = false)
        {
            String WaitImageCSS = "*.pleaseWaitImage";
            //String WaitLabelCSS = "*.titleLblCover";
            Boolean bExist = false;
            IWebElement loadingImage = null;
            //IWebElement loadingLabel = null;
            IWebElement modalHider = null;

            try
            {
                Driver.SessionLogger.WriteLine("Checking if page is loading...", Logger.MessageType.INF);
                if (!DoesAlertExist(1))
                {
                    if (Driver.Instance.FindElements(By.CssSelector(WaitImageCSS)).Count > 0)
                    {
                        loadingImage = Driver.Instance.FindElements(By.CssSelector(WaitImageCSS)).First();
                        modalHider = Driver.Instance.FindElements(By.ClassName("modalHider")).First();
                    }

                    if (IsComponentModal)
                    {
                        // Modal fixed wait
                        Driver.SessionLogger.WriteLine("Modal component fixed wait initiated...", Logger.MessageType.INF);
                        Thread.Sleep(3000);
                        Driver.SessionLogger.WriteLine("Modal component fixed wait finished.", Logger.MessageType.INF);
                        Driver.SessionLogger.WriteLine("Ignoring page loading state.", Logger.MessageType.INF);
                        return;
                    }

                    for (int i = 0; loadingImage != null && i < mLongWaitMs / 1000; i++)
                    {
                        Thread.Sleep(1000);
                        if (loadingImage.GetCssValue("visibility") == "visible")
                        {
                            Driver.SessionLogger.WriteLine("Page is still loading", Logger.MessageType.INF);
                            bExist = true;
                            //Thread.Sleep(DlkEnvironment.MediumWaitMs);
                        }
                        else
                        {
                            if (!IsComponentModal && modalHider.GetCssValue("display") != "none" && !IsModalFormDisplayed())
                            {
                                bExist = true;
                                continue;
                            }
                            bExist = false;
                            break;
                        }
                    }
                    if (bExist)
                    {
                        Driver.SessionLogger.WriteLine("Page still loading.", Logger.MessageType.WRN);
                    }
                    else
                    {
                        Driver.SessionLogger.WriteLine("Page finished loading.", Logger.MessageType.INF);
                    }
                }
            }
            catch (Exception e)
            {
                // do nothing
                Driver.SessionLogger.WriteLine("WaitLoadingFinished() threw an unexpected exception. Logging call stack for debugging purposes...", Logger.MessageType.WRN);
                Driver.SessionLogger.WriteLine("Exception Message: " + e.Message, Logger.MessageType.WRN);
                Driver.SessionLogger.WriteLine("Exceeption Call Stack: " + e.StackTrace, Logger.MessageType.WRN);
            }
        }

        public static void WaitProcessProgressFinished(int Timeout)
        {
            Control ProcessProgress_OK = new Control("OK", "ID", "progMtrBtn");
            for (int i = 0; i < Timeout; i++)
            {
                Thread.Sleep(1000);
                if (ProcessProgress_OK.GetValue().ToLower() != "ok")
                {
                    Driver.SessionLogger.WriteLine("OK button not yet visible. Waiting...", Logger.MessageType.INF);
                    continue;
                }
                Driver.SessionLogger.WriteLine("Successfully executed WaitForProcessFinished().", Logger.MessageType.INF);
                return;
            }
            Driver.SessionLogger.WriteLine("WaitForProcessFinished() has timed out.", Logger.MessageType.INF);
        }

        public static Boolean DoesAlertExist(int iTimeInSecs)
        {
            Boolean bExist = false;
            IAlert mAlert;
            for (int i = 1; i <= iTimeInSecs; i++)
            {
                try
                {
                    mAlert = Driver.Instance.SwitchTo().Alert();
                    if (mAlert.Text.Length > 0)
                    {
                        bExist = true;
                        break;
                    }
                }
                catch
                {
                    // do nothing...
                }
                Thread.Sleep(1000);
            }
            return bExist;
        }

        public static void AssertEqual(string Expected, string Actual, bool IsCaseSensitive = true)
        {
            if(IsCaseSensitive)
            {
                if(!Expected.Equals(Actual))
                {
                    throw new Exception("Expected Result [" + Expected + "] is NOT equal to Actual Result [" + Actual + "].");
                }
            }
            else
            {
                if (!Expected.Equals(Actual, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("Expected Result [" + Expected + "] is NOT equal to Actual Result [" + Actual + "].");
                }
            }

             Driver.SessionLogger.WriteLine("Expected Result [" + Expected + "] is equal to Actual Result [" + Actual + "].", Logger.MessageType.INF);
        }

        public static void AssertEqual(bool Expected, bool Actual)
        {
            if(Expected != Actual)
            {
                throw new Exception("Expected Result [" + Expected + "] is NOT equal to Actual Result [" + Actual + "].");
            }

            Driver.SessionLogger.WriteLine("Expected Result [" + Expected + "] is equal to Actual Result [" + Actual + "].", Logger.MessageType.INF);
        }

        public static bool IsCurrentComponentModal(bool isMessageArea = false)
        {
            bool ret = false;
            switch (CurrentComponent.ToLower())
            {
                case "query":
                case "printoptions":
                case "processprogress":
                case "dialog":
                case "fileuploadmanager":
                    ret = true;
                    break;
                case "cp7main":
                    switch (isMessageArea)
                    {
                        case true:
                            ret = true;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return ret;
        }

        public static void SendKeys(String Keys)
        {
            String[] arrKeys = Keys.Split('~');
            String strKeyToSend = String.Empty;
            try
            {
                Type typeOfKeys = typeof(OpenQA.Selenium.Keys);

                // To handle special case of 'Ctrl+~/Ctrl~~' for shortcut key of Next tab in Costpoint/TE
                if (Keys.Contains("~~"))
                {
                    FieldInfo fld = typeOfKeys.GetField(arrKeys[0]);
                    if (fld != null)
                    {
                        strKeyToSend += fld.GetValue(typeOfKeys).ToString();
                        strKeyToSend += "`".ToLower();
                    }
                }
                else
                {
                    // parse input Keys and convert to Selenium.Keys equivalent
                    for (int i = 0; i < arrKeys.Count(); i++)
                    {
                        FieldInfo fld = typeOfKeys.GetField(arrKeys[i]);

                        if (fld != null) // key is a special key, Tab, Control, etc.
                        {
                            strKeyToSend += fld.GetValue(typeOfKeys).ToString();
                        }
                        else // key is a standard input key, A, 2, etc.
                        {
                            strKeyToSend += arrKeys[i].ToLower();
                        }
                    }
                }
                Driver.SessionLogger.WriteLine("SendKeys() : keys to send = " + strKeyToSend, Logger.MessageType.INF);
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(Driver.Instance);
                mAction.SendKeys(strKeyToSend).Build().Perform();
                Driver.SessionLogger.WriteLine("Successfully executed SendKeys()", Logger.MessageType.INF);
            }
            catch (Exception e)
            {
                throw new Exception("SendKeys() failed : " + e.Message, e);
            }
        }

        public static void Wait(int WaitTime)
        {
          
            for (int cnt = 1; cnt <= WaitTime; cnt++)
            {
                Thread.Sleep(1000);
                Driver.SessionLogger.WriteLine("Wait() : Waiting ... " + cnt + " sec elapsed", Logger.MessageType.INF);
            }
            
        }

        public static void ClickOkDialogIfExists(String ExpectedAlertText)
        {
            if (DoesAlertExist(INT_SHORT_WAIT_SEC) && VerifyAlertText(ExpectedAlertText))
            {
                Driver.Instance.SwitchTo().Alert().Accept();
                Driver.SessionLogger.WriteLine("Successfully executed Accept() on alert.", Logger.MessageType.INF);
            }
            else
            {
                Driver.SessionLogger.WriteLine("No alert exists.", Logger.MessageType.INF);
            }
        }

        public static void ClickOkDialogWithMessage(String Message)
        {
            if (DoesAlertExist(INT_SHORT_WAIT_SEC) && VerifyAlertText(Message))
            {
                Driver.Instance.SwitchTo().Alert().Accept();
                Driver.SessionLogger.WriteLine("Successfully executed Accept() on alert.", Logger.MessageType.INF);
            }
            else
            {
                throw new Exception("No alert with " + Message + " exists.");
            }
        }

        public static bool VerifyAlertText(String ExpectedAlertText)
        {
            String ActAlertText = "";
            if (DoesAlertExist(INT_SHORT_WAIT_SEC))
            {
                ActAlertText = System.Text.RegularExpressions.Regex.Replace(Driver.Instance.SwitchTo().Alert().Text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "), @"\s+", " ");
            }
            return ExpectedAlertText == ActAlertText;
        }

        public static void ScrollDown()
        {
            try
            {
                ((OpenQA.Selenium.Remote.RemoteWebDriver)Driver.Instance).ExecuteScript("scroll(0,20000)");
                Driver.SessionLogger.WriteLine("Successfully executed ScrollDown()", Logger.MessageType.INF);
            }
            catch (Exception e)
            {
                throw new Exception("ScrollDown() failed : " + e.Message);
            }
        }

        public static String ReplaceCarriageReturn(String InputString, String ReplacementString)
        {
            String sResult = "";
            sResult = InputString.Replace("\n\r", ReplacementString);
            sResult = sResult.Replace("\r\n", ReplacementString);
            sResult = sResult.Replace("\n", ReplacementString);
            sResult = sResult.Replace("\r", ReplacementString);
            return sResult;
        }
        #endregion

        #region ENVIRONMENTS
        public static Environment[] Environments = new Environment[]
        {
                new Environment()
                {
                    Id = "default",
                    Url = "http://makapp02/cpweb",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "C71MQCO11Q1"
                }

                ,new Environment()
                {
                    Id = "CP_DAILY_C71MQCO12AEHQ",
                    Url = "http://makapp02/cpweb/",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER123",
                    Database = "C71MQCO12AEHQ"
                }

                ,new Environment()
                {
                    Id = "CP_DAILY_C71MQCM16AE",
                    Url = "http://makapp02/cpweb/",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "C71MQCM16AE"
                }

                ,new Environment()
                {
                    Id = "CP_STAGING_C71STAGEM",
                    Url = "http://ashapt577vs:7009/",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "C71STAGEM"
                }

                ,new Environment()
                {
                    Id = "CP_STAGING_C71STAGEO",
                    Url = "http://ashapt577vs:7009/",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "C71STAGEO"
                }

                ,new Environment()
                {
                    Id = "TE_DAILY_TE10QCO12AUTOTEST",
                    Url = "http://ashapt62/cpweb/ ",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "TE10QCO12AUTOTEST"
                }

                ,new Environment()
                {
                    Id = "TE_DAILY_T10QCM16AUTOTEST",
                    Url = "http://ashapt62/cpweb/ ",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "T10QCM16AUTOTEST"
                }

                ,new Environment()
                {
                    Id = "TE_REG_MSS_TE10AUTOTEST",
                    Url = "http://ASHAPT38VS:7009/cpweb/",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "TE10AUTOTEST"
                }

                ,new Environment()
                {
                    Id = "TE_REG_ORA_TE10AUTOTEST",
                    Url = "http://ASHAPT22VS:7009/cpweb/",
                    UserName = "CPSUPERUSER",
                    Password = "CPSUPERUSER",
                    Database = "TE10AUTOTEST"
                }


                /* Copy and uncomment below to add new env. Do not delete this block, just copy */

                //,new Environment()
                //{
                //    Id = "NEW",
                //    Url = "",
                //    UserName = "",
                //    Password = "",
                //    Database = ""
                //}
        };
        #endregion

        #region PRIVATE METHODS
        private static bool IsModalFormDisplayed()
        {
            IWebElement qry = Driver.Instance.FindElements(By.Id("qryFrm")).First();
            IWebElement printOptions = Driver.Instance.FindElements(By.Id("printSetupForm")).First();
            IWebElement pageSetup = Driver.Instance.FindElements(By.Id("pageSetupForm")).First();
            IWebElement processProgress = Driver.Instance.FindElements(By.Id("progMtrDiv")).First();

            if (qry.GetCssValue("visibility") == "visible")
            {
                return true;
            }
            if (printOptions.GetCssValue("visibility") == "visible")
            {
                return true;
            }
            if (pageSetup.GetCssValue("visibility") == "visible")
            {
                return true;
            }
            if (pageSetup.GetCssValue("display") == "block")
            {
                return true;
            }
            return false;
        }
        #endregion
    }

    public class Environment
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}
