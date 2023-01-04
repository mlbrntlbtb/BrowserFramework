using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using ConceptShareLib.DlkControls;
using ConceptShareLib.DlkUtility;
using System.Threading;

namespace ConceptShareLib.DlkControls
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
                Click(4.5);

                DlkLogger.LogInfo("Click() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies button's text. Requires strExpectedText parameter
        /// </summary>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String strExpectedText)
        {
            try
            {
                String actualText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, strExpectedText, actualText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if button exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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
