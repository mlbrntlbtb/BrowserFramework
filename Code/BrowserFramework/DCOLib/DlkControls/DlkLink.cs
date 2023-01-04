using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace DCOLib.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : DlkBaseControl
    {
        
        public DlkLink(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLink(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();        
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Click(1.5);
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
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


    }
}
