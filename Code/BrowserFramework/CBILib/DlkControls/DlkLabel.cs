using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
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
        ///  Verifies if label exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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
        /// Verifies label's content. Requires strExpectedText parameter
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

        #endregion
    }
}
