#define NATIVE_MAPPING

using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace CPTouchLib.DlkControls
{
    [ControlType("UDTLookupList")]
    public class DlkUDTLookupList : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;
        private const int INT_INDEX_ID = 1;
        private const int INT_INDEX_NAME = 3;
        private const string STR_INNER_ELM = ".//div[contains(@class,'simplelistitem')]";
        private const string STR_INNER_ELM_NAME = ".//*[contains(@class,'ColumnName')]";
        private const string STR_INNER_ELM_ID = ".//*[contains(@class,'ColumnId')]";
        private const string STR_INNER_ELM_NATIVE = "//*[contains(@resource-id,'simplelistitem')]";
        private const string STR_INNER_TEXT_ELM = "//*[string-length(@text)!=0]";

        public DlkUDTLookupList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkUDTLookupList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkUDTLookupList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private IList<IWebElement> listbox = null;
#if NATIVE_MAPPING
        private bool mIsWebView = false;
#endif

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

        [Keyword("SelectByID")]
        public void SelectByID(string ID)
        {
            try
            {
                List<string> listItemsID = new List<string>();

                Initialize();
#if NATIVE_MAPPING
                listItemsID = mIsWebView ? GetListContent(STR_INNER_ELM_ID) : GetListContent(INT_INDEX_ID.ToString());
#else
                listItemsID = GetListContent(STR_INNER_ELM_ID);
#endif

                for (int i = 0; i < listbox.Count; i++)
                {
        
                    if (listItemsID[i] == ID)
                    {
                        DlkMobileControl ctlItem = new DlkMobileControl("Item", listbox.ElementAt(i));
                        ctlItem.Tap();
                    }
                }
               
                DlkLogger.LogInfo("Successfully executed SelectByID()");

            }
            catch (Exception e)
            {
                throw new Exception("SelectByID() failed : " + e.Message, e);
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

        [Keyword("SelectByName")]
        public void SelectByName(string Name)
        {
            try
            {
                List<string> listItemsID = new List<string>();

                Initialize();
#if NATIVE_MAPPING
                listItemsID = mIsWebView ? GetListContent(STR_INNER_ELM_NAME) : GetListContent(INT_INDEX_NAME.ToString());
#else
                listItemsID = GetListContent(STR_INNER_ELM_NAME);
#endif
                for (int i = 0; i < listbox.Count; i++)
                {
                    if (listItemsID[i].Equals(Name))
                    {
                        DlkMobileControl ctlItem = new DlkMobileControl("Item", listbox.ElementAt(i));
                        ctlItem.Tap();
                        break;
                    }
                }

                DlkLogger.LogInfo("Successfully executed SelectByID()");

            }
            catch (Exception e)
            {
                throw new Exception("SelectByID() failed : " + e.Message, e);
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
            String[] inputArray = OneBasedIndex.Split('~');
            try
            {
                int row;
                int index;

                Initialize();

                if (!Int32.TryParse(OneBasedIndex, out row))
                {
                    throw new Exception("RowIndex must be a valid positive integer.");
                }

                GetAvailableListItems(mElement);

                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }

                if (index < 1 || index > listbox.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
    
                DlkMobileControl ctlItem = new DlkMobileControl("Item", listbox.ElementAt(row - 1));
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

        [Keyword("VerifyItemsByID")]
        public void VerifyItemsByID(string TildeBoundIDs)
        {
            try
            {
                FindElement();
                DlkLogger.LogInfo("Successfully executed VerifyItemsByID().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemsByID() failed : " + e.Message, e);
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

        [Keyword("GetRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowCount(string Variable)
        {
            try
            {
                Initialize();
                GetAvailableListItems(mElement);
                DlkVariable.SetVariable(Variable, listbox.Count.ToString());
                DlkLogger.LogInfo("GetRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
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

        [Keyword("VerifyRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyRowCount(string ExpectedCount)
        {
            try
            {
                int expected;

                Initialize();

                GetAvailableListItems(mElement);

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
        private void GetAvailableListItems(IWebElement mElms)
        {
            DlkBaseControl mList = new DlkBaseControl("List", mElms);
            int listSize = -1;

            listSize = mList.GetNativeViewCenterCoordinates().Y;
#if NATIVE_MAPPING
            string locator = mIsWebView ? STR_INNER_ELM : STR_INNER_ELM_NATIVE;
            if (mElms.FindElements(By.XPath(locator)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElms.FindElements(By.XPath(locator)).ToList();
                }
                else
                {
                    listbox = mElms.FindElements(By.XPath(locator)).Where(x => x.Displayed).ToList();
                }
            }
#else
            if (mElms.FindElements(By.XPath(STR_INNER_ELM)).Count > 0)
            {
                if (listSize > DlkEnvironment.mDeviceHeight)
                {
                    listbox = mElms.FindElements(By.XPath(STR_INNER_ELM)).ToList();
                }
                else
                {
                    listbox = mElms.FindElements(By.XPath(STR_INNER_ELM)).Where(x => x.Displayed).ToList();
                }
            }
#endif
            else
            {
                throw new Exception("List type not yet supported.");
            }
        }
#if NATIVE_MAPPING
       public List<string> GetListContent(string locator)
        {
            List<string> listItemsID = new List<string>();

            GetAvailableListItems(mElement);

            if(mIsWebView)
                listbox.ToList().ForEach(x => listItemsID.Add(x.FindElement(By.XPath(locator)).Text));
            else
                listbox.ToList().ForEach(x => listItemsID.Add(x.FindElement(By.XPath(STR_INNER_TEXT_ELM + "[" + locator + "]")).Text));

            return listItemsID;
        }
#else
        public List<string> GetListContent(string innerElm)
        {
            List<string> listItemsID = new List<string>();

            GetAvailableListItems(mElement);

            listbox.ToList().ForEach(x => listItemsID.Add(x.FindElement(By.XPath(innerElm)).Text));

            return listItemsID;
        }
      
#endif
        #endregion
    }

}

