using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {

        private String mstrTabItemsCSS = "span[class*='TabLbl'],span[class*='tabRightBtn'],span[class*='tabLeftBtn']";
        private List<DlkBaseControl> mlstTabs;

        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            mlstTabs = new List<DlkBaseControl>();
            FindElement();
            FindTabs();
        }

        private void FindTabs()
        {
            IList<IWebElement> lstTabElements;
            lstTabElements = mElement.FindElements(By.CssSelector(mstrTabItemsCSS));
            foreach (IWebElement tabElement in lstTabElements)
            {
                mlstTabs.Add(new DlkBaseControl("Tab", tabElement));
            }
        }

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String pStrTabCaption)
        {
            bool bFound = false;

            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    DlkLogger.LogInfo(tab.GetValue());
                    if (tab.GetValue().ToLower() == pStrTabCaption.ToLower())
                    {
                        if (DlkEnvironment.mBrowser.ToLower() != "ie")
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                        }
                        ScrollTab(tab.mElement);
                        tab.Click();

                        //probably used to make sure the tab is clicked
                        if (tab.Exists())
                        {
                            if (DlkEnvironment.mBrowser.ToLower() != "ie")
                            {
                                tab.ScrollIntoViewUsingJavaScript();
                            }
                            ScrollTab(tab.mElement);
                            tab.Click();
                        }
                        bFound = true;
                        break;
                    }
                }

                if (bFound)
                {
                    DlkLogger.LogInfo("Successfully executed Select() : " + mControlName + " = " + pStrTabCaption);
                }
                else
                {
                    throw new Exception(mControlName + " = '" + pStrTabCaption + "' tab not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColor", new String[] { "1|text|Tab Caption|Tab1", "2|text|ExpectedColor|Default|Orange" })]
        public void VerifyColor(string TabCaption, string ExpectedColor)
        {
            bool bFound = false;
            string actualcolor = "default";
            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    string tabValue = tab.GetValue();
                    DlkLogger.LogInfo(tabValue);
                    if (tabValue.ToLower() == TabCaption.ToLower())
                    {
                        string tabColor = tab.mElement.GetCssValue("background");

                        if (tabColor.Contains("rgb(242, 119, 42)"))
                            actualcolor = "orange";

                        bFound = true;
                        break;
                    }
                }

                if (bFound)
                {
                    DlkAssert.AssertEqual("VerifyColor()", ExpectedColor.ToLower(), actualcolor);
                }
                else
                {
                    throw new Exception(mControlName + " = '" + TabCaption + "' tab not found");
                }

                DlkLogger.LogInfo("Successfully executed VerifyColor()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColor failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyTabs", new String[] { "1|text|Tab Names|Header~Other Information~Accouting Defaults" })]
        public void VerifyTabs(String TabNames)
        {
            try
            {
                mlstTabs = new List<DlkBaseControl>();
                FindElement();
                FindVisibleTabs();

                String strTabList = "";
                int itemIdx = 0;
                int elemIdx = mlstTabs.Count - 1;

                foreach (DlkBaseControl tab in mlstTabs)
                {
                    itemIdx = mlstTabs.IndexOf(tab);
                    if (itemIdx == elemIdx)
                    {
                        strTabList = strTabList + tab.GetValue();
                    }
                    else
                    {
                        strTabList = strTabList + tab.GetValue() + "~";
                    }

                }

                DlkAssert.AssertEqual("VerifyTabs()", strTabList, TabNames);
                DlkLogger.LogInfo("VerifyTabs() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabs() failed : " + e.Message, e);
            }
        }


        [Keyword("SelectIfExists", new String[] { "1|text|Tab Caption|Details" })]
        public void SelectIfExists(String TabCaption)
        {
            bool bFound = false;

            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    DlkLogger.LogInfo(tab.GetValue());
                    if (tab.GetValue().ToLower() == TabCaption.ToLower())
                    {
                        if (DlkEnvironment.mBrowser.ToLower() != "ie")
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                        }
                        ScrollTab(tab.mElement);
                        tab.Click();

                        //probably used to make sure the tab is clicked
                        if (tab.Exists())
                        {
                            if (DlkEnvironment.mBrowser.ToLower() != "ie")
                            {
                                tab.ScrollIntoViewUsingJavaScript();
                            }
                            ScrollTab(tab.mElement);
                            tab.Click();
                        }
                        bFound = true;
                        break;
                    }
                }

                if (bFound)
                {
                    DlkLogger.LogInfo("Successfully executed SelectIfExists() : " + mControlName + " = " + TabCaption);
                }
                else
                {
                    DlkLogger.LogInfo(mControlName + " = " + TabCaption + " does not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectIfExists() failed : " + e.Message, e);
            }
        }

        private void FindVisibleTabs()
        {
            IList<IWebElement> lstTabElements;
            lstTabElements = mElement.FindElements(By.CssSelector(mstrTabItemsCSS));
            foreach (IWebElement tabElement in lstTabElements)
            {
                if (string.IsNullOrEmpty(tabElement.Text) == false)
                {
                    mlstTabs.Add(new DlkBaseControl("Tab", tabElement));
                }
            }
        }

        /// <summary>
        /// Some tabs have an internal scroll bar within the tab container. This method allow us to scroll to the tab we want to select within tab container.
        /// </summary>
        /// <param name="tabElem">The tab we want to scroll to within the tab cotainer</param>
        private void ScrollTab(IWebElement tabElem)
        {
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            //get the top position of the tab to select, then scroll the tab container to its position
            javascript.ExecuteScript(
                "var topOffset = arguments[0].offsetTop;" + 
                "var topMargin = parseInt(getComputedStyle(arguments[0]).getPropertyValue('margin-top', 10));" +
                "arguments[1].scrollTop = topOffset - topMargin;"
                , tabElem, mElement);
        }

    }
}
