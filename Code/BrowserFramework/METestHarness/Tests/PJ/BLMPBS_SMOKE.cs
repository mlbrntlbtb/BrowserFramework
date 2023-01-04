 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMPBS_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing History", "xpath","//div[@class='navItem'][.='Billing History']").Click();
new Control("Manage Project Bill Summary", "xpath","//div[@class='navItem'][.='Manage Project Bill Summary']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMPBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPBS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMPBS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMPBS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPBS] Perfoming VerifyExists on Project_Project...", Logger.MessageType.INF);
			Control BLMPBS_Project_Project = new Control("Project_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMPBS_Project_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMPBS";
							CPCommon.WaitControlDisplayed(BLMPBS_MainForm);
IWebElement formBttn = BLMPBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMPBS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMPBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMPBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPBS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMPBS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPBS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORMTABLE");


												
				CPCommon.CurrentComponent = "BLMPBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPBS] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control BLMPBS_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPBS_PROJBILLHS_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPBS_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMPBS";
							CPCommon.WaitControlDisplayed(BLMPBS_MainForm);
formBttn = BLMPBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

