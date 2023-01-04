using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Drawing;
using System.Linq;

namespace MaconomyTouchLib.DlkControls
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
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        item = mElement.FindElement(By.XPath(".//div[@class='workday'][contains(.,'" + Value + "')]"));
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

                if (DlkEnvironment.mIsMobile && (!item.Displayed || item.Location.X < 0))
                {
                    //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                    Point selectedNativeCoord = btnItem.GetNativeViewCoordinates();
                    Point calNativeStartCoord = GetNativeViewCoordinates();
                    Point calNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                    if (selectedNativeCoord.X < calNativeStartCoord.X || selectedNativeCoord.X > calNativeEndCoord.X || selectedNativeCoord.X > DlkEnvironment.mDeviceHeight)
                    {
                        Point calNativeCenterCoord = GetNativeViewCenterCoordinates();
                        double dXTranslation = Convert.ToDouble(selectedNativeCoord.X) - Convert.ToDouble(calNativeCenterCoord.X);
                        if (dXTranslation < 0)
                        {
                            Swipe(SwipeDirection.Right, Convert.ToInt32(Math.Abs(dXTranslation)), SwipeOrigin.LeftCenter);
                        }
                        else
                        {
                            Swipe(SwipeDirection.Left, Convert.ToInt32(dXTranslation));
                        }

                    }
                }

                btnItem.ScrollIntoViewUsingJavaScript();
                btnItem.Click();

            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        [Keyword("ClickLeftArrow")]
        public void ClickLeftArrow()
        {
            try
            {
                Initialize();
                IWebElement arrowLeft = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        arrowLeft = mElement.FindElement(By.XPath("//div[@class='tapArrowLeft']"));
                        if (arrowLeft != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkButton btnLeft = new DlkButton("LeftArrow", arrowLeft);
                btnLeft.ScrollIntoViewUsingJavaScript();
                btnLeft.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickLeftArrow() failed : " + e.Message, e);
            }

        }

        [Keyword("ClickRightArrow")]
        public void ClickRightArrow()
        {
            try
            {
                Initialize();
                IWebElement arrowRight = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        arrowRight = mElement.FindElement(By.XPath("//div[@class='tapArrowRight']"));
                        if (arrowRight != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkButton btnRight = new DlkButton("RightArrow", arrowRight);
                btnRight.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickRightArrow() failed : " + e.Message, e);
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

        [Keyword("VerifyDay", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDay(String Day, String TrueOrFalse)
        {
            try
            {
                bool ExpectedValue=false, ActualValue=false;
                if(!Boolean.TryParse(TrueOrFalse,out ExpectedValue))
                    throw new Exception("Invalid value for TrueorFalse. Kindly enter a valid boolean parameter.");

                Initialize();

                IWebElement item = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        item = mElement.FindElement(By.XPath(".//div[@class='workday']//*[.=" + Day + "]"));
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

                if(item.GetAttribute("class").ToLower().Contains("select"))
                    ActualValue = true;

                DlkAssert.AssertEqual("VerifyDay", ExpectedValue, ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDay() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                int dateIndex = 0;
                if (!Int32.TryParse(Index, out dateIndex))
                {
                    throw new Exception("SelectByIndex() : Invalid input for date index [" + Index + "]");
                }

                Initialize();

                var itemList = mElement.FindElements(By.XPath(".//div[contains(@id,'daytype')]")).Where(x => x.Displayed).ToList();
                if (dateIndex > itemList.Count)
                {
                    throw new Exception("SelectByIndex() : Index [" + Index + "] is greater than calendar carousel ");
                }

                int count = 0;
                foreach (var item in itemList)
                    if (item != null)
                    {
                        count++;
                        if (count == dateIndex)
                        {
                            DlkBaseButton dateButton = new DlkBaseButton("Date", item);
                            dateButton.Click();
                            break;
                        }
                    }
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignIndexToVariable", new String[] { "17|SampleVariable" })]
        public void AssignIndexToVariable(String Day, String VariableName)
        {
            try
            {
                Initialize();
                var itemList = mElement.FindElements(By.XPath(".//div[contains(@id,'daytype')]/div[contains(@class,'calNumber')]")).Where(x => x.Displayed).ToList();
               
                int count = 0;
                int requestedIndex = 0;
                foreach (var item in itemList)
                {
                    if (item != null)
                    {
                        count++;
                        if (item.Text == Day)
                        {
                            requestedIndex = count;
                            break;
                        }
                    }
                }

                DlkVariable.SetVariable(VariableName, requestedIndex.ToString());
                DlkLogger.LogInfo("Successfully executed AssignIndexToVariable(). Variable:[" + VariableName + "], Value:[" + requestedIndex + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignIndexToVariable() failed : " + e.Message, e);
            }
        }
        
    }
}
