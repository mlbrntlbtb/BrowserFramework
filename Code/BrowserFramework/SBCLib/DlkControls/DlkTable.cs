using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using System.Collections;

namespace SBCLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region Constructors
        public DlkTable(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region DECLARATIONS
        private Boolean IsInit;
        private string strHeadersXPath = ".//thead//th";
        private IList<IWebElement> lstColHeaders;
        private string mCellType = "default";
        #endregion

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
            InitializeColumnHeaders();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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

        /// <summary>
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), base.IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks the column header with the specified name or index
        /// </summary>
        [Keyword("ClickColumnHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickColumnHeader(String NameOrIndex)
        {
            try
            {
                Initialize();
                IWebElement header = null;
                if (int.TryParse(NameOrIndex, out int colIndex)) {
                   header = lstColHeaders.ElementAt(colIndex - 1);
                } else {
                    header = lstColHeaders.Where(x => DlkString.RemoveCarriageReturn(x.Text).Trim(' ').Equals(NameOrIndex)).FirstOrDefault();
                }
                if (header == null) throw new Exception($"Header [{NameOrIndex}] not found");
                header.Click();
                DlkLogger.LogInfo("ClickColumnHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickColumnHeader() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks links inside a cell. Requires row/column number to be integers
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("ClickCellLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellLink(String Row, String ColumnNumber, String LinkText)
        {
            try
            {
                Initialize();
                IWebElement cell = GetCell(Row, ColumnNumber);
                IWebElement link = cell.FindElements(By.XPath($".//a[contains(.,'{LinkText}')]")).FirstOrDefault();
                if(link == null)
                {
                    throw new Exception($"Link [{LinkText}] not found");
                }
                else
                {
                    link.Click();
                    DlkLogger.LogInfo("ClickCellLink() passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellLink() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks links inside a cell in a child table. Requires row/column number to be integers
        /// </summary>
        [Keyword("ClickChildTableCellLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickChildTableCellLink(String RowWithTable, String Row, String ColumnNumber, String LinkText)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowWithTable, out int rowWithTable)) throw new Exception($"Row: [{RowWithTable}] is not a valid integer input.");
                // Get child table
                IWebElement table = mElement.FindWebElementCoalesce(By.XPath($".//tbody/tr[{rowWithTable}]//table"), By.XPath($".//tr[not(contains(@class,'header'))][{rowWithTable}]//table"));
                if (table == null) throw new Exception($"No child table was found at row [{RowWithTable}]");
                DlkLogger.LogInfo("Child Table found...");
                DlkTable childTable = new DlkTable("ChildTable", table);
                childTable.ClickCellLink(Row, ColumnNumber, LinkText);
                DlkLogger.LogInfo("ClickChildTableCellLink() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickChildTableCellLink() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks a button inside a cell. Requires row/column number to be integers
        /// </summary>
        [Keyword("ClickCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellButton(String Row, String ColumnNumber)
        {
            try
            {
                Initialize();
                IWebElement cell = GetCell(Row, ColumnNumber);
                IWebElement button = cell.FindWebElementCoalesce(By.XPath(".//span[contains(@class,'Icon')]"),By.XPath(".//input[@type='button']"), By.XPath(".//a"));
                if (button == null) throw new Exception($"Button not found");
                
                button.Click();
                DlkLogger.LogInfo("ClickCellButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellButton() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks a button inside a cell with a given name. Requires row to be integer
        /// </summary>
        [Keyword("ClickButtonName", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickButtonName(String Row, String ButtonName)
        {
            try
            {
                Initialize();
                IWebElement row = GetRow(Row);
                IWebElement button = row.FindWebElementCoalesce(By.XPath($".//span[contains(@class,'Icon')][contains(.,'{ButtonName}')]"), 
                    By.XPath($".//input[@type='button'][contains(.,'{ButtonName}')]"),
                    By.XPath($".//button[contains(.,'{ButtonName}')]"));
                if (button == null) throw new Exception($"Button not found");

                button.Click();
                DlkLogger.LogInfo("ClickButtonName() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickButtonName() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks a button inside a cell in a child table. Requires row/column number to be integers
        /// </summary>
        [Keyword("ClickChildTableCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickChildTableCellButton(String RowWithTable, String Row, String ColumnNumber)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowWithTable, out int rowWithTable)) throw new Exception($"Row: [{RowWithTable}] is not a valid integer input.");
                // Get child table
                IWebElement table = mElement.FindWebElementCoalesce(By.XPath($".//tbody/tr[{rowWithTable}]//table"), By.XPath($".//tr[not(contains(@class,'header'))][{rowWithTable}]//table"));
                if (table == null) throw new Exception($"No child table was found at row [{RowWithTable}]");
                DlkLogger.LogInfo("Child Table found...");
                DlkTable childTable = new DlkTable("ChildTable", table);
                childTable.ClickCellButton(Row, ColumnNumber);
                DlkLogger.LogInfo("ClickChildTableCellButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickChildTableCellButton() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets the value of a cell
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="ColumnNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("SetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValue(String Row, String ColumnNumber, String Value)
        {
            try
            {
                Initialize();
                string ActValue = string.Empty;
                IWebElement cell = GetCell(Row, ColumnNumber);
                switch (mCellType)
                {
                    case "textbox":
                        cell = cell.FindElement(By.TagName("input"));
                        new DlkTextBox("Textbox", cell).Set(Value);
                        break;
                    case "checkbox":
                        cell = cell.FindElement(By.TagName("input"));
                        new DlkCheckBox("Checkbox", cell).SetValue(Value);
                        break;
                    default:
                        throw new Exception("This cell type is currently unsupported.");
                }
                DlkLogger.LogInfo("SetCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets the value of a cell in a child table
        /// </summary>
        [Keyword("SetChildTableCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetChildTableCellValue(String RowWithTable, String Row, String ColumnNumber, String Value)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowWithTable, out int rowWithTable)) throw new Exception($"Row: [{RowWithTable}] is not a valid integer input.");
                // Get child table
                IWebElement table = mElement.FindWebElementCoalesce(By.XPath($".//tbody/tr[{rowWithTable}]//table"), By.XPath($".//tr[not(contains(@class,'header'))][{rowWithTable}]//table"));
                if (table == null) throw new Exception($"No child table was found at row [{RowWithTable}]");
                DlkLogger.LogInfo("Child Table found...");
                DlkTable childTable = new DlkTable("ChildTable", table);
                childTable.SetCellValue(Row,ColumnNumber,Value);                
                DlkLogger.LogInfo("SetChildTableCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetChildTableCellValue() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Gets the value of the cell and assigns it to a variable
        /// </summary>
        [Keyword("GetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetCellValue(String Row, String ColumnNumber, string VariableName)
        {
            try
            {
                Initialize();
                IWebElement cell = GetCell(Row, ColumnNumber);
                string ActValue = GetTargetCellValue(cell);
                DlkVariable.SetVariable(VariableName, ActValue);
                DlkLogger.LogInfo($"GetCellValue() passed. Variable:[{VariableName}], Value:[{ActValue}]");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the value of a cell
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="ColumnNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String Row, String ColumnNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement cell = GetCell(Row, ColumnNumber);
                if (mCellType.Equals("checkbox") || mCellType.Equals("image")) { ExpectedValue = ExpectedValue.ToLower(); }
                DlkAssert.AssertEqual("VerifyCellValue() : ", ExpectedValue, GetTargetCellValue(cell));
                DlkLogger.LogInfo("VerifyCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets the value of a cell in a child table
        /// </summary>
        [Keyword("VerifyChildTableCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyChildTableCellValue(String RowWithTable, String Row, String ColumnNumber, String Value)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowWithTable, out int rowWithTable)) throw new Exception($"Row: [{RowWithTable}] is not a valid integer input.");
                // Get child table
                IWebElement table = mElement.FindWebElementCoalesce(By.XPath($".//tbody/tr[{rowWithTable}]//table"), By.XPath($".//tr[not(contains(@class,'header'))][{rowWithTable}]//table"));
                if (table == null) throw new Exception($"No child table was found at row [{RowWithTable}]");
                DlkLogger.LogInfo("Child Table found...");
                DlkTable childTable = new DlkTable("ChildTable", table);
                childTable.VerifyCellValue(Row, ColumnNumber, Value);
                DlkLogger.LogInfo("VerifyChildTableCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChildTableCellValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Gets the row index of the cell with the matching value given the column number
        /// </summary>
        [Keyword("GetRowWithColumnValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowWithColumnValue(string ColumnNumber, string Value, string VariableName)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(ColumnNumber, out int colnum)) throw new Exception($"Column: [{ColumnNumber}] is not a valid integer input.");
                Boolean bFound = false;
                int rowCount = mElement.FindWebElementsCoalesce(false, By.XPath($".//tbody/tr"), By.XPath($".//tr[not(contains(@class,'header'))]")).Count;
                for (int i =1; i <= rowCount; i++)
                {
                    IWebElement cell = GetCell(i.ToString(), ColumnNumber);
                    string ActValue = new DlkBaseControl("Cell", cell).GetValue();
                    if (ActValue.Equals(Value))
                    {
                        DlkVariable.SetVariable(VariableName,i.ToString());
                        DlkLogger.LogInfo("GetRowWithColumnValue() passed");
                        bFound = true;
                    }
                }
                if(!bFound) { throw new Exception("No matching cell was found."); };
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnValue() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Gets table row count then assigns it to a variable
        /// </summary>
        [Keyword("GetRowCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowCount(string VariableName) 
        {
            try
            {
                Initialize();
                int tableRowCount = CountRow();
                DlkVariable.SetVariable(VariableName, tableRowCount.ToString());
                DlkLogger.LogInfo($"GetRowCount() passed. Variable:[{VariableName}], Value:[{tableRowCount.ToString()}]");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies row count of a table then compares it to the expected row count parameter
        /// </summary>
        [Keyword("VerifyRowCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRowCount(string ExpectedRowCount) 
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyRowCount() : " + mControlName, CountRow(), Convert.ToInt32(ExpectedRowCount));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }


        #endregion

        #region Private Methods
        private void InitializeColumnHeaders()
        {
            lstColHeaders = mElement.FindElements(By.XPath(strHeadersXPath));
        }

        private IWebElement GetCell(String Row, String ColIndex)
        {
            //Guard clauses
            if (!Int32.TryParse(Row, out int row)) throw new Exception($"Row: [{Row}] is not a valid integer input.");
            if (!Int32.TryParse(ColIndex, out int colIndex)) throw new Exception($"ColumnNumber: [{ColIndex}] is not a valid integer input.");

            IWebElement cell = null;
            cell = mElement.FindWebElementCoalesce(
                By.XPath($".//tbody/tr[{row}]/td[{colIndex}]"), 
                By.XPath($".//tr[not(contains(@class,'header'))][{row}]/td[{colIndex}]"),
                By.XPath($"./table[{row}][contains(@class, 'taskGrid')]/tbody/tr[1]/td[{colIndex}]")
                );
            if (cell == null) throw new Exception($"Cannot find cell with Row[{Row}] and Column[{ColIndex}].");

            if(cell.FindElements(By.XPath(".//input")).Count > 0) {
                mCellType =  cell.FindElement(By.TagName("input")).GetAttribute("type").ToLower().Equals("checkbox") ? "checkbox" : "textbox";
                DlkLogger.LogInfo($"Current cell is a [{ mCellType }] type.");
            }else if(cell.FindElements(By.TagName("img")).Count > 0){
                mCellType = "image";
            }
            return cell;
        }

        private IWebElement GetRow(String Row)
        {
            //Guard clauses
            if (!Int32.TryParse(Row, out int row)) throw new Exception($"Row: [{Row}] is not a valid integer input.");
            
            IWebElement mRow = null;
            mRow = mElement.FindWebElementCoalesce(
                By.XPath($".//tbody/tr[{row}]"),
                By.XPath($".//tr[not(contains(@class,'header'))][{row}]"),
                By.XPath($"./table[{row}][contains(@class, 'taskGrid')]/tbody/tr[1]")
                );
            if (mRow == null) throw new Exception($"Cannot find Row[{Row}].");

            return mRow;
        }

        private int CountRow()
        {
            Initialize();
            int tableRowCount = mElement.FindElements(By.XPath(".//tbody/tr")).Count();
            return tableRowCount;
        }

        private String GetTargetCellValue(IWebElement cell)
        {
            string ret = string.Empty;
            switch (mCellType)
            {
                case "textbox":
                    cell = cell.FindElement(By.TagName("input"));
                    ret = new DlkBaseControl("Cell", cell).GetValue();
                    break;
                case "checkbox":
                    cell = cell.FindElement(By.TagName("input"));
                    ret = new DlkCheckBox("Checkbox", cell).GetCheckedState().ToString().ToLower();
                    break;
                case "image":
                    cell = cell.FindElement(By.TagName("img"));
                    bool IsBoxType = cell.GetAttribute("src").Contains("Box_");
                    ret = IsBoxType ?
                        cell.GetAttribute("src").ToLower().Contains("check").ToString().ToLower() :
                        cell.GetAttribute("class").ToLower().Contains("successful").ToString().ToLower();
                    break;
                default:
                    ret = new DlkBaseControl("Cell", cell).GetValue();
                    break;
            }
            return ret;
        }
        #endregion
    }
 }

