using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace StormTouchCRMLib.DlkControls
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

        [Keyword("VerifyBlank", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyBlank(String TrueOrFalse)
        {
            try
            {
                // perform checks before performing keyword logic
                bool expected = false;
                if (!Boolean.TryParse(TrueOrFalse, out expected))
                {
                    throw new ArgumentException("Parameter must be either true or false.");
                }
                Initialize();
                bool isBlank = false;

                DlkLogger.LogInfo("Looking at the label text...");
                isBlank = mElement.Text.Equals(String.Empty);

                DlkAssert.AssertEqual("Verify if text is blank/empty for label: " + mControlName, expected, isBlank);
                DlkLogger.LogInfo("VerifyBlank() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyBlank() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify="")
        {
            try
            {
                Initialize();
                string ExpectedValue = TextToVerify.Replace("\r\n", "\n").Replace("\r", "\n");
                string ActualValue = GetValue().Replace("\r\n", "\n").Replace("\r", "\n");
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualValue);
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
    }
}
