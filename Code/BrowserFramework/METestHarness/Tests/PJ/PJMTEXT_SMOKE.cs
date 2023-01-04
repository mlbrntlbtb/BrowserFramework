 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMTEXT_SMOKE : TestScript
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
new Control("Manage Standard Text", "xpath","//div[@class='navItem'][.='Manage Standard Text']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMTEXT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMTEXT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMTEXT_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMTEXT_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
							CPCommon.WaitControlDisplayed(PJMTEXT_MainForm);
IWebElement formBttn = PJMTEXT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMTEXT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMTEXT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMTEXT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMTEXT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Standard Text Codes");


												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExist on StandardTextCodesFormTable...", Logger.MessageType.INF);
			Control PJMTEXT_StandardTextCodesFormTable = new Control("StandardTextCodesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_STDTEXT_AVAILSTDTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMTEXT_StandardTextCodesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control PJMTEXT_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_STDTEXT_SELECTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMTEXT_StandardTextFormTable.Exists());

											Driver.SessionLogger.WriteLine("Where Used");


												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExists on StandardTextCodes_WhereUsedLink...", Logger.MessageType.INF);
			Control PJMTEXT_StandardTextCodes_WhereUsedLink = new Control("StandardTextCodes_WhereUsedLink", "ID", "lnk_1007158_PJM_STDTEXT_AVAILSTDTEXT");
			CPCommon.AssertEqual(true,PJMTEXT_StandardTextCodes_WhereUsedLink.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
							CPCommon.WaitControlDisplayed(PJMTEXT_StandardTextCodes_WhereUsedLink);
PJMTEXT_StandardTextCodes_WhereUsedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExists on StandardTextCodes_WhereUsedForm...", Logger.MessageType.INF);
			Control PJMTEXT_StandardTextCodes_WhereUsedForm = new Control("StandardTextCodes_WhereUsedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_WHEREUSEDPARENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMTEXT_StandardTextCodes_WhereUsedForm.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExists on StandardTextCodes_WhereUsed_TextCode...", Logger.MessageType.INF);
			Control PJMTEXT_StandardTextCodes_WhereUsed_TextCode = new Control("StandardTextCodes_WhereUsed_TextCode", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_WHEREUSEDPARENT_']/ancestor::form[1]/descendant::*[@id='TEXT_CD']");
			CPCommon.AssertEqual(true,PJMTEXT_StandardTextCodes_WhereUsed_TextCode.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTEXT] Perfoming VerifyExist on StandardTextCodes_WhereUsed_ChildFormTable...", Logger.MessageType.INF);
			Control PJMTEXT_StandardTextCodes_WhereUsed_ChildFormTable = new Control("StandardTextCodes_WhereUsed_ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_WHEREUSEDCHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMTEXT_StandardTextCodes_WhereUsed_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMTEXT";
							CPCommon.WaitControlDisplayed(PJMTEXT_StandardTextCodes_WhereUsedForm);
formBttn = PJMTEXT_StandardTextCodes_WhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMTEXT";
							CPCommon.WaitControlDisplayed(PJMTEXT_MainForm);
formBttn = PJMTEXT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

