 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMMOD_SMOKE : TestScript
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
new Control("Project Master", "xpath","//div[@class='navItem'][.='Project Master']").Click();
new Control("Manage Modifications", "xpath","//div[@class='navItem'][.='Manage Modifications']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMMOD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMMOD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMMOD_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMMOD_Project.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on UnitInfoLink...", Logger.MessageType.INF);
			Control PJMMOD_UnitInfoLink = new Control("UnitInfoLink", "ID", "lnk_1004439_PJM_PROJMOD_HDR");
			CPCommon.AssertEqual(true,PJMMOD_UnitInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_UnitInfoLink);
PJMMOD_UnitInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on UnitInfoForm...", Logger.MessageType.INF);
			Control PJMMOD_UnitInfoForm = new Control("UnitInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMODUNITS_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMMOD_UnitInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on UnitInfo_CLIN...", Logger.MessageType.INF);
			Control PJMMOD_UnitInfo_CLIN = new Control("UnitInfo_CLIN", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMODUNITS_CHILD_']/ancestor::form[1]/descendant::*[@id='CLIN_ID']");
			CPCommon.AssertEqual(true,PJMMOD_UnitInfo_CLIN.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_UnitInfoForm);
IWebElement formBttn = PJMMOD_UnitInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on NotesLink...", Logger.MessageType.INF);
			Control PJMMOD_NotesLink = new Control("NotesLink", "ID", "lnk_1004440_PJM_PROJMOD_HDR");
			CPCommon.AssertEqual(true,PJMMOD_NotesLink.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_NotesLink);
PJMMOD_NotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on NotesForm...", Logger.MessageType.INF);
			Control PJMMOD_NotesForm = new Control("NotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMODNOTES_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMMOD_NotesForm.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on Notes_Text...", Logger.MessageType.INF);
			Control PJMMOD_Notes_Text = new Control("Notes_Text", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMODNOTES_CHILD_']/ancestor::form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,PJMMOD_Notes_Text.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_NotesForm);
formBttn = PJMMOD_NotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on AwardFeeLink...", Logger.MessageType.INF);
			Control PJMMOD_AwardFeeLink = new Control("AwardFeeLink", "ID", "lnk_3423_PJM_PROJMOD_HDR");
			CPCommon.AssertEqual(true,PJMMOD_AwardFeeLink.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_AwardFeeLink);
PJMMOD_AwardFeeLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExist on AwardFeeTable...", Logger.MessageType.INF);
			Control PJMMOD_AwardFeeTable = new Control("AwardFeeTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMOD_AWARDFEE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMOD_AwardFeeTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming ClickButton on AwardFeeForm...", Logger.MessageType.INF);
			Control PJMMOD_AwardFeeForm = new Control("AwardFeeForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMOD_AWARDFEE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMMOD_AwardFeeForm);
formBttn = PJMMOD_AwardFeeForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMOD_AwardFeeForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMOD_AwardFeeForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.AssertEqual(true,PJMMOD_AwardFeeForm.Exists());

													
				CPCommon.CurrentComponent = "PJMMOD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMOD] Perfoming VerifyExists on AwardFee_ModID...", Logger.MessageType.INF);
			Control PJMMOD_AwardFee_ModID = new Control("AwardFee_ModID", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJMOD_AWARDFEE_']/ancestor::form[1]/descendant::*[@id='PROJ_MOD_ID']");
			CPCommon.AssertEqual(true,PJMMOD_AwardFee_ModID.Exists());

												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_AwardFeeForm);
formBttn = PJMMOD_AwardFeeForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMMOD";
							CPCommon.WaitControlDisplayed(PJMMOD_MainForm);
formBttn = PJMMOD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

