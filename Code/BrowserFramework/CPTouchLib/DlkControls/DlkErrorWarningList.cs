#define NATIVE_MAPPING

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTouchLib.DlkControls
{
    [ControlType("ErrorWarningList")]
    public class DlkErrorWarningList : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;

        public DlkErrorWarningList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkErrorWarningList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkErrorWarningList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private IList<IWebElement> listbox = null;
#if NATIVE_MAPPING
        private bool mIsWebView = false;
#endif
        private string STR_INNER_ELM = ".//*[contains(@class,'simplelistitem')]";
        private string STR_ERROR_ICON_ELM = ".//*[contains(@class,'listitem')]//*[@class='error']";
        private string STR_ERROR_HDR_ELM = ".//*[contains(@class,'listitem')]//*[@class='warnheading']";
        private string STR_ERROR_MSG_ELM = ".//*[contains(@class,'listitem')]//*[@class='errorText']";
        private string STR_WARNING_ICON_ELM = ".//*[contains(@class,'listitem')]//*[@class='warning']";

        public void Initialize(bool findElement = true)
        {
#if NATIVE_MAPPING
            if (mSearchValues != null && mSearchValues.First().Contains(STR_WEBVIEW_INDICATOR))
            {
                mIsWebView = true;
                var sValue = mSearchValues.FirstOrDefault();
                var arrSearch = sValue.Split(CHAR_SEARCH_VAL_DELIMITER);
                mSearchValues[0] = arrSearch[INT_INDEX_SEARCH_VAL];
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
            }
#endif
            if (findElement)
            {
                FindElement();
            }
        }

        [Keyword("VerifyMessageHeader", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyMessageHeader(string RowIndex, string ExpectedMessageHeader)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                string messageHeader = GetInnerText(row, STR_ERROR_HDR_ELM);

                DlkAssert.AssertEqual("VerifyMessageText()", ExpectedMessageHeader, messageHeader);
                DlkLogger.LogInfo("VerifyMessageHeader() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMessageHeader() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyMessageTypeIcon", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyMessageTypeIcon(string RowIndex, string ExpectedMessageTypeIcon)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                GetAvailableListItems(mElement);

                string messageTypeIcon = string.Empty;
                IWebElement mSelected = null;
                for (int iCounter = 0; iCounter < this.iFindElementDefaultSearchMax; iCounter++)
                {
                    mSelected = listbox.ElementAt(row);
                    if (mSelected != null)
                    {
                        messageTypeIcon = mSelected.FindElements(By.XPath(STR_ERROR_ICON_ELM)).Any(icon => icon.Displayed) ? "Error" :
                            (mSelected.FindElements(By.XPath(STR_WARNING_ICON_ELM)).Any(icon => icon.Displayed) ? "Warning" : string.Empty);
                    }
                }

                DlkAssert.AssertEqual("VerifyMessageTypeIcon()", ExpectedMessageTypeIcon.ToLower(), messageTypeIcon.ToLower());
                DlkLogger.LogInfo("VerifyMessageTypeIcon() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMessageTypeIcon() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyMessageText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyMessageText(string RowIndex, string ExpectedMessage)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                string message = GetInnerText(row, STR_ERROR_MSG_ELM);

                DlkAssert.AssertEqual("VerifyMessageText()", ExpectedMessage, message);
                DlkLogger.LogInfo("VerifyMessageText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyVerifyMessageTextMessageTypeIcon() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }
        [Keyword("GetType", new String[] { "1|text|Value|SampleValue" })]
        public void GetType(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                string messageType = GetInnerText(row, STR_ERROR_HDR_ELM).Replace("(s)", string.Empty).Trim();
                DlkVariable.SetVariable(Variable, messageType);
                DlkLogger.LogInfo("GetType() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetType() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("GetErrorWarningMessage", new String[] { "1|text|Value|SampleValue" })]
        public void GetErrorWarningMessage(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERROR_MSG_ELM));
                DlkLogger.LogInfo("GetErrorWarningMessage() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetErrorWarningMessage() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("GetErrorWarningHeader", new String[] { "1|text|Value|SampleValue" })]
        public void GetErrorWarningHeader(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERROR_HDR_ELM));
                DlkLogger.LogInfo("GetErrorWarningHeader() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetErrorWarningHeader() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                Initialize();

                if (!Int32.TryParse(ExpectedCount, out int iExpectedCount)) throw new Exception("ExpectedCount must be a valid positive integer.");

                int iCurrentValue = mElement.FindElements(By.XPath(STR_INNER_ELM)).Count;

                DlkAssert.AssertEqual("VerifyItemCount()", iExpectedCount, iCurrentValue);
                DlkLogger.LogInfo("Successfully executed VerifyItemCount().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize(false);
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("GetItemCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetItemCount(string Variable)
        {
            try
            {
                Initialize();
                int iCurrentValue = mElement.FindElements(By.XPath(STR_INNER_ELM)).Count;

                DlkVariable.SetVariable(Variable, listbox.Count.ToString());
                DlkLogger.LogInfo("GetItemCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetItemCount() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                bool expected;
                if (!bool.TryParse(TrueOrFalse, out expected))
                {
                    throw new Exception("Invalid input: '" + TrueOrFalse + "'. Keyword expecting TRUE or FALSE");
                }
                Initialize();
                var elemClass = string.Empty;
#if NATIVE_MAPPING
                if (!mIsWebView)
                {
                    var idNative = GetAttributeValue(DlkMobileControl.ATTRIB_RESOURCE_ID_NATIVE);
                    /* find in WebView */
                    DlkEnvironment.mLockContext = false;
                    DlkEnvironment.SetContext("WEBVIEW");
                    var elemInWebView = DlkEnvironment.AutoDriver.FindElements(By.Id(idNative)).FirstOrDefault();
                    if (elemInWebView == null)
                    {
                        throw new Exception("Cannot determine state. Cannot locate element in webview");
                    }
                    elemClass = elemInWebView.GetAttribute("class");
                }
                else
                {
                    elemClass = mElement.GetAttribute("class");
                }
#else
                elemClass = mElement.GetAttribute("class");
#endif
                DlkAssert.AssertEqual("VerifyReadOnly()", expected, elemClass.Contains(ATTRIB_CLASS_DISABLED_PARTIAL));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        [Keyword("VerifyIfBlank")]
        public void VerifyIfBlank(string TrueOrFalse)
        {
            try
            {
                Initialize();

                int iCurrentValue = mElement.FindElements(By.XPath(STR_INNER_ELM)).Count;

                DlkAssert.AssertEqual("VerifyIfBlank()", Convert.ToBoolean(TrueOrFalse), iCurrentValue == 0);
                DlkLogger.LogInfo("Successfully executed VerifyIfBlank().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfBlank() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
#endif
        }

        #region PRIVATE METHODS
        public void GetAvailableListItems(IWebElement mElms)
        {
            DlkBaseControl mList = new DlkBaseControl("List", mElms);
            int listSize = -1;

            listSize = mList.GetNativeViewCenterCoordinates().Y;

            if (mElement.FindElements(By.XPath(STR_INNER_ELM)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElement.FindElements(By.XPath(STR_INNER_ELM)).ToList();
                }
                else
                {
                    listbox = mElement.FindElements(By.XPath(STR_INNER_ELM)).Where(x => x.Displayed).ToList();
                }
            }
            else
            {
                throw new Exception("List type not yet supported.");
            }
        }
        private string GetInnerText(int row, string innerElm)
        {
            GetAvailableListItems(mElement);

            if (row < 1 || row > listbox.Count)
            {
                throw new Exception("Index out of item range: '" + row + "'");
            }

            var itemText = listbox[row - 1].FindElements(By.XPath(innerElm)).FirstOrDefault(x => x.Displayed);

            if (itemText == null) throw new Exception("Could not find Error/Warning item text.");
            return itemText.Text;
        }
        #endregion

    }
}
