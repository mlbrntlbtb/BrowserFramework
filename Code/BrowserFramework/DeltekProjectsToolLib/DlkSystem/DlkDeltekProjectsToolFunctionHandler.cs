using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System.Diagnostics;

namespace DeltekProjectsToolLib.DlkSystem
{
    [ControlType("Function")]
    public static class DlkDeltekProjectsToolFunctionHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public const string DEFAULT_WAIT_TIME = "40";
        public static void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "WaitScreenGetsReady":
                        WaitScreenGetsReady(DEFAULT_WAIT_TIME);
                        break;
                    case "GoToUrl":
                        GoToUrl(Parameters[0]);
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
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }

        [Keyword("WaitScreenGetsReady")]
        public static void WaitScreenGetsReady(String WaitTimeInSeconds)
        {
            try
            {
                List<IWebElement> topBar = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='wMnuTitle']")).ToList();
                List<IWebElement> loadingDialog = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='appriseTitle']")).Where(x => x.Text == "Replicating Costpoint Projects").ToList();
                int maxWaitTime = int.Parse(WaitTimeInSeconds) * 1000;
                Stopwatch mWatch = Stopwatch.StartNew();
                mWatch.Start();
                while ((mWatch.ElapsedMilliseconds < maxWaitTime) && ((topBar.Count() == 0) || (loadingDialog.Count() > 0)))
                {
                    DlkLogger.LogInfo("Waiting for page to load completely...");
                    Thread.Sleep(1000);
                    topBar = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='wMnuTitle']")).ToList();
                    loadingDialog = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='appriseTitle']")).Where(x => x.Text == "Replicating Costpoint Projects").ToList();
                }
                mWatch.Stop();
                if (topBar.Count(x => x.Displayed == true) > 0 && loadingDialog.Count == 0)
                {
                    DlkLogger.LogInfo("Page load completed, topmost bar found, loading dialog not found");
                }
                else if (topBar.Count(x => x.Displayed == true) == 0)
                {
                    DlkLogger.LogInfo("Page still not loaded. Maximum wait time of " + (maxWaitTime).ToString() + " seconds reached. Executing next step...");
                }
                else if (loadingDialog.Count > 0)
                {
                    DlkLogger.LogInfo("Loading dialog still on screen. Maximum wait time of " + (maxWaitTime).ToString() + " seconds reached. Executing next step...");
                }
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

        [Keyword("GoToUrl")]
        public static void GoToUrl(string URL)
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().GoToUrl(URL);
            }
            catch (Exception ex)
            {
                throw new Exception("GoToUrl() failed: " + ex.Message);
            }
        }

        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
        }
    }
}
