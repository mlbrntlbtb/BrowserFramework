 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJRREVW_SMOKE : TestScript
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
new Control("Project Inquiry and Reporting", "xpath","//div[@class='deptItem'][.='Project Inquiry and Reporting']").Click();
new Control("Project Reports/Inquiries", "xpath","//div[@class='navItem'][.='Project Reports/Inquiries']").Click();
new Control("Print Revenue Worksheet", "xpath","//div[@class='navItem'][.='Print Revenue Worksheet']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJRREVW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRREVW] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJRREVW_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJRREVW_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJRREVW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRREVW] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJRREVW_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJRREVW_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJRREVW";
							CPCommon.WaitControlDisplayed(PJRREVW_MainForm);
IWebElement formBttn = PJRREVW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJRREVW_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJRREVW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJRREVW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRREVW] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJRREVW_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJRREVW_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Project Non Contiguus Ranges");


												
				CPCommon.CurrentComponent = "PJRREVW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRREVW] Perfoming VerifyExists on ProjectNonContigouosRangesLink...", Logger.MessageType.INF);
			Control PJRREVW_ProjectNonContigouosRangesLink = new Control("ProjectNonContigouosRangesLink", "ID", "lnk_2588_PJRREVW_PARAM");
			CPCommon.AssertEqual(true,PJRREVW_ProjectNonContigouosRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PJRREVW";
							CPCommon.WaitControlDisplayed(PJRREVW_ProjectNonContigouosRangesLink);
PJRREVW_ProjectNonContigouosRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJRREVW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRREVW] Perfoming VerifyExist on ProjectNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PJRREVW_ProjectNonContiguousRangesTable = new Control("ProjectNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJRREVW_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJRREVW_ProjectNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PJRREVW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRREVW] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PJRREVW_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJRREVW_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJRREVW_ProjectNonContiguousRangesForm);
formBttn = PJRREVW_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJRREVW";
							CPCommon.WaitControlDisplayed(PJRREVW_MainForm);
formBttn = PJRREVW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

