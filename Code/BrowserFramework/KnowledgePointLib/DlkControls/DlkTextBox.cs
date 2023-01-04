using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        #region Constructors
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion
        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Verifies if textbox exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>

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
        /// Verifies control's text. Requires ExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>
        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();
                string value = null;
                if (mElement.GetAttribute("value") != null)
                    value = GetAttributeValue("value");
                else
                    value = mElement.Text;
                DlkAssert.AssertEqual("VerifyText()", ExpectedText, value);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), base.IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies error message in a textbox
        /// </summary>
        /// <param name="ExpectedErrorMessage"></param>
        [Keyword("VerifyErrorMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyErrorMessage(String ExpectedValue)
        {
            try
            {
                Initialize();
                string actualErrorMessage = mElement.FindElement(By.XPath("./parent::div/parent::div//p[contains(@class,'Mui-error')]")).Text;
                DlkAssert.AssertEqual("VerifyErrorMessage()", ExpectedValue, actualErrorMessage);
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Error message does not exist or is not supported");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyErrorMessage() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets text box value, requires TextToEnter parameter
        /// </summary>
        /// <param name="TextToEnter">Text to set</param>
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                // Clear textbox first by sending ctrl a + backspace
                mElement.SendKeys(Keys.Control + "a");
                mElement.SendKeys(Keys.Backspace);
                mElement.SendKeys(TextToEnter);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
