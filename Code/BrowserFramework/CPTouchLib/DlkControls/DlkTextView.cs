#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("TextView")]
    public class DlkTextView : DlkMobileControl
    {
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const string STR_TYPE_INCLUDE = "include";
        private const string STR_TYPE_EXTRACT = "extract";
        private const string STR_TYPE_WEBVIEW = "webview";
        private const int INT_INDEX_TYPE = 0;
        private const int INT_INDEX_NUMERIC = 1;
        private const int INT_INDEX_SEARCH_VAL = 2;

        public DlkTextView(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextView(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public enum TextViewType
        {
            DEFAULT,
            INCLUDE,
            EXTRACT,
            WEBVIEW
        }

        private TextViewType mType = TextViewType.DEFAULT;
        private int mNumeric = -1;
#if NATIVE_MAPPING
        public void Initialize(bool findElement=true)
#else
        public void Initialize()
#endif
        {
#if NATIVE_MAPPING
            var sValue = mSearchValues.FirstOrDefault();
            mType = TextViewType.DEFAULT;
            if (sValue != null && sValue.Contains(CHAR_SEARCH_VAL_DELIMITER.ToString()))
            {
                var arrSearch = sValue.Split(CHAR_SEARCH_VAL_DELIMITER);
                mSearchValues[0] = arrSearch[INT_INDEX_SEARCH_VAL];
                mNumeric = int.Parse(arrSearch[INT_INDEX_NUMERIC]);
                switch (arrSearch[INT_INDEX_TYPE])
                {
                    case STR_TYPE_INCLUDE:
                        mType = TextViewType.INCLUDE;
                        break;
                    case STR_TYPE_EXTRACT:
                        mType = TextViewType.EXTRACT;
                        break;
                    case STR_TYPE_WEBVIEW:
                        mType = TextViewType.WEBVIEW;
                        DlkEnvironment.mLockContext = false;
                        DlkEnvironment.SetContext("WEBVIEW");
                        break;
                    default:
                        mType = TextViewType.DEFAULT;
                        break;
                }
            }
            if (findElement)
#endif
            {
                FindElement();
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetText());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                if (mType == TextViewType.WEBVIEW)
                {
                    DlkEnvironment.SetContext("NATIVE", true);
                }
            }
#endif
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
#if NATIVE_MAPPING
                Initialize(false);
#endif
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
                if (mType == TextViewType.WEBVIEW)
                {
                    DlkEnvironment.SetContext("NATIVE", true);
                }
            }
#endif
        }

        private string GetText()
        {
            string ret = string.Empty;
#if NATIVE_MAPPING
            switch (mType)
            {
                case TextViewType.INCLUDE:
                    /* include all n child nodes text, n equals to mNumeric */
                    for (int i=1; i <= mNumeric; i++)
                    {
                        var child = mElement.FindElements(By.XPath("(./*/*)[" + i + "]")).FirstOrDefault();
                        if (child != null)
                        {
                            ret += child.Text;
                        }
                    }
                    break;
                case TextViewType.EXTRACT:
                    /* extract only nth token of all text with space as delimeter, n equals to mNumeric */
                    var arrTokens = CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(GetValue()).Split(' ');
                    ret = arrTokens[mNumeric - 1];
                    break;
                case TextViewType.DEFAULT:
                case TextViewType.WEBVIEW:
                default:
                    ret = mElement.Text;
                    break;
            }
#else
            ret = mElement.Text;
#endif
            return ret;
        }
    }
}
