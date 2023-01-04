using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using SFTLib.DlkUtility;
using System.Linq;

namespace SFTLib.DlkControls
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
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;

        }
        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        public Boolean GetToggledState()
        {
            if (mElement.GetAttribute("class").ToLower().Contains("pressed"))
            {
                return true;
            }
            else
            {
                if (mElement.FindElements(By.XPath(".//parent::*")) != null)
                {
                    if (mElement.FindElements(By.XPath(".//parent::*")).FirstOrDefault() != null)
                    {
                        return mElement.FindElements(By.XPath(".//parent::*")).FirstOrDefault().GetAttribute("class").ToLower().Contains("pressed");
                    }
                }

                return false;
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|OFF" })]
        public void Set(String Value)
        {
            if(Value.ToLower() != "off" && Value.ToLower() != "on")
                throw new Exception("The only acceptable values are 'off' or 'on'.");

            try
            {
                Initialize();
                bool hasPressedClass = mElement.GetAttribute("class").ToLower().Contains("pressed") ||
                                       mElement.FindElements(By.XPath(".//parent::*")).FirstOrDefault().GetAttribute("class").ToLower().Contains("pressed");

                if (!hasPressedClass && Value.ToLower() == "on" || hasPressedClass && Value.ToLower() == "off")
                    mElement.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }

        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ON" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = "";
                if (GetToggledState())
                {
                    ActText = "ON";
                }
                else
                {
                    ActText = "OFF";
                }

                DlkAssert.AssertEqual("VerifyValue: " + mControlName, ExpectedValue.ToUpper(), ActText);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
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
            finally
            {
                Terminate();
            }
        }

    }
}
