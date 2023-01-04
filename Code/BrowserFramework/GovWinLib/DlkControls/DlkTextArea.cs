using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {
        private Boolean IsInit = false;
        

        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextArea(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, IWebElement ExistingWebElement)
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
            DlkEnvironment.FocusNewWindowHandle();
            Initialize();
            mElement.Clear();
            mElement.Click();
            mElement.SendKeys(TextToEnter);
            DlkLogger.LogInfo("Successfully executed Set() : " + mControlName + ": " + TextToEnter);
        }

        [Keyword("GetText", new String[] {"1|text|VariableName|MyText"})]
        public void GetText(String VariableName)
        {
            Initialize();
            String outputVal = GetValue();


            DlkVariable.SetVariable(VariableName, outputVal);

            DlkLogger.LogInfo("Successfully executed AppendText(), Output Text = '" + outputVal + "'");

        }

        [Keyword("AppendText", new String[] {"1|text|Cursor Position (Start or End)|Start", 
                                                "2|text|Text to Append|Additional text" })]
        public void AppendText(String CursorPosition, String TextToAppend)
        {
            Initialize();
            string strCurrentNotes = mElement.GetAttribute("value");

            if (CursorPosition.ToLower() == "start")
            {
                mElement.Clear();
                mElement.SendKeys(TextToAppend + strCurrentNotes);
                DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the start of the text.");
            }
            else if (CursorPosition.ToLower() == "end")
            {
                mElement.Clear();
                mElement.SendKeys(strCurrentNotes + TextToAppend);
                DlkLogger.LogInfo("Successfully executed AppendText() : " + TextToAppend + " added at the end of the text.");
            }
        }

        #region Verify methods
        [RetryKeyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedText)
        {
            String TextToVerify = ExpectedText.TrimStart(' ').TrimEnd(' ');

            this.PerformAction(() =>
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyText()", TextToVerify, GetValue());
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyContainsText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyContainsText(String ExpectedValue)
        {
            String TextToVerify = ExpectedValue.TrimStart(' ').TrimEnd(' ');
            bool containsText = false;

            this.PerformAction(() =>
            {
                Initialize();
                if (GetValue().Contains(TextToVerify))
                    containsText = true;
                DlkAssert.AssertEqual("VerifyContainsText()", true, containsText);
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {

            this.PerformAction(() =>
            {
                VerifyAttribute("isContentEditable", ExpectedValue);
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
 
            this.PerformAction(() =>
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                /*
                Boolean bExists = Exists();

                if (bExists == Convert.ToBoolean(ExpectedValue))
                {
                    DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + ExpectedValue);
                }
                else
                {
                    throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + ExpectedValue));
                }*/
            }, new String[]{"retry"});
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

