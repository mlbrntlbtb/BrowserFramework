 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMRTAX_SMOKE : TestScript
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
new Control("Manage Reciprocal State Taxes", "xpath","//div[@class='navItem'][.='Manage Reciprocal State Taxes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMRTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMRTAX] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMRTAX_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMRTAX_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMRTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMRTAX] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control PRMRTAX_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,PRMRTAX_State.Exists());

												
				CPCommon.CurrentComponent = "PRMRTAX";
							CPCommon.WaitControlDisplayed(PRMRTAX_MainForm);
IWebElement formBttn = PRMRTAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMRTAX_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMRTAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMRTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMRTAX] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMRTAX_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMRTAX_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Reciprocal State Form");


												
				CPCommon.CurrentComponent = "PRMRTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMRTAX] Perfoming VerifyExists on ReciprocalStateLink...", Logger.MessageType.INF);
			Control PRMRTAX_ReciprocalStateLink = new Control("ReciprocalStateLink", "ID", "lnk_5439_PRMRTAX_STATERECPINFO_HDR");
			CPCommon.AssertEqual(true,PRMRTAX_ReciprocalStateLink.Exists());

												
				CPCommon.CurrentComponent = "PRMRTAX";
							CPCommon.WaitControlDisplayed(PRMRTAX_ReciprocalStateLink);
PRMRTAX_ReciprocalStateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMRTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMRTAX] Perfoming VerifyExists on ReciprocalStateForm...", Logger.MessageType.INF);
			Control PRMRTAX_ReciprocalStateForm = new Control("ReciprocalStateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMRTAX_STATERECPTBL_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMRTAX_ReciprocalStateForm.Exists());

												
				CPCommon.CurrentComponent = "PRMRTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMRTAX] Perfoming VerifyExist on ReciprocalState_ReciprocalStateTable...", Logger.MessageType.INF);
			Control PRMRTAX_ReciprocalState_ReciprocalStateTable = new Control("ReciprocalState_ReciprocalStateTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMRTAX_STATERECPTBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMRTAX_ReciprocalState_ReciprocalStateTable.Exists());

												
				CPCommon.CurrentComponent = "PRMRTAX";
							CPCommon.WaitControlDisplayed(PRMRTAX_ReciprocalStateForm);
formBttn = PRMRTAX_ReciprocalStateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMRTAX";
							CPCommon.WaitControlDisplayed(PRMRTAX_MainForm);
formBttn = PRMRTAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

