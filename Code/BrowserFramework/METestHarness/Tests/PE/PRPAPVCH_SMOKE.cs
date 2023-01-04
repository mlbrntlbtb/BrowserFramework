 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPAPVCH_SMOKE : TestScript
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
new Control("Payroll Closing", "xpath","//div[@class='navItem'][.='Payroll Closing']").Click();
new Control("Create Accounts Payable Vouchers", "xpath","//div[@class='navItem'][.='Create Accounts Payable Vouchers']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPAPVCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPAPVCH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRPAPVCH_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPAPVCH_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on StateNonContiguousLink...", Logger.MessageType.INF);
			Control PRPAPVCH_StateNonContiguousLink = new Control("StateNonContiguousLink", "ID", "lnk_15110_PRPAPVCH_PARAM");
			CPCommon.AssertEqual(true,PRPAPVCH_StateNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_StateNonContiguousLink);
PRPAPVCH_StateNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on StateNonContiguousForm...", Logger.MessageType.INF);
			Control PRPAPVCH_StateNonContiguousForm = new Control("StateNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPAPVCH_NCR_STATECD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPAPVCH_StateNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExist on StateNonContiguousTable...", Logger.MessageType.INF);
			Control PRPAPVCH_StateNonContiguousTable = new Control("StateNonContiguousTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPAPVCH_NCR_STATECD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPAPVCH_StateNonContiguousTable.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_StateNonContiguousForm);
IWebElement formBttn = PRPAPVCH_StateNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on LocalitiesNonContiguousLink...", Logger.MessageType.INF);
			Control PRPAPVCH_LocalitiesNonContiguousLink = new Control("LocalitiesNonContiguousLink", "ID", "lnk_15111_PRPAPVCH_PARAM");
			CPCommon.AssertEqual(true,PRPAPVCH_LocalitiesNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_LocalitiesNonContiguousLink);
PRPAPVCH_LocalitiesNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on LocalitiesNonContiguousForm...", Logger.MessageType.INF);
			Control PRPAPVCH_LocalitiesNonContiguousForm = new Control("LocalitiesNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPAPVCH_NCR_LOCALCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPAPVCH_LocalitiesNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExist on LocalitiesNonContiguousTable...", Logger.MessageType.INF);
			Control PRPAPVCH_LocalitiesNonContiguousTable = new Control("LocalitiesNonContiguousTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPAPVCH_NCR_LOCALCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPAPVCH_LocalitiesNonContiguousTable.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_LocalitiesNonContiguousForm);
formBttn = PRPAPVCH_LocalitiesNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on DeductionsContributionsNonContiguousLink...", Logger.MessageType.INF);
			Control PRPAPVCH_DeductionsContributionsNonContiguousLink = new Control("DeductionsContributionsNonContiguousLink", "ID", "lnk_15112_PRPAPVCH_PARAM");
			CPCommon.AssertEqual(true,PRPAPVCH_DeductionsContributionsNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_DeductionsContributionsNonContiguousLink);
PRPAPVCH_DeductionsContributionsNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExists on DeductionsContributionsNonContiguousForm...", Logger.MessageType.INF);
			Control PRPAPVCH_DeductionsContributionsNonContiguousForm = new Control("DeductionsContributionsNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPAPVCH_NCR_DEDCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPAPVCH_DeductionsContributionsNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExist on DeductionsContributionsNonContiguousTable...", Logger.MessageType.INF);
			Control PRPAPVCH_DeductionsContributionsNonContiguousTable = new Control("DeductionsContributionsNonContiguousTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPAPVCH_NCR_DEDCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPAPVCH_DeductionsContributionsNonContiguousTable.Exists());

												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_DeductionsContributionsNonContiguousForm);
formBttn = PRPAPVCH_DeductionsContributionsNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_MainForm);
formBttn = PRPAPVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPAPVCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPAPVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPAPVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAPVCH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRPAPVCH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPAPVCH_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRPAPVCH";
							CPCommon.WaitControlDisplayed(PRPAPVCH_MainForm);
formBttn = PRPAPVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

