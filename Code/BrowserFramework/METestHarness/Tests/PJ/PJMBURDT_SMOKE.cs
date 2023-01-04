 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBURDT_SMOKE : TestScript
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
new Control("Advanced Project Budgeting", "xpath","//div[@class='deptItem'][.='Advanced Project Budgeting']").Click();
new Control("Burden Templates", "xpath","//div[@class='navItem'][.='Burden Templates']").Click();
new Control("Manage Burden Templates", "xpath","//div[@class='navItem'][.='Manage Burden Templates']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBURDT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBURDT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming VerifyExists on MainForm_Template...", Logger.MessageType.INF);
			Control PJMBURDT_MainForm_Template = new Control("MainForm_Template", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TEMP_ID']");
			CPCommon.AssertEqual(true,PJMBURDT_MainForm_Template.Exists());

												
				CPCommon.CurrentComponent = "PJMBURDT";
							CPCommon.WaitControlDisplayed(PJMBURDT_MainForm);
IWebElement formBttn = PJMBURDT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMBURDT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMBURDT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control PJMBURDT_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBURDT_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming Close on BurdenDetailsForm...", Logger.MessageType.INF);
			Control PJMBURDT_BurdenDetailsForm = new Control("BurdenDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBURDT_BURDTEMP_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBURDT_BurdenDetailsForm);
formBttn = PJMBURDT_BurdenDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming VerifyExists on MainForm_BurdenTemplatesDetailLink...", Logger.MessageType.INF);
			Control PJMBURDT_MainForm_BurdenTemplatesDetailLink = new Control("MainForm_BurdenTemplatesDetailLink", "ID", "lnk_1002341_PJMBURDT_BURDTEMP");
			CPCommon.AssertEqual(true,PJMBURDT_MainForm_BurdenTemplatesDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBURDT";
							CPCommon.WaitControlDisplayed(PJMBURDT_MainForm_BurdenTemplatesDetailLink);
PJMBURDT_MainForm_BurdenTemplatesDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming VerifyExist on BurdenTemplatesDetail_Table...", Logger.MessageType.INF);
			Control PJMBURDT_BurdenTemplatesDetail_Table = new Control("BurdenTemplatesDetail_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBURDT_BURDTEMP_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBURDT_BurdenTemplatesDetail_Table.Exists());

												
				CPCommon.CurrentComponent = "PJMBURDT";
							CPCommon.WaitControlDisplayed(PJMBURDT_BurdenDetailsForm);
formBttn = PJMBURDT_BurdenDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBURDT_BurdenDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBURDT_BurdenDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJMBURDT";
							CPCommon.AssertEqual(true,PJMBURDT_BurdenDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMBURDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBURDT] Perfoming VerifyExists on BurdenDetails_FiscalYear...", Logger.MessageType.INF);
			Control PJMBURDT_BurdenDetails_FiscalYear = new Control("BurdenDetails_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBURDT_BURDTEMP_CHLD_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJMBURDT_BurdenDetails_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMBURDT";
							CPCommon.WaitControlDisplayed(PJMBURDT_MainForm);
formBttn = PJMBURDT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

