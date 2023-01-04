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
    [ControlType("Label")]
    public class DlkLabel : DlkMobileControl
    {
        #region Constructors
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

#if NATIVE_MAPPING
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_SEARCH_VAL = 2;
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
                FindElement();
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify = "")
        {
            try
            {
                Initialize();
                String ActValue = GetActualValue();

                if (ActValue.Contains("\r\n"))
                {
                    ActValue = ActValue.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify.ToLower(), ActValue.ToLower());
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

        [Keyword("VerifyExactText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyExactText(String TextToVerify = "")
        {
            try
            {
                Initialize();
                String ActValue = GetActualValue();

                if (ActValue.Contains("\r\n"))
                {
                    ActValue = ActValue.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify, ActValue);
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

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String TextToVerify = "")
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, GetActualValue().Contains(TextToVerify.ToLower()));
                DlkLogger.LogInfo("VerifyTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
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

        #region Private Method
        private String GetActualValue()
        {
            var isRequired = mElement.FindElements(By.XPath("./parent::*[contains(@class, 'x-required')]")).Count == 1 || mElement.FindElements(By.XPath("./parent::*/parent::*[contains(@class, 'x-required')]")).Count == 1;
            String ActValue = mElement.Text + (isRequired ? " *" : string.Empty);
            return ActValue;
        }
        #endregion
    }
}
