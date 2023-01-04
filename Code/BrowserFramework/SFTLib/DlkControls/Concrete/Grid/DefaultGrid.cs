using OpenQA.Selenium;
using SFTLib.DlkControls.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using SFTLib.DlkSystem;
using System.Threading;

namespace SFTLib.DlkControls.Concrete.Grid
{
    public class DefaultGrid : IGrid
    {
        private const string columnsXPath = @".//*[contains(@class,'x-column-header-inner')]";
        private const string secondHalfRowsXPath = @".//*[contains(@class,'x-grid-with-col-lines')][last()]//tbody[last()]//tr[contains(@class,'x-grid-row')]";
        //private const string firstHalfRowsXPath = @".//*[contains(@class,'x-grid-with-col-lines')][1]//tbody//tr[contains(@class,'x-grid-row')]";
        private const string areTwoPartsIdentifier = @".//*[contains(@class,'x-grid-with-col-lines')]//tbody";
        private const string gridRows = @".//tr[contains(@class,'x-grid-row')]";
        private const string notDescendentXPath = @".//*[contains(@class,'x-grid-cell-inner')]";//".//*[not(descendant::*)]";
        private const string gridValuesXpath = @".//descendant::tbody[2]//div[@class='x-grid-cell-inner']";
        private const string ascendingColumnXpath = @".//ancestor::div[contains(@class, 'column-header-sort-ASC')]";
        private const string descendingColumnXpath = @".//ancestor::div[contains(@class, 'column-header-sort-DESC')]";
        private string columnMenuXpath = ".//self::*[contains(@class, 'header-over')]//*[contains(@class, 'column-header-trigger')]";
        private const string checkboxXpath = "./child::*/child::*[@type='checkbox']";

        IWebElement webElement;
        public DefaultGrid(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public string ActionRowsXpath {
            get
            {
                return "(.//*[contains(@class,'x-grid-with-col-lines')])//img//ancestor::tr";
            }
        }
        public string ModifyButtonXpath
        {
            get
            {
                return ".//*[@data-qtip='Modify']";
            }
        }

        public string ColumnMenuXpath
        {
            get { return columnMenuXpath; }
        }

        public IWebElement GetCellElement(string columnHeaderName, string rowNumber)
        {
            List<IWebElement> rowCells = new List<IWebElement>();

            var columns = webElement.FindElements(By.XPath(columnsXPath)).GetWebElementsValues();
            var columnHeaderIndex = columns.IndexOf(columns.Where(column => column.Trim() == columnHeaderName).FirstOrDefault());

            if (columnHeaderIndex == -1) throw new Exception(String.Format("Column {0} not found.", columnHeaderName));
            
            foreach (var gridBody in webElement.FindElements(By.XPath(areTwoPartsIdentifier)).Where(element => element.Displayed))
            {
                var rows = gridBody.FindElements(By.XPath(gridRows)).Where(row => row.Displayed).ToList()[int.Parse(rowNumber) - 1];
                var column = rows.FindElements(By.XPath(notDescendentXPath));
                var hasActionIcon = column.FirstOrDefault().FindElements(By.XPath(".//img")).ToList().Count() > 0;
                if (!hasActionIcon) // check if the inner value has action icons and exclude it
                    rowCells.AddRange(column);
            }
            
            if (rowCells.Count > columns.Count)
                rowCells.RemoveRange(0, rowCells.Count - columns.Count);
            
            if (DlkEnvironment.mBrowser.ToLower() == "ie")
                new DlkBaseControl("Cell", rowCells[columnHeaderIndex]).ScrollIntoView();
            else
                new DlkBaseControl("Cell", rowCells[columnHeaderIndex]).ScrollIntoViewUsingJavaScript();

            return rowCells[columnHeaderIndex];
        }

        public IWebElement GetCellElementByContent(string columnHeaderName, string cellContent)
        {
            IWebElement rowCell = null;
            int ColumnCountOffset = 0;

            var columns = webElement.FindElements(By.XPath(columnsXPath)).GetWebElementsValues();
            var columnHeaderIndex = columns.IndexOf(columns.Where(column => column.Trim() == columnHeaderName.Trim()).FirstOrDefault());

            if (columnHeaderIndex == -1) throw new Exception(String.Format("Column {0} not found.", columnHeaderName));

            var gridBodies = webElement.FindElements(By.XPath(areTwoPartsIdentifier));

            foreach (var gridBody in gridBodies)
            {
                //skip grid cells that have button controls as those have blank texts and are not covered by the validation in this method
                if (!gridBody.FindElements(By.XPath(gridRows)).All(row => row.Text.Trim() == ""))
                {
                    var rows = gridBody.FindElements(By.XPath(gridRows)).Where(row => row.Displayed).ToList();
                    foreach (var row in rows)
                    {
                         
                        var cellsInRow = row.FindElements(By.XPath(notDescendentXPath));
                        foreach (var cell in cellsInRow)
                        {
                            //check if cell content under selected column matches the expected value
                            if (GetGridCellText(cell) == cellContent && cellsInRow.IndexOf(cell) + ColumnCountOffset == columnHeaderIndex)
                            {
                                //exit loop once there is a return value
                                rowCell = cell;
                                break;
                            }
                        }

                        //Adding the cell count to offset the next grid body on the last Iteration
                        if (rows.Last() == row)
                            ColumnCountOffset += cellsInRow.Count();
                        //exit loop once there is a return value
                        if (rowCell != null)
                            break;
                    }

                    //exit loop once there is a return value
                    if (rowCell != null)
                        break;
                }
            }

            if (rowCell == null)
                return rowCell;

            if (DlkEnvironment.mBrowser.ToLower() == "ie")
                new DlkBaseControl("Cell", rowCell).ScrollIntoView();
            else
                new DlkBaseControl("Cell", rowCell).ScrollIntoViewUsingJavaScript();

            return rowCell;
        }
        public int GetRowCount()
        {
            return webElement.FindElements(By.XPath(secondHalfRowsXPath)).Count();
        }
        public IList<List<IWebElement>> GetRows()
        {
            List<IWebElement> rows = new List<IWebElement>();
            IList<List<IWebElement>> list = new List<List<IWebElement>>();

            foreach (var gridBody in webElement.FindElements(By.XPath(areTwoPartsIdentifier)).Where(element => element.Displayed))
            {
                if (!String.IsNullOrEmpty(gridBody.Text.Trim()))
                    rows.AddRange(gridBody.FindElements(By.XPath(gridRows)).Where(row => row.Displayed).ToList());
            }

            var tempList = from row in rows
                         select new
                         {
                             columnElements = row.FindElements(By.XPath(notDescendentXPath)).ToList()
                         };

            foreach (var item in tempList)
                list.Add(item.columnElements);

            return list;
        }
        public List<IWebElement> GetGridHeaders()
        {
            return webElement.FindElements(By.XPath(columnsXPath)).ToList();
        }

        public List<IWebElement> GetGridValues()
        {
           return webElement.FindElements(By.XPath(gridValuesXpath)).ToList();
        }

        public void SelectRow(string rowNumber)
        {
            var row = webElement.FindElements(By.XPath(secondHalfRowsXPath));
            int rowIndex = (int.Parse(rowNumber) - 1);
            if (rowIndex < 0) throw new Exception("Row no. must be greater than 0");
            if (row.Count > 0 && row.Count > rowIndex) row[rowIndex].ClickUsingJS();
            else throw new Exception("Row no. " + rowNumber + " cannot find");
        }

        public int GetCellRowNumber(string columnHeaderName, string cellContent)
        {
            int result = 0;

            var columns = webElement.FindElements(By.XPath(columnsXPath)).GetWebElementsValues();
            var columnHeaderIndex = columns.IndexOf(columns.Where(column => column.Trim() == columnHeaderName).FirstOrDefault());

            if (columnHeaderIndex == -1) throw new Exception(String.Format("Column {0} not found.", columnHeaderName));

            foreach (var gridBody in webElement.FindElements(By.XPath(areTwoPartsIdentifier)).Where(element => element.Displayed))
            {
                if (String.IsNullOrEmpty(gridBody.Text.Trim()))
                    continue;

                int rowNumber = 0;
                var rows = gridBody.FindElements(By.XPath(gridRows)).Where(row => row.Displayed).ToList();
                foreach (var row in rows)
                {
                    rowNumber++;
                    var  cells = row.FindElements(By.XPath(notDescendentXPath)).ToList();
                    if (cells.Count > columnHeaderIndex && cells[columnHeaderIndex].Text == cellContent)
                    {
                        result = rowNumber;
                        break;
                    }
                }
            }

            return result;
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
            var gridHeader = GetGridHeaders().FirstOrDefault(header => header.Text.Trim() == columnHeaderName);

            if (gridHeader == null)
                throw new Exception(string.Format("Unable to find column {0}", columnHeaderName));

            new DlkBaseControl("Column Header", gridHeader).MouseOver();
                    
            var menuOnHover = gridHeader.FindElement(By.XPath(ColumnMenuXpath));
        }

        public String GetGridCellText(IWebElement cell)
        {
            // Get Value from Text
            if (!String.IsNullOrEmpty(cell.Text))
            {
                return cell.Text;
            }
            // Get Value from Checkbox
            else if (cell.FindElements(By.XPath(checkboxXpath)).Any())
            {
                return (cell.FindElements(By.XPath(checkboxXpath))[0].GetAttribute("checked") == "true").ToString();
            }

            return "";
        }
    }
}
