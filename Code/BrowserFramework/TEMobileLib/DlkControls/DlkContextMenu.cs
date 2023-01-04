using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
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
            bool bFound = false;
            try
            {
                Initialize();
                IList<IWebElement> menuitems = mElement.FindElements(By.ClassName("tlbrDDActionDiv"));
                foreach (IWebElement menuItem in menuitems)
                {
                    if (menuItem.Text == MenuItem)
                    {
                        DlkLogger.LogInfo("Successfully executed Select() : " + menuItem.Text + " = " + MenuItem);
                        menuItem.Click();
                        
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Select() failed: Menu item '" + MenuItem + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
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

        private void FindMenuItems()
        {
            
        }
    }
}
