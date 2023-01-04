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
    [ControlType("List")]
    public class DlkList : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;
        
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private IList<IWebElement> listbox = null;
        private string STR_INNER_ELM = "//div[contains(@class,'simplelistitem')]";
        private string STR_INNER_ELM_NATIVE = "//*[contains(@resource-id,'simplelistitem')]";
        private bool mIsWebView = false;

        public void Initialize(bool findElement = true)
        {
            if (mSearchValues != null && mSearchValues.First().Contains(STR_WEBVIEW_INDICATOR))
            {
                mIsWebView = true;
                var sValue = mSearchValues.FirstOrDefault();
                var arrSearch = sValue.Split(CHAR_SEARCH_VAL_DELIMITER);
                mSearchValues[0] = arrSearch[INT_INDEX_SEARCH_VAL];
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
            }
            if (findElement)
            {
                FindElement();
            }
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

        [Keyword("SelectPartialMatch")]
        public void SelectPartialMatch(string ItemToSelect, string OneBasedIndex)
        {
            try
            {
                FindElement();
                DlkLogger.LogInfo("Successfully executed SelectPartialMatch().");
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartialMatch() failed : " + e.Message, e);
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

        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {
            String[] inputArray = Index.Split('~');
            try
            {
                int row;
                Initialize();
                if (!Int32.TryParse(Index, out row)) throw new Exception("RowIndex must be a valid positive integer.");
            
                GetAvailableListItems(mElement, mIsWebView ? false : true);

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

                if (mIsWebView)
                {
                    ctlItem.ScrollIntoView();

                    ctlItem.Click();
                }
                else
                {
                    ctlItem.ScollIntoViewUsingWebView();

                    ctlItem.Tap();
                }
            
                DlkLogger.LogInfo("Successfully executed SelectByIndex()");
                
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
            finally
            {
                if (mIsWebView)
                {
                    DlkEnvironment.SetContext("NATIVE");
                }
            }
        }

        [Keyword("VerifyItemCount")]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                FindElement();
                DlkLogger.LogInfo("Successfully executed VerifyItemCount().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItems")]
        public void VerifyItems(string TildeBoundItems)
        {
            try
            {
                FindElement();
                DlkLogger.LogInfo("Successfully executed VerifyItems().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItems() failed : " + e.Message, e);
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
        #endregion
    }
}
