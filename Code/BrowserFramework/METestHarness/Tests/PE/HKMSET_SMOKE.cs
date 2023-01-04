 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HKMSET_SMOKE : TestScript
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
new Control("Deferred Compensation Admin", "xpath","//div[@class='deptItem'][.='Deferred Compensation Admin']").Click();
new Control("Deferred Compensation Plan Controls", "xpath","//div[@class='navItem'][.='Deferred Compensation Plan Controls']").Click();
new Control("Manage Deferred Compensation Plans", "xpath","//div[@class='navItem'][.='Manage Deferred Compensation Plans']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HKMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HKMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExists on DeferredCompensationPlan...", Logger.MessageType.INF);
			Control HKMSET_DeferredCompensationPlan = new Control("DeferredCompensationPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CODA_PLAN_CD']");
			CPCommon.AssertEqual(true,HKMSET_DeferredCompensationPlan.Exists());

												
				CPCommon.CurrentComponent = "HKMSET";
							CPCommon.WaitControlDisplayed(HKMSET_MainForm);
IWebElement formBttn = HKMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HKMSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HKMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HKMSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HKMSET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Plan Tax Information");


												
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExists on PlanTaxInformationLink...", Logger.MessageType.INF);
			Control HKMSET_PlanTaxInformationLink = new Control("PlanTaxInformationLink", "ID", "lnk_15549_HKMSET_HCODASETUP");
			CPCommon.AssertEqual(true,HKMSET_PlanTaxInformationLink.Exists());

												
				CPCommon.CurrentComponent = "HKMSET";
							CPCommon.WaitControlDisplayed(HKMSET_PlanTaxInformationLink);
HKMSET_PlanTaxInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExists on PlanTaxInformationForm...", Logger.MessageType.INF);
			Control HKMSET_PlanTaxInformationForm = new Control("PlanTaxInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HKMSET_HCODATAXINFO_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HKMSET_PlanTaxInformationForm.Exists());

												
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExist on PlanTaxInformationFormTable...", Logger.MessageType.INF);
			Control HKMSET_PlanTaxInformationFormTable = new Control("PlanTaxInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HKMSET_HCODATAXINFO_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HKMSET_PlanTaxInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "HKMSET";
							CPCommon.WaitControlDisplayed(HKMSET_PlanTaxInformationForm);
formBttn = HKMSET_PlanTaxInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HKMSET_PlanTaxInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HKMSET_PlanTaxInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HKMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMSET] Perfoming VerifyExists on PlanTaxInformationForm_PlanDates_StartDate...", Logger.MessageType.INF);
			Control HKMSET_PlanTaxInformationForm_PlanDates_StartDate = new Control("PlanTaxInformationForm_PlanDates_StartDate", "xpath", "//div[translate(@id,'0123456789','')='pr__HKMSET_HCODATAXINFO_DTL_']/ancestor::form[1]/descendant::*[@id='CODA_START_DT']");
			CPCommon.AssertEqual(true,HKMSET_PlanTaxInformationForm_PlanDates_StartDate.Exists());

												
				CPCommon.CurrentComponent = "HKMSET";
							CPCommon.WaitControlDisplayed(HKMSET_PlanTaxInformationForm);
formBttn = HKMSET_PlanTaxInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HKMSET";
							CPCommon.WaitControlDisplayed(HKMSET_MainForm);
formBttn = HKMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

