using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace ngCRMLib.DlkControls
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

        [Keyword("Click")]
        public new void Click()
        {
            base.Click();
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                string ActualText = "";

                mElement.Clear();
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);

                    ActualText = mElement.GetAttribute("value");
                    if (ActualText == null)
                    {
                        ActualText = mElement.Text;
                    }

                    if (!mElement.GetAttribute("class").Contains("numberGridInput"))
                    {
                        if (ActualText.ToLower() != Value.ToLower())
                        {
                            mElement.Clear();
                            mElement.SendKeys(Value);
                        }
                    }
                }
                else
                {
                    mElement.SendKeys(Keys.Shift + Keys.Tab);
                }
                //mElement.SendKeys(Keys.Shift + Keys.Tab);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter (String Value)
        {
            try
            {
                Initialize();
                string ActualText = "";
                mElement.Clear();
                
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                    Thread.Sleep(3000);
                    mElement.SendKeys(Keys.Enter);

                    ActualText = mElement.GetAttribute("value");
                    if (ActualText == null)
                    {
                        ActualText = mElement.Text;
                    }
                    
                    if (ActualText.ToLower() != Value.ToLower())
                    {
                        mElement.Clear();
                        mElement.SendKeys(Value);
                        Thread.Sleep(3000);
                        mElement.SendKeys(Keys.Enter);
                    }
                }
                //mElement.SendKeys(Keys.Shift + Keys.Tab);
                DlkLogger.LogInfo("Successfully executed SetAndEnter()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("PressTab")]
        public void PressTab()
        {
            try
            {
                Initialize();
                mElement.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("Successfully executed PressTab()");
            }
            catch (Exception e)
            {
                throw new Exception("PressTab() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            String ActValue = GetAttributeValue("value");
            if (ActValue != ExpectedValue)
            {
                ActValue = mElement.Text;
            }

            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);            
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            String ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyTextContains()", true, ActValue.Contains(ExpectedValue));
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {            
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkLogger.LogInfo("VerifyExists() passed");            
        }

        [Keyword("ShowAutoComplete", new String[] { "1|text|LookupString|AAA" })]
        public void ShowAutoComplete(String LookupValue)
        {
            try
            {
                Initialize();
                if (string.IsNullOrEmpty(LookupValue))
                {
                    LookupValue = Keys.Backspace;
                }
                mElement.Clear();
                mElement.SendKeys(LookupValue);
                Thread.Sleep(3000);
                try
                {
                    if (mElement.Displayed == true)
                    {
                        mElement.SendKeys(Keys.Tab);
                    }
                }
                catch
                {
                    //nothing
                }
              // mElement.SendKeys(Keys.Enter);

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
