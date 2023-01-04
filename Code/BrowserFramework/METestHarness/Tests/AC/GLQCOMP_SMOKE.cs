 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLQCOMP_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Reports/Inquiries", "xpath","//div[@class='navItem'][.='General Ledger Reports/Inquiries']").Click();
new Control("Compare General Ledger Actual to Budget Activity", "xpath","//div[@class='navItem'][.='Compare General Ledger Actual to Budget Activity']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLQCOMP_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLQCOMP_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLQCOMP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQCOMP_RPTFSCOMPARE_MAINTABLE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQCOMP_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control GLQCOMP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQCOMP_RPTFSCOMPARE_MAINTABLE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQCOMP_ChildForm);
IWebElement formBttn = GLQCOMP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQCOMP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQCOMP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming VerifyExists on ChildForm_Account...", Logger.MessageType.INF);
			Control GLQCOMP_ChildForm_Account = new Control("ChildForm_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQCOMP_RPTFSCOMPARE_MAINTABLE_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLQCOMP_ChildForm_Account.Exists());

												
				CPCommon.CurrentComponent = "GLQCOMP";
							CPCommon.WaitControlDisplayed(GLQCOMP_ChildForm);
formBttn = GLQCOMP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? GLQCOMP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
GLQCOMP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("Period");


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming Click on ChildForm_PeriodLink...", Logger.MessageType.INF);
			Control GLQCOMP_ChildForm_PeriodLink = new Control("ChildForm_PeriodLink", "ID", "lnk_1005969_GLQCOMP_RPTFSCOMPARE_MAINTABLE");
			CPCommon.WaitControlDisplayed(GLQCOMP_ChildForm_PeriodLink);
GLQCOMP_ChildForm_PeriodLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming VerifyExist on ChildForm_PeriodFormTable...", Logger.MessageType.INF);
			Control GLQCOMP_ChildForm_PeriodFormTable = new Control("ChildForm_PeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQCOMP_RPTFSCOMPARE_PERIOD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQCOMP_ChildForm_PeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming ClickButton on ChildForm_PeriodForm...", Logger.MessageType.INF);
			Control GLQCOMP_ChildForm_PeriodForm = new Control("ChildForm_PeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQCOMP_RPTFSCOMPARE_PERIOD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQCOMP_ChildForm_PeriodForm);
formBttn = GLQCOMP_ChildForm_PeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQCOMP_ChildForm_PeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQCOMP_ChildForm_PeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming VerifyExists on ChildForm_Period_Period...", Logger.MessageType.INF);
			Control GLQCOMP_ChildForm_Period_Period = new Control("ChildForm_Period_Period", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQCOMP_RPTFSCOMPARE_PERIOD_']/ancestor::form[1]/descendant::*[@id='PD_NO']");
			CPCommon.AssertEqual(true,GLQCOMP_ChildForm_Period_Period.Exists());

												
				CPCommon.CurrentComponent = "GLQCOMP";
							CPCommon.WaitControlDisplayed(GLQCOMP_ChildForm_PeriodForm);
formBttn = GLQCOMP_ChildForm_PeriodForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "GLQCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQCOMP] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control GLQCOMP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLQCOMP_MainForm);
formBttn = GLQCOMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

