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
    [ControlType("ExpenseReportsList")]
    public class DlkExpenseReportList : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_SEARCH_VAL = 2;
        
        public DlkExpenseReportList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkExpenseReportList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkExpenseReportList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private IList<IWebElement> listbox = null;
#if NATIVE_MAPPING
        private bool mIsWebView = false;
#endif
        private string STR_INNER_ELM = ".//*[contains(@class,'simplelistitem')]";
        private string STR_ERDESC_INNER_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class,'expRptPrimaryLine')]";
        private string STR_ERAMT_INNER_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class,'expRptPrimaryLine')]//*[contains(@class,'expRptListTxRight')]"; 
        private string STR_ERCURRENCY_INNER_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class,'expRptSecondaryLine')]//*[contains(@class,'expListTxRight')]";
        private string STR_ERDAY_INNER_ELM = ".//*[contains(@class,'listitem')]//div[2]";
        private string STR_ERTYPE_INNER_ELM = ".//*[contains(@class,'listitem')]//div[3]";
        private string STR_ERID_INNER_ELM = ".//*[contains(@class,'listitem')]//div[4]";
        private string STR_ERSTATUS_INNER_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class, 'expenseStatusText')]";
        private string STR_ERT_ROWHEADER_ELM = ".//*[contains(@class,'listitem')]//*[@class='expRptOverallFirst']";
        private string STR_ERT_AMTCURRENCY_ELM = ".//*[contains(@class,'listitem')]//*[@class='expListTxRight']"; 
        private string STR_ERT_TOTALTOME_ELM = ".//*[contains(@class,'listitem')]//*[@class='expRptTotalAmount']";
        private string STR_ERT_DESC_ELM = ".//*[contains(@class,'listitem')]//*[@class='expRptOverall'][1]";
        private string STR_ERT_SUBDESC_ELM = ".//*[contains(@class,'listitem')]//*[@class='expRptOverall'][2]";//elems covered: date, desc
        private string STR_CLAIMEDEXPENSE_HEADER_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class, 'ex-rpt-list')]//span[contains(@class, 'desc-text')]";//elems covered: charge header, dates
        private string STR_CLAIMEDEXPENSE_DESC_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class, 'ex-rpt-list')]//*[contains(@class, 'locationList')]"; 
        private string STR_CLAIMEDEXPENSE_AMT_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class, 'ex-rpt-list')]//*[contains(@class, 'amount')]";
        private string STR_CLAIMEDEXPENSE_ROW_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class, 'ex-rpt-list')]";
        private string STR_CEATTACHMENT_DESC_ELM = ".//*[contains(@class,'listitem')]//*[contains(@class, 'locationListTx')][1]";
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

        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                Initialize();
                GetAvailableListItems(mElement);

                if (!Int32.TryParse(Index, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                IWebElement mSelected = null;
                mSelected = GetElementByIndex(row);

                DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                ctlItem.ScrollIntoView();

                ctlItem.Tap();
                DlkLogger.LogInfo("Successfully executed SelectByIndex()");
                
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
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

        [Keyword("SelectByPartialText", new String[] { "1|text|Partial Text|TRUE" })]
        public void SelectByPartialText(String PartialText)
        {
            try
            {
                int indexOf = 0;
                List<string> listContainer = new List<string>();

                Initialize();

                GetAvailableListItems(mElement);

                listbox.ToList().ForEach(x => listContainer.Add(x.Text));

                for(int i = 0; i < listbox.Count(); i++)
                {
                    if (listContainer[i].ToLower().Contains(PartialText.ToLower()))
                    {
                        indexOf = i + 1;
                        break;
                    }
                }

                IWebElement mSelected = null;
                mSelected = GetElementByIndex(indexOf);

                DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                ctlItem.ScrollIntoViewUsingJavaScript();

                ctlItem.Click();
                DlkLogger.LogInfo("Successfully executed SelectByPartialText()");                                             
            }
            catch (Exception e)
            {
                throw new Exception("SelectByPartialText() failed : " + e.Message, e);
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

        [Keyword("GetExpenseResultID", new String[] { "1|text|Value|SampleValue" })]
        public void GetExpenseResultID(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERID_INNER_ELM));
                DlkLogger.LogInfo("GetExpenseResultID() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetExpenseResultID() failed : " + e.Message, e);
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

        [Keyword("GetExpenseResultDay", new String[] { "1|text|Value|SampleValue" })]
        public void GetExpenseResultDay(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERDAY_INNER_ELM));
                DlkLogger.LogInfo("GetExpenseResultDay() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetExpenseResultDay() failed : " + e.Message, e);
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

        [Keyword("GetExpenseResultType", new String[] { "1|text|Value|SampleValue" })]
        public void GetExpenseResultType(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERTYPE_INNER_ELM));
                DlkLogger.LogInfo("GetExpenseResultType() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetExpenseResultType() failed : " + e.Message, e);
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

        [Keyword("GetExpenseResultDescription", new String[] { "1|text|Value|SampleValue" })]
        public void GetExpenseResultDescription(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERDESC_INNER_ELM));
                DlkLogger.LogInfo("GetExpenseResultDescription() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetExpenseResultDescription() failed : " + e.Message, e);
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

        [Keyword("GetExpenseResultStatus", new String[] { "1|text|Value|SampleValue" })]
        public void GetExpenseResultStatus(string RowIndex, string Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkVariable.SetVariable(Variable, GetInnerText(row, STR_ERSTATUS_INNER_ELM));
                DlkLogger.LogInfo("GetExpenseResultStatus() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetExpenseResultStatus() failed : " + e.Message, e);
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

        [Keyword("GetExpenseResultStatusColor", new String[] { "1|text|Value|SampleValue" })]
        public void GetExpenseResultStatusColor(string RowIndex, string Variable)
        {
            try
            {
                string color;
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                GetAvailableListItems(mElement);

                if (row < 1 || row > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + row + "'");
                }

                color = listbox[row - 1].FindElement(By.XPath(STR_ERSTATUS_INNER_ELM)).GetAttribute("class").Replace("expenseStatusBody ", "");

                DlkVariable.SetVariable(Variable, color);
                DlkLogger.LogInfo("GetExpenseResultStatusColor() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetExpenseResultStatusColor() failed : " + e.Message, e);
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

        [Keyword("GetERTransactionRowTotalToMe", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void GetERTransactionRowTotalToMe(String RowIndex, String Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERT_TOTALTOME_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.FirstOrDefault().Trim();
                }

                DlkVariable.SetVariable(Variable, actualText);
                DlkLogger.LogInfo("GetERTransactionRowTotalToMe() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetERTransactionRowTotalToMe() failed : " + e.Message, e);
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

        [Keyword("GetERTransactionRowTotalAmount", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void GetERTransactionRowTotalAmount(String RowIndex, String Variable)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERT_AMTCURRENCY_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.FirstOrDefault().Trim();
                }

                DlkVariable.SetVariable(Variable, actualText);
                DlkLogger.LogInfo("GetERTransactionRowTotalAmount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetERTransactionRowTotalAmount() failed : " + e.Message, e);
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
        [Keyword("VerifyItemCount")]
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
                GetAvailableListItems(mElement);
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

        [Keyword("GetRowByPartialText")]
        public void GetRowByPartialText(string PartialText, string Variable)
        {
            try
            {
                Initialize();
                int indexOf = 0;
                List<string> listContainer = new List<string>();

                GetAvailableListItems(mElement);

                listbox.ToList().ForEach(x => listContainer.Add(x.Text));

                for (int i = 0; i < listbox.Count(); i++)
                {
                    if (listContainer[i].ToLower().Contains(PartialText.ToLower()))
                    {
                        indexOf = i + 1;
                        break;
                    }
                }

                DlkVariable.SetVariable(Variable, indexOf.ToString());
                DlkLogger.LogInfo("GetRowByPartialText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowByPartialText() failed : " + e.Message, e);
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

        [Keyword("GetRowByMultiplePartialText")]
        public void GetRowByMultiplePartialText(string MultiplePartialText, string Variable)
        {
            try
            {
                Initialize();
                int indexOf = 0;
                List<string> listContainer = new List<string>();
                var partialTexts = MultiplePartialText.Split(CHAR_SEARCH_VAL_DELIMITER);
                if (partialTexts.Length < 1) throw new Exception("Invalid input for MultiplePartialText.");

                GetAvailableListItems(mElement);

                listbox.ToList().ForEach(x => listContainer.Add(x.Text));

                for (int i = 0; i < listbox.Count(); i++)
                {
                    if (partialTexts.All(text => listContainer[i].ToLower().Contains(text.ToLower())))
                    {
                        indexOf = i + 1;
                        break;
                    }
                }
                if (indexOf <= 0) throw new Exception($"Could not find row containing the texts: {MultiplePartialText}");

                DlkVariable.SetVariable(Variable, indexOf.ToString());
                DlkLogger.LogInfo("GetRowByPartialText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowByPartialText() failed : " + e.Message, e);
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

        [Keyword("VerifyExpenseReportStatus", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportStatus(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                DlkAssert.AssertEqual("VerifyExpenseReportStatus()", ExpectedValue, GetInnerText(row, STR_ERSTATUS_INNER_ELM));
                DlkLogger.LogInfo("VerifyExpenseReportStatus() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportStatus() failed : " + e.Message, e);
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

        [Keyword("SetCheckBoxOnRow", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void SetCheckBoxOnRow(String RowIndex, String IsChecked)
        {
            try
            {
                Initialize();

                int retryCount = 0;
                IWebElement mSelected = null;

                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                if (!bool.TryParse(IsChecked, out bool isChecked)) throw new Exception("Invalid input: '" + IsChecked + "'. Keyword expecting TRUE or FALSE");

                GetAvailableListItems(mElement);
                mSelected = GetElementByIndex(row);

                bool currentState = GetCurrentState(mSelected);

                while (++retryCount <= 3 && currentState != isChecked)
                {
                    var checkBoxElem = mSelected.FindElements(By.XPath(".//*[contains(@class,'listitem')]//*[contains(@class, 'checkbox')]")).FirstOrDefault(x => x.Displayed);
                    if (checkBoxElem == null) throw new Exception("Row checkbox not found.");
                    
                    var checkBox = new DlkMobileControl("Row Checkbox", checkBoxElem);
                    checkBox.ScrollIntoViewUsingJavaScript();
                    checkBox.Tap();

                    currentState = GetCurrentState(mSelected);
                    if (currentState == isChecked)
                    {
                        break;
                    }
                    else
                    {
                        DlkLogger.LogInfo("SetCheckBoxOnRow() failed. Retrying...");
                    }
                }
                DlkLogger.LogInfo("Successfully executed SetCheckBoxOnRow()");
            }
            catch (Exception e)
            {
                throw new Exception("SetCheckBoxOnRow() failed : " + e.Message, e);
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

        [Keyword("VerifyCheckBoxOnRow", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyCheckBoxOnRow(String RowIndex, String IsChecked)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;

                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                if (!bool.TryParse(IsChecked, out bool isChecked)) throw new Exception("Invalid input: '" + IsChecked + "'. Keyword expecting TRUE or FALSE");

                GetAvailableListItems(mElement);
                mSelected = GetElementByIndex(row);
                
                bool currentState = GetCurrentState(mSelected);

                DlkAssert.AssertEqual("VerifyCheckBoxOnRow()", isChecked, currentState);
                DlkLogger.LogInfo("Successfully executed VerifyCheckBoxOnRow()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCheckBoxOnRow() failed : " + e.Message, e);
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

        #region Expense Report Tab

        [Keyword("VerifyExpenseReportRowDescription", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportRowDescription(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERDESC_INNER_ELM);

                if (actualText.Contains("\r\n") && actualText.Contains(ExpectedValue))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.FirstOrDefault(txt => txt == ExpectedValue);
                }

                DlkAssert.AssertEqual("VerifyExpenseReportRowDescription()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyExpenseReportRowDescription() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportRowDescription() failed : " + e.Message, e);
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

        [Keyword("VerifyExpenseReportRowDate", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportRowDate(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
               
                DlkAssert.AssertEqual("VerifyExpenseReportRowDate()", ExpectedValue, GetActualText(ExpectedValue, row, STR_ERDAY_INNER_ELM));
                DlkLogger.LogInfo("VerifyExpenseReportRowDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportRowDate() failed : " + e.Message, e);
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

        [Keyword("VerifyExpenseReportRowType", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportRowType(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                DlkAssert.AssertEqual("VerifyExpenseReportRowType()", ExpectedValue, GetActualText(ExpectedValue, row, STR_ERTYPE_INNER_ELM));
                DlkLogger.LogInfo("VerifyExpenseReportRowType() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportRowType() failed : " + e.Message, e);
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

        [Keyword("VerifyExpenseReportRowID", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportRowID(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkAssert.AssertEqual("VerifyExpenseReportRowID()", ExpectedValue, GetActualText(ExpectedValue, row, STR_ERID_INNER_ELM));
                DlkLogger.LogInfo("VerifyExpenseReportRowID() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportRowID() failed : " + e.Message, e);
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

        [Keyword("VerifyExpenseReportRowAmount", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportRowAmount(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkAssert.AssertEqual("VerifyExpenseReportRowAmount()", ExpectedValue, GetActualText(ExpectedValue, row, STR_ERAMT_INNER_ELM));
                DlkLogger.LogInfo("VerifyExpenseReportRowAmount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportRowAmount() failed : " + e.Message, e);
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

        [Keyword("VerifyExpenseReportRowCurrency", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyExpenseReportRowCurrency(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                
                DlkAssert.AssertEqual("VerifyExpenseReportRowCurrency()", ExpectedValue, GetActualText(ExpectedValue, row, STR_ERCURRENCY_INNER_ELM));
                DlkLogger.LogInfo("VerifyExpenseReportRowCurrency() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExpenseReportRowCurrency() failed : " + e.Message, e);
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
        #endregion
        #region Expense Report Transaction
        [Keyword("VerifyERTransactionRowHeader", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyERTransactionRowHeader(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERT_ROWHEADER_ELM);
                
                if(actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.Any(txt => txt == ExpectedValue.Trim()) ? texts.FirstOrDefault(txt => txt == ExpectedValue) : actualText;
                }

                DlkAssert.AssertEqual("VerifyERTransactionRowHeader()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyERTransactionRowHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyERTransactionRowHeader() failed : " + e.Message, e);
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

        [Keyword("VerifyERTransactionRowDescription", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyERTransactionRowDescription(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERT_DESC_ELM);

                if (actualText.Contains("\r\n") && actualText.Contains(ExpectedValue))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.FirstOrDefault(txt => txt == ExpectedValue);
                }
                else//check other subdescriptions on row
                {
                    var subText = GetInnerText(row, STR_ERT_SUBDESC_ELM);
                    if (subText.Contains("\r\n") && subText.Contains(ExpectedValue))
                    {
                        var texts = subText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        actualText = texts.FirstOrDefault(txt => txt == ExpectedValue);
                    }
                }

                DlkAssert.AssertEqual("VerifyERTransactionRowDescription()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyERTransactionRowDescription() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyERTransactionRowDescription() failed : " + e.Message, e);
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

        [Keyword("VerifyERTransactionRowAmount", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyERTransactionRowAmount(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERT_AMTCURRENCY_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.Any(txt => txt == ExpectedValue.Trim()) ? texts.FirstOrDefault(txt => txt == ExpectedValue) : actualText;
                }

                DlkAssert.AssertEqual("VerifyERTransactionRowAmount()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyERTransactionRowAmount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyERTransactionRowAmount() failed : " + e.Message, e);
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

        [Keyword("VerifyERTransactionRowTotalToMe", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyERTransactionRowTotalToMe(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_ERT_TOTALTOME_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.Any(txt => txt == ExpectedValue.Trim()) ? texts.FirstOrDefault(txt => txt == ExpectedValue) : actualText;
                }

                DlkAssert.AssertEqual("VerifyERTransactionRowTotalToMe()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyERTransactionRowTotalToMe() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyERTransactionRowTotalToMe() failed : " + e.Message, e);
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

        [Keyword("VerifyClaimedExpenseRowHeader", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyClaimedExpenseRowHeader(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_CLAIMEDEXPENSE_HEADER_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.Any(txt => txt == ExpectedValue.Trim()) ? texts.FirstOrDefault(txt => txt == ExpectedValue) : actualText;
                }

                DlkAssert.AssertEqual("VerifyClaimedExpenseRowHeader()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyClaimedExpenseRowHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyClaimedExpenseRowHeader() failed : " + e.Message, e);
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

        [Keyword("VerifyClaimedExpenseRowDescription", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyClaimedExpenseRowDescription(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");
                var actualText = GetInnerText(row, STR_CLAIMEDEXPENSE_DESC_ELM);

                if (actualText.Contains("\r\n") && actualText.Contains(ExpectedValue))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.FirstOrDefault(txt => txt == ExpectedValue);
                }
                else//check other subdescriptions on row
                {
                    var subText = GetInnerText(row, STR_CLAIMEDEXPENSE_HEADER_ELM);
                    if (subText.Contains("\r\n") && subText.Contains(ExpectedValue))
                    {
                        var texts = subText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        actualText = texts.FirstOrDefault(txt => txt == ExpectedValue);
                    }
                }

                DlkAssert.AssertEqual("VerifyClaimedExpenseRowDescription()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyClaimedExpenseRowDescription() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyClaimedExpenseRowDescription() failed : " + e.Message, e);
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

        [Keyword("VerifyClaimedExpenseRowAmountCurrency", new String[] {"1|text|Row Index|Row Index",
                                                            "2|text|Expected Value|Value"})]
        public void VerifyClaimedExpenseRowAmountCurrency(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                var actualText = GetInnerText(row, STR_CLAIMEDEXPENSE_AMT_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.Any(txt => txt == ExpectedValue.Trim()) ? texts.FirstOrDefault(txt => txt == ExpectedValue) : actualText;
                }

                DlkAssert.AssertEqual("VerifyClaimedExpenseRowAmountCurrency()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyClaimedExpenseRowAmountCurrency() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyClaimedExpenseRowAmountCurrency() failed : " + e.Message, e);
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

        [Keyword("VerifyClaimedExpenseRowItemCount", new String[] { "1|text|ExpectedCount|Value" })]
        public void VerifyClaimedExpenseRowItemCount(String ExpectedCount)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(ExpectedCount, out int iExpectedCount)) throw new Exception("RowIndex must be a valid positive integer.");

                int iCurrentValue = mElement.FindElements(By.XPath(STR_CLAIMEDEXPENSE_ROW_ELM)).Count;

                DlkAssert.AssertEqual("VerifyClaimedExpenseRowItemCount()", iExpectedCount, iCurrentValue);
                DlkLogger.LogInfo("VerifyClaimedExpenseRowItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyClaimedExpenseRowItemCount() failed : " + e.Message, e);
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

        [Keyword("VerifyAttachmentRowItemName", new String[] { "1|text|ExpectedCount|Value" })]
        public void VerifyAttachmentRowItemName(String RowIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(RowIndex, out int row)) throw new Exception("RowIndex must be a valid positive integer.");

                var actualText = GetInnerText(row, STR_CEATTACHMENT_DESC_ELM);

                if (actualText.Contains("\r\n"))
                {
                    var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    actualText = texts.Any(txt => txt == ExpectedValue.Trim()) ? texts.FirstOrDefault(txt => txt == ExpectedValue) : actualText;
                }

                DlkAssert.AssertEqual("VerifyAttachmentRowItemName()", ExpectedValue, actualText);
                DlkLogger.LogInfo("VerifyAttachmentRowItemName() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAttachmentRowItemName() failed : " + e.Message, e);
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

        [Keyword("VerifyAttachmentItemNameIfExists")]
        public void VerifyAttachmentItemNameIfExists(String ExpectedValue, String TrueOrFalse)
        {
            try
            {
                Initialize();
                List<string> listContainer = new List<string>();

                if (!bool.TryParse(TrueOrFalse, out bool expected)) throw new Exception("Invalid input: '" + TrueOrFalse + "'. Keyword expecting TRUE or FALSE");

                GetAvailableListItems(mElement);
                listbox.ToList().ForEach(x => listContainer.Add(x.Text));
                var exists = listContainer.Contains(ExpectedValue);

                DlkAssert.AssertEqual("VerifyAttachmentItemNameIfExists()", expected, exists);
                DlkLogger.LogInfo("VerifyAttachmentItemNameIfExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAttachmentItemNameIfExists() failed : " + e.Message, e);
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
        #endregion

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
            
            var itemText = listbox[row - 1].FindElements(By.XPath(innerElm)).FirstOrDefault(x => x.Displayed);

            if (itemText == null) throw new Exception("Could not find ExpenseReportList item text.");
            return itemText.Text;
        }

        private static bool GetCurrentState(IWebElement mSelected)
        {
            return mSelected.FindElements(By.XPath(".//*[contains(@class,'listitem')]//*[contains(@class, 'checkbox_checked')]")).Any(state => state.Displayed);
        }

        private IWebElement GetElementByIndex(int row)
        {
            IWebElement mSelected = null;
            for (int iCounter = 0; iCounter < this.iFindElementDefaultSearchMax; iCounter++)
            {
                mSelected = listbox.ElementAt(row - 1);
                if (mSelected != null)
                {
                    break;
                }
            }
            if (mSelected == null) throw new Exception($"Row index '{row}' not found.");
            return mSelected;
        }

        private string GetActualText(string ExpectedValue, int row, string locator)
        {
            var actualText = GetInnerText(row, locator);

            if (actualText.Contains("\r\n") && actualText.Contains(ExpectedValue))
            {
                var texts = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                actualText = texts.FirstOrDefault(txt => txt == ExpectedValue);
            }

            return actualText;
        }

        #endregion

    }
}
