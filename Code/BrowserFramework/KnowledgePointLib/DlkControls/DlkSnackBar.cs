using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("SnackBar")]
    public class DlkSnackBar : DlkBaseControl
    {
        #region Constructors
        public DlkSnackBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSnackBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSnackBar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if SnackBar exists. Requires TrueOrFalse - can either be True or False
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
        /// Verifies SnackBar's content. Requires ExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample SnackBar Text" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();

                string actualResult = DlkString.RemoveCarriageReturn(mElement.Text.Trim());
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
