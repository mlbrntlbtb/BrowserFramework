using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using ProjectInformationManagementLib.System;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        #region Declarations

        private bool _iframeSearchType = false;
        private List<IWebElement> mItems;

        #endregion

        public DlkMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        /// <summary>
        /// Always call this for every keyword
        /// </summary>
        public void Initialize()
        {
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                FindElement();
                DlkEnvironment.mSwitchediFrame = true;
                GetItems();
            }
            else
            {
                _iframeSearchType = false;
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                GetItems();
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        #region KEYWORDS

        [Keyword("MainMenuSelectByRowId", new String[] { "1|text|RowId|1" })]
        public void MainMenuSelectByRowId(string RowId)
        {
            try
            {
                //DlkEnvironment.AutoDriver.Title;

                bool bFound = false;
                string xpath = string.Format("//div/ul[@id='lbMenu']/li[@rowid='{0}']", RowId);
                //Initialize();

                DlkBaseControl item = new DlkBaseControl("MenuItem", "xpath", xpath);
                if (item != null)
                {
                    item.Click();
                    bFound = true;
                }

                if (!bFound)
                {
                    throw new Exception("Menu row id '" + RowId + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("MainMenuSelectByRowId() failed : " + e.Message, e);
            }
        }

        [Keyword("MainMenuSelect", new String[] { "1|text|Item|Activity" })]
        public void MainMenuSelect(string Item)
        {
            try
            {
                //DlkEnvironment.AutoDriver.Title;
                
                bool bFound = false;
                string xpath = string.Format("//div/ul[@id='lbMenu']//div[@title='{0}']", Item);
                //Initialize();

                DlkBaseControl item = new DlkBaseControl("MenuItem", "xpath", xpath);
                if(item != null)
                {
                    item.Click();
                    bFound = true;
                }

                if (!bFound)
                {
                    throw new Exception("Menu item '" + Item + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("MainMenuSelect() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().Trim().ToLower() == Item.Trim().ToLower())
                    {
                        menuItem.Click(4.5);
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Menu item '" + Item + "' not found");
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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
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

        #endregion

        #region METHODS

        private void GetItems()
        {
            try
            {
                var mitemList = this.mElement.FindElements(By.XPath(".//ul/li//div[@class='label']"));
                List<IWebElement> visibleMenus = new List<IWebElement>();
                foreach (IWebElement item in mitemList)
                {
                    if (item.Displayed)
                    {
                        visibleMenus.Add(item);
                    }
                }
                mItems = visibleMenus;
            }
            catch
            {
                //do nothing
            }
        }

        #endregion
    }
}
