#define NATIVE_MAPPING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;

namespace CPTouchLib.DlkUtility
{
    public static class DlkCPTouchCommon
    {
        private static string oktaScreenXpath = "//*[contains(@resource-id, 'okta-sign-in')] | //*[contains(@resource-id, 'displayName')]";
        private static string pingScreenXpath = "//*[contains(@resource-id, 'ping-content')] | //*[contains(@resource-id, 'loginHeader')]";
        private static string adfsScreenXpath = " //*[contains(@resource-id, 'loginArea')] | //*[@name='main' and @label='main']";
        private static string azureScreenXpath = "//XCUIElementTypeTextField | XCUIElementTypeButton";

        public static void WaitForSpinnerToFinishLoading(int Timeout)
        {
            bool found = false;

#if NATIVE_MAPPING
            DlkEnvironment.mLockContext = false;
            DlkEnvironment.SetContext("WEBVIEW");
#endif
            for (int sleep = 1; sleep <= Timeout; sleep++)
            {
#if NATIVE_MAPPING
                DlkBaseControl spin = new DlkBaseControl("Wait", "XPATH", "//*[contains(@class, 'loading-text') and contains(@style, 'display: block')]");
#else
                 DlkBaseControl spin = new DlkBaseControl("Wait", "XPATH", "//*[@text='Please Wait']");
#endif
                if (spin.Exists(1))
                {
                    if (!IsOnThirdPartyScreen())
                    {
                        //if current spinner is visible, sleep 1 sec  
                        DlkLogger.LogInfo("Waiting for page to load... " + sleep + "s");
                        found = true;
#if NATIVE_MAPPING
                        DlkEnvironment.SetContext("WEBVIEW");//set back to webview
#endif
                    }
                    else
                    {
                        break;//exit loading for 3rd party screens as the loading icon is still considered shown even while hidden
                    }
                }
                else
                {
                    if (found)
                    {
                        Thread.Sleep(DlkEnvironment.mMediumLongWaitMs);
                    }
                    break;
                }
            }
#if NATIVE_MAPPING
            DlkEnvironment.SetContext("NATIVE");
#endif
        }

        private static bool IsOnThirdPartyScreen()
        {
            DlkEnvironment.SetContext("NATIVE");

            bool isThirdParty = DlkEnvironment.AutoDriver.FindElements(By.XPath($"{ pingScreenXpath } | { oktaScreenXpath } | { adfsScreenXpath } | {azureScreenXpath}")).Any();

            return isThirdParty;
        }

        public static void WaitForScreenToLoad(double timeout = 1)
        {
            new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(timeout)).Until(webDriver =>
            {
                try
                {
                    ((IJavaScriptExecutor)webDriver).ExecuteScript("return (document.readyState == 'complete')");
                }
                catch (Exception ex)
                {
                    if (ex is NoSuchElementException ||
                       ex is ElementNotVisibleException ||
                       ex is StaleElementReferenceException ||
                       ex is ElementNotInteractableException ||
                       ex is InvalidElementStateException ||
                       ex is WebDriverException)
                    {
                        //ignore element-related exceptions and rerun the wait func for duration of timeout 
                        //to verify if page has loaded elements
                        return false;
                    }
                }
                return true;//exits wait func
            });

        }

        public static void WaitForElementToLoad(DlkBaseControl control, int timespan = DlkEnvironment.mMediumWaitMs)
        {
            int counter = 0;

            while (!control.mElement.Enabled && !control.mElement.Displayed && counter < 3)
            {
                control.FindElement(timespan);
                counter++;
            }
        }

    }
}
