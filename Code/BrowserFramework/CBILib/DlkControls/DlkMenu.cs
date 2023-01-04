using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CBILib.DlkControls;
using CBILib.DlkUtility;
using System.Threading;

namespace CBILib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        #region Constructors
        public DlkMenu(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            //WaitForMenuReady();
            FindElement();
        }

        #region Keywords
       

        /// <summary>
        /// Verifies if button exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects menu
        /// </summary>
        /// <param name="MenuPath"></param>
        [Keyword("SelectMenu", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectMenu(String MenuPath)
        {
            try
            {
                Initialize();
                if (MenuPath.Contains("~")) // multiple items
                {
                    var menuItems = MenuPath.Split('~');
                    bool firstIteration = true;
                    foreach(var menuItem in menuItems)
                    {
                        if (firstIteration)
                        {
                            ScrollIntoViewUsingJavaScript();
                            var menu = mElement.FindElement(By.XPath(".//div[contains(@id,'PluginContainer')]//div[@class='ba-common-button__label'and contains(text(),'" + menuItem.Trim() + "')]"));
                            // Select in main nav menu
                            DlkBaseControl navigationMenu = new DlkBaseControl("Menu", menu);
                            navigationMenu.ClickUsingJavaScript(false);
                            firstIteration = false;
                        }
                        else
                        {
                            WaitForMenuReady();
                            ScrollIntoViewUsingJavaScript();
                            Thread.Sleep(3000);

                            var menu = mElement.FindElement(By.XPath("//table[@role='grid']//tr[@data-name='" + menuItem.Trim() + "']//div[@role='link']"));
                            new DlkBaseControl("NavMenu", menu).ScrollIntoViewUsingJavaScript();
                            menu.Click();
                        }
                        Thread.Sleep(4000);
                    }
                }
                else
                {
                    mElement.FindElement(By.XPath(".//div[contains(@id,'PluginContainer')]//div[@class='ba-common-button__label'and contains(text(),'" + MenuPath.Trim() + "')]")).Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectMenu() failed : " + e.Message, e);
            }
        }


        #endregion

        #region Private Methods

        private void WaitForMenuReady()
        {

            DlkBaseControl menuLoadingIdentifier = new DlkBaseControl("spinner", "xpath_display", "//table[contains(@class,'listControl ')]/tbody/tr"); //has value

            //if(mElement.FindElements(By.XPath("//table[contains(@class,'listControl ')]/tbody/tr")).Count() > 0)

            if (!menuLoadingIdentifier.Exists(1)) // does not exists, means no value
            {
                try
                {
                    while (!menuLoadingIdentifier.Exists(1))
                    {
                        DlkLogger.LogInfo("Menu loading...");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    DlkLogger.LogInfo("Menu finished loading.");
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    return;
                }
            }
            DlkLogger.LogInfo("Menu finished loading.");
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
        }

        #endregion

    }
}
