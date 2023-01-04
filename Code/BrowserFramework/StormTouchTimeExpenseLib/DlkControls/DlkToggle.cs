using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace StormTouchTimeExpenseLib.DlkControls
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
                if (Value.ToLower().Equals("on") || Value.ToLower().Equals("off"))
                {
                    bool bClick = false;
                    string toggleClass = GetAttributeValue("class");
                    if((toggleClass.Contains("x-deltektoggle-off") || toggleClass.Contains("x-off")) 
                        && Value.ToLower().Equals("on"))
                    {
                        //if the currentstate if OFF and the user wants to set the value to select ON,
                        bClick = true;
                    }
                    else if ((toggleClass.Contains("x-deltektoggle-on")||toggleClass.Contains("x-deltektoggle_yes-on")||toggleClass.Contains("x-on")) 
                        && Value.ToLower().Equals("off"))
                    {
                        //if the currentstate if ON and the user wants to set the value to OFF,
                        bClick = true;
                    }

                    if (bClick)
                        base.Click();
                }
                else
                {
                    throw new Exception("Set() failed : " + "Value must me either 'ON' or 'OFF'");
                }

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
                //edit class, this seems to change
                string toggleClass = GetAttributeValue("class");
                if (toggleClass.Contains("x-deltektoggle-off") || toggleClass.Contains("x-off"))
                {
                    ActText = "OFF";
                }
                else if (toggleClass.Contains("x-deltektoggle_yes-on") || toggleClass.Contains("x-on") || toggleClass.Contains("x-deltektoggle-on"))
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
