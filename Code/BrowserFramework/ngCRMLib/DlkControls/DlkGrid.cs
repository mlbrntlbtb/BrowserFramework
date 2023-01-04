using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace ngCRMLib.DlkControls
{
    [ControlType("Grid")]
    public class DlkGrid : DlkBaseControl
    {
        private Boolean IsInit = false;
        
        public DlkGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
        }
        
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
        
        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String RowNumber, String ColumnNumber, String Value)
        {
            try
            {
                Initialize();
                int targetColumnNumber = Convert.ToInt32(ColumnNumber);
                int targetRowNumber = Convert.ToInt32(RowNumber);
                IWebElement targetCell = null;
                String ActValue = "";
                if (mElement.GetAttribute("id").ToLower().Contains("report-grid"))
                {
                    targetCell = mElement.FindElement(By.XPath(".//tr[" + targetRowNumber + "]/td[" + targetColumnNumber + "]"));
                }
                else
                {
                    if (!IsDisplayed(targetColumnNumber))
                    {
                        targetColumnNumber = (targetColumnNumber + GetHiddenColumnCount());
                    }
                    targetCell = mElement.FindElement(By.XPath(".//tr[" + targetRowNumber + "]/td[" + targetColumnNumber + "]/input"));
                }

                try
                {
                    ActValue = new DlkBaseControl("Target", targetCell).GetValue();
                    if (ActValue.Contains("\r\n"))
                    {
                        ActValue = ActValue.Replace("\r\n", "<br>");
                    }
                }
                catch
                {
                    ActValue = targetCell.GetAttribute("title");
                }

                DlkAssert.AssertEqual("VerifyCellValue()", Value, ActValue);
                DlkLogger.LogInfo("VerifyCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellLink(String RowNumber, String ColumnNumber, String LinkText)
        {
            try
            {
                Initialize();
                int targetColumnNumber = Convert.ToInt32(ColumnNumber);
                int targetRowNumber = Convert.ToInt32(RowNumber);
                IWebElement targetCell = null;

                if (mElement.GetAttribute("id").ToLower().Contains("report-grid"))
                {
                    targetCell = mElement.FindElement(By.XPath(".//tr[" + targetRowNumber + "]/td[" + targetColumnNumber + "]"));
                }
                else
                {
                    if (!IsDisplayed(targetColumnNumber))
                    {
                        targetColumnNumber = (targetColumnNumber + GetHiddenColumnCount());
                    }
                    targetCell = mElement.FindElement(By.XPath(".//tr[" + targetRowNumber + "]/td[" + targetColumnNumber + "]"));
                }

                DlkBaseControl ctlCellLink;
                DlkBaseControl ctlCell = new DlkBaseControl("Cell", targetCell);
                ctlCell.ScrollIntoViewUsingJavaScript();
                ctlCell.Click();
                try
                {
                    IWebElement mCellLink = targetCell.FindElement(By.LinkText(LinkText));
                    ctlCellLink = new DlkBaseControl("Cell Link", mCellLink);
                    ctlCellLink.Click();
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("ClickCellLink() failed. Cell button '" + LinkText + "' not found.");
                }
                catch
                {
                    // Ignore. For chrome, sometimes 1 click "ctlCell.Click()" is enough to perform desired click
                }
                DlkLogger.LogInfo("ClickCellLink() successfully executed.");                
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellLink() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithColumnValueContains", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowWithColumnValueContains(string ColumnNumber, string Value, string VariableName)
        {
            Initialize();
            int iRow = GetRowWithColumnValueContains(ColumnNumber, Value);
            if (iRow > -1)
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                DlkLogger.LogInfo("GetRowWithColumnValueContains() passed.");
            }
            else
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                //throw new Exception("GetRowWithColumnValue() failed. Unable to find row.");
            }
        }

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowWithColumnValue(string ColumnNumber, string Value, string VariableName)
        {
            Initialize();
            int iRow = GetRowWithColumnValue(ColumnNumber, Value);
            if (iRow > -1)
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                DlkLogger.LogInfo("GetRowWithColumnValue() passed.");
            }
            else
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                //throw new Exception("GetRowWithColumnValue() failed. Unable to find row.");
            }
        }

        public int GetRowWithColumnValueContains(string ColumnNumber, string sValue)
        {
            int i = 1;
            int targetColumnNumber = Convert.ToInt32(ColumnNumber);
            if (sValue == "")
            {
                sValue = null;
            }

            string cellValue = GetCellValue(i, targetColumnNumber);
            while (cellValue != null)
            {
                if (cellValue.Contains(sValue))
                {
                    return i;
                }

                i++;
                cellValue = GetCellValue(i, targetColumnNumber);
            }

            return -1;
        }

        public int GetRowWithColumnValue(string ColumnNumber, string sValue)
        {
            int i = 1;
            int targetColumnNumber = Convert.ToInt32(ColumnNumber);
            if (sValue == "")
            {
                sValue = null;
            }

            string cellValue = GetCellValue(i, targetColumnNumber);
            while (cellValue != null)
            {
                if (cellValue == sValue)
                {
                    return i;
                }

                i++;
                cellValue = GetCellValue(i, targetColumnNumber);
            }

            return -1;
        }

        public string GetCellValue(int iRow, int targetColumnNumber)
        {
            Initialize();
            IWebElement mCellElm = null;
            if (mElement.GetAttribute("id").ToLower().Contains("report-grid"))
            {
                mCellElm = mElement.FindElement(By.XPath(".//tr[" + iRow + "]/td[" + targetColumnNumber + "]"));
            }
            else
            {
                if (!IsDisplayed(targetColumnNumber))
                {
                    targetColumnNumber = (targetColumnNumber + GetHiddenColumnCount());
                }
                mCellElm = mElement.FindElement(By.XPath(".//tr[" + iRow + "]/td[" + targetColumnNumber + "]"));
            }
            if (mCellElm == null)
                return null;

            DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
            return mCell.GetValue();
        }

        private int GetColumnCount()
        {
            return mElement.FindElements(By.XPath("./tr[1]/td")).Count;
        }

        private int GetHiddenColumnCount()
        {
            int hCol = 0;
            IReadOnlyCollection<IWebElement> mColumns = mElement.FindElements(By.XPath(".//tr[1]/td"));
            foreach(IWebElement column in mColumns ){
                if (!column.Displayed)
                {
                    hCol++;
                }
            }
            return hCol;
        }

        private bool IsDisplayed(int col)
        {
           return mElement.FindElement(By.XPath(".//tr[1]/td[" + col.ToString() + "]")).Displayed;
        }
    }
}
