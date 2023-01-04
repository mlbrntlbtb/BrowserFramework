using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using FieldEaseLib.DlkSystem;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace FieldEaseLib.DlkControls
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
        private string mTabItemsString;
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetTabItems();
        }
        #endregion

        #region PRIVATE METHODS

        private void GetTabItems()
        {
            mTabItems = mElement.FindElements(By.XPath(".//li[contains(@id,'tab')]")).Where(x => x.Displayed).ToList();

            //added condition settings list
            if (mTabItems.Count() == 0)
                mTabItems = mElement.FindElements(By.XPath(".//li[contains(@class,'rtsLI')]")).Where(x => x.Displayed).ToList();
        }

        private string GetTabItemsToString()
        {
            foreach (IWebElement tabItem in mTabItems)
            {
                string currentTab = DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "") == "" ?
                    DlkString.ReplaceCarriageReturn(tabItem.GetAttribute("title").Trim().ToLower(), "") :
                    DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "");
                mTabItemsString += currentTab + "~";
            }
            return mTabItemsString.Trim('~');
        }

        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String TabItem)
        {
            try
            {
                Initialize();
                bool bFound = false;

                foreach (IWebElement tabItem in mTabItems)
                {
                    string currentTab = DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(tabItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "");
                    if (currentTab == TabItem.ToLower())
                    {
                        if (tabItem.GetAttribute("aria-selected") == null)
                        {
                            if (DlkEnvironment.mBrowser.ToLower() == "ie")
                            {
                                new DlkBaseControl("tabItem", tabItem).ClickUsingJavaScript();
                            }
                            else
                            {
                                tabItem.Click();
                            }
                        }

                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + TabItem + "] not found. Actual list: [" + GetTabItemsToString() + "]");

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTabItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTabItems(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                DlkAssert.AssertEqual("VerifyTabItems(): ", ExpectedValue.Trim(), GetTabItemsToString());
                DlkLogger.LogInfo("VerifyTabItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTabItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetTabItems(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetTabItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetTabItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetTabItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTabItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetTabItemCount(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String actualTabItemCount = mTabItems.Count().ToString();
                DlkVariable.SetVariable(VariableName, actualTabItemCount);
                DlkLogger.LogInfo("[" + actualTabItemCount + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetTabItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetTabItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTabItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTabItemCount(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                String actualTabItemCount = mTabItems.Count().ToString();
                DlkAssert.AssertEqual("VerifyTabItemCount(): ", ExpectedValue.Trim(), actualTabItemCount);
                DlkLogger.LogInfo("VerifyTabItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabItemCount() failed : " + e.Message, e);
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
                throw new Exception("VerifyExists() failed : " + e.Message, e);
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
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInTabList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInTabList(String TabItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = false;

                foreach (IWebElement tabItem in mTabItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(tabItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "");
                    if (currentItem == TabItem.ToLower())
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemInTabList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + TabItem.ToLower() + "] found in list: [" + GetTabItemsToString() + "]");
                DlkLogger.LogInfo("VerifyItemInTabList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInTabList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialItemInTabList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialItemInTabList(String PartialTabItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = false;

                foreach (IWebElement tabItem in mTabItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(tabItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(tabItem.Text.Trim().ToLower(), "");
                    if (currentItem.Contains(PartialTabItem.ToLower()))
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyPartialItemInTabList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + PartialTabItem.ToLower() + "] found in list: [" + GetTabItemsToString() + "]");
                DlkLogger.LogInfo("VerifyPartialItemInTabList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialItemInTabList() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
