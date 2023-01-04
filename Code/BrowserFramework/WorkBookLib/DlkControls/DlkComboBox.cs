using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace WorkBookLib.DlkControls
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

        private static string dropDownListXPath = "//div[@id='select2-drop']//ul";
        private static string dropDownListXPath2 = "//ul[@class='wbcontextmenu'][not(contains(@class,'hidden'))]";
        private static string dropDownListXPath3 = "//ul[contains(@class,'wbcontextmenu ')][not(contains(@class,'hidden'))]";
        private static string dropDownListXPath4 = "//div[contains(@class,'select2-drop-active')][not((contains(@style,'display: none')))]//ul";
        private static string dropDownItemXPath = "//li//div[@role='option']";
        private static string dropDownItemXPath2 = ".//li[contains(@class,'item')]/span";
        private static string dropDownItemXPath3 = ".//li[contains(@class,'select2')]";
        private static string dropDownInnerItemXPath = ".//parent::li//ul[contains(@class,'childContainer')]";
        private static string dropDownInputXPath = "//input[contains(@class,'select2-input')][contains(@class,'select2-focused')]";
        private static string dropDownInputXPath2 = "//div[@id='select2-drop']//input";
        private static string mDropMaskXPath = "//*[contains(@id,'select2-drop-mask')][not((contains(@style,'display: none')))]";
        private static string mContextMaskXpath = "//*[contains(@class,'contextmenuOverlay')]";
        private static string closeClearButtonXPath = ".//abbr[contains(@class,'close')]";

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        public void OpenDropdown()
        {
            //Ensure dropdown is at close state first
            CloseDropdown();

            DlkLogger.LogInfo("Opening dropdown list ...");

            if (DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath)).Count == 0 &&
                DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath2)).Count == 0 &&
                DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath3)).Count == 0)
                mElement.Click();

            //Check if list items are still loading
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
        }

        public void CloseDropdown()
        {
            if (DlkEnvironment.AutoDriver.FindElements(By.XPath(mDropMaskXPath)).Count > 0 || 
                DlkEnvironment.AutoDriver.FindElements(By.XPath(mContextMaskXpath)).Count > 0)
            {
                IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                activeElement.SendKeys(Keys.Escape);
            }
        }

        public void SetValueToInputField(string Value)
        {
            IWebElement dropDownInput = mElement.FindWebElementCoalesce(By.XPath(dropDownInputXPath), By.XPath(dropDownInputXPath2));
            if (dropDownInput == null)
                throw new Exception("Drop down input text box not found.");

            DlkTextBox DropDownBox = new DlkTextBox("Dropdown TextBox", dropDownInput);

            DropDownBox.ScrollIntoViewUsingJavaScript();
            DropDownBox.Set(Value);

            //Check if list items are still loading
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
        }
        
        public IWebElement GetDropdownList()
        {
            IWebElement dropDownList = DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath)).Count > 0 ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath)) :
                DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath2)).Count > 0 ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath2)) :
                DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownListXPath3)).Count > 0 ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath3)) :
                DlkEnvironment.AutoDriver.FindElement(By.XPath(dropDownListXPath4));

            return dropDownList;
        }

        public string GetDropdownItems(IWebElement dropDownList)
        {
            string ActualDropdownItems = "";
            IList<IWebElement> dropDownItems = dropDownList.FindWebElementsCoalesce(false, By.XPath(dropDownItemXPath), By.XPath(dropDownItemXPath2), By.XPath(dropDownItemXPath3))
                    .Where(x => x.Displayed).ToList();

            foreach (IWebElement item in dropDownItems)
            {
                DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                ActualDropdownItems = ActualDropdownItems + dropDownItem.GetValue() + "~";
            }

            return ActualDropdownItems.Trim('~');
        }
        #endregion

        #region KEYWORDS
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                bool bFound = false;
                string ActualDropdownItems = "";

                OpenDropdown();

                Thread.Sleep(1000);
                IWebElement dropDownList = GetDropdownList();

                IList<IWebElement> dropDownItems = dropDownList.FindWebElementsCoalesce(false, By.XPath(dropDownItemXPath), By.XPath(dropDownItemXPath2))
                    .Where(x=>x.Displayed).ToList();

                if (!Value.Contains('~'))
                {
                    foreach (IWebElement item in dropDownItems)
                    {
                        DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                        ActualDropdownItems = ActualDropdownItems + dropDownItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(dropDownItem.GetValue(),"").ToLower() == Value.ToLower())
                        {
                            dropDownItem.Click();
                            bFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    string[] value = Value.Split('~');
                    
                    foreach (IWebElement item in dropDownItems)
                    {
                        DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                        ActualDropdownItems = ActualDropdownItems + dropDownItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(dropDownItem.GetValue(), "").ToLower() == value[0].ToLower())
                        {
                            dropDownItem.Click();
                            IWebElement dropDownInnerItem = item.FindElement(By.XPath(dropDownInnerItemXPath));
                            IList<IWebElement> dropDownInnerItems = dropDownInnerItem.FindElements(By.XPath(".//li/span"))
                                .Where(x => x.Displayed).ToList(); ;

                            foreach (IWebElement dDownInnerItem in dropDownInnerItems)
                            {
                                DlkBaseControl dDownItem = new DlkBaseControl("Item", dDownInnerItem);
                                ActualDropdownItems = ActualDropdownItems + dropDownItem.GetValue() + "~";
                                if (DlkString.ReplaceCarriageReturn(dDownItem.GetValue(),"").ToLower() == value[1].ToLower())
                                {
                                    dDownItem.Click();
                                    bFound = true;
                                    break;
                                }
                            }
                        }

                        if (bFound)
                            break;

                    }
                }

                CloseDropdown();
                if (!bFound)
                    throw new Exception("Control : " + mControlName + " : '" + Value + "' not found in list. : Actual List = " + ActualDropdownItems);

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
                bool bFound = false;

                OpenDropdown();

                Thread.Sleep(1000);
                IWebElement dropDownList = GetDropdownList();
                IList<IWebElement> dropDownItems = dropDownList.FindWebElementsCoalesce(false, By.XPath(dropDownItemXPath), By.XPath(dropDownItemXPath2))
                    .Where(x => x.Displayed).ToList();

                int targetIndex = index - 1;

                for (int i = 0; i < dropDownItems.Count; i++)
                {
                    DlkBaseControl dropDownItem = new DlkBaseControl("Item", dropDownItems[i]);
                    if (i == targetIndex)
                    {
                        dropDownItem.Click();
                        bFound = true;
                        break;
                    }
                }

                CloseDropdown();
                if (!bFound)
                    throw new Exception("Control : " + mControlName + " : Item at index [" + Index + "] not found in list. : Actual List Count = [" + dropDownItems.Count.ToString() + "]");

                DlkLogger.LogInfo("Successfully executed SelectByIndex()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SetSelect", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetSelect(String Value)
        {
            try
            {
                Initialize();
                OpenDropdown();

                SetValueToInputField(Value);

                string ActualDropdownItems = "";
                bool bFound = false;
                IWebElement dropDownList = GetDropdownList();
                IList<IWebElement> dropDownItems = dropDownList.FindElements(By.XPath(dropDownItemXPath))
                    .Where(x => x.Displayed).ToList();

                if (!Value.Contains("~"))
                {
                    foreach (IWebElement item in dropDownItems)
                    {
                        DlkBaseControl dropDownItem = new DlkBaseControl("Item", item);
                        ActualDropdownItems = ActualDropdownItems + dropDownItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(dropDownItem.GetValue(),"").ToLower() == Value.ToLower())
                        {
                            dropDownItem.Click();
                            bFound = true;
                            break;
                        }
                    }
                }

                CloseDropdown();
                if (!bFound)
                    throw new Exception("Control : " + mControlName + " : '" + Value + "' not found in current list. : Current List = " + ActualDropdownItems);

                DlkLogger.LogInfo("SetSelect() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetSelect() failed : " + e.Message, e);
            }
        }

        [Keyword("SetSelectByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetSelectByIndex(String Value, String Index)
        {
            try
            {
                int index = 0;
                if (!int.TryParse(Index, out index))
                    throw new Exception("[" + Index + "] is not a valid input for parameter Index.");

                Initialize();
                OpenDropdown();

                SetValueToInputField(Value);
                
                string ActualDropdownItems = "";
                bool bFound = false;
                IWebElement dropDownList = GetDropdownList();
                IList<IWebElement> dropDownItems = dropDownList.FindElements(By.XPath(dropDownItemXPath))
                    .Where(x => x.Displayed).ToList();

                int targetIndex = index - 1;
                if (!Value.Contains("~"))
                {
                    for(int i = 0; i < dropDownItems.Count; i++)
                    {
                        DlkBaseControl dropDownItem = new DlkBaseControl("Item", dropDownItems[i]);
                        ActualDropdownItems = ActualDropdownItems + dropDownItem.GetValue() + "~";
                        if (i == targetIndex)
                        {
                            dropDownItem.Click();
                            bFound = true;
                            break;
                        }
                    }
                }

                CloseDropdown();
                if (!bFound)
                    throw new Exception("Control : " + mControlName + " : Item at index [" + Index + "] not found in list. : Actual List Count = [" + dropDownItems.Count.ToString() + "]");

                DlkLogger.LogInfo("SetSelectByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetSelectByIndex() failed : " + e.Message, e);
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
                OpenDropdown();

                IWebElement dropDownList = GetDropdownList();
                IList<IWebElement> dropDownItems = dropDownList.FindElements(By.XPath(dropDownItemXPath))
                    .Where(x => x.Displayed).ToList();

                int listCount = dropDownItems.Count;
                CloseDropdown();
                DlkVariable.SetVariable(VariableName, listCount.ToString());
                DlkLogger.LogInfo("[" + listCount.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenGetListCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetThenGetListCount(String Value, String VariableName)
        {
            try
            {
                Initialize();
                OpenDropdown();

                SetValueToInputField(Value);
                
                IWebElement dropDownList = GetDropdownList();
                IList<IWebElement> dropDownItems = dropDownList.FindElements(By.XPath(dropDownItemXPath))
                    .Where(x => x.Displayed).ToList();

                int listCount = dropDownItems.Count;
                CloseDropdown();
                DlkVariable.SetVariable(VariableName, listCount.ToString());
                DlkLogger.LogInfo("[" + listCount.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("SetThenGetListCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenGetListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();

                DlkBaseControl currentValue = new DlkBaseControl("Value", mElement);
                string ActualValue = DlkString.ReplaceCarriageReturn(currentValue.GetValue(),"");
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

                DlkBaseControl currentValue = new DlkBaseControl("Value", mElement);
                string ActualValue = DlkString.ReplaceCarriageReturn(currentValue.GetValue(), "");
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

        [Keyword("ClearField", new String[] { "1|text|Value|TRUE" })]
        public void ClearField()
        {
            try
            {
                Initialize();

                string currentValue = mElement.Text.Trim();

                IWebElement closeButton = mElement.FindElements(By.XPath(closeClearButtonXPath)).Count > 0 ?
                        mElement.FindElement(By.XPath(closeClearButtonXPath)) : throw new Exception("Close clear button not found. Clearing of field not applicable.");

                if (!String.IsNullOrEmpty(currentValue))
                {
                    try
                    {
                        Actions action = new Actions(DlkEnvironment.AutoDriver);
                        action.MoveToElement(closeButton).Click().Build().Perform();
                    }
                    catch
                    {
                        DlkLogger.LogInfo("Clicking of close or clear button is not applicable with this type of dropdown.");
                        throw new Exception();
                    }
                    
                }
                else
                {
                    DlkLogger.LogInfo("Dropdown has no value. Clearing of value not executed... ");
                }

                DlkLogger.LogInfo("ClearField() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClearField() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItems", new String[] { "1|text|Value|TRUE" })]
        public void VerifyListItems(String ExpectedValue)
        {
            try
            {
                Initialize();
                OpenDropdown();

                Thread.Sleep(1000);
                IWebElement dropDownList = GetDropdownList();
                string actualDropDownItems = GetDropdownItems(dropDownList);

                CloseDropdown();

                DlkAssert.AssertEqual("VerifyListItems(): ", ExpectedValue.ToLower(), actualDropDownItems.ToLower().Trim());
                DlkLogger.LogInfo("VerifyListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListItems", new String[] { "1|text|Value|TRUE" })]
        public void GetListItems(String VariableName)
        {
            try
            {
                Initialize();
                OpenDropdown();

                Thread.Sleep(1000);
                IWebElement dropDownList = GetDropdownList();
                string actualDropDownItems = GetDropdownItems(dropDownList);

                CloseDropdown();

                DlkVariable.SetVariable(VariableName, actualDropDownItems);
                DlkLogger.LogInfo("[" + actualDropDownItems + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItems() failed : " + e.Message, e);
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
