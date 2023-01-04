using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace TrafficLiveLib.DlkControls
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

        #region KEYWORDS

        [Keyword("Select", new String[] { "1|text|Value|OFF" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                var toggleState = mElement.FindElement(By.XPath("./parent::*")).GetAttribute("class");
                bool bClick = false;
                if ((toggleState.Contains("x-deltektoggle-off") || toggleState.Contains("x-deltektoggle_yes-off")) && Value.ToLower() == "yes")
                {
                    bClick = true;
                }
                else if ((toggleState.Contains("x-deltektoggle-on") || toggleState.Contains("x-deltektoggle_yes-on")) && Value.ToLower() == "no")
                {
                    bClick = true;
                }
                if (bClick) new DlkBaseControl("toggle",mElement).Tap();
                else throw new Exception("Select() failed. Either the type of the toggle is not supported yet or it is already set to the desired option");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", TrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|Yes/No" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = "";
                if (GetAttributeValue("class").Contains("x-deltektoggle-off") || GetAttributeValue("class").Contains("x-deltektoggle_yes-off"))
                {
                    ActText = "NO";
                }
                else if (GetAttributeValue("class").Contains("x-deltektoggle-on") || GetAttributeValue("class").Contains("x-deltektoggle_yes-on"))
                {
                    ActText = "YES";
                }

                DlkAssert.AssertEqual("VerifySelected: " + mControlName, ExpectedValue.ToUpper(), ActText);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        #endregion


    }
}
