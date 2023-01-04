 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPBLDRA_SMOKE : TestScript
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
new Control("Build Rate Application Table", "xpath","//div[@class='navItem'][.='Build Rate Application Table']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJPBLDRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPBLDRA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPBLDRA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPBLDRA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPBLDRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPBLDRA] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJPBLDRA_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJPBLDRA_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJPBLDRA";
							CPCommon.WaitControlDisplayed(PJPBLDRA_MainForm);
IWebElement formBttn = PJPBLDRA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJPBLDRA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJPBLDRA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJPBLDRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPBLDRA] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJPBLDRA_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPBLDRA_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PJPBLDRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPBLDRA] Perfoming VerifyExists on SelectionRanges_PoolsLink...", Logger.MessageType.INF);
			Control PJPBLDRA_SelectionRanges_PoolsLink = new Control("SelectionRanges_PoolsLink", "ID", "lnk_2333_PJPBLDRA");
			CPCommon.AssertEqual(true,PJPBLDRA_SelectionRanges_PoolsLink.Exists());

												
				CPCommon.CurrentComponent = "PJPBLDRA";
							CPCommon.WaitControlDisplayed(PJPBLDRA_SelectionRanges_PoolsLink);
PJPBLDRA_SelectionRanges_PoolsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJPBLDRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPBLDRA] Perfoming VerifyExist on Pools_PoolsTable...", Logger.MessageType.INF);
			Control PJPBLDRA_Pools_PoolsTable = new Control("Pools_PoolsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPBLDRA_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPBLDRA_Pools_PoolsTable.Exists());

												
				CPCommon.CurrentComponent = "PJPBLDRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPBLDRA] Perfoming Close on PoolsForm...", Logger.MessageType.INF);
			Control PJPBLDRA_PoolsForm = new Control("PoolsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPBLDRA_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJPBLDRA_PoolsForm);
formBttn = PJPBLDRA_PoolsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJPBLDRA";
							CPCommon.WaitControlDisplayed(PJPBLDRA_MainForm);
formBttn = PJPBLDRA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

