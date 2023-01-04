using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRSmartLib.DlkControls;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("List")]
    public class DlkList : CommonLib.DlkControls.DlkBaseControl
    {
        #region Declarations


        #endregion

        #region Constructors

        public DlkList(String ControlName, String SearchType, String SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        public DlkList(String ControlName, IWebElement ExistingElement)
            : base(ControlName, ExistingElement)
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("HoverAndVerifyLinkCaptionByRow")]
        public void HoverAndVerifyLinkCaptionByRow(string Row, string Caption, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool bActualResult = false;
                bool bExpectedResult = Convert.ToBoolean(TrueOrFalse);
                IWebElement row = GetRow(Row);
                //DlkCommon.DlkCommonFunction.ScrollIntoElement(row);
                string actualResult = row.FindElement(By.XPath(@".//a")).GetAttribute("data-content");
                string expectedResult = Caption.Trim();
                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(row).Build();
                mAction.Perform();

                if (actualResult.Equals(expectedResult))
                {
                    bActualResult = true;
                }

                DlkAssert.AssertEqual("HoverAndVerifyLinkCaptionByRow : ", bExpectedResult, bActualResult);

                DlkLogger.LogInfo("HoverAndVerifyLinkCaptionByRow() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HoverAndVerifyLinkCaptionByRow() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("HoverAndVerifyClickableByRowIndex")]
        public void HoverAndVerifyClickableByRowIndex(string Row, string Index, string Caption, string TrueOrFalse)
        {
            try
            {
                initialize();
                int index = Convert.ToInt16(Index) - 1;
                bool bActualResult = false;
                bool bExpectedResult = Convert.ToBoolean(TrueOrFalse);
                string expectedResult = Caption.Trim();
                string actualResult = string.Empty;
                IWebElement row = GetRow(Row);
                //DlkCommon.DlkCommonFunction.ScrollIntoElement(row);
                IList<IWebElement> clickableElements = row.FindElements(By.XPath(".//a[not(contains(@class,'hidden'))] | .//button[not(contains(@class,'hidden'))]"));

                if (clickableElements[index] != null) 
                {
                    actualResult = string.IsNullOrEmpty(clickableElements[index].GetAttribute("data-content")) ? clickableElements[index].GetAttribute("data-original-title") : clickableElements[index].GetAttribute("data-content");
                    if (string.IsNullOrEmpty(actualResult))
                    {
                        actualResult = string.IsNullOrEmpty(clickableElements[index].GetAttribute("title")) ? string.Empty : clickableElements[index].GetAttribute("title");
                    }
                }

                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(row).Build();
                mAction.Perform();

                if (actualResult.Equals(expectedResult))
                {
                    bActualResult = true;
                }

                DlkAssert.AssertEqual("HoverAndVerifyClickableByRowIndex : ", bExpectedResult, bActualResult);

                DlkLogger.LogInfo("HoverAndVerifyClickableByRowIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HoverAndVerifyClickableByRowIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemPosition")]
        public void VerifyItemPosition(string ItemText, string PositionIndex, string TrueOrFalse)
        {
            try
            {
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                initialize();
                IWebElement row = GetRow(PositionIndex);
                DlkBaseControl rowControl = new DlkBaseControl("Row", row);
                string actualItemText = rowControl.GetValue();

                // special case when value is not the text
                if (mControlName == "Detail_List_ReadAndSignFileURL")
                {
                    if (!string.IsNullOrEmpty(rowControl.GetAttributeValue("data-original-title")))
                    {
                        actualItemText = rowControl.GetAttributeValue("data-original-title");
                    }
                }
                
                if (ItemText.Equals(actualItemText))
                {
                    actualResult = true;
                }

                DlkLogger.LogInfo("Actual Result : " + actualItemText);
                DlkAssert.AssertEqual("Row Item", expectedResult, actualResult);
                
                DlkLogger.LogInfo("VerifyItemPosition() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemPosition() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount")]
        public void VerifyRowCount(string RowCount)
        {
            try
            {
                initialize();
                int expectedResult = Convert.ToInt32(RowCount);
                int actualResult = getRows().Count;
                DlkAssert.AssertEqual("VerifyRowCount : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() execution failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyClickableRowItem")]
        public void VerifyClickableRowItem(string Row, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                IWebElement row = GetRow(Row);
                IList<IWebElement> clickableElements = row.FindElements(By.XPath(".//a | .//button | .//i"));
                if (clickableElements.Count > 0)
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("VerifyClickableRowItem : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyClickableRowItem() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyClickableRowItem() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyClickableInputValueByRowIndex")]
        public void VerifyClickableInputValueByRowIndex(string Row, string Index, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool elementFound = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int index = Convert.ToInt16(Index) - 1;
                int indexCounter = -1;

                IWebElement row = GetRow(Row);
                IList<IWebElement> clickableElements = row.FindElements(By.XPath(".//input[@type='radio' or @type='checkbox'] | .//a/i[@class='fa fa-history']"));

                foreach (IWebElement item in clickableElements)
                {
                    if (!item.Displayed)
                    {
                        continue;
                    }

                    switch (item.TagName)
                    {
                        case "input":
                            {
                                string typeAttribute = item.GetAttribute("type");
                                if (typeAttribute != null &&
                                   (typeAttribute.Trim().Equals("checkbox") || typeAttribute.Trim().Equals("radio")))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        elementFound = true;
                                        string checkedAttribute = item.GetAttribute("checked");
                                        if (checkedAttribute != null &&
                                            (checkedAttribute.Equals("true") || checkedAttribute.Equals("checked")))
                                        {
                                            actualResult = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case "i" :
                            {
                                indexCounter++;
                                if (index == indexCounter)
                                {
                                    elementFound = true;
                                    actualResult = true;
                                }
                                
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    if (elementFound)
                    {
                        break;
                    }
                }


                if (!elementFound)
                {
                    actualResult = false;
                }

                DlkAssert.AssertEqual("VerifyClickableInputValueByRowIndex : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyClickableInputValueByRowIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyClickableInputValueByRowIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyReadOnlyByRowIndex")]
        public void VerifyReadOnlyByRowIndex(string Row, string Index, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool elementFound = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int index = Convert.ToInt16(Index) - 1;
                int indexCounter = -1;

                IWebElement row = GetRow(Row);
                IList<IWebElement> clickableElements = row.FindElements(By.XPath(".//input[(@type='radio' or @type='checkbox') and not(contains(@class,'hidden'))] | .//a[not(contains(@class,'hidden'))] | .//button[not(contains(@class,'hidden'))]"));

                foreach (IWebElement item in clickableElements)
                {
                    if (!item.Displayed)
                    {
                        continue;
                    }

                    switch (item.TagName)
                    {
                        case "input":
                            {
                                string typeAttribute = item.GetAttribute("type");
                                indexCounter++;
                                if (index == indexCounter)
                                {
                                    elementFound = true;
                                    string readOnly = item.GetAttribute("disabled");
                                    if (readOnly != null &&
                                        (readOnly.Equals("true") || readOnly.Equals("disabled")))
                                    {
                                        actualResult = true;
                                    }
                                }

                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    if (elementFound)
                    {
                        break;
                    }
                }


                if (!elementFound)
                {
                    throw new Exception("No element found to set.");
                }

                DlkAssert.AssertEqual("VerifyReadOnlyByRowIndex : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyReadOnlyByRowIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyReadOnlyByRowIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string ItemText, string TrueOrFalse)
        {
            StringBuilder actualResults = new StringBuilder();
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                List<IWebElement> rows = getRows();
                foreach (IWebElement row in rows)
                {
                    if (row != null)
                    {
                        DlkBaseControl rowControl = new DlkBaseControl(row.Text, row);
                        string result = rowControl.GetValue().Replace("\r\n", "\n").Replace("\n\r", "\n").Trim();
                        if (result.Equals(ItemText))
                        {
                            actualResult = true;
                            break;
                        }
                        actualResults.AppendLine("Item : [" + result + "]");
                    }
                }

                DlkAssert.AssertEqual("VerifyItemExists : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyItemExists() successfully executed.");
            }
            catch (Exception ex)
            {
                DlkLogger.LogInfo("Actual Results : \n " + actualResults);
                throw new Exception("VerifyItemExists() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyItemContains")]
        public void VerifyItemContains(string ItemText, string TrueOrFalse)
        {
            try
            {
                initialize();
                StringBuilder actualTextBuilder = new StringBuilder();
                string[] expectedResults = ItemText.Split('~');
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);

                for (int i = 0; i < expectedResults.Length; i++)
                {
                    bool actualResult = false;
                    List<IWebElement> rows = getRows();

                    string expectedText = expectedResults[i];
                    string actualText = string.Empty;

                    for (int j = 0; j < rows.Count; j++)
                    {
                        IWebElement row = rows[j];
                        DlkBaseControl rowControl = new DlkBaseControl(row.Text, row);
                        actualText = rowControl.GetValue().Replace("\r\n", "\n").Replace("\n\r", "\n").Trim();

                        if (i == 0)
                        {
                            //build the actual text of the list ONCE.
                            int rowNumber = j + 1;
                            actualTextBuilder.AppendLine("[ROW : " + rowNumber + "\n" + actualText + "]");
                        }

                        if (actualText.Contains(expectedText))
                        {
                            actualResult = true;
                            break;
                        }
                    }

                    if (i == 0)
                    {
                        DlkLogger.LogInfo(actualTextBuilder.ToString());
                    }

                    DlkAssert.AssertEqual("Expected Text : " + expectedText, expectedResult, actualResult);
                }

                DlkLogger.LogInfo("VerifyItemContains() successfully executed."); ;
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemContains() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickByRow")]
        public void ClickByRow(string Row)
        {
            try
            {
                initialize();
                clickByRowIndex(GetRow(Row), 1); 
                DlkLogger.LogInfo("ClickByRow( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("GetIndexFromRowByName")]
        public void GetIndexFromRowByName(string Row, string Name, string VariableName)
        {
            try
            {
                initialize();
                IWebElement row = GetRow(Row);
                List<IWebElement> elements = row.FindElements(By.XPath(".//a")).ToList();

                for (int i = 0; i < elements.Count; i++)
                {
                    string text = elements[i].FindElement(By.XPath("./parent::*")).Text;

                    if (text == Name)
                    {
                        int index = i + 1;
                        DlkVariable.SetVariable(VariableName, index.ToString());
                        DlkLogger.LogInfo(string.Concat("Successfully stored index[" + index.ToString() + "] in : ", VariableName));
                        break;
                    }
                }

                DlkLogger.LogInfo("GetIndexFromRowByName( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("GetIndexFromRowByName() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickByRowIndex")]
        public void ClickByRowIndex(string Row, string Index)
        {
            try
            {
                initialize();
                clickByRowIndex(GetRow(Row), Convert.ToInt32(Index)); 
                DlkLogger.LogInfo("ClickByRowIndex( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickByRowIndex() execution failed. " + ex.Message, ex);
            }
        }


        [RetryKeyword("ClickRetryIfSetControlNotExistByIndex")]
        public void ClickRetryIfSetControlNotExistByIndex(string Row, string Index)
        {
            int retryCounter = -1;
            this.PerformAction(() =>
            {
                retryCounter++;
                initialize();
                IWebElement rowElement = GetRow(Row);
                
                if (retryCounter == 0 ||
                    !DlkCommon.DlkCommonFunction.VerifyElementByIndexExists(rowElement, 0))
                {
                    clickByRowIndex(GetRow(Row), Convert.ToInt32(Index));
                    bool actualResult = DlkCommon.DlkCommonFunction.VerifyElementByIndexExists(rowElement, 0);
                    DlkAssert.AssertEqual("ClickRetryIfSetControlNotExistByIndex", true, actualResult);
                }

            }, new String[] { "retry" });
        }

        [Keyword("ClickByTitle")]
        public void ClickByTitle(string Title)
        {
            try
            {
                initialize();
                IWebElement elementWithText = DlkCommon.DlkCommonFunction.GetElementWithText(Title, mElement, true, returnDisplayedElements:true)[0];

                if (elementWithText.GetAttribute("class").Contains("dataSourceCatalog")) // handles catalog list
                {
                    elementWithText.Click();
                }
                else if (mElement.GetAttribute("id") == "cal-day-box")
                {
                    IList<IWebElement> rows = mElement.FindElements(By.XPath(".//div[contains(@class,'day-item-wrapper')]/a")).ToList();

                    foreach (var row in rows)
                    {
                        string rowTitle = row.Text.Trim();

                        if (rowTitle == Title)
                        {
                            row.Click();
                            break;
                        }
                    }
                }
                else if (mElement.FindElement(By.XPath("./parent::div")).GetAttribute("class") == "cal-week-box")
                {
                    IList<IWebElement> rows = mElement.FindElements(By.XPath(".//div[contains(@class,'week-day-item')]/a")).ToList();

                    foreach (var row in rows)
                    {
                        string rowTitle = row.Text.Trim();

                        if (rowTitle == Title)
                        {
                            row.Click();
                            break;
                        }
                    }
                }
                else
                {
                    IList<IWebElement> rows = elementWithText.FindElements(By.XPath("(./ancestor::li)[last()]"));
                    if (rows.Count == 0)
                    {
                        rows = elementWithText.FindElements(By.XPath("./ancestor::div"));
                    }

                    IWebElement row = rows[0];
                    IList<IWebElement> clickableElement = row.FindElements(By.XPath(@".//img"));
                    if (clickableElement.Count == 0)
                    {
                        clickByRowIndex(row, 1);
                    }
                    else
                    {
                        clickableElement[0].Click();
                    }
                }

                DlkLogger.LogInfo("ClickByTitle( ) successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("ClickByTitle( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("SetByRowIndex")]
        public void SetByRowIndex(string Row, string Value, string Index)
        {
            try
            {
                initialize();
                IWebElement rowElement = GetRow(Row);
                DlkCommon.DlkCommonFunction.SetElementByIndex(rowElement, Value, Convert.ToInt32(Index) - 1);
                DlkLogger.LogInfo("SetByRowIndex( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SetByRowIndex( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("GetRowWithValue")]
        public void GetRowWithValue(string RowSearchValue, string VariableName)
        {
            try
            {
                initialize();
                bool rowFound = false;
                IList<IWebElement> rowElements = mElement.FindElements(By.XPath(@".//h4[@id or @class='panel-title'] | .//div[@class='row']//*[@title]
                | .//div[@class='form-control-static']/a | .//div[@class='row']//preceding-sibling::h4"));               
                var items = rowElements.Where(x => !string.IsNullOrEmpty(x.Text)).GroupBy(x => x.Text).Select(x => x.First()).ToList();

                if (items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        int rowNumber = i + 1;
                        DlkBaseControl rowControl = new DlkBaseControl("Row", items[i]);
                        string rowValue = rowControl.GetValue();
                        if (rowValue.Equals(RowSearchValue))
                        {
                            DlkVariable.SetVariable(VariableName, rowNumber.ToString());
                            DlkLogger.LogInfo(string.Concat("Successfully stored value in : ", VariableName));
                            rowFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("No rows found.");
                }

                if (!rowFound)
                {
                    throw new Exception(string.Concat("Row not foung using : ", RowSearchValue, "."));
                }

                DlkLogger.LogInfo("GetRowWithValue( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("GetRowWithValue( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySort")]
        public void VerifySort(string SortOrder, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                IList<IWebElement> rowElements = mElement.FindElements(By.XPath(@".//h4[@id]"));
                List<string> actualRows = new List<string>();
                List<string> sortedRows = new List<string>();

                foreach (IWebElement element in rowElements)
                {
                    DlkBaseControl rowControl = new DlkBaseControl("row", element);
                    string val = rowControl.GetValue();
                    actualRows.Add(val);
                    sortedRows.Add(val);
                }


                var sorted = sortedRows.OrderByDescending(item => item);

                if (SortOrder.ToUpper().Contains("ASC") ||
                    SortOrder.ToUpper().Contains("ASCENDING"))
                {

                    sorted = sortedRows.OrderBy(item => item);

                }


                if (actualRows.SequenceEqual(sorted))
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("VerifySort  : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifySort ( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySort( ) execution failed. : " + ex.Message, ex);
            }
        }
        
        [Keyword("DragAndDropByRow")]
        public void DragAndDropByRow(string FromRow, string ToRow)
        {
            try
            {
                initialize();
                int toRowIndex = Convert.ToInt16(ToRow) - 1;
                int fromRowIndex = Convert.ToInt16(FromRow) - 1;
                List<IWebElement> rows = getDragAndDropRows(out int rowType);

                if (rows[fromRowIndex] == null || 
                    rows[toRowIndex] == null)
                {
                    throw new Exception("Please check parameter value as it contains null.");
                }

                // use rowType to customize DragAndDrop functionality depending on the row type found
                switch (rowType)
                {
                    case 0:
                    case 3:
                    case 4:
                        if (toRowIndex > fromRowIndex) // higher position
                        {
                            if (toRowIndex != rows.Count - 1)
                            {
                                toRowIndex++;
                                DlkCommon.DlkCommonFunction.DragAndDrop(rows[fromRowIndex], rows[toRowIndex], 0, -5);
                            }
                            else
                            {
                                DlkCommon.DlkCommonFunction.DragAndDrop(rows[fromRowIndex], rows[toRowIndex], 0, 10);
                            }
                        }
                        else
                        {
                            if (toRowIndex != 0)
                            {
                                toRowIndex--;
                                DlkCommon.DlkCommonFunction.DragAndDrop(rows[fromRowIndex], rows[toRowIndex], 0, 5);
                            }
                            else
                            {
                                DlkCommon.DlkCommonFunction.DragAndDrop(rows[fromRowIndex], rows[toRowIndex], 0, -10);
                            }
                        }
                        break;
                    default:
                        DlkCommon.DlkCommonFunction.DragAndDrop(rows[fromRowIndex], rows[toRowIndex]);
                        break;
                }                
            }
            catch(Exception ex)
            {
                throw new Exception("DragAndDropByRow() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private List<IWebElement> getRows()
        {
            //return rows using class attribute if it is from a form element.
            //this is a label list control.
            if (mElement.TagName.Equals("form"))
            {
                return mElement.FindElements(By.XPath(".//div[@class='form-group']")).ToList();
            }

            return mElement.FindElements(By.XPath("./li[not(contains(@style,'none'))] | ./div | ./span | ./ul[@class='list-unstyled']")).Select(item => { if (item.Displayed) return item; else return null; }).ToList();
        }

        public  IWebElement GetRow(string row)
        {
            IWebElement rowElement = null;

            // special case on Read_And_Sign_List under Add_Edit_Course
            if (mControlName == "Read_And_Sign_URL_List" || mControlName == "Read_And_Sign_File_List")
            {
                int numRow = Int32.Parse(row);
                numRow = numRow * 2;
                row = numRow.ToString();
            }

            List<string> searchRows = new List<string>()
            {
                "(./li | .//span[contains(@class,'ng-scope')]/li | .//div[@class='row']/ancestor::*[@id='profile-content' or @id='editWorkInfoForm']//div[@class='row'])[" + row + "] | ./div[@class='mar-btm'] | .//div[contains(@style,'display')]/li",
                "(.//div[@class='calendar_cell_slot big'] | .//div[contains(@class,'row')]/parent::div[@class='row-table']/./div[contains(@class,'row')] | ./ul[contains(@class,'list-unstyled')])[" + row + "]",
                "(.//div[@id and not(contains(@style,'none')) and @class='panel-group '])[" + row + "]",
                "(.//div[@id and @role='tab'] | .//div[contains(@class,'form-group')])[" + row + "]",
                "(.//div[@id and not(contains(@style,'none'))])[" + row + "]",
                "(.//div[@class='media' or @class='checkbox'])[" + row + "]",
                "(.//span)[" + row + "]",
                "./div[" + row + "]/div"
            };

            for (int i = 0; i < searchRows.Count; i++)
            {
                IList<IWebElement> rowElements = mElement.FindElements(By.XPath(searchRows[i]));
                if (rowElements.Count > 0)
                {
                    rowElement = rowElements[0];
                    break;
                }
            }

            return rowElement;
        }

        public List<IWebElement> getDragAndDropRows(out int rowType)
        {
            List<string> searchRows = new List<string>()
            {
                ".//div[@class='form-group']//a/parent::div", //supports Add_Edit_Course Read_And_Sign_URL_List
                ".//span[@class='handler']/..", //dynamic forms
                "./li[contains(@class,'sortable')]", //recruiting workflow
                ".//div[contains(@class,'sortable-handler')]", // supports Select_List_Management Definition_Item_List
                ".//li", // general list element
                ".//div[contains(@class,'sortable-handle')]" // approval chains
            };
            

            for (int i = 0; i < searchRows.Count; i++)
            {
                IList<IWebElement> rowElements = mElement.FindElements(By.XPath(searchRows[i]));
                if (rowElements.Count > 0)
                {
                    rowType = i;
                    return rowElements.ToList();
                }
            }

            rowType = -1;
            return null;
        }

        private void clickByRowIndex(IWebElement rowElement, int index)
        {
            int indexCounter = 0;
            bool hasExpectedElement = false;          
            IList<IWebElement> rowElements = rowElement.FindElements(By.XPath(".//button | .//a | .//h6 | .//input | .//i"));
            foreach (IWebElement element in rowElements)
            {
                if (element.Displayed)
                {
                    switch (element.TagName.Trim())
                    {
                        case "button":
                        case "a":
                            {
                                if (element.TagName.Trim() == "a" && element.GetAttribute("href") == null && element.GetAttribute("data-toggle") == "tooltip")
                                {
                                    continue;
                                }
                                else
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkButton buttonControl = new DlkButton("Button_Element", element);
                                        if (IsFromIFrame)
                                        {
                                            buttonControl.ClickFromIframe();
                                        }
                                        else
                                        {
                                            buttonControl.Click();
                                        }
                                    }
                                }
                                break;
                            }
                        case "h6":
                            {

                                if (element.GetAttribute("class") != null &&
                                    element.GetAttribute("class").Trim().Contains("dropdown"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        element.Click();
                                    }
                                }
                                break;
                            }
                        case "input":
                            {
                                if (element.GetAttribute("type") != null &&
                                    element.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox_Element : " + element.Text, element);
                                        checkBoxControl.Click();
                                    }
                                }
                                else if (element.GetAttribute("type") != null &&
                                         element.GetAttribute("type").Equals("radio"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkRadioButton radioButtonControl = new DlkRadioButton("RadioButton_Element : " + element.Text, element);
                                        radioButtonControl.Click();
                                    }
                                }
                                else if (element.GetAttribute("type") != null &&
                                         element.GetAttribute("type").Equals("image"))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkButton buttonControl = new DlkButton("Button Control : " + element.Text, element);
                                        buttonControl.Click();
                                    }
                                }
                                break;
                            }
                        case "i":
                            {
                                if (element.GetAttribute("class") != null && 
                                   (element.GetAttribute("class").Contains("glyphicon-chevron-right") ||
                                    element.GetAttribute("class").Contains("glyphicon-chevron-down")))
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkButton buttonControl = new DlkButton("Button_Element", element);
                                        buttonControl.Click();
                                        //element.SendKeys(Keys.Enter);
                                    }
                                }
                                break;
                            }
                    }

                    if (hasExpectedElement)
                    {
                        break;
                    }
                }
            }

            if (!hasExpectedElement)
            {
                throw new Exception("Clickable Element not found.");
            }
        }
        private bool IsFromIFrame
        {
            get
            {
                if (mSearchType.ToLower().Equals("iframe_xpath"))
                {
                    return true;
                }

                return false;
            }
        }

        #endregion
    }
}
