using BPMLib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        private String mstrHeaderClass = "headingRowCell";
        private String mstrBodyXPath = "//tbody[@id='ListingURE_detailView_mainTableBody']/tr";
        private List<IWebElement> mlstHeaders;
        private List<IWebElement> mlstBody;
        int colIndex;

        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private void Initialize()
        {
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
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

        [Keyword("DoubleClickRowWithColumnValue", new String[] {"1|text|ColumnHeader|Title", 
                                                            "2|text|ColumnValue|Reporting"})]
        public void DoubleClickRowWithColumnValue(String ColumnHeader, String ColumnValue)
        {
            try
            {
                Initialize();
                colIndex = GetColumnIndexByHeader(ColumnHeader);
                ClickForwardIfNotFoundInTable(ColumnValue);
                int rowIndex = GetRowIndexByColumnValue(ColumnValue);

                DlkBaseControl rowControl = new DlkBaseControl("Row", GetRow(rowIndex, colIndex));
                
                RowDoubleClick(rowControl);

                DlkBPMCommon.WaitForSpinner();
                DlkLogger.LogInfo("Successfully executed DoubleClickRowWithColumnValue() : " + ColumnValue);
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickRowWithColumnValue() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("SortAscendingByHeader", new String[] { "1|text|Header|Title" })]
        public void SortAscendingByHeader(String Header)
        {
            try
            {
                Initialize();
                RefreshHeaders();
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    if (Header.Trim().ToLower() == mlstHeaders[i].Text.Trim().ToLower())
                    {
                        DlkBaseControl ctrHeader = new DlkBaseControl("Header", mlstHeaders[i]);
                        IList<IWebElement> element = ctrHeader.mElement.FindElements(By.XPath(".//a"));
                        if (element.Any())
                        {
                            if (element[0].GetAttribute("title").Contains("Sort by Title, in descending order"))
                            {
                                element[0].Click();
                                DlkLogger.LogInfo("Successfully executed SortAscendingByHeader()");
                                break;
                            }
                            else
                            {
                                DlkLogger.LogInfo("List is already in ascending order.");
                                break;
                            }
                        }
                        else
                        {
                            throw new Exception("Header could not be found.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("SortAscendingByHeader() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("SortDescendingByHeader", new String[] { "1|text|Header|Title" })]
        public void SortDescendingByHeader(String Header)
        {
            try
            {
                Initialize();
                RefreshHeaders();
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    if (Header.Trim().ToLower() == mlstHeaders[i].Text.Trim().ToLower())
                    {
                        DlkBaseControl ctrHeader = new DlkBaseControl("Header", mlstHeaders[i]);
                        IList<IWebElement> element = ctrHeader.mElement.FindElements(By.XPath(".//a"));
                        if (element.Any())
                        {
                            if (element[0].GetAttribute("title").Contains("Sort by Title, in ascending order"))
                            {
                                element[0].Click();
                                DlkLogger.LogInfo("Successfully executed SortDescendingByHeader()");
                                break;
                            }
                            else
                            {
                                DlkLogger.LogInfo("List is already in descending order.");
                                break;
                            }
                        }
                        else
                        {
                            throw new Exception("Header could not be found.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("SortDescendingByHeader() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyTotalCellValue", new String[] { "1|text|TableIndex|1",
                                                    "2|text|ColumnHeader|No Due",
                                                    "3|text|ExpectedValue|test"})]
        public void VerifyTotalCellValue(String TableIndex, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();
                string hXPath = string.Format("//div[{0}]//table[2]/tbody/tr[@r='1']", TableIndex);
                var header = mElement.FindElements(By.XPath(hXPath));
                string[] headers = header[0].Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < headers.Count(); i++)
                {
                    if (ColumnHeader == headers[i].ToString())
                    {
                        colIndex = i;
                        break;
                    }
                }

                string tXPath = string.Format("//div[{0}]//table[2]/tbody", TableIndex);
                var row = mElement.FindElement(By.XPath(tXPath));
                string totalRow = row.Text.Substring(row.Text.IndexOf("Total:"));
                string[] totalRows = totalRow.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if(ExpectedValue == totalRows[colIndex])
                {
                    DlkLogger.LogInfo("VerifyTotalCellValue(): Passed");
                }
                else
                {
                    DlkLogger.LogInfo("VerifyTotalCellValue(): Expected value did not match on the actual value");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTotalCellValue() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyCellValue", new String[] { "1|text|TableIndex|1",
                                                    "2|text|ColumnHeader|No Due",
                                                    "3|text|RowIndex|1" +
                                                    "4|text|ExpectedValue|test"})]
        public void VerifyCellValue(String TableIndex, String ColumnHeader, String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();

                int colIndex = 0;
                string hXPath = string.Format("//div[{0}]//table[2]/tbody/tr[@r='1']", TableIndex);
                var header = mElement.FindElements(By.XPath(hXPath));
                string[] headers = header[0].Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < headers.Count(); i++)
                {
                    if(ColumnHeader == headers[i].ToString())
                    {
                        colIndex = i;
                        break;
                    }
                }

                string vXPath = string.Format("//div[{0}]//table[2]/tbody/tr[@r='{1}']/td[{2}]//span", TableIndex, Convert.ToInt32(RowIndex) + 1, colIndex);
                IWebElement actualVal = mElement.FindElement(By.XPath(vXPath));
                if(actualVal.Text == ExpectedValue)
                {
                    DlkLogger.LogInfo("VerifyCellValue(): Passed");
                }
                else
                {
                    DlkLogger.LogInfo("VerifyCellValue(): Expected value did not match on the actual value");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyColumnHeaderText", new String[] { "1|text|ColumnIndex|1",
                                                    "2|RowIndex|1",
                                                    "3|ExpectedValue|test"})]
        public void VerifyColumnHeaderText(String TableIndex, String ColumnIndex, String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                string xpath = string.Format("//div[{0}]//table[2]/tbody/tr[@r='{1}']/th[{2}]//span", TableIndex, RowIndex, ColumnIndex);
                IWebElement actualHeader = mElement.FindElement(By.XPath(xpath));
                if(actualHeader.Text == ExpectedValue)
                {
                    DlkLogger.LogInfo("VerifyColumnHeaderText(): Passed");
                }
                else
                    {
                    DlkLogger.LogInfo("VerifyColumnHeaderText(): Expected value did not match on the actual value");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderText() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyCurrencyValue", new String[] { "1|text|TableIndex|1",
                                                       "2|text|ExpectedValue|DKK"})]
        public void VerifyCurrencyValue(String TableIndex, String ExpectedValue)
        {
            try
            {
                Initialize();

                string xpath = string.Format("//div[{0}]//table[1]/tbody/tr/td/div/span", TableIndex);
                IWebElement currency = mElement.FindElement(By.XPath(xpath));

                string actualResult = DlkString.ReplaceCarriageReturn(currency.Text.Trim(), "\n");
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedValue, "\n");

                DlkAssert.AssertEqual("VerifyCurrencyValue() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCurrencyValue() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        /// <summary>
        /// Double click selected table row
        /// </summary>
        /// <param name="rowControl">selected row control</param>
        private void RowDoubleClick(DlkBaseControl rowControl)
        {
            if (rowControl.mElement.Location.Y > mElement.Size.Height)
            {
                ScrollDownTable();
                RowDoubleClick(rowControl);
            }

            rowControl.DoubleClick();
        }

        /// <summary>
        /// Scroll down table vertical scroll bar
        /// </summary>
        private void ScrollDownTable()
        {
            mElement.Click();
            var mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.SendKeys(OpenQA.Selenium.Keys.PageDown).Build().Perform();
            DlkLogger.LogInfo("RowDoubleClick(): Table scroll down");
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// Get column index by header value
        /// </summary>
        /// <param name="columnHeader">Column header</param>
        /// <returns>Returns the column index on the table</returns>
        private int GetColumnIndexByHeader(String columnHeader)
        {
            int index = -1;
            Boolean bCont = true;

            RefreshHeaders();
            while(bCont && index == -1)
            {
                for(int i = 0; i < mlstHeaders.Count; i++)
                {
                    if(columnHeader.Trim() == mlstHeaders[i].Text.Trim())
                    {
                        index = 1;
                        break;
                    }
                }

                if(index > -1)
                {
                    bCont = false;
                }
            }
            return index;
        }

        /// <summary>
        /// Get row index by column value
        /// </summary>
        /// <param name="ColumnValue">Column value</param>
        /// <returns>Returns the row index on the table</returns>
        private int GetRowIndexByColumnValue(String ColumnValue)
        {
            int index = -1;
            Boolean bCont = true;

            RefreshTableBody();
            while(bCont && index == -1)
            {
                for(int i=0; i < mlstBody.Count; i++)
                {
                    String tBodyText = mlstBody[i].Text.Replace("\r\n", "|");
                    String[] arrBodyText = tBodyText.Split('|');
                    if (ColumnValue.Trim() == arrBodyText[colIndex - 1])
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                {
                    bCont = false;
                }
            }

            return index;
        }

        /// <summary>
        /// Get web element of a table row
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="colIndex">Column index</param>
        /// <returns>Returns web element type of an specified row</returns>
        private IWebElement GetRow(int rowIndex, int colIndex)
        {
            IWebElement row = null;
            RefreshTableBody();
            if (mlstBody.Count <= 0)
            {
                return row;
            }

            row = mlstBody[rowIndex];
            return row;
        }

        /// <summary>
        /// Refresh table header
        /// </summary>
        private void RefreshHeaders()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            FindElement();

            mlstHeaders = new List<IWebElement>();

            IList<IWebElement> headers = mElement.FindElements(By.ClassName(mstrHeaderClass));
            foreach (IWebElement columnHeader in headers)
            {
                if (columnHeader.GetCssValue("display") != "none")
                {
                    mlstHeaders.Add(columnHeader);
                }
            }
        }

        /// <summary>
        /// Refresh table body
        /// </summary>
        private void RefreshTableBody()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            FindElement();

            mlstBody = new List<IWebElement>();
            IList<IWebElement> body = mElement.FindElements(By.XPath(mstrBodyXPath));
            foreach(IWebElement rowBody in body)
            {
                if(rowBody.GetCssValue("display") != "none")
                {
                    mlstBody.Add(rowBody);
                }
            }

        }

        /// <summary>
        /// Click forward button on a table is value not in current list
        /// </summary>
        /// <param name="target">Value to search</param>
        private void ClickForwardIfNotFoundInTable(string target)
        {
            try
            {
                while (true)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    Initialize();
                    IWebElement lastRowTitle = mElement.FindElements(By.XPath("//*[@id='ListingURE_detailView_mainTableBody']/descendant::tr[last()]/td[2]")).SingleOrDefault();

                    if (lastRowTitle != null)
                    {
                        if (string.Compare(target, lastRowTitle.Text) > 0)
                        {
                            // check current page against total page
                            DlkBaseControl currentPageInfo = new DlkBaseControl("currentPageInfo", "id", "ListingURE_pageNumberInput");
                            string[] pages = currentPageInfo.GetAttributeValue("title").Replace(" of ", "/").Split('/');
                            if (pages.First() == pages.Last())
                            {
                                break;
                            }
                            else
                            {
                                DlkBaseControl forward = new DlkBaseControl("forward", "xpath", "//*[@id='IconImg_ListingURE_goForwardButton']/..");
                                forward.Click();
                                System.Threading.Thread.Sleep(1000);
                                continue;
                            }
                        }
                    }
                    else
                        throw new ArgumentException("ClickForwardIfNotFoundInTable() failed: No row(s) found in table.");

                    break;
                }
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
            catch (Exception e)
            {
                throw new Exception($"ClickForwardIfNotFoundInTable() failed: {e.Message}", e);
            }
        }
    }
}
