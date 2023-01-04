using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("Picker")]
    public class DlkPicker : DlkBaseControl
    {
        public DlkPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private int ClickOffsetInitialValue = 10;
        private int ClickOffsetMaxValue = 50;
        private string mItemsXPath = ".//div[contains(@class, 'x-dataview-item')]";
        private string mSelectedItemXPath = ".//div[contains(@class,'selected')]/div[contains(@class,'x-picker-item')]";
        private string mPreviousItemXPath = ".//div[contains(@class,'selected')]/div[contains(@class,'x-picker-item')]/../preceding-sibling::div[1]";
        private string mNextItemXPath = ".//div[contains(@class,'selected')]/div[contains(@class,'x-picker-item')]/../following-sibling::div[1]";
        //private List<IWebElement> mItems;

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue"})]
        public void Select(String Value)
        {
            try
            {
                Initialize();

                if (DlkEnvironment.mIsMobile)
                {
                    List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                    bool bToBeSelectedFound = false;
                    IWebElement mTarget = null;

                    for (int i = 0; i < mItems.Count; i++)
                    {
                        var pickerItem = mItems[i].FindElement(By.TagName("div"));
                        if (pickerItem.Text.Trim() == Value)
                        {
                            mTarget = mItems[i];
                            bToBeSelectedFound = true;
                            break;
                        }
                    }
                    if (!bToBeSelectedFound)
                    {
                        throw new Exception("Unable to find value '" + Value + "'.");
                    }

                    DlkBaseControl mTargetItem = new DlkBaseControl("Selected", mTarget);

                    /* scroll into view */
                    mTargetItem.ScrollIntoViewUsingJavaScript();

                    /* Click with magic offset */
                    var offset = ClickOffsetInitialValue;
                    var currentSelectedText = string.Empty;
                    do
                    {
                        mTargetItem.Click(offset, offset);
                        offset += ClickOffsetInitialValue;
                        var selected = mItems.FirstOrDefault(x => x.GetAttribute("class").Contains("selected"));
                        currentSelectedText = selected == null ? string.Empty : selected.FindElement(By.TagName("div")).Text;
                    }
                    while (currentSelectedText != Value && offset <= ClickOffsetMaxValue);
                }
                else
                {
                    DlkLogger.LogInfo("Picker control accessed in browser. This control is touch action specific and requires swipe. Framework will replace swipe action with series of click actions.");
                    Thread.Sleep(1000);
                    List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                    bool bToBeSelectedFound = false;
                    bool bCurrentSelectedFound = false;
                    int iCurrentIndex = 0;
                    int iToBeSelectedIndex = 0;
                    for (int i = 0; i < mItems.Count; i++)
                    {
                        if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML") == Value)
                        {
                            iToBeSelectedIndex = i;
                            bToBeSelectedFound = true;
                        }
                        if (mItems[i].GetAttribute("class").Contains("x-item-selected"))
                        {
                            iCurrentIndex = i;
                            bCurrentSelectedFound = true;
                        }
                        if (bToBeSelectedFound && bCurrentSelectedFound)
                        {
                            break;
                        }
                    }
                    if (!bToBeSelectedFound)
                    {
                        throw new Exception("Select() failed. Unable to find value '" + Value + "'");
                    }
                    int iDiff = iToBeSelectedIndex - iCurrentIndex;
                    if (iDiff < 0)
                    {
                        for (int i = iDiff; i < 0; i++)
                        {
                            IWebElement previousElement = mElement.FindElement(By.XPath(mPreviousItemXPath));
                            DlkBaseControl previousItem = new DlkBaseControl("Previous", previousElement);
                            previousItem.Click();
                            Thread.Sleep(1000);
                        }
                    }
                    else if (iDiff > 0)
                    {
                        for (int i = iDiff; i > 0; i--)
                        {
                            IWebElement nextElement = mElement.FindElement(By.XPath(mNextItemXPath));
                            DlkBaseControl nextItem = new DlkBaseControl("Next", nextElement);
                            nextItem.Click();
                            Thread.Sleep(1000);
                        }
                    }


                    //Refresh the selected item control
                    Initialize();
                    IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                    DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                    ctlActualSelected.Click();
                }
                DlkLogger.LogInfo("Select() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectByIndex(String ZeroBasedItemIndex)
        {
            try
            {
                Initialize();
                int index = 0;
                if (!int.TryParse(ZeroBasedItemIndex, out index))
                {
                    throw new Exception("Invalid index'" + ZeroBasedItemIndex + "'.");
                }
                if (DlkEnvironment.mIsMobile)
                {
                    List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                    string textOfTarget = string.Empty;
                    if (index < 0 || index > mItems.Count - 1)
                    {
                        throw new Exception("Invalid index'" + ZeroBasedItemIndex + "'.");
                    }
                    IWebElement mTarget = null;

                    var pickerItems = mItems[index].FindElements(By.TagName("div"));
                    if (pickerItems.Any())
                    {
                        mTarget = mItems[index];
                        textOfTarget = pickerItems.First().Text;
                    }
                    else
                    {
                        throw new Exception("Index '" + ZeroBasedItemIndex + "' out bounds of picker items '" + mItems.Count + "'.");
                    }

                    DlkBaseControl mTargetItem = new DlkBaseControl("Selected", mTarget);

                    /* scroll into view */
                    mTargetItem.ScrollIntoViewUsingJavaScript();

                    /* Click with magic offset */
                    var offset = ClickOffsetInitialValue;
                    var currentSelectedText = string.Empty;
                    do
                    {
                        mTargetItem.Click(offset, offset);
                        offset += ClickOffsetInitialValue;
                        var selected = mItems.FirstOrDefault(x => x.GetAttribute("class").Contains("selected"));
                        currentSelectedText = selected == null ? string.Empty : selected.FindElement(By.TagName("div")).Text;
                    }
                    while (currentSelectedText != textOfTarget && offset <= ClickOffsetMaxValue);
                }
                else
                {
                    DlkLogger.LogInfo("Picker control accessed in browser. This control is touch action specific and requires swipe. Framework will replace swipe action with series of click actions.");
                    Thread.Sleep(1000);
                    List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                    bool bToBeSelectedFound = false;
                    int iCurrentIndex = 0;
                    int iToBeSelectedIndex = 0;

                    if (!string.IsNullOrEmpty(mItems[index].FindElement(By.CssSelector("div")).GetAttribute("innerHTML")))
                        {
                            iToBeSelectedIndex = index;
                            bToBeSelectedFound = true;
                        }
                    if (mItems[index].GetAttribute("class").Contains("x-item-selected"))
                        {
                            iCurrentIndex = index;
                        }
                       
                    if (!bToBeSelectedFound)
                    {
                        throw new Exception("Select() failed. Unable to find value of index '" + ZeroBasedItemIndex + "'");
                    }
                    int iDiff = iToBeSelectedIndex - iCurrentIndex;
                    if (iDiff < 0)
                    {
                        for (int i = iDiff; i < 0; i++)
                        {
                            IWebElement previousElement = mElement.FindElement(By.XPath(mPreviousItemXPath));
                            DlkBaseControl previousItem = new DlkBaseControl("Previous", previousElement);
                            previousItem.Click();
                            Thread.Sleep(1000);
                        }
                    }
                    else if (iDiff > 0)
                    {
                        for (int i = iDiff; i > 0; i--)
                        {
                            IWebElement nextElement = mElement.FindElement(By.XPath(mNextItemXPath));
                            DlkBaseControl nextItem = new DlkBaseControl("Next", nextElement);
                            nextItem.Click();
                            Thread.Sleep(1000);
                        }
                    }


                    //Refresh the selected item control
                    Initialize();
                    IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                    DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                    ctlActualSelected.Click();
                }
                DlkLogger.LogInfo("SelectByIndex() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue.ToLower(), ctlActualSelected.GetValue().ToLower());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ctlActualSelected.GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                string actual = "";
                string expected = "";

                // process expected
                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                foreach (IWebElement elm in mItems)
                {

                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    if (ctl.GetValue().Contains("x-picker"))
                    {
                        IWebElement elem = elm.FindElement(By.XPath(".//div[contains(@class,'x-picker-item')]"));
                        ctl = new DlkBaseControl("Item", elem);
                    }
                    actual += DlkString.ReplaceCarriageReturn(ctl.GetValue().TrimEnd(), "") + "~";
                }
                actual = actual.Trim('~');

                DlkAssert.AssertEqual("VerifyList() ", expected.ToLower(), actual.ToLower());
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyItemInList(String Value)
        {
            try
            {
                Initialize();
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                bool bFound = false;
                foreach (IWebElement elm in mItems)
                {

                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    if (ctl.GetValue().Contains("x-picker"))
                    {
                        IWebElement elem = elm.FindElement(By.XPath(".//div[contains(@class,'x-picker-item')]"));
                        ctl = new DlkBaseControl("Item", elem);
                    }
                    string actualValue = ctl.GetValue().Trim();
                    if (Value == actualValue)
                    {
                        DlkLogger.LogInfo("VerifyItemInList() : List item found [" + Value + "]");
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("VerifyItemInList () : Item not found [" + Value + "]");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
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

        [Keyword("AssignItemValueByIndexToVariable", new string[] { "1|text|expected value|true" })]
        public void AssignItemValueByIndexToVariable(String Index, String VariableName)
        {
            Initialize();
            int index = 0;
            //guard clauses
            if (string.IsNullOrWhiteSpace(Index)) throw new ArgumentException("Index must not be null or empty");
            if (!int.TryParse(Index, out index)) throw new ArgumentException("Index must be a number");
            if (index < 0) throw new ArgumentException("Index must not be less than zero");

            if (DlkEnvironment.mIsMobile)
            {
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                if (index < 0 || index > mItems.Count - 1)
                {
                    throw new Exception("Invalid index'" + Index + "'.");
                }
                IWebElement mTarget = null;

                var pickerItems = mItems[index].FindElements(By.TagName("div"));
                if (pickerItems.Any())
                {
                    mTarget = mItems[index];
                    var textOfTarget = pickerItems.First().Text;
                    DlkVariable.SetVariable(VariableName, textOfTarget);
                    DlkLogger.LogInfo("AssignItemValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + textOfTarget + "].");
                }
                else
                {
                    throw new Exception("Index '" + index + "' out bounds of picker items '" + mItems.Count + "'.");
                }
            }
            else
            {
                DlkLogger.LogInfo("Picker control accessed in browser. This control is touch action specific and requires swipe. Framework will replace swipe action with series of click actions.");
                Thread.Sleep(1000);
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                bool bToBeSelectedFound = false;
                bool bCurrentSelectedFound = false;
                int iCurrentIndex = 0;
                int iToBeSelectedIndex = 0;
                for (int i = 0; i < mItems.Count; i++)
                {
                    if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML") == Index)
                    {
                        iToBeSelectedIndex = i;
                        bToBeSelectedFound = true;
                    }
                    if (mItems[i].GetAttribute("class").Contains("x-item-selected"))
                    {
                        iCurrentIndex = i;
                        bCurrentSelectedFound = true;
                    }
                    if (bToBeSelectedFound && bCurrentSelectedFound)
                    {
                        break;
                    }
                }
                if (!bToBeSelectedFound)
                {
                    throw new Exception("Select() failed. Unable to find value '" + Index + "'");
                }
                int iDiff = iToBeSelectedIndex - iCurrentIndex;
                if (iDiff < 0)
                {
                    for (int i = iDiff; i < 0; i++)
                    {
                        IWebElement previousElement = mElement.FindElement(By.XPath(mPreviousItemXPath));
                        DlkBaseControl previousItem = new DlkBaseControl("Previous", previousElement);
                        previousItem.Click();
                        Thread.Sleep(1000);
                    }
                }
                else if (iDiff > 0)
                {
                    for (int i = iDiff; i > 0; i--)
                    {
                        IWebElement nextElement = mElement.FindElement(By.XPath(mNextItemXPath));
                        DlkBaseControl nextItem = new DlkBaseControl("Next", nextElement);
                        nextItem.Click();
                        Thread.Sleep(1000);
                    }
                }
                //Refresh the selected item control
                Initialize();
                IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                DlkLogger.LogInfo("Select() successfully executed.");
                /*
                 * 1. Select Item
                 * 2. Get Item's Value
                 * 3. Store Value To Variable
                 */
                String mValue = selectedItem.Text;
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignItemValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
            }
           
        }
    }
}
