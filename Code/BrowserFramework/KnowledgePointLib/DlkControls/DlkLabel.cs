using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;

namespace KnowledgePointLib.DlkControls
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

                string actualResult = DlkString.RemoveCarriageReturn(mElement.Text.Trim());
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies label's content. Requires ExpectedText parameter
        /// </summary>
        /// <param name="VerifyFontColor"></param>

        [Keyword("VerifyFontColor", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyFontColor(String RedOrGreen)
        {
            try
            {
                Initialize();
                string color = null;
                if (mElement.GetAttribute("class") == "valid")
                    color = "green";
                else if(mElement.GetAttribute("class") == "invalid")
                    color = "red";
                else
                    throw new Exception("VerifyFontColor() failed: Label type not supported");
                DlkAssert.AssertEqual("VerifyFontColor() : " + mControlName, color, RedOrGreen.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFontColor() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
