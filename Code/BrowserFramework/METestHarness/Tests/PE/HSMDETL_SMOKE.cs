 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMDETL_SMOKE : TestScript
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
new Control("Manage Detail Job Titles", "xpath","//div[@class='navItem'][.='Manage Detail Job Titles']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMDETL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMDETL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMDETL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMDETL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMDETL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMDETL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control HSMDETL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(HSMDETL_MainForm);
IWebElement formBttn = HSMDETL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMDETL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMDETL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMDETL";
							CPCommon.AssertEqual(true,HSMDETL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "HSMDETL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMDETL] Perfoming VerifyExists on DetailJobTitleCode...", Logger.MessageType.INF);
			Control HSMDETL_DetailJobTitleCode = new Control("DetailJobTitleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DETL_JOB_CD']");
			CPCommon.AssertEqual(true,HSMDETL_DetailJobTitleCode.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMDETL";
							CPCommon.WaitControlDisplayed(HSMDETL_MainForm);
formBttn = HSMDETL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

