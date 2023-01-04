#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;

namespace CPTouchLib.DlkControls
{
    [ControlType("LookupList")]
    public class DlkLookupList : DlkMobileControl
    {
        public DlkLookupList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLookupList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLookupList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

#if NATIVE_MAPPING
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~'; 
        private bool mIsWebView = false;
        private const int INT_INDEX_SEARCH_VAL = 2;
#endif

        public void Initialize(bool findElem = true)
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
            if(findElem)
                FindElement();
        }

        [Keyword("Select")]
        public void Select(string ItemToSelect)
        {
            try
            {
                Initialize();
                var itms = GetItems();
                foreach (var itm in itms)
                {
                    if (ItemToSelect.Equals(GetItemText(itm)))
                    {
                        itm.Tap();
                        DlkLogger.LogInfo("Successfully executed Select().");
                        return;
                    }
                }
                throw new Exception("Item: '" + ItemToSelect + "' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
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

        [Keyword("SelectPartialMatch")]
        public void SelectPartialMatch(string ItemToSelect, string OneBasedIndex)
        {
            try
            {
                Initialize();
                var itms = GetItems();
                foreach (var itm in itms)
                {
                    if (ItemToSelect.Equals(GetItemText(itm)))
                    {
                        itm.Tap();
                        DlkLogger.LogInfo("Successfully executed Select().");
                        return;
                    }
                }
                throw new Exception("Item: '" + ItemToSelect + "' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
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

        [Keyword("SelectByIndex")]
        public void SelectByIndex(string OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                var itms = GetItems();
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > itms.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                itms[index - 1].Tap();
                DlkLogger.LogInfo("Successfully executed SelectByIndex().");
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


        [Keyword("VerifyItemCount")]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                var itms = GetItems();
                DlkAssert.AssertEqual("VerifyItemCount", expected, itms.Count);
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

        [Keyword("VerifyItems")]
        public void VerifyItems(string TildeBoundItems)
        {
            try
            {
                Initialize();
                string actual = string.Empty;
                try
                {
                    var itms = GetItems();
                    actual = string.Join("~", itms.Select(x => GetItemText(x)));
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected control error encountered: " + e.Message);
                }
                DlkAssert.AssertEqual("VerifyItems", TildeBoundItems, actual);
                DlkLogger.LogInfo("Successfully executed VerifyItems().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItems() failed : " + e.Message, e);
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

        private List<DlkMobileControl> GetItems()
        {
#if NATIVE_MAPPING
            string listXpath = mIsWebView ? ".//*[contains(@id, 'simplelistitem')]" : ".//*[contains(@resource-id, 'simplelistitem')]";

            List<DlkMobileControl> ret = new List<DlkMobileControl>();
            var items = mElement.FindElements(By.XPath(listXpath));
            if (items.Any())
            {
                items.ToList().ForEach(x => ret.Add(new DlkMobileControl("item", x)));
            }
            return ret;
#else
            List<DlkMobileControl> ret = new List<DlkMobileControl>();
            var items = mElement.FindElements(By.XPath("//*[contains(@id, 'simplelistitem')]"));
            if (items.Any())
            {
                items.ToList().ForEach(x => ret.Add(new DlkMobileControl("item", x)));
            }
            return ret;
#endif
        }

        private string GetItemText(DlkMobileControl control)
        {
#if NATIVE_MAPPING
            //string STR_XPATH_HEADERS = "//*[string-length(@resource-id)!=0][string-length(@text)!=0]";
            string xpath = mIsWebView ? ".//*[contains(@class, 'chrgDetailLn') or contains(@class,'primaryLine')] " : ".//*[string-length(@text)!=0]";
            var txtNode = control.mElement.FindElements(By.XPath(xpath)).FirstOrDefault();
            return txtNode != null ? txtNode.Text : string.Empty;
#else
            var txtIDNode = control.mElement.FindElements(By.XPath("//*[contains(@class,'primaryLine')]")).FirstOrDefault();
            return txtIDNode != null ? txtIDNode.Text : string.Empty;
#endif
        }
    }
}
