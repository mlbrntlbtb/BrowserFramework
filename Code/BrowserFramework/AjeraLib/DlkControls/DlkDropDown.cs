using System;
using System.Linq;
using AjeraLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AjeraLib.DlkControls
{
    [ControlType("DropDown")]
    class DlkDropDown : DlkAjeraBaseControl
    {
        #region DECLARATIONS
        private Boolean IsListDisplayed;
        private DlkDropDownItemList mDropDownItemList;
        private Boolean VerifyAfterSelect = true;
        private Boolean IsInit = false;
        private int retryLimit = 3;
        #endregion

        #region PROPERTIES
        public DlkDropDownItemList DropDownItemList
        {
            get
            {
                return mDropDownItemList;
            }
        }
        #endregion

        #region CONSTRUCTORS
        public DlkDropDown(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropDown(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropDown(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkDropDown(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
        }

        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                ScrollIntoViewUsingJavaScript();
                DisplayComboBoxList(3, VerifyAfterSelect);
                
                if (!Value.Equals("Edit Stacked List"))
                {
                    //because edit stacked list is not being selected as the 
                    //dropdown value and it is an
                    //intended feature T#593669
                    mDropDownItemList.SelectItem(Value, 3);
                    if (VerifyAfterSelect)
                    {
                        VerifyValue(Value);
                    }
                }
                else
                {
                    mDropDownItemList.SelectItem(Value, 1);
                    DlkLogger.LogInfo("Desired value to be selected is Edit Stacked List.");
                }
                

                IsListDisplayed = false;
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyValue()", ExpectedValue.ToLower(), DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(GetSelectedValue().ToLower())));
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyReadOnly(string IsTrueOrFalse)
        {
            try
            {
                string actualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", IsTrueOrFalse.ToLower(), DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST

        [Keyword("SelectByRow", new String[] { "1|text|Value|SampleValue" })]
        public void SelectByRow(String RowNumber, String Value)
        {
            try
            {
                bool bFound = false;
                int curRetry = 0;
                InitializeRow(RowNumber);

                while (++curRetry <= retryLimit && !bFound)
                {
                    ScrollIntoViewUsingJavaScript();
                    DisplayComboBoxList(3);
                    mDropDownItemList.SelectItem(Value, 3);
                    VerifyValueByRow(RowNumber, Value);
                    bFound = true;
                    DlkLogger.LogInfo("Successfully executed SelectByRow()");
                }

                if (!bFound)
                {
                    throw new Exception("SelectByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                       "' is out of bounds of the list count.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValueByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyValueByRow(string RowNumber, string ExpectedValue)
        {
            try
            {
                InitializeRow(RowNumber);
                if (mElement.Displayed)
                {
                    string actualValue = GetSelectedValue();
                    DlkAssert.AssertEqual("VerifyValueByRow()", ExpectedValue.ToLower(),
                        DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                    DlkLogger.LogInfo("VerifyValueByRow() passed");
                }
                else
                {
                    throw new Exception("VerifyValueByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                        "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValueByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnlyByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyReadOnlyByRow(string RowNumber, string IsTrueOrFalse)
        {
            try
            {
                InitializeRow(RowNumber);
                if (mElement.Displayed)
                {
                    string actualValue = IsReadOnly();
                    DlkAssert.AssertEqual("VerifyReadOnlyByRow()", IsTrueOrFalse.ToLower(),
                        DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                    DlkLogger.LogInfo("VerifyReadOnlyByRow() passed");
                }
                else
                {
                    throw new Exception("VerifyReadOnlyByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                        "' cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnlyByRow() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExistsByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExistsByRow(string RowNumber, string IsTrueOrFalse)
        {
            try
            {
                string actualValue = SelectedElementExists(RowNumber).ToString();
                DlkAssert.AssertEqual("VerifyExistsByRow()", IsTrueOrFalse.ToLower(), DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                DlkLogger.LogInfo("VerifyExistsByRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsByRow() failed : " + e.Message, e);
            }

        }

        #endregion

        #region METHODS

        public bool DisplayComboBoxList(int retryLimit, bool isStrict = true)
        {
            DlkBaseControl ctlToClick = this;
            if (!IsListDisplayed)
            {
                mDropDownItemList = new DlkDropDownItemList("DropDownItemList", mElement);

                int currRetry = 0;
                while (++currRetry <= retryLimit)
                {
                    try
                    {
                        ctlToClick.mElement.Click();
                        IsListDisplayed = true;
                        break;
                    }
                    catch
                    {
                        DlkLogger.LogInfo("DisplayComboBoxList() : Unable to display combobox dropdown list.");
                        IsListDisplayed = false;
                    }
                }
            }
            return IsListDisplayed;
        }


        public String GetSelectedValue()
        {
            String actualValue = "";
            SelectElement selectedItem;
            if (mElement.TagName != "select")
            {
                selectedItem = new SelectElement(mElement.FindElement(By.CssSelector("select")));
            }
            else
            {
                selectedItem = new SelectElement(mElement);
            }
            actualValue = selectedItem.SelectedOption.Text;
            return actualValue;
        }
        #endregion
    }

    public class DlkDropDownItemList : DlkAjeraBaseControl
    {
        #region DECLARATIONS
/*
        private String mStrListItemDesc = "option";
*/
        private string _actualValueSelected = "";
        private Boolean IsInit;
        #endregion

        #region CONSTRUCTORS
        public DlkDropDownItemList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropDownItemList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropDownItemList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }


        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        private new void InitializeSelectedElement(string RowNumber)
        {
            FindElements();
            int iTargetItemPos = Convert.ToInt32(RowNumber) - 1;
            mElement = mElementList[iTargetItemPos];
        }

        #endregion

        #region METHODS
        public void SelectItemFromList(String Value)
        {
            Boolean bFound = false;
            string actualItems = "";

            SelectElement mSelection;
            if (mElement.TagName != "select")
            {
                mSelection = new SelectElement(mElement.FindElement(By.CssSelector("select")));
            }
            else
            {
                mSelection = new SelectElement(mElement);
            }

            string ddClass = mElement.GetAttribute("class");
            foreach (var option in mSelection.Options)
            {
                actualItems = actualItems + "-" + option.Text;

                if (option.Text.ToLower() == Value.ToLower())
                {                    
                    if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                    {
                        Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                        //select the option at lightning speed
                        mAction.MoveToElement(mElement, 5, 5).SendKeys(Value).Click().Perform();
                    }
                    else
                    {
                        mSelection.SelectByText(option.Text);
                        if (!ddClass.Contains("sortgrouplevel")) 
                            mElement.SendKeys(Keys.Enter);
                    }
                    _actualValueSelected = Value;
                    bFound = true;
                    break;
                }
            }

            if (!bFound)
            {
                throw new Exception("Select() failed. Control : " + mControlName + " : '" + Value +
                                        "' not found in list. : Actual List = " + actualItems);
            }
        }

        public void SelectItem(string value, int retryLimit)
        {
            int currRetry = 0;
            try
            {
                while (++currRetry <= retryLimit && _actualValueSelected != value)
                {
                    SelectItemFromList(value);
                }
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("SelectItem() : Unable to select item from dropdown list.");
                throw new Exception(e.Message);
            }
        }

       
        #endregion
    }
}
