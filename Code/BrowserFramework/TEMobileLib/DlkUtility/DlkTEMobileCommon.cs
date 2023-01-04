using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;

namespace TEMobileLib.DlkUtility
{
    public static class DlkTEMobileCommon
    {
        public static string CurrentComponent = string.Empty;
        public static string CurrentControl = string.Empty;
        private const int mLongWaitMs = 900000;
        public static void WaitLoadingFinished(Boolean IsComponentModal = false, Boolean IsLogin = false)
        {
            String WaitImageXPath = ".//*[contains(@class, 'pleaseWaitImage')]";
            Boolean bExist = false;
            IWebElement loadingImage = null;
            //IWebElement loadingLabel = null;
            IWebElement modalHider = null;

            try
            {
                DlkLogger.LogInfo("Checking if page is loading...");
                WaitForScreenToLoad(DlkEnvironment.mShortWaitMs);
                if (!DlkAlert.DoesAlertExist(1))
                {
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(WaitImageXPath)).Any())
                    {
                        loadingImage = DlkEnvironment.AutoDriver.FindElements(By.XPath(WaitImageXPath)).First();
                        modalHider = DlkEnvironment.AutoDriver.FindElements(By.ClassName("modalHider")).First();
                    }

                    if (IsComponentModal)
                    {
                        // Modal fixed wait
                        DlkLogger.LogInfo("Modal component fixed wait initiated...");
                        Thread.Sleep(3000);
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
                            if (DlkEnvironment.mIsMobileBrowser && IsLogin)
                            {
                                IWebElement menuMobile = DlkEnvironment.AutoDriver.FindElements(By.Id("menuMobile")).First();
                                if (menuMobile.GetCssValue("visibility") != "visible")
                                {
                                    Thread.Sleep(3000);
                                    continue;
                                }
                            }
                            bExist = false;
                            break;
                        }
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

        public static void WaitForScreenToLoad(double timeout = 1000)
        {
            new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromMilliseconds(timeout)).Until(webDriver =>
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

            while(!control.mElement.Enabled && !control.mElement.Displayed && counter < 3)
            {
                control.FindElement(timespan);
                counter++;
            }
        }

        public static bool IsElementStale(DlkBaseControl control)
        {
            try
            {
                return control.IsElementStale();
            }
            catch(Exception ex)
            {
                if (ex is NoSuchElementException ||
                       ex is ElementNotVisibleException ||
                       ex is ElementNotInteractableException ||
                       ex is InvalidElementStateException ||
                       ex is WebDriverException ||
                       ex is NullReferenceException)
                {
                    return true;//return true if button is non-interactable
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
