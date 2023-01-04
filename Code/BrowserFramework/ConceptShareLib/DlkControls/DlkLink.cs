using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptShareLib.DlkControls
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
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords

        /// <summary>
        /// Verifies if link exists. Requires strExpectedValue - can either be True or False
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
        /// Verifies link's text. Requires strExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>
        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String strExpectedText)
        {
            try
            {
                Initialize();

                string actualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "\n");
                string textToVerify = DlkString.ReplaceCarriageReturn(strExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Clicks link
        /// </summary>
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                ClickByObjectCoordinates();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
