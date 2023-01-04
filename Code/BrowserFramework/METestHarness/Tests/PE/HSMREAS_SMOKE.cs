 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMREAS_SMOKE : TestScript
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
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Personnel Information", "xpath","//div[@class='navItem'][.='Personnel Information']").Click();
new Control("Manage Personnel Actions", "xpath","//div[@class='navItem'][.='Manage Personnel Actions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HSMREAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMREAS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMREAS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMREAS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMREAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMREAS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMREAS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMREAS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMREAS";
							CPCommon.WaitControlDisplayed(HSMREAS_MainForm);
IWebElement formBttn = HSMREAS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMREAS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMREAS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HSMREAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMREAS] Perfoming VerifyExists on PersonnelActionCode...", Logger.MessageType.INF);
			Control HSMREAS_PersonnelActionCode = new Control("PersonnelActionCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PERS_ACT_RSN_CD']");
			CPCommon.AssertEqual(true,HSMREAS_PersonnelActionCode.Exists());

											Driver.SessionLogger.WriteLine("Required Forms by Personnel Action");


												
				CPCommon.CurrentComponent = "HSMREAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMREAS] Perfoming VerifyExists on RequiredFormsByPersonnelActionLink...", Logger.MessageType.INF);
			Control HSMREAS_RequiredFormsByPersonnelActionLink = new Control("RequiredFormsByPersonnelActionLink", "ID", "lnk_4807_HSMREAS_PERSACTREASON_HDR");
			CPCommon.AssertEqual(true,HSMREAS_RequiredFormsByPersonnelActionLink.Exists());

												
				CPCommon.CurrentComponent = "HSMREAS";
							CPCommon.WaitControlDisplayed(HSMREAS_RequiredFormsByPersonnelActionLink);
HSMREAS_RequiredFormsByPersonnelActionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMREAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMREAS] Perfoming VerifyExists on RequiredFormsByPersonnelActionForm...", Logger.MessageType.INF);
			Control HSMREAS_RequiredFormsByPersonnelActionForm = new Control("RequiredFormsByPersonnelActionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMREAS_HRQFRMPERRSN_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMREAS_RequiredFormsByPersonnelActionForm.Exists());

												
				CPCommon.CurrentComponent = "HSMREAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMREAS] Perfoming VerifyExist on RequiredFormsByPersonnelActionFormTable...", Logger.MessageType.INF);
			Control HSMREAS_RequiredFormsByPersonnelActionFormTable = new Control("RequiredFormsByPersonnelActionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMREAS_HRQFRMPERRSN_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMREAS_RequiredFormsByPersonnelActionFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMREAS";
							CPCommon.WaitControlDisplayed(HSMREAS_RequiredFormsByPersonnelActionForm);
formBttn = HSMREAS_RequiredFormsByPersonnelActionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HSMREAS";
							CPCommon.WaitControlDisplayed(HSMREAS_MainForm);
formBttn = HSMREAS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

