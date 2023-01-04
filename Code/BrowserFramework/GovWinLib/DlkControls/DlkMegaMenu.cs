using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;


using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("MegaMenu")]
    public class DlkMegaMenu : DlkBaseControl
    {
        private List<DlkBaseControl> mlstLinks;

        public DlkMegaMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMegaMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMegaMenu(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
        {
            mlstLinks = new List<DlkBaseControl>();
            FindElement();
        }

        [Keyword("Select", new String[] { "1|text|Menu Path|My GovWin IQ~My Task Orders"})]
        public void Select(String MenuPath)
        {
            String[] MenuItems = MenuPath.Split('~');

            Initialize();
            for (int i = 0; i < MenuItems.Count(); i++)
            {
                try
                {
                    if (i > 0 && MenuItems.Count() == 3)
                    {
                        DlkLink ctlMenuItem = new DlkLink(MenuItems[1] + "~" + MenuItems[2], "XPATH", "//li/h2/a[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"+ MenuItems[1].ToLower() +"']/../../..//a[text()='"+ MenuItems[2] +"']");
                        ctlMenuItem.Click();
                        Thread.Sleep(500);
                        break;
                    }
                    else
                    {
                        //DlkLink ctlMenuItem = new DlkLink(MenuItems[i], "LINKTEXT", MenuItems[i]);
                        OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                        IWebElement ctlMenuItem = mElement.FindElement(By.LinkText(MenuItems[i]));
                        mAction.MoveToElement(ctlMenuItem).MoveByOffset(5, 5).ClickAndHold().Perform();
                        Thread.Sleep(500);
                        mAction.Release().Perform();
                        //DlkBaseControlctl = new DlkBaseControl("control", ctlMenuItem);
                        //ctl.MouseOverOffset(5, 5);
                        //ctl.Click();
                    }
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    throw new Exception("Select() failed. Menu Path '" + MenuPath + "' not found in MegaMenu.");
                }
            }
            DlkLogger.LogInfo("Successfully executed Select().");
        }

        [Keyword("VerifyList", new String[] { "1|text|Menu Tab|My Network", "1|text|Sub Menu|My Dashboard~My Opportunities~My Saved Searches" })]
        public void VerifyList(String MenuTab, String SubMenus)
        {
            String strMenuTab = MenuTab;
            String MenuItems = SubMenus;
            bool actual = false;
            //String[] MenuItems = inputs[1].Split('~');
            
            Initialize();

            IList<IWebElement> MenuItemsActual = mElement.FindElements(By.XPath(".//a[contains(.,'" + strMenuTab + "')]//following-sibling::ul//a"));
            
            String strMenuItemsActual = "";
            int index = 0;
            
            foreach (IWebElement elem in MenuItemsActual)
            {
                strMenuItemsActual = elem.Text;

                if (index < MenuItemsActual.Count)
                    strMenuItemsActual += "~";

                index++;
            }

            if (strMenuItemsActual == MenuItems)
                actual = true;

            DlkAssert.AssertEqual("VerifyList()", true, actual);
            DlkLogger.LogInfo("Successfully executed Select().");
        }
        
    }
}

