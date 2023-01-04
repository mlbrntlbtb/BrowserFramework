 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSBE_SMOKE : TestScript
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

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Set on SearchApplications...", Logger.MessageType.INF);
			Control CP7Main_SearchApplications = new Control("SearchApplications", "ID", "appFltrFld");
			CP7Main_SearchApplications.Click();
CP7Main_SearchApplications.SendKeys("AOMESSBE", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMESSBE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMESSBE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control AOMESSBE_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,AOMESSBE_Employee.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on BenefitPlanDetailsForm...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetailsForm = new Control("BenefitPlanDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_BenefitPlanDetailsForm);
IWebElement formBttn = AOMESSBE_BenefitPlanDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSBE_BenefitPlanDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSBE_BenefitPlanDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExist on BenefitPlanDetailsFormTable...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetailsFormTable = new Control("BenefitPlanDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_BenefitPlanDetailsForm);
formBttn = AOMESSBE_BenefitPlanDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMESSBE_BenefitPlanDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMESSBE_BenefitPlanDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on BenefitPlanDetails_BenefitPlan...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetails_BenefitPlan = new Control("BenefitPlanDetails_BenefitPlan", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_CTW_']/ancestor::form[1]/descendant::*[@id='BEN_PLAN_CD']");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetails_BenefitPlan.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on DependentFSAElectionLink...", Logger.MessageType.INF);
			Control AOMESSBE_DependentFSAElectionLink = new Control("DependentFSAElectionLink", "ID", "lnk_5204_AOMESSBE_HBEMPLESSELEC_HDR");
			CPCommon.AssertEqual(true,AOMESSBE_DependentFSAElectionLink.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on MedicalFSAElectionLink...", Logger.MessageType.INF);
			Control AOMESSBE_MedicalFSAElectionLink = new Control("MedicalFSAElectionLink", "ID", "lnk_5205_AOMESSBE_HBEMPLESSELEC_HDR");
			CPCommon.AssertEqual(true,AOMESSBE_MedicalFSAElectionLink.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on BenefitPlanDetails_EditDependentElectionsLink...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetails_EditDependentElectionsLink = new Control("BenefitPlanDetails_EditDependentElectionsLink", "ID", "lnk_5206_AOMESSBE_HBEMPLESSELEC_CTW");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetails_EditDependentElectionsLink.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_DependentFSAElectionLink);
AOMESSBE_DependentFSAElectionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on DependentFSAElectionForm...", Logger.MessageType.INF);
			Control AOMESSBE_DependentFSAElectionForm = new Control("DependentFSAElectionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_FSAELEC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSBE_DependentFSAElectionForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on DependentFSAElection_FSAYear...", Logger.MessageType.INF);
			Control AOMESSBE_DependentFSAElection_FSAYear = new Control("DependentFSAElection_FSAYear", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_FSAELEC_']/ancestor::form[1]/descendant::*[@id='DEP_FSA_YR_NO']");
			CPCommon.AssertEqual(true,AOMESSBE_DependentFSAElection_FSAYear.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_DependentFSAElectionForm);
formBttn = AOMESSBE_DependentFSAElectionForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSBE_DependentFSAElectionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSBE_DependentFSAElectionForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExist on DependentFSAElectionFormTable...", Logger.MessageType.INF);
			Control AOMESSBE_DependentFSAElectionFormTable = new Control("DependentFSAElectionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_FSAELEC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSBE_DependentFSAElectionFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_DependentFSAElectionForm);
formBttn = AOMESSBE_DependentFSAElectionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_MedicalFSAElectionLink);
AOMESSBE_MedicalFSAElectionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on MedicalFSAElectionForm...", Logger.MessageType.INF);
			Control AOMESSBE_MedicalFSAElectionForm = new Control("MedicalFSAElectionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_FSA_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSBE_MedicalFSAElectionForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on MedicalFSAElection_FSAYear...", Logger.MessageType.INF);
			Control AOMESSBE_MedicalFSAElection_FSAYear = new Control("MedicalFSAElection_FSAYear", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_FSA_']/ancestor::form[1]/descendant::*[@id='MED_FSA_YR_NO']");
			CPCommon.AssertEqual(true,AOMESSBE_MedicalFSAElection_FSAYear.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_MedicalFSAElectionForm);
formBttn = AOMESSBE_MedicalFSAElectionForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSBE_MedicalFSAElectionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSBE_MedicalFSAElectionForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExist on MedicalFSAElectionFormTable...", Logger.MessageType.INF);
			Control AOMESSBE_MedicalFSAElectionFormTable = new Control("MedicalFSAElectionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBEMPLESSELEC_FSA_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSBE_MedicalFSAElectionFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_MedicalFSAElectionForm);
formBttn = AOMESSBE_MedicalFSAElectionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_BenefitPlanDetails_EditDependentElectionsLink);
AOMESSBE_BenefitPlanDetails_EditDependentElectionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on BenefitPlanDetails_EditDependentElectionsForm...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm = new Control("BenefitPlanDetails_EditDependentElectionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBDEPESSELEC_DEP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExist on BenefitPlanDetails_EditDependentElectionsFormTable...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetails_EditDependentElectionsFormTable = new Control("BenefitPlanDetails_EditDependentElectionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBDEPESSELEC_DEP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetails_EditDependentElectionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm);
formBttn = AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExists on BenefitPlanDetails_EditDependentElections_DependentName...", Logger.MessageType.INF);
			Control AOMESSBE_BenefitPlanDetails_EditDependentElections_DependentName = new Control("BenefitPlanDetails_EditDependentElections_DependentName", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSBE_HBDEPESSELEC_DEP_']/ancestor::form[1]/descendant::*[@id='LAST_FIRST_NAME']");
			CPCommon.AssertEqual(true,AOMESSBE_BenefitPlanDetails_EditDependentElections_DependentName.Exists());

												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm);
formBttn = AOMESSBE_BenefitPlanDetails_EditDependentElectionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_MainForm);
formBttn = AOMESSBE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSBE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSBE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSBE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSBE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSBE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSBE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "AOMESSBE";
							CPCommon.WaitControlDisplayed(AOMESSBE_MainForm);
formBttn = AOMESSBE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

