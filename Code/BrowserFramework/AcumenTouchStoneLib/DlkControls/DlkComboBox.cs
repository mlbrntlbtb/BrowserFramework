using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {

        private const String DDwnClass = "ddwnInput";
        private const String DDwnContainerClass = "dropdown-field-container";
        private const String DDwnContainerClass2 = "dropdown-field";
        private const String CoreDdwnField = "core_dropdown_field";
        private const String CoreFieldClass = "core-field";
        private const String PopupMenuClass = "popupmenu";
        private const String PopupMenu2Class = "popup-menu";
        private const String PopupMenu3Class = "search-filter narrow";
        private const String DropDownClass = "dropdown"; //Database field on Login screen
        private const String DDwnColClass = "ddwnCol"; //regular drop-down in the table
        private const String SearchClass = "search-input-container";
        private const String SearchListFluid = "navigator_ngcrm_search_list fluid"; //Search List
        private const String SearchList = "navigator_ngcrm_search_list"; //Search List
        private const String SearchColList = "ddwnCol navigator_ngcrm_search_list"; //Search List in a table
        private const String QuickEdit = "navigator_ngcrm_quick_edit";
        private const String FormDdown = "formDdwn navigator_ngcrm_search_list"; //Copy Timesheet
        private const String SelectOption = "";
        private const String CustomDivClass = "div";
        private const String ModeChooserClass = "mode-chooser"; //Used by Application Mode in login

        private String ddwnInputListClass = "ddwnTableDiv";
        private String newDdwnInputListClass = "dropdown-list";
        private String mDDwnArrowDownXPATH = ".//*[contains(@class,'ddwnArrow')]";
        private String newDDwnArrowDownXPATH = ".//*[contains(@class,'dropdown-arrow')]";
        private String mSearchArrowXpath = ".//*[contains(@class,'tap-target')]";
        private String mOptionArrowXpath = ".//*[contains(@class,'icon-droparrow')]";
        private String mSearchListXPath = "//ul[contains(@class,'results')]";
        private String mCoreFieldColListXPATH = ".//*[contains(@class, 'format-list')]";
        private String mSearchArrowXpath2 = ".//*[contains(@class,'show-results')]";
        private String mArrowDownCSS = "div>div";
        private String mSearchListClass = "results";
        private String mOptionListClass = "popup-body";
        private String mComboBoxType = "";
        private String mPopupArrowXPath = ".//*[contains(@class,'arrow')]";
        private String mPopupListClass = "popupmenuBody";
        private String mPopupListClass2 = "popupmenu";
        private bool bIsMergeProduct = false;

        //DDwn Controls
        private String mInputCSS = "input";

        //Popup Controls
        private String mPopupInputXPath = "./span[1]";
        private String mPopupInputXPath2 = "./span[2]";
        //Search Controls
        private String mSearchInputXPath = ".//input[contains(@class,'search-input')]";

        //ddwnCol Controls (Cell ComboBox)
        private String mDDwnColInputCSS = "input";

        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            FindElement();
            bIsMergeProduct = DlkEnvironment.mProductFolder.Contains("AcumenTouchStone_81");
            this.ScrollIntoViewUsingJavaScript();
            String mClass = this.mElement.GetAttribute("class");
            if (mClass.Contains(DDwnClass))
            {
                mComboBoxType = DDwnClass;
            }
            else if (mClass.Contains(DDwnContainerClass) || mClass.Contains(DDwnContainerClass2))
            {
                mComboBoxType = DDwnContainerClass;
            }
            else if (mClass.Contains(CoreDdwnField))
            {
                mComboBoxType = CoreDdwnField;
            }
            else if (mClass.Contains(DropDownClass))
            {
                mComboBoxType = DropDownClass;
            }
            else if (mClass.Contains(CoreFieldClass))
            {
                mComboBoxType = CoreFieldClass;
            }
            else if (mClass.Contains(PopupMenuClass) || mClass.Contains(PopupMenu2Class) || mClass.Contains(PopupMenu3Class)) //"navigator_popupmenu"
            {
                mComboBoxType = PopupMenuClass;
            }
            else
            {
                throw new Exception("Initialize() error. Unknown ComboBox type '" + mClass + "'");
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case ModeChooserClass:
                        ModeChooserVerifyList(Items);
                        break;
                    case DDwnClass:
                        DDwnVerifyList(Items);
                        break;
                    case CustomDivClass:
                    case PopupMenuClass:
                        PopupVerifyList(Items);
                        break;
                    case DDwnColClass:
                        DDwnColVerifyList(Items);
                        break;
                    case SearchClass:
                    case CoreFieldClass:
                    case DDwnContainerClass:
                        SearchVerifyList(Items);
                        break;
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool ActualValue = false;
                ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), ActualValue);
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                    case SearchListFluid:
                    case DDwnContainerClass:
                    case CoreFieldClass:
                    case CoreDdwnField:
                        DDwnVerifyValue(ExpectedValue);
                        break;
                    case PopupMenuClass:
                        PopupVerifyValue(ExpectedValue);
                        break;
                    case SearchClass:
                        SearchVerifyValue(ExpectedValue);
                        break;
                    case DDwnColClass:
                    case SearchColList:
                        DDwnColVerifyValue(ExpectedValue);
                        break;
                    case QuickEdit:
                        QuickEditVerifyValue(ExpectedValue);
                        break;
                    default:
                        throw new Exception("Invalid combo box type");
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if a list has a sequence of the partially entered items. Copy the switch case from VerifyList everytime you update it.
        /// </summary>
        /// <param name="Items"></param>
        [Keyword("VerifyPartialList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyPartialList(String Items)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Items)) throw new Exception("Items must not be empty.");
                Initialize();
                Items = Items.Trim().ToLower();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnVerifyList(Items, true);
                        break;
                    case PopupMenuClass:
                        PopupVerifyList(Items, true);
                        break;
                    case DDwnColClass:
                        DDwnColVerifyList(Items, true);
                        break;
                    case SearchClass:
                    case CoreFieldClass:
                    case DDwnContainerClass:
                    case CoreDdwnField:
                    case DropDownClass:
                        SearchVerifyList(Items, true);
                        break;
                    default:
                        throw new Exception("VerifyPartiaList failed. Unsupported class [" + mComboBoxType + "].");
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Item)) throw new Exception("Item must not be empty");
                Item = Item.Trim();
                Initialize();
                if (mElement.Text == Item)
                {
                    DlkLogger.LogInfo("Item [" + Item + "] already selected by default.");
                    return;
                }
                else if (mElement.Text == "" && mElement.FindElements(By.TagName("input")).Any())
                {
                    IWebElement comboBoxElement = mElement.FindElement(By.TagName("input"));
                    DlkComboBox cmbInput = new DlkComboBox("Input", comboBoxElement);
                    if (cmbInput.GetValue() == Item)
                    {
                        DlkLogger.LogInfo("Item [" + Item + "] already selected by default.");
                        return;
                    }
                }
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnSelect(Item);
                        break;
                    case DropDownClass:
                        NewDDwnSelect(Item);
                        break;
                    case DDwnContainerClass:
                        SearchSelectWithoutSearch(Item);
                        break;
                    case CoreDdwnField:
                    case CoreFieldClass:
                        CoreFieldSelect(Item);
                        break;
                    case PopupMenuClass:
                        PopupSelect(Item);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("ClearField")]
        public void ClearField()
        {
            try
            {
                string xpath_ClearIcon = ".//*[contains(@class, 'clear-icon')]";
                string id_ClearField = "ClearInputField";

                Initialize();
                mElement.Click();

                IWebElement clear = mElement.FindWebElementCoalesce(By.XPath(xpath_ClearIcon), By.Id(id_ClearField));

                if (clear != null)
                {
                    DlkBaseControl Clear = new DlkBaseControl("Clear", clear);
                    Clear.MouseOver();
                    Clear.Click();
                    DlkLogger.LogInfo("ClearField() passed.");
                }
                else
                {
                    DlkLogger.LogInfo("Clear icon is not available. Clear input field instead.");
                    DlkBaseControl mComboBox = new DlkBaseControl("ComboBox", mElement);
                    DlkBaseControl input = new DlkBaseControl("Input", mComboBox, "XPATH", ".//input");

                    if (input.Exists() && input.IsReadOnly() != "true")
                    {
                        input.mElement.SendKeys(Keys.Control + "a");
                        Thread.Sleep(100);
                        input.mElement.SendKeys(Keys.Backspace);
                        DlkLogger.LogInfo("ClearField() passed.");
                    }
                    else
                    {
                        throw new Exception("Input field is read-only or does not exists.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClearField() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInList", new String[]
         {
             "1|text|Item|Sample item",
             "2|text|Expected Value|TRUE"
         })]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                if (String.IsNullOrWhiteSpace(TrueOrFalse))
                    throw new ArgumentException("TrueOrFalse must be a boolean value.");
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnAvailableInList(Item, TrueOrFalse);
                        break;
                    case CoreFieldClass:
                    case DDwnContainerClass:
                        CoreFieldAvailableInList(Item, TrueOrFalse);
                        break;
                    case PopupMenuClass:
                        PopupAvailableInList(Item, TrueOrFalse);
                        break;
                    default:
                        throw new Exception("Invalid combo box type");
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemReadOnly", new String[]
         {
             "1|text|Item|Sample item",
             "2|text|Expected Value|TRUE"
         })]
        public void VerifyItemReadOnly(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                if (String.IsNullOrWhiteSpace(TrueOrFalse))
                    throw new ArgumentException("TrueOrFalse must be a boolean value.");
                if (mComboBoxType == PopupMenuClass)
                {
                    PopupVerifyReadOnly(TrueOrFalse, Item);
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("OpenDropdown", new String[] { "1|text|Value" })]
        public void OpenDropdown()
        {
            try
            {
                Initialize();
                IWebElement mArrowDown = null;
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                        if (mArrowDown.GetAttribute("class") == "progress-area")
                        {
                            mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
                        }
                        break;
                    case PopupMenuClass:
                        if (this.mElement.FindElements(By.XPath(mPopupArrowXPath)).Count > 0)
                        {
                            mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                        }
                        else
                        {
                            mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
                        }
                        break;
                }

                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();

            }
            catch (Exception e)
            {
                throw new Exception("OpenDropdown() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "True" })]
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

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
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

        public new bool IsReadOnly()
        {
            bool readOnly;

            if ((mComboBoxType == PopupMenuClass) || (mComboBoxType == QuickEdit) || mElement.GetAttribute("class").Contains("disabled"))
            {
                String sValue = mElement.GetAttribute("class");
                readOnly = !sValue.Contains("editable");
            }
            else
            {
                IWebElement mInput = this.mElement.FindElement(By.CssSelector(mInputCSS));
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                readOnly = Convert.ToBoolean(txtInput.IsReadOnly());
            }
            return readOnly;
        }

        private void DDwnVerifyValue(String ExpectedValue)
        {
            String ActualValue = "";
            IWebElement mInput = mElement.FindElement(By.TagName("input"));
            //IWebElement mInput = this.mElement.FindElement(By.CssSelector(mInputCSS));

            if (mInput.FindElements(By.XPath("./following-sibling::*[@class='tagsinput']/*[@class='tag']//*[@class='tagstext']")).Count > 0)
            {
                DlkLogger.LogInfo("Reading values of each tag in the control...");
                foreach (IWebElement tag in mInput.FindElements(By.XPath("./following-sibling::*[@class='tagsinput']/*[@class='tag']//*[@class='tagstext']")))
                {
                    DlkTextBox txtInput = new DlkTextBox("Input", tag);
                    ActualValue += DlkString.RemoveCarriageReturn(txtInput.GetValue()).Trim() + "~";
                }
                ActualValue = ActualValue.Trim('~');
                DlkLogger.LogInfo("Actual value obtained: [" + ActualValue + "]");
                DlkAssert.AssertEqual("VerifyValue()", ExpectedValue, ActualValue);
            }
            else if (mInput.GetAttribute("class").Contains("date-time"))
            {
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                //Some date-time fields have data-focus-value, some have none
                txtInput.Click();
                string ActVal = String.IsNullOrEmpty(txtInput.GetAttributeValue("data-focus-value")) ? txtInput.GetValue() : txtInput.GetAttributeValue("data-focus-value");
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActVal);
                //Close dropdown.
                txtInput.Click();
            }
            else
            {
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.VerifyText(ExpectedValue);
            }
        }

        private void PopupVerifyValue(String ExpectedValue)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mPopupInputXPath));
            DlkLabel lblInput = new DlkLabel("Input", mInput);
            if (lblInput.GetAttributeValue("class").ToLower() != "label")
            {
                lblInput.VerifyText(ExpectedValue);
            }
            else
            {
                mInput = this.mElement.FindElement(By.XPath(mPopupInputXPath2));
                lblInput = new DlkLabel("Input", mInput);
                if (lblInput.GetAttributeValue("class").ToLower().Contains("status-sprite"))
                {
                    mInput = this.mElement.FindElement(By.XPath("./span[3]"));
                    lblInput = new DlkLabel("Input", mInput);
                }
                lblInput.VerifyText(ExpectedValue);
            }
        }

        private void QuickEditVerifyValue(String ExpectedValue)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(".//*[contains(@class,'content')]"));
            DlkLabel lblInput = new DlkLabel("Input", mInput);
            lblInput.VerifyText(ExpectedValue);
        }

        private void SearchVerifyValue(String ExpectedValue)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.VerifyText(ExpectedValue);
        }

        private void DDwnColVerifyValue(String ExpectedValue)
        {
            IWebElement mInput = this.mElement.FindElement(By.CssSelector(mDDwnColInputCSS));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.VerifyText(ExpectedValue);

        }

        public void ClickTableLink(String Value)
        {
            FindElement();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");

            IWebElement mArrowDown;
            if (this.mElement.FindElements(By.XPath(mDDwnArrowDownXPATH)).Count > 0)
                mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            else if (this.mElement.FindElements(By.XPath(".//span[contains(@class,'tap-target')]")).Count > 0)
                mArrowDown = this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
            else
                throw new Exception("Unable to find the dropdown arrow to open the list.");

            if (!list.Exists(1))
            {
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
                Thread.Sleep(3000);
            }


            list.FindElement();
            IWebElement item =
                list.mElement.FindElement(By.XPath("./descendant::span[contains(text(),'" + Value + "')]"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);

            ctlItem.Click();
            // ctlItem.ClickUsingJavaScript();

        }

        public void VerifyTableLinkExists(String ExpectedValue)
        {
            // Initialize();
            IWebElement mArrowDown;
            if (this.mElement.FindElements(By.XPath(mDDwnArrowDownXPATH)).Count > 0)
                mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            else if (this.mElement.FindElements(By.XPath(".//span[contains(@class,'tap-target')]")).Count > 0)
                mArrowDown = this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
            else
                throw new Exception("Unable to find the dropdown arrow to open the list.");

            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            Thread.Sleep(3000);
            ctlArrowDown.Click();
            Thread.Sleep(3000);
            bool bFound = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");
            bFound = list.Exists();
            if (!bFound)
            {
                //for results dropdown
                list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                bFound = list.Exists();
            }
            DlkAssert.AssertEqual("CompareValues() List: ", Convert.ToBoolean(ExpectedValue), bFound);
            //close the list
            ctlArrowDown.Click();
        }

        private void DDwnVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            try
            {
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
                IWebElement mArrowDown = this.mElement.FindElement(By.CssSelector(mArrowDownCSS));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                if (!list.Exists(1))
                {
                    ctlArrowDown.ClickUsingJavaScript();
                }
                list.FindElement();
                var lstItems = list.mElement.FindElements(By.XPath(".//tr[contains(@class,'ddwnListItem')]/td/div")).Where(item => item.Displayed).ToList();
                String ActValues = "";
                for (int i = 0; i < lstItems.Count; i++)
                {
                    DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                    if (ActValues != "")
                    {
                        ActValues = ActValues + "~";
                    }
                    ActValues = ActValues + item.GetValue();
                }
                if (VerifyPartialList)
                {
                    DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, ActValues.ToLower(), true);
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActValues);
                }
                this.ClickUsingJavaScript(); //close the list
            }
            catch (Exception ex)
            {
                throw new Exception("Error clicking element.", ex);
            }
        }

        private void ModeChooserVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            try
            {
                //Click element to display list contents
                this.mElement.Click();

                var lstItems = this.mElement.FindElements(By.XPath(".//option")).Where(item => item.Displayed).ToList();
                String ActValues = "";
                for (int i = 0; i < lstItems.Count; i++)
                {
                    DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                    if (ActValues != "")
                    {
                        ActValues = ActValues + "~";
                    }
                    ActValues = ActValues + item.GetValue();
                }
                if (VerifyPartialList)
                {
                    DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, ActValues.ToLower(), true);
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActValues);
                }
                this.mElement.Click(); //close the list
            }
            catch (Exception ex)
            {
                throw new Exception("Error clicking element.", ex);
            }
        }

        private void DDwnVerifyListCount(String ExpectedCount)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.CssSelector(mArrowDownCSS));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./ul[@class='popupmenuItems']/li/a/span"));
            DlkAssert.AssertEqual("VerifyListCount", Convert.ToInt32(ExpectedCount), lstItems.Count());
        }

        private void DDwnColVerifyListCount(String ExpectedCount)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath(".//tr[@class='ddwnListItem']"));
            DlkAssert.AssertEqual("VerifyListCount", Convert.ToInt32(ExpectedCount), lstItems.Count());
        }

        private void PopupVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            IWebElement mArrowDown = null;
            if (this.mElement.FindElements(By.XPath(mPopupArrowXPath)).Count > 0)
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
            }
            else
            {
                mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
            }
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.Click();

                if (!list.Exists(1))
                {
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                }
            }

            list.FindElement();
            var lstItems = list.mElement.FindElements(By.XPath(".//li[not(contains(@class,'divider'))]")).Where(item => item.Displayed).ToList();
            String sActualValues = "";
            for (int i = 0; i < lstItems.Count; i++)
            {
                DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                if (sActualValues != "")
                {
                    sActualValues = sActualValues + "~";
                }
                sActualValues = sActualValues + item.GetValue().Replace("\r\n", " ");
            }
            if (VerifyPartialList)
            {
                DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, sActualValues.ToLower(), true);
            }
            else
            {
                DlkAssert.AssertEqual("VerifyList()", ExpectedValues, sActualValues);
            }
            ctlArrowDown.Click(); //close the list
        }

        /// <summary>
        /// Doesn't do anything yet
        /// </summary>
        /// <param name="ExpectedValues"></param>
        /// <param name="VerifyPartialList"></param>
        private void DDwnColVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();

            String ActualValues = "";
            List<IWebElement> ddwnValues = list.mElement.FindElements(By.TagName("tr")).ToList();

            foreach (IWebElement ddwnValue in ddwnValues)
            {
                DlkBaseControl ddwnItem = new DlkBaseControl("DropdownItem", ddwnValue);
                ActualValues += ddwnItem.GetValue() + "~";
            }

            ActualValues = ActualValues.TrimEnd(new char[] { '~' });

            if (VerifyPartialList)
            {
                DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, ActualValues.ToLower(), true);
            }
            else
            {
                DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActualValues);
            }
        }

        private void SearchVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            try
            {
                String ActualValues = "";
                DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);

                if (!list.Exists(1))
                {
                    OpenSearchDropdown();
                    list.FindElement(); //Need to re-initialize after clicking to update the list control
                }
                /* [JAM 6/8/2017] Isolating issue to Sorting Options Dialog only for now. Other dialogs that are working fine are being affected by the previous fix. */
                else if (this.mElement.FindElements(By.XPath("//*[contains(@class,'dialog-title')][contains(.,'Sorting Options')]")).Count > 0)
                {
                    OpenSearchDropdown();

                    //Check if more than one list is opened. If more than one list is opened, get the last displayed list
                    if (this.mElement.FindElements(By.XPath(mSearchListXPath)).Where(dropdown => dropdown.Displayed).ToList().Count > 1)
                    {
                        DlkLogger.LogInfo("More than one opened list detected. Check top displayed list...");
                        list = new DlkBaseControl("List", this.mElement.FindElements(By.XPath(mSearchListXPath)).Where(dropdown => dropdown.Displayed).ToList().Last());
                    }
                }

                IReadOnlyCollection<IWebElement> ddList = (list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Count > 0) ?
                                  list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Where(item => item.Displayed).ToList() :
                                  list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]")).Where(item => item.Displayed).ToList();

                foreach (IWebElement item in ddList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("List", item);
                    string val = item.FindElements(By.ClassName("search-name")).Count > 0 ?
                        item.FindElement(By.ClassName("search-name")).Text : ctl.GetValue();

                    ActualValues += DlkString.RemoveCarriageReturn(val.Trim()) + "~";
                }

                ExpectedValues = ExpectedValues.ToLower();
                ActualValues = ActualValues.TrimEnd(new char[] { '~' }).ToLower();

                if (VerifyPartialList)
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActualValues.ToLower(), true);
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActualValues, false);
                }
            }
            catch (Exception ex)
            {
                if (VerifyPartialList)
                    throw new Exception("VerifyPartialList() failed: " + ex.Message);
                else
                    throw new Exception("VerifyList() failed: " + ex.Message);
            }
            finally
            {
                if (!IsElementStale())
                    TryClickElement(new DlkBaseControl("List", mElement));
            }
        }

        private void DDwnSelect(String Value)
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            bool bFound = false;
            for (int retryCount = 0; retryCount <= 2; retryCount++)
            {
                if (!list.Exists(1))
                {
                    DlkLogger.LogInfo("Clicking dropdown arrow for the list...");
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.ClickUsingJavaScript();
                    DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
                    if (list.Exists(3))
                    {
                        DlkLogger.LogInfo("List found. Proceeding...");
                        break;
                    }
                }
            }
            list.FindElement();
            IReadOnlyCollection<IWebElement> ddList = list.mElement.FindElements(By.XPath(".//td/div"));
            foreach (IWebElement item in ddList)
            {
                DlkBaseControl ctl = new DlkBaseControl("List", item);
                string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                if (val.Equals(Value.Trim()))
                {
                    bFound = true;
                    ctl.Click();
                    DlkLogger.LogInfo("Item [" + Value + "]found. Clicking...");

                    break;
                }
            }
            if (!bFound)
            {
                throw new Exception("Unable to find item in the list.");
            }
        }

        private void NewDDwnSelect(String Value)
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", newDdwnInputListClass);
            bool bFound = false;
            for (int retryCount = 0; retryCount <= 2; retryCount++)
            {
                if (!list.Exists(1))
                {
                    DlkLogger.LogInfo("Clicking dropdown arrow for the list...");
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(newDDwnArrowDownXPATH));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.ClickUsingJavaScript();
                    DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
                    if (list.Exists(3))
                    {
                        DlkLogger.LogInfo("List found. Proceeding...");
                        break;
                    }
                }
            }
            
            list.FindElement();
            IReadOnlyCollection<IWebElement> ddList = list.mElement.FindElements(By.XPath(".//li"));
            foreach (IWebElement item in ddList)
            {
                DlkBaseControl ctl = new DlkBaseControl("List", item);
                string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                if (val.Equals(Value.Trim()))
                {
                    bFound = true;
                    ctl.Click();
                    DlkLogger.LogInfo("Item [" + Value + "]found. Clicking...");

                    break;
                }
            }
            if (!bFound)
            {
                throw new Exception("Unable to find item in the list.");
            }
        }

        private void PopupSelect(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            Action<DlkBaseControl> ClickDDarrow = (control) =>
            {
                if (!control.Exists(1))
                {
                    IWebElement mArrowDown = null;
                    try
                    {
                        mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                    }
                    catch
                    {
                        mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
                    }
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.ClickUsingJavaScript();
                }
            };
            try
            {
                ClickDDarrow(list);
                if (!list.Exists(1))
                {
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                    ClickDDarrow(list);
                }
                list.FindElement();
                IWebElement item = list.mElement.FindElement(By.XPath(".//*[normalize-space()='" + Value + "']"));
                DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
                ctlItem.Click();
            }
            catch (Exception e)
            {
                throw new Exception("PopUpSelect() failed : " + e.Message);
            }
        }

        private void SearchSelectWithoutSearch(String Value)
        {
            IWebElement listContainer = null; 
            Boolean bFound = false;

            if (!DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchListXPath)).Where(x=>x.Displayed).Any())
            {
                DlkLogger.LogInfo("List menu not available. Opening dropdown list... ");
                OpenSearchDropdown();
            }

            listContainer = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchListXPath)).Any() ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mSearchListXPath)) : throw new Exception("List container not found.");
            DlkLogger.LogInfo("List container found. ");

            string sortingOptions_XPath = "//*[contains(@class,'dialog-title')][contains(.,'Sorting Options')]";
            if (mElement.FindElements(By.XPath(sortingOptions_XPath)).Any())
            {
                DlkLogger.LogInfo("Sorting type list container found. Changing list container... ");
                listContainer = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchListXPath)).LastOrDefault();
            }

            if (listContainer != null)
            {
                string listItem_XPath1 = ".//li[contains(@class,'search-result')]";
                string listItem_XPath2 = ".//div[contains(@class,'resultListItem')]";
                IList<IWebElement> listItems = listContainer.FindElements(By.XPath(listItem_XPath1)).Where(x => x.Displayed).ToList().Count > 0 ?
                    listContainer.FindElements(By.XPath(listItem_XPath1)).Where(x => x.Displayed).ToList() : null;

                if(listItems == null)
                {
                    DlkLogger.LogInfo("List items not found on list container. Trying again... ");
                    listItems = listContainer.FindElements(By.XPath(listItem_XPath2)).Where(x => x.Displayed).ToList().Count > 0 ?
                    listContainer.FindElements(By.XPath(listItem_XPath2)).Where(x => x.Displayed).ToList() :
                    throw new Exception("List items not found on list container.");
                }

                foreach (IWebElement currentItem in listItems)
                {
                    if (currentItem.Displayed)
                    {
                        string currentValue = currentItem.Text.Trim();
                        DlkLogger.LogInfo("Current list item: [" + currentValue + "]");
                        if (Value.Equals(currentValue))
                        {
                            currentItem.Click();
                            bFound = true;
                            DlkLogger.LogInfo("Item [" + Value + "] found. Clicking...");
                            break;
                        }
                    }
                }
            }

            if (!bFound)
            {
                throw new Exception("Unable to find item [" + Value + "] in the list.");
            }
        }

        private void CoreFieldSelect(String Item)
        {
            try
            {
                int retryClickLimit = 3;
                IWebElement dropDownArrow = mElement.FindElements(By.XPath(mSearchArrowXpath)).Any() ?
                    mElement.FindElement(By.XPath(mSearchArrowXpath)) : throw new Exception("Core field dropdown arrow not found.");
                IWebElement coreFieldList = null;

                for (int r = 1; r <= retryClickLimit; r++)
                {
                    dropDownArrow.Click();
                    DlkLogger.LogInfo("Clicking core field drop down arrow [" + r.ToString() + "]... ");
                    Thread.Sleep(1000);

                    coreFieldList = mElement.FindElements(By.XPath(mCoreFieldColListXPATH)).Any() ?
                        mElement.FindElement(By.XPath(mCoreFieldColListXPATH)) : null;

                    if (coreFieldList == null)
                    {
                        DlkLogger.LogInfo("Core field list not found. Trying again... ");
                        coreFieldList = mElement.FindElements(By.XPath(mSearchListXPath)).Where(x => x.Displayed).Any() ?
                            mElement.FindElements(By.XPath(mSearchListXPath)).Where(x => x.Displayed).FirstOrDefault() :
                            null;
                    }

                    if (coreFieldList != null)
                        break;
                }

                if(coreFieldList != null)
                {
                    string coreFieldItem_XPath2 = ".//li[.='" + Item + "']";
                    DlkLogger.LogInfo("Core field drop down list found.");

                    List <IWebElement> coreFieldItems = coreFieldList.FindElements(By.TagName("li")).Where(x=> x.Displayed).ToList();
                    bool cFound = false;

                    foreach(IWebElement currentItem in coreFieldItems)
                    {
                        string currentValue = currentItem.Text.Trim();
                        DlkLogger.LogInfo("Current Value: [" + currentValue + "]");
                        if (currentValue.Equals(Item))
                        {
                            currentItem.Click();
                            cFound = true;
                            DlkLogger.LogInfo("Item: [" + Item + "] found on list. Selecting item... ");
                            break;
                        }
                    }
                   
                    if(!cFound)
                        throw new Exception("Item: [" + Item + "] not found on core field list");
                }
                else
                {
                    throw new Exception("Core field list not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message);
            }
        }

        private void DDwnAvailableInList(String ItemValue, String ExpectedValue)
        {
            Boolean ActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.CssSelector(mArrowDownCSS));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown); //open dropdown
            if (!list.Exists(1))
            {
                ctlArrowDown.ClickUsingJavaScript();
            }
            list.FindElement();
            var ctl = list.mElement.FindElements(By.XPath(".//tr[contains(@class,'ddwnListItem')]/td/div[contains(text(),'" + ItemValue + "')]"));
            if (ctl.Count > 0)
            {
                ActualValue = true;
            }
            DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), ActualValue);
            ctlArrowDown.ClickUsingJavaScript();
        }

        private void PopupVerifyReadOnly(String ExpectedValue, String ItemToFind)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            IWebElement mArrowDown = null;
            if (this.mElement.FindElements(By.XPath(mPopupArrowXPath)).Count > 0)
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
            }
            else
            {
                mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
            }
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.Click();

                if (!list.Exists(1))
                {
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                }
            }

            list.FindElement();
            IWebElement item = list.mElement.FindElements(By.XPath(".//li[not(contains(@class,'divider'))]")).First(x => x.Text == ItemToFind);
            bool ActualValue = item.GetAttribute("class").Contains("disabled");

            DlkAssert.AssertEqual("VerifyItemReadOnly()", Convert.ToBoolean(ExpectedValue), ActualValue);
            ctlArrowDown.Click(); //close the list
        }

        private void CoreFieldAvailableInList(String Item, String ExpectedValue)
        {
            try
            {
                int retryClickLimit = 3;
                IWebElement dropDownArrow = mElement.FindElements(By.XPath(mSearchArrowXpath)).Any() ?
                    mElement.FindElement(By.XPath(mSearchArrowXpath)) : throw new Exception("Core field dropdown arrow not found.");
                IWebElement coreFieldList = null;

                for (int r = 1; r <= retryClickLimit; r++)
                {
                    dropDownArrow.Click();
                    DlkLogger.LogInfo("Clicking core field drop down arrow [" + r.ToString() + "]... ");
                    Thread.Sleep(1000);

                    coreFieldList = mElement.FindElements(By.XPath(mCoreFieldColListXPATH)).Any() ?
                        mElement.FindElement(By.XPath(mCoreFieldColListXPATH)) : null;

                    if (coreFieldList == null)
                    {
                        DlkLogger.LogInfo("Core field list not found. Trying again... ");
                        coreFieldList = mElement.FindElements(By.XPath(mSearchListXPath)).Where(x => x.Displayed).Any() ?
                            mElement.FindElements(By.XPath(mSearchListXPath)).Where(x => x.Displayed).FirstOrDefault() :
                            null;
                    }

                    if (coreFieldList != null)
                        break;
                }

                bool actualValue = false;
                if (coreFieldList != null)
                {
                    string coreFieldItem_XPath2 = ".//li[.='" + Item + "']";
                    DlkLogger.LogInfo("Core field drop down list found.");

                    List<IWebElement> coreFieldItems = coreFieldList.FindElements(By.TagName("li")).Where(x => x.Displayed).ToList();
                    
                    foreach (IWebElement currentItem in coreFieldItems)
                    {
                        string currentValue = currentItem.Text.Trim();
                        DlkLogger.LogInfo("Current Value: [" + currentValue + "]");
                        if (currentValue.Equals(Item))
                        {
                            actualValue = true;
                            DlkLogger.LogInfo("Item: [" + Item + "] found on list.");
                            break;
                        }
                    }

                    if (!actualValue)
                        DlkLogger.LogInfo("Item: [" + Item + "] not found on core field list");

                    dropDownArrow.Click();
                    DlkLogger.LogInfo("Closing dropdown list... ");
                    Thread.Sleep(1000);

                    DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), actualValue);
                }
                else
                {
                    throw new Exception("Core field list not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message);
            }
        }

        private void PopupAvailableInList(String Item, String ExpectedValue)
        {
            Boolean ActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mOptionListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mOptionArrowXpath));
            //IWebElement input = this.mElement.FindElement(By.XPath("..//input[contains(@class,'dropdown-field')]"));
            if (!list.Exists(1))
            {
                mArrowDown.Click();
                Thread.Sleep(5000);
            }
            list.FindElement();
            IReadOnlyCollection<IWebElement> ddList = list.mElement.FindElements(By.XPath(".//li[contains(@class,'menu-item')]"));
            foreach (IWebElement item in ddList.Where(x => x.Displayed))
            {
                DlkBaseControl ctl = new DlkBaseControl("List", item);
                string val = item.FindElements(By.ClassName("popupLabel")).Count > 0 ?
                    item.FindElement(By.ClassName("popupLabel")).Text : ctl.GetValue();

                val = DlkString.RemoveCarriageReturn(val.Trim());

                if (val.Equals(Item.Trim()))
                {
                    ActualValue = true;
                    break;
                }
            }
            DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), ActualValue);
            ClickBanner();
        }

        private void OpenSearchDropdown()
        {
            try
            {
                IWebElement mArrowDown = this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0 ?
                        this.mElement.FindElement(By.XPath(mSearchArrowXpath)) :
                        this.mElement.FindElement(By.XPath(mSearchArrowXpath2));

                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                TryClickElement(ctlArrowDown);
                DlkLogger.LogInfo("Opening search drop down list... ");
                Thread.Sleep(5000);
            }
            catch
            {
                DlkLogger.LogInfo("Opening search drop down list failed.");
            }
        }

        public void TryClickElement(DlkBaseControl bElement)
        {
            try
            {
                bElement.ClickUsingJavaScript();
            }
            catch
            {
                DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);
                if (!list.Exists(1))
                    bElement.Click(0, 0);
            }
        }

        /// <summary>
        /// Click on banner to avoid timeout
        /// </summary>
        public void ClickBanner()
        {
            /*Removed this functionality after adjusting timeout recurrence to 30mins*/

            //try
            //{
            //    DlkLogger.LogInfo("Performing Click on Banner to avoid timeout.");
            //    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='banner']")));
            //    bannerCtrl.Click();
            //}
            //catch
            //{
            //    //Do nothing -- there might be instances that setting a text or value would display a dialog message
            //    //Placing a log instead for tracking
            //    DlkLogger.LogInfo("Problem performing Click on Banner. Proceeding...");
            //}
        }
    }
}
