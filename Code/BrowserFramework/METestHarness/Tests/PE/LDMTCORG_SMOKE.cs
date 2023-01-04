 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMTCORG_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Interface", "xpath","//div[@class='navItem'][.='Timesheet Interface']").Click();
new Control("Manage Organizations for Export", "xpath","//div[@class='navItem'][.='Manage Organizations for Export']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMTCORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTCORG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMTCORG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMTCORG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMTCORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTCORG] Perfoming VerifyExists on MassUpdateOrganizations_ExportToDeltekTimeAndExpenseCheck...", Logger.MessageType.INF);
			Control LDMTCORG_MassUpdateOrganizations_ExportToDeltekTimeAndExpenseCheck = new Control("MassUpdateOrganizations_ExportToDeltekTimeAndExpenseCheck", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'TECHK') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,LDMTCORG_MassUpdateOrganizations_ExportToDeltekTimeAndExpenseCheck.Exists());

												
				CPCommon.CurrentComponent = "LDMTCORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTCORG] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control LDMTCORG_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTCORG_TIMECOLLECTION_ORG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMTCORG_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDMTCORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTCORG] Perfoming Close on ChildForm...", Logger.MessageType.INF);
			Control LDMTCORG_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTCORG_TIMECOLLECTION_ORG_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(LDMTCORG_ChildForm);
IWebElement formBttn = LDMTCORG_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

