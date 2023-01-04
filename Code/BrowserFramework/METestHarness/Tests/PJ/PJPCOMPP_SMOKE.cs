 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCOMPP_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost Pool Processing", "xpath","//div[@class='navItem'][.='Cost Pool Processing']").Click();
new Control("Compute/Print Pool Rates", "xpath","//div[@class='navItem'][.='Compute/Print Pool Rates']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPCOMPP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCOMPP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJPCOMPP_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJPCOMPP_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJPCOMPP";
							CPCommon.WaitControlDisplayed(PJPCOMPP_MainForm);
IWebElement formBttn = PJPCOMPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJPCOMPP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJPCOMPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJPCOMPP_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCOMPP_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Employees");


												
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming Click on PoolsLink...", Logger.MessageType.INF);
			Control PJPCOMPP_PoolsLink = new Control("PoolsLink", "ID", "lnk_2407_PJPCOMPP");
			CPCommon.WaitControlDisplayed(PJPCOMPP_PoolsLink);
PJPCOMPP_PoolsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming VerifyExist on PoolsTable...", Logger.MessageType.INF);
			Control PJPCOMPP_PoolsTable = new Control("PoolsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCOMPP_POOLS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCOMPP_PoolsTable.Exists());

												
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming Close on PoolsForm...", Logger.MessageType.INF);
			Control PJPCOMPP_PoolsForm = new Control("PoolsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCOMPP_POOLS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJPCOMPP_PoolsForm);
formBttn = PJPCOMPP_PoolsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Assign PLC to Employee Work Force");


												
				CPCommon.CurrentComponent = "PJPCOMPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPP] Perfoming Click on PurgeOldSIELink...", Logger.MessageType.INF);
			Control PJPCOMPP_PurgeOldSIELink = new Control("PurgeOldSIELink", "ID", "lnk_2432_PJPCOMPP");
			CPCommon.WaitControlDisplayed(PJPCOMPP_PurgeOldSIELink);
PJPCOMPP_PurgeOldSIELink.Click(1.5);


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJPCOMPP";
							CPCommon.WaitControlDisplayed(PJPCOMPP_MainForm);
formBttn = PJPCOMPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

