 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMSETUP_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Performance Reviews", "xpath","//div[@class='navItem'][.='Performance Reviews']").Click();
new Control("Manage Performance Review Forms", "xpath","//div[@class='navItem'][.='Manage Performance Review Forms']").Click();


												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMSETUP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMSETUP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on ReviewFormID...", Logger.MessageType.INF);
			Control HSMSETUP_ReviewFormID = new Control("ReviewFormID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REVIEW_FORM_ID']");
			CPCommon.AssertEqual(true,HSMSETUP_ReviewFormID.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming Click on EmployeeEvaluationLink...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeEvaluationLink = new Control("EmployeeEvaluationLink", "ID", "lnk_1002158_HSMSETUP_RFS1CONFIG");
			CPCommon.WaitControlDisplayed(HSMSETUP_EmployeeEvaluationLink);
HSMSETUP_EmployeeEvaluationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeEvaluationForm...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeEvaluationForm = new Control("EmployeeEvaluationForm", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS2HDRCONFIG_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeEvaluationForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeEvaluation_PrintingOption_PageBreak...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeEvaluation_PrintingOption_PageBreak = new Control("EmployeeEvaluation_PrintingOption_PageBreak", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS2HDRCONFIG_')]/ancestor::form[1]/descendant::*[@id='S_BRK_CRIT_CD' and @value='P']");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeEvaluation_PrintingOption_PageBreak.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeEvaluation_EmployeeEvaluationDetailsForm...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeEvaluation_EmployeeEvaluationDetailsForm = new Control("EmployeeEvaluation_EmployeeEvaluationDetailsForm", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS2LNCONFIG_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeEvaluation_EmployeeEvaluationDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExist on EmployeeEvaluation_EmployeeEvaluationDetailsTable...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeEvaluation_EmployeeEvaluationDetailsTable = new Control("EmployeeEvaluation_EmployeeEvaluationDetailsTable", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS2LNCONFIG_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeEvaluation_EmployeeEvaluationDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EmployeeEvaluationForm);
IWebElement formBttn = HSMSETUP_EmployeeEvaluationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeDevelopmentLink...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeDevelopmentLink = new Control("EmployeeDevelopmentLink", "ID", "lnk_1002160_HSMSETUP_RFS1CONFIG");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeDevelopmentLink.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EmployeeDevelopmentLink);
HSMSETUP_EmployeeDevelopmentLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeDevelopment_PrintingOption_PageBreak...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeDevelopment_PrintingOption_PageBreak = new Control("EmployeeDevelopment_PrintingOption_PageBreak", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS3CONFIG_')]/ancestor::form[1]/descendant::*[@id='S_BRK_CRIT_CD' and @value='P']");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeDevelopment_PrintingOption_PageBreak.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeDevelopmentForm...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeDevelopmentForm = new Control("EmployeeDevelopmentForm", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS3CONFIG_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeDevelopmentForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EmployeeDevelopmentForm);
formBttn = HSMSETUP_EmployeeDevelopmentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EvaluatorCommentsLink...", Logger.MessageType.INF);
			Control HSMSETUP_EvaluatorCommentsLink = new Control("EvaluatorCommentsLink", "ID", "lnk_1002162_HSMSETUP_RFS1CONFIG");
			CPCommon.AssertEqual(true,HSMSETUP_EvaluatorCommentsLink.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EvaluatorCommentsLink);
HSMSETUP_EvaluatorCommentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EvaluatorComments_PrintingOption_PageBreak...", Logger.MessageType.INF);
			Control HSMSETUP_EvaluatorComments_PrintingOption_PageBreak = new Control("EvaluatorComments_PrintingOption_PageBreak", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS4CONFIG_')]/ancestor::form[1]/descendant::*[@id='S_BRK_CRIT_CD' and @value='P']");
			CPCommon.AssertEqual(true,HSMSETUP_EvaluatorComments_PrintingOption_PageBreak.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EvaluatorCommentsForm...", Logger.MessageType.INF);
			Control HSMSETUP_EvaluatorCommentsForm = new Control("EvaluatorCommentsForm", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS4CONFIG_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMSETUP_EvaluatorCommentsForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EvaluatorCommentsForm);
formBttn = HSMSETUP_EvaluatorCommentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeCommentsLink...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeCommentsLink = new Control("EmployeeCommentsLink", "ID", "lnk_1002163_HSMSETUP_RFS1CONFIG");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeCommentsLink.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EmployeeCommentsLink);
HSMSETUP_EmployeeCommentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeComments_PrintingOption_PageBreak...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeComments_PrintingOption_PageBreak = new Control("EmployeeComments_PrintingOption_PageBreak", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS5CONFIG_')]/ancestor::form[1]/descendant::*[@id='S_BRK_CRIT_CD' and @value='P']");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeComments_PrintingOption_PageBreak.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSETUP] Perfoming VerifyExists on EmployeeCommentsForm...", Logger.MessageType.INF);
			Control HSMSETUP_EmployeeCommentsForm = new Control("EmployeeCommentsForm", "xpath", "//div[starts-with(@id,'pr__HSMSETUP_RFS5CONFIG_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMSETUP_EmployeeCommentsForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_EmployeeCommentsForm);
formBttn = HSMSETUP_EmployeeCommentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HSMSETUP";
							CPCommon.WaitControlDisplayed(HSMSETUP_MainForm);
formBttn = HSMSETUP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

