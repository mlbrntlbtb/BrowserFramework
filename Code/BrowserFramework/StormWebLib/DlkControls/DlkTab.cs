using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;


namespace StormWebLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private String mstrTabItemsCSS = "li";
        private String mstrTabItemsXPath = ".//li/a";
        private List<DlkBaseControl> mlstTabs;
        #endregion

        #region CONSTRUCTORS
        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            //find all tabs
            mlstTabs = mElement.FindElements(By.XPath(mstrTabItemsXPath))
                .Select(y => new DlkBaseControl("Tab", y))
                .Where(y => (String.IsNullOrEmpty(y.GetValue())) == false).ToList();


            this.ScrollIntoViewUsingJavaScript();
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
        #endregion

        #region PRIVATE METHODS
        private List<DlkBaseControl> FindVisibleTabs()
        {
            List<DlkBaseControl> mlstTabs = new List<DlkBaseControl>();

            FindElement();

            return mElement.FindElements(By.CssSelector(mstrTabItemsCSS))
                .Where(x => String.IsNullOrEmpty(x.Text) == false).AsEnumerable()
                .Select(y => new DlkBaseControl("Tab", y)).ToList();
        }
        #endregion

        #region KEYWORDS
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
                        if (!tab.Exists(1))
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                        }
                        tab.Click();
                        DlkLogger.LogInfo("Successfully executed Select() : " + mControlName + " = " + Item);
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
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
                Initialize();

                String strTabList = "";
                int itemIdx = 0;

                mlstTabs = mlstTabs.Where((tab) => !tab.GetAttributeValue("style").Replace(" ", "").Contains("display:none")).ToList();
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

        [Keyword("VerifyTabExists", new String[] { "1|text|Tab Name|Tab1",
                                                   "2|text|Expected Value|TRUE" })]
        public void VerifyTabExists(String Item, String TrueOrFalse)
        {
            try
            {
                bool ActualValue = false;
                Initialize();

                var tabToBeVerified = mlstTabs.Where(x => x.GetValue().Trim().ToLower() == Item.ToLower()).FirstOrDefault();
                if (tabToBeVerified != null)
                {
                    IList<IWebElement> tabItem = tabToBeVerified.mElement.FindElements(By.XPath("./ancestor::li"));
                    if (tabItem.Count > 0)
                        ActualValue = tabItem[0].GetCssValue("display").Contains("none") ? false : true;
                }

                DlkAssert.AssertEqual("VerifyTabExists()", Convert.ToBoolean(TrueOrFalse), ActualValue);
                DlkLogger.LogInfo("VerifyTabExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTabReadOnly", new String[] { "1|text|Tab Name|Tab1",
                                                   "2|text|Expected Value|TRUE" })]
        public void VerifyTabReadOnly(String Item, String TrueOrFalse)
        {
            try
            {
                bool ActualValue = false;
                Initialize();

                var tabToBeVerified = mlstTabs.Where(x => x.GetValue().Trim().ToLower() == Item.ToLower()).FirstOrDefault();
                IWebElement tabItem = tabToBeVerified.mElement.FindElement(By.XPath("./ancestor::li"));
                ActualValue = tabItem.GetAttribute("class").Contains("disabled") ? true : false;

                DlkAssert.AssertEqual("VerifyTabReadOnly()", Convert.ToBoolean(TrueOrFalse), ActualValue);
                DlkLogger.LogInfo("VerifyTabReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabReadOnly() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
