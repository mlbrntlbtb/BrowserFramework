using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using TEMobileLib.System;
using TEMobileLib.DlkControls;

namespace TEMobileLib.DlkFunctions
{
    [Component("OpenApplications")]
    public static class DlkOpenApplications
    {
        [Keyword("Close", new String[] {"1|text|Application|Manage Expense Reports"})]
        public static void Close(String Application)
        {
            try
            {
                DlkBaseControl button = new DlkBaseControl("Close Button", "XPATH", "//*[@id='img__" + Application + "']/parent::div/following-sibling::div//span[@title='Close Application']");

                if (button.Exists())
                {
                    button.Click();
                }
                else
                {
                    throw new Exception("Application ID '" + Application + "' not found.");
                }

                DlkLogger.LogInfo("Successfully executed Close()");
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }
        }

        [Keyword("CollapseOrExpandTreeView", new String[] { "1|text|Application ID|EPMEXPAUTH" })]
        public static void CollapseOrExpandTreeView(String Application)
        {
            CollapseOrExpand(Application);
        }

        [Keyword("SwitchToApplication", new String[] { "1|text|Application|EPMEXPAUTHRPT or EPMEXPAUTHRPT~Planned Expenses" })]
        public static void SwitchToApplication(String ApplicationName)
        {
            try
            {
                DlkBaseControl tab = null;
                String[] arrInputString = null;

                // Click on children nodes under treeview (i.e EPMEXPAUTHRPT~Planned Expenses)
                if (ApplicationName.Contains('~'))
                {
                    arrInputString = ApplicationName.Split('~');

                    IWebElement treeview = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@id='img__" + arrInputString[0] + "']"));
                    if(treeview.GetAttribute("class").Equals("mnuGrPlusS")) //view is collapsed
                    {
                        CollapseOrExpand(arrInputString[0]);
                    }

                    var container = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@id='cnt__" + arrInputString[0] + "']//div"));
                    foreach(var cntTab in container)
                    {
                        if(cntTab.Text.Equals(arrInputString[1]))
                        {
                            tab = new DlkBaseControl("Application Tab", cntTab);
                        }
                    }
                }
                else
                {
                    tab = new DlkBaseControl("Application Tab", "XPATH", "//*[@id='img__" + ApplicationName + "']/following-sibling::span");
                    if(!tab.Exists())
                    {
                        throw new Exception("Application Tab '" + ApplicationName + "' not found.");
                    }
                }

                tab.Click();
                DlkLogger.LogInfo("Successfully executed SwitchToApplication()");
            }
            catch (Exception e)
            {
                throw new Exception("SwitchToApplication() failed : " + e.Message, e);
            }
        }

        #region PUBLIC METHODS

        public static void CollapseOrExpand(String Application)
        {
            try
            {
                DlkBaseControl button = new DlkBaseControl("Expand/Collapse Button", "XPATH", "//*[@id='img__" + Application + "']");

                if (button.Exists())
                {
                    button.Click();
                }
                else
                {
                    throw new Exception("Application ID '" + Application + "' not found.");
                }

                DlkLogger.LogInfo("Successfully executed CollapseOrExpandTreeView()");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseOrExpandTreeView() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
