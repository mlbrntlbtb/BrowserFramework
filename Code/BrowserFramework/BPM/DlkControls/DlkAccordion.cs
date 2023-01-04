using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using BPMLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMLib.DlkControls
{
    [ControlType("Accordion")]
    public class DlkAccordion : DlkBaseControl
    {
        public DlkAccordion(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkAccordion(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkAccordion(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

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

        [Keyword("Select", new String[] { "1|text|Name|Accordion1" })]
        public void Select(String Name)
        {
            try
            {
                Initialize();

                string attrValue = string.Format("Expand {0}",Name);

                IWebElement target = mElement.FindElements(By.TagName("a")).FirstOrDefault(e => e.GetAttribute("title") == attrValue);

                if (target != null)
                {
                    target.Click();
                    DlkLogger.LogInfo("Successfully executed Select() : " + Name);
                }
                else
                {
                    throw new Exception("Accordion control: " + Name + " not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
    }
}
