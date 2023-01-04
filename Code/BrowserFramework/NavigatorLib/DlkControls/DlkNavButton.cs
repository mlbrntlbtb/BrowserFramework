using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for Buttons
    /// </summary>
    [ControlType("Button")]
    public class DlkNavButton : DlkBaseButton
    {
        public DlkNavButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        [Keyword("Click")]
        public new void Click()
        {
            base.Click();
        }

        [Keyword("GetText")]
        public void GetText(String OutputVariable)
        {
            DlkVariable.SetVariable(OutputVariable, base.GetText());
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public new void VerifyText(String ExpectedText)
        {
            base.VerifyText(ExpectedText);
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedResult)
        {
            base.VerifyExists(bool.Parse(ExpectedResult));
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", strExpectedValue.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }


    }
}

