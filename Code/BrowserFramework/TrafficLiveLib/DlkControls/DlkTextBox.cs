using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace TrafficLiveLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
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

        #region KEYWORDS

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                string browser = DlkEnvironment.mBrowser.ToLower();
                //if(browser != "ios" && browser != "android")
                    mElement.Clear();
                mElement.SendKeys(TextToEnter);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
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

        [Keyword("Clear")]
        public void Clear()
        {
            try
            {
                Initialize();
                var clearIcon = mElement.FindElement(By.XPath("./following-sibling::div[@class='x-clear-icon']"));
                DlkButton clearButton = new DlkButton("Clear Button", clearIcon);
                if (clearButton != null)
                {
                    clearButton.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Clear() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            Initialize();
            String ActValue = new DlkBaseControl("TargetTextbox", mElement).GetValue();
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        #endregion
    }
}
