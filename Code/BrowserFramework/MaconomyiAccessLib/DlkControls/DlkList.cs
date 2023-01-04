using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Linq;
using System.Collections.Generic;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        #region CONSTRUCTORS

        public DlkList(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkTextBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        #endregion

        #region PRIVATE METHODS

        private void SelectItem(String Item, bool Partial = false)
        {
            bool itemFound = false;
            List<IWebElement> targetListItems = GetListItems();

            foreach (IWebElement item in targetListItems)
            {
                DlkBaseControl listItem = new DlkBaseControl("List Item", item);

                if (!Partial)
                {
                    if (DlkString.ReplaceCarriageReturn(listItem.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                    {
                        listItem.Click();
                        itemFound = true;
                    }
                }
                else
                {
                    if (DlkString.ReplaceCarriageReturn(listItem.GetValue(), "").Contains(DlkString.ReplaceCarriageReturn(Item, "")))
                    {
                        listItem.Click();
                        itemFound = true;
                    }
                }
            }

            if (!itemFound)
            {
                throw new Exception("Item [" + Item + "] not found on the list.");
            }
        }

        private void SelectItemByIndex(int Index)
        {
            List<IWebElement> targetListItems = GetListItems();

            if(targetListItems.Count >= Index)
            {
                DlkBaseControl targetListItem = new DlkBaseControl("Target List Item", targetListItems.ElementAt(Index - 1));
                targetListItem.ScrollIntoViewUsingJavaScript();
                targetListItem.Click();
            }
            else
            {
                throw new Exception("Index exceeds maximum items on the list.");
            }
        }

        private IWebElement GetItem(String Item)
        {
            bool itemFound = false;
            List<IWebElement> targetListItems = GetListItems();
            IWebElement targetItem = null;

            foreach (IWebElement item in targetListItems)
            {
                DlkBaseControl listItem = new DlkBaseControl("List Item", item);
                if (DlkString.ReplaceCarriageReturn(listItem.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                {
                    targetItem = listItem.mElement;
                    itemFound = true;
                }
            }

            if (!itemFound)
            {
                throw new Exception("Item [" + Item + "] not found on the list.");
            }

            return targetItem;
        }

        private IWebElement GetItemByIndex(int Index)
        {
            List<IWebElement> targetListItems = GetListItems();

            if (targetListItems.Count >= Index)
            {
                return targetListItems.ElementAt(Index - 1);
            }
            else
            {
                throw new Exception("Index exceeds maximum items on the list.");
            }
        }

        private List<IWebElement> GetListItems()
        {
            List<IWebElement> targetListItems = null;
            string mClass = mElement.GetAttribute("class").ToString();

            switch (mClass)
            {
                case string m when mClass.Contains("dropdown-menu settings"):
                    targetListItems = mElement.FindElements(By.XPath("./descendant::li/a")).Where(x => x.Displayed).ToList();
                    break;

                case string m when mClass.Contains("dropdown-menu dropdown-menu-right"):
                    targetListItems = mElement.FindElements(By.XPath("./descendant::a")).Where(x => x.Displayed).ToList();
                    break;

                case string m when mClass.Contains("flex-container"):
                    targetListItems = mElement.FindElements(By.XPath("./div[contains(@class,'flex-item')]")).Where(x => x.Displayed).ToList();
                    break;

                case string m when mClass.Contains("grid"):
                    targetListItems = mElement.FindElements(By.XPath(".//*[contains(@class,'d-flex')]")).Where(x => x.Displayed).ToList();
                    break;
                default:
                    targetListItems = mElement.FindElements(By.XPath("./descendant::*[contains(@class,'ng-scope')]")).Where(x => x.Displayed).ToList();
                    targetListItems = targetListItems == null ? mElement.FindElements(By.XPath("./descendant::a")).Where(x => x.Displayed).ToList() :
                        targetListItems;
                    break;
            }

            if (targetListItems == null)
            {
                throw new Exception("Item list not found.");
            }

            return targetListItems;
        }

        private String GetListItemsString(bool LowerCase = true)
        {
            string actual = "";
            //Get actual list items
            List<IWebElement> targetListItems = GetListItems();
            foreach (IWebElement item in targetListItems)
            {
                DlkBaseControl targetItem = new DlkBaseControl("Target Item", item);
                actual += DlkString.ReplaceCarriageReturn(targetItem.GetValue(), "") + "~";
            }

            actual = LowerCase ? actual.ToLower().Trim('~') : actual.Trim('~');
            return actual;
        }

        private bool VerifyItemInList(String Item, bool Partial = false, bool LowerCase = true)
        {
            bool itemFound = false;
            List<IWebElement> targetListItems = GetListItems();

            foreach (IWebElement item in targetListItems)
            {
                DlkBaseControl listItem = new DlkBaseControl("List Item", item);
                string actualValue = LowerCase ? DlkString.ReplaceCarriageReturn(listItem.GetValue(), "").ToLower() : DlkString.ReplaceCarriageReturn(listItem.GetValue(), "");
                string expectedValue = LowerCase ? DlkString.ReplaceCarriageReturn(Item.ToLower(), "") : DlkString.ReplaceCarriageReturn(Item, "");

                if (!Partial)
                {
                    if (actualValue == expectedValue)
                    {
                        itemFound = true;
                    }
                }
                else
                {
                    if (actualValue.Contains(expectedValue))
                    {
                        itemFound = true;
                    }
                }
               
            }
            return itemFound;
        }

        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();
                SelectItem(Item); //Select item
                DlkLogger.LogInfo("Select() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectByIndex(String NonZeroIndex)
        {
            try
            {
                Initialize();

                int index = 0;
                if (!int.TryParse(NonZeroIndex, out index) || index == 0)
                    throw new Exception("[" + NonZeroIndex + "] is not a valid input for parameter NonZeroIndex.");

                SelectItemByIndex(index); //Select item by index
                DlkLogger.LogInfo("SelectByIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLineOption", new String[] { "1|text|Value|SampleValue" })]
        public void SelectLineOption(String Item, String LineOption)
        {
            try
            {
                Initialize();
                IWebElement targetItem = GetItem(Item); //Get target item
                // Select line option
                IWebElement targetLineOption = targetItem.FindElements(By.TagName("button")).Count > 0 ?
                    targetItem.FindElement(By.TagName("button")) :
                    targetItem.FindElements(By.TagName("i")).Count > 0 ?
                     targetItem.FindElement(By.TagName("i")) :
                     throw new Exception("Line option [" + LineOption + "] not found.");

                DlkBaseControl targetOption = new DlkBaseControl("Target Line Option", targetLineOption);
                targetOption.MouseOver();
                targetOption.Click();
                DlkLogger.LogInfo("SelectLineOption() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLineOption() failed : " + e.Message, e);
            }
        }

        [Keyword("GetItemByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetItemByIndex(String NonZeroIndex, String VariableName)
        {
            try
            {
                Initialize();

                int index = 0;
                if (!int.TryParse(NonZeroIndex, out index) || index == 0)
                    throw new Exception("[" + NonZeroIndex + "] is not a valid input for parameter NonZeroIndex.");

                DlkBaseControl targetItem = new DlkBaseControl("List Item", GetItemByIndex(index)); //Get item by index
                string actualValue = DlkString.ReplaceCarriageReturn(targetItem.GetValue(), "");

                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetItemByIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetItemByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListCount(string VariableName)
        {
            try
            {
                Initialize();
                List<IWebElement> targetListItems = GetListItems(); // Get list items
                int actual = targetListItems.Count;

                DlkVariable.SetVariable(VariableName, actual.ToString());
                DlkLogger.LogInfo("[" + actual.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetList", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetList(string VariableName)
        {
            try
            {
                Initialize();

                string actual = GetListItemsString(); //Get string of list items

                DlkVariable.SetVariable(VariableName, actual);
                DlkLogger.LogInfo("[" + actual + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetList() failed : " + e.Message, e);
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
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyList(string Items)
        {
            try
            {
                Initialize();

                string actual = GetListItemsString(); //Get string of list items
                string expected = "";

                // process expected
                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                DlkAssert.AssertEqual("VerifyList()", expected, actual);
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCount(string Count)
        {
            try
            {
                Initialize();
                List<IWebElement> targetListItems = GetListItems(); // Get list items
                int actual = targetListItems.Count;
                int expected = int.Parse(Count);

                DlkAssert.AssertEqual("VerifyCount()", expected, actual);
                DlkLogger.LogInfo("VerifyCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(string Item, string TrueOrFalse)
        {
            try
            {
                Initialize();

                bool actual = VerifyItemInList(Item); //Verify item in list 
                bool expected = bool.Parse(TrueOrFalse);

                DlkAssert.AssertEqual("VerifyItemInList()", expected, actual);
                DlkLogger.LogInfo("VerifyItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialTextInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialTextInList(string Item, string TrueOrFalse)
        {
            try
            {
                Initialize();

                bool actual = VerifyItemInList(Item, true); //Verify partial item in list
                bool expected = bool.Parse(TrueOrFalse);

                DlkAssert.AssertEqual("VerifyPartialTextInList()", expected, actual);
                DlkLogger.LogInfo("VerifyPartialTextInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialTextInList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectPartialTextInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectPartialTextInList(string Item)
        {
            try
            {
                Initialize();
                SelectItem(Item, true); //Select partial item
                DlkLogger.LogInfo("SelectPartialTextInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartialTextInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactList(string Items)
        {
            try
            {
                Initialize();

                string actual = GetListItemsString(false); //Get exact string of list items without lower casing
                string expected = "";

                // process expected
                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                DlkAssert.AssertEqual("VerifyExactList()", expected, actual);
                DlkLogger.LogInfo("VerifyExactList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactList() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExactItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactItemInList(string Item, string TrueOrFalse)
        {
            try
            {
                Initialize();

                bool actual = VerifyItemInList(Item, false, false); //Verify exact item in list without lower case
                bool expected = bool.Parse(TrueOrFalse);

                DlkAssert.AssertEqual("VerifyExactItemInList()", expected, actual);
                DlkLogger.LogInfo("VerifyExactItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactPartialTextInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactPartialTextInList(string Item, string TrueOrFalse)
        {
            try
            {
                Initialize();

                bool actual = VerifyItemInList(Item, true, false); //Verify partial item in list without lower case
                bool expected = bool.Parse(TrueOrFalse);
                
                DlkAssert.AssertEqual("VerifyExactPartialTextInList()", expected, actual);
                DlkLogger.LogInfo("VerifyExactPartialTextInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactPartialTextInList() failed : " + e.Message, e);
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

        #endregion
    }
}
