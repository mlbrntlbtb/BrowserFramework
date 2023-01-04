 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEPMCC_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Proposal Bills of Material", "xpath","//div[@class='navItem'][.='Proposal Bills of Material']").Click();
new Control("Apply Mass Component Changes to Proposal BOMs", "xpath","//div[@class='navItem'][.='Apply Mass Component Changes to Proposal BOMs']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEPMCC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEPMCC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MEPMCC_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MEPMCC_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MEPMCC";
							CPCommon.WaitControlDisplayed(MEPMCC_MainForm);
IWebElement formBttn = MEPMCC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MEPMCC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MEPMCC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MEPMCC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEPMCC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PBOM Assemblies");


												
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExists on MainForm_PBOMAssembliesLink...", Logger.MessageType.INF);
			Control MEPMCC_MainForm_PBOMAssembliesLink = new Control("MainForm_PBOMAssembliesLink", "ID", "lnk_2146_MEPMCC_PARAM");
			CPCommon.AssertEqual(true,MEPMCC_MainForm_PBOMAssembliesLink.Exists());

												
				CPCommon.CurrentComponent = "MEPMCC";
							CPCommon.WaitControlDisplayed(MEPMCC_MainForm_PBOMAssembliesLink);
MEPMCC_MainForm_PBOMAssembliesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExist on ProposalBillOfMaterialFormTable...", Logger.MessageType.INF);
			Control MEPMCC_ProposalBillOfMaterialFormTable = new Control("ProposalBillOfMaterialFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEPMCC_ASSY_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEPMCC_ProposalBillOfMaterialFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExists on ProposalBillOfMaterialForm...", Logger.MessageType.INF);
			Control MEPMCC_ProposalBillOfMaterialForm = new Control("ProposalBillOfMaterialForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEPMCC_ASSY_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEPMCC_ProposalBillOfMaterialForm.Exists());

												
				CPCommon.CurrentComponent = "MEPMCC";
							CPCommon.WaitControlDisplayed(MEPMCC_ProposalBillOfMaterialForm);
formBttn = MEPMCC_ProposalBillOfMaterialForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEPMCC_ProposalBillOfMaterialForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEPMCC_ProposalBillOfMaterialForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MEPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPMCC] Perfoming VerifyExists on ProposalBillOfMaterial_Proposal_Proposal...", Logger.MessageType.INF);
			Control MEPMCC_ProposalBillOfMaterial_Proposal_Proposal = new Control("ProposalBillOfMaterial_Proposal_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__MEPMCC_ASSY_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEPMCC_ProposalBillOfMaterial_Proposal_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEPMCC";
							CPCommon.WaitControlDisplayed(MEPMCC_ProposalBillOfMaterialForm);
formBttn = MEPMCC_ProposalBillOfMaterialForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "MEPMCC";
							CPCommon.WaitControlDisplayed(MEPMCC_MainForm);
formBttn = MEPMCC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

