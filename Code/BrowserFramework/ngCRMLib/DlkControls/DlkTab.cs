using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace ngCRMLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        private String mstrTabItemsCSS = "li>a";
        private List<DlkBaseControl> mlstTabs;


        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        //public DlkTab(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public void Initialize()
        {
            
            mlstTabs = new List<DlkBaseControl>();
            FindElement();
            FindTabs();
            
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (this.mElement.GetAttribute("class").ToLower().Contains("tabs"))
            {
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'tabs')]"));
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }

            }
        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("Tabs", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                IWebElement parentTab = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'tabs')]"));
                mCorrectControl = new DlkBaseControl("CorrectControl", parentTab);
                mAutoCorrect = true;

                if (mAutoCorrect)
                {
                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClassName = mCorrectControl.GetAttributeValue("class");
                    if (mId != null && mId != "")
                    {
                        SearchType = "ID";
                        SearchValue = mId;
                    }
                    else if (mName != null && mName != "")
                    {
                        SearchType = "NAME";
                        SearchValue = mName;
                    }
                    else if (mClassName != null && mClassName != "")
                    {
                        SearchType = "CLASSNAME";
                        SearchValue = mClassName.Split(' ').First();
                    }
                    else
                    {
                        SearchType = "XPATH";
                        SearchValue = mCorrectControl.FindXPath();
                    }
                }
            }
            catch
            {

            }
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
        public void Select(String Item)
        {
            bool bFound = false;
            String strActualTabs = "";

            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    DlkLogger.LogInfo(tab.GetValue());
                    strActualTabs = strActualTabs + tab.GetValue().Trim() + " ";
                    if (tab.GetValue().Trim().ToLower() == Item.ToLower())
                    {
                        tab.ScrollIntoViewUsingJavaScript();
                        tab.Click();
                        if (tab.Exists())
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                            tab.Click();
                        }
                        bFound = true;
                        break;
                    }
                }
                if (bFound)
                {
                    DlkLogger.LogInfo("Successfully executed Select() : " + mControlName + " = " + Item);
                }
                else
                {
                    throw new Exception(mControlName + " = '" + Item + "' tab not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

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
                throw new Exception("VerifyExists failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyTabs", new String[] { "1|text|Tab Names|Header~Other Information~Accouting Defaults" })]
        public void VerifyTabs(String Items)
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
                        strTabList = strTabList + tab.GetValue().Trim();
                    }
                    else
                    {
                        strTabList = strTabList + tab.GetValue() + "~";
                    }

                }

                DlkAssert.AssertEqual("VerifyTabs()", Items, strTabList);
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
