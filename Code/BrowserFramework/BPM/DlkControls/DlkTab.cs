using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using BPMLib.DlkControls;
using BPMLib.DlkUtility;

namespace BPMLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement(1);
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("SelectTab")]
        public void SelectTab()
        {
            try
            {
                Initialize();
                mElement.Click();
                DlkBPMCommon.WaitForSpinner();
                DlkLogger.LogInfo("Successfully executed SelectTab()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectTab() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkBPMCommon.WaitForSpinner();
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
