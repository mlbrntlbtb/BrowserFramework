using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("DashboardFilter")]
    public class DlkDashboardFilter : DlkBaseControl
    {
        #region Constructors
        public DlkDashboardFilter(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkDashboardFilter(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDashboardFilter(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        const string mXpathGlobal = ".//div[@class='filterDock global']";
        const string mXpathOther = ".//div[@class='filterDock other']";

        private IList<IWebElement> mGlobalFilterButtons;
        private IList<IWebElement> mOtherFilterButtons;

        private bool mIsGlobalFilterButton = false;

        private void Initialize()
        {
            FindElement();
            GetFilterButtons();
        }

        private void GetFilterButtons()
        {
            mGlobalFilterButtons = new List<IWebElement>();
            mOtherFilterButtons = new List<IWebElement>();
            string xpathToggleButtons = "//div[contains(@class,'filter-dock-begin')]";
            var globalButtons = mElement.FindElements(By.XPath($"{mXpathGlobal}//span[contains(@class,'filterDock-filterName')]|{mXpathGlobal}{xpathToggleButtons}"));
            var otherButtons = mElement.FindElements(By.XPath($"{mXpathOther}//span[contains(@class,'filterDock-filterName')]|{mXpathOther}{xpathToggleButtons}"));

            foreach (var button in globalButtons)
            {
                mGlobalFilterButtons.Add(button);
            }

            foreach (var button in otherButtons)
            {
                mOtherFilterButtons.Add(button);
            }
        }

        private IWebElement GetFilterButtonByCaption(string caption)
        {
            ScrollFilter(mGlobalFilterButtons, caption, true, out IWebElement btn);

            if (btn != null)
            {
                mIsGlobalFilterButton = true;
            }
            else
            {
                ScrollFilter(mOtherFilterButtons, caption, false, out btn);
            }

            return btn;
        }

        private void ScrollFilter(IList<IWebElement> buttons, string caption, bool global, out IWebElement result)
        {
            result = null;
            while (true)
            {
                var btn = buttons.FirstOrDefault(f => f.Displayed && f.Text == caption);
                if (btn != null)
                {
                    result = btn;
                    break;
                }
                else
                {
                    string xPath = global ? $"{mXpathGlobal}//div[contains(@class,'filterDockChevronRight')]" :
                                        $"{mXpathOther}//div[contains(@class,'filterDockChevronRight')]";

                    var scroll = mElement.FindElements(By.XPath(xPath)).FirstOrDefault();

                    if (scroll == null)
                    {
                        break;
                    }
                    else if (scroll.Displayed)
                    {
                        scroll.Click();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        [Keyword("ClickFilterButton", new String[] { "1|text|Caption|Company Name" })]
        public void ClickFilterButton(string Caption)
        {
            try
            {
                Initialize();

                var button = GetFilterButtonByCaption(Caption);
                if (button != null)
                {
                    button.Click();
                }
                else
                {
                    throw new Exception($"Filter button '{Caption}' not found.");
                }

                DlkLogger.LogInfo("ClickFilterButton successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickFilterButton() failed : " + e.Message, e);
            }
        }
    }
}
