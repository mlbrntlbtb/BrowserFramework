 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class TE_BA : TestScript
    {
        public override bool TestExecute(out string ErrorMessage, string TestEnvironment)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
                /* Login */
				CPCommon.Login(TestEnvironment, out ErrorMessage);

                /* 1 - Navigate to Timesheets app */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
                    Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed)
                        new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Time & Expense", "xpath", "//div[@class='busItem'][.='Time & Expense']").Click();
                    new Control("Time", "xpath", "//div[@class='deptItem'][.='Time']").Click();
                    new Control("Timesheets", "xpath", "//div[@class='navItem'][.='Timesheets']").Click();
                    new Control("Manage/Approve Timesheets", "xpath", "//div[@class='navItem'][.='Manage/Approve Timesheets']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error navigating to Manage/Approve Timesheets app [ln 39]. " + ex.Message);
                }

                /* 2 - Click Execute */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute (F3)')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Execute (F3).");
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Excute [ln 57]. " + ex.Message);
                }
                /* 3 - Click Browse Apps */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Click on BrowseApplications...", Logger.MessageType.INF);
                    Control CP7Main_BrowseApplications = new Control("BrowseApplications", "ID", "goToLbl");
                    CPCommon.WaitControlDisplayed(CP7Main_BrowseApplications);
                    if (CP7Main_BrowseApplications.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        CP7Main_BrowseApplications.Click(5, 5);
                    else
                        CP7Main_BrowseApplications.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Browse Apps [ln 74]. " + ex.Message);
                }

                /* 4 - Navigate to Manage/Approve Expense Authorization */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed)
                        new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Time & Expense", "xpath", "//div[@class='busItem'][.='Time & Expense']").Click();
                    new Control("Expense", "xpath", "//div[@class='deptItem'][.='Expense']").Click();
                    new Control("Expense Authorizations", "xpath", "//div[@class='navItem'][.='Expense Authorizations']").Click();
                    new Control("Manage/Approve Expense Authorizations", "xpath", "//div[@class='navItem'][.='Manage/Approve Expense Authorizations']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error navigating to Manage/Approve Expense Authorization app [ln 90]. " + ex.Message);
                }

                /* 5 - Click Execute */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute (F3)')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Execute (F3).");
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Execute [ln 106]. " + ex.Message);
                }

                /* 6 - Logout */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Click on LogOut...", Logger.MessageType.INF);
                    Control CP7Main_LogOut = new Control("LogOut", "ID", "lgoffBttn");
                    CPCommon.WaitControlDisplayed(CP7Main_LogOut);
                    if (CP7Main_LogOut.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        CP7Main_LogOut.Click(5, 5);
                    else
                        CP7Main_LogOut.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error logging out [ln 124]. " + ex.Message);
                }
            }
			catch (Exception ex)
			{
				ret = false;
				ErrorMessage = ex.Message;
				throw new Exception(ex.Message);
			}
			return ret;
        }
    }
	
}

