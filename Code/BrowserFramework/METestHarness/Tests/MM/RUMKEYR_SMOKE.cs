 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMKEYR_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Routings Controls", "xpath","//div[@class='navItem'][.='Routings Controls']").Click();
new Control("Manage Key Resources", "xpath","//div[@class='navItem'][.='Manage Key Resources']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "RUMKEYR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMKEYR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUMKEYR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUMKEYR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUMKEYR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMKEYR] Perfoming VerifyExists on KeyResource...", Logger.MessageType.INF);
			Control RUMKEYR_KeyResource = new Control("KeyResource", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='KEY_RSRCE_ID']");
			CPCommon.AssertEqual(true,RUMKEYR_KeyResource.Exists());

												
				CPCommon.CurrentComponent = "RUMKEYR";
							CPCommon.WaitControlDisplayed(RUMKEYR_MainForm);
IWebElement formBttn = RUMKEYR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RUMKEYR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RUMKEYR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RUMKEYR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMKEYR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RUMKEYR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMKEYR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CAPACITYDETAILS");


												
				CPCommon.CurrentComponent = "RUMKEYR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMKEYR] Perfoming VerifyExist on CapacityDetailsFormTable...", Logger.MessageType.INF);
			Control RUMKEYR_CapacityDetailsFormTable = new Control("CapacityDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMKEYR_KEYRSRCECAP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMKEYR_CapacityDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMKEYR";
							CPCommon.WaitControlDisplayed(RUMKEYR_MainForm);
formBttn = RUMKEYR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

