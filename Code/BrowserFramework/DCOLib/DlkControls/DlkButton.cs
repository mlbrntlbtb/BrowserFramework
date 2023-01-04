using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace DCOLib.DlkControls
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
                Click(4.5);
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

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
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #region private methods
        private String GetText()
        {
            Initialize();
            String mText = GetAttributeValue("value");

            //this is to handle other button controls with text as element's text
            if (string.IsNullOrEmpty(mText))
            {
                mText = mElement.Text;
            }
                
            return mText;
        }

        #endregion

    }
}
