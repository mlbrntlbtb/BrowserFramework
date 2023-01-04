using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace ngCRMLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        #region Constants
        private const String DDwnClass = "ddwnInput";  //regular drop-down
        private const String DDwnColClass = "ddwnCol"; //regular drop-down in the table
       // private const String PopupMenuClass = "navigator_popupmenu";
        private const String PopupMenuClass = "popupmenu";
        private const String SearchClass = "search-input-container";
        private const String SearchListFluid = "navigator_ngcrm_search_list fluid"; //Search List
        private const String SearchList = "navigator_ngcrm_search_list"; //Search List
        private const String SearchColList = "ddwnCol navigator_ngcrm_search_list"; //Search List in a table
        private const String QuickEdit = "navigator_ngcrm_quick_edit";
        private const String FormDdown = "formDdwn navigator_ngcrm_search_list"; //Copy Timesheet
        private const String SelectOption = "";
        #endregion

        #region Private Variables
        private SelectElement mobjSelectElement;

        private String mComboBoxType = "";

        //DDwn Controls
        private String mInputCSS = "input";
        private String ddwnInputListClass = "ddwnTableDiv";
        private String mArrowDownCSS = "div>div";
        //private String mArrowDownXPath = "//div[contains(@class,'ddwnArrow')]";

        //Popup Controls
        private String mPopupInputXPath = "./span[1]";
        private String mPopupInputXPath2 = "./span[2]";
        private String mPopupArrowXPath = ".//span[contains(@class,'arrow')]";
        private String mPopupListClass = "popupmenuBody";

        //Search Controls
        private String mSearchInputXPath = ".//input[contains(@class,'search-input')]";
        private String mSearchArrowXpath = ".//*[contains(@class,'show-results')]";
      //  private String mSearchArrowXpath = ".//span[contains(@class,'tap-target')]"; 
        private String mSearchListClass = "results";

        //ddwnCol Controls (Cell ComboBox)
        private String mDDwnColInputCSS = "input";
        private String mDDwnArrowDownXPATH = ".//button[contains(@class,'ddwnArrow')]";
        private String mDDwnSearchColClass = "search_list";
        private String mDDwnSearchColListClass = "results";

        private Boolean VerifyAfterSelect = true;

        //Used for auto correct
        private String mTentativeComboBoxType = "";
        #endregion

        #region Constructors
        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String SearchValue, Boolean VerifyAfterSelect)
            : base(ControlName, SearchType, SearchValue)
        {
            this.VerifyAfterSelect = VerifyAfterSelect;
        }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        //public DlkComboBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }

        #endregion

        #region Public Functions
        /// <summary>
        /// Always call this for every keyword
        /// </summary>
        public void Initialize()
        {

            FindElement();
            //if (DlkEnvironment.mBrowser.ToLower() != "ie")
            //{
            //    this.ScrollIntoViewUsingJavaScript();
            //}
                
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
            else if (mClass.Contains(PopupMenuClass)) //"navigator_popupmenu"
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
            else if ((mClass.Contains(SearchList)) && (!mClass.Contains("fluid"))) //"navigator_ngcrm_search_list"; //Search List
            {
                mComboBoxType = SearchList;
            }
            else if (mClass.Contains(QuickEdit)) //"navigator_ngcrm_quick_edit"
            {
                mComboBoxType = QuickEdit;
            }
            else if ((mClass == "") || (mClass == "ui-widget-content")) //new language selection list
            {
                mComboBoxType = SelectOption;
            }
            else if (mClass.Contains("standard-select")) //new Configuration Security table
            {
                mComboBoxType = SelectOption;
            }
            else if (mClass.Contains("edit")) //booking column for CRM
            {
                mComboBoxType = DDwnColClass;
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
            switch(mElement.TagName)
            {
                case "div":
                    mClass = GetAttributeValue("class").ToLower();
                    if(mClass.Contains("ddwnarrow") || mClass.Contains("ddwninput"))
                    {
                        mTentativeComboBoxType = DDwnClass;
                        return true;
                    }
                    else if(mClass.Contains("popupmenu"))
                    {
                        mTentativeComboBoxType = PopupMenuClass;
                        return true;
                    }
                    else if(mClass.Contains("search-input-container"))
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
                    if(mClass.Contains("ddwnfilter"))
                    {
                        mTentativeComboBoxType = DDwnClass;
                        return true;
                    }                    
                    else if(mClass.Contains("search-input"))
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
                    if(mClass.Contains("filter"))
                    {
                        mTentativeComboBoxType = PopupMenuClass;
                        return true;
                    }
                    else if(mClass.Contains("show-results"))
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
                        IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[@class='search-input-container']"));
                        mTentativeComboBoxType = SearchClass;
                        return true;
                    }
                    catch(OpenQA.Selenium.NoSuchElementException)
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
                        switch(mTag)
                        {
                            case "div":
                                if (mClass.Contains("ddwnarrow"))
                                {
                                    IWebElement parentDDwn = mElement.FindElement(By.XPath("./ancestor::div[@class='ddwnInput']"));
                                    mCorrectControl = new DlkBaseControl("CorrectControl", parentDDwn);
                                    mAutoCorrect = true;                                
                                }
                                break;
                            case "input":
                                if(mClass.Contains("ddwnfilter"))
                                {
                                    IWebElement parentDDwn = mElement.FindElement(By.XPath("./ancestor::div[@class='ddwnInput']"));
                                    mCorrectControl = new DlkBaseControl("CorrectControl", parentDDwn);
                                    mAutoCorrect = true;     
                                }
                                break;
                        }
                        break;

                    case PopupMenuClass:
                        if(mTag == "span" && mClass.Contains("filter"))
                        {
                            IWebElement parentPopup = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'navigator_popupmenu')]"));
                            mCorrectControl = new DlkBaseControl("CorrectControl", parentPopup);
                            mAutoCorrect = true;
                        }
                        break;

                    case SearchClass:
                        IWebElement parentSearch = mElement.FindElement(By.XPath("./ancestor::div[@class='search-input-container']"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentSearch);
                        mAutoCorrect = true;
                        break;
                }

                if(mAutoCorrect)
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
            Initialize();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");            
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            ctlArrowDown.Click();
            Thread.Sleep(3000);

            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath("./descendant::span[contains(text(),'" + Value + "')]"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);

            ctlItem.Click();
            // ctlItem.ClickUsingJavaScript();

        }

        public void VerifyTableLinkExists(String ExpectedValue)
        {
           // Initialize();
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
           Thread.Sleep(3000);
            ctlArrowDown.Click();
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");
            list.VerifyExists(Convert.ToBoolean(ExpectedValue));
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
        #endregion

        #region Keywords
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnSelect(Item);
                        break;
                    case PopupMenuClass:
                        PopupSelect(Item);
                        break;
                    case SearchClass:
                        SearchSelect(Item);
                      //  DDwnSet(Item);
                        break;
                    case SearchListFluid:
                        DDwnSet(Item);
                        break;
                    case SearchList:
                        DDwnSelect(Item);
                        break;
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
                        SelectOptionSelect(Item);  //new language selection list
                        break;
                    case FormDdown:
                        FormDDwnSelect(Item);  //Copy Timesheet combo box
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e); 
            }
        }

        [Keyword("SelectItemInListWithoutSearch", new String[] { "1|text|Value|" })]
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

        [Keyword("ClickLink", new String[] { "1|text|Value" })]
        public void ClickLink(String Value)
        {
            Initialize();
            this.Click();
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "add-new");
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));    //".//span[contains(@class,'show-results')]";        
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);           
            ctlArrowDown.Click();            
            Thread.Sleep(3000);                     
            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath("./descendant::span[contains(text(),'" + Value + "')]"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);

            ctlItem.Click();
           // ctlItem.ClickUsingJavaScript();

        }

        [Keyword("VerifyLinkExists", new String[] { "1|text|Expected Value|TRUE",})]
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

        [Keyword("Set", new String[] { "1|text|Value|" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnSet(Value);
                        break;
                    case PopupMenuClass:
                        PopupSet(Value);
                        break;
                    case SearchClass:
                        SearchSet(Value);
                        break;
                    case SearchListFluid:
                        DDwnSet(Value);
                        break;
                    case SearchList:
                        DDwnSet(Value);
                        break;
                    case SearchColList:
                        DDwnSet(Value);
                        break;
                    
                }

            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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
                        DDwnVerifyValue(ExpectedValue);
                        break;
                    case PopupMenuClass:
                        PopupVerifyValue(ExpectedValue);
                        break;
                    case SearchClass:
                        SearchVerifyValue(ExpectedValue);
                        break;
                    case DDwnColClass:
                        DDwnColVerifyValue(ExpectedValue);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();                
                if ((mComboBoxType == PopupMenuClass) || (mComboBoxType ==QuickEdit))
                {
                    String sValue = mElement.GetAttribute("class");                   
                        if (!sValue.Contains("editable"))
                        {
                            sValue = "true";
                        }
                        else
                        {
                            sValue = "false";
                        }

                        DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(sValue));
                }
                else                   
                    {
                        IWebElement mInput = this.mElement.FindElement(By.CssSelector(mInputCSS));
                        DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                        txtInput.VerifyReadOnly(TrueOrFalse);
                    }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|Sample item",
                                                         "2|text|Expected Value|TRUE"})]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnAvailableInList(Item, TrueOrFalse);
                        break;
                    case PopupMenuClass:
                        PopupAvailableInList(Item, TrueOrFalse);
                        break;
                    case DDwnColClass:
                        DDwnColAvailableInList(Item, TrueOrFalse);
                        break;
                    case SearchClass:
                        SearchAvailableInList(Item, TrueOrFalse);
                        break;
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
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
                Initialize();

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

        [Keyword("VerifyListItemEnabled", new String[] { "1|text|Item|Sample item",
                                                         "2|text|Expected Value|TRUE"})]
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
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyList(String Items)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Items)) throw new Exception("Items must not be empty");
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnVerifyList(Items);
                        break;
                    case PopupMenuClass:
                    case QuickEdit:
                        PopupVerifyList(Items);
                        break;
                    case DDwnColClass:
                        DDwnColVerifyList(Items);
                        break;
                    case SearchClass:
                        SearchVerifyList(Items);
                        break;
                    default:
                        throw new Exception("Unsupported list type.");
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyPartialList(String Items)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Items)) throw new Exception("Items must not be empty");
                Initialize();
                switch (mComboBoxType)
                {
                    case DDwnClass:
                        DDwnVerifyList(Items,true);
                        break;
                    case PopupMenuClass:
                        PopupVerifyList(Items, true);
                        break;
                    case DDwnColClass:
                        DDwnColVerifyList(Items, true); // this method actually doesn't do anything..
                        break;
                    case SearchClass:
                        SearchVerifyList(Items, true);
                        break;
                    default:
                        throw new Exception("Unsupported list type.");
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
                        PopupVerifyListCount(ExpectedCount);
                        break;
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

        [Keyword("SearchItemExists", new String[] { "1|text|SearchedItem|ItemValue", 
                                                             "2|text|VariableName|TrueFalse" })]        
        public void SearchItemExists(String SearchedItem, String VariableName)
        {           
            Initialize();
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
           // txtInput.ShowAutoComplete(SearchedItem);
            txtInput.Set(SearchedItem);
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            list.FindElement();
            IReadOnlyCollection<IWebElement> ctl = list.mElement.FindElements(By.XPath(".//div[contains(@class,'search-info')]//div[contains(text(),'" + SearchedItem + "')]"));
            if (ctl.Count > 0)
            {
                DlkVariable.SetVariable(VariableName, true.ToString());
                DlkLogger.LogInfo("Item found in list - assigned the value of TRUE to variable");
            }
            else
            {
                DlkVariable.SetVariable(VariableName, false.ToString());
                DlkLogger.LogInfo("Item not found in list - assigned the value of FALSE to variable");
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
            }

            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath(".//span[text()='" + Value + "']/../.."));
            DlkBaseControl FilterItem = new DlkBaseControl("ArrowDown", item);
            FilterItem.MouseOver();

            IWebElement Edit = item.FindElement(By.XPath(".//span[@class='popupRightImage']"));
            DlkButton btnEdit = new DlkButton("Edit Icon", Edit);
            btnEdit.Click();

        }


        [Keyword("FilterItemExists", new String[] { "1|text|SearchedItem|ItemValue", 
                                                             "2|text|VariableName|TrueFalse" })]
        public void FilterItemExists(String SearchedItem, String VariableName)
        {
            Initialize();
            if (mComboBoxType == DDwnClass)
            {
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.CssSelector(mArrowDownCSS));  //"div>div"
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                }
                list.FindElement();
                try
                {
                    IWebElement itm = list.mElement.FindElement(By.XPath("./descendant::td/div[text()='" + SearchedItem + "']"));
                    DlkVariable.SetVariable(VariableName, true.ToString());
                }
                catch
                {
                    DlkVariable.SetVariable(VariableName, false.ToString());
                }
            }
            else
            {
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
                if (!list.Exists(1))
                {
                    this.MouseOver();
                    IWebElement mArrowDown = null;
                    mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                }

                list.FindElement();
                try
                {
                    IWebElement item = list.mElement.FindElement(By.XPath(".//span[text()='" + SearchedItem + "']/../.."));
                    DlkVariable.SetVariable(VariableName, true.ToString());
                }
                catch
                {
                    DlkVariable.SetVariable(VariableName, false.ToString());
                }
            }
        }

        [Keyword("VerifyListItem", new String[] { "1|text|SearchedItem|Posted",
                                                 "2|text|Items|1/8/2014~Posted~Posted",
                                                    "3|text|ImageExists|TRUE"})]
        public void VerifyListItem(String SearchedItem, String Items, String ImageExists)
        {
            Initialize();
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.Set(SearchedItem);
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

            DlkAssert.AssertEqual("VerifyListItem", Items, list.GetValue().Replace("\r\n", "~"));
            try 
            {
                IWebElement mImage = list.mElement.FindElement(By.TagName("img"));
                DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), mImage.Displayed);
            }
            catch
            {
                DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists),false);
            }
        }

        [Keyword("VerifyItemImageExists", new String[] { "1|text|SearchText|abc", "2|text|TargetItem|123 abc",
                                                    "3|text|ImageExists|TRUE"})]
        public void VerifyItemImageExists(String SearchText, String TargetItem, String ImageExists)
        {
            try
            {
                Initialize();
                IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.Set(SearchText);
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
                try
                {
                    item = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + TargetItem + "')]"));

                }
                catch
                {
                    item = list.mElement.FindElement(By.XPath("./descendant::div[contains(@class,'resultListItem')][contains(@data-key,'" + TargetItem + "')]"));
                }

                bool imgExists = item.FindElements(By.TagName("img")).Count > 0 ;
                DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), imgExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageDimmed", new String[] { "1|text|SearchText|abc", "2|text|TargetItem|123 abc",
                                                    "3|text|ImageExists|TRUE"})]
        public void VerifyItemImageDimmed(String SearchText, String TargetItem, String TrueOrFalse)
        {
            try
            {
                Initialize();

                Boolean ExpectedValue = false;
                if (Boolean.TryParse(TrueOrFalse, out ExpectedValue))
                {

                    IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
                    DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                    txtInput.Set(SearchText);
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

                    string xpath_text = "./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + TargetItem + "')]/ancestor::li";
                    string xpath_datakey = "./descendant::div[contains(@class,'resultListItem')][contains(@data-key,'" + TargetItem + "')]/ancestor::li";

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

        [Keyword("VerifyItemImageTooltip", new String[] { "1|SearchText|text|TRUE", "2|SearchText|TargetItem|FALSE", "3|SearchText|text|FALSE",
                                                    "3|SearchText|TargetItem|TRUE"})]
        public void VerifyItemImageTooltip(String SearchText, String TargetItem, String ExpectedValue, String TrueOrFalse)
        {
            try
            {
                Initialize();


                IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.Set(SearchText);
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
                        IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();
                    }
                    Thread.Sleep(5000);
                }
                list.FindElement();
                IWebElement item = null;

                string xpath_text = "./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + TargetItem + "')]/ancestor::li";
                string xpath_datakey = "./descendant::div[contains(@class,'resultListItem')][contains(@data-key,'" + TargetItem + "')]/ancestor::li";
                string xpath_innertext = "./descendant::div[contains(@class,'resultListItem')][contains(.,'" + TargetItem + "')]";

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
                mItem.MouseOverOffset(0,0);

                DlkAssert.AssertEqual("VerifyItemImageTooltip()", ExpectedValue, ActualValue);

            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("ClearField")]
        public void ClearField()
        {
            Initialize();
            Click();
            IWebElement mButton = DlkEnvironment.AutoDriver.FindElement(By.Id("ClearInputField"));
            try
            {
                DlkButton btnClear = new DlkButton("Clear", mButton);
                btnClear.Click();
            }
            catch 
            { 
                //the field doesn't have any value.
            }
        }

        [Keyword("MouseOver")]
        public void MouseOverComboBox()
        {

            this.MouseOver();
        }
        #endregion

        #region Private Functions
        private void DDwnSelect(String Value)
        {

                DlkBaseControl list;
                string listXpath;

                DlkBaseControl el = new DlkBaseControl("ComboBox", mElement);
                el.ScrollIntoViewUsingJavaScript();

                IWebElement mArrowDown = this.mElement.FindElement(By.CssSelector(mArrowDownCSS));  //"div>div"
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

                // IWebElement itm = list.mElement.FindElement(By.XPath("./descendant::td/div[contains(text(),'" + Value + "')]"));
                IWebElement itm = list.mElement.FindElement(By.XPath(listXpath));

                DlkBaseControl ctlItem = new DlkBaseControl("Item", itm);
                ctlItem.Click();
            
        }

        private void FormDDwnSelect(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "XPath", "//div[@class='results-container navigator_ngcrm_popup_overlay']");
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.ClassName("tap-target"));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
                Thread.Sleep(2000);
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
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
            if (!list.Exists(1))
            {
                ctlArrowDown.Click();
            }

            list.FindElement();
            if (list.mElement.FindElements(By.XPath(".//div[text()[normalize-space()='"+ ItemValue +"']]")).Count > 0)
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
                IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath(".//tr[contains(@class,'ddwnListItem')]/td/div"));
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
                    DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, ActValues, true);
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

        private void SearchVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {

            try
            {
                ExpectedValues = ExpectedValues.ToLower();
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }

                list.FindElement();
                List<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./descendant::li[contains(@class,'search-result')]")).Where(item => item.Displayed).ToList();

                if (lstItems.Count == 0)
                {
                    DlkLogger.LogInfo("Looking for primary item class...");
                    lstItems = list.mElement.FindElements(By.XPath(".//div[contains(@class,'Primary')]")).Where(item => item.Displayed).ToList();
                }
                // if no items found, attempt this:
                if (lstItems.Count == 0)
                {
                    DlkLogger.LogInfo("Primary item class not found, looking for resultListItem class...");
                    lstItems = list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]")).Where(item => item.Displayed).ToList();
                }
                String ActValues = "";
                for (int i = 0; i < lstItems.Count; i++)
                {
                    DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                    if (ActValues != "")
                    {
                        ActValues = ActValues + "~";
                    }
                    ActValues = ActValues + item.GetValue().Trim();
                }
                ActValues = ActValues.ToLower();
                if (VerifyPartialList)
                {
                    if (ActValues.Contains(ExpectedValues))
                    {
                        DlkLogger.LogInfo(String.Format("VerifyPartialList() passed. Actual values:[{0}] contain Expected Values:[{1}]", ActValues.ToLower(), ExpectedValues.ToLower()));
                        return;
                    }
                }
                // try to get the items using secondary value.
                if (!ActValues.Equals(ExpectedValues))
                {
                    DlkLogger.LogInfo("Expected not equal to actual items, finding items using the secondary class...");
                    DlkLogger.LogInfo("Comparing expected and actual items using secondary class...");
                    lstItems = list.mElement.FindElements(By.XPath(".//div[contains(@class,'Secondary')]")).Where(item => item.Displayed).ToList();
                    if (lstItems.Count == 0)
                    {
                        throw new Exception("VerifyList failed. No secondary items were found.");
                    }
                    ActValues = "";
                    for (int i = 0; i < lstItems.Count; i++)
                    {
                        DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                        if (ActValues != "")
                        {
                            ActValues = ActValues + "~";
                        }
                        ActValues = ActValues + item.GetValue().Trim();
                    }
                }
                else
                {
                    DlkLogger.LogInfo(String.Format("VerifyList() passed. Actual values:[{0}] is equal to Expected Values:[{1}]", ActValues.ToLower(), ExpectedValues.ToLower()));
                }

                ActValues = ActValues.ToLower();
                // log comparison results
                if (VerifyPartialList)
                {
                    DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, ActValues, true);
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyList()", ExpectedValues, ActValues);
                }
                this.ClickUsingJavaScript(); //close the list

            }
            catch (Exception ex)
            {
                throw new Exception("VerifyPartialList() failed: " + ex.Message);
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
            DlkBaseControl list;
            IWebElement itm;
            DlkBaseControl ctlItem;

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
                txtInput.ShowAutoComplete(Value);

                list = new DlkBaseControl("List", "CLASS_DISPLAY", mDDwnSearchColListClass);  //"results"
                list.VerifyExists(true);
                itm = list.mElement.FindElement(By.XPath("./descendant::li/div[contains(.,'" + Value + "')]"));
                ctlItem = new DlkBaseControl("Item", itm);
                ctlItem.Click();
            }
            else
            {
                list.FindElement();
                itm = list.mElement.FindElement(By.XPath("./descendant::td/div[contains(text(),'" + Value + "')]"));
                ctlItem = new DlkBaseControl("Item", itm);
                ctlItem.ScrollIntoViewUsingJavaScript();
                ctlItem.Click();
            }
        }

        private void DDwnColAvailableInList(String ItemValue, String ExpectedValue)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", ddwnInputListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mDDwnArrowDownXPATH));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();
            DlkBaseControl ctlItem = new DlkBaseControl("Item", "XPATH", "//tr[@class='ddwnListItem']/td/div[contains(text(),'" + ItemValue + "')]");
            ctlItem.VerifyExists(Convert.ToBoolean(ExpectedValue));
        }

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
            if (!list.Exists(1))
            {
                this.MouseOver();
                IWebElement mArrowDown = null;
                try
                { 
                    mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                }
                catch
                {
                     mArrowDown = this.mElement.FindElement(By.XPath("//p[contains(@class,'arrow')]"));
                }
                
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath(".//span[text()='" + Value + "']/../.."));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
            ctlItem.Click();
        }

        private void QuickEditSelect(String Value)  //modified because of Reporting in CRM (when too many records to display)
        {
            try
            {
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = mElement.FindElement(By.XPath(".//div//span[contains(@class, 'content')]"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                }
                list.FindElement();
                IWebElement item = null;

                string xpath_Span = ".//span[text()='" + Value + "']/../..";
                string xpath_SpanContainer = ".//li[descendant::span[.='" + Value.Trim('.') + "'] and descendant::a[contains(.,'...')]]";

                if(CheckElementExists(list.mElement, xpath_Span))
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
                    list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
                    list.FindElement();
                    item = list.mElement.FindElement(By.XPath(".//span[text()='" + Value + "']/../.."));
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
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();
            IWebElement item = list.mElement.FindElement(By.XPath("./ul[@class='popupmenuItems']/li/a/span[text()='" + ItemValue + "']"));
            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
            ctlItem.VerifyExists(Convert.ToBoolean(ExpectedValue));
        }

        private void PopupItemEnabled(String ItemValue, String ExpectedValue)
        {
            Boolean bActualValue = false;
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./ul[@class='popupmenuItems']/li/a/span"));
            for (int i = 0; i < lstItems.Count; i++)
            {
                DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));
                if (item.GetValue() == ItemValue)
                {
                   String sDisabled = item.FindParentByTagName("li").GetAttribute("class");
                   if (!sDisabled.Contains("disabled"))
                    {
                        bActualValue = true;
                        break;
                    }
                }
            }
            DlkAssert.AssertEqual("PopupItemDisabled", Convert.ToBoolean(ExpectedValue), bActualValue);          
        }

        private void PopupVerifyList(String ExpectedValues, Boolean VerifyPartialList = false)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
            DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);

            if (!list.Exists(1))
            {
                ctlArrowDown.Click();
            }

            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath("./ul[@class='popupmenuItems']/li/a/span"));
            String sActualValues = "";
            for(int i=0; i < lstItems.Count; i++)
            {
                DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));

                if(sActualValues != "")
                {
                    sActualValues = sActualValues + "~";
                }
                sActualValues = sActualValues + item.GetValue();
            }

            if (VerifyPartialList)
            {
                DlkAssert.AssertEqual("VerifyPartialList()", ExpectedValues, sActualValues, true);
            }
            else
            {
                DlkAssert.AssertEqual("VerifyList", ExpectedValues, sActualValues);
            }

            //close the list after Verify
            ctlArrowDown.Click();
        }

        private void PopupVerifyListCount(String ExpectedCount)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mPopupListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mPopupArrowXPath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
            }

            list.FindElement();
            IReadOnlyCollection<IWebElement> lstItems = list.mElement.FindElements(By.XPath(".//ul[@class='popupmenuItems']/li"));
            DlkAssert.AssertEqual("VerifyListCount", Convert.ToInt32(ExpectedCount), lstItems.Count());
        }

        private void SearchSelect(String Value)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath)); //".//input[contains(@class,'search-input')]";
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
         //   txtInput.ShowAutoComplete(Value);
            txtInput.Set(Value);
           
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);  //"results"
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
            ctlItem.Click();
           // ctlItem.ClickUsingJavaScript();

        }

        private void SearchVerifyListCount(String ExpectedCount)
        {
            var count = 0;
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
            var recordCount = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@class='recordCount']"));
            foreach (var item in recordCount)
            {       
                if (int.TryParse(item.Text.Replace(",",""),out count))
                {
                    break;
                }
              
            }
            if(lstItems.Count == 0){
                lstItems = list.mElement.FindElements(By.XPath(".//div[contains(@class,'resultListItem')]"));
            }

            if (count == 0) count = lstItems.Count;
            DlkAssert.AssertEqual("VerifyListCount", Convert.ToInt32(ExpectedCount), count);
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
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            //txtInput.ShowAutoComplete(Value);
            txtInput.Set(Value);
            Thread.Sleep(3000);
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
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
        }

        /// <summary>
        /// Selects item from the combobox list without setting a value to filter out the list items.
        /// </summary>
        /// <param name="Value"></param>
        private void SearchSelectWithoutSearch(String Value)
        {
            DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", mSearchListClass);
            if (!list.Exists(1))
            {
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(mSearchArrowXpath));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.ClickUsingJavaScript();
                Thread.Sleep(5000);
            }

            list.FindElement();
            IWebElement item = null;

            string itemXPath = "";
            string xpath1 = "./descendant::li[contains(@class,'search-result')]//*[normalize-space(text()) = '" + Value + "']";
            string xpath2 = "./descendant::li[contains(@class,'search-result')]//*[normalize-space(.) = '" + Value + "']";
            string xpath3 = "./descendant::div[contains(@class,'resultListItem')]//*[normalize-space(text()) = '" + Value + "']";

            if (list.mElement.FindElements(By.XPath(xpath1)).Count > 0)
                itemXPath = xpath1;
            else if (list.mElement.FindElements(By.XPath(xpath2)).Count > 0)
                itemXPath = xpath2;
            else if (list.mElement.FindElements(By.XPath(xpath3)).Count > 0)
                itemXPath = xpath3;
            else
                throw new Exception("Unable to find item in the list.");

            item = list.mElement.FindElement(By.XPath(itemXPath));

            DlkBaseControl ctlItem = new DlkBaseControl("Item", item);
            ctlItem.Click();
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
            IWebElement mInput = this.mElement.FindElement(By.CssSelector(mInputCSS)); //"input"
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.ShowAutoComplete(Value); 
          //txtInput.Set(Value); 
            
        }

        private void PopupSet(String Value)
        {
            DlkLogger.LogInfo("Set() not supported for 'Popup Menu' Combo Box.");
        }

        private void SearchSet(String Value)
        {
            IWebElement mInput = this.mElement.FindElement(By.XPath(mSearchInputXPath));  //".//input[contains(@class,'search-input')]"
            
            mInput.Clear();
            mInput.SendKeys(Value);
            Thread.Sleep(3000);
        }

        private void DDwnVerifyValue(String ExpectedValue)
        {
            IWebElement mInput = this.mElement.FindElement(By.CssSelector(mInputCSS));
            DlkTextBox txtInput = new DlkTextBox("Input", mInput);
            txtInput.VerifyText(ExpectedValue);

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
        #endregion
    }
}
