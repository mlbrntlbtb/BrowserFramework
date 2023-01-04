 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HARPERS_SMOKE : TestScript
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
new Control("Affirmative Action", "xpath","//div[@class='deptItem'][.='Affirmative Action']").Click();
new Control("Affirmative Action Reports", "xpath","//div[@class='navItem'][.='Affirmative Action Reports']").Click();
new Control("Print Personnel Action Analysis Report", "xpath","//div[@class='navItem'][.='Print Personnel Action Analysis Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HARPERS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARPERS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HARPERS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HARPERS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HARPERS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARPERS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HARPERS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HARPERS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HARPERS";
							CPCommon.WaitControlDisplayed(HARPERS_MainForm);
IWebElement formBttn = HARPERS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HARPERS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HARPERS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HARPERS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARPERS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HARPERS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HARPERS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HARPERS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARPERS] Perfoming VerifyExists on PersonnelActionNonContiguousRangesLink...", Logger.MessageType.INF);
			Control HARPERS_PersonnelActionNonContiguousRangesLink = new Control("PersonnelActionNonContiguousRangesLink", "ID", "lnk_4121_HARPERS_PARAM");
			CPCommon.AssertEqual(true,HARPERS_PersonnelActionNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "HARPERS";
							CPCommon.WaitControlDisplayed(HARPERS_PersonnelActionNonContiguousRangesLink);
HARPERS_PersonnelActionNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HARPERS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARPERS] Perfoming VerifyExist on PersonnelActionNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control HARPERS_PersonnelActionNonContiguousRangesFormTable = new Control("PersonnelActionNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HARPERS_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HARPERS_PersonnelActionNonContiguousRangesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HARPERS";
							CPCommon.WaitControlDisplayed(HARPERS_MainForm);
formBttn = HARPERS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

