using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace DCOLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        private Boolean IsListDisplayed = false;
        private DlkComboBoxList mComboBoxList;
        private string mClassName;

        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkComboBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


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

                DlkBaseControl ctlToClick = this;
                if (!IsListDisplayed)
                {
                    var textValue = Value;
                    ctlToClick.Click();

                    string searchString = ".//option[text()='" + textValue + "']";

                    string ret = string.Empty;
                    DlkBaseControl ctlComboText = new DlkBaseControl("ComboText", mElement.FindElement(By.XPath(searchString)));
                    ctlComboText.MouseOverUsingJavaScript();
                    ctlComboText.Click();
                    IsListDisplayed = true;
                }
            
                IsListDisplayed = false;
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        ///// <summary>
        ///// Select a combo box value within a table.
        ///// </summary>
        ///// <param name="Value">Value to select</param>
        ///// <param name="UseShiftTab">Default would be False, can be set to True if ShiftTab() is needed to be performed in the control.</param>
        //public void SelectDropdownValue(String Value, bool UseShiftTab)
        //{
        //    try
        //    {
        //        Initialize();
        //        if (mClassName != "tccbtb") // to prevent unnecessary scrolling of a table
        //        {
        //            this.ScrollIntoViewUsingJavaScript();
        //        }
        //        DisplayComboBoxList(Value, 3, VerifyAfterSelect);
        //        string actualValue = string.Empty;
        //        if (mClassName == "tccbtb") // use directional SendKeys for table dropdown lists
        //        {
        //            actualValue = SelectTCCBV(Value, VerifyAfterSelect);
        //            if (this.GetValue() != Value) //ensure correct value was selected
        //            {
        //                mComboBoxList.Select(Value);
        //            }
        //        }
        //        else // otherwise, click on target item
        //        {
        //            if (VerifyAfterSelect)
        //            {
        //                actualValue = mComboBoxList.SelectItem(Value, VerifyAfterSelect);
        //            }
        //            else if (!Value.Contains("'"))
        //            {
        //                mComboBoxList.SelectItemWithoutVerify(Value, VerifyAfterSelect);
        //            }
        //            else
        //            {
        //                mComboBoxList.Select(mItemIdx);
        //            }
        //        }
        //        if (UseShiftTab)
        //        {
        //            VerifyValue(Value);
        //            ShiftTab();
        //        }

        //        IsListDisplayed = false;
        //        DlkLogger.LogInfo("Successfully executed Select()");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Select() failed : " + e.Message, e);
        //    }
        //}

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

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
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

        public bool DisplayComboBoxList(string Value, int RetryLimit, bool IsStrict = true)
        {
            DlkBaseControl ctlToClick = this;
            if (!IsListDisplayed)
            {
                var textValue = Value;
                ctlToClick.Click();

                string searchString = "//option[@text='" + textValue + "']/..";
                mComboBoxList = new DlkComboBoxList("ComboBoxList", "ID", searchString);

                string ret = string.Empty;
                DlkBaseControl ctlComboText = new DlkBaseControl("ComboText", mElement.FindElement(By.XPath("//option[text()='" + textValue + "']")));
                ctlComboText.MouseOver();
                ctlComboText.Click();
                IsListDisplayed = true;
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
            ret = GetValue();

            return ret;
        }

    }
}
