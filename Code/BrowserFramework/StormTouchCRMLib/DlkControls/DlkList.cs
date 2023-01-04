using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Drawing;

namespace StormTouchCRMLib.DlkControls
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

                Action elseBlock = () => {
                    var elementContainer = ".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']";
                    containerClass = ".//div[@class='x-innerhtml']";
                    var container = mElement.FindElements(By.XPath(containerClass));
                    if (container == null || container.Count == 0)
                    {
                        elementContainer = ".//div[contains(@class, 'x-dataview-container')]//div[contains(@class,'dataview-item')]";
                        containerClass = ".div[contains(@class,'dataview-item')]";
                    }

                    foreach (IWebElement elm in mElement.FindElements(By.XPath(elementContainer)))
                     {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()).Contains(Value))
                        {
                            mSelected = ctl.mElement;
                            return;
                        }
                    }
                };

                Func<bool> nullCheck = () =>
                    {
                        if (mSelected != null)
                        {
                            bFound = true;
                            return bFound;
                        }
                        return bFound;
                    };

                for (int i = 0; i < iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        if (mElement.GetAttribute("class").Contains("fieldset"))
                        {
                            var elementContainer = ".//div[contains(@class, 'chevron_fields')]//div[contains(@class, 'deltekfldlbl')]";                            
                            mSelected = mElement.FindElement(By.XPath(elementContainer + "[contains(., '" + Value + "')]"));
                            if (nullCheck())
                                break;
                            
                        }
                        //T# 696311, the List in this task is different from the other lists in terms of class
                        else if (mElement.FindElements(By.XPath(".//div[contains(@class,'x-inner')]")).Count > 0)
                        {                                                
                            if (nullCheck())
                            {
                                break;
                            }
                            else
                            {
                                elseBlock();
                                if (nullCheck())
                                    break;
                            }
                        }
                        else
                        {
                            elseBlock();
                            if (nullCheck())
                            {
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
                if (bFound)
                {
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
                else
                {
                    throw new Exception("Value not found.");
                }
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
                        mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ElementAt(int.Parse(Row) - 1);
                        if (mSelected != null)
                        {
                            break;
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
                int curRetry = 0;
                IList<IWebElement> listItems;
                IWebElement mSelected = null;
                while (curRetry++ <= this.iFindElementDefaultSearchMax)
                {
                    int index = 1;
                    listItems = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).Where(x => x.Displayed).ToList();
                    // check for multi-line items.
                    if (mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']//div[contains(@class,'Title')]")).Where(x => x.Displayed).Count() > 0)
                    {
                        listItems = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']//div[contains(@class,'Title')]")).Where(x => x.Displayed).ToList();
                    }
                    if (!bFound)
                    {
                        foreach (IWebElement listItem in listItems)
                        {
                            if (DlkString.NormalizeNonBreakingSpace(listItem.Text) == SearchedText)
                            {
                                bFound = true;
                                mSelected = listItem;

                                DlkBaseControl ctlItem = new DlkBaseControl("Item", listItem);
                                if (DlkString.NormalizeNonBreakingSpace(ctlItem.GetValue()).Contains(SearchedText))
                                {
                                    DlkVariable.SetVariable(VariableName, (index).ToString());
                                    DlkLogger.LogInfo("Row index stored in variable : [" + VariableName + " : " + index + "]");
                                    bFound = true;
                                    break;
                                }
                            }
                            index++;
                        }
                    }
                    else
                    {
                        break;
                    }
                  
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

        [Keyword("VerifyCompletedCheckmark")]
        public void VerifyCompletedCheckmark(String Row, String TrueOrFalse)
        {
            try
            {
                // variable area
                int row = 0;
                Boolean ActualResult = false;
                Boolean ExpectedResult = false;
                
                // guard clauses
                if (String.IsNullOrWhiteSpace(Row)) throw new ArgumentException("Row must not be empty.");
                if (!int.TryParse(Row, out row)) throw new ArgumentException("Row must be a number.");
                if (!Boolean.TryParse(TrueOrFalse, out ExpectedResult)) throw new ArgumentException("TrueOrFalse must be a Boolean value.");
                if (row < 1) throw new ArgumentException("Row must be greater than 0.");
                if (String.IsNullOrWhiteSpace(TrueOrFalse)) throw new ArgumentException("TrueOrFalse must not be empty.");
                
                Initialize();
                var mRow = mElement.FindElement(By.XPath(String.Format(".//div[contains(@class,'x-list-item')][{0}]", Row)));
                // check if the checkmark exists. if yes then it the activity is completed.
                if (mRow.FindElements(By.XPath(".//div[@class='activityTaskCompleted']")).Count > 0)
                {
                    ActualResult = true;
                }
                DlkAssert.AssertEqual("Verify if Completed", ExpectedResult, ActualResult);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCompletedCheckmark() failed: "+ ex.Message);
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
                int curRetry = 0;
                IList<IWebElement> mItems;

                while (curRetry++ <= this.iFindElementDefaultSearchMax)
                {
                    if (bFound)
                    {
                        break;
                    }

                    //Using this instead of loop to be able to verify all elements in a list item not just the Title or the Small detail
                    mItems = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']//*[.='" + ListItem + "']")).Where(x => x.Displayed).ToList();
                    if (mItems.Count > 0)
                    {
                        bFound = true;
                    }
                }

                DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(ExpectedValue), bFound);
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
                        mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ElementAt(int.Parse(Row) - 1);
                        if (mSelected != null)
                        {
                            break;
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

        [Keyword("VerifyDate", new String[] { "1|text|Row|1"})]
        public void VerifyDate(String Date)
        {
            try
            {
                Initialize();
                var dates = mElement.FindElements(By.XPath(string.Format(".//*[text()='{0}']", Date))).Where(date => date.Displayed).ToList();
                if (! (dates.Count() > 0))
                {
                    throw new Exception(string.Format("The date {0} is not present in the list. Please see the exception image.", Date));
                }
                DlkLogger.LogInfo(String.Format("{0} is in the list.", Date));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDay", new String[] { "1|text|Row|1" })]
        public void VerifyDay(String Day)
        {
            try
            {
                Initialize();
                var Days = mElement.FindElements(By.XPath(string.Format(".//*[text()='{0}']", Day))).Where(dayOfWeek => dayOfWeek.Displayed).ToList();
                if (Days.Count() < 1) //if zero
                {
                    throw new Exception(string.Format("The date {0} is not present in the list. Please see the exception image.", Day));
                }
                foreach (var day in Days)
                {
                    if (day.Text.Equals(Day))
                    {
                        var date = day.FindElement(By.XPath("./following-sibling::*[contains(@class,'TaskActivityListRightHeader')]")).Text;
                        DlkLogger.LogInfo(String.Format("{0} is in the list, beside {1}", day.Text, date));
                    }
                }
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

        [Keyword("SelectCheckMark", new String[] { "1|text|Row|1" })]
        public void SelectCheckMark(String Row)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']//div[contains(translate(@class,'C','c'),'complete')]")).ElementAt(int.Parse(Row) - 1);
                        if (mSelected != null)
                        {
                            break;
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
                DlkLogger.LogInfo("SelectCheckMark() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCheckMark() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCheckMark", new String[] { "1|text|Row|1" })]
        public void VerifyCheckMark(String Row, String IsTrueOrFalse)
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
                        mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")).ElementAt(int.Parse(Row) - 1);

                        if (mSelected != null)
                        {
                            DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                            ctlItem.ScrollIntoViewUsingJavaScript();
                            var checkMark = mSelected.FindElement(By.XPath(".//div[contains(translate(@class,'C','c'),'complete')]"));
                            if (checkMark != null && checkMark.Displayed)
                                bFound = true;
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
             
                DlkAssert.AssertEqual("VerifyCheckMark() " + Row + ":" , bFound, Convert.ToBoolean(IsTrueOrFalse));

                DlkLogger.LogInfo("VerifyCheckMark() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCheckMark() failed : " + e.Message, e);
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
