using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WebTimeClockLib.DlkSystem;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace WebTimeClockLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES

        private static string mColumnContainer_XPath = ".//tr[@class='gridHeaderRow']";
        private static string mColumnHeaders_XPath = ".//th[@class='gridHeaderCell']";
        private static string mRows_XPath = ".//tr[not(contains(@class,'gridHeaderRow'))]";
        private static string mCells_XPath = ".//td[@class='gridCell']";

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
        }

        public IWebElement GetCell(string rowIndex, string colNameOrIndex)
        {
            IWebElement targetRow = GetTargetRow(rowIndex);
            int targetColumnNumber = GetColumnIndexFromName(colNameOrIndex);

            IList<IWebElement> targetCells = GetAllCells(targetRow);
            IWebElement targetCell = targetCells.ElementAt(targetColumnNumber) != null ?
               targetCells.ElementAt(targetColumnNumber) : throw new Exception("Cell not found.");

            return targetCell;
        }
        public string GetCellValue(string rowIndex, string colNameOrIndex)
        {
            IWebElement targetCell = GetCell(rowIndex, colNameOrIndex);
            string cellValue = new DlkBaseControl("Target Cell", targetCell).GetValue().Trim();
            return cellValue;
        }

        #endregion

        #region PRIVATE METHODS

        private IList<IWebElement> GetColumnHeaders()
        {
            IWebElement mColumnContainer = mElement.FindElements(By.XPath(mColumnContainer_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mColumnContainer_XPath)) : throw new Exception("Column header container not found.");
            IList<IWebElement> mColumnHeaders = mColumnContainer.FindElements(By.XPath(mColumnHeaders_XPath)).Count > 0 ?
                mColumnContainer.FindElements(By.XPath(mColumnHeaders_XPath)).Where(x => x.Displayed).ToList() : throw new Exception("Column headers not found.");

            return mColumnHeaders;
        }

        private IList<IWebElement> GetRows()
        {
            IList<IWebElement> mRows = mElement.FindElements(By.XPath(mRows_XPath)).Count > 0 ?
                  mElement.FindElements(By.XPath(mRows_XPath)).Where(x => x.Displayed).ToList() : throw new Exception("Rows not found.");

            return mRows;
        }

        private IList<IWebElement> GetAllCells(IWebElement targetRow)
        {
            IList<IWebElement> mCells = targetRow.FindElements(By.XPath(mCells_XPath)).Count > 0 ?
                targetRow.FindElements(By.XPath(mCells_XPath)).Where(x => x.Displayed).ToList() : throw new Exception("Cells not found.");

            return mCells;
        }

        private IWebElement GetTargetRow(string rowIndex)
        {
            int targetRowNumber = Convert.ToInt32(rowIndex) - 1;
            IList<IWebElement> AllRows = GetRows();

            return AllRows.ElementAt(targetRowNumber);
        }

        private int GetColumnIndexFromName(string columnName)
        {
            IList<IWebElement> mColumnHeaders = GetColumnHeaders();

            int index = -1;
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                if (mColumnHeaders[i].Text.Trim().ToLower() == columnName.ToLower())
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private IWebElement GetTargetColumn(string colNameOrIndex)
        {
            int targetColumnNumber = 0;
            if (!int.TryParse(colNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(colNameOrIndex);
            else
                targetColumnNumber = Convert.ToInt32(colNameOrIndex) - 1;

            if (targetColumnNumber == -1)
                throw new Exception("Column '" + colNameOrIndex + "' was not found.");

            IList<IWebElement> mColumnHeaders = GetColumnHeaders();
            IWebElement targetColumn = mColumnHeaders.ElementAt(targetColumnNumber);
            return targetColumn;
        }

        #endregion

        #region KEYWORDS
        
        [Keyword("GetCellValue")]
        public void GetCellValue(String RowIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                string actualValue = GetCellValue(RowIndex, ColNameOrIndex);
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowWithColumnValue(String ColNameOrIndex, String Value, String VariableName)
        {
            try
            {
                Initialize();
                bool rFound = false;

                int rowCount = GetRows().Count;

                for (int r = 1; r <= rowCount; r++)
                {
                    string actualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (actualValue.ToLower() == Value.ToLower())
                    {
                        DlkVariable.SetVariable(VariableName, r.ToString());
                        DlkLogger.LogInfo("[" + r.ToString() + "] value set to Variable: [" + VariableName + "]");
                        rFound = true;
                        break;
                    }
                }

                if (!rFound)
                    throw new Exception("Row not found with value [" + Value + "]");

                DlkLogger.LogInfo("GetRowWithColumnValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowExistWithColumnValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRowExistWithColumnValue(String ColNameOrIndex, String Value, String ExpectedValue)
        {
            try
            {
                Initialize();
                bool expectedValue;
                if (!Boolean.TryParse(ExpectedValue, out expectedValue))
                    throw new Exception("[" + ExpectedValue + "] is not a valid input for parameter ExpectedValue.");

                bool rowExist = false;

                int rowCount = GetRows().Count;

                for (int r = 1; r <= rowCount; r++)
                {
                    string actualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (actualValue.ToLower() == Value.ToLower())
                    {
                        rowExist = true;
                        DlkLogger.LogInfo("Value [" + Value + "] from Column [" + ColNameOrIndex + "] found at Row [" + r.ToString() + "]");
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyRowExistWithColumnValue(): ", expectedValue, rowExist);
                DlkLogger.LogInfo("VerifyRowExistWithColumnValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowExistWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue")]
        public void VerifyCellValue(String RowIndex, String ColNameOrIndex, String ExpectedValue)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                string actualValue = GetCellValue(RowIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellValue() :", ExpectedValue, actualValue);
                DlkLogger.LogInfo("VerifyCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellHasValue")]
        public void GetCellHasValue(String RowIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                string hasValue = "False";

                Initialize();
                string actualValue = GetCellValue(RowIndex, ColNameOrIndex);

                if (!String.IsNullOrEmpty(actualValue))
                {
                    hasValue = "True";
                }

                DlkVariable.SetVariable(VariableName, hasValue);
                DlkLogger.LogInfo("[" + hasValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellHasValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellHasValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellHasValue")]
        public void VerifyCellHasValue(String RowIndex, String ColNameOrIndex, String TrueOrFalse)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                bool hasValue = false;

                Initialize();
                string actualValue = GetCellValue(RowIndex, ColNameOrIndex);

                if (!String.IsNullOrEmpty(actualValue))
                    hasValue = true;

                DlkAssert.AssertEqual("VerifyCellHasValue() :", Convert.ToBoolean(TrueOrFalse), hasValue);
                DlkLogger.LogInfo("VerifyCellHasValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellHasValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
