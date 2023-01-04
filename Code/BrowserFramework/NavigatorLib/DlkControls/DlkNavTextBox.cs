using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for Textboxes
    /// </summary>
    [ControlType("TextBox")]
    public class DlkNavTextBox : DlkBaseTextBox
    {
        public DlkNavTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }
               
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]      
        public new void Set(String TextToEnter)
        {
            base.Set(TextToEnter);
        }

        [Keyword("SetAndPressEnter", new String[] { "1|text|Value|SampleValue" })] 
        public new void SetAndPressEnter(String TextToEnter)
        {
            base.SetAndPressEnter(TextToEnter);
        }

        [Keyword("SetAndPressTab", new String[] { "1|text|Value|SampleValue" })]        
        public new void SetAndPressTab(String TextToEnter)
        {
            base.SetAndPressTab(TextToEnter);
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedResult)
        {
            base.VerifyExists(bool.Parse(ExpectedResult));
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public new void VerifyText(String TextToVerify)
        {
            base.VerifyText(TextToVerify);
        }
    }
}

