using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using AcumenTouchStoneLib.DlkSystem;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkBaseControl
    {

        #region DECLARATIONS
        private String mTableHeaderXPath = ".//thead";
        private String mTableBodyXPath = ".//tbody";

        private const String MONTHVIEW = "fc-month-view";
        private const String DAYVIEW = "fc-agendaDay-view";
        private const String WEEKVIEW = "fc-agendaWeek-view";
        private String mViewType = "";
        private IWebElement mHeader = null;
        private IWebElement mBody = null;

        internal IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

        #endregion


        public DlkCalendar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCalendar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #region PUBLIC METHODS AND KEYWORDS
        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

            FindElement();
            String mClass = this.mElement.GetAttribute("class");
            if (mClass.Contains(WEEKVIEW))
            {
                mViewType = WEEKVIEW;
            }
            else if (mClass.Contains(DAYVIEW))
            {
                mViewType = DAYVIEW;
            }
            else if (mClass.Contains(MONTHVIEW) || mClass.Contains("xdsoft_datepicker"))
            {
                mViewType = MONTHVIEW;
            }
            mHeader = mElement.FindElement(By.XPath(mTableHeaderXPath));
            mBody = mElement.FindElement(By.XPath(mTableBodyXPath));

            this.ScrollIntoViewUsingJavaScript();
        }

        /// <summary>
        /// Clicks a date on the calendar
        /// </summary>
        /// <param name="SearchDate">The desired date on the calendar where the event is allocated</param>
        [Keyword("ClickDate", new String[] { "1|text|Expected Value|Sample value" })]
        public void ClickDate(String SearchDate)
        {
            try
            {
                Initialize();
                MonthClickDate(SearchDate);
                DlkLogger.LogInfo("ClickDate() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("ClickDate() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Boolean bFound = false;
                if (this.Exists(1))
                {
                    if (mElement.GetCssValue("display") != "none")
                    {
                        bFound = true;
                    }
                }
                DlkAssert.AssertEqual("VerifyExists", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
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

        [Keyword("ScrollLeftOrRight", new String[] { "1|text|Expected Value|Sample value" })]
        public void ScrollLeftOrRight(String NumberOfScrolls, String Direction)
        {
            try
            {
                Initialize();
                DayMonthScrollLeftOrRight(NumberOfScrolls, Direction);
                DlkLogger.LogInfo("ScrollLeftOrRight() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollLeftOrRight() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks the See More link for the date/time given
        /// </summary>
        /// <param name="Instance">The instance number of the link you want to click</param>
        [Keyword("SeeMore", new String[] { "1|text|Expected Value|Sample value" })]
        public void SeeMore(String Instance)
        {
            try
            {
                Initialize();
                switch (mViewType)
                {
                    case MONTHVIEW:
                        MonthSeeMore(Instance);
                        break;
                    case DAYVIEW:
                        break;
                    case WEEKVIEW:
                        break;
                    default:
                        throw new Exception("Unknown calendar view");
                }
                DlkLogger.LogInfo("SeeMore() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("SeeMore() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verify an all-day event
        /// </summary>
        /// <param name="SearchDate">The desired date or time on the calendar where the event is allocated</param>
        /// <param name="Event">The name of the event</param>
        [Keyword("VerifyDayEvent", new String[] { "1|text|Expected Value|Sample value" })]
        public void VerifyDayEvent(String SearchDate, String Event)
        {
            try
            {
                Initialize();
                switch (mViewType)
                {
                    case MONTHVIEW:
                        throw new Exception("This keyword is not applicable for this view.");
                    case DAYVIEW:
                        DayVerifyDayEvent(SearchDate, Event);
                        break;
                    case WEEKVIEW:
                        WeekVerifyDayEvent(SearchDate, Event);
                        break;
                    default:
                        throw new Exception("Unknown calendar view");
                }
                DlkLogger.LogInfo("VerifyDayEvent() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDayEvent() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verify a non all-day event
        /// </summary>
        /// <param name="SearchDate">The desired date on the calendar where the event is allocated</param>
        /// <param name="EventTime">The desired time on the calendar where the event is allocated</param>
        /// <param name="Event">The expected name of the event</param>
        [Keyword("VerifyEvent", new String[] { "1|text|Expected Value|Sample value" })]
        public void VerifyEvent(String SearchDate, String EventTime, String Event)
        {
            try
            {
                IReadOnlyCollection<IWebElement> eventsOnDate;
                string ActualEvent = "";
                string dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
                Initialize();

                switch (mViewType)
                {
                    case MONTHVIEW:
                        eventsOnDate = GetMonthEvents(dtTarget);

                        foreach (IWebElement eventOnDate in eventsOnDate)
                        {
                            string time = eventOnDate.FindElement(By.XPath(".//*[@class='fc-time']")).GetAttribute("textContent").ToUpper();
                            if (time == EventTime.ToUpper())
                            {
                                DlkBaseControl eventSelected = new DlkBaseControl("Event", eventOnDate.FindElement(By.ClassName("fc-title")));
                                ActualEvent = eventSelected.GetValue();
                                break;
                            }
                        }
                        break;
                    case WEEKVIEW:
                        eventsOnDate = GetWeekEvents(dtTarget);

                        foreach (IWebElement eventOnDate in eventsOnDate)
                        {
                            string time = eventOnDate.FindElement(By.XPath(".//div[@class='fc-time']")).GetAttribute("data-full").Split(new char[] { '-' })[0].Trim();
                            if (time == EventTime.ToUpper())
                            {
                                DlkBaseControl eventSelected = new DlkBaseControl("Event", eventOnDate);
                                ActualEvent = eventSelected.GetValue();
                                break;
                            }
                        }
                        break;
                    case DAYVIEW:
                        eventsOnDate = GetDayEvents(dtTarget);

                        foreach (IWebElement eventOnDate in eventsOnDate)
                        {
                            string time = eventOnDate.FindElement(By.XPath(".//div[@class='fc-time']")).GetAttribute("data-full").Split(new char[] { '-' })[0].Trim();
                            if (time == EventTime.ToUpper())
                            {
                                DlkBaseControl eventSelected = new DlkBaseControl("Event", eventOnDate);
                                ActualEvent = eventSelected.GetValue();
                                break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("Unknown calendar view");
                }

                DlkAssert.AssertEqual("VerifyEvent", Event, ActualEvent);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyEvent() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verify if a non all-day event exists
        /// </summary>
        /// <param name="SearchDate">The desired date on the calendar where the event is allocated</param>
        /// <param name="EventTime">The time on the calendar where the event is allocated</param>
        /// <param name="Event">The name of the event</param>
        /// <param name="TrueOrFalse">Indicates whether the event is expected to be existing or not</param>
        [Keyword("VerifyEventExists", new String[] { "1|text|Expected Value|Sample value" })]
        public void VerifyEventExists(String EventDate, String EventTime, String Event, String TrueOrFalse)
        {
            try
            {
                Initialize();
                switch (mViewType)
                {
                    case MONTHVIEW:
                        MonthVerifyEventExists(EventDate, EventTime, Event, TrueOrFalse);
                        break;
                    case DAYVIEW:
                        DayVerifyEventExists(EventDate, EventTime, Event, TrueOrFalse);
                        break;
                    case WEEKVIEW:
                        WeekVerifyEventExists(EventDate, EventTime, Event, TrueOrFalse);
                        break;
                    default:
                        throw new Exception("Unknown calendar view");
                }
                DlkLogger.LogInfo("VerifyEventExists() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyEventExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This will verify the headers in the order they are listed from left to right. The start of the week should be the first in the list. Similar to verifying a list. Will only work for Month/Week views.
        /// </summary>
        /// <param name="Items">Items for each header</param>
        [Keyword("VerifyHeaders", new String[] { "1|text|Expected Value|Sample value" })]
        public void VerifyHeaders(String Items)
        {
            try
            {
                Initialize();
                switch (mViewType)
                {
                    case DAYVIEW:
                        throw new Exception("This keyword is not applicable for this view.");
                    case MONTHVIEW:
                    case WEEKVIEW:
                        VerifyHeaderItems(Items);
                        break;
                    default:
                        throw new Exception("Unknown calendar view");
                }
                DlkLogger.LogInfo("VerifyHeaders() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaders() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This will return true if the day exists, otherwise will return false. Will work only for Week/Month view.
        /// </summary>
        /// <param name="Items">Items for each header</param>
        [Keyword("VerifyDayExists", new String[] { "1|text|Expected Value|Sample value" })]
        public void VerifyDayExists(String Date, String TrueOrFalse)
        {
            try
            {
                Initialize();
                switch (mViewType)
                {
                    case DAYVIEW:
                        throw new Exception("This keyword is not applicable for this view.");
                    case MONTHVIEW:
                    case WEEKVIEW:
                        VerifySearchDateExists(Date, TrueOrFalse);
                        break;
                    default:
                        throw new Exception("Unknown calendar view");
                }
                DlkLogger.LogInfo("VerifyDayExists() performed successfully");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDayExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region PRIVATE METHODS
        private IWebElement GetTargetEventFromSearchValues(String SearchDate, String SearchValue, String SearchTime = null)
        {
            IWebElement ret = null;
            int index = 0;
            switch (mViewType)
            {
                case MONTHVIEW:
                    IWebElement mCell = GetTargetDateTime(SearchDate, out index);
                    if (mCell == null)
                    {
                        throw new Exception("Searched datetime: " + SearchDate + " not found.");
                    }
                    //Using index, find the event in the proper column
                    if (mCell.FindElements(By.XPath("./ancestor::table[1]/tbody//tr//a")).Count > 0)
                    {
                        IList<IWebElement> rets = mCell.FindElements(By.XPath("./ancestor::table[1]/tbody//tr//a"));
                        foreach (IWebElement mEvent in rets)
                        {
                            IWebElement mEventTitle = mEvent.FindElement(By.ClassName("fc-title"));
                            string mEventTime = mEvent.FindElement(By.ClassName("fc-time")).GetAttribute("textContent");

                            if (mEventTitle.GetAttribute("textContent").ToLower() == SearchValue.ToLower())
                            {
                                if (!String.IsNullOrEmpty(SearchTime))
                                {
                                    if (mEventTime.ToLower() == SearchTime.ToLower())
                                    {
                                        ret = mEventTitle;
                                        break;
                                    }
                                }
                                else
                                {
                                    ret = mEventTitle;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Searched value: " + SearchValue + " not found.");
                    }
                    break;
                case WEEKVIEW:
                    break;
                case DAYVIEW:
                    break;
            }
            return ret;
        }

        private IWebElement GetTargetDateTime(String SearchDateTime, out int ItemIndex)
        {
            IWebElement ret = null;
            ItemIndex = -1;
            if (mBody.FindElements(By.XPath(".//tr//td[@data-date='" + SearchDateTime + "']")).Count > 0)
            {
                //Get index from date header
                IWebElement mCell = mBody.FindElement(By.XPath(".//tr//td[@data-date='" + SearchDateTime + "']"));
                IList<IWebElement> lstItems = mBody.FindElements(By.XPath(".//tr//td")).ToList();
                foreach (IWebElement elem in lstItems)
                {
                    if (elem.GetAttribute("data-date") == SearchDateTime)
                    {
                        ret = elem;
                        ItemIndex = lstItems.IndexOf(elem) + 1;
                        break;
                    }
                }
            }
            return ret;
        }

        private IWebElement GetTargetDayEvent(String SearchDateTime)
        {
            IWebElement ret = null;
            int index = 0;
            switch (mViewType)
            {
                case MONTHVIEW:
                    throw new Exception("Month view is not supported by this keyword.");
                case WEEKVIEW:
                    IWebElement wCell = GetTargetDateTime(SearchDateTime, out index);
                    if (wCell == null)
                    {
                        throw new Exception("Searched date: " + SearchDateTime + " not found.");
                    }
                    //Using index, find the event in the proper column
                    if (wCell.FindElements(By.XPath("./ancestor::table//tbody//td[" + index + "]//a[contains(@class,'day-grid-event')]//*[@class='fc-title']")).Count > 0)
                    {
                        ret = wCell.FindElement(By.XPath("./ancestor::table//tbody//td[" + index + "]//a[contains(@class,'day-grid-event')]//*[@class='fc-title']"));
                    }
                    else
                    {
                        throw new Exception("No event found on " + SearchDateTime + ".");
                    }
                    break;
                case DAYVIEW:
                    IWebElement dCell = GetTargetDateTime(SearchDateTime, out index);
                    if (dCell == null)
                    {
                        throw new Exception("Searched datetime: " + SearchDateTime + " not found.");
                    }
                    //Using index, find the event in the proper column
                    if (dCell.FindElements(By.XPath("./ancestor::table//tbody//td[" + index + "]//a[contains(@class,'day-grid-event')]//*[@class='fc-title']")).Count > 0)
                    {
                        ret = dCell.FindElement(By.XPath("./ancestor::table//tbody//td[" + index + "]//a[contains(@class,'day-grid-event')]//*[@class='fc-title']"));
                    }
                    else
                    {
                        throw new Exception("No event found on " + SearchDateTime + ".");
                    }
                    break;
            }
            return ret;
        }

        private IReadOnlyCollection<IWebElement> GetMonthEvents(String SearchDateTime)
        {
            int index = 0;
            DlkLogger.LogInfo("Getting events on MONTH view for the date [" + SearchDateTime + "]");

            IWebElement wCell = GetTargetDateTime(SearchDateTime, out index);
            if (wCell == null)
            {
                throw new Exception("Searched date: " + SearchDateTime + " not found.");
            }

            IWebElement wCalendarRow = mBody.FindElement(By.XPath(".//div[contains(@class,'fc-row')][./descendant::*[@data-date='" + SearchDateTime + "']]"));
            DlkLogger.LogInfo("Found row on the calendar");

            List<DayInMonth> daysInMonth = new List<DayInMonth>();

            foreach (IWebElement rowWithEvents in wCalendarRow.FindElements(By.XPath(".//div[@class='fc-content-skeleton']//tbody/tr")))
            {
                if (daysInMonth.Count < 1)
                    foreach (IWebElement eventsInRow in rowWithEvents.FindElements(By.TagName("td")))
                    {
                        DayInMonth newDay = new DayInMonth();
                        newDay.RowSpan = eventsInRow.GetAttribute("rowspan") != string.Empty ?
                            Convert.ToInt32(eventsInRow.GetAttribute("rowspan")) : 0;
                        newDay.Events = eventsInRow.FindElements(By.TagName("a")).ToList();
                        daysInMonth.Add(newDay);
                    }
                else
                    foreach (IWebElement eventsInRow in rowWithEvents.FindElements(By.TagName("td")))
                    {
                        int highestRowSpan = daysInMonth.Max(x => x.RowSpan);
                        foreach (DayInMonth dayInMonth in daysInMonth)
                        {
                            if (dayInMonth.Events.Count < (highestRowSpan - dayInMonth.RowSpan))
                            {
                                dayInMonth.Events.AddRange(eventsInRow.FindElements(By.TagName("a")).ToList());
                                break;
                            }
                        }
                    }
            }
            DlkLogger.LogInfo("Found [" + daysInMonth[index - 1].Events.Count + "] events on the date [" + SearchDateTime + "]");
            return daysInMonth[index - 1].Events;
        }

        private IReadOnlyCollection<IWebElement> GetWeekEvents(String SearchDateTime)
        {
            int index = 0;

            DlkLogger.LogInfo("Getting events on WEEK view for the date [" + SearchDateTime + "]");
            IWebElement wCell = GetTargetDateTime(SearchDateTime, out index);
            return mBody.FindElements(By.XPath(".//*[contains(@class,'fc-time-grid-container')]//div[@class='fc-content-skeleton']//td[" + index + "]//div[@class='fc-event-container']//a"));
        }

        private IReadOnlyCollection<IWebElement> GetDayEvents(String SearchDateTime)
        {
            DlkLogger.LogInfo("Getting events on DAY view for the date [" + SearchDateTime + "]");
            return mElement.FindElements(By.XPath(".//a[contains(@class,'fc-time-grid-event')]"));
        }

        private void MonthHoverEvent(String SearchDate, String SearchValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IWebElement mEvent = GetTargetEventFromSearchValues(dtTarget, SearchValue);
            DlkBaseControl ctlEvent = new DlkBaseControl("EventLink", mEvent);
            DlkLogger.LogInfo("Performing hover...");
            ctlEvent.MouseOver();
        }

        private void MonthClickEvent(String SearchDate, String SearchValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            String dtTime = "";
            if (SearchDate.Split(' ').Count() > 1)
            {
                dtTime = String.Format("{0:ht}", Convert.ToDateTime(SearchDate)).ToLower();
            }
            IWebElement mEvent = GetTargetEventFromSearchValues(dtTarget, SearchValue, dtTime);

            if (mEvent != null)
            {
                DlkBaseControl ctlEvent = new DlkBaseControl("EventLink", mEvent);
                DlkLogger.LogInfo("[MonthView] Performing click...");
                ctlEvent.ClickUsingJavaScript();
            }
            else
            {
                throw new Exception("[MonthView] Unable to find the event [" + SearchValue + "] on SearchDateTime [" + SearchDate + "]");
            }
        }

        private void WeekClickEvent(String SearchDate, String SearchValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IWebElement eventToBeClicked = null;
            IReadOnlyCollection<IWebElement> weekEvents = GetWeekEvents(dtTarget);
            foreach (IWebElement dayEvent in weekEvents)
            {
                if (dayEvent.FindElement(By.ClassName("fc-title")).Text == SearchValue)
                {
                    eventToBeClicked = dayEvent.FindElement(By.ClassName("fc-title"));
                    break;
                }
            }
            if (eventToBeClicked != null)
            {
                DlkLogger.LogInfo("[WeekView] Performing click...");
                eventToBeClicked.Click();
            }
            else
                throw new Exception("[WeekView] Unable to find the event [" + SearchValue + "] on SearchDateTime [" + SearchDate + "]");
        }

        private void DayClickEvent(String SearchDate, String SearchValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IWebElement eventToBeClicked = null;
            IReadOnlyCollection<IWebElement> dayEvents = GetDayEvents(dtTarget);
            foreach (IWebElement dayEvent in dayEvents)
            {
                if (dayEvent.FindElement(By.ClassName("fc-title")).Text == SearchValue)
                {
                    eventToBeClicked = dayEvent.FindElement(By.ClassName("fc-title"));
                    break;
                }
            }
            if (eventToBeClicked != null)
                eventToBeClicked.Click();
            else
                throw new Exception("[DayView] Unable to find the event [" + SearchValue + "] on SearchDateTime [" + SearchDate + "]");
        }

        private void MonthClickDateTime(String SearchDate, String SearchTime)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");

            int index = 0;
            IWebElement mDate = GetTargetDateTime(dtTarget, out index);
            if (mDate == null)
            {
                throw new Exception("Searched date: " + SearchDate + " not found.");
            }
            else if (mDate.FindElements(By.XPath("./ancestor::table[1]/tbody//td[" + index + "]//span[@class='fc-time' and contains(.,'" + SearchTime + "')]")).Count > 0)
            {
                IWebElement eventColorIndicator = null;
                eventColorIndicator = mDate.FindElement(By.XPath("./ancestor::table[1]/tbody//td[" + index + "]//span[@class='fc-time' and contains(.,'" + SearchTime + "')]"));
                DlkLogger.LogInfo("Performing click...");
                eventColorIndicator.Click();
            }
            else
            {
                throw new Exception("Searched date: " + SearchDate + " not found.");
            }
        }

        private void MonthClickDate(String SearchDate)
        {
            int index = 0;
            IWebElement mDate = GetTargetDateTime(SearchDate, out index);
            if (mDate == null)
            {
                throw new Exception("Searched date: " + SearchDate + " not found.");
            }
            DlkBaseControl ctlDate = new DlkBaseControl("CalendarDate", mDate);
            DlkLogger.LogInfo("Performing click...");
            ctlDate.ClickByObjectCoordinates();
        }

        private void MonthVerifyHeader(String SearchDate, String ExpectedValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            int index = 0;
            IWebElement mDate = GetTargetDateTime(dtTarget, out index);
            if (mDate == null)
            {
                throw new Exception("Searched date: " + SearchDate + " not found.");
            }
            IWebElement mActual = mHeader.FindElement(By.XPath(".//th[" + index + "]"));
            DlkAssert.AssertEqual("VerifyHeader()", ExpectedValue, mActual.Text);
        }

        private void MonthSeeMore(String Instance)
        {
            int index = 0;
            if (!Int32.TryParse(Instance, out index))
            {
                throw new Exception("Instance " + Instance + " is not a valid number.");
            }
            List<IWebElement> mLinks = mBody.FindElements(By.XPath(".//tbody//td[@class='fc-more-cell']//a")).ToList();
            DlkLogger.LogInfo("Performing click...");
            DlkBaseControl ctlLink = new DlkBaseControl("SeeMore", mLinks[index - 1]);
            ctlLink.ClickUsingJavaScript();
        }

        private void DayMonthScrollLeftOrRight(String NumOfScrolls, String Direction)
        {
            int scrollCount;

            if (int.TryParse(NumOfScrolls, out scrollCount))
            {
                ScrollTable(scrollCount, Direction);
            }
            else
            {
                throw new Exception("DayScrollUpOrDown() failed. Invalid NumberOfScrolls.");
            }
        }

        private void ScrollTable(int scrollcount, string pageDirection)
        {
            Actions actions = new Actions(DlkEnvironment.AutoDriver);
            IWebElement scrollBtn;
            switch (pageDirection.ToLower())
            {
                case "left":
                    scrollBtn = mElement.FindElement(By.XPath(".//button[@class='xdsoft_prev']"));
                    break;
                case "right":
                    scrollBtn = mElement.FindElement(By.XPath(".//button[@class='xdsoft_next']"));
                    break;
                default:
                    throw new Exception("Invalid direction");
            }
            for (int i = 1; i <= scrollcount; i++)
            {
                scrollBtn.Click();
                Thread.Sleep(500);
            }
        }

        private void WeekVerifyDayEvent(String SearchDate, String ExpectedValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IWebElement mEvent = GetTargetDayEvent(dtTarget);
            DlkBaseControl ctlEvent = new DlkBaseControl("Event", mEvent);
            DlkAssert.AssertEqual("VerifyDayEvent() : ", ExpectedValue, ctlEvent.GetValue());
        }

        private void DayVerifyDayEvent(String SearchDate, String ExpectedValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IWebElement mEvent = GetTargetDayEvent(dtTarget);
            DlkBaseControl ctlEvent = new DlkBaseControl("Event", mEvent);
            DlkAssert.AssertEqual("VerifyDayEvent() : ", ExpectedValue, ctlEvent.GetValue());
        }

        private void MonthVerifyEventExists(String SearchDate, String SearchTime, String SearchValue, String ExpectedValue)
        {
            bool bFound = false;
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IReadOnlyCollection<IWebElement> monthEvents = GetMonthEvents(dtTarget);

            foreach (IWebElement monthEvent in monthEvents)
            {
                string eventTime = monthEvent.FindElement(By.XPath(".//*[@class='fc-time']")).GetAttribute("textContent").ToUpper();
                string eventTitle = monthEvent.FindElement(By.XPath(".//*[@class='fc-title']")).GetAttribute("textContent");

                if ((eventTime == SearchTime.ToUpper()) && (eventTitle == SearchValue))
                {
                    DlkLogger.LogInfo("Found event: Time [" + eventTime + ", Title [" + eventTitle + "]");
                    bFound = true;
                    break;
                }
            }
            DlkAssert.AssertEqual("VerifyEventExists", Convert.ToBoolean(ExpectedValue), bFound);
        }

        private void WeekVerifyEventExists(String SearchDate, String SearchTime, String SearchValue, String ExpectedValue)
        {
            bool bFound = false;
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IReadOnlyCollection<IWebElement> weekEvents = GetWeekEvents(dtTarget);

            foreach (IWebElement weekEvent in weekEvents)
            {
                string eventTime = weekEvent.FindElement(By.XPath(".//div[@class='fc-time']")).GetAttribute("data-full").Split(new char[] { '-' })[0].Trim();
                string eventTitle = weekEvent.FindElement(By.XPath(".//div[@class='fc-title']")).GetAttribute("textContent");

                if ((eventTime == SearchTime) && (eventTitle == SearchValue))
                {
                    DlkLogger.LogInfo("Found event: Time [" + eventTime + ", Title [" + eventTitle + "]");
                    bFound = true;
                    break;
                }
            }
            DlkAssert.AssertEqual("VerifyEventExists", Convert.ToBoolean(ExpectedValue), bFound);
        }

        private void DayVerifyEventExists(String SearchDate, String SearchTime, String SearchValue, String ExpectedValue)
        {
            bool bFound = false;
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            IReadOnlyCollection<IWebElement> dayEvents = GetDayEvents(dtTarget);

            foreach (IWebElement dayEvent in dayEvents)
            {
                string eventTime = "", eventTitle = "";
                if (dayEvent.FindElements(By.XPath(".//div[@class='fc-time']")).Count > 0)
                    eventTime = dayEvent.FindElement(By.XPath(".//div[@class='fc-time']")).GetAttribute("data-full").Split(new char[] { '-' })[0].Trim();
                if (dayEvent.FindElements(By.XPath(".//div[@class='fc-title']")).Count > 0)
                    eventTitle = dayEvent.FindElement(By.XPath(".//div[@class='fc-title']")).GetAttribute("textContent");

                if ((eventTime == SearchTime) && (eventTitle == SearchValue))
                {
                    DlkLogger.LogInfo("Found event: Time [" + eventTime + ", Title [" + eventTitle + "]");
                    bFound = true;
                    break;
                }
            }

            DlkAssert.AssertEqual("VerifyEventExists", Convert.ToBoolean(ExpectedValue), bFound);
        }

        private void VerifyHeaderItems(String Items)
        {
            String ActualItems = "";
            IList<IWebElement> lstItems = mHeader.FindElements(By.XPath(".//thead//th[contains(@class,'fc-day-header')]")).ToList();
            foreach (IWebElement elem in lstItems)
            {
                ActualItems += elem.Text.Trim() + "~";
            }
            DlkAssert.AssertEqual("VerifyDayEvent() : ", Items, ActualItems.Trim('~'));
        }

        private void VerifySearchDateExists(String SearchDate, String ExpectedValue)
        {
            String dtTarget = DlkString.GetDateAsText(Convert.ToDateTime(SearchDate), "yyyy-mm-dd");
            Boolean bFound = false;
            int index = 0;
            IWebElement mEvent = GetTargetDateTime(dtTarget, out index);
            if (mEvent != null)
            {
                bFound = true;
            }
            DlkAssert.AssertEqual("VerifyDayExists() : ", Convert.ToBoolean(ExpectedValue), bFound);
        }
        #endregion

        private class DayInMonth
        {
            public int RowSpan { get; set; }
            public List<IWebElement> Events { get; set; }
        }
    }
}
