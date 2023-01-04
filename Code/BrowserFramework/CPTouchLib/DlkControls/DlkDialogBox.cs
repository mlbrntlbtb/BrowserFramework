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
    [ControlType("DialogBox")]
    public class DlkDialogBox : DlkMobileControl
    {
#if NATIVE_MAPPING
        private const string STR_WEBVIEW_INDICATOR = "webview";
        private const char CHAR_SEARCH_VAL_DELIMITER = '~';
        private const int INT_INDEX_SEARCH_VAL = 2;

        private const string STR_XPATH_BUTTONS = "//*[contains(@resource-id,'ext-button')]";
        private const string STR_XPATH_TITLE = "//*[contains(@resource-id,'ext-paneltitle')]/*";
        private const string STR_XPATH_MSG = "//*[contains(@resource-id,'ext-component')]/*)[2]";

        private const string STR_WEBVIEW_XPATH_BUTTONS = "//*[contains(@id,'ext-button')]";
        private const string STR_WEBVIEW_XPATH_TITLE = "//*[@class='x-panel-title-text']";
        private const string STR_WEBVIEW_XPATH_MSG = "//*[contains(@class,'x-msgbox')]//*[@class='x-innerhtml']";
#else
        private const string STR_XPATH_BUTTONS = "//*[contains(@id,'ext-button')]";
        private const string STR_XPATH_TITLE = "//*[@class='x-panel-title-text']";
        private const string STR_XPATH_MSG = "//*[contains(@class,'x-msgbox')]//*[@class='x-innerhtml']";
#endif

        public DlkDialogBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDialogBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDialogBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

#if NATIVE_MAPPING
        private bool mIsWebView = false;
#endif
        private List<DlkMobileControl> mButtons = new List<DlkMobileControl>();

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

        private void FindButtons()
        {
            mElement.FindElements(By.XPath(mSearchValues.First() + (mIsWebView ? STR_WEBVIEW_XPATH_BUTTONS : STR_XPATH_BUTTONS))).ToList()
                .ForEach(x => mButtons.Add(new DlkMobileControl("btn", x)));
        }

        [Keyword("TapButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void TapButton(String ButtonCaption)
        {
            try
            {
                Initialize();
                if (!TapButtonCommon(ButtonCaption))
                {
                    throw new Exception("Button with caption '" + ButtonCaption + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed TapButton().");
            }
            catch (Exception e)
            {
                throw new Exception("TapButton() failed : " + e.Message, e);
            }
        }

        [Keyword("TapButtonIfExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void TapButtonIfExists(String ButtonCaption)
        {
            try
            {
                if (Exists())
                {
                    if (!TapButtonCommon(ButtonCaption))
                    {
                        DlkLogger.LogInfo("Button with caption '" + ButtonCaption + "' not found. Proceeding...");
                    }
                }
                else
                {
                    DlkLogger.LogInfo("Dialog Box not found. Proceeding...");
                }
                DlkLogger.LogInfo("Successfully executed TapButtonIfExists().");
            }
            catch (Exception e)
            {
                throw new Exception("TapButtonIfExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyMessage(String ExpectedMessage)
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                var msg = mElement.FindElements(By.XPath("(" + mSearchValues.First() + STR_XPATH_MSG)).FirstOrDefault();
#else
                var msg = mElement.FindElements(By.XPath(mSearchValues.First() + STR_XPATH_MSG)).FirstOrDefault();
#endif
                if (msg == null)
                {
                    throw new Exception("Cannot locate Message cell");
                }
                DlkAssert.AssertEqual("VerifyMessage() : " + mControlName, ExpectedMessage, msg.Text);
                DlkLogger.LogInfo("VerifyMessage() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTitle(String ExpectedTitle)
        {
            try
            {
                Initialize();
                var title = mElement.FindElements(By.XPath(mSearchValues.First() + STR_XPATH_TITLE)).FirstOrDefault();
                if (title == null)
                {
                    throw new Exception("Cannot locate title cell");
                }
                DlkAssert.AssertEqual("VerifyTitle() : " + mControlName, ExpectedTitle, title.Text);
                DlkLogger.LogInfo("VerifyTitle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
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
        }

        private bool TapButtonCommon(string ButtonCaption)
        {
            FindButtons();
            var target = mButtons.FirstOrDefault(x => GetButtonText(x) == ButtonCaption);
            if (target == null)
            {
                return false;
            }
            try
            {
                target.Tap();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private string GetButtonText(DlkMobileControl target)
        {
#if NATIVE_MAPPING
            if (target == null || target.mElement == null)
            {
                return string.Empty;
            }
            var txt = target.mElement.FindElements(By.XPath(mIsWebView ? ".//*[contains(@class, 'button-label')]" : "./*/*")).FirstOrDefault();
            if (txt == null)
            {
                return string.Empty;
            }
            return txt.Text;
#else
            return target.GetValue();
#endif
        }
    }
}
