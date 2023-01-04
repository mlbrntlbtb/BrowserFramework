using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("MaskedTextBox")]
    public class DlkMaskedTextBox : DlkBaseControl
    {
        #region Constructors
        public DlkMaskedTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMaskedTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMaskedTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkMaskedTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region CONSTRUCTOR
        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActValue = string.Empty;

                if (mElement.GetAttribute("class").Contains("pin"))
                {
                    IWebElement pinBullet = mElement.FindElement(By.XPath(".//div[@class='x-innerhtml']/div"));
                    ActValue = pinBullet.Text.Trim();
                }
                else
                {
                    ActValue = GetValue().Trim();
                }

                if (ActValue.Contains("\r\n"))
                {
                    ActValue = ActValue.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyValue() : " + mControlName, ExpectedValue.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("VerifyValue() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActValue = string.Empty;

                if (mElement.GetAttribute("class").Contains("pin"))
                {
                    IWebElement pinBullet = mElement.FindElement(By.XPath(".//div[@class='x-innerhtml']/div"));
                    ActValue = pinBullet.Text.Trim();
                }
                else
                {
                    ActValue = GetValue().Trim();
                }

                if (ActValue.Contains("\r\n"))
                {
                    ActValue = ActValue.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyValue() : " + mControlName, ExpectedValue, ActValue);
                DlkLogger.LogInfo("VerifyValue() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
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
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
