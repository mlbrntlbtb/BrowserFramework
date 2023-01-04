using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace TEMobileLib.DlkControls
{
    [ControlType("Toggle")]
    public class DlkToggle : DlkBaseControl
    {
        public DlkToggle(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToggle(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToggle(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }

        [Keyword("Set", new String[] { "1|text|Value|OFF" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                bool bClick = false;
                if (GetAttributeValue("class").Contains("toggle-off") && Value.ToLower() == "on")
                {
                    bClick = true;
                }
                else if (GetAttributeValue("class").Contains("toggle-on") && Value.ToLower() == "off")
                {
                    bClick = true;
                }

                if (bClick)
                    base.Click();

            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ON" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = "";
                if (GetAttributeValue("class").Contains("toggle-off"))
                {
                    ActText = "OFF";
                }
                else if (GetAttributeValue("class").Contains("toggle-on"))
                {
                    ActText = "ON";
                }

                DlkAssert.AssertEqual("VerifyValue: " + mControlName, ExpectedValue.ToUpper(), ActText);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|Expected Value|ON" })]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();
                String ActText = "";
                if (GetAttributeValue("class").Contains("x-deltektoggle-off"))
                {
                    ActText = "OFF";
                }
                else if (GetAttributeValue("class").Contains("x-deltektoggle-on"))
                {
                    ActText = "ON";
                }

                DlkVariable.SetVariable(VariableName, ActText);
                DlkLogger.LogInfo("GetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
            }
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

    }
}
