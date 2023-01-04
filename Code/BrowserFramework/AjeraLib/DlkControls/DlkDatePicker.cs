using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AjeraLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker :DlkAjeraBaseControl
    {

        #region DECLARATIONS

        private IWebElement mCalendarComponent = null;
        private IWebElement mHeaderComponent = null;
        private IWebElement mPrev = null;
        private IWebElement mNext = null;

        #endregion

        #region CONSTRUCTORS
        public DlkDatePicker(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue){}
        public DlkDatePicker(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues){}
        public DlkDatePicker(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}
        public DlkDatePicker(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}
        public DlkDatePicker(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public void Initialize()
        {
            FindElement();

            if (mSearchValues[0].Contains("Run this widget as of"))
            {
                mHeaderComponent = mElement.FindElement(By.XPath(".//*[contains(@class,'ui-datepicker-header')]"));
                mCalendarComponent = mElement.FindElement(By.XPath(".//*[contains(@class,'ui-datepicker-calendar')]"));
                mPrev = mElement.FindElement(By.XPath(".//*[contains(@class,'ui-datepicker-prev')]"));
                mNext = mElement.FindElement(By.XPath(".//*[contains(@class,'ui-datepicker-next')]"));
            }
            else 
            {
                mHeaderComponent = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-header')]"));
                mCalendarComponent = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-calendar')]"));
                mPrev = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-prev')]"));
                mNext = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-next')]"));
            }
        }

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
            mHeaderComponent = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-header')]"));
            mCalendarComponent = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-calendar')]"));
            mPrev = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-prev')]"));
            mNext = mElement.FindElement(By.XPath("//*[contains(@class,'ui-datepicker-next')]"));
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

        [Keyword("SetDate", new String[] { "1|text|Date to set|XXX" })]
        public void SetDate(String Date)
        {
            try
            {
                //display date picker
                FindElement();
                if (!mSearchValues[0].Contains("Run this widget as of"))
                {
                    mElement.Clear();
                    mElement.SendKeys(Keys.Space);
                    mElement.Click();
                }

                Initialize();
                // set month and year
                DateTime convertedDate = DateTime.Parse(Date);
                int targetMonth = convertedDate.Month;
                int targetYear = convertedDate.Year;

                string strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-year"))).GetValue();
                int actualYear = int.Parse(strActualYear);
                while (targetYear != actualYear) // year is not yet set
                {
                    {
                        Initialize();
                        IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                        myButton.Click();
                        Initialize();
                        strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-year"))).GetValue();
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
                        myButton.Click();
                        Initialize();
                        strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                        actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                    }
                }

                // Click on Day
                IWebElement targetDay = mCalendarComponent.FindElement(By.XPath("./descendant::a[text()='" + convertedDate.Day + "'][not(contains(@class,'secondary'))]"));
                targetDay.Click();
                DlkLogger.LogInfo("SetDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }
        }

        [Keyword("EnterDate", new String[] { "1|text|Date to set|XXX" })]
        public void EnterDate(String Date)
        {
            try
            {
                if (mSearchValues[0].Contains("Run this widget as of"))
                {
                    mSearchValues[0] =  mSearchValues[0] + "/preceding-sibling::span/input";
                }
                FindElement();
                mElement.Clear();
                if (!string.IsNullOrEmpty(Date))
                {
                    mElement.SendKeys(Date);

                }

                if (mElement.GetAttribute("value").ToLower() != Date.ToLower())
                {
                    throw new Exception("EnterDate() failed : Cannot enter value : "+ Date + "as input.");
                }
                DlkLogger.LogInfo("Successfully executed EnterDate()");
            }
            catch (Exception e)
            {
                throw new Exception("EnterDate() failed : " + e.Message, e);
            }
        }
        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST
        [Keyword("VerifyExistsByRow", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExistsByRow(String RowNumber, String IsTrueOrFalse)
        {
            try
            {
                string actualValue = SelectedElementExists(RowNumber).ToString();
                DlkAssert.AssertEqual("VerifyExistsByRow()", IsTrueOrFalse.ToLower(), DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(actualValue.ToLower())));
                DlkLogger.LogInfo("VerifyExistsByRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("SetDateByRow", new String[] { "1|text|Date to set|XXX" })]
        public void SetDateByRow(String RowNumber, String Date)
        {
            try
            {
                //display date picker
                FindElement();
                mElement.Clear();
                mElement.SendKeys(Keys.Space);
                mElement.Click();

                InitializeRow(RowNumber);
                // set month and year
                DateTime convertedDate = DateTime.Parse(Date);
                int targetMonth = convertedDate.Month;
                int targetYear = convertedDate.Year;

                // actual
                string strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-year"))).GetValue();
                int actualYear = int.Parse(strActualYear);
                while (targetYear != actualYear) // year is not yet set
                {
                    {
                        InitializeRow(RowNumber);
                        IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                        myButton.Click();
                        InitializeRow(RowNumber);
                        strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-year"))).GetValue();
                        actualYear = int.Parse(strActualYear);
                    }
                }

                InitializeRow(RowNumber);
                string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                int actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                while (targetMonth != actualMonth)
                {
                    {
                        InitializeRow(RowNumber);
                        IWebElement myButton = targetMonth > actualMonth ? mNext : mPrev;
                        myButton.Click();
                        InitializeRow(RowNumber);
                        strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.ClassName("ui-datepicker-month"))).GetValue();
                        actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                    }
                }

                // Click on Day
                IWebElement targetDay = mCalendarComponent.FindElement(By.XPath("./descendant::a[text()='" + convertedDate.Day + "'][not(contains(@class,'secondary'))]"));
                targetDay.Click();
                DlkLogger.LogInfo("SetDateByRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDateByRow() failed : " + e.Message, e);
            }
        }
        #endregion

    }
}
