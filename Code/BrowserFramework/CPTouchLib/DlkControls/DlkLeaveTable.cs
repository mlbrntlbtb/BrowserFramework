using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace CPTouchLib.DlkControls
{
    [ControlType("LeaveTable")]
    public class DlkLeaveTable : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;

        public DlkLeaveTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLeaveTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLeaveTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private IList<IWebElement> listbox = null;
        private string STR_INNER_ELM = "//div[contains(@class,'simplelistitem')]";
        private string STR_INNER_ELM_NATIVE = "//*[contains(@resource-id,'simplelistitem')]";
        private string STR_INNER_TEXT_ELM = "//*[string-length(@text)!=0]";
        private bool mIsWebView = false;

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Select")]
        public void Select(string ItemToSelect)
        {
            try
            {
                FindElement();
                DlkLogger.LogInfo("Successfully executed Select().");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
        
        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {
            String[] inputArray = Index.Split('~');
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(Index, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                GetAvailableListItems(mElement, true);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                IWebElement mSelected = null;
                for (int iCounter = 0; iCounter < this.iFindElementDefaultSearchMax; iCounter++)
                {
                    try
                    {
                        mSelected = listbox.ElementAt(row - 1);
                        if (mSelected != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }

                DlkMobileControl ctlItem = new DlkMobileControl("Item", mSelected);
                ctlItem.ScollIntoViewUsingWebView();

                ctlItem.Tap();
                DlkLogger.LogInfo("Successfully executed SelectByIndex()");

            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetColumnValue", new String[] { "1|text|Value|SampleValue" })]
        public void GetColumnValue(string RowIndex, string ColumnIndex, string Variable)
        {
            try
            {
                int row;
                int column;
                Initialize();
                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");
                if (!Int32.TryParse(ColumnIndex, out column)) throw new Exception("ColumnIndex must be a valid positive integer.");

                GetAvailableListItems(mElement, true);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                DlkVariable.SetVariable(Variable, listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM + "[" + GetSwitchedColumnValue(column) + "]")).GetAttribute("text"));
                DlkLogger.LogInfo("GetColumnValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLeaveType", new String[] { "1|text|Value|SampleValue" })]
        public void GetLeaveType(string RowIndex, string Variable)
        {
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                GetAvailableListItems(mElement, true);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                DlkVariable.SetVariable(Variable, listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM + "[2]")).GetAttribute("text"));
                DlkLogger.LogInfo("GetLeaveType() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLeaveType() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLeaveBalance", new String[] { "1|text|Value|SampleValue" })]
        public void GetLeaveBalance(string RowIndex, string Variable)
        {
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                GetAvailableListItems(mElement, true);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                DlkVariable.SetVariable(Variable, listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM + "[1]")).GetAttribute("text"));
                DlkLogger.LogInfo("GetLeaveBalance() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLeaveBalance() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLeaveTypeValue", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyLeaveTypeValue(string RowIndex, string ExpectedValue)
        {
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                GetAvailableListItems(mElement, true);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM + "[2]")).GetAttribute("text"));
                DlkLogger.LogInfo("GetLeaveType() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLeaveType() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLeaveBalanceValue", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyLeaveBalanceValue(string RowIndex, string ExpectedValue)
        {
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                GetAvailableListItems(mElement, true);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM + "[1]")).GetAttribute("text"));
                DlkLogger.LogInfo("GetLeaveBalance() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLeaveBalance() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowCount(string Variable)
        {
            try
            {
                Initialize();
                GetAvailableListItems(mElement, true);
                DlkVariable.SetVariable(Variable, listbox.Count.ToString());
                DlkLogger.LogInfo("GetRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyRowCount(string ExpectedCount)
        {
            try
            {
                int expected;

                Initialize();
                GetAvailableListItems(mElement, true);

                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }

                Initialize();
                DlkAssert.AssertEqual("VerifyRowCount", expected, listbox.Count);
                DlkLogger.LogInfo("VerifyRowCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }
        
        [Keyword("VerifyColumnValue", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyColumnValue(string RowIndex, string ColumnIndex, string ValueToVerify)
        {
            try
            {
                int row;
                int column;
                
                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");
                if (!Int32.TryParse(ColumnIndex, out column)) throw new Exception("ColumnIndex must be a valid positive integer.");

                Initialize();
                
                GetAvailableListItems(mElement, true);
               
                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + RowIndex + "'");
                }

                DlkAssert.AssertEqual("VerifyColumnValue", ValueToVerify, listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM + "[" + GetSwitchedColumnValue(column) + "]")).GetAttribute("text"));
                DlkLogger.LogInfo("VerifyColumnValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnValue() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #region PRIVATE METHODS
        private void GetAvailableListItems(IWebElement mElms, bool isNative = false)
        {
            DlkBaseControl mList = new DlkBaseControl("List", mElms);
            int listSize = -1;

            listSize = mList.GetNativeViewCenterCoordinates().Y;

            if (mElms.FindElements(By.XPath(isNative ? STR_INNER_ELM_NATIVE : STR_INNER_ELM)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElms.FindElements(By.XPath(isNative ? STR_INNER_ELM_NATIVE : STR_INNER_ELM)).ToList();
                }
                else
                {
                    listbox = mElms.FindElements(By.XPath(isNative ? STR_INNER_ELM_NATIVE : STR_INNER_ELM)).Where(x => x.Displayed).ToList();
                }
            }
            else
            {
                throw new Exception("List type not yet supported.");
            }
        }
        /// <summary>
        /// Switching column value because on the mapping of Leave Screen, LeaveType (supposed to be Column 1) is located at the 2nd column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private int GetSwitchedColumnValue(int column)
        {
            return column == 1 ? 2 : 1;
        }
        #endregion
    }
}
