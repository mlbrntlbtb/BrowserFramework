using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using TM1WebLib.DlkUtility;
using TM1WebLib.DlkControls;

namespace TM1WebLib.DlkControls
{
    [ControlType("ContextMenu")]
    public class DlkContextMenu : DlkBaseControl
    {
        public DlkContextMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkContextMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected value|TRUE" })]
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

        [Keyword("Select", new String[] { "1|text|Menu Item Caption|MenuItem1" })]
        public void Select(String MenuItem)
        {
            try
            {
                Initialize();
                IWebElement contextMenuItem = mElement.FindElement(By.XPath("/descendant::td[text()='" + MenuItem + "' and(ancestor::table[contains(@class,'dijitMenuActive')])]"));
                contextMenuItem.Click();

                if (MenuItem == "Copy")
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        private void FindMenuItems()
        {

        }
    }
}
