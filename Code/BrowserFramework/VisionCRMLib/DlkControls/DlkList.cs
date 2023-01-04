using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace VisionCRMLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }
        

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();

                IWebElement mSelected = null;
                bool bFound = false;
                string containerClass = string.Empty;
                for (int i = 0; i < iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        if (mElement.GetAttribute("class").Contains("fieldset"))
                        {
                            var elementContainer = ".//div[contains(@class, 'chevron_fields')]//div[contains(@class, 'deltekfldlbl')]";                            
                            mSelected = mElement.FindElement(By.XPath(elementContainer + "[contains(., '" + Value + "')]"));
                            if (mSelected != null)
                            {
                                bFound = true;
                                break;
                            }
                        }
                        else
                        {
                            var elementContainer = ".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']";
                            containerClass = ".//div[@class='x-innerhtml']";
                            var container = mElement.FindElements(By.XPath(containerClass));
                            if (container == null || container.Count == 0)
                            {
                                elementContainer = ".//div[contains(@class, 'x-dataview-container')]//div[contains(@class,'dataview-item')]";
                                containerClass = ".div[contains(@class,'dataview-item')]";
                            }

                            mSelected = mElement.FindElement(By.XPath(elementContainer + "[contains(., '" + Value + "')]"));
                            if (mSelected != null)
                            {
                                bFound = true;
                                break;
                            }
                        }
                        
                    }
                    catch (NoSuchElementException)
                    {

                    }
                    Thread.Sleep(1000);
                }

                if (!bFound)
                {
                    for (int i = 0; i < iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            foreach (IWebElement elem in mElement.FindElements(By.XPath(containerClass)))
                            {
                                DlkBaseControl ctl = new DlkBaseControl("Item", elem);
                                if (ctl.GetValue().Contains(Value))
                                {
                                    mSelected = elem;
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                        catch (OpenQA.Selenium.NoSuchElementException)
                        {
                        }
                        Thread.Sleep(1000);
                    }
                }
                DlkBaseControl mSelectedItem = new DlkBaseControl("Selected", mSelected);
                mSelectedItem.ScrollIntoViewUsingJavaScript();

                if (DlkEnvironment.mIsMobile)
                {
                    //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                    Point selectedNativeCoord = mSelectedItem.GetNativeViewCoordinates();
                    Point listNativeStartCoord = GetNativeViewCoordinates();
                    Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                    if (selectedNativeCoord.Y < listNativeStartCoord.Y || selectedNativeCoord.Y > listNativeEndCoord.Y || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight)
                    {
                        Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                        double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                        if (dYTranslation < 0)
                        {
                            Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                        }
                        else
                        {
                            Swipe(SwipeDirection.Up, Convert.ToInt32(dYTranslation));
                        }

                    }
                }
                else
                {
                    while (mSelected.Location.Y > DlkEnvironment.AutoDriver.Manage().Window.Size.Height || mSelected.Location.Y < 0)
                    {
                        DragToAppear(mSelected);
                    }
                }
                
                mSelectedItem.Click();
                DlkLogger.LogInfo("Select() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        [Keyword("SelectByRow", new String[] { "1|text|Row|1" })]
        public void SelectByRow(String Row)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        if (mElement.GetAttribute("class").Contains("fieldset"))
                        {
                            mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'chevron_fields')]//div[contains(@class, 'deltekfldlbl')]")).ElementAt(int.Parse(Row) - 1);
                            if (mSelected != null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ElementAt(int.Parse(Row) - 1);
                            if (mSelected != null)
                            {
                                break;
                            }
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                ctlItem.ScrollIntoViewUsingJavaScript();

                if (DlkEnvironment.mIsMobile)
                {
                    //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                    Point selectedNativeCoord = ctlItem.GetNativeViewCoordinates();
                    Point listNativeStartCoord = GetNativeViewCoordinates();
                    Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                    if (selectedNativeCoord.Y < listNativeStartCoord.Y || selectedNativeCoord.Y > listNativeEndCoord.Y || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight)
                    {
                        Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                        double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                        if (dYTranslation < 0)
                        {
                            Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                        }
                        else
                        {
                            Swipe(SwipeDirection.Up, Convert.ToInt32(dYTranslation));
                        }

                    }
                }
                else
                {
                    while (mSelected.Location.Y > DlkEnvironment.AutoDriver.Manage().Window.Size.Height || mSelected.Location.Y < 0)
                    {
                        DragToAppear(mSelected);
                    }
                }
                ctlItem.Click();
                DlkLogger.LogInfo("SelectByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickRowButton", new String[] { "1|text|Row|1" })]
        public void ClickRowButton(String Row)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                IWebElement mButton = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ElementAt(int.Parse(Row) - 1);
                        if (mSelected != null)
                        {
                            mButton = mSelected.FindElement(By.XPath(".//div[contains(@class, 'Icon')]"));
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkBaseControl ctlButton = new DlkBaseControl("Button", mButton);
                ctlButton.ScrollIntoViewUsingJavaScript();

                if (DlkEnvironment.mIsMobile)
                {
                    //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                    Point selectedNativeCoord = ctlButton.GetNativeViewCoordinates();
                    Point listNativeStartCoord = GetNativeViewCoordinates();
                    Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                    if (selectedNativeCoord.Y < listNativeStartCoord.Y || selectedNativeCoord.Y > listNativeEndCoord.Y || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight)
                    {
                        Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                        double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                        if (dYTranslation < 0)
                        {
                            Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                        }
                        else
                        {
                            Swipe(SwipeDirection.Up, Convert.ToInt32(dYTranslation));
                        }

                    }
                }
                else
                {
                    while (mSelected.Location.Y > DlkEnvironment.AutoDriver.Manage().Window.Size.Height || mSelected.Location.Y < 0)
                    {
                        DragToAppear(mSelected);
                    }
                }
                ctlButton.Click();
                DlkLogger.LogInfo("ClickRowButton() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickRowButton() failed : " + e.Message, e);
            }
        }

        [Keyword("SwipeToRefresh", new String[] { "1|text|Expected Value|Up" })]
        public void SwipeToRefresh(String UpOrDown, String NumberOfSwipes)
        {
            try
            {
                Initialize();
                string mDirection = UpOrDown.ToLower();
                SwipeDirection mSwipeDirection;
                int swipes = 0;
                int swipeDist = DlkEnvironment.mDeviceHeight / 2;

                if (mDirection.Equals("up"))
                {
                    mSwipeDirection = SwipeDirection.Up;
                }
                else if (mDirection.Equals("down"))
                {

                    mSwipeDirection = SwipeDirection.Down;
                    swipeDist = swipeDist / 2 ;
                }
                else
                {
                    throw new Exception("SwipeToRefresh() failed : Invalid direction.");
                }
                while (swipes < Convert.ToInt32(NumberOfSwipes))
                {
                    Swipe(mSwipeDirection, swipeDist);
                    /* Pause for refresh*/
                    Thread.Sleep(1000);
                    swipes++;
                    DlkLogger.LogInfo("Swipe() performed");
                }
                DlkEnvironment.SetContext("WEBVIEW");
                DlkLogger.LogInfo("SwipeToRefresh() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SwipeToRefresh() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithText", new String[] { "1|text|SearchedText|SampleValue",
                                                  "2|text|VariableName|MyRow"})]
        public void GetRowWithText(String SearchedText, String VariableName)
        {
            try
            {
                Initialize();
                bool bFound = false;
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        if (mElement.GetAttribute("class").Contains("fieldset"))
                        {
                            var elementContainer = ".//div[contains(@class, 'chevron_fields')]//div[contains(@class, 'deltekfldlbl')]";
                            mSelected = mElement.FindElement(By.XPath(elementContainer + "[contains(., '" + SearchedText + "')]"));
                            if (mSelected != null)
                            {
                                new DlkBaseControl("selectedItem", mSelected).ScrollIntoViewUsingJavaScript();
                                IList<IWebElement> lstItems = mElement.FindElements(By.XPath(elementContainer)).ToList();
                                int index = 0;
                                for (index = 0; index < lstItems.Count; index++)
                                {
                                    DlkBaseControl ctlItem = new DlkBaseControl("Item", lstItems[index]);
                                    if (ctlItem.GetValue().Contains(SearchedText))
                                    {
                                        DlkVariable.SetVariable(VariableName, (index + 1).ToString());
                                        bFound = true;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            mSelected = mElement.FindElement(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml'][contains(., '" + SearchedText + "')]"));
                            if (mSelected != null)
                            {
                                new DlkBaseControl("selectedItem", mSelected).ScrollIntoViewUsingJavaScript();
                                IList<IWebElement> lstItems = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ToList();
                                int index = 0;
                                for (index = 0; index < lstItems.Count; index++)
                                {
                                    DlkBaseControl ctlItem = new DlkBaseControl("Item", lstItems[index]);
                                    if (ctlItem.GetValue().Contains(SearchedText))
                                    {
                                        DlkVariable.SetVariable(VariableName, (index + 1).ToString());
                                        bFound = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }

                if (!bFound)
                {
                    throw new Exception("Unable to find list item with value '" + SearchedText + "'");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|SearchedText|SampleValue",
                                                  "2|text|VariableName|MyRow"})]
        public void VerifyAvailableInList(String ListItem, String ExpectedValue)
        {
            try
            {
                Initialize();
                bool bFound = false;
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElement(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml'][contains(., '" + ListItem + "')]"));
                        if (mSelected != null)
                        {
                            bFound = true;
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }              
                DlkAssert.AssertEqual("VerifyTextContains", Convert.ToBoolean(ExpectedValue), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Row|1",
                                                      "2|text|ExpectedText|Job1"})]
        public void VerifyTextContains(String Row, String ExpectedText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        if (mElement.GetAttribute("class").Contains("fieldset"))
                        {
                            mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'chevron_fields')]//div[contains(@class, 'deltekfldlbl')]")).ElementAt(int.Parse(Row) - 1);
                            if (mSelected != null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ElementAt(int.Parse(Row) - 1);
                            if (mSelected != null)
                            {
                                break;
                            }
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                DlkAssert.AssertEqual("VerifyTextContains", true, ctlItem.GetValue().Contains(ExpectedText));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
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

        public void DragToAppear(IWebElement SelectedItem)
        {
            Point target = SelectedItem.Location;
            Point destination = new Point();
            IWebElement dragElem = null;
            ICollection<IWebElement> elems = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]")).ToList();
            foreach (IWebElement elms in elems)
            {
                if (elms.Location.Y > 0)
                {
                    dragElem = elms;
                    break;
                }
            }

            if (SelectedItem.Location.Y < 0)
            {
                destination.X = target.X;
                destination.Y = 0 + Math.Abs(target.Y);
            }
            else
            {
                destination.X = target.X;
                destination.Y = 0 - target.Y;
            }
            new DlkBaseControl("", dragElem).DragAndDropToOffset(destination.X, destination.Y);
            Thread.Sleep(1000);
        }

    }
}
