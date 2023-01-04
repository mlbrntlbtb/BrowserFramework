using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using ProjectInformationManagementLib.DlkSystem;
using System;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkUtility;
using System.Collections.Generic;
using System.Threading;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;
        private Boolean IsListDisplayed;
        private DlkComboBoxItemList mComboBoxItemList;
        #endregion

        #region CONSTRUCTOR
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
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Contains("iframe"))
            {
                _iframeSearchType = true;
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
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
                FindElement();
                DisplayComboBoxList(3);
                Thread.Sleep(5000);
                mComboBoxItemList.SelectItem(Value, 3);
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        #endregion

        #region METHODS
        public bool DisplayComboBoxList(int retryLimit)
        {
            if (!IsListDisplayed)
            {
                mComboBoxItemList = new DlkComboBoxItemList("ComboBoxItemList", mElement);

                int currRetry = 0;
                while (++currRetry <= retryLimit && !mComboBoxItemList.ComboBoxListExist())
                {
                    DlkBaseControl ctlToClick = this;

                    try
                    {
                        DlkBaseControl dropDownButton = new DlkBaseControl("DropdownButton", mElement.FindElement(By.XPath("./following-sibling::span[@class='hitBox']")));
                        dropDownButton.Click();
                        IsListDisplayed = true;
                        break;
                    }
                    catch
                    {
                        DlkLogger.LogInfo("DisplayComboBoxList() : Unable to display combobox list.");
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

    public class DlkComboBoxItemList : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private string _actualValueSelected = "";
        private string popupCombo_XPath = "//ul[@class='searchResults']";
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTORS
        public DlkComboBoxItemList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBoxItemList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBoxItemList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        

        public void Initialize()
        {
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Contains("iframe"))
            {
                _iframeSearchType = true;
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
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
        public bool ComboBoxListExist()
        {
            Boolean bFound = false;

            try
            {
                if (mElement.TagName == "input")
                {
                    //Popup Type ComboBox
                    var cmbSelectionList = mElement.FindElements(By.XPath(popupCombo_XPath));
                    foreach (var list in cmbSelectionList)
                    {
                        if (list.Displayed)
                        {
                            bFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    //Select Type ComboBox
                    SelectElement mSelection;
                    if (mElement.TagName != "select")
                    {
                        mSelection = new SelectElement(mElement.FindElement(By.CssSelector("select")));
                        bFound = true;
                    }
                    else
                    {
                        mSelection = new SelectElement(mElement);
                        bFound = true;
                    }
                }
            }
            catch
            {
                bFound = false;
            }
            return bFound;
        }


        public void SelectItemFromList(String Value)
        {
            Boolean bFound = false;
            string actualItems = "";
            
            if (mElement.TagName == "input")
            {
                //Popup Type ComboBox
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.mWindowIndex]);
                var cmbSelectionList = DlkEnvironment.AutoDriver.FindElements(By.XPath("//ul[@class='searchResults']/li"));
                foreach (var option in cmbSelectionList)
                {
                    if (option.Displayed)
                    {
                        if (option.Text == "")
                            actualItems = actualItems + "-" + option.GetAttribute("innerHTML");
                        else
                            actualItems = actualItems + "-" + option.Text;

                        if (option.Text.ToLower() == Value.ToLower())
                        {
                            option.Click();
                            _actualValueSelected = Value;
                            bFound = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                //Select Type ComboBox
                SelectElement mSelection;
                if (mElement.TagName != "select")
                {
                    mSelection = new SelectElement(mElement.FindElement(By.CssSelector("select")));
                }
                else
                {
                    mSelection = new SelectElement(mElement);
                }

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
                        }
                        _actualValueSelected = Value;
                        bFound = true;
                        break;
                    }
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
                DlkLogger.LogInfo("SelectItem() : Unable to select item from combobox list.");
                throw new Exception(e.Message);
            }
        }


        #endregion
    }
}

