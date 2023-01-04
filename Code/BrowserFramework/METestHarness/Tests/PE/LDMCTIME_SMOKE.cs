 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMCTIME_SMOKE : TestScript
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
new Control("Manage Correcting Timesheets", "xpath","//div[@class='navItem'][.='Manage Correcting Timesheets']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMCTIME_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMCTIME_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMCTIME_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCTIME_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_MainForm);
IWebElement formBttn = LDMCTIME_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMCTIME_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMCTIME_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on Date...", Logger.MessageType.INF);
			Control LDMCTIME_Date = new Control("Date", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NEW_TS_DT']");
			CPCommon.AssertEqual(true,LDMCTIME_Date.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control LDMCTIME_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(LDMCTIME_MainFormTab);
IWebElement mTab = LDMCTIME_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Timesheet Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetHeader_HoursAccountingPeriod_RegularHours...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetHeader_HoursAccountingPeriod_RegularHours = new Control("TimesheetHeader_HoursAccountingPeriod_RegularHours", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REG_HRS']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetHeader_HoursAccountingPeriod_RegularHours.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_MainFormTab);
mTab = LDMCTIME_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Entry Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on EntryInformation_EnteredBy...", Logger.MessageType.INF);
			Control LDMCTIME_EntryInformation_EnteredBy = new Control("EntryInformation_EnteredBy", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TH___USER_ID']");
			CPCommon.AssertEqual(true,LDMCTIME_EntryInformation_EnteredBy.Exists());

											Driver.SessionLogger.WriteLine("TimesheetLineForm");


												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLineForm...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLineForm = new Control("TimesheetLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLineForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExist on TimesheetLineFormTable...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLineFormTable = new Control("TimesheetLineFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLineFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLineForm);
formBttn = LDMCTIME_TimesheetLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMCTIME_TimesheetLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMCTIME_TimesheetLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLineTab...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLineTab = new Control("TimesheetLineTab", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLineTab.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLineTab);
mTab = LDMCTIME_TimesheetLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Charge Data").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_ChargeData_LineType...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_ChargeData_LineType = new Control("TimesheetLine_ChargeData_LineType", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='TS_LN___S_TS_LN_TYPE_CD']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_ChargeData_LineType.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLineTab);
mTab = LDMCTIME_TimesheetLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Miscellaneous").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_Miscellaneous_AC2...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_Miscellaneous_AC2 = new Control("TimesheetLine_Miscellaneous_AC2", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLN_']/ancestor::form[1]/descendant::*[@id='TS_LN___REF_STRUC_2_ID']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_Miscellaneous_AC2.Exists());

											Driver.SessionLogger.WriteLine("Manufacturing Order Timesheet Information Form");


												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_ManufacturingOrderTimesheetInformationLink...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationLink = new Control("TimesheetLine_ManufacturingOrderTimesheetInformationLink", "ID", "lnk_2597_LDMCTIME_TSLN");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationLink.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationLink);
LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_ManufacturingOrderTimesheetInformationForm...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm = new Control("TimesheetLine_ManufacturingOrderTimesheetInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLNMO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder = new Control("TimesheetLine_ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLNMO_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformation_ManufacturingOrder_ManufacturingOrder.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm);
formBttn = LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExist on TimesheetLine_ManufacturingOrderTimesheetInformationFormTable...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationFormTable = new Control("TimesheetLine_ManufacturingOrderTimesheetInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLNMO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm);
formBttn = LDMCTIME_TimesheetLine_ManufacturingOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Sales Order Timesheet Information Form");


												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_SalesOrderTimesheetInformationLink...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationLink = new Control("TimesheetLine_SalesOrderTimesheetInformationLink", "ID", "lnk_2598_LDMCTIME_TSLN");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationLink.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationLink);
LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_SalesOrderTimesheetInformationForm...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm = new Control("TimesheetLine_SalesOrderTimesheetInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLNSO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExists on TimesheetLine_SalesOrderTimesheetInformation_SalesOrder...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_SalesOrderTimesheetInformation_SalesOrder = new Control("TimesheetLine_SalesOrderTimesheetInformation_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLNSO_']/ancestor::form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_SalesOrderTimesheetInformation_SalesOrder.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm);
formBttn = LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMCTIME";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCTIME] Perfoming VerifyExist on TimesheetLine_SalesOrderTimesheetInformationFormTable...", Logger.MessageType.INF);
			Control LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationFormTable = new Control("TimesheetLine_SalesOrderTimesheetInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCTIME_TSLNSO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm);
formBttn = LDMCTIME_TimesheetLine_SalesOrderTimesheetInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDMCTIME";
							CPCommon.WaitControlDisplayed(LDMCTIME_MainForm);
formBttn = LDMCTIME_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

