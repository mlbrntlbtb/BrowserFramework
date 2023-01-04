 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMMC_SMOKE : TestScript
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
new Control("Deductions", "xpath","//div[@class='navItem'][.='Deductions']").Click();
new Control("Manage Deduction Modify Codes", "xpath","//div[@class='navItem'][.='Manage Deduction Modify Codes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMMC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMMC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMMC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMMC] Perfoming VerifyExists on DeductionModifyCode...", Logger.MessageType.INF);
			Control PRMMC_DeductionModifyCode = new Control("DeductionModifyCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DED_MOD_CD']");
			CPCommon.AssertEqual(true,PRMMC_DeductionModifyCode.Exists());

												
				CPCommon.CurrentComponent = "PRMMC";
							CPCommon.WaitControlDisplayed(PRMMC_MainForm);
IWebElement formBttn = PRMMC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMMC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMMC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMMC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMMC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMMC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Modify Code Details Link");


												
				CPCommon.CurrentComponent = "PRMMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMMC] Perfoming Click on ModifyCodeDetailsLink...", Logger.MessageType.INF);
			Control PRMMC_ModifyCodeDetailsLink = new Control("ModifyCodeDetailsLink", "ID", "lnk_3822_PRMMC_DEDMOD_HDR");
			CPCommon.WaitControlDisplayed(PRMMC_ModifyCodeDetailsLink);
PRMMC_ModifyCodeDetailsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PRMMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMMC] Perfoming VerifyExist on ModifyCodeDetailsFormTable...", Logger.MessageType.INF);
			Control PRMMC_ModifyCodeDetailsFormTable = new Control("ModifyCodeDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMMC_DEDMODPAYTYPE_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMMC_ModifyCodeDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMMC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMMC] Perfoming Click on ModifyCodeDetails_Ok...", Logger.MessageType.INF);
			Control PRMMC_ModifyCodeDetails_Ok = new Control("ModifyCodeDetails_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMMC_DEDMODPAYTYPE_CTW_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRMMC_ModifyCodeDetails_Ok);
if (PRMMC_ModifyCodeDetails_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRMMC_ModifyCodeDetails_Ok.Click(5,5);
else PRMMC_ModifyCodeDetails_Ok.Click(4.5);


											Driver.SessionLogger.WriteLine("Close");


												
				CPCommon.CurrentComponent = "PRMMC";
							CPCommon.WaitControlDisplayed(PRMMC_MainForm);
formBttn = PRMMC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

