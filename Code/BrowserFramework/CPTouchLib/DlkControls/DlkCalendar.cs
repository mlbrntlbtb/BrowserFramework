#define NATIVE_MAPPING

using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkMobileControl
    {
        private const string ID_WEBVIEW = "ext-deltekdatepickercal-1";
        private const string CLASS_SELECTED_PARTIAL = "selected";
        private const string CLASS_TODAY_PARTIAL = "caltoday";

        public DlkCalendar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCalendar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

#if NATIVE_MAPPING
        public void Initialize(bool IsWebView=false)
#else
        public void Initialize()
#endif
        {
#if NATIVE_MAPPING
            if (IsWebView)
            {
                mSearchType = "ID";
                mSearchValues = new string[] { ID_WEBVIEW };
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
            }
#endif
            FindElement();
        }

        [Keyword("SetDate")]
        public void SetDate(string Month, string Day, string Year)
        {
            try
            {
                Initialize();
                var myDate = new CalendarDate(Month, Day, Year);
#if NATIVE_MAPPING
                var currentDateContainer = mElement.FindElements(By.XPath("//*[contains(@text,'Wed')]")).FirstOrDefault();
                var currentDateString = currentDateContainer.Text.Replace("Wed", string.Empty).Trim();
#else
                var currentDateContainer = mElement.FindElements(By.ClassName("month-year-label")).FirstOrDefault();
                if (currentDateContainer == null)
                {
                    throw new Exception("Cannot determine current date.");
                }
                var currentDateString = currentDateContainer.Text;
#endif
                var currentDateArr = currentDateString.Split(' ');

                while (true)
                {
                    var currentYear = currentDateArr.Last();
                    var diffYear = GetDifferenceInYears(myDate.Year, currentYear);

                    if (diffYear < 0)
                    {
                        // tap back
                        for (int i = 1; i <= Math.Abs(diffYear); i++)
                        {
#if NATIVE_MAPPING
                            var prevYear = mElement.FindElements(By.XPath("//*[contains(@text,'Sun')]")).FirstOrDefault();
#else
                            var prevYear = mElement.FindElements(By.ClassName("goto-prevyear")).FirstOrDefault();
#endif
                            if (prevYear == null)
                            {
                                throw new Exception("Error encountered tapping 'Previous Year' button: Button not found");
                            }
                            new DlkMobileControl("PrevYear", prevYear).Tap();
                            Thread.Sleep(1000);
                        }
                    }
                    else if (diffYear > 0)
                    {
                        // tap forward
                        for (int i = 1; i <= diffYear; i++)
                        {
#if NATIVE_MAPPING
                            var nextYear = mElement.FindElements(By.XPath("//*[contains(@text,'Sat')]")).FirstOrDefault();
#else
                            var nextYear = mElement.FindElements(By.ClassName("goto-nextyear")).FirstOrDefault();
#endif
                            if (nextYear == null)
                            {
                                throw new Exception("Error encountered tapping 'Next Year' button: Button not found");
                            }
                            new DlkMobileControl("NextYear", nextYear).Tap();
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        break;
                    }
#if NATIVE_MAPPING
                    currentDateContainer = mElement.FindElements(By.XPath("//*[contains(@text,'Wed')]")).FirstOrDefault();
                    currentDateString = currentDateContainer.Text.Replace("Wed", string.Empty).Trim();
#else
                    currentDateContainer = mElement.FindElements(By.ClassName("month-year-label")).FirstOrDefault();
                    if (currentDateContainer == null)
                    {
                        throw new Exception("Cannot determine current date.");
                    }
                    currentDateString = currentDateContainer.Text;
#endif
                    currentDateArr = currentDateString.Split(' ');
                }

                while (true)
                {
                    var currentMonth = currentDateArr.First();
                    var diffMonth = GetDifferenceInMonths(myDate.Month, currentMonth);
                    if (diffMonth < 0)
                    {
                        // tap back
                        for (int i = 1; i <= Math.Abs(diffMonth); i++)
                        {
#if NATIVE_MAPPING
                            var prevMonth = mElement.FindElements(By.XPath("//*[contains(@text,'Mon')]")).FirstOrDefault();
#else
                            var prevMonth = mElement.FindElements(By.ClassName("goto-prevmonth")).FirstOrDefault();
#endif
                            if (prevMonth == null)
                            {
                                throw new Exception("Error encountered tapping 'Previous Month' button: Button not found");
                            }
                            new DlkMobileControl("PrevMonth", prevMonth).Tap();
                            Thread.Sleep(1000);
                        }
                    }
                    else if (diffMonth > 0)
                    {
                        // tap forward
                        for (int i = 1; i <= diffMonth; i++)
                        {
#if NATIVE_MAPPING
                            var nextMonth = mElement.FindElements(By.XPath("//*[contains(@text,'Fri')]")).FirstOrDefault();
#else
                            var nextMonth = mElement.FindElements(By.ClassName("goto-nextmonth")).FirstOrDefault();
#endif
                            if (nextMonth == null)
                            {
                                throw new Exception("Error encountered tapping 'Next Month' button: Button not found");
                            }
                            new DlkMobileControl("NextMonth", nextMonth).Tap();
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        break;
                    }
#if NATIVE_MAPPING
                    currentDateContainer = mElement.FindElements(By.XPath("//*[contains(@text,'Wed')]")).FirstOrDefault();
                    currentDateString = currentDateContainer.Text.Replace("Wed", string.Empty).Trim();
#else
                    currentDateContainer = mElement.FindElements(By.ClassName("month-year-label")).FirstOrDefault();
                    if (currentDateContainer == null)
                    {
                        throw new Exception("Cannot determine current date.");
                    }
                    currentDateString = currentDateContainer.Text;
#endif
                    currentDateArr = currentDateString.Split(' ');
                }
#if NATIVE_MAPPING
                var targetDay = mElement.FindElements(By.XPath("//*[@text='" + myDate.Day +"']")).LastOrDefault();
#else
                var targetDay = mElement.FindElements(By.XPath("//*[@datetime='" + myDate.DateTimeShortString + "']")).LastOrDefault();
#endif
                if (targetDay == null)
                {
                    throw new Exception("Error encountered tapping 'Target Day' button: Button not found");
                }
                new DlkMobileControl("TargetDay", targetDay).Tap();
                DlkLogger.LogInfo("Successfully executed SetDate()");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }
        }

        [Keyword("Today")]
        public void Today()
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
				var today = mElement.FindElements(By.XPath("(//*[contains(@resource-id,'ext-deltekbutton')])[2]")).FirstOrDefault();
#else
				var today = mElement.FindElements(By.XPath("//*[text()='Today']/ancestor::*[contains(@id,'deltekbutton')]")).FirstOrDefault();
#endif
				if (today == null)
                {
                    throw new Exception("Error encountered tapping 'Today' button: Button not found");
                }
                new DlkMobileControl("Today", today).Tap();
                DlkLogger.LogInfo("Successfully executed Today()");
            }
            catch (Exception e)
            {
                throw new Exception("Today() failed : " + e.Message, e);
            }
        }

        [Keyword("PreviousMonth")]
        public void PreviousMonth()
        {
            try
            {
                Initialize();
                var prevMonth = mElement.FindElements(By.XPath("//*[contains(@text,'Mon')]")).FirstOrDefault();
                if (prevMonth == null)
                {
                    throw new Exception("Error encountered tapping 'Previous Month' button: Button not found");
                }
                new DlkMobileControl("PrevMonth", prevMonth).Tap();
                DlkLogger.LogInfo("Successfully executed PreviousMonth()");
            }
            catch (Exception e)
            {
                throw new Exception("PreviousMonth() failed : " + e.Message, e);
            }
        }

        [Keyword("PreviousYear")]
        public void PreviousYear()
        {
            try
            {
                Initialize();
                var prevYear = mElement.FindElements(By.XPath("//*[contains(@text,'Sun')]")).FirstOrDefault();
                if (prevYear == null)
                {
                    throw new Exception("Error encountered tapping 'Previous Year' button: Button not found");
                }
                new DlkMobileControl("PrevYear", prevYear).Tap();
                DlkLogger.LogInfo("Successfully executed PreviousYear()");
            }
            catch (Exception e)
            {
                throw new Exception("PreviousYear() failed : " + e.Message, e);
            }
        }

        [Keyword("NextYear")]
        public void NextYear()
        {
            try
            {
                Initialize();
                var nextYear = mElement.FindElements(By.XPath("//*[contains(@text,'Sat')]")).FirstOrDefault();
                if (nextYear == null)
                {
                    throw new Exception("Error encountered tapping 'Next Year' button: Button not found");
                }
                new DlkMobileControl("NextYear", nextYear).Tap();
                DlkLogger.LogInfo("Successfully executed NextYear()");
            }
            catch (Exception e)
            {
                throw new Exception("NextYear() failed : " + e.Message, e);
            }
        }

        [Keyword("NextMonth")]
        public void NextMonth()
        {
            try
            {
                Initialize();
                var nextMonth = mElement.FindElements(By.XPath("//*[contains(@text,'Fri')]")).FirstOrDefault();
                if (nextMonth == null)
                {
                    throw new Exception("Error encountered tapping 'Next Month' button: Button not found");
                }
                new DlkMobileControl("NextMonth", nextMonth).Tap();
                DlkLogger.LogInfo("Successfully executed NextMonth()");
            }
            catch (Exception e)
            {
                throw new Exception("NextMonth() failed : " + e.Message, e);
            }
        }

        [Keyword("Cancel")]
        public void Cancel()
        {
            try
            {
                Initialize();
                var cancel = mElement.FindElements(By.XPath("(//*[contains(@resource-id,'ext-deltekbutton')])[1]")).FirstOrDefault();
                if (cancel == null)
                {
                    throw new Exception("Error encountered tapping 'Cancel' button: Button not found");
                }
                new DlkMobileControl("Cancel", cancel).Tap();
                DlkLogger.LogInfo("Successfully executed Cancel()");
            }
            catch (Exception e)
            {
                throw new Exception("Cancel() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySelectedDate")]
        public void VerifySelectedDate(string Month, string Day, string Year)
        {
            try
            {
#if NATIVE_MAPPING
                Initialize(true);
#else
                Initialize();
#endif
                var selected = mElement.FindElements(By.XPath(".//*[contains(@class,'" + CLASS_SELECTED_PARTIAL + "')]")).FirstOrDefault();
                if (selected == null)
                {
                    throw new Exception("Error encountered locating 'Selected' cell: Cell not found");
                }
                var expected = new CalendarDate(Month, Day, Year).DateTimeShortString;
                var actual = selected.GetAttribute("datetime");
                DlkAssert.AssertEqual("VerifySelectedDate()", expected, actual);
                DlkLogger.LogInfo("VerifySelectedDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedDate() failed : " + e.Message, e);
            }
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
        }

        [Keyword("VerifyToday")]
        public void VerifyToday(string Month, string Day, string Year)
        {
            try
            {
#if NATIVE_MAPPING
                Initialize(true);
#else
                Initialize();
#endif
                var today = mElement.FindElements(By.XPath(".//*[contains(@class,'" + CLASS_TODAY_PARTIAL + "')]")).FirstOrDefault();
                if (today == null)
                {
                    throw new Exception("Error encountered locating 'Today' cell: Cell not found");
                }
                var expected = new CalendarDate(Month, Day, Year).DateTimeShortString;
                var actual = today.GetAttribute("datetime");
                DlkAssert.AssertEqual("VerifyToday()", expected, actual);
                DlkLogger.LogInfo("VerifyToday() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToday() failed : " + e.Message, e);
            }
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
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

        private int GetDifferenceInYears(string Year1, string Year2)
        {
            return int.Parse(Year1) - int.Parse(Year2);
        }

        private int GetDifferenceInMonths(string Month1, string Month2)
        {
            return CalendarDate.MonthAsInt(Month1) - CalendarDate.MonthAsInt(Month2);
        }
    }

    public class CalendarDate
    {
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }

        public string DateTimeShortString
        {
            get
            {
                var iMonth = MonthAsInt(Month);
                var sMonth = iMonth.ToString();
                sMonth = sMonth.Length == 1 ? "0" + sMonth : sMonth;
                var sDay = Day.Length == 1 ? "0" + Day : Day;
                var sYear = Year;
                return string.Join("-", sYear, sMonth, sDay);
            }
        }

        public CalendarDate(string month, string day, string year)
        {
            Month = GetMonth(month);
            Day = GetDay(day);
            Year = GetYear(year);
        }

        private string GetMonth(string Month)
        {
            int mnth;
            if (int.TryParse(Month, out mnth))
            {
                switch (mnth)
                {
                    case 2:
                        return "February";
                    case 3:
                        return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6:
                        return "June";
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9:
                        return "September";
                    case 10:
                        return "October";
                    case 11:
                        return "November";
                    case 12:
                        return "December";
                    case 1:
                    default:
                        return "January";
                }
            }
            else
            {
                switch (Month.Substring(0, 3).ToLower())
                {
                    case "feb":
                        return "February";
                    case "mar":
                        return "March";
                    case "apr":
                        return "April";
                    case "may":
                        return "May";
                    case "jun":
                        return "June";
                    case "jul":
                        return "July";
                    case "aug":
                        return "August";
                    case "sep":
                        return "September";
                    case "oct":
                        return "October";
                    case "nov":
                        return "November";
                    case "dec":
                        return "December";
                    case "jan":
                    default:
                        return "January";
                }
            }
        }

        private string GetDay(string Day)
        {
            int day = 1;
            if (int.TryParse(Day, out day) && day > 0 && day <= 31)
            {
                return day.ToString();
            }
            return "1";
        }

        private string GetYear(string Year)
        {
            int yr;
            return Year.Length == 4 && int.TryParse(Year, out yr) ? Year : DateTime.Today.Year.ToString();
        }
        public static int MonthAsInt(string month)
        {
            switch (month)
            {
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                case "January":
                default:
                    return 1;
            }
        }
    }
}
