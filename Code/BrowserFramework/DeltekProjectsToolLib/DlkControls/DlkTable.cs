using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using DeltekProjectsToolLib.DlkSystem;
using System.Linq;

namespace DeltekProjectsToolLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {

        #region CONSTRUCTOR
        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
        }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }
        #endregion

        [Keyword("ClickCell", new String[] {"1|text|Column Header|Line*",
                                           "2|text|Value|1"})]
        public void ClickCell(String Row, String ColumnHeader)
        {
            try
            {
                Initialize();
                IWebElement mCellElm = GetCell(int.Parse(Row), ColumnHeader);
                mCellElm.Click();
                DlkLogger.LogInfo("Successfully executed ClickCell()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Looks for the cell - whole row in this product's case - based on the row index and the column header
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="sColumnHeader"></param>
        /// <returns></returns>
        public IWebElement GetCell(int iRow, string sColumnHeader)
        {
            int iCol = GetColumnIndex(sColumnHeader);
            IWebElement mRow = GetRowElement(iRow);
            if (mRow == null)
            {
                return null;
            }
            else
            {
                return mRow;
            }
        }

        /// <summary>
        /// Determines column index by looking for the specified column header in the list of headers
        /// </summary>
        /// <param name="sColumnHeader">header to look for in the list</param>
        /// <returns>Column index</returns>
        public int GetColumnIndex(string sColumnHeader)
        {
            int iCol = -1;
            List<IWebElement> lstHeaders = mElement.FindElements(By.XPath(".//div[@class='rlbHeader']//td")).ToList();
            for (int idx = 0; idx < lstHeaders.Count; idx++)
            {
                if (lstHeaders[idx].Text.ToLower() == sColumnHeader.ToLower())
                {
                    iCol = idx;
                    break;
                }
            }
            return iCol;
        }

        /// <summary>
        /// Looks for the row element based on specified row index
        /// </summary>
        /// <param name="iRow">Row number</param>
        /// <returns>Row element</returns>
        public IWebElement GetRowElement(int iRow)
        {
            List<IWebElement> lstRows = mElement.FindElements(By.XPath(".//td[@class='rlbGroupCell']//ul//li")).ToList();
            return lstRows.Count > 0 ? lstRows[iRow - 1] : null;
        }
    }
}



