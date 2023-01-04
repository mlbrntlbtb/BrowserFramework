using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using SFTLib.DlkControls.Concrete.Grid;
using SFTLib.DlkControls.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using System.Data;
using CommonLib.DlkRecords;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace SFTLib.DlkControls
{
    [ControlType("Grid")]
    public class DlkGrid : DlkBaseControl
    {
        IGrid grid;
        private const string gridFooterXPath = @"./ancestor::*[contains(@class,'rich-tabpanel-content')]//*[contains(@class,'panelGrid_footer')]";

        #region Constructors
        public DlkGrid(String ControlName, String SearchType, String SearchValue)
          : base(ControlName, SearchType, SearchValue) { }
        public DlkGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(strExpectedValue), ControlExists(2));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("VerifyCellValue")]
        public void VerifyCellValue(String ColumnHeaderName, String RowNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                var cell = grid.GetCellElement(ColumnHeaderName, RowNumber);
                DlkAssert.AssertEqual("VerifyCellValue()", ExpectedValue.ToLower().Trim(), cell.GetCellValue().ToLower().Trim());
            }
            catch (Exception exception)
            {
                throw new Exception("VerifyCellValue() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("SelectAllRows")]
        public void SelectAllRows()
        {
            try
            {
                Initialize();
                var selectAllRowsButton = mElement.FindElement(By.XPath(".//*[contains(@title,'Select All Rows')]"));
                selectAllRowsButton.Click();
                DlkLogger.LogInfo("SelectAllRows() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("SelectAllRows() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("SelectRow")]
        public void SelectRow(string RowNumber)
        {
            try
            {
                Initialize();
                grid.SelectRow(RowNumber);
                DlkLogger.LogInfo("SelectRow() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("SelectRow() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("RightClickCell")]
        public void RightClickCell(string ColumnHeaderName, string RowNumber, string ItemToSelect)
        {
            try
            {
                Initialize();
                var cell = grid.GetCellElement(ColumnHeaderName, RowNumber);
                cell.RightClickOnElement(ItemToSelect);
                DlkLogger.LogInfo("RightClickCell() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("RightClickCell() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("SelectRowWithColumnValue")]
        public void SelectRowWithColumnValue(string ColumnHeaderName, string ItemToSelect)
        {
            try
            {
                Initialize();
                var cell = grid.GetCellElementByContent(ColumnHeaderName, ItemToSelect);
                cell.Click();
                DlkLogger.LogInfo("SelectRowWithColumnValue() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("SelectRowWithColumnValue() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("SetRowsPerPage")]
        public void SetRowsPerPage(string RowsPerPage)
        {
            try
            {
                Initialize();
                var parentElements = mElement.FindElements(By.XPath(gridFooterXPath + @"//*[contains(text(),'Rows per page')]/ancestor::tbody[1]//*[contains(@class,'pager')]//*[@class='rich-inplace-field']/parent::*"));
                var childElements = parentElements[0].FindElements(By.XPath(".//*[@class='rich-inplace-field']"));

                if (childElements.Count == 0)
                    throw new Exception("Rows per page not found.");

                parentElements[0].Click();
                childElements[0].SendKeys(Keys.Control + "a");
                childElements[0].SendKeys(RowsPerPage);
                childElements[0].SendKeys(Keys.Return);

                DlkLogger.LogInfo("SetRowsPerPage() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("SetRowsPerPage() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("SetPageNavigatorInput")]
        public void SetPageNavigatorInput(string PageNumber)
        {
            try
            {
                Initialize();
                var parentElements = mElement.FindElements(By.XPath(gridFooterXPath + @"//*[@class='rich-inplace-field'][1]/parent::*"));
                var childElements = parentElements[0].FindElements(By.XPath(".//*[@class='rich-inplace-field']"));

                if (childElements.Count == 0)
                    throw new Exception("Page navigator not found.");

                parentElements[0].Click();
                childElements[0].SendKeys(Keys.Control + "a");
                childElements[0].SendKeys(PageNumber);
                childElements[0].SendKeys(Keys.Return);

                DlkLogger.LogInfo("SetPageNavigatorInput() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("SetPageNavigatorInput() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("VerifyRowCount")]
        public void VerifyRowCount(String RowCount)
        {
            try
            {
                Initialize();

                //V1
                if (mElement.GetAttribute("class").Contains("rich-sdt"))
                {
                    //v1 has a textbox that displays the number of row results
                    var itemsFound = mElement.FindElements(By.XPath(gridFooterXPath + "//*[contains(text(),'Items found')]/following-sibling::*"));

                    if (itemsFound.Count > 0)
                        DlkAssert.AssertEqual("VerifyRowCount()", RowCount, itemsFound.FirstOrDefault().Text);
                    else
                        throw new Exception("No rows were found.");
                }
                else
                {
                    var gridRowCount = grid.GetRows().Count();

                    if (gridRowCount > 0)
                        DlkAssert.AssertEqual("VerifyRowCount", Convert.ToInt32(RowCount), gridRowCount);
                    else
                        throw new Exception("No rows were found.");
                }

                DlkLogger.LogInfo("VerifyRowCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyGridRowExistWithColumnValue", new String[] {"1|text|Column Header|Sample Column Header",
                                                                    "2|text|Value|Sample Value",
                                                                    "3|text|Expected Value|True or False"})]
        public void VerifyGridRowExistWithColumnValue(String ColumnHeader, String Value, String ExpectedValue)
        {
            try
            {
                Initialize();
                ExpandPanels();

                var cell = grid.GetCellElementByContent(ColumnHeader, Value);

                DlkAssert.AssertEqual("VerifyGridRowExistWithColumnValue() : " + mControlName, Convert.ToBoolean(ExpectedValue.ToLower()), cell != null ? true : false);

                DlkLogger.LogInfo("VerifyGridRowExistWithColumnValue() passed");
            }
            catch (Exception exception)
            {
                throw new Exception("VerifyGridRowExistWithColumnValue() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyGridNoRecord")]
        public void VerifyGridNoRecord(string strExpectedValue)
        {
            try
            {
                Initialize();
                var recordNoLabel = mElement.FindElements(By.XPath("./ancestor::*[2]//*[contains(@class,'x-toolbar-item')][last()]")).Where(elem => !String.IsNullOrEmpty(elem.Text.Trim())).ToList();
                if (recordNoLabel.Count() > 0)
                {
                    DlkAssert.AssertEqual("VerifyGridRecordNo() : " + mControlName, strExpectedValue, recordNoLabel[0].Text);
                }
                else
                {
                    throw new Exception("VerifyGridRecordNo() failed : Record No label not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridRecordNo() failed : " + e.ToString());
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyGridColumnHeaders", new String[] { "1|text|Expected header texts|Header1~Header2~Header3" })]
        public void VerifyGridColumnHeaders(String ExpectedHeaders)
        {
            try
            {
                Initialize();
                ExpandPanels();

                var columnHeaders = grid.GetGridHeaders()
                    .Select(column =>
                    {
                        new DlkBaseControl("column", column).ScrollIntoViewUsingJavaScript();
                        return column.Text;
                    }).Where(column => !string.IsNullOrEmpty(column.Trim())).ToList();
                var columnHeadersWithDelimiter = String.Join("~", columnHeaders);
                DlkAssert.AssertEqual("VerifyGridColumnHeaders() : " + mControlName, ExpectedHeaders, columnHeadersWithDelimiter);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableColumnHeaders() failed : " + e.ToString());
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyGridCellStrikethrough", new String[] {"1|text|Row number|1",
                                                   "2|text|Column Header|Line*",
                                                   "3|text|Expected Value|True or False"})]
        public void VerifyGridCellStrikethrough(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();
                var cell = grid.GetCellElement(ColumnHeader, Row);
                if (cell == null)
                {
                    throw new Exception("Cell value in row = '" + Row + "' under Column = '" + ColumnHeader + "' not found in table");
                }
                else
                {
                    if (cell.FindElements(By.XPath("./parent::td")).Any())
                    {
                        bool actualResult = cell.FindElements(By.XPath("./parent::td[contains(@style, 'text-decoration: line-through')]")).Any();
                        DlkAssert.AssertEqual("VerifyGridCellStrikethrough", Convert.ToBoolean(ExpectedValue), actualResult);
                    }
                    else
                    {
                        throw new Exception("No cell were found.");
                    }
                }

                DlkLogger.LogInfo("Successfully executed VerifyGridCellStrikethrough()");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridCellStrikethrough() failed : " + e.ToString());
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyGridRowWithMultipleColumnValues", new String[] {"1|text|Column Headers|Header1~Header2~Header3",
                                                                        "2|text|Values|Value1~Value2~Value3",
                                                                        "3|text|Expected Value|True or False"})]
        public void VerifyGridRowWithMultipleColumnValues(String ColumnHeaders, String Values, String ExpectedValue)
        {
            try
            {
                String[] headers = ColumnHeaders.Split('~');
                String[] values = Values.Split('~');
                bool expectedResult;
                int result = -1;

                if (!bool.TryParse(ExpectedValue, out expectedResult))
                    throw new Exception("Invalid expected value [" + expectedResult + "]");

                if (headers.Count() != values.Count())
                    throw new Exception("Column Headers count = " + headers.Count() + " not equal to Values count = " + values.Count());

                Initialize();
                ExpandPanels();

                var rows = grid.GetRows();
                if (rows.Count() > 0)
                {
                    IList<int> columnIndexes = new List<int>();

                    //get column indexes. this will serve as a locator for the values parameter for the rows
                    foreach (var headerValue in headers)
                        columnIndexes.Add(grid.GetGridHeaders().FindIndex(header => header.Text == headerValue));

                    foreach (var row in rows)
                    {
                        //find the cells in the row that match the values provided
                        var subCells = row.Where(subcell => columnIndexes.Any(index => row.IndexOf(subcell) == index));

                        //if the values provided are found in the same row, return the row's index
                        if (subCells.Count() > 0 && subCells.Count(cell => values.Any(value => value == grid.GetGridCellText(cell))) == values.Count())
                        {  
                            result = rows.IndexOf(row) + 1;
                            break;
                        }
                    }
                    DlkAssert.AssertEqual("VerifyGridRowWithMultipleColumnValues()", expectedResult.ToString(), (result > 0).ToString());
                }
                else
                {
                    // Actual result will be false if no row found in the table.
                    DlkAssert.AssertEqual("VerifyGridRowWithMultipleColumnValues()", expectedResult.ToString(), (false).ToString());
                }
            }
            catch (Exception exception)
            {
                throw new Exception("VerifyGridRowWithMultipleColumnValues() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }
        
        [Keyword("GetGridRowWithColumnValue", new String[] {"1|text|Column Header|Line*",
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public void GetGridRowWithColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            try
            {
                Initialize();
                ExpandPanels();

                int number = grid.GetCellRowNumber(ColumnHeader, Value);
                DlkVariable.SetVariable(VariableName, number.ToString());

                if (number == 0)
                    throw new Exception("Value = '" + Value + "' under Column = '" + ColumnHeader + "' not found in table");
                else
                    DlkLogger.LogInfo("Successfully executed GetGridRowWithColumnValue()");
            }
            catch(Exception e)
            {
                throw new Exception("GetGridRowWithColumnValue() failed : " + e.ToString());
            }
        }

        [Keyword("GetGridCellValue", new String[] {"1|text|Row number|1",
                                                   "2|text|Column Header|Line*",
                                                   "3|text|VariableName|MyRow"})]
        public void GetGridCellValue(String Row, String ColumnHeader, String VariableName)
        {
            try
            {
                Initialize();

                var cell = grid.GetCellElement(ColumnHeader, Row);
                if (cell == null)
                    throw new Exception("Cell value in row = '" + Row + "' under Column = '" + ColumnHeader + "' not found in table");

                DlkVariable.SetVariable(VariableName, cell.Text.ToString().Trim());

                DlkLogger.LogInfo("Successfully executed GetGridCellValue()");

            }
            catch (Exception e)
            {
                throw new Exception("GetGridCellValue() failed : " + e.ToString());
                throw;
            }
        }

      [Keyword("GetGridRowWithMultipleColumnValues", new String[] {"1|text|Column Headers|Header1~Header2~Header3",
                                                                      "2|text|Values|Value1~Value2~Value3",
                                                                        "3|text|VariableName|MyRow"})]
        public void GetGridRowWithMultipleColumnValues(String ColumnHeaders, String Values, String VariableName)
        {
            try
            {
                String[] columnHeaders = ColumnHeaders.Split('~');
                String[] columnValues = Values.Split('~');

                if (columnHeaders.Count() != columnValues.Count())
                    throw new Exception("Column Headers count = " + columnHeaders.Count() + " not equal to Values count = " + columnValues.Count());

                Initialize();
                ExpandPanels();

                int result = -1;
                var rows = grid.GetRows().Count() > 0 ? grid.GetRows() : throw new Exception("No rows were found on grid");
                IList<int> columnIndexes = new List<int>();

                //get column indexes. this will serve as a locator for the values parameter for the rows
                foreach (var headerValue in columnHeaders)
                    columnIndexes.Add(grid.GetGridHeaders().FindIndex(header => header.Text == headerValue));

                foreach (var row in rows)
                {
                    //find the cells in the row that match the values provided
                    var subCells = row.Where(subcell => columnIndexes.Any(index => row.IndexOf(subcell) == index));

                    //if the values provided are found in the same row, return the row's index
                    if (subCells.Count() > 0 && subCells.Count(cell => columnValues.Any(value => value == cell.Text)) == columnValues.Count())
                    {
                        result = rows.IndexOf(row) + 1;
                        break;
                    }
                }

                if (result < 0) throw new Exception("Unable to find row with column/values provided.");

               
                DlkVariable.SetVariable(VariableName, result.ToString());
                DlkLogger.LogInfo("Successfully executed GetGridRowWithColumnValue()");
            }
            catch (Exception e)
            {
                throw new Exception("GetGridRowWithColumnValue() failed : " + e.ToString());
            }
        }
        public Boolean ControlExists(int iSecsToWait)
        {
            Boolean bExists = false;
            try
            {
                FindElement(iSecsToWait);
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    ScrollIntoView();
                else
                    ScrollIntoViewUsingJavaScript();
                if (mElement.Displayed)
                {
                    bExists = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == DlkLogger.STR_CANCELLATION_MESSAGE)
                {
                    throw;
                }
                bExists = false;
            }
            return bExists;
        }

        [Keyword("CompareGridToCsv")]
        public void CompareGridToCsv()
        {
            try
            {
                Initialize();
                string downloadsFilePath = GetLocalMachineDownloadsFolder();
                var directory = new DirectoryInfo(downloadsFilePath);
                var myFile = directory.GetFiles()
                             .OrderByDescending(f => f.LastWriteTime)
                             .FirstOrDefault();
                // check if myFile is CSV
                if (Path.GetExtension(myFile.ToString()) == ".csv")
                {
                    var latestCsv = downloadsFilePath + myFile;
                    string csvOutput = File.ReadAllText(latestCsv),
                           csvOutputFormatted = Regex.Replace(csvOutput, "\n", ",");
                    List<string> gridHeader = grid.GetGridHeaders().Select(column => column.Text).ToList(),
                                 gridValue = grid.GetGridValues().Select(column => column.Text).ToList();
                    string headers = string.Join(",", gridHeader.ToArray()),
                           values = Regex.Replace(string.Join(",", gridValue.ToArray()), " *, *", ","),
                           gridContents = string.Concat(headers, ",", values, ",");
                    DlkAssert.AssertEqual("CompareGridToCsv()", csvOutputFormatted, gridContents);
                    DlkLogger.LogInfo("Successfully executed CompareGridToCsv()");
                }
                else
                {
                    DlkLogger.LogInfo("CompareGridToCsv() failed: Latest file is not in CSV format.");
                    throw new Exception();
                }
            }
            catch(IOException io)
            {
                throw new Exception("CompareGridToCsv() failed: File is in use or not accessible [EXCEPTION]:" + io.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("CompareGridToCsv() failed : " + e.ToString());
            }
        }
        [Keyword("SortGridColumn", new String[] {"1|text|Column Header|Line*", "2|text|IsAscending|True or False" })]
        public void SortGridColumn(string ColumnHeaderName, string IsAscending)
        {
            try
            {
                Initialize();
                
                if(Convert.ToBoolean(IsAscending.ToLower()))
                    SortAscendingColumn(ColumnHeaderName, Convert.ToBoolean(IsAscending.ToLower()));
                else
                    SortDescendingColumn(ColumnHeaderName, Convert.ToBoolean(IsAscending.ToLower()));                
            }
            catch (Exception exception)
            {
                throw new Exception("SortGridColumn() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("ClickModifyOnRow", new String[] {"1|Number|Row Number|1" })]
        public void ClickModifyOnRow(string RowNumber)
        {
            try
            {
                Initialize();

                int rowNumber;
                int.TryParse(RowNumber, out rowNumber);
                var actionRows = mElement.FindElements(By.XPath(grid.ActionRowsXpath)).ToList();

                if (actionRows.Count() > rowNumber && rowNumber < 1)
                    throw new Exception($"Invalid row number [{rowNumber}]");

                var modifyButton = actionRows[rowNumber-1].FindElements(By.XPath(grid.ModifyButtonXpath)).FirstOrDefault();

                if (modifyButton == null)
                    throw new Exception("modifyButton not found.");

                modifyButton.Click();
                DlkLogger.LogInfo("Successfully executed ClickModifyOnRow()");
            }
            catch (Exception exception)
            {
                throw new Exception("ClickModifyOnRow() failed : " + exception);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyGridCellCheckBoxValue", new String[] {"1|text|Column Header|Sample Column Header",
                                                                    "2|text|Value|Sample Row Number",
                                                                    "3|text|Expected Value|True or False"})]
        public void VerifyGridCellCheckBoxValue(string ColumnHeaderName, string RowNumber, string ExpectedValue)
        {
            try
            {
                Initialize();
                if (!mElement.GetAttribute("class").Contains("rich-sdt"))
                {
                    bool ExpectedResult;
                    bool.TryParse(ExpectedValue, out ExpectedResult);
                    bool ActualResult = false;
                    int rowNumber;
                    Int32.TryParse(RowNumber, out rowNumber);

                    if (!(rowNumber > 0))
                        throw new Exception("Invalid row number. must be greater than or equal to 1.");

                    var columns = mElement.FindElements(By.XPath(@".//*[contains(@class,'x-column-header-inner')]")).GetWebElementsValues();
                    var columnHeaderIndex = columns.IndexOf(columns.Where(column => column.Trim() == ColumnHeaderName).FirstOrDefault()) +1 ;

                    List<IWebElement> gridRows = mElement.FindElements(By.XPath("(.//table[contains(@class,'x-grid-table')])[last()]//*[contains(@class,'x-grid-data-row')]")).ToList();

                    if(rowNumber > gridRows.Count())
                        throw new Exception($"Invalid row number ({rowNumber}). Must not exceed the actual total number of rows ({gridRows})");

                    if (gridRows.Count() > 0)
                    {
                        var cell = gridRows[rowNumber - 1].FindElements(By.XPath($".//td[{columnHeaderIndex}]//input[@type='checkbox']")).FirstOrDefault();
                        if (cell != null)
                            ActualResult = !string.IsNullOrEmpty(cell.GetAttribute("checked"));
                        else
                            throw new Exception($"CheckBox not found in cell with column name of ({ColumnHeaderName}) and row number of ({rowNumber}).");
                    }
                    else
                        throw new Exception($"No rows found.");

                    DlkAssert.AssertEqual("VerifyGridCellCheckBoxValue()", ExpectedResult, ActualResult);
                }
                else
                    throw new Exception("Is only applicable for v1");
            }
            catch(Exception e)
            {
                throw new Exception("VerifyGridCellCheckBoxValue() : " + e);
            }
        }

        [Keyword("GetGridRowWithColumnValue", new String[] {"1|text|Column Headers|Line*",
                                                            "2|text|Values|1",
                                                            "3|text|VariableName|MyRow"})]
        public void GetGridRowWithMultipleColumnValue(String ColumnHeaders, String Values, String VariableName)
        {
            try
            {
                Initialize();
                int number = grid.GetCellRowNumber(ColumnHeaders, Values);
                DlkVariable.SetVariable(VariableName, number.ToString());

                if (number == 0)
                    throw new Exception("Value = '" + Values + "' under Column = '" + ColumnHeaders + "' not found in table");
                else
                    DlkLogger.LogInfo("Successfully executed GetGridRowWithColumnValue()");
            }
            catch (Exception e)
            {
                throw new Exception("GetGridRowWithColumnValue() failed : " + e.ToString());
            }
        }

        [Keyword("GetRowCount", new String[] {"|text|VariableName|MyRow"})]
        public void GetRowCount(String VariableName)
        {
            try
            {
                Initialize();

                //V1
                if (mElement.GetAttribute("class").Contains("rich-sdt"))
                {
                    //v1 has a textbox that displays the number of row results
                    var itemsFound = mElement.FindElements(By.XPath(gridFooterXPath + "//*[contains(text(),'Items found')]/following-sibling::*"));

                    if (itemsFound.Count > 0)
                        DlkVariable.SetVariable(VariableName, itemsFound.FirstOrDefault().Text);
                    else
                        throw new Exception("No rows were found.");
                }
                else
                {
                    var gridRowCount = grid.GetRows().Count();

                    if (gridRowCount > 0)
                        DlkVariable.SetVariable(VariableName, gridRowCount.ToString());
                    else
                        throw new Exception("No rows were found.");
                }

                DlkLogger.LogInfo("GetRowCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e);
            }
            finally
            {
                Terminate();
            }
        }

        public string GetLocalMachineDownloadsFolder()
        {
            // Identify the users "user" directory
            string userPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            DirectoryInfo user = new DirectoryInfo(userPath);
            if (user.Exists)
            {
                // Identify the "%USERPROFILE%\Downloads" directory on Windows Vista, 7, 8 systems.
                DirectoryInfo downloads = new DirectoryInfo(user + @"\Downloads\");
                if (downloads.Exists)
                {
                    // return the full path "C:\Users\USERNAME\Downloads"
                    return downloads.FullName;
                }
                else
                {
                    // Couldn't find it, maybe they're on Windows XP
                    string xpDocs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                    DirectoryInfo xpDownloads = new DirectoryInfo(xpDocs + @"\Downloads\");
                    if (xpDownloads.Exists)
                    {
                        // return the full path "C:\Documents and Settings\USERNAME\My Documents\Downloads"
                        return xpDownloads.FullName;
                    }
                    else
                    {
                        // Couldn't identify a "Downloads" directory in either location
                        throw new DirectoryNotFoundException("Cannot identify the users 'Downloads' directory.");
                    }
                }
            }
            else
            {
                // Couldn't identify a "%USERPROFILE%" folder. Shouldn't ever happen...
                throw new DirectoryNotFoundException("Cannot identify the users default directory.");
            }
        }
        [Keyword("SelectMenuFromColumnHeader", new String[] {"1|text|Column Header|Header Text",
                                                                      "2|text|MenuParameters|Value1~Value2~Value3"})]
        public void SelectMenuFromColumnHeader(String ColumnHeaderText, String Parameters)
        {
            try
            {
                Initialize();
                grid.HoverOnColumnHeader(ColumnHeaderText);

                var menu = DlkEnvironment.AutoDriver.FindElements(By.XPath(grid.ColumnMenuXpath != null ? grid.ColumnMenuXpath : throw new NotImplementedException("SelectMenuFromColumnHeader() not implemented for SFT version 1."))).FirstOrDefault(elem => elem.Displayed);

                var menuCtrl = menu != null ? new DlkMenu("Menu", menu) : throw new Exception($"Menu control on column header {ColumnHeaderText} not found.");

                menuCtrl.Select(Parameters);
                DlkLogger.LogInfo(string.Format("Successfully clicked menu trigger on {0}", ColumnHeaderText));

            }
            catch (Exception e)
            {
                throw new Exception("SelectMenuFromColumnHeader() failed : " + e.ToString());
            }
            finally
            {
                Terminate();
            }
        }

        #region PRIVATE METHODS

        private void Initialize()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkSFTCommon.SearchValues = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf('_'));
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
            grid = GridTypeFactory(mElement.GetAttribute("class"));
        }
        

        private IGrid GridTypeFactory(string className)
        {
            if (className.Contains("x-box-inner", "x-panel"))
                return new DefaultGrid(mElement);
            else if (className.Contains("rich-sdt"))
                return new RichSDTGrid(mElement);
            else
                throw new Exception("Grid type is not supported.");
        }
        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        private void SortAscendingColumn(string columnHeaderName, bool isAscending)
        {
            var columnHeader = grid.GetHeaderElement(columnHeaderName, mElement);
            var header = new DlkBaseControl("Column header", columnHeader);

            if (grid.GetSortedColumnCount(columnHeader, isAscending).Count == 0)
            {
                header.Click();
                header.ScrollIntoView();

                //Re-initialize the element bec the screen refreshes after sorting
                Initialize();
                Thread.Sleep(500);
            }

            columnHeader = grid.GetHeaderElement(columnHeaderName, mElement);//get the updated header column to prevent element from becoming stale
            if (grid.GetSortedColumnCount(columnHeader, isAscending).FirstOrDefault() != null)
                DlkLogger.LogInfo("SortGridColumn() in Ascending order successfully executed.");
            else
                throw new Exception("Unable to sort column by Ascending order.");
        }

        private void SortDescendingColumn(string columnHeaderName, bool isAscending)
        {
            var columnHeader = grid.GetHeaderElement(columnHeaderName, mElement);
            var header = new DlkBaseControl("Column header", columnHeader);

            if (grid.GetSortedColumnCount(columnHeader, isAscending).Count == 0)
            {
                header.Click();
                header.ScrollIntoView();

                Initialize();//need to re-initialize the element bec the screen refreshes after sorting
                columnHeader = grid.GetHeaderElement(columnHeaderName, mElement);//get the updated header column to prevent element from becoming stale
                header = new DlkBaseControl("Column header", columnHeader);

                //The column header needs to be clicked again if it is sorted in ascending order
                if (grid.GetSortedColumnCount(columnHeader, true).Count > 0)
                {
                    header.Click();
                    header.ScrollIntoView();
                    Initialize();//need to re-initialize the element bec the screen refreshes after sorting
                }
                
                Thread.Sleep(500);
            }

            columnHeader = grid.GetHeaderElement(columnHeaderName, mElement);//get the updated header column to prevent element from becoming stale
            if (grid.GetSortedColumnCount(columnHeader, isAscending).FirstOrDefault() != null)
                DlkLogger.LogInfo("SortGridColumn() in Descending order successfully executed.");
            else
                throw new Exception("Unable to sort column by Descending order.");
        }

        private void ExpandPanels()
        {
            if (mElement.GetAttribute("class").Contains("rich-sdt"))//expand grid panel for v1
            {
                DlkSFTCommon.ExpandPanel();
                Initialize();//re-initialize the element after expanding panel to prevent stale element
            }
        }

        #endregion
    }
}
