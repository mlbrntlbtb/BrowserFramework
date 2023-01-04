 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPCDCTS_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Ceridian Interface", "xpath","//div[@class='navItem'][.='Ceridian Interface']").Click();
new Control("Export Timesheets to Ceridian", "xpath","//div[@class='navItem'][.='Export Timesheets to Ceridian']").Click();


												
				CPCommon.CurrentComponent = "AOPCDCTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCDCTS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPCDCTS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPCDCTS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPCDCTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCDCTS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPCDCTS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPCDCTS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPCDCTS";
							CPCommon.WaitControlDisplayed(AOPCDCTS_MainForm);
IWebElement formBttn = AOPCDCTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

