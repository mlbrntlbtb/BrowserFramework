 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MSMSET_SMOKE : TestScript
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
new Control("Master Production Scheduling", "xpath","//div[@class='deptItem'][.='Master Production Scheduling']").Click();
new Control("MPS Controls", "xpath","//div[@class='navItem'][.='MPS Controls']").Click();
new Control("Configure Master Production Scheduling Settings", "xpath","//div[@class='navItem'][.='Configure Master Production Scheduling Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "MSMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MSMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MSMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MSMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMSET] Perfoming VerifyExists on UserDefinedForecastLabel...", Logger.MessageType.INF);
			Control MSMSET_UserDefinedForecastLabel = new Control("UserDefinedForecastLabel", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UDEF_FCST_LBL']");
			CPCommon.AssertEqual(true,MSMSET_UserDefinedForecastLabel.Exists());

											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "MSMSET";
							CPCommon.WaitControlDisplayed(MSMSET_MainForm);
IWebElement formBttn = MSMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MSMSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MSMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MSMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MSMSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MSMSET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MSMSET";
							CPCommon.WaitControlDisplayed(MSMSET_MainForm);
formBttn = MSMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

