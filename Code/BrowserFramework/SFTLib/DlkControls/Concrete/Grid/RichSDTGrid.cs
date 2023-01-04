using CommonLib.DlkControls;
using OpenQA.Selenium;
using SFTLib.DlkControls.Contract;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SFTLib.DlkControls.Concrete.Grid
{
    public class RichSDTGrid : IGrid
    {
        private const string firstHalfRowsXPath = @".//ancestor::*[contains(@class,'rich-sdt-fb')]//*[contains(@class,'dataTableRow')]";
        private const string secondHalfRowsXPath = @".//ancestor::*[contains(@class,'rich-sdt-nb')]//*[contains(@class,'dataTableRow')]";
        private const string secondHalfRowCellsXPath = @".//td[contains(@id, 'form:dataTable')]";
        private const string firstHalfRowCellsXPath = secondHalfRowCellsXPath + "//*[contains(@class, 'dataTableColumnStyle')]";
        private const string columnsXPath = @".//*[contains(@class,'rich-sdt-inlinebox')][1]//*[contains(@class,'dataTableColumnSt')]";
        private const string ascendingColumnXpath = @".//*[contains(@src, 'sort_arrow_up')]/..";
        private const string descendingColumnXpath = @".//*[contains(@src, 'sort_arrow_down')]/..";
        private string columnMenuXpath = null;

        IWebElement webElement;

        public RichSDTGrid(IWebElement webElement)
        {
            this.webElement = webElement;
        }
        public string ActionRowsXpath
        {
            get
            {
                return ".//ancestor::*[contains(@class,'rich-sdt-fb')]//*[contains(@class,'dataTableRow')]//td[1]";
            }
        }
        public string ModifyButtonXpath
        {
            get
            {
                return ".//*[contains(@id, 'modify')]";
            }
        }
        public string ColumnMenuXpath
        {
            get { return columnMenuXpath; }
        }
        public IWebElement GetCellElement(string columnHeaderName, string rowNumber)
        {
            //Get rows cells
            List<IWebElement> rowCells = new List<IWebElement>();

            var columns = webElement.FindElements(By.XPath(columnsXPath)).GetWebElementsValues();

            var columnHeaderIndex = columns.IndexOf(columns.Where(column => column == columnHeaderName).FirstOrDefault());


            if (columnHeaderIndex < 0) throw new Exception(String.Format("Column {0} not found.", columnHeaderName));


            var firstHalfRow = webElement.FindElements(By.XPath(firstHalfRowsXPath))[int.Parse(rowNumber) - 1];
            rowCells.AddRange(firstHalfRow.FindElements(By.XPath(firstHalfRowCellsXPath)));


            var secondHalfRow = webElement.FindElements(By.XPath(secondHalfRowsXPath))[int.Parse(rowNumber) - 1];
            rowCells.AddRange(secondHalfRow.FindElements(By.XPath(secondHalfRowCellsXPath)));

            new DlkBaseControl("Cell", rowCells[columnHeaderIndex]).ScrollIntoViewUsingJavaScript();

            return rowCells[columnHeaderIndex];
        }
        public IWebElement GetCellElementByContent(string columnHeaderName, string cellContent)
        {
            List<IWebElement> rows = new List<IWebElement>();
            IWebElement rowCell = null;

            var columns = webElement.FindElements(By.XPath(columnsXPath)).GetWebElementsValues();

            var columnHeaderIndex = columns.IndexOf(columns.Where(column => column == columnHeaderName).FirstOrDefault());


            if (columnHeaderIndex == -1) throw new Exception(String.Format("Column {0} not found.", columnHeaderName));


            var firstHalfRow = webElement.FindElements(By.XPath(firstHalfRowsXPath)).Select((webElement, webElementIndex) => new { RowNumber = webElementIndex, RowElement = webElement });

            var secondHalfRow = webElement.FindElements(By.XPath(secondHalfRowsXPath)).Select((webElement, webElementIndex) => new { RowNumber = webElementIndex, RowElement = webElement });

            var firstHalfAndSecondHalfRow = from firstHalf in firstHalfRow
                                            join seconfHalf in secondHalfRow on firstHalf.RowNumber equals seconfHalf.RowNumber
                                            select new
                                            {
                                                columnsElement = firstHalf.RowElement.FindElements(By.XPath(firstHalfRowCellsXPath))
                                                .Concat(seconfHalf.RowElement.FindElements(By.XPath(secondHalfRowCellsXPath))).ToList()
                                            };
            foreach (var row in firstHalfAndSecondHalfRow)
            {
                new DlkBaseControl("row", row.columnsElement[columnHeaderIndex]).ScrollIntoViewUsingJavaScript();
                if (row.columnsElement[columnHeaderIndex].Text.Trim() == cellContent)
                    rowCell = row.columnsElement[columnHeaderIndex];
            }

            if (rowCell == null)
                return rowCell;

            new DlkBaseControl("Cell", rowCell).ScrollIntoViewUsingJavaScript();

            return rowCell;
        }

        public int GetCellRowNumber(string columnHeaderName, string cellContent)
        {
            int result = 0;
            var columns = webElement.FindElements(By.XPath(columnsXPath));

            var columnHeaderIndex = columns.IndexOf(columns.Where(column => {
                column.Click();
                return column.Text.Trim() == columnHeaderName;
            }).FirstOrDefault());

            if (columnHeaderIndex == -1) throw new Exception(String.Format("Column {0} not found.", columnHeaderName));
            
            //Combine rows with differing xpaths (First column and Normal columns)
            var firstHalfRow = webElement.FindElements(By.XPath(firstHalfRowsXPath)).Select((webElement, webElementIndex) => new { RowNumber = webElementIndex, RowElement = webElement });
            var secondHalfRow = webElement.FindElements(By.XPath(secondHalfRowsXPath)).Select((webElement, webElementIndex) => new { RowNumber = webElementIndex, RowElement = webElement });
            var firstHalfAndSecondHalfRow = from firstHalf in firstHalfRow
                                            join seconfHalf in secondHalfRow on firstHalf.RowNumber equals seconfHalf.RowNumber
                                            select new
                                            {
                                                columnsElement = firstHalf.RowElement.FindElements(By.XPath(firstHalfRowCellsXPath)).Concat(seconfHalf.RowElement.FindElements(By.XPath(secondHalfRowCellsXPath))).ToList()
                                            };

            int rowNumber = 0;
            foreach (var row in firstHalfAndSecondHalfRow)
            {
                rowNumber++;
                if (row.columnsElement[columnHeaderIndex].Text.Trim() == cellContent)
                {
                    result = rowNumber;
                    break;
                }
            }
            return result;
        }
        
        public IList<List<IWebElement>> GetRows()
        {
            IList<List<IWebElement>> rows = new List<List<IWebElement>>();

            //Combine rows with differing xpaths (First column and Normal columns)
            var firstHalfRow = webElement.FindElements(By.XPath(firstHalfRowsXPath)).Select((webElement, webElementIndex) => new { RowNumber = webElementIndex, RowElement = webElement });
            var secondHalfRow = webElement.FindElements(By.XPath(secondHalfRowsXPath)).Select((webElement, webElementIndex) => new { RowNumber = webElementIndex, RowElement = webElement });
            var firstHalfAndSecondHalfRow = from firstHalf in firstHalfRow
                                            join seconfHalf in secondHalfRow on firstHalf.RowNumber equals seconfHalf.RowNumber
                                            select new
                                            {
                                                columnsElement = firstHalf.RowElement.FindElements(By.XPath(firstHalfRowCellsXPath)).Concat(seconfHalf.RowElement.FindElements(By.XPath(secondHalfRowCellsXPath))).ToList()
                                            };

            foreach (var row in firstHalfAndSecondHalfRow)
                rows.Add(row.columnsElement);

            return rows;
        }

        public List<IWebElement> GetGridHeaders()
        {
            return webElement.FindElements(By.XPath(".//*[contains(@class,'dataTableColumnHeader')]//*[contains(@class,'dataTableColumnStyleHeader')]")).ToList();
        }
        public List<IWebElement> GetGridValues()
        {
            // Pending: Correct xpath for grid values - since implementation in DlkGrid is the priority for now
            return webElement.FindElements(By.XPath(".//*[contains(@class,'dataTableColumnHeader')]//*[contains(@class,'dataTableColumnStyleHeader')]")).ToList();
        }

        public int GetRowCount()
        {
            return webElement.FindElements(By.XPath(firstHalfRowsXPath)).Count();
        }
        public void SelectRow(string rowNumber)
        {
            var row = webElement.FindElements(By.XPath(firstHalfRowsXPath));
            int rowIndex = (int.Parse(rowNumber) - 1);
            if (rowIndex < 0) throw new Exception("Row no. must be greater than 0");
            if (row.Count > 0 && row.Count > rowIndex) row[rowIndex].ClickUsingJS();
            else throw new Exception("Row no. " + rowNumber + " cannot find");
        }

        public IWebElement GetHeaderElement(string columnHeaderName, IWebElement element)
        {
            Thread.Sleep(1000);
            var column = element.FindElements(By.XPath(columnsXPath)).FirstOrDefault(item => item.Text.Trim() == columnHeaderName);
            if (column != null)
                new DlkBaseControl("Header", column).ScrollIntoViewUsingJavaScript();
            else
                throw new Exception(string.Format("Column '{0}' not found.", columnHeaderName));

            return column;
        }

        public List<IWebElement> GetSortedColumnCount(IWebElement element, bool isAscending)
        {
            return element.FindElements(By.XPath((isAscending ? ascendingColumnXpath : descendingColumnXpath))).ToList();
        }

        public void HoverOnColumnHeader(string columnHeaderName)
        {
            throw new NotImplementedException("HoverOnColumnHeader() not implemented on SFT version 1.");
        }

        public String GetGridCellText(IWebElement cell)
        {
            throw new NotImplementedException("GetGridCellText() not implemented on SFT version 1.");
        }

    }
}
