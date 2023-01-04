 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMWCMC_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Controls", "xpath","//div[@class='navItem'][.='Payroll Controls']").Click();
new Control("Manage Workers' Compensation Modify Codes", "xpath","//div[@class='navItem'][.='Manage Workers' Compensation Modify Codes']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PRMWCMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMWCMC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMWCMC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMWCMC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMWCMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMWCMC] Perfoming VerifyExists on WCModifyCode...", Logger.MessageType.INF);
			Control PRMWCMC_WCModifyCode = new Control("WCModifyCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WC_MOD_CD']");
			CPCommon.AssertEqual(true,PRMWCMC_WCModifyCode.Exists());

											Driver.SessionLogger.WriteLine("Table");


												
				CPCommon.CurrentComponent = "PRMWCMC";
							CPCommon.WaitControlDisplayed(PRMWCMC_MainForm);
IWebElement formBttn = PRMWCMC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMWCMC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMWCMC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMWCMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMWCMC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMWCMC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMWCMC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Workers Compensation Mod");


												
				CPCommon.CurrentComponent = "PRMWCMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMWCMC] Perfoming VerifyExists on WorkersCompensationModifyCodeDetailsLink...", Logger.MessageType.INF);
			Control PRMWCMC_WorkersCompensationModifyCodeDetailsLink = new Control("WorkersCompensationModifyCodeDetailsLink", "ID", "lnk_3837_PRMWCMC_WCMODCD_HDR");
			CPCommon.AssertEqual(true,PRMWCMC_WorkersCompensationModifyCodeDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMWCMC";
							CPCommon.WaitControlDisplayed(PRMWCMC_WorkersCompensationModifyCodeDetailsLink);
PRMWCMC_WorkersCompensationModifyCodeDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMWCMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMWCMC] Perfoming VerifyExists on WorkersCompensationModifyCodeDetailsForm...", Logger.MessageType.INF);
			Control PRMWCMC_WorkersCompensationModifyCodeDetailsForm = new Control("WorkersCompensationModifyCodeDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMWCMC_WCMODPAYTYPE_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMWCMC_WorkersCompensationModifyCodeDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMWCMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMWCMC] Perfoming VerifyExist on WorkersCompensationModifyCodeDetailsFormTable...", Logger.MessageType.INF);
			Control PRMWCMC_WorkersCompensationModifyCodeDetailsFormTable = new Control("WorkersCompensationModifyCodeDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMWCMC_WCMODPAYTYPE_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMWCMC_WorkersCompensationModifyCodeDetailsFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "PRMWCMC";
							CPCommon.WaitControlDisplayed(PRMWCMC_MainForm);
formBttn = PRMWCMC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

