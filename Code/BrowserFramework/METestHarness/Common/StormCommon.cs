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
using System.Diagnostics;

namespace METestHarness.Common
{
    public static class StormCommon
    {
        #region COMMON METHODS
        public static bool Login(string EnvId, out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            int waitLoadingFinished = 300;
            try
            {

                /* Check if found */
                if (!Environments.Any(x => x.Id == EnvId))
                {
                    ErrorMessage = "Invalid environment id";
                    return false;
                }

                Environment myEnv = Environments.First(x => x.Id == EnvId);

                /* set url */
                Driver.Instance.Url = myEnv.Url;

                Control loginButton = new Control("loginBtn", "CSS", "div.btn-bar button.btn.pn-blue.primary");
                for (int i = 0; i < waitLoadingFinished; i++)
                {
                    if (!loginButton.Exists())
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Driver.SessionLogger.WriteLine(string.Format("Login button was found, page loading was finished in {0} seconds"
                            , i.ToString()), Logger.MessageType.INF);
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(myEnv.UserName))
                {
                    Control user = new Control("UserName", "ID", "userID");
                    user.SendKeys(myEnv.UserName, true);
                    Control password = new Control("Password", "ID", "password");
                    password.SendKeys(myEnv.Password, true);
                    Control dbasetext = new Control("DataBase", "xpath", "//*[@id='dbDdwn']/input[1]");
                    dbasetext.SendKeys(myEnv.Database, true);
                    Control dbListItem = new Control("ListItem", "XPATH", "//*[@class='ddwnListItem'][*[.='" + myEnv.Database + "']]");
                    dbListItem.Click();
                    Control copyRight = new Control("copyRight", "ID", "loginCopyright");
                    copyRight.Click();
                    loginButton.Click();

                    ////Wait for Navigation to appear (denotes that the main page has loaded)
                    //DlkObjectStoreFileControlRecord osRec = DlkDynamicObjectStoreHandler.GetControlRecord("Main", "Navigation");
                    //DlkSideBar sideBar = new DlkSideBar(osRec.mKey, osRec.mSearchMethod, osRec.mSearchParameters);
                    //if (!sideBar.Exists(60))
                    //{
                    //    DlkLogger.LogWarning("Login() : Main page not loaded within timeout.");
                    //}
                    //else
                    //{
                    //    Thread.Sleep(8000);
                    //    DlkLogger.LogInfo("Login() passed.");
                    //}

                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                ret = false;
            }
            return ret;
        }

        public static void WaitControlDisplayed(Control Ctrl, int WaitLoadingFinished = 300)
        {
            for (int i = 0; i < WaitLoadingFinished; i++)
            {
                if (!Ctrl.Exists())
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    Driver.SessionLogger.WriteLine(string.Format("{0} button was found, control found in {1} seconds"
                        , Ctrl.mControlName, i.ToString()), Logger.MessageType.INF);
                    break;
                }
            }
        }

        public static void WaitScreenGetsReady()
        {
            try
            {
                List<IWebElement> itemsForWait = new List<IWebElement>();

                //add spinners
                itemsForWait.AddRange(Driver.Instance.FindElements(By.XPath("//*[contains(@class,'spinner')][not(contains(@class,'spinnercontainer'))][not(contains(@class,'spinnerContainer'))][not(contains(@class,'spinner-area'))][not(contains(@class,'grid-spinner'))][not(contains(@class,'has-spinner'))][not(contains(@class,'spinner-control'))][not(contains(@class,'spinner-up'))][not(contains(@class,'spinner-down'))]")));
                //add blockers
                itemsForWait.AddRange(Driver.Instance.FindElements(By.XPath("//*[contains(@id,'application-blocker')]")));
                //add mainPageLoading = 
                itemsForWait.AddRange(Driver.Instance.FindElements(By.XPath("//*[contains(@class,'src-core-workbench-navigation-menu')]/ancestor::*[contains(@class,'ngcrm en-US')]/*[@id='applicationLoadingOverlay']")));
                //add reportViewerSpinner
                itemsForWait.AddRange(Driver.Instance.FindElements(By.XPath("//*[@id='sqlrsReportViewer_AsyncWait_Wait']")));

                long ctr = 0;
                int retryLimit = 3, maxWaitTime = 120000;

                Stopwatch mWatch = Stopwatch.StartNew();
                mWatch.Start();

                while ((mWatch.ElapsedMilliseconds < maxWaitTime) && (itemsForWait.Count(x => x.Displayed == true) > 0))
                {
                    Driver.SessionLogger.WriteLine(string.Format("Waiting for screen to completely load...", Logger.MessageType.INF));
                    Thread.Sleep(1000);
                }
                ctr += mWatch.ElapsedMilliseconds / 1000;
                mWatch.Stop();

                Driver.SessionLogger.WriteLine("Slept for " + ctr.ToString() + " seconds.");
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
        #endregion

        #region ENVIRONMENTS
        public static Environment[] Environments = new Environment[]
        {
            new Environment()
            {
                Id = "SAMPLE",
                Url = "http://makapt618vs/DeltekPS11/app/#!",
                UserName = "ADMIN",
                Password = "",
                Database = "AutoDB_FWDev_01 (MAKAPT676VS)"
            }

            ,new Environment()
            {
                Id = "SAMPLE2.0",
                Url = "http://makapt618vs/DeltekPS/app/#!",
                UserName = "ADMIN",
                Password = "",
                Database = "AutoDB_FWDev_01 (MAKAPT676VS)"
            }

            ,new Environment()
            {
                Id = "Nikka",
                Url = "http://makapt618vs/DeltekPS11/app/#!",
                UserName = "ADMIN",
                Password = "",
                Database = "AutoDB_FWDev_02 (MAKAPT676VS)"
            }

            ,new Environment()
            {
                Id = "Nikka2.0",
                Url = "http://makapt618vs/DeltekPS/app/#!",
                UserName = "ADMIN",
                Password = "",
                Database = "AutoDB_FWDev_02 (MAKAPT676VS)"
            }

            ,new Environment()
            {
                Id = "Mel_1.1",
                Url = "http://makapt618vs/DeltekPS11/app/#!",
                UserName = "ADMIN",
                Password = "",
                Database = "AutoDB_FWDev_03 (MAKAPT676VS)"
            }

            ,new Environment()
            {
                Id = "Mel_2.0",
                Url = "http://makapt618vs/DeltekPS/app/#!",
                UserName = "ADMIN",
                Password = "",
                Database = "AutoDB_FWDev_03 (MAKAPT676VS)"
            }
        };
        #endregion
    }

    public class StormEnvironment
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}
