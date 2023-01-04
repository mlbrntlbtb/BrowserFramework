 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPCLHF_SMOKE : TestScript
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
new Control("Compute Leave Accruals", "xpath","//div[@class='navItem'][.='Compute Leave Accruals']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDPCLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPCLHF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPCLHF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPCLHF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPCLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPCLHF] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPCLHF_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPCLHF_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPCLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPCLHF] Perfoming VerifyExists on ComputeLeaveAccrualLink...", Logger.MessageType.INF);
			Control LDPCLHF_ComputeLeaveAccrualLink = new Control("ComputeLeaveAccrualLink", "ID", "lnk_1004531_LDPCLHF");
			CPCommon.AssertEqual(true,LDPCLHF_ComputeLeaveAccrualLink.Exists());

												
				CPCommon.CurrentComponent = "LDPCLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPCLHF] Perfoming VerifyExists on ComputeLeaveAccrualForm...", Logger.MessageType.INF);
			Control LDPCLHF_ComputeLeaveAccrualForm = new Control("ComputeLeaveAccrualForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPCLHF_NCREMPLID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDPCLHF_ComputeLeaveAccrualForm.Exists());

												
				CPCommon.CurrentComponent = "LDPCLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPCLHF] Perfoming VerifyExist on ComputeLeaveAccrualTable...", Logger.MessageType.INF);
			Control LDPCLHF_ComputeLeaveAccrualTable = new Control("ComputeLeaveAccrualTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPCLHF_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPCLHF_ComputeLeaveAccrualTable.Exists());

												
				CPCommon.CurrentComponent = "LDPCLHF";
							CPCommon.WaitControlDisplayed(LDPCLHF_ComputeLeaveAccrualForm);
IWebElement formBttn = LDPCLHF_ComputeLeaveAccrualForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "LDPCLHF";
							CPCommon.WaitControlDisplayed(LDPCLHF_MainForm);
formBttn = LDPCLHF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPCLHF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPCLHF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPCLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPCLHF] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control LDPCLHF_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPCLHF_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDPCLHF";
							CPCommon.WaitControlDisplayed(LDPCLHF_MainForm);
formBttn = LDPCLHF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

