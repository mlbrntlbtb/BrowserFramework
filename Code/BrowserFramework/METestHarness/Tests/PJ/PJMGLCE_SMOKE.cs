 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMGLCE_SMOKE : TestScript
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
new Control("Manage Estimate To Complete GLC Hours", "xpath","//div[@class='navItem'][.='Manage Estimate To Complete GLC Hours']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMGLCE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMGLCE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMGLCE_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMGLCE_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
							CPCommon.WaitControlDisplayed(PJMGLCE_MainForm);
IWebElement formBttn = PJMGLCE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMGLCE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMGLCE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMGLCE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMGLCE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("GENERAL CATEGORY DETAILS");


												
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on GeneralLaborCategoryDetailsLink...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetailsLink = new Control("GeneralLaborCategoryDetailsLink", "ID", "lnk_1001700_PJMGLCE_ETCGLCHRS_HDR");
			CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming ClickButton on GeneralLaborCategoryDetailsForm...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetailsForm = new Control("GeneralLaborCategoryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCE_ETCGLCHRS_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMGLCE_GeneralLaborCategoryDetailsForm);
formBttn = PJMGLCE_GeneralLaborCategoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMGLCE_GeneralLaborCategoryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMGLCE_GeneralLaborCategoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMGLCE";
							CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on GeneralLaborCategoryDetails_GLC...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetails_GLC = new Control("GeneralLaborCategoryDetails_GLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCE_ETCGLCHRS_CHLD_']/ancestor::form[1]/descendant::*[@id='GENL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetails_GLC.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
							CPCommon.WaitControlDisplayed(PJMGLCE_GeneralLaborCategoryDetailsForm);
formBttn = PJMGLCE_GeneralLaborCategoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMGLCE_GeneralLaborCategoryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMGLCE_GeneralLaborCategoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExist on GeneralLaborCategoryDetailsFormTable...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetailsFormTable = new Control("GeneralLaborCategoryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCE_ETCGLCHRS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetailsFormTable.Exists());

											Driver.SessionLogger.WriteLine("REVISE ESTIMATE TO COMPLETE");


												
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on GeneralLaborCategoryDetails_ReviseEstimatetoCompleteLink...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimatetoCompleteLink = new Control("GeneralLaborCategoryDetails_ReviseEstimatetoCompleteLink", "ID", "lnk_1001701_PJMGLCE_ETCGLCHRS_CHLD");
			CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimatetoCompleteLink.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
							CPCommon.WaitControlDisplayed(PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimatetoCompleteLink);
PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimatetoCompleteLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on GeneralLaborCategoryDetails_ReviseEstimateToCompleteForm...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimateToCompleteForm = new Control("GeneralLaborCategoryDetails_ReviseEstimateToCompleteForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCE_ETCGLCHRS_REVISE_ETC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimateToCompleteForm.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCE] Perfoming VerifyExists on GeneralLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred...", Logger.MessageType.INF);
			Control PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred = new Control("GeneralLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCE_ETCGLCHRS_REVISE_ETC_']/ancestor::form[1]/descendant::*[@id='REVISION_TYPE' and @value='E']");
			CPCommon.AssertEqual(true,PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimateToComplete_RevisionType_UpdateETCWithBudgetMinusIncurred.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCE";
							CPCommon.WaitControlDisplayed(PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimateToCompleteForm);
formBttn = PJMGLCE_GeneralLaborCategoryDetails_ReviseEstimateToCompleteForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMGLCE";
							CPCommon.WaitControlDisplayed(PJMGLCE_MainForm);
formBttn = PJMGLCE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

