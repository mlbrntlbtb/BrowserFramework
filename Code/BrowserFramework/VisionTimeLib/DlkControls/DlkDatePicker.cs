using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace VisionTimeLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        //private string mItemsXPath = ".//div[contains(@class, 'x-dataview-item')]";
        //private string mSelectedItemXPath = ".//div[contains(@class,'x-item-selected')]/div[contains(@class,'x-picker-item')]";
        private string mMonthPickerXPath = ".//div[contains(@class,'x-picker-right')]";
        private string mDayPickerXPath = ".//div[contains(@class,'x-picker-center')][1]";
        private string mYearPickerXPath = ".//div[contains(@class,'x-picker-center')][2]";
        //private List<IWebElement> mItems;

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        [Keyword("SetDate", new String[] { "1|text|Value|SampleValue" })]
        public void SetDate(String Month, String Day, String Year)
        {
            try
            {
                Initialize();
                IWebElement mMonth = mElement.FindElement(By.XPath(mMonthPickerXPath));
                DlkPicker pkMonth = new DlkPicker("MonthPicker", mMonth);
                pkMonth.Select(Month);

                IWebElement mDay = mElement.FindElement(By.XPath(mDayPickerXPath));
                DlkPicker pkDay = new DlkPicker("DayPicker", mDay);
                pkDay.Select(Day);

                IWebElement mYear = mElement.FindElement(By.XPath(mYearPickerXPath));
                DlkPicker pkYear = new DlkPicker("YearPicker", mYear);
                pkYear.Select(Year);

                //Refresh the selected item control
                Initialize();

                //this.VerifyValues(Month, Day,Year);
                DlkLogger.LogInfo("SetDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyValues", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValues(String Month, String Day, String Year)
        {
            try
            {
                Initialize();
                IWebElement mMonth = mElement.FindElement(By.XPath(mMonthPickerXPath));
                DlkPicker pkMonth = new DlkPicker("MonthPicker", mMonth);
                pkMonth.VerifyValue(Month);

                IWebElement mDay = mElement.FindElement(By.XPath(mDayPickerXPath));
                DlkPicker pkDay = new DlkPicker("DayPicker", mDay);
                pkDay.VerifyValue(Day);

                IWebElement mYear = mElement.FindElement(By.XPath(mYearPickerXPath));
                DlkPicker pkYear = new DlkPicker("YearPicker", mYear);
                pkYear.VerifyValue(Year);
                //IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                //DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                //DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ctlActualSelected.GetValue());
                DlkLogger.LogInfo("VerifyValues() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValues() failed : " + e.Message, e);
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



    }
}
