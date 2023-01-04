using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("DropDownMenu")]
    public class DlkDropDownMenu : DlkBaseControl
    {
        #region Constructors

        public DlkDropDownMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords
        
        /// <summary>
        /// Will click the selected (Value parameter) drop down menu item.
        /// </summary>
        /// <param name="Value">will search using XPath contains text function.</param>
        [Keyword("Select")]
        public void Select(string Value)
        {
            try
            {
                //Click the menu drop down.
                Click(3);
                IWebElement selectedElement = null;

                if (mElement.TagName.Equals("button"))
                {
                    selectedElement = FindElementInControls(@"//*[@id='search-select']/li/a", Value);
                }
                else
                {
                    selectedElement = FindElementInControls(@"//li[contains(@class,'open')]//a[@role='menuitem']", Value);
                }
                //Click the selected item in the drop down menu.
                DlkLink link = new DlkLink("SelectedMenuItem", selectedElement);
                //Simple IWebElement.Click() not working on chrome.
                link.ClickUsingJavaScript();

                if (DlkEnvironment.mBrowser.ToLower().Equals("ie"))
                {
                    //IE not responding well in using the control after clicking we need to delay.
                    Thread.Sleep(3000);
                }

                DlkLogger.LogInfo("Select( ) passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Select( ) failed " + ex.Message, ex);
            }
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                mElement.Click();
                DlkLogger.LogInfo("Click( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Click( ) failed " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Methods

        private void Initialize()
        {
            FindElement();
        }

        private IWebElement FindElementInControls(string xpathSearchKey, string searchKey)
        {
            IList<IWebElement> controls = mElement.FindElements(By.XPath(xpathSearchKey));

            foreach (IWebElement element in controls)
            {
                if (element.Text.Equals(searchKey))
                {
                    return element;
                }
            }

            return null;
        }

        #endregion
    }
}
