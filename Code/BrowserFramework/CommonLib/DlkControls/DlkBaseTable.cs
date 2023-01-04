using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Base class for tables
    /// </summary>
    public class DlkBaseTable : DlkBaseControl
    {
        /// <summary>
        /// the table rows
        /// </summary>
        public IList<IWebElement> TableRows
        {
            get
            {
                Initialize();
                return _ElmTableRows;
            }
        }
        private IList<IWebElement> _ElmTableRows;

        /// <summary>
        /// table row count
        /// </summary>
        public int RowCount
        {
            get
            {
                Initialize();
                return _ElmTableRows.Count;
            }
        }

        /// <summary>
        /// the table path
        /// </summary>
        public String mTablePath = "";

        /// <summary>
        /// the table path list
        /// </summary>
        public List<String> mTablePathList;


        public DlkBaseTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
            _ElmTableRows = new List<IWebElement>();
        }
        public DlkBaseTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            _ElmTableRows = new List<IWebElement>();
        }
        public DlkBaseTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            _ElmTableRows = new List<IWebElement>();
            Initialize();
        }

        /// <summary>
        /// Finds the table and populates the table rows
        /// </summary>
        public void Initialize()
        {
            if ((_ElmTableRows == null) || (_ElmTableRows.Count < 1))
            {
                RefreshTableData();
            }
        }

        /// <summary>
        /// populates the table row data properties
        /// </summary>
        public void RefreshTableData()
        {
            FindElement(); // find the table          
            mTablePath = GetPath();
            mTablePathList = mTablePath.Split('>').ToList();
            _ElmTableRows.Clear();
            IList<IWebElement> mElmTableRowsTmp = mElement.FindElements(By.TagName("tr"));
            for (int i = 0; i < mElmTableRowsTmp.Count; i++)
            {
                if (IsRowImmediateChild(mElmTableRowsTmp[i]))
                {
                    _ElmTableRows.Add(mElmTableRowsTmp[i]);
                }
            }
        }

        /// <summary>
        /// Used to determine if the row belongs to the table in question or is child to another table
        //  Note - don't initialize - this is called from RefreshTableData -- that would cause an infinite loop
        /// </summary>
        /// <param name="TableRow"></param>
        /// <returns></returns>
        private Boolean IsRowImmediateChild(IWebElement TableRow)
        {
            Boolean bResult = true;

            List<String> mParentPath = mTablePathList;
            List<String> mChildPath = GetPathAsList(TableRow, false);

            for (int i = 0; i < mChildPath.Count; i++)
            {
                if (i >= mParentPath.Count)
                {
                    if (mChildPath[i].ToLower() == "table")
                    {
                        bResult = false;
                        break;
                    }
                }
            }
            return bResult;
        }

        /// <summary>
        /// Clicks a cell
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iCol"></param>
        public void ClickCell(int iRow, int iCol)
        {
            Initialize();
            IWebElement TableRow = TableRows[iRow];
            IList<IWebElement> mElmCells = TableRow.FindElements(By.TagName("td"));
            List<IWebElement> mVisibleCells = new List<IWebElement>();

            foreach (IWebElement mTmpElm in mElmCells)
            {
                if (mTmpElm.Displayed)
                {
                    mVisibleCells.Add(mTmpElm);
                }
            }
            mVisibleCells[iCol].Click();
            Thread.Sleep(DlkEnvironment.mShortWaitMs);
            DlkLogger.LogInfo("Successfully clicked cell. Row: " + iRow.ToString() + ", Col: " + iCol.ToString());
        }

        /// <summary>
        /// Dumps the table data
        /// </summary>
        public void DumpTable()
        {
            Initialize();
            for (int i = 0; i < TableRows.Count; i++)
            {
                List<String> rData = GetRowData(i, true);
                DlkLogger.LogData("Table: " + mControlName + ", Row: " + i.ToString() + ", " + String.Join("~", rData));
            }
        }

        /// <summary>
        /// Get the data for the row whether displayed or not. To view the displayed row data only, you might want to use DlkTable.GetDisplayedRowData(),
        /// which has the same function as this method
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="IncludeInputValues"></param>
        /// <returns></returns>
        public List<String> GetRowData(int iRow, Boolean IncludeInputValues)
        {
            Initialize();
            List<String> rowData = new List<String>();
            IWebElement TableRow = TableRows[iRow];
            IList<IWebElement> mElmCells = TableRow.FindElements(By.TagName("td"));
            if ((mElmCells == null) || (mElmCells.Count < 1))
            {
                mElmCells = TableRow.FindElements(By.TagName("th"));
            }
            foreach (IWebElement mCell in mElmCells)
            {
                String mCellVal = mCell.Text.Trim();
                if (IncludeInputValues)
                {
                    if (mCellVal == "")
                    {
                        try
                        {
                            IWebElement mInputElm;
                            mInputElm = mCell.FindElement(By.TagName("input"));
                            mCellVal = mInputElm.GetAttribute("value");
                        }
                        catch
                        {
                            // nothing
                        }
                    }
                }
                rowData.Add(mCellVal);
            }
            return rowData;
        }

        /// <summary>
        /// Get the cell data
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iCol"></param>
        /// <param name="IncludeInputValues"></param>
        /// <returns></returns>
        public String GetCellData(int iRow, int iCol, Boolean IncludeInputValues)
        {
            Initialize();
            List<String> rowData = GetRowData(iRow, IncludeInputValues);
            return rowData[iCol].Trim();
        }

        public void LogColumnData(int iCol, String Msg)
        {
            Initialize();
            List<String> mData = new List<String>();
            for (int i = 0; i < _ElmTableRows.Count; i++)
            {
                String mCell = GetCellData(i, iCol,true);
                mData.Add(mCell);
            }
            DlkLogger.LogData(Msg, mData);
        }

        public int GetRowByKey(int iCol, String MatchValue)
        {

            int iRow = -1;

            for (int i = 0; i < _ElmTableRows.Count; i++)
            {
                List<String> rData = GetRowData(i, true);
                if (rData.Contains(MatchValue))
                {
                    if (rData[iCol].Contains(MatchValue))
                    {
                        iRow = i;
                        break;
                    }
                }
            }
            return iRow;
        }
        public IWebElement GetRow(int iRow)
        {
            try
            {
                IWebElement mRow = mElement.FindElement(By.CssSelector("table.bodyTable>tbody>tr:not([class~='hide']):nth-child(" + (iRow + 1).ToString() + ")"));
                return mRow;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return null;
            }
        }
    }
}

