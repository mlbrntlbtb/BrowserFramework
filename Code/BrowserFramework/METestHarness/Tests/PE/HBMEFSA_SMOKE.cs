 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMEFSA_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee FSA/HSA Information", "xpath","//div[@class='navItem'][.='Employee FSA/HSA Information']").Click();
new Control("Manage Employee Dependent Care FSA Elections", "xpath","//div[@class='navItem'][.='Manage Employee Dependent Care FSA Elections']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HBMEFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEFSA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMEFSA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMEFSA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEFSA] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HBMEFSA_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HBMEFSA_Employee.Exists());

												
				CPCommon.CurrentComponent = "HBMEFSA";
							CPCommon.WaitControlDisplayed(HBMEFSA_MainForm);
IWebElement formBttn = HBMEFSA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMEFSA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMEFSA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMEFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEFSA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMEFSA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEFSA_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Update Deduction Form");


												
				CPCommon.CurrentComponent = "HBMEFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEFSA] Perfoming VerifyExists on UpdateDeductionLink...", Logger.MessageType.INF);
			Control HBMEFSA_UpdateDeductionLink = new Control("UpdateDeductionLink", "ID", "lnk_5575_HBMEFSA_HBDEPFSAELEC");
			CPCommon.AssertEqual(true,HBMEFSA_UpdateDeductionLink.Exists());

												
				CPCommon.CurrentComponent = "HBMEFSA";
							CPCommon.WaitControlDisplayed(HBMEFSA_UpdateDeductionLink);
HBMEFSA_UpdateDeductionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBMEFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEFSA] Perfoming VerifyExists on UpdateDeductionForm...", Logger.MessageType.INF);
			Control HBMEFSA_UpdateDeductionForm = new Control("UpdateDeductionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEFSA_UPDATEDED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMEFSA_UpdateDeductionForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEFSA] Perfoming VerifyExists on UpdateDeductionForm_PayCycle...", Logger.MessageType.INF);
			Control HBMEFSA_UpdateDeductionForm_PayCycle = new Control("UpdateDeductionForm_PayCycle", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEFSA_UPDATEDED_']/ancestor::form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,HBMEFSA_UpdateDeductionForm_PayCycle.Exists());

												
				CPCommon.CurrentComponent = "HBMEFSA";
							CPCommon.WaitControlDisplayed(HBMEFSA_UpdateDeductionForm);
formBttn = HBMEFSA_UpdateDeductionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HBMEFSA";
							CPCommon.WaitControlDisplayed(HBMEFSA_MainForm);
formBttn = HBMEFSA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

