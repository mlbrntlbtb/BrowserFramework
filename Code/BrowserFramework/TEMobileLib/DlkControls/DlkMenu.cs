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
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        private String mstrMenuHeadDesc = "//span[@class='wMnuHead']";
        private String mstrMenuClass = "tlbrDDMenuDiv";
        private List<DlkBaseControl> mlstMenu;

        public DlkMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            mlstMenu = new List<DlkBaseControl>();    
            FindElement();
        }

        private void FindMenu(string pStrMenuDesc)
        {
            mlstMenu = new List<DlkBaseControl>();
            mlstMenu.Clear();
            IList<IWebElement> plstMenuCaptions = null;

            plstMenuCaptions = mElement.FindElements(By.XPath(pStrMenuDesc)).Where(elem => elem.Displayed).ToList();
            foreach (IWebElement pMenuCaption in plstMenuCaptions)
            {
                mlstMenu.Add(new DlkBaseControl("Menu", pMenuCaption));
            }
        }


        private void ErrorMessage(bool blnFound, string strActual, string menuItem)
        {
            if (blnFound)
            {
                DlkLogger.LogInfo("Successfully executed SelectMenu(). Control : " + mControlName + " : " + menuItem);
            }
            else
            {
                throw new Exception ("SelectMenu() failed. Control : " + mControlName + " : '" + menuItem + "' path not found. : Actual Menu = " + strActual);
            }
        }

        [Keyword("SelectMenu", new String[] { "1|text|MenuPath|File~Execute" })]
        public void SelectMenu(String sMenuPath)
        {
            bool bFound = false;
            String strInitial = "";

            String[] menuItems = sMenuPath.Split('~');

            try
            {
                if (menuItems.Length <= 3)
                {
                    Initialize();
                    for (int i = 0; i < menuItems.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                FindMenu(mstrMenuHeadDesc);
                                break;
                            case 1:
                                FindMenu("//div[@class='wMnuPick' and contains(@id, '" + strInitial + "_')]");
                                break;

                            case 2:
                                bFound = false;
                                if (mElement.FindElements(By.XPath("//*[@class='" + mstrMenuClass + "']")).Count > 0)
                                {
                                    foreach (IWebElement itm in mElement.FindElements(By.XPath("//*[@class='" + mstrMenuClass + "']")))
                                    {
                                        if (itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
                                            + menuItems[2] + "')]")).Count > 0)
                                        {
                                            IWebElement itmTbrMenuItem = itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
                                                + menuItems[2] + "')]")).First();
                                            DlkBaseControl ctlMenuItem = new DlkBaseControl("ToolbarMenuItem", itmTbrMenuItem);
                                            new DlkBaseControl("ToolbarMenuItem", ctlMenuItem.GetParent()).Click();
                                            bFound = true;
                                            break;
                                        }
                                    }
                                    if (!bFound)
                                    {
                                        throw new Exception("Menu item '" + menuItems[2] + "' not found");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Menu for '" + menuItems.ToString() + "' not found");
                                }

                                break;
                        }

                        if (i < 2)
                        {
                            bFound = false;
                            foreach (DlkBaseControl mMenu in mlstMenu)
                            {
                                if (mMenu.GetValue().ToLower().Contains(menuItems[i].ToLower()))
                                {
                                    if (i == 0)
                                    {
                                        strInitial = mMenu.GetAttributeValue("id").Substring(2, 1);
                                    }
                                    mMenu.ScrollIntoViewUsingJavaScript();
                                    mMenu.Click(2);
                                    bFound = true;
                                    break;
                                }
                            }
                            if (!bFound)
                            {
                                throw new Exception("Menu item '" + menuItems[i] + "' not found");
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("SelectMenu() only supports up to three menu paths");
                }
                DlkLogger.LogInfo("Successfully executed SelectMenu()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectMenu failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyMenu", new String[] { "1|text|MenuPath|File~Execute", "2|text|Expected Value|True or False" })]
        public void VerifyMenu(String sMenuPath, String bExpectedValue)
        {
            bool bFound = false;
            String strActualMenu = "";
            String[] menuItems = sMenuPath.Split('~');

            try
            {
                if (menuItems.Length <= 3)
                {
                    Initialize();
                    Thread.Sleep(2000);
                    for (int i = 0; i < menuItems.Length; i++)
                    {

                        strActualMenu = "";
                        switch (i)
                        {
                            case 0:
                                FindMenu(mstrMenuHeadDesc);
                                break;
                            case 1:
                                string strInitial = (menuItems[i - 1].ToUpper()).Substring(0, 1) + "_";
                                FindMenu("//div[@class='wMnuPick' and contains(@id, '" + strInitial + "_')]");
                                break;

                            case 2:
                                if (!bFound)
                                {
                                    break;
                                }
                                bFound = false;
                                if (mElement.FindElements(By.XPath("//*[@class='" + mstrMenuClass + "']")).Count > 0)
                                {
                                    foreach (IWebElement itm in mElement.FindElements(By.XPath("//*[@class='" + mstrMenuClass + "']")))
                                    {
                                        if (itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
                                            + menuItems[2] + "')]")).Count > 0)
                                        {
                                            bFound = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("Menu for '" + menuItems.ToString() + "' not found");
                                }

                                break;
                        }

                        if (i < 2)
                        {
                            bFound = false;
                            foreach (DlkBaseControl mMenu in mlstMenu)
                            {

                                int intPos = mMenu.GetValue().IndexOf("\r\n");
                                if (intPos < 0)
                                {
                                    strActualMenu = strActualMenu + "\r\n" + mMenu.GetValue();
                                }
                                else
                                {
                                    string strMenuLabel = mMenu.GetValue().Substring(0, intPos);
                                    strActualMenu = strActualMenu + "\r\n" + strMenuLabel;
                                }
                                if (mMenu.GetValue().ToLower().Contains(menuItems[i].ToLower()))
                                {
                                    if (i + 1 < menuItems.Length)
                                    {
                                        mMenu.Click();
                                    }
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                    }

                    DlkAssert.AssertEqual("VerifyMenu()", Convert.ToBoolean(bExpectedValue), bFound);

                    //click the header again to close menu dropdown
                    FindMenu(mstrMenuHeadDesc);
                    foreach (DlkBaseControl mMenu in mlstMenu)
                    {
                        if (mMenu.GetValue().ToLower().Contains(menuItems[0].ToLower()))
                        {
                            mMenu.Click();
                        }
                    }
                    DlkLogger.LogInfo("VerifyMenu() passed");
                }
                else
                {
                    throw new Exception("VerifyMenu() only supports up to three menu paths");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenu() failed : " + e.Message, e);
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

    }
}
