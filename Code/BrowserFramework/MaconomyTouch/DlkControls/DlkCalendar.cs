using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;
using System.Globalization;
using System.Threading;
using System.Drawing;
using CommonLib.DlkUtility;
using MaconomyTouchLib.System;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkCalendar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCalendar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region CONSTANTS
        private const string prevYearClass = "goto-prevyear";
        private const string prevMonthClass = "goto-prevmonth";
        private const string nextMonthClass = "goto-nextmonth";
        private const string nextYearClass = "goto-nextyear";

        public static readonly Color[] redColors = 
        { 
            Color.FromArgb(170, 0, 0),
            Color.FromArgb(160, 0, 0)
        };

        public static readonly Color[] grayColors = 
        {
            Color.FromArgb(211, 210, 215),
            Color.FromArgb(100, 100, 100)
        };

        public static readonly Color[] greenColors =
        {
             Color.FromArgb(75, 150, 45),
             Color.FromArgb(83, 168, 0)
        };

        public static readonly Color[] blueColors = 
        {
            Color.FromArgb(35, 107, 142),
            Color.FromArgb(0, 128, 255)
        };

        #endregion

        #region VARIABLES
        private DateTime currentMonthYear;
        #endregion

        #region PUBLIC FUNCTIONS
        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();

            string[] _currentMonthYear = mElement.FindElement(By.ClassName("month-year-label")).GetAttribute("textContent").Trim().Split(' ');
            currentMonthYear = new DateTime(
                Convert.ToInt32(_currentMonthYear[1]),
                DateTime.ParseExact(_currentMonthYear[0], "MMMM", CultureInfo.CurrentCulture).Month,
                1);
        }
        #endregion

        #region PRIVATE FUNCTIONS
        /// <summary>
        /// This function will use the calendar's buttons to go to the page where the required date can be found
        /// </summary>
        /// <param name="datetime"></param>
        private void NavigateTo(DateTime requiredDate)
        {
            DlkLogger.LogInfo(String.Format("Attempting to navigate to page {0:MMMM-yyyy}", requiredDate.Date));
            
            Initialize();
            int yearDifference = (requiredDate.Year - currentMonthYear.Year);
            int monthDifference = (requiredDate.Month - currentMonthYear.Month);
            string selectedBtnClass;

            if (yearDifference > 0)
                selectedBtnClass = nextYearClass;
            else if (yearDifference < 0)
                selectedBtnClass = prevYearClass;
            else
            {
                if (monthDifference > 0)
                    selectedBtnClass = nextMonthClass;
                else if (monthDifference < 0)
                    selectedBtnClass = prevMonthClass;
                else
                    selectedBtnClass = "";
            }

            if (!String.IsNullOrEmpty(selectedBtnClass))
            {
                int count = Math.Abs(yearDifference != 0 ? yearDifference : monthDifference);

                for (int i = 0; i < count; i++)
                {
                    Initialize();
                    DlkButton btnToBeClicked = new DlkButton("CalendarButton", mElement.FindElement(By.ClassName(selectedBtnClass)));
                    DlkLogger.LogInfo(String.Format("Clicking button with class '{0}'...",selectedBtnClass));
                    btnToBeClicked.Click();
                    Thread.Sleep(2000);
                }
                NavigateTo(requiredDate);
            }
            else
            {
                return;
            }
        }

        private bool FindDateWithColor(List<IWebElement> dates, Color[] desiredColor)
        {
            bool found = false;
            DlkLogger.LogInfo(String.Format("Finding DesiredColor in {0} {1}", currentMonthYear.Month, currentMonthYear.Year));
            foreach (IWebElement date in dates)
            {
                string[] ActualColor = date.GetCssValue("background-color").Split(new char[] { '(', ')', ',' });
                Color actualColor = Color.FromArgb(Convert.ToInt32(ActualColor[1]), Convert.ToInt32(ActualColor[2]), Convert.ToInt32(ActualColor[3]));
                //GetColorName(actualColor);
                if (desiredColor.Any(x => x == actualColor))
                {
                    DlkLogger.LogInfo(String.Format("Selecting Date [{0}]...", date.GetAttribute("datetime")));
                    DlkBaseControl dateBtn = new DlkBaseControl("Date", date);
                    dateBtn.Click();
                    found = true;
                    break;
                }
            }
            return found;
        }

        #endregion

        #region KEYWORDS
        /// <summary>
        /// This keyword will navigate to the page of the specified date and select corresponding date in the calendar control
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("SelectDate", new String[] { "1|text|Value|2017-07-07" })]
        public void SelectDate(String Value)
        {
            try
            {
                Initialize();
                DateTime requiredDate = new DateTime();
                if (!DateTime.TryParse(Value, out requiredDate))
                    throw new Exception("Invalid Date. Kindly provide a valid date.");

                NavigateTo(requiredDate);

                string requiredDateTime = String.Format("{0:yyyy-MM-dd}", requiredDate.Date);
                new DlkButton("CalendarButton", mElement.FindElement(By.XPath(String.Format(".//td[@datetime='{0}']", requiredDateTime)))).Click();
                DlkLogger.LogInfo(String.Format("SelectDate() : Successfully selected date {0:yyyy-MM-dd}", requiredDate.Date));
            }
            catch (Exception ex)
            {
                throw new Exception("SelectDate() failed : Error encountered while selecting date.", ex);
            }
        }

        /// <summary>
        /// This keyword will navigate to the page of the specified date and select corresponding date in the calendar control
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("AssignDateToVariable", new String[] { "1|text|Value|2017-07-07" })]
        public void AssignDateToVariable(String VariableName)
        {
            try
            {
                Initialize();
                string format = "M/d/yyyy";
                IWebElement selectedDate = mElement.FindElement(By.XPath(".//*[contains(@class,\'selected\')]"));
                if (selectedDate != null)
                {
                    var selectedValue = selectedDate.GetAttribute("datetime");
                    string dtValue = DlkString.GetDateAsText(Convert.ToDateTime(selectedValue), format);
                    DlkVariable.SetVariable(VariableName, dtValue);
                    DlkLogger.LogInfo("Successfully executed AssignDateToVariable(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");

                }
                else
                {
                    throw  new Exception("No selected date.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AssignDateToVariable() failed :", ex);
            }
        }

        /// <summary>
        /// This keyword will allow the user to control the navigation of the calendar by specifying which button to click, 
        /// how many times it must be clicked, and the interval of each clicks
        /// </summary>
        /// <param name="Button"></param>
        /// <param name="Instance"></param>
        /// <param name="Interval"></param>
        [Keyword("NavigatePeriod", new String[] { "RightArrow|1|10" })]
        public void NavigatePeriod(string Button, string Instance, string Interval)
        {
            try
            {
                string selectedBtnClass;
                int iteration = 0;
                int interval = 0;

                Initialize();

                switch (Button.ToLower().Trim())
                {
                    case "rightarrow": selectedBtnClass = nextMonthClass; break;
                    case "leftarrow": selectedBtnClass = prevMonthClass; break;
                    case "doublerightarrow": selectedBtnClass = nextYearClass; break;
                    case "doubleleftarrow": selectedBtnClass = prevYearClass; break;
                    default: throw new Exception("[Button] Invalid input. The following are the only valid values for Button parameter: [RightArrow | LeftArrow | DoubleRightArrow | DoubleLeftArrow]");
                }

                if (!Int32.TryParse(Instance, out iteration) | iteration < 1)
                    throw new Exception("[Instance] Invalid input. Kindly supply a valid positive integer.");

                if (!Int32.TryParse(Instance, out interval) | interval < 0)
                    throw new Exception("[Interval] Invalid input. Kindly supply a valid integer.");
                else
                    interval = interval * 1000;

                for (int i = 0; i < iteration; i++)
                {
                    Initialize();
                    DlkButton btnToBeClicked = new DlkButton("CalendarButton", mElement.FindElement(By.ClassName(selectedBtnClass)));
                    DlkLogger.LogInfo(String.Format("Clicking button '{0}' (x{1})...", Button, (i+1)));
                    btnToBeClicked.Click();
                    Thread.Sleep(interval);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("NavigatePeriod() failed : Error encountered while navigating to selected period.", ex);
            }
        }

        /// <summary>
        /// This keyword will check of the color of the provided date and throw an error when the expected color is not met
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="ExpectedColor"></param>
        [Keyword("VerifyColor", new String[] { "7/20/2017|Green" })]
        public void VerifyColor(string Date, string ExpectedColor)
        {
            try
            {
                Color expectedColor, actualColor;
                DateTime requiredDate = new DateTime();

                if (!DateTime.TryParse(Date, out requiredDate))
                    throw new Exception("Invalid Date. Kindly provide a valid date.");
                NavigateTo(requiredDate);

                switch (ExpectedColor.ToLower().Trim())
                {
                    case "red": expectedColor = Color.FromArgb(170, 0, 0);
                        break;
                    case "blue": expectedColor = Color.FromArgb(121, 146, 180);
                        break;
                    case "green": expectedColor = Color.FromArgb(75, 150, 45);
                        break;
                    case "gray": expectedColor = Color.FromArgb(211, 210, 215);
                        break;
                    default:
                        throw new Exception("Invalid Color. The following is the list of valid inputs: [ Red | Blue | Green | Gray ].");
                }

                string requiredDateTime = String.Format("{0:yyyy-MM-dd}", requiredDate.Date);
                IWebElement date = mElement.FindElement(By.XPath(String.Format(".//td[@datetime='{0}']", requiredDateTime)));
                string[] ActualColor = date.GetCssValue("background-color").Split(new char[] {'(', ')', ','});

                actualColor = Color.FromArgb(Convert.ToInt32(ActualColor[1]), Convert.ToInt32(ActualColor[2]), Convert.ToInt32(ActualColor[3]));

                DlkAssert.AssertEqual("VerifyColor()", expectedColor.ToString(), actualColor.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColor() failed : " + ex.Message, ex);
            }
        }

        /// <summary>
        /// This keyword will verify if the provided date is selected in the calendar by checking if the class contains 'selected'
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyDateSelected", new String[] { "7/20/2017|True" })]
        public void VerifyDateSelected(string Date, string TrueOrFalse)
        {
            try
            {
                Initialize();

                DateTime requiredDate = new DateTime();
                if (!DateTime.TryParse(Date, out requiredDate))
                    throw new Exception("Invalid Date. Kindly provide a valid date.");

                bool ExpectedValue = false, ActualValue = false;
                if (!Boolean.TryParse(TrueOrFalse, out ExpectedValue))
                    throw new Exception("Invalid input for TrueOrFalse. Kindly enter a valid boolean value.");
                                
                string requiredDateTime = String.Format("{0:yyyy-MM-dd}", requiredDate.Date);
                IWebElement date = mElement.FindElement(By.XPath(String.Format(".//td[@datetime='{0}']", requiredDateTime)));
                if (date.GetAttribute("class").Contains("selected"))
                    ActualValue = true;

                DlkAssert.AssertEqual("VerifyDateSelected()", ExpectedValue, ActualValue);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyDateSelected() failed : " + ex.Message, ex);
            }
        }

        /// <summary>
        /// This keyword will verify if the period currently shown is equal to the provided parameter
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyPeriod", new String[] { "July 2017|True" })]
        public void VerifyPeriod(string ExpectedValue)
        {
            try
            {
                Initialize();
                string ActualValue = mElement.FindElement(By.ClassName("month-year-label")).GetAttribute("textContent").Trim();
                DlkAssert.AssertEqual("VerifyPeriod()", ExpectedValue, ActualValue);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyPeriod() failed : " + ex.Message, ex);
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
        /// Select a date by the given color. 
        /// If a date of that color is not found in the current (starting) period, it would navigate to the previous period to look.
        /// If a date of that color is still not found, it would navigate to the next period from the starting period.
        /// If a date of that color is still not found, the keyword would fail.
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SelectDateByColor", new String[] { "1|text|Expected Value|Red" })]
        public void SelectDateByColor(String DesiredColor)
        {
            try
            {
                Color[] desiredColor;
                switch (DesiredColor.ToLower().Trim())
                {
                    case "red":
                        desiredColor = redColors;
                        break;
                    case "blue": desiredColor = blueColors;
                        break;
                    case "green": desiredColor = greenColors;
                        break;
                    case "gray": desiredColor = grayColors;
                        break;
                    default:
                        throw new Exception("Invalid Color. The following is the list of valid inputs: [ Red | Blue | Green | Gray ].");
                }

                Initialize();
                List<IWebElement> currPeriodDates = mElement.FindElements(By.XPath("..//td[contains(@class, 'day')]")).ToList();
                // Try to find within the starting period
                if(!FindDateWithColor(currPeriodDates, desiredColor))
                {
                    //Navigate to previous period
                    NavigatePeriod("leftarrow", "1", "5");
                    Initialize();
                    currPeriodDates = mElement.FindElements(By.XPath("..//td[contains(@class, 'day')]")).ToList();

                    if(!FindDateWithColor(currPeriodDates, desiredColor))
                    {
                        //Navigate to next period from starting
                        NavigatePeriod("rightarrow", "2", "5");
                        Initialize();
                        currPeriodDates = mElement.FindElements(By.XPath("..//td[contains(@class, 'day')]")).ToList();
                        
                        if (!FindDateWithColor(currPeriodDates, desiredColor))
                        {
                            throw new Exception("Unable to find DesiredColor.");
                        }
                    }
                }
                DlkLogger.LogInfo("SelectDateByColor() successful.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectDateByColor() failed : " + e.Message, e);
            }
        
        }

        /// <summary>
        /// Select a date by the given day (Monday, Tuesday, etc.) 
        /// </summary>
        /// <param name="DesiredDay"></param>
        /// <param name="Status">Default is In Progress or Due</param>
        [Keyword("SelectDateByDay", new String[] { "1|text|Expected Value|Red" })]
        public void SelectDateByDay(String DesiredDay, String Status = null)
        {
            try
            {
                int iDay;
                Initialize();
                //Finding the index of the header dynamically since the start of the week can be changed between Mon and Sun
                List<IWebElement> HeaderDays = mElement.FindElements(By.XPath(".//thead//th")).ToList();
                iDay = HeaderDays.FindIndex(x => x.Text.ToLower().Trim() == DesiredDay.ToLower().Trim());
                
                List<Color> desiredColors = new List<Color>();
                switch (Status.ToLower().Trim())
                {
                    case null:
                    case "":
                        desiredColors.AddRange(grayColors);
                        desiredColors.AddRange(redColors);
                        break;
                    case "in progress":
                        desiredColors.AddRange(grayColors);
                        break;
                    case "due":
                        desiredColors.AddRange(redColors);
                        break;
                    case "submitted":
                        desiredColors.AddRange(greenColors);
                        break;
                    case "approved":
                        desiredColors.AddRange(blueColors);
                        break;
                    default:
                        throw new Exception("Unrecognized Status.");
                }

                const int DAYS_IN_A_WEEK = 7;

                // Get all dates that fall under desired day
                List<IWebElement> dates = mElement.FindElements(By.XPath(".//td")).Where((date, idx) => (idx + (DAYS_IN_A_WEEK - iDay)) % DAYS_IN_A_WEEK == 0).ToList();

                if(!FindDateWithColor(dates, desiredColors.ToArray()))
                {
                    throw new Exception("No Day [" + DesiredDay + "] found that has a Status of " + Status);
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectDateByDay() failed : " + e.Message, e);
            }
        }

         /// <summary>
        /// This keyword will verify if the period currently shown is equal to the provided parameter
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyNavButtonExists", new String[] { "July 2017|True" })]
        public void VerifyNavButtonExists(string Button, String TrueOrFalse)
        {
            try
            {
                string selectedBtnClass;
                bool expected, actual;

                if (!Boolean.TryParse(TrueOrFalse, out expected)) throw new Exception("Invalid input for TrueOrFalse");

                Initialize();

                switch (Button.ToLower().Trim())
                {
                    case "rightarrow": 
                        selectedBtnClass = nextMonthClass; 
                        break;
                    case "leftarrow": 
                        selectedBtnClass = prevMonthClass; 
                        break;
                    case "doublerightarrow": 
                        selectedBtnClass = nextYearClass; 
                        break;
                    case "doubleleftarrow": 
                        selectedBtnClass = prevYearClass; 
                        break;
                    default: throw new Exception("[Button] Invalid input. The following are the only valid values for Button parameter: [RightArrow | LeftArrow | DoubleRightArrow | DoubleLeftArrow]");
                }

                actual = mElement.FindElements(By.ClassName(selectedBtnClass)).Where(x => x.Displayed).Count() > 0;
                DlkAssert.AssertEqual("VerifyNavButtonExists", expected, actual);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNavButtonExists() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
