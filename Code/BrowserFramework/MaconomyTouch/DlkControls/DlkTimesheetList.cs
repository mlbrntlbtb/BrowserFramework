using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("TimesheetList")]
    public class DlkTimesheetList : DlkBaseControl
    {
        public DlkTimesheetList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTimesheetList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTimesheetList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        #region CONSTANTS
        String STR_LST_ITEMS_XPATH_OLD = ".//div[contains(@class, 'x-list-item')]//div[@class='x-innerhtml']";
        String STR_LST_ITEMS_XPATH_NEW = ".//div[contains(@class, 'x-listitem')]//div[@class='x-innerhtml']";
        #endregion

        #region VARIABLES
        IList<IWebElement> listbox = null;
        #endregion

        [Keyword("LongPress", new String[] { "1|text|Value|SampleValue" })]
        public void LongPress(String Value)
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
                            Swipe("down", Convert.ToInt32(Math.Abs(dYTranslation)));
                        }
                        else
                        {
                            Swipe("up", Convert.ToInt32(dYTranslation));
                        }

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

        [Keyword("LongPressByRow", new String[] { "1|text|Value|SampleValue" })]
        public void LongPressByRow(String RowIndex)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                GetAvailableListItems(mElement);
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = listbox.ElementAt(int.Parse(RowIndex) - 1);
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
                            Swipe("down", Convert.ToInt32(Math.Abs(dYTranslation)));
                        }
                        else
                        {
                            Swipe("up", Convert.ToInt32(dYTranslation));
                        }

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


        [Keyword("Select", new String[] { "1|text|Value|SampleValue"})]
        public void Select(String Value)
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
                mSelectedItem.Click();

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
                GetAvailableListItems(mElement);
                IWebElement mSelected = null;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
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
                ctlItem.Click();
                DlkLogger.LogInfo("SelectByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByRow() failed : " + e.Message, e);
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

        /// <summary>
        /// Swipe a list item given its value
        /// </summary>
        /// <param name="Value">Value of item to swipe</param>
        /// <param name="UpDownLeftRight">Direction of swipe</param>
        [Keyword("SwipeItemByRow", new String[] { "1|text|Row|1", 
                                                  "2|text|UpDownLeftRight|Up"})]
        public void SwipeItemByRow(String Row, String UpDownLeftRight)
        {
            try
            {
                Initialize();

                IWebElement mSelected = null;
                GetAvailableListItems(mElement);
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
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
                        mSelected = mElement.FindElement(By.XPath(GetAvailableListItems(mElement) + "[contains(., '" + SearchedText + "')]"));
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
                    GetAvailableListItems(mElement);
                    int index = 0;
                    for (index = 0; index < listbox.Count; index++)
                    {
                        DlkBaseControl ctlItem = new DlkBaseControl("Item", listbox[index]);
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

        [Keyword("GetLastRowWithText", new String[] { "1|text|Value|SampleValue" })]
        public void GetLastRowWithText(String ColumnIndex, String PartialText, String VariableName)
        {
            try
            {
                //guard clause
                int colIndex = 0;
                int.TryParse(ColumnIndex, out colIndex);

                Initialize();
                bool bFound = false;
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        /* Get all items */
                        IList<IWebElement> lstItems = mElement.FindElements(By.XPath(GetAvailableListItems(mElement) + "/div[" + colIndex.ToString() + "]")).ToList();

                        for (int index = 0; index < lstItems.Count; index++)
                        {
                            DlkBaseControl ctl = new DlkBaseControl("Item", lstItems[index]);
                            if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "").Contains(DlkString.ReplaceCarriageReturn(PartialText, "")))
                            {
                                DlkVariable.SetVariable(VariableName, (index + 1).ToString()); // keep overwriting until last item is reached
                                bFound = true;
                            }
                        }
                        break;
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }

                if (!bFound)
                {
                    throw new Exception("Unable to find list item with value '" + PartialText + "'");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetLastRowWithText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Row|1" })]
        public void VerifyRowCount(String ExpectedCount)
        {
            try
            {
                Initialize(); 
                int rowCount = 0;
                GetAvailableListItems(mElement);
                 if (listbox != null)
                {
                    rowCount = listbox.Count();
                }

                DlkAssert.AssertEqual("VerifyRowCount", Convert.ToInt32(ExpectedCount), rowCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowCount(String VariableName)
        {
            try
            {
                 Initialize();
                GetAvailableListItems(mElement);
                int rowCount = 0;

                if (listbox != null)
                {
                    rowCount = listbox.Count();
                }

                DlkFunctionHandler.AssignToVariable(VariableName, rowCount.ToString());
                DlkLogger.LogInfo("GetRowCount() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
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
                GetAvailableListItems(mElement);
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
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
                DlkAssert.AssertEqual("VerifyTextContains", true, ctlItem.GetValue().ToLower().Contains(ExpectedText.ToLower()));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemAvailableInList", new String[] { "1|text|ExpectedText|Job1"})]
        public void VerifyItemAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                bool expected = false;
                if (!Boolean.TryParse(TrueOrFalse, out expected))
                {
                    throw new ArgumentException("TrueOrFalse must be a Boolean value.");
                }
                if (String.IsNullOrWhiteSpace(Item))
                {
                    throw new ArgumentException("Item must not be empty.");
                }
                Initialize();
                IWebElement mSelected = null;
                var bFound = false;
                var expectedLines = Item.Split('~'); // to add support for items that have multiple lines.
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++) // retry to find element.
                {
                    if (expectedLines.Length < 1) // if single line
                    {
                        mSelected = mElement.FindElements(By.XPath(GetAvailableListItems(mElement) + "[contains(.,'" + Item + "')]")).Where(x => x.Displayed).FirstOrDefault();
                        if (mSelected != null)
                        {
                            bFound = true;
                            DlkAssert.AssertEqual("VerifyItemAvailableInList", expected, bFound);
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                    else // if multiple lines
                    {
                        // get an array of visible list items.
                        GetAvailableListItems(mElement);
                        var items = listbox.ToArray();
                        // iterate over each list item, then construct a string by iterating over its items.
                        bFound = false;
                        bool bPass = false;
                        int itemCounter = 0;
                        foreach (var item in items)
                        {
                            try
                            {
                                DlkBaseControl mListItem = new DlkBaseControl("List Item", item);
                                if (!item.Displayed) mListItem.ScrollIntoViewUsingJavaScript();
                                itemCounter++;
                                // store the single list item's multiple lines of text
                                var itemText = item.FindElements(By.XPath("./div/div")).Where(x => !String.IsNullOrWhiteSpace(x.Text)).ToArray(); // do not add divs that do not have texts because there are.
                                if (itemText.Length < 1)
                                {
                                    DlkLogger.LogInfo(String.Format("No text found for item number {0}. Skipping...", itemCounter));
                                    continue;
                                }
                                // fail it immediately here because we know that it will already fail if expected == true and supplied parameters and actual line count is not the same
                                if (itemText.Length != expectedLines.Length && expected && itemCounter < items.Length)
                                {
                                    DlkLogger.LogInfo(String.Format("Mismatch of line number for item {2}. Line by line checking will fail. Actual line count:[{1}], Expected line count:[{0}].",  expectedLines.Length, itemText.Length, itemCounter));
                                    continue;
                                }
                                else if (itemText.Length != expectedLines.Length && expected && itemCounter == items.Length)
                                {
                                    throw new Exception("Expected line count and actual line count are not equal for all items.");
                                }

                                var results = new Boolean[itemText.Length];

                                for (int index = 0; index < itemText.Length; index++)
                                {
                                    if (!itemText[index].Displayed) new DlkBaseControl("item line of text", itemText[index]).ScrollIntoViewUsingJavaScript();
                                    DlkLogger.LogInfo(String.Format("Expected value: [{0}] . Actual value: [{1}]", expectedLines[index], itemText[index].Text));
                                    results[index] = itemText[index].Text.Contains(expectedLines[index]);
                                }
                                bFound = results.All(result => result == results.FirstOrDefault()); // check if value is consistent across all lines in an item
                                /* 
                                 POSSIBLE SCENARIOS (FOR FUTURE EDITORS' REFERENCE)
                                 ================================================
                                 =  Expected  = ACTUAL  = LAST LINE?= RESULT
                                 =      T     =    T    =     F/T   = AUTO PASS
                                 =      F     =    F    =     T     = PASS
                                 =      F     =    F    =     F     = GO TO NEXT
                                 =      T     =    F    =     T     = FAIL
                                 =      T     =    F    =     F     = GO TO NEXT
                                 =      F     =    T    =     F/T   = AUTO FAIL
                                 ================================================
                                 */
                                // for auto-pass and auto-fail scenarios. see multi-line comment above for reference
                                if (expected && bFound) // T,T,T/F
                                {
                                    bPass = true;
                                    break;
                                }
                                else if (!expected && bFound) // F,T,T/F
                                {
                                    bPass = false;
                                    break;
                                }
                                // for other pass and fail scenarios, including go to next scenarios
                                bPass = (expected && !bFound) ? false : true; // F,F,T and T,F,T
                                if (itemCounter < items.Length)// F,F,F and T,F,F
                                {
                                    DlkLogger.LogInfo("Looking at next item...");
                                    continue;
                                };  
                                Thread.Sleep(1000);
                            }
                            catch 
                            {
                                continue;
                            }
                        }
                        if (!bPass)
                        {
                            throw new Exception(String.Format("Expected Result [{0}] not equal to Actual result [{1}]", expected, bFound));
                        }
                        else
                        {
                            DlkLogger.LogInfo("Passed.");
                        }
                        break;

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHoursField", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedHours|8:00"})]
        public void VerifyHoursField(String Row, String ExpectedHours)
        {
            try
            {
                Initialize();
                GetAvailableListItems(mElement);
                IWebElement item = listbox.ElementAt(int.Parse(Row) - 1);
                IWebElement fldHours = item.FindElement(By.XPath(".//input[contains(@class,'tsHoursField')]"));
                DlkBaseControl ctlItem = new DlkBaseControl("Item", fldHours);
                DlkAssert.AssertEqual("VerifyHoursField", ExpectedHours, ctlItem.GetValue());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHoursField() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyDeleteButtonExists", new String[] { "1|text|Row|1",
                                                    "2|TRUE|FALSE"})]
        public void VerifyDeleteButtonExists(String Row, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bFound = false;
                GetAvailableListItems(mElement);
                IWebElement item = listbox.ElementAt(int.Parse(Row) - 1);
                if (item != null)
                {
                    var button = item.FindElements(By.XPath(".//button[contains(@class,'swipeDeleteBtn')]")).Count > 0 ? item.FindElement(By.XPath(".//button[contains(@class,'swipeDeleteBtn')]")) : 
                        null;
                    bFound = button != null && button.Displayed;  
                }

                DlkAssert.AssertEqual("VerifyDeleteButtonExists", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDeleteButtonExists() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickHoursField", new String[] { "1|text|Row|1" })]
        public void ClickHoursField(String Row)
        {
            try
            {
                try
                {
                    Initialize();
                    IWebElement item = mElement
                        .FindElements(By.XPath(GetAvailableListItems(mElement) + "//input[@class='tsHoursField']"))
                        .ElementAt(int.Parse(Row) - 1);
                    DlkTextBox ctlItem = new DlkTextBox("Item", item);
                    ctlItem.Click();
                }
                catch 
                {
                    IWebElement item = mElement
                        .FindElements(By.XPath(GetAvailableListItems(mElement) + "//*[@class='pickertap']"))
                        .ElementAt(int.Parse(Row) - 1);
                    DlkTextBox ctlItem = new DlkTextBox("Item", item);
                    ctlItem.Click();
                }
            }
            catch( Exception e)
            {
                throw new Exception("ClickHoursField() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickDeleteButton", new String[] { "1|text|Row|1" })]
        public void ClickDeleteButton(String Row)
        {
            try
            {
                Initialize();
                GetAvailableListItems(mElement);
                IWebElement item = listbox.ElementAt(int.Parse(Row) - 1);
                if (item != null)
                {
                    var button = item.FindElements(By.XPath(".//button[contains(@class,'swipeDeleteBtn')]")).Count > 0 ? item.FindElement(By.XPath(".//button[contains(@class,'swipeDeleteBtn')]")) :
                        null;
                    DlkButton ctlItem = new DlkButton("Item", button);
                    ctlItem.Click(3);
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("ClickDeleteButton() failed : " + e.Message, e);
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

        [Keyword("VerifyRowField", new String[] { "1|text|Row|1",
                                                  "2|text|ColumnIndex",
                                                  "3|text|ExpectedField|8:00",
                                                  "4|text|TrueOrFalse|True"})]
        public void VerifyRowField(String Row, String ColumnIndex, String ExpectedField, String TrueOrFalse)
        {
            try
            {
                bool expIsEqual, actIsEqual = false;
                int rowIdx, colIdx;

                if (!Boolean.TryParse(TrueOrFalse, out expIsEqual)) throw new Exception("Invalid input for TrueOrFalse");
                if (!Int32.TryParse(Row, out rowIdx)) throw new Exception("Invalid input for Row");
                if (!Int32.TryParse(ColumnIndex, out colIdx)) throw new Exception("Invalid input for ColumnIndex");

                Initialize();
                IWebElement mField = null;
                

                string fieldXPath = GetAvailableListItems(mElement) + "[ " + Row + "]" + "//div[" + ColumnIndex + "]";
                if (mElement.FindElements(By.XPath(fieldXPath)).Count > 0)
                {
                    string actualFieldValue = string.Empty;

                    mField = mElement.FindElement(By.XPath(fieldXPath));

                    DlkBaseControl ctlItem = new DlkBaseControl("Item", mField);
                    actualFieldValue = DlkString.RemoveCarriageReturn(ctlItem.GetValue());
                    
                    DlkLogger.LogInfo(String.Format("Field value found: {0}", actualFieldValue));

                    if (ExpectedField.Equals("period", StringComparison.InvariantCultureIgnoreCase))
                    {
                        List<String> dates = actualFieldValue.Split('-').ToList().ConvertAll(x => x.Trim());
                        
                        DateTime tDate; //Just a placeholder for the out; Don't have to do anything with it.
                        if (dates.All(x => DateTime.TryParse(x, out tDate)))
                        {
                            actIsEqual = true;
                        }
                    }
                    else if (ExpectedField.Equals("hours", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if(DlkString.IsValidTime(actualFieldValue))
                        {
                            actIsEqual = true;
                        }
                    }
                    
                }
                else
                {
                    throw new Exception(String.Format("Unable to find row field with the given Row [{0}] and ColumnIndex [{1}].", Row, ColumnIndex));
                }
                
                DlkAssert.AssertEqual("VerifyRowField", expIsEqual, actIsEqual);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowField() failed : " + e.Message, e);
            }
        }

        public String GetAvailableListItems(IWebElement mElms)
        {
            DlkBaseControl mList = new DlkBaseControl("List", mElms);
            int listSize = -1;

            listSize = mList.GetNativeViewCenterCoordinates().Y;

            if (mElement.FindElements(By.XPath(STR_LST_ITEMS_XPATH_OLD)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
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
            else
            {
                throw new Exception("List type not yet supported.");
            }
        }
    }
}
