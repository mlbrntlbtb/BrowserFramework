 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMEMPRF_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Performance Review Processing", "xpath","//div[@class='navItem'][.='Performance Review Processing']").Click();
new Control("Manage Employee Review", "xpath","//div[@class='navItem'][.='Manage Employee Review']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMEMPRF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMEMPRF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HSMEMPRF_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HSMEMPRF_Employee.Exists());

											Driver.SessionLogger.WriteLine("Table");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_MainForm);
IWebElement formBttn = HSMEMPRF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSMEMPRF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSMEMPRF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMEMPRF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMEMPRF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Section II");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIILink...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIILink = new Control("SectionIILink", "ID", "lnk_1003598_HSMEMPRF_EMPLRFS1HDR_HDR");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIILink.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_SectionIILink);
HSMEMPRF_SectionIILink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIIEmployeeEvaluationForm...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIIEmployeeEvaluationForm = new Control("SectionIIEmployeeEvaluationForm", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS2HDR_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIIEmployeeEvaluationForm.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIIEmployeeEvaluation_WeightedAverageRating...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIIEmployeeEvaluation_WeightedAverageRating = new Control("SectionIIEmployeeEvaluation_WeightedAverageRating", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS2HDR_')]/ancestor::form[1]/descendant::*[@id='OVERALL_RT']");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIIEmployeeEvaluation_WeightedAverageRating.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionII_EvaluationCriteriaForm...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionII_EvaluationCriteriaForm = new Control("SectionII_EvaluationCriteriaForm", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS2LN_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionII_EvaluationCriteriaForm.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExist on SectionII_EvaluationCriteriaFormTable...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionII_EvaluationCriteriaFormTable = new Control("SectionII_EvaluationCriteriaFormTable", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS2LN_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionII_EvaluationCriteriaFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_SectionII_EvaluationCriteriaForm);
formBttn = HSMEMPRF_SectionII_EvaluationCriteriaForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMEMPRF_SectionII_EvaluationCriteriaForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMEMPRF_SectionII_EvaluationCriteriaForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionII_EvaluationCriteria_OrderNumber...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionII_EvaluationCriteria_OrderNumber = new Control("SectionII_EvaluationCriteria_OrderNumber", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS2LN_')]/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionII_EvaluationCriteria_OrderNumber.Exists());

											Driver.SessionLogger.WriteLine("Section III");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIIILink...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIIILink = new Control("SectionIIILink", "ID", "lnk_4861_HSMEMPRF_EMPLRFS1HDR_HDR");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIIILink.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_SectionIIILink);
HSMEMPRF_SectionIIILink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIIIEmployeeDevelopmentForm...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIIIEmployeeDevelopmentForm = new Control("SectionIIIEmployeeDevelopmentForm", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPL_CHILD_S3_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIIIEmployeeDevelopmentForm.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIIIEmployeeDevelopment_EmployeeDevelopment...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIIIEmployeeDevelopment_EmployeeDevelopment = new Control("SectionIIIEmployeeDevelopment_EmployeeDevelopment", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPL_CHILD_S3_')]/ancestor::form[1]/descendant::*[@id='DEVL_TEXT']");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIIIEmployeeDevelopment_EmployeeDevelopment.Exists());

											Driver.SessionLogger.WriteLine("Section IV");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIVLink...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIVLink = new Control("SectionIVLink", "ID", "lnk_1003601_HSMEMPRF_EMPLRFS1HDR_HDR");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIVLink.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_SectionIVLink);
HSMEMPRF_SectionIVLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIVEvaluatorCommentsForm...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIVEvaluatorCommentsForm = new Control("SectionIVEvaluatorCommentsForm", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS4_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIVEvaluatorCommentsForm.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionIVEvaluatorComments_EvaluatorComments...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionIVEvaluatorComments_EvaluatorComments = new Control("SectionIVEvaluatorComments_EvaluatorComments", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS4_')]/ancestor::form[1]/descendant::*[@id='EMPLR_TEXT']");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionIVEvaluatorComments_EvaluatorComments.Exists());

											Driver.SessionLogger.WriteLine("Section V");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionVLink...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionVLink = new Control("SectionVLink", "ID", "lnk_1003602_HSMEMPRF_EMPLRFS1HDR_HDR");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionVLink.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_SectionVLink);
HSMEMPRF_SectionVLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionVEmployeeCommentsForm...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionVEmployeeCommentsForm = new Control("SectionVEmployeeCommentsForm", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS5_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionVEmployeeCommentsForm.Exists());

												
				CPCommon.CurrentComponent = "HSMEMPRF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMEMPRF] Perfoming VerifyExists on SectionVEmployeeComments_ManagerRating...", Logger.MessageType.INF);
			Control HSMEMPRF_SectionVEmployeeComments_ManagerRating = new Control("SectionVEmployeeComments_ManagerRating", "xpath", "//div[starts-with(@id,'pr__HSMEMPRF_EMPLRFS5_')]/ancestor::form[1]/descendant::*[@id='S_EMPL_EMPLR_CD']");
			CPCommon.AssertEqual(true,HSMEMPRF_SectionVEmployeeComments_ManagerRating.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "HSMEMPRF";
							CPCommon.WaitControlDisplayed(HSMEMPRF_MainForm);
formBttn = HSMEMPRF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

