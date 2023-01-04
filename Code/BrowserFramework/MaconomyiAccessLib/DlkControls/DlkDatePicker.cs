using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Globalization;
using MaconomyiAccessLib.DlkSystem;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private string mDatePickerClass;
        private const String KENDO_CLASS = "kendo-calendar";
        private const String DX_CLASS = "devex";
        private IWebElement mCalendarComponent = null;
        private IWebElement mHeaderComponent = null;
        private IWebElement mPrev = null;
        private IWebElement mNext = null;
        private static String mWeekNumber_XPath = ".//td[@class='weeknumbercol'] | //td//span[contains(@class,'weeknumbercol')]";
        

        #endregion

        #region CONSTRUCTORS

        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
       {
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                mDatePickerClass = GetDatePickerClass();
                GetComponents();
            }
        }

        public void ClickUsingJavaScriptThenSelenium(IWebElement targetControl)
        {
            //Solution for Issues when using ClickUsingJavaScript function
            DlkBaseControl TargetControl = new DlkBaseControl("Target Control", targetControl);
            try
            {
                TargetControl.ClickUsingJavaScript();
            }
            catch (Exception)
            {
                targetControl.Click();
            }
        }

        public IList<IWebElement> GetWeekNumbers()
        {
            IList<IWebElement> mWeekNumbers = mElement.FindElements(By.XPath(mWeekNumber_XPath)).Count > 0 ?
                mElement.FindElements(By.XPath(mWeekNumber_XPath)).Where(x => x.Displayed).ToList() :
                throw new Exception("Week numbers not found.");

            return mWeekNumbers;
        }

        public void SetTargetDate(DateTime convertedDate)
        {
            int targetMonth = convertedDate.Month;
            int targetYear = convertedDate.Year;

            switch (mDatePickerClass)
            {
                case KENDO_CLASS: //Modify if under KENDO CLASS only

                    String dtInput = "";
                    IWebElement calendarTitle = null;
                    if (mElement.TagName.Contains("dm-date-picker"))
                    {
                        IWebElement datePickerButton = mElement.FindElements(By.TagName("a")).Count > 0 ?
                            mElement.FindElement(By.TagName("a")) : mElement.FindElement(By.TagName("i"));
                        new DlkBaseControl("TargetDate", datePickerButton).Click();
                        dtInput = new DlkBaseControl("targetDate", mElement.FindElement(By.TagName("input"))).GetValue();
                    }
                    else
                    {
                        mElement = mCalendarComponent;
                        calendarTitle = mElement.FindElement(By.XPath(".//*[contains(@class,'k-title')]"));
                        dtInput = new DlkBaseControl("targetDate", calendarTitle).GetValue();
                    }

                    String targetDate = convertedDate.ToString("MMMM d, yyyy");
                    String targetMonthYear = convertedDate.ToString("yyyy MMM");
                    int currentMonth;
                    int currentYear;
                    if (!String.IsNullOrEmpty(dtInput))
                    {
                        currentMonth = Convert.ToDateTime(dtInput).Month;
                        currentYear = Convert.ToDateTime(dtInput).Year;
                    }
                    else
                    {
                        currentMonth = DateTime.Now.Month;
                        currentYear = DateTime.Now.Year;
                    }

                    string xpathTargetDate = ".//tbody//td[contains(@title,'" + targetDate + "')]";
                    IWebElement kendoCalendarView = mCalendarComponent.FindElement(By.XPath(".//kendo-virtualization"));

                    while (mCalendarComponent.FindElements(By.XPath(xpathTargetDate)).Count == 0)
                    {
                        IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        if ((currentMonth > targetMonth && currentYear == targetYear) || (currentYear > targetYear))
                        {
                            try
                            {
                                jse.ExecuteScript("$('.k-scrollable').scrollTop($('.k-scrollable').scrollTop()-50);");
                            }
                            catch
                            {
                                jse.ExecuteScript("arguments[0].scrollTop -= 20", kendoCalendarView);
                            }
                        }
                        else if ((currentMonth < targetMonth && currentYear == targetYear) || (currentYear < targetYear))
                        {
                            try
                            {
                                jse.ExecuteScript("$('.k-scrollable').scrollTop($('.k-scrollable').scrollTop()+50);");
                            }
                            catch
                            {
                                jse.ExecuteScript("arguments[0].scrollTop += 20", kendoCalendarView);
                            }
                        }
                    }
                    IWebElement newDate = mCalendarComponent.FindElement(By.XPath(xpathTargetDate));
                    ClickUsingJavaScriptThenSelenium(newDate);
                    break;

                case DX_CLASS: //Modify if under DEVEX CLASS only

                    bool isCalendarMonthView = mCalendarComponent.GetAttribute("class").ToLower().Contains("fastnav");

                    int dx_expectedYear = convertedDate.Year;
                    int dx_expectedMonth = convertedDate.Month;

                    //Check calendar if month view or day view
                    if (!isCalendarMonthView) //DAY VIEW Calendar
                    {
                        DateTime dx_actualDate = DateTime.Parse(mHeaderComponent.Text.Trim());
                        int dx_actualYear = dx_actualDate.Year;
                        int dx_actualMonth = dx_actualDate.Month;

                        //Compare current year to target year
                        if (dx_actualYear != dx_expectedYear)
                        {
                            string dx_prevYearButton_XPath = ".//img[contains(@class,'CalendarPrevYear_DevEx')]";
                            string dx_nextYearButton_XPath = ".//img[contains(@class,'CalendarNextYear_DevEx')]";
                            IWebElement dx_prevYearButton = mHeaderComponent.FindElements(By.XPath(dx_prevYearButton_XPath)).Count > 0 ?
                                mHeaderComponent.FindElement(By.XPath(dx_prevYearButton_XPath)) : throw new Exception("Previous year button not found.");
                            IWebElement dx_nextYearButton = mHeaderComponent.FindElements(By.XPath(dx_nextYearButton_XPath)).Count > 0 ?
                                mHeaderComponent.FindElement(By.XPath(dx_nextYearButton_XPath)) : throw new Exception("Next year button not found.");

                            while (dx_actualYear != dx_expectedYear)
                            {
                                //Go to previous year
                                if (dx_actualYear > dx_expectedYear)
                                {
                                    DlkLogger.LogInfo("Clicking previous year button... ");
                                    dx_prevYearButton.Click();
                                    Thread.Sleep(1000);
                                }

                                //Go to next year
                                if (dx_actualYear < dx_expectedYear)
                                {
                                    DlkLogger.LogInfo("Clicking next year button... ");
                                    dx_nextYearButton.Click();
                                    Thread.Sleep(1000);
                                }

                                //Get current year
                                dx_actualYear = DateTime.Parse(mHeaderComponent.Text.Trim()).Year;
                            }
                        }
                        DlkLogger.LogInfo("Target year is equal to current year.");

                        //Compare current month to target month
                        if (dx_actualMonth != dx_expectedMonth)
                        {
                            string dx_prevMonthButton_XPath = ".//img[contains(@class,'CalendarPrevMonth_DevEx')]";
                            string dx_nextMonthButton_XPath = ".//img[contains(@class,'CalendarNextMonth_DevEx')]";
                            IWebElement dx_prevMonthButton = mHeaderComponent.FindElements(By.XPath(dx_prevMonthButton_XPath)).Count > 0 ?
                                mHeaderComponent.FindElement(By.XPath(dx_prevMonthButton_XPath)) : throw new Exception("Previous month button not found.");
                            IWebElement dx_nextMonthButton = mHeaderComponent.FindElements(By.XPath(dx_nextMonthButton_XPath)).Count > 0 ?
                                mHeaderComponent.FindElement(By.XPath(dx_nextMonthButton_XPath)) : throw new Exception("Next month button not found.");

                            while (dx_actualMonth != dx_expectedMonth)
                            {
                                //Go to previous month
                                if (dx_actualMonth > dx_expectedMonth)
                                {
                                    DlkLogger.LogInfo("Clicking previous month button... ");
                                    dx_prevMonthButton.Click();
                                    Thread.Sleep(1000);
                                }

                                //Go to next month
                                if (dx_actualMonth < dx_expectedMonth)
                                {
                                    DlkLogger.LogInfo("Clicking next month button... ");
                                    dx_nextMonthButton.Click();
                                    Thread.Sleep(1000);
                                }

                                //Get current month
                                dx_actualMonth = DateTime.Parse(mHeaderComponent.Text.Trim()).Month;
                            }
                        }
                        DlkLogger.LogInfo("Target month is equal to current month.");

                        //Select target day
                        string dx_calendarDays_XPath = ".//td[contains(@class,'dxeCalendarDay_DevEx')]";
                        string expectedDayStr = convertedDate.Day.ToString();

                        List<IWebElement> calendarDays = mCalendarComponent.FindElements(By.XPath(dx_calendarDays_XPath)).Where(x => x.Displayed).ToList();

                        foreach (IWebElement currentDay in calendarDays)
                        {
                            string currentDayStr = currentDay.Text.Trim();
                            if (currentDayStr == expectedDayStr)
                            {
                                DlkLogger.LogInfo("Selecting target day... ");
                                currentDay.Click();
                                break;
                            }
                        }
                    }
                    else //MONTH VIEW Calendar
                    {
                        //Select target year
                        string dx_yearContainer_XPath = ".//div[contains(@class,'dxeCalendarFastNavYearArea_DevEx')]";
                        IWebElement mYearComponent = mCalendarComponent.FindElements(By.XPath(dx_yearContainer_XPath)).Count > 0 ?
                                mCalendarComponent.FindElement(By.XPath(dx_yearContainer_XPath)) : throw new Exception("Year area from month view not found.");

                        string dx_calendarYears_XPath = ".//td[contains(@class,'dxeCalendarFastNavYear_DevEx')]";
                        List<IWebElement> calendarYears = mYearComponent.FindElements(By.XPath(dx_calendarYears_XPath)).Where(x => x.Displayed).ToList();
                        int calendarFirstYear = Convert.ToInt32(calendarYears.FirstOrDefault().Text.Trim());
                        int calendarLastYear = Convert.ToInt32(calendarYears.LastOrDefault().Text.Trim());

                        string dx_prevYearButton_XPath = ".//img[contains(@class,'CalendarFNPrevYear_DevEx')]";
                        string dx_nextYearButton_XPath = ".//img[contains(@class,'CalendarFNNextYear_DevEx')]";
                        IWebElement dx_prevYearButton = mYearComponent.FindElements(By.XPath(dx_prevYearButton_XPath)).Count > 0 ?
                            mYearComponent.FindElement(By.XPath(dx_prevYearButton_XPath)) : throw new Exception("Previous year button not found.");
                        IWebElement dx_nextYearButton = mYearComponent.FindElements(By.XPath(dx_nextYearButton_XPath)).Count > 0 ?
                            mYearComponent.FindElement(By.XPath(dx_nextYearButton_XPath)) : throw new Exception("Next year button not found.");

                        //Go to previous year
                        while (dx_expectedYear < calendarFirstYear && dx_expectedYear < calendarLastYear)
                        {
                            DlkLogger.LogInfo("Clicking previous year set button... ");
                            dx_prevYearButton.Click();
                            Thread.Sleep(1000);
                            calendarYears = mYearComponent.FindElements(By.XPath(dx_calendarYears_XPath)).Where(x => x.Displayed).ToList();
                            calendarFirstYear = Convert.ToInt32(calendarYears.FirstOrDefault().Text.Trim());
                            calendarLastYear = Convert.ToInt32(calendarYears.LastOrDefault().Text.Trim());
                        }

                        //Go to next year
                        while (dx_expectedYear > calendarFirstYear && dx_expectedYear > calendarLastYear)
                        {
                            DlkLogger.LogInfo("Clicking next year set button... ");
                            dx_nextYearButton.Click();
                            Thread.Sleep(1000);
                            calendarYears = mYearComponent.FindElements(By.XPath(dx_calendarYears_XPath)).Where(x => x.Displayed).ToList();
                            calendarFirstYear = Convert.ToInt32(calendarYears.FirstOrDefault().Text.Trim());
                            calendarLastYear = Convert.ToInt32(calendarYears.LastOrDefault().Text.Trim());
                        }

                        //Current target year set displayed
                        string expectedYearStr = dx_expectedYear.ToString();
                        foreach (IWebElement dxCurrentYear in calendarYears)
                        {
                            string currentYearStr = dxCurrentYear.Text.Trim();
                            if (currentYearStr == expectedYearStr)
                            {
                                DlkLogger.LogInfo("Selecting target year... ");
                                dxCurrentYear.Click();
                                break;
                            }
                        }
                        DlkLogger.LogInfo("Target year is equal to current year.");

                        //Select target month
                        string dx_monthContainer_XPath = ".//div[contains(@class,'dxeCalendarFastNavMonthArea_DevEx')]";
                        IWebElement mMonthComponent = mCalendarComponent.FindElements(By.XPath(dx_monthContainer_XPath)).Count > 0 ?
                                mCalendarComponent.FindElement(By.XPath(dx_monthContainer_XPath)) : throw new Exception("Month area from month view not found.");

                        string dx_calendarMonths_XPath = ".//td[contains(@class,'dxeCalendarFastNavMonth_DevEx')]";
                        string expectedMonthStr = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(dx_expectedMonth).ToString();
                        List<IWebElement> calendarMonths = mMonthComponent.FindElements(By.XPath(dx_calendarMonths_XPath)).Where(x => x.Displayed).ToList();

                        foreach (IWebElement dxCurrentMonth in calendarMonths)
                        {
                            string currentMonthStr = dxCurrentMonth.Text.Trim();
                            if (currentMonthStr == expectedMonthStr)
                            {
                                DlkLogger.LogInfo("Selecting target month... ");
                                dxCurrentMonth.Click();
                                break;
                            }
                        }
                        DlkLogger.LogInfo("Target month is equal to current month.");

                        //Confirm OK button
                        string dx_footerContainer_XPath = ".//following-sibling::div[contains(@class,'dxeCalendarFastNavFooter_DevEx')]";
                        IWebElement mFooterComponent = mCalendarComponent.FindElements(By.XPath(dx_footerContainer_XPath)).Count > 0 ?
                                mCalendarComponent.FindElement(By.XPath(dx_footerContainer_XPath)) : throw new Exception("Footer area from month view not found.");

                        string dx_OKButton_XPath = ".//td[contains(@class,'dxeCalendarButton_DevEx')]";
                        IWebElement mOKButton = mFooterComponent.FindElements(By.XPath(dx_OKButton_XPath)).Count > 0 ?
                                mFooterComponent.FindElement(By.XPath(dx_OKButton_XPath)) : throw new Exception("Button from footer area not found.");

                        mOKButton.Click();
                        DlkLogger.LogInfo("Selecting OK... ");
                    }
                    break;

                default: //Modify if under DEFAULT class only

                    if (mElement.TagName.Contains("dm-calendar"))
                    {
                        GetDefaultCalendarComponents();
                        // set month and year

                        string strActualYear = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("headermonthtxt"))).GetValue();
                        strActualYear = strActualYear.Substring(strActualYear.Length - 4);
                        int actualYear = int.Parse(strActualYear);
                        while (targetYear != actualYear) // year is not yet set
                        {
                            GetDefaultCalendarComponents();
                            IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                            ClickUsingJavaScriptThenSelenium(myButton);
                            GetDefaultCalendarComponents();
                            strActualYear = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("headermonthtxt"))).GetValue();
                            strActualYear = strActualYear.Substring(strActualYear.Length - 4);
                            actualYear = int.Parse(strActualYear);
                        }
                        GetDefaultCalendarComponents();
                        string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("headermonthtxt"))).GetValue();
                        strActualMonth = strActualMonth.Substring(0, strActualMonth.Length - 5);
                        int actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                        while (targetMonth != actualMonth)
                        {
                            {
                                GetDefaultCalendarComponents();
                                IWebElement myButton = targetMonth > actualMonth ? mNext : mPrev;
                                ClickUsingJavaScriptThenSelenium(myButton);
                                GetDefaultCalendarComponents();
                                strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("headermonthtxt"))).GetValue();
                                strActualMonth = strActualMonth.Substring(0, strActualMonth.Length - 5);
                                actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                            }
                        }

                        IWebElement targetDay = mCalendarComponent.FindElement(By.XPath("./descendant::span[text()='" + convertedDate.Day.ToString() + "' and not(contains(@class,'othermonth'))]"));
                        ClickUsingJavaScriptThenSelenium(targetDay);
                    }
                    else
                    {
                        throw new Exception("DatePicker format '" + mElement.TagName + "' not yet supported.");
                    }
                    break;
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void GetDefaultCalendarComponents()
        {
            string calendarClassName = "caltable";
            string calendarPrev_ClassName = "icon-recordarrow-left";
            string calendarNext_ClassName = "icon-recordarrow-right";

            mHeaderComponent = mElement.FindElements(By.ClassName("header")).Count > 0 ?
                mElement.FindElement(By.ClassName("header")) : null;
            mCalendarComponent = mElement.FindElements(By.ClassName(calendarClassName)).Count > 0 ?
                mElement.FindElement(By.ClassName(calendarClassName)) : null;
            mPrev = mElement.FindElements(By.ClassName(calendarPrev_ClassName)).Count > 0 ?
                mElement.FindElement(By.ClassName(calendarPrev_ClassName)) : null;
            mNext = mElement.FindElements(By.ClassName(calendarNext_ClassName)).Count > 0 ?
                mElement.FindElement(By.ClassName(calendarNext_ClassName)) : null;
        }

        private void GetComponents()
        {
            switch (mDatePickerClass)
            {
                case KENDO_CLASS: //Modify if under KENDO CLASS only

                    string kendoContainer_XPath = "//div[contains(@class,'kendo-calendar')][contains(@class,'show')]";
                    string kendoTagName = "kendo-calendar";
                    string kendoHeaderTagName = "kendo-calendar-header";

                    IWebElement kendoContainer = DlkEnvironment.AutoDriver.FindElements(By.XPath(kendoContainer_XPath)).Count > 0 ?
                         DlkEnvironment.AutoDriver.FindElement(By.XPath(kendoContainer_XPath)) : null;
                    IWebElement kendoCalendar = kendoContainer != null ? kendoContainer.FindElement(By.TagName(kendoTagName)) :
                         DlkEnvironment.AutoDriver.FindElements(By.TagName(kendoTagName)).Count > 0 ?
                         DlkEnvironment.AutoDriver.FindElement(By.TagName(kendoTagName)) : null;

                    mHeaderComponent = kendoCalendar.FindElements(By.ClassName("header")).Count > 0 ?
                        kendoCalendar.FindElement(By.ClassName("header")) : kendoCalendar.FindElement(By.TagName(kendoHeaderTagName));
                    mCalendarComponent = kendoCalendar;
                    break;

                case DX_CLASS: //Modify if under DEV_EX CLASS ONLY

                    string devExContainer_XPath = ".//table[contains(@class,'dxeCalendar_DevEx')]";
                    string devExMonthContainer_XPath = ".//*[contains(@class,'dxeCalendarFastNav_DevEx')]";
                    string devExHeader_XPath = ".//*[contains(@class,'dxeCalendarHeader_DevEx')]";

                    IWebElement devExCalender = mElement.FindWebElementCoalesce(By.XPath(devExContainer_XPath), By.XPath(devExMonthContainer_XPath));
                   
                    mHeaderComponent = devExCalender.FindElements(By.XPath(devExHeader_XPath)).Count > 0 ?
                        devExCalender.FindElement(By.XPath(devExHeader_XPath)) : null;
                    mCalendarComponent = devExCalender;
                    break;

                default: //Modify if under DEFAULT CLASS only
                    GetDefaultCalendarComponents();
                    break;
            }

            if (mCalendarComponent == null)
                throw new Exception("Calendar component not found.");
        }

        private string GetDatePickerClass()
        {
            string datePickerClass = mElement.GetAttribute("class") != null ?
                mElement.GetAttribute("class").ToString().ToLower() : string.Empty;

            //Identify datepicker class
            if (datePickerClass.Contains(KENDO_CLASS) || mElement.TagName.Contains(KENDO_CLASS) || mElement.TagName.Contains("dm-date-picker"))
                datePickerClass = KENDO_CLASS;
            else if (datePickerClass.Contains(DX_CLASS))
                datePickerClass = DX_CLASS;

            return datePickerClass;
        }

        #endregion

        #region KEYWORDS

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
                DateTime convertedDate = DateTime.Parse(Date);
                Initialize();
                SetTargetDate(convertedDate);
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
                    ClickUsingJavaScriptThenSelenium(myButton);
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

        /// <summary>
        /// Checks whether the date picker input box contains the specified text.
        /// </summary>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyTextContains", new String[] { "1|text|ExpextedText|09/21/2017" })]
        public void VerifyTextContains(String ExpectedText)
        {
            try
            {
                FindElement();
                string actualValue = string.Empty;
                if (mElement.FindElements(By.TagName("input")).Count > 0)
                {
                    actualValue = new DlkBaseControl("DatePicker", mElement.FindElement(By.TagName("input"))).GetValue();
                }
                else
                {
                    DlkBaseControl datePicker = new DlkBaseControl("DatePicker", mElement);
                    actualValue = datePicker.GetValue();
                }
                actualValue = DlkString.ReplaceCarriageReturn(actualValue, " ").Trim().ToLower();
                DlkAssert.AssertEqual("VerifyTextContains", ExpectedText.ToLower(), actualValue, true);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectWeekNumber", new String[] { "1|WeekNumber|1" })]
        public void SelectWeekNumber(String WeekNumber)
        {
            int weekno = 0;
            if (!int.TryParse(WeekNumber, out weekno) || weekno == 0)
                throw new Exception("[" + WeekNumber + "] is not a valid input for parameter WeekNumber.");

            try
            {
                FindElement();
                bool wFound = false;
                IList<IWebElement> mWeekNumbers = GetWeekNumbers();

                foreach(IWebElement weekNumber in mWeekNumbers)
                {
                    string actualWeekValue = !String.IsNullOrEmpty(weekNumber.Text) ?
                        weekNumber.Text : new DlkBaseControl("Week Number", weekNumber).GetValue().Trim();
                    if (actualWeekValue.Equals(WeekNumber))
                    {
                        weekNumber.Click();
                        wFound = true;
                        DlkLogger.LogInfo("Week Number [" + WeekNumber + "] has been selected.");
                        break;
                    }
                }

                if(!wFound)
                    throw new Exception("Selected week number [" + WeekNumber + "] is not visibile or does not exist.");

                DlkLogger.LogInfo("SelectWeekNumber() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectWeekNumber() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
