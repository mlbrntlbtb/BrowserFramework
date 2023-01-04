using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyTouchLib.DlkControls
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

        #region CONSTANTS
        String STR_LST_ITEMS_XPATH_OLD = "./div[contains(@class, 'list-item')][not(contains(@class,'x-list-header'))]";
        String STR_LST_ITEMS_XPATH_NEW = "./div[contains(@class, 'listitem')][not(contains(@class,'x-list-header'))]";
        String STR_LST_ITEMS_XPATH_32 = ".//div[contains(@class, 'simplelistitem')][not(contains(@class,'x-list-header'))]";
        String lstPath = "";
        #endregion

        #region VARIABLES
        IList<IWebElement> listbox = null;
        IWebElement innerElm = null;
        #endregion

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String ColumnIndex, String ExactText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        /* Get all items */
                        GetAvailableListItems(mElement);
                        foreach (IWebElement elm in listbox)
                        {
                            innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']/div[" + ColumnIndex + "]"));
                            DlkBaseControl ctl = new DlkBaseControl("Item", innerElm);
                            if (innerElm.GetAttribute("class").Equals("timesheetlistDate"))
                            {
                                IWebElement elem = innerElm.FindElement(By.XPath("./div[1]"));
                                ctl = new DlkBaseControl("Item", elem);
                            }

                            string ctlVal = DlkString.ReplaceCarriageReturn(ctl.GetValue(), "");
                            if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(ExactText, ""))
                            {
                                mSelected = innerElm;
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

        [Keyword("SwipeControl", new String[] { "1|text|Value|SampleValue" })]
        public void SwipeControl(String Direction)
        {
            try
            {
                if (!DlkEnvironment.mIsMobile) throw new NotImplementedException("SwipeControl for non-mobile testing is not yet implemented");

                Initialize();                
                Direction = Direction.ToLower();
                if (! (Direction.Equals("left") || Direction.Equals("right") || Direction.Equals("up") || Direction.Equals("down")))
                {
                    throw new Exception("Parameter must be either left, right, up or down.");
                }
                //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                var list = new DlkBaseControl("entire list", mElement);
                DlkLogger.LogInfo("Scrolling into view...");
                list.ScrollIntoViewUsingJavaScript();

                DlkLogger.LogInfo("Getting coordinates...");
                Point selectedNativeCoord = list.GetNativeViewCoordinates();
                Point endNativeCoord = ConvertToNativeViewCoordinates(mElement.Size.Width + mElement.Location.X, mElement.Size.Height + mElement.Location.Y);
                Point listNativeCenterCoord = GetNativeViewCenterCoordinates();

                double distance = 0; 
                DlkLogger.LogInfo("Determining direction...");

                switch (Direction)
                {
                    case "left":
                        distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.X) - Convert.ToDouble(listNativeCenterCoord.X));
                        DlkLogger.LogInfo("Direction is LEFT");
                        Swipe(SwipeDirection.Left, Convert.ToInt32(distance));
                        break;
                    case "right":
                        distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.X) - Convert.ToDouble(listNativeCenterCoord.X));
                        DlkLogger.LogInfo("Direction is RIGHT");
                        Swipe(SwipeDirection.Right, Convert.ToInt32(distance));
                        break;
                    case "up":
                        distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y));
                        DlkLogger.LogInfo("Direction is UP");
                        Swipe(SwipeDirection.Up, Convert.ToInt32(distance));
                        break;
                    case "down":
                        distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y));
                        DlkLogger.LogInfo("Direction is DOWN");
                        Swipe(SwipeDirection.Down, Convert.ToInt32(distance));
                        break;
                    default:
                        break;
                }
                
                DlkLogger.LogInfo("Swipe distance is (" + distance.ToString() + ")");
                DlkLogger.LogInfo("Successfully swiped the list control!");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw new Exception("SwipeControl() failed. " + ex.Message);
            }
        }

        [Keyword("SwipeControlRepeatedly", new String[] { "1|text|UpDownLeftRight|Up", 
                                             "2|text|NumberOfSwipes|1", 
                                             "3|text|SwipeInterval|5"})]
        public void SwipeControlRepeatedly(String Direction, String NumberOfSwipes, String SwipeInterval)
        {
            try
            {
                if (!DlkEnvironment.mIsMobile) throw new NotImplementedException("SwipeControl for non-mobile testing is not yet implemented");
                for (int i = 0; i < int.Parse(NumberOfSwipes); i++)
                {
                    Initialize();
                    Direction = Direction.ToLower();
                    if (!(Direction.Equals("left") || Direction.Equals("right") || Direction.Equals("up") || Direction.Equals("down")))
                    {
                        throw new Exception("Parameter must be either left, right, up or down.");
                    }
                    //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                    var list = new DlkBaseControl("entire list", mElement);
                    DlkLogger.LogInfo("Scrolling into view...");
                    list.ScrollIntoViewUsingJavaScript();

                    DlkLogger.LogInfo("Getting coordinates...");
                    Point selectedNativeCoord = list.GetNativeViewCoordinates();
                    Point endNativeCoord = ConvertToNativeViewCoordinates(mElement.Size.Width + mElement.Location.X, mElement.Size.Height + mElement.Location.Y);
                    Point listNativeCenterCoord = GetNativeViewCenterCoordinates();

                    double distance = 0;
                    DlkLogger.LogInfo("Determining direction...");

                    switch (Direction)
                    {
                        case "left":
                            distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.X) - Convert.ToDouble(listNativeCenterCoord.X));
                            DlkLogger.LogInfo("Direction is LEFT");
                            Swipe(SwipeDirection.Left, Convert.ToInt32(distance));
                            break;
                        case "right":
                            distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.X) - Convert.ToDouble(listNativeCenterCoord.X));
                            DlkLogger.LogInfo("Direction is RIGHT");
                            Swipe(SwipeDirection.Right, Convert.ToInt32(distance));
                            break;
                        case "up":
                            distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y));
                            DlkLogger.LogInfo("Direction is UP");
                            Swipe(SwipeDirection.Up, Convert.ToInt32(distance));
                            break;
                        case "down":
                            distance = Math.Abs(Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y));
                            DlkLogger.LogInfo("Direction is DOWN");
                            Swipe(SwipeDirection.Down, Convert.ToInt32(distance));
                            break;
                        default:
                            break;
                    }

                    DlkLogger.LogInfo("Swipe distance is (" + distance.ToString() + ")");
                    DlkLogger.LogInfo("Successfully swiped the list control!");
                    Thread.Sleep(int.Parse(SwipeInterval) * 1000);
                }

              
            }
            catch (Exception ex)
            {
                throw new Exception("SwipeControl() failed. " + ex.Message);
            }
        }

        [Keyword("SelectPartialText", new String[] { "1|text|Value|SampleValue" })]
        public void SelectPartialText(String ColumnIndex, String PartialText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                GetAvailableListItems(mElement);

             
                        /* Get all items */

                foreach (IWebElement elm in listbox)
                {
                    innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']/div[" + ColumnIndex + "]"));
                    DlkBaseControl ctl = new DlkBaseControl("Item", innerElm);
                    string normalizedValue = DlkString.ReplaceCarriageReturn(ctl.GetValue(), "");
                    if (FormatSpace(normalizedValue).Contains(DlkString.ReplaceCarriageReturn(PartialText, "")))
                    {
                        mSelected = innerElm;
                        break;
                    }
                }
                 

                DlkBaseControl mSelectedItem = new DlkBaseControl("Selected", mSelected);
                mSelectedItem.ScrollIntoViewUsingJavaScript();

                //if (!DlkEnvironment.mIsMobile)
                //{
                //    //   mSelectedItem.ScrollIntoView();
                //    while (mSelected.Location.Y > DlkEnvironment.AutoDriver.Manage().Window.Size.Height || mSelected.Location.Y < 0)
                //    {
                //        DragToAppear(mSelected);
                //    }
                //}
                
                mSelectedItem.Click();
                DlkLogger.LogInfo("SelectPartialText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartialText() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectFullText", new String[] { "1|text|Value|SampleValue" })]
        public void SelectFullText(String FullText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        /* Get all items */
                        GetAvailableListItems(mElement);
                        foreach (IWebElement elm in listbox)
                        {
                            innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']"));
                            DlkBaseControl ctl = new DlkBaseControl("Item", innerElm);

                            if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "~").TrimEnd(new char[] {'~'}) == FullText)
                            {
                                mSelected = innerElm;
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
                DlkBaseControl mSelectedItem = new DlkBaseControl("Selected", mSelected);
                mSelectedItem.ScrollIntoViewUsingJavaScript();

                if (DlkEnvironment.mIsMobile)
                {
                    int targetY = -1;

                    while (targetY < 0 || targetY > DlkEnvironment.mDeviceWidth)
                    {
                        targetY = mSelectedItem.GetNativeViewCenterCoordinates().Y;

                        if (targetY > DlkEnvironment.mDeviceWidth)/* Swipe Up */
                        {
                            this.Swipe(SwipeDirection.Up, (DlkEnvironment.mDeviceWidth / 2) - 1);
                        }
                        else if (targetY < 0) /* Swipe Down */
                        {
                            this.Swipe(SwipeDirection.Down, (DlkEnvironment.mDeviceWidth / 2) - 1);
                        }
                        else
                        {
                            break;
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
                DlkLogger.LogInfo("SelectFullText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectFullText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowCount(String VariableName)
        {
            try
            {
                Initialize();
                /* Get all items */
                int listCount = 0;
                GetAvailableListItems(mElement);
                if (listbox != null)
                {
                    listCount = listbox.Count;
                }
                DlkFunctionHandler.AssignToVariable(VariableName, listCount.ToString());
                DlkLogger.LogInfo("GetRowCount() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithText", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowWithText(String ColumnIndex, String PartialText, String VariableName)
        {
            try
            {
                Initialize();
                bool bFound = false;
                IWebElement mSelected = null;
              
                        /* Get all items */
                GetAvailableListItems(mElement);
                foreach (IWebElement elm in listbox)
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    if (DlkString.ReplaceCarriageReturn(FormatSpace(ctl.GetValue()), "").ToLower().Contains(DlkString.ReplaceCarriageReturn(PartialText, "").ToLower()))
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        mSelected = elm;
                        DlkVariable.SetVariable(VariableName, ctl.ToString());
                        bFound = true;
                        break;
                    }
                }

                if (mSelected != null)
                {
                    int index = 0;
                    for (index = 0; index < listbox.Count; index++)
                    {
                        DlkBaseControl ctlItem = new DlkBaseControl("Item", listbox[index]);
                        if (DlkString.ReplaceCarriageReturn(FormatSpace(ctlItem.GetValue()), "").ToLower().Contains(DlkString.ReplaceCarriageReturn(PartialText, "").ToLower()))
                        {
                            DlkVariable.SetVariable(VariableName, (index + 1).ToString());
                            bFound = true;
                            break;
                        }
                    }

                }
                
                if (!bFound)
                {
                    throw new Exception("Unable to find list item with value '" + PartialText + "'");
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLastRowWithText", new String[] { "1|text|Value|SampleValue" })]
        public void GetLastRowWithText(String ColumnIndex, String PartialText, String VariableName)
        {
            try
            {
                //guard clause
                int colIndex = 0;
                if (!int.TryParse(ColumnIndex, out colIndex))
                {
                    throw new Exception("ColumnIndex '" + ColumnIndex + "' is invalid");
                }

                Initialize();

                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        /* Get all items */
                        GetAvailableListItems(mElement);
                        for (int idx = listbox.Count - 1; idx >= 0; idx--)
                        {
                            var ctl = new DlkBaseControl("item", listbox[idx]);
                            if (DlkString.ReplaceCarriageReturn(FormatSpace(ctl.GetValue()), "").Contains(DlkString.ReplaceCarriageReturn(PartialText, "")))
                            {
                                var targetIndex = (idx + 1).ToString();
                                DlkVariable.SetVariable(VariableName, targetIndex);
                                DlkLogger.LogInfo("Variable '" + VariableName + "' value set to '" + targetIndex + "'");
                                return;
                            }
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        /* do nothing */
                    }
                    Thread.Sleep(1000);
                }
                throw new Exception("Unable to find list item with value '" + PartialText + "'"); // not found
            }
            catch (Exception e)
            {
                throw new Exception("GetLastRowWithText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetFullText", new String[] { "1|text|Value|SampleValue" })]
        public void GetFullText(String RowIndex, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                String ActValue ="";
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = mElement.FindElements(By.XPath(lstPath + "/descendant::div[contains(@class,'innerhtml')]")).ElementAt(int.Parse(RowIndex) - 1);
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

                ActValue = ctlItem.GetValue();
                DlkVariable.SetVariable(VariableName, DlkString.RemoveCarriageReturn(ActValue));
                DlkLogger.LogInfo("GetFullText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetFullText() failed : " + e.Message, e);
            }
        }

        [Keyword("LongPress", new String[] { "1|text|Value|SampleValue" })]
        public void LongPress(String ColumnIndex, String ExactText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        /* Get all items */
                        GetAvailableListItems(mElement);
                        foreach (IWebElement elm in listbox)
                        {
                            innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']/div[" + ColumnIndex + "]"));
                            DlkBaseControl ctl = new DlkBaseControl("Item", innerElm);
                            if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(ExactText, ""))
                            {
                                mSelected = innerElm;
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

                mSelectedItem.ClickAndHold();
                DlkLogger.LogInfo("LongPress() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("LongPress() failed : " + e.Message, e);
            }
        }

        [Keyword("LongPressTextContains", new String[] { "1|text|Value|SampleValue" })]
        public void LongPressTextContains(String ColumnIndex, String PartialText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        /* Get all items */
                        GetAvailableListItems(mElement);
                        foreach (IWebElement elm in listbox)
                        {
                            innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']/div[" + ColumnIndex + "]"));
                            DlkBaseControl ctl = new DlkBaseControl("Item", innerElm);
                            if (DlkString.ReplaceCarriageReturn(FormatSpace(ctl.GetValue()), "").Contains(DlkString.ReplaceCarriageReturn(PartialText, "")))
                            {
                                mSelected = innerElm;
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

                mSelectedItem.ClickAndHold();
                DlkLogger.LogInfo("LongPressTextContains() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("LongPressTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("LongPressByRow", new String[] { "1|text|Value|SampleValue" })]
        public void LongPressByRow(String RowIndex)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = mElement.FindElements(By.XPath(lstPath + "//div[@class='x-innerhtml']")).ElementAt(int.Parse(RowIndex) - 1);
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

                ctlItem.ClickAndHold();
                DlkLogger.LogInfo("LongPressByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("LongPressByRow() failed : " + e.Message, e);
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
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = listbox.ElementAt(int.Parse(Row) - 1);
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
                    //workaround for android only. scrollintoviewusingjs works for ios.
                    if (DlkEnvironment.mBrowser.ToLower() == "android")
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

        [Keyword("CheckByRow", new String[] { "1|text|Row|1" })]
        public void CheckByRow(String Row)
        {
            try
            {
                Initialize();
                IWebElement mCheck = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mCheck = mElement.FindElements(By.XPath("//div[contains(@class,'Checkmark')]")).ElementAt(int.Parse(Row) - 1);
                        if (mCheck != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkBaseControl ctlItem = new DlkBaseControl("Item", mCheck);
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
                    while (mCheck.Location.Y > DlkEnvironment.AutoDriver.Manage().Window.Size.Height || mCheck.Location.Y < 0)
                    {
                        DragToAppear(mCheck);
                    }
                }

                ctlItem.Click();
                DlkLogger.LogInfo("CheckByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("CheckByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("CheckAllRows", new String[] { "1|text" })]
        public void CheckAllRows(String TrueOrFalse)
        {
            try
            {
                bool toCheck = Convert.ToBoolean(TrueOrFalse);

                Initialize();

                IReadOnlyCollection<IWebElement> listItems = mElement.FindElements(By.XPath(".//div[contains(@class,'Checkmark')]"));

                foreach (IWebElement listItem in mElement.FindElements(By.XPath(".//div[contains(@class,'Checkmark')]")))
                {
                    DlkBaseControl ctlItem = new DlkBaseControl("Item", listItem);
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
                        while (listItem.Location.Y > DlkEnvironment.AutoDriver.Manage().Window.Size.Height || listItem.Location.Y < 0)
                        {
                            DragToAppear(listItem);
                        }
                    }

                    switch (listItem.GetAttribute("class"))
                    {
                        case "jobSelectedCheckmark": if (!toCheck) { ctlItem.Click(); } break;
                        case "jobUnselectedCheckmark": if (toCheck) { ctlItem.Click(); } break;
                        default: throw new Exception("Check mark class unknown. Kindly report to automation developer for investigation.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("CheckAllRows() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowChecked", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRowChecked(String Row, String TrueOrFalse)
        {
            try
            {
                bool ActualValue = false, ExpectedValue = Convert.ToBoolean(TrueOrFalse);

                Initialize();

                IWebElement listItem = mElement.FindElements(By.XPath(".//div[contains(@class,'Checkmark')]")).ElementAt(int.Parse(Row) - 1);

                switch (listItem.GetAttribute("class"))
                {
                    case "jobSelectedCheckmark": ActualValue = true; break;
                    case "jobUnselectedCheckmark": ActualValue = false; break;
                    default: throw new Exception("Check mark class unknown. Kindly report to automation developer for investigation.");
                }

                DlkAssert.AssertEqual("VerifyRowChecked", ExpectedValue, ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowChecked() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChevronExists", new String[] { "1|text|Row|1" })]
        public void VerifyChevronExists(String Row, String ExpectedValue)
        {
            try
            {
                bool expected = false;
                int index = 0;
                bool.TryParse(ExpectedValue,out expected);
                int.TryParse(Row, out index);
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = mElement.FindElement(By.XPath(string.Format(lstPath + "//div[contains(@class,'innerhtml')][{0}]/preceding-sibling::div", index)));
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
                var existence = ctlItem.Exists();
                if (!existence)
                {
                    throw new Exception("There is no Chevron in Row " + Row);                    
                }
                else
                {
                    DlkAssert.AssertEqual("ExistenceCheck", expected, existence);
                    DlkLogger.LogInfo("VerifyChevronExists() successfully executed.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChevronExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                bool expected;
                if (!Boolean.TryParse(TrueOrFalse, out expected))
                {
                    throw new Exception("Invalid value for TrueOrFalse");
                }

                Initialize();
                bool actual = Exists();

                //ADDED for double checking
                string mStyle = mElement.GetAttribute("style");
                if (mStyle != null && mStyle.Contains("display: none"))
                {
                    actual = false;
                }

                DlkAssert.AssertEqual("VerifyExists", expected, actual);
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyList(String ColumnIndex, String ListItems)
        {
            try
            {
                Initialize();
                List<string> actualList = new List<string>();

                /* Get all items */
                GetAvailableListItems(mElement);
                foreach (IWebElement elm in listbox)
                {
                    innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']/div[" + ColumnIndex + "]"));
                    actualList.Add(new DlkBaseControl("element", innerElm).GetValue());
                }

                DlkAssert.AssertEqual("VerifyList()", ListItems.Split('~'), actualList.ToArray());
                DlkLogger.LogInfo("VerifyList() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialTextInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyPartialTextInList(String ColumnIndex, String PartialText, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bFound = false;

                /* Get all items */
                GetAvailableListItems(mElement);
                foreach (IWebElement elm in listbox)
                {
                    innerElm = elm.FindElement(By.XPath(".//div[@class='x-innerhtml']/div[" + ColumnIndex + "]"));
                    if (DlkString.ReplaceCarriageReturn(FormatSpace(new DlkBaseControl("element", innerElm).GetValue().ToLower()), "").Contains(DlkString.ReplaceCarriageReturn(PartialText, "").ToLower()))
                    {
                        bFound = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyPartialTextInList()", Convert.ToBoolean(TrueOrFalse), bFound);
                DlkLogger.LogInfo("VerifyPartialTextInList() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialTextInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyTextInList(String ColumnIndex, String ExactText, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bFound = false;

                /* Get all items */
                GetAvailableListItems(mElement);
                foreach (IWebElement elm in listbox)
                {
                    innerElm = elm.FindElement(By.XPath("./descendant::div[not(div)][" + ColumnIndex + "]"));
                    IWebElement textContainer = innerElm;
                    if (DlkString.ReplaceCarriageReturn(new DlkBaseControl("element", textContainer).GetValue(), "").ToLower() == DlkString.ReplaceCarriageReturn(ExactText, "").ToLower())
                    {
                        bFound = true;
                        break;
                    }

                }
                DlkAssert.AssertEqual("VerifyTextInList()", Convert.ToBoolean(TrueOrFalse), bFound);
                DlkLogger.LogInfo("VerifyTextInList() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyText(String RowIndex, String ColumnIndex, String ExactText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = mElement.FindElements(By.XPath(lstPath + "/descendant::div[@class='x-innerhtml']/div[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
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

                DlkAssert.AssertEqual("VerifyText()", DlkString.ReplaceCarriageReturn(ExactText, "").ToLower(), DlkString.ReplaceCarriageReturn(ctlItem.GetValue(), "").ToLower());
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExactText(String RowIndex, String ColumnIndex, String ExactText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = mElement.FindElements(By.XPath(lstPath + "/descendant::div[@class='x-innerhtml']/div[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
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

                DlkAssert.AssertEqual("VerifyText()", DlkString.ReplaceCarriageReturn(ExactText, ""), DlkString.ReplaceCarriageReturn(ctlItem.GetValue(), ""));
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyPartialText(String RowIndex, String ColumnIndex, String PartialText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        lstPath = GetAvailableListItems(mElement);
                        mSelected = mElement.FindElements(By.XPath(lstPath + "/descendant::div[@class='x-innerhtml']/div[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
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
                DlkAssert.AssertEqual("VerifyPartialText()", true, Convert.ToBoolean(DlkString.ReplaceCarriageReturn(FormatSpace(ctlItem.GetValue()), "").Contains(DlkString.ReplaceCarriageReturn(PartialText, ""))));
                DlkLogger.LogInfo("VerifyPartialText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Row|1" })]
        public void VerifyRowCount(String ExpectedCount)
        {
            try
            {
                Initialize();
                /* Get all items */
                int listCount = 0;
                GetAvailableListItems(mElement);
                if (listbox != null)
                {
                    listCount = listbox.Count;
                }

                DlkAssert.AssertEqual("VerifyRowCount", Convert.ToInt32(ExpectedCount), listCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if all list items are checked
        /// </summary>
        /// <param name="TrueOrFalse">True if all should be checked, false if at least one is not checked</param>
        [Keyword("VerifyListItemsAreChecked", new String[] { "1|text|Row|1" })]
        public void VerifyListItemsAreChecked(String TrueOrFalse)
        {
            try
            {
                // default actual to true then set to false once an unchecked item is encountered
                bool expected = Boolean.Parse(TrueOrFalse);
                bool actual = true;
                Initialize();
                /* Get all items */
                GetAvailableListItems(mElement);
                foreach (var element in listbox)
                {
                    if (element.FindElements(By.XPath(".//div[contains(@class,'Unselected')]")).Count > 0)
                    {
                        actual = false;
                    }
                }
                DlkAssert.AssertEqual("VerifyListItemsAreChecked", expected, actual);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItemsAreChecked() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Swipe a list item given its value
        /// </summary>
        /// <param name="Value">Value of item to swipe</param>
        /// <param name="UpDownLeftRight">Direction of swipe</param>
        [Keyword("SwipeItem", new String[] { "1|text|Value|SampleValue", 
                                             "2|text|UpDownLeftRight|Up"})]
        public void SwipeItem(String Value, String UpDownLeftRight)
        {
            try
            {
                Initialize();

                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElement(By.XPath(GetAvailableListItems(mElement) + "[contains(., '" + Value + "')]"));
                        
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
                            throw new Exception("Invalid swipe direction. Please use 'Up', 'Down', 'Left', or 'Right'.");
                    }
                    mSelectedItem.Swipe(sdir, sdistance);
                }
                else
                {
                    // browser integration
                    throw new NotImplementedException("SwipeItem for non-mobile testing is not yet implemented");
                }
                DlkLogger.LogInfo("SwipeItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SwipeItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SwipeItemByRow", new String[] { "1|text|Row|1", 
                                                  "2|text|UpDownLeftRight|Up"})]
        public void SwipeItemByRow(String Row, String UpDownLeftRight)
        {
            try
            {
                Initialize();

                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElements(By.XPath(GetAvailableListItems(mElement))).ElementAt(int.Parse(Row) - 1);
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
                            throw new Exception("Invalid swipe direction. Please use 'Up', 'Down', 'Left', or 'Right'.");
                    }
                    mSelectedItem.Swipe(sdir, sdistance);
                }
                else
                {
                    // browser integration
                    throw new NotImplementedException("SwipeItem for non-mobile testing is not yet implemented");
                }
                DlkLogger.LogInfo("SwipeItemByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SwipeItemByRow() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Assign the value of the text on the left side of the row to a variable
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="VariableName"></param>
        [Keyword("AssignPendingItemsToVariable", new String[] { "1|text|Row|1", 
                                                  "2|text|VariableName|MyVariable"})]
        public void AssignPendingItemsToVariable(String RowIndex, String VariableName)
        {
            try
            {
                int mRow = int.Parse(RowIndex) - 1;
                if (mRow < 0) throw new Exception("RowIndex must be greater than 0.");
                
                Initialize();

                string mValue = string.Empty;
                IWebElement mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]")).ElementAt(mRow);
                DlkBaseControl mNumber = new DlkBaseControl("PendingItems", mSelected.FindElement(By.XPath(".//span[contains(@style, 'border-color')]")));
                if (mNumber.Exists(1))
                {
                    mValue = mNumber.GetValue();
                }
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignPendingItemsToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignPendingItemsToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Assign list value/s to variable
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColumnIndex">0 or NULL = all rows; 1,2,3... = specified line/row</param>
        /// <param name="VariableName"></param>
        [Keyword("AssignListItemsToVariable", new String[] { "1|text|Row|1", 
                                                  "2|text|VariableName|MyVariable"})]
        public void AssignListItemsToVariable(String RowIndex, String ColumnIndex, String VariableName)
        {
            try
            {
                int mRow = int.Parse(RowIndex) - 1;
                if (mRow < 0) throw new Exception("RowIndex must be greater than 0.");
                int mCol = int.Parse(ColumnIndex);
                if (mCol < 0) throw new Exception("ColumnIndex must be non negative.");

                Initialize();

                IWebElement mSelected;
                if (mCol == 0 || String.IsNullOrEmpty(ColumnIndex))
                {
                    mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'listitem')]/descendant::div[@class='x-innerhtml']/div[contains(@style, 'left')]")).ElementAt(int.Parse(RowIndex) - 1);
                }
                else
                {
                    mSelected = mElement.FindElements(By.XPath(".//div[contains(@class, 'listitem')]/descendant::div[@class='x-innerhtml']/div[contains(@style, 'left')]/*[" + ColumnIndex + "]")).ElementAt(int.Parse(RowIndex) - 1);
                }

                string mValue = string.Empty;
                mValue = DlkString.ReplaceCarriageReturn(new DlkBaseControl("Item", mSelected).GetValue(), "~");

                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignListItemsToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignListItemsToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verify the text on the given row, column and line
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="ColumnIndex"></param>
        /// <param name="LineIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyLineText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyLineText(String RowIndex, String ColumnIndex, String LineIndex, String ExpectedValue)
        {
            try
            {
                int mRowIdx, mColumnIdx, mLineIdx;
                string ActualValue;

                if (!Int32.TryParse(RowIndex, out mRowIdx)) throw new Exception("RowIndex must be a valid positive integer.");
                if (!Int32.TryParse(ColumnIndex, out mColumnIdx)) throw new Exception("ColumnIndex must be a valid positive integer.");
                if (!Int32.TryParse(LineIndex, out mLineIdx)) throw new Exception("LineIndex must be a valid positive integer.");

                Initialize();
                lstPath = GetAvailableListItems(mElement);
                string rowColumnXPath = lstPath + "[" + RowIndex + "]//div[contains(@class,'innerhtml')]/div[" + ColumnIndex + "]";
                string lineXPath = string.Empty;
                IWebElement mLine;

                //Some Lists don't have Column
                if(mElement.FindElements(By.XPath(rowColumnXPath)).Count <= 0)
                {
                    lineXPath = lstPath + "[" + RowIndex + "]//div[contains(@class,'x-innerhtml')]/*[" + LineIndex + "]";
                }
                else
                {
                    //Check if Row-Column has multiple lines
                    //This is usually the case with those texts that are on the right side of the screen
                    IWebElement rowColumn = mElement.FindElement(By.XPath(rowColumnXPath));
                    if (rowColumn.FindElements(By.XPath("./*[" + LineIndex + "]")).Count < 1)
                    {
                        lineXPath = rowColumnXPath;
                    }
                    else
                    {
                        lineXPath = rowColumnXPath + "/*[" + LineIndex + "]";
                    }
                }

                mLine = mElement.FindElement(By.XPath(lineXPath));

                ActualValue = DlkString.RemoveCarriageReturn(new DlkBaseControl("Line", mLine).GetValue()).Trim();
                DlkAssert.AssertEqual("VerifyLineText", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyLineText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Assigns value to variable based on given Row (Top to Bottom), Column (Left to Right) and Line
        /// </summary>
        /// <param name="RowIndex">Desired row on the list</param>
        /// <param name="ColumnIndex">Desired column on the list; 1,2,3 = item from left to right</param>
        /// <param name="LineIndex">Desired specific line based on the given Row and Column;</param>
        /// <param name="VariableName"></param>
        [Keyword("AssignLineValueToVariable", new String[] { "1|text|RowIndex|1",
                                                             "2|text|ColumnIndex|1",
                                                             "3|text|LineIndex|1",
                                                             "4|text|VariableName|MyLine"})]
        public void AssignLineValueToVariable(String RowIndex, String ColumnIndex, String LineIndex, String VariableName)
        {
            try
            {
                int mRowIdx, mColumnIdx, mLineIdx;
                string lineValue;

                if (!Int32.TryParse(RowIndex, out mRowIdx)) throw new Exception("RowIndex must be a valid positive integer.");
                if (!Int32.TryParse(ColumnIndex, out mColumnIdx)) throw new Exception("ColumnIndex must be a valid positive integer.");
                if (!Int32.TryParse(LineIndex, out mLineIdx)) throw new Exception("LineIndex must be a valid positive integer.");

                Initialize();
                lstPath = GetAvailableListItems(mElement);
                string rowColumnXPath = lstPath + "[" + RowIndex + "]//div[contains(@class,'x-innerhtml')]/div[" + ColumnIndex + "]";
                string lineXPath = string.Empty;
                IWebElement mLine;

                //Some Lists don't have Column
                if (mElement.FindElements(By.XPath(rowColumnXPath)).Count < 1)
                {
                    lineXPath = lstPath + "[" + RowIndex + "]//div[contains(@class,'x-innerhtml')]/*[" + LineIndex + "]";
                }
                else
                {
                    //Check if Row-Column has multiple lines
                    //This is usually the case with those texts that are on the right side of the screen
                    IWebElement rowColumn = mElement.FindElement(By.XPath(rowColumnXPath));
                    if (rowColumn.FindElements(By.XPath("./*[" + LineIndex + "]")).Count < 1)
                    {
                        lineXPath = rowColumnXPath;
                    }
                    else
                    {
                        lineXPath = rowColumnXPath + "/*[" + LineIndex + "]";
                    }
                }

                mLine = mElement.FindElement(By.XPath(lineXPath));

                lineValue = DlkString.RemoveCarriageReturn(new DlkBaseControl("Line", mLine).GetValue()).Trim();

                DlkVariable.SetVariable(VariableName, lineValue);
                DlkLogger.LogInfo("AssignLineValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + lineValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignLineValueToVariable() failed : " + e.Message, e);
            }
        }
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
        
        public String FormatSpace(String text)
        {
            var textToReturn = string.Empty;
            foreach (char c in text)
            {
                if (Convert.ToByte(c) == 63 || Convert.ToByte(c) == 160)
                    textToReturn += ' ';
                else
                    textToReturn += c;
            }
            return textToReturn;
        }
        
        public String GetAvailableListItems(IWebElement mElms)
        {
            DlkBaseControl mList = new DlkBaseControl("List", mElms);
            int listSize = -1;

            listSize = mList.GetNativeViewCenterCoordinates().Y;

            if (mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_OLD)).Count > 0)
            {
                if(listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_OLD)).ToList();
                }
                else
                {
                    listbox = mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_OLD)).Where(x => x.Displayed).ToList();
                }
                return STR_LST_ITEMS_XPATH_OLD;
            }
            else if (mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_NEW)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_NEW)).ToList();
                }
                else
                {
                    listbox = mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_NEW)).Where(x => x.Displayed).ToList();
                }
                return STR_LST_ITEMS_XPATH_NEW;
            }
            else if (mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_32)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_32)).ToList();
                }
                else
                {
                    listbox = mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_32)).Where(x => x.Displayed).ToList();
                }
                return STR_LST_ITEMS_XPATH_32;
            }
            else
            {
                throw new Exception("List type not yet supported.");
            }
        }

        #endregion


    }
}
