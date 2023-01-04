#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("Carousel")]
    public class DlkCarousel : DlkMobileControl
    {
        private const string CLASS_CAL_DAY_SELECTED_PARTIAL_WEBVIEW = "stormDaySelected";
        private const string CLASS_CAL_NUM_SELECTED_PARTIAL_WEBVIEW = "calNumberSelect";
        private const string XPATH_ITEMS_WEBVIEW = ".//*[contains(@id,'daytype')]/ancestor::*[contains(@class,'x-dataview-item')]";

        private const int INT_SELECTION_RETRY_LIMIT = 5;

        public DlkCarousel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCarousel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCarousel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private List<DlkMobileControl> mCarouselItems = new List<DlkMobileControl>();

        private void Initialize(bool isWebView = true)
        {
#if NATIVE_MAPPING
            DlkEnvironment.mLockContext = false;
            DlkEnvironment.SetContext("WEBVIEW");
#endif
            FindElement();
        }

        private void GetCarouselItems()
        {
            mElement.FindElements(By.XPath(XPATH_ITEMS_WEBVIEW)).ToList()
                .ForEach(x => mCarouselItems.Add(new DlkMobileControl("itm", x)));
        }

        [Keyword("Select")]
        public void Select(string DayOfWeek, string DayOfMonth)
        {
            try
            {
                Initialize();
                GetCarouselItems();
                var target = new CarouselDate(DayOfWeek, DayOfMonth);
                var found = mCarouselItems.FirstOrDefault(x => CarouselDate.AreEqual(GetCarouselDate(x), target));
                string error = string.Empty;
                if (found == null)
                {
                    throw new Exception("Target carousel date not found");
                }
                int attempt = 0;
                while (++attempt <= INT_SELECTION_RETRY_LIMIT)
                {
                    DlkLogger.LogInfo("Selecting carousel date attempt #" + attempt + "...");
#if NATIVE_MAPPING
                    found.ScrollIntoView();
#else
                    found.ScrollIntoViewUsingJavaScript();
#endif
                    found.Tap();
                    if (CarouselDate.AreEqual(GetCarouselDate(found),
                        GetSelectedCarouselDate(out error)))
                    {
                        break;
                    }
                }
                found.Tap();
                DlkLogger.LogInfo("Successfully executed Select().");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        [Keyword("SelectByIndex")]
        public void SelectByIndex(string OneBasedIndex)
        {
            try
            {
                Initialize();
                GetCarouselItems();
                int index;
                string error = string.Empty;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mCarouselItems.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                int attempt = 0;
                while (++attempt <= INT_SELECTION_RETRY_LIMIT)
                {
                    DlkLogger.LogInfo("Selecting carousel date attempt #" + attempt + "...");
#if NATIVE_MAPPING
                    mCarouselItems[index - 1].ScrollIntoView();
#else
                    mCarouselItems[index - 1].ScrollIntoViewUsingJavaScript();
#endif
                    mCarouselItems[index - 1].Tap();
                    if (CarouselDate.AreEqual(GetCarouselDate(mCarouselItems[index - 1]), 
                        GetSelectedCarouselDate(out error)))
                    {
                        break;
                    }
                }
                DlkLogger.LogInfo("Successfully executed SelectByIndex().");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        [Keyword("VerifySelectedDate")]
        public void VerifySelectedDate(string DayOfWeek, string DayOfMonth)
        {
            try
            {
                Initialize(true);
                string error = string.Empty;
                var expected = new CarouselDate(DayOfWeek, DayOfMonth);
                var actual = GetSelectedCarouselDate(out error);
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
                DlkAssert.AssertEqual("VerifySelectedDate()", expected.CalDateString, actual.CalDateString);
                DlkLogger.LogInfo("VerifySelectedDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedDate() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        [Keyword("VerifyPeriodDatesCount")]
        public void VerifyPeriodDatesCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                GetCarouselItems();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                DlkAssert.AssertEqual("VerifyItemCount", expected, mCarouselItems.Count);
                DlkLogger.LogInfo("VerifyPeriodDatesCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPeriodDatesCount() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        [Keyword("VerifyPeriodDateExists")]
        public void VerifyPeriodDateExists(string DayOfWeek, string DayOfMonth, string TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bExpected;
                if (!bool.TryParse(TrueOrFalse, out bExpected))
                {
                    throw new Exception("Invalid boolean value: '" + TrueOrFalse+ "'");
                }
                GetCarouselItems();
                var target = new CarouselDate(DayOfWeek, DayOfMonth);
                var found = mCarouselItems.FirstOrDefault(x => CarouselDate.AreEqual(GetCarouselDate(x), target));
                DlkAssert.AssertEqual("VerifyPeriodDateExists", bExpected, found != null);
                DlkLogger.LogInfo("VerifyPeriodDateExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPeriodDateExists() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
#if NATIVE_MAPPING
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
#endif
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        private CarouselDate GetCarouselDate(DlkMobileControl calItem)
        {
            var calDay = calItem.mElement.FindElements(By.XPath(".//*[contains(@class,'calDaySelected')]")).FirstOrDefault();
            var sCalDay = calDay != null ? calDay.Text : "MON";
            var calNum = calItem.mElement.FindElements(By.XPath(".//*[contains(@class,'calNumber')]")).FirstOrDefault();
            var sCalNum = calNum != null ? calNum.Text : "1";
            return new CarouselDate(sCalDay, sCalNum);
        }

        private CarouselDate GetSelectedCarouselDate(out string Error)
        {
            Error = string.Empty;
            var selectedCalDay = mElement.FindElements(By.XPath(".//*[contains(@class,'"
                + CLASS_CAL_DAY_SELECTED_PARTIAL_WEBVIEW + "')]")).FirstOrDefault();
            if (selectedCalDay == null)
            {
                Error = "Cannot locate selected date: Missing Day indicator";
                return null;
            }
            var selectedCalNum = mElement.FindElements(By.XPath(".//*[contains(@class,'"
                + CLASS_CAL_NUM_SELECTED_PARTIAL_WEBVIEW + "')]")).FirstOrDefault();
            if (selectedCalNum == null)
            {
                Error = "Cannot locate selected date: Missing Num indicator";
                return null;
            }
            return new CarouselDate(selectedCalDay.Text, selectedCalNum.Text);
        }
    }

    public class CarouselDate
    {
        public string DayOfWeek { get; set; }
        public string DayOfMonth { get; set; }
        public string CalDateString
        {
            get
            {
                return string.Join(" ", DayOfWeek, DayOfMonth);
            }
        }

        public CarouselDate(string dayOfWeek, string dayOfMonth)
        {
            DayOfWeek = GetDayOfWeek(dayOfWeek);
            DayOfMonth = GetDayOfMonth(dayOfMonth);
        }

        private string GetDayOfWeek(string dayOfWeek)
        {
            switch (dayOfWeek.ToLower().Substring(0, 3))
            {
                case "tue":
                    return "TUE";
                case "wed":
                    return "WED";
                case "thu":
                    return "THU";
                case "fri":
                    return "FRI";
                case "sat":
                    return "SAT";
                case "sun":
                    return "SUN";
                case "mon":
                default:
                    return "MON";
            }
        }

        private string GetDayOfMonth(string dayOfMonth)
        {
            int iDay = 1;

            if (int.TryParse(dayOfMonth, out iDay))
            {
                iDay = iDay <= 31 && iDay >= 1 ? iDay : 1;
            }
            return iDay.ToString();
        }

        public static bool AreEqual(CarouselDate Date1, CarouselDate Date2)
        {
            return Date1.DayOfMonth.Equals(Date2.DayOfMonth) && Date1.DayOfWeek.Equals(Date2.DayOfWeek);
        }
    }
}
