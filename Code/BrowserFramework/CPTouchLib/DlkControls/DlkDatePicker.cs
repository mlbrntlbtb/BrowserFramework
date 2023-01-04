#define NATIVE_MAPPING
using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkMobileControl
    {
        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private DlkMobileControl mDone = null;
        private DlkMobileControl mCancel = null;
        private DlkPicker mMonthPicker = null;
        private DlkPicker mDayPicker = null;
        private DlkPicker mYearPicker = null;

        public void Initialize()
        {
            FindElement();
            GetPickerControls();
        }

        [Keyword("Cancel")]
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

        [Keyword("Done")]
        public void Done()
        {
            try
            {
                Initialize();
                mDone.FindElement();
                mDone.Tap();
                DlkLogger.LogInfo("Done() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Done() failed : " + e.Message, e);
            }
        }

        [Keyword("Set", new String[] { "1|text|Month|Value", "2|text|Date|Value", "3|text|Year|Value" })]
        public void Set(string Month, string Day, string Year)
        {
            try
            {
                Initialize();
                mMonthPicker.Select(Month, DlkPicker.PickerType.Date);
                mDayPicker.Select(Day, DlkPicker.PickerType.Date);
                mYearPicker.Select(Year, DlkPicker.PickerType.Date);
                mDone.Tap();
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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


        private void GetPickerControls()
        {
#if NATIVE_MAPPING
            mCancel = new DlkMobileControl("Cancel", "XPATH_DISPLAY", "("
                + this.mSearchValues.First() + "//*[contains(@resource-id,'button')])[1]");

            mDone = new DlkMobileControl("Accept", "XPATH_DISPLAY", "("
                + this.mSearchValues.First() + "//*[contains(@resource-id,'button')])[2]");
            mDone.FindElement();

            mMonthPicker = new DlkPicker("Month", "XPATH_DISPLAY", 
                this.mSearchValues.First() + "//*[contains(@resource-id,'ext-pickerslot')][1]");
            mMonthPicker.FindElement();

            mDayPicker = new DlkPicker("Day", "XPATH",
                this.mSearchValues.First() + "//*[contains(@resource-id,'ext-pickerslot')][2]");
            mDayPicker.FindElement();

            mYearPicker = new DlkPicker("Year", "XPATH",
                this.mSearchValues.First() + "//*[contains(@resource-id,'ext-pickerslot')][3]");
            mYearPicker.FindElement();
#else
             mCancel = new DlkMobileControl("Cancel", "XPATH_DISPLAY", "("
                + this.mSearchValues.First() + "//*[contains(@id,'button')])[1]");

            mDone = new DlkMobileControl("Accept", "XPATH_DISPLAY", "("
                + this.mSearchValues.First() + "//*[contains(@id,'button')])[2]");
            mDone.FindElement();

            mMonthPicker = new DlkPicker("Month", "XPATH_DISPLAY", 
                this.mSearchValues.First() + "//*[contains(@id,'ext-pickerslot')][1]");
            mMonthPicker.FindElement();

            mDayPicker = new DlkPicker("Day", "XPATH",
                this.mSearchValues.First() + "//*[contains(@id,'ext-pickerslot')][2]");
            mDayPicker.FindElement();

            mYearPicker = new DlkPicker("Year", "XPATH",
                this.mSearchValues.First() + "//*[contains(@id,'ext-pickerslot')][3]");
            mYearPicker.FindElement();
#endif
        }
    }
}
