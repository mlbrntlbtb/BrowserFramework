#define NATIVE_MAPPING

using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using System.Linq;

namespace CPTouchLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;
        private bool mIsWebView = false;

        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        
        public void Initialize()
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
            FindElement();
        }

        [Keyword("Set")]
        public void Set(string TrueOrFalse)
        {
            try
            {
                Boolean ExpectedValue = Convert.ToBoolean(TrueOrFalse);
                Initialize();

                if (ExpectedValue != GetCheckedState())
                {
                    base.Tap();
                }

                //verify 
                DlkAssert.AssertEqual("VerifyValue()", ExpectedValue, GetCheckedState());
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(string TrueOrFalse)
        {
            try
            { 
                Initialize();
                Boolean bIsChecked = Convert.ToBoolean(TrueOrFalse);
                Boolean bCurrentValue = GetCheckedState();
                DlkAssert.AssertEqual("VerifyValue()", bIsChecked, bCurrentValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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

#region PRIVATE METHODS
        public Boolean GetCheckedState()
        {
#if NATIVE_MAPPING
            var elemClass = string.Empty;
            var idNative = GetAttributeValue(DlkMobileControl.ATTRIB_RESOURCE_ID_NATIVE);
            Boolean bCurrentVal = false;

            if (mIsWebView)
            {
                /* find in WebView */
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
                var elemInWebView = DlkEnvironment.AutoDriver.FindElements(By.Id(idNative)).FirstOrDefault();
                var parentElemInWebView = elemInWebView.FindElement(By.XPath("./ancestor::*[contains(@id,'deltekcheckboxfield')]"));
                if (parentElemInWebView == null)
                {
                    throw new Exception("Cannot determine state. Cannot locate element in webview");
                }
                elemClass = parentElemInWebView.GetAttribute("class");

                /* set back to NATIVE */
                DlkEnvironment.SetContext("NATIVE");

                bCurrentVal = elemClass.Contains("checked");
            }
            else
            {
                bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
            }
            return bCurrentVal;
#else
            var elemClass = string.Empty;

            var parentElemInWebView = mElement.FindElement(By.XPath("./ancestor::*[contains(@id,'deltekcheckboxfield')]"));

            elemClass = parentElemInWebView.GetAttribute("class");
            
            Boolean bCurrentVal = elemClass.Contains("checked");
            return bCurrentVal;
#endif

        }
#endregion
    }
}
