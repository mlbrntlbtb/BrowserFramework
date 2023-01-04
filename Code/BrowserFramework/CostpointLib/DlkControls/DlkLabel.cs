using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace CostpointLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        private const string ORANGE_RGB = "color: rgb(242, 119, 42)";

        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
            {
                FindElement();
            }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify)
        {
            Initialize();

            //sanitize the strings to compare: we want to unify all carriage returns to compare more accurately
            string actualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "\n");
            string textToVerify = DlkString.ReplaceCarriageReturn(TextToVerify, "\n");

            DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            base.VerifyExists(Convert.ToBoolean(ExpectedValue));
        }

        [Keyword("VerifyTextColor", new String[] { "1|text|ExpectedColor|Orange" })]
        public void VerifyTextColor(string ExpectedColor)
        {
            try
            {
                Initialize();
                string actualColor = "Default";
                string style = mElement.GetAttribute("style");

                if (style != null && style.Contains(ORANGE_RGB))
                {
                    actualColor = "Orange";
                }

                DlkAssert.AssertEqual("VerifyTextColor()", ExpectedColor, actualColor);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextColor() failed : " + e.Message, e);
            }
        }
    }
}
