using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyNavigatorLib.DlkControls
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
                mPrev = mElement.FindElement(By.ClassName("ui-datepicker-prev"));
                mNext = mElement.FindElement(By.ClassName("ui-datepicker-next"));
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

        /// <summary>
        /// This keyword selects a specific day after navigating to the specific month and year.
        /// </summary>
        /// <param name="Date"></param>
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
                string strActualYear = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-year"))).GetValue();
                int actualYear = int.Parse(strActualYear);
                while (targetYear != actualYear) // year is not yet set
                {
                    //while(targetMonth != actualMonth)
                    {
                        Initialize();
                        IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                        new DlkBaseControl("Button", myButton).ClickUsingJavaScript();
                        //myButton.Click();                        
                        Initialize();
                        //strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                        //actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                        strActualYear = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-year"))).GetValue();
                        actualYear = int.Parse(strActualYear);
                    }
                }
                Initialize();
                string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                int actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                while (targetMonth != actualMonth)
                {
                    {
                        Initialize();
                        IWebElement myButton = targetMonth > actualMonth ? mNext : mPrev;
                        new DlkBaseControl("Button", myButton).ClickUsingJavaScript();                        
                        //myButton.Click();
                        Initialize();
                        strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                        actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                    }
                }

                // Click on Day
                IWebElement targetDay = mCalendarComponent.FindElement(By.XPath("./descendant::a[text()='" + convertedDate.Day.ToString() + "' and not(contains(@class,'secondary'))]"));
                new DlkBaseControl("Button", targetDay).ClickUsingJavaScript();
                //targetDay.Click();
                DlkLogger.LogInfo("SetDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword navigates to previous/next month.
        /// </summary>
        /// <param name="NextOrPrevious">user input that accepts either "next" or "previous" only (not case sensitve)</param>
        [Keyword("NavigatePeriodsTo", new String[] { "1|text|NextOrPrevious|next" })]
        public void NavigatePeriodsTo(String NextOrPrevious)
        {
            try
            {
                var nextOrPrevious = NextOrPrevious.ToLower();
                if (nextOrPrevious == "next" || nextOrPrevious == "previous")
                {
                    Initialize();
                    IWebElement myButton = nextOrPrevious == "next" ? mNext : mPrev;
                    new DlkBaseControl("Button", myButton).ClickUsingJavaScript();
                }
                else
                {
                    throw new Exception("NavigatePeriodsTo() failed. Parameter only accepts 'next' or 'previous' as inputs.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("NavigatePeriodsTo() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This assign the month and year to variable.
        /// </summary>
        /// <param name="VariableName">name of variable to store the month and year</param>
        [Keyword("AssignMonthYearToVariable", new String[] { "1|text|NextOrPrevious|next" })]
        public void AssignMonthYearToVariable(String VariableName)
        {
            try
            {
                Initialize();
                string monthYear = new DlkBaseControl("MonthYear", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-title"))).GetValue();
                //sanitize string
                monthYear = DlkString.ReplaceCarriageReturn(monthYear, "").Trim();
                DlkVariable.SetVariable(VariableName, monthYear);
            }
            catch (Exception e)
            {
                throw new Exception("AssignMonthYearToVariable() failed : " + e.Message, e);
            }
        }

        public void VerifyStatus(String Date, String Status)
        {

        }
    }
}
