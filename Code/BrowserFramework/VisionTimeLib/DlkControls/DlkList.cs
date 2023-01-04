using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace VisionTimeLib.DlkControls
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
        //TODO: Not all list-types are working.
        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();

                IWebElement mSelected = null;
                bool bFound = false;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElement(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml'][contains(., '" + Value + "')]"));
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
                if (!bFound)
                {
                    for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                    {
                        try
                        {
                            foreach (IWebElement elem in mElement.FindElements(By.XPath(".//div[@class='x-innerhtml']")))
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

                //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                Point selectedNativeCoord = mSelectedItem.GetNativeViewCoordinates();
                Point listNativeStartCoord = GetNativeViewCoordinates();
                Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                if (selectedNativeCoord.Y < listNativeStartCoord.Y // target item is out of list upper bounds 
                    || selectedNativeCoord.Y > listNativeEndCoord.Y  // target item is out of list lower bounds
                    || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight // target item is out of device lower bounds
                    || selectedNativeCoord.Y < 0) // target item is out of device upper bounds
                {
                    Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                    double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                    if (dYTranslation < 0)
                    {
                        Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    else
                    {
                        Swipe(SwipeDirection.Up, Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    // put some delay before executing click right after swipe
                    Thread.Sleep(1500);

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

                //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                Point selectedNativeCoord = ctlItem.GetNativeViewCoordinates();
                Point listNativeStartCoord = GetNativeViewCoordinates();
                Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                if (selectedNativeCoord.Y < listNativeStartCoord.Y // target item is out of list upper bounds 
                    || selectedNativeCoord.Y > listNativeEndCoord.Y  // target item is out of list lower bounds
                    || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight // target item is out of device lower bounds
                    || selectedNativeCoord.Y < 0) // target item is out of device upper bounds
                {
                    Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                    double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                    if (dYTranslation < 0)
                    {
                        Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    else
                    {
                        Swipe(SwipeDirection.Up, Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    // put some delay before executing click right after swipe
                    Thread.Sleep(1500);

                }

                ctlItem.Click();
                DlkLogger.LogInfo("SelectByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByRow() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets the toggle state to the desired state.
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="State"></param>
        [Keyword("SetToggleStateByRow", new String[] { "1|text|Row|1" })]
        public void SetToggleStateByRow(String Row, String State)
        {
            try
            {
                Initialize();
                IWebElement mListItem = null;
                int zeroBasedRowIndex = 0;
                int.TryParse(Row, out zeroBasedRowIndex);
                string itemsXpath = string.Format(".//div[contains(@class,'nozebralist')]//div[contains(@class,'list-item-relative') and contains(@id,'simplelistitem')][{0}]", zeroBasedRowIndex);
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mListItem = mElement.FindElement(By.XPath(itemsXpath));
                        if (mListItem != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        throw;
                    }
                    Thread.Sleep(1000);
                }
                var mCheckBox = mListItem.FindElement(By.XPath(".//div[contains(@class,'empCheckBox')]"));
                DlkBaseControl ctlItem = new DlkBaseControl("Item", mCheckBox);
                ctlItem.ScrollIntoViewUsingJavaScript();
              
                //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
                Point selectedNativeCoord = ctlItem.GetNativeViewCoordinates();
                Point listNativeStartCoord = GetNativeViewCoordinates();
                Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
                if (selectedNativeCoord.Y < listNativeStartCoord.Y // target item is out of list upper bounds 
                    || selectedNativeCoord.Y > listNativeEndCoord.Y  // target item is out of list lower bounds
                    || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight // target item is out of device lower bounds
                    || selectedNativeCoord.Y < 0) // target item is out of device upper bounds
                {
                    Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                    double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                    if (dYTranslation < 0)
                    {
                        Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    else
                    {
                        Swipe(SwipeDirection.Up, Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    // put some delay before executing click right after swipe
                    Thread.Sleep(1500);

                }

                if (mCheckBox.GetAttribute("class").Contains("unchecked") && State.ToLower().Equals("checked"))
                {
                    DlkLogger.LogInfo("The selected item is 'unchecked' ... clicking to set the state to 'checked'..");
                    ctlItem.Tap();
                }
                else if (!mCheckBox.GetAttribute("class").Contains("unchecked") && State.ToLower().Equals("unchecked"))
                {
                    DlkLogger.LogInfo("The selected item is 'checked' ... clicking to set the state to 'unchecked'..");
                    ctlItem.Tap();
                }

                DlkLogger.LogInfo("SetToggleStateByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetToggleStateByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickRowButton", new String[] { "1|text|Row|1" })]
        public void ClickRowButton(String Row, String ButtonToClick)
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
                            mButton = mSelected.FindElement(By.XPath("./descendant::*[contains(translate(@class, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '" 
                                +  ButtonToClick.ToLower() + "')]"));
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
                        Swipe("down", Convert.ToInt32(Math.Abs(dYTranslation)));
                    }
                    else
                    {
                        Swipe("up", Convert.ToInt32(dYTranslation));
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
                        mSelected = mElement.FindElement(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml'][contains(., '" + SearchedText + "')]"));
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
                if (mSelected != null)
                {
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

        [Keyword("VerifyPartialTextInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyPartialTextInList(String PartialText, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bFound = false;

                /* Get all items */
                foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']/div")))
                {
                    if (new DlkBaseControl("element", elm).GetValue().Contains(PartialText))
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
        public void VerifyTextInList(String ExactText, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bFound = false;

                /* Get all items */
                foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']")))
                {
                    if (DlkString.ReplaceCarriageReturn(new DlkBaseControl("element", elm).GetValue(), "") == DlkString.ReplaceCarriageReturn(ExactText, ""))
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

        [Keyword("VerifyRowCount", new String[] { "1|text|Row|1" })]
        public void VerifyRowCount(String ExpectedCount)
        {
            try
            {
                int expectedCount = 0;
                //guard clause
                int.TryParse(ExpectedCount, out expectedCount);
                if (expectedCount < 0) throw new ArgumentException("Expected count must not be less than zero");
                Initialize();
                /* Get all items */
                int listCount = 0;
                var itemList = mElement.FindElements(By.XPath(".//div[contains(@class, 'x-list-item') and descendant::div[contains(@class,'ApprovalItem')]]"));
                if (itemList != null)
                {
                    foreach (var item in itemList)
                    {
                        if (item.Displayed) listCount++;
                    }
                }

                DlkAssert.AssertEqual("VerifyRowCount", expectedCount, listCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }


    }
}
