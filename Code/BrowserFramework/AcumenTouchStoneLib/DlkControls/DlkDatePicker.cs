using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        private IWebElement mCalendarComponent = null;
        private IWebElement mHeaderComponent = null;
        private IWebElement mPrev = null;
        private IWebElement mNext = null;
        private IWebElement mHome = null;


        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            {
                DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

                FindElement();

                mHeaderComponent = mElement.FindElement(By.ClassName("xdsoft_monthpicker"));
                mCalendarComponent = mElement.FindElement(By.ClassName("xdsoft_calendar"));
                mPrev = mHeaderComponent.FindElement(By.XPath("./button[@class='xdsoft_prev']"));
                mNext = mHeaderComponent.FindElement(By.XPath("./button[@class='xdsoft_next']"));
                mHome = mHeaderComponent.FindElement(By.XPath("./button[@class='xdsoft_today_button']"));
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        new public void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                String mValue = this.GetValue().TrimEnd();
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
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

        [Keyword("ClickDatePickerButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickDatePickerButton(String ButtonName)
        {
            try
            {
                Initialize();
                switch (ButtonName.ToLower())
                {
                    case "home":
                        mHome.Click();
                        break;
                    case "previous":
                        mPrev.Click();
                        break;
                    case "next":
                        mNext.Click();
                        break;
                    default:
                        throw new Exception("ClickDatePickerButton() failed : " + ButtonName + " not recognized as a valid datepicker button");
                }
                DlkLogger.LogInfo("ClickDatePickerButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickDatePickerButton() failed : " + e.Message, e);
            }
        }

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
                //string strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-year')]/option[@selected='selected']"))).GetValue();
                string strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.XPath("./div[contains(@class,'xdsoft_year')]/span"))).GetValue();
                int actualYear = int.Parse(strActualYear);
                while (targetYear != actualYear) // year is not yet set
                {
                    //while(targetMonth != actualMonth)
                    {
                        Initialize();
                        IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                        myButton.Click();
                        Initialize();
                        strActualYear = new DlkBaseControl("ActualYear", mHeaderComponent.FindElement(By.XPath("./div[contains(@class,'xdsoft_year')]/span"))).GetValue();
                        actualYear = int.Parse(strActualYear);
                    }
                }
                Initialize();
                //string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-month')]/option[@selected='selected']"))).GetValue();
                string strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.XPath("./div[contains(@class,'xdsoft_month')]/span"))).GetValue();
                int actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                while (targetMonth != actualMonth)
                {
                    {
                        Initialize();
                        IWebElement myButton = targetMonth > actualMonth ? mNext : mPrev;
                        myButton.Click();
                        Initialize();
                        //strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.XPath(".//select[contains(@class,'ui-datepicker-month')]/option[@selected='selected']"))).GetValue();
                        strActualMonth = new DlkBaseControl("ActualMonth", mHeaderComponent.FindElement(By.XPath("./div[contains(@class,'xdsoft_month')]/span"))).GetValue();
                        actualMonth = Convert.ToDateTime(strActualMonth + " 01, 1900").Month;
                    }
                }

                // Click on Day
                IWebElement targetDay = mCalendarComponent.FindElement(By.XPath("./descendant::td[@data-date='" + convertedDate.Day.ToString() + "'][not(contains(@class,'other_month'))]"));
                targetDay.Click();
                DlkLogger.LogInfo("SetDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
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
    }
}
