using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using DeltekProjectsToolLib.DlkSystem;

namespace DeltekProjectsToolLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                SetText(Value);
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
            String ActValue = new DlkBaseControl("TextBox", mElement).GetValue();
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        /// <summary>
        /// Set Text box value
        /// </summary>
        /// <param name="Value">Text to set</param>
        private void SetText(String Value)
        {
            mElement.Clear();
            mElement.Click();
            if (!string.IsNullOrEmpty(Value))
            {
                mElement.SendKeys(Value);
            }
        }
    }
}
