using System;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace StormTouchCRMLib.DlkControls
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

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter(String TextToEnter)
        {
            try
            {
                Initialize();
                string browser = DlkEnvironment.mBrowser.ToLower();
                //if(browser != "ios" && browser != "android")
                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                mElement.SendKeys(Keys.Return);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            String ActValue = GetAttributeValue("value");
            if (String.IsNullOrEmpty(ActValue) && !String.IsNullOrEmpty(this.mElement.Text))
            {
                ActValue = this.mElement.Text;
            }
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifySearchText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifySearchText(String ExpectedValue)
        {
            Initialize();
            string ActualValue = mElement.GetAttribute("placeholder");
            DlkAssert.AssertEqual("VerifySearchText", ExpectedValue, ActualValue);
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

        [Keyword("ClickTextboxButton")]
        public void ClickTextboxButton(String SearchOrClear)
        {
            try
            {
                Initialize();
                IWebElement ctlBtn = null;
                switch (SearchOrClear.ToLower()){
                    case "clear":
                        ctlBtn = mElement.FindElement(By.XPath("./following-sibling::div[contains(@class,'x-clear')]"));
                        (new DlkBaseControl("Textbox Button", ctlBtn)).Click();
                        break;
                    case "search":
                        //ctlBtn = mElement.FindElement(By.CssSelector("input::before")); //pseudoelements not found 
                        base.Click(-3,0);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextboxButton() failed : " + e.Message, e);
            }

        }
    }
}
