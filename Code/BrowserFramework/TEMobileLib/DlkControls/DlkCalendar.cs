using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkBaseControl
    {

        public DlkCalendar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
                FindElement();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectDate", new String[] { "1|text|Date to select|01/01/2012" })]
        public void SelectDate(String DateToSelect)
        {
            try
            {
                Initialize();

                DateTime dtmDateToSelect = Convert.ToDateTime(DateToSelect);

                // Select 'Month' using 'Right' arrow, use of drop down fails to trigger event
                // critical to setting Date textbox value
                IWebElement objMonthControl = null;
                if (mElement.FindElements(By.Id("calMo")).Count == 0)
                {
                    throw new Exception("MonthControl not found");
                }
                objMonthControl = mElement.FindElements(By.Id("calMo")).First();

                IWebElement objRightArrow = null;
                if (mElement.FindElements(By.ClassName("popupCalRArrow")).Count == 0)
                {
                    throw new Exception("RightArrow nor found");
                }
                objRightArrow = mElement.FindElements(By.ClassName("popupCalRArrow")).First();

                string origValue = objMonthControl.Text;
                string currValue = origValue;
                do
                {
                    if (currValue == dtmDateToSelect.ToString("MMMM"))
                    {
                        break;
                    }
                    objRightArrow.Click();
                    currValue = objMonthControl.Text;
                }
                while (origValue != currValue);

                // Set 'Year' value
                IWebElement objYearControl = null;
                if (mElement.FindElements(By.Id("calYrEdit")).Count == 0)
                {
                    throw new Exception("YearControl not found");

                }
                objYearControl = mElement.FindElements(By.Id("calYrEdit")).First();
                DlkTextBox txtYear = new DlkTextBox("Year Edit Box", objYearControl);
                txtYear.Set(dtmDateToSelect.Year.ToString());

                // Click 'Day' tile in calendar grid
                IWebElement objDayTile = null;
                if (mElement.FindElements(By.XPath("./descendant::div[@class='popupCalDate' and text()='" + dtmDateToSelect.Day.ToString() + "']")).Count == 0)
                {
                    throw new Exception("DayTile not found");
                }
                objDayTile = mElement.FindElements(By.XPath("./descendant::div[@class='popupCalDate' and text()='" + dtmDateToSelect.Day.ToString() + "']")).First();
                objDayTile.Click();

                DlkLogger.LogInfo("Successfully executed SelectDate()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectDate() : " + e.Message, e);
            }
        }

        [Keyword("ClickToday")]
        public void ClickToday()
        {
            try
            {
                Initialize();

                IWebElement objButton = null;
                if (mElement.FindElements(By.Id("calTdyBtn")).Count == 0)
                {
                    throw new Exception("Today button not found");
                }
                objButton = mElement.FindElements(By.Id("calTdyBtn")).First();
                DlkBaseControl ctlTodayButton = new DlkBaseControl("TodayButton", objButton);
                ctlTodayButton.Click();
                DlkLogger.LogInfo("Successfully executed ClickToday()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToday() failed : " + e.Message, e);
            }
        }

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();
                IWebElement objButton = null;
                if (mElement.FindElements(By.Id("sqlCalClose")).Count == 0)
                {
                    throw new Exception("Close button not found");
                }
                objButton = mElement.FindElements(By.Id("sqlCalClose")).First();
                DlkBaseControl ctlCloseButton = new DlkBaseControl("CloseButton", objButton);
                ctlCloseButton.Click();
                DlkLogger.LogInfo("Successfully executed Close()");
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

    }
}
