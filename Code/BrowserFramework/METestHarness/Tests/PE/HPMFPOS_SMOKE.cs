 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMFPOS_SMOKE : TestScript
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
new Control("Job Titles", "xpath","//div[@class='navItem'][.='Job Titles']").Click();
new Control("Manage Functional Position Descriptions", "xpath","//div[@class='navItem'][.='Manage Functional Position Descriptions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMFPOS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMFPOS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExists on FunctionalJobTitle...", Logger.MessageType.INF);
			Control HPMFPOS_FunctionalJobTitle = new Control("FunctionalJobTitle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FUNC_JOB_CD']");
			CPCommon.AssertEqual(true,HPMFPOS_FunctionalJobTitle.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
							CPCommon.WaitControlDisplayed(HPMFPOS_MainForm);
IWebElement formBttn = HPMFPOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMFPOS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMFPOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMFPOS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMFPOS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Skills Form");


												
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExists on SkillsLink...", Logger.MessageType.INF);
			Control HPMFPOS_SkillsLink = new Control("SkillsLink", "ID", "lnk_1001836_HPMFPOS_HFUNCPOSDESC_HDR");
			CPCommon.AssertEqual(true,HPMFPOS_SkillsLink.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
							CPCommon.WaitControlDisplayed(HPMFPOS_SkillsLink);
HPMFPOS_SkillsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExists on SkillsForm...", Logger.MessageType.INF);
			Control HPMFPOS_SkillsForm = new Control("SkillsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMFPOS_HFUNCSKILL_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMFPOS_SkillsForm.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExist on SkillsFormTable...", Logger.MessageType.INF);
			Control HPMFPOS_SkillsFormTable = new Control("SkillsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMFPOS_HFUNCSKILL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMFPOS_SkillsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
							CPCommon.WaitControlDisplayed(HPMFPOS_SkillsForm);
formBttn = HPMFPOS_SkillsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Degrees Form");


												
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExists on DegreesLink...", Logger.MessageType.INF);
			Control HPMFPOS_DegreesLink = new Control("DegreesLink", "ID", "lnk_1001837_HPMFPOS_HFUNCPOSDESC_HDR");
			CPCommon.AssertEqual(true,HPMFPOS_DegreesLink.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
							CPCommon.WaitControlDisplayed(HPMFPOS_DegreesLink);
HPMFPOS_DegreesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExists on DegreesForm...", Logger.MessageType.INF);
			Control HPMFPOS_DegreesForm = new Control("DegreesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMFPOS_HFUNCDEGREE_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMFPOS_DegreesForm.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMFPOS] Perfoming VerifyExist on DegreesFormTable...", Logger.MessageType.INF);
			Control HPMFPOS_DegreesFormTable = new Control("DegreesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMFPOS_HFUNCDEGREE_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMFPOS_DegreesFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMFPOS";
							CPCommon.WaitControlDisplayed(HPMFPOS_DegreesForm);
formBttn = HPMFPOS_DegreesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPMFPOS";
							CPCommon.WaitControlDisplayed(HPMFPOS_MainForm);
formBttn = HPMFPOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

