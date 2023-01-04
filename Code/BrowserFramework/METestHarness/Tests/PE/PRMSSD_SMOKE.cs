 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMSSD_SMOKE : TestScript
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
new Control("State Taxes", "xpath","//div[@class='navItem'][.='State Taxes']").Click();
new Control("Manage State Standard Deductions", "xpath","//div[@class='navItem'][.='Manage State Standard Deductions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMSSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSSD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMSSD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMSSD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSSD] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control PRMSSD_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,PRMSSD_State.Exists());

												
				CPCommon.CurrentComponent = "PRMSSD";
							CPCommon.WaitControlDisplayed(PRMSSD_MainForm);
IWebElement formBttn = PRMSSD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMSSD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMSSD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMSSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSSD] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMSSD_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSSD_MainTable.Exists());

											Driver.SessionLogger.WriteLine("State Standard Deduction Table Form");


												
				CPCommon.CurrentComponent = "PRMSSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSSD] Perfoming VerifyExists on StateStandardDeductionTableLink...", Logger.MessageType.INF);
			Control PRMSSD_StateStandardDeductionTableLink = new Control("StateStandardDeductionTableLink", "ID", "lnk_3938_PRMSSD_STATESTDDED_HDR");
			CPCommon.AssertEqual(true,PRMSSD_StateStandardDeductionTableLink.Exists());

												
				CPCommon.CurrentComponent = "PRMSSD";
							CPCommon.WaitControlDisplayed(PRMSSD_StateStandardDeductionTableLink);
PRMSSD_StateStandardDeductionTableLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMSSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSSD] Perfoming VerifyExists on StateStandardDeductionTableForm...", Logger.MessageType.INF);
			Control PRMSSD_StateStandardDeductionTableForm = new Control("StateStandardDeductionTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSSD_STATESTDDED_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMSSD_StateStandardDeductionTableForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSSD] Perfoming VerifyExist on StateStandardDeductionTableTable...", Logger.MessageType.INF);
			Control PRMSSD_StateStandardDeductionTableTable = new Control("StateStandardDeductionTableTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSSD_STATESTDDED_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSSD_StateStandardDeductionTableTable.Exists());

												
				CPCommon.CurrentComponent = "PRMSSD";
							CPCommon.WaitControlDisplayed(PRMSSD_StateStandardDeductionTableForm);
formBttn = PRMSSD_StateStandardDeductionTableForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMSSD";
							CPCommon.WaitControlDisplayed(PRMSSD_MainForm);
formBttn = PRMSSD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

