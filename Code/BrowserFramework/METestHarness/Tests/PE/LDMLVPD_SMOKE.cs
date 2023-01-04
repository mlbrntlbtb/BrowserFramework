 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMLVPD_SMOKE : TestScript
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
new Control("Leave", "xpath","//div[@class='deptItem'][.='Leave']").Click();
new Control("Leave Processing", "xpath","//div[@class='navItem'][.='Leave Processing']").Click();
new Control("Manage Leave Periods", "xpath","//div[@class='navItem'][.='Manage Leave Periods']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMLVPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMLVPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMLVPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLVPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVPD] Perfoming VerifyExists on LeaveCycleCode...", Logger.MessageType.INF);
			Control LDMLVPD_LeaveCycleCode = new Control("LeaveCycleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LV_PD_CD']");
			CPCommon.AssertEqual(true,LDMLVPD_LeaveCycleCode.Exists());

												
				CPCommon.CurrentComponent = "LDMLVPD";
							CPCommon.WaitControlDisplayed(LDMLVPD_MainForm);
IWebElement formBttn = LDMLVPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMLVPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMLVPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMLVPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVPD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMLVPD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVPD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Leave Period Details Form");


												
				CPCommon.CurrentComponent = "LDMLVPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVPD] Perfoming VerifyExists on LeavePeriodsDetailsLink...", Logger.MessageType.INF);
			Control LDMLVPD_LeavePeriodsDetailsLink = new Control("LeavePeriodsDetailsLink", "ID", "lnk_1002120_LDMLVPD_LVPD_HDR");
			CPCommon.AssertEqual(true,LDMLVPD_LeavePeriodsDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMLVPD";
							CPCommon.WaitControlDisplayed(LDMLVPD_LeavePeriodsDetailsLink);
LDMLVPD_LeavePeriodsDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMLVPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVPD] Perfoming VerifyExists on LeavePeriodsDetailsForm...", Logger.MessageType.INF);
			Control LDMLVPD_LeavePeriodsDetailsForm = new Control("LeavePeriodsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVPD_LVPDSCH_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMLVPD_LeavePeriodsDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLVPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVPD] Perfoming VerifyExist on LeavePeriodsDetailsTable...", Logger.MessageType.INF);
			Control LDMLVPD_LeavePeriodsDetailsTable = new Control("LeavePeriodsDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVPD_LVPDSCH_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVPD_LeavePeriodsDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "LDMLVPD";
							CPCommon.WaitControlDisplayed(LDMLVPD_LeavePeriodsDetailsForm);
formBttn = LDMLVPD_LeavePeriodsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMLVPD";
							CPCommon.WaitControlDisplayed(LDMLVPD_MainForm);
formBttn = LDMLVPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

