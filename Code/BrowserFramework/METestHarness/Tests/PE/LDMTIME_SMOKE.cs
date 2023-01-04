 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMTIME_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Entry/Creation", "xpath","//div[@class='navItem'][.='Timesheet Entry/Creation']").Click();
new Control("Manage Timesheets", "xpath","//div[@class='navItem'][.='Manage Timesheets']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMTIME_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMTIME_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on Date...", Logger.MessageType.INF);
			Control LDMTIME_Date = new Control("Date", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TS_DT']");
			CPCommon.AssertEqual(true,LDMTIME_Date.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on Tab...", Logger.MessageType.INF);
			Control LDMTIME_Tab = new Control("Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,LDMTIME_Tab.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_Tab);
IWebElement mTab = LDMTIME_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Timesheet Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on TimesheetHeader_HoursAndAccountingPeriod_RegularHours...", Logger.MessageType.INF);
			Control LDMTIME_TimesheetHeader_HoursAndAccountingPeriod_RegularHours = new Control("TimesheetHeader_HoursAndAccountingPeriod_RegularHours", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REG_HRS']");
			CPCommon.AssertEqual(true,LDMTIME_TimesheetHeader_HoursAndAccountingPeriod_RegularHours.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_Tab);
mTab = LDMTIME_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Entry Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on EntryInformation_EnteredBy...", Logger.MessageType.INF);
			Control LDMTIME_EntryInformation_EnteredBy = new Control("EntryInformation_EnteredBy", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TH___USER_ID']");
			CPCommon.AssertEqual(true,LDMTIME_EntryInformation_EnteredBy.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control LDMTIME_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(LDMTIME_ChildForm);
IWebElement formBttn = LDMTIME_ChildForm.mElement.FindElements(By.CssSelector("*[title*='New']")).Count <= 0 ? LDMTIME_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'New')]")).FirstOrDefault() :
LDMTIME_ChildForm.mElement.FindElements(By.CssSelector("*[title*='New']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" New not found ");


												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.AssertEqual(true,LDMTIME_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control LDMTIME_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMTIME_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_ChildForm);
formBttn = LDMTIME_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMTIME_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMTIME_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on ChildFormTab...", Logger.MessageType.INF);
			Control LDMTIME_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,LDMTIME_ChildFormTab.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_ChildFormTab);
mTab = LDMTIME_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Charge Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on ChildForm_ChargeInformation_LineNumber...", Logger.MessageType.INF);
			Control LDMTIME_ChildForm_ChargeInformation_LineNumber = new Control("ChildForm_ChargeInformation_LineNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='TS_LN___TS_LN_NO']");
			CPCommon.AssertEqual(true,LDMTIME_ChildForm_ChargeInformation_LineNumber.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_ChildFormTab);
mTab = LDMTIME_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Reference and Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on ChildForm_ReferenceAndNotes_FringeBasis_UnionFringeCode...", Logger.MessageType.INF);
			Control LDMTIME_ChildForm_ReferenceAndNotes_FringeBasis_UnionFringeCode = new Control("ChildForm_ReferenceAndNotes_FringeBasis_UnionFringeCode", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='TS_LN___FRINGE_CD']");
			CPCommon.AssertEqual(true,LDMTIME_ChildForm_ReferenceAndNotes_FringeBasis_UnionFringeCode.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on ManufacturingOrderTimesheetInformationLink...", Logger.MessageType.INF);
			Control LDMTIME_ManufacturingOrderTimesheetInformationLink = new Control("ManufacturingOrderTimesheetInformationLink", "ID", "lnk_1832_LDMTIME_TSLN");
			CPCommon.AssertEqual(true,LDMTIME_ManufacturingOrderTimesheetInformationLink.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on SalesOrderTimesheetInformationLink...", Logger.MessageType.INF);
			Control LDMTIME_SalesOrderTimesheetInformationLink = new Control("SalesOrderTimesheetInformationLink", "ID", "lnk_1833_LDMTIME_TSLN");
			CPCommon.AssertEqual(true,LDMTIME_SalesOrderTimesheetInformationLink.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_ManufacturingOrderTimesheetInformationLink);
LDMTIME_ManufacturingOrderTimesheetInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on ManufacturingOrderTimesheetInformationForm...", Logger.MessageType.INF);
			Control LDMTIME_ManufacturingOrderTimesheetInformationForm = new Control("ManufacturingOrderTimesheetInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLNMO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMTIME_ManufacturingOrderTimesheetInformationForm.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder...", Logger.MessageType.INF);
			Control LDMTIME_ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder = new Control("ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLNMO_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,LDMTIME_ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_ManufacturingOrderTimesheetInformationForm);
formBttn = LDMTIME_ManufacturingOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_SalesOrderTimesheetInformationLink);
LDMTIME_SalesOrderTimesheetInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on SalesOrderTimesheetInformationForm...", Logger.MessageType.INF);
			Control LDMTIME_SalesOrderTimesheetInformationForm = new Control("SalesOrderTimesheetInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLNSO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMTIME_SalesOrderTimesheetInformationForm.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExists on SalesOrderTimesheetInformation_SalesOrder...", Logger.MessageType.INF);
			Control LDMTIME_SalesOrderTimesheetInformation_SalesOrder = new Control("SalesOrderTimesheetInformation_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLNSO_']/ancestor::form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,LDMTIME_SalesOrderTimesheetInformation_SalesOrder.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_SalesOrderTimesheetInformationForm);
formBttn = LDMTIME_SalesOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMTIME_SalesOrderTimesheetInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMTIME_SalesOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTIME] Perfoming VerifyExist on SalesOrderTimesheetInformationTable...", Logger.MessageType.INF);
			Control LDMTIME_SalesOrderTimesheetInformationTable = new Control("SalesOrderTimesheetInformationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTIME_TSLNSO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMTIME_SalesOrderTimesheetInformationTable.Exists());

												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_SalesOrderTimesheetInformationForm);
formBttn = LDMTIME_SalesOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDMTIME";
							CPCommon.WaitControlDisplayed(LDMTIME_MainForm);
formBttn = LDMTIME_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");


												
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

