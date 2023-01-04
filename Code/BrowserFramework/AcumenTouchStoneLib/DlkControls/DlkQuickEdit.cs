using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using AcumenTouchStoneLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("QuickEdit")]
    public class DlkQuickEdit : DlkBaseControl
    {
        #region DECLARATIONS
        private const int SHORT_WAIT = 1000;
        private const int MEDIUM_WAIT = 3000;
        private const int LONG_WAIT = 5000;

        private const String RegClass = "quick-edit";
        private const String OneFourthClass = "span-one-fourth";
        private const String HeaderClass = "header-title-container";
        private const String DDwnClass = "ddwnInput";  //regular drop-down
        private const String DDwnColClass = "ddwnCol"; //regular drop-down in the table
        private const String CoreClass = "core-component";
        private const String CoreInputListClass = "";
        private const String DdwnContainerClass = "dropdown-field-container";
        private const String PopupMenuClass = "popupmenu";
        private String mType = "";


        //Regular Quick Edit controls
        private String mRegLabelXPath = ".//*[contains(@class,'label')]";
        //private String mRegContentXPath = ".//span[@class='content']";
        private String mHeaderLabelXPath = ".//div[contains(@class,'header-title')]";
        private String mRegContentXPath = ".//*[contains(@class,'content')]";
        //private String mRegDivContentXPath = ".//div[@class='content']";
        //private String mRegDivSpanXPath = "./span[2]";
        private String mRegEditIconXPath = ".//span[contains(@class,'icon-edit')]";

        //One-Fourth Quick Edit controls
        private String mOneFourthLabelXPath = "./div/span[contains(@class, 'label')]";

        private String mOneFourthContentXPath = "./div[2]";
        private String mOneFourthEditIconXPath = "./div/div[@class='icon-edit']";

        private String mTentativeQuickEditType = "";
        private int loadingBuffer = 5000;

        //Core Quick Edit Controls
        private String mCoreLabelXPath = ".//*[contains(@class,'core-label')]";
        private String mCoreEditIconXPath = ".//*[contains(@class,'icon-edit')]";
        private String mCoreContentXPath = ".//div[contains(@class,'core-field')]";
        private String mCoreListClass = "results";
        private String mCoreDdwnArrowClass = ".//*[contains(@class,'tap-target')]";

        //others
        private String mDDwnArrowDownXPATH = ".//*[contains(@class,'ddwnArrow')]";
        private String ddwnInputListClass = "ddwnTableDiv";
        private String mSearchArrowXpath = ".//*[contains(@class,'tap-target')]";
        private String mSearchArrowXpath2 = ".//*[contains(@class,'show-results')]";
        private String mSearchListClass = "results";
        private String mSearchListXPath = "//ul[contains(@class,'results')]";
        private String mSearchLinkXPath = "./ancestor::div//*[contains(@class,'linked-text')]";

        private Boolean inEditMode = false;
        #endregion

        #region Constructors
        public DlkQuickEdit(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkQuickEdit(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkQuickEdit(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkQuickEdit(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            string sClass = this.mElement.GetAttribute("class").ToLower();

            if (sClass.Contains("in-quick-edit"))
            {
                inEditMode = true;
            }

            if (sClass.Contains(RegClass))
            {
                if (sClass.Contains(OneFourthClass) || sClass.Contains("summary-block"))
                {
                    mType = OneFourthClass;
                }
                else if (sClass.Contains(HeaderClass))
                {
                    mType = HeaderClass;
                }
                else if (sClass.Contains(CoreClass))
                {
                    mType = CoreClass;
                }
                else
                {
                    mType = RegClass;
                }
            }
            else if (sClass.Contains(DDwnClass))
            {
                mType = DDwnClass;
            }
            else if (sClass.Contains(CoreClass))
            {
                mType = CoreClass;
            }
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (this.mElement.GetAttribute("class").ToLower().Contains(RegClass))
            {
                if (this.mElement.GetAttribute("class").ToLower().Contains(OneFourthClass))
                {
                    mTentativeQuickEditType = OneFourthClass;
                }
                else if ((this.mElement.GetAttribute("class").ToLower().Contains(HeaderClass)))
                {
                    mTentativeQuickEditType = HeaderClass;
                }
                else
                {
                    mTentativeQuickEditType = RegClass;
                }
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'span-one-forth')]"));
                    mTentativeQuickEditType = OneFourthClass;
                    return true;
                }
                catch
                {
                }

                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'quick-edit')]"));
                    mTentativeQuickEditType = RegClass;
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }

            }
        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("QuickEdit", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                switch (mTentativeQuickEditType)
                {
                    case RegClass:
                        IWebElement parentRegQuickEdit = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'quick-edit')]"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentRegQuickEdit);
                        mAutoCorrect = true;
                        break;
                    case HeaderClass:
                        IWebElement parentHeaderQuickEdit = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'quick-edit')]"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentHeaderQuickEdit);
                        mAutoCorrect = true;
                        break;
                    case OneFourthClass:
                        IWebElement parentOneFourthQuickEdit = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'span-one-forth')]"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentOneFourthQuickEdit);
                        mAutoCorrect = true;
                        break;
                }

                if (mAutoCorrect)
                {
                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClassName = mCorrectControl.GetAttributeValue("class");
                    if (mId != null && mId != "")
                    {
                        SearchType = "ID";
                        SearchValue = mId;
                    }
                    else if (mName != null && mName != "")
                    {
                        SearchType = "NAME";
                        SearchValue = mName;
                    }
                    else if (mClassName != null && mClassName != "")
                    {
                        SearchType = "CLASSNAME";
                        SearchValue = mClassName.Split(' ').First();
                    }
                    else
                    {
                        SearchType = "XPATH";
                        SearchValue = mCorrectControl.FindXPath();
                    }
                }
            }
            catch
            {

            }
        }

        #region KEYWORDS
        [Keyword("AssignValueToVariable")]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                switch (mType)
                {
                    case RegClass:
                        DlkVariable.SetVariable(VariableName, RegGetText());
                        break;
                    case OneFourthClass:
                        DlkVariable.SetVariable(VariableName, OneFourthGetText());
                        break;
                    case HeaderClass:
                        DlkVariable.SetVariable(VariableName, HeaderGetText());
                        break;
                    case CoreClass:
                        DlkVariable.SetVariable(VariableName, CoreGetText());
                        break;
                    default:
                        throw new Exception("Unknown QuickEdit type.");
                }
                DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignToVariable() failed: " + e.Message);
            }
        }

        [Keyword("AssignPartialValueToVariable")]
        public new void AssignPartialValueToVariable(String VariableName, string StartIndex, string Length)
        {
            try
            {
                Initialize();
                string ActValue = string.Empty;
                switch (mType)
                {
                    case RegClass:
                        ActValue = RegGetText();
                        break;
                    case OneFourthClass:
                        ActValue = OneFourthGetText();
                        break;
                    case HeaderClass:
                        ActValue = HeaderGetText();
                        break;
                    case CoreClass:
                        ActValue = CoreGetText();
                        break;
                    default:
                        throw new Exception("Unknown QuickEdit type.");
                }

                string PartialValue = string.IsNullOrEmpty(ActValue) ? string.Empty : ActValue.Substring(int.Parse(StartIndex), int.Parse(Length));

                DlkVariable.SetVariable(VariableName, PartialValue);
                DlkLogger.LogInfo("AssignPartialValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + PartialValue + "].");
                DlkLogger.LogInfo("AssignPartialValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignPartialValueToVariable() failed: " + e.Message);
            }
        }

        [Keyword("Edit")]
        public void Edit()
        {
            try
            {
                Initialize();
                ClickEditButton();
            }
            catch (Exception e)
            {
                throw new Exception("Edit() failed : " + e.Message, e);
            }
        }

        [Keyword("Set")]
        public void Set(String Value)
        {
            // Sets the text for QuickEdit control types
            try
            {
                // code for the Edit(), clicks the pencil icon
                Initialize();
                try
                {
                    SetQuickEditText(Value);
                }
                catch
                {
                    ClickEditButton();
                    SetQuickEditText(Value);
                }
                DlkLogger.LogInfo("Sleeping for 3(s) to let the value be assigned.");
                Thread.Sleep(MEDIUM_WAIT);
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTextAndVerifyList")]
        public void SetTextAndVerifyList(String TextToEnter, String Items)
        {
            try
            {
                Initialize();
                try
                {
                    SetTextOnly(TextToEnter);
                }
                catch
                {
                    ClickEditButton();
                    SetTextOnly(TextToEnter);
                }

                //After setting text, verify the search list
                IWebElement comboBox;
                if (mType == CoreClass)
                {
                    comboBox = mElement.FindElement(By.XPath(".//div[@class='core-field']/div[not(contains(@class, 'hot-swap'))]"));
                    if (comboBox.GetAttribute("class").Contains(DdwnContainerClass))
                        CoreVerifyList(Items);
                    else
                        throw new Exception("QuickEdit does not contain a ComboBox");
                }
                else
                {
                    throw new Exception("Unsupported input type");
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("SetTextAndVerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList")]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                IWebElement comboBox;
                // code for the Edit(), clicks the pencil icon, update this switch block if Edit() changes.
                ClickEditButton();

                DlkLogger.LogInfo("Re-initializing because of Edit Icon Click");
                Initialize();

                //Currently supports: CoreClass
                if (mType == CoreClass)
                {
                    comboBox = mElement.FindElement(By.XPath(".//div[@class='core-field']/div[not(contains(@class, 'hot-swap'))]"));
                    if (comboBox.GetAttribute("class").Contains(DdwnContainerClass))
                        CoreVerifyList(Items);
                    else
                        throw new Exception("QuickEdit does not contain a ComboBox");
                }
                else
                {
                    throw new Exception("Unsupported input type");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyAvailableInList")]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement comboBox;
                // code for the Edit(), clicks the pencil icon, update this switch block if Edit() changes.
                ClickEditButton();

                DlkLogger.LogInfo("Re-initializing because of Edit Icon Click");
                Initialize();

                //Currently supports: CoreClass
                if (mType == CoreClass)
                {
                    comboBox = mElement.FindElement(By.XPath(".//div[@class='core-field']/div[not(contains(@class, 'hot-swap'))]"));
                    IWebElement mArrowDown = comboBox.FindElement(By.XPath(mSearchArrowXpath));
                    mArrowDown.Click();
                    Thread.Sleep(2000);
                    DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);
                    list.FindElement();
                    List<IWebElement> mlstItems = list.mElement.FindElements(By.XPath("//li[contains(@class,'search-result')]")).Where(item => item.Displayed).ToList();
                    bool existsInList = false;
                    foreach (IWebElement item in mlstItems)
                    {
                        if (item.Text == Item)
                        {
                            existsInList = true;
                            break;
                        }
                    }
                    DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(TrueOrFalse), existsInList);
                    ClickBanner();
                }
                else
                {
                    throw new Exception("Unsupported input type");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyPartialList")]
        public void VerifyPartialList(String Items)
        {
            try
            {
                Initialize();
                IWebElement comboBox;
                // code for the Edit(), clicks the pencil icon, update this switch block if Edit() changes.
                ClickEditButton();

                DlkLogger.LogInfo("Re-initializing because of Edit Icon Click");
                Initialize();

                //Currently supports: CoreClass
                if (mType == CoreClass)
                {
                    comboBox = mElement.FindElement(By.XPath(".//div[@class='core-field']/div[not(contains(@class, 'hot-swap'))]"));
                    if (comboBox.GetAttribute("class").Contains(DdwnContainerClass))
                        CoreVerifyList(Items, true);
                    else
                        throw new Exception("QuickEdit does not contain a ComboBox");
                }
                else
                {
                    throw new Exception("Unsupported input type");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialList() failed : " + e.Message, e);
            }
        }

        [Keyword("Select")]
        public void Select(String Value)
        {
            try
            {
                Initialize();

                // Check if quick-edit is in edit mode before proceeding
                if (!inEditMode)
                {
                    ClickEditButton();
                }

                /* classes of dropdowns currently supported:
                 * navigator_ngcrm_search_list fluid
                 * ddwnInput
                */
                DlkLogger.LogInfo("Re-initializing element because of the dropdown arrow click.");
                Initialize();
                IWebElement comboBox;
                if (mType == CoreClass)
                {
                    comboBox = mElement.FindElement(By.XPath(".//div[contains(@class,'core-field')][not(contains(@class,'container'))]/div"));
                }
                else
                {
                    try
                    {
                        comboBox = mElement.FindElement(By.XPath(".//form/div"));
                    }
                    catch
                    {
                        comboBox = mElement.FindElement(By.CssSelector("div.quickEditInput"));
                    }
                }
                try
                {
                    // clear the text because if there is text inside,the clear button will block the dropdown arrow when being clicked.
                    // causing a different element to receive the click
                    var input = comboBox.FindElement(By.XPath(".//input"));
                    var isReadOnly = input.GetAttribute("readonly");
                    if (isReadOnly != "true")
                    {
                        ClearField(input);
                    }
                    
                }
                catch
                {
                    //swallow. input was not found
                }
                var dropdownClassContainer = comboBox.GetAttribute("class");
                // performs the clicking of the dropdown arrow depending on the type
                const string searchList = "search_list";
                const string ddwnInput = "ddwnInput";
                const string coreInput = "dropdown-field";
                const string coreFluid = "fluid";
                const string coreDdwn = "core_dropdown_field";

                //There seems to be class changes. searchList and ddwnInput are no longer used.


                if (dropdownClassContainer.Contains(searchList))
                {
                    mType = searchList;
                }
                else if (dropdownClassContainer.Contains(ddwnInput))
                {
                    mType = ddwnInput;
                }
                else if (dropdownClassContainer.Contains(coreInput) || dropdownClassContainer.Contains(coreFluid) || dropdownClassContainer.Contains(coreDdwn)) //As of now fluid works like a normal core ddwn when selecting
                {
                    // If dropdown class doesn't have core-component, get the core-component parent class
                    // searchlist type dropdowns don't have 'fluid' in the class value
                    if (!dropdownClassContainer.Contains("core-component"))
                    {
                        dropdownClassContainer = comboBox.FindElement(By.XPath("./ancestor::div[contains(@class,'core-component')][1]")).GetAttribute("class");
                    }
                    mType = !dropdownClassContainer.Contains(coreFluid) ? searchList : coreInput;
                }

                switch (mType)
                {
                    case searchList:
                        SetSearchListText(Value);
                        //SelectItemInSearchListDropdown(Value);
                        break;
                    case ddwnInput:
                        SelectItemInDdwnInputDropdown(Value);
                        break;
                    case coreInput:
                        CoreSelect(Value);
                        break;
                    default:
                        throw new Exception("Select() failed : Unsupported QuickEdit Type.");
                }
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemInListWithoutSearch")]
        public void SelectItemInListWithoutSearch(String Value)
        {
            try
            {
                Initialize();
                ClickEditButton();

                /* classes of dropdowns currently supported:
                 * coreinput only. If used on other dropdowns, use Select
                */
                DlkLogger.LogInfo("Re-initializing element because of the dropdown arrow click.");
                Initialize();
                IWebElement comboBox = mElement.FindElement(By.XPath(".//div[@class='core-field']/div"));

                try
                {
                    // clear the text because if there is text inside,the clear button will block the dropdown arrow when being clicked.
                    // causing a different element to receive the click
                    // Selenium's Clear() is causing the quickedit to lose focus on the control
                    var input = comboBox.FindElement(By.XPath(".//input"));
                    ClearField(input);
                }
                catch
                {
                    //swallow. input was not found
                }
                var dropdownClassContainer = comboBox.GetAttribute("class");

                SearchSelectWithoutSearch(Value);
                Thread.Sleep(MEDIUM_WAIT);

                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemInListWithoutSearch() failed : " + e.Message, e);
            }
        }

        [Keyword("ClearField")]
        public void ClearField()
        {
            Initialize();
            IWebElement ctlInput = mElement.FindElements(By.XPath(".//input")).Count > 0 ? mElement.FindElement(By.XPath(".//input")) : null;
            IWebElement ctlLabel = mElement.FindElements(By.XPath(".//descendant::*[contains(@class,'label')]")).Count > 0 ? mElement.FindElement(By.XPath(".//descendant::*[contains(@class,'label')]")) : null;
            // Clears the field for QuickEdit Control Type
            try
            {
                // code for the Edit(), clicks the pencil icon         
                if (ctlInput == null || ctlInput.Displayed == false)
                {
                    ClickEditButton();
                    ctlInput = mElement.FindElement(By.XPath(".//input"));
                }
                //Determine if combobox or textbox
                string inputClass = ctlInput.GetAttribute("class").ToLower();

                if (inputClass.Contains("text-field") || inputClass.Contains("number-field"))
                {
                    ClearField(ctlInput);
                }
                else
                {
                    IWebElement mButton = null;
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath(".//following-sibling::*[@class='clear-icon']")).Count > 0)
                    {
                        mButton = ctlInput.FindElement(By.XPath(".//following-sibling::*[@class='clear-icon']"));
                        if (!mButton.Displayed)
                        {
                            ctlInput.Click();
                        }
                        mButton.Click();
                    }
                    else
                    {
                        ClearField(ctlInput);
                    }

                }
                
                DlkLogger.LogInfo("ClearField() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClearField() failed : " + e.Message, e);
            }
        }
        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            ExpectedValue = ExpectedValue.Trim();
            try
            {
                Initialize();
                switch (mType)
                {
                    case RegClass:
                        RegVerifyText(ExpectedValue);
                        break;
                    case OneFourthClass:
                        OneFourthVerifyText(ExpectedValue);
                        break;
                    case HeaderClass:
                        HeaderEditVerifyText(ExpectedValue);
                        break;
                    case CoreClass:
                        CoreVerifyText(ExpectedValue);
                        break;
                    default:
                        throw new Exception("VerifyText() failed : Unsupported QuickEdit Type.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            ExpectedValue = ExpectedValue.Trim();
            try
            {
                Initialize();
                switch (mType)
                {
                    case RegClass:
                        RegVerifyTextContains(ExpectedValue);
                        break;
                    case OneFourthClass:
                        OneFourthVerifyTextContains(ExpectedValue);
                        break;
                    case HeaderClass:
                        HeaderEditVerifyTextContains(ExpectedValue);
                        break;
                    case CoreClass:
                        CoreVerifyTextContains(ExpectedValue);
                        break;
                    default:
                        throw new Exception("Unknown QuickEdit type.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLabel", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyLabel(String ExpectedValue)
        {
            if (String.IsNullOrWhiteSpace(ExpectedValue)) throw new ArgumentException("ExpectedValue must not be empty.");
            ExpectedValue = ExpectedValue.Trim();
            try
            {
                Initialize();
                DlkLabel label = null;
                switch (mType)
                {
                    case RegClass:
                        label = new DlkLabel("Label", this, "XPATH", mRegLabelXPath);
                        break;
                    case OneFourthClass:
                        label = new DlkLabel("Label", this, "XPATH", mRegLabelXPath);
                        break;
                    case HeaderClass:
                        label = new DlkLabel("Label", this, "XPATH", mHeaderLabelXPath);
                        break;
                    case CoreClass:
                        label = new DlkLabel("Label", this, "XPATH", mCoreLabelXPath);
                        break;
                }
                try
                {
                    DlkAssert.AssertEqual("Label text", ExpectedValue, label.GetValue());
                }
                catch
                {
                    DlkLogger.LogInfo("Attempting to compare values without colon");
                    // try to compare after trimming colon. GetValue cannot get the colon in the summarypane. Even XPath helper cannot get the value with the colon.
                    // it seems that the product performs some code to the quickedit's text, appending a colon to it before it is displayed (something like that) 
                    // which is why we are seeing a quickedit label that has a colon but the actual value doesn't really have a colon
                    DlkAssert.AssertEqual("Label text after trimming the colon", ExpectedValue.Trim(':'), label.GetValue());
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLabel() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
                //Will only scroll into view if control exists anywhere on the page, otherwise, skip scrollintoview
                try
                {
                    FindElement();
                    this.ScrollIntoViewUsingJavaScript();
                }
                catch
                {
                    //do nothing
                }
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            Initialize();
            try
            {
                String sValue = mElement.GetAttribute("class");
                if ((sValue.Contains("locked")))
                {
                    sValue = "true";
                }
                else
                {
                    //Most QuickEdit controls have 'editable' in their class even thought they are not
                    //Try to hover on the control and check if the edit icon is readonly
                    DlkBaseControl ctrl = new DlkBaseControl("QuickEdit", mElement);
                    ctrl.MouseOver();
                    if (mElement.FindElements(By.XPath("..//*[contains(@class, 'icon-edit')]")).Count > 0)
                    {
                        sValue =
                            (!mElement.FindElement(By.XPath("..//*[contains(@class, 'icon-edit')]")).Enabled)
                                .ToString();
                    }
                    else
                    {
                        sValue = "true";
                    }
                }

                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(sValue));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyToolTip(String ExpectedValue)
        {
            Initialize();
            try
            {
                switch (mType)
                {
                    case RegClass:
                        RegEdit(ExpectedValue);
                        break;
                    case OneFourthClass:
                        OneFourthEdit(ExpectedValue);
                        break;
                    case HeaderClass:
                        HeaderEdit(ExpectedValue);
                        break;
                    case CoreClass:
                        CoreEdit(ExpectedValue);
                        break;
                    default:
                        throw new Exception("Unknown QuickEdit type.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolTip() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickLink")]
        public void ClickLink()
        {
            try
            {
                Initialize();
                if (mElement.FindElements(By.XPath(".//*[contains(@class, 'linked-text')]")).Count > 0)
                {
                    IWebElement mLink = mElement.FindElement(By.XPath(".//*[contains(@class, 'linked-text')]"));
                    DlkBaseControl link = new DlkBaseControl("Link", mLink);
                    link.ClickUsingJavaScript();
                }
                else
                {
                    throw new Exception("No link available.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickLink() failed: " + e.Message);
            }
        }

        [Keyword("VerifyRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRequired(String ExpectedValue)
        {
            bool expectedValue, actualValue;
            if (!bool.TryParse(ExpectedValue, out expectedValue)) throw new Exception("ExpectedValue must be a boolean.");

            try
            {
                Initialize();
                actualValue = mElement.GetAttribute("class").Contains("required");
                DlkAssert.AssertEqual("VerifyRequired", expectedValue, actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRequired() failed : " + e.Message);
            }
        }

        [Keyword("ClickQuickEditButton")]
        public void ClickQuickEditButton(String ButtonName)
        {
            try
            {
                Initialize();
                IWebElement ctlBtn = null;
                var txtBox = mElement.FindElement(By.TagName("input"));
                switch (ButtonName.ToLower())
                {
                    case "opendate":
                    case "search":
                        ctlBtn = txtBox.FindElement(By.XPath(".//following-sibling::*[@class='tap-target']"));
                        break;
                    case "clear":
                        ctlBtn = txtBox.FindElement(By.XPath(".//span[contains(@class,'clear-icon')]"));
                        break;
                    default:
                        throw new Exception("ClickQuickEditButton() failed : Button name " + ButtonName + " not recognized");
                        //break; to remove 'unreachable code detected' warning
                }

                (new DlkBaseControl("QuickEdit Button", ctlBtn)).Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickQuickEditButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyQuickEditInputExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyQuickEditInputExists(String ExpectedValue)
        {
            try
            {
                bool expectedValue, actualValue;
                if (!Boolean.TryParse(ExpectedValue, out expectedValue)) throw new Exception("ExpectedValue must be a boolean.");

                Initialize();
                //check if textbox or combobox is existing
                actualValue = mElement.FindElements(By.XPath(".//input")).Where(x => x.Displayed).Count() > 0;
                //check if textarea is existsing
                if (!actualValue)
                    actualValue = mElement.FindElements(By.XPath(".//textarea")).Where(x => x.Displayed).Count() > 0;

                DlkAssert.AssertEqual("VerifyQuickEditInputExists", expectedValue, actualValue);
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyQuickEditInputExists failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDropDownOptionExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDropDownOptionExists(String ExpectedValue)
        {
            try
            {
                bool expectedValue, actualValue;
                if (!Boolean.TryParse(ExpectedValue, out expectedValue)) throw new Exception("ExpectedValue must be a boolean.");

                //check if dropdown option is existing
                Initialize();
                CoreEdit();
                actualValue = mElement.FindElements(By.XPath("//div[@class='dropdown-field-container']")).Count() > 0;
                DlkAssert.AssertEqual("VerifyDropDownOptionExists", expectedValue, actualValue);
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDropDownOptionExists failed: " + e.Message, e);
            }
        }

        [Keyword("VerifyErrorMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyErrorMessage(String ErrorMessage)
        {
            try
            {
                Initialize();
                IWebElement errorElm = null;
                IList<IWebElement> errorElements;
                //If class doesn't contain core-component, try searching for parent that contains core-component... then search for core-error
                if (!mElement.GetAttribute("class").Contains("core-component"))
                {
                    errorElements = mElement.FindElements(By.XPath("./parent::div[contains(@class,'core-component')]//*[@class='core-error']"));
                }
                else
                {
                    errorElements = mElement.FindElements(By.XPath(".//*[@class='core-error']"));
                }

                errorElm = errorElements.Where(x => x.Displayed).FirstOrDefault();

                if (errorElm == null || !errorElm.Displayed)
                {
                    throw new Exception("Error message not found.");
                }
                DlkAssert.AssertEqual("VerifyErrorMessage() : " + mControlName, ErrorMessage, errorElm.Text.Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyErrorMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemDimmed", new String[]
         {
             "1|text|SearchText|abc", "2|text|TargetItem|123 abc company",
             "3|text|TrueOrFalse|True",
         })]
        public void VerifyItemDimmed(String SearchText, String TargetItem, String TrueOrFalse)
        {
            try
            {
                bool ExpectedValue = false;

                if (!Boolean.TryParse(TrueOrFalse, out ExpectedValue)) throw new Exception("Value in TrueOrFalse is invalid.");
                if (String.IsNullOrWhiteSpace(TargetItem)) throw new Exception("TargetItem must not be empty.");

                Initialize();
                switch (mType)
                {
                    case CoreClass:
                        CoreEdit();
                        break;
                    default:
                        throw new Exception("QuickEdit type (" + mType + "' is not yet supported.");
                }

                IWebElement mInput = null;
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    if (this.mElement.FindElements(By.TagName("input")).Where(x => x.Displayed).Count() > 0)
                    {
                        mInput = this.mElement.FindElement(By.TagName("input"));
                    }

                    DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                    txtInput.SetTextOnly(SearchText);
                    Thread.Sleep(SHORT_WAIT);
                }

                /*Code below currently works on: 
                 * CORE type of ComboBox
                 * I haven't seen a RegEdit/HeaderEdit/OneFourth with a combobox.
                */

                DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", "//ul[contains(@class,'" + mCoreListClass + "')]");
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mCoreDdwnArrowClass));
                    mArrowDown.Click();
                    Thread.Sleep(LONG_WAIT);
                }
                list.FindElement();

                IReadOnlyCollection<IWebElement> ddList = list.mElement.FindElements(By.CssSelector("li.search-result"));

                IWebElement item = null;
                bool bFound = false;
                foreach (IWebElement listItem in ddList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("List", listItem);
                    string val = DlkString.RemoveCarriageReturn(ctl.GetValue());

                    if (val.Trim() == TargetItem)
                    {
                        item = listItem;
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyItemDimmed() : Unable to find item [" + TargetItem + "]");
                }

                string itmClass = item.GetAttribute("class");
                Boolean ActualValue = false;
                if (itmClass.ToLower().Contains("dimmed"))
                    ActualValue = true;
                else
                    ActualValue = false;

                DlkAssert.AssertEqual("VerifyItemDimmed()", ExpectedValue, ActualValue);

                //Clear the input and click on banner to lose focus
                if (!String.IsNullOrWhiteSpace(SearchText)) mInput.Clear();
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemDimmed() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTextAndVerifyListNoResults", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetTextAndVerifyListNoResults(String TextToEnter, String TrueOrFalse)
        {
            try
            {
                Boolean expected = false;
                String noResultsExpectedString = "no results were found";
                // guard clause
                if (!Boolean.TryParse(TrueOrFalse, out expected)) throw new ArgumentException("TrueOrFalse must be a Boolean value");

                Initialize();

                try
                {
                    SetTextOnly(TextToEnter);
                }
                catch
                {
                    ClickEditButton();
                    SetTextOnly(TextToEnter);
                }

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }

                list.FindElement();
                var noResultElement = list.mElement.FindElement(By.XPath(".//div[@class='no-results']"));
                // check if displayed
                DlkAssert.AssertEqual("Check if no results were found", expected, noResultElement.Displayed & noResultElement.Text.ToLower().Trim().Equals(noResultsExpectedString));
            }
            catch (NoSuchElementException ex)
            {
                throw new Exception(String.Format("SetTextAndVerifyListNoResults() failed. Unable to find no results class. See error message:\n{0} ", ex.Message));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("SetTextAndVerifyListNoResults() failed. {0}", ex.Message));
            }
        }

        [Keyword("OpenDropdown")]
        public void OpenDropdown()
        {
            try
            {
                Initialize();
                ClickEditButton();

                if (mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0)
                {
                    ClickDropdownArrow();
                    DlkLogger.LogInfo("OpenDropdown() passed.");
                }
                else
                {
                    throw new Exception("Dropdown arrow not found.");
                }

            }
            catch (Exception e)
            {
                throw new Exception("OpenDropdown() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickDropdownLink")]
        public void ClickDropdownLink(String TargetLinkText)
        {
            try
            {
                Initialize();
                ClickEditButton();
                ClickDropdownArrow();

                DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);
                list.FindElement();
                IWebElement mLink = list.mElement.FindElement(By.XPath(mSearchLinkXPath + "[contains(text(),'" + TargetLinkText + "')]"));
                mLink.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickDropdownLink() failed : " + e.Message, e);
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

        #endregion

        #region PRIVATE METHODS
        /*
        private void SelectItemInSearchListDropdown(string Value)
        {
            bool bFound = false;
            var ddArrow = mElement.FindElement(By.XPath(".//form//span[@class='tap-target']"));
            ddArrow.Click();
            Thread.Sleep(loadingBuffer); // wait while the list loads somewhere in the document
            var resultItems = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'results-container')]//li"));
            var visibleResultItems = resultItems.Where(item => item.Displayed).ToList();
            foreach (var item in visibleResultItems)
            {
                if (item.FindElement(By.XPath(".//div[@class='search-name ']")).Text.Trim().Equals(Value.Trim()))
                {
                    DlkLogger.LogInfo(string.Format("Clicking {0} ...", Value));
                    item.Click();
                    bFound = true;
                    break;
                }
            }
            
            Thread.Sleep(loadingBuffer); //just wait a bit for the selected item to update the quick edit
            if (!bFound)
            {
                throw new Exception("Select() failed. Item was not found in the Dropdown");
            }
            else
            {
                DlkLogger.LogInfo("Select() passed.");
            }
        }
        */
        private void SelectItemInDdwnInputDropdown(string Value)
        {
            try
            {
                bool bFound = false;
                var ddArrow = mElement.FindElement(By.XPath(".//form//div[@class='ddwnArrow down']"));
                //ddArrow.Click();
                new DlkBaseControl("DropdownArrow", ddArrow).Click(5, 5);
                Thread.Sleep(loadingBuffer); // wait while the list loads somewhere in the document
                var resultItems = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'ddwnListContainer')]//tr")).Where(item => item.Displayed);

                foreach (var item in resultItems)
                {
                    var elem = new DlkBaseControl("resultItem", item.FindElement(By.XPath(".//div[contains(@class,'text')]")));
                    if (elem.IsElementStale())
                    {
                        throw new Exception("Element is stale");
                    }
                    else
                    {
                        var itemTxt = elem.GetValue().Trim();
                        if (itemTxt.Equals(Value.Trim()))
                        {
                            DlkLogger.LogInfo(string.Format("Clicking {0} ...", Value));
                            item.Click();
                            bFound = true;
                            break;
                        }
                    }
                }
                Thread.Sleep(loadingBuffer); //just wait a bit for the selected item to update the quick edit
                if (!bFound)
                {
                    throw new Exception("Select() failed. Item was not found in the Dropdown");
                }
                else
                {
                    DlkLogger.LogInfo("Select() passed.");
                }
            }
            catch
            {
                throw;
            }
        }

        private void RegEdit(String ExpectedValue = null)
        {
            DlkLogger.LogInfo("RegEdit() started");
            //IWebElement mLabelElement = mElement.FindElement(By.XPath(mRegLabelXPath));
            try
            {
                DlkLabel label = new DlkLabel("Label", this, "XPATH", mRegLabelXPath);

                if (label.Exists(1))
                {
                    label.MouseOver();
                }
                else
                {
                    //IWebElement mContentElement = mElement.FindElement(By.CssSelector(mRegContentCSS));
                    DlkLabel lblContent = new DlkLabel("Content", this, "XPATH", mRegContentXPath);
                    DlkLogger.LogInfo("Start performing mouse over on content label.");
                    lblContent.MouseOver();
                    DlkLogger.LogInfo("No error during mouse over.");
                }

                DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", mRegEditIconXPath);
                try
                {
                    if (ExpectedValue == null)
                    {
                        btnEdit.Click();
                    }
                    else
                    {
                        btnEdit.VerifyToolTip(ExpectedValue);
                    }
                }
                catch
                {
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].style.display='block'", btnEdit.mElement);
                    btnEdit.mElement.Click();
                    btnEdit.mElement.Click();//click twice. first selenium click doesnt work. clickusingjavascript also doesn't work.
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].style.display='none'", btnEdit.mElement);
                }

            }
            catch
            {
                CustomEdit(ExpectedValue);
            }
        }

        private void CustomEdit(String ExpectedValue = null)
        {
            DlkLogger.LogInfo("CustomEdit() started");
            DlkLabel label = new DlkLabel("Label", this, "XPATH", "./div[contains(@class, 'label')]");

            if (label.Exists(1))
            {
                label.MouseOver();
            }
            else
            {
                DlkLabel lblContent = new DlkLabel("Content", this, "XPATH", "./div[contains(@class, 'content')]");
                DlkLogger.LogInfo("Start performing mouse over on content label.");
                lblContent.MouseOver();
                DlkLogger.LogInfo("No error during mouse over.");
            }

            DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", ".//div[@class='icon-edit']");
            if (ExpectedValue == null)
            {
                btnEdit.Click();
            }
            else
            {
                btnEdit.VerifyToolTip(ExpectedValue);
            }
        }
        private void CoreEdit(String ExpectedValue = null)
        {
            DlkLogger.LogInfo("CoreEdit() started");
            DlkBaseControl hoverTo = new DlkBaseControl("Label", this, "XPATH", mCoreLabelXPath);

            if (hoverTo.Exists(1))
            {
                DlkLogger.LogInfo("Start performing initial mouse over on core label.");
                hoverTo.MouseOver();
                DlkLogger.LogInfo("No error during mouse over.");
            }
            else
            {
                hoverTo = new DlkBaseControl("Content", this, "XPATH", mCoreContentXPath);
                DlkLogger.LogInfo("Start performing initial mouse over on core content.");
                hoverTo.MouseOver();
                DlkLogger.LogInfo("No error during mouse over.");
            }

            DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", mCoreEditIconXPath);

            Action<DlkBaseControl> hoverOnCtrl = (ctrl) =>
            {
                int i = 1, retryLimit = 3;
                while (!btnEdit.Exists(1) && i++ <= retryLimit)
                {
                    DlkLogger.LogInfo("Retry [" + i + "]");
                    ctrl.MouseOverOffset(i, i);
                    btnEdit = new DlkButton("Edit Icon", this, "XPATH", mCoreEditIconXPath);
                    Thread.Sleep(SHORT_WAIT);
                }
            };

            hoverOnCtrl(hoverTo);

            if (ExpectedValue == null)
            {
                IWebElement quickEditIcon = mElement.FindElements(By.XPath(mCoreEditIconXPath)).Any() ?
                       mElement.FindElement(By.XPath(mCoreEditIconXPath)) : throw new Exception("Core quick edit icon not found.");
                Actions actions = new Actions(DlkEnvironment.AutoDriver);

                try
                {
                    DlkLogger.LogInfo("Performing click on edit icon.");
                    actions.MoveToElement(quickEditIcon).Click().Perform();

                    // Few instances where edit icon is clicked but input field does not appear, if it does not appear retry click action
                    // Add another tag element to support new tags input field

                    if (!mElement.GetAttribute("class").Contains("in-quick-edit"))
                    {
                        DlkLogger.LogInfo("Input field not found. Retrying click again.");
                        hoverOnCtrl(hoverTo);
                        actions.MoveToElement(quickEditIcon).Click().Perform();
                    }
                }
                catch
                {
                    DlkLogger.LogInfo("Click on EditIcon failed. Retrying to hover.");
                    hoverOnCtrl(hoverTo);
                    DlkLogger.LogInfo("Performing click on edit icon.");
                    actions.MoveToElement(quickEditIcon).Click().Perform();
                }
            }
            else
            {
                btnEdit.VerifyToolTip(ExpectedValue);
            }
        }
        private void HeaderEdit(String ExpectedValue = null)
        {
            DlkLogger.LogInfo("HeaderEdit() started");

            DlkLabel label = new DlkLabel("Label", this, "XPATH", mHeaderLabelXPath);
            if (label.Exists(1))
            {
                label.MouseOver();
            }
            else
            {
                DlkLogger.LogInfo("Label is not found");
            }

            DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", mRegEditIconXPath);
            try
            {
                if (ExpectedValue == null)
                {
                    btnEdit.Click();
                }
                else
                {
                    btnEdit.VerifyToolTip(ExpectedValue);
                }
            }
            catch (Exception)
            {
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].style.display='block'", btnEdit.mElement);
                btnEdit.mElement.Click();
                btnEdit.mElement.Click();//click twice. first selenium click doesnt work.clickusingjavascript also doesn't work.
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].style.display='none'", btnEdit.mElement);
            }
        }

        private void DDwnSelect(String Value)
        {
            DlkBaseControl list;
            string listXpath;

            DlkBaseControl el = new DlkBaseControl("ComboBox", mElement);
            el.ScrollIntoViewUsingJavaScript();

            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            if (mArrowDown.GetAttribute("class") == "progress-area")
            {
                mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
                list = new DlkBaseControl("List", "XPATH_DISPLAY", ".//*[@class='search-list']");
                listXpath = "./descendant::li/div[contains(.,'" + Value + "')]/..";
            }
            else
            {
                list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
                listXpath = "./descendant::td/div[text()='" + Value + "']";
            }

            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!mArrowDown.FindElement(By.XPath("..")).GetAttribute("class").ToLower().Contains("openedbelow"))
            {
                ctlArrowDown.ClickUsingJavaScript();
            }

            list.FindElement();

            IWebElement itm = list.mElement.FindElement(By.XPath(listXpath));

            DlkBaseControl ctlItem = new DlkBaseControl("Item", itm);
            ctlItem.Click();

        }

        private void OneFourthEdit(String ExpectedValue = null)
        {
            try
            {
                IWebElement mLabelElement = mElement.FindElement(By.XPath(mOneFourthLabelXPath));
                DlkLabel label = new DlkLabel("Label", mLabelElement);
                if (label.Exists(3))
                {
                    label.MouseOver();
                }
                else
                {
                    IWebElement mContentElement = mElement.FindElement(By.XPath(mOneFourthContentXPath));
                    DlkLabel lblContent = new DlkLabel("Content", mContentElement);
                    lblContent.MouseOver();
                }

                IWebElement editIconElement = mElement.FindElement(By.XPath(mOneFourthEditIconXPath));
                DlkButton btnEdit = new DlkButton("Edit Icon", editIconElement);
                try
                {
                    if (ExpectedValue == null)
                    {
                        btnEdit.Click();
                    }
                    else
                    {
                        btnEdit.VerifyToolTip(ExpectedValue);
                    }
                }
                catch
                {
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].style.display='block'", btnEdit.mElement);
                    btnEdit.mElement.Click();
                    btnEdit.mElement.Click();//click twice. first selenium click doesnt work. clickusingjavascript also doesn't work.
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].style.display='none'", btnEdit.mElement);
                }
            }
            catch
            {
                RegEdit(ExpectedValue);
            }
        }

        private void RegVerifyText(String ExpectedText)
        {
            IWebElement mContentElement = null;
            DlkLabel lblContent = null;
            String ActualResult = "";
            int Index = -1;


            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mRegContentXPath));
                lblContent = new DlkLabel("Content", mContentElement);
                lblContent.VerifyText(ExpectedText);
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }

                }

                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyText()", ExpectedText, ActualResult.Trim());
            }
        }

        private void RegVerifyTextContains(String ExpectedText)
        {
            IWebElement mContentElement = null;
            DlkLabel lblContent = null;
            String ActualResult = "";
            int Index = -1;

            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mRegContentXPath));
                lblContent = new DlkLabel("Content", mContentElement);
                lblContent.VerifyTextContains(ExpectedText);
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }

                }

                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyTextContains()", true, ActualResult.Contains(ExpectedText));
            }
        }
        private String RegGetText()
        {
            IWebElement mContentElement = null;
            String ActualResult = "";
            int Index = -1;

            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mRegContentXPath));
                ActualResult = mContentElement.Text;
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }

                }
            }
            if (ActualResult.Contains("\r\n"))
            {
                ActualResult = ActualResult.Replace("\r\n", "<br>");
            }
            return ActualResult;
        }
        private void HeaderEditVerifyText(String ExpectedText)
        {
            IWebElement mContentElement = null;
            DlkLabel lblContent = null;
            String ActualResult = "";
            int Index = -1;


            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mHeaderLabelXPath));
                lblContent = new DlkLabel("Content", mContentElement);
                lblContent.VerifyText(ExpectedText);
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }

                }

                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyText()", ExpectedText, ActualResult.Trim());
            }
        }

        private void HeaderEditVerifyTextContains(String ExpectedText)
        {
            IWebElement mContentElement = null;
            DlkLabel lblContent = null;
            String ActualResult = "";
            int Index = -1;


            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mHeaderLabelXPath));
                lblContent = new DlkLabel("Content", mContentElement);
                lblContent.VerifyTextContains(ExpectedText);
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }

                }

                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyTextContains()", true, ActualResult.Contains(ExpectedText));
            }
        }

        private String HeaderGetText()
        {
            IWebElement mContentElement = null;
            String ActualResult = "";
            int Index = -1;

            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mHeaderLabelXPath));
                ActualResult = mContentElement.Text;
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }
                }
            }
            if (ActualResult.Contains("\r\n"))
            {
                ActualResult = ActualResult.Replace("\r\n", "<br>");
            }
            return ActualResult;
        }

        private void OneFourthVerifyText(String ExpectedText)
        {
            IWebElement mContentElement = null;
            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mOneFourthContentXPath));
            }
            catch
            {
                RegVerifyText(ExpectedText);
                // mContentElement = this.mElement.FindElement(By.XPath(mRegDivSpanXPath)); 
            }
        }

        private void OneFourthVerifyTextContains(String ExpectedText)
        {
            IWebElement mContentElement = null;
            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mOneFourthContentXPath));
            }
            catch
            {
                RegVerifyTextContains(ExpectedText);
                // mContentElement = this.mElement.FindElement(By.XPath(mRegDivSpanXPath)); 
            }
        }
        private String OneFourthGetText()
        {
            IWebElement mContentElement = null;
            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mOneFourthContentXPath));
                return mContentElement.Text;
            }
            catch
            {
                return RegGetText();
                // mContentElement = this.mElement.FindElement(By.XPath(mRegDivSpanXPath));                
            }
        }


        /// <summary>
        /// Allows setting of text to QuickEdit control types
        /// </summary>
        /// <param name="Value"></param>
        private void SetQuickEditText(String Value)
        {
            Boolean bFound = false;
            Thread.Sleep(SHORT_WAIT); //loading and what not
            ReadOnlyCollection<IWebElement> txtBoxes = mElement.FindElements(By.XPath(".//form/*[1]")); //can be input or textarea

            //Added to support CoreClass type of QuickEdit
            if (txtBoxes.Count == 0)
            {
                txtBoxes = mElement.FindElements(By.XPath("./div[contains(@class,'core-field')]"));
            }

            foreach (var item in txtBoxes)
            {
                try
                {
                    // add different classes here as needed to support other types of quickedits
                    if (item.GetAttribute("class").ToLower().Contains("quick-edit-input"))
                    {
                        item.Clear();
                        item.SendKeys(Value + Keys.Enter);
                        bFound = true;
                        break;
                    }
                    else if (item.GetAttribute("class").ToLower().Contains("quickeditinput"))
                    {
                        item.Clear();
                        item.SendKeys(Value + Keys.Enter);
                        bFound = true;
                        break;
                    }
                    else if (item.GetAttribute("class").ToLower().Contains("search_list"))
                    {
                        var searchlistTxtBox = item.FindElement(By.XPath(".//input[contains(@class,'input')]"));
                        searchlistTxtBox.Clear();
                        searchlistTxtBox.SendKeys(Value);
                        bFound = true;
                        break;
                    }
                    else if (item.TagName == "div" && item.FindElements(By.TagName("input")).Count > 0)
                    {
                        var txtBox = item.FindElements(By.TagName("input")).Where(x => x.Displayed).FirstOrDefault();

                        if (item.FindElements(By.XPath(".//*[contains(@class, 'clear-icon')]")).Where(x => x.Displayed).Count() > 0)
                        {
                            IWebElement clearBttn = item.FindElement(By.XPath(".//*[contains(@class, 'clear-icon')]"));
                            clearBttn.Click();
                        }
                        else
                        {
                            txtBox.SendKeys(Keys.Control + "a");
                        }

                        txtBox.SendKeys(Value == String.Empty ? Keys.Delete : Value);
                        bFound = true;
                        break;
                    }
                    else if (item.GetAttribute("class").ToLower().Contains("core-field"))
                    {
                        IWebElement txtBox = null;
                        if (item.FindElements(By.XPath(".//iframe")).Where(element => element.Displayed).Count() > 0)
                        {
                            txtBox = item.FindElement(By.XPath(".//iframe"));
                        }
                        else if (item.FindElements(By.TagName("input")).Where(element => element.Displayed).Count() > 0)
                        {
                            txtBox = item.FindElement(By.TagName("input"));
                        }
                        if (txtBox == null)
                        {
                            if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@data-id, 'quick-edit')]//div[@class='core-field']//iframe")).Where(element => element.Displayed).Count() > 0)
                            {
                                txtBox = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@data-id, 'quick-edit')]//div[@class='core-field']//iframe"));
                            }
                            else
                            {
                                throw new Exception("Unsupported core-field textbox");
                            }
                        }
                        txtBox.Click(); // give the cursor to the textbox
                        txtBox.SendKeys(Keys.Control + "a"); // .Clear() isn't working for some reason
                        txtBox.SendKeys(Value == String.Empty ? Keys.Delete : Value);
                        bFound = true;
                        break;
                    }
                }
                catch
                {
                    throw;
                }
            }

            if (!bFound)
            {
                throw new Exception("Textbox not recognized.");
            }
        }

        /// <summary>
        /// Searchlist combo boxes do not display contents of list in one go. Set text on the input box instead
        /// </summary>
        /// <param name="Value"></param>
        private void SetSearchListText(String Value)
        {
            IWebElement txtInput = mElement.FindElement(By.XPath(".//input"));
            ClearField(txtInput);
            txtInput.SendKeys(Value);
            Thread.Sleep(SHORT_WAIT);
            try
            {
                txtInput = mElement.FindElement(By.XPath(".//input"));
                if (txtInput.Displayed)
                {
                    txtInput.SendKeys(Keys.Tab); //select first item in selection and close dropdown
                    Thread.Sleep(loadingBuffer);
                }
            }
            catch
            {
                //do nothing. This means the input is no longer visible.
            }

            /// [JAM 12.07.2017] Restructuring of quickedit comboboxes made the next lines of code unnecessary
            /// The aim is to set text only and not to select from list
            /// For observation, if scripts need to select from list, use the keyword Set + SelectItemInListWithoutSearch combination just like in comboboxes
            /// 
            //try
            //{
            //    Initialize();
            //    IWebElement mContent = this.mElement.FindElement(By.XPath(mRegContentXPath));
            //    if (!mContent.Text.Trim().Equals(Value.Trim()))
            //    {

            //        if (!mContent.Text.Trim().Contains(Value.Trim()))
            //        {
            //            throw new Exception("Select() failed. Item '" + Value + "' not found in dropdown.");
            //        }
            //        else
            //        {
            //            DlkLogger.LogInfo(Value + "was found in " + mContent.Text);
            //        }
            //    }
            //    else
            //    {
            //        DlkLogger.LogInfo(Value + "was found.");
            //    }
            //}
            //catch
            //{
            //    // Find element again if stale reference
            //    Thread.Sleep(loadingBuffer);
            //    Initialize();
            //    IWebElement mContent = this.mElement.FindElement(By.XPath(mRegContentXPath));
            //    if (!mContent.Text.Trim().Equals(Value.Trim()))
            //    {
            //        if (!mContent.Text.Trim().Contains(Value.Trim()))
            //        {
            //            throw new Exception("Select() failed. Item '" + Value + "' not found in dropdown.");
            //        }
            //        else
            //        {
            //            DlkLogger.LogInfo(Value + "was found in " + mContent.Text);
            //        }
            //    }
            //    else
            //    {
            //        DlkLogger.LogInfo(Value + "was found.");
            //    }
            //}

        }

        /// <summary>
        /// Sets text only without additional actions
        /// </summary>
        /// <param name="Value"></param>
        private void SetTextOnly(String Value)
        {
            IWebElement txtInput = mElement.FindElement(By.XPath(".//input"));
            ClearField(txtInput);
            txtInput.SendKeys(Value);
            Thread.Sleep(SHORT_WAIT);
        }

        private void CoreSelect(String Value)
        {
            IWebElement txtInput = mElement.FindElement(By.XPath(".//input"));
            var isReadOnly = txtInput.GetAttribute("readonly");
            if (isReadOnly == "true")
            {
                txtInput.Click();
            }
            else
            {
                ClearField(txtInput);
                txtInput.SendKeys(Value);
            }

            Thread.Sleep(SHORT_WAIT);
            try
            {
                // we do this instead so we can check for result count instead of instantly failing when the element is not found
                var results = DlkEnvironment.AutoDriver.FindElements(By.ClassName("results")).Where(element => element.Displayed).ToList();
                var list = results.Count > 0 ? results.First() : null;
                if (list != null)
                {
                    IWebElement noResults = list.FindElement(By.ClassName("no-results"));
                    if (noResults.Displayed)
                    {
                        txtInput.Clear();
                        throw new Exception("Search value [" + Value + "] not in the list.");
                    }
                }

                Thread.Sleep(3000);
                var resultItems = DlkEnvironment.AutoDriver.FindElements(By.XPath("//li[contains(@class,'search-result')]")).Where(item => item.Displayed);
                Thread.Sleep(MEDIUM_WAIT);

                foreach (var item in resultItems)
                {
                    var elem = new DlkBaseControl("resultItem", item.FindElement(By.XPath(".//div[@class='search-info']")));
                    if (!elem.IsElementStale())
                    {
                        var itemTxt = elem.GetValue().Trim();
                        if (itemTxt.Equals(Value.Trim()))
                        {
                            DlkLogger.LogInfo(string.Format("Clicking {0} ...", Value));
                            item.Click();
                            break;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Couldn't find control"))
                    throw new Exception("Search value [" + Value + "] not found.");
            }

        }

        /// <summary>
        /// Used to verify items in list of CoreClass 
        /// Parameter ExpectedValues should contain all items in ComboBox separated by ~
        /// </summary>
        /// <param name="ExpectedValues"></param>
        private void CoreVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            try
            {
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mCoreListClass);
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mCoreDdwnArrowClass));
                IWebElement input = this.mElement.FindElement(By.XPath("..//input[contains(@class,'dropdown-field')]"));
                if (!list.Exists(1))
                {
                    mArrowDown.Click();
                }
                list.FindElement();
                List<IWebElement> lstItems = list.mElement.FindElements(By.XPath(".//*[@class='search-name']")).ToList();
                if (lstItems.Count == 0)
                    lstItems = list.mElement.FindElements(By.XPath(".//*[contains(@class,'search-result')]")).Where(x => x.Displayed).ToList();
                String ActValues = "";
                for (int i = 0; i < lstItems.Count; i++)
                {
                    DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                    if (!String.IsNullOrEmpty(ActValues))
                    {
                        ActValues = ActValues + "~";
                    }
                    ActValues = ActValues + item.GetValue();
                }

                if (VerifyPartialList)
                {
                    DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, ActValues, true);
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActValues, false);
                }
                ClickBanner();
            }
            catch (Exception ex)
            {
                if (VerifyPartialList)
                    throw new Exception("VerifyPartialList() failed: " + ex.Message);
                else
                    throw new Exception("VerifyList() failed: " + ex.Message);
            }
        }

        private String CoreGetText()
        {
            IWebElement txtContent = null;

            if (mElement.FindElements(By.XPath(".//input")).Count > 0) // QuickEdit is in edit mode
            {
                txtContent = mElement.FindElement(By.XPath(".//input"));
            }
            else
            {
                txtContent = mElement.FindElement(By.XPath(".//div[contains(@class, 'core-field')]"));
            }

            DlkBaseControl content = new DlkBaseControl("Content", txtContent);
            String ActualResult = content.GetValue().Trim();

            if (ActualResult.Contains("/>") || ActualResult.Contains("</") || ActualResult.Contains("emptyAsNone"))
            {
                ActualResult = "none";
            }
            return ActualResult;
        }

        /// <summary>
        /// Used to verify the text of CoreClass 
        /// </summary>
        /// <param name="ExpectedValue"></param>
        private void CoreVerifyText(String ExpectedValue)
        {
            try
            {
                IWebElement txtContent = null;

                if (mElement.FindElements(By.XPath(".//input")).Count > 0) // QuickEdit is in edit mode
                {
                    txtContent = mElement.FindElement(By.XPath(".//input"));
                }
                else
                {
                    txtContent = mElement.FindElement(By.XPath(".//div[contains(@class, 'core-field')]"));
                }

                DlkBaseControl content = new DlkBaseControl("Content", txtContent);
                string ActualValue = content.GetValue().Replace("\r\n", " ").Trim();

                if (!ActualValue.ToLower().Equals(ExpectedValue.ToLower()))
                {
                    DlkLogger.LogInfo(String.Format("Assertion failed. Expected value: [{0}] is not equal to Actual value: [{1}]. Checking if the item has 'None' value by looking at the class...", ExpectedValue, ActualValue));
                    // look for emptyAsNone class
                    //look for html tags
                    ActualValue = String.IsNullOrEmpty(ActualValue) || (ActualValue.Contains("/>") || ActualValue.Contains("</")) || txtContent.FindElements(By.XPath(".//*[contains(@class,'emptyAsNone')]")).Where(elem => elem.Displayed).Count() > 0 ?
                        "none" : ActualValue;
                }
                // log assertion
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue.ToLower(), ActualValue.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CoreVerifyTextContains(String ExpectedValue)
        {
            try
            {
                IWebElement txtContent = null;

                if (mElement.FindElements(By.XPath(".//input")).Count > 0) // QuickEdit is in edit mode
                {
                    txtContent = mElement.FindElement(By.XPath(".//input"));
                }
                else
                {
                    txtContent = mElement.FindElement(By.XPath(".//div[contains(@class, 'core-field')]"));
                }

                DlkBaseControl content = new DlkBaseControl("Content", txtContent);
                string ActualValue = DlkString.RemoveCarriageReturn(content.GetValue().Trim()).ToLower();
                ExpectedValue = ExpectedValue.ToLower();

                if (!ActualValue.Contains(ExpectedValue))
                {
                    DlkLogger.LogInfo(String.Format("Assertion failed. Expected value: [{0}] does not contain to Actual value: [{1}]. Checking if the item has 'None' value by looking at the class...", ExpectedValue, ActualValue));
                    ActualValue = String.IsNullOrEmpty(ActualValue) || (ActualValue.Contains("/>") || ActualValue.Contains("</")) || txtContent.FindElements(By.XPath(".//*[contains(@class,'emptyAsNone')]")).Where(elem => elem.Displayed).Count() > 0 ?
                            "none" : ActualValue;
                }

                DlkAssert.AssertEqual("VerifyTextContains()", true, ActualValue.Contains(ExpectedValue));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private bool CheckIfQuickEditHasAComboBox()
        {
            if (mElement.GetAttribute("class").Contains("dropdown_field"))
                return true;
            else if (mElement.FindElements(By.XPath(".//div[contains(@class, 'dropdown-field-container')]")).Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Select an item without setting search text
        /// </summary>
        private void SearchSelectWithoutSearch(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);
            Boolean bFound = false;
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0 ?
                    this.mElement.FindElement(By.XPath(mSearchArrowXpath)) :
                    this.mElement.FindElement(By.XPath(mSearchArrowXpath2));

                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
                Thread.Sleep(LONG_WAIT);
                list.FindElement(); //Need to re-initialize after clicking to update the list control
            }

            IReadOnlyCollection<IWebElement> ddList = (list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Count > 0) ?
                              list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")) : list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]"));

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

        /// <summary>
        /// The QuickEdit needs to lose focus after performing any action to ensure other keywords would perform as expected.
        /// </summary>
        private void ClickBanner()
        {
            /*Removed this functionality after adjusting timeout recurrence to 30mins*/

            //// If a dialog exist, do not click on banner
            //if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class, 'dialog')]")).Where(x => x.Displayed).ToList().Count <= 0)
            //{
            //    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@class='banner-left']")));
            //    bannerCtrl.Click();
            //}
            //else
            //{
            //    DlkLogger.LogInfo("Dialog detected. Don't click banner.");
            //}
        }

        /// <summary>
        /// Clear a field without using the X icon
        /// </summary>
        /// <param name="Field">Field to be cleared</param>
        private void ClearField(IWebElement Field)
        {
            try
            {
                DlkLogger.LogInfo("Clearing the field without using 'X' icon.");
                if (DlkEnvironment.mBrowser != "safari")
                {
                    Field.SendKeys(Keys.Control + "a");
                    Field.SendKeys(Keys.Delete);
                }
                else
                {
                    //Update in the future in case Selenium's Clear malfunctions for Safari
                    Field.Clear();
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClearField() failed: " + e.Message);
            }
        }

        private void ClickEditButton()
        {
            switch (mType)
            {
                case RegClass:
                    RegEdit();
                    break;
                case OneFourthClass:
                    OneFourthEdit();
                    break;
                case HeaderClass:
                    HeaderEdit();
                    break;
                case CoreClass:
                    CoreEdit();
                    break;
                default:
                    throw new Exception("Unknown QuickEdit type.");
            }
        }

        private void ClickDropdownArrow()
        {
            DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0 ?
                    this.mElement.FindElement(By.XPath(mSearchArrowXpath)) :
                    this.mElement.FindElement(By.XPath(mSearchArrowXpath2));

                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
                Thread.Sleep(LONG_WAIT);
            }
        }
        #endregion
    }
}
