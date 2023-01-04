using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace TrafficLiveLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        #region Constructors
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }

        #region KEYWORDS

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();

                base.Click();

            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify = "")
        {
            try
            {
                Initialize();
                String ActValue = GetValue().Trim();

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
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String TextToVerify = "")
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, GetValue().Contains(TextToVerify));
                DlkLogger.LogInfo("VerifyTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", TrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
