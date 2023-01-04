 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class TEBuildAcceptanceTest_Reg : TestScript
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
                    new Control("Manage Timesheets", "xpath", "//div[@class='navItem'][.='Manage Timesheets']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error navigating to Manage/Approve Timesheets app [ln 39]. " + ex.Message);
                }

                /* 2 - Click Browse Applications */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Click on BrowseApplications...", Logger.MessageType.INF);
                    Control CP7Main_BrowseApplications = new Control("BrowseApplications", "ID", "goToLbl");
                    CPCommon.WaitControlDisplayed(CP7Main_BrowseApplications);
                    if (CP7Main_BrowseApplications.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        CP7Main_BrowseApplications.Click(5, 5);
                    else CP7Main_BrowseApplications.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Browse Applications [ln 56]. " + ex.Message);
                }

                /* 3 - Navigate to Expense Authorization app */
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
                    throw new Exception("Error navigating to Expense Authorization app [ln 72]. " + ex.Message);
                }

                /* 4 - Select FilterBy */
                try
                {
                    CPCommon.CurrentComponent = "EPMEXPAUTHAPPROVE";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[EPMEXPAUTHAPPROVE] Perfoming Select on FilterBy...", Logger.MessageType.INF);
                    Control EPMEXPAUTHAPPROVE_FilterBy = new Control("Description", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FILTER_BY']");
                    EPMEXPAUTHAPPROVE_FilterBy.FindElement();
                    EPMEXPAUTHAPPROVE_FilterBy.ScrollIntoViewUsingJavaScript();
                    Control EPMEXPAUTHAPPROVE_FilterByList = new Control("FilterByList", "xpath_display", "//div[@class='tCCBV']/div[text()='Status']/..");
                    int currRetry = 0;
                    while (++currRetry <= 3)
                    {
                        try
                        {
                            EPMEXPAUTHAPPROVE_FilterBy.Click();
                            EPMEXPAUTHAPPROVE_FilterByList.FindElement(1); // quicker -> if not found then initilize will not be called
                            break;
                        }
                        catch
                        {
                            Driver.SessionLogger.WriteLine("Unable to display combobox dropdown list...", Logger.MessageType.INF);
                        }
                    }
                    if (currRetry > 3)
                    {
                        throw new Exception("Unable to display combobox dropdown list on final attempt.");
                    }
                    Control target = new Control("ItemToSelect", EPMEXPAUTHAPPROVE_FilterByList, "xpath", "./descendant::div[text()='Status']");
                    target.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error selecting FilterBy [ln 108]. " + ex.Message);
                }

                /* 5 - Click Execute */
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
                    throw new Exception("Error clicking Excute [ln 126]. " + ex.Message);
                }

                /* 6 - Click Browse Applications */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Click on BrowseApplications...", Logger.MessageType.INF);
                    Control CP7Main_BrowseApplications = new Control("BrowseApplications", "ID", "goToLbl");
                    CPCommon.WaitControlDisplayed(CP7Main_BrowseApplications);
                    if (CP7Main_BrowseApplications.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        CP7Main_BrowseApplications.Click(5, 5);
                    else CP7Main_BrowseApplications.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Browse Applications [ln 143]. " + ex.Message);
                }

                /* 7 - Navigate to Manage Expense Report app */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Time & Expense", "xpath", "//div[@class='busItem'][.='Time & Expense']").Click();
                    new Control("Expense", "xpath", "//div[@class='deptItem'][.='Expense']").Click();
                    new Control("Expense Reports", "xpath", "//div[@class='navItem'][.='Expense Reports']").Click();
                    new Control("Manage Expense Report", "xpath", "//div[@class='navItem'][.='Manage Expense Report']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error navigating to Manage Expense Report app [ln 158]. " + ex.Message);
                }

                /* 8 - Click Browse Applications */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Click on BrowseApplications...", Logger.MessageType.INF);
                    Control CP7Main_BrowseApplications = new Control("BrowseApplications", "ID", "goToLbl");
                    CPCommon.WaitControlDisplayed(CP7Main_BrowseApplications);
                    if (CP7Main_BrowseApplications.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        CP7Main_BrowseApplications.Click(5, 5);
                    else CP7Main_BrowseApplications.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Browse Applications [ln 175]. " + ex.Message);
                }

                /* 9 - Navigate to Manage Expense Types app */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Time & Expense", "xpath", "//div[@class='busItem'][.='Time & Expense']").Click();
                    new Control("Expense", "xpath", "//div[@class='deptItem'][.='Expense']").Click();
                    new Control("Expense Reports", "xpath", "//div[@class='navItem'][.='Expense Controls']").Click();
                    new Control("Manage Expense Report", "xpath", "//div[@class='navItem'][.='Manage Expense Types']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error navigating to Manage Expense Report app [ln 190]. " + ex.Message);
                }

                /* 10 - Set ExpenseTypCode */
                try
                {
                    CPCommon.CurrentComponent = "EPMEXPTYPE";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[EPMEXPTYPE] Perfoming Set on ExpenseTypeCode...", Logger.MessageType.INF);
                    Control EPMEXPTYPE_ExpenseTypeCode = new Control("ExpenseTypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EXP_TYPE_CD']");
                    EPMEXPTYPE_ExpenseTypeCode.Click();
                    EPMEXPTYPE_ExpenseTypeCode.SendKeys("BUILDTEST", true);
                    CPCommon.WaitLoadingFinished();
                    EPMEXPTYPE_ExpenseTypeCode.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error setting ExpenseTypeCode [ln 207]. " + ex.Message);
                }

                /* 11 - Set Description */
                try
                {

                    CPCommon.CurrentComponent = "EPMEXPTYPE";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[EPMEXPTYPE] Perfoming Set on Description...", Logger.MessageType.INF);
                    Control EPMEXPTYPE_Description = new Control("Description", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EXP_TYPE_DESC']");
                    EPMEXPTYPE_Description.Click();
                    EPMEXPTYPE_Description.SendKeys("Build Test", true);
                    CPCommon.WaitLoadingFinished();
                    EPMEXPTYPE_Description.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error setting Description [ln 225]. " + ex.Message);
                }

                /* 12 - Select WizardType */
                try
                {
                    CPCommon.CurrentComponent = "EPMEXPTYPE";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[EPMEXPTYPE] Perfoming Select on WizardType...", Logger.MessageType.INF);
                    Control EPMEXPTYPE_WizardType = new Control("Description", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_TYPE_CD']");
                    EPMEXPTYPE_WizardType.FindElement();
                    EPMEXPTYPE_WizardType.ScrollIntoViewUsingJavaScript();
                    Control EPMEXPTYPE_WizardTypeList = new Control("WizardTypeList", "xpath_display", "//div[@class='tCCBV']/div[text()='Car Rental']/..");
                    int currRetry = 0;
                    while (++currRetry <= 3)
                    {
                        try
                        {
                            EPMEXPTYPE_WizardType.Click();
                            EPMEXPTYPE_WizardTypeList.FindElement(1); // quicker -> if not found then initilize will not be called
                            break;
                        }
                        catch
                        {
                            Driver.SessionLogger.WriteLine("Unable to display combobox dropdown list...", Logger.MessageType.INF);
                        }
                    }
                    if (currRetry > 3)
                    {
                        throw new Exception("Unable to display combobox dropdown list on final attempt.");
                    }
                    Control target = new Control("ItemToSelect", EPMEXPTYPE_WizardTypeList, "xpath", "./descendant::div[text()='Car Rental']");
                    target.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error selecting WizardType [ln 261]. " + ex.Message);
                }

                /* 13 - Click Save & Continue */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save & Continue')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Save & Continue.");
                    Thread.Sleep(800);
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Save and Continue [ln 278]. " + ex.Message);
                }

                /* 14 - Click Query */
                try
                {
                    CPCommon.CurrentComponent = "EPMEXPTYPE";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[EPMEXPTYPE] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
                    Control EPMEXPTYPE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(EPMEXPTYPE_MainForm);
                    IWebElement formBttn = EPMEXPTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 
                        ? EPMEXPTYPE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
                    EPMEXPTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
                    if (formBttn != null)
                    {
                        new Control("FormButton", formBttn).MouseOver();
                        formBttn.Click();
                    }
                    else
                    {
                        throw new Exception("Query not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Query [ln 304]. " + ex.Message);
                }

                /* 15 - Set Find criteria */
                try
                {
                    CPCommon.CurrentComponent = "Query";
                    CPCommon.WaitLoadingFinished(true);
                    Driver.SessionLogger.WriteLine("[Query] Perfoming Set on Find_CriteriaValue1...", Logger.MessageType.INF);
                    Control Query_Find_CriteriaValue1 = new Control("Find_CriteriaValue1", "ID", "basicField0");
                    Query_Find_CriteriaValue1.Click();
                    Query_Find_CriteriaValue1.SendKeys("BUILDTEST", true);
                    CPCommon.WaitLoadingFinished(true);
                    Query_Find_CriteriaValue1.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error setting Find Criteria [ln 321]. " + ex.Message);
                }

                /* 16 - Click Find */
                try
                {
                    CPCommon.CurrentComponent = "Query";
                    CPCommon.WaitLoadingFinished(true);
                    Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
                    Control Query_Find = new Control("Find", "ID", "submitQ");
                    CPCommon.WaitControlDisplayed(Query_Find);
                    if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        Query_Find.Click(5, 5);
                    else Query_Find.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Find [ln 338]. " + ex.Message);
                }

                /* 17 - Verify ExpenseTypeCode */
                try
                {
                    CPCommon.CurrentComponent = "EPMEXPTYPE";
                    Control EPMEXPTYPE_ExpenseTypeCode = new Control("ExpenseTypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EXP_TYPE_CD']");
                    CPCommon.AssertEqual("BUILDTEST", EPMEXPTYPE_ExpenseTypeCode.GetAttributeValue("value"));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error verifying ExpenseTypeCode [ln 350]. " + ex.Message);
                }

                /* 18 - Click Delete */
                try
                {
                    CPCommon.CurrentComponent = "EPMEXPTYPE";
                    Control EPMEXPTYPE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(EPMEXPTYPE_MainForm);
                    IWebElement formBttn = EPMEXPTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 
                        ? EPMEXPTYPE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
                    EPMEXPTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
                    if (formBttn != null)
                    {
                        new Control("FormButton", formBttn).MouseOver();
                        formBttn.Click();
                    }
                    else
                    {
                        throw new Exception("Delete not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Delete [ln 374]. " + ex.Message);
                }

                /* 19 - Click Save & Continue */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save & Continue')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Save & Continue.");
                    Thread.Sleep(800);
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking Save and Continue [ln 391]. " + ex.Message);
                }

                /* 20 - Logout */
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
                    throw new Exception("Error logging out [ln 409]. " + ex.Message);
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

