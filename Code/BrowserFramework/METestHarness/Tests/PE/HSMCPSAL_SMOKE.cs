 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMCPSAL_SMOKE : TestScript
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
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Compensation Plans", "xpath","//div[@class='navItem'][.='Compensation Plans']").Click();
new Control("Manage Compensation Plan Salary Ranges", "xpath","//div[@class='navItem'][.='Manage Compensation Plan Salary Ranges']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMCPSAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMCPSAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on CompensationPlan...", Logger.MessageType.INF);
			Control HSMCPSAL_CompensationPlan = new Control("CompensationPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,HSMCPSAL_CompensationPlan.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
							CPCommon.WaitControlDisplayed(HSMCPSAL_MainForm);
IWebElement formBttn = HSMCPSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSMCPSAL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSMCPSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMCPSAL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMCPSAL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on AutoFillPercentCriteria_SalaryRangesLink...", Logger.MessageType.INF);
			Control HSMCPSAL_AutoFillPercentCriteria_SalaryRangesLink = new Control("AutoFillPercentCriteria_SalaryRangesLink", "ID", "lnk_1000673_HSMCPSAL_COMPPLANHRD_HDR");
			CPCommon.AssertEqual(true,HSMCPSAL_AutoFillPercentCriteria_SalaryRangesLink.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
							CPCommon.WaitControlDisplayed(HSMCPSAL_AutoFillPercentCriteria_SalaryRangesLink);
HSMCPSAL_AutoFillPercentCriteria_SalaryRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExist on SalaryRangesFormTable...", Logger.MessageType.INF);
			Control HSMCPSAL_SalaryRangesFormTable = new Control("SalaryRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMCPSAL_COMPLANLN_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMCPSAL_SalaryRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming ClickButton on SalaryRangesForm...", Logger.MessageType.INF);
			Control HSMCPSAL_SalaryRangesForm = new Control("SalaryRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMCPSAL_COMPLANLN_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HSMCPSAL_SalaryRangesForm);
formBttn = HSMCPSAL_SalaryRangesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMCPSAL_SalaryRangesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMCPSAL_SalaryRangesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on SalaryRanges_GradeStep...", Logger.MessageType.INF);
			Control HSMCPSAL_SalaryRanges_GradeStep = new Control("SalaryRanges_GradeStep", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMCPSAL_COMPLANLN_TBL_']/ancestor::form[1]/descendant::*[@id='SAL_GRADE_CD']");
			CPCommon.AssertEqual(true,HSMCPSAL_SalaryRanges_GradeStep.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on SalaryRanges_RecalculationLink...", Logger.MessageType.INF);
			Control HSMCPSAL_SalaryRanges_RecalculationLink = new Control("SalaryRanges_RecalculationLink", "ID", "lnk_1001416_HSMCPSAL_COMPLANLN_TBL");
			CPCommon.AssertEqual(true,HSMCPSAL_SalaryRanges_RecalculationLink.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
							CPCommon.WaitControlDisplayed(HSMCPSAL_SalaryRanges_RecalculationLink);
HSMCPSAL_SalaryRanges_RecalculationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on SalaryRanges_RecalculationForm...", Logger.MessageType.INF);
			Control HSMCPSAL_SalaryRanges_RecalculationForm = new Control("SalaryRanges_RecalculationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMCPSAL_RECALC_SUBTASK_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMCPSAL_SalaryRanges_RecalculationForm.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSAL] Perfoming VerifyExists on SalaryRanges_Recalculation_StartAtRowPosition...", Logger.MessageType.INF);
			Control HSMCPSAL_SalaryRanges_Recalculation_StartAtRowPosition = new Control("SalaryRanges_Recalculation_StartAtRowPosition", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMCPSAL_RECALC_SUBTASK_']/ancestor::form[1]/descendant::*[@id='ROW_POSITION']");
			CPCommon.AssertEqual(true,HSMCPSAL_SalaryRanges_Recalculation_StartAtRowPosition.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSAL";
							CPCommon.WaitControlDisplayed(HSMCPSAL_SalaryRanges_RecalculationForm);
formBttn = HSMCPSAL_SalaryRanges_RecalculationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMCPSAL";
							CPCommon.WaitControlDisplayed(HSMCPSAL_MainForm);
formBttn = HSMCPSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

