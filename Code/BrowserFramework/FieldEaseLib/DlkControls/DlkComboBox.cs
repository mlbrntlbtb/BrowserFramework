using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using FieldEaseLib.DlkSystem;
using System.Threading;

namespace FieldEaseLib.DlkControls
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
        
        private string comboBoxClass;
        private static string cbBox_RFDClass = "RFD";
        private static string cbBox_KClass = "K";
        private static string cbBox_RCBClass = "RCB";
        private static string cbBox_RDLClass = "RDDL";
        private IList<IWebElement> mComboBoxItems;
        private IWebElement mComboBoxList;
        private string mComboBoxItemsString;

        //K ComboBox Type Class Variables
        private static string K_comboBoxArrow_XPath = ".//span[contains(@class,'k-select')]";
        private static string K_comboBoxInput_XPath = ".//input[contains(@class,'k-input')]";
        private static string K_comboBoxList_XPath = "//div[contains(@class,'k-list-container')][contains(@class,'k-state-border')]";

        //RFD ComboBox Type Class Variables
        private static string RFD_comboBoxList_XPath = "//div[contains(@class,'rfdSelectBoxDropDown')]";

        //RCB ComboBox Type Class Variables
        private static string RCB_comboBoxArrow_XPath = ".//td[contains(@class,'rcbArrowCell')]";
        private static string RCB_comboBoxInput_XPath = ".//input[contains(@class,'rcbInput')]";
        private static string RCB_comboBoxList_XPath = "//div[contains(@class,'rcbSlide')][contains(@style,'overflow: visible')]";
        private static string RCB_comboBoxCheckAll_XPath = ".//div[contains(@class,'rcbCheckAllItems')]";

        private static string RDDL_comboBoxListIcon_Xpath = ".//span[contains(@class,'rddlIcon')]";
        private static string RDDL_comboBoxList_Xpath = "//div[contains(@class,'rddlSlide')][contains(@style,'overflow: visible')]";

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            comboBoxClass = GetComboBoxClass();
            this.ScrollIntoViewUsingJavaScript();
        }

        public string GetComboBoxClass()
        {
            string cbBox_Class = "";
            string comboBox_RFD_Class = "rfdSelect";
            string comboBox_K_Class = "k-combobox";
            string comboBox_K_Class2 = "k-dropdown";
            string comboBox_RCB_Class = "RadComboBox";
            string comboBox_RADDropDown_Class = "RadDropDownList";
            string currentClass = this.GetAttributeValue("class");

            if (currentClass.Contains(comboBox_K_Class) || currentClass.Contains(comboBox_K_Class2))
            {
                cbBox_Class = cbBox_KClass;
            }
            else if (currentClass.Contains(comboBox_RFD_Class))
            {
                cbBox_Class = cbBox_RFDClass;
            }
            else if (currentClass.Contains(comboBox_RCB_Class))
            {
                cbBox_Class = cbBox_RCBClass;
            }
            else if (currentClass.Contains(comboBox_RADDropDown_Class))
            {
                cbBox_Class = cbBox_RDLClass;
            }
            else
            {
                throw new Exception("Combo box type not supported.");
            }

            return cbBox_Class;
        }

        public void OpenComboBox()
        {
            //Ensure combobox is at close state first
            CloseComboBox();
            Thread.Sleep(1000);

            DlkLogger.LogInfo("Opening combo box list ...");
            
            if (comboBoxClass.Equals(cbBox_KClass))
            {
                IWebElement comboBox_Arrow = mElement.FindElements(By.XPath(K_comboBoxArrow_XPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(K_comboBoxArrow_XPath)) : throw new Exception("K - Combo box arrow not found.");
                comboBox_Arrow.Click();
            }
            else if (comboBoxClass.Equals(cbBox_RCBClass))
            {
                IWebElement comboBox_Arrow = mElement.FindElements(By.XPath(RCB_comboBoxArrow_XPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(RCB_comboBoxArrow_XPath)) : throw new Exception("RAD - Combo box arrow not found.");
                comboBox_Arrow.Click();
            }
            else if (comboBoxClass.Equals(cbBox_RDLClass))
            {                
                mElement.FindElements(By.XPath(RDDL_comboBoxListIcon_Xpath)).FirstOrDefault()?.Click();
            }
            else if (comboBoxClass.Equals(cbBox_RFDClass))
            {
                mElement.Click();
            }
            Thread.Sleep(2000);
        }
 
        public void CloseComboBox()
        {
            if (comboBoxClass.Equals(cbBox_KClass))
            {
                if (this.GetAttributeValue("class").Contains("k-state-border-up"))
                {
                    DlkLogger.LogInfo("Closing combo box list ...");
                    IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    activeElement.SendKeys(Keys.Escape);
                    Thread.Sleep(2000);
                }
            }
            else if (comboBoxClass.Equals(cbBox_RCBClass))
            {
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(RCB_comboBoxList_XPath)).Count > 0)
                {
                    DlkLogger.LogInfo("Closing combo box list ...");
                    IWebElement comboBox_Arrow = mElement.FindElements(By.XPath(RCB_comboBoxArrow_XPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(RCB_comboBoxArrow_XPath)) : throw new Exception("RAD - Combo box arrow not found.");
                    comboBox_Arrow.Click();
                    Thread.Sleep(2000);
                }
            }
            else if (comboBoxClass.Equals(cbBox_RFDClass))
            {
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(RFD_comboBoxList_XPath)).Count > 0)
                {
                    DlkLogger.LogInfo("Closing combo box list ...");
                    IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    activeElement.SendKeys(Keys.Escape);
                    Thread.Sleep(2000);
                }
            }
            else
            {
                IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                activeElement.SendKeys(Keys.Escape);
            }
            
        }

        public void GetComboBoxList()
        {
            if (comboBoxClass.Equals(cbBox_KClass))
            {
                mComboBoxList = DlkEnvironment.AutoDriver.FindElements(By.XPath(K_comboBoxList_XPath)).Count > 0 ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(K_comboBoxList_XPath)) : throw new Exception("K - Combobox list not found");
            }
            else if (comboBoxClass.Equals(cbBox_RCBClass))
            {
                mComboBoxList = DlkEnvironment.AutoDriver.FindElements(By.XPath(RCB_comboBoxList_XPath)).Count > 0 ?
                 DlkEnvironment.AutoDriver.FindElement(By.XPath(RCB_comboBoxList_XPath)) : throw new Exception("RCB - Combobox list not found");
            }
            else if (comboBoxClass.Equals(cbBox_RFDClass))
            {
                mComboBoxList = DlkEnvironment.AutoDriver.FindElements(By.XPath(RFD_comboBoxList_XPath)).Count > 0 ?
                  DlkEnvironment.AutoDriver.FindElement(By.XPath(RFD_comboBoxList_XPath)) : throw new Exception("RFD - Combobox list not found");
            }
            else if (comboBoxClass.Equals(cbBox_RDLClass))
            {
                IWebElement ul = DlkEnvironment.AutoDriver.FindElements(By.XPath(RDDL_comboBoxList_Xpath)).FirstOrDefault();
                mComboBoxList = ul??throw new Exception("RDDL - Combobox list not found");
            }
            
            GetComboBoxItems();
        }

        public void GetComboBoxItems()
        {
            mComboBoxItems = mComboBoxList.FindElements(By.TagName("li"))
                    .Where(x => x.Displayed).ToList();
        }

        private string GetComboBoxItemsToString()
        {
            foreach (IWebElement listItem in mComboBoxItems)
            {
                string currentItem = DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "") == "" ?
                    DlkString.ReplaceCarriageReturn(listItem.GetAttribute("title").Trim().ToLower(), "") :
                    DlkString.ReplaceCarriageReturn(listItem.Text.Trim().ToLower(), "");
                mComboBoxItemsString += currentItem + "~";
            }
            return mComboBoxItemsString.Trim('~');
        }

        public void SetValueToInputField(string Value)
        {
            IWebElement comboBoxInput = null;

            if (comboBoxClass.Equals(cbBox_KClass))
            {
                comboBoxInput = mElement.FindElements(By.XPath(K_comboBoxInput_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(K_comboBoxInput_XPath)) : throw new Exception("K - Combo box input field not found.");
            }
            else if (comboBoxClass.Equals(cbBox_RCBClass))
            {
                comboBoxInput = mElement.FindElements(By.XPath(RCB_comboBoxInput_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(RCB_comboBoxInput_XPath)) : throw new Exception("RCB - Combo box input field not found.");
            }
            else
            {
                throw new Exception("Combo box does not have input field.");
            }

            DlkLogger.LogInfo("Setting [" + Value + "] to input field... ");

            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            js.ExecuteScript("arguments[0].blur();", comboBoxInput);

            try
            {
                comboBoxInput.Clear();
            }
            catch
            {
                //cannot clear time
            }

            comboBoxInput.SendKeys(Value);
            Thread.Sleep(2000);
        }

        public void SetValueToCheckBoxField(IWebElement targetItem, string TrueOrFalse)
        {
            IWebElement checkAllInput;

            checkAllInput = targetItem.FindElements(By.TagName("input")).Count > 0 ?
                    targetItem.FindElement(By.TagName("input")) : throw new Exception("Input field not found in check all option");

            DlkCheckBox targetCheckBox = new DlkCheckBox("Target CheckBox", checkAllInput);
            targetCheckBox.Set(TrueOrFalse);
        }

        public bool GetValueCheckBoxItem(IWebElement targetItem)
        {
            bool actualValue = false;
            IWebElement checkAllInput;

            checkAllInput = targetItem.FindElements(By.TagName("input")).Count > 0 ?
                    targetItem.FindElement(By.TagName("input")) : throw new Exception("Input field not found in check all option");

            DlkCheckBox targetCheckBox = new DlkCheckBox("Target CheckBox", checkAllInput);
            return actualValue = targetCheckBox.GetState();
        }

        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Expected Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();
                OpenComboBox();
                GetComboBoxList();
                bool bFound = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem == Item.ToLower())
                    {
                        comboBoxItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + Item + "] not found. Actual list: [" + GetComboBoxItemsToString() + "]");


                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectPartialItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectPartialItem(String PartialItem)
        {
            try
            {
                Initialize();
                OpenComboBox();
                GetComboBoxList();
                bool bFound = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem.Contains(PartialItem.ToLower()))
                    {
                        comboBoxItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + PartialItem + "] not found. Actual list: [" + GetComboBoxItemsToString() + "]");


                DlkLogger.LogInfo("SelectPartialItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartialItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SetSelect", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetSelect(String Value)
        {
            try
            {
                Initialize();
                SetValueToInputField(Value);
                OpenComboBox();
                GetComboBoxList();
                bool bFound = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem.Contains(Value.ToLower()))
                    {
                        comboBoxItem.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + Value + "] not found. Actual list: [" + GetComboBoxItemsToString() + "]");


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
                SetValueToInputField(Value);
                GetComboBoxList();
                bool bFound = false;

                int targetIndex = index - 1;
                for (int c=0; c < mComboBoxItems.Count; c++)
                {
                    if (c == targetIndex)
                    {
                        mComboBoxItems[c].Click();
                        bFound = true;
                        break;
                    }
                }
             
                if (!bFound)
                    throw new Exception("[" + Value + "] not found. Actual list: [" + GetComboBoxItemsToString() + "]");


                DlkLogger.LogInfo("SetSelectByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetSelectByIndex() failed : " + e.Message, e);
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

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkBaseControl currentValue;
                string ActualValue;
                try
                {
                    ActualValue = DlkString.ReplaceCarriageReturn(mElement.GetAttribute("value"),"");
                }
                catch
                {
                    currentValue = new DlkBaseControl("Value", mElement);
                    ActualValue = DlkString.ReplaceCarriageReturn(currentValue.GetValue(), "");
                }
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

        [Keyword("GetListItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItems(String VariableName)
        {
            try
            {
                Initialize();
                OpenComboBox();
                GetComboBoxList();

                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetComboBoxItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListItems(String ExpectedValue)
        {
            try
            {
                Initialize();
                OpenComboBox();
                GetComboBoxList();
                
                String ActualValue = GetComboBoxItemsToString();
                DlkAssert.AssertEqual("VerifyListItems(): ", ExpectedValue.Trim(), ActualValue);
                DlkLogger.LogInfo("VerifyListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenGetListItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetThenGetListItems(String Value, String VariableName)
        {
            try
            {
                Initialize();
                SetValueToInputField(Value);
                GetComboBoxList();

                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetComboBoxItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("SetThenGetListItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenGetListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetListItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItemCount(String VariableName)
        {
            try
            {
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();

                String actualListItemCount = mComboBoxItems.Count().ToString();
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
        public void VerifyListItemCount(String ExpectedCount)
        {
            try
            {
                int count = 0;
                if (!int.TryParse(ExpectedCount, out count))
                    throw new Exception("[" + ExpectedCount + "] is not a valid input for parameter ExpectedCount.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();

                String actualListItemCount = mComboBoxItems.Count().ToString();
                DlkAssert.AssertEqual("VerifyListItemCount(): ", ExpectedCount, actualListItemCount);
                DlkLogger.LogInfo("VerifyListItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenGetListItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetThenGetListItemCount(String Value, String VariableName)
        {
            try
            {
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                Initialize();
                SetValueToInputField(Value);
                GetComboBoxList();

                String actualListItemCount = mComboBoxItems.Count().ToString();
                DlkVariable.SetVariable(VariableName, actualListItemCount);
                DlkLogger.LogInfo("[" + actualListItemCount + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("SetThenGetListItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenGetListItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(String Item, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if(!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();
                bool actualValue = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem == Item.ToLower())
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemInList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + Item.ToLower() + "] found in list: [" + GetComboBoxItemsToString() + "]");
                DlkLogger.LogInfo("VerifyItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialItemInList(String PartialItem, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();
                bool actualValue = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem.Contains(PartialItem.ToLower()))
                    {
                        actualValue = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyPartialItemInList() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + PartialItem.ToLower() + "] found in list: [" + GetComboBoxItemsToString() + "]");
                DlkLogger.LogInfo("VerifyPartialItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCheckAllCheckBoxItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCheckAllCheckBoxItems(String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();

                if (comboBoxClass.Equals(cbBox_RCBClass))
                {
                    IWebElement checkAllContainer = mComboBoxList.FindElements(By.XPath(RCB_comboBoxCheckAll_XPath)).Count > 0 ?
                        mComboBoxList.FindElement(By.XPath(RCB_comboBoxCheckAll_XPath)) : throw new Exception("Check all option not found in Combo box");

                    IWebElement checkAllInput = checkAllContainer.FindElements(By.TagName("input")).Count > 0 ?
                        checkAllContainer.FindElement(By.TagName("input")) : throw new Exception("Input field not found in check all option");

                    DlkCheckBox targetCheckBox = new DlkCheckBox("Target CheckBox", checkAllInput);
                    targetCheckBox.Set(TrueOrFalse);
                }
                else
                {
                    throw new Exception("Combo box type not supported with check all option.");
                }

                DlkLogger.LogInfo("SetCheckAllCheckBoxItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCheckAllCheckBoxItems() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCheckBoxItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCheckBoxItem(String Item, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();
                bool bFound = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem == Item.ToLower())
                    {
                        SetValueToCheckBoxField(comboBoxItem, TrueOrFalse);
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + Item + "] not found. Actual list: [" + GetComboBoxItemsToString() + "]");


                DlkLogger.LogInfo("SetCheckBoxItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCheckBoxItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCheckBoxItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCheckBoxItem(String Item, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                OpenComboBox();
                GetComboBoxList();
                bool bFound = false;
                bool actualValue = false;

                foreach (IWebElement comboBoxItem in mComboBoxItems)
                {
                    string currentItem = DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(comboBoxItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(comboBoxItem.Text.Trim().ToLower(), "");
                    if (currentItem == Item.ToLower())
                    {
                        actualValue = GetValueCheckBoxItem(comboBoxItem);
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + Item + "] not found. Actual list: [" + GetComboBoxItemsToString() + "]");

                DlkAssert.AssertEqual("VerifyCheckBoxItem() ", expectedValue, actualValue);
                DlkLogger.LogInfo("[" + Item.ToLower() + "] found in list: [" + GetComboBoxItemsToString() + "]");
                DlkLogger.LogInfo("VerifyCheckBoxItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCheckBoxItem() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
