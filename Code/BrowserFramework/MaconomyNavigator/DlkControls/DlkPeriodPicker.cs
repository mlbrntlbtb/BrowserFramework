using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyNavigatorLib.DlkControls
{
    [ControlType("PeriodPicker")]
    public class DlkPeriodPicker : DlkBaseControl
    {
        private IWebElement mPrev = null;
        private IWebElement mNext = null;
        private IWebElement mPeriod = null;

        public DlkPeriodPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPeriodPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPeriodPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            {
                FindElement();
                mPeriod = mElement.FindElement(By.XPath(".//span[1]"));
                mPrev = mElement.FindElement(By.XPath(".//a[@class='triangle west']"));
                mNext = mElement.FindElement(By.XPath(".//a[@class='triangle east']"));
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

        [Keyword("SetPeriod", new String[] { "1|text|Period to set|XXX" })]
        public void SetPeriod(String Year)
        {
            try
            {
                // set month and year
                int targetYear = Convert.ToInt32(Year);

                Initialize();
                // actual
                string strActualYear = new DlkBaseControl("ActualYear", mPeriod).GetValue();
                int actualYear = int.Parse(strActualYear.Substring(0,4));
                while (targetYear != actualYear) // year is not yet set
                {
                    //while(targetMonth != actualMonth)
                    {
                        Initialize();
                        IWebElement myButton = targetYear > actualYear ? mNext : mPrev;
                        myButton.Click();
                        Thread.Sleep(1000);
                        Initialize();
                        strActualYear = new DlkBaseControl("ActualYear", mPeriod).GetValue();
                        actualYear = int.Parse(strActualYear.Substring(0, 4));
                    }
                }
                Initialize();

                DlkLogger.LogInfo("SetPeriod() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetPeriod() failed : " + e.Message, e);
            }
        }

        public void GoToNextMonth()
        {

        }

        public void GoToPreviousMonth()
        {

        }

        public void VerifyStatus(String Date, String Status)
        {

        }
    }
}
