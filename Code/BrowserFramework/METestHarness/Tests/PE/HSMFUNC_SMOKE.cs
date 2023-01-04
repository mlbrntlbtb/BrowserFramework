 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMFUNC_SMOKE : TestScript
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
new Control("Manage Functional Job Titles", "xpath","//div[@class='navItem'][.='Manage Functional Job Titles']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMFUNC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMFUNC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMFUNC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMFUNC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMFUNC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMFUNC] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control HSMFUNC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(HSMFUNC_MainForm);
IWebElement formBttn = HSMFUNC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMFUNC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMFUNC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMFUNC";
							CPCommon.AssertEqual(true,HSMFUNC_MainForm.Exists());

													
				CPCommon.CurrentComponent = "HSMFUNC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMFUNC] Perfoming VerifyExists on FunctionalJobTitleCode...", Logger.MessageType.INF);
			Control HSMFUNC_FunctionalJobTitleCode = new Control("FunctionalJobTitleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FUNC_JOB_CD']");
			CPCommon.AssertEqual(true,HSMFUNC_FunctionalJobTitleCode.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMFUNC";
							CPCommon.WaitControlDisplayed(HSMFUNC_MainForm);
formBttn = HSMFUNC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

