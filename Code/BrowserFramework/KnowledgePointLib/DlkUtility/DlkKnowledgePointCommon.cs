using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;

namespace KnowledgePointLib.DlkUtility
{
    public static class DlkKnowledgePointCommon
    {
        private static string loadingSpinnerXpath = ".//*[@role='progressbar'] | .//*[contains(@class, 'MuiTypography') and contains(text(), 'Loading')]";
        public static void WaitForLoadingSpinnerToFinish(double timeout = 1)
        {
            try
            {
                new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(timeout)).Until(webDriver =>
                {
                    try
                    {
                        return !webDriver.FindElement(By.XPath(loadingSpinnerXpath)).Displayed;
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
                            //to verify loading element
                            return false;
                        }
                    }
                    return true;//exits wait func
                });
            }
            catch 
            { 
                //do nothing
            }
        }

        public static void WaitForScreenToLoad(double timeout = 1)
        {
            try
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
            catch
            {
                //do nothing
            }

        }

    }
}
