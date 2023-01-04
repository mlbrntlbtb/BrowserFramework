#define NATIVE_MAPPING
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Drawing;

namespace CPTouchLib.DlkControls
{
    [ControlType("Picker")]
    public class DlkPicker : DlkMobileControl
    {
        public enum PickerType
        {
            Default,
            Hours,
            Date
        }

        private const int INT_MAX_SELECTION_ATTEMPT = 10;
        public DlkPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public List<DlkMobileControl> mItems = new List<DlkMobileControl>();
        private DlkMobileControl mDone = null;
        private DlkMobileControl mCancel = null;
        private DlkMobileControl mSelectedIndicator = null;
        private DlkMobileControl mPickerSlot = null;

        [Keyword("Cancel", new String[] { "1|text|Value|SampleValue" })]
        public void Cancel()
        {
            try
            {
                Initialize();
                mCancel.FindElement();
                mCancel.Tap();
                DlkLogger.LogInfo("Cancel() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Cancel() failed : " + e.Message, e);
            }
        }

        public void Select(String TextToSelect, PickerType KindOfPicker)
        {
            try
            {
                Initialize(KindOfPicker);
                foreach (var itm in mItems)
                {
                    if (itm.mElement.Text == TextToSelect)
                    {
                        var currAttempt = 0;
#if NATIVE_MAPPING
                        do
                        {
                            DlkLogger.LogInfo("Attempt to select #" + ++currAttempt);
                            itm.Tap();
                        }
                        while (!IsMarginallyEqual(mSelectedIndicator.mElement.Location.Y, itm.mElement.Location.Y)
                            && currAttempt < mItems.Count);
#else
                        Point itemLoc = new Point();
                        do
                        {
                            DlkLogger.LogInfo("Attempt to select #" + ++currAttempt);
                            itemLoc = itm.mElement.Location;//get item location first because the element goes stale after tapping
                            itm.Tap();
                            if (!mSelectedIndicator.Exists(1))//check for selected item first
                                throw new Exception("No item was selected.");
                        }
                        while (!IsMarginallyEqual(mSelectedIndicator.mElement.Location.Y, itemLoc.Y)
                            && currAttempt < mItems.Count);
#endif
                        if (currAttempt == mItems.Count)
                        {
                            DlkLogger.LogWarning(mItems.Count +
                                " selection attempts reached.. Wrong item might have been selected.");
                        }
                        if (KindOfPicker == PickerType.Default)
                        {
                            mDone.Tap();
                        }
                        DlkLogger.LogInfo("Successfully executed Select().");
                        return;
                    }
                }
                throw new Exception("Item '" + TextToSelect + "' not found");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String TextToSelect)
        {
            Select(TextToSelect, PickerType.Default);
        }


        [Keyword("SelectByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectByIndex(String OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mItems.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }

                DlkMobileControl mTarget = mItems[index - 1];
                var currAttempt = 0;
#if NATIVE_MAPPING
                do
                {
                    DlkLogger.LogInfo("Attempt to select #" + ++currAttempt);
                    mTarget.Tap();
                }
                while (mSelectedIndicator.mElement.Location.Y != mTarget.mElement.Location.Y 
                    && currAttempt < INT_MAX_SELECTION_ATTEMPT);
#else
                Point itemLoc = new Point();
                do
                {
                    DlkLogger.LogInfo("Attempt to select #" + ++currAttempt);
                    itemLoc = mTarget.mElement.Location;
                    mTarget.Tap();

                    if (!mSelectedIndicator.Exists(1))//check for highlighted item first.
                        throw new Exception("No item was selected.");
                }
                while (mSelectedIndicator.mElement.Location.Y != itemLoc.Y
                    && currAttempt < INT_MAX_SELECTION_ATTEMPT);
#endif
                if (currAttempt == INT_MAX_SELECTION_ATTEMPT)
                {
                    DlkLogger.LogWarning(INT_MAX_SELECTION_ATTEMPT + 
                        " selection attempts reached.. Wrong item might have been selected.");
                }

                mDone.Tap();
                DlkLogger.LogInfo("SelectByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemCount")]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                DlkAssert.AssertEqual("VerifyItemCount", expected, mItems.Count);
                DlkLogger.LogInfo("Successfully executed VerifyItemCount().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItems")]
        public void VerifyItems(string TildeBoundItems)
        {
            try
            {
                Initialize();
                string actual = string.Empty;
                try
                {
                    actual = string.Join("~", mItems.Select(x => x.mElement.Text));
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected control error encountered: " + e.Message);
                }
                DlkAssert.AssertEqual("VerifyItems", TildeBoundItems, actual);
                DlkLogger.LogInfo("Successfully executed VerifyItems().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        public void Initialize(PickerType KindOfPicker = PickerType.Default)
        {
            FindElement();
            mItems = GetItems(KindOfPicker);
            GetPickerControls(KindOfPicker);
        }

        private List<DlkMobileControl> GetItems(PickerType KindOfPicker)
        {
            List<DlkMobileControl> ret = new List<DlkMobileControl>();

            /* Actual picker list */
#if NATIVE_MAPPING
            DlkMobileControl lstComponent = new DlkMobileControl("Component", "XPATH", 
                "(" + this.mSearchValues.First() + "//*[contains(@resource-id,'component')])" + ((KindOfPicker == PickerType.Default || KindOfPicker == PickerType.Date) ? string.Empty : "[2]"));
#else
            DlkMobileControl lstComponent = new DlkMobileControl("Component", "XPATH_DISPLAY", 
               "(" + this.mSearchValues.First() + "//*[contains(@id,'component')])" + (KindOfPicker == PickerType.Default ? string.Empty : "[2]"));
#endif
            if (!lstComponent.Exists())
            {
                throw new Exception("Unexpected control error encountered: Component not found");
            }
#if NATIVE_MAPPING
            var itemContainer = lstComponent.mElement.FindElements(By.XPath("./*[1]/*/*/*"));
#else
            var itemContainer = lstComponent.mElement.FindElements(By.XPath(".//*[contains(@class, 'dataview-item')]"));
#endif
            if (itemContainer.Any())
            {
                itemContainer.ToList().ForEach(x => ret.Add(new DlkMobileControl("item", x)));
            }
#if NATIVE_MAPPING
            return ret;
#else
            if(DlkEnvironment.mBrowser.ToLower() == "ios")
            {
                ret.Last().ScrollIntoViewUsingJavaScript();
                itemContainer = lstComponent.mElement.FindElements(By.XPath(".//*[contains(@class, 'dataview-item')]"));//reinitialize list to get updated elements
                if (itemContainer.Any())
                {
                    ret.Clear();//clear previous picker list
                    itemContainer.ToList().ForEach(x => ret.Add(new DlkMobileControl("item", x)));
                }
            }
            return ret;
#endif
        }

        private void GetPickerControls(PickerType KindOfPicker)
        {
            if (KindOfPicker == PickerType.Default)
            {
#if NATIVE_MAPPING
                mCancel = new DlkMobileControl("Cancel", "XPATH", "("
                    + this.mSearchValues.First() + "//*[contains(@resource-id,'button')])[1]");

                mDone = new DlkMobileControl("Accept", "XPATH", "("
                    + this.mSearchValues.First() + "//*[contains(@resource-id,'button')])[2]");
                mDone.FindElement();

                mPickerSlot = new DlkMobileControl("PickerSlot", "XPATH",
                    this.mSearchValues.First() + "//*[contains(@resource-id,'pickerslot')]");
                mSelectedIndicator = new DlkMobileControl("Indicator", "XPATH", "("
                + mPickerSlot.mSearchValues.First() + "/../*/*)[1]");
                mSelectedIndicator.FindElement();
#else
                mCancel = new DlkMobileControl("Cancel", "XPATH_DISPLAY", "("
                    + this.mSearchValues.First() + "//*[contains(@id,'button')])[1]");

                mDone = new DlkMobileControl("Accept", "XPATH_DISPLAY", "("
                    + this.mSearchValues.First() + "//*[contains(@id,'button')])[2]");
                mDone.FindElement();

                mPickerSlot = new DlkMobileControl("PickerSlot", "XPATH_DISPLAY",
                    this.mSearchValues.First() + "//*[contains(@id,'pickerslot')]");
                mSelectedIndicator = new DlkMobileControl("Indicator", "XPATH", mPickerSlot.mSearchValues.First() + "//*[contains(@class, 'selected')]");
#endif
            }
            else
            {
#if NATIVE_MAPPING
                mSelectedIndicator = new DlkMobileControl("Indicator", "XPATH", "("
                    + this.mSearchValues.First() + "/../*/*)[1]");
            mSelectedIndicator.FindElement();
#else
                mSelectedIndicator = new DlkMobileControl("Indicator", "XPATH_DISPLAY", this.mSearchValues.First() + "//*[contains(@class, 'selected')]");
#endif
            }
        }

        private bool IsMarginallyEqual(int value1, int value2)
        {
            int margin = 10;
            return (Math.Abs(value1 - value2) <= margin);
        }
    }
}
