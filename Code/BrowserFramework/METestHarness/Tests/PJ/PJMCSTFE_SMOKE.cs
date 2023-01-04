 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMCSTFE_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Ceilings", "xpath","//div[@class='navItem'][.='Project Ceilings']").Click();
new Control("Manage Cost Fee Overrides", "xpath","//div[@class='navItem'][.='Manage Cost Fee Overrides']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMCSTFE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSTFE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMCSTFE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMCSTFE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMCSTFE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSTFE] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMCSTFE_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMCSTFE_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMCSTFE";
							CPCommon.WaitControlDisplayed(PJMCSTFE_MainForm);
IWebElement formBttn = PJMCSTFE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMCSTFE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMCSTFE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMCSTFE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSTFE] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMCSTFE_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCSTFE_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCSTFE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSTFE] Perfoming VerifyExist on ProjectDirectCostCeilingDetailsTable...", Logger.MessageType.INF);
			Control PJMCSTFE_ProjectDirectCostCeilingDetailsTable = new Control("ProjectDirectCostCeilingDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSTFE_OVRIDEFEEONDIR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCSTFE_ProjectDirectCostCeilingDetailsTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMCSTFE";
							CPCommon.WaitControlDisplayed(PJMCSTFE_MainForm);
formBttn = PJMCSTFE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

