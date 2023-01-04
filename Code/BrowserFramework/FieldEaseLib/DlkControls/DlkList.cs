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
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkList(String ControlName, String SearchType, String SearchValue)
          : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES

        //RLB List Class
        private static string RLB_listContainer_XPath = ".//ul[contains(@class,'rlbList')]";
        private static string RLB_listItem_XPath = ".//li[contains(@class,'rlbItem')]";

        //MS List Class
        private static string MS_listContainer_XPath = ".//ul[contains(@class,'ms-list')]";
        private static string MS_listItem_XPath = ".//li[contains(@class,'ms-elem')]";

        private IWebElement mlistContainer;
        private IList<IWebElement> mListItems;
        private string mListItemsString;
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetListItems();
        }

        #endregion

        #region PRIVATE METHODS

        private void GetListItems()
        {
            if (mElement.FindElements(By.XPath(RLB_listContainer_XPath)).Count > 0)
            {
                mlistContainer = mElement.FindElement(By.XPath(RLB_listContainer_XPath));
                mListItems = mlistContainer.FindElements(By.XPath(RLB_listItem_XPath))
                .Where(x => x.Displayed).ToList();
            }
            else if (mElement.FindElements(By.XPath(MS_listContainer_XPath)).Count > 0)
            {
                mlistContainer = mElement.FindElement(By.XPath(MS_listContainer_XPath));
                mListItems = mlistContainer.FindElements(By.XPath(MS_listItem_XPath))
                .Where(x => x.Displayed).ToList();
            }
            else
            {
                var li = mElement.FindElements(By.XPath(".//li"));
                if (li.Count() > 0)
                {
                    mListItems = li;
                }
                else
                    throw new Exception("List type not supported.");
            }
        }

        private string GetListItemsToString()
        {
            foreach (IWebElement listItem in mListItems)
            {
                string currentItem = DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "") == "" ?
                    DlkString.ReplaceCarriageReturn(listItem.GetAttribute("title").Trim().ToLower(), "") :
                    DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "");
                mListItemsString += currentItem + "~";
            }
            return mListItemsString.Trim('~');
        }

        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String ListItem)
        {
            try
            {
                Initialize();
                bool bFound = false;

                foreach (IWebElement listItem in mListItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(listItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "");
                    if (currentItem == ListItem.ToLower())
                    {
                        listItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + ListItem + "] not found. Actual list: [" + GetListItemsToString() + "]");

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListItems(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                DlkAssert.AssertEqual("VerifyListItems(): ", ExpectedValue.Trim(), GetListItemsToString());
                DlkLogger.LogInfo("VerifyListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(String ListItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = false;

                foreach (IWebElement listItem in mListItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(listItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "");
                    if (currentItem == ListItem.ToLower())
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemInList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + ListItem.ToLower() + "] found in list: [" + GetListItemsToString() + "]");
                DlkLogger.LogInfo("VerifyItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialItemInList(String PartialListItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = false;

                foreach (IWebElement listItem in mListItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(listItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "");
                    if (currentItem.Contains(PartialListItem.ToLower()))
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyPartialItemInList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + PartialListItem.ToLower() + "] found in list: [" + GetListItemsToString() + "]");
                DlkLogger.LogInfo("VerifyPartialItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItems(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetListItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItemCount(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String actualListItemCount = mListItems.Count().ToString();
                DlkVariable.SetVariable(VariableName, actualListItemCount);
                DlkLogger.LogInfo("[" + actualListItemCount + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListItemCount(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                String actualListItemCount = mListItems.Count().ToString();
                DlkAssert.AssertEqual("VerifyListItemCount(): ", ExpectedValue.Trim(), actualListItemCount);
                DlkLogger.LogInfo("VerifyListItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItemCount() failed : " + e.Message, e);
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

        #endregion
    }
}
