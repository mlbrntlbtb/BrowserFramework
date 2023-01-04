 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJRPROJ_SMOKE : TestScript
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
new Control("Print Project Status Report", "xpath","//div[@class='navItem'][.='Print Project Status Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJRPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRPROJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJRPROJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJRPROJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJRPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRPROJ] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJRPROJ_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJRPROJ_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJRPROJ";
							CPCommon.WaitControlDisplayed(PJRPROJ_MainForm);
IWebElement formBttn = PJRPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJRPROJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJRPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJRPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRPROJ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJRPROJ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJRPROJ_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Project Non Contiguus Ranges");


												
				CPCommon.CurrentComponent = "PJRPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRPROJ] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PJRPROJ_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2387_PJRPROJ_PARAM");
			CPCommon.AssertEqual(true,PJRPROJ_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PJRPROJ";
							CPCommon.WaitControlDisplayed(PJRPROJ_ProjectNonContiguousRangesLink);
PJRPROJ_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJRPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRPROJ] Perfoming VerifyExist on ProjectNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PJRPROJ_ProjectNonContiguousRangesTable = new Control("ProjectNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJRPROJ_NCRUSERID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJRPROJ_ProjectNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PJRPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRPROJ] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PJRPROJ_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJRPROJ_NCRUSERID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJRPROJ_ProjectNonContiguousRangesForm);
formBttn = PJRPROJ_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJRPROJ";
							CPCommon.WaitControlDisplayed(PJRPROJ_MainForm);
formBttn = PJRPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

