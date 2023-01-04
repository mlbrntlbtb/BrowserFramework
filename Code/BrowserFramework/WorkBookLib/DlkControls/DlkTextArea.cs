using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Collections;

namespace WorkBookLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private String mEditorFrameXPath = ".//iframe[contains(@class,'cke_wysiwyg_frame')]";
        private IWebElement mEditorElement;
        #endregion

        #region CONSTRUCTORS
        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
                   : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextArea(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            //Switch to frame of text area
            IWebElement mFrameElement = mElement.FindElements(By.XPath(mEditorFrameXPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mEditorFrameXPath)) : throw new Exception("Text area frame not found");
            DlkEnvironment.AutoDriver.SwitchTo().Frame(mFrameElement);
            mEditorElement = DlkEnvironment.AutoDriver.FindElements(By.XPath(".//body")).Count > 0 ?
                DlkEnvironment.AutoDriver.FindElement(By.XPath(".//body")) : throw new Exception("Text area body not found");
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                mEditorElement.Clear();
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("arguments[0].focus();", mEditorElement);
                }
                else
                {
                    mEditorElement.SendKeys(Keys.Shift + Keys.Tab);
                }
                mEditorElement.SendKeys(Value);
                mEditorElement.SendKeys(Keys.Tab);

                DlkLogger.LogInfo("Set() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkBaseControl textArea = new DlkBaseControl("EditorElement", mEditorElement);
                string ActualValue = DlkString.ReplaceCarriageReturn(textArea.GetValue(), "");
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed " + e.Message, e);
            }
            finally
            {
                //revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();

            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
