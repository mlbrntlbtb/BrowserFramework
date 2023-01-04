using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using AjeraTimeLib.DlkSystem;

namespace AjeraTimeLib.DlkControls
{
    [ControlType("CalendarCarousel")]
    public class DlkCalendarCarousel : DlkBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;
        #endregion

        #region CONSTRUCTOR
        public DlkCalendarCarousel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendarCarousel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCalendarCarousel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("SelectDay", new String[] { "1|text|Value|MON" })]
        public void SelectDay(String Value)
        {
            try
            {
                Initialize();
                IWebElement item = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        item = mElement.FindElement(By.XPath("./..//div[contains(@class, 'ax-week-topbox')][text()='" + Value + "']/ancestor::*[contains(@id, 'Day_Box')]"));
                        if (item != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkButton btnItem = new DlkButton("Item", item);
                btnItem.ScrollIntoViewUsingJavaScript();

                btnItem.Click();

            }
            catch (Exception e)
            {
                throw new Exception("SelectDay() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDay", new String[] { "1|text|Value|MON" })]
        public void VerifyDay(String Index, String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = GetItemByIndex(Index, "Day");
                DlkAssert.AssertEqual("VerifyDay() : " + mControlName, ExpectedValue, ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDay() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDate", new String[] { "1|text|Value|1st" })]
        public void VerifyDate(String Index, String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = GetItemByIndex(Index, "Date");
                DlkAssert.AssertEqual("VerifyDate() : " + mControlName, ExpectedValue, ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDate() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHours", new String[] { "1|text|Value|0" })]
        public void VerifyHours(String Index, String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = GetItemByIndex(Index, "Hours");
                DlkAssert.AssertEqual("VerifyDate() : " + mControlName, ExpectedValue, ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHours() failed : " + e.Message, e);
            }
        }
        #endregion

        #region METHODS
        public String GetItemByIndex(String Index, String CalendarOption = "")
        {
            String[] actualResult;
            IWebElement item = null;
            String result = "";
            IList<IWebElement> mElementList = mElement.FindElements(By.XPath("//div[contains(@class, 'ax-week-topbox')]/ancestor::*[contains(@id, 'Day_Box')]"));
            if (Convert.ToInt32(Index) - 1 > mElementList.Count)
            {
                throw new Exception("'" + Index + "' is out of bounds.");
            }

            item = mElementList.ElementAt(Convert.ToInt32(Index) - 1);

            actualResult = item.Text.ToString().Trim().Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            switch(CalendarOption)
            {
                case "Day":
                    result = actualResult[0];
                    break;
                case "Date":
                    result = actualResult[1];
                    break;
                case "Hours":
                    result = actualResult[2];
                    break;
                default:
                    result = item.Text.ToString().Trim().Replace("\r\n", ";");
                    break;
            }
            return result;
        }
        #endregion
    }
}
