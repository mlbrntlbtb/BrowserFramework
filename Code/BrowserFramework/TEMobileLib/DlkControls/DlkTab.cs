using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {

        private const string NEXT_TAB_BTN_XPATH = "//*[@id='tabRightBtn']";
        private const string PREV_TAB_BTN_XPATH = "//*[@id='tabLeftBtn']";
        private const string MSTR_TAB_ITEMS_XPATH = ".//span[contains(@class,'TabLbl')]";

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
            lstTabElements = mElement.FindElements(By.XPath(MSTR_TAB_ITEMS_XPATH));
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

                        var isClicked = false;
                        do
                        {
                            try
                            {
                                tab.Click();
                                isClicked = true;
                            }
                            catch { };
                        } while (!isClicked && ClickNextTabBtn());

                        if (!isClicked) throw new Exception($"Cannot ineract with the tab {pStrTabCaption}.");

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
                        TabReset();
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
                Initialize();
                List<string> Tabs = new List<string>();
                do Tabs.AddRange(GetVisibleTabs().Select(t => t.Text.Trim()).Where(t => !Tabs.Contains(t)));
                while (ClickNextTabBtn());

                if (Tabs.Count < 1) throw new Exception("No tabs found.");

                var ActualTabs = String.Join("~", Tabs);

                TabReset();
                DlkAssert.AssertEqual("VerifyTabs()", TabNames, ActualTabs);
                DlkLogger.LogInfo("VerifyTabs() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabs() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        private void FindVisibleTabs()
        {
            IList<IWebElement> lstTabElements;
            lstTabElements = mElement.FindElements(By.XPath(MSTR_TAB_ITEMS_XPATH));
            foreach (IWebElement tabElement in lstTabElements)
            {
                if (string.IsNullOrEmpty(tabElement.Text) == false)
                {
                    mlstTabs.Add(new DlkBaseControl("Tab", tabElement));
                }
            }
        }

        private List<IWebElement> GetVisibleTabs()
        {
            var currentVisibleTabs = mElement.FindElements(By.XPath("//span[contains(@class,'TabLbl')]")).Where(t => t.Displayed).ToList();
            if (currentVisibleTabs.Count < 1) throw new Exception("No visible tabs found.");
            return currentVisibleTabs;
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

        private bool ClickNextTabBtn()
        {
            Thread.Sleep(500);
            var nextBtn = DlkEnvironment.AutoDriver.FindElements(By.XPath(NEXT_TAB_BTN_XPATH)).FirstOrDefault(nb => nb.Displayed);
            if (nextBtn == null) return false;

            try { nextBtn.Click(); }
            catch { return false; }

            return true;
        }

        private bool ClickPrevTabBtn()
        {
            var prevBtn = DlkEnvironment.AutoDriver.FindElements(By.XPath(PREV_TAB_BTN_XPATH)).FirstOrDefault();
            if (prevBtn == null) return false;

            try { prevBtn.Click(); }
            catch { return false; }

            return true;
        }

        private void TabReset()
        {
            while (ClickPrevTabBtn()) ;
        }

    }
}
