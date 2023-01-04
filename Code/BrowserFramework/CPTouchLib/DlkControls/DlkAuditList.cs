#define NATIVE_MAPPING

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
    [ControlType("AuditList")]
    public class DlkAuditList : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_REVISION = 0;
        private const int INT_INDEX_SIGNEDBY = 1;
        private const int INT_INDEX_APPROVEDBY = 2;
        private const int INT_SPACES_COUNT = 4;

        public DlkAuditList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkAuditList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkAuditList(String ControlName, IWebElement ExistingWebElement)
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
        
        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {
            String[] inputArray = Index.Split('~');
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(Index, out row)) throw new Exception("RowIndex must be a valid positive integer.");
#if NATIVE_MAPPING
                GetAvailableListItems(mElement, true);
#else
                GetAvailableListItems(mElement, false);
#endif

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }
                
                DlkMobileControl ctlItem = new DlkMobileControl("Item", listbox.ElementAt(row - 1));
                ctlItem.ScollIntoViewUsingWebView();

                ctlItem.Tap();
                DlkLogger.LogInfo("Successfully executed SelectByIndex()");

            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowCount(string Variable)
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                GetAvailableListItems(mElement, true);
#else
                GetAvailableListItems(mElement, false);
#endif
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
#if NATIVE_MAPPING
                GetAvailableListItems(mElement, true);
#else
                GetAvailableListItems(mElement, false);
#endif

                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }

                DlkAssert.AssertEqual("VerifyRowCount", expected, listbox.Count);
                DlkLogger.LogInfo("VerifyRowCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRevision", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyRevision(string RowIndex, string ValueToVerify)
        {
            try
            {
                int row;
                string result = string.Empty;

                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                result = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetRowInnerText(row, INT_INDEX_REVISION)).Split(' ')[1];

                DlkAssert.AssertEqual("VerifyRevision()", ValueToVerify, result);
                DlkLogger.LogInfo("VerifyRevision() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySignedBy", new String[] { "1|text|Value|SampleValue" })]
        public void VerifySignedBy(string RowIndex, string ValueToVerify)
        {
            try
            {
                int row;
                string result = string.Empty;

                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                result = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetRowInnerText(row, INT_INDEX_SIGNEDBY));

                DlkAssert.AssertEqual("VerifySignedBy()", ValueToVerify, result.Substring(result.IndexOf("By:") + INT_SPACES_COUNT));
                DlkLogger.LogInfo("VerifySignedBy() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyApprovedBy", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyApprovedBy(string RowIndex, string ValueToVerify)
        {
            try
            {
                int row;
                string result = string.Empty;

                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                result = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetRowInnerText(row, INT_INDEX_APPROVEDBY));

                DlkAssert.AssertEqual("VerifyApprovedBy()", ValueToVerify, result.Substring(result.IndexOf("By:") + INT_SPACES_COUNT));
                DlkLogger.LogInfo("VerifyApprovedBy() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRevision", new String[] { "1|text|Value|SampleValue" })]
        public void GetRevision(string RowIndex, string Variable)
        {
            try
            {
                int row;
                string result = string.Empty;

                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                result = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetRowInnerText(row, INT_INDEX_REVISION)).Split(' ')[1];

                DlkVariable.SetVariable(Variable, result);
                DlkLogger.LogInfo("GetRevision() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRevision() failed : " + e.Message, e);
            }
        }

        [Keyword("GetSignedBy", new String[] { "1|text|Value|SampleValue" })]
        public void GetSignedBy(string RowIndex, string Variable)
        {
            try
            {
                int row;
                string result = string.Empty;

                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                result = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetRowInnerText(row, INT_INDEX_SIGNEDBY));

                DlkVariable.SetVariable(Variable, result.Substring(result.IndexOf("By:") + INT_SPACES_COUNT));
                DlkLogger.LogInfo("GetSignedBy() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetSignedBy() failed : " + e.Message, e);
            }
        }

        [Keyword("GetApprovedBy", new String[] { "1|text|Value|SampleValue" })]
        public void GetApprovedBy(string RowIndex, string Variable)
        {
            try
            {
                int row;
                string result = string.Empty;

                if (!Int32.TryParse(RowIndex, out row)) throw new Exception("RowIndex must be a valid positive integer.");

                result = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetRowInnerText(row, INT_INDEX_APPROVEDBY));
                
                DlkVariable.SetVariable(Variable, result.Substring(result.IndexOf("By:") + INT_SPACES_COUNT));
                DlkLogger.LogInfo("GetApprovedBy() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("GetApprovedBy() failed : " + e.Message, e);
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
        /// Gets all the inner text in row
        /// </summary>
        /// <param name="row">Row from list</param>
        /// <param name="innerRow">Row of the text from the selected list element</param>
        /// <returns></returns>
        private string GetRowInnerText(int row, int innerRow)
        {
            string ret = string.Empty;
            Initialize();

#if NATIVE_MAPPING
            GetAvailableListItems(mElement, true);
#else
            GetAvailableListItems(mElement, false);
#endif

            if (row < 1 || row > listbox.Count)
            {
                throw new Exception("Index out of item range: '" + row + "'");
            }

            var arrTokens = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(listbox[row - 1].FindElement(By.XPath(STR_INNER_TEXT_ELM)).GetAttribute("text")).Split('\r');
            ret = arrTokens[innerRow];

            return ret;
        }
#endregion
    }
}
