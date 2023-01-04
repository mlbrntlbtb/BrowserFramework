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

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region Declarations

        private const string ACTIONS = @".//td[@class='actions_td']//a | .//td[@class='actions_td']//button";
        private const string ROW = @".//tbody/tr";

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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTableRowWithColumnValue")]
        public void GetTableRowWithColumnValue(string ColumnHeader, string ColumnSearchValue, string VariableName)
        {
            try
            {
                initialize();
                int columnHeaderIndex = int.MinValue;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                if (columnHeaderIndex > int.MinValue)
                {
                    string rowNumber = string.Empty;
                    string currentPage = string.Empty;
                    string currentNumberOfItemPerPage = string.Empty;
                    IWebElement tableRow = getTableRowByHeader(columnHeaderIndex, ColumnSearchValue, out rowNumber);
                    IWebElement paginationElement = findPaginationControl();
                    if (paginationElement == null)
                    {
                        DlkVariable.SetVariable(VariableName, string.Concat(currentPage, "|", currentNumberOfItemPerPage, "|", rowNumber));
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
                            throw new Exception("Missing details for table row.");
                        }
                        else
                        {
                            DlkVariable.SetVariable(VariableName, string.Concat(currentPage, "|", currentNumberOfItemPerPage, "|", rowNumber));
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

        [Keyword("VerifyTableRowWithColumnValue")]
        public void VerifyTableRowWithColumnValue(string ColumnHeader, string ColumnValue, string Row)
        {
            try
            {
                initialize();
                verifyTableRowWithColumnValue(ColumnHeader, ColumnValue, Row, false);
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

        [Keyword("VerifyTableRowWithColumnValuePartially")]
        public void VerifyTableRowWithColumnValuePartially(string ColumnHeader, string ColumnValue, string Row)
        {
            try
            {
                initialize();
                verifyTableRowWithColumnValue(ColumnHeader, ColumnValue, Row, true);
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

        [Keyword("VerifyRowExists")]
        public void VerifyRowExists(string ColumnHeader, string ColumnSearchValue, string TrueOrFalse = "true")
        {
            try
            {
                initialize();
                int columnHeaderIndex = int.MinValue;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                if (columnHeaderIndex > int.MinValue)
                {
                    string rowNumber = string.Empty;
                    IWebElement tableRow = getTableRowByHeader(columnHeaderIndex, ColumnSearchValue, out rowNumber);
                    if (tableRow == null)
                    {
                        //Not existing;
                        actualResult = false;
                    }
                    else
                    {
                        //existing.
                        IWebElement tableRowColumn = tableRow.FindElement(By.XPath(@".//td[" + columnHeaderIndex + "]"));
                        string actualValue = new DlkBaseControl("TableRowColumn", tableRowColumn).GetValue().Trim();
                        if (ColumnSearchValue == actualValue)
                        {
                            actualResult = true;
                        }
                        else
                        {
                            actualResult = false;
                        }
                        DlkLogger.LogAssertion("VerifyRowExists : ", ColumnSearchValue, actualValue);
                    }

                    DlkAssert.AssertEqual("VerifyRowExists : ", expectedResult, actualResult);
                }
                else
                {
                    throw new Exception("Column Header Not Found.");
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

        [Keyword("SortColumn")]
        public void SortColumn(string ColumnHeader, string SortOrder)
        {
            try
            {
                initialize();
                int columnHeaderIndex = int.MinValue;
                if (!Int32.TryParse(ColumnHeader, out columnHeaderIndex))
                {
                    columnHeaderIndex = getColumnIndexByHeader(ColumnHeader);
                }

                if (columnHeaderIndex > int.MinValue)
                {
                    IWebElement columnHeader = mElement.FindElement(By.XPath(@".//th[" + columnHeaderIndex + "]//a"));
                    DlkLink headerLink = new DlkLink("Sort " + ColumnHeader, columnHeader);
                    string columnHeaderClassAttr = DlkString.RemoveCarriageReturn(columnHeader.GetAttribute("class").Trim());

                    if (columnHeaderClassAttr.Equals("sortable"))
                    {
                        headerLink.Click(1.5);
                        //Reintialize header and link after click.
                        initialize();
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

        [Keyword("ClickTableCell")]
        public void ClickTableCell(String Row, String Column)
        {
            try
            {
                initialize();
                int columnIndex = -1;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != -1)
                {
                    bool hasExpectedElement = false;
                    IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*"));
                    foreach (IWebElement element in rowColumnElementList)
                    {
                        switch (element.TagName.Trim())
                        {
                            case "button" :
                            case "a" :
                            {
                                hasExpectedElement = true;
                                //DlkButton buttonControl = new DlkButton("Button_Element", element);
                                //buttonControl.Click();
                                element.SendKeys(Keys.Enter);
                                break;
                            }
                            case "input" :
                            {
                                if (element.GetAttribute("type") != null &&
                                    element.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    hasExpectedElement = true;
                                    DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox_Element : " + element.Text, element);
                                    checkBoxControl.ClickUsingJavaScript();
                                }
                                else if (element.GetAttribute("type") != null &&
                                         element.GetAttribute("type").Equals("radio"))
                                {
                                    hasExpectedElement = true;
                                    DlkRadioButton radioButtonControl = new DlkRadioButton("RadioButton_Element : " + element.Text, element);
                                    radioButtonControl.Click();
                                }
                                break;
                            }
                            case "img" :
                            {
                                if (element.GetAttribute("class") != null &&
                                    element.GetAttribute("class").Equals("edit_coaching_tip_link sprite sprite-edit"))
                                {
                                    hasExpectedElement = true;
                                    DlkBaseControl imageControl = new DlkBaseControl("Image_Control", element);
                                    imageControl.Click();
                                }
                                break;
                            }
                            case "ul" :
                            {
                                if (element.GetAttribute("class") != null &&
                                    element.GetAttribute("class").Equals("expandable_ul"))
                                {
                                    hasExpectedElement = true;
                                    base.ScrollIntoViewUsingJavaScript();
                                    DlkBaseControl listControl = new DlkBaseControl("List_Control", element);
                                    listControl.Click();
                                }
                                break;
                            }
                            case "i" :
                            {
                                if (element.GetAttribute("class") != null &&
                                       element.GetAttribute("class").Equals("fa fa-pencil edit_coaching_tip_link"))
                                {
                                    hasExpectedElement = true;
                                    DlkBaseControl imageControl = new DlkBaseControl("Image_Control", element);
                                    imageControl.Click();
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

                    if (!hasExpectedElement)
                    {
                        throw new Exception("ClickTableCell( ) execution failed. Expected element to Click( ) not found.");
                    }
                }
                else
                {
                    throw new Exception("Column '" + Column + "' not found");
                }
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
                int columnIndex = -1;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != -1)
                {
                    bool hasExpectedElement = false;
                    IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]/descendant::*[contains(text(),'" + Title + "')]"));
                    foreach (IWebElement element in rowColumnElementList)
                    {
                        switch (element.TagName.Trim())
                        {
                            case "button":
                            case "a":
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

        [Keyword("VerifyCellCheckBoxValue")]
        public void VerifyCellCheckBoxValue(String Row, String Column, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int columnIndex = -1;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != -1)
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

        [Keyword("VerifyCellRadioButtonValue")]
        public void VerifyCellRadioButtonValue(String Row, String Column, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                int columnIndex = -1;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                if (columnIndex != -1)
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

        [Keyword("SetTableCellValue")]
        public void SetTableCellValue(string Row, string Column, string Value)
        {
            try
            {
                initialize();
                int columnIndex = -1;
                IWebElement tableRow = getTableRow(Row);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }
                
                if (columnIndex != -1)
                {
                    bool hasExpectedElement = false;
                    IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*"));
                    foreach (IWebElement element in rowColumnElementList)
                    {
                        hasExpectedElement = false;
                        switch (element.TagName)
                        {
                            case "input" :
                            {
                                if (element.GetAttribute("type") != null &&
                                    element.GetAttribute("type").Trim().Equals("text"))
                                {
                                    hasExpectedElement = true;
                                    DlkTextBox textBoxControl = new DlkTextBox("Column : " + Column, element);
                                    textBoxControl.Set(Value);
                                }
                                else if (element.GetAttribute("type") != null &&
                                         element.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    hasExpectedElement = true;
                                    DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox_Element", element);
                                    checkBoxControl.Set(Value);
                                }
                                else if (element.GetAttribute("type") != null &&
                                         element.GetAttribute("type").Equals("radio"))
                                {
                                    //value should be row or column number.
                                    hasExpectedElement = true;
                                    int rowColumnNumber = Convert.ToInt32(Value) - 1;
                                    IList<IWebElement> radioButtons = element.FindElements(By.XPath(@"../input[@type='radio']"));
                                    DlkRadioButton radioButton = new DlkRadioButton("Radio Button", radioButtons[rowColumnNumber]);
                                    radioButton.Click();
                                }
                                break;
                            }
                            case "select" :
                            {
                                if (element.GetAttribute("multiple") == null)
                                {
                                    DlkComboBox comboBoxControl = new DlkComboBox("Column : " + Column, element);
                                    comboBoxControl.Select(Value);
                                }
                                else
                                {
                                    DlkMultiSelect multiSelectControl = new DlkMultiSelect("Column : " + Column, element);
                                    multiSelectControl.Select(Value);
                                }
                                hasExpectedElement = true;
                                break;
                            }
                            case "iframe" :
                            {
                                if (element.GetAttribute("class") != null &&
                                    element.GetAttribute("class").Trim().Equals("cke_wysiwyg_frame cke_reset"))
                                {
                                    hasExpectedElement = true;
                                    DlkRichTextEditor richTextEditorControl = new DlkRichTextEditor("RichTextEditor_Element", element);
                                    richTextEditorControl.Set(Value);
                                }
                                break;
                            }
                        }

                        if (hasExpectedElement)
                        {
                            DlkLogger.LogInfo("SetTableCellValue( ) execution passed.");
                            break;
                        }
                    }

                    if (!hasExpectedElement)
                    {
                        throw new Exception("SetTableCellValue( ) execution failed. Expected element to Set( ) not found.");
                    }
                } 
                else
                {
                    DlkLogger.LogError(new Exception("Column " + Column + " not found"));
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

        [Keyword("VerifyRowActionColumnToolTip")]
        public void VerifyRowActionColumnToolTip(string Row, string ColumnNumber, string ExpectedResult)
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
                    string dataOriginalTitleAttr = selectedAction.GetAttribute("data-original-title");
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

        [Keyword("VerifyRowActionColumnState")]
        public void VerifyRowActionColumnState(string Row, string ColumnNumber, string TrueOrFalse)
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

            return int.MinValue;
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
                @".//thead[@class='header']/tr/th[@scope='col']",
                @".//thead/tr[@class='thclass']/th[@scope='col']",
                @".//thead/tr/th[@scope='col']",
                @".//thead/tr/th"
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
            string[] rowDetailsArray = rowVariable.Split('|');
            string pageNumber = rowDetailsArray[0];
            string numberOfItemsOnPage = rowDetailsArray[1];
            string rowNumber = rowDetailsArray[2];
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
                    }
                }
            }

            return mElement.FindElement(By.XPath(@".//tbody/tr[" + rowNumber  + "]"));
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
                if (!DlkEnvironment.mSwitchediFrame)
                    initialize();
                IList<IWebElement> tableRowsColumns = mElement.FindElements(By.XPath(@".//tbody/tr/td[" + columnHeaderIndex + "]")); 

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
                        tableRow = mElement.FindElement(By.XPath(@".//tbody/tr[" + tableRowIndex + "]"));
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
            link.ClickUsingJavaScript();

            if (link.IsAttributeExists("aria-expanded") || 
                    (link.IsAttributeExists("data-toggle") &&
                     link.GetAttributeValue("data-toggle").Equals("dropdown")))
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
            IList<IWebElement> dropDownMenuOptions = actionElement.FindElements(By.XPath(@"../ul[@role='menu']/li/a"));
            foreach (IWebElement option in dropDownMenuOptions)
            {
                DlkBaseControl optionControl = new DlkBaseControl("Option", option);
                if (optionControl.GetValue().Trim().Contains(popUpAction))
                {
                    optionControl.ClickUsingJavaScript();
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
            int columnHeaderIndex = int.MinValue;
            if (!Int32.TryParse(columnHeader, out columnHeaderIndex))
            {
                columnHeaderIndex = getColumnIndexByHeader(columnHeader);
            }

            IWebElement rowElement = getTableRow(row);
            IWebElement rowColumnElement = rowElement.FindElement(By.XPath("./td[" + columnHeaderIndex + "]"));
            DlkBaseControl rowColumnControl = new DlkBaseControl("Row_Column_Control", rowColumnElement);
            string actualResult = rowColumnControl.GetValue().Replace("\r\n", "\n");
            DlkAssert.AssertEqual("Column Value Compare", columnValue, actualResult, partialMatch);
        }

        private IList<IWebElement> getActionList(IWebElement tableRow)
        {
            IList<IWebElement> actionElementList = tableRow.FindElements(By.XPath(ACTIONS));

            if (actionElementList == null ||
                actionElementList.Count == 0)
            {
                //My Employee EmployeeList table.
                actionElementList = tableRow.FindElements(By.XPath(".//td/div[@class='dropdown text-right']/a | .//td/div[@class='dropdown text-right']/button"));
            }

            return actionElementList;
        }

        private void verifySorting(string column, string sortClassValue, bool expectedResult)
        {
            bool actualResult = false;
            int columnIndex = int.MinValue;

            if (!Int32.TryParse(column, out columnIndex))
            {
                columnIndex = getColumnIndexByHeader(column);
            }

            IWebElement columnHeaderSortableElement = mElement.FindElement(By.XPath("./thead//th[" + columnIndex + "]/a"));
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

        #endregion
    }
}
