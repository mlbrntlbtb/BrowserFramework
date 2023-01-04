using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace StormWebLib.DlkControls
{
    /// <summary>
    /// Navigator class for Buttons
    /// </summary>
    [ControlType("Image")]
    public class DlkImage : DlkBaseImage
    {
        public DlkImage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkImage(String ControlName, IWebElement ExistingWebElement)
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
            try
            {
                Initialize();
                ClickUsingJavaScript();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("MouseOver")]
        public new void MouseOver()
        {
            base.MouseOver();
            DlkLogger.LogInfo("MouseOver() passed.");
        }

        [Keyword("VerifyExists")]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyToolTip(String ExpectedValue)
        {
            Initialize();
            String ActToolTip = mElement.GetAttribute("title");
            DlkAssert.AssertEqual("Verify tooltip for button: " + mControlName, ExpectedValue, ActToolTip);
            DlkLogger.LogInfo("VerifyToolTip() passed");
        }

        [Keyword("VerifyPhotoDimmed", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPhotoDimmed(String TrueOrFalse)
        {
            Initialize();
            IWebElement photo = mElement.FindElement(By.XPath(".//parent::div[contains(@class,'sidebar-image')]"));
            String ActValue = photo.GetAttribute("class");
            if (ActValue.Contains("inactive"))
            {
                ActValue = "true";
            }
            else
            {
                ActValue = "false";
            }
            DlkAssert.AssertEqual("VerifyPhotoDimmed()", TrueOrFalse.ToLower(), ActValue.ToLower());
            DlkLogger.LogInfo("VerifyPhotoDimmed() passed");
        }     
    }
}

