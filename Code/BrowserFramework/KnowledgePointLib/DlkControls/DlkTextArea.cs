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
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {
        #region Constructors
        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextArea(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkTextArea(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion
        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Verifies if TextArea exists. Requires TrueOrFalse - can either be True or False
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
                string actualValue = null;
                if (GetAttributeValue("value") != null)
                    actualValue = GetAttributeValue("value");
                else
                    actualValue = mElement.Text;

                DlkAssert.AssertEqual("VerifyText()", ExpectedText, actualValue);
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
                IWebElement editor;
                bool readOnly = true;
                string type = GetTextAreaType();
                switch (type)
                {
                    case "editor":
                        editor = mElement.FindElements(By.XPath(".//div[@role='input' and contains(@class,'editableTextEditorDiv')]")).FirstOrDefault();
                        if (editor != null)
                            readOnly = false;
                        break;
                    default:
                        readOnly = Convert.ToBoolean(IsReadOnly());
                        break;
                }
                DlkAssert.AssertEqual("VerifyReadOnly() : ", Convert.ToBoolean(TrueOrFalse), readOnly);
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
                string type = GetTextAreaType();
                IWebElement textArea;
                switch (type)
                {
                    case "editor":
                        textArea = mElement.FindElements(By.XPath(".//div[@role='input' and contains(@class,'editableTextEditorDiv')]")).FirstOrDefault();
                        break;
                    default:
                        textArea = mElement;
                        break;
                }
                textArea.SendKeys(Keys.Control + "a" + Keys.Backspace);
                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    textArea.SendKeys(TextToEnter);
                }

                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private methods
        private string GetTextAreaType()
        {
            string type = null;
            string dataTestId = mElement.GetAttribute("data-testid");
            if (dataTestId != null && dataTestId.Contains("Editor"))
                type = "editor";
            else
                type = "default";
            return type;
        }
        #endregion
    }
}
