using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace StormTouchCRMLib.DlkControls
{
    [ControlType("TextEditor")]
    public class DlkTextEditor : DlkBaseControl
    {
        public DlkTextEditor(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextEditor(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextEditor(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            DlkEnvironment.AutoDriver.SwitchTo().Frame("ceframe");
            //DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("//iframe[@id='ceframe']")));
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
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
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
            String ActValue = GetAttributeValue("value");
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            Initialize();
            String ActValue = IsReadOnly();
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            Initialize();
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkLogger.LogInfo("VerifyExists() passed");
        }

        


    }
}
