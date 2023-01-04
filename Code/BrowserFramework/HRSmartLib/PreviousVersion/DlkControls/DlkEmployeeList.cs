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

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("EmployeeList")]
    public class DlkEmployeeList : DlkBaseControl
    {
        #region Declarations

        private const string TABLE_CONTROL = @".//div[@class='table-responsive ' or @class='table-responsive']/table";
        private const string ROW_LIST_CONTROL = @"./div[@class='tabbed_interface_page tab-pane active' or @class='panel panel-default']";
        private const string MENU_RIBBON_CONTROL = @".//div[contains(@class,'dropdown')]/a[not(./../../../td)]";
        private const string TOGGLE_SECTION = @".//div[@class='col-sm-12 text-right text-center-xs tabbed_interface_toggle_sections' or @class='text-right text-center-xs tabbed_interface_toggle_sections']/a";

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
                        DlkVariable.SetVariable(Variable, string.Concat(currentPage, "|", currentNumberOfItemPerPage, "|", rowNumber));
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
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
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

        [Keyword("ToggleCurrentAppraisalByRow")]
        public void ToggleCurrentAppraisalByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement toggleElement = getCurrentAppraisalControl(row).mElement.FindElement(By.XPath(".//div[contains(@class,'panel-heading')]"));
                DlkBaseControl toggleControl = new DlkBaseControl("Toggle_Control : ", toggleElement);
                toggleControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ToggleCurrentAppraisalByRow() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("ToggleCurrentAppraisalByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("TogglePendingAppraisalsByRow")]
        public void TogglePendingAppraisalsByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement toggleElement = getPendingAppraisalsControl(row).mElement.FindElement(By.XPath(".//div[contains(@class,'panel-heading')]"));
                DlkBaseControl toggleControl = new DlkBaseControl("Toggle_Control : ", toggleElement);
                toggleControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("TogglePendingAppraisalsByRow() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("TogglePendingAppraisalsByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("TogglePastAppraisalsByRow")]
        public void TogglePastAppraisalsByRow(string Row)
        {
            try
            {
                IWebElement row = getRow(Row);
                IWebElement toggleElement = getPastAppraisalsControl(row).mElement.FindElement(By.XPath(".//div[contains(@class,'panel-heading')]"));
                DlkBaseControl toggleControl = new DlkBaseControl("Toggle_Control : ", toggleElement);
                toggleControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("TogglePastAppraisalsByRow() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("TogglePastAppraisalsByRow() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("CurrentAppraisalRowMenuButtonClickByTitle")]
        public void CurrentAppraisalRowMenuButtonClickByTitle(string Row, string CurrentAppraisalRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                rowMenuButtonClickByTitle(EmployeeListContainer.FirstContainer, row, CurrentAppraisalRow, Title);
                DlkLogger.LogInfo("CurrentAppraisalRowMenuButtonClickByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("CurrentAppraisalRowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("PendingAppraisalRowMenuButtonClickByTitle")]
        public void PendingAppraisalRowMenuButtonClickByTitle(string Row, string PendingAppraisalRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                rowMenuButtonClickByTitle(EmployeeListContainer.SecondContainer, row, PendingAppraisalRow, Title);
                DlkLogger.LogInfo("PendingAppraisalRowMenuButtonClickByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PendingAppraisalRowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("PastAppraisalRowMenuButtonClickByTitle")]
        public void PastAppraisalRowMenuButtonClickByTitle(string Row, string PastAppraisalRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                rowMenuButtonClickByTitle(EmployeeListContainer.ThirdContainer, row, PastAppraisalRow, Title);
                DlkLogger.LogInfo("PastAppraisalRowMenuButtonClickByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PastAppraisalRowMenuButtonClickByTitle() execution failed. " + ex.Message, ex);
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

        [Keyword("CurrentAppraisalClickRowControlByTitle")]
        public void CurrentAppraisalClickRowControlByTitle(string Row, string CurrentAppraisalRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup container = getRowContainerGroup(EmployeeListContainer.FirstContainer, row);
                container.ClickRowControlByTitle(CurrentAppraisalRow, Title);
                DlkLogger.LogInfo("CurrentAppraisalClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("CurrentAppraisalClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("PendingAppraisalClickRowControlByTitle")]
        public void PendingAppraisalClickRowControlByTitle(string Row, string PendingAppraisalRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup container = getRowContainerGroup(EmployeeListContainer.SecondContainer, row);
                container.ClickRowControlByTitle(PendingAppraisalRow, Title);
                DlkLogger.LogInfo("PendingAppraisalClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PendingAppraisalClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("PastAppraisalClickRowControlByTitle")]
        public void PastAppraisalClickRowControlByTitle(string Row, string PastAppraisalRow, string Title)
        {
            try
            {
                IWebElement row = getRow(Row);
                DlkContainerGroup container = getRowContainerGroup(EmployeeListContainer.ThirdContainer, row);
                container.ClickRowControlByTitle(PastAppraisalRow, Title);
                DlkLogger.LogInfo("PastAppraisalClickRowControlByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("PastAppraisalClickRowControlByTitle() execution failed. " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private DlkContainerGroup getCurrentAppraisalControl(IWebElement rowElement)
        {
            return new DlkContainerGroup("Current_Appraisal", rowElement.FindElement(By.XPath(".//div[@role='tablist'][1]")));
        }

        private DlkContainerGroup getPastAppraisalsControl(IWebElement rowElement)
        {
            return new DlkContainerGroup("Past_Appraisal", rowElement.FindElement(By.XPath(".//div[@role='tablist'][3]")));
        }

        private DlkContainerGroup getPendingAppraisalsControl(IWebElement rowElement)
        {
            return new DlkContainerGroup("Pending_Appraisal", rowElement.FindElement(By.XPath(".//div[@role='tablist'][2]")));
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
                    container = new DlkContainerGroup("Current_Appraisal", rowElement.FindElement(By.XPath("(.//div[@id and @role='tabpanel'])[1]")));
                    break;
                case EmployeeListContainer.ThirdContainer :
                    container = new DlkContainerGroup("Past_Appraisal", rowElement.FindElement(By.XPath("(.//div[@id and @role='tabpanel'])[3]")));
                    break;
                case EmployeeListContainer.SecondContainer :
                    container = new DlkContainerGroup("Pending_Appraisal", rowElement.FindElement(By.XPath("(.//div[@id and @role='tabpanel'])[2]")));
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

            return mElement.FindElement(By.XPath(@"./div[" + rowNumber + "]"));
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

        #endregion
    }
}
