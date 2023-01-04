using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using ConceptShareLib.DlkControls;

namespace ConceptShareLib.DlkControls
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
        }

        #region Keywords
        /// <summary>
        /// Verifies if textbox exists. Requires strExpectedValue - can either be True or False
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
        /// <summary>
        /// Verifies button's text. Requires strExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>
        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String strExpectedText)
        {
            try
            {
                String ActValue = GetAttributeValue("value");
                DlkAssert.AssertEqual("VerifyText()", strExpectedText, ActValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets text box value, requires strTextToEnter parameter
        /// </summary>
        /// <param name="TextToEnter">Text to set</param>
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String strTextToEnter)
        {
            try
            {
                Initialize();

                mElement.SendKeys(Keys.Control + "a");
                if (!string.IsNullOrEmpty(strTextToEnter))
                {
                    mElement.SendKeys(strTextToEnter);
                }

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
