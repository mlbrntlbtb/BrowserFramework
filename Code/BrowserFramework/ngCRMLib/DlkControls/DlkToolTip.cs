using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace CommonLib.DlkControls
{
    /// <summary>
    /// Navigator class for Buttons
    /// </summary>
    [ControlType("ToolTip")]
    public class DlkToolTip : DlkBaseControl
    {
        public DlkToolTip(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolTip(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToolTip(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        private void Initialize()
        {
            FindElement();
        }

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

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|Sample tool tip text" })]
        public void VerifyText(String ExpectedValue)
        {
            //Mew line was represented by "\n" when the data was entered from Test Runner.
            //When the data was derived from the website, new line was represented by "\r\n".
            //Replacing "\r\n" with "\n" on both the expected and actual value will eliminate errors caused by
            //inconsistent representations of the new line coming from Test Runner (manual data entry
            //and data driven) and from the website especially on data with multiple lines.
            string toolTipValue = GetValue().Replace("\r\n", "\n");
            string expectedValue = ExpectedValue.Replace("\r\n", "\n");
            DlkAssert.AssertEqual("VerifyText", expectedValue, toolTipValue);
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|Sample tool tip text" })]
        public void VerifyTextContains(String PartialValue)
        {
            try
            {
                Initialize();
                string actualValue = GetValue();
                DlkLogger.LogInfo("Actual value [" + actualValue + "]");
                DlkLogger.LogInfo("Expected partial value [" + PartialValue + "]");
                DlkAssert.AssertEqual("VerifyTextContains", true, DlkString.NormalizeNonBreakingSpace(
                    DlkString.ReplaceCarriageReturn(actualValue, "")).Contains(PartialValue));
                DlkLogger.LogInfo("VerifyTextContains() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }
    }
}

