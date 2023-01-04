using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace StormTouchTimeExpenseLib.DlkControls
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
        //private string mPreviousItemXPath = ".//div[contains(@class,'x-item-selected')]/preceding-sibling::div";
        //private string mNextItemXPath = ".//div[contains(@class,'x-item-selected')]/following-sibling::div";
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
                //Initialize();
                //List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                //bool bToBeSelectedFound = false;
                //bool bCurrentSelectedFound = false;
                //int iCurrentIndex = 0;
                //int iToBeSelectedIndex = 0;
                //for (int i = 0; i < mItems.Count; i++)
                //{
                //    if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML") == Value)
                //    {
                //        iToBeSelectedIndex = i;
                //        bToBeSelectedFound = true;
                //    }
                //    if (mItems[i].GetAttribute("class").Contains("x-item-selected"))
                //    {
                //        iCurrentIndex = i;
                //        bCurrentSelectedFound = true;
                //    }
                //    if (bToBeSelectedFound && bCurrentSelectedFound)
                //    {
                //        break;
                //    }
                //}
                //if (!bToBeSelectedFound)
                //{
                //    throw new Exception("Select() failed. Unable to find value '" + Value + "'");
                //}
                //int iDiff = iToBeSelectedIndex - iCurrentIndex;
                //if (iDiff < 0)
                //{
                //    for (int i = iDiff; i < 0; i++)
                //    {
                //        IWebElement previousElement = mElement.FindElement(By.XPath(mPreviousItemXPath));
                //        DlkBaseControl previousItem = new DlkBaseControl("Previous", previousElement);
                //        previousItem.Click();
                //    }
                //}
                //else if (iDiff > 0)
                //{
                //    for (int i = iDiff; i > 0; i--)
                //    {
                //        IWebElement nextElement = mElement.FindElement(By.XPath(mNextItemXPath));
                //        DlkBaseControl nextItem = new DlkBaseControl("Next", nextElement);
                //        nextItem.Click();
                //    }
                //}


                ////Refresh the selected item control
                //Initialize();
                //IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                //DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                //DlkAssert.AssertEqual("Select", Value, ctlActualSelected.GetAttributeValue("innerHTML"));
                Initialize();
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                bool bToBeSelectedFound = false;
                IWebElement mTarget = null;

                for (int i = 0; i < mItems.Count; i++)
                {
                    if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML") == Value)
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

                /* Optimal swipe distance is determined to be item height with an offset of 10px. Too 'wide' a swipe scrolls the picker control too much */
                //int swipeDistance = deviceItemHeight;
                int swipeDistance = (pickerNativeEndCoord.Y - pickerNativeStartCoord.Y) / 2;

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
                    /* re-compute swipe direction, there were cases of accidental big swipe that reaches end of list, resulting to infinite loop */
                    iSwipeDirection = ((pickerNativeEndCoord.Y - targetNativeCenterCoord.Y) / Math.Abs(pickerNativeEndCoord.Y - targetNativeCenterCoord.Y));
                }

                ///* This click will release the swipe */
                //mTargetItem.Click();

                /* this will click on target item */
                mTargetItem.Click();

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

        [Keyword("VerifyItemExists", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyItemExists(String Value)
        {
            try
            {
                Initialize();
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                bool bFound = false;
                foreach (IWebElement elm in mItems)
                {

                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    if (ctl.GetValue().Contains("x-picker"))
                    {
                        IWebElement elem = elm.FindElement(By.XPath(".//div[contains(@class,'x-picker-item')]"));
                        ctl = new DlkBaseControl("Item", elem);
                    }
                    string actualValue = ctl.GetValue().Trim();
                    if (Value == actualValue)
                    {
                        DlkLogger.LogInfo("VerifyItemExists() : List item found [" + Value + "]");
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("VerifyItemExists () : Item not found [" + Value + "]");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }
        }

    }
}
