using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using KonaLib.DlkUtility;
using KonaLib.DlkControls;


namespace KonaLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkTextBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "input")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();

                mElement.Clear();
                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    mElement.SendKeys(TextToEnter);

                    if (mElement.GetAttribute("value").ToLower() != TextToEnter.ToLower())
                    {
                        mElement.Clear();
                        mElement.SendKeys(TextToEnter);
                    }
                }
                mElement.SendKeys(Keys.Shift + Keys.Tab);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String TextToVerify)
        {
            String ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyText()", TextToVerify, ActValue);
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            base.VerifyExists(Convert.ToBoolean(strExpectedValue));
            DlkLogger.LogInfo("VerifyExists() passed");
        }

        [Keyword("ShowAutoComplete", new String[] { "1|text|LookupString|AAA" })]
        public void ShowAutoComplete(String LookupString)
        {
            try
            {
                Initialize();
                if (string.IsNullOrEmpty(LookupString))
                {
                    LookupString = Keys.Backspace;
                }
                mElement.Clear();
                mElement.SendKeys(LookupString);
            }
            catch (Exception e)
            {
                throw new Exception("ShowAutoComplete() failed : " + e.Message, e);
            }
        }

        private void SetText(String sTextToEnter)
        {
            switch (DlkEnvironment.mBrowser.ToLower())
            {
                case "safari":
                    mElement.Clear();
                    mElement.SendKeys(sTextToEnter);
                    break;
                default:
                    mElement.Clear();
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.SendKeys(sTextToEnter).Build().Perform();
                    break;
            }
        }



    }
}
