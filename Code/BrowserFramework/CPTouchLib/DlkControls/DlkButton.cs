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
    [ControlType("Button")]
    public class DlkButton : DlkMobileControl
    {
        private const string ATTRIB_CLASS_DISABLED_PARTIAL = "disabled";
#if NATIVE_MAPPING
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
#endif
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;

        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

#if NATIVE_MAPPING
        private bool mIsWebView = false;
#endif

        public void Initialize(bool findElement=true)
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

        [Keyword("Tap")]
        public new void Tap()
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                if(mIsWebView)
                    ScrollIntoViewUsingJavaScript();
#endif
                base.Tap();
                DlkLogger.LogInfo("Successfully executed Tap().");
            }
            catch (StaleElementReferenceException)
            {
                Tap();
            }
            catch (Exception e)
            {
                throw new Exception("Tap() failed : " + e.Message, e);
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

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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
    }
}
