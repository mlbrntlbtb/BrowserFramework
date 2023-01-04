using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFTLib.DlkUtility
{
    public static class DlkSFTCommon
    {
        public static string SearchValues { get; set; }
        public static void WaitForSpinner(bool notOnInit = false)
        {
            try
            {
                var browser = DlkEnvironment.mBrowser.ToLower();
                var driver = DlkEnvironment.AutoDriver;
                const string XPATH = "//*[contains(@class,'mask-msg-text') and contains(text(), 'Wait')] | //*[@id='widgetWait:subviewWait:ajaxLoadingModalBoxContainer' and not(contains(@style, 'display: none'))]";
                const string XPATH2 = "//*[@id='widgetWait:subviewWait:ajaxLoadingModalBoxDiv' and not(contains(@style, 'display: none'))]";
                const int MAX_TRY = 40;
                int initTry = 0;

                driver.SwitchTo().DefaultContent();

                var spinner = driver.FindElements(By.XPath(XPATH));

                if (spinner.Count() < 1)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Frame("contentFrame");
                    spinner = driver.FindElements(By.XPath(XPATH2));
                }
                
                while (spinner.Count() > 0 && initTry < MAX_TRY)
                {
                    initTry += 1;
                    DlkLogger.LogInfo("Application loading...");
                    Thread.Sleep(1000);
                    spinner = driver.FindElements(By.XPath(XPATH));
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
                DlkLogger.LogInfo("Application finished loading.");
                if (notOnInit)
                {
                    if (SearchValues.Contains("~"))
                    {
                        foreach(var frame in SearchValues.Split('~'))
                        {
                            DlkEnvironment.AutoDriver.SwitchTo().Frame(frame);
                            Thread.Sleep(500);
                        }
                    }
                    else
                     DlkEnvironment.AutoDriver.SwitchTo().Frame(SearchValues);
                }
                else
                    driver.SwitchTo().DefaultContent();
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo($"WaitForSpinner() catch: {e.Message}");
                //continue if the spinner has already loaded.
            }
        }

            public static void WaitForLoadingDialogFinished(int Timeout)
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkBaseControl spinner = new DlkBaseControl("spinner", "iframe_nested_xpath", "servletBridgeIframe~6~webiViewFrame_//*[@id='img_waitDlg_uprogressBar']");
            if (spinner.Exists(1))
            {
                while (spinner.mElement.Displayed)
                {
                    DlkLogger.LogInfo("Loading dialog displayed: TRUE");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            DlkLogger.LogInfo("Loading dialog displayed: FALSE");
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Wait for page document to load.
        /// </summary>
        /// <param name="timeout">Number of wait seconds</param>
        public static void WaitForScreenToLoad(int timeout = 15)
        {
            try
            {
                new WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(timeout)).Until(
                    wait => ((IJavaScriptExecutor)wait).ExecuteScript("return (document.readyState == 'complete' || jQuery.active == 0)"));
            }
            catch (Exception)
            {
                //continue when the document has already loaded
            }
        }
        /// <summary>
        /// Expands subpanels on page
        /// </summary>
        public static void ExpandPanel()
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                var expandBtn = new DlkBaseControl("Expand Panel button", "IFRAME_XPATH", "contentFrame_//*[@class='classMasterDetailSeparatorButtons']//*[contains(@src, 'md-mid')]/..");

                try
                {
                    expandBtn.FindElement(3);
                }
                catch
                {
                    expandBtn = new DlkBaseControl("Expand Panel button", "IFRAME_NESTED_XPATH", "contentFrame~iframeDetail_//*[@class='classMasterDetailSeparatorButtons']//*[contains(@src, 'md-mid')]/..");
                    expandBtn.FindElement(3);
                }

                expandBtn.ClickUsingJavaScript(false);

                DlkSFTCommon.WaitForScreenToLoad();
                DlkSFTCommon.WaitForSpinner();
            }
            catch
            {
                //continue if the expand button is not found.
            }
        }

        public static void ScrollToElement(DlkBaseControl baseControl)
        {
            if (!baseControl.Exists(2))
            {
                WaitForScreenToLoad();
                WaitForSpinner();
                baseControl.FindElement();
                baseControl.ScrollIntoViewUsingJavaScript(false);
            }
        }
    }
}
