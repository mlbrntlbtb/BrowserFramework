using System;
using System.Linq;
using System.Threading;
using AjeraLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace AjeraLib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;

        #endregion

        #region PROPERTIES
        #endregion

        #region CONSTRUCTORS
        
        public DlkMenu(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkMenu(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public DlkMenu(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkMenu(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public DlkMenu(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }


        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        #endregion

        #region KEYWORDS

        [Keyword("SelectMenuItem", new String[] { "Manage | Purchase Orders" })]
        public void SelectMenuItem(String MenuHeader, String MenuItem)
        {
            bool bFound = false;
            String strActualMenus = "";

            try
            {
                Initialize();
                var menuItemList = mElement.FindElements(
                    By.XPath(".//li[contains(@class,'menu-root') and text()='"+ MenuHeader +"']/ancestor::ul[1]//li[contains(@class,'menu-leaf') and text()='"+ MenuItem +"']"));

                foreach (var item in menuItemList.Where(i => i.Text != "" && i.Displayed))
                {
                    strActualMenus = strActualMenus + "-" + item.Text;
                    if (item.Text.ToLower().Equals(MenuItem.ToLower()))
                    {
                        bFound = true;
                        item.Click();
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("SelectMenuItem() failed. Control : " + mControlName + " : '" + MenuItem +
                                        "' not found in list. : Actual List = " + strActualMenus);
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectMenuItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExistsMenuItem", new String[] { "Manage | Purchase Orders" })]
        public void VerifyExistsMenuItem(String MenuHeader, String MenuItem)
        {
            bool bFound = false;
            String strActualMenus = "";

            try
            {
                Initialize();
                var menuItemList = mElement.FindElements(
                     By.XPath(".//li[contains(@class,'menu-root') and text()='" + MenuHeader + "']/ancestor::ul[1]//li[contains(@class,'menu-leaf') and text()='" + MenuItem + "']"));


                foreach (var item in menuItemList.Where(i => i.Text != "" && i.Displayed))
                {
                    strActualMenus = strActualMenus + "-" + item.Text;
                    if (item.Text.ToLower().Equals(MenuItem.ToLower()))
                    {
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyExistsMenuItem() failed. Control : " + mControlName + " : '" + MenuItem +
                                        "' not found in list. : Actual List = " + strActualMenus);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsMenuItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExistsMenuHeader", new String[] { "Manage" })]
        public void VerifyExistsMenuHeader(String MenuHeader)
        {
            bool bFound = false;
            String strActualMenus = "";

            try
            {
                Initialize();
                var menuHeaderList = mElement.FindElements(
                    By.XPath(".//li[contains(@class,'menu-root') and text()='" + MenuHeader +"']"));

                foreach (var header in menuHeaderList.Where(i => i.Text != ""))
                {
                    strActualMenus = strActualMenus + "-" + header.Text;
                    if (header.Text.ToLower().Equals(MenuHeader.ToLower()))
                    {
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyExistsMenuHeader() failed. Control : " + mControlName + " : '" + MenuHeader +
                                        "' not found in list. : Actual List = " + strActualMenus);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsMenuHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("ExpandMenuBranch", new String[] { "Setup|Utility" })]
        public void ExpandMenuBranch(String MenuHeader, String MenuBranch)
        {
            try
            {
                Initialize();
                var menuBranch = mElement.FindElement(
                    By.XPath(".//li[contains(@class,'menu-root') and text()='"+ MenuHeader +"']/ancestor::ul[1]//li[contains(@class,'menu-branch') and text()='"+ MenuBranch +"']"));
                
                //not yet expanded
                if (menuBranch != null && !menuBranch.GetAttribute("class").ToLower().Contains("expand"))
                {
                    menuBranch.Click();
                }
                else
                {
                    throw new Exception("ExpandMenuBranch() failed. Control : " + mControlName + " : '" + MenuBranch +
                                           "' cannot be found or is already expanded.");
                }
            }

            catch (Exception e)
            {
                throw new Exception("ExpandMenuBranch() failed : " + e.Message, e);
            }
        }

        [Keyword("CollapseMenuBranch", new String[] { "Setup|Utility" })]
        public void CollapseMenuBranch(String MenuHeader, String MenuBranch)
        {
            try
            {
                Initialize();
                var menuBranch = mElement.FindElement(
                    By.XPath(".//li[contains(@class,'menu-root') and text()='" + MenuHeader + "']/ancestor::ul[1]//li[contains(@class,'menu-branch') and text()='" + MenuBranch + "']"));

                //already expanded
                if (menuBranch != null && menuBranch.GetAttribute("class").ToLower().Contains("expand"))
                {
                    menuBranch.Click();
                }
                else
                {
                    throw new Exception("CollapseMenuBranch() failed. Control : " + mControlName + " : '" + MenuBranch +
                                           "' cannot be found or is already collapsed.");
                }
            }

            catch (Exception e)
            {
                throw new Exception("CollapseMenuBranch() failed : " + e.Message, e);
            }
        }

        #endregion

        #region METHODS
        #endregion

    }
}
