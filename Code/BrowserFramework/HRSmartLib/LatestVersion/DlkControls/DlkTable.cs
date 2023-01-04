using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRSmartLib.DlkControls;
using System.Dynamic;
using OpenQA.Selenium.Support.UI;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region Declarations

        private const string ACTIONS = @".//td[contains(@class,'actions_td')]//a | .//td[contains(@class,'actions_td')]//button";
        private const string ROW = @".//tbody/tr | ./tr";
        private const int COLUMN_HEADER_NOT_FOUND = -1;

        #endregion

        #region Constructors

        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {

        }

        public DlkTable(string ControlName, IWebElement ExistingElement)
            : base(ControlName, ExistingElement)
        {

        }

        #endregion

        #region Properties

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

        #region Keywords

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    if (IsFromIFrame)
                    {
                        DlkEnvironment.mSwitchediFrame = false;
                    }

                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("GetTableRowWithColumnValue")]
        public void GetTableRowWithColumnValue(string ColumnHeader, string ColumnSearchValue, string VariableName)
        {
            try
            {
                initialize();
                int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                if (columnHeaderIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    string rowNumber = string.Empty;
                    string currentPage = string.Empty;
                    string currentNumberOfItemPerPage = string.Empty;
                    IWebElement tableRow = getTableRowByHeader(columnHeaderIndex, ColumnSearchValue, out rowNumber);
                    IWebElement paginationElement = findPaginationControl();
                    if (paginationElement == null)
                    {
                        if (rowNumber.Equals(string.Empty))
                        {
                            throw new Exception("Missing details for table row. \nColumn : " + ColumnHeader + "\nColumn Value : " + ColumnSearchValue);
                        }
                        else
                        {
                            DlkVariable.SetVariable(VariableName, string.Concat(currentPage, "-", currentNumberOfItemPerPage, "-", rowNumber));
                        }
                    }
                    else
                    {
                        DlkPagination pagination = new DlkPagination("Pagination_Control", paginationElement);
                        currentPage = pagination.GetCurrentPageNumber();
                        currentNumberOfItemPerPage = pagination.GetCurrentDisplayPerPage();
                        if (rowNumber.Equals(string.Empty) ||
                            currentPage.Equals(string.Empty) ||
                            currentNumberOfItemPerPage.Equals(string.Empty))
                        {
                            throw new Exception("Missing details for table row. \nColumn : " + ColumnHeader + "\nColumn Value : " + ColumnSearchValue);
                        }
                        else
                        {
                            DlkVariable.SetVariable(VariableName, string.Concat(currentPage, "-", currentNumberOfItemPerPage, "-", rowNumber));
                            DlkLogger.LogInfo("GetTableRowWithColumnValue( ) successfully executed.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Column Header Not Found.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("GetTableRowWithColumnValue( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyTableRowPrimaryIcon")]
        public void VerifyTableRowPrimaryIcon(string Row, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> primaryIcon = tableRow.FindElements(By.XPath(".//i[contains(@class,'fa fa-star text-gold') or contains(@class,'fa fa-file-text')" +
                    " or contains(@class,'fa fa-check text-muted')]")); // if next icon to be added still contains 'fa fa', simplify xpath locator
                if (primaryIcon.Count > 0)
                {
                    actualResult = true;
                }
                DlkAssert.AssertEqual("VerifyTableRowPrimaryIcon", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyTableRowPrimaryIcon() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowPrimaryIcon( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("AssertTableRowPrimaryIcon")]
        public void AssertTableRowPrimaryIcon(string Row, string ExpectedIcon)
        {
            try
            {
                // ExpectedIcon must be set to following:
                // "check", "link"," file", "ninebox"
                initialize();
                bool equalResult = false;
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> primaryIcon = tableRow.FindElements(By.XPath(".//td/i"));

                if (primaryIcon.Count > 0)
                {
                    IWebElement icon = primaryIcon[0];
                    string classIcon = icon.GetAttribute("class");
                    string actualIcon = "";

                    switch (classIcon)
                    {
                        case "fa fa-check text-muted":
                            actualIcon = "check";
                            break;
                        case "fa fa-fw fa-link":
                            actualIcon = "link";
                            break;
                        case "fa fa-fw fa-file-o":
                            actualIcon = "file";
                            break;
                        default:
                            break;
                    }

                    if (actualIcon.Equals(ExpectedIcon.ToLower().Trim()))
                    {
                        equalResult = true;
                    }
                }
                else //this is for icon that is under span tag
                {
                    primaryIcon = tableRow.FindElements(By.XPath(".//td/span"));

                    if (primaryIcon.Count > 0)
                    {
                        IWebElement icon = primaryIcon[0];
                        string classIcon = icon.GetAttribute("class");
                        string actualIcon = "";

                        switch (classIcon)
                        {
                            case "glyphicon glyphicon-th":
                                actualIcon = "ninebox";
                                break;
                            default:
                                break;
                        }

                        if (actualIcon.Equals(ExpectedIcon.ToLower().Trim()))
                        {
                            equalResult = true;
                        }
                    }
                }
               
                DlkAssert.AssertEqual("AssertTableRowPrimaryIcon", true, equalResult);
                DlkLogger.LogInfo("AssertTableRowPrimaryIcon() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssertTableRowPrimaryIcon( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyTableRowWithColumnValue")]
        public void VerifyTableRowWithColumnValue(string ColumnHeader, string ColumnValue, string Row)
        {
            try
            {
                initialize();
                string[] columnHeaders = ColumnHeader.Split('~');
                string[] columnValues = ColumnValue.Split('~');

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    verifyTableRowWithColumnValue(columnHeaders[i], columnValues[i], Row, false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowWithColumnValue( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyTableRowHasText")]
        public void VerifyTableRowHasText(string Value, string Row)
        {
            try
            {
                initialize();
                string[] valueIndex = Value.Split('~');
                string[] rowIndex = Row.Split('~');

                for (int i = 0; i < valueIndex.Length; i++)
                {
                    IWebElement rowElement = getTableRow(rowIndex[i]);
                    IList<IWebElement> elementsWithText = DlkCommon.DlkCommonFunction.GetElementWithText(valueIndex[i], rowElement, true);

                    if (elementsWithText.Count > 0)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text Control", elementsWithText[0]);
                        DlkAssert.AssertEqual("VerifyTableRowHasText", valueIndex[i], textControl.GetValue().Trim());
                    }
                    else
                    {
                        throw new Exception("No row element with content : " + valueIndex[i]);
                    }
                }

                DlkLogger.LogInfo("Successfully executed VerifyTableRowHasText()");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowHasText( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("HasRowTextContent")]
        public void HasRowTextContent(string Content)
        {
            try
            {
                initialize();
                IList<IWebElement> elementsWithText  = DlkCommon.DlkCommonFunction.GetElementWithText(Content, mElement.FindElement(By.XPath(".//tbody")));
                if (elementsWithText.Count > 0)
                {
                    DlkBaseControl textControl = new DlkBaseControl("Text Control", elementsWithText[0]);
                    DlkAssert.AssertEqual("HasRowTextContent : ", Content, textControl.GetValue().Trim());
                }
                else
                {
                    throw new Exception("No row element with content : " + Content);
                }
                DlkLogger.LogInfo("HasRowTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HasRowTextContent( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyTableRowWithColumnValuePartially")]
        public void VerifyTableRowWithColumnValuePartially(string ColumnHeader, string ColumnValue, string Row)
        {
            try
            {
                initialize();
                string[] columnHeaders = ColumnHeader.Split('~');
                string[] columnValues = ColumnValue.Split('~');

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    verifyTableRowWithColumnValue(columnHeaders[i], columnValues[i], Row, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowWithColumnValuePartially( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyTableRowActionColumn")]
        public void VerifyTableRowActionColumn(string Row, string ColumnNumber, string Value, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                int columnIndex = Convert.ToInt32(ColumnNumber) - 1;
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> actionElementList = getActionList(tableRow, true);

                if (columnIndex < actionElementList.Count)
                {
                    IWebElement selectedAction = actionElementList[columnIndex];
                    string actualValue = selectedAction.GetAttribute("data-original-title") == null ? 
                        selectedAction.GetAttribute("title") : 
                        selectedAction.GetAttribute("data-original-title");

                    if (actualValue == "More Options" || actualValue == string.Empty)
                    {
                        actualValue = string.Empty;
                        IList<IWebElement> dropDownMenuOptions = selectedAction.FindElements(By.XPath(@"../ul[@role='menu' or contains(@class,'menu')]/li/a"));
                        foreach (IWebElement option in dropDownMenuOptions)
                        {
                            DlkBaseControl optionControl = new DlkBaseControl("Option", option);
                            string currentValue = optionControl.GetValue().Trim();
                            actualValue = string.Concat(actualValue, currentValue, "|");
                            if (currentValue.Contains(Value))
                            {
                                actualValue = optionControl.GetValue().Trim();
                                break;
                            }
                        }
                    }

                    if (actualValue.Equals(Value))
                    {
                        actualResult = true;
                    }

                    DlkAssert.AssertEqual("Action Column", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyTableRowActionColumn( ) was successfully executed.");
                }
                else
                {
                    DlkAssert.AssertEqual("Action Column", expectedResult, false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowActionColumn( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyRowExists")]
        public void VerifyRowExists(string ColumnHeader, string ColumnSearchValue, string TrueOrFalse = "true")
        {
            try
            {
                initialize();

                string[] columnHeaders = ColumnHeader.Split('~');
                string[] columnValues = ColumnSearchValue.Split('~');

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    verifyRowExists(columnHeaders[i], columnValues[i], TrueOrFalse);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRowExists( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyRowCount")]
        public void VerifyRowCount(string ExpectedResult)
        {
            try
            {
                initialize();
                int expectedResult = int.MinValue;
                if (Int32.TryParse(ExpectedResult, out expectedResult))
                {
                    IList<IWebElement> tableRows = getPageCurrentRows();
                    DlkAssert.AssertEqual("Verify Row Count : ", expectedResult, tableRows.Count);
                    DlkLogger.LogInfo("VerifyRowCount successfully executed.");
                }
                else
                {
                    throw new Exception("Expected Result : " + ExpectedResult + " not recognized.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRowCount( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyColumnHeaderExists")]
        public void VerifyColumnHeaderExists(string ColumnName, string TrueOrFalse)
        {
            try
            {
                initialize();
                string[] columnNames = ColumnName.Split('~');

                foreach (string columnName in columnNames)
                {
                    int columnHeaderIndex = getColumnIndexByHeader(columnName);
                    bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                    bool actualResult = false;
                    if (columnHeaderIndex != COLUMN_HEADER_NOT_FOUND)
                    {
                        actualResult = true;
                    }

                    DlkAssert.AssertEqual("VerifyColumnHeaderExists()", expectedResult, actualResult);
                }

                DlkLogger.LogInfo("VerifyColumnHeaderExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnHeaderExists( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyColumnHeaderValue")]
        public void VerifyColumnHeaderValue(string ColumnNumber, string ExpectedValue)
        {
            try
            {
                initialize();
                int columnHeaderIndex = int.MinValue;
                List<string> columnHeaderValues = getColumnHeaderValues();

                if (Int32.TryParse(ColumnNumber, out columnHeaderIndex) &&
                    --columnHeaderIndex <= columnHeaderValues.Count)
                {
                    string actualResult = columnHeaderValues[columnHeaderIndex].Trim();
                    DlkAssert.AssertEqual("Verify Column Header Value : ", ExpectedValue, actualResult);
                    DlkLogger.LogInfo("VerifyColumnHeaderValue successfully executed.");
                }
                else
                {
                    throw new Exception("Column Number : " + ColumnNumber + " not recognized.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnHeaderValue( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyColumnSortAscending")]
        public void VerifyColumnSortAscending(string Column, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                verifySorting(Column, "sorted_asc", expectedResult);
                DlkLogger.LogInfo("VerifyColumnSortAscending successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnSortAscending( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifySortByNewLineIndex")]
        public void VerifySortByNewLineIndex(string NewLineIndex, string SplitSpace, string SortOrder, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool splitSpace = Convert.ToBoolean(SplitSpace);
                int newLineIndex = Convert.ToInt32(NewLineIndex);
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                List<IWebElement> rows = mElement.FindElements(By.XPath("./tbody//tr/td/..")).ToList();
                List<string> actualRows = new List<string>();
                List<string> sortedRows = new List<string>();

                foreach (IWebElement element in rows)
                {
                    string[] text = element.Text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    string val = string.Empty;

                    if (splitSpace)
                    {
                        text = text[newLineIndex - 1].Split(' ');
                        val = text[0];
                    }
                    else
                    {
                        val = text[newLineIndex - 1];
                    }

                    actualRows.Add(val);
                    sortedRows.Add(val);
                }


                var sorted = sortedRows.OrderByDescending(item => item);

                if (SortOrder.ToUpper().Contains("ASC"))
                {

                    sorted = sortedRows.OrderBy(item => item);

                }


                if (actualRows.SequenceEqual(sorted))
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("Verify Column Sort  : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyColumnSortAscending successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnSortAscending( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifySortOrderFromList")]
        public void VerifySortOrderFromList()
        {
            try
            {
                initialize();
                IList<IWebElement> row = mElement.FindElements(By.XPath("./tbody//tr"));
                IList<IWebElement> sortOrderElement = mElement.FindElements(By.XPath("//table[contains(@id,'Internal')]/./preceding::div[contains(@class , 'datatable_sortbox')]//ul/li"));
                var sortOrder = sortOrderElement.Select(item => 
                {
                    Dictionary<string, string> orderBy = new Dictionary<string, string>();
                    //get column 
                    string itemSortColumn = item.Text;
                    //get sort order
                    IWebElement itemSortOrder = item.FindElement(By.XPath(".//img[@alt='Sort']"));
                    //checking seems wrong but it was checking the image source preemtively
                    orderBy.Add(itemSortColumn, itemSortOrder.GetAttribute("src").Contains("asc") ? "descending" : "ascending");

                    return orderBy;
                });

                var columns = new Dictionary<string, string> ();
                foreach (var order in sortOrder)
                {
                    foreach (var columnName in order.Keys)
                    {
                        columns.Add(columnName, ((Dictionary<string, string>)order)[columnName]);
                    }
                }

                List<dynamic> tableRows = new List<dynamic>();
                foreach (IWebElement item in row)
                {
                    var itemRow = new ExpandoObject() as IDictionary<string, Object>;
                    foreach (var column in columns)
                    {
                        int columnHeaderIndex = getColumnIndexByHeader(column.Key.ToUpper());
                        IWebElement rowColumnElement = item.FindElement(By.XPath("./td[" + columnHeaderIndex + "]"));
                        itemRow.Add(column.Key, rowColumnElement.Text);
                    }
                    tableRows.Add((dynamic)itemRow);
                }

                //first sort order will be sorted by order by.
                int counter = 0;
                IOrderedEnumerable<dynamic> sortedList = null;
                foreach (KeyValuePair<string,string> order in columns)
                {
                    if (counter == 0)
                    {

                        if (order.Value == "ascending")
                        {
                            sortedList = tableRows.OrderBy(x => ((IDictionary<string, object>)x)[order.Key]);
                        }
                        else
                        {
                            sortedList = tableRows.OrderByDescending(x => ((IDictionary<string, object>)x)[order.Key]);
                        }
                        counter++;
                        continue;
                    }

                    if (order.Value == "ascending")
                    {
                        sortedList = sortedList.ThenBy(x => ((IDictionary<string, object>)x)[order.Key]);
                    }
                    else
                    {
                        sortedList = sortedList.ThenByDescending(x => ((IDictionary<string, object>)x)[order.Key]);
                    }
                }
               
                if (!tableRows.SequenceEqual(sortedList))
                {
                    throw new Exception("Sorted list not equal");
                }

                DlkLogger.LogInfo("VerifySortOrderFromList successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySortOrderFromList( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }
        
        [Keyword("VerifyColumnSortDescending")]
        public void VerifyColumnSortDescending(string Column, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                verifySorting(Column, "sorted_desc", expectedResult);
                DlkLogger.LogInfo("VerifyColumnSortDescending successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnSortDescending( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }
        
        [Keyword("PerformRowActionByColumn")]
        public void PerformRowActionByColumn(string Row, string ColumnNumber, string MenuAction)
        {
            try
            {
                initialize();
                int columnIndex = Convert.ToInt32(ColumnNumber) - 1;
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> actionElementList = getActionList(tableRow);
                if (columnIndex < actionElementList.Count)
                {
                    IWebElement selectedAction = actionElementList[columnIndex];
                    processAction(selectedAction, selectedAction.Text, MenuAction);
                    DlkLogger.LogInfo("PerformRowActionByColumn( ) was successfully executed.");
                }
                else
                {
                    throw new Exception("Action column " + ColumnNumber + " doesnt exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PerformRowActionByColumn( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [RetryKeyword("RetryPerformRowActionByColumnIfValidationFails")]
        public void RetryPerformRowActionByColumnIfValidationFails(string Row, string ColumnNumber, string MenuAction, string WizardStepOrHeaderName)
        {
            try
            {
                this.PerformAction(() =>
                {
                    this.PerformRowActionByColumn(Row, ColumnNumber, MenuAction);
                    string actualResult = DlkCommon.DlkCommonFunction.GetWizardStepOrHeader();
                    DlkAssert.AssertEqual("RetryPerformRowActionByColumnIfValidationFails", WizardStepOrHeaderName, actualResult);
                }, new String[] { "retry" });
            }
            catch(Exception ex)
            {
                throw new Exception("RetryPerformRowActionByColumnIfValidationFails() execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("PerformRowActionByTitle")]
        public void PerformRowActionByTitle(string Title, string Row, string MenuAction)
        {
            try
            {
                initialize();
                bool bFoundActionElement = false;
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> actionElementList = getActionList(tableRow);

                foreach (IWebElement actionElement in actionElementList)
                {
                    bool isDataOrigSameTitle = IsAttributeExists("data-original-title", actionElement) && DlkString.RemoveCarriageReturn(actionElement.GetAttribute("data-original-title")).Equals(Title);
                    bool isDataToggleSameTitle = this.IsAttributeExists("data-toggle", actionElement) && DlkString.RemoveCarriageReturn(actionElement.GetAttribute("data-toggle")).Equals(Title);

                    if (isDataOrigSameTitle || isDataToggleSameTitle)
                    {
                        bFoundActionElement = true;
                        processAction(actionElement, Title, MenuAction);
                        DlkLogger.LogInfo("PerformRowActionByTitle( ) was successfully executed.");
                        break;
                    }
                }

                if (!bFoundActionElement)
                {
                    throw new Exception (string.Concat(Title, " is not available or not found."));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PerformRowActionByTitle( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("ClickColumnHeader")]
        public void ClickColumnHeader(string Column)
        {
            try
            {
                initialize();
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                IWebElement columnHeader = mElement.FindElement(By.XPath(@".//th[" + columnIndex + "]//a | .//th[" + columnIndex + "]//input"));
                DlkBaseControl columnHeaderControl = new DlkBaseControl("Column : " + Column, columnHeader);
                columnHeaderControl.ScrollIntoViewUsingJavaScript();
                columnHeaderControl.Click();
                DlkLogger.LogInfo("ClickColumnHeader() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("ClickColumnHeader() execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SortColumn")]
        public void SortColumn(string ColumnHeader, string SortOrder)
        {
            try
            {
                initialize();
                int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                if (columnHeaderIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement columnHeader = mElement.FindElement(By.XPath(@".//th[" + columnHeaderIndex + "]//a"));
                    DlkLink headerLink = new DlkLink("Sort " + ColumnHeader, columnHeader);
                    string columnHeaderClassAttr = DlkString.RemoveCarriageReturn(columnHeader.GetAttribute("class").Trim());

                    if (columnHeaderClassAttr.Equals("sortable"))
                    {
                        IList<IWebElement> pagination = mElement.FindElements(By.XPath("./../preceding-sibling::div//ul[contains(@class,'pagination')]"));
                        if (pagination.Count > 0)
                            DlkCommon.DlkCommonFunction.ScrollIntoView(pagination[pagination.Count - 1]);
                        headerLink.Click(1.5);
                        //Reintialize header and link after click.

                        if (IsFromIFrame)
                        {
                            DlkEnvironment.mSwitchediFrame = false;
                        }

                        initialize();
                        if (pagination.Count > 0)
                        {
                            pagination = mElement.FindElements(By.XPath("./../preceding-sibling::div//ul[contains(@class,'pagination')]"));
                            DlkCommon.DlkCommonFunction.ScrollIntoView(pagination[pagination.Count - 1]);
                        }
                        columnHeader = mElement.FindElement(By.XPath(@".//th[" + columnHeaderIndex + "]//a"));
                        headerLink = new DlkLink("Sort " + ColumnHeader, columnHeader);
                        columnHeaderClassAttr = DlkString.RemoveCarriageReturn(columnHeader.GetAttribute("class").Trim());
                    }

                    if (columnHeaderClassAttr.Equals("sorted_desc") &&
                        SortOrder.ToLower().Equals("ascending"))
                    {
                        headerLink.Click(1.5);
                    }
                    else if (columnHeaderClassAttr.Equals("sorted_asc") &&
                             SortOrder.ToLower().Equals("descending"))
                    {
                        headerLink.Click(1.5);
                    }
                }
                else
                {
                    throw new Exception("Column Header Not Found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SortColumn( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("ClickAllRowColumnByIndex")]
        public void ClickAllRowColumnByIndex(String Column, String Index)
        {
            try
            {
                initialize();
                int index = Convert.ToInt32(Index);
                IList<IWebElement> rowList = mElement.FindElements(By.XPath(".//tbody/tr"));
                for (int i = 0; i < rowList.Count; i++)
                {
                    string row = Convert.ToString(i + 1);
                    clickTableCellByIndex(row, Column, index);
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickAllRowColumnByIndex( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("ClickTableCell")]
        public void ClickTableCell(String Row, String Column)
        {
            try
            {
                initialize();
                clickTableCellByIndex(Row, Column, 1);
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCell( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("ClickTableCellByIndex")]
        public void ClickTableCellByIndex(String Row, String Column, string Index)
        {
            try
            {
                initialize();
                clickTableCellByIndex(Row, Column, int.Parse(Index));
            }
            catch (Exception e)
            { 
                throw new Exception("ClickTableCell( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("ClickTableCellByTitle")]
        public void ClickTableCellByTitle(string Row, string Column, string Title)
        {
            try
            {
                initialize();
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    bool hasExpectedElement = false;
                    IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]/descendant::*[contains(text(),'" + Title + "') or contains(@title,'" + Title + "') or contains(@data-original-title,'" + Title + "')]"));
                    foreach (IWebElement element in rowColumnElementList)
                    {
                        switch (element.TagName.Trim())
                        {
                            case "button" :
                            case "a" :
                            case "span" :
                                {
                                    hasExpectedElement = true;
                                    DlkButton buttonControl = new DlkButton("Button_Element : " + element.Text, element);
                                    buttonControl.Click();
                                    break;
                                }
                        }

                        if (hasExpectedElement)
                        {
                            DlkLogger.LogInfo("ClickTableCellByTitle( ) successfully executed.");
                            break;
                        }
                    }

                    if (!hasExpectedElement)
                    {
                        throw new Exception("ClickTableCellByTitle( ) execution failed. Expected element to Click( ) not found.");
                    }
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("ClickTableCellByTitle( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellComboBoxValueByIndex")]
        public void VerifyCellComboBoxValueByIndex(String Row, String Column, string Text, string TrueOrFalse, string Index)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement rowColumnCheckBoxElement = tableRow.FindElement(By.XPath(@"(./td[" + columnIndex + "]//select)[" + Index + "]"));

                    SelectElement comboBoxElement = new SelectElement(rowColumnCheckBoxElement);
                    string actualText = comboBoxElement.SelectedOption.Text.Trim().Replace("\r\n", string.Empty);
                    if (actualText.Equals(Text))
                    {
                        actualResult = true;
                    }
                    DlkAssert.AssertEqual("VerifyCellComboBoxValue() : ", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyCellComboBoxValue() successfully executed.");
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellComboBoxValue( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }
        [Keyword("VerifyCellCheckBoxValue")]
        public void VerifyCellCheckBoxValue(String Row, String Column, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement rowColumnCheckBoxElement = tableRow.FindElement(By.XPath(@"./td[" + columnIndex + "]//input[@type='checkbox']"));
                    bool actualResult = rowColumnCheckBoxElement.Selected;
                    DlkAssert.AssertEqual("VerifyCellCheckBoxValue() : ", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyCellCheckBoxValue() successfully executed.");
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellCheckBoxValue( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellTextBoxValue")]
        public void VerifyCellTextBoxValue(String Row, String Column, string TextValue, string TrueOrFalse)
        {
            try
            {
                initialize();

                string[] columnHeaders = Column.Split('~');
                string[] columnValues = TextValue.Split('~');

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                    int columnIndex = COLUMN_HEADER_NOT_FOUND;
                    IWebElement tableRow = getTableRow(Row);

                    if (!Int32.TryParse(columnHeaders[i], out columnIndex))
                    {
                        columnIndex = getColumnIndexByHeader(columnHeaders[i]);
                    }

                    if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                    {
                        bool actualResult = false;
                        IWebElement cellTextBox = tableRow.FindElement(By.XPath(@"./td[" + columnIndex + "]//input[@type='text']"));
                        string actualText = new DlkBaseControl("textBox : ", cellTextBox).GetValue();
                        if (columnValues[i].Equals(actualText))
                        {
                            actualResult = true;
                        }

                        DlkLogger.LogInfo("Expected Text : " + columnValues[i] + "\nActual Text : " + actualText);
                        DlkAssert.AssertEqual("VerifyCellTextBoxValue() : ", expectedResult, actualResult);
                        DlkLogger.LogInfo("VerifyCellTextBoxValue() successfully executed.");
                    }
                    else
                    {
                        throw new Exception("Column '" + Column + "' not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellTextBoxValue( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellRadioButtonValue")]
        public void VerifyCellRadioButtonValue(String Row, String Column, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement rowColumnCheckBoxElement = tableRow.FindElement(By.XPath(@"./td[" + columnIndex + "]//input[@type='radio']"));
                    bool actualResult = rowColumnCheckBoxElement.Selected;
                    DlkAssert.AssertEqual("VerifyCellRadioButtonValue() : ", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyCellRadioButtonValue() successfully executed.");
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellRadioButtonValue( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellRadioButtonValueByIndex")]
        public void VerifyCellRadioButtonValueByIndex(String Row, String Column, string TrueOrFalse, string Index)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                int index = Convert.ToInt32(Index);
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement rowColumnCheckBoxElement = tableRow.FindElement(By.XPath(@"(./td[" + columnIndex + "]//input[@type='radio'])[" + index + "]"));
                    bool actualResult = rowColumnCheckBoxElement.Selected;
                    DlkAssert.AssertEqual("VerifyCellRadioButtonValueByIndex() : ", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyCellRadioButtonValueByIndex() successfully executed.");
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellRadioButtonValueByIndex( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SetTableCellValue")]
        public void SetTableCellValue(string Row, string Column, string Value)
        {
            try
            {
                string[] columnHeaders = Column.Split('~');
                string[] columnValues = Value.Split('~');

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    setTableCell(Row, columnHeaders[i], columnValues[i], 1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetTableCellValue( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SetTableCellByIndex")]
        public void SetTableCellByIndex(string Row, string Column, string Value, string Index)
        {
            try
            {
                int index = Int32.Parse(Index);
                setTableCell(Row, Column, Value, index);
            }
            catch (Exception ex)
            {
                throw new Exception("SetTableCellByIndex( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SelectTableCellValue")]
        public void SelectTableCellValue(string Row, string Column, string Value)
        {
            try
            {
                selectTableCell(Row, Column, Value);
            }
            catch (Exception ex)
            {
                throw new Exception("SelectTableCellValue( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyRowActionColumnToolTip")]
        public void VerifyRowActionColumnToolTip(string Row, string ColumnNumber, string ExpectedResult)
        {
            try
            {
                initialize();
                int columnIndex = Convert.ToInt32(ColumnNumber) - 1;
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> actionElementList = getActionList(tableRow, true);

                if (columnIndex < actionElementList.Count)
                {
                    IWebElement selectedAction = actionElementList[columnIndex];
                    string dataOriginalTitleAttr = selectedAction.GetAttribute("data-original-title") == null ? selectedAction.GetAttribute("title") : selectedAction.GetAttribute("data-original-title");
                    //check for underlying element.
                    if (string.IsNullOrEmpty(dataOriginalTitleAttr))
                    {
                        dataOriginalTitleAttr = selectedAction.FindElements(By.XPath(@"./i")).Count > 0 ? selectedAction.FindElement(By.XPath(@"./i")).GetAttribute("data-original-title") : string.Empty;
                    }
                    string actualResult = string.IsNullOrEmpty(dataOriginalTitleAttr) ? string.Empty : dataOriginalTitleAttr;
                    DlkAssert.AssertEqual("Verify Action ToolTip : ", ExpectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyRowActionColumnToolTip( ) was successfully executed.");
                }
                else
                {
                    throw new Exception("Action column " + ColumnNumber + " doesnt exist.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyRowActionColumnToolTip. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellLinkPopover")]
        public void VerifyCellLinkPopover(string Row, string ColumnHeader, string LinkIndex, string ExpectedResult)
        {
            try
            {
                initialize();
                int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                IWebElement rowElement = getTableRow(Row);
                IWebElement rowColumnElement = rowElement.FindElement(By.XPath("./td[" + columnHeaderIndex + "]"));
                List<IWebElement> linkList = rowColumnElement.FindElements(By.XPath(@".//a")).Where(item => item.Displayed).ToList();

                IWebElement link = linkList[Convert.ToInt32(LinkIndex) - 1];
                DlkLink linkControl = new DlkLink("Link_Element", link);
                linkControl.Click();

                IWebElement popOverElement = mElement.FindElement(By.XPath(".//a[contains(@aria-describedby,'popover')]"));
                string actualResult = popOverElement.GetAttribute("data-bs-content");
                actualResult = actualResult.Replace("<br />", "~");

                DlkAssert.AssertEqual("VerifyCellLinkPopover(): ", ExpectedResult, actualResult);
                DlkLogger.LogInfo("VerifyCellLinkPopover() was successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCellLinkPopover(): " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellIcon")]
        public void VerifyCellIcon(string Row, string ColumnHeader, string ExpectedIcon)
        {
            try
            {
                // ExpectedIcon must be set to following:
                // "thumbs up", "x", "check", "like", "liked"
                initialize();
                bool equalResult = false;
                int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                IWebElement rowElement = getTableRow(Row);
                IWebElement rowColumnElement = rowElement.FindElement(By.XPath("./td[" + columnHeaderIndex + "]"));
                List<IWebElement> iconList = rowColumnElement.FindElements(By.XPath(@".//i | .//span")).Where(item => item.Displayed).ToList();

                foreach (IWebElement icon in iconList)
                {
                    string classIcon = icon.GetAttribute("class");
                    string actualIcon = "";

                    switch (classIcon)
                    {
                        case "fa fa-thumbs-up":
                            actualIcon = "thumbs up";
                            break;
                        case "fa fa-times":
                            actualIcon = "x";
                            break;
                        case "fa fa-check":
                            actualIcon = "check";
                            break;
                        case "like far fa-thumbs-up":
                            actualIcon = "like";
                            break;
                        case "unlike fa fas fa-thumbs-up":
                            actualIcon = "liked";
                            break;
                        default:
                            break;
                    }

                    if (actualIcon.Equals(ExpectedIcon.ToLower().Trim()))
                    {
                        equalResult = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyCellIcon(): ", true, equalResult);
                DlkLogger.LogInfo("VerifyCellIcon() was successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCellIcon(): " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellToolTip")]
        public void VerifyCellToolTip(string Row, string ColumnHeader, string ExpectedResult)
        {
            try
            {
                initialize();
                int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                string actualResult = string.Empty;
                IWebElement rowElement = getTableRow(Row);
                IWebElement rowColumnElement = rowElement.FindElement(By.XPath("./td[" + columnHeaderIndex + "]"));
                List<IWebElement> elementList = rowColumnElement.FindElements(By.XPath(@".//*")).Where(item => item.Displayed).ToList();

                if (elementList.Count > 0)
                {
                    foreach (IWebElement element in elementList)
                    {
                        string dataOriginalTitleAttr = string.Empty;
                        string elementClassAttr = element.GetAttribute("class");
                        if (element.TagName.Equals("i") && element.GetAttribute("class") != null &&
                            (elementClassAttr.Contains("popover-toggle") || elementClassAttr.Contains("fa fa") || elementClassAttr.Contains("huaicon")))
                        {
                            dataOriginalTitleAttr = element.GetAttribute("data-content") != null ? element.GetAttribute("data-content") : string.Empty;
                            if (string.IsNullOrEmpty(dataOriginalTitleAttr))
                            {
                                dataOriginalTitleAttr = element.GetAttribute("data-title") != null ? element.GetAttribute("data-title") : string.Empty;
                            }
                        }


                        if (string.IsNullOrEmpty(dataOriginalTitleAttr))
                        {
                            dataOriginalTitleAttr = element.GetAttribute("data-original-title") == null ? element.GetAttribute("title") : element.GetAttribute("data-original-title");
                        }
                        //check for underlying element.
                        if (string.IsNullOrEmpty(dataOriginalTitleAttr))
                        {
                            IList<IWebElement> images = element.FindElements(By.XPath(@"./i"));
                            dataOriginalTitleAttr =  images.Count > 0 ? element.FindElement(By.XPath(@"./i")).GetAttribute("data-original-title") : string.Empty;
                            if (dataOriginalTitleAttr == null &&
                                images.Count > 0)
                            {
                                dataOriginalTitleAttr = images.Count > 0 ? element.FindElement(By.XPath(@"./i")).GetAttribute("title") : string.Empty;
                            }
                        }

                        if (string.IsNullOrEmpty(dataOriginalTitleAttr))
                        {
                            continue;
                        }

                        actualResult = DlkCommon.DlkCommonFunction.StripHTMLTags(string.IsNullOrEmpty(dataOriginalTitleAttr) ? string.Empty : dataOriginalTitleAttr, true);

                        if (ExpectedResult.Equals(actualResult.Trim(new char[] { '\n' })))
                        {
                            break;
                        }
                        else
                        {
                            DlkLogger.LogInfo("ToolTip : " + actualResult);
                        }
                    }


                    DlkAssert.AssertEqual("Verify Cell ToolTip : ", ExpectedResult, actualResult.Trim(new char[] { '\n' }));
                    DlkLogger.LogInfo("VerifyCellToolTip( ) was successfully executed.");
                }
                else
                {
                    throw new Exception("VerifyCellToolTip() element with tooltip not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCellToolTip. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyRowActionColumnState")]
        public void VerifyRowActionColumnState(string Row, string ColumnNumber, string TrueOrFalse)
        {
            try
            {
                initialize();
                int columnIndex = 0;
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> actionElementList = getActionList(tableRow, true);
                string[] columns = ColumnNumber.Split('~');

                for (int i = 0; i < columns.Length; i++)
                {
                    columnIndex = Convert.ToInt16(columns[i]) - 1;
                    if (columnIndex < actionElementList.Count)
                    {
                        IWebElement selectedAction = actionElementList[columnIndex];
                        string classAttr = selectedAction.GetAttribute("class");
                        bool actualResult;
                        if (string.IsNullOrEmpty(classAttr) ||
                            !classAttr.Contains("disabled"))
                        {
                            actualResult = true;
                        }
                        else
                        {
                            actualResult = false;
                        }

                        DlkAssert.AssertEqual("Verify Action ToolTip : ", Convert.ToBoolean(TrueOrFalse.ToLower()), actualResult);
                        DlkLogger.LogInfo("VerifyRowActionColumnState( ) was successfully executed.");
                    }
                    else
                    {
                        throw new Exception("Action column " + ColumnNumber + " doesnt exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRowActionColumnState( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyClickableCellItemByIndex")]
        public void VerifyClickableCellItemByIndex(string Row, string Column, string TrueOrFalse, string Index)
        {
            try
            {
                initialize();
                int index = Int32.Parse(Index) - 1;
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);

                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement cellItem = tableRow.FindElement(By.XPath(@"./td[" + columnIndex + "]"));
                    List<IWebElement> clickableItems = cellItem.FindElements(By.XPath(@".//a | .//i[not(parent::a)]")).
                        Where(item => item.Displayed).ToList();

                    for (int i = 0; i < clickableItems.Count; i++)
                    {
                        if (index == i)
                        {
                            actualResult = true;
                            break;
                        }
                    }
                }

                DlkAssert.AssertEqual("VerifyClickableCellItemByIndex : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyClickableCellItemByIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyClickableCellItemByIndex( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellStateByIndex")]
        public void VerifyCellStateByIndex(String Row, String Column, string TrueOrFalse, string Index)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IList<IWebElement> cellControls = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*"));
                    if (cellControls.Count > 0)
                    {
                        int indexCounter = 0;
                        foreach(IWebElement element in cellControls)
                        {
                            if (element.TagName.Equals("input") &&
                                element.Displayed)
                            {
                                indexCounter++;
                                if (indexCounter.Equals(Convert.ToInt16(Index)) &&
                                    element.Enabled)
                                {
                                    actualResult = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        actualResult = false;
                    }

                    DlkAssert.AssertEqual("VerifyCellStateByIndex() : ", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyCellStateByIndex() successfully executed.");
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellStateByIndex( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyCellMarkedText")]
        public void VerifyCellMarkedText(string Row, string Column, string Text, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != COLUMN_HEADER_NOT_FOUND)
                {
                    IWebElement cellElement = tableRow.FindElement(By.XPath(@"./td[" + columnIndex + "]"));
                    IList<IWebElement> markedItem = DlkCommon.DlkCommonFunction.GetElementWithText(Text, cellElement, elementTag: "mark");
                    if (markedItem.Count > 0)
                    {
                        actualResult = true;
                    }

                    DlkAssert.AssertEqual("VerifyCellMarkedText() : ", expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyCellMarkedText() successfully executed.");
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellMarkedText( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("PerformRowMenuAction")]
        public void PerformRowMenuAction(string Row, string MenuAction)
        {
            try
            {
                initialize();
                IWebElement tableRow = getTableRow(Row);
                IList<IWebElement> menuRibbonElement = tableRow.FindElements(By.XPath(".//a[@data-toggle='dropdown']"));
                if (menuRibbonElement.Count > 0)
                {
                    processAction(menuRibbonElement[0], menuRibbonElement[0].Text, MenuAction);
                }
                else
                {
                    throw new Exception("Ribbon menu not found.");
                }

                DlkLogger.LogInfo("PerformRowMenuAction() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("PerformRowMenuAction( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("DragAndDropByRow")]
        public void DragAndDropByRow(string FromRow, string ToRow)
        {
            try
            {
                initialize();
                IList<IWebElement> tableRows = getPageCurrentRows();
                int totalTableRows = tableRows.Count;
                string FromRowInNumberFormat = GetNumbers(FromRow);
                string ToRowInNumberFormat = GetNumbers(ToRow);

                // moving row to a higher row number
                if (Convert.ToInt16(ToRowInNumberFormat) > Convert.ToInt16(FromRowInNumberFormat))
                {
                    bool dropToLastRow = false;
                    int toRowIndex = Convert.ToInt16(ToRowInNumberFormat) + 1;

                    if (Convert.ToInt16(ToRowInNumberFormat) == tableRows.Count)
                    {
                        toRowIndex = Convert.ToInt16(ToRowInNumberFormat);
                        dropToLastRow = true;
                    }

                    IWebElement fromRow = getTableRow(FromRowInNumberFormat);
                    IWebElement toRow = getTableRow(toRowIndex.ToString());
                    IWebElement tbody = mElement.FindElement(By.XPath(".//tbody"));

                    if (string.IsNullOrEmpty(tbody.GetAttribute("id")))
                    {
                        fromRow = fromRow.FindElement(By.XPath(".//a[contains(@class,'sortable-handler')]"));
                        toRow = toRow.FindElement(By.XPath(".//a[contains(@class,'sortable-handler')]"));
                    }

                    if (dropToLastRow)
                    {
                        DlkCommon.DlkCommonFunction.DragAndDrop(fromRow, toRow, 0, 10);
                    }
                    else
                    {
                        DlkCommon.DlkCommonFunction.DragAndDrop(fromRow, toRow, 0, -5);
                    }
                }
                else // moving row to a lower row number
                {
                    bool dropToFirstRow = false;
                    int toRowIndex = Convert.ToInt16(ToRowInNumberFormat) - 1;

                    if (Convert.ToInt16(ToRowInNumberFormat) == 1)
                    {
                        toRowIndex = Convert.ToInt16(ToRowInNumberFormat);
                        dropToFirstRow = true;
                    }

                    IWebElement fromRow = getTableRow(FromRowInNumberFormat);
                    IWebElement toRow = getTableRow(toRowIndex.ToString());
                    IWebElement tbody = mElement.FindElement(By.XPath(".//tbody"));

                    if (string.IsNullOrEmpty(tbody.GetAttribute("id")))
                    {
                        fromRow = fromRow.FindElement(By.XPath(".//a[contains(@class,'sortable-handler')]"));
                        toRow = toRow.FindElement(By.XPath(".//a[contains(@class,'sortable-handler')]"));
                    }

                    if (dropToFirstRow)
                    {
                        DlkCommon.DlkCommonFunction.DragAndDrop(fromRow, toRow, 0, -10);
                    }
                    else
                    {
                        DlkCommon.DlkCommonFunction.DragAndDrop(fromRow, toRow, 0, 5);
                    }
                }

                DlkLogger.LogInfo("DragAndDropByRow() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("DragAndDropByRow( ) execution failed : " + e.Message, e);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyHighlightedText")]
        public void VerifyHighlightedText(string Text)
        {
            try
            {
                initialize();
                List<IWebElement> highlightedCells = mElement.FindElements(By.XPath(".//td[@class='calendar_cell_selected']")).ToList();
                bool highlighted = highlightedCells.Any(highlightedCell => highlightedCell.Text.Trim() == Text) ? true : false;

                DlkAssert.AssertEqual("VerifyHighlightedText", true, highlighted);
                DlkLogger.LogInfo("VerifyHighlightedText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHighlightedText() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();

            if (IsFromIFrame)
            {
                DlkEnvironment.mSwitchediFrame = true;
            }
        }

        /// <summary>
        /// Gets table row by row number or row variable.
        /// </summary>
        /// <param name="row">Row Number or Row Variable</param>
        /// <returns>Table Row Element</returns>
        private IWebElement getTableRow(string row)
        {
            int rowNumber = int.MinValue;

            if (int.TryParse(row, out rowNumber))
            {
                return getTableRowByRowNumber(rowNumber - 1);
            }
            else
            {
                return getTableRowByVariable(row);
            }
        }

        /// <summary>
        /// Will get exisiting column header under table element.
        /// </summary>
        /// <param name="sColumnHeader">Search Key</param>
        /// <returns>Index of the Column Header</returns>
        private int getColumnIndexByHeader(string columnHeader)
        {
            List<string> columnHeaders = getColumnHeaderValues();

            for (int i = 0; i < columnHeaders.Count; i++ )
            {
                string header = columnHeaders[i];
                if (columnHeader.Equals(header.Trim()))
                {
                    return i + 1;
                }
            }

            return COLUMN_HEADER_NOT_FOUND;
        }

        /// <summary>
        /// This method will get the list of values of existing column headers under table element.
        /// </summary>
        /// <returns>List of Column Header Values.</returns>
        private List<string> getColumnHeaderValues()
        {
            if (IsFromIFrame)
            {
                DlkEnvironment.mSwitchediFrame = false;
            }

            initialize();

            //find separately for each header locator.
            List<string> headersLocator = new List<string>()
            {
                @".//thead/tr/th | .//thead[@class='header']/tr/th[@scope='col'] | .//thead/tr[@class='thclass']/th[@scope='col'] | .//thead/tr/th[@scope='col']",
                @"(.//tbody/tr[@class='thclass'])[1]/td"
            };

            IList<IWebElement> columnHeaderElements = null;
            for (int i = 0; i < headersLocator.Count; i++)
            {
                columnHeaderElements = mElement.FindElements(By.XPath(headersLocator[i]));
                if (columnHeaderElements.Count > 0)
                {
                    break;
                }
            }

            List<string> columnHeaders = new List<string>();

            foreach (IWebElement columnHeader in columnHeaderElements)
            {
                DlkBaseControl header = new DlkBaseControl("Header", columnHeader);
                string headerText = header.GetValue();
                columnHeaders.Add(DlkString.RemoveCarriageReturn(headerText));
            }

            return columnHeaders;
        }

        private IWebElement getTableRowByRowNumber(int rowNumber)
        {
            IWebElement tableRow = null;

            List<IWebElement> tableRows = getPageCurrentRows();
            if (rowNumber < tableRows.Count)
            {
                tableRow = tableRows[rowNumber];
            }
            else
            {
                DlkLogger.LogInfo("No Rows Found.");
            }

            return tableRow;
        }

        private IWebElement getTableRowByVariable(string rowVariable)
        {
            string[] rowDetailsArray = rowVariable.Split('-');
            string pageNumber = rowDetailsArray[0];
            string numberOfItemsOnPage = rowDetailsArray[1];
            string rowNumber = rowDetailsArray[2];
            int row = Convert.ToInt32(rowNumber) - 1;
            IWebElement paginationElement = findPaginationControl();

            if (paginationElement != null)
            {
                DlkPagination pagination = new DlkPagination("Pagination", paginationElement);
                if (numberOfItemsOnPage != string.Empty &&
                    !pagination.IsCurrentDisplayPerPageEqual(numberOfItemsOnPage))
                {
                    pagination.SelectByText(numberOfItemsOnPage);
                }

                if (pageNumber != string.Empty &&
                    !pagination.IsCurrentPageEqual(pageNumber))
                {
                    pagination.FirstPage();
                    while (!pagination.IsCurrentPageEqual(pageNumber))
                    {
                        pagination.Next();
                        initialize();
                        pagination = new DlkPagination("Pagination", findPaginationControl());
                    }
                }
            }

            return getCellDetails()[row];
        }

        private List<IWebElement> getPageCurrentRows()
        {
            List<IWebElement> filteredRowList = new List<IWebElement>();
            if (!IsFromIFrame)
            {
                initialize();
            }
            IList<IWebElement> rowList = mElement.FindElements(By.XPath(ROW));
            foreach (IWebElement element in rowList)
            {
                if ((element.GetAttribute("class") != null &&
                    element.GetAttribute("class").Trim().Contains("empty_grid_row")) ||
                    (element.GetAttribute("style") != null &&
                     element.GetAttribute("style").Trim().Contains("display: none;")))
                {
                    //we dont want to add to the list.
                }
                else
                {
                    filteredRowList.Add(element);
                }
            }

            return filteredRowList;
        }

        /// <summary>
        /// Will traverse through all the pages of the table to find the row.
        /// </summary>
        /// <param name="columnHeaderIndex"></param>
        /// <param name="columnSearchValue"></param>
        /// <returns></returns>
        private IWebElement getTableRowByHeader(int columnHeaderIndex, string columnSearchValue, out string rowNumber)
        {
            rowNumber = string.Empty;
            bool bRowFound = false;
            DlkPagination pagination = null;
            IWebElement tableRow = null;
            IWebElement paginationControl = findPaginationControl();
            
            //Start with the first page of the table.
            if (paginationControl != null)
            {
                pagination = new DlkPagination("Pagination", paginationControl);
                pagination.FirstPage();
            }

            do
            {
                Thread.Sleep(1500);
                DlkEnvironment.mSwitchediFrame = false;
                initialize();
                IList<IWebElement> tableRowsColumns = getCellDetails(columnHeaderIndex);

                for (int i = 0; i < tableRowsColumns.Count; i++)
                {
                    IWebElement rowColumn = tableRowsColumns[i];
                    DlkBaseControl tempTableRow = new DlkBaseControl("TableRow", rowColumn);
                    string tableRowColumnValue = tempTableRow.GetValue().Trim().Replace("\r\n","\n");

                    if (tableRowColumnValue.Equals(columnSearchValue))
                    {
                        int tableRowIndex = i + 1;
                        rowNumber = tableRowIndex.ToString();
                        //Row found.
                        bRowFound = true;
                        tableRow = tempTableRow.mElement;// mElement.FindElement(By.XPath(@".//tbody/tr[" + tableRowIndex + "]"));
                        break;
                    }
                }

                if (!bRowFound)
                {
                    //Reintilize page elements because of the pagination.
                    paginationControl = findPaginationControl();
                    if (paginationControl != null)
                    {
                        pagination = new DlkPagination("Pagination", paginationControl);
                    }
                }
            }
            while (!bRowFound &&
                   paginationControl != null &&
                   pagination.Next());

            if (!bRowFound)
            {
                DlkLogger.LogInfo("Row Not Found.");
            }

            return tableRow;
        }

        private IList<IWebElement> getCellDetails(int columnHeaderIndex = 0)
        {
            //return tr if columnHeaderIndex == 0
            IList<IWebElement> tableCellDetails = columnHeaderIndex == 0 ? 
                mElement.FindElements(By.XPath(@".//tbody/tr[not(contains(@style,'none')) and not(contains(@id,'org_row'))]/td/..")) : 
                mElement.FindElements(By.XPath(@".//tbody/tr[not(contains(@style,'none')) and not(contains(@id,'org_level'))]/td[" + columnHeaderIndex + "]"));
            if (tableCellDetails.Count > 0)
            {
                //Check if the cell is from Learning Search
                IWebElement cell = columnHeaderIndex == 0 ? 
                    tableCellDetails[0].FindElement(By.XPath("./td")) : 
                    tableCellDetails[0];
                string cellClassAttribute = cell.GetAttribute("class");

                if ((!string.IsNullOrEmpty(cellClassAttribute) &&
                   (cellClassAttribute.Contains("lms-course-container") ||
                    cellClassAttribute.Contains("lms-curriculum-container"))) ||
                    cell.FindElements(By.XPath("./div[@class='media']")).Count > 0)
                {
                    tableCellDetails = columnHeaderIndex == 0 ? 
                        mElement.FindElements(By.XPath(@".//tbody/tr[not(contains(@style,'none')) and not(contains(@id,'org_level'))]/td//h4[@class='media-heading']/parent::div/parent::div/parent::td/parent::tr")) :
                        mElement.FindElements(By.XPath(@".//tbody/tr[not(contains(@style,'none')) and not(contains(@id,'org_level'))]/td[" + columnHeaderIndex + "]//h4[@class='media-heading']"));

                    // check other option
                    if (tableCellDetails.Count == 0)
                    {
                        tableCellDetails = mElement.FindElements(By.XPath(@".//tbody/tr[not(contains(@style,'none')) and not(contains(@id,'org_level'))]/td[" + columnHeaderIndex + "]//div[@class='media-body']"));

                        // return tr if columnHeaderIndex == 0
                        if (columnHeaderIndex == 0)
                        {
                            tableCellDetails = mElement.FindElements(By.XPath(@".//tbody/tr"));
                        }
                    }
                }

                // check if cell retrieved is under Badge Management table
                if (cellClassAttribute == "badge-container-text")
                {
                    tableCellDetails = columnHeaderIndex == 0 ?
                        mElement.FindElements(By.XPath(@".//tbody/tr")) :
                        mElement.FindElements(By.XPath(@".//tbody/tr/td[" + columnHeaderIndex + "]//h1/div"));
                }
            }

            return tableCellDetails;
        }

        private IWebElement findPaginationControl()
        {
            IWebElement pagination = null;
            string paginationXpath = @"../preceding-sibling::div[1]//ul[contains(@class,'pagination')]";

            IList<IWebElement> paginationElement = mElement.FindElements(By.XPath(paginationXpath));
            if (paginationElement.Count > 0)
            {
                pagination = paginationElement[0];
            }
            else
            {
                DlkLogger.LogInfo("Pagination doesnt exist.");
            }

            return pagination;
        }

        private void processAction(IWebElement actionElement, string action, string menuAction)
        {
            DlkLink link = new DlkLink(string.Concat(action, " Action"), actionElement);
            if (link.mElement.TagName == "div")
            {
                if (DlkEnvironment.mBrowser == "Firefox")
                {
                    OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    actions.MoveToElement(actionElement).Click().Build().Perform();
                }
                else
                {
                    link.Click();
                }
            }
            else
            {
                link.Click();
            }

            if (DlkAlert.DoesAlertExist())
            {
                return;
            }

            if ((link.IsAttributeExists("aria-expanded") ||
                    (link.IsAttributeExists("data-toggle") &&
                     link.GetAttributeValue("data-toggle").Equals("dropdown")) ||
                     (link.IsAttributeExists("class") &&
                     link.GetAttributeValue("class").Contains("open"))) 
                     
                     ||

                     (link.IsAttributeExists("class") && link.mElement.TagName.Equals("i") && link.GetAttributeValue("class").Equals("icon-rowtools")))
            {
                handleDropDownAction(actionElement, menuAction);
            }
            //else if (DlkAlert.DoesAlertExist(2))
            //{
            //    handlePopUpAlert(popUpAction);
            //}
        }

        private void handleDropDownAction(IWebElement actionElement, string popUpAction)
        {
            // scroll slightly before getting all the dropdown menu options
            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            js.ExecuteScript("window.scrollBy(0,50)");

            List<IWebElement> dropDownMenuOptions = actionElement.FindElements(By.XPath(@"../ul[@role='menu' or contains(@class,'menu')]/li/a | 
                                                                                           //div[contains(@id,'report_actions')]/ul//li//a | 
                                                                                           ..//div[contains(@class,'menuItemText')]")).Where(item => item.Displayed).ToList();
            
            // if dropdown menu options is 0, click element again (special case of disappearing dropdown menus in Learning Search)
            if (dropDownMenuOptions.Count == 0)
            {
                actionElement.Click();

                dropDownMenuOptions = actionElement.FindElements(By.XPath(@"../ul[@role='menu' or contains(@class,'menu')]/li/a | 
                                                                                           //div[contains(@id,'report_actions')]/ul//li//a | 
                                                                                           ..//div[contains(@class,'menuItemText')]")).Where(item => item.Displayed).ToList();
            }

            foreach (IWebElement option in dropDownMenuOptions)
            {
                DlkBaseControl optionControl = new DlkBaseControl("Option", option);
                if (optionControl.GetValue().Trim().Contains(popUpAction))
                {
                    optionControl.mElement.Click();
                    break;
                }
            }
        }

        private void handlePopUpAlert(string popUpAction)
        {
            if (popUpAction.ToLower().Equals("accept"))
            {
                DlkAlert.Accept();
            }
            else if (popUpAction.ToLower().Equals("dismiss"))
            {
                DlkAlert.Dismiss();
            }
        }

        public bool IsAttributeExists(string attributeName, IWebElement element = null)
        {
            if (element == null)
            {
                if (mElement == null)
                {
                    FindElement();
                }
                element = mElement;
            }

            string attributeValue = element.GetAttribute(attributeName);

            if (attributeValue == null)
            {
                return false;
            }

            return true;
        }

        private void verifyTableRowWithColumnValue(string columnHeader, string columnValue, string row, bool partialMatch = false)
        {
            int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
            if (!Int32.TryParse(columnHeader, out columnHeaderIndex))
            {
                columnHeaderIndex = getColumnIndexByHeader(columnHeader);
            }

            IWebElement rowElement = getTableRow(row);
            IWebElement rowColumnElement = rowElement.FindElement(By.XPath("./td[" + columnHeaderIndex + "]"));
            DlkBaseControl rowColumnControl = new DlkBaseControl("Row_Column_Control", rowColumnElement);
            string actualResult = rowColumnControl.GetValue().Replace("\r\n", "\n");

            if (columnValue == string.Empty)
            {
                actualResult = rowColumnControl.mElement.Text;
            }
            DlkAssert.AssertEqual("Column Value Compare", columnValue, actualResult, partialMatch);
        }

        private void verifyTableRowHasText(string valueIndex, string rowIndex)
        {
            string actualResult = "";
            IWebElement rowElement = getTableRow(rowIndex);
            List<IWebElement> rowCellElements = rowElement.FindElements(By.XPath(".//td")).ToList();

            foreach (var el in rowCellElements)
            {
                
                DlkBaseControl rowColumnControl = new DlkBaseControl("Row_Column_Control", el);
                actualResult = rowColumnControl.GetValue().Replace("\r\n", "\n");
                actualResult = actualResult.Trim();

                if (actualResult == valueIndex)
                {
                    break;
                }

                actualResult = "'Text not found'";
            }
           
            DlkAssert.AssertEqual("Value Compare", valueIndex, actualResult, false);
        }

        private IList<IWebElement> getActionList(IWebElement tableRow, bool includeNotDisplayedElements = false)
        {
            List<string> actionsSearch = new List<string>()
            {
                ACTIONS,
                @".//td/div[@class='dropdown text-right']/a | .//td/div[@class='dropdown text-right']/button",
                @".//td[contains(@class,'actions_td')]//a | .//td[contains(@class,'actions_td')]//button",
                @".//td[contains(@class,'actions')]//div[contains(@class,'actions')]",
                @".//td[contains(@class,'actions_td')]//i",
                @".//a[@data-toggle='dropdown']/../ul"
            };

            IList<IWebElement> actionElementList = null;
            for (int i = 0; i < actionsSearch.Count; i++)
            {
                actionElementList = tableRow.FindElements(By.XPath(actionsSearch[i]));
                if (actionElementList.Count > 0)
                {
                    break;
                }
            }

            if (actionElementList.Count == 0)
            {
                int columnHeader = getColumnIndexByHeader("ACTIONS");
                actionElementList = tableRow.FindElements(By.XPath(@".//td[" + columnHeader + "]//a | .//td[" + columnHeader + "]//button"));
            }

            if (includeNotDisplayedElements)
            {
                return actionElementList;
            }

            return actionElementList.Where(item => item.Displayed).ToList();
        }

        private void verifySorting(string column, string sortClassValue, bool expectedResult)
        {
            bool actualResult = false;
            int columnIndex = COLUMN_HEADER_NOT_FOUND;

            if (!Int32.TryParse(column, out columnIndex))
            {
                columnIndex = getColumnIndexByHeader(column);
            } 

            IList<IWebElement> columnHeaderElementList = mElement.FindElements(By.XPath("./thead//th[" + columnIndex + "]/a"));
            if (columnHeaderElementList.Count > 0)
            {
                IWebElement columnHeaderSortableElement = columnHeaderElementList[0];

                if (columnHeaderSortableElement != null)
                {
                    string classAttr = columnHeaderSortableElement.GetAttribute("class").Trim().ToLower();
                    if (classAttr.Equals(sortClassValue))
                    {
                        actualResult = true;
                    }

                    DlkAssert.AssertEqual("Verify Column Sort  : ", expectedResult, actualResult);
                }
                else
                {
                    throw new Exception("Column : " + column + " not sortable.");
                }
            }
            else
            {
                List<IWebElement> rows = mElement.FindElements(By.XPath("./tbody//tr/td/..")).ToList();
                List<IWebElement> sortedRows = mElement.FindElements(By.XPath("./tbody//tr/td/..")).ToList();


                var orderByDescendingResult = from s in sortedRows
                                              orderby s.Text descending
                                              select s;
                if (sortClassValue.Contains("asc"))
                {
                    orderByDescendingResult = from s in sortedRows
                                            orderby s.Text ascending
                                            select s;

                }


                if (rows.SequenceEqual(orderByDescendingResult))
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("Verify Column Sort  : ", expectedResult, actualResult);
            }

        }

        private void clickTableCellByIndex(string row, string column, int index)
        {
            int columnIndex = COLUMN_HEADER_NOT_FOUND;
            IWebElement tableRow = getTableRow(row);

            if (!Int32.TryParse(column, out columnIndex))
            {
                columnIndex = getColumnIndexByHeader(column);
            }

            if (columnIndex != COLUMN_HEADER_NOT_FOUND)
            {
                int counter = 0;
                bool hasExpectedElement = false;
                IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*[not(contains(@id,'cke'))]"));
                foreach (IWebElement element in rowColumnElementList)
                {
                    string classAttr = string.Empty;
                    if (element.Displayed)
                    {
                        switch (element.TagName.Trim())
                        {
                            case "button":
                            case "a":
                                {
                                    classAttr = element.GetAttribute("class");
                                    counter++;
                                    if (index == counter)
                                    {
                                        hasExpectedElement = true;
                                        DlkButton buttonControl = new DlkButton("Button_Element", element);
                                        buttonControl.Click();
                                    }
                                    //element.SendKeys(Keys.Enter);
                                    break;
                                }
                            case "input":
                                {
                                    if (element.GetAttribute("type") != null &&
                                        element.GetAttribute("type").Trim().Equals("checkbox"))
                                    {
                                        counter++;
                                        if (index == counter)
                                        {
                                            hasExpectedElement = true;
                                            DlkButton checkBoxControl = new DlkButton("CheckBox_Element : " + element.Text, element);
                                            checkBoxControl.Click();
                                        }
                                    }
                                    else if (element.GetAttribute("type") != null &&
                                             element.GetAttribute("type").Equals("radio"))
                                    {
                                        counter++;
                                        if (index == counter)
                                        {
                                            hasExpectedElement = true;
                                            DlkRadioButton radioButtonControl = new DlkRadioButton("RadioButton_Element : " + element.Text, element);
                                            radioButtonControl.Click();
                                        }
                                    }
                                    break;
                                }
                            case "img":
                                {
                                    if (element.GetAttribute("class") != null &&
                                        element.GetAttribute("class").Equals("edit_coaching_tip_link sprite sprite-edit"))
                                    {
                                        counter++;
                                        if (index == counter)
                                        {
                                            hasExpectedElement = true;
                                            DlkBaseControl imageControl = new DlkBaseControl("Image_Control", element);
                                            imageControl.Click();
                                        }
                                    }
                                    break;
                                }
                            case "ul":
                                {
                                    if (element.GetAttribute("class") != null &&
                                        element.GetAttribute("class").Equals("expandable_ul"))
                                    {
                                        counter++;
                                        if (index == counter)
                                        {
                                            hasExpectedElement = true;
                                            DlkCommon.DlkCommonFunction.ScrollIntoView(element);
                                            DlkBaseControl listControl = new DlkBaseControl("List_Control", element);
                                            listControl.Click();
                                        }
                                    }
                                    break;
                                }
                            case "i":
                                {
                                    classAttr = element.GetAttribute("class");
                                    if (!string.IsNullOrEmpty(classAttr) &&
                                        (classAttr.Equals("fa fa-pencil edit_coaching_tip_link") ||
                                         classAttr.Equals("fa fa-calendar") ||
                                         classAttr.Equals("fa fa-lock")))
                                    {
                                        counter++;
                                        if (index == counter)
                                        {
                                            hasExpectedElement = true;
                                            DlkBaseControl imageControl = new DlkBaseControl("Image_Control", element);
                                            imageControl.Click();
                                        }
                                    }
                                    break;
                                }
                            case "span":
                                {
                                    classAttr = element.GetAttribute("role");
                                    DlkBaseControl control = new DlkBaseControl("Control", element);
                                    IWebElement parentControl = control.GetParent();

                                    if ((!string.IsNullOrEmpty(classAttr) &&
                                        classAttr.Equals("button")) ||
                                        (element.GetAttribute("class").Trim().Contains("fa") &&
                                        parentControl.TagName != "button"))
                                    {
                                        counter++;
                                        if (index == counter)
                                        {
                                            DlkButton currentControl = new DlkButton("Current_Control", element);
                                            hasExpectedElement = true;
                                            currentControl.Click();
                                        }
                                    }
                                    break;
                                }
                        }

                        if (hasExpectedElement)
                        {
                            DlkLogger.LogInfo("ClickTableCell( ) execution passed.");
                            break;
                        }
                    }
                }

                if (!hasExpectedElement)
                {
                    throw new Exception("ClickTableCell( ) execution failed. Expected element to Click( ) not found.");
                }
            }
            else
            {
                throw new Exception("Column '" + column + "' not found");
            }
        }

        private void setTableCell(string row, string column, string value, int index)
        {
            initialize();
            int indexCounter = 0;
            int columnIndex = COLUMN_HEADER_NOT_FOUND;
            IWebElement tableRow = getTableRow(row);

            if (!Int32.TryParse(column, out columnIndex))
            {
                columnIndex = getColumnIndexByHeader(column);
            }

            if (columnIndex != COLUMN_HEADER_NOT_FOUND)
            {
                if (value.StartsWith("%") && value.EndsWith("%")) // seperate logic for two multi-select controls
                {
                    string[] values = value.Split('%');
                    IList<IWebElement> multiSelectElements = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//select"));
                    IWebElement multiSelectControlOne = multiSelectElements[0];
                    IWebElement multiSelectControlTwo = multiSelectElements[1];
                    IWebElement addButton = multiSelectElements[0].FindElement(By.XPath("./preceding-sibling::button"));
                    IWebElement removeButton = multiSelectElements[1].FindElement(By.XPath("./preceding-sibling::button"));

                    // remove all existing selected items
                    List<IWebElement> selectedItems = multiSelectControlTwo.FindElements(By.XPath(".//option")).ToList();
                    if (selectedItems.Count > 0)
                    {
                        DlkButton currRemoveButton = new DlkButton("Remove Button: ", removeButton);
                        removeButton.Click();

                        foreach (var selectedItem in selectedItems)
                        {
                            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                            mAction.KeyDown(Keys.LeftControl).Click(selectedItem).Build().Perform();
                        }
                    }

                    foreach (string val in values)
                    {
                        if (!string.IsNullOrWhiteSpace(val))
                        {
                            DlkMultiSelect multiSelectControl = new DlkMultiSelect("Multi-select: ", multiSelectControlOne);
                            multiSelectControl.Select(val);
                        }
                    }

                    DlkButton currAddButton = new DlkButton("Add Button: ", addButton);
                    currAddButton.Click();
                    DlkLogger.LogInfo("SetTableCell( ) execution passed.");
                }
                else
                {
                    bool hasExpectedElement = false;
                    IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*"));
                    List<IWebElement> displayedElements = rowColumnElementList.Where(item => item.Displayed && !item.TagName.Equals("option")).ToList();
                    foreach (IWebElement element in displayedElements)
                    {
                        hasExpectedElement = false;
                        switch (element.TagName)
                        {
                            case "textarea":
                                {
                                    indexCounter++;
                                    if (index == indexCounter)
                                    {
                                        hasExpectedElement = true;
                                        DlkTextBox textBoxControl = new DlkTextBox("Column : " + column, element);
                                        textBoxControl.Set(value);
                                    }
                                    break;
                                }
                            case "input":
                                {
                                    if ((element.GetAttribute("type") != null &&
                                        element.GetAttribute("type").Trim().Equals("text")) ||
                                        element.GetAttribute("type").Trim().Equals("password"))
                                    {
                                        indexCounter++;
                                        if (index == indexCounter)
                                        {
                                            hasExpectedElement = true;
                                            DlkTextBox textBoxControl = new DlkTextBox("Column : " + column, element);
                                            textBoxControl.Set(value);
                                        }
                                    }
                                    else if (element.GetAttribute("type") != null &&
                                             element.GetAttribute("type").Trim().Equals("checkbox"))
                                    {
                                        indexCounter++;
                                        if (index == indexCounter)
                                        {
                                            hasExpectedElement = true;
                                            DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox_Element", element);
                                            checkBoxControl.Set(value);
                                        }
                                    }
                                    else if (element.GetAttribute("type") != null &&
                                             element.GetAttribute("type").Equals("radio"))
                                    {

                                        indexCounter++;
                                        if (index == indexCounter)
                                        {
                                            //value should be row or column number.
                                            hasExpectedElement = true;
                                            int rowColumnNumber = Convert.ToInt32(value) - 1;
                                            IList<IWebElement> radioButtons = element.FindElements(By.XPath(@"./ancestor::td//input[@type='radio']"));
                                            DlkRadioButton radioButton = new DlkRadioButton("Radio Button", radioButtons[rowColumnNumber]);
                                            radioButton.ClickUsingJavaScript();
                                        }
                                    }
                                    break;
                                }
                            case "select":
                                {
                                    if (element.GetAttribute("multiple") == null)
                                    {
                                        indexCounter++;
                                        if (index == indexCounter)
                                        {
                                            DlkComboBox comboBoxControl = new DlkComboBox("Column : " + column, element);
                                            comboBoxControl.Select(value);
                                            hasExpectedElement = true;
                                        }
                                    }
                                    else
                                    {
                                        indexCounter++;
                                        if (index == indexCounter)
                                        {
                                            DlkMultiSelect multiSelectControl = new DlkMultiSelect("Column : " + column, element);
                                            multiSelectControl.Select(value);
                                            hasExpectedElement = true;
                                        }
                                    }
                                    break;
                                }
                            case "iframe":
                                {
                                    if (element.GetAttribute("class") != null &&
                                        element.GetAttribute("class").Trim().Equals("cke_wysiwyg_frame cke_reset"))
                                    {
                                        indexCounter++;
                                        if (index == indexCounter)
                                        {
                                            hasExpectedElement = true;
                                            DlkRichTextEditor richTextEditorControl = new DlkRichTextEditor("RichTextEditor_Element", element);
                                            richTextEditorControl.Set(value);
                                        }
                                    }
                                    break;
                                }
                        }

                        if (hasExpectedElement)
                        {
                            DlkLogger.LogInfo("SetTableCell( ) execution passed.");
                            break;
                        }
                    }

                    if (!hasExpectedElement)
                    {
                        throw new Exception("SetTableCellValue( ) execution failed. Expected element to Set( ) not found.");
                    }
                }
            }
            else
            {
                DlkLogger.LogError(new Exception("Column " + column + " not found"));
            }
        }

        private void selectTableCell(string row, string column, string value)
        {
            initialize();
            int columnIndex = COLUMN_HEADER_NOT_FOUND;
            IWebElement tableRow = getTableRow(row);
            IWebElement textbox = null;

            if (!Int32.TryParse(column, out columnIndex))
            {
                columnIndex = getColumnIndexByHeader(column);
            }

            if (columnIndex != COLUMN_HEADER_NOT_FOUND)
            {
                IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*"));
                List<IWebElement> displayedElements = rowColumnElementList.Where(item => item.Displayed && !item.TagName.Equals("option")).ToList();

                foreach (IWebElement element in displayedElements)
                {
                    if (element.TagName.Equals("input"))
                    {
                        if (element.GetAttribute("class").Contains("form-control"))
                        {
                            textbox = element;
                            DlkTextBox textBoxControl = new DlkTextBox("Column : " + column, element);
                            textBoxControl.Set(value);
                            break;
                        }
                    }
                }

                // dropdown results
                IList<IWebElement> comboBoxContainer = textbox.FindElements(By.XPath("./ancestor::div[@class='combo_box_container']"));
                IList<IWebElement> resultContainer = comboBoxContainer[0].FindElements(By.XPath("./div[@class='ffb']/div[1]"));

                if (resultContainer.Count > 0)
                {
                    Thread.Sleep(1000);
                    bool resultNotFound = resultContainer[0].GetAttribute("class").Contains("no-results");

                    if (resultNotFound)
                    {
                        throw new Exception("SelectTableCellValue( ) execution failed. Value is not available in the dropdown choices");
                    }
                    else
                    {
                        IWebElement result = resultContainer[0].FindElement(By.XPath("./div"));
                        if (result.Text.Equals(value))
                        {
                            DlkButton buttonControl = new DlkButton("ComboBox Choice: " + result.Text, result);
                            buttonControl.Click();
                            DlkLogger.LogInfo("SelectTableCellValue( ) execution passed.");
                        }
                    }
                }
            }
            else
            {
                DlkLogger.LogError(new Exception("Column " + column + " not found"));
            }
        }

        [Keyword("ClickColumnHeaderList")]
        public void ClickColumnHeaderList(string Column, string Item)
        {
            try
            {
                bool foundMatching = false;
                initialize();
                int columnIndex = COLUMN_HEADER_NOT_FOUND;
                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                IWebElement columnHeader = mElement.FindElement(By.XPath(@".//th[" + columnIndex + "]//a | .//th[" + columnIndex + "]//input"));
                DlkBaseControl columnHeaderControl = new DlkBaseControl("Column : " + Column, columnHeader);
                columnHeaderControl.ScrollIntoViewUsingJavaScript();
                columnHeaderControl.Click();

                IList<IWebElement> listElements = columnHeader.FindElements(By.XPath("..//ul[@class='dropdown-menu']/li"));
                foreach (IWebElement element in listElements)
                {
                    DlkBaseControl content = new DlkBaseControl("Content", element);
                    if (content.GetValue().TrimEnd().Equals(Item))
                    {
                        string attr = content.GetAttributeValue("class");
                        content.mElement.Click();
                        foundMatching = true;
                        break;
                    }
                }

                if (!foundMatching)
                {
                    throw new Exception("Item : " + Item + " not found.");
                }

                DlkLogger.LogInfo("ClickColumnHeader() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickColumnHeaderList() execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        private void verifyRowExists(string columnHeader, string columnSearchValue, string trueOrFalse)
        {
            int columnHeaderIndex = COLUMN_HEADER_NOT_FOUND;
            if (!Int32.TryParse(columnHeader, out columnHeaderIndex))
            {
                columnHeaderIndex = getColumnIndexByHeader(columnHeader);
            }

            bool expectedResult = Convert.ToBoolean(trueOrFalse);
            bool actualResult = false;

            if (columnHeaderIndex != COLUMN_HEADER_NOT_FOUND)
            {
                string rowNumber = string.Empty;
                IWebElement tableRow = getTableRowByHeader(columnHeaderIndex, columnSearchValue, out rowNumber);
                if (tableRow == null)
                {
                    //Not existing;
                    actualResult = false;
                }
                else
                {
                    //existing.
                    //IWebElement tableRowColumn = tableRow.FindElement(By.XPath(@".//td[" + columnHeaderIndex + "]"));
                    string actualValue = new DlkBaseControl("TableRowColumn", tableRow).GetValue().Replace("\r\n", "\n").Trim();
                    if (columnSearchValue == actualValue && tableRow.Displayed)
                    {
                        actualResult = true;
                    }
                    else
                    {
                        actualResult = false;
                    }
                    DlkLogger.LogAssertion("VerifyRowExists : ", columnSearchValue, actualValue);
                }

                DlkAssert.AssertEqual("VerifyRowExists : ", expectedResult, actualResult);
            }
            else
            {
                throw new Exception("Column Header Not Found.");
            }
        }

        private string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        #endregion
    }
}
