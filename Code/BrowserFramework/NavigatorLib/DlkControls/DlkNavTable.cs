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
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for tables
    /// </summary>
    [ControlType("Table")]
    public class DlkNavTable : DlkBaseTable
    {
        public DlkNavTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
        }
        public DlkNavTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }
        public DlkNavTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }

        /// <summary>
        /// Searches a table for a cell value and clicks it.
        /// If more than one exist, only the first is selected
        /// </summary>
        /// <param name="CellValue"></param>
        [Keyword("SelectCellContaining", new String[] { "1|text|Expected Cell Value|SampleValue" })]      
        public void SelectCellContaining(String CellValue)
        {
            Initialize();
            foreach (IWebElement mRow in TableRows)
            {
                IList<IWebElement> mCells = mRow.FindElements(By.TagName("td"));
                foreach (IWebElement mCell in mCells)
                {
                    DlkBaseControl mControl = new DlkBaseControl("Cell", mCell);
                    if (mControl.GetValue() == CellValue)
                    {
                        mControl.Click();
                        DlkLogger.LogInfo("Successfully clicked cell containing: " + CellValue);
                        return;
                    }
                }
            }
            throw new Exception("Unable to find cell containing: " + CellValue);
        }

        /// <summary>
        /// Given coordinates, verifies the data of an input cell
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        /// <param name="ExpectedResult"></param>
        [Keyword("VerifyInputCell", new String[] { "1|text|Expected Value|Sample Row Number",
                                                         "2|text|Expected Value|Sample Column Number",
                                                               "3|text|ExpectedResult|Sample Expected Result"})]
        public void VerifyInputCell(String Row, String Column, String ExpectedResult)
        {
            List<String> mCellVals = GetRowData(Convert.ToInt32(Row) - 1, true);
            DlkAssert.AssertEqual("VerifyInputCell [" + Row + ":" + Column + "]", ExpectedResult, mCellVals[Convert.ToInt32(Column) - 1]);
        }

        /// <summary>
        /// Given coordinates, sets an input cell
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        /// <param name="ValueToSet"></param>
        [Keyword("SetInputCell", new String[] { "1|text|Expected Value|Sample Row Number",
                                                         "2|text|Expected Value|Sample Column Number",
                                                               "3|text|Expected ValueToSet|Sample Expected Value"})]
        public void SetInputCell(String Row, String Column, String ValueToSet)
        {
            Initialize(); // needed to find and populate the TableRows
            IWebElement TableRow = TableRows[Convert.ToInt32(Row) - 1];
            IList<IWebElement> mElmCells = TableRow.FindElements(By.TagName("td"));
            IWebElement mElmCell = mElmCells[Convert.ToInt32(Column) - 1];
            IWebElement mElmCellInput = mElmCell.FindElement(By.TagName("input"));     
           // mElmCellInput.Click();
            mElmCellInput.Clear();
            mElmCellInput.SendKeys(ValueToSet);
            mElmCellInput.SendKeys(Keys.Tab);
            DlkLogger.LogInfo("SetInputCell()", this.mControlName, "Row:[" + Row + "], Column:[" + Column + "] set to:" + ValueToSet);
        }

        /// <summary>
        /// Verifies Row Data
        /// </summary>
        /// <param name="ExpectedResult"></param>           
        [Keyword("VerifyRowData", new String[] { "1|text|Expected Value|Sample Row Number",
                                                         "2|text|Expected Result|Sample Text"})]
        public void VerifyRowData(String Row, String ExpectedResult)
        {
            List<String> lsRowData = GetRowData(Convert.ToInt32(Row) - 1, true);
            String RowData = String.Join("~", lsRowData.ToArray());
            if (RowData.Contains("\r\n"))
            {
                RowData = RowData.Replace("\r\n", " ");
            }   
            DlkAssert.AssertEqual("Verify Row Data", ExpectedResult, RowData);

        }

        /// <summary>
        /// Verifies Column Data
        /// </summary>
        /// <param name="Col"></param> 
        /// <param name="ExpectedResult"></param>   
        /// <param name="IncludeInputValues"></param>       
        [Keyword("VerifyColumnData", new String[] { "1|text|Expected Value|Sample Column Number",
                                                         "2|text|Expected Value|Sample Expected Value",
                                                               "3|text|Value|TRUE"})]
        public void VerifyColumnData(String Col, String ExpectedResults, String IncludeInputValues)
        {
            Initialize();
            int iCol = Convert.ToInt32(Col);
            List<String> lsExpectedResults = ExpectedResults.Split('~').ToList();
            Boolean bIncludeInputValues = Convert.ToBoolean(IncludeInputValues);

            if (lsExpectedResults.Count != RowCount)
            {
                LogColumnData(iCol, "Actual Data for Column: " + Col);
            }
            DlkAssert.AssertEqual("Columns contain the same number of rows.", lsExpectedResults.Count, RowCount);

            for (int i = 0; i < RowCount; i++)
            {
                String ActResult = GetCellData(i, iCol, bIncludeInputValues);
                List<String> mRange = lsExpectedResults[i].Split(':').ToList();
                if (mRange.Count == 1) // no range
                {
                    double dResult = -1;
                    if (double.TryParse(mRange[0], out dResult)) // numerical comparison if applicable
                    {
                        double dActResult = -1;
                        if (double.TryParse(ActResult, out dActResult))
                        {
                            DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + i.ToString(), dResult, dActResult);
                        }
                        else // string compare
                        {
                            DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + i.ToString(), lsExpectedResults[i], ActResult);
                        }
                    }
                    else // string compare
                    {
                        DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + i.ToString(), lsExpectedResults[i], ActResult);
                    }
                }
                else //we have a range
                {
                    double dLower = double.Parse(mRange[0], System.Globalization.NumberStyles.Currency);
                    double dUpper = double.Parse(mRange[1], System.Globalization.NumberStyles.Currency);
                    double dActResult = double.Parse(ActResult, System.Globalization.NumberStyles.Currency);

                    DlkAssert.AssertWithinRange("Column compare with range validation.", dLower, dUpper, dActResult);
                }
            }
        }

    }
}

