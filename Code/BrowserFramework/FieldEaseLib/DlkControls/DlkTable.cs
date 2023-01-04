using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using FieldEaseLib.DlkSystem;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

namespace FieldEaseLib.DlkControls
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

        private IList<IWebElement> mFilterColumns;
        private IList<IWebElement> mColumns;
        private IList<IWebElement> mRows;
        private IList<IWebElement> mTemp_Rows = null;
        private IWebElement mCellComboBox;
        private string mTableClass;
        private static string tb_RGDATAClass = "RGDATA";
        private static string tb_KGRIDClass = "KGRID";
        private static string tb_RGRIDClass = "RGRID";
        
        private static string mCell_CMBBox = "combobox";
        private static string mCell_CHKBox = "checkbox";
        private static string mCell_TXTBox = "textbox";
        //private static string mCell_LBLBox = "label";

        //RGDATA Table Class Type Variables
        private static string RGDATA_columnContainerTagName = "thead";
        private static string RGDATA_columnTagName = "th";
        private static string RGDATA_rowContainerTagName = "tbody";
        private static string RGDATA_rowTagName = "tr";
        private static string RGDATA_cellTagName = "td";

        //KGRID Table Class Type Variables
        private static string KGRID_columnHeadersNewXPATH = ".//div[contains(@class,'k-grid-header')]//th[@role='columnheader'] | .//thead[contains(@class,'k-grid-header')]//th[@role='columnheader'] | .//thead[contains(@class,'k-grid-header')]//th[@class='k-header']";
        private static String KGRID_columnContainer1XPath = ".//div[@class='k-grid-header-locked']";
        private static String KGRID_columnContainer2XPath = ".//div[contains(@class,'k-grid-header-wrap')]";
        private static String KGRID_columnXPath = ".//th[contains(@role,'columnheader')]";
        private static String KGRID_rowContainer1XPath = ".//div[@class='k-grid-content-locked']";
        private static String KGRID_rowContainer2XPath = ".//div[contains(@class,'k-grid-content ')]";
        private static String KGRID_rowContainer3XPath = ".//table//tbody[@role='rowgroup']";
        private static String KGRID_rowXPath = ".//tr[contains(@role,'row')]";
        private static String KGRID_row3XPath = ".//table//tbody[@role='rowgroup']//tr";
        private static String KGRID_cellXPath = ".//td[contains(@role,'gridcell')]";

        //RGRID Table Class Type Variables
        private static String RGRID_columnContainerXPath = ".//div[contains(@class,'rgHeaderWrapper')]";
        private static String RGRID_columnXPath = ".//th[contains(@class,'rgHeader')]";
        private static String RGRID_rowContainerXPath = ".//div[contains(@class,'rgDataDiv')]";
        private static String RGRID_rowXPath = ".//tr[contains(@class,'Row')]";
        private static String RGRID_cellTagName =  "td";
        //private static String RGRID_groupPanelXPath = ".//div[@data-role='droptarget']";

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            //Reset all current cells in active
            IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
            activeElement.SendKeys(Keys.Escape);

            FindElement();
            GetTableClass();
            GetRows();
            GetColumns();
            GetColumnFilters();
            this.ScrollIntoViewUsingJavaScript();
        }

        public void GetTableClass()
        {
            string tableClass = mElement.GetAttribute("class");

            string table_RGDATA_Class = "rgDataDiv";
            string table_KGRID_Class = "k-grid";
            string table_RGRID_Class = "RadGrid";

            if (tableClass.Contains(table_RGDATA_Class))
                mTableClass = tb_RGDATAClass;
            else if (tableClass.Contains(table_RGRID_Class))
                mTableClass = tb_RGRIDClass;
            else if (tableClass.Contains(table_KGRID_Class))
                mTableClass = tb_KGRIDClass;
            else
                throw new Exception("Table type not supported");
        }

        private void GetColumnFilters()
        {
            if (mTableClass == tb_RGRIDClass)
            {
                mFilterColumns = mElement.FindElements(By.XPath(".//tr[contains(@class,'rgFilterRow')]//td"));
            }
        }

        public void GetColumns()
        {
            IWebElement columnsContainer;
            if (mTableClass.Equals(tb_RGDATAClass))
            {
                columnsContainer = mElement.FindElements(By.TagName(RGDATA_columnContainerTagName)).Count > 0 ?
                    mElement.FindElement(By.TagName(RGDATA_columnContainerTagName)) : throw new Exception("Column container not found.");
                mColumns = columnsContainer.FindElements(By.TagName(RGDATA_columnTagName))
                       .Where(x => x.Displayed).ToList();
            }
            else if (mTableClass.Equals(tb_RGRIDClass))
            {
                columnsContainer = mElement.FindElements(By.XPath(RGRID_columnContainerXPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(RGRID_columnContainerXPath)) : throw new Exception("Column container not found.");
                mColumns = columnsContainer.FindElements(By.XPath(RGRID_columnXPath)).ToList();
                //comment 9.25.2020 href text not visible in selenium xpath if not displayed in grid       
                //.Where(x => x.Displayed).ToList();
            }
            else if (mTableClass.Equals(tb_KGRIDClass))
            {
                if (mElement.FindElements(By.XPath(KGRID_columnHeadersNewXPATH)).Count > 0)
                {
                    //New xpath to get table column headers for KGrid class
                    mColumns = mElement.FindElements(By.XPath(KGRID_columnHeadersNewXPATH)).Where(w => w.Displayed).ToList();
                }
                else if (mElement.FindElements(By.XPath(KGRID_columnContainer1XPath)).Count > 0)
                {
                    //Get column headers from 'K-Grid Locked' container
                    mColumns = mElement.FindElement(By.XPath(KGRID_columnContainer1XPath)).
                            FindElements(By.XPath(KGRID_columnXPath)).Where(x => x.Displayed).ToList();
                }
                else if(mElement.FindElements(By.XPath(KGRID_columnContainer2XPath)).Count > 0)
                {
                    //Get column headers from 'K-Grid Wrap' container
                    mColumns = mElement.FindElement(By.XPath(KGRID_columnContainer2XPath)).
                            FindElements(By.XPath(KGRID_columnXPath)).Where(x => x.Displayed).ToList();
                }
                else
                    throw new Exception("Column K-Grid locked container not found.");
            }
            else
                throw new Exception("No table columns found.");
        }

        public int GetColumnIndexFromName(string ColumnName)
        {
            GetColumns();
            int index = -1;
            for (int i = 0; i < mColumns.Count; i++)
            {
                if (mTableClass.Equals(tb_RGDATAClass) || mTableClass.Equals(tb_RGRIDClass))
                {
                    DlkBaseControl col = new DlkBaseControl("col", mColumns[i]);

                    if (col.GetValue().ToLower() == ColumnName.ToLower())//mColumns[i].Text.ToString().ToLower()
                    {
                        index = i;
                        break;
                    }
                }
                else if (mTableClass.Equals(tb_KGRIDClass))
                {
                    string dataTitle = mColumns[i].GetAttribute("data-title") != null ?
                         mColumns[i].GetAttribute("data-title").ToString().Trim().ToLower() : "";
                    if (dataTitle == ColumnName.ToLower())
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

        public void GetRows()
        {
            IWebElement rowsContainer;
            if (mTableClass.Equals(tb_RGDATAClass))
            {
                rowsContainer = mElement.FindElements(By.TagName(RGDATA_rowContainerTagName)).Count > 0 ?
                   mElement.FindElement(By.TagName(RGDATA_rowContainerTagName)) : throw new Exception("Row container not found.");
                mRows = rowsContainer.FindElements(By.TagName(RGDATA_rowTagName))
                    .Where(x => x.Displayed).ToList();
            }
            else if (mTableClass.Equals(tb_RGRIDClass))
            {
                rowsContainer = mElement.FindElements(By.XPath(RGRID_rowContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(RGRID_rowContainerXPath)) : throw new Exception("Row container not found.");
                mRows = rowsContainer.FindElements(By.XPath(RGRID_rowXPath))
                    .Where(x => x.Displayed).ToList();
            }
            else if (mTableClass.Equals(tb_KGRIDClass))
            {
                if (mElement.FindElements(By.XPath(KGRID_rowContainer1XPath)).Count > 0)
                {
                    //Get rows from 'K-Grid Locked' container
                    mRows = mElement.FindElement(By.XPath(KGRID_rowContainer1XPath)).
                                FindElements(By.XPath(KGRID_rowXPath)).Where(x => x.Displayed).ToList();
                }
                else if (mElement.FindElements(By.XPath(KGRID_rowContainer2XPath)).Count > 0)
                {
                    //Get rows from 'K-Grid K-Auto' container
                    mRows = mElement.FindElement(By.XPath(KGRID_rowContainer2XPath)).
                                FindElements(By.XPath(KGRID_rowXPath)).Where(x => x.Displayed).ToList();
                }
                else if (mElement.FindElements(By.XPath(KGRID_rowContainer3XPath)).Count > 0)
                {
                    mRows = mElement.FindElements(By.XPath(KGRID_row3XPath)).Where(x => x.Displayed).ToList();
                }
                else
                {
                    mRows = new List<IWebElement>();
                }
            }
        }

        public IWebElement GetTargetRow(string RowIndex)
        {
            IWebElement targetRow = null;
            int targetRowNumber = Convert.ToInt32(RowIndex) - 1;

            GetRows();
            targetRow = mRows.Count > 0 ? mRows.ElementAt(targetRowNumber) :
                throw new Exception("No rows (0) found from target table.");
            return targetRow;
        }

        public IWebElement GetSubstituteRow(string RowIndex)
        {
            IWebElement targetRow = null;
            int targetRowNumber = Convert.ToInt32(RowIndex) - 1;

            GetRows();
            targetRow = mTemp_Rows.Count > 0 ? mTemp_Rows.ElementAt(targetRowNumber) :
                throw new Exception("No rows (0) found from target table.");
            return targetRow;
        }

        public IWebElement GetTargetColumn(string ColNameOrIndex)
        {
            int targetColumnNumber = 0;
            if (!int.TryParse(ColNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(ColNameOrIndex);
            else
                targetColumnNumber = Convert.ToInt32(ColNameOrIndex) - 1;

            if (targetColumnNumber == -1)
                throw new Exception("Column name or Column index [" + ColNameOrIndex + "] not found.");

            IWebElement targetColumn = mColumns.ElementAt(targetColumnNumber);
            return targetColumn;
        }

        public IWebElement GetCell(string RowIndex, string ColNameOrIndex)
        {
            IWebElement targetRow = GetTargetRow(RowIndex);
            IWebElement targetCell = null;

            int targetColumnNumber = 0;
            if (!int.TryParse(ColNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(ColNameOrIndex);
            else
                targetColumnNumber = Convert.ToInt32(ColNameOrIndex) - 1;

            if (targetColumnNumber == -1)
                throw new Exception("Column name or Column index [" + ColNameOrIndex + "] not found.");

            List<IWebElement> targetCells = null;
            if (mTableClass.Equals(tb_RGDATAClass))
            {
                targetCells = targetRow.FindElements(By.TagName(RGDATA_cellTagName)).Where(x => x.Displayed).ToList();
            }
            else if (mTableClass.Equals(tb_RGRIDClass))
            {
                targetCells = targetRow.FindElements(By.TagName(RGRID_cellTagName)).Where(x => x.Displayed).ToList();
            }
            else if (mTableClass.Equals(tb_KGRIDClass))
            {
                //Get cells from primary target row
                targetCells = targetRow.FindElements(By.XPath(KGRID_cellXPath)).Where(x => x.Displayed).ToList();
                int currentCellCount = targetCells.Count;

                //Get cells from substitute row if column number exceeds the number of cells from primary target row
                if(targetColumnNumber >= currentCellCount)
                {
                    IWebElement tempTargetRow = GetSubstituteRow(RowIndex);
                    targetCells = tempTargetRow.FindElements(By.XPath(KGRID_cellXPath)).Where(x => x.Displayed).ToList();
                    targetColumnNumber = targetColumnNumber - currentCellCount;
                }
            }

            targetCell = targetCells.ElementAt(targetColumnNumber);

            if (targetCell == null)
                throw new Exception("Cell not found.");

            return targetCell;
        }

        public string GetCellValue(string RowIndex, string ColNameOrIndex)
        {
            string ActualValue = "";
            IWebElement targetCell = GetCell(RowIndex.ToString(), ColNameOrIndex);
            ActualValue = targetCell.Text != "" ? targetCell.Text.Trim() :
                new DlkBaseControl("Target Cell", targetCell).GetValue().Trim();
            return ActualValue;
        }

        public bool GetCellReadOnly(string LineIndex, string ColNameOrIndex)
        {
            bool ActualValue;
            IWebElement targetCell = GetCell(LineIndex.ToString(), ColNameOrIndex);
            ActualValue = Convert.ToBoolean(new DlkBaseControl("Cell", targetCell).IsReadOnly());
            return ActualValue;
        }

        public string GetCellType(IWebElement targetCell)
        {
            //Click cell for edit mode
            targetCell.Click();

            string cellType = "";
            if (targetCell.FindElements(By.TagName("input")).Count > 0)
            {
                IWebElement inputField = targetCell.FindElement(By.TagName("input"));

                if (inputField.GetAttribute("type") != null & inputField.GetAttribute("type").Contains("checkbox"))
                {
                    cellType = mCell_CHKBox;
                }
                else if (IsComboBox(targetCell))
                {
                    cellType = mCell_CMBBox;
                }
                else if (inputField.GetAttribute("type") != null & inputField.GetAttribute("type").Contains("text"))
                {
                    cellType = mCell_TXTBox;
                }
                else
                {
                    throw new Exception("Cell cannot be set with a value or not supported.");
                }
            }
            return cellType;
        }

        private bool IsComboBox(IWebElement targetCell)
        {
            if (targetCell.FindElements(By.XPath(".//span[contains(@class,'k-combobox')]")).Count > 0)
            {
                mCellComboBox = targetCell.FindElements(By.XPath(".//span[contains(@class,'k-combobox')]")).First();
                return true;
            }
            else if (targetCell.FindElements(By.XPath(".//div[contains(@class,'RadDropDownList')]")).Count > 0)
            {
                mCellComboBox = targetCell.FindElements(By.XPath(".//div[contains(@class,'RadDropDownList')]")).First();
                return true;
            }
            else
                return false;
        }

        public void SetCellValue(IWebElement targetCell, string expectedValue)
        {
            string cellType = GetCellType(targetCell);

            if (cellType.Equals(mCell_CHKBox))
            {
                IWebElement checkBox = targetCell.FindElement(By.TagName("input"));
                DlkCheckBox targetCheckBox = new DlkCheckBox("Target Checkbox", checkBox);
                targetCheckBox.Set(expectedValue);
            }
            else if (cellType.Equals(mCell_CMBBox))
            {
                var cmb = targetCell.FindElements(By.XPath(".//span[contains(@class,'combobox')]")).FirstOrDefault();
                
                if (cmb != null)
                {
                    DlkComboBox targetComboBox = new DlkComboBox("Target ComboBox", cmb);
                    targetComboBox.Select(expectedValue);
                }
                else
                {
                    new DlkComboBox("Target ComboBox", mCellComboBox).Select(expectedValue);
                }                
            }
            else if (cellType.Equals(mCell_TXTBox))
            {
                IWebElement textBox = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                textBox = !textBox.TagName.Contains("input") ? targetCell.FindElement(By.TagName("input")) : textBox;
                DlkTextBox targetTextBox = new DlkTextBox("Target TextBox", textBox);
                targetTextBox.Set(expectedValue);
            }
            else
            {
                throw new Exception("Cell cannot be set with a value or not supported.");
            }
        }

        #endregion

        #region KEYWORDS

        [Keyword("ClickCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCell(String RowIndex, String ColNameOrIndex)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                IWebElement targetCell = GetCell(RowIndex, ColNameOrIndex);
                DlkLogger.LogInfo("Clicking target cell...");
                targetCell.Click();
                DlkLogger.LogInfo("ClickCell() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellButton", new String[] { "1|text|Row|O{Row}", "2|text|Col|O{Col}", "3|text|Start|Stop" })]
        public void ClickCellButton(string RowIndex, string ColNameOrIndex,string ButtonName)
        {
            try
            {
                if (!int.TryParse(RowIndex, out int row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                IWebElement targetCell = GetCell(RowIndex, ColNameOrIndex);

                IWebElement button = targetCell.FindElements(By.XPath($"//a[contains(.,'{ButtonName}')]")).FirstOrDefault();

                if (button != null)
                {
                    DlkLogger.LogInfo($"Clicking button {ButtonName}...");
                    targetCell.Click();
                    DlkLogger.LogInfo("ClickCellButton() passed.");
                }
                else
                    throw new Exception($"Target cell does not contains button {ButtonName}");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void DoubleClickCell(String RowIndex, String ColNameOrIndex)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                IWebElement targetCell = GetCell(RowIndex, ColNameOrIndex);
                DlkLogger.LogInfo("Executing double click on target row...");
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                actions.DoubleClick(targetCell).Perform();
                DlkLogger.LogInfo("DoubleClickCell() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectRow", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectRow(String RowIndex)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                IWebElement targetRow = GetTargetRow(RowIndex);
                DlkLogger.LogInfo("Selecting target row...");
                targetRow.Click();
                DlkLogger.LogInfo("SelectRow() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRow() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickRow", new String[] { "1|text|Expected Value|TRUE" })]
        public void DoubleClickRow(String RowIndex)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                IWebElement targetRow = GetTargetRow(RowIndex);
                DlkLogger.LogInfo("Executing double click on target row...");
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                actions.DoubleClick(targetRow).Perform();
                DlkLogger.LogInfo("DoubleClickRow() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String RowIndex, String ColNameOrIndex, String ExpectedValue)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                string ActualValue = GetCellValue(RowIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellValue() ",ExpectedValue,ActualValue);
                DlkLogger.LogInfo("VerifyCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellPartialValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellPartialValue(String RowIndex, String ColNameOrIndex, String PartialValue)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                string ActualValue = GetCellValue(RowIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellPartialValue() ", PartialValue, ActualValue, true);
                DlkLogger.LogInfo("VerifyCellPartialValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellReadOnly(String RowIndex, String ColNameOrIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                bool actualValue = GetCellReadOnly(RowIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellReadOnly(): ", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyCellReadOnly() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetCellReadOnly(String RowIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellReadOnly(RowIndex, ColNameOrIndex).ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellReadOnly() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetCellValue(String RowIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                string ActualValue = GetCellValue(RowIndex, ColNameOrIndex);
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowCount(String VariableName)
        {
            try
            {
                Initialize();
                string actualRowCount = mRows.Count().ToString();
                DlkVariable.SetVariable(VariableName, actualRowCount);
                DlkLogger.LogInfo("[" + actualRowCount + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetRowCount() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRowCount(String ExpectedValue)
        {
            try
            {
                Initialize();
                string actualRowCount = mRows.Count().ToString();
                DlkAssert.AssertEqual("VerifyRowCount() ", ExpectedValue, actualRowCount);
                DlkLogger.LogInfo("VerifyRowCount() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowWithColumnValue(String ColNameOrIndex, String Value, String VariableName)
        {
            try
            {
                Initialize();
                bool rFound = false;

                for (int r = 1; r <= mRows.Count; r++)
                {
                    string ActualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (ActualValue.ToLower() == Value.ToLower())
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

        [Keyword("GetRowWithColumnPartialValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowWithColumnPartialValue(String ColNameOrIndex, String Value, String VariableName)
        {
            try
            {
                Initialize();
                bool rFound = false;

                for (int r = 1; r <= mRows.Count; r++)
                {
                    string ActualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (ActualValue.ToLower().Contains(Value.ToLower()))
                    {
                        DlkVariable.SetVariable(VariableName, r.ToString());
                        DlkLogger.LogInfo("[" + r.ToString() + "] value set to Variable: [" + VariableName + "]");
                        rFound = true;
                        break;
                    }
                }

                if (!rFound)
                    throw new Exception("Row not found with value [" + Value + "]");

                DlkLogger.LogInfo("GetRowWithColumnPartialValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnPartialValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValue(String RowIndex, String ColNameOrIndex, String ExpectedValue)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(RowIndex, out row) || row == 0)
                    throw new Exception("[" + RowIndex + "] is not a valid input for parameter RowIndex.");

                Initialize();
                IWebElement targetCell = GetCell(RowIndex, ColNameOrIndex);
                SetCellValue(targetCell,ExpectedValue);
                DlkLogger.LogInfo("SetCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetColumnFilter", new String[] { "1|text|ColName|1" })]
        public void SetColumnFilter(string ColNameOrIndex, string Value)
        {
            try
            {
                Initialize();

                int col = -1;
                if (!int.TryParse(ColNameOrIndex, out col))
                {
                    col = GetColumnIndexFromName(ColNameOrIndex);
                }

                if (col > -1)
                {
                    var cell = new DlkBaseControl("filterCell", mFilterColumns[col]);
                    cell.FindElement();
                    cell.ClickByObjectCoordinates();
                    IWebElement filterText = mFilterColumns[col].FindElements(By.XPath($".//input[contains(@type,'text')]")).FirstOrDefault();
                    filterText.SendKeys(Value);
                    filterText.SendKeys(Keys.Tab);
                    Thread.Sleep(3000);
                        
                    DlkLogger.LogInfo("SetColumnFilter() passed.");
                }
                else
                    throw new Exception($"SetColumnFilter() failed: Invalid column name or index {ColNameOrIndex}");
            }
            catch (Exception e)
            {
                throw new Exception("SetColumnFilter() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickColumnFilter", new String[] { "1|text|ColName|1","2|text|Filter|Find" })]
        public void ClickColumnFilter(string ColNameOrIndex,string ButtonName)
        {
            try
            {
                Initialize();

                int col=-1;
                if (!int.TryParse(ColNameOrIndex, out col))
                {
                    col = GetColumnIndexFromName(ColNameOrIndex);
                }

                if (col > -1)
                {
                    var cell = new DlkBaseControl("filterCell", mFilterColumns[col - 1]);
                    cell.FindElement();
                    cell.ClickByObjectCoordinates();
                    IWebElement filterButton = mFilterColumns[col - 1].FindElements(By.XPath($".//input[contains(@title,'{ButtonName}')]")).FirstOrDefault();

                    if (filterButton != null)
                        filterButton.Click();
                    else
                        throw new Exception($"ClickColumnFilter() failed: Cannot find button {ButtonName}");

                    DlkLogger.LogInfo("ClickColumnFilter() passed.");
                }
                else
                    throw new Exception($"ClickColumnFilter() failed: Invalid column name or index {ColNameOrIndex}");
            }
            catch (Exception e)
            {
                throw new Exception("ClickColumnFilter() failed : " + e.Message, e);
            }
        }

        //[Keyword("DragDropColumnGroup")]
        //public void DragDropColumnGroup(string ColNameOrIndex)
        //{
        //    try
        //    {
        //        Initialize();

        //        int targetColumnNumber = 0;
        //        if (!int.TryParse(ColNameOrIndex, out targetColumnNumber))
        //            targetColumnNumber = GetColumnIndexFromName(ColNameOrIndex);
        //        else
        //            targetColumnNumber = Convert.ToInt32(ColNameOrIndex) - 1;

        //        if (targetColumnNumber == -1)
        //            throw new Exception("Column '" + ColNameOrIndex + "' was not found.");

        //        IWebElement groupPanel = mElement.FindElement(By.XPath(mGroupPanelXPath));
        //        Actions actions = new Actions(DlkEnvironment.AutoDriver);
        //        actions.DragAndDrop(mColumns.ElementAt(targetColumnNumber), groupPanel).Perform();
        //        DlkLogger.LogInfo("DragAndDropColumnHeader() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("DragAndDropColumnHeader() failed : " + e.Message, e);
        //    }
        //}

        #endregion
    }
}
