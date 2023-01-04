using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for Tab Pages
    /// </summary>
    [ControlType("TabPage")]
    public class DlkNavTabPage : DlkBaseTabPage
    {
        public DlkNavTabPage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavTabPage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavTabPage(String ControlName, IWebElement ExistingWebElement)
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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Result|TRUE" })]      
        public void VerifyExists(String ExpectedResult)
        {
            base.VerifyExists(bool.Parse(ExpectedResult));
        }

    }
}

