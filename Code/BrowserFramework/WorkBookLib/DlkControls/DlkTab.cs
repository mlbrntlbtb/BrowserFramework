using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using CommonLib.DlkUtility;
using System.Linq;
using WorkBookLib.DlkSystem;
using System.Threading;
using System.Text.RegularExpressions;

namespace WorkBookLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES
        private IList<IWebElement> mTabItems;
        private String mTabItemsXPath1 = ".//div[contains(@class, 'GenericToolbarTab')]";
        private String mTabItemsXPath2 = ".//div[contains(@class, 'Tab')]";
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            FindTabs();
        }
        public string GetControlValue(IWebElement item, String controlName) {
            String value = "";

            value = new DlkBaseControl(controlName, item).GetValue().Trim();
            if (!new Regex("[@#^\":{}|<>=]").IsMatch(value) && !double.TryParse(value, out double x)) {
                return value;
            }
            value = item.GetAttribute("title");
            if ((value != "") && (value != null)) {
                return value;
            }
            value = item.GetAttribute("placeholder");
            if ((value != "") && (value != null)) {
                return value;
            }
            return "";
        }
        #endregion

        #region PUBLIC METHODS
        private void FindTabs()
        {
            mTabItems = mElement.FindWebElementsCoalesce(false, By.XPath(mTabItemsXPath1), By.XPath(mTabItemsXPath2))
                .Where(x => x.Displayed).ToList();
        }
        private string GetTabItemsToString() {
            String items = "";
            foreach(IWebElement item in mTabItems) {
                items += "~" + GetControlValue(item, "mTabItem");
            }
            return items.Trim('~');
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifyTabItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTabItems(String ExpectedValue) {
            try {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                DlkAssert.AssertEqual("VerifyTabItems(): ", ExpectedValue.Trim(), GetTabItemsToString());
                DlkLogger.LogInfo("VerifyTabItems() passed");
            } catch (Exception e) {
                throw new Exception("VerifyTabItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTabItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetTabItems(String VariableName) {
            try {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetTabItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetTabItems() passed");
            } catch (Exception e) {
                throw new Exception("GetTabItems() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String TabItem)
        {
            try
            {
                Initialize();
                bool bFound = false;
                string actualTabItems = "";

                foreach (IWebElement tabItem in mTabItems)
                {
                    string currentTab = DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(tabItem.GetAttribute("title").Trim().ToLower(),"") :
                        DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "");
                    actualTabItems += currentTab + "~";
                    if (currentTab == TabItem.ToLower())
                    {
                        tabItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + TabItem + "] not found. Actual list: [" + actualTabItems + "]");

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains", new String[] { "1|text|Tab Caption|Tab1" })]
        public void SelectContains(String TabItem)
        {
            try
            {
                Initialize();
                bool bFound = false;
                string actualTabItems = "";

                foreach (IWebElement tabItem in mTabItems)
                {
                    string currentTab = DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(tabItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "");
                    actualTabItems += currentTab + "~";
                    if (currentTab.Contains(TabItem.ToLower()))
                    {
                        tabItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + TabItem + "] not found. Actual list: [" + actualTabItems + "]");

                DlkLogger.LogInfo("SelectContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}