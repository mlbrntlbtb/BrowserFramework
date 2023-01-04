using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("PeriodPicker")]
    public class DlkPeriodPicker : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private IWebElement mPrev = null;
        private IWebElement mNext = null;
        private IWebElement mPeriod = null;

        #endregion

        #region CONSTRUCTORS

        public DlkPeriodPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPeriodPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPeriodPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                mPeriod = mElement.FindElement(By.XPath(".//span[1]"));
                mPrev = mElement.FindElement(By.XPath(".//a[@class='triangle west']"));
                mNext = mElement.FindElement(By.XPath(".//a[@class='triangle east']"));
            }
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
                int actualYear = int.Parse(strActualYear.Substring(0, 4));
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
