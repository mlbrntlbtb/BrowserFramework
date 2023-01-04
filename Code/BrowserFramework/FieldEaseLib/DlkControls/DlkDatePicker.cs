using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using FieldEaseLib.DlkSystem;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace FieldEaseLib.DlkControls
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

        private IWebElement mContainer;
        private IWebElement mHeaderContainer;
        private IList<IWebElement> mDateCells; 
        private static string mContainer_XPath = "//div[contains(@class,'k-calendar-container')][contains(@style,'block')]";
        private static string mHeaderContainer_XPath = ".//div[@class='k-header']";
        private static string mMonthYear_XPath = ".//a[contains(@class,'k-nav-fast')]";
        private static string mDateCell_XPath = ".//td[@role='gridcell'][not(contains(@class,'k-other-month'))]";
        private static string mMonthYearCell_XPath = ".//td[@role='gridcell']";
        private static string mIsDisabled_XPath = ".//div[@class='disabledDay']";
        //private static string mFooterContainer_XPath = ".//div[@class='k-footer']";


        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            OpenDatePicker();
            mContainer = GetContainer();
            mHeaderContainer = GetHeaderContainer(mContainer);
            Thread.Sleep(1000);
            mDateCells = GetDateCells(mContainer);
        }

        public void OpenDatePicker()
        {
            //DlkEnvironment.AutoDriver.SwitchTo().ActiveElement().SendKeys(Keys.Escape);
            if (DlkEnvironment.AutoDriver.FindElements(By.XPath(mContainer_XPath)).Count == 0)
            {
                mElement.Click();
            }
        }

        public IWebElement GetContainer()
        {
            IWebElement container = DlkEnvironment.AutoDriver.FindElements(By.XPath(mContainer_XPath)).Count > 0 ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(mContainer_XPath)) : throw new Exception("Datepicker container not found.");
            return container;
        }

        public IWebElement GetHeaderContainer(IWebElement container)
        {
            IWebElement hContainer = container.FindElements(By.XPath(mHeaderContainer_XPath)).Count > 0 ?
                container.FindElement(By.XPath(mHeaderContainer_XPath)) : throw new Exception("Datepicker header container not found.");
            return hContainer;
        }

        public IList<IWebElement> GetDateCells(IWebElement container)
        {
            IList<IWebElement> dateCells = container.FindElements(By.XPath(mDateCell_XPath))
                .Where(x => x.Displayed).ToList();

            return dateCells;
        }

        public IList<IWebElement> GetMonthYearCells(IWebElement container)
        {
            IList<IWebElement> monthYearCells = container.FindElements(By.XPath(mMonthYearCell_XPath))
                .Where(x => x.Displayed).ToList();

            return monthYearCells;
        }
        #endregion

        #region KEYWORDS

        [Keyword("SelectDate", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectDate(String Date)
        {
            try
            {
                DateTime targetDate;
                if (!DateTime.TryParse(Date, out targetDate))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                Initialize();

                string targetMonthName = targetDate.ToString("MMMM");
                string targetMonthACR = targetDate.ToString("MMM");
                string targetYear = targetDate.ToString("yyyy");
                string targetDay = Convert.ToInt32(targetDate.ToString("dd")).ToString();

                IWebElement mMonthYear = mHeaderContainer.FindElements(By.XPath(mMonthYear_XPath)).Count > 0 ?
                    mHeaderContainer.FindElement(By.XPath(mMonthYear_XPath)) : throw new Exception("Month and year not found in header container");

                bool dFound = false;
                string currentMonthYear = mMonthYear.Text.Trim();

                // If target date does not match with month and year
                if(!currentMonthYear.Equals(targetMonthName + " " + targetYear))
                {
                    mMonthYear.Click(); //Opens month table
                    Thread.Sleep(2000);
                    mMonthYear.Click(); //Opens year table
                    Thread.Sleep(2000);

                    //Select target month
                    IList<IWebElement> mYearList = GetMonthYearCells(mContainer);
                    foreach (IWebElement year in mYearList)
                    {
                        string currentYear = year.Text.Trim();
                        if (currentYear.Equals(targetYear))
                        {
                            year.Click();
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    //Select target year
                    IList<IWebElement> mMonthList = GetMonthYearCells(mContainer);
                    foreach (IWebElement month in mMonthList)
                    {
                        string currentMonth = month.Text.Trim();
                        if (currentMonth.Equals(targetMonthACR))
                        {
                            month.Click();
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    mDateCells = GetDateCells(mContainer);
                }
                
                foreach (IWebElement dateCell in mDateCells)
                {
                    string currentDate = dateCell.Text.Trim();
                    if (currentDate.Equals(targetDay))
                    {
                        if(dateCell.FindElements(By.XPath(mIsDisabled_XPath)).Count > 0)
                        {
                            throw new Exception("Target date not selected since it is disabled.");
                        }
                        else
                        {
                            dateCell.Click();
                            DlkLogger.LogInfo("Target date [" + Date + "] selected.");
                            Thread.Sleep(1000);
                            dFound = true;
                            break;
                        }
                    }
                }

                if (!dFound)
                {
                    mElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    mElement.SendKeys(Keys.Escape);
                    throw new Exception("Selected date not found in the calendar");
                }

                DlkLogger.LogInfo("SelectDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectDate() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDateDisabled", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDateDisabled(String Date, String TrueOrFalse)
        {
            try
            {
                DateTime targetDate;
                if (!DateTime.TryParse(Date, out targetDate))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                bool isDisabled = false;

                Initialize();

                string targetMonthName = targetDate.ToString("MMMM");
                string targetMonthACR = targetDate.ToString("MMM");
                string targetYear = targetDate.ToString("yyyy");
                string targetDay = Convert.ToInt32(targetDate.ToString("dd")).ToString();

                IWebElement mMonthYear = mHeaderContainer.FindElements(By.XPath(mMonthYear_XPath)).Count > 0 ?
                    mHeaderContainer.FindElement(By.XPath(mMonthYear_XPath)) : throw new Exception("Month and year not found in header container");

                bool dFound = false;
                string currentMonthYear = mMonthYear.Text.Trim();

                // If target date does not match with month and year
                if (!currentMonthYear.Equals(targetMonthName + " " + targetYear))
                {
                    mMonthYear.Click(); //Opens month table
                    Thread.Sleep(2000);
                    mMonthYear.Click(); //Opens year table
                    Thread.Sleep(2000);

                    //Select target month
                    IList<IWebElement> mYearList = GetMonthYearCells(mContainer);
                    foreach (IWebElement year in mYearList)
                    {
                        string currentYear = year.Text.Trim();
                        if (currentYear.Equals(targetYear))
                        {
                            year.Click();
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    //Select target year
                    IList<IWebElement> mMonthList = GetMonthYearCells(mContainer);
                    foreach (IWebElement month in mMonthList)
                    {
                        string currentMonth = month.Text.Trim();
                        if (currentMonth.Equals(targetMonthACR))
                        {
                            month.Click();
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    mDateCells = GetDateCells(mContainer);
                }

                foreach (IWebElement dateCell in mDateCells)
                {
                    string currentDate = dateCell.Text.Trim();
                    if (currentDate.Equals(targetDay))
                    {
                        if (dateCell.FindElements(By.XPath(mIsDisabled_XPath)).Count > 0)
                        {
                            DlkLogger.LogInfo("Target date [" + Date + "] is currently disabled.");
                            isDisabled = true;
                        }
                        else
                        {
                            isDisabled = false;
                        }
                        dFound = true;
                        break;
                    }
                }

                if (!dFound)
                {
                    mElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    mElement.SendKeys(Keys.Escape);
                    throw new Exception("Selected date not found in the calendar");
                }

                DlkAssert.AssertEqual("VerifyDateDisabled(): ", expectedValue, isDisabled);
                DlkLogger.LogInfo("VerifyDateDisabled() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDateDisabled() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}