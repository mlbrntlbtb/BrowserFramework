using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace AcumenTouchStoneLib.DlkControls
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

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }
    }
}

