 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMLVMOD_SMOKE : TestScript
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
new Control("Leave Controls", "xpath","//div[@class='navItem'][.='Leave Controls']").Click();
new Control("Manage Leave Modify Codes", "xpath","//div[@class='navItem'][.='Manage Leave Modify Codes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMLVMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVMOD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMLVMOD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMLVMOD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLVMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVMOD] Perfoming VerifyExists on LeaveModifierCode...", Logger.MessageType.INF);
			Control LDMLVMOD_LeaveModifierCode = new Control("LeaveModifierCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LV_MOD_CD']");
			CPCommon.AssertEqual(true,LDMLVMOD_LeaveModifierCode.Exists());

												
				CPCommon.CurrentComponent = "LDMLVMOD";
							CPCommon.WaitControlDisplayed(LDMLVMOD_MainForm);
IWebElement formBttn = LDMLVMOD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMLVMOD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMLVMOD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMLVMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVMOD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMLVMOD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVMOD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Leave Modifer Details Form");


												
				CPCommon.CurrentComponent = "LDMLVMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVMOD] Perfoming VerifyExists on LeaveModifierDetailsLink...", Logger.MessageType.INF);
			Control LDMLVMOD_LeaveModifierDetailsLink = new Control("LeaveModifierDetailsLink", "ID", "lnk_5634_LDMLVMOD_LVMODIFIER_HDR");
			CPCommon.AssertEqual(true,LDMLVMOD_LeaveModifierDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMLVMOD";
							CPCommon.WaitControlDisplayed(LDMLVMOD_LeaveModifierDetailsLink);
LDMLVMOD_LeaveModifierDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMLVMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVMOD] Perfoming VerifyExists on LeaveModifierDetailsForm...", Logger.MessageType.INF);
			Control LDMLVMOD_LeaveModifierDetailsForm = new Control("LeaveModifierDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVMOD_LVMODTBL_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMLVMOD_LeaveModifierDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLVMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVMOD] Perfoming VerifyExist on LeaveModifierDetails_Table...", Logger.MessageType.INF);
			Control LDMLVMOD_LeaveModifierDetails_Table = new Control("LeaveModifierDetails_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVMOD_LVMODTBL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVMOD_LeaveModifierDetails_Table.Exists());

												
				CPCommon.CurrentComponent = "LDMLVMOD";
							CPCommon.WaitControlDisplayed(LDMLVMOD_LeaveModifierDetailsForm);
formBttn = LDMLVMOD_LeaveModifierDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMLVMOD";
							CPCommon.WaitControlDisplayed(LDMLVMOD_MainForm);
formBttn = LDMLVMOD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

