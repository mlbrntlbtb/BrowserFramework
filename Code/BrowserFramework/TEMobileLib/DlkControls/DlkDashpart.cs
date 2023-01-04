using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMobileLib.DlkControls
{
    [ControlType("Dashpart")]
    class DlkDashpart : DlkBaseControl
    {
        public DlkDashpart(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkDashpart(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDashpart(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("VerifyExists")]
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
        
        [Keyword("ClickDashpartButton")]
        public void ClickDashpartButton(String ButtonTitle)
        {
            try
            {
                var button = GetDashpartButton(ButtonTitle);
                if (button == null) throw new Exception($"cannot find button with title [{ButtonTitle}]");

                button.Click();

                DlkLogger.LogInfo("ClickDashpartButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickDashpartButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartButtonExists")]
        public void VerifyDashpartButtonExists(String ButtonTitle, String ExpectedValue)
        {
            try
            {
                var button = GetDashpartButton(ButtonTitle);
                var ActialValue = button != null;

                DlkAssert.AssertEqual("VerifyDashpartButtonExists()", Convert.ToBoolean(ExpectedValue), ActialValue);
                DlkLogger.LogInfo("VerifyDashpartButtonExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartButtonExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDashpartTitle")]
        public void VerifyDashpartTitle(String ExpectedTitle)
        {
            try
            {
                Initialize();
                var dpTitle = mElement.FindElements(By.XPath("./*[@id='rptHdr']/*[@id='rptHrdTtl']")).FirstOrDefault();
                if (dpTitle == null) throw new Exception("No Dashpart title found.");

                var ActualTitle = dpTitle.Text.Trim();

                DlkAssert.AssertEqual("VerifyDashpartTitle()", ExpectedTitle.Trim(), ActualTitle);
                DlkLogger.LogInfo("VerifyDashpartTitle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDashpartTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        private IWebElement GetDashpartButton(String ButtonTitle)
        {
            Initialize();

            var buttons = mElement.FindElements(By.XPath("./*[@id='rptHdr']/*[@id='dTbr']/*[contains(@class, 'dTbBtn')]")).ToList();
            if (buttons.Count < 1) throw new Exception("No Dashpart button found.");

            var button = buttons.FirstOrDefault(b => b.GetAttribute("title").Trim() == ButtonTitle.Trim());

            return button;
        }


    }
}
