using System;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("Link")]
    class DlkLink : DlkBaseControl
    {

        #region DECLARATIONS
        private Boolean IsInit;
        #endregion

        #region CONSTRUCTOR

        public DlkLink(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkLink(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public DlkLink(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkLink(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public DlkLink(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                MouseOver();

                if (mElement.GetAttribute("class") == "popupBtn")
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
                else
                {
                    Click(4.5);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToLower().Contains("the http request to the remote webdriver server for url"))
                    throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }
        #endregion

        #region METHODS
        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "button" || mElement.TagName == "img")
            {
                return true;
            }
            return false;
        }

        public String GetText()
        {
            Initialize();
            String mText = "";
            mText = GetAttributeValue("value");
            return mText;
        }

        #endregion
    }
}
