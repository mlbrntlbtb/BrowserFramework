using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.IO;

namespace HRSmartLib.LatestVersion.DlkControls
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
                if (Value.Equals(DlkCommon.DlkCommonFunction.SKIP_TEXTBOX_SET))
                {
                    DlkLogger.LogWarning(string.Concat("using ", DlkCommon.DlkCommonFunction.SKIP_TEXTBOX_SET, ", skipped setting this element."));
                }
                else
                {
                    Initialize();
                    selectAllClear();
                    if (!string.IsNullOrEmpty(Value))
                    {
                        mElement.SendKeys(Value);

                        if (mElement.GetAttribute("value").ToLower() != Value.ToLower())
                        {
                            mElement.Clear();
                            selectAllClear();
                            mElement.SendKeys(Value);
                        }
                    }
                    //mElement.SendKeys(Keys.Shift + Keys.Tab);
                    DlkLogger.LogInfo("Successfully executed Set()");
                }
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
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text|SampleValue" })]
        public void VerifyTextContains(string Text)
        {
            string ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyTextContains()", true, ActValue.Contains(Text));
            DlkLogger.LogInfo("VerifyTextContains() passed");
        }

        [Keyword("FileUpload")]
        public void FileUpload(string FileName)
        {
            try
            {
                Initialize();
                if (mElement.TagName.ToLower().Equals("input") &&
                    mElement.GetAttribute("type").Contains("file"))
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    js.ExecuteScript("arguments[0].style.visibility='visible'", mElement);
                    mElement.SendKeys(Path.IsPathRooted(FileName) ? FileName : DlkEnvironment.mDirUserData + FileName);
                }
                else
                {
                    throw new Exception("Textbox does not support file upload. It should contain 'type' attribute with 'file' value.");
                }
                DlkLogger.LogInfo("FileUpload() passed");
            }
            catch (Exception e)
            {
                throw new Exception("FileUpload() failed : " + e.Message, e);
            }
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



        private void selectAllClear()
        {
            mElement.SendKeys(Keys.Control + "a");
            mElement.SendKeys(Keys.Delete);
        }

    }
}
