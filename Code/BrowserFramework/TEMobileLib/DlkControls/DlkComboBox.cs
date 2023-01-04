using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        private Boolean IsListDisplayed = false;
        private DlkComboBoxList mComboBoxList;
        private string mClassName;
        //private Boolean VerifyAfterSelect = true;
        private Boolean VerifyAfterSelect = false;
        private int mItemIdx;


        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkComboBox(String ControlName, String SearchType, String SearchValue, Boolean VerifyAfterSelect)
            : base(ControlName, SearchType, SearchValue)
        {
            this.VerifyAfterSelect = VerifyAfterSelect;
        }
        public DlkComboBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        public new bool VerifyControlType()
        {
            Initialize();
            if(mClassName.ToLower()=="tccbf" || mClassName.ToLower() == "popupdatafield")
            {
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::span[@class='tCCB']"));
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
                DlkBaseControl mCorrectControl = new DlkBaseControl("ComboBox", "", "");
                bool mAutoCorrect = false;
                IWebElement parentControl = null;

                VerifyControlType();
                if (mClassName.ToLower() == "tccbf" || mClassName.ToLower() == "popupdatafield")
                {
                    mAutoCorrect = false;
                }
                else
                {
                    try
                    {
                        parentControl = mElement.FindElement(By.XPath("./ancestor::span[@class='tCCB']"));
                        mAutoCorrect = true;
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        mAutoCorrect = false;
                    }
                }

                
                //mAutoCorrect = true;

                if (mAutoCorrect)
                {
                    mCorrectControl = new DlkBaseControl("CorrectControl", parentControl);

                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClass = mCorrectControl.GetAttributeValue("class");
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
                    else if (mClass != null && mClass != "")
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

        public void Initialize()
        {
            FindElement();
            mClassName = mElement.GetAttribute("class").ToLower();
        }

        public DlkComboBoxList ComboBoxList
        {
            get
            {
                return mComboBoxList;
            }
        }

       
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                this.ScrollIntoViewUsingJavaScript();
                DisplayComboBoxList(Value, 3, VerifyAfterSelect);
                string actualValue = string.Empty;
                if (mClassName == "tccbtb") // use directional SendKeys for table dropdown lists
                {
                    actualValue = SelectTCCBV(Value, VerifyAfterSelect);
                }
                else // otherwise, click on target item
                {
                    if (VerifyAfterSelect)
                    {
                        actualValue = mComboBoxList.SelectItem(Value, VerifyAfterSelect);
                    }
                    else if (!Value.Contains("'"))
                    {
                        mComboBoxList.SelectItemWithoutVerify(Value, VerifyAfterSelect, mClassName == "menumobile");
                    }
                    else
                    {
                        mComboBoxList.Select(mItemIdx);
                    }

                    // if item selection was unsuccessful, select using directional SendKeys
                    //if (string.IsNullOrEmpty(actualValue))
                    //{
                    //    DisplayComboBoxList(Value, 3, IsStrict);
                    //    actualValue = SelectTCCBV(Value, IsStrict);
                    //}
                }
                if (VerifyAfterSelect)
                {
                    VerifyValue(Value);
                    //mElement.SendKeys(Keys.Shift + Keys.Tab);
                    ShiftTab();
                }

                IsListDisplayed = false;
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Select a combo box value within a table.
        /// </summary>
        /// <param name="Value">Value to select</param>
        /// <param name="UseShiftTab">Default would be False, can be set to True if ShiftTab() is needed to be performed in the control.</param>
        public void SelectDropdownValue(String Value, bool UseShiftTab)
        {
            try
            {
                Initialize();
                if (mClassName != "tccbtb") // to prevent unnecessary scrolling of a table
                {
                    this.ScrollIntoViewUsingJavaScript();
                }
                DisplayComboBoxList(Value, 3, VerifyAfterSelect);
                string actualValue = string.Empty;
                if (mClassName == "tccbtb") // use directional SendKeys for table dropdown lists
                {
                    actualValue = SelectTCCBV(Value, VerifyAfterSelect);
                    if (this.GetValue() != Value) //ensure correct value was selected
                    {
                        mComboBoxList.Select(Value);
                    }
                }
                else // otherwise, click on target item
                {
                    if (VerifyAfterSelect)
                    {
                        actualValue = mComboBoxList.SelectItem(Value, VerifyAfterSelect);
                    }
                    else if (!Value.Contains("'"))
                    {
                        mComboBoxList.SelectItemWithoutVerify(Value, VerifyAfterSelect);
                    }
                    else
                    {
                        mComboBoxList.Select(mItemIdx);
                    }
                }
                if (UseShiftTab)
                {
                    VerifyValue(Value);
                    ShiftTab();
                }

                IsListDisplayed = false;
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|1" })]
        public void SelectByIndex(String ItemIndex)
        {
            int iTargetItemPos = Convert.ToInt32(ItemIndex);

            try
            {
                Initialize();
                this.ScrollIntoViewUsingJavaScript();
                string itemText = GetText();
                DisplayComboBoxList(itemText, 3);
                mComboBoxList.Select(iTargetItemPos);

                string selectedText = GetText();
                DlkLogger.LogInfo("Successfully executed SelectByIndex(). Value: " + selectedText + " was selected from the list.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        private string SelectTCCBV(String Value, bool IsStrict)
        {
            IWebElement comboText;
            switch (mClassName)
            {
                case "popupdatafield":
                case "tccb":
                    DlkBaseControl dropDownButton = new DlkBaseControl("DropdownButton", mElement.FindElement(By.CssSelector("*[class|=tCCBImg")));
                    comboText = mElement.FindElement(By.CssSelector("span.tCCBT"));
                    break;
                default:
                    comboText = mElement;
                    break;
            }
            int iOrigPos;
            int iTargetPos;
            string ret = string.Empty;
            DlkBaseControl comboTextControl = new DlkBaseControl("Combo Text", comboText);
            iOrigPos = mComboBoxList.GetItemPosition(comboTextControl.GetValue(), IsStrict, out ret);
            ret = string.Empty; // flush value, original position text is not relevant
            iTargetPos = mComboBoxList.GetItemPosition(Value, IsStrict, out ret);
            DlkLogger.LogInfo("SelectTCCBV() : targetpos=" + iTargetPos + " origpos=" + iOrigPos);

            //CollapseDropDownList();

            if (iOrigPos > 0 && iTargetPos > 0)
            {
                while (iOrigPos != iTargetPos)
                {
                    if (iOrigPos < iTargetPos)
                    {
                        SendKeysToComboBox(OpenQA.Selenium.Keys.ArrowDown);
                        iOrigPos++;
                    }
                    else if (iOrigPos > iTargetPos)
                    {
                        SendKeysToComboBox(Keys.ArrowUp);
                        iOrigPos--;
                    }
                }
            }
            if (iOrigPos == 0)
            {
                throw new Exception("Original value '" + comboTextControl.GetValue() + "' not found in list");
            }
            if (iTargetPos == 0)
            {
                throw new Exception("Value '" + Value + "' not found in list");
            }
            return ret;
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                //ExpectedValue = DlkEnvironment.BuildNumber == "7.0.1" ? ExpectedValue : ExpectedValue.Replace("<SPAN class=tCVI>", "").Replace(@"</SPAN>", "");
                DlkAssert.AssertEqual("VerifyValue()", ExpectedValue, DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(GetText())));
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|Sample item",
                                                         "2|text|Expected Value|TRUE"})]
        public void VerifyAvailableInList(String ItemValue, String ExpectedValue)
        {
            try
            {
                Initialize();
                String comboBoxItems = "";
                this.ScrollIntoViewUsingJavaScript();
                DisplayComboBoxList(this.GetText(), 3);
                comboBoxItems = mComboBoxList.GetAllItemsWithDelimiter();
                CollapseDropDownList();
                DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(ExpectedValue),
                    DlkString.NormalizeNonBreakingSpace(comboBoxItems).Split('~').Contains(ItemValue));
                DlkLogger.LogInfo("VerifyAvailableInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyList(String ExpectedValues)
        {
            try
            {
                Initialize();
                String comboBoxItems = "";
                this.ScrollIntoViewUsingJavaScript();
                DisplayComboBoxList(this.GetText(), 3);
                comboBoxItems = mComboBoxList.GetAllItemsWithDelimiter();
                CollapseDropDownList();
                DlkAssert.AssertEqual("VerifyList()", ExpectedValues, DlkString.NormalizeNonBreakingSpace(comboBoxItems));
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        private void SendKeysToComboBox(String sTextToEnter)
        {
            switch (DlkEnvironment.mBrowser.ToLower())
            {
                case "safari":
                    mComboBoxList.mElement.SendKeys(sTextToEnter);
                    break;
                default:
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.SendKeys(sTextToEnter).Build().Perform();
                    break;

            }
        }

        public bool DisplayComboBoxList(string Value, int RetryLimit, bool IsStrict = true)
        {
            DlkBaseControl ctlToClick = this;
            if (!IsListDisplayed)
            {
                switch (mClassName)
                {
                    case "popupdatafield":
                    case "tccb":
                        DlkBaseControl dropDownButton = new DlkBaseControl("DropdownButton", mElement.FindElement(By.CssSelector("*[class|=tCCBImg]")));
                        ctlToClick = dropDownButton;
                        if (DlkEnvironment.mBrowser.ToLower() == "safari" || DlkEnvironment.mIsMobileBrowser)
                        {
                            ctlToClick.MouseOverUsingJavaScript();
                        }
                        else
                        {
                            ctlToClick.MouseOverOffset(5, 5);
                        }
                        break;
                    default:
                        ctlToClick = this;
                        break;
                }

                //get the part of the string that we need to find the comboboxlist
                //i.e. outside<span>inside</span> returns "outside\r\ninside" - we only need "outside"
                var textValue = Value;
                if (textValue.IndexOf("\r\n") > -1)
                {
                    textValue = textValue.Substring(0, textValue.IndexOf("\r\n"));
                }

                if (mClassName == "menumobile")
                {
                    string searchString = IsStrict ? "//div[@class='menuMobileBody']//span[text()='" + textValue + "']/.."
                        : "//div[@class='menuMobileBody']//span[contains(text(),'" + textValue + "')]/..";
                    mComboBoxList = new DlkComboBoxList("ComboBoxList", "xpath_display", searchString);
                }
                else if (!textValue.Contains("'"))
                {
                    string searchString = IsStrict ? "//div[@class='tCCBV']/div[text()='" + textValue + "']/.."
                        : "//div[@class='tCCBV']/div[contains(text(),'" + textValue + "')]/..";
                    mComboBoxList = new DlkComboBoxList("ComboBoxList", "xpath_display", searchString);
                }
                else if (textValue != mElement.Text) // set search text to currently selected value
                {
                    string searchString = IsStrict ? "//div[@class='tCCBV']/div[text()='" + mElement.Text + "']/.."
                        : "//div[@class='tCCBV']/div[contains(text(),'" + mElement.Text + "')]/..";
                    mComboBoxList = new DlkComboBoxList("ComboBoxList", "xpath_display", searchString);
                }
                else // set search text to empty string
                {
                    string searchString = IsStrict ? "//div[@class='tCCBV']/div[text()='" + "" + "']/.."
                        : "//div[@class='tCCBV']/div[contains(text(),'" + "" + "')]/..";
                    mComboBoxList = new DlkComboBoxList("ComboBoxList", "xpath_display", searchString);
                }

                int currRetry = 0;
                while (++currRetry <= RetryLimit)
                {
                    try
                    {
                        this.Initialize();
                        ctlToClick.Click();
                        if (!textValue.Contains("'"))
                        {
                            mComboBoxList.FindElement(1); // quicker -> if not found then initilize will not be called
                            mComboBoxList.Initialize();
                            IsListDisplayed = true;
                            break;
                        }
                        else
                        {
                            string ret = string.Empty;
                            mItemIdx = 0;
                            mItemIdx = mComboBoxList.GetItemPosition(textValue, IsStrict, out ret);
                            if (mItemIdx < 0)
                            {
                                IsListDisplayed = false;
                            }
                            else
                            {
                                IsListDisplayed = true;
                                break;
                            }
                        }
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

        public void CollapseDropDownList()
        {
            if (IsListDisplayed)
            {
                try
                {
                    this.Click();
                }
                catch
                {
                    //mElement.SendKeys(Keys.Shift + Keys.Tab);
                    ShiftTab();
                    FocusUsingJavaScript();
                }
                finally
                {
                    IsListDisplayed = false;
                }
            }
        }

        public String GetText()
        {
            Initialize();
            String ret = "";
            if (mClassName == "tccbf" || mClassName == "tccbtb")
            {
                ret = GetValue();
            }
            else if (mClassName == "popupdatafield" || mClassName == "tccb")
            {
                DlkBaseControl ctlComboText = new DlkBaseControl("ComboText", mElement.FindElement(By.CssSelector("span.tCCBT")));
                ret = ctlComboText.GetValue();
            }
            return ret;
        }

    }
}
