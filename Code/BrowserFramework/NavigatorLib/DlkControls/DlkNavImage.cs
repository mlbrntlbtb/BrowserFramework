using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for Buttons
    /// </summary>
    [ControlType("Image")]
    public class DlkNavImage : DlkBaseImage
    {
        public DlkNavImage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavImage(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        [Keyword("VerifyImageNotBroken")]
        public new void VerifyImageNotBroken()
        {
            base.VerifyImageNotBroken();
        }

        [Keyword("Click")]
        public new void Click()
        {
            base.Click();
        }
    }
}

