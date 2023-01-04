using SFTLib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTLib.DlkControls
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
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
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
                rowControl.DoubleClick();
                DlkSFTCommon.WaitForSpinner();
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

                if (index > -1)
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
            while (true)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkBaseControl lastRowTitle = new DlkBaseControl("lastRowTitle", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//*[@id='ListingURE_detailView_mainTableBody']/descendant::tr[last()]/td[2]");
                lastRowTitle.FindElement();
                if (string.Compare(target, lastRowTitle.mElement.Text) > 0)
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
                break;
            }
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
        }
    }
}
