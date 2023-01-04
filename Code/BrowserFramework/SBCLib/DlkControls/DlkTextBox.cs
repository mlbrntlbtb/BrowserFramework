using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace SBCLib.DlkControls
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
                String ActValue = GetAttributeValue("value");
                DlkAssert.AssertEqual("VerifyText()", ExpectedText, ActValue);
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
                Initialize();
                string bActual = base.mElement.TagName == "textarea" ? new DlkBaseControl("Parent",base.mElement.FindElement(By.XPath(".//parent::*"))).IsReadOnly() : base.IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ",  TrueOrFalse.ToLower(), bActual.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
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
                if (mElement.TagName.Equals("textarea")) mElement.Click();
                if (!string.IsNullOrEmpty(base.GetValue())) mElement.SendKeys(Keys.Control + "a");

                if (mElement.TagName.Equals("textarea"))
                {
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.SendKeys(TextToEnter).Build().Perform();
                }else{
                    mElement.SendKeys(TextToEnter);
                }

                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clear text box value
        /// </summary>
        [Keyword("Clear", new String[] { "1|text|Value|SampleValue" })]
        public void Clear()
        {
            try
            {
                Initialize();
                if(!String.IsNullOrWhiteSpace(mElement.Text)) mElement.SendKeys(Keys.Control + "a" + Keys.Delete);
                DlkLogger.LogInfo("Successfully executed Clear()");
            }
            catch (Exception e)
            {
                throw new Exception("Clear() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
