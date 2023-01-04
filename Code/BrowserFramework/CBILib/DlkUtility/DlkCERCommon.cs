using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkUtility
{
    public static class DlkCERCommon
    {
        public static void WaitForSpinner()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkBaseControl spinner = new DlkBaseControl("spinner", "xpath_display", "//div[@class='ba-splash-loading']");

            if (spinner.Exists(1))
            {
                try
                {
                    while (spinner.mElement.Displayed)
                    {
                        DlkLogger.LogInfo("Application loading...");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    DlkLogger.LogInfo("Application finished loading.");
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    return;
                }
            }
            DlkLogger.LogInfo("Application finished loading.");
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
        }

        public static void WaitForPromptSpinner()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkBaseControl spinner = new DlkBaseControl("spinner", "xpath_display", "//*[@class='rsBlockerDlg' and contains(@style,'visibility: visible;')]");

            if (spinner.Exists(1))
            {
                try
                {
                    while (spinner.mElement.Displayed)
                    {
                        DlkLogger.LogInfo("Application loading...");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    DlkLogger.LogInfo("Application finished loading.");
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    return;
                }
            }
            DlkLogger.LogInfo("Application finished loading.");
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
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
    }
}
