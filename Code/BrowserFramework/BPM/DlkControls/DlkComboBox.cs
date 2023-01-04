using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using BPMLib.DlkControls;
using BPMLib.DlkUtility;

namespace BPMLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
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

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                bool bFound = false;

                Initialize();
                mElement.Click();

                IReadOnlyCollection<IWebElement> ddList = mElement.FindElements(By.XPath(".//option"));
                foreach (IWebElement item in ddList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("List", item);
                    string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                    if (val.Equals(Value.Trim()))
                    {
                        bFound = true;
                        ctl.Click();
                        DlkLogger.LogInfo("Item [" + Value + "]found. Clicking...");
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Unable to find item in the list.");
                }

                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
    }
}
