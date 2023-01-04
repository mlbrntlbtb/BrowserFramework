using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace TrafficLiveLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region FIELDS
        private string mMonthPickerXPath = ".//div[not(contains(@class,'mask')) and contains(@class,'x-container x-dataview')][1]";
        private string mDayPickerXPath = ".//div[not(contains(@class,'mask')) and contains(@class,'x-container x-dataview')][2]";
        private string mYearPickerXPath = ".//div[not(contains(@class,'mask')) and contains(@class,'x-container x-dataview')][3]";
        private DlkPicker mFirstPickerColumn;
        private DlkPicker mSecondPickerColumn;
        private DlkPicker mThirdPickerColumn;
        private Boolean bThereAreThreePickers = true;
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            InitializePickerColumns();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        public void InitializePickerColumns()
        {
            /* Check if which xpath search string will work */
            if (mElement.FindElements(By.XPath(mMonthPickerXPath)).Count() > 0) /* Use 'Left' justified */
            {
                mFirstPickerColumn = new DlkPicker("MonthPicker", mElement.FindElement(By.XPath(mMonthPickerXPath)));
                mSecondPickerColumn = new DlkPicker("DayPicker", mElement.FindElement(By.XPath(mDayPickerXPath)));
                try
                {
                    mThirdPickerColumn = new DlkPicker("YearPicker", mElement.FindElement(By.XPath(mYearPickerXPath)));
                }
                catch
                {
                    bThereAreThreePickers = false;
                }
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("SetDate", new String[] { "1|text|Value|SampleValue" })]
        public void SetDate(String MonthPicker, String DayPicker, String YearPicker)
        {
            try
            {
                Initialize();

                mFirstPickerColumn.Select(MonthPicker);
                mSecondPickerColumn.Select(DayPicker);
                if (bThereAreThreePickers) // because the mobile version only has two pickers and the web version has three pickers.
                {
                    mThirdPickerColumn.Select(YearPicker);
                }

                //Refresh the selected item control
                Initialize();

                DlkLogger.LogInfo("SetDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyContainValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyContainValue(String FirstPickerValue, String SecondPickerValue, String ThirdPickerValue)
        {
            try
            {
                Initialize();
                mFirstPickerColumn.VerifyContainValue(FirstPickerValue, "true");
                mSecondPickerColumn.VerifyContainValue(SecondPickerValue, "true");
                mThirdPickerColumn.VerifyContainValue(ThirdPickerValue, "true");

                DlkLogger.LogInfo("VerifyContainValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyContainValue() failed : " + e.Message, e);
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

        #endregion

    }
}
