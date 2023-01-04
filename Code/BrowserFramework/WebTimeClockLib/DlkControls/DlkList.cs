using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using WebTimeClockLib.DlkSystem;
using System.Threading;

namespace WebTimeClockLib.DlkControls
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

        private static string listItems_XPath = ".//option";

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
        }

        public IList<IWebElement> GetListItems()
        {
            IList<IWebElement> listItems = mElement.FindElements(By.XPath(listItems_XPath));

            if (listItems.Count == 0)
                throw new Exception("List items empty or not found.");
            else
                return listItems;
        }
        #endregion

        #region KEYWORDS
        [Keyword("SelectByValue", new String[] { "1|text|Value|TRUE" })]
        public void SelectByValue(String Value)
        {
            try
            {
                Initialize();

                bool itemFound = false;
                string actualListItems = "";

                IList<IWebElement> listItems = GetListItems();

                foreach (IWebElement item in listItems)
                {
                    DlkBaseControl listItem = new DlkBaseControl("Item", item);
                    actualListItems = actualListItems + listItem.GetValue() + "~";
                    if (DlkString.ReplaceCarriageReturn(listItem.GetValue(), "").ToLower() == Value.ToLower())
                    {
                        listItem.Click();
                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                    throw new Exception("Control : " + mControlName + " : '" + Value + "' not found in list. : Actual List = " + actualListItems);

                DlkLogger.LogInfo("Successfully executed SelectByValue()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByPartialValue", new String[] { "1|text|Value|TRUE" })]
        public void SelectByPartialValue(String PartialValue)
        {
            try
            {
                Initialize();

                bool itemFound = false;
                string actualListItems = "";

                IList<IWebElement> listItems = GetListItems();

                foreach (IWebElement item in listItems)
                {
                    DlkBaseControl listItem = new DlkBaseControl("Item", item);
                    actualListItems = actualListItems + listItem.GetValue() + "~";
                    if (DlkString.ReplaceCarriageReturn(listItem.GetValue(), "").ToLower().Contains(PartialValue.ToLower()))
                    {
                        listItem.Click();
                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                    throw new Exception("Control : " + mControlName + " : '" + PartialValue + "' not found in list. : Actual List = " + actualListItems);

                DlkLogger.LogInfo("Successfully executed SelectByPartialValue()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByPartialValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|TRUE" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                int index = 0;
                if (!int.TryParse(Index, out index) || index == 0)
                    throw new Exception("[" + Index + "] is not a valid input for parameter Index.");

                Initialize();

                IList<IWebElement> listItems = GetListItems();

                if (index > listItems.Count)
                    throw new Exception("Input index exceeds total number of list items.");

                for (int i=0; i < listItems.Count; i++)
                {
                    if(i == index - 1)
                    {
                        listItems[i].Click();
                        break;
                    }
                }

                DlkLogger.LogInfo("Successfully executed SelectByIndex()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
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

        [Keyword("GetListItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItemCount(String VariableName)
        {
            try
            {
                Initialize();

                int listCount = 0;
                IList<IWebElement> listItems = mElement.FindElements(By.XPath(listItems_XPath));
                listCount = listItems.Count;
                
                DlkVariable.SetVariable(VariableName, listCount.ToString());
                DlkLogger.LogInfo("[" + listCount.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListItemByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItemByIndex(String Index, String VariableName)
        {
            try
            {
                int index = 0;
                if (!int.TryParse(Index, out index) || index == 0)
                    throw new Exception("[" + Index + "] is not a valid input for parameter Index.");

                Initialize();
                string ActualValue = "";

                IList<IWebElement> listItems = GetListItems();

                if (index > listItems.Count)
                    throw new Exception("Input index exceeds total number of list items.");

                for (int i = 0; i < listItems.Count; i++)
                {
                    if (i == index - 1)
                    {
                        ActualValue = listItems[i].Text.Trim();
                        DlkVariable.SetVariable(VariableName, ActualValue);
                        DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                        break;
                    }
                }

                DlkLogger.LogInfo("GetListItemByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItemByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListItemByPartialValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItemByPartialValue(String PartialValue, String VariableName)
        {
            try
            {
                Initialize();
                string ActualValue = "";

                IList<IWebElement> listItems = GetListItems();
                
                foreach (IWebElement item in listItems)
                {
                    ActualValue = item.Text.Trim();

                    if (ActualValue.Contains(PartialValue))
                    {
                        DlkVariable.SetVariable(VariableName, ActualValue);
                        DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                        break;
                    }
                }

                DlkLogger.LogInfo("GetListItemByPartialValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItemByPartialValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItemByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListItemByIndex(String Index, String ExpectedValue)
        {
            try
            {
                int index = 0;
                if (!int.TryParse(Index, out index) || index == 0)
                    throw new Exception("[" + Index + "] is not a valid input for parameter Index.");

                Initialize();
                string ActualValue = "";

                IList<IWebElement> listItems = GetListItems();

                if (index > listItems.Count)
                    throw new Exception("Input index exceeds total number of list items.");

                for (int i = 0; i < listItems.Count; i++)
                {
                    if (i == index - 1)
                    {
                        ActualValue = listItems[i].Text.Trim();
                        DlkAssert.AssertEqual("VerifyListItemByIndex() :", ExpectedValue, ActualValue);
                        break;
                    }
                }

                DlkLogger.LogInfo("VerifyListItemByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItemByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItemByPartialValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListItemByPartialValue(String PartialValue, String ExpectedValue)
        {
            try
            {
                Initialize();
                string ActualValue = "";

                IList<IWebElement> listItems = GetListItems();

                foreach (IWebElement item in listItems)
                {
                    ActualValue = item.Text.Trim();
                    if (ActualValue.Contains(PartialValue))
                    {
                        DlkAssert.AssertEqual("VerifyListItemByPartialValue() :", ExpectedValue, ActualValue);
                        break;
                    }
                }

                DlkLogger.LogInfo("VerifyListItemByPartialValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItemByPartialValue() failed : " + e.Message, e);
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
