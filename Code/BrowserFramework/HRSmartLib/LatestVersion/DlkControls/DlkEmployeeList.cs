using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using HRSmartLib.DlkCommon;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("EmployeeList")]
    public class DlkEmployeeList : DlkBaseControl
    {
        #region Declarations

        private const string TABLE_CONTROL = @".//div[@class='table-responsive ' or @class='table-responsive' or @class='col-sm-6']/table";
        private const string ROW_LIST_CONTROL = @"./div[@class='tabbed_interface_page tab-pane active' or @class='panel panel-default']";
        private const string MENU_RIBBON_CONTROL = @".//div[contains(@class,'dropdown')]/a[not(./../../../td)]";
        private const string TOGGLE_SECTION = @".//div[@class='col-sm-12 text-right text-center-xs tabbed_interface_toggle_sections' or @class='text-right text-center-xs tabbed_interface_toggle_sections']/a | .//a[contains(@id,'ribbon_expand_link_employee')]";

        #endregion

        #region Constructors

        public DlkEmployeeList(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        #endregion

        #region Properties
        #endregion

        #region Keywords

        [Keyword("GetRowWithValue")]
        public void GetRowWithValue(string RowValue, string Variable)
        { 
            try
            {
                int rowIndex = int.MinValue;
                if (!Int32.TryParse(RowValue, out rowIndex))
                {
                    rowIndex = getRowIndex(RowValue);
                }

                if (rowIndex > int.MinValue)
                {
                    string rowNumber = rowIndex.ToString();
                    string currentPage = string.Empty;
                    string currentNumberOfItemPerPage = string.Empty;
                    IWebElement tableRow = mElement.FindElement(By.XPath("./div[" + rowIndex + "]"));
                    IWebElement paginationElement = findPaginationControl();
                    if (paginationElement == null)
                    {
                        DlkVariable.SetVariable(Variable, string.Concat(currentPage, "-", currentNumberOfItemPerPage, "-", rowNumber));
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
                            DlkVariable.SetVariable(Variable, string.Concat(currentPage, "|", currentNumberOfItemPerPage, "|", rowNumber));
                            DlkLogger.LogInfo("GetTableRowWithColumnValue( ) successfully executed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetRowWithValue() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("GetTableRowWithColumnValue")]
        public void GetTableRowWithColumnValue(string Row, string ColumnHeader, string ColumnValue, string RowVariable)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement tableElement = row.FindElement(By.XPath(TABLE_CONTROL));
                DlkTable tableControl = new DlkTable("Table", tableElement);
                tableControl.GetTableRowWithColumnValue(ColumnHeader, ColumnValue, RowVariable);
                DlkLogger.LogInfo("GetTableRowWithColumnValue() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("GetTableRowWithColumnValue() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTableRowWithColumnValue")]
        public void VerifyTableRowWithColumnValue(string Row, string ColumnHeader, string ColumnValue, string TableRow)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement tableElement = row.FindElement(By.XPath(TABLE_CONTROL));
                DlkTable tableControl = new DlkTable("Table", tableElement);
                tableControl.VerifyTableRowWithColumnValue(ColumnHeader, ColumnValue, TableRow);
                DlkLogger.LogInfo("VerifyTableRowWithColumnValue() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowWithColumnValue() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTableRowPrimaryIcon")]
        public void VerifyTableRowPrimaryIcon(string Row, string TableRow, string TrueOrFalse)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement tableElement = row.FindElement(By.XPath(TABLE_CONTROL));
                DlkTable tableControl = new DlkTable("Table", tableElement);
                tableControl.VerifyTableRowPrimaryIcon(TableRow, TrueOrFalse);
                DlkLogger.LogInfo("VerifyTableRowPrimaryIcon() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowPrimaryIcon() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickTableCell")]
        public void ClickTableCell(string Row, string TableRow, string Column)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement tableElement = row.FindElement(By.XPath(TABLE_CONTROL));
                DlkTable tableControl = new DlkTable("Table", tableElement);
                tableControl.ClickTableCell(TableRow, Column);
                DlkLogger.LogInfo("ClickTableCell() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickTableCell() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("PerformRowActionByColumn")]
        public void PerformRowActionByColumn(string Row, string TableRow, string ColumnNumber, string MenuAction)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement tableElement = row.FindElement(By.XPath(TABLE_CONTROL));
                DlkTable tableControl = new DlkTable("Table", tableElement);
                tableControl.PerformRowActionByColumn(TableRow, ColumnNumber, MenuAction);
                DlkLogger.LogInfo("PerformRowActionByColumn() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformRowActionByColumn() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("RowMenuButtonSelectByTitle")]
        public void RowMenuButtonSelectByTitle(string Row, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement menuRibbonElement = row.FindElement(By.XPath(MENU_RIBBON_CONTROL));
                DlkButtonGroup menuRibbonControl = new DlkButtonGroup("Menu_Ribbon", menuRibbonElement);
                menuRibbonControl.SelectByTitle(Title);
            }
            catch(Exception ex)
            {
                throw new Exception("RowMenuButtonSelectByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("RowMenuButtonSelectByIndex")]
        public void RowMenuButtonSelectByIndex(string Row, string Index)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement menuRibbonElement = row.FindElement(By.XPath(MENU_RIBBON_CONTROL));
                DlkButtonGroup menuRibbonControl = new DlkButtonGroup("Menu_Ribbon", menuRibbonElement);
                menuRibbonControl.SelectByIndex(Index);
            }
            catch (Exception ex)
            {
                throw new Exception("RowMenuButtonSelectByIndex() execution failed. " + ex.Message, ex);
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

        [Keyword("RowHasTextContent")]
        public void RowHasTextContent(string Row, string Text)
        {
            try
            {
                IWebElement row = getRow(Row);
                string[] expectedResults = Text.Split('~');

                foreach (string expectedResult in expectedResults)
                {
                    string actualResult = string.Empty;
                    IWebElement element = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, row)[0];
                    DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                    actualResult = textControl.GetValue().Trim();
                    DlkAssert.AssertEqual("RowHasTextContent : ", expectedResult, actualResult);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowHasTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("RowHasTextContentByIndex")]
        public void RowHasTextContentByIndex(string Row, string Text, string Index)
        {
            try
            {
                IWebElement row = getRow(Row);
                string expectedResult = Text;
                string actualResult = string.Empty;
                int index = Int32.Parse(Index) - 1;
                IWebElement element = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, row)[index];
                DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                actualResult = textControl.GetValue().Trim();
                DlkAssert.AssertEqual("RowHasTextContentByIndex : ", expectedResult, actualResult);
            }
            catch (Exception ex)
            {
                throw new Exception("RowHasTextContentByIndex() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("RowHasPartialTextContent")]
        public void RowHasPartialTextContent(string Row, string Text)
        {
            try
            {
                IWebElement row = getRow(Row);
                string[] expectedResults = Text.Split('~');
                string actualResult = string.Empty;

                foreach (string expectedResult in expectedResults)
                {
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, row, true);
                    foreach (IWebElement element in elements)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                        actualResult = textControl.GetValue().Trim();
                        if (actualResult.Trim().Contains(expectedResult))
                        {
                            break;
                        }
                    }

                    DlkAssert.AssertEqual("RowHasPartialTextContent : ", expectedResult, actualResult, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowHasPartialTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("RowHasPartialTextContentByIndex")]
        public void RowHasPartialTextContentByIndex(string Row, string Text, string Index)
        {
            try
            {
                IWebElement row = getRow(Row);
                string expectedResult = Text;
                string actualResult = string.Empty;
                int index = Int32.Parse(Index) - 1;
                IWebElement element = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, row, true)[index];
                DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                actualResult = textControl.GetValue().Trim();
                DlkAssert.AssertEqual("RowHasPartialTextContentByIndex : ", expectedResult, actualResult, true);
            }
            catch (Exception ex)
            {
                throw new Exception("RowHasPartialTextContentByIndex() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("RowVerifyPartialTextContent")]
        public void RowVerifyPartialTextContent(string Row, string Text, string TrueOrFalse)
        {
            try
            {
                IWebElement row = getRow(Row);
                string[] expectedResults = Text.Split('~');
                string actualResultText = string.Empty;

                foreach (string expectedResult in expectedResults)
                {
                    bool actualResult = false;
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, row, true);
                    foreach (IWebElement element in elements)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                        actualResultText = textControl.GetValue().Trim();
                        if (actualResultText.Trim().Contains(expectedResult))
                        {
                            actualResult = true;
                            break;
                        }
                    }

                    DlkAssert.AssertEqual("RowVerifyPartialTextContent : ", Convert.ToBoolean(TrueOrFalse), actualResult);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RowVerifyPartialTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("ClickToggleByRow")]
        public void ClickToggleByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement expandCollapseElement = row.FindElement(By.XPath(TOGGLE_SECTION));
                DlkButton toggleControl = new DlkButton("Toggle_Control : ", expandCollapseElement);
                toggleControl.Click();
                DlkLogger.LogInfo("ClickToggleByRow() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickToggleByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTableRowWithColumnValuePartially")]
        public void VerifyTableRowWithColumnValuePartially(string Row, string ColumnHeader, string ColumnValue, string TableRow)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement tableElement = row.FindElement(By.XPath(TABLE_CONTROL));
                DlkTable tableControl = new DlkTable("Table", tableElement);
                tableControl.VerifyTableRowWithColumnValuePartially(ColumnHeader, ColumnValue, TableRow);
                DlkLogger.LogInfo("VerifyTableRowWithColumnValuePartially() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTableRowWithColumnValuePartially() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ToggleFirstContainerByRow")]
        public void ToggleFirstContainerByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement toggleElement = getFirstContainerControl(row).mElement.FindElement(By.XPath(".//div[contains(@class,'panel-heading')]"));
                DlkBaseControl toggleControl = new DlkBaseControl("Toggle_Control : ", toggleElement);
                toggleControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ToggleFirstContainerByRow() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("ToggleFirstContainerByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ToggleSecondContainerByRow")]
        public void ToggleSecondContainerByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement toggleElement = getSecondContainerControl(row).mElement.FindElement(By.XPath(".//div[contains(@class,'panel-heading')]"));
                DlkBaseControl toggleControl = new DlkBaseControl("Toggle_Control : ", toggleElement);
                toggleControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ToggleSecondContainerByRow() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ToggleSecondContainerByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ToggleThirdContainerByRow")]
        public void ToggleThirdContainerByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement toggleElement = getThirdContainerControl(row).mElement.FindElement(By.XPath(".//div[contains(@class,'panel-heading')]"));
                DlkBaseControl toggleControl = new DlkBaseControl("Toggle_Control : ", toggleElement);
                toggleControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ToggleThirdContainerByRow() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ToggleThirdContainerByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("FirstContainerRowMenuButtonClickByTitle")]
        public void FirstContainerRowMenuButtonClickByTitle(string Row, string FirstContainerRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                rowMenuButtonClickByTitle(EmployeeListContainer.FirstContainer, row, FirstContainerRow, Title);
                DlkLogger.LogInfo("FirstContainerRowMenuButtonClickByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("FirstContainerRowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("SecondContainerRowMenuButtonClickByTitle")]
        public void SecondContainerRowMenuButtonClickByTitle(string Row, string SecondContainerRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                rowMenuButtonClickByTitle(EmployeeListContainer.SecondContainer, row, SecondContainerRow, Title);
                DlkLogger.LogInfo("SecondContainerRowMenuButtonClickByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SecondContainerRowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ThirdContainerRowMenuButtonClickByTitle")]
        public void ThirdContainerRowMenuButtonClickByTitle(string Row, string ThirdContainerRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                rowMenuButtonClickByTitle(EmployeeListContainer.ThirdContainer, row, ThirdContainerRow, Title);
                DlkLogger.LogInfo("ThirdContainerRowMenuButtonClickByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ThirdContainerRowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickRowControlByTitle")]
        public void ClickRowControlByTitle(string Row, string Title)
        {
            try
            {
                IWebElement rowElement = getRow(Row);
                IWebElement controlWithTitle = DlkCommon.DlkCommonFunction.GetElementWithText(Title, rowElement, true)[0];
                DlkBaseControl clickableControl = new DlkBaseControl("Control : " + Title, controlWithTitle);
                clickableControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickRowControlByTitleAndIndex")]
        public void ClickRowControlByTitleAndIndex(string Row, string Title, string Index)
        {
            try
            {
                int index = Convert.ToInt32(Index) - 1;
                IWebElement rowElement = getRow(Row);
                List<IWebElement> controlWithTitle = DlkCommon.DlkCommonFunction.GetElementWithText(Title, rowElement, true).ToList();

                for (int i = 0; i < controlWithTitle.Count; i++)
                {
                    IWebElement element = controlWithTitle[i];
                    if (!element.Displayed)
                    {
                        controlWithTitle.RemoveAt(i--);
                    }
                }

                DlkBaseControl clickableControl = new DlkBaseControl("Control : " + Title, controlWithTitle[index]);
                clickableControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ClickRowControlByTitleAndIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickRowControlByTitleAndIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("FirstContainerClickRowControlByTitle")]
        public void FirstContainerClickRowControlByTitle(string Row, string FirstContainerRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup container = getRowContainerGroup(EmployeeListContainer.FirstContainer, row);
                container.ClickRowControlByTitle(FirstContainerRow, Title);
                DlkLogger.LogInfo("FirstContainerClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("FirstContainerClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("SecondContainerClickRowControlByTitle")]
        public void SecondContainerClickRowControlByTitle(string Row, string SecondContainerRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup container = getRowContainerGroup(EmployeeListContainer.SecondContainer, row);
                container.ClickRowControlByTitle(SecondContainerRow, Title);
                DlkLogger.LogInfo("SecondContainerClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SecondContainerClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ThirdContainerClickRowControlByTitle")]
        public void ThirdContainerClickRowControlByTitle(string Row, string ThirdContainerRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup container = getRowContainerGroup(EmployeeListContainer.ThirdContainer, row);
                container.ClickRowControlByTitle(ThirdContainerRow, Title);
                DlkLogger.LogInfo("ThirdContainerClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ThirdContainerClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("FirstContainerClickByRowIndex")]
        public void FirstContainerClickByRowIndex(string Row, string FirstContainerRow, string Index)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup rowContainerGroup = getRowContainerGroup(EmployeeListContainer.FirstContainer, row);
                rowContainerGroup.ClickRowControlByIndex(FirstContainerRow, Index);
                DlkLogger.LogInfo("FirstContainerClickByRowIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("FirstContainerClickByRowIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("SecondContainerClickByRowIndex")]
        public void SecondContainerClickByRowIndex(string Row, string SecondContainerRow, string Index)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup rowContainerGroup = getRowContainerGroup(EmployeeListContainer.SecondContainer, row);
                rowContainerGroup.ClickRowControlByIndex(SecondContainerRow, Index);
                DlkLogger.LogInfo("SecondContainerClickByRowIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SecondContainerClickByRowIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ThirdContainerClickByRowIndex")]
        public void ThirdContainerClickByRowIndex(string Row, string ThirdContainerRow, string Index)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup rowContainerGroup = getRowContainerGroup(EmployeeListContainer.ThirdContainer, row);
                rowContainerGroup.ClickRowControlByIndex(ThirdContainerRow, Index);
                DlkLogger.LogInfo("ThirdContainerClickByRowIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ThirdContainerClickByRowIndex() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("FirstContainerPerformTableRowActionByColumn")]
        public void FirstContainerPerformTableRowActionByColumn(string Row, 
                                                                string FirstContainerRow, 
                                                                string TableRow, 
                                                                string Column, 
                                                                string MenuAction)
        {
            performTableRowActionByColumn(EmployeeListContainer.FirstContainer,
                                          Row,
                                          FirstContainerRow,
                                          TableRow,
                                          Column,
                                          MenuAction);
        }

        [Keyword("SecondContainerPerformTableRowActionByColumn")]
        public void SecondContainerPerformTableRowActionByColumn(string Row,
                                                                string FirstContainerRow,
                                                                string TableRow,
                                                                string Column,
                                                                string MenuAction)
        {
            performTableRowActionByColumn(EmployeeListContainer.SecondContainer,
                                          Row,
                                          FirstContainerRow,
                                          TableRow,
                                          Column,
                                          MenuAction);
        }

        [Keyword("ThirdContainerPerformTableRowActionByColumn")]
        public void ThirdContainerPerformTableRowActionByColumn(string Row,
                                                                string FirstContainerRow,
                                                                string TableRow,
                                                                string Column,
                                                                string MenuAction)
        {
            performTableRowActionByColumn(EmployeeListContainer.ThirdContainer,
                                          Row,
                                          FirstContainerRow,
                                          TableRow,
                                          Column,
                                          MenuAction);
        }

        [Keyword("VerifyRowCount")]
        public void VerifyRowCount(string ExpectedResult)
        {
            try
            {
                int expectedResult = int.MinValue;
                if (Int32.TryParse(ExpectedResult, out expectedResult))
                {
                    IList<IWebElement> tableRows = mElement.FindElements(By.XPath("./div[@id]"));
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
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string Item, string TrueOrFalse)
        {
            try
            {
                bool foundItem = false;
                IList<IWebElement> tableRows = mElement.FindElements(By.XPath(ROW_LIST_CONTROL));

                for (int i = 0; i < tableRows.Count; i++)
                {
                    IWebElement rowElement = tableRows[i].FindElement(By.XPath("./descendant::a[@class='data_ellipsis']"));
                    DlkBaseControl tempTableRow = new DlkBaseControl("TableRow", rowElement);
                    string tableRowColumnValue = tempTableRow.GetValue().Trim().Replace("\r\n", "\n");

                    if (tableRowColumnValue.Equals(Item))
                    {
                        foundItem = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyItemExists:", Convert.ToBoolean(TrueOrFalse), foundItem);
                DlkLogger.LogInfo("VerifyItemExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemExists() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private DlkContainerGroup getFirstContainerControl(IWebElement rowElement)
        {
            return new DlkContainerGroup("First_Container", rowElement.FindElement(By.XPath(".//div[@role='tablist'][1]")));
        }

        private DlkContainerGroup getThirdContainerControl(IWebElement rowElement)
        {
            return new DlkContainerGroup("Third_Container", rowElement.FindElement(By.XPath(".//div[@role='tablist'][3]")));
        }

        private DlkContainerGroup getSecondContainerControl(IWebElement rowElement)
        {
            return new DlkContainerGroup("Second_Container", rowElement.FindElement(By.XPath(".//div[@role='tablist'][2]")));
        }

        private void rowMenuButtonClickByTitle(EmployeeListContainer employeeListContainer, IWebElement rowElement, string row, string title)
        {
            DlkContainerGroup rowContainerGroup = getRowContainerGroup(employeeListContainer, rowElement);
            rowContainerGroup.RowMenuButtonClickByTitle(row, title);
        }

        private void rowMenuButtonClickByIndex(EmployeeListContainer employeeListContainer, IWebElement rowElement, string row, string index)
        {
            DlkContainerGroup rowContainerGroup = getRowContainerGroup(employeeListContainer, rowElement);
            rowContainerGroup.RowMenuButtonClickByIndex(row, index);
        }

        private DlkContainerGroup getRowContainerGroup(EmployeeListContainer employeeListContainer, IWebElement rowElement)
        {
            DlkContainerGroup container = null;
            switch (employeeListContainer)
            {
                case EmployeeListContainer.FirstContainer :
                    container = new DlkContainerGroup("First_Container", rowElement.FindElement(By.XPath("(.//div[@id and @role='tabpanel'])[1]")));
                    break;
                case EmployeeListContainer.ThirdContainer :
                    container = new DlkContainerGroup("Third_Container", rowElement.FindElement(By.XPath("(.//div[@id and @role='tabpanel'])[3]")));
                    break;
                case EmployeeListContainer.SecondContainer :
                    container = new DlkContainerGroup("Second_Container", rowElement.FindElement(By.XPath("(.//div[@id and @role='tabpanel'])[2]")));
                    break;
                default :
                    break;
            }

            return container;
        }

        private int getRowIndex(string Row)
        {
            int tableRowIndex = int.MinValue;
            bool bRowFound = false;
            DlkPagination pagination = null;
            //IWebElement tableRow = null;
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
                //initialize();
                IList<IWebElement> tableRows = mElement.FindElements(By.XPath(ROW_LIST_CONTROL));

                for (int i = 0; i < tableRows.Count; i++)
                {
                    IWebElement rowElement = tableRows[i].FindElement(By.XPath("./descendant::a[@class='data_ellipsis']"));
                    DlkBaseControl tempTableRow = new DlkBaseControl("TableRow", rowElement);
                    string tableRowColumnValue = tempTableRow.GetValue().Trim().Replace("\r\n", "\n");

                    if (tableRowColumnValue.Equals(Row))
                    {
                        tableRowIndex = i + 1;
                        //Row found.
                        bRowFound = true;
                        //tableRow = mElement.FindElement(By.XPath(@"./div[" + tableRowIndex + "]"));
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

            return tableRowIndex;
        }

        private IWebElement findPaginationControl()
        {
            IWebElement pagination = null;
            string paginationXpath = @"../preceding-sibling::div[1]//ul[@class='pagination pagination-sm pagination_spans pagination_info_my_employees_performance_1900'] | .//ul[@class='pagination pagination-sm pagination_spans pagination_info_']";
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

        private IWebElement getTableRowByVariable(string rowVariable)
        {
            string[] rowDetailsArray = rowVariable.Split('-');
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

            return mElement.FindElement(By.XPath(@"./div[@id][" + rowNumber + "]"));
        }
        
        /// <summary>
        /// Gets row by row number or row variable.
        /// </summary>
        /// <param name="row">Row Number or Row Variable</param>
        /// <returns>Row Element</returns>
        private IWebElement getRow(string row)
        {
            int rowNumber = int.MinValue;

            if (int.TryParse(row, out rowNumber))
            {
                return mElement.FindElement(By.XPath("./div[@id][" + rowNumber + "]"));
            }
            else
            {
                return getTableRowByVariable(row);
            }
        }

        public void performTableRowActionByColumn(EmployeeListContainer container,
                                                  string Row,
                                                  string FirstContainerRow,
                                                  string TableRow,
                                                  string Column,
                                                  string MenuAction)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup containerRow = getRowContainerGroup(container, row);
                containerRow.PerformTableRowActionByColumn(FirstContainerRow, TableRow, Column, MenuAction);
                DlkLogger.LogInfo("PerformTableRowActionByColumn() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformTableRowActionByColumn() execution failed. " + ex.Message, ex);
            }
        }

        #endregion
    }
}
