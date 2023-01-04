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
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES

        private static string dropDownListItems = ".//option";
        
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
        }

        //public void OpenDropdown()
        //{
        //    //Ensure dropdown is at close state first
        //    CloseDropdown();

        //    DlkLogger.LogInfo("Opening dropdown list ...");

        //    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath)).Count == 0 &&
        //        DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath2)).Count == 0 &&
        //        DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath3)).Count == 0)
        //        mElement.Click();
        //}

        //public void CloseDropdown()
        //{
        //    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(mDropMaskXPath)).Count > 0)
        //    {
        //        IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
        //        activeElement.SendKeys(Keys.Escape);
        //    }
        //}
        
        //public IWebElement GetDropdownList()
        //{
        //    IWebElement dropDownList = DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath)).Count > 0 ?
        //        DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath)) :
        //        DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath2)).Count > 0 ?
        //        DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath2)) :
        //        DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath3));

        //    return dropDownList;
        //}

        public IList<IWebElement> GetDropdownItems()
        {
            IList<IWebElement> dropDownItems = mElement.FindElements(By.XPath(dropDownListItems));

            if (dropDownItems.Count == 0)
                throw new Exception("Combo box items not found.");
            else
                return dropDownItems;
        }
        #endregion

        #region KEYWORDS
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();

                bool itemFound = false;
                string actualDropdownItems = "";

                //Open dropdown
                mElement.Click();

                IList<IWebElement> dropDownItems = GetDropdownItems();
                
                if (!Value.Contains('~'))
                {
                    foreach (IWebElement item in dropDownItems)
                    {
                        DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                        actualDropdownItems = actualDropdownItems + dropDownItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(dropDownItem.GetValue(), "").ToLower() == Value.ToLower())
                        {
                            dropDownItem.Click();
                            itemFound = true;
                            break;
                        }
                    }
                }

                //Close dropdown
                mElement.Click();

                if (!itemFound)
                    throw new Exception("Control : " + mControlName + " : '" + Value + "' not found in list. : Actual List = " + actualDropdownItems);

                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|TRUE" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                int index = 0;
                if (!int.TryParse(Index, out index))
                    throw new Exception("[" + Index + "] is not a valid input for parameter Index.");

                Initialize();

                bool itemFound = false;

                //Open dropdown
                mElement.Click();

                IList<IWebElement> dropDownItems = GetDropdownItems();

                int targetIndex = index - 1;

                for (int i = 0; i < dropDownItems.Count; i++)
                {
                    DlkBaseControl dropDownItem = new DlkBaseControl("Item", dropDownItems[i]);
                    if (i == targetIndex)
                    {
                        dropDownItem.Click();
                        itemFound = true;
                        break;
                    }
                }

                //Close dropdown
                mElement.Click();

                if (!itemFound)
                    throw new Exception("Control : " + mControlName + " : Item at index [" + Index + "] not found in list. : Actual List Count = [" + dropDownItems.Count.ToString() + "]");

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

        [Keyword("GetListCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListCount(String VariableName)
        {
            try
            {
                Initialize();

                int listCount = 0;

                //Open dropdown
                mElement.Click();

                IList<IWebElement> dropDownItems = GetDropdownItems();
                listCount = dropDownItems.Count;

                //Close dropdown
                mElement.Click();

                DlkVariable.SetVariable(VariableName, listCount.ToString());
                DlkLogger.LogInfo("[" + listCount.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();

                string ActualValue = "";

                //Open dropdown
                mElement.Click();

                IList<IWebElement> dropDownItems = GetDropdownItems();

                foreach (IWebElement item in dropDownItems)
                {
                    DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                    if (item.GetAttribute("selected") != null)
                    {
                        ActualValue = dropDownItem.GetValue().Trim();
                        break;
                    }
                }

                //Close dropdown
                mElement.Click();
                
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");

                DlkLogger.LogInfo("GetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();

                string ActualValue = "";

                //Open dropdown
                mElement.Click();

                IList<IWebElement> dropDownItems = GetDropdownItems();

                foreach (IWebElement item in dropDownItems)
                {
                    DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                    if (item.GetAttribute("selected") != null)
                    {
                        ActualValue = dropDownItem.GetValue().Trim();
                        break;
                    }
                }

                //Close dropdown
                mElement.Click();

                DlkAssert.AssertEqual("VerifyValue(): ", ExpectedValue, ActualValue);

                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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
