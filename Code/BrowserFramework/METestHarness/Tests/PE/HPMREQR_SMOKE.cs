 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMREQR_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee Controls", "xpath","//div[@class='navItem'][.='Employee Controls']").Click();
new Control("Manage Job Templates", "xpath","//div[@class='navItem'][.='Manage Job Templates']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMREQR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMREQR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming VerifyExists on RequisitionNumber...", Logger.MessageType.INF);
			Control HPMREQR_RequisitionNumber = new Control("RequisitionNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_NO']");
			CPCommon.AssertEqual(true,HPMREQR_RequisitionNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control HPMREQR_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(HPMREQR_MainFormTab);
IWebElement mTab = HPMREQR_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Requisition Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming VerifyExists on RequisitionDetails_RequestedBy...", Logger.MessageType.INF);
			Control HPMREQR_RequisitionDetails_RequestedBy = new Control("RequisitionDetails_RequestedBy", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_EMPL_ID']");
			CPCommon.AssertEqual(true,HPMREQR_RequisitionDetails_RequestedBy.Exists());

												
				CPCommon.CurrentComponent = "HPMREQR";
							CPCommon.WaitControlDisplayed(HPMREQR_MainFormTab);
mTab = HPMREQR_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Job Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming VerifyExists on JobDetails_TaxableEntity...", Logger.MessageType.INF);
			Control HPMREQR_JobDetails_TaxableEntity = new Control("JobDetails_TaxableEntity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY_ID']");
			CPCommon.AssertEqual(true,HPMREQR_JobDetails_TaxableEntity.Exists());

												
				CPCommon.CurrentComponent = "HPMREQR";
							CPCommon.WaitControlDisplayed(HPMREQR_MainFormTab);
mTab = HPMREQR_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Timesheet Defaults & Product Interface").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming VerifyExists on TimesheetDefaultsProductInterface_CostpointTimesheetDefaults_Project...", Logger.MessageType.INF);
			Control HPMREQR_TimesheetDefaultsProductInterface_CostpointTimesheetDefaults_Project = new Control("TimesheetDefaultsProductInterface_CostpointTimesheetDefaults_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_PROJ_ID']");
			CPCommon.AssertEqual(true,HPMREQR_TimesheetDefaultsProductInterface_CostpointTimesheetDefaults_Project.Exists());

												
				CPCommon.CurrentComponent = "HPMREQR";
							CPCommon.WaitControlDisplayed(HPMREQR_MainForm);
IWebElement formBttn = HPMREQR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMREQR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMREQR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMREQR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMREQR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMREQR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMREQR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "HPMREQR";
							CPCommon.WaitControlDisplayed(HPMREQR_MainForm);
formBttn = HPMREQR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

