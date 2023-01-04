 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class IWMPROJ_SMOKE : TestScript
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
new Control("Inter-Company Work Orders", "xpath","//div[@class='deptItem'][.='Inter-Company Work Orders']").Click();
new Control("Inter-Company Work Orders Processing", "xpath","//div[@class='navItem'][.='Inter-Company Work Orders Processing']").Click();
new Control("Manage IWO Projects", "xpath","//div[@class='navItem'][.='Manage IWO Projects']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "IWMPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMPROJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control IWMPROJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,IWMPROJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "IWMPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMPROJ] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control IWMPROJ_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,IWMPROJ_Project.Exists());

												
				CPCommon.CurrentComponent = "IWMPROJ";
							CPCommon.WaitControlDisplayed(IWMPROJ_MainForm);
IWebElement formBttn = IWMPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWMPROJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWMPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "IWMPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMPROJ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control IWMPROJ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMPROJ_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("IWO Posting Accounts");


												
				CPCommon.CurrentComponent = "IWMPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMPROJ] Perfoming VerifyExists on IWOPostingAccountsLink...", Logger.MessageType.INF);
			Control IWMPROJ_IWOPostingAccountsLink = new Control("IWOPostingAccountsLink", "ID", "lnk_3771_IWMPROJ_IWOPROJ_HDR");
			CPCommon.AssertEqual(true,IWMPROJ_IWOPostingAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "IWMPROJ";
							CPCommon.WaitControlDisplayed(IWMPROJ_IWOPostingAccountsLink);
IWMPROJ_IWOPostingAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "IWMPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMPROJ] Perfoming VerifyExist on IWOPostingAccountsFormTable...", Logger.MessageType.INF);
			Control IWMPROJ_IWOPostingAccountsFormTable = new Control("IWOPostingAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMPROJ_IWOPROJACCTS_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMPROJ_IWOPostingAccountsFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "IWMPROJ";
							CPCommon.WaitControlDisplayed(IWMPROJ_MainForm);
formBttn = IWMPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

