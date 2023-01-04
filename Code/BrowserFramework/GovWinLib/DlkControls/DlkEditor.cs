using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Editor")]
    public class DlkEditor : DlkBaseControl
    {
        private Boolean IsInit = false;
        

        public DlkEditor(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkEditor(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkEditor(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkEditor(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        [Keyword("Set", new String[] {"1|text|Value|SampleValue"})]
        public void Set(String TextToEnter)
        {
            Initialize();
            Clear();
            mElement.SendKeys(TextToEnter);
            DlkLogger.LogInfo("Successfully executed Set() : " + mControlName + ": " + TextToEnter);
        }

        [Keyword("AppendText", new String[] {"1|text|Cursor Position|Start", 
                                             "2|text|Text to Append|Additional text" })]
        public void AppendText(String CursorPosition, String TextToAppend)
        {
            Initialize();
            string strCurrentNotes = TrimValue(GetValue());

            if (CursorPosition.ToLower() == "start")
            {
                Clear();
                mElement.SendKeys(TextToAppend + strCurrentNotes);
                DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the start of the text.");
            }
            else if (CursorPosition.ToLower() == "end")
            {
                Clear();
                mElement.SendKeys(strCurrentNotes + TextToAppend);
                DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the end of the text.");
            }
        }

        private String TrimValue(String sValue)
        {
            sValue = sValue.Replace("\n", " ");
            sValue = sValue.Replace("\r", " ");
            while (sValue.Contains("  "))
            {
                sValue = sValue.Replace("  ", " ");
            }
            return sValue.Trim();
        }

        private void Clear()
        {
            ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("document.body.innerHTML = '<br>'");
        }

        #region Verify methods
        [RetryKeyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            String textToVerify = ExpectedValue;

            this.PerformAction(() =>
            {
                DlkAssert.AssertEqual("VerifyText", textToVerify, TrimValue(GetValue()));
            }, new String[] {"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Initialize();
                //Boolean bExists = Exists();

                //DlkAssert.AssertEqual("VerifyExists", Convert.ToBoolean(expectedValue), bExists);
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));

            }, new String[] { "retry" });
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }
        #endregion
    }
}

