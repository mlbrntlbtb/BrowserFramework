 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMMLPAO_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Charging Information", "xpath","//div[@class='navItem'][.='Charging Information']").Click();
new Control("Mass Link Projects/Accounts/Organizations", "xpath","//div[@class='navItem'][.='Mass Link Projects/Accounts/Organizations']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Mass Link Proj/Acct/Org", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMMLPAO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMLPAO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMMLPAO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMMLPAO_MainForm);
IWebElement formBttn = PJMMLPAO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMLPAO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMLPAO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMMLPAO";
							CPCommon.AssertEqual(true,PJMMLPAO_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMMLPAO_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMMLPAO_Project.Exists());

											Driver.SessionLogger.WriteLine("MASS LINK PROJ/ACCT/ORG");


												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExists on MassLinkProjAcctOrgForm...", Logger.MessageType.INF);
			Control PJMMLPAO_MassLinkProjAcctOrgForm = new Control("MassLinkProjAcctOrgForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_ACCTGRPSETUPMLPAO_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMMLPAO_MassLinkProjAcctOrgForm.Exists());

												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExist on MassLinkProjAcctOrgFormTable...", Logger.MessageType.INF);
			Control PJMMLPAO_MassLinkProjAcctOrgFormTable = new Control("MassLinkProjAcctOrgFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_ACCTGRPSETUPMLPAO_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMLPAO_MassLinkProjAcctOrgFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExists on MassLinkProjAcctOrg_Select...", Logger.MessageType.INF);
			Control PJMMLPAO_MassLinkProjAcctOrg_Select = new Control("MassLinkProjAcctOrg_Select", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_ACCTGRPSETUPMLPAO_CTW_']/ancestor::form[1]/descendant::*[contains(@id,'SELECT') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,PJMMLPAO_MassLinkProjAcctOrg_Select.Exists());

												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExist on OrganizationsFormTable...", Logger.MessageType.INF);
			Control PJMMLPAO_OrganizationsFormTable = new Control("OrganizationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_VGLLOOKUPAOMLPAO_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMLPAO_OrganizationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExists on OrganizationsForm...", Logger.MessageType.INF);
			Control PJMMLPAO_OrganizationsForm = new Control("OrganizationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_VGLLOOKUPAOMLPAO_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMMLPAO_OrganizationsForm.Exists());

											Driver.SessionLogger.WriteLine("SELECTED ACCOUNTS / ORGANIZATIONS");


												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExist on SelectedAccountOrganizationsFormTable...", Logger.MessageType.INF);
			Control PJMMLPAO_SelectedAccountOrganizationsFormTable = new Control("SelectedAccountOrganizationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJORGACCTMLPAO_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMLPAO_SelectedAccountOrganizationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming ClickButton on SelectedAccountOrganizationsForm...", Logger.MessageType.INF);
			Control PJMMLPAO_SelectedAccountOrganizationsForm = new Control("SelectedAccountOrganizationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJORGACCTMLPAO_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMMLPAO_SelectedAccountOrganizationsForm);
formBttn = PJMMLPAO_SelectedAccountOrganizationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMLPAO_SelectedAccountOrganizationsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMLPAO_SelectedAccountOrganizationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMMLPAO";
							CPCommon.AssertEqual(true,PJMMLPAO_SelectedAccountOrganizationsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMMLPAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMLPAO] Perfoming VerifyExists on SelectedAccountOrganizations_Account...", Logger.MessageType.INF);
			Control PJMMLPAO_SelectedAccountOrganizations_Account = new Control("SelectedAccountOrganizations_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJORGACCTMLPAO_CTW_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PJMMLPAO_SelectedAccountOrganizations_Account.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMMLPAO";
							CPCommon.WaitControlDisplayed(PJMMLPAO_MainForm);
formBttn = PJMMLPAO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

