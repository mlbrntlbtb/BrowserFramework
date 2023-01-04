using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using Controls = HRSmartLib.LatestVersion.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using OpenQA.Selenium;
using System.Threading;

namespace HRSmartLib.LatestVersion.DlkFunctions
{
    [Component("Core_Menu")]
    public static class DlkCoreMenu
    {
        #region Declarations

        private const string PARENTMENU = "//div[contains(@class,'collapse')]/ul[contains(@class,'navmenu')]";
        private const string MAINMENU = "//li[contains(@class,'mainmenu-link') and not(contains(@style,'none'))]//span[@class='mainmenulabel']";
        private const string SUBMENUS = "./following-sibling::div[contains(@class,'collapse') and contains(@style,'block')]";

        #endregion 

        #region Properties
        private static DlkDynamicObjectStoreHandler _dynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        private static string _screenName
        {
            get { return "Core_Menu"; }
        }
        private static string _careerCenter
        {
            get { return "Career Center"; }
        }

        private static Controls.DlkDropDownMenu _coreMenu
        {
            get
            {
                DlkObjectStoreFileControlRecord control = _dynamicObjectStoreHandler.GetControlRecord(_screenName, _careerCenter);
                return new Controls.DlkDropDownMenu(control.mKey, control.mSearchMethod, control.mSearchParameters.Split('~'));
            }
        }

        #endregion
        
        #region Navigate Methods

        [Keyword("NavigateToCCCompetencies")]
        public static void NavigateToCCCompetencies()
        {
            _coreMenu.Select("My Résumés~Competencies");
        }


        [Keyword("Select")]
        public static void Select(string Menu, string SubMenu, string Header)
        {
            try
            {
                //get menu
                IWebElement selectedMenu = getMenuByName(Menu);
                IList<IWebElement> submenus = null;

                if (selectedMenu == null)
                {
                    throw new Exception(string.Format("Menu : {0} not found.", Menu));
                }
                else
                {
                    IWebElement subMenuBlock = selectedMenu.FindElement(By.XPath("./following-sibling::div"));
                    string subMenuBlockClassAttri = string.IsNullOrEmpty(subMenuBlock.GetAttribute("style")) ? string.Empty : subMenuBlock.GetAttribute("style");

                    //Click Selected Menu if the sub menu is not displayed yet.
                    if (subMenuBlockClassAttri.Contains("none") ||
                        subMenuBlockClassAttri.Equals(string.Empty))
                    {
                        selectedMenu.Click();
                        Thread.Sleep(500);
                        DlkLogger.LogInfo("Menu : " + Menu + " clicked successfully.");
                    }

                    submenus = selectedMenu.FindElements(By.XPath(SUBMENUS));
                    //check if sub menu exist. this should exist for all menus.
                    if (submenus.Count > 0)
                    {
                        string[] param = SubMenu.Split('~');
                        List<string> subMenuList = param.ToList();
                        int iterationCount = 0;
                        IWebElement parentElement = submenus[0];

                        foreach (string subMenu in subMenuList)
                        {
                            iterationCount++;
                            IList<IWebElement> subMenuElementList = DlkCommon.DlkCommonFunction.GetElementWithText(subMenu, parentElement, false, "*", true);
                            if (subMenuElementList.Count > 0)
                            {
                                string subMenuClassAttribute = subMenuElementList[0].GetAttribute("class");
                                
                                //check if the sub menu was already collapsed.
                                if (!subMenuClassAttribute.Contains("active"))
                                {
                                    subMenuElementList[0].Click();
                                    DlkLogger.LogInfo("Sub Menu : " + subMenu + " clicked successfully.");

                                    //only sleep if the sub menu is a toggle.
                                    if (subMenuClassAttribute.Contains("dropdown-toggle") &&
                                        subMenuList.Count > 1)
                                    {
                                        Thread.Sleep(500);
                                        parentElement = subMenuElementList[0].FindElement(By.XPath("./.."));
                                        subMenuElementList = DlkCommon.DlkCommonFunction.GetElementWithText(subMenuList[iterationCount], parentElement, false, "*", true);

                                        IWebElement divElement = null;
                                        if (subMenuElementList.Count > 0)
                                        {
                                            divElement = subMenuElementList[0].FindElement(By.XPath("./../.."));
                                        }

                                        //sleep until style is blank
                                        //initially there is no style attribute then upon toggle style will appear with Display:none or blank.
                                        string styleAttribute = "none";
                                        do
                                        {
                                            styleAttribute = divElement.GetAttribute("style");
                                            Thread.Sleep(100);
                                            DlkLogger.LogInfo("sleeping...");
                                        }
                                        while (subMenuElementList.Count > 0 && styleAttribute.Contains("none"));
                                    }
                                }
                                else
                                {
                                    //if Submenu contains the same title.
                                    if (iterationCount > 1 &&
                                        subMenuElementList.Count > 1)
                                    {
                                        subMenuElementList[1].Click();
                                        DlkLogger.LogInfo("Sub Menu : " + subMenu + " clicked successfully.");
                                    }
                                    else
                                    {
                                        DlkLogger.LogInfo("Sub Menu : " + subMenu + " already collapsed. skipping click.");
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Sub Menu : " + subMenu + " not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Sub menu not found for Menu : " + Menu);
                    }
                }

                DlkLogger.LogInfo("Returning to Main Menu.");
                //We are going back to main menu to reduce time checking more elements.
                IList<IWebElement> mainMenu = DlkEnvironment.AutoDriver.FindElements(By.XPath(MAINMENU));
                if (mainMenu.Count > 0 &&
                    mainMenu[0].Displayed)
                {
                    mainMenu[0].Click();
                }
                else
                {
                    if (Menu.ToLower().Contains("custom menu"))
                    {
                        DlkLogger.LogInfo("Skipping returning to Main Menu.");
                    }
                    else
                    {
                        throw new Exception("Main menu missing.");
                    }
                }

                IWebElement actualHeader = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h1"));
                Controls.DlkLabel headerControl = new Controls.DlkLabel("Header", actualHeader);
                headerControl.VerifyText(Header);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("MenuClick")]
        public static void MenuClick(string Menu)
        {
            try
            {
                //get menu
                IWebElement selectedMenu = getMenuByName(Menu);

                if (selectedMenu == null)
                {
                    throw new Exception(string.Format("Menu : {0} not found.", Menu));
                }
                else
                {
                    selectedMenu.Click();
                    DlkLogger.LogInfo("Menu : " + Menu + " clicked successfully.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifyMenuExists")]
        public static void VerifyMenuExists(string Menu, string TrueOrFalse)
        {
            try
            {
                //get menu
                IWebElement selectedMenu = getMenuByName(Menu);
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                if (selectedMenu != null)
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("Menu : " + Menu, expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyMenuExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifySubMenuExists")]
        public static void VerifySubMenuExists(string Menu, string SubMenu, string TrueOrFalse)
        {
            try
            {
                //get menu
                IWebElement selectedMenu = getMenuByName(Menu);
                StringBuilder actualMenuItemText = new StringBuilder();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                if (selectedMenu != null)
                {
                    List<IWebElement> subMenus = selectedMenu.FindElements(By.XPath("../div[@id]//span[@class='menuitem-text']"))
                                                             .Where(item=> item.Enabled).ToList();
                    foreach(IWebElement menuItem in subMenus)
                    {
                        CommonLib.DlkControls.DlkBaseControl menuItemControl = new CommonLib.DlkControls.DlkBaseControl("Menu Item", menuItem);
                        string actualText = menuItemControl.GetValue();
                        actualMenuItemText.AppendLine(actualText);

                        if (actualText.Equals(SubMenu))
                        {
                            actualResult = true;
                            break;
                        }
                    }
                    actualMenuItemText.Append("]").Replace("\r\n]", "]");
                }
                else
                {
                    actualResult = false;
                }

                DlkLogger.LogInfo("Actual Sub Menus for : "+ Menu + "\n[" + actualMenuItemText.ToString());
                DlkAssert.AssertEqual("Sub Menu : " + SubMenu, expectedResult, actualResult);
                DlkLogger.LogInfo("VerifySubMenuExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("VerifySubMenuToggleExists")]
        public static void VerifySubMenuToggleExists(string Menu, string SubMenuToggle, string TrueOrFalse)
        {
            try
            {
                //get menu
                IWebElement selectedMenu = getMenuByName(Menu);
                StringBuilder actualMenuItemText = new StringBuilder();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                if (selectedMenu != null)
                {
                    List<IWebElement> subMenus = selectedMenu.FindElements(By.XPath("../div[@id]//div[contains(@class,'dropdown-toggle')]"))
                                                             .Where(item => item.Enabled).ToList();
                    foreach (IWebElement menuItem in subMenus)
                    {
                        CommonLib.DlkControls.DlkBaseControl menuItemControl = new CommonLib.DlkControls.DlkBaseControl("Menu Item", menuItem);
                        string actualText = menuItemControl.GetValue().Replace("\r\n","").Trim();
                        actualMenuItemText.AppendLine(actualText);

                        if (actualText.Equals(SubMenuToggle))
                        {
                            actualResult = true;
                            break;
                        }
                    }

                    actualMenuItemText.Append("]").Replace("\r\n]", "]");
                }
                else
                {
                    actualResult = false;
                }

                DlkLogger.LogInfo("Actual Sub MenuToggle for : " + Menu + "\n[" + actualMenuItemText.ToString());
                DlkAssert.AssertEqual("Sub Menu Toggle : " + SubMenuToggle, expectedResult, actualResult);
                DlkLogger.LogInfo("VerifySubMenuExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IWebElement getMenuByName(string menuName)
        {
            IList<IWebElement> menuResult = DlkCommon.DlkCommonFunction.GetElementWithText(menuName, null, false, "h6", ignoreCasing: true);

            if (menuResult.Count > 0)
            {
                return menuResult[0];
            }
            else
            {
                IWebElement parentMenu = DlkEnvironment.AutoDriver.FindElement(By.XPath(PARENTMENU));
                menuResult = DlkCommon.DlkCommonFunction.GetElementWithText(menuName, parentMenu, false, "a");
                if (menuResult.Count > 0)
                {
                    return menuResult[0];
                }
            }

            return null;
        }

        #endregion
    }
}
