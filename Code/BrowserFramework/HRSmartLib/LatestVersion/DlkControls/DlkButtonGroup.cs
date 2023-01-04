using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("ButtonGroup")]
    public class DlkButtonGroup : DlkBaseControl
    {
        #region Declarations

        private const string MENU_ITEMS_XPATH = @"../ul[@role='menu']/li/a";

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public DlkButtonGroup(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        public DlkButtonGroup(string ControlName, IWebElement ExistingElement) : 
            base (ControlName, ExistingElement)
        {
            
        }

        #endregion

        #region Keywords

        [Keyword("SelectByTitle")]
        public void SelectByTitle(string Title)
        {
            try
            {
                ClickUsingJavaScript();
                bool isMenuItemFound = false;
                IList<IWebElement> menuList = mElement.FindElements(By.XPath(MENU_ITEMS_XPATH));
                foreach (IWebElement menuItem in menuList)
                {
                    DlkBaseControl menuItemControl = new DlkBaseControl("Menu_Item_Control : " + menuItem.Text, menuItem);
                    if (menuItemControl.GetValue().Trim().Equals(Title) ||
                        menuItemControl.GetAttributeValue("data-original-title") != null &&
                        menuItemControl.GetAttributeValue("data-original-title").Equals(Title))
                    {
                        menuItemControl.Click();
                        DlkLogger.LogInfo("SelectByTitle( ) successfully executed.");
                        isMenuItemFound = true;
                        break;
                    }
                }

                if (!isMenuItemFound)
                {
                    throw new Exception("SelectByTitle( ) menu item " + Title + " not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectByTitle( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("SelectByIndex")]
        public void SelectByIndex(string Index)
        {
            try
            {
                ClickUsingJavaScript();
                int zeroBaseIndex = Convert.ToInt32(Index) - 1;
                IList<IWebElement> menuList = mElement.FindElements(By.XPath(MENU_ITEMS_XPATH));

                if (zeroBaseIndex < menuList.Count)
                {
                    IWebElement menuItem = menuList[zeroBaseIndex];
                    DlkBaseControl menuItemControl = new DlkBaseControl("Menu_Item_Control : " + menuItem.Text, menuItem);
                    menuItemControl.Click();
                    DlkLogger.LogInfo("SelectByIndex( ) successfully executed.");
                }
                else
                {
                    throw new Exception("SelectByIndex( ) menu item index " + Index + " not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectByIndex( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string ItemValue)
        {
            try
            {
                bool itemFound = false;
                IList<IWebElement> menuList = mElement.FindElements(By.XPath(MENU_ITEMS_XPATH));
                foreach (IWebElement menuItem in menuList)
                {
                    DlkBaseControl menuItemControl = new DlkBaseControl("Menu_Item_Control : " + menuItem.Text, menuItem);
                    if (menuItemControl.GetValue().Contains(ItemValue))
                    {
                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                {
                    throw new Exception("Item : " + ItemValue + "not found.");
                }

                DlkLogger.LogInfo("VerifyItemExists() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyItemExists() execution failed. " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}