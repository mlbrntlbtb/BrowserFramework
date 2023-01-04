using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("DatePicker")]
    public class DlkDatePicker : DlkBaseControl
    {
        #region Constructors
        public DlkDatePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDatePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDatePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if DatePicker exists. Requires TrueOrFalse - can either be True or False
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
        /// Verifies DatePicker's content. Requires ExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample DatePicker Text" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();

                string actualResult = DlkString.RemoveCarriageReturn(mElement.GetAttribute("value").Trim());
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
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
        /// Sets datepicker's date
        /// </summary>
        /// <param name="Value"></param>

        [Keyword("Set", new String[] { "1|text|Text To Verify|Sample DatePicker Text" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                mElement.SendKeys(Keys.Control + "a");
                mElement.SendKeys(Keys.Backspace);
                mElement.SendKeys(Value);

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
