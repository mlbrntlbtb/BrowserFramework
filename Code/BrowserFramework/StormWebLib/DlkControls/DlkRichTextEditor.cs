using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;

namespace StormWebLib.DlkControls
{
    [ControlType("RichTextEditor")]
    public class DlkRichTextEditor : DlkBaseControl
    {
        public DlkRichTextEditor(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRichTextEditor(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRichTextEditor(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            //DlkEnvironment.AutoDriver.SwitchTo().Frame("cke_wysiwyg_frame cke_reset");
            DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']")));
            DlkEnvironment.mSwitchediFrame = true;
            FindElement();
            this.ScrollIntoViewUsingJavaScript(true);
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                mElement.Clear();

                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].focus();", mElement);
                }

                mElement.SendKeys(TextToEnter);
                mElement.SendKeys(Keys.Tab);
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
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
            Initialize();
            //String ActValue = GetAttributeValue("value");
            String ActValue = new DlkBaseControl("Control", mElement).GetValue();
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            Initialize();
            String ActValue = IsReadOnly();
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            Initialize();
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
            DlkLogger.LogInfo("VerifyExists() passed");
        }

        [Keyword("ClearField", new String[] { "1|text|Value|SampleValue" })]
        public void ClearField()
        {
            try
            {
                Initialize();
                
                //makes sure all characters are erased
                mElement.SendKeys(Keys.Control + "a");
                mElement.SendKeys(Keys.Backspace);
                mElement.SendKeys(Keys.Tab);
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
                DlkLogger.LogInfo("Successfully executed ClearField()");
            }
            catch (Exception e)
            {
                throw new Exception("ClearField() failed : " + e.Message, e);
            }
        }

        public void ClickBanner()
        {
            try
            {
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@role='dialog'][not(contains(@class,'fullscreen'))]")).Count > 0)
                {
                    DlkLogger.LogInfo("Performing Click on dialog to remove focus on textbox.");
                    DlkBaseControl dialogBody = new DlkBaseControl("Banner", "XPATH_DISPLAY", "//*[@role='dialog'][not(contains(@class,'fullscreen'))]/*[contains(@class,'content')]");
                    dialogBody.Click(5, 5);
                }
                else if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='banner']")).Count > 0)
                {
                    DlkLogger.LogInfo("Performing Click on Banner to remove focus on textbox.");
                    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", "XPATH_DISPLAY", "//div[@class='banner']");
                    bannerCtrl.Click();
                }
            }
            catch
            {
                //Do nothing -- there might be instances that setting a text or value would display a dialog message
                //Placing a log instead for tracking
                DlkLogger.LogInfo("Problem performing Click on Banner. Proceeding...");
            }
        }
    }
}
