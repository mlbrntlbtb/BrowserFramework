using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using DCOLib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace DCOLib.DlkControls
{
    [ControlType("TextArea")]
    public class DlkTextArea : DlkBaseControl
    {

        public DlkTextArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                mElement.Clear();
                mElement.Click();
                mElement.SendKeys(TextToEnter);

                DlkLogger.LogInfo("Successfully executed Set() : " + mControlName + ": " + TextToEnter);
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String TextToVerify)
        {
            try
            {
                String actualValue = DlkString.ReplaceCarriageReturn(GetValue(), "\n");
                String expectedValue = DlkString.ReplaceCarriageReturn(TextToVerify, "\n");

                DlkAssert.AssertEqual("VerifyText()", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed " + e.Message, e);
            }
        }

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
                throw new Exception("VerifyExists() failed " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|VariableName|MyVar" })]
        public void GetValue(string sVariableName)
        {
            try
            {
                Initialize();
                String txtValue = GetAttributeValue("value");
                DlkVariable.SetVariable(sVariableName, txtValue);
                DlkLogger.LogInfo("Successfully executed GetValue().");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
            }
        }

    }
}
