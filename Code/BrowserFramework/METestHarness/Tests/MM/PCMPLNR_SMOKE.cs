 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMPLNR_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
								
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Manage Planners", "xpath","//div[@class='navItem'][.='Manage Planners']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCMPLNR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PCMPLNR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PCMPLNR_MainForm);
IWebElement formBttn = PCMPLNR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMPLNR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMPLNR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PCMPLNR";
							CPCommon.AssertEqual(true,PCMPLNR_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExists on PlannerID...", Logger.MessageType.INF);
			Control PCMPLNR_PlannerID = new Control("PlannerID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PLANNER_ID']");
			CPCommon.AssertEqual(true,PCMPLNR_PlannerID.Exists());

											Driver.SessionLogger.WriteLine("Selected Inventory");


												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExists on SelectedInventoryProjectsLink...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedInventoryProjectsLink = new Control("SelectedInventoryProjectsLink", "ID", "lnk_1002195_PCMPLNR_PLANNER");
			CPCommon.AssertEqual(true,PCMPLNR_SelectedInventoryProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
							CPCommon.WaitControlDisplayed(PCMPLNR_SelectedInventoryProjectsLink);
PCMPLNR_SelectedInventoryProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on InventoryProjectsTable...", Logger.MessageType.INF);
			Control PCMPLNR_InventoryProjectsTable = new Control("InventoryProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_INVPROJECTS_FROM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_InventoryProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on SelectedInventoryProjectsTable...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedInventoryProjectsTable = new Control("SelectedInventoryProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_INVPROJECTS_TO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_SelectedInventoryProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming Close on SelectedInventoryProjectsForm...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedInventoryProjectsForm = new Control("SelectedInventoryProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_INVPROJECTS_TO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMPLNR_SelectedInventoryProjectsForm);
formBttn = PCMPLNR_SelectedInventoryProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Selected Netting");


												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExists on SelectedNettingGroupsLink...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedNettingGroupsLink = new Control("SelectedNettingGroupsLink", "ID", "lnk_1002217_PCMPLNR_PLANNER");
			CPCommon.AssertEqual(true,PCMPLNR_SelectedNettingGroupsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
							CPCommon.WaitControlDisplayed(PCMPLNR_SelectedNettingGroupsLink);
PCMPLNR_SelectedNettingGroupsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on NettingGroupTable...", Logger.MessageType.INF);
			Control PCMPLNR_NettingGroupTable = new Control("NettingGroupTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_NETGRPS_FROM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_NettingGroupTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on SelectedNettingGroupTable...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedNettingGroupTable = new Control("SelectedNettingGroupTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_NETGRPS_TO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_SelectedNettingGroupTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming Close on SelectedNettingGroupForm...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedNettingGroupForm = new Control("SelectedNettingGroupForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_NETGRPS_TO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMPLNR_SelectedNettingGroupForm);
formBttn = PCMPLNR_SelectedNettingGroupForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Selected Commodities");


												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExists on SelectedCommoditiesLink...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedCommoditiesLink = new Control("SelectedCommoditiesLink", "ID", "lnk_1002219_PCMPLNR_PLANNER");
			CPCommon.AssertEqual(true,PCMPLNR_SelectedCommoditiesLink.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
							CPCommon.WaitControlDisplayed(PCMPLNR_SelectedCommoditiesLink);
PCMPLNR_SelectedCommoditiesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on CommoditiesTable...", Logger.MessageType.INF);
			Control PCMPLNR_CommoditiesTable = new Control("CommoditiesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_COMM_FROM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_CommoditiesTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming VerifyExist on SelectedCommoditiesTable...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedCommoditiesTable = new Control("SelectedCommoditiesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_COMM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMPLNR_SelectedCommoditiesTable.Exists());

												
				CPCommon.CurrentComponent = "PCMPLNR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMPLNR] Perfoming Close on SelectedCommoditiesForm...", Logger.MessageType.INF);
			Control PCMPLNR_SelectedCommoditiesForm = new Control("SelectedCommoditiesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMPLNR_COMM_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMPLNR_SelectedCommoditiesForm);
formBttn = PCMPLNR_SelectedCommoditiesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PCMPLNR";
							CPCommon.WaitControlDisplayed(PCMPLNR_MainForm);
formBttn = PCMPLNR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
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

