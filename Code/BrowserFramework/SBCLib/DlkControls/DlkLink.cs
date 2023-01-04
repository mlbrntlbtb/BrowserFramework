using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace SBCLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        #region Constructors
        public DlkLink(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
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
        /// Clicks specified link
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
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE" })]
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
                throw new Exception($"VerifyText() failed : {e.Message}", e);
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
                string ActValue = base.GetParent().GetAttribute("class").Contains("inactive") ? "true" : base.IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private Methods


        #endregion
    }
 }

