 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COMLNKLC_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Consolidations", "xpath","//div[@class='deptItem'][.='Consolidations']").Click();
new Control("Consolidations Controls", "xpath","//div[@class='navItem'][.='Consolidations Controls']").Click();
new Control("Link Consolidation Locations", "xpath","//div[@class='navItem'][.='Link Consolidation Locations']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "COMLNKLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMLNKLC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control COMLNKLC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMLNKLC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Customer Non Contiguous Ranges");


												
				CPCommon.CurrentComponent = "COMLNKLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMLNKLC] Perfoming Click on ConsolidationPercentageLink...", Logger.MessageType.INF);
			Control COMLNKLC_ConsolidationPercentageLink = new Control("ConsolidationPercentageLink", "ID", "lnk_3717_COMLNKLC_CONSLINKLOCMAP_HDR");
			CPCommon.WaitControlDisplayed(COMLNKLC_ConsolidationPercentageLink);
COMLNKLC_ConsolidationPercentageLink.Click(1.5);


												
				CPCommon.CurrentComponent = "COMLNKLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMLNKLC] Perfoming VerifyExist on ConsolidationPercentageFormTable...", Logger.MessageType.INF);
			Control COMLNKLC_ConsolidationPercentageFormTable = new Control("ConsolidationPercentageFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__COMLNKLC_CONSLNKLOCPCT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMLNKLC_ConsolidationPercentageFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMLNKLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMLNKLC] Perfoming Close on ConsolidationPercentageForm...", Logger.MessageType.INF);
			Control COMLNKLC_ConsolidationPercentageForm = new Control("ConsolidationPercentageForm", "xpath", "//div[translate(@id,'0123456789','')='pr__COMLNKLC_CONSLNKLOCPCT_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(COMLNKLC_ConsolidationPercentageForm);
IWebElement formBttn = COMLNKLC_ConsolidationPercentageForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "COMLNKLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMLNKLC] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control COMLNKLC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(COMLNKLC_MainForm);
formBttn = COMLNKLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

