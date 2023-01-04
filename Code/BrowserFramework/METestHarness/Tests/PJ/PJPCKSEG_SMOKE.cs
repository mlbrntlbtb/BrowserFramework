 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCKSEG_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Administrative Utilities", "xpath","//div[@class='navItem'][.='Administrative Utilities']").Click();
new Control("Check and/or Rebuild Project Segment IDs", "xpath","//div[@class='navItem'][.='Check and/or Rebuild Project Segment IDs']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPCKSEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKSEG] Perfoming VerifyExists on CheckTheProjectIDAndLineProjectSegmentIDsForm...", Logger.MessageType.INF);
			Control PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDsForm = new Control("CheckTheProjectIDAndLineProjectSegmentIDsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDsForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCKSEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKSEG] Perfoming VerifyExists on CheckTheProjectIDAndLineProjectSegmentIDs_Check...", Logger.MessageType.INF);
			Control PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_Check = new Control("CheckTheProjectIDAndLineProjectSegmentIDs_Check", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'EXECUTE') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_Check.Exists());

											Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "PJPCKSEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKSEG] Perfoming VerifyExists on CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm...", Logger.MessageType.INF);
			Control PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm = new Control("CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCKSEG_ZPJPTOOLPRJERRS_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCKSEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKSEG] Perfoming VerifyExist on CheckTheProjectIDAndLineProjectSegmentIDs_ChildFormTable...", Logger.MessageType.INF);
			Control PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildFormTable = new Control("CheckTheProjectIDAndLineProjectSegmentIDs_ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCKSEG_ZPJPTOOLPRJERRS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("Rebuild the Line Project Segments");


												
				CPCommon.CurrentComponent = "PJPCKSEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKSEG] Perfoming VerifyExists on CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDsForm...", Logger.MessageType.INF);
			Control PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDsForm = new Control("CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDsForm", "xpath", "//div[starts-with(@id,'pr__PJPCKSEG_ZPJPTOOLLVLWRK1_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDsForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCKSEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKSEG] Perfoming VerifyExists on CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDs_ProjectRange...", Logger.MessageType.INF);
			Control PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDs_ProjectRange = new Control("CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDs_ProjectRange", "xpath", "//div[starts-with(@id,'pr__PJPCKSEG_ZPJPTOOLLVLWRK1_')]/ancestor::form[1]/descendant::*[@id='PROJ_RANGE']");
			CPCommon.AssertEqual(true,PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDs_ChildForm_RebuildTheLineProjectSegmentIDs_ProjectRange.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJPCKSEG";
							CPCommon.WaitControlDisplayed(PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDsForm);
IWebElement formBttn = PJPCKSEG_CheckTheProjectIDAndLineProjectSegmentIDsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

