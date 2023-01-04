using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptShareLib.DlkUtility
{
    public static class DlkConceptShareCommon
    {
        public static void WaitForSpinner()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkBaseControl spinner = new DlkBaseControl("spinner", "iframe_xpath", "contentFrame_//*[@id='widgetWait:subviewWait:ajaxLoadingModalBoxContainer']|//*[contains(@class,'x-mask-loading')]//*[contains(text(),'Wait')]");
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
