using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        #region Constructors
        public DlkButton(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Clicks specified button
        /// </summary>
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                if (mElement.TagName.Equals("span") && !mElement.GetAttribute("class").Contains("close-modal")) // clickusingjs not applicable for dialogs
                {
                    ClickUsingJavaScript(false);
                }
                else
                {
                    Click(4.5);
                }
                DlkLogger.LogInfo("Click() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies button's text. Requires ExpectedText parameter
        /// </summary>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();
                String actualText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedText, actualText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if button exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
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

        /// <summary>
        /// Verifies if button is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower() , base.IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Handles other button controls with text as element's text
        /// </summary>
        /// <returns></returns>
        private String GetText()
        {
            FindElement();
            String mText = GetAttributeValue("value");

            if (string.IsNullOrEmpty(mText))
            {
                mText = mElement.Text;
            }

            return mText;
        }

        #endregion
    }
 }

