#define NATIVE_MAPPING
using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Linq;

namespace CPTouchLib.DlkControls
{
    [ControlType("Toggle")]
    public class DlkToggle : DlkMobileControl
    {
#if NATIVE_MAPPING
        private const string REL_XPATH_ON = "./*/*/*/*";
        private const string REL_XPATH_OFF = "./*/*/*";
        private const string REL_WEBVIEW_XPATH_ON = ".//*[not(contains(@class, 'hidden'))]/*[@class='onSpan']";
        private const string REL_WEBVIEW_XPATH_OFF = ".//*[@class='offSpan']";
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private bool mIsWebView = false; 
        private const int INT_INDEX_SEARCH_VAL = 2;
#else
        private const string REL_XPATH_ON = ".//*[not(contains(@class, 'hidden'))]/*[@class='onSpan']";
        private const string REL_XPATH_OFF = ".//*[@class='offSpan']";
#endif
        private const string VAL_ON = "on";
        private const string VAL_OFF = "off";
        public DlkToggle(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToggle(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToggle(String ControlName, IWebElement ExistingWebElement)
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

        [Keyword("Set")]
        public void Set(string TrueOrFalse)
        {
            try
            {
                Initialize();
                bool expected;

                if (!bool.TryParse(TrueOrFalse, out expected))
                {
                    throw new Exception("Invalid input: '" + TrueOrFalse + "'. Keyword expecting TRUE or FALSE");
                }

                if (!SetValue(expected))
                {
                    throw new Exception("Unexpected control error: Value cannot be set");
                }

                DlkLogger.LogInfo("Successfully executed Set().");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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

        [Keyword("VerifyValue")]
        public void VerifyValue(string TrueOrFalse)
        {
            try
            {
                Initialize();
                bool expected;

                if (!bool.TryParse(TrueOrFalse, out expected))
                {
                    throw new Exception("Invalid input: '" + TrueOrFalse + "'. Keyword expecting TRUE or FALSE");
                }
                var actual = GetValue();

                if (actual == null)
                {
                    throw new Exception("Unexpected control error: Actual value is null");
                }

                DlkAssert.AssertEqual("VerifyValue", expected, (bool)actual);
                DlkLogger.LogInfo("Successfully executed VerifyValue().");
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

        private new bool? GetValue()
        {
            bool? ret = null;

#if NATIVE_MAPPING
            var onNode = mElement.FindElements(By.XPath(mIsWebView ? REL_WEBVIEW_XPATH_ON : REL_XPATH_ON)).FirstOrDefault();

            if (DlkEnvironment.mBrowser.ToLower() == "ios" && !mIsWebView)
            {
                ret = mElement.Text.Trim().ToLower() == VAL_ON;
            }
            else if (onNode != null && !string.IsNullOrEmpty(onNode.Text))
            {
                ret = onNode.Text.Trim().ToLower() == VAL_ON;
            }
            else
            {
                var offNode = mElement.FindElements(By.XPath(mIsWebView ? REL_WEBVIEW_XPATH_OFF : REL_XPATH_OFF)).FirstOrDefault();
                if (offNode != null && !string.IsNullOrEmpty(offNode.Text))
                {
                    ret = offNode.Text.Trim().ToLower() != VAL_OFF;
                }
            }
#else
            var onNode = mElement.FindElements(By.XPath(REL_XPATH_ON)).FirstOrDefault();
            if (onNode != null && !string.IsNullOrEmpty(onNode.Text))
            {
                ret = onNode.Text.Trim().ToLower() == VAL_ON;
            }
            else
            {
                var offNode = mElement.FindElements(By.XPath(REL_XPATH_OFF)).FirstOrDefault();
                if (offNode != null && !string.IsNullOrEmpty(offNode.Text))
                {
                    ret = offNode.Text.Trim().ToLower() != VAL_OFF;
                }
            }
#endif
            return ret;
        }


        private bool SetValue(bool value)
        {
            var onNode = mElement.FindElements(By.XPath(mIsWebView ? REL_WEBVIEW_XPATH_ON : REL_XPATH_ON)).FirstOrDefault();
#if NATIVE_MAPPING

            if (DlkEnvironment.mBrowser.ToLower() == "ios" && !mIsWebView)
            {
                if ((value && mElement.Text.ToLower() == VAL_OFF) || (!value && mElement.Text.ToLower() == VAL_OFF))
                {
                    DlkLogger.LogInfo($"Setting toggle value to '{ value.ToString().ToUpper() }'...");
                    new DlkButton("ToggleArea", mElement).Tap();
                    return true;
                }
            }
            else if (onNode != null && !string.IsNullOrEmpty(onNode.Text))
            {
                if (onNode.Text.Trim().ToLower() == VAL_ON)
                {
                    if (value)
                    {
                        DlkLogger.LogInfo("No action was taken. Value already 'TRUE'");
                        return true;
                    }

                    DlkLogger.LogInfo("Setting toggle value to 'FALSE'...");
                    new DlkButton("ToggleArea", onNode).Tap();
                    return true;
                }
            }
            else
            {
                var offNode = mElement.FindElements(By.XPath(mIsWebView ? REL_WEBVIEW_XPATH_OFF : REL_XPATH_OFF)).FirstOrDefault();
                if (offNode != null && !string.IsNullOrEmpty(offNode.Text))
                {
                    if (offNode.Text.Trim().ToLower() == VAL_OFF)
                    {
                        if (!value)
                        {
                            DlkLogger.LogInfo("No action was taken. Value already 'FALSE'");
                            return true;
                        }

                        DlkLogger.LogInfo("Setting toggle value to 'TRUE'...");
                        new DlkButton("ToggleArea", offNode).Tap();
                        return true;
                    }
                }
            }
#else
            if (onNode != null && !string.IsNullOrEmpty(onNode.Text))
            {
                if (value && (onNode.Text.ToLower() == VAL_ON))
                {
                    DlkLogger.LogInfo("No action was taken. Value already 'TRUE'");
                    return true;
                }

                DlkLogger.LogInfo("Setting toggle value to 'FALSE'...");
                new DlkButton("ToggleArea", onNode).Tap();
                return true;
            }
            else
            {
                var offNode = mElement.FindElements(By.XPath(REL_XPATH_OFF)).FirstOrDefault();
                if (offNode != null && !string.IsNullOrEmpty(offNode.Text))
                {
                    if (!value && offNode.Text.Trim().ToLower() == VAL_OFF)
                    {
                        DlkLogger.LogInfo("No action was taken. Value already 'FALSE'");
                        return true;
                    }

                    DlkLogger.LogInfo("Setting toggle value to 'TRUE'...");
                    new DlkButton("ToggleArea", offNode).Tap();
                    return true;
                }
            }
#endif
            return false;
        }
    }
}
