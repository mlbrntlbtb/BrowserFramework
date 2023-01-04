 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPYGL_SMOKE : TestScript
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
new Control("Project History", "xpath","//div[@class='navItem'][.='Project History']").Click();
new Control("Manage Prior Year Billable Value", "xpath","//div[@class='navItem'][.='Manage Prior Year Billable Value']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPYGL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPYGL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJMPYGL_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJMPYGL_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJMPYGL";
							CPCommon.WaitControlDisplayed(PJMPYGL_MainForm);
IWebElement formBttn = PJMPYGL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPYGL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPYGL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPYGL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYGL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("BILLABLE VALUE DETAILS");


												
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming VerifyExists on BillableValueDetailsLink...", Logger.MessageType.INF);
			Control PJMPYGL_BillableValueDetailsLink = new Control("BillableValueDetailsLink", "ID", "lnk_1004393_PJMPYGL_PYGOAL_HDR");
			CPCommon.AssertEqual(true,PJMPYGL_BillableValueDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control PJMPYGL_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYGL_PYGOAL_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYGL_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control PJMPYGL_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYGL_PYGOAL_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPYGL_DetailsForm);
formBttn = PJMPYGL_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPYGL_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPYGL_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPYGL";
							CPCommon.AssertEqual(true,PJMPYGL_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPYGL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYGL] Perfoming VerifyExists on Details_Account...", Logger.MessageType.INF);
			Control PJMPYGL_Details_Account = new Control("Details_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYGL_PYGOAL_CTW_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PJMPYGL_Details_Account.Exists());

												
				CPCommon.CurrentComponent = "PJMPYGL";
							CPCommon.WaitControlDisplayed(PJMPYGL_DetailsForm);
formBttn = PJMPYGL_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMPYGL";
							CPCommon.WaitControlDisplayed(PJMPYGL_MainForm);
formBttn = PJMPYGL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? PJMPYGL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
PJMPYGL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "PJMPYGL";
							CPCommon.WaitControlDisplayed(PJMPYGL_MainForm);
formBttn = PJMPYGL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

