using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;

namespace FieldEaseLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkTextArea(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement)
        {
        }

        public DlkTextArea(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkTextArea(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkTextArea(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector)
        {
        }

        public DlkTextArea(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue)
        {
        }

        #endregion

        #region PRIVATE METHODS
        private void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }
        #endregion

        #region KEYWORDS
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                mElement.Click();
                if (DlkEnvironment.mBrowser.ToLower() != "ie")
                {
                    mElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    mElement.SendKeys(Keys.Control + "a");
                    mElement.SendKeys(Keys.Delete);
                }

                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                string ActualValue = !String.IsNullOrEmpty(mElement.Text) ? mElement.Text : new DlkBaseControl("TextArea", mElement).GetValue().Trim();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
