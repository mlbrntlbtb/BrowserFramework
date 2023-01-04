using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Image")]
    public class DlkImage : DlkBaseControl
    {
        #region Constructors
        public DlkImage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkImage(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if Image exists. Requires TrueOrFalse - can either be True or False
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
        ///  Verifies if Image exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyUploadedImageExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyUploadedImageExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement image = mElement.FindElements(By.XPath(".//img")).FirstOrDefault();
                bool hasImage = true;
                if (image == null)
                    hasImage = false;
                DlkAssert.AssertEqual("VerifyUploadedImageExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), hasImage);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyUploadedImageExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies Image's content. Requires ExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Image Text" })]
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
