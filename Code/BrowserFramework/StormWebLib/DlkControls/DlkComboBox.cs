using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;
using CommonLib.DlkUtility;


namespace StormWebLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        #region Constants

        private const String DDwnClass = "ddwnInput"; //regular drop-down
        private const String DropDownClass = "dropdown"; //Database field on Login screen
        private const String DDwnColClass = "ddwnCol"; //regular drop-down in the table
        //private const String PopupMenuClass = "navigator_popupmenu";
        private const String PopupMenuClass = "popupmenu";
        private const String PopupMenu2Class = "popup-menu";
        private const String PopupMenu3Class = "search-filter narrow";
        private const String SearchClass = "search-input-container";
        private const String SearchListFluid = "navigator_ngcrm_search_list fluid"; //Search List
        private const String SearchList = "navigator_ngcrm_search_list"; //Search List
        private const String SearchColList = "ddwnCol navigator_ngcrm_search_list"; //Search List in a table
        private const String QuickEdit = "navigator_ngcrm_quick_edit";
        private const String FormDdown = "formDdwn navigator_ngcrm_search_list"; //Copy Timesheet
        private const String SelectOption = "";
        private const String CoreFieldClass = "core-field";
        private const String CustomDivClass = "div";
        private const String DDwnContainerClass = "dropdown-field-container"; //New UI for search list
        private const String CoreDdwnField = "core_dropdown_field"; //Used by tables under HUBS
        private const String ModeChooserClass = "mode-chooser"; //Used by Application Mode in login
        #endregion

        #region Private Variables

        private SelectElement mobjSelectElement;

        private String mComboBoxType = "";

        //DDwn Controls
        private String mInputCSS = "input";
        private String ddwnInputListClass = "ddwnTableDiv";
        private String dropdownListClass = "dropdown-list";
        private String mArrowDownCSS = "div>div";
        //private String mArrowDownXPath = "//div[contains(@class,'ddwnArrow')]";

        //Popup Controls
        private String mPopupInputXPath = "./span[1]";
        private String mPopupInputXPath2 = "./span[2]";
        private String mPopupArrowXPath = ".//*[contains(@class,'arrow')]";
        private String mPopupListClass = "popupmenuBody";
        private String mPopupListClass2 = "popupmenu";
        //Search Controls
        private String mSearchInputXPath = ".//input[contains(@class,'search-input')]";
        private String mSearchArrowXpath = ".//*[contains(@class,'tap-target')]";
        private string mDropDownArrowXpath = ".//*[contains(@class,'droparrow')]";
        private String mSearchArrowXpath2 = ".//*[contains(@class,'show-results')]";
        private String mSearchListClass = "results";
        private String mSearchListXPath = "//ul[contains(@class,'results')]";

        //ddwnCol Controls (Cell ComboBox)
        private String mDDwnColInputCSS = "input";
        private String mDDwnArrowDownXPATH = ".//*[contains(@class,'ddwnArrow')]";
        private String mDropDownArrowDownXPATH = ".//*[contains(@class,'dropdown-arrow')]";
        private String mDDwnSearchColClass = "search_list";
        private String mDDwnSearchColListClass = "results";

        //core-field Controls
        //private String mCoreFieldDdwnArrowXPATH = ".//*[contains(@class, 'dropdown-icon')]";
        private String mCoreFieldColListXPATH = ".//*[contains(@class, 'format-list')]";

        private Boolean VerifyAfterSelect = true;

        //Used for auto correct
        private String mTentativeComboBoxType = "";


        //New Search Control for Core Field
        private String mCoreInput = ".//input[@class='dropdown-field']";

       #endregion

        #region Constructors

        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkComboBox(String ControlName, String SearchType, String SearchValue, Boolean VerifyAfterSelect)
            : base(ControlName, SearchType, SearchValue)
        {
            this.VerifyAfterSelect = VerifyAfterSelect;
        }

        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }

        //public DlkComboBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Always call this for every keyword
        /// </summary>
        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();            

            String mClass = this.mElement.GetAttribute("class");
            if (mClass.Contains(DDwnClass)) //"ddwnInput" //regular drop-down
            {
                mComboBoxType = DDwnClass;
            }

            else if (mClass == FormDdown) //formDdwn navigator_ngcrm_search_list
            {
                mComboBoxType = FormDdown;
            }
            else if (mClass.Contains(SearchColList)) //"ddwnCol navigator_ngcrm_search_list"; //Search List in a table
            {
                mComboBoxType = SearchColList;
            }
            else if (mClass.Contains(DDwnColClass)) //"ddwnCol" //regular drop-down in the table
            {
                mComboBoxType = DDwnColClass;
            }
            else if (mClass.Contains(PopupMenuClass) || mClass.Contains(PopupMenu2Class) || mClass.Contains(PopupMenu3Class)) //"navigator_popupmenu"
            {
                mComboBoxType = PopupMenuClass;
            }
            else if (mClass.Contains(SearchClass)) //"search-input-container"
            {
                mComboBoxType = SearchClass;

            }
            else if (mClass.Contains(SearchListFluid)) //"navigator_ngcrm_search_list fluid"; //Search List
            {
                mComboBoxType = SearchListFluid;
            }
            else if ((mClass.Contains(SearchList)) && (!mClass.Contains("fluid")) && (!mClass.Contains("edit")))
            //"navigator_ngcrm_search_list"; //Search List
            {
                mComboBoxType = SearchList;
            }
            else if (mClass.Contains(QuickEdit)) //"navigator_ngcrm_quick_edit"
            {
                mComboBoxType = QuickEdit;
            }
            else if (mClass.Contains(DDwnContainerClass)) //dropdown-input-container
            {
                mComboBoxType = DDwnContainerClass;
            }
            else if (mClass.Contains(CoreDdwnField)) //Used by tables under Hubs
            {
                mComboBoxType = CoreDdwnField;
            }
            else if (mClass.Contains(DropDownClass)) //Database field on Login screen
            {
                mComboBoxType = DropDownClass;
            }
            else if ((mClass == "") || (mClass == "ui-widget-content")) //new language selection list
            {
                if (this.mElement.FindElements(By.XPath(".//*[contains(@class,'actionsMenu')]")).Count > 0)
                {
                    mComboBoxType = PopupMenuClass;
                }
                else
                {
                    mComboBoxType = SelectOption;
                }
            }
            else if (mClass.Contains("standard-select")) //new Configuration Security table
            {
                mComboBoxType = SelectOption;
            }
            
            else if (mClass.Contains("edit")) //booking column for CRM
            {
                mComboBoxType = DDwnColClass;
            }
            else if (mClass.Contains("actions")) //actionsMenu
            {
                mComboBoxType = PopupMenuClass;
            }
            else if (mClass.Contains(CoreFieldClass))
            {
                mComboBoxType = CoreFieldClass;
            }
            else if (mClass.Contains(CustomDivClass))
            {
                mComboBoxType = CustomDivClass;
            }
            else if (mClass.Contains(ModeChooserClass))
            {
                mComboBoxType = ModeChooserClass;
            }
            else if ( this.mElement.TagName.ToLower() == "select")
            {
                mComboBoxType = SelectOption;
            }
            else
            {
                throw new Exception("Initialize() error. Unknown ComboBox type '" + mClass + "'");
            }



        }

        public new bool VerifyControlType()
        {
            string mClass;

            FindElement();
            //Check the tag of the element selected
            switch (mElement.TagName)
            {
                case "div":
                    mClass = GetAttributeValue("class").ToLower();
                    if (mClass.Contains("ddwnarrow") || mClass.Contains("ddwninput"))
                    {
                        mTentativeComboBoxType = DDwnClass;
                        return true;
                    }
                    else if (mClass.Contains("popupmenu"))
                    {
                        mTentativeComboBoxType = PopupMenuClass;
                        return true;
                    }
                    else if (mClass.Contains("search-input-container"))
                    {
                        mTentativeComboBoxType = SearchClass;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //break;

                case "input":
                    mClass = GetAttributeValue("class").ToLower();
                    if (mClass.Contains("ddwnfilter"))
                    {
                        mTentativeComboBoxType = DDwnClass;
                        return true;
                    }
                    else if (mClass.Contains("search-input"))
                    {
                        mTentativeComboBoxType = SearchClass;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //break;

                case "span":
                    mClass = GetAttributeValue("class").ToLower();
                    if (mClass.Contains("filter"))
                    {
                        mTentativeComboBoxType = PopupMenuClass;
                        return true;
                    }
                    else if (mClass.Contains("show-results"))
                    {
                        mTentativeComboBoxType = SearchClass;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //break;

                case "label":
                    try
                    {
                        IWebElement parentElement =
                            mElement.FindElement(By.XPath("./ancestor::div[@class='search-input-container']"));
                        mTentativeComboBoxType = SearchClass;
                        return true;
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        return false;
                    }
                //break;

                default:
                    return false;
            }

        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("ComboBox", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                string mTag = mElement.TagName;
                string mClass = GetAttributeValue("class").ToLower();
                switch (mTentativeComboBoxType)
                {
                    case DDwnClass:
                        switch (mTag)
                        {
                            case "div":
                                if (mClass.Contains("ddwnarrow"))
                                {
                                    IWebElement parentDDwn =
                                        mElement.FindElement(By.XPath("./ancestor::div[@class='ddwnInput']"));
                                    mCorrectControl = new DlkBaseControl("CorrectControl", parentDDwn);
                                    mAutoCorrect = true;
                                }
                                break;
                            case "input":
                                if (mClass.Contains("ddwnfilter"))
                                {
                                    IWebElement parentDDwn =
                                        mElement.FindElement(By.XPath("./ancestor::div[@class='ddwnInput']"));
                                    mCorrectControl = new DlkBaseControl("CorrectControl", parentDDwn);
                                    mAutoCorrect = true;
                                }
                                break;
                        }
                        break;

                    case PopupMenuClass:
                        if (mTag == "span" && mClass.Contains("filter"))
                        {
                            IWebElement parentPopup =
                                mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'navigator_popupmenu')]"));
                            mCorrectControl = new DlkBaseControl("CorrectControl", parentPopup);
                            mAutoCorrect = true;
                        }
                        break;

                    case SearchClass:
                        IWebElement parentSearch =
                            mElement.FindElement(By.XPath("./ancestor::div[@class='search-input-container']"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentSearch);
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
            if(!bFound)
            {
                //for results dropdown
                list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                bFound = list.Exists();
            }
            DlkAssert.AssertEqual("CompareValues() List: ", Convert.ToBoolean(ExpectedValue), bFound); 
            //close the list
            ctlArrowDown.Click();
        }

        public new string GetValue()
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case CoreFieldClass:
                    case DDwnColClass:
                        return DDwnColGetValue();
                    case SearchColList:
                        return DDwnColGetValue();
                    default:
                        return base.GetValue();
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);

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

        #endregion

        #region Keywords

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                mElement.Click();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] {"1|text|Value|TRUE"})]
        public void Select(String Item)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Item)) throw new Exception("Item must not be empty");
                Item = Item.Trim();
                Initialize();
                switch (mComboBoxType)
                {
                    case ModeChooserClass:
                        ModeChooserSelect(Item);
                        break;
                    case DDwnClass:
                        DDwnSelect(Item);
                        break;
                    case DropDownClass:
                        DropDownSelect(Item);
                        break;
                    case CustomDivClass:
                    case PopupMenuClass:
                        PopupSelect(Item);
                        break;
                    case SearchClass:
                        SearchSelect(Item);
                        //  DDwnSet(Item);
                        break;
                    case SearchListFluid:
                    case SearchList:
                    case SearchColList:
                        DDwnSet(Item);
                        break;
                    case DDwnColClass:
                        DDwnColSelect(Item);
                        break;
                    case QuickEdit:
                        QuickEditSelect(Item);
                        break;
                    case SelectOption:
                        SelectOptionSelect(Item); //new language selection list
                        break;
                    case FormDdown:
                        FormDDwnSelect(Item); //Copy Timesheet combo box
                        break;
                    case CoreDdwnField:
                    case CoreFieldClass:
                    case DDwnContainerClass:
                        try
                        {
                            SearchSelectWithoutSearch(Item); //search type corefield
                        }
                        catch
                        {
                            CoreFieldSelect(Item);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Checks the sorting order of the dropdown list if it is in ascending order, kw is specific to control SearchActivity combobox under BD_Activities
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("VerifySort", new String[]
         {
             "1|text|SortByNameOrDate|Name or Date",
             "2|text|TrueOrFalse|True or False"
         })]
        public void VerifySort(String SortByNameOrDate, String TrueOrFalse)
        {
            try
            {
                //guard clauses
                if (String.IsNullOrWhiteSpace(TrueOrFalse) || String.IsNullOrWhiteSpace(SortByNameOrDate))
                    throw new ArgumentException("Parameter must not be empty.");
                SortByNameOrDate = SortByNameOrDate.ToUpper();
                TrueOrFalse = TrueOrFalse.ToUpper();
                if (!(SortByNameOrDate.Equals("NAME") || SortByNameOrDate.Equals("DATE")))
                    throw new ArgumentException("Parameter must be either 'Name' or 'Date' (not case sensitive).");
                if (!(TrueOrFalse.Equals("TRUE") || TrueOrFalse.Equals("FALSE")))
                    throw new ArgumentException("Parameter must be either 'True' or 'False' (not case sensitive).");
                bool bSortedExpected = bool.Parse(TrueOrFalse);
                bool bConsecutiveElements = false;

                Initialize();
                var cmbBoxItemsXpath = "//*[contains(@class,'results')]";
                    // xpath of the dropdown that appears after clicking SearchActivity dropdown arrow
                var nameXpath = ".//li//div[normalize-space(@class)='search-name']";
                    // xpath of to find all items in the dropdown, relative to SearchActivity control
                var dateXpath = ".//li//span[normalize-space(@class)='activity-start-date']";
                List<IWebElement> ActualListByName = null;
                List<IWebElement> ActualListByDate = null;
                List<IWebElement> ExpectedListByName = null;
                List<IWebElement> ExpectedListByDate = null;
                var dropdowns =
                    DlkEnvironment.AutoDriver.FindElements(By.XPath(cmbBoxItemsXpath))
                        .Where(dropdown => dropdown.Displayed)
                        .ToList(); // should only be 1
                Action<List<IWebElement>, List<IWebElement>, Int32> ItemsComparer = (actual, expected, index) =>
                {
                    // ACTUAL RESULT= NOT SORTED, EXPECTED RESULT = SORTED
                    if (!actual[index].Text.Equals(expected[index].Text) && !bConsecutiveElements && bSortedExpected)
                    {
                        throw new Exception(
                            string.Format(
                                "Items are not sorted in item {0} where '{1}' is expected to be '{2}' when sorted in ascending order. Comparison was made using the name.",
                                (index + 1), actual[index].Text, expected[index].Text));
                    }
                    // ACTUAL RESULT= NOT SORTED, EXPECTED RESULT = SORTED
                    else if (!actual[index].Text.Equals(expected[index].Text) && bConsecutiveElements && bSortedExpected)
                    {
                        throw new Exception(
                            string.Format(
                                "Items are not sorted in item {0} where '{1}' is expected to be '{2}' when sorted in ascending order. Comparison was made using the date because the name is the equal.",
                                (index + 1), actual[index].Text, expected[index].Text));
                    }
                    else if (actual[index].Text.Equals(expected[index].Text) && bSortedExpected)
                    {
                        // pass
                        DlkLogger.LogInfo(
                            string.Format(
                                "Item {0} is sorted because '{1}' is equal to '{2}' when sorted in ascending order",
                                (index + 1), actual[index].Text, expected[index].Text));
                    }
                };

                foreach (var dropdown in dropdowns)
                {
                    // gets an unordered list of all displayed items
                    ActualListByName = dropdown.FindElements(By.XPath(nameXpath)).Where(item => item.Displayed).ToList();
                    ActualListByDate = dropdown.FindElements(By.XPath(dateXpath)).Where(item => item.Displayed).ToList();
                    // sorted list to check against the actual list
                    ExpectedListByName = ActualListByName.OrderBy(item => item.Text).ToList();
                    ExpectedListByDate = ActualListByDate.OrderBy(item => item.Text).ToList();
                }
                if (SortByNameOrDate.Equals("NAME"))
                {
                    if (bSortedExpected == false)
                    {
                        if (ActualListByName.SequenceEqual(ExpectedListByName))
                        {
                            throw new Exception("Items are in ascending order");
                        }
                        else
                        {
                            DlkLogger.LogInfo("It is not sorted.");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ActualListByName.Count - 1; i++)
                        {
                            // if sequence is same then check if consecutive items are the same in text. if yes, check if the secondary name is sorted in ascending order
                            if (i > 0)
                            {
                                if (ActualListByName[i].Text.Equals(ActualListByName[i - 1].Text))
                                    bConsecutiveElements = true;
                            }
                            if (bConsecutiveElements)
                            {
                                ItemsComparer(ActualListByDate, ExpectedListByDate, i);
                                bConsecutiveElements = false; // to check for next pair in next iteration
                            }
                            else ItemsComparer(ActualListByName, ExpectedListByName, i);
                        }
                    }
                }
                else if (SortByNameOrDate.Equals("DATE"))
                {
                    if (bSortedExpected == false)
                    {
                        if (ActualListByDate.SequenceEqual(ExpectedListByDate))
                        {
                            throw new Exception("Items are in ascending order");
                        }
                        else
                        {
                            DlkLogger.LogInfo("It is not sorted.");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ActualListByDate.Count - 1; i++)
                        {
                            // if sequence is same then check if consecutive items are the same in text. if yes, check if the secondary name is sorted in ascending order
                            if (i > 0)
                            {
                                if (ActualListByDate[i].Text.Equals(ActualListByDate[i - 1].Text))
                                    bConsecutiveElements = true;
                            }
                            if (bConsecutiveElements)
                            {
                                ItemsComparer(ActualListByName, ExpectedListByName, i);
                                bConsecutiveElements = false; // to check for next pair in next iteration
                            }
                            else ItemsComparer(ActualListByDate, ExpectedListByDate, i);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        [Keyword("ClickLink", new String[] {"1|text|Value"})]
        public void ClickLink(String Value)
        {
            Initialize();
            this.Click();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
                //".//span[contains(@class,'show-results')]";        
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            Thread.Sleep(3000);
            ctlArrowDown.Click();
            Thread.Sleep(3000);
            list.FindElement();
            IWebElement item =
                list.mElement.FindElement(By.XPath("./descendant::span[contains(text(),'" + Value + "')]"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);

            ctlItem.Click();
            // ctlItem.ClickUsingJavaScript();

        }

        [Keyword("OpenDropdown", new String[] {"1|text|Value"})]
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
                    case SearchClass:
                        if (this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0)
                        {
                            mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                        }
                        else
                        {
                            mArrowDown = this.mElement.FindElement(By.XPath(".//*[contains(@class,'show-results')]"));
                        }
                        break;
                    case SearchList:
                        mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                        if (mArrowDown.GetAttribute("class") == "progress-area")
                        {
                            mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
                        }
                        break;
                    case SearchColList:
                        mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                        break;
                    case DDwnColClass:
                        mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                        break;
                    case QuickEdit:
                        mArrowDown = mElement.FindElement(By.XPath(".//div//span[contains(@class, 'content')]"));
                        break;
                    case FormDdown:
                    case DDwnContainerClass:
                    case CoreFieldClass:
                        mArrowDown = this.mElement.FindElement(By.ClassName("tap-target"));
                        break;
                    case CustomDivClass:
                        try
                        {
                            try
                            {
                                mArrowDown = mElement.FindElement(By.CssSelector("button"));
                            }
                            catch 
                            {
                                mArrowDown = mElement.FindElement(By.XPath(mSearchArrowXpath));
                            }
                        }
                        catch
                        {
                            mArrowDown = mElement.FindElement(By.XPath(mDropDownArrowXpath));
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

        [Keyword("VerifyLinkExists", new String[] {"1|text|Expected Value|TRUE",})]
        public void VerifyLinkExists(String TrueOrFalse)
        {
            Initialize();
            // IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath)); //".//span[contains(@class,'show-results')]"
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            ctlArrowDown.Click();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");
            list.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            //close the list
            ctlArrowDown.Click();
        }

        [Keyword("Set", new String[] {"1|text|Value|"})]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                    case DDwnContainerClass:
                        DDwnSet(Value);
                        break;
                    case PopupMenuClass:
                        PopupSet(Value);
                        break;
                    case SearchClass:
                    case SearchListFluid:
                        SearchSet(Value);
                        break;
                    case SearchList:
                        DDwnSet(Value);
                        break;
                    case SearchColList:
                        DDwnSet(Value);
                        break;
                    case CoreFieldClass:
                        DDwnSet(Value);
                        break;

                }

            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("SetText", new String[] { "1|text|Value|" })]
        public void SetText(String Value)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                    case CoreFieldClass:
                    case DDwnContainerClass:
                        SetTextOnly(Value);
                        break;
                    case PopupMenuClass:
                        PopupSet(Value);
                        break;
                    case SearchClass:
                    case SearchListFluid:
                    case SearchList:
                    case SearchColList:
                        SearchSetTextOnly(Value);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetText() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemInListWithoutSearch", new String[] {"1|text|Value|"})]
        public void SelectItemInListWithoutSearch(String Value)
        {
            try
            {
                if (string.IsNullOrEmpty(Value)) throw new Exception("Value must not be empty");
                Initialize();
                switch (mComboBoxType)
                {
                    case SearchClass:
                    case SearchListFluid:
                    case SearchList:
                    case SearchColList:
                    case DDwnContainerClass:
                    case CoreFieldClass:
                        SearchSelectWithoutSearch(Value);
                        break;
                    default:
                        throw new Exception("Keyword not supported for this combo box type (" + mComboBoxType + ")");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemInListWithoutSearch() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] {"1|text|Expected Value|ExampleValue"})]
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
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] {"1|text|Expected Value|TRUE"})]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (Convert.ToBoolean(TrueOrFalse) == true)
                    Initialize();

                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] {"1|text|Expected Value|TRUE"})]
        public void VerifyReadOnly(String TrueOrFalse)
        { 
            try
            {
                Initialize();
                bool ActualValue = false;
                ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
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
                    case DropDownClass:
                        DropDownAvailableInList(Item, TrueOrFalse);
                        break;
                    case QuickEdit:
                    case PopupMenuClass:
                        PopupAvailableInList(Item, TrueOrFalse);
                        break;
                    case DDwnColClass:
                        DDwnColAvailableInList(Item, TrueOrFalse);
                        break;
                    case SearchClass:
                    case SearchList:
                    case SearchListFluid:
                        SearchAvailableInList(Item, TrueOrFalse);
                        break;
                   
                    case CoreFieldClass:
                    case DDwnContainerClass:
                        CoreFieldAvailableInList(Item, TrueOrFalse);
                        break;
                    default:
                        throw new Exception("Invalid combo box type");
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListItemEnabled", new String[]
         {
             "1|text|Item|Sample item",
             "2|text|Expected Value|TRUE"
         })]
        public void VerifyListItemEnabled(String Item, String TrueOrFalse)
        {
            try
            {
               Initialize();
                switch (mComboBoxType)
                {

                    case PopupMenuClass:
                        PopupItemEnabled(Item, TrueOrFalse);
                        break;
                    default:
                        //for future use
                        break;
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItemEnabled() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] {"1|text|Expected Values|-Select-~All~Range"})]
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

            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
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
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListCount", new String[] { "1|text|ExpectedValue|1" })]
        public void VerifyListCount(String ExpectedCount)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnVerifyListCount(ExpectedCount);
                        break;
                    case PopupMenuClass:
                    case QuickEdit:
                        PopupVerifyListCount(ExpectedCount);
                        break;
                    case DDwnColClass:
                        DDwnColVerifyListCount(ExpectedCount);
                        break;
                    case SearchClass:
                        SearchVerifyListCount(ExpectedCount);
                        break;
                    case SearchColList:
                        SearchVerifyListCount(ExpectedCount);
                        break;
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInListWithoutSearch", new String[]
         {
             "1|text|Item|Sample item",
             "2|text|Expected Value|TRUE"
         })]
        public void VerifyAvailableInListWithoutSearch(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case SearchClass:
                    case SearchList:
                    case SearchListFluid:
                    case DDwnContainerClass:
                    case CoreFieldClass:
                        SearchVerifyAvailableWithoutSearch(Item, TrueOrFalse);
                        break;
                    default:
                        throw new Exception("Keyword not supported for this combo box type (" + mComboBoxType + ")");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInListWithoutSearch() failed : " + e.Message, e);
            }
        }

        [Keyword("SearchItemExists", new String[]
         {
             "1|text|SearchedItem|ItemValue",
             "2|text|VariableName|TrueFalse"
         })]
        public void SearchItemExists(String SearchedItem, String VariableName)
        {
            Initialize();
            IWebElement mInput;
            if (String.IsNullOrWhiteSpace(SearchedItem)) throw new Exception("SearchedItem must not be empty");
            if (String.IsNullOrWhiteSpace(VariableName)) throw new Exception("VariableName must not be empty");
            switch (mComboBoxType)
            {
                case SearchClass:
                    mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
                    break;
                case DDwnContainerClass:
                    mInput = this.mElement.FindElement(By.XPath(mCoreInput));
                    break;
                default:
                    throw new Exception("ComboBox Type error");
            }
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            // txtInput.ShowAutoComplete(SearchedItem);
            txtInput.SetTextOnly(SearchedItem);
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            if (list.GetValue() == "No results were found")
            {
                DlkVariable.SetVariable(VariableName, false.ToString());
            }
            else
            {
                DlkVariable.SetVariable(VariableName, true.ToString());
            }
        }

        /// <summary>
        /// Search the list by clicking the dropdown, then 
        /// </summary>
        /// <param name="SearchedItem"></param>
        /// <param name="VariableName"></param>
        [Keyword("SearchItemExistsWithoutSet", new String[]
         {
             "1|text|SearchedItem|sample item value",
             "2|text|VariableName|true or false"
         })]
        public void SearchItemExistsWithoutSet(String SearchedItem, String VariableName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(SearchedItem)) throw new Exception("SearchedItem must not be empty");
                if (String.IsNullOrWhiteSpace(VariableName)) throw new Exception("VariableName must not be empty");
                Initialize();

                var mInput = this.mElement.FindElements(By.XPath(mSearchInputXPath)).Count > 0 ?
                    this.mElement.FindElement(By.XPath(mSearchInputXPath)) : this.mElement.FindElement(By.CssSelector(mDDwnColInputCSS));

                var dropdown = mInput.FindElement(By.XPath("./following-sibling::span[@class='tap-target']"));
                dropdown.Click();
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (list.GetValue() == "No results were found")
                {
                    DlkVariable.SetVariable(VariableName, false.ToString());
                }
                else
                {
                    bool found = false;
                    foreach (
                        var item in
                        list.mElement.FindElements(
                            By.XPath("./div[@class='result-items']/*//div[normalize-space(@class)='search-name']")))
                    {
                        if (item.Text.Trim().Equals(SearchedItem))
                        {
                            found = true;
                            break;
                        }
                    }
                    DlkVariable.SetVariable(VariableName, found.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SearchItemExistsWithoutSearch() failed. " + ex.Message);
            }
        }

        [Keyword("EditFilterItem", new String[] { "1|text|Value" })]
        public void EditFilterItem(String Value)
        {
            Initialize();

            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            if (!list.Exists(1))
            {
                this.MouseOver();
                IWebElement mArrowDown = null;
                mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();

                if (!list.Exists(1))
                {
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                }
            }

            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath(".//span[normalize-space()='" + Value + "']/../.."));
            DlkBaseControl FilterItem = new DlkBaseControl("ArrowDown", item);
            FilterItem.MouseOver();

            IWebElement Edit = item.FindElement(By.XPath(".//*[@class='popupRightImage']"));
            DlkButton btnEdit = new DlkButton("Edit Icon", Edit);
            btnEdit.Click();

        }


        [Keyword("ItemExists", new String[]
         {
             "1|text|SearchedItem|ItemValue",
             "2|text|VariableName|TrueFalse"
         })]
        public void ItemExists(String SearchedItem, String VariableName)
        {
            try
            {
                if (String.IsNullOrEmpty(VariableName)) throw new Exception("Variable name must not be empty.");

                Initialize();
                Boolean isExisting = false;

                if (mComboBoxType == DDwnClass)
                {
                    DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
                    
                    if (!list.Exists(1))
                    {
                        IWebElement mArrowDown = this.mElement.FindElement(By.CssSelector(mArrowDownCSS)); //"div>div"
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();
                    }
                    list.FindElement();

                    isExisting = list.mElement.FindElements(By.XPath("./descendant::td/div[text()='" + SearchedItem + "']"))
                        .Where(x => x.Displayed).Count() > 0;
                    //to close the dropdown after checking if the filter exists
                    mElement.Click();
                }
                else
                {
                    DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
                    if (!list.Exists(1))
                    {
                        this.MouseOver();
                        IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();

                        if (!list.Exists(1))
                        {
                            list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                        }
                    }

                    list.FindElement();
                    isExisting = list.mElement.FindElements(By.XPath(".//span[normalize-space()='" + SearchedItem + "']/../.."))
                        .Where(x => x.Displayed).Count() > 0;
                    //to close the dropdown after checking if the filter exists
                    mElement.Click();
                }

                DlkVariable.SetVariable(VariableName, isExisting.ToString());
                DlkLogger.LogInfo(VariableName + " is now [" + isExisting.ToString() + "]");
            }
            catch(Exception e)
            {
                throw new Exception("ItemExists() failed: " + e.Message);
            }
        }

        [Keyword("VerifyListItem", new String[]
         {
             "1|text|SearchedItem|Posted",
             "2|text|Items|1/8/2014~Posted~Posted",
             "3|text|ImageExists|TRUE"
         })]
        public void VerifyListItem(String SearchedItem, String Items, String ImageExists)
        {
            try
            {
                Initialize();
                IWebElement mInput = this.mElement.FindElements(By.XPath(mSearchInputXPath)).Count > 0
                    ? this.mElement.FindElements(By.XPath(mSearchInputXPath)).Where(x => x.Displayed).First()
                    : this.mElement.FindElements(By.CssSelector(mDDwnColInputCSS)).Where(x => x.Displayed).First();

                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.SetTextOnly(SearchedItem);
                Thread.Sleep(3000);

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0 ? 
                        this.mElement.FindElement(By.XPath(mSearchArrowXpath)) : this.mElement.FindElement(By.XPath(mSearchArrowXpath2));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }
                list.FindElement();

                DlkAssert.AssertEqual("VerifyListItem", Items, list.GetValue().Replace("\r\n", "~"));
                try
                {
                    IWebElement mImage = list.mElement.FindElement(By.TagName("img"));
                    DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), mImage.Displayed);
                }
                catch
                {
                    DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyListItem() failed: " + ex.Message);
            }
        }

        [Keyword("VerifyItemImageExists", new String[]
         {
             "1|text|SearchText|abc", "2|text|TargetItem|123 abc",
             "3|text|ImageExists|TRUE"
         })]
        public void VerifyItemImageExists(String SearchText, String TargetItem, String ImageExists)
        {
            try
            {
                Initialize();
                IWebElement mInput = this.mElement.FindElements(By.XPath(mSearchInputXPath)).Count > 0 ? this.mElement.FindElement(By.XPath(mSearchInputXPath)) : mElement.FindElement(By.TagName(mDDwnColInputCSS));
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.SetTextOnly(SearchText);
                Thread.Sleep(3000);
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }
                list.FindElement();
                IWebElement listItem = null;

                IReadOnlyCollection<IWebElement> ddList = (list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Count > 0) ?
                             list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")) : list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]"));

                foreach (IWebElement item in ddList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("List", item);
                    string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                    if (val.Equals(TargetItem.Trim()))
                    {
                        listItem = item;
                        DlkLogger.LogInfo("Item [" + TargetItem + "]found. Clicking...");
                        break;
                    }
                }

                if (listItem == null) throw new Exception("Unable to find item in the list.");

                bool imgExists = false;
                
                if(listItem.FindElements(By.TagName("img")).Count > 0)
                {
                    imgExists = true;
                }
                else if(listItem.FindElements(By.XPath(".//a[contains(@class, 'search_thumb')][contains(@style,'background-image: url')]")).Count > 0)
                {
                    imgExists = true;
                }

                DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), imgExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageDimmed", new String[]
         {
             "1|text|SearchText|abc", "2|text|TargetItem|123 abc",
             "3|text|ImageExists|TRUE"
         })]
        public void VerifyItemImageDimmed(String SearchText, String TargetItem, String TrueOrFalse)
        {
            try
            {
                Initialize();

                Boolean ExpectedValue = false;
                if (Boolean.TryParse(TrueOrFalse, out ExpectedValue))
                {
                    IWebElement mInput = null;
                    if (this.mElement.FindElements(By.XPath(mSearchInputXPath)).Count > 0)
                    {
                        mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
                    }
                    else if (this.mElement.FindElements(By.XPath(".//input[contains(@class,'dropdown-field')]")).Count > 0)
                    {
                        mInput = this.mElement.FindElement(By.XPath(".//input[contains(@class,'dropdown-field')]"));
                    }
                    else
                    {
                        throw new Exception("VerifyItemImageDimmed() : Unable to find input control of item [" + TargetItem + "]");
                    }
                    DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                    txtInput.SetTextOnly(SearchText);
                    Thread.Sleep(3000);
                    DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                    if (!list.Exists(1))
                    {
                        IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();
                        Thread.Sleep(5000);
                    }
                    list.FindElement();
                    IWebElement item = null;

                    string xpath_text = "./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" +
                                        TargetItem + "')]/ancestor::li";
                    string xpath_datakey = "./descendant::div[contains(@class,'resultListItem')][contains(@data-key,'" +
                                           TargetItem + "')]/ancestor::li";

                    if (list.mElement.FindElements(By.XPath(xpath_text)).Count > 0)
                    {
                        item = list.mElement.FindElement(By.XPath(xpath_text));
                    }
                    else if (list.mElement.FindElements(By.XPath(xpath_datakey)).Count > 0)
                    {
                        item = list.mElement.FindElement(By.XPath(xpath_datakey));
                    }
                    else
                    {
                        throw new Exception("VerifyItemImageDimmed() : Unable to find item [" + TargetItem + "]");
                    }

                    string itmClass = item.GetAttribute("class");
                    Boolean ActualValue = false;
                    if (itmClass.ToLower().Contains("dimmed"))
                        ActualValue = true;
                    else
                        ActualValue = false;

                    DlkAssert.AssertEqual("VerifyItemImageDimmed()", ExpectedValue, ActualValue);
                }
                else
                {
                    throw new Exception("VerifyItemImageDimmed() : Invalid input [" + TrueOrFalse + "]");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageDimmed() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageTooltip", new String[]
         {
             "1|SearchText|text|TRUE", "2|SearchText|TargetItem|FALSE", "3|SearchText|text|FALSE",
             "3|SearchText|TargetItem|TRUE"
         })]
        public void VerifyItemImageTooltip(String SearchText, String TargetItem, String ExpectedValue,
            String TrueOrFalse)
        {
            try
            {
                Initialize();


                IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.SetTextOnly(SearchText);
                Thread.Sleep(3000);
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    try
                    {
                        IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();
                    }
                    catch
                    {
                        IWebElement mArrowDown =
                            this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();
                    }
                    Thread.Sleep(5000);
                }
                list.FindElement();
                IWebElement item = null;

                string xpath_text = "./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" +
                                    TargetItem + "')]/ancestor::li";
                string xpath_datakey = "./descendant::div[contains(@class,'resultListItem')][contains(@data-key,'" +
                                       TargetItem + "')]/ancestor::li";
                string xpath_innertext = "./descendant::div[contains(@class,'resultListItem')][contains(.,'" +
                                         TargetItem + "')]";

                if (list.mElement.FindElements(By.XPath(xpath_text)).Count > 0)
                {
                    item = list.mElement.FindElement(By.XPath(xpath_text));
                }
                else if (list.mElement.FindElements(By.XPath(xpath_datakey)).Count > 0)
                {
                    item = list.mElement.FindElement(By.XPath(xpath_datakey));
                }
                else if (list.mElement.FindElements(By.XPath(xpath_innertext)).Count > 0)
                {
                    item = list.mElement.FindElement(By.XPath(xpath_innertext));
                }
                else
                {
                    throw new Exception("VerifyItemImageTooltip() : Unable to find item [" + TargetItem + "]");
                }

                string ActualValue = item.GetAttribute("title");
                DlkBaseControl mItem = new DlkBaseControl("ComboBox", item);
                mItem.MouseOverOffset(0, 0);

                DlkAssert.AssertEqual("VerifyItemImageTooltip()", ExpectedValue, ActualValue);

            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemTooltip", new String[] {"1|SearchText|TargetItem|FALSE"})]
        public void VerifyItemTooltip(String TargetItem, String ExpectedValue)
        {
            try
            {
                Initialize();
                Boolean bFound = false;               
                DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", ".//ul[contains(@class,'menu')]");
               if (!list.Exists(1))
               {
                   IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                   DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                   ctlArrowDown.ClickUsingJavaScript();
                   Thread.Sleep(5000);
               }

                list.FindElement();
                IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./ul[@class='popupmenuItems']/li/a/span"));
                if (lstItems.Count <= 0)
                {
                    lstItems = list.mElement.FindElements(By.XPath(".//li/a/span"));
                }
                for (int i = 0; i < lstItems.Count; i++)
                {
                    DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));
                    if (item.GetValue() == TargetItem)
                    {
                        item.MouseOver();
                        string ActualValue = item.GetAttributeValue("title").Trim();
                        DlkAssert.AssertEqual("VerifyItemTooltip()", ExpectedValue.Trim(), ActualValue);
                        bFound = true;
                        break;
                    }
                }

               if(!bFound)
                {
                    throw new Exception("VerifyItemTooltip() : Unable to find item [" + TargetItem + "]");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemTooltip() failed : " + e.Message, e);
            }
            finally
            {
                if (!this.IsElementStale())
                    this.ClickUsingJavaScript();
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

        [Keyword("MouseOver")]
        public void MouseOverComboBox()
        {

            this.MouseOver();
        }

        [Keyword("GetItemAtIndex", new String[] { "1|text|Value|TRUE" })]
        public void GetItemAtIndex(String ItemIndex, String VariableName)
        {
            try
            {
                int index;
                IWebElement items = null;
                String ItemIndexText = "";
                String itemsXpath = ""; // this is the xpath we're going to use to find the item in the returned list.
                if (String.IsNullOrWhiteSpace(ItemIndex)) throw new ArgumentException("ItemIndex must not be empty.");
                if (String.IsNullOrWhiteSpace(VariableName))
                    throw new ArgumentException("VariableName must not be empty.");
                if (!Int32.TryParse(ItemIndex, out index))
                    throw new ArgumentException("ItemIndex must be a positive number.");
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        items = GetDDwnSelectDropdownList(out itemsXpath); // <- returns the list in  DDwnSelect
                        ItemIndexText =
                            items.FindElement(
                                    By.XPath(String.Format(".//*[contains(@class,'ddwnListItem')][{0}]", ItemIndex)))
                                .Text.Trim();
                        break;
                    case PopupMenuClass:
                        items = GetPopupSelectDropdownList(out itemsXpath); // <- returns the list in PopupSelect(Item);

                        if (items.FindElements(By.XPath(".//*[contains(@class,'popup')]//li[contains(@class,'popupmenuItem')]")).Count > 0)
                        {
                            ItemIndexText =
                                items.FindElement(By.XPath(String.Format(".//li[contains(@class,'popupmenuItem')][not(contains(@class,'popupmenuSeparator'))]", ItemIndex)))
                                    .Text.Trim();
                        }
                        else
                        {
                            ItemIndexText =
                                items.FindElement(By.XPath(String.Format(".//*[contains(@class,'popup')][{0}]", ItemIndex)))
                                    .Text.Trim();
                        }
                        break;
                    case DDwnColClass:
                        items = GetDDwnColSelectDropdownList(out itemsXpath); //DDwnColSelect(Item);
                        break;
                    case QuickEdit:
                        items = GetQuickEditSelectDropdownList(out itemsXpath); //QuickEditSelect(Item);
                        ItemIndexText =
                            items.FindElement(By.XPath(String.Format("//li[contains(@class,'popup')][{0}]", ItemIndex)))
                                .Text.Trim(); //detached from mElement.
                        break;
                    case FormDdown:
                    case SearchListFluid:
                    case DDwnContainerClass:
                        items = GetFormDDwnSelectDropdownList(out itemsXpath);
                        // FormDDwnSelect(Item);  //Copy Timesheet combo box
                        IWebElement item = items.FindElement(
                                    By.XPath(String.Format("//li[contains(@class,'search-result')][not(contains(@style,'none'))][{0}]", ItemIndex)));
                        ItemIndexText = DlkString.ReplaceCarriageReturn(item.Text, " "); //detached from mElement.
                        break;
                    case SearchClass:
                        items = GetSearchSelectDropdownList(); // <- returns the list in  SearchSelect
                        
                        if(items.FindElements(By.XPath(".//div[contains(@class,'timesheetListPrimary')]")).Count > 0)
                        {
                            ItemIndexText = items.FindElement(
                                    By.XPath(String.Format("(.//*[contains(@class,'timesheetListPrimary')])[{0}]", ItemIndex)))
                                .Text.Trim();
                        }
                        else
                        {
                            ItemIndexText = items.FindElement(
                                    By.XPath(String.Format(".//*[contains(@class,'resultListItem')][{0}]", ItemIndex)))
                                 .Text.Trim();
                        }
                        break;
                }
                DlkVariable.SetVariable(VariableName, ItemIndexText.Replace("\r\n", " "));
                DlkLogger.LogInfo(String.Format("Assigned {0} to {1}", ItemIndexText, VariableName));
            }
            catch (Exception e)
            {
                throw new Exception("GetItemAtIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable")]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                if (String.IsNullOrEmpty(VariableName)) throw new Exception("VariableName must not be empty");

                Initialize();
                String mValue = "";
                switch (mComboBoxType)
                {
                    case CoreFieldClass:
                        mValue = GetValue().TrimEnd();
                        DlkVariable.SetVariable(VariableName, mValue);
                        DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
                        break;
                    case SearchListFluid:
                    case DDwnClass:
                    case DDwnContainerClass:
                    case CoreDdwnField:
                        mValue = new DlkBaseControl("Input", mElement.FindElement(By.TagName("input"))).GetValue();
                        DlkVariable.SetVariable(VariableName, mValue);
                        DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
                        break;
                    default:
                        base.AssignValueToVariable(VariableName);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRequired(String TrueOrFalse)
        {
            Initialize();
            try
            {
                IWebElement mInput = mElement.FindElement(By.XPath(".//input"));
                DlkBaseControl input = new DlkBaseControl("Input", mInput);
                /* also check for parent or parent-of-parent labels for "required" attribute */
                DlkBaseControl labelParent = new DlkBaseControl("Label", mElement.FindElement(By.XPath("..")));
                DlkBaseControl labelGrandParent = new DlkBaseControl("Label", mElement.FindElement(By.XPath("../..")));
                bool hasRequired = (input.GetAttributeValue("class").ToLower().Contains("required") || labelParent.GetAttributeValue("class").ToLower().Contains("required")
                    || labelGrandParent.GetAttributeValue("class").ToLower().Contains("required"));
                DlkAssert.AssertEqual("VerifyRequired() : " + mControlName, Convert.ToBoolean(TrueOrFalse), hasRequired);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRequired() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySearchListNoResults", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySearchListNoResults(String TrueOrFalse)
        {
            try
            {
                Boolean expected = false;
                String noResultsExpectedString = "no results were found";
                // guard clause
                if (!Boolean.TryParse(TrueOrFalse, out expected)) throw new ArgumentException("TrueOrFalse must be a Boolean value");
                FindElement();
                //Initialize();

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
                throw new Exception(String.Format("VerifySearchListNoResults() failed. Unable to find no results class. See error message:\n{0} ", ex.Message));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("VerifySearchListNoResults() failed. {0}", ex.Message));
            }
        }

        [Keyword("VerifyErrorMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyErrorMessage(String ErrorMessage)
        {
            try
            {
                Initialize();
                var errorElm = mElement.FindWebElementCoalesce(By.XPath("./ancestor::div[contains(@class,'core-component')]//*[@class='core-error']")
                   , By.XPath("./ancestor::*[contains(@class,'core_dropdown_field')]//*[contains(@class,'core-error')]")
                   , By.XPath(".//*[@class='core-error']"));

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

        [Keyword("VerifyWarningIndicatorAndTooltip", new String[]
         {
             "1|text|SearchedItem|Posted",
             "2|text|WarningIndicator|Serious Warning",
             "3|text|ToolTipText|Text"
         })]
        public void VerifyWarningIndicatorAndTooltip(String SearchedItem, String WarningIndicator, String ToolTipText)
        {
            try
            {
                Initialize();

                String expectedWarning = "";
                String actualWarning = "";

                if(WarningIndicator.ToLower() == "serious warning")
                {
                    expectedWarning = "seriousWarning";
                }
                else if (WarningIndicator.ToLower() == "warning")
                {
                    expectedWarning = "warning";
                }
                else
                {
                    throw new Exception("VerifyWarningIndicatorAndTooltip() failed: '" + WarningIndicator + "' is not a supported warning type.");
                }

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0 ?
                        this.mElement.FindElement(By.XPath(mSearchArrowXpath)) : this.mElement.FindElement(By.XPath(mSearchArrowXpath2));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }
                list.FindElement();

                IReadOnlyCollection<IWebElement> resList = list.mElement.FindElements(By.TagName("li"));
                foreach (IWebElement item in resList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", item);
                    string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                    if (val.Equals(SearchedItem.Trim()))
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        Thread.Sleep(3000);
                        ctl.MouseOverUsingJavaScript();
                        actualWarning = item.GetAttribute("class");
                        break;
                    }
                }
                DlkBaseControl toolTip = new DlkBaseControl("ToolTip", "CLASS_DISPLAY", "tpd-content-wrapper");
                toolTip.FindElement();
                DlkAssert.AssertEqual("VerifyWarningIndicatorAndTooltip", true, actualWarning.Contains(expectedWarning));
                DlkAssert.AssertEqual("VerifyWarningIndicatorAndTooltip", ToolTipText, DlkString.RemoveCarriageReturn(toolTip.GetValue().Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyWarningIndicatorAndTooltip() failed: " + ex.Message);
            }
        }

        [Keyword("VerifyWarningIndicatorExists", new String[]
         {
             "1|text|SearchedItem|Posted",
             "2|text|WarningIndicatorExists|TRUE"
         })]
        public void VerifyWarningIndicatorExists(String SearchedItem, String TrueOrFalse)
        {
            try
            {
                Initialize();

                String ActualValue = "";
                bool isExisting = false;

                if (!Boolean.TryParse(TrueOrFalse, out isExisting))
                {
                    throw new Exception("VerifyWarningIndicatorExists(): Invalid TrueOrFalse Parameter: " + TrueOrFalse);
                }

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0 ?
                        this.mElement.FindElement(By.XPath(mSearchArrowXpath)) : this.mElement.FindElement(By.XPath(mSearchArrowXpath2));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }
                list.FindElement();

                IReadOnlyCollection<IWebElement> resList = list.mElement.FindElements(By.TagName("li"));
                foreach (IWebElement item in resList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", item);
                    string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                    if (val.Equals(SearchedItem.Trim()))
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        Thread.Sleep(3000);
                        ActualValue = item.GetAttribute("class").ToLower();
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyWarningIndicatorExists", isExisting, ActualValue.Contains("warning"));
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyWarningIndicatorExists() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the list that we are interacting with in FormDDwnSelect. 
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// </summary>
        /// <returns></returns>
        private IWebElement GetFormDDwnSelectDropdownList(out String ItemsXpath, String Value = "")
        {
            DlkBaseControl list = new DlkBaseControl("List", "XPath", "//div[contains(@class,'results-container navigator_ngcrm_popup_overlay')]");
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.ClassName("tap-target"));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();

            // IWebElement itm = list.mElement.FindElement(By.XPath("./descendant::td/div[contains(text(),'" + Value + "')]"));
            ItemsXpath = "./descendant::div/div[text()='" + Value + "']";
            return list.mElement;
        }

        /// <summary>
        /// Returns the list that we are interacting with in QuickEditSelect. Perform the logic that we want with the list inside the KW 
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// </summary>
        /// <returns></returns>
        private IWebElement GetQuickEditSelectDropdownList(out String ItemsXpath, String Value = "")
        {
            try
            {
                string correctClass = mPopupListClass;

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);

                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = mElement.FindElement(By.XPath(".//div//span[contains(@class, 'content')]"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();

                    if (!list.Exists(1))
                    {
                        list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                        correctClass = mPopupListClass2;
                    }
                }

                string xpath_Span = ".//span[normalize-space()='" + Value + "']/../..";
                string xpath_SpanContainer = ".//li[descendant::span[.='" + Value.Trim('.') + "'] and descendant::a[contains(.,'...')]]";

                if (CheckElementExists(list.mElement, xpath_Span))
                {
                    ItemsXpath = xpath_Span;
                    //item = list.mElement.FindElement(By.XPath(xpath_Span));
                }
                else if (CheckElementExists(list.mElement, xpath_SpanContainer))
                {
                    ItemsXpath = xpath_SpanContainer;
                    //item = list.mElement.FindElement(By.XPath(xpath_SpanContainer));
                }
                else
                {
                    /////
                    //IWebElement mArrowDown = mElement.FindElement(By.XPath(".//div//span[contains(@class, 'content')]"));
                    //DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    //ctlArrowDown.Click();
                    //need to re-define the List as it defined a list that was initially opened upon login to the page
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", correctClass);
                    list.FindElement();
                    ItemsXpath = ".//span[normalize-space()='" + Value + "']/../..";
                }

                return list.mElement;
            }
            catch (Exception ex)
            {
                throw new Exception("QuickEditSelect(): Unable to find the option selected. ", ex);
            }
        }

        /// <summary>
        /// Returns the list that we are interacting with in DDwnColSelect. Perform the logic that we want with the list inside the KW 
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// </summary>
        /// <returns></returns>
        private IWebElement GetDDwnColSelectDropdownList(out String ItemsXpath, String Value = "")
        {
            DlkBaseControl list;

            list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass); //"ddwnTableDiv"
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            if (GetAttributeValue("class").Contains(mDDwnSearchColClass))  //"search_list"
            {
                // Click();
                IWebElement mInput = this.mElement.FindElement(By.CssSelector(mDDwnColInputCSS));  //"input"
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.SetTextOnly(Value);

                list = new DlkBaseControl("List", "CLASS_DISPLAY", mDDwnSearchColListClass);  //"results"
                if (list.Exists(1))
                {
                    ItemsXpath = "./descendant::li/div[contains(.,'" + Value + "')]";
                }
                else
                {
                    ItemsXpath = "";
                }
            }
            else
            {
                list.FindElement();
                ItemsXpath = "./descendant::td/div[contains(text(),'" + Value + "')]";
            }
            return list.mElement;
        }

        /// <summary>
        /// Returns the list that we are interacting with in DDwnSet. Perform the logic that we want with the list inside the KW
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private IWebElement GetDDwnSetDropdownList(String Value = "")
        {
            IWebElement mInput = this.mElement.FindElement(By.CssSelector(mInputCSS)); //"input"
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.ShowAutoComplete(Value);
            DlkBaseControl list = null;

            try
            {
                list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                list.FindElement();
                IWebElement noResults = list.mElement.FindElement(By.ClassName("no-results"));
                if (noResults.Displayed)
                {
                    mInput.Clear();
                    throw new Exception("Search value [" + Value + "not in the list.");
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Couldn't find control"))
                {
                    throw new Exception("Search value [" + Value + "not found.");
                }
            }
            return list.mElement;
        }

        /// <summary>
        /// Returns the list that we are interacting with in SearchSelect. Perform the logic that we want with the list inside the KW
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private IWebElement GetSearchSelectDropdownList()
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);  //"results"
            if (!list.Exists(1))
            {
                IWebElement mArrowDown;
                if(this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Count > 0)
                {
                   mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                }
                else
                {
                    mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath2));
                }
                
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click(5, 5);
                Thread.Sleep(5000);
            }

            list.FindElement();
            return list.mElement;
        }

        /// <summary>
        /// Returns the list that we are interacting with in PopupSelect. Perform the logic that we want with the list inside the KW
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// </summary>
        private IWebElement GetPopupSelectDropdownList(out String ItemsXpath, String Value = "")
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);

            Action<DlkBaseControl> ClickDDarrow = (control) =>
            {
                //click dropdown arrow 
                if (!control.Exists(1))
                {
                    this.MouseOver();
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
                    ctlArrowDown.Click();
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
                ItemsXpath = ".//*[normalize-space(text())='" + Value + "']";
                return list.mElement;
            }
            catch (Exception e)
            {
                throw new Exception("PopUpSelect() failed : " + e.Message);
            }
        }

        /// <summary>
        /// Returns the list that we are interacting with in DDwnSelect. Perform the logic that we want with the list inside the KW
        /// Perform the logic that we want with the said list in the Keyword that invokes this method.
        /// <param name="ItemsXpath">The out paramter is the XPath to locate the specific item in the returned list</param>
        /// </summary>
        private IWebElement GetDDwnSelectDropdownList(out String ItemsXpath, String Value = "")
        {
            DlkBaseControl list;
            DlkBaseControl el = new DlkBaseControl("ComboBox", mElement);
            el.ScrollIntoViewUsingJavaScript();

            IWebElement mArrowDown = null;
            if (this.mElement.FindElements(By.XPath(mDDwnArrowDownXPATH)).Where(dropdown => dropdown.Displayed).ToList().Count > 0)
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            }
            else // add if statements when there are new dropdown types
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
            }

            // add if necessary
            if (mArrowDown.GetAttribute("class") == "progress-area")
            {
                mArrowDown = this.mElement.FindElement(By.CssSelector("button"));
                list = new DlkBaseControl("List", "XPATH_DISPLAY", ".//*[@class='search-list']");
                ItemsXpath = "./descendant::li/div[contains(.,'" + Value + "')]/..";
            }
            else if (mArrowDown.GetAttribute("class") == "tap-target")
            {
                list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                ItemsXpath = "./descendant::li/div[contains(.,'" + Value + "')]/..";
            }
            else
            {
                list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
                ItemsXpath = "./descendant::td/div[normalize-space()='" + Value + "']";
            }

            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!mArrowDown.FindElement(By.XPath("..")).GetAttribute("class").ToLower().Contains("openedbelow"))
            {
                ctlArrowDown.ClickUsingJavaScript();
            }

            list.FindElement();
            return list.mElement;
        }

        [Keyword("ClickComboBoxButton")]
        public void ClickComboBoxButton(String ButtonName)
        {
            try
            {
                Initialize();
                IWebElement ctlBtn = null;
                switch (ButtonName.ToLower())
                {
                    case "clear":
                        ctlBtn = mElement.FindElement(By.XPath(".//span[contains(@class,'clear-icon')]"));
                        break;
                    case "search":
                        //ctlBtn = mElement.FindElement(By.CssSelector("input::before")); //pseudoelements not found 
                        ctlBtn = mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
                        break;
                    default:
                        throw new Exception("Button name " + ButtonName + " not recognized");
                    //break; to remove 'unreachable code detected' warning
                }

                (new DlkBaseControl("ComboBox Button", ctlBtn)).Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickComboBoxButton() failed : " + e.Message, e);
            }
        }

        [Keyword("HoverItem", new String[] { "1|text|Value|Item1" })]
        public void HoverItem(String Item)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Item)) throw new Exception("Item must not be empty");
                
                Item = Item.Trim();
                Initialize();
                
                switch (mComboBoxType)
                {
                    case PopupMenuClass:
                        PopupHoverItem(Item);
                        break;
                    default:
                        throw new Exception("Keyword does not support the specified combobox.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("HoverItem() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private Functions
        private void DDwnSelect(String Value)
        {                                                         
            DlkStormWebFunctionHandler.WaitScreenGetsReady();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            bool bFound = false;

            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));             
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
                DlkStormWebFunctionHandler.WaitScreenGetsReady();
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

        private void DropDownSelect(String Value)  //Database field on Login screen
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", dropdownListClass);
            bool bFound = false;

            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDropDownArrowDownXPATH));

                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
                DlkStormWebFunctionHandler.WaitScreenGetsReady();
            }

            list.FindElement();

            IReadOnlyCollection<IWebElement> ddList = list.mElement.FindElements(By.XPath("./li"));
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

        private void ModeChooserSelect(String Value)
        {

            bool bFound = false;

            //Click element to display list contents
            this.mElement.Click();

            IReadOnlyCollection<IWebElement> ddList = this.mElement.FindElements(By.XPath(".//option"));
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

        private void FormDDwnSelect(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "XPath", "//div[@class='results-container navigator_ngcrm_popup_overlay']");
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.ClassName("tap-target"));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();

            // IWebElement itm = list.mElement.FindElement(By.XPath("./descendant::td/div[contains(text(),'" + Value + "')]"));
            IWebElement itm = list.mElement.FindElement(By.XPath("./descendant::div/div[text()='" + Value + "']"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", itm);
            ctlItem.Click();
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
            ctlArrowDown.ClickUsingJavaScript(); //close the list
        }

        private void DropDownAvailableInList(String ItemValue, String ExpectedValue) //Database field on Login screen
        {
            Boolean ActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", dropdownListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDropDownArrowDownXPATH));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown); //open dropdown
            if (!list.Exists(1))
            {
                ctlArrowDown.ClickUsingJavaScript();
            }

            list.FindElement();
            var ctl = list.mElement.FindElements(By.XPath("./li[contains(text(),'" + ItemValue + "')]"));
            if (ctl.Count > 0)
            {
                ActualValue = true;
            }
            DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), ActualValue);
            ctlArrowDown.ClickUsingJavaScript(); //close the list
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

        private void DDwnColVerifyValue(String ExpectedValue)
        {
            IWebElement mInput = this.mElement.FindElement(By.CssSelector(mDDwnColInputCSS));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.VerifyText(ExpectedValue);

        }

        private string DDwnColGetValue()
        {
            IWebElement mInput = this.mElement.FindElement(By.CssSelector(mDDwnColInputCSS));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            return txtInput.GetValue();

        }

        private void DDwnColSelect(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            bool bFound = false;

            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
            }

            list.FindElement();

            IReadOnlyCollection<IWebElement> ddList = (list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Count > 0) ?
                              list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")) : list.mElement.FindElements(By.XPath(".//td/div"));
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

        private void DDwnColAvailableInList(String ItemValue, String ExpectedValue)
        {
            Boolean ActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.ClickUsingJavaScript();
            }

            list.FindElement();
            var ctl = list.mElement.FindElements(By.XPath(".//tr[@class='ddwnListItem']/td/div[contains(text(),'" + ItemValue + "')]"));
            if (ctl.Count > 0)
            {
                ActualValue = true;
            }
            DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), ActualValue);
            ctlArrowDown.ClickUsingJavaScript(); //close the list
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
                DlkBaseControl ddwnItem = new DlkBaseControl("DropdownItem",ddwnValue);
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

        private void PopupSelect(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);

            Action<DlkBaseControl> ClickDDarrow = (control) =>
            {
                //click dropdown arrow 
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

        private void QuickEditSelect(String Value = "")  //modified because of Reporting in CRM (when too many records to display)
        {
            try
            {
                string correctClass = mPopupListClass;

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);

                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = mElement.FindElement(By.XPath(".//div//span[contains(@class, 'content')]"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();

                    if (!list.Exists(1))
                    {
                        list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                        correctClass = mPopupListClass2;
                    }
                }

                IWebElement item = null;

                string xpath_Span = ".//span[normalize-space()='" + Value + "']/../..";
                string xpath_SpanContainer = ".//li[descendant::span[.='" + Value.Trim('.') + "'] and descendant::a[contains(.,'...')]]";

                if (CheckElementExists(list.mElement, xpath_Span))
                {
                    item = list.mElement.FindElement(By.XPath(xpath_Span));
                }
                else if (CheckElementExists(list.mElement, xpath_SpanContainer))
                {
                    item = list.mElement.FindElement(By.XPath(xpath_SpanContainer));
                }
                else
                {
                    /////
                    //IWebElement mArrowDown = mElement.FindElement(By.XPath(".//div//span[contains(@class, 'content')]"));
                    //DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    //ctlArrowDown.Click();
                    //need to re-define the List as it defined a list that was initially opened upon login to the page
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", correctClass);
                    list.FindElement();
                    item = list.mElement.FindElement(By.XPath(".//span[normalize-space()='" + Value + "']/../.."));
                }

                DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
                ctlItem.Click();
            }
            catch (Exception ex)
            {
                throw new Exception("QuickEditSelect(): Unable to find the option selected. ", ex);
            }
        }

        private void SelectOptionSelect(String Value)
        {

            mobjSelectElement = new SelectElement(mElement);
            mobjSelectElement.SelectByText(Value);
        }

        private void PopupAvailableInList(String ItemValue, String ExpectedValue)
        {
            Boolean ActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.ClickUsingJavaScript();

                if (!list.Exists(1))
                {
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                }
            }

            list.FindElement();
            if (list.mElement.FindElements(By.XPath(".//*[normalize-space()='" + ItemValue + "']")).Where(x => x.Displayed).Count() > 0)
            {
                ActualValue = true;
            }
            DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), ActualValue);
            ctlArrowDown.ClickUsingJavaScript(); //close the list
        }

        private void PopupItemEnabled(String ItemValue, String ExpectedValue)
        {
            Boolean bActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
            }

            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./ul[@class='popupmenuItems']/li/a/span"));
            if (lstItems.Count <= 0)
            {
                lstItems = list.mElement.FindElements(By.XPath(".//li/a/span"));
            }
            for (int i = 0; i < lstItems.Count; i++)
            {
                DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));
                if (item.GetValue() == ItemValue)
                {
                    String sDisabled = item.FindParentByTagName("li").GetAttribute("class");
                    if (!sDisabled.Contains("disabled"))
                    {
                        bActualValue = true;
                    }
                    else
                    {
                        bActualValue = false;
                    }
                    break;
                }
            }
            DlkAssert.AssertEqual("PopupItemDisabled", Convert.ToBoolean(ExpectedValue), bActualValue);
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

        private void PopupVerifyListCount(String ExpectedCount)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            DlkBaseControl ctlArrowDown = null;
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();

                if (!list.Exists(1))
                {
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                    list.FindElement();
                }
            }

            var lstItems = list.mElement.FindElements(By.XPath(".//li[not(contains(@class,'divider'))]")).Where(item => item.Displayed).ToList();
            DlkAssert.AssertEqual("VerifyListCount", Convert.ToInt32(ExpectedCount), lstItems.Count());
            ctlArrowDown.Click(); //close the list
        }

        private void PopupHoverItem(String Item)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);

            Action<DlkBaseControl> ClickDDarrow = (control) =>
            {
                //click dropdown arrow 
                if (!control.Exists(1))
                {
                    this.MouseOver();
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
                    ctlArrowDown.Click();
                }
            };

            ClickDDarrow(list);
            if (!list.Exists(1))
            {
                list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass2);
                ClickDDarrow(list);
            }

            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath(".//*[normalize-space()='" + Item + "']"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
            ctlItem.MouseOver();
        }

        private void SearchSelect(String Value)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath)); //".//input[contains(@class,'search-input')]";
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            //   txtInput.ShowAutoComplete(Value);
            if(txtInput.IsReadOnly().Equals(Boolean.FalseString, StringComparison.InvariantCultureIgnoreCase))
            {
                txtInput.SetTextOnly(Value);
            }
            
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);  //"results"
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click(5, 5);
                Thread.Sleep(5000);
            }

            list.FindElement();
            IWebElement item = null;
            try
            {
                item = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + Value + "')]"));
            }
            catch
            {
                try
                {
                    item = list.mElement.FindElement(By.XPath("./descendant::div[contains(@class,'resultListItem')]//div[contains(text(),'" + Value + "')]"));
                }
                catch
                {
                    // Select fails, there was probably a change in the class
                    item = list.mElement.FindElement(By.XPath("./descendant::div[contains(@class,'results-container')]//div[contains(text(),'" + Value + "')]"));
                }
            }
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
            ctlItem.Click();
            // ctlItem.ClickUsingJavaScript();

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
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActualValues.ToLower(),true);
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActualValues,false);
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
                    TryClickElement(new DlkBaseControl ("List" ,mElement));
            }
        }

        private void SearchVerifyListCount(String ExpectedCount)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));           
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
                Thread.Sleep(5000);
            }

            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./descendant::li[contains(@class,'search-result')]"));
            if(lstItems.Count == 0){
                lstItems = list.mElement.FindElements(By.XPath("./descendant::div[contains(@class,'resultListItem')]"));
            }
            DlkAssert.AssertEqual("VerifyListCount", Convert.ToInt32(ExpectedCount), lstItems.Count());
       }

        private void SearchListSelect(String Value)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.ShowAutoComplete(Value);
            Thread.Sleep(3000);
            String sAddEnabled = txtInput.GetAttributeValue("add-enabled");
            if (sAddEnabled != null)
            {


                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }

                list.FindElement();
                IWebElement item = null;
                try
                {
                    item = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + Value + "')]"));
                }
                catch
                {
                    item = list.mElement.FindElement(By.XPath("./descendant::div[contains(@class,'resultListItem')]//div[contains(text(),'" + Value + "')]"));
                }
                DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
                //ctlItem.Click();
                ctlItem.ClickUsingJavaScript();
            }

        }

        private void SearchAvailableInList(String Value, String ExpectedValue)
        {
            IWebElement mInput = mElement.FindElement(By.XPath(mSearchInputXPath));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            //txtInput.ShowAutoComplete(Value);
            txtInput.SetTextOnly(Value);
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            IWebElement mArrowDown = null;
            if (mElement.FindElements(By.XPath(mSearchArrowXpath)).Where(elem => elem.Displayed).ToList().Count > 0)
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
            }
            else if (mElement.FindElements(By.XPath(mSearchArrowXpath2)).Where(elem => elem.Displayed).ToList().Count > 0)
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath2));
            }
             
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.ClickUsingJavaScript();
                Thread.Sleep(5000);
            }

            if ((list.GetValue() == "No results were found") && (ExpectedValue.ToLower() == "false"))
            {
                list.VerifyExists(Convert.ToBoolean("True"));
                return;
            }

            list.FindElement();
            IWebElement item = null;
            try
            {
                item = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + Value + "')]"));

            }
            catch
            {
                item = list.mElement.FindElement(By.XPath("./descendant::div[contains(@class,'resultListItem')]//div[contains(text(),'" + Value + "')]"));
            }
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
            ctlItem.VerifyExists(Convert.ToBoolean(ExpectedValue));
            ctlArrowDown.ClickUsingJavaScript(); //close the list
        }

        private void ScrollVertical(int iPixels)
        {
            if (DlkEnvironment.mBrowser.ToLower() == "chrome")
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("$('#" + mSearchValues[0] + "').animate({scrollTop: '+=" + iPixels + "'});");
            }
            else
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("window.scrollBy(0, " + iPixels.ToString() + ");");
            }
            Thread.Sleep(5000);
        }

        private void DDwnSet(String Value)
        {
            IWebElement mInput = mElement.FindElements(By.CssSelector(mInputCSS)).Where(x => x.Displayed).FirstOrDefault(); //"input"
            if (string.IsNullOrEmpty(Value))
            {
                Value = Keys.Backspace;
            }
            mInput.Clear();
            mInput.SendKeys(Value);
            Thread.Sleep(3000);

            try
            {
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                list.FindElement();
                IWebElement noResults = list.mElement.FindElement(By.ClassName("no-results"));
                if (noResults.Displayed)
                {
                    mInput.Clear();
                    throw new Exception("Search value [" + Value + "not in the list.");
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Couldn't find control")) 
                    throw new Exception("Search value [" + Value + "not found.");
            }
        }

        private void PopupSet(String Value)
        {
            DlkLogger.LogInfo("Set() not supported for 'Popup Menu' Combo Box.");
        }

        private void SearchSet(String Value)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));  //".//input[contains(@class,'search-input')]"
            DlkTextBox inputBox = new DlkTextBox("TextBox", mInput);
            inputBox.FocusUsingJavaScript();
            inputBox.ClearField();
            inputBox.Set(Value);
            Thread.Sleep(3000);
        }

        private void SearchSetTextOnly(String Value)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));  //".//input[contains(@class,'search-input')]"
            DlkTextBox inputBox = new DlkTextBox("TextBox", mInput);
            inputBox.FocusUsingJavaScript();
            inputBox.ClearField();
            inputBox.SetTextOnly(Value);
            Thread.Sleep(3000);
        }

        private void SetTextOnly(String Value)
        {
            IWebElement mInput = this.mElement.FindElements(By.CssSelector(mInputCSS)).Where(x => x.Displayed).FirstOrDefault(); //"input"
            if (string.IsNullOrEmpty(Value))
            {
                Value = Keys.Backspace;
            }
            mInput.SendKeys(Keys.Control + "a");
            Thread.Sleep(100);
            mInput.SendKeys(Keys.Delete);

            //Requested to be changed to Ctrl+A then Delete keys instead of using Selenium Clear
            //mInput.Clear();

            mInput.SendKeys(Value);
            Thread.Sleep(3000);
            DlkLogger.LogInfo("Successfully executed SetText()");
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
            else if(mInput.GetAttribute("class").Contains("date-time"))
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

        private bool CheckElementExists(IWebElement mElement, String xpath)
        {
            try
            {
                if (mElement.FindElements(By.XPath(xpath)).Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
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
                Thread.Sleep(5000);
            }
            catch 
            { 
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
                    bElement.Click(0,0);
            }
        }

        private void SearchSelectWithoutSearch(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "XPATH_DISPLAY", mSearchListXPath);

            Boolean bFound = false;
            try
            {
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

            }
            catch { }

            if (list != null)
            {
                IList<IWebElement> ddList = list.mElement.FindWebElementsCoalesce(false, By.XPath(".//li[contains(@class,'search-result')]"), By.XPath(".//div[contains(@class,'resultListItem')]"));

                foreach (IWebElement item in ddList)
                {
                    if (item.Displayed)
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
                }
            }

            if (!bFound)
            {
                throw new Exception("Unable to find item in the list.");
            }          

        }

        private void SearchVerifyAvailableWithoutSearch(String Item, String ExpectedValue)
        {
            bool actualResult = false;

            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            IWebElement mArrowDown = null;
            if (this.mElement.FindElements(By.XPath(mSearchArrowXpath)).Where(dropdown => dropdown.Displayed).ToList().Count > 0)
	        {
                mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
            }
            else if (this.mElement.FindElements(By.XPath(mSearchArrowXpath2)).Where(dropdown => dropdown.Displayed).ToList().Count > 0)
            {
                mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath2));                
            }
           
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.Click();
                Thread.Sleep(5000);
            }

            if ((list.GetValue() == "No results were found") && (ExpectedValue.ToLower() == "false"))
            {
                list.VerifyExists(Convert.ToBoolean("True"));
                return;
            }

            IReadOnlyCollection<IWebElement> ddList = (list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Count > 0) ?
                  list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")) : list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]"));

            foreach (IWebElement item in ddList)
            {
                DlkBaseControl ctl = new DlkBaseControl("List", item);
                string val = item.FindElements(By.ClassName("search-info")).Count > 0 ?
                             item.FindElement(By.ClassName("search-info")).Text : ctl.GetValue();

                val = DlkString.RemoveCarriageReturn(val.Trim());

                if (val.Equals(Item.Trim()))
                {
                    actualResult = true;
                    break;
                }
            }

            DlkAssert.AssertEqual("VerifyAvailableInListWithoutSearch", Convert.ToBoolean(ExpectedValue), actualResult);
            ctlArrowDown.ClickUsingJavaScript();
        }
        
        private void CoreFieldSelect(String Item)
        {
            try
            {
                //IWebElement mArrow = null;
                //if (this.mElement.FindElements(By.XPath(mCoreFieldDdwnArrowXPATH)).Count > 0)
                //{
                //    mArrow = this.mElement.FindElement(By.XPath(mCoreFieldDdwnArrowXPATH));
                //}
                //else
                //{
                //    mArrow = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                //}
                
                Action<DlkBaseControl> ClickDDarrow = (control) =>
                {
                    //click dropdown arrow 
                    if (!control.Exists(1))
                    {
                        IWebElement mArrowDown = null;
                        mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                        mArrowDown.Click();
                    }
                };

                DlkBaseControl list = new DlkBaseControl("List", "xpath_display", mCoreFieldColListXPATH);
                if (!list.Exists(1))
                {
                    ClickDDarrow(list);
                }

                list.FindElement();
                IWebElement mItem = list.mElement.FindElement(By.XPath(".//*[normalize-space()='" + Item + "']"));
                mItem.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message);
            }
        }
        private void CoreFieldAvailableInList(String Item, String ExpectedValue)
        {
            Boolean ActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
            IWebElement input = this.mElement.FindElement(By.XPath("..//input[contains(@class,'dropdown-field')]"));
            if (!list.Exists(1))
            {
                mArrowDown.Click();
                Thread.Sleep(5000);
            }
            list.FindElement();
            IReadOnlyCollection<IWebElement> ddList = (list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")).Count > 0) ?
                list.mElement.FindElements(By.XPath(".//li[contains(@class,'search-result')]")) : list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]"));

            foreach (IWebElement item in ddList.Where(x => x.Displayed))
            {
                DlkBaseControl ctl = new DlkBaseControl("List", item);
                string val = item.FindElements(By.ClassName("search-info")).Count > 0 ?
                    item.FindElement(By.ClassName("search-info")).Text : ctl.GetValue();

                val = DlkString.RemoveCarriageReturn(val.Trim());

                if (val.Equals(Item.Trim()))
                {
                    ActualValue = true;
                    break;
                }
            }

            DlkAssert.AssertEqual("VerifyAvailableInList(): ", Convert.ToBoolean(ExpectedValue), ActualValue);
            input.SendKeys(Keys.Escape);
            input.SendKeys(Keys.Tab);
        }

       /// <summary>
       /// Clear a field without using the X icon
       /// </summary>
       /// <param name="Field">Field to be cleared</param>
        private void ClearField(IWebElement Field)
        {
            try
            {
                if(DlkEnvironment.mBrowser != "safari")
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


        #endregion

    }
}
