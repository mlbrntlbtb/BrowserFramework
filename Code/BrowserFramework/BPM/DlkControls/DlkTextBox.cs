using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using BPMLib.DlkControls;

namespace BPMLib.DlkControls
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
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
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

        /// <summary>
        /// Set Text box value
        /// </summary>
        /// <param name="TextToEnter">Text to set</param>
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                mElement.Clear();
                mElement.Click();
                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    if (DateTime.TryParse(TextToEnter, out DateTime res))
                    {
                        TextToEnter.ToCharArray().ToList().ForEach(c =>
                        {
                            mElement.SendKeys(c.ToString());
                            System.Threading.Thread.Sleep(100);
                        });
                    }
                    else
                    {
                        mElement.SendKeys(TextToEnter);
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
