using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
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

                mElement.Clear();
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                    
                    if (mElement.GetAttribute("value").ToLower() != Value.ToLower())
                    {
                        mElement.Clear();
                        mElement.SendKeys(Value);
                    }
                }
                //mElement.SendKeys(Keys.Shift + Keys.Tab);
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
            String ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);            
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

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
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
            }
            catch (Exception e)
            {
                throw new Exception("ShowAutoComplete() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndClickSuggestionBox")]
        public void SetAndClickSuggestionBox(String TextToEnter, String SuggestionText)
        {
            Initialize();

            bool isNotClicked = true;

            while(isNotClicked)
            {                
                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                
                Thread.Sleep(3000);
                IWebElement suggestionBox = mElement.FindElement(By.XPath("./following-sibling::div[@class='suggestions']//*[contains(.,'" + SuggestionText + "')]"));
                try
                {
                    suggestionBox.Click();
                }
                catch(Exception)
                {
                    continue; 
                }

                isNotClicked = false;

                Thread.Sleep(1000);
            }
            
            DlkLogger.LogInfo("SetAndClickSuggestionBox() Passed.");
        }

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter(String TextToEnter)
        {

            Initialize();

            mElement.Clear();
            mElement.SendKeys(TextToEnter);
            Thread.Sleep(DlkEnvironment.MediumWaitMs);
            //if the Selenium SendKeys failed, try to use the Browser's native send keys method
            if (mElement.GetAttribute("value") != TextToEnter)
            {
                DlkLogger.LogInfo("Set() Retrying...");
                SetText(TextToEnter);
                Thread.Sleep(DlkEnvironment.MediumWaitMs);
            }

            mElement.SendKeys(Keys.Enter);

            //DlkLogger.LogInfo("SetAndEnter() : control name = '" + mControlName + "' , text to enter = '" + TextToEnter + "'");
            DlkLogger.LogInfo("Successfully executed SetAndEnter()");

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
