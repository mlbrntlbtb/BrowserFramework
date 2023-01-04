#define NATIVE_MAPPING
using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("HoursPicker")]
    public class DlkHoursPicker : DlkMobilePicker
    {
        public DlkHoursPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkHoursPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkHoursPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private DlkMobileControl mDone = null;
        private DlkMobileControl mCancel = null;
        //private DlkMobileControl mSelectedIndicator = null;
        //private DlkMobileControl mPickerSlot = null;
        private DlkPicker mPlusMinusPicker = null;
        private DlkPicker mHoursPicker = null;
        private DlkPicker mFractionalPicker = null;

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

        [Keyword("Set")]
        public void Set(string PlusMinus, string Hours, string Fractional)
        {
            try
            {
                Initialize();
                mPlusMinusPicker.Select(PlusMinus, DlkPicker.PickerType.Hours);
                mHoursPicker.Select(Hours, DlkPicker.PickerType.Hours);
                mFractionalPicker.Select(Fractional, DlkPicker.PickerType.Hours);
                mDone.Tap();
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyFractionalIncrement")]
        public void VerifyFractionalIncrement(string ExpectedIncrement)
        {
            try
            {
                Initialize();
                mFractionalPicker.Initialize(DlkPicker.PickerType.Hours);
                if (mFractionalPicker.mItems.Count < 2)
                {
                    throw new Exception("Cannot determine fractional increment if item count is 1 or less.");
                }
                int iExpected;
                if (!int.TryParse(ExpectedIncrement, out iExpected) || iExpected < 0)
                {
                    throw new Exception("Invalid ExpectedIncrement: '" + ExpectedIncrement + "'");
                }
                int iItem1;
                int iItem2;
                if (!int.TryParse(mFractionalPicker.mItems[0].mElement.Text, out iItem1)
                    || !int.TryParse(mFractionalPicker.mItems[1].mElement.Text, out iItem2))
                {
                    throw new Exception("Cannot detemine value of picker items");
                }
                DlkAssert.AssertEqual("VerifyFractionalIncrement()", iExpected, (iItem2 - iItem1));
                DlkLogger.LogInfo("VerifyFractionalIncrement() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFractionalIncrement() failed : " + e.Message, e);
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

        private void Initialize()
        {
            FindElement();
            GetPickerControls();
        }

        private void GetPickerControls()
        {
#if NATIVE_MAPPING
            mCancel = new DlkMobileControl("Cancel", "XPATH", "("
                + this.mSearchValues.First() + "//*[contains(@resource-id,'button')])[1]");

            mDone = new DlkMobileControl("Accept", "XPATH", "("
                + this.mSearchValues.First() + "//*[contains(@resource-id,'button')])[2]");
            mDone.FindElement();

            //mPickerSlot = new DlkMobileControl("PickerSlot", "XPATH",
            //    this.mSearchValues.First() + "//*[contains(@resource-id,'pickerslot')]");

            //mSelectedIndicator = new DlkMobileControl("Indicator", "XPATH", "("
            //    + mPickerSlot.mSearchValues.First() + "/../*/*)[1]");
            //mSelectedIndicator.FindElement();

            mPlusMinusPicker = new DlkPicker("PlusMinus", "XPATH", 
                this.mSearchValues.First() + "//*[contains(@resource-id,'ext-pickerslot-1')]");
            mPlusMinusPicker.FindElement();

            mHoursPicker = new DlkPicker("Hours", "XPATH",
                this.mSearchValues.First() + "//*[contains(@resource-id,'ext-pickerslot-2')]");
            mHoursPicker.FindElement();

            mFractionalPicker = new DlkPicker("Fractional", "XPATH",
                this.mSearchValues.First() + "//*[contains(@resource-id,'ext-pickerslot-3')]");
            mFractionalPicker.FindElement();
#else
            mCancel = new DlkMobileControl("Cancel", "XPATH_DISPLAY", this.mSearchValues.First() + "//*[contains(@class, 'titlebar-left')]/child::*[1]");

            mDone = new DlkMobileControl("Accept", "XPATH", this.mSearchValues.First() + "//*[contains(@class, 'titlebar-right')]/child::*[1]");
            mDone.FindElement();

            mPlusMinusPicker = new DlkPicker("PlusMinus", "XPATH",
               this.mSearchValues.First() + "//*[contains(@id,'ext-pickerslot-')][1]");

            mPlusMinusPicker.FindElement();

            mHoursPicker = new DlkPicker("Hours", "XPATH",
                this.mSearchValues.First() + "//*[contains(@id,'ext-pickerslot-')][2]");
            mHoursPicker.FindElement();

            mFractionalPicker = new DlkPicker("Fractional", "XPATH",
                this.mSearchValues.First() + "//*[contains(@id,'ext-pickerslot-')][3]");
            mFractionalPicker.FindElement();
#endif
        }
    }

    public class HoursUnit
    {
        public string PlusMinus { get; set; }
        public string Hours { get; set; }
        public string Fractional { get; set; }

        private string mTextForm = string.Empty;


        public HoursUnit(string UnitInTextForm)
        {
            mTextForm = UnitInTextForm;
            Initialize();
        }

        public HoursUnit()
        {
            Initialize();
        }

        private void Initialize()
        {
            PlusMinus = GetPlusMinus();
            Hours = "0";
            Fractional = "00";
            var arrInput = mTextForm.Split('.');
            if (arrInput.Count() == 2)
            {
                Hours = GetHours(arrInput.First());
                Fractional = GetFractional(arrInput.Last());
            }
        }

        private string GetPlusMinus()
        {
            return mTextForm[0] == '-' ? "-" : "+";
        }

        private string GetHours(string InputHours)
        {
            string ret = "0";
            if (InputHours[0] == '+' || InputHours[0] == '-')
            {
                InputHours = InputHours.Substring(1);
            }
            int iHours;
            if (int.TryParse(InputHours, out iHours) && iHours >= 0 && iHours < 24)
            {
                ret = InputHours;
            }
            return ret;
        }

        private string GetFractional(string InputFractional)
        {
            string ret = "00";
            int iFractional;
            if (InputFractional.Length == 2 &&
                int.TryParse(InputFractional, out iFractional) 
                && iFractional >= 0 && iFractional < 100)
            {
                ret = InputFractional;
            }
            return ret;
        }
    }
}
