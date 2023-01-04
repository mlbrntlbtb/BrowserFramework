using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace VisionTimeLib.DlkControls
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
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyBlank", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyBlank(String TrueOrFalse)
        {
            String ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyBlank()", Convert.ToBoolean(TrueOrFalse), String.IsNullOrEmpty(ActValue));
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
                        try
                        {
                            ctlBtn = mElement.FindElement(By.XPath("./following-sibling::div[contains(@id,'numpadclear')]"));
                        }catch {
                            ctlBtn = mElement.FindElement(By.XPath("./following-sibling::div[contains(@class,'x-clear')]"));
                        }
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
