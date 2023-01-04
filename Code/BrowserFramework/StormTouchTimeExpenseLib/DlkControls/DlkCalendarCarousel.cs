using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;

namespace StormTouchTimeExpenseLib.DlkControls
{
    [ControlType("CalendarCarousel")]
    public class DlkCalendarCarousel : DlkBaseControl
    {
        public DlkCalendarCarousel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendarCarousel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCalendarCarousel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            
        }

        [Keyword("Select", new String[] { "1|text|Value|MON" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                IWebElement item = null;
                int targetX = -1;

                while (targetX < 0 || targetX > DlkEnvironment.mDeviceWidth)
                {
                    try
                    {
                        item = mElement.FindElement(By.XPath("./descendant::*[contains(@class, 'calNumber')][text()='" + Value + "']/ancestor::*[@class='workday'][1]"));
                        targetX = new DlkBaseControl("target", item).GetNativeViewCenterCoordinates().X;
                    }
                    catch (NoSuchElementException)
                    {
                        throw new Exception("The target carousel item was not found.");
                    }

                    /* Swipe left */
                    if (targetX > DlkEnvironment.mDeviceWidth)
                    {
                        this.Swipe(SwipeDirection.Left, (DlkEnvironment.mDeviceWidth / 2) - 1);
                    }
                    else if (targetX < 0) /* Swipe right */
                    {
                        this.Swipe(SwipeDirection.Right, (DlkEnvironment.mDeviceWidth / 2) - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                DlkButton btnItem = new DlkButton("Item", item);
                btnItem.Click();

            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        //[Keyword("ClickLeftArrow")]
        //public void ClickLeftArrow()
        //{
        //    try
        //    {
        //        Initialize();
        //        IWebElement arrowLeft = null;
        //        for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
        //        {
        //            try
        //            {
        //                arrowLeft = mElement.FindElement(By.XPath("//div[@class='tapArrowLeft']"));
        //                if (arrowLeft != null)
        //                {
        //                    break;
        //                }
        //            }
        //            catch (OpenQA.Selenium.NoSuchElementException)
        //            {
        //            }
        //            Thread.Sleep(1000);
        //        }
        //        DlkButton btnLeft = new DlkButton("LeftArrow", arrowLeft);
        //        btnLeft.ScrollIntoViewUsingJavaScript();
        //        btnLeft.Click();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("ClickLeftArrow() failed : " + e.Message, e);
        //    }

        //}

        //[Keyword("ClickRightArrow")]
        //public void ClickRightArrow()
        //{
        //    try
        //    {
        //        Initialize();
        //        IWebElement arrowRight = null;
        //        for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
        //        {
        //            try
        //            {
        //                arrowRight = mElement.FindElement(By.XPath("//div[@class='tapArrowRight']"));
        //                if (arrowRight != null)
        //                {
        //                    break;
        //                }
        //            }
        //            catch (OpenQA.Selenium.NoSuchElementException)
        //            {
        //            }
        //            Thread.Sleep(1000);
        //        }
        //        DlkButton btnRight = new DlkButton("RightArrow", arrowRight);
        //        btnRight.Click();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("ClickRightArrow() failed : " + e.Message, e);
        //    }

        //}

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

        [Keyword("SelectedDay", new String[] { "MON|TRUE" })]
        public void SelectedDay(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement item = null;
                var ActualValue = string.Empty;
                try
                {
                    item = mElement.FindElement(By.XPath(".//div[contains(@class,'calNumberSelect')]/preceding-sibling::div[1]"));
                    ActualValue = item.Text.Trim();
                    
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("No carousel item was selected.");
                }
                DlkAssert.AssertEqual("SelectedDay() : " + ExpectedValue, ExpectedValue, ActualValue);

            }
            catch (Exception e)
            {
                throw new Exception("SelectedDay() failed : " + e.Message, e);
            }

        }


        [Keyword("VerifyDay", new String[] { "1|text|Value|MON" })]
        public void VerifyDay(String Value, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement item;
                string selectedDay = string.Empty;
                bool selectBackDay = false;
                try
                {
                    item = mElement.FindElement(By.XPath(".//div[contains(@class,'calNumber')]/preceding-sibling::div[1][text()='" + Value + "']"));
                    if (!item.Displayed)
                    {
                        var selectedItem = mElement.FindElement(By.XPath(".//div[contains(@class,'calNumberSelect')]/preceding-sibling::div[1]"));
                        selectedDay = selectedItem.Text.Trim();
                        SwipeIntoView(item);
                        selectBackDay = true;
                    }
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("The target carousel item was not found.");
                }

                DlkButton btnItem = new DlkButton("Item", item);
                btnItem.VerifyExists(TrueOrFalse);

                if(selectBackDay)
                    SelectDay(selectedDay);

            }
            catch (Exception e)
            {
                throw new Exception("VerifyDay() failed : " + e.Message, e);
            }

        }

        public void SelectDay(String Value)
        {
            try
            {
                Initialize();
                IWebElement item = null;
                int targetX = -1;

                while (targetX < 0 || targetX > DlkEnvironment.mDeviceWidth)
                {
                    try
                    {
                        item = mElement.FindElement(By.XPath("./descendant::*[contains(@class,'calNumber')]/preceding-sibling::div[1][text()='" + Value + "']"));
                        targetX = new DlkBaseControl("target", item).GetNativeViewCenterCoordinates().X;
                    }
                    catch (NoSuchElementException)
                    {
                        throw new Exception("The target carousel item was not found.");
                    }

                    /* Swipe left */
                    if (targetX > DlkEnvironment.mDeviceWidth)
                    {
                        this.Swipe(SwipeDirection.Left, (DlkEnvironment.mDeviceWidth / 2) - 1);
                    }
                    else if (targetX < 0) /* Swipe right */
                    {
                        this.Swipe(SwipeDirection.Right, (DlkEnvironment.mDeviceWidth / 2) - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                DlkButton btnItem = new DlkButton("Item", item);
                btnItem.Click();

            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        private void SwipeIntoView(IWebElement item)
        {
            int targetX = -1;
            while (targetX < 0 || targetX > DlkEnvironment.mDeviceWidth)
            {
                try
                {
                    targetX = new DlkBaseControl("target", item).GetNativeViewCenterCoordinates().X;
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("The target carousel item was not found.");
                }

                /* Swipe left */
                if (targetX > DlkEnvironment.mDeviceWidth)
                {
                    this.Swipe(SwipeDirection.Left, (DlkEnvironment.mDeviceWidth / 2) - 1);
                }
                else if (targetX < 0) /* Swipe right */
                {
                    this.Swipe(SwipeDirection.Right, (DlkEnvironment.mDeviceWidth / 2) - 1);
                }
                else
                {
                    break;
                }
            }
        }

    }
}
