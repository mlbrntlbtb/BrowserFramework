using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using SFTLib.DlkControls;
using SFTLib.DlkUtility;
using SFTLib.DlkSystem;
using System.Threading;

namespace SFTLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        public void Initialize()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                Thread.Sleep(1000);

                DlkSFTCommon.ScrollToElement(this);
                DlkEnvironment.mSwitchediFrame = false;
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
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

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String TextToVerify)
        {
            try
            {
                Thread.Sleep(1000);
                String ActValue = GetAttributeValue("value");
                DlkAssert.AssertEqual("VerifyText()", TextToVerify, ActValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                Initialize();
                if (bool.TryParse(strExpectedValue, out bool result))
                {
                    bool bIsReadOnly = false;
                    if (mElement.GetAttribute("class").Contains("x-item-disabled"))
                    {
                        bIsReadOnly = true;
                    }
                    else if (mElement.FindElements(By.XPath("./ancestor::table[contains(@class, 'x-field ')]")).Any())
                    {
                        IWebElement parentField = mElement.FindElements(By.XPath("./ancestor::table[contains(@class, 'x-field ')]")).First();
                        bIsReadOnly = parentField.GetAttribute("class").Contains("x-item-disabled");
                    }
                    DlkAssert.AssertEqual("Verify read-only state for textbox: ", result, bIsReadOnly);
                }
                else
                {
                    throw new Exception("VerifyReadOnly() failed : value can only be True or False");
                }
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        /// <summary>
        /// Set Text box value
        /// </summary>
        /// <param name="TextToEnter">Text to set</param>
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                DlkSFTCommon.WaitForScreenToLoad();
                DlkSFTCommon.WaitForSpinner();
                Initialize();
                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    var classattr = mElement.GetAttribute("class");
                    if (classattr.Contains("x-form-text") || classattr.Contains("inputField"))
                    {
                        //setting value for SFT v2
                        mElement.SendKeys(Keys.Control + "a");
                        mElement.Clear();
                        mElement.SendKeys(TextToEnter);
                    }
                    else
                    {
                        //setting value for SFT v1
                        mElement.ExecJS("arguments[0].value = '" + TextToEnter + "'");
                    }
                }
                DlkLogger.LogInfo("Successfully executed Set()");
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
    }
}
