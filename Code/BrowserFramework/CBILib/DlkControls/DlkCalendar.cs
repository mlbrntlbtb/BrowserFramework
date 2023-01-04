using CBILib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkBaseControl
    {
        #region constructors
        public DlkCalendar(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkCalendar(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkCalendar(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }


        public DlkCalendar(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public DlkCalendar(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion

        private void Initialize()
        {
            DlkCERCommon.WaitForPromptSpinner();
            FindElement();
        }

        [Keyword("SetYear", new String[] { "1|text|Year|2021" })]
        public void SetYear(string Year)
        {
            try
            {
                Initialize();
                IWebElement txtYear = mElement.FindElements(By.XPath(".//input[contains(@id,'myYear')]")).FirstOrDefault();

                if (txtYear != null)
                {
                    txtYear.SendKeys(Keys.Control + "a");
                    txtYear.SendKeys(Year);
                }                    
                else
                    throw new Exception("Year input box not found.");

                DlkLogger.LogInfo("SetYear() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetYear() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectMonth", new String[] { "1|text|Month|Jan" })]
        public void SelectMonth(string Month)
        {
            try
            {
                Initialize();

                //not using xpath query to avoid nbsp tag issue
                var months = mElement.FindElements(By.XPath(".//td[contains(@class,'clsSelectDateMonths')]")).ToList();

                foreach (var month in months)
                {
                    if (month.Text.ToLower() == Month.ToLower())
                    {
                        month.Click();
                        break;
                    }
                }
                DlkLogger.LogInfo("SelectMonth() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectMonth() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectDay", new String[] { "1|text|Day|1" })]
        public void SelectDay(string Day)
        {
            try
            {
                Initialize();

                //not using xpath query to avoid nbsp tag issue
                var days = mElement.FindElements(By.XPath(".//td[contains(@class,'clsSelectDateDays')]")).ToList();

                foreach (var day in days)
                {
                    if (day.Text.ToLower() == Day.ToLower())
                    {
                        day.Click();
                        break;
                    }
                }
                DlkLogger.LogInfo("SelectDay() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectDay() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickYearDown")]
        public void ClickYearDown()
        {
            try
            {
                Initialize();
                var yearDownButton = mElement.FindElements(By.XPath(".//img[contains(@id,'btnYearDown')]")).FirstOrDefault();

                if (yearDownButton != null)
                    yearDownButton.Click();
                else
                    throw new Exception("Year down button not found.");

                DlkLogger.LogInfo("ClickYearDown() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickYearDown() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickYearUp")]
        public void ClickYearUp()
        {
            try
            {
                Initialize();
                var yearUpButton = mElement.FindElements(By.XPath(".//img[contains(@id,'btnYearUp')]")).FirstOrDefault();

                if (yearUpButton != null)
                    yearUpButton.Click();
                else
                    throw new Exception("Year up button not found.");

                DlkLogger.LogInfo("ClickYearUp() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickYearUp() failed : " + e.Message, e);
            }
        }
    }
}
