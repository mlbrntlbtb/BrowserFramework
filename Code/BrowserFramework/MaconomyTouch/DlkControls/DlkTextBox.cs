using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkMobileControl
    {
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                HideKeyboard();
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            Initialize();
            String ActValue = new DlkBaseControl("TargetTextbox", mElement).GetValue();
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExactText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactText(String ExpectedValue)
        {
            Initialize();
            String ActValue = new DlkBaseControl("TargetTextbox", mElement).GetValue();
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextContains(String PartialText, String TrueOrFalse)
        {
            try
            {
                bool expected = false;
                if (!Boolean.TryParse(TrueOrFalse, out expected))
                {
                    throw new ArgumentException("Parameter must be true or false.");
                }
                Initialize();
                String ActValue = new DlkBaseControl("Target textbox", mElement).GetValue().ToLower();
                DlkAssert.AssertEqual("VerifyTextContains()", expected, ActValue.Contains(PartialText.ToLower()));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
         
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkLogger.LogInfo("VerifyExists() passed");
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
