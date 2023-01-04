using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace KonaLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "button" || GetAttributeValue("type") == "button" || mElement.TagName == "img")
            {
                return true;
            }
            else if(mElement.TagName == "span")
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::span[@type='button']"));
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {

            FindElement();
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
                else
                {
                    Click(4.5);
                }
            }
            catch (Exception e)
            {
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
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedText, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
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
