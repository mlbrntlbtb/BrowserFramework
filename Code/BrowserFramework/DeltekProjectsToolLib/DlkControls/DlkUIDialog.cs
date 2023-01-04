using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using DeltekProjectsToolLib.DlkSystem;


namespace DeltekProjectsToolLib.DlkControls
{
    [ControlType("UIDialog")]
    public class DlkUIDialog : DlkBaseControl
    {

        public DlkUIDialog(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkUIDialog(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkUIDialog(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
            FindElement();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            base.VerifyExists(Convert.ToBoolean(ExpectedValue));
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Title|Sample form title" })]
        public void VerifyTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement titleElement = mElement.FindElement(By.XPath(".//div[@class='appriseTitle']"));
                DlkLabel lblTitle = new DlkLabel("Title", titleElement);
                lblTitle.VerifyText(ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMessage", new String[] { "1|text|Expected Message|Sample form message" })]
        public void VerifyMessage(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement messageElement = mElement.FindElement(By.XPath(".//div[@class='appriseContent']"));
                DlkLabel lblTitle = new DlkLabel("MessageBody", messageElement);
                lblTitle.VerifyText(ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickOKButton")]
        public void ClickOKButton()
        {
            try
            {
                Initialize();
                IWebElement buttonElement = mElement.FindElement(By.XPath(".//button[@id='apprise-btn-confirm']"));
                DlkButton btnClose = new DlkButton("ClickOKButton", buttonElement);
                btnClose.Click();
                DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
            }
            catch (Exception e)
            {
                throw new Exception("ClickOKButton() failed : " + e.Message, e);
            }
        }


    }
}
