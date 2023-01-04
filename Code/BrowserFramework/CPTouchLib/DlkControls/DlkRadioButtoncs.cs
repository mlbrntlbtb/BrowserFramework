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
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkMobileControl
    {
#if NATIVE_MAPPING
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_SEARCH_VAL = 2;
        private bool mIsWebView = false;
#endif
        public DlkRadioButton(String ControlName, String SearchType, String SearchValue)
              : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

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

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                if (Convert.ToBoolean(Value)) // process only when Value is TRUE
                {
                    int retryCount = 0;
                    Initialize();
                    Boolean bCurrentValue = GetState();
                    while (++retryCount <= 3 && bCurrentValue != true)
                    {
                        //ScrollIntoViewUsingJavaScript();
#if NATIVE_MAPPING
                        if (mIsWebView)
                            Click();
                        else
                            Tap();
#else
                        Click();
#endif
                        if (!DlkAlert.DoesAlertExist())  // If dialog is present, exit retry
                        {
                            bCurrentValue = GetState();
                            VerifyValue(true);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                DlkLogger.LogInfo("Successfully executed Select()");
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

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String IsSelected)
        {
            try
            {
                VerifyValue(Convert.ToBoolean(IsSelected));
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
#if NATIVE_MAPPING
                Initialize(false);
#endif
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
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

#region PRIVATE METHODS

        private Boolean GetState()
        {
            Boolean bCurrentVal = false;
            Initialize();
#if NATIVE_MAPPING
            bCurrentVal = mIsWebView ? this.GetAttributeValue("class").Contains("checked") : Convert.ToBoolean(this.GetAttributeValue("checked"));
#else
            bCurrentVal = this.GetAttributeValue("class").Contains("checked");
#endif
            return bCurrentVal;
        }

        private void VerifyValue(Boolean IsSelected)
        {
            Boolean bCurrentValue = GetState();
            DlkAssert.AssertEqual("VerifyValue() : " + IsSelected.ToString() + " : " + mControlName, IsSelected, bCurrentValue);
        }

#endregion
    }
}
