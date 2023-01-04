using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Drawing;

namespace StormTouchCRMLib.DlkControls
{
    [ControlType("Picker")]
    public class DlkPicker : DlkBaseControl
    {
        public DlkPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private string mItemsXPath = ".//div[contains(@class, 'x-dataview-item')]";
        private string mSelectedItemXPath = ".//div[contains(@class,'x-item-selected')]/div[contains(@class,'x-picker-item')]";
        private string mPreviousItemXPath = ".//div[contains(@class,'x-item-selected')]/preceding-sibling::div[1]";
        private string mNextItemXPath = ".//div[contains(@class,'x-item-selected')]/following-sibling::div";
        //private List<IWebElement> mItems;

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

                if (DlkEnvironment.mIsMobile)
                {
                    List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                    bool bToBeSelectedFound = false;
                    IWebElement mTarget = null;

                    for (int i = 0; i < mItems.Count; i++)
                    {
                        if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML").Trim() == Value)
                        {
                            mTarget = mItems[i];
                            bToBeSelectedFound = true;
                            break;
                        }
                    }
                    if (!bToBeSelectedFound)
                    {
                        throw new Exception("Unable to find value '" + Value + "'.");
                    }

                    /* Determine coordinates of target item */
                    DlkBaseControl mTargetItem = new DlkBaseControl("Selected", mTarget);
                    Point targetNativeStartCoord = mTargetItem.GetNativeViewCoordinates();
                    Point targetNativeCenterCoord = mTargetItem.GetNativeViewCenterCoordinates();
                    Point targetNativeEndCoord = ConvertToNativeViewCoordinates(mTarget.Location.X + mTarget.Size.Width,
                        mTarget.Location.Y + mTarget.Size.Height);

                    /* Determine start and end coordinates of picker control, this will be basis for bounds of swipe */
                    Point pickerNativeStartCoord = GetNativeViewCoordinates();
                    Point pickerNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);

                    /* Get swipe direction: negative int is up swipe, positive is down swipe */
                    int iSwipeDirection = ((pickerNativeEndCoord.Y - targetNativeCenterCoord.Y) / Math.Abs(pickerNativeEndCoord.Y - targetNativeCenterCoord.Y));

                    /* Get height approximation in pixel of a picker item */
                    int deviceItemHeight = targetNativeEndCoord.Y - targetNativeStartCoord.Y;

                    /* Optimal swipe distance is determined to be item height with an offset of 30px. Too 'wide' a swipe scrolls the picker control too much */
                    /* 
                     * Swipe only 3/4 of swipe distance by subtracting 1/3 of the swipe distance. This would guarantee that we dont over-scroll on the item.
                     * This way we swipe only 2 items at a time, if ever it will over-swipe to 3 items, it would be fine unlike when like swiping 3 items,
                     * Where we can over-swipe to 4 items which is not ok due to skipping of 1 item.
                    */
                    int swipeDistance = ((pickerNativeEndCoord.Y - pickerNativeStartCoord.Y) / 2) - (((pickerNativeEndCoord.Y - pickerNativeStartCoord.Y) / 2) / 4); ; // subtract 1/3 of total swipe distance.

                    /* Continue swiping until: target item Y coordinate is inside the Y bounds of the picker control */
                    while (targetNativeCenterCoord.Y < pickerNativeStartCoord.Y || targetNativeCenterCoord.Y > pickerNativeEndCoord.Y)
                    {
                        /* Default to up swipe */
                        SwipeDirection swipeDirection = SwipeDirection.Up;

                        if (iSwipeDirection > 0) // down swipe
                        {
                            swipeDirection = SwipeDirection.Down;
                            //swipeOrigin = SwipeOrigin.Center;
                        }

                        Swipe(swipeDirection, swipeDistance);
                        DlkEnvironment.SetContext("WEBVIEW");
                        targetNativeCenterCoord = mTargetItem.GetNativeViewCenterCoordinates();
                    }

                    mTargetItem.Tap();
                }
                else
                {
                    DlkLogger.LogInfo("Picker control accessed in browser. This control is touch action specific and requires swipe. Framework will replace swipe action with series of click actions.");
                    Thread.Sleep(1000);
                    List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                    bool bToBeSelectedFound = false;
                    bool bCurrentSelectedFound = false;
                    int iCurrentIndex = 0;
                    int iToBeSelectedIndex = 0;
                    for (int i = 0; i < mItems.Count; i++)
                    {
                        if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML") == Value)
                        {
                            iToBeSelectedIndex = i;
                            bToBeSelectedFound = true;
                        }
                        if (mItems[i].GetAttribute("class").Contains("x-item-selected"))
                        {
                            iCurrentIndex = i;
                            bCurrentSelectedFound = true;
                        }
                        if (bToBeSelectedFound && bCurrentSelectedFound)
                        {
                            break;
                        }
                    }
                    if (!bToBeSelectedFound)
                    {
                        throw new Exception("Select() failed. Unable to find value '" + Value + "'");
                    }
                    int iDiff = iToBeSelectedIndex - iCurrentIndex;
                    if (iDiff < 0)
                    {
                        for (int i = iDiff; i < 0; i++)
                        {
                            IWebElement previousElement = mElement.FindElement(By.XPath(mPreviousItemXPath));
                            DlkBaseControl previousItem = new DlkBaseControl("Previous", previousElement);
                            previousItem.Click();
                            Thread.Sleep(1000);
                        }
                    }
                    else if (iDiff > 0)
                    {
                        for (int i = iDiff; i > 0; i--)
                        {
                            IWebElement nextElement = mElement.FindElement(By.XPath(mNextItemXPath));
                            DlkBaseControl nextItem = new DlkBaseControl("Next", nextElement);
                            nextItem.Click();
                            Thread.Sleep(1000);
                        }
                    }


                    //Refresh the selected item control
                    Initialize();
                    IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                    DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                    ctlActualSelected.Click();
                }
                DlkLogger.LogInfo("Select() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ctlActualSelected.GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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

        [Keyword("VerifyItemExists", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyItemExists(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                Thread.Sleep(1000);
                bool bFound = false;
                bFound = mElement.FindElements(By.XPath(mItemsXPath)).ToList().Exists( x => x.Text == Item);
                DlkAssert.AssertEqual("VerifyItemExists: " + mControlName, Convert.ToBoolean(TrueOrFalse), bFound);
                DlkLogger.LogInfo("VerifyItemExists() passed");
               
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }

        }

    }
}
