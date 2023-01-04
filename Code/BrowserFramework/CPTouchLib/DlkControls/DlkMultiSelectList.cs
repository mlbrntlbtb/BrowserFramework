#define NATIVE_MAPPING

using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace CPTouchLib.DlkControls
{
    [ControlType("MultiSelectList")]
    public class DlkMultiSelectList : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;

        public DlkMultiSelectList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMultiSelectList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMultiSelectList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private IList<IWebElement> listbox = null;
        private string STR_INNER_ELM = "//div[contains(@class,'simplelistitem')]";
        private string STR_INNER_ELM_CHECKBOX = "//div[contains(@class,'simplelistitem')]//div[contains(@class,'checkReview')]";
        private bool mIsWebView = false;

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
            if (findElement)
            {
                FindElement();
            }
#else
            FindElement();
#endif
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {

            String[] inputArray = Index.Split('~');
            try
            {
                int row;
                Initialize();

                GetAvailableListItems(mElement, true);

                for (int i = 0; i < inputArray.Length; i++)
                {
                    if (!Int32.TryParse(inputArray[i], out row)) throw new Exception("RowIndex must be a valid positive integer.");
#if NATIVE_MAPPING
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

                    DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                    ctlItem.ScrollIntoView();
                    
                    mSelected.Click();
                    DlkLogger.LogInfo("Successfully executed SelectByIndex()");
#else
                    DlkBaseControl ctlItem = new DlkBaseControl("Item", listbox.ElementAt(row - 1));
                    ctlItem.ScrollIntoViewUsingJavaScript();

                    ctlItem.Tap();
                    DlkLogger.LogInfo("Successfully executed SelectByIndex()");
#endif
                }
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

        [Keyword("GetItemCount", new String[] { "1|text|Value|SampleValue" })]
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

        [Keyword("VerifyItemCount", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyItemCount(string ExpectedCount)
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
                
                DlkAssert.AssertEqual("VerifyRowCount", expected, listbox.Count);
                DlkLogger.LogInfo("VerifyRowCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

#region PRIVATE METHODS
        public void GetAvailableListItems(IWebElement mElms, bool checkInnerElm = false)
        {
            DlkBaseControl mList = new DlkBaseControl("List", mElms);
            int listSize = -1;

            listSize = mList.GetNativeViewCenterCoordinates().Y;

            if (mElement.FindElements(By.XPath(STR_INNER_ELM)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElement.FindElements(checkInnerElm ? By.XPath(STR_INNER_ELM_CHECKBOX) : By.XPath(STR_INNER_ELM)).ToList();
                }
                else
                {
                    listbox = mElement.FindElements(checkInnerElm ? By.XPath(STR_INNER_ELM_CHECKBOX) : By.XPath(STR_INNER_ELM)).Where(x => x.Displayed).ToList();
                }
            }
            else
            {
                throw new Exception("MultiList type not yet supported.");
            }
        }
#endregion

    }
}
