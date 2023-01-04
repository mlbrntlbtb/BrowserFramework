using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for Label
    /// </summary>
    [ControlType("Label")]
    public class DlkNavLabel : DlkBaseLabel
    {
        public DlkNavLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }


        [Keyword("VerifyText", new String[] { "1|text|Expected Text To Verify|Sample Text" })]
        public new void VerifyText(String ExpectedText)
        {
            base.VerifyText(ExpectedText);
        }

        [Keyword("VerifyPartialText", new String[] { "1|text|Expected part of Text To Verify|Sample Text" })]
        public new void VerifyPartialText(String ExpectedText)
        {
            base.VerifyPartialText(ExpectedText);
        }

       [Keyword("VerifyExists", new String[] { "1|text|Expected Result|TRUE" })]
        public void VerifyExists(String ExpectedResult)
        {
            base.VerifyExists(bool.Parse(ExpectedResult));
        }

    }
}

