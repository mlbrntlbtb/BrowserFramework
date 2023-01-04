using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private String mTabItemsCSS = "div.icon_container";
        private String mTabItemsXPATH = ".//div[contains(@class,'nav-item app-item')]";
        private String mSidebarVisibleXPATH = "//body[contains(@class,'menu-visible')]";
        private String mMenuButtonXPATH = "//span[contains(@class,'navigation-menu-toggle')]";
        #endregion

        #region CONSTRUCTORS
        public DlkSideBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSideBar(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            ClickMenuButton(CheckSidebarMenuVisible());
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        public bool CheckSidebarMenuVisible()
        {
            bool isVisible = false;
            if (DlkEnvironment.AutoDriver.FindElements(By.XPath(mSidebarVisibleXPATH)).Count > 0)
            {
                DlkLogger.LogInfo("Menu list visible. Proceeding... ");
                isVisible = true;
            }
            return isVisible;
        }

        public void ClickMenuButton (bool IsMenuVisible)
        {
            if (!IsMenuVisible)
            {
                DlkLogger.LogInfo("Menu list not visible. Executing click action to menu button to open... ");
                IWebElement mMenuButton = DlkEnvironment.AutoDriver.FindElements(By.XPath(mMenuButtonXPATH)).Count > 0 ?
                   DlkEnvironment.AutoDriver.FindElement(By.XPath(mMenuButtonXPATH)) : throw new Exception("Menu button not found.");
                mMenuButton.Click();
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("Select")]
        public void Select(String Item)
        {
            Boolean bFound = false;
            try
            {
                for (int retryCount = 0; retryCount < this.iFindElementDefaultSearchMax; retryCount++)
                {
                    Initialize();
                    IWebElement mTab = null;
                    IList<IWebElement> tabs = this.mElement.FindElements(By.CssSelector(mTabItemsCSS));
                    if (tabs.Count <= 0)
                    {
                        tabs = this.mElement.FindElements(By.XPath(mTabItemsXPATH));
                    }
                    for (int i = 0; i < tabs.Count; i++)
                    {
                        if (tabs[i].GetAttribute("title").ToLower() == Item.ToLower())
                        {
                            bFound = true;
                            mTab = tabs[i];
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        for (int i = 0; i < tabs.Count; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(tabs[i].GetAttribute("data-app-id")) && (tabs[i].GetAttribute("data-app-id").ToLower().Trim() == Item.ToLower().Trim())) //check data-app-id attribute for new UI
                            {
                                bFound = true;
                                mTab = tabs[i];
                                break;
                            }
                        }
                    }
                    if (!bFound)
                    {
                        for (int i = 0; i < tabs.Count; i++)
                        {
                            if (tabs[i].Text.ToLower().Trim() == Item.ToLower().Trim())
                            {
                                bFound = true;
                                mTab = tabs[i];
                                break;
                            }
                        }
                    }
                    if (bFound)
                    {
                        DlkBaseControl ctlTab = new DlkBaseControl("TabItem", mTab);
                        ctlTab.ScrollIntoViewUsingJavaScript();
                        ctlTab.Click();
                        DlkLogger.LogInfo("Select() passed.");
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Select() failed. '" + Item + "' not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySidebarItemExists")]
        public void VerifySidebarItemExists(String Item, String ExpectedValue)
        {
            Boolean actualValue = false;
            try
            {
                Initialize();
                IList<IWebElement> tabs = this.mElement.FindElements(By.CssSelector(mTabItemsCSS));
                if (tabs.Count <= 0)
                {
                    tabs = this.mElement.FindElements(By.XPath(mTabItemsXPATH));
                }
                for (int i = 0; i < tabs.Count; i++)
                {
                    string tabTitle = tabs[i].GetAttribute("title").ToString().ToLower();
                    if (tabTitle.Equals(Item.ToLower()))
                    {
                        DlkLogger.LogInfo("Sidebar item: [" + Item + "] found.");
                        actualValue = true;
                        break;
                    }
                }
                if (!actualValue)
                {
                    for (int i = 0; i < tabs.Count; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(tabs[i].GetAttribute("data-app-id")) && (tabs[i].GetAttribute("data-app-id").ToLower().Trim() == Item.ToLower().Trim())) //check data-app-id attribute for new UI
                        {
                            DlkLogger.LogInfo("Sidebar item: [" + Item + "] found.");
                            actualValue = true;
                            break;
                        }
                    }
                }
                if (!actualValue)
                {
                    for (int i = 0; i < tabs.Count; i++)
                    {
                        string tabText = tabs[i].Text.ToLower().Trim();
                        if (tabText.Equals(Item.ToLower().Trim()))
                        {
                            DlkLogger.LogInfo("Sidebar item: [" + Item + "] found.");
                            actualValue = true;
                            break;
                        }
                    }
                }
                if (!actualValue)
                {
                    DlkLogger.LogInfo("Sidebar item: [" + Item + "] not found.");
                }

                DlkAssert.AssertEqual("Sidebar item", Convert.ToBoolean(ExpectedValue), actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySidebarItemExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "True" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                FindElement();
                bool actualValue = mElement.Displayed;
                actualValue = CheckSidebarMenuVisible(); //Added method check since element.displayed does not accurately return correct value

                DlkAssert.AssertEqual("VerifyExists(): ", ExpectedValue, actualValue.ToString());
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                FindElement();
                bool actualValue = mElement.Displayed;
                actualValue = CheckSidebarMenuVisible(); //Added method check since element.displayed does not accurately return correct value

                DlkVariable.SetVariable(VariableName, actualValue.ToString());
                DlkLogger.LogInfo("[" + actualValue.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
