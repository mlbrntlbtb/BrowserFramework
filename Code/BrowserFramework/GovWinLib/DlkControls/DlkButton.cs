using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace GovWinLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {                
                FindElement();
                IsInit = true;
            }
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "button" || mElement.TagName == "img")
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
            try
            {
                Initialize();

                if (mElement.GetAttribute("class") == "popupBtn")
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
                else if (mElement.GetAttribute("class") == "medium")//for upload/download button
                {
                    DlkEnvironment.mPreviousTitle = DlkEnvironment.AutoDriver.Title;
                    ClickByObjectCoordinates();
                }
                else
                {
                    mElement.Click();
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToLower().Contains("the http request to the remote webdriver server for url"))
                    throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        public String GetText()
        {
            Initialize();
            String mText = "";
            mText = GetAttributeValue("value");
            return mText;
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
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


        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", TrueOrFalse.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("FileUpload", new String[] {"1|text|File Name|C:\\test.txt",
                                                "2|text|Wait Time(secs)|30"})]
        public static void FileUpload(String Filename, String WaitTime)
        {
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.FileUploadWithDelay(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }
    }
}
