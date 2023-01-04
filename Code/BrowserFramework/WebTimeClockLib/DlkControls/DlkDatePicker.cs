using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WebTimeClockLib.DlkSystem;
using System.Threading;

namespace WebTimeClockLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES

        private static string inputYear_XPath = ".//input[contains(@id,'year')]";
        private static string inputMonth_XPath = ".//input[contains(@id,'month')]";
        private static string inputDay_XPath = ".//input[contains(@id,'day')]";
        private IWebElement inputYear;
        private IWebElement inputMonth;
        private IWebElement inputDay;

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
        }

        #endregion

        #region PRIVATE METHODS

        public void GetFields()
        {
            inputYear = mElement.FindElements(By.XPath(inputYear_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(inputYear_XPath)) : throw new Exception("Year field not found.");
            inputMonth = mElement.FindElements(By.XPath(inputMonth_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(inputMonth_XPath)) : throw new Exception("Month field not found.");
            inputDay = mElement.FindElements(By.XPath(inputDay_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(inputDay_XPath)) : throw new Exception("Day field not found.");
        }

        public new string GetValue()
        {
            GetFields();

            string year = new DlkBaseControl("Target Year", inputYear).GetValue().Trim();
            string month = new DlkBaseControl("Target Month", inputMonth).GetValue().Trim();
            string day = new DlkBaseControl("Target Day", inputDay).GetValue().Trim();

            string actualValue = year + "/" + month + "/" + day;
            return actualValue;
        }

        #endregion

        #region KEYWORDS
        [Keyword("SetValue")]
        public void SetValue(String Date)
        {
            try
            {
                DateTime date;
                if (! DateTime.TryParse(Date, out date))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                string convertedDate = date.ToString("yyyy/MM/dd");
                string year = Convert.ToDateTime(convertedDate).Year.ToString();
                string month = Convert.ToDateTime(convertedDate).Month.ToString();
                string day = Convert.ToDateTime(convertedDate).Day.ToString();

                Initialize();
                GetFields();

                inputYear.Click();
                inputYear.SendKeys(year);

                inputMonth.Click();
                inputMonth.SendKeys(month);

                inputDay.Click();
                inputDay.SendKeys(day);

                DlkLogger.LogInfo("SetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue")]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();
                string actualValue = GetValue();
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue")]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                string actualValue = GetValue();
                DlkAssert.AssertEqual("VerifyValue() :", ExpectedValue, actualValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
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
