using CBILib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("ReportTable")]
    public class DlkReportTable : DlkBaseControl
    {
        private List<string> mColumnHeaders = new List<string>();
        private List<IWebElement> mRows = new List<IWebElement>();

        #region Constructor
        public DlkReportTable(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement) { }

        public DlkReportTable(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue) { }

        public DlkReportTable(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues) { }

        public DlkReportTable(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public DlkReportTable(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue) { }

        #endregion

        private void Initialize(bool lineTable, string line = null, string table = null)
        {
            FindElement();

            if (lineTable)
            {
                GetLineTable(line, table);
            }
            GetColumnHeaders();
            GetTableRows();
        }

        [Keyword("ClickTableCell", new string[] { "1|text|Caption|Project ID" })]
        public void ClickTableCell(string Row, string ColumnIndex)
        {
            try
            {
                Initialize(false);

                IWebElement cell = GetCell(int.Parse(Row), int.Parse(ColumnIndex));
                var field = cell.FindElement(By.XPath(".//span[@specname='textItem']"));
                field.Click();
                DlkLogger.LogInfo("ClickTableCell(): Passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCell() failed: " + e.Message, e);
            }
        }

        [Keyword("GetTableCellValue", new string[] { "1|text|Caption|Project ID" })]
        public void GetTableCellValue(string Row, string ColumnIndex, string VariableName)
        {
            try
            {
                Initialize(false);

                IWebElement cell = GetCell(int.Parse(Row), int.Parse(ColumnIndex));
                var field = cell.FindElement(By.XPath(".//span[@specname='textItem']"));

                DlkVariable.SetVariable(VariableName, field.Text);
                DlkLogger.LogInfo("GetTableCellValue(): Passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetTableCellValue() failed: " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellValue", new string[] { "1|text|Caption|Project ID" })]
        public void VerifyTableCellValue(string Row, string ColumnIndex, string ExpectedValue)
        {
            try
            {
                Initialize(false);

                IWebElement cell = GetCell(int.Parse(Row), int.Parse(ColumnIndex));
                DlkAssert.AssertEqual("VerifyTableCellValue()", ExpectedValue, cell.Text);
                DlkLogger.LogInfo("VerifyTableCellValue(): Passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellValue() failed: " + e.Message, e);
            }
        }

        [Keyword("VerifyFieldValue", new string[] { "1|text|Caption|Project ID" })]
        public void VerifyFieldValue(string Caption, string ExpectedValue)
        {
            try
            {
                Initialize(false);
                IWebElement field = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]")).FirstOrDefault();

                if (field != null)
                {
                    IWebElement value = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]//ancestor::td[1]/following-sibling::td[1]")).FirstOrDefault();
                    if (value != null)
                    {
                        DlkAssert.AssertEqual("VerifyFieldValue()", ExpectedValue, value);
                        DlkLogger.LogInfo("VerifyFieldValue(): Passed");
                    }
                    else
                    {
                        throw new Exception($"'{Caption}' value not found.");
                    }
                }
                else
                    throw new Exception($"Field '{Caption}' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFieldValue() failed: " + e.Message, e);
            }
        }

        [Keyword("GetLineTableCellValue", new string[] { "1|text|LineTable|1~1",
            "2|text|Row|1", "3|text|ColumnIndex|1", "4|text|VariableName|varName" })]
        public void GetLineTableCellValue(string Line, string Table, string Row, string ColumnIndex, string VariableName)
        {
            try
            {
                Initialize(true, Line, Table);

                IWebElement cell = GetCell(int.Parse(Row), int.Parse(ColumnIndex));
                var field = cell.FindElement(By.XPath(".//span[@specname='textItem']"));

                DlkVariable.SetVariable(VariableName, field.Text);
                DlkLogger.LogInfo("GetTableCellValue(): Passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineTableCellValue() failed: " + e.Message, e);
            }
        }

        [Keyword("ClickLineTableCell", new string[] { "1|text|LineTable|1~1",
            "2|text|Row|1", "3|text|ColumnIndex|1", "4|text|VariableName|varName" })]
        public void ClickLineTableCell(string Line, string Table, string Row, string ColumnIndex)
        {
            try
            {
                Initialize(true, Line, Table);

                IWebElement cell = GetCell(int.Parse(Row), int.Parse(ColumnIndex));
                var field = cell.FindElement(By.XPath(".//span[@specname='textItem']"));
                field.Click();
                DlkLogger.LogInfo("ClickLineTableCell(): Passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLineTableCell() failed: " + e.Message, e);
            }
        }

        [Keyword("GetLineFieldValue", new string[] { "1|text|Caption|Project ID" })]
        public void GetLineFieldValue(string Line, string Caption, string VariableName)
        {
            try
            {
                Initialize(true, Line);

                IWebElement field = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]")).FirstOrDefault();

                if (field != null)
                {
                    IWebElement value = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]//ancestor::td[1]/following-sibling::td[1]")).FirstOrDefault();

                    if (value != null)
                    {

                        DlkVariable.SetVariable(VariableName, value.Text);
                        DlkLogger.LogInfo("GetTableCellValue(): Passed");
                    }
                    else
                    {
                        throw new Exception($"'{Caption}' value not found.");
                    }
                }
                else
                    throw new Exception($"Field '{Caption}' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("GetFieldValue() failed: " + e.Message, e);
            }
        }

        private void GetLineTable(string line, string table = null)
        {
            int lineIndex = int.Parse(line);
            int tableIndex = table == null ? 1 : (int.Parse(table) + 1);
            mElement = mElement.FindElement(By.XPath($"(.//following-sibling::tr[{lineIndex}]//table)[{tableIndex}]"));            
        }

        private IWebElement GetCell(int row, int col)
        {
            return mRows[row - 1].FindElements(By.XPath(".//td")).ElementAt(col - 1);
        }

        private void GetTableRows()
        {
            mRows =  new List<IWebElement>(mElement.FindElements(By.XPath("./tbody//tr")).Skip(1).ToList());
        }

        private void GetColumnHeaders()
        {
            var headers = mElement.FindElements(By.XPath("//td[@specname='listColumnTitle']"));
            foreach (var header in headers)
            {
                mColumnHeaders.Add(header.Text);
            }
        }
    }
}
