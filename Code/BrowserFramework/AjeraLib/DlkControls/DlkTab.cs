using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace AjeraLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        #region DECLARATIONS
        private List<DlkBaseControl> mlstTabs;
        #endregion

        #region CONSTRUCTORS
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
        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String TabCaption)
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
                    if (tab.GetValue().ToLower() == TabCaption.ToLower())
                    {
                        if (tab.Exists())
                        {
                            tab.MouseOver();
                            tab.Click();
                            Thread.Sleep(5000);
                        }
                        bFound = true;
                        break;
                    }
                }
                  if (bFound)
                {
                    DlkLogger.LogInfo("Successfully executed Select() : " + mControlName + " = " + TabCaption);
                }
                else
                {
                    throw new Exception(mControlName + " = '" + TabCaption + "' tab not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Tab Caption|Tab1",
                                                "2|text|Expected Value|TRUE"})]
        public void VerifyExists(String TabCaption, String TrueOrFalse)
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
                        bFound = tab.Exists();
                        break;
                    }
                }

                if (bFound == Convert.ToBoolean(TrueOrFalse))
                {
                    DlkLogger.LogInfo("VerifyExists() passed: Actual = " + Convert.ToString(bFound) + " : Expected = " + TrueOrFalse);
                }
                else
                {
                    DlkLogger.LogInfo("VerifyExists() failed: Actual = " + Convert.ToString(bFound) + " : Expected = " + TrueOrFalse);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region METHODS
        private void FindTabs()
        {
            
             string mstrTabItemsCSS = "li";

            var mClassName = mElement.GetAttribute("class").ToString();

            IList<IWebElement> lstTabElements;

            if (mClassName.Contains("widget-header"))
            {
                mstrTabItemsCSS = "./li/a";
                lstTabElements = mElement.FindElements(By.XPath(mstrTabItemsCSS));
            }
            else if (mClassName == "ax-tab-bar")
            {
                mstrTabItemsCSS = "li[class*=ax-tab-state-default]";
                lstTabElements = mElement.FindElements(By.CssSelector(mstrTabItemsCSS));
            }
            else
            {
                mstrTabItemsCSS = "li";
                lstTabElements = mElement.FindElements(By.CssSelector(mstrTabItemsCSS));
            }


            foreach (IWebElement tabElement in lstTabElements)
            {
                mlstTabs.Add(new DlkBaseControl("Tab", tabElement));
            }
        }
        #endregion

    }
}
