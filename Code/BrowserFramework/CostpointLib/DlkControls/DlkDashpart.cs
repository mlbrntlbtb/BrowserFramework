using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using System.Threading;
using System.Text.RegularExpressions;

namespace CostpointLib.DlkControls
{
    [ControlType("Dashpart")]
    public class DlkDashpart : DlkBaseControl
    {
        private const char DEFAULT_DELIMETER = '~';
        private String mstrRowClass = "dR";
        private String mstrChartTitleXPATHString = ".//table[contains(@id, 'AUTOGENBOOKMARK')]//div[contains(@style, 'text-align')]";
        private String mstrTableTitleXPATHString = ".//div[contains(@class, 'dMH')]";
        private String mstrChartMainTitleXPATHString = ".//span[@id='rptHrdTtl']";
        private String mstrDashpartYAxisTitleXPATHString = "./descendant::*[name()='text' and boolean(@transform)]";
        private IList<string> mlstHeaderTexts;
        private List<IWebElement> mlstHeaders;
        private IList<IWebElement> mlstRows;

        public DlkDashpart(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDashpart(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDashpart(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        #region Keywords

        [Keyword("GetDashpartRowWithColumnValue", new String[] {"1|text|Column Header|Line*",
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public void GetDashpartRowWithColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            bool blnFound = false;
            int intColIndex = -1;

            try
            {
                Initialize();
                bool bContinue = true;

                intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    RefreshRows();
                    int i = 0;
                    while (bContinue)
                    {
                        for (i = 1; i <= mlstRows.Count; i++)
                        {
                            DlkBaseControl cellControl = new DlkBaseControl("Cell", GetCell(i, intColIndex));
                            String cellValue = cellControl.GetValue();
                            if (cellValue.Equals(Value))
                            {
                                DlkVariable.SetVariable(VariableName, i.ToString());
                                blnFound = true;
                                bContinue = false;
                                break;
                            }
                        }
                    }

                    if (blnFound)
                    {
                        DlkLogger.LogInfo("Successfully executed GetDashpartRowWithColumnValue()");
                    }
                    else
                    {
                        throw new Exception("Value = '" + Value + "' under Column = '" + ColumnHeader + "' not found in dashpart");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetDashpartRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickDashpartCellByRowColumn", new String[] {"1|text|Row|O{Row}",
                                                            "2|text|Column Header|Line*"})]
        public void ClickDashpartCellByRowColumn(String Row, String ColumnHeader)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cellElement = GetCell(Convert.ToInt32(Row), intColIndex);
                    DlkBaseControl cellControl = new DlkBaseControl("Cell", cellElement);
                    cellControl.ScrollIntoViewUsingJavaScript();
                    if (DlkEnvironment.mBrowser.ToLower() == "firefox")
                    {
                        IWebElement cellParent = cellElement.FindElement(By.XPath("./.."));
                        cellParent.Click();
                    }
                    else
                    {
                        cellControl.Click();
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed ClickDashpartCellByRowColumn()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickDashpartCellByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("SetDashpartHeaderValue", new String[] {"1|text|Column|O{Column}",
                                                        "2|text|Value|Sample Value"})]
        public void SetDashpartHeaderValue(String Column, String Value)
        {
            try
            {
                Initialize();

                int intColIndex = Convert.ToInt32(Column);
                RefreshHeaders();
                if (mlstHeaders[intColIndex - 1].FindElements(By.XPath(".//input[@type='checkbox']")).Any())
                {
                    IWebElement headerCheckBox = mlstHeaders[intColIndex - 1].FindElement(By.XPath(".//input[@type='checkbox']"));
                    DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", headerCheckBox);
                    chkCell.Set(Value);
                }
                else
                {
                    throw new Exception("Column in index '" + Column + "' does not have a checkbox header");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetDashpartHeaderValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetDashpartCellValue", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Value|Sample Value"})]
        public void SetDashpartCellValue(String Row, String ColumnHeader, String Value)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cannot find cell");
                    }
                    else if (cell.GetAttribute("type") != "checkbox")
                    {
                        throw new Exception("Column '" + ColumnHeader + "' does not have checkbox cells");
                    }
                    else
                    {
                        DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", cell);
                        chkCell.Set(Value);
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetDashpartCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartCellValue", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyDashpartCellValue(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cannot find cell");
                    }
                    else
                    {
                        DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, cell.Text);
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartCellTooltip", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|AS"})]
        public void VerifyDashpartCellTooltip(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cannot find cell");
                    }
                    else
                    {
                        new DlkBaseControl("", cell).MouseOver();
                        DlkAssert.AssertEqual("VerifyDashpartCellTooltip() : " + mControlName, ExpectedValue, cell.GetAttribute("title"));
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartCellTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartRowCount", new String[] { "1|text|Expected Row Count|0" })]
        public void VerifyDashpartRowCount(String ExpectedRowCount)
        {
            try
            {
                int iExpRowCount = Convert.ToInt32(ExpectedRowCount);
                Initialize();
                RefreshRows();
                int iActRowCount = mlstRows.Count();
                DlkAssert.AssertEqual("VerifyDashpartRowCount() : Dashpart Row Count", iExpRowCount, iActRowCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartRowCount() failed : " + e.Message, e);
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

        [Keyword("GetDashpartExistsState", new string[] { "1|text|Variable Name|Sample Variable" })]
        public void GetDashpartExistsState(string VariableName)
        {
            try
            {
                Initialize();
                base.GetIfExists(VariableName);
                string result = DlkVariable.GetVariable("O{" + VariableName + "}");
                DlkLogger.LogInfo("GetDashpartExistsState() : Successfully assigned dashpart existing state " + result + " to variable " + VariableName);
            }
            catch (Exception e)
            {
                throw new Exception("GetDashpartExistsState() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartColumnHeaders", new String[] { "1|text|Expected header texts|Header1~Header2~Header3" })]
        public void VerifyDashpartColumnHeaders(String ExpectedHeaders)
        {
            String strActualHeaders = String.Empty;
            String strDelimeter = String.Empty;
            IList<string> mlstHeadersFullList;
            mlstHeadersFullList = new List<String>();

            try
            {
                Initialize();
                RefreshHeaders();

                foreach (IWebElement hdrName in mlstHeaders)
                {
                    String headerText = GetColumnHeaderText(hdrName);
                    mlstHeadersFullList.Add(headerText);
                }

                foreach (String hdr in mlstHeadersFullList)
                {
                    strActualHeaders += (strDelimeter + hdr);
                    strDelimeter = DEFAULT_DELIMETER.ToString();
                }

                DlkAssert.AssertEqual("VerifyDashpartColumnHeaders()", ExpectedHeaders.ToLower(), strActualHeaders.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartColumnHeaders() failed : " + e.Message, e);
            }
        }

        [Keyword("GetDashpartCellValue", new String[] {"1|text|Row|O{Row}",
                                                     "2|text|Column Header|Account*",
                                                     "3|text|VariableName|MyCell"})]
        public void GetDashpartCellValue(String Row, String ColumnHeader, String VariableName)
        {
            String cellValue = String.Empty;

            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cell cannot be found");
                    }
                    else
                    {
                        DlkVariable.SetVariable(VariableName, cell.Text);
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed GetDashpartCellValue()");
            }
            catch (Exception e)
            {
                throw new Exception("GetDashpartCellValue() failed : " + e.Message, e);
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
                if (iColIndex > mlstHeaderTexts.Count && iColIndex <= 0)
                {
                    throw new Exception("Column index " + iColIndex + " out of bounds of header indices");
                }
                DlkVariable.SetVariable(VariableName, mlstHeaderTexts[iColIndex-1]);
                DlkLogger.LogInfo("Successfully executed GetColumnHeaderValue(). Value obtained: " + mlstHeaderTexts[iColIndex]);
            }
            catch (Exception e)
            {
                throw new Exception("GetColumnHeaderValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellContentFontStyle", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyCellContentFontStyle(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cannot find cell");
                    }
                    else
                    {
                        DlkAssert.AssertEqual("VerifyCellContentFontStyle() : " + mControlName, ExpectedValue.ToLower(), cell.GetCssValue("font-family").ToLower());
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellContentFontStyle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellContentFontColor", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyCellContentFontColor(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cannot find cell");
                    }
                    else
                    {
                        string fontColor = cell.GetCssValue("color");
                        if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                        {
                            fontColor = fontColor.Replace(")", ", " + cell.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                        }
                        DlkAssert.AssertEqual("VerifyCellContentFontColor() : " + mControlName, ExpectedValue, fontColor);
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellContentFontColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellContentFontSize", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyCellContentFontSize(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    if (cell == null)
                    {
                        throw new Exception("Cannot find cell");
                    }
                    else
                    {
                        DlkAssert.AssertEqual("VerifyCellContentFontSize() : " + mControlName, ExpectedValue, StandardizeFontSize(cell.GetCssValue("font-size")));
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellContentFontSize() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeaderFontStyle", new String[] {"1|text|ColumnIndex|0",
                                                       "2|text|ExpectedValue|MyVal"})]
        public void VerifyColumnHeaderFontStyle(String ColumnIndex, String ExpectedValue)
        {
            try
            {
                int iColIndex = Convert.ToInt32(ColumnIndex);
                Initialize();
                RefreshHeaders();
                if (iColIndex > mlstHeaders.Count && iColIndex <= 0)
                {
                    throw new Exception("Column index " + iColIndex + " out of bounds of header indices");
                }
                DlkAssert.AssertEqual("VerifyColumnHeaderFontStyle() : " + mControlName, ExpectedValue.ToLower(), mlstHeaders[iColIndex-1].GetCssValue("font-family").ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderFontStyle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeaderFontColor", new String[] {"1|text|ColumnIndex|0",
                                                       "2|text|ExpectedValue|MyVal"})]
        public void VerifyColumnHeaderFontColor(String ColumnIndex, String ExpectedValue)
        {
            try
            {
                int iColIndex = Convert.ToInt32(ColumnIndex);
                Initialize();
                RefreshHeaders();
                if (iColIndex > mlstHeaders.Count && iColIndex <= 0)
                {
                    throw new Exception("Column index " + iColIndex + " out of bounds of header indices");
                }
                string fontColor = mlstHeaders[iColIndex - 1].GetCssValue("color");
                if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    fontColor = fontColor.Replace(")", ", " + mlstHeaders[iColIndex - 1].GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                }
                DlkAssert.AssertEqual("VerifyColumnHeaderFontColor() : " + mControlName, ExpectedValue, fontColor);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderFontColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeaderFontSize", new String[] {"1|text|ColumnIndex|0",
                                                       "2|text|ExpectedValue|MyVal"})]
        public void VerifyColumnHeaderFontSize(String ColumnIndex, String ExpectedValue)
        {
            try
            {
                int iColIndex = Convert.ToInt32(ColumnIndex);
                Initialize();
                RefreshHeaders();
                if (iColIndex > mlstHeaders.Count && iColIndex <= 0)
                {
                    throw new Exception("Column index " + iColIndex + " out of bounds of header indices");
                }
                DlkAssert.AssertEqual("VerifyColumnHeaderFontSize() : " + mControlName, ExpectedValue, StandardizeFontSize(mlstHeaders[iColIndex - 1].GetCssValue("font-size")));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderFontSize() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartTitle", new String[] { "1|text|ExpectedValue|Pending Expenses" })]
        public void VerifyDashpartTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement titleLabel = FindTitleElement(mElement);
                if (titleLabel != null)
                {
                    if (titleLabel.Text == "")
                    {
                        throw new Exception("VerifyDashpartTitle() failed : Dashpart not in view");
                    }
                    DlkAssert.AssertEqual("VerifyDashpartTitle() : " + mControlName, ExpectedValue, titleLabel.Text);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartTitleFontStyle", new String[] { "1|text|ExpectedValue|Arial" })]
        public void VerifyDashpartTitleFontStyle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement titleLabel = FindTitleElement(mElement);
                if (titleLabel != null)
                {
                    DlkAssert.AssertEqual("VerifyDashpartTitleFontStyle() : " + mControlName, ExpectedValue.ToLower(), titleLabel.GetCssValue("font-family").ToLower());
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartTitleFontStyle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartTitleFontColor", new String[] { "1|text|ExpectedValue|rgb(0, 0, 0)" })]
        public void VerifyDashpartTitleFontColor(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement titleLabel = FindTitleElement(mElement);
                if (titleLabel != null)
                {
                    string fontColor = titleLabel.GetCssValue("color");
                    if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                    {
                        fontColor = fontColor.Replace(")", ", " + titleLabel.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                    }
                    DlkAssert.AssertEqual("VerifyDashpartTitleFontColor() : " + mControlName, ExpectedValue, fontColor);
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartTitleFontColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartTitleFontSize", new String[] { "1|text|ExpectedValue|10.667px" })]
        public void VerifyDashpartTitleFontSize(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement titleLabel = FindTitleElement(mElement);
                if (titleLabel != null)
                {
                    DlkAssert.AssertEqual("VerifyDashpartTitleFontSize() : " + mControlName, ExpectedValue, StandardizeFontSize(titleLabel.GetCssValue("font-size")));
                }
                else
                {
                    throw new Exception("VerifyDashpartTitleFontSize() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartTitleFontSize() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartXAxisValue", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|20"})]
        public void VerifyChartXAxisValue(String Column, String ExpectedValue)
        {
            try
            {
                int iColumn = Convert.ToInt32(Column);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                string labelToTest = listLabels[sideLabelIndex + iColumn].Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartXAxisValue() : " + mControlName, ExpectedValue, labelToTest);
                }
                else
                {
                    throw new Exception("VerifyChartXAxisValue() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartXAxisValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartYAxisValue", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|20"})]
        public void VerifyChartYAxisValue(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                string labelToTest = listLabels[sideLabelIndex - iRow].Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartYAxisValue() : " + mControlName, ExpectedValue, labelToTest);
                }
                else
                {
                    throw new Exception("VerifyChartYAxisValue() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartYAxisValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartXAxisTitle", new String[] {"1|text|ExpectedValue|Projects"})]
        public void VerifyChartXAxisTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement bottomLabel = mElement.FindElements(By.XPath("./descendant::embed/ancestor::tr/following-sibling::tr//div")).First();
                if (bottomLabel != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartXAxisTitle() : " + mControlName, ExpectedValue, bottomLabel.Text);
                }
                else
                {
                    throw new Exception("VerifyChartXAxisTitle() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartXAxisTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartYAxisTitle", new String[] {"1|text|ExpectedValue|Projects"})]
        public void VerifyChartYAxisTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                string labelToTest = sideLabel.Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (sideLabel != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartYAxisTitle() : " + mControlName, ExpectedValue, labelToTest);
                }
                else
                {
                    throw new Exception("VerifyChartYAxisTitle() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartYAxisTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartXAxisValueFontColor", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|rgba(0, 0, 0, 1)"})]
        public void VerifyChartXAxisValueFontColor(String Column, String ExpectedValue)
        {
            try
            {
                int iColumn = Convert.ToInt32(Column);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                IWebElement labelToTest = listLabels[sideLabelIndex + iColumn];
                string fontColor = labelToTest.GetCssValue("color");
                if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    fontColor = fontColor.Replace(")", ", " + labelToTest.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                }
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartXAxisValueFontColor() : " + mControlName, ExpectedValue, fontColor);
                }
                else
                {
                    throw new Exception("VerifyChartXAxisValueFontColor() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartXAxisValueFontColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartYAxisValueFontColor", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|rgba(0, 0, 0, 1)"})]
        public void VerifyChartYAxisValueFontColor(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                IWebElement labelToTest = listLabels[sideLabelIndex - iRow];
                string fontColor = labelToTest.GetCssValue("color");
                if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    fontColor = fontColor.Replace(")", ", " + labelToTest.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                }
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartYAxisValueFontColor() : " + mControlName, ExpectedValue, fontColor);
                }
                else
                {
                    throw new Exception("VerifyChartYAxisValueFontColor() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartYAxisValueFontColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartXAxisValueFontStyle", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|Arial"})]
        public void VerifyChartXAxisValueFontStyle(String Column, String ExpectedValue)
        {
            try
            {
                int iColumn = Convert.ToInt32(Column);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                IWebElement labelToTest = listLabels[sideLabelIndex + iColumn];
                string fontStyle = labelToTest.GetCssValue("font-family").ToLower();
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartXAxisValueFontStyle() : " + mControlName, ExpectedValue.ToLower(), fontStyle);
                }
                else
                {
                    throw new Exception("VerifyChartXAxisValueFontStyle() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartXAxisValueFontStyle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartYAxisValueFontStyle", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|Arial"})]
        public void VerifyChartYAxisValueFontStyle(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                IWebElement labelToTest = listLabels[sideLabelIndex - iRow];
                string fontStyle = labelToTest.GetCssValue("font-family").ToLower();
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartYAxisValueFontStyle() : " + mControlName, ExpectedValue.ToLower(), fontStyle);
                }
                else
                {
                    throw new Exception("VerifyChartYAxisValueFontStyle() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartYAxisValueFontStyle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartXAxisValueFontSize", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|9px"})]
        public void VerifyChartXAxisValueFontSize(String Column, String ExpectedValue)
        {
            try
            {
                int iColumn = Convert.ToInt32(Column);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                IWebElement labelToTest = listLabels[sideLabelIndex + iColumn];
                string fontSizeValue = labelToTest.GetCssValue("font-size");
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartXAxisValueFontSize() : " + mControlName, ExpectedValue, StandardizeFontSize(fontSizeValue));
                }
                else
                {
                    throw new Exception("VerifyChartXAxisValueFontSize() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartXAxisValueFontSize() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartYAxisValueFontSize", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|9px"})]
        public void VerifyChartYAxisValueFontSize(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                IWebElement labelToTest = listLabels[sideLabelIndex - iRow];
                string fontSizeValue = labelToTest.GetCssValue("font-size");
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (labelToTest != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartYAxisValueFontSize() : " + mControlName, ExpectedValue, StandardizeFontSize(fontSizeValue));
                }
                else
                {
                    throw new Exception("VerifyChartYAxisValueFontSize() failed : label control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartYAxisValueFontSize() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartYAxisRowCount", new String[] { "1|text|ExpectedRowCount|20" })]
        public void VerifyChartYAxisRowCount(String ExpectedRowCount)
        {
            try
            {
                int iExpRowCount = Convert.ToInt32(ExpectedRowCount);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentLabel = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLabels = parentLabel.FindElements(By.XPath("./descendant::*[name()='text']"));
                IWebElement sideLabel = parentLabel.FindElements(By.XPath(mstrDashpartYAxisTitleXPATHString)).First();
                int sideLabelIndex = listLabels.IndexOf(sideLabel);
                int iActRowCount = listLabels.Count - (listLabels.Count - sideLabelIndex);
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                DlkAssert.AssertEqual("VerifyChartYAxisRowCount() : " + mControlName, iExpRowCount, iActRowCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartYAxisRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartGraphBarColor", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|rgba(0, 0, 0, 1)"})]
        public void VerifyChartGraphBarColor(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentBar = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listBars = parentBar.FindElements(By.XPath("./descendant::*[contains(@clip-path, 'url') and contains(@style, 'fill:#')]"));
                IWebElement barToTest = listBars[listBars.Count - iRow];
                string color = barToTest.GetCssValue("fill");
                if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    color = color.Replace(")", ", " + barToTest.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                }
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (color != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartGraphBarColor() : " + mControlName, ExpectedValue, color);
                }
                else
                {
                    throw new Exception("VerifyChartGraphBarColor() failed : bar control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartGraphBarColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartGraphPieColor", new String[] {"1|text|Index|1",
                                                       "2|text|ExpectedValue|rgba(0, 0, 0, 1)"})]
        public void VerifyChartGraphPieColor(String Index, String ExpectedValue)
        {
            try
            {
                int idx = Convert.ToInt32(Index);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentPie = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listPies = parentPie.FindElements(By.XPath("./descendant::*[contains(@style, 'stroke-linecap:square')]/preceding-sibling::*[1]"));
                IWebElement pieToTest = listPies[idx - 1];
                string color = pieToTest.GetCssValue("fill");
                if (DlkEnvironment.mBrowser.ToLower() == "firefox" || DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    color = color.Replace(")", ", " + pieToTest.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
                }
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (color != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartGraphPieColor() : " + mControlName, ExpectedValue, color);
                }
                else
                {
                    throw new Exception("VerifyChartGraphPieColor() failed : pie control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartGraphPieColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartGraphPieTitle", new String[] {"1|text|ExpectedValue|SampleValue"})]
        public void VerifyChartGraphPieTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentPie = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IWebElement pieTitle = parentPie.FindElements(By.XPath("./descendant::*[contains(@style, 'stroke:none')]/following-sibling::*[name()='text']")).First();
                string title = pieTitle.Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (title != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartGraphPieTitle() : " + mControlName, ExpectedValue, title);
                }
                else
                {
                    throw new Exception("VerifyChartGraphPieTitle() failed : pie control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartGraphPieTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartGraphPieLegend", new String[] {"1|text|Index|1",
                                                       "2|text|ExpectedValue|SampleValue"})]
        public void VerifyChartGraphPieLegend(String Index, String ExpectedValue)
        {
            try
            {
                int idx = Convert.ToInt32(Index);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentPie = DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).First();
                IList<IWebElement> listLegendItems = parentPie.FindElements(By.XPath("./descendant::*[contains(@style, 'stroke-linecap:square')]/following-sibling::*[name()='text']"));
                string legendText = listLegendItems[idx - 1].Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (legendText != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartGraphPieLegend() : " + mControlName, ExpectedValue, legendText);
                }
                else
                {
                    throw new Exception("VerifyChartGraphPieLegend() failed : pie control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartGraphPieLegend() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartGraphBarTooltip", new String[] {"1|text|RowIndex|0",
                                                       "2|text|ExpectedValue|SampleValue"})]
        public void VerifyChartGraphBarTooltip(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String originalUrl = DlkEnvironment.AutoDriver.Url;
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentBar = DlkEnvironment.AutoDriver.FindElements(By.Id("hotSpots")).First();
                IList<IWebElement> listTooltips = parentBar.FindElements(By.XPath(".//*[name()='title']"));
                IWebElement tooltipToTest = listTooltips[listTooltips.Count - iRow];
                string tooltipText = tooltipToTest.Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (tooltipText != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartGraphBarTooltip() : " + mControlName, ExpectedValue, tooltipText);
                }
                else
                {
                    throw new Exception("VerifyChartGraphBarTooltip() failed : tooltip control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartGraphBarTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartGraphPieTooltip", new String[] {"1|text|Index|1",
                                                       "2|text|ExpectedValue|SampleValue"})]
        public void VerifyChartGraphPieTooltip(String Index, String ExpectedValue)
        {
            try
            {
                int idx = Convert.ToInt32(Index);
                Initialize();
                IWebElement chartElement = mElement.FindElements(By.XPath("./descendant::embed")).First();
                String chartAttribute = chartElement.GetAttribute("src");
                ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("window.open('" + chartAttribute + "', 'new_window')");
                CheckEmbeddedElement();
                IWebElement parentPie = DlkEnvironment.AutoDriver.FindElements(By.Id("hotSpots")).First();
                IList<IWebElement> listTooltips = parentPie.FindElements(By.XPath(".//*[name()='title']"));
                string toolTipText = listTooltips[idx - 1].Text;
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.First());
                if (toolTipText != null)
                {
                    Initialize();
                    DlkAssert.AssertEqual("VerifyChartGraphPieTooltip() : " + mControlName, ExpectedValue, toolTipText);
                }
                else
                {
                    throw new Exception("VerifyChartGraphPieTooltip() failed : tooltip control not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartGraphPieTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartIndex", new String[] { "1|text|ExpectedIndex|1" })]
        public void VerifyDashpartIndex(String ExpectedIndex)
        {
            try
            {
                int iDashpartIndex = Convert.ToInt32(ExpectedIndex);
                Initialize();
                IWebElement titleLabel = FindTitleElement(mElement);
                if (titleLabel != null)
                {
                    string sTitleToFind = titleLabel.Text;
                    IList<IWebElement> mlstDashparts = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='reportpage' and contains(@style, 'display: block')]")).ToList();
                    IWebElement dashpartTitleLabel;
                    for (int i = 0; i < mlstDashparts.Count; i++)
                    {
                        dashpartTitleLabel = FindTitleElement(mlstDashparts[i]);

                        if (dashpartTitleLabel.Text == sTitleToFind)
                        {
                            DlkAssert.AssertEqual("Dashpart Index", ExpectedIndex, (i+1).ToString());
                            return;
                        }
                    }
                    throw new Exception("VerifyDashpartIndex() failed : Title not found in any dashpart");
                }
                else
                {
                    throw new Exception("VerifyDashpartIndex() failed : Dashpart has no title");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickDashpartButton", new String[] { "1|text|ButtonCaption|Edit" })]
        public void ClickDashpartButton(String ButtonCaption)
        {
            try
            {
                Initialize();
                IWebElement buttonToClick = mElement.FindElement(By.XPath(".//div[@title='" + ButtonCaption + "']"));
                buttonToClick.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickDashpartButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartButtonExists", new String[] {"1|text|ButtonCaption|Edit",
                                                             "2|text|ExpectedValue|TRUE"})]
        public void VerifyDashpartButtonExists(String ButtonCaption, String ExpectedValue)
        {
            try
            {
                Initialize();
                bool actualValue = false;
                if (mElement.FindElements(By.XPath(".//div[@title='" + ButtonCaption + "']")).Any())
                {
                    actualValue = true;
                }
                DlkAssert.AssertEqual("VerifyDashpartButtonExists() : ", Convert.ToBoolean(ExpectedValue), actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartButtonExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartCount", new String[] { "1|text|DashpartCount|8" })]
        public void VerifyDashpartCount(String ExpectedCount)
        {
            try
            {
                int mDashpartCount = Convert.ToInt32(ExpectedCount);
                int mCurrentDashpartCount = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='reportpage']")).Count();
                DlkAssert.AssertEqual("VerifyDashpartCount() : ", mDashpartCount, mCurrentDashpartCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartCount() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get table column index based on header value
        /// </summary>
        /// <param name="sColumnHeader">Header to match</param>
        private int GetColumnIndexByHeader(String sColumnHeader)
        {
            int index = -1;
            
            RefreshHeaders();
            if (sColumnHeader == String.Empty && mElement.FindElements(By.XPath(".//span[@class='dLinkD']")).Count > 0)
            {
                index = 0;
                return index;
            }
            for (int i = 0; i < mlstHeaders.Count; i++)
            {
                string currentHeader = GetColumnHeaderText(mlstHeaders[i]).Trim();
                if (currentHeader == String.Empty && sColumnHeader == String.Empty)
                {
                    if (mlstHeaders[i].FindElements(By.XPath(".//input[@type='checkbox']")).Any())
                    {
                        index = i;
                        break;
                    }
                }
                else if (sColumnHeader.Trim() == currentHeader)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// Gets all header values from the element
        /// </summary>
        private void RefreshHeaders()
        {
            FindElement();

            mlstHeaders = new List<IWebElement>();
            mlstHeaderTexts = new List<String>();

            IList<IWebElement> headers = mElement.FindElements(By.XPath("./descendant::span[contains(@class, 'dHC')]"));
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

        /// <summary>
        /// Checks if embedded element exists, switches index if it cannot find embedded element
        /// </summary>
        private void CheckEmbeddedElement()
        {
            for (int retryCount = 1; retryCount <=3; retryCount++)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles.Last());
                if (DlkEnvironment.AutoDriver.FindElements(By.Id("outerG")).Any())
                {
                    DlkLogger.LogInfo("Embedded element found.");
                    return;
                }
                DlkLogger.LogInfo("Cannot locate embedded element. Retrying...");
                Thread.Sleep(1500);
            }
        }

        /// <summary>
        /// Gets all row values from the element
        /// </summary>
        private void RefreshRows()
        {
            FindElement();
            mlstRows = mElement.FindElements(By.ClassName(mstrRowClass));
        }

        /// <summary>
        /// Gets column header text value from column header element
        /// </summary>
        /// <param name="columnHeader">Header element to get value from</param>
        private String GetColumnHeaderText(IWebElement columnHeader)
        {
            var header = new DlkBaseControl("ColumnHeader", columnHeader);
            String headerText = DlkEnvironment.mBrowser.ToLower() == "safari" ? header.GetAttributeValue("innerText")
                : header.GetValue();
            if (headerText.Contains("<span") || headerText.Contains("<input"))
            {
                headerText = RemoveHTMLTagsInHeader(headerText);
            }
            return DlkString.RemoveCarriageReturn(headerText.Trim()).Replace("/ ", "/"); // trim of space after slash for some headers
        }

        /// <summary>
        /// Gets cell element based on given row and column indices
        /// </summary>
        /// <param name="iRow">Row index of target cell</param>
        /// <param name="iColumn">Column index of target cell</param>
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
            foreach (IWebElement item in row.FindElements(By.XPath("./descendant::input[@type='checkbox'] | ./descendant::span[@class='dData' or @class='dLinkD']")))
            {
                // do not include un-displayed cells
                if (item.GetCssValue("display") != "none")
                {
                    cells.Add(item);
                }
            }
            cell = cells[iColumn];
            return cell;
        }

        /// <summary>
        /// Removes HTML tags from header string
        /// </summary>
        /// <param name="strToFormat">String to remove HTML tags from</param>
        private string RemoveHTMLTagsInHeader(String strToFormat)
        {
            return Regex.Replace(strToFormat, @"<[^>]+>|&nbsp;", "").Trim();
        }

        /// <summary>
        /// Standardizes font sizes across all browsers
        /// </summary>
        /// <param name="strToFormat">String to standardize</param>
        private string StandardizeFontSize(String strToFormat)
        {
            string fontSizeValue = strToFormat;
            double fontSize = Convert.ToDouble(fontSizeValue.Replace("px", ""));
            fontSize = Math.Floor(fontSize * 100) / 100;
            fontSizeValue = fontSize.ToString() + "px";
            return fontSizeValue;
        }

        /// <summary>
        /// Looks for the needed title element in the dashpart
        /// </summary>
        private IWebElement FindTitleElement(IWebElement dashpartElement)
        {
            IWebElement titleElement = dashpartElement.FindElements(By.XPath(mstrChartTitleXPATHString)).Any() ?
                                        dashpartElement.FindElement(By.XPath(mstrChartTitleXPATHString)) :
                                        dashpartElement.FindElement(By.XPath(mstrTableTitleXPATHString));
            titleElement = titleElement.FindElements(By.XPath(mstrChartMainTitleXPATHString)).Any() ?
                            titleElement.FindElement(By.XPath(mstrChartMainTitleXPATHString)) :
                            titleElement;
            return titleElement;
        }

        #endregion

    }
}