using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("NavigationPath")]
    public class DlkNavigationPath : DlkBaseControl
    {
        public DlkNavigationPath(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavigationPath(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("VerifyMenuPath", new String[] { "1|text|Expected Value|Front Office>Administration>Users & Groups>Security Roles" })]
        public void VerifyMenuPath(String ExpectedValue)
        {
            try
            {
                string ActValue = DlkEnvironment.mBrowser.ToLower() == "safari" 
                    ? GetAttributeValue("innerText") : GetValue();

                DlkAssert.AssertEqual("VerifyMenuPath()", ExpectedValue, ActValue);
                DlkLogger.LogInfo("VerifyMenuPath() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenuPath() failed : " + e.Message, e);
            }
        }
    }
}
