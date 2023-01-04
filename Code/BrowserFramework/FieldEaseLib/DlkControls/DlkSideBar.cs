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
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
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

        #region PRIVATE VARIABLES
        private IList<IWebElement> mSidebarItems;
        private string mSidebarItemsString;
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetSidebarItems();
        }
        #endregion

        #region PRIVATE VARIABLES
        private void GetSidebarItems()
        {
            mSidebarItems = mElement.FindElements(By.XPath(".//li"))
                .Where(x => x.Displayed).ToList();
        }

        private string GetSidebarItemsToString()
        {
            foreach (IWebElement sideBarItem in mSidebarItems)
            {
                string currentTab = DlkString.ReplaceCarriageReturn(sideBarItem.Text.Trim().ToLower(), "") == "" ?
                    DlkString.ReplaceCarriageReturn(sideBarItem.GetAttribute("title").Trim().ToLower(), "") :
                    DlkString.ReplaceCarriageReturn(sideBarItem.Text.Trim().ToLower(), "");
                mSidebarItemsString += currentTab + "~";
            }
            return mSidebarItemsString.Trim('~');
        }
        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String SidebarItem)
        {
            try
            {
                Initialize();
                bool bFound = false;

                foreach (IWebElement sidebarItem in mSidebarItems)
                {
                    string currentTab = DlkString.ReplaceCarriageReturn(sidebarItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(sidebarItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(sidebarItem.Text.Trim().ToLower(), "");
                    if (currentTab == SidebarItem.ToLower())
                    {
                        sidebarItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + SidebarItem + "] not found. Actual list: [" + GetSidebarItemsToString() + "]");

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySidebarItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySidebarItems(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                DlkAssert.AssertEqual("VerifySidebarItems(): ", ExpectedValue.Trim(), GetSidebarItemsToString());
                DlkLogger.LogInfo("VerifySidebarItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySidebarItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetSidebarItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetSidebarItems(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetSidebarItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetSidebarItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetSidebarItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetSidebarItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetSidebarItemCount(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String actualTabItemCount = mSidebarItems.Count().ToString();
                DlkVariable.SetVariable(VariableName, actualTabItemCount);
                DlkLogger.LogInfo("[" + actualTabItemCount + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetSidebarItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetSidebarItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySidebarItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySidebarItemCount(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                String actualSidebarItemCount = mSidebarItems.Count().ToString();
                DlkAssert.AssertEqual("VerifySidebarItemCount(): ", ExpectedValue.Trim(), actualSidebarItemCount);
                DlkLogger.LogInfo("VerifySidebarItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySidebarItemCount() failed : " + e.Message, e);
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

        [Keyword("VerifyItemInSidebarList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInSidebarList(String SidebarItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = false;

                foreach (IWebElement sideBarItem in mSidebarItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(sideBarItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(sideBarItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(sideBarItem.Text.Trim().ToLower(), "");
                    if (currentItem == SidebarItem.ToLower())
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemInSidebarList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + SidebarItem.ToLower() + "] found in list: [" + GetSidebarItemsToString() + "]");
                DlkLogger.LogInfo("VerifyItemInSidebarList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInSidebarList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialItemInSidebarList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialItemInSidebarList(String PartialSidebarItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = false;

                foreach (IWebElement sideBarItem in mSidebarItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(sideBarItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(sideBarItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(sideBarItem.Text.Trim().ToLower(), "");
                    if (currentItem.Contains(PartialSidebarItem.ToLower()))
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyPartialItemInSidebarList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + PartialSidebarItem.ToLower() + "] found in list: [" + GetSidebarItemsToString() + "]");
                DlkLogger.LogInfo("VerifyPartialItemInSidebarList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialItemInSidebarList() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
