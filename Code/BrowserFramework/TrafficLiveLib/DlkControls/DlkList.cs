using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System.Drawing;
using System.Threading;
using CommonLib.DlkUtility;


namespace TrafficLiveLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        private string mItemsXPath = ".//div[contains(@class, 'x-list-item')]//div[contains(@class,'x-innerhtml')]";
        private string mItemIconXpath = ".//div[@class='swipebuttonsDiv']//div/*[contains(@class,'swipelistbutton')]"; // Xpath of the Add or Edit icon that appears after an item in the list is swiped.
        string listContainer = string.Empty;
        #region CONSTRUCTOR

        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();

            if (!mSearchValues[0].Contains("select-overlay"))
            {
                listContainer = "/div/div";
            }

        }

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

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String ColumnIndex, String ExactText, String OptionalRowIndex = "", String OptionalDate = "")
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                if (String.IsNullOrWhiteSpace(OptionalDate))
                {
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            /* Get all items */

                            foreach (IWebElement elm in mElement.FindElements(By.XPath(mItemsXPath + listContainer + "[" + ColumnIndex + "]")))
                            {
                                DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                                string ctlVal = DlkString.ReplaceCarriageReturn(ctl.GetValue().Trim(), "~");
                                if (ctlVal == DlkString.ReplaceCarriageReturn(ExactText.Trim(), ""))
                                {
                                    mSelected = elm;
                                    break;
                                }
                            }

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
                }
                else if (!String.IsNullOrWhiteSpace(OptionalDate))
                {
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            /* Get all items */
                            var header = mElement.FindElement(By.XPath(String.Format(".//*[text()='{0}']/parent::*", OptionalDate)));
                            //find items until the next header
                            var items = header.FindElements(By.XPath("./following-sibling::div[contains(@class, 'x-list-item') and not(following-sibling::div[contains(@class,'header')])]//div[contains(@class,'x-innerhtml')]" + listContainer + "[" + ColumnIndex + "]"));
                            int rowindex = 0;
                            if (int.TryParse(OptionalRowIndex, out rowindex))
                            {
                                if (rowindex < 1) throw new ArgumentException("OptionalRowIndex must be greater than zero.");
                                mSelected = items[rowindex - 1];//mElement.FindElements(By.XPath(mItemsXPath + listContainer + "[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
                                if (mSelected != null)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                foreach (IWebElement elm in items)
                                {
                                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                                    string ctlVal = DlkString.ReplaceCarriageReturn(ctl.GetValue().Trim(), "~");
                                    if (ctlVal == DlkString.ReplaceCarriageReturn(ExactText.Trim(), ""))
                                    {
                                        mSelected = elm;
                                        break;
                                    }
                                }
                            }

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

        /// <summary>
        /// Swipes the item with the specified index, display the icon.
        /// </summary>
        /// <param name="Index">Index of the list item that will be swiped</param>
        /// <param name="UpDownLeftRight">Direction of swipe</param>
        [Keyword("SwipeItemByIndex", new String[] { "1|text|Expected Value|SampleValue" })]
        public void SwipeItemByIndex(String Index, String UpDownLeftRight, String OptionalDate = "")
        {
            try
            {
                int index = -1;
                if (String.IsNullOrWhiteSpace(Index)) throw new ArgumentException("Index must not be empty.");
                if (!Int32.TryParse(Index, out index)) throw new ArgumentException("Index must be a number.");
                if (index < 1) throw new ArgumentException("Index must be a natural number."); // fail if zero or less
                if (String.IsNullOrWhiteSpace(UpDownLeftRight)) throw new ArgumentException("Direction must not be an empty string.");
                UpDownLeftRight = UpDownLeftRight.ToUpper();
                index = index - 1; // non-zero based(starts from 1).

                Initialize();
                IWebElement mSelected = null;
                if (String.IsNullOrWhiteSpace(OptionalDate))
                {
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            var items = mElement.FindElements(By.XPath(mItemsXPath)).Where(li => li.Displayed).ToList();
                            if (items.Count < 1)
                            {
                                throw new Exception(String.Format("{0} items are found", items.Count));
                            }
                            else
                            {
                                mSelected = items[index];
                                break;
                            }
                        }
                        catch (OpenQA.Selenium.NoSuchElementException)
                        {
                        }
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            var items = mElement.FindElements(By.XPath("//*[contains(@class,'list-item') and not(contains(@style,'display: none'))]")).ToList();
                            if (items.Count < 1)
                            {
                                throw new Exception(String.Format("{0} items are found", items.Count));
                            }
                            else
                            {
                                var headerListItems = GetListItemsUnderHeader(items, OptionalDate);
                                mSelected = headerListItems[index];
                                break;
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
                    SwipeDirection sdir = SwipeDirection.Right;
                    Point locStart = GetNativeViewCoordinates();
                    Point locEnd = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                    int sdistance = 50; // just default to something                    
                    switch (UpDownLeftRight.ToLower())
                    {
                        case "up":
                            sdir = SwipeDirection.Up;
                            sdistance = locEnd.Y - locStart.Y;
                            break;
                        case "down":
                            sdir = SwipeDirection.Down;
                            sdistance = locEnd.Y - locStart.Y;
                            break;
                        case "left":
                            sdir = SwipeDirection.Left;
                            sdistance = ((locEnd.X - locStart.X) / 2) - 1;
                            break;
                        case "right":
                            sdir = SwipeDirection.Right;
                            sdistance = ((locEnd.X - locStart.X) / 2) - 1;
                            break;
                        default:
                            throw new Exception("Invalid swipe direction. Please use 'Up', 'Down', 'Left', or 'Right' (not case-sensitive).");
                    }
                    mSelectedItem.Swipe(sdir, sdistance);
                }
                else
                {
                    // browser integration
                    throw new NotImplementedException("SwipeItemByIndex for non-mobile testing is not yet implemented");
                }
                DlkLogger.LogInfo("SwipeItemByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SwipeItemByIndex() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Clicks the icon that was displayed after using SwipeItemByIndex.
        /// </summary>
        [Keyword("ClickIcon")]
        public void ClickIcon()
        {
            try
            {
                Initialize();
                var icon = mElement.FindElement(By.XPath(mItemIconXpath));
                new DlkBaseControl("Icon that appeared after swipe", icon).Click(); // Uses Tap() inside since we're using mobile.
                DlkLogger.LogInfo("ClickIcon() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickIcon() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyText(String RowIndex, String ColumnIndex, String ExactText, String OptionalDate = "")
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                if (String.IsNullOrWhiteSpace(OptionalDate))
                {
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            mSelected = mElement.FindElements(By.XPath(mItemsXPath + listContainer + "[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
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
                }
                else if (!String.IsNullOrWhiteSpace(OptionalDate))
                {
                    // if optional date is supplied, only find the element belonging to that date.
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            var header = mElement.FindElement(By.XPath(String.Format(".//*[text()='{0}']/parent::*", OptionalDate)));
                            //find items until the next header
                            var items = header.FindElements(By.XPath("./following-sibling::div[contains(@class, 'x-list-item') and not(following-sibling::div[contains(@class,'header')])]//div[contains(@class,'x-innerhtml')]" + listContainer + "[" + ColumnIndex + "]"));
                            mSelected = items[int.Parse(RowIndex) - 1];//mElement.FindElements(By.XPath(mItemsXPath + listContainer + "[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
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

                DlkAssert.AssertEqual("VerifyText()", DlkString.ReplaceCarriageReturn(ExactText.Trim(), "~"), DlkString.ReplaceCarriageReturn(ctlItem.GetValue().Trim(), "~"));
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Value|SampleValue" })]
        public void AssignValueToVariable(String RowIndex, String ColumnIndex, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElements(By.XPath(mItemsXPath + listContainer + "[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
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

                DlkFunctionHandler.AssignToVariable(VariableName, DlkString.ReplaceCarriageReturn(ctlItem.GetValue().Trim(), "~"));
                DlkLogger.LogInfo("AssignToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignToVariable() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyList", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath + listContainer)).Where(x => x.Displayed) .ToList();
                string actual = "";
                string expected = "";

                // process expected
                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                foreach (IWebElement elm in mItems)
                {
                    actual += DlkString.ReplaceCarriageReturn(elm.Text.TrimEnd(), " ") + "~";
                }
                actual = actual.Trim('~');

                DlkAssert.AssertEqual("VerifyList() ", expected, actual);
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }
        #endregion

        #region METHODS

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

        /// <summary>
        /// This method returns a list of items under the OptionalDate header. Associated with T#720729.
        /// </summary>
        /// <param name="items">The list of visible elements of the entire list, including all date headers and their items.</param>
        /// <param name="OptionalDate">This parameter is the header which we will be the criteria for filtering the entire list of elements. </param>
        /// <returns></returns>
        private List<IWebElement> GetListItemsUnderHeader(List<IWebElement> items, string OptionalDate)
        {
            var actualItemsUnderSpecificDate = new List<IWebElement>();
            bool bUnderDate = false;
            foreach (var item in items)
            {
                if (item.GetAttribute("class").Contains("x-list-header"))
                {
                    // we get the text so we can check if it is equivalent to our OptionalDate parameter.
                    var elementText = item.GetAttribute("innerText").Trim();
                    // if element text == our desired date, change flag value to true meaning our desired header has been encountered.
                    if (elementText.Equals(OptionalDate))
                    {
                        bUnderDate = true;
                    }
                    // headers before the one that we want. we skip these headers if our desired date header has not been encountered yet
                    else if (!elementText.Equals(OptionalDate) && !bUnderDate)
                    {
                        continue;
                    }
                    // headers after the one that we want. we stop looping in the items upon encountering another header.
                    else
                    {
                        break;
                    }
                }
                else if (bUnderDate)
                {
                    // get all list items until the next header.
                    actualItemsUnderSpecificDate.Add(item);
                }
            }

            if (actualItemsUnderSpecificDate.Count > 0)
            {
                return actualItemsUnderSpecificDate;
            }
            else
            {
                throw new Exception("No items were found under the header " + OptionalDate);
            }
        }

        #endregion
    }
}
