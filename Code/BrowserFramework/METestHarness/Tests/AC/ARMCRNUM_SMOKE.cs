 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMCRNUM_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Accounts Receivable Controls", "xpath","//div[@class='navItem'][.='Accounts Receivable Controls']").Click();
new Control("Configure System Assigned Cash Receipt Number", "xpath","//div[@class='navItem'][.='Configure System Assigned Cash Receipt Number']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ARMCRNUM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCRNUM_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control ARMCRNUM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ARMCRNUM_MainForm);
IWebElement formBttn = ARMCRNUM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCRNUM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCRNUM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control ARMCRNUM_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,ARMCRNUM_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Details");


												
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming VerifyExists on DetailsLink...", Logger.MessageType.INF);
			Control ARMCRNUM_DetailsLink = new Control("DetailsLink", "ID", "lnk_1002251_ACMNUM_ACCTINGPD");
			CPCommon.AssertEqual(true,ARMCRNUM_DetailsLink.Exists());

												
				CPCommon.CurrentComponent = "ARMCRNUM";
							CPCommon.WaitControlDisplayed(ARMCRNUM_DetailsLink);
ARMCRNUM_DetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control ARMCRNUM_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ACMNUM_LASTJENO_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCRNUM_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control ARMCRNUM_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ACMNUM_LASTJENO_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCRNUM_DetailsForm);
formBttn = ARMCRNUM_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCRNUM_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCRNUM_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCRNUM";
							CPCommon.AssertEqual(true,ARMCRNUM_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "ARMCRNUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCRNUM] Perfoming VerifyExists on Details_JournalCode...", Logger.MessageType.INF);
			Control ARMCRNUM_Details_JournalCode = new Control("Details_JournalCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ACMNUM_LASTJENO_CTW_']/ancestor::form[1]/descendant::*[@id='S_JNL_CD_AR']");
			CPCommon.AssertEqual(true,ARMCRNUM_Details_JournalCode.Exists());

												
				CPCommon.CurrentComponent = "ARMCRNUM";
							CPCommon.WaitControlDisplayed(ARMCRNUM_DetailsForm);
formBttn = ARMCRNUM_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ARMCRNUM";
							CPCommon.WaitControlDisplayed(ARMCRNUM_MainForm);
formBttn = ARMCRNUM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

