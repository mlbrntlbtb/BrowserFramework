 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAPVUAT_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("Compute/Update Amount Taken Purchase Year-To-Date", "xpath","//div[@class='navItem'][.='Compute/Update Amount Taken Purchase Year-To-Date']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAPVUAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPVUAT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAPVUAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAPVUAT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAPVUAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPVUAT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control FAPVUAT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,FAPVUAT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "FAPVUAT";
							CPCommon.WaitControlDisplayed(FAPVUAT_MainForm);
IWebElement formBttn = FAPVUAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAPVUAT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAPVUAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FAPVUAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPVUAT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAPVUAT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPVUAT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPVUAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPVUAT] Perfoming VerifyExists on ImportantInformationForm...", Logger.MessageType.INF);
			Control FAPVUAT_ImportantInformationForm = new Control("ImportantInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPVUAT_LABEL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPVUAT_ImportantInformationForm.Exists());

												
				CPCommon.CurrentComponent = "FAPVUAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPVUAT] Perfoming VerifyExists on ImportantInformation_ThisUtilityWillOnlyVerifyUpdateTheAmountTakenPurchaseYTDForAssetsWhoseAssignedDepreciationMethodUsesADateOfPurchaseBasisAndWhoseDepreciationStartDateIsPRIORToTheCurrentFAPostingFYPdAndSubperiodForAssetsInTheFirstPeriodOfAPurchaseYearTheAmountTakenPurchaseYTDValueWillBeZeroWhenYouComputeDepreciationForTheFirstTimeAfterApplyingTheUpdatePortionOfThisUtilityMakeSureThatYouCheckAndAgreeWithTheResultsOfTheDepreciationCalculationsLabel...", Logger.MessageType.INF);
			Control FAPVUAT_ImportantInformation_ThisUtilityWillOnlyVerifyUpdateTheAmountTakenPurchaseYTDForAssetsWhoseAssignedDepreciationMethodUsesADateOfPurchaseBasisAndWhoseDepreciationStartDateIsPRIORToTheCurrentFAPostingFYPdAndSubperiodForAssetsInTheFirstPeriodOfAPurchaseYearTheAmountTakenPurchaseYTDValueWillBeZeroWhenYouComputeDepreciationForTheFirstTimeAfterApplyingTheUpdatePortionOfThisUtilityMakeSureThatYouCheckAndAgreeWithTheResultsOfTheDepreciationCalculationsLabel = new Control("ImportantInformation_ThisUtilityWillOnlyVerifyUpdateTheAmountTakenPurchaseYTDForAssetsWhoseAssignedDepreciationMethodUsesADateOfPurchaseBasisAndWhoseDepreciationStartDateIsPRIORToTheCurrentFAPostingFYPdAndSubperiodForAssetsInTheFirstPeriodOfAPurchaseYearTheAmountTakenPurchaseYTDValueWillBeZeroWhenYouComputeDepreciationForTheFirstTimeAfterApplyingTheUpdatePortionOfThisUtilityMakeSureThatYouCheckAndAgreeWithTheResultsOfTheDepreciationCalculationsLabel", "xpath", "//span[@id='LBL']/ancestor::form[1]/descendant::*[@id='LBL']");
			CPCommon.AssertEqual(true,FAPVUAT_ImportantInformation_ThisUtilityWillOnlyVerifyUpdateTheAmountTakenPurchaseYTDForAssetsWhoseAssignedDepreciationMethodUsesADateOfPurchaseBasisAndWhoseDepreciationStartDateIsPRIORToTheCurrentFAPostingFYPdAndSubperiodForAssetsInTheFirstPeriodOfAPurchaseYearTheAmountTakenPurchaseYTDValueWillBeZeroWhenYouComputeDepreciationForTheFirstTimeAfterApplyingTheUpdatePortionOfThisUtilityMakeSureThatYouCheckAndAgreeWithTheResultsOfTheDepreciationCalculationsLabel.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "FAPVUAT";
							CPCommon.WaitControlDisplayed(FAPVUAT_MainForm);
formBttn = FAPVUAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

