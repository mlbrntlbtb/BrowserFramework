 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMDPOS_SMOKE : TestScript
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
new Control("Manage Detail Position Descriptions", "xpath","//div[@class='navItem'][.='Manage Detail Position Descriptions']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMDPOS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMDPOS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExists on DetailJobTitle...", Logger.MessageType.INF);
			Control HPMDPOS_DetailJobTitle = new Control("DetailJobTitle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DETL_JOB_CD']");
			CPCommon.AssertEqual(true,HPMDPOS_DetailJobTitle.Exists());

												
				CPCommon.CurrentComponent = "HPMDPOS";
							CPCommon.WaitControlDisplayed(HPMDPOS_MainForm);
IWebElement formBttn = HPMDPOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMDPOS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMDPOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMDPOS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMDPOS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExists on SkillsForm...", Logger.MessageType.INF);
			Control HPMDPOS_SkillsForm = new Control("SkillsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMDPOS_HDETLSKILL_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMDPOS_SkillsForm.Exists());

												
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExist on SkillsFormTable...", Logger.MessageType.INF);
			Control HPMDPOS_SkillsFormTable = new Control("SkillsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMDPOS_HDETLSKILL_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMDPOS_SkillsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExists on DegreesForm...", Logger.MessageType.INF);
			Control HPMDPOS_DegreesForm = new Control("DegreesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMDPOS_HDETLDEGREE_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMDPOS_DegreesForm.Exists());

												
				CPCommon.CurrentComponent = "HPMDPOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMDPOS] Perfoming VerifyExist on DegreesFormTable...", Logger.MessageType.INF);
			Control HPMDPOS_DegreesFormTable = new Control("DegreesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMDPOS_HDETLDEGREE_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMDPOS_DegreesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HPMDPOS";
							CPCommon.WaitControlDisplayed(HPMDPOS_MainForm);
formBttn = HPMDPOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

