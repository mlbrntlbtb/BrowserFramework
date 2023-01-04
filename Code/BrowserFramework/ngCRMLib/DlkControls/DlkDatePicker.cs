using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace ngCRMLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        private IWebElement mCalendarComponent = null;
        private IWebElement mHeaderComponent = null;
        private IWebElement mPrev = null;
        private IWebElement mNext = null;

        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            {
                FindElement();
                mHeaderComponent = mElement.FindElement(By.ClassName("ui-datepicker-header"));
                mCalendarComponent = mElement.FindElement(By.ClassName("ui-datepicker-calendar"));
                mPrev = mElement.FindElement(By.XPath(".//a[@title='Prev']"));
                mNext = mElement.FindElement(By.XPath(".//a[@title='Next']"));
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

        [Keyword("SetDate", new String[] { "1|text|Date to set|XXX" })]
        public void SetDate(String Date)
        {
            try
            {
                // set month and year
                DateTime convertedDate = DateTime.Parse(Date);
                int targetMonth = convertedDate.Month;
                int targetYear = convertedDate.Year;

                Initialize();
                // actual
                //string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                //int actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                string strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-year')]/option[@selected='selected']"))).GetValue();
                int actualYear = int.Parse(strActualYear);
                while (targetYear != actualYear) // year is not yet set
                {
                    //while(targetMonth != actualMonth)
                    {
                        Initialize();
                        IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                        myButton.Click();
                        Initialize();
                        //strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                        //actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                        strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-year')]/option[@selected='selected']"))).GetValue();
                        actualYear = int.Parse(strActualYear);
                    }
                }
                Initialize();
                string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-month')]/option[@selected='selected']"))).GetValue();
                int actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                while (targetMonth != actualMonth)
                {
                    {
                        Initialize();
                        IWebElement myButton = targetMonth > actualMonth ? mNext : mPrev; 
                        myButton.Click();
                        Initialize();
                        strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-month')]/option[@selected='selected']"))).GetValue();
                        actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                    }
                }

                // Click on Day
                IWebElement targetDay = mCalendarComponent.FindElement(By.XPath("./descendant::a[text()='" + convertedDate.Day.ToString() + "'][not(contains(@class,'secondary'))]"));
                targetDay.Click();
                DlkLogger.LogInfo("SetDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }
        }
    }
}
