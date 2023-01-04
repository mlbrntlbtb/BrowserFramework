using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;

namespace CostpointLib.DlkUtility
{
    public static class DlkCostpointCommon
    {
        public static string CurrentComponent = string.Empty;
        public static string CurrentControl = string.Empty;
        private const int mLongWaitMs = 900000;
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
                DlkLogger.LogInfo("Checking if page is loading...");
                if (!DlkAlert.DoesAlertExist(1))
                {
                    if (DlkEnvironment.AutoDriver.FindElements(By.CssSelector(WaitImageCSS)).Count > 0)
                    {
                        loadingImage = DlkEnvironment.AutoDriver.FindElements(By.CssSelector(WaitImageCSS)).First();
                        modalHider = DlkEnvironment.AutoDriver.FindElements(By.ClassName("modalHider")).First();
                    }

                    if (IsComponentModal)
                    {
                        // Modal fixed wait
                        DlkLogger.LogInfo("Modal component fixed wait initiated...");
                        //Thread.Sleep(3000);
                        Thread.Sleep(1000);
                        DlkLogger.LogInfo("Modal component fixed wait finished.");
                        DlkLogger.LogInfo("Ignoring page loading state.");
                        return;
                    }

                    for (int i = 0; loadingImage != null && i < mLongWaitMs / 1000; i++)
                    {
                        Thread.Sleep(1000);
                        if (loadingImage.GetCssValue("visibility") == "visible")
                        {
                            DlkLogger.LogInfo("Page is still loading");
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

                        //stop loading if message area exists with OK button
                        IWebElement messageArea = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'msg') and contains(@style,'visible')]")).FirstOrDefault();
                        if (messageArea != null && messageArea.FindElements(By.CssSelector("*[id=wok]")).Count() > 0)
                            break;

                        //stop loading if merge offline data dialog exists
                        IWebElement mergeOfflineDataDialog = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='mergeOfflineDiv' and contains(@style,'visibility: visible')]")).FirstOrDefault();
                        if (mergeOfflineDataDialog != null && mergeOfflineDataDialog.Text.Contains("Offline Data Available"))
                            break;
                    }
                    if (bExist)
                    {
                        DlkLogger.LogWarning("Page still loading.");
                    }
                    else
                    {
                        DlkLogger.LogInfo("Page finished loading.");
                    }
                }
            }
            catch (Exception e)
            {
                // do nothing
                DlkLogger.LogWarning("WaitLoadingFinished() threw an unexpected exception. Logging call stack for debugging purposes...");
                DlkLogger.LogWarning("Exception Message: " + e.Message);
                DlkLogger.LogWarning("Exceeption Call Stack: " + e.StackTrace);
            }
        }

        private static bool IsModalFormDisplayed()
        {
            IWebElement qry = DlkEnvironment.AutoDriver.FindElements(By.Id("qryFrm")).First();
            IWebElement printOptions = DlkEnvironment.AutoDriver.FindElements(By.Id("printSetupForm")).First();
            IWebElement pageSetup = DlkEnvironment.AutoDriver.FindElements(By.Id("pageSetupForm")).First();
            IWebElement processProgress = DlkEnvironment.AutoDriver.FindElements(By.Id("progMtrDiv")).First();

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

        public static bool IsCurrentComponentModal()
        {
            bool ret = false;
            switch (CurrentComponent.ToLower())
            {
                case "query":
                case "printoptions":
                case "processprogress":
                case "dialog":
                case "fileuploadmanager":
                    DlkLogger.LogInfo("Current component is modal.");
                    ret = true;
                    break;
                case "cp7main":
                    switch(CurrentControl.ToLower())
                    {
                        case "messagesarea":
                            DlkLogger.LogInfo("Current component is CP7Main. Determining control...");
                            DlkLogger.LogInfo("Current control is MessageArea and will be treated as modal");
                            ret = true;
                            break;
                        default:
                            DlkLogger.LogInfo("Current component is CP7Main. Determining control...");
                            DlkLogger.LogInfo("Current control is " + CurrentControl + " and not modal");
                            break;
                    }
                    break;
                default:
                    break;
            }
            return ret;
        }

        public static bool IsComponentModal(string PreviousComponent, string CurrentComponent)
        {
            bool ret = false;
            switch (PreviousComponent.ToLower())
            {
                case "query":
                case "printoptions":
                case "processprogress":
                case "dialog":
                case "fileuploadmanager":
                    DlkLogger.LogInfo("Previous component is modal.");
                    ret = true;
                    break;
                case "cp7main":
                    switch (PreviousComponent.ToLower())
                    {
                        case "messagesarea":
                            DlkLogger.LogInfo("Previous component is CP7Main. Determining control...");
                            DlkLogger.LogInfo("Previous control is MessageArea and will be treated as modal");
                            ret = true;
                            break;
                        default:
                            DlkLogger.LogInfo("Previous control is not modal");
                            break;
                    }
                    break;
                default:
                    break;
            }

            switch (CurrentComponent.ToLower())
            {
                case "query":
                case "printoptions":
                case "processprogress":
                case "dialog":
                case "fileuploadmanager":
                    DlkLogger.LogInfo("Current component is modal.");
                    ret = true;
                    break;
                case "function":
                    DlkLogger.LogInfo("Current component is Function and will be treated as modal");
                    ret = true;
                    break;
                case "cp7main":
                    DlkLogger.LogInfo("Current component is CP7Main. Determining control...");
                    switch (CurrentControl.ToLower())
                    {
                        case "messagesarea":
                            DlkLogger.LogInfo("Current control is MessageArea and will be treated as modal");
                            ret = true;
                            break;
                        case "calendar":
                            DlkLogger.LogInfo("Current control is Calendar and will be treated as modal");
                            ret = true;
                            break;
                        default:
                            DlkLogger.LogInfo("Current control is " + CurrentControl + " and not modal");
                            break;
                    }
                    break;
                default:
                    break;
            }

            return ret;

        }

        public static bool IsSystemError()
        {
            // system error check logic
            bool bSystemError = false;
            try
            {
                if (!DlkAlert.DoesAlertExist(1))
                {
                    // probably possible that there are other modal headers that use this class,
                    var modals = DlkEnvironment.AutoDriver.FindElements(By.Id("msgPopDivTBar"));
                    // so check each of them here.
                    foreach (var modal in modals)
                    {
                        bSystemError = modal.Text.Contains("System Error");
                        if (bSystemError) break;
                    }
                }
            }
            catch
            {
                bSystemError = false; // always ignore errors encountered here
            }
            return bSystemError;
        }
    }
}
