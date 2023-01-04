 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPLCE_SMOKE : TestScript
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
new Control("Estimate To Complete", "xpath","//div[@class='navItem'][.='Estimate To Complete']").Click();
new Control("Manage Estimate to Complete PLC Hours", "xpath","//div[@class='navItem'][.='Manage Estimate to Complete PLC Hours']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPLCE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPLCE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMPLCE_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPLCE_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
							CPCommon.WaitControlDisplayed(PJMPLCE_MainForm);
IWebElement formBttn = PJMPLCE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPLCE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPLCE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPLCE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT LABOR CATEGORY DETAILS");


												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on ProjectLaborCategoryDetailsLink...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetailsLink = new Control("ProjectLaborCategoryDetailsLink", "ID", "lnk_1002438_PJMPLCE_ETCPLCHRS_HDR");
			CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExist on ProjectLaborCategoryDetailsFormTable...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetailsFormTable = new Control("ProjectLaborCategoryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCE_ETCPLCHRS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming ClickButton on ProjectLaborCategoryDetailsForm...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetailsForm = new Control("ProjectLaborCategoryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCE_ETCPLCHRS_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPLCE_ProjectLaborCategoryDetailsForm);
formBttn = PJMPLCE_ProjectLaborCategoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPLCE_ProjectLaborCategoryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPLCE_ProjectLaborCategoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPLCE";
							CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on ProjectLaborCategoryDetails_PLC...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetails_PLC = new Control("ProjectLaborCategoryDetails_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCE_ETCPLCHRS_CHLD_']/ancestor::form[1]/descendant::*[@id='BILL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetails_PLC.Exists());

											Driver.SessionLogger.WriteLine("REVISE ESTIMATE TO COMPLETE");


												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on ProjectLaborCategoryDetails_ReviseEstimatetoCompleteLink...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimatetoCompleteLink = new Control("ProjectLaborCategoryDetails_ReviseEstimatetoCompleteLink", "ID", "lnk_1002439_PJMPLCE_ETCPLCHRS_CHLD");
			CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimatetoCompleteLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
							CPCommon.WaitControlDisplayed(PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimatetoCompleteLink);
PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimatetoCompleteLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on ProjectLaborCategoryDetails_ReviseEstimateToCompleteForm...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimateToCompleteForm = new Control("ProjectLaborCategoryDetails_ReviseEstimateToCompleteForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCE_ETCPLCHRS_REVISE_ETC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimateToCompleteForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCE] Perfoming VerifyExists on ProjectLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred...", Logger.MessageType.INF);
			Control PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred = new Control("ProjectLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCE_ETCPLCHRS_REVISE_ETC_']/ancestor::form[1]/descendant::*[@id='REVISION_TYPE' and @value='E']");
			CPCommon.AssertEqual(true,PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCE";
							CPCommon.WaitControlDisplayed(PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimateToCompleteForm);
formBttn = PJMPLCE_ProjectLaborCategoryDetails_ReviseEstimateToCompleteForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMPLCE";
							CPCommon.WaitControlDisplayed(PJMPLCE_MainForm);
formBttn = PJMPLCE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

