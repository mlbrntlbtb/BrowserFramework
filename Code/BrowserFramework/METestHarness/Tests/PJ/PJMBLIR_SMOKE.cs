 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBLIR_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Encumbrance Tracking", "xpath","//div[@class='navItem'][.='Encumbrance Tracking']").Click();
new Control("Manage Project Budget Line Item Revisions", "xpath","//div[@class='navItem'][.='Manage Project Budget Line Item Revisions']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PJMBLIR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBLIR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBLIR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBLIR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBLIR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBLIR] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMBLIR_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMBLIR_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMBLIR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBLIR] Perfoming VerifyExists on BudgetLineItemRevisionsLink...", Logger.MessageType.INF);
			Control PJMBLIR_BudgetLineItemRevisionsLink = new Control("BudgetLineItemRevisionsLink", "ID", "lnk_3798_PJMBLIR_PROJBUGLNREV_HDR");
			CPCommon.AssertEqual(true,PJMBLIR_BudgetLineItemRevisionsLink.Exists());

											Driver.SessionLogger.WriteLine("Budget Line Item Revisions");


												
				CPCommon.CurrentComponent = "PJMBLIR";
							CPCommon.WaitControlDisplayed(PJMBLIR_BudgetLineItemRevisionsLink);
PJMBLIR_BudgetLineItemRevisionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBLIR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBLIR] Perfoming VerifyExists on BudgetLineItemRevisionsForm...", Logger.MessageType.INF);
			Control PJMBLIR_BudgetLineItemRevisionsForm = new Control("BudgetLineItemRevisionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBLIR_PROJBUGLNREV_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBLIR_BudgetLineItemRevisionsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBLIR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBLIR] Perfoming VerifyExist on BudgetLineItemRevisionsFormTable...", Logger.MessageType.INF);
			Control PJMBLIR_BudgetLineItemRevisionsFormTable = new Control("BudgetLineItemRevisionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBLIR_PROJBUGLNREV_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBLIR_BudgetLineItemRevisionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBLIR";
							CPCommon.WaitControlDisplayed(PJMBLIR_BudgetLineItemRevisionsForm);
IWebElement formBttn = PJMBLIR_BudgetLineItemRevisionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBLIR_BudgetLineItemRevisionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBLIR_BudgetLineItemRevisionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJMBLIR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBLIR] Perfoming VerifyExists on BudgetLineItemRevisions_RevisionID...", Logger.MessageType.INF);
			Control PJMBLIR_BudgetLineItemRevisions_RevisionID = new Control("BudgetLineItemRevisions_RevisionID", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBLIR_PROJBUGLNREV_CHLD_']/ancestor::form[1]/descendant::*[@id='REV_ID']");
			CPCommon.AssertEqual(true,PJMBLIR_BudgetLineItemRevisions_RevisionID.Exists());

												
				CPCommon.CurrentComponent = "PJMBLIR";
							CPCommon.WaitControlDisplayed(PJMBLIR_MainForm);
formBttn = PJMBLIR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

