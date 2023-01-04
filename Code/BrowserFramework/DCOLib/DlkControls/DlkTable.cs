using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using OpenQA.Selenium.Support.UI;

namespace DCOLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        private String mstrHeaderXPATH = "./thead/tr[@class='tableHeaderRow']/th";
        private String mstrRowXPATH = "./tbody/tr";
        private String mstrURLXPATH = "./a";
        private IList<string> mlstHeaderTexts;
        private List<IWebElement> mlstHeaders;
        private IList<IWebElement> mlstRows;

        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            WaitForPageLoad();
            ClearDashboardTimer();
            ClearWorkflowTimer();

            FindElement();
        }

        [Keyword("GetTableRowWithColumnValue", new String[] {"1|text|Column Header|Line*", 
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public void GetTableRowWithColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            Initialize();
            bool blnFound = false;
            int intColIndex = -1;

            try
            {
                bool bContinue = true;

                intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    RefreshRows();
                    int i = 0;
                    while (bContinue && i < mlstRows.Count)
                    {
                        for (i = 0; i < mlstRows.Count; i++)
                        {
                            DlkBaseControl cellControl = new DlkBaseControl("Cell", GetCell(i + 1, intColIndex));
                            String cellValue = cellControl.GetValue();
                            if (cellValue.Equals(Value))
                            {
                                DlkVariable.SetVariable(VariableName, (i + 1).ToString());
                                blnFound = true;
                                bContinue = false;
                                break;
                            }
                        }
                    }

                    if (blnFound)
                    {
                        DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue()");
                    }
                    else
                    {
                        throw new Exception("Value = '" + Value + "' under Column = '" + ColumnHeader + "' not found in table");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetTableRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTableRowWithURLValue", new String[] {"1|text|URL Value|https://acbp.deltekfirst.com/dco/workflow_detail?processInstanceId=20304#4796",
                                                            "2|text|VariableName|MyRow"})]
        public void GetTableRowWithURLValue(String Value, String VariableName)
        {
            Initialize();
            bool blnFound = false;

            try
            {
                bool bContinue = true;
                    RefreshRows();
                    int i = 0;
                    while (bContinue && i < mlstRows.Count)
                    {
                        for (i = 0; i < mlstRows.Count; i++)
                        {
                            DlkBaseControl cellControl = new DlkBaseControl("Cell", GetCell(i + 1, 0));
                            IWebElement cellHrefControl = cellControl.mElement.FindElement(By.XPath(mstrURLXPATH));
                            String cellHrefValue = cellHrefControl.GetAttribute("href");
                            if (cellHrefValue.Equals(Value))
                            {
                                DlkVariable.SetVariable(VariableName, (i + 1).ToString());
                                blnFound = true;
                                bContinue = false;
                                break;
                            }
                        }
                    }

                    if (blnFound)
                    {
                        DlkLogger.LogInfo("Successfully executed GetTableRowWithURLValue()");
                    }
                    else
                    {
                        throw new Exception("Value = '" + Value + "' in Workflow column not found in table");
                    }
            }
            catch (Exception e)
            {
                throw new Exception("GetTableRowWithURLValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableCellByRowColumn", new String[] {"1|text|Row|O{Row}", 
                                                            "2|text|Column Header|Line*"})]
        public void ClickTableCellByRowColumn(String Row, String ColumnHeader)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    DlkBaseControl cellControl = new DlkBaseControl("Cell", GetCell(Convert.ToInt32(Row), intColIndex));
                    cellControl.Click();
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed ClickTableCellByRowColumn()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCellByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExist", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExist(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExist() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExist() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellReadOnly", new String[] {"1|text|Row|O{Row}", 
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|TRUE"})]
        public void VerifyTableCellReadOnly(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    String cellClass = cell.GetAttribute("class").ToString().ToLower();
                    DlkAssert.AssertEqual("VerifyTableCellReadOnly()", Convert.ToBoolean(ExpectedValue), cellClass.Equals("cllro"));
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellReadOnly() failed : " + e.Message, e);
            }

        }

        [Keyword("GetColumnHeaderValue", new String[] {"1|text|ColumnIndex|0", 
                                                       "2|text|VariableName|MyVar"})]
        public void GetColumnHeaderValue(String ColumnIndex, String VariableName)
        {
            try
            {
                int iColIndex = Convert.ToInt32(ColumnIndex);
                Initialize();
                RefreshHeaders();
                if (iColIndex >= mlstHeaderTexts.Count)
                {
                    throw new Exception("Column index " + iColIndex + " out of bounds of header indices");
                }
                DlkVariable.SetVariable(VariableName, mlstHeaderTexts[iColIndex]);
                DlkLogger.LogInfo("Successfully executed GetColumnHeaderValue(). Value obtained: " + mlstHeaderTexts[iColIndex]);
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeader() failed : " + e.Message, e);
            }
        }

        #region Private Methods

        private int GetColumnIndexByHeader(String sColumnHeader)
        {
            int index = -1;
            Boolean bContinue = true;

            RefreshHeaders();
            while (bContinue && index == -1)
            {
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    string currentHeader = GetColumnHeaderText(mlstHeaders[i]).Trim('*').Trim();
                    if (sColumnHeader.Trim('*').Trim() == currentHeader)
                    {
                        index = i;
                        if (i == mlstHeaders.Count - 1 && mlstHeaders.Count > 1)
                        {
                            index--;
                            if ((i == mlstHeaders.Count - 1))
                            {
                                i = -1;
                                continue;
                            }
                        }
                        break;
                    }
                }
            }
            return index;
        }

        private void RefreshHeaders()
        {
            FindElement();

            mlstHeaders = new List<IWebElement>();
            mlstHeaderTexts = new List<String>();

            IList<IWebElement> headers = mElement.FindElements(By.XPath(mstrHeaderXPATH));
            foreach (IWebElement columnHeader in headers)
            {
                DlkBaseControl hdr = new DlkBaseControl("Header", columnHeader);
                if (columnHeader.GetCssValue("display") != "none")
                {
                    mlstHeaders.Add(columnHeader);
                    mlstHeaderTexts.Add(GetColumnHeaderText(columnHeader));
                }
            }
        }

        private void RefreshRows()
        {
            FindElement();
            mlstRows = mElement.FindElements(By.XPath(mstrRowXPATH));
        }

        private String GetColumnHeaderText(IWebElement columnHeader)
        {
            String headerText = "";
            headerText = new DlkBaseControl("ColumnHeader", columnHeader).GetValue();
            return DlkString.RemoveCarriageReturn(headerText.Trim()).Replace("/ ", "/"); // trim of space after slash for some headers
        }

        private IWebElement GetCell(int iRow, int iColumn)
        {
            IWebElement cell = null;
            RefreshRows();
            if (mlstRows.Count < iRow || iRow <= 0)
            {
                return cell;
            }
            IWebElement row = mlstRows[iRow - 1];
            IList<IWebElement> cells = new List<IWebElement>();
            foreach (IWebElement item in row.FindElements(By.XPath("./*")))
            {
                cells.Add(item);
            }
            cell = cells[iColumn];
            return cell;
        }

        /// <summary>
        /// Wait for page load complete
        /// </summary>
        private void WaitForPageLoad()
        {
            try
            {
                IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
                new WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(15)).Until(
                       d => jse.ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch
            {
                //continue
            }
        }

        /// <summary>
        /// Clear timer in dashboard to retrieve element & not result in stale reference element error
        /// </summary>
        private void ClearDashboardTimer()
        {
            try
            {
                IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
                jse.ExecuteScript("$(window).load(function(){$('#dashboard').attr('id','dashhboard_altered');clearTimeout(dashBoardTimeOut);});$(window).trigger('load');", null);
            }
            catch
            {
                //continue
            }
        }

        /// <summary>
        /// Clear timer in dashboard to retrieve element & not result in stale reference element error
        /// </summary>
        private void ClearWorkflowTimer()
        {
            try
            {
                IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
                //jse.ExecuteScript("$(window).trigger('unload');", null);
                jse.ExecuteScript("$('#dvWfSteps').attr('id','dvWfSteps_altered')", null);
                jse.ExecuteScript("$('#dvWfDetails').attr('id','dvWfDetails_altered')", null);
            }
            catch
            {
                //continue
            }
        }

        #endregion

    }
}
