using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostpointLib.DlkControls
{
    [ControlType("CustomTable")]
    public class DlkCustomTable : DlkBaseControl
    {
        private IList<IWebElement> mRows;
        private IList<IWebElement> mColumns;

        public DlkCustomTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCustomTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCustomTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            GetTableRows();
            GetTableColumns();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellValue", new String[] {"1|text|Row|3",
                                                        "2|text|ColumnHeaderOrIndex|1",
                                                        "3|text|ExpectedValue|Status"})]
        public void VerifyTableCellValue(string Row, string ColumnHeaderOrIndex, string ExpectedValue)
        {
            try
            {
                Initialize();

                int colIndex = GetColumnIndex(ColumnHeaderOrIndex);

                if (!int.TryParse(Row, out int rowIndex))
                {
                    throw new Exception($"Invalid parameter format for Row:'{Row}'"); 
                }

                string actualValue = GetValue(GetCell(colIndex, rowIndex -1, out string controlType), controlType);

                DlkAssert.AssertEqual("VerifyTableCellValue()", ExpectedValue.ToLower(), actualValue.ToLower());
                DlkLogger.LogInfo("Successfully executed VerifyTableCellValue()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTableCellValue", new String[] {"1|text|Row|1",
                                                    "2|text|ColumnHeaderOrIndex|Line",
                                                    "3|text|Value|Sample Value"})]
        public void SetTableCellValue(string Row, string ColumnHeaderOrIndex, string Value)
        {
            try
            {
                Initialize();

                int colIndex = GetColumnIndex(ColumnHeaderOrIndex);

                if (!int.TryParse(Row, out int rowIndex))
                {
                    throw new Exception($"Invalid parameter format for Row:'{Row}'");
                }

                IWebElement targetCell = GetCell(colIndex, rowIndex - 1, out string controlType);
                string currentValue = GetValue(targetCell, controlType);

                switch (controlType)
                {
                    case "checkbox":
                        if (!bool.TryParse(Value, out bool newValue))
                            throw new Exception($"Invalid parameter value for Value: {Value}");

                        if (newValue != bool.Parse(currentValue))
                            targetCell.Click();
                        break;
                    case "label":
                        throw new Exception("Cannot set value to label cell control");
                }
                DlkLogger.LogInfo("Successfully executed SetTableCellValue()");
            }
            catch (Exception e)
            {
                throw new Exception("SetTableCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTableRowWithColumnValue", new String[] {"1|text|Column Header|Line*",
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public void GetTableRowWithColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            try
            {
                Initialize();

                bool found = false;
                int colIndex = GetColumnIndex(ColumnHeader);

                if (colIndex == -1)
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }

                for (int i = 0; i < mRows.Count; i++)
                {
                    var selectedRow = mRows[i];

                    if (selectedRow.Text.Contains(Value))
                    {
                        var targetCol = selectedRow.FindElement(By.XPath($".//div[{colIndex + 1}]"));
                        if (targetCol.Text == Value)
                        {
                            DlkVariable.SetVariable(VariableName, (i+1).ToString());
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    throw new Exception("Value = '" + Value + "' under Column = '" + ColumnHeader + "' not found in table");
                }
                else
                {
                    DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue()");
                }                
            }
            catch (Exception e)
            {
                throw new Exception("GetTableRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTableHeaderValue", new String[] {"1|text|ColumnIndex|1",
                                                    "2|text|Value|Sample Value"})]
        public void SetTableHeaderValue(string ColumnIndex, string Value)
        {
            try
            {
                Initialize();

                if (!int.TryParse(ColumnIndex, out int colIndex))
                {
                    throw new Exception($"Invalid parameter value for ColumnIndex = '{ColumnIndex}'");
                }

                if (!bool.TryParse(Value, out bool expectedValue))
                {
                    throw new Exception($"Invalid parameter value for Value = '{Value}'");
                }

                IWebElement chkControl = mColumns[colIndex - 1].FindElements(By.XPath(".//input")).FirstOrDefault();

                if (chkControl == null)
                {
                    throw new Exception($"Input control not found in column '{colIndex}'");
                }

                string controlType = chkControl.GetAttribute("type");

                switch (controlType)
                {
                    case "checkbox":
                        string chkValue = chkControl.GetAttribute("checked") ?? "false";
                        if (bool.TryParse(chkValue, out bool actualValue))
                        {
                            if (actualValue != expectedValue)
                            {
                                chkControl.Click();
                            }
                            DlkLogger.LogInfo("Successfully executed VerifyTableCellValue()");
                        }
                        else
                        {
                            throw new Exception($"Invalid checkbox value '{chkValue}' for column index '{colIndex}'");
                        }
                        break;
                    default:
                        throw new Exception($"Control type '{controlType}' not supported");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetTableHeaderValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Gets available row in table and stored it on mRows list
        /// </summary>
        private void GetTableRows()
        {
            mRows = new List<IWebElement>();
            IReadOnlyCollection<IWebElement> rows = mElement.FindElements(By.XPath(".//*[@class='tcolRow']"));

            foreach (var row in rows)
            {
                mRows.Add(row);
            }
        }

        /// <summary>
        /// Gets available columns in table and stored it on mColumns list
        /// </summary>
        private void GetTableColumns()
        {
            mColumns = new List<IWebElement>();
            IReadOnlyCollection<IWebElement> columns = mElement.FindElements(By.XPath(".//preceding-sibling::div[@class='tcolTblHead']//div"));

            foreach (var col in columns)
            {
                mColumns.Add(col);
            }
        }

        /// <summary>
        /// Gets the column index of the specified columnheader
        /// </summary>
        /// <param name="columnHeader">Column header of the table</param>
        /// <returns>Column Index</returns>
        private int GetColumnIndex(string columnHeader)
        {
            if (int.TryParse(columnHeader, out int columnIndex))
                return columnIndex - 1;

            for (int i = 0; i < mColumns.Count; i++)
            {
                if (mColumns[i].Text == columnHeader)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets the table cell element of the specified column index and row index
        /// </summary>
        /// <param name="colIndex">Column index</param>
        /// <param name="rowIndex">Row index</param>
        /// <param name="controlType">Control types (checkbox or label)</param>
        /// <returns>Cell element</returns>
        private IWebElement GetCell(int colIndex, int rowIndex, out string controlType)
        {
            try
            {
                IWebElement result = null;
                controlType = "";
                if (mRows.Count - 1 < rowIndex)
                {
                    throw new Exception($"Row {rowIndex + 1} not found.");
                }
                else if (mColumns.Count - 1 < colIndex)
                {
                    throw new Exception($"Column {colIndex + 1} not found.");
                }
                else
                {
                    result = mRows[rowIndex].FindElement(By.XPath($".//div[{colIndex + 1}]")); 
                    new DlkBaseControl("TableCell", result).ScrollIntoViewUsingJavaScript();

                    IWebElement checkbox = result.FindElements(By.XPath(".//input[@type='checkbox']")).FirstOrDefault();
                    if (checkbox != null)
                    {
                        controlType = "checkbox";
                        result = checkbox;
                    }
                    else
                        controlType = "label";
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the value of the specified cell element
        /// </summary>
        /// <param name="targetCell">Cell element of the table</param>
        /// <returns>Value of the cell</returns>
        private string GetValue(IWebElement targetCell, string controlType)
        {
            string result;
            if (controlType == "checkbox")
            {
                result = targetCell.GetAttribute("checked") ?? "false";
            }
            else
            {
                result = targetCell.Text;
            }
            return result;
        }
    }
}
