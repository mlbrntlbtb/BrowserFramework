 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMMFSA_SMOKE : TestScript
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
new Control("Manage Employee Medical Care FSA Elections", "xpath","//div[@class='navItem'][.='Manage Employee Medical Care FSA Elections']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HBMMFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMFSA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMMFSA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMMFSA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMMFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMFSA] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HBMMFSA_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HBMMFSA_Employee.Exists());

												
				CPCommon.CurrentComponent = "HBMMFSA";
							CPCommon.WaitControlDisplayed(HBMMFSA_MainForm);
IWebElement formBttn = HBMMFSA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMMFSA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMMFSA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMMFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMFSA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMMFSA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMMFSA_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Update Deduction Form");


												
				CPCommon.CurrentComponent = "HBMMFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMFSA] Perfoming VerifyExists on UpdateDeductionLink...", Logger.MessageType.INF);
			Control HBMMFSA_UpdateDeductionLink = new Control("UpdateDeductionLink", "ID", "lnk_1005700_HBMMFSA_HBMEDFSAELEC_HDR");
			CPCommon.AssertEqual(true,HBMMFSA_UpdateDeductionLink.Exists());

												
				CPCommon.CurrentComponent = "HBMMFSA";
							CPCommon.WaitControlDisplayed(HBMMFSA_UpdateDeductionLink);
HBMMFSA_UpdateDeductionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBMMFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMFSA] Perfoming VerifyExists on UpdateDeductionForm...", Logger.MessageType.INF);
			Control HBMMFSA_UpdateDeductionForm = new Control("UpdateDeductionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMMFSA_HBMEDFSAELEC_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMMFSA_UpdateDeductionForm.Exists());

												
				CPCommon.CurrentComponent = "HBMMFSA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMFSA] Perfoming VerifyExists on UpdateDeductionForm_PayCycle...", Logger.MessageType.INF);
			Control HBMMFSA_UpdateDeductionForm_PayCycle = new Control("UpdateDeductionForm_PayCycle", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMMFSA_HBMEDFSAELEC_CHLD_']/ancestor::form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,HBMMFSA_UpdateDeductionForm_PayCycle.Exists());

												
				CPCommon.CurrentComponent = "HBMMFSA";
							CPCommon.WaitControlDisplayed(HBMMFSA_UpdateDeductionForm);
formBttn = HBMMFSA_UpdateDeductionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HBMMFSA";
							CPCommon.WaitControlDisplayed(HBMMFSA_MainForm);
formBttn = HBMMFSA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

