using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;

namespace SBCLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        #region Constructors
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if label exists. Requires TrueOrFalse - can either be True or False
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
        /// Verifies label's content. Requires ExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();

                string actualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "\n");
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Attempts to click a label and will check if it's clickable. Return true if click fails, false if click is successful. 
        /// </summary>
        [Keyword("VerifyClickFailed")]
        public void VerifyClickFailed(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool ActValue = false;
                try
                {
                    Click(1);
                    DlkLogger.LogInfo("Click successfully executed");
                }
                catch (Exception e)
                {
                    ActValue = true;
                    DlkLogger.LogInfo("Attempt to click failed : " + e.Message);
                }
                DlkAssert.AssertEqual("VerifyClickFailed()", Convert.ToBoolean(TrueOrFalse), ActValue);
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("VerifyClickFailed() failed : " + e.Message);
            }
        }

        #endregion
    }
}
