 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COMMAP_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Consolidations", "xpath","//div[@class='deptItem'][.='Consolidations']").Click();
new Control("Consolidations Controls", "xpath","//div[@class='navItem'][.='Consolidations Controls']").Click();
new Control("Manage Consolidation Acct/Org Mappings", "xpath","//div[@class='navItem'][.='Manage Consolidation Acct/Org Mappings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control COMMAP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,COMMAP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on MappingCode...", Logger.MessageType.INF);
			Control COMMAP_MappingCode = new Control("MappingCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CONS_MAP_CD']");
			CPCommon.AssertEqual(true,COMMAP_MappingCode.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
							CPCommon.WaitControlDisplayed(COMMAP_MainForm);
IWebElement formBttn = COMMAP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? COMMAP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
COMMAP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control COMMAP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMMAP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control COMMAP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_CONSMAP_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COMMAP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control COMMAP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_CONSMAP_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMMAP_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on ChildForm2...", Logger.MessageType.INF);
			Control COMMAP_ChildForm2 = new Control("ChildForm2", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_ACCTORG_COUNT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COMMAP_ChildForm2.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on ChildForm2_AccountCountText...", Logger.MessageType.INF);
			Control COMMAP_ChildForm2_AccountCountText = new Control("ChildForm2_AccountCountText", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_ACCTORG_COUNT_']/ancestor::form[1]/descendant::*[@id='ACCT_COUNT']");
			CPCommon.AssertEqual(true,COMMAP_ChildForm2_AccountCountText.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on SampleAccountsForm...", Logger.MessageType.INF);
			Control COMMAP_SampleAccountsForm = new Control("SampleAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_ACCT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COMMAP_SampleAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExist on SampleAccountsFormTable...", Logger.MessageType.INF);
			Control COMMAP_SampleAccountsFormTable = new Control("SampleAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_ACCT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMMAP_SampleAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExists on SampleOrganizationsForm...", Logger.MessageType.INF);
			Control COMMAP_SampleOrganizationsForm = new Control("SampleOrganizationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_ORG_ORGCTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COMMAP_SampleOrganizationsForm.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMMAP] Perfoming VerifyExist on SampleOrganizationsFormTable...", Logger.MessageType.INF);
			Control COMMAP_SampleOrganizationsFormTable = new Control("SampleOrganizationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__COMMAP_ORG_ORGCTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMMAP_SampleOrganizationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMMAP";
							CPCommon.WaitControlDisplayed(COMMAP_MainForm);
formBttn = COMMAP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

