using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        private const string ORANGE_RGB = "color: rgb(242, 119, 42)";

        public DlkLink(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Click(1.5);
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickLinkIfExists")]
        public void ClickLinkIfExists()
        {
            try
            {
                if (Exists())
                {
                    Click(1.5);
                }
                else
                {
                    DlkLogger.LogInfo("ClickLinkIfExists(): Link does not exist.");
                }                    

                DlkLogger.LogInfo("ClickLinkIfExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLinkIfExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
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
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyIfValidLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIfValidLink(String ExpectedValue)
        {
            try
            {
                Initialize();

                string linkClass = GetAttributeValue("class");
                bool actualValue;

                switch (linkClass.ToLower())
                {
                    case "navrptavail":
                    case "dtbbtn":
                    case "dashheadlink":
                    case "dashoptionslink": actualValue = true;
                        break;
                    case "navrptactive": actualValue = false;
                        break;
                    default:
                        throw new ArgumentException($"VerifyIfValidLink() failed : Class {linkClass} not supported");
                }

                DlkAssert.AssertEqual("VerifyIfValidLink()", bool.Parse(ExpectedValue), actualValue);
                DlkLogger.LogInfo("VerifyIfValidLink() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfValidLink() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(string ExpectedValue)
        {
            try
            {
                Initialize();

                string state = GetAttributeValue("dis");
                bool actualValue;

                switch (state)
                {
                    case null: actualValue = false;
                        break;
                    case "1": actualValue = true;
                        break;
                    default:
                        throw new ArgumentException($"VerifyReadOnly() failed : Link class not supported");
                }

                DlkAssert.AssertEqual("VerifyReadOnly()", bool.Parse(ExpectedValue), actualValue);
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
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
