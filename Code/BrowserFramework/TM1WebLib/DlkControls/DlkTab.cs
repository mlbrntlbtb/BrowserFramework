using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TM1WebLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {

        private String mstrTabItemsCSS = "span[class*='tabLabel']";
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
            String strActualTabs = "";

            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    DlkLogger.LogInfo(tab.GetValue());
                    strActualTabs = strActualTabs + tab.GetValue() + " ";
                    if (tab.GetValue().ToLower() == pStrTabCaption.ToLower())
                    {
                        tab.Click();
                        if (tab.Exists())
                        {
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

    }
}
