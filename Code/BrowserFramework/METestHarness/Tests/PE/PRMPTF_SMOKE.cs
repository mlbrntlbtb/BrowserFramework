 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMPTF_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Processing", "xpath","//div[@class='navItem'][.='Payroll Processing']").Click();
new Control("Manage Payroll Records", "xpath","//div[@class='navItem'][.='Manage Payroll Records']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMPTF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMPTF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on EMPLOYEE...", Logger.MessageType.INF);
			Control PRMPTF_EMPLOYEE = new Control("EMPLOYEE", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRMPTF_EMPLOYEE.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_MainForm);
IWebElement formBttn = PRMPTF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMPTF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMPTF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMPTF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("STATE PAY RATES LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on StatePayTypesLink...", Logger.MessageType.INF);
			Control PRMPTF_StatePayTypesLink = new Control("StatePayTypesLink", "ID", "lnk_15568_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_StatePayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_StatePayTypesLink);
PRMPTF_StatePayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on StatePayTypesForm...", Logger.MessageType.INF);
			Control PRMPTF_StatePayTypesForm = new Control("StatePayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_PAYTYPE_STATE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_StatePayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on StatePayTypesTable...", Logger.MessageType.INF);
			Control PRMPTF_StatePayTypesTable = new Control("StatePayTypesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_PAYTYPE_STATE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_StatePayTypesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_StatePayTypesForm);
formBttn = PRMPTF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PAY TYPES LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on PayTypesLink...", Logger.MessageType.INF);
			Control PRMPTF_PayTypesLink = new Control("PayTypesLink", "ID", "lnk_15565_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_PayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_PayTypesLink);
PRMPTF_PayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on PayTypesForm...", Logger.MessageType.INF);
			Control PRMPTF_PayTypesForm = new Control("PayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_PAYTYPE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_PayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on PayTypesTable...", Logger.MessageType.INF);
			Control PRMPTF_PayTypesTable = new Control("PayTypesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_PAYTYPE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_PayTypesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on PayTypeProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMPTF_PayTypeProjectDistributionLink = new Control("PayTypeProjectDistributionLink", "ID", "lnk_15567_PRMPTF_PAYTYPE");
			CPCommon.AssertEqual(true,PRMPTF_PayTypeProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_PayTypesForm);
formBttn = PRMPTF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("STATE TAXES LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on StateTaxesLink...", Logger.MessageType.INF);
			Control PRMPTF_StateTaxesLink = new Control("StateTaxesLink", "ID", "lnk_15528_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_StateTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_StateTaxesLink);
PRMPTF_StateTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on StateTaxesForm...", Logger.MessageType.INF);
			Control PRMPTF_StateTaxesForm = new Control("StateTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_TAXES_STATE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_StateTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on StateTaxesTable...", Logger.MessageType.INF);
			Control PRMPTF_StateTaxesTable = new Control("StateTaxesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_TAXES_STATE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_StateTaxesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_StateTaxesForm);
formBttn = PRMPTF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LOCAL TAXES LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on LocalTaxesLink...", Logger.MessageType.INF);
			Control PRMPTF_LocalTaxesLink = new Control("LocalTaxesLink", "ID", "lnk_15529_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_LocalTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_LocalTaxesLink);
PRMPTF_LocalTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on LocalTaxesForm...", Logger.MessageType.INF);
			Control PRMPTF_LocalTaxesForm = new Control("LocalTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_TAXES_LOCAL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_LocalTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on LocalTaxesTable...", Logger.MessageType.INF);
			Control PRMPTF_LocalTaxesTable = new Control("LocalTaxesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_TAXES_LOCAL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_LocalTaxesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_LocalTaxesForm);
formBttn = PRMPTF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DEDUCTIONS LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on DeductionsLink...", Logger.MessageType.INF);
			Control PRMPTF_DeductionsLink = new Control("DeductionsLink", "ID", "lnk_15572_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_DeductionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_DeductionsLink);
PRMPTF_DeductionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on DeductionsForm...", Logger.MessageType.INF);
			Control PRMPTF_DeductionsForm = new Control("DeductionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_DED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_DeductionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on DeductionsTable...", Logger.MessageType.INF);
			Control PRMPTF_DeductionsTable = new Control("DeductionsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_DED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_DeductionsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on UnionDeductionDetailLink...", Logger.MessageType.INF);
			Control PRMPTF_UnionDeductionDetailLink = new Control("UnionDeductionDetailLink", "ID", "lnk_15573_PRMPTF_DED");
			CPCommon.AssertEqual(true,PRMPTF_UnionDeductionDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on GarnishmentDetailLink...", Logger.MessageType.INF);
			Control PRMPTF_GarnishmentDetailLink = new Control("GarnishmentDetailLink", "ID", "lnk_15574_PRMPTF_DED");
			CPCommon.AssertEqual(true,PRMPTF_GarnishmentDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_DeductionsForm);
formBttn = PRMPTF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CONTRIBUTIONS LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on ContributionsLink...", Logger.MessageType.INF);
			Control PRMPTF_ContributionsLink = new Control("ContributionsLink", "ID", "lnk_15575_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_ContributionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_ContributionsLink);
PRMPTF_ContributionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on ContributionsForm...", Logger.MessageType.INF);
			Control PRMPTF_ContributionsForm = new Control("ContributionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_CNTRB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_ContributionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on ContributionsTable...", Logger.MessageType.INF);
			Control PRMPTF_ContributionsTable = new Control("ContributionsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_CNTRB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_ContributionsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on ContributionProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMPTF_ContributionProjectDistributionLink = new Control("ContributionProjectDistributionLink", "ID", "lnk_15576_PRMPTF_CNTRB");
			CPCommon.AssertEqual(true,PRMPTF_ContributionProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_ContributionsForm);
formBttn = PRMPTF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("WORKERS' COMP LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on WorkersCompLink...", Logger.MessageType.INF);
			Control PRMPTF_WorkersCompLink = new Control("WorkersCompLink", "ID", "lnk_15570_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_WorkersCompLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_WorkersCompLink);
PRMPTF_WorkersCompLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on WorkersCompForm...", Logger.MessageType.INF);
			Control PRMPTF_WorkersCompForm = new Control("WorkersCompForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_WC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_WorkersCompForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on WorkersCompTable...", Logger.MessageType.INF);
			Control PRMPTF_WorkersCompTable = new Control("WorkersCompTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_WC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_WorkersCompTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on WorkersComProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMPTF_WorkersComProjectDistributionLink = new Control("WorkersComProjectDistributionLink", "ID", "lnk_15571_PRMPTF_WC");
			CPCommon.AssertEqual(true,PRMPTF_WorkersComProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_WorkersCompForm);
formBttn = PRMPTF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TAX WITHHOLDING PROJECT DISTRIBUTION LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on TaxWithholdingProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMPTF_TaxWithholdingProjectDistributionLink = new Control("TaxWithholdingProjectDistributionLink", "ID", "lnk_15530_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_TaxWithholdingProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_TaxWithholdingProjectDistributionLink);
PRMPTF_TaxWithholdingProjectDistributionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on TaxesWithheldProjectDistributionForm...", Logger.MessageType.INF);
			Control PRMPTF_TaxesWithheldProjectDistributionForm = new Control("TaxesWithheldProjectDistributionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_PROJDIST_TAXES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_TaxesWithheldProjectDistributionForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on TaxesWithheldProjectDistributionTable...", Logger.MessageType.INF);
			Control PRMPTF_TaxesWithheldProjectDistributionTable = new Control("TaxesWithheldProjectDistributionTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_PROJDIST_TAXES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_TaxesWithheldProjectDistributionTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_TaxesWithheldProjectDistributionForm);
formBttn = PRMPTF_TaxesWithheldProjectDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TIMESHEETS LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on TimesheetsLink...", Logger.MessageType.INF);
			Control PRMPTF_TimesheetsLink = new Control("TimesheetsLink", "ID", "lnk_15577_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_TimesheetsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_TimesheetsLink);
PRMPTF_TimesheetsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on TimesheetsForm...", Logger.MessageType.INF);
			Control PRMPTF_TimesheetsForm = new Control("TimesheetsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_TIMESHEET_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_TimesheetsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on TimesheetsTable...", Logger.MessageType.INF);
			Control PRMPTF_TimesheetsTable = new Control("TimesheetsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_TIMESHEET_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_TimesheetsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_TimesheetsForm);
formBttn = PRMPTF_TimesheetsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DIRECT DEPOSIT INFORMATION LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on DirectDepositInformationLink...", Logger.MessageType.INF);
			Control PRMPTF_DirectDepositInformationLink = new Control("DirectDepositInformationLink", "ID", "lnk_15578_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_DirectDepositInformationLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_DirectDepositInformationLink);
PRMPTF_DirectDepositInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on DirectDepositInfoForm...", Logger.MessageType.INF);
			Control PRMPTF_DirectDepositInfoForm = new Control("DirectDepositInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_DIRDEPOSIT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_DirectDepositInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on DirectDepositInfoTable...", Logger.MessageType.INF);
			Control PRMPTF_DirectDepositInfoTable = new Control("DirectDepositInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_DIRDEPOSIT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_DirectDepositInfoTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_DirectDepositInfoForm);
formBttn = PRMPTF_DirectDepositInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("FRINGE DETAIL LINK");


												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on FringeDetailLink...", Logger.MessageType.INF);
			Control PRMPTF_FringeDetailLink = new Control("FringeDetailLink", "ID", "lnk_15579_PRMPTF_EDIT_PAYROLL");
			CPCommon.AssertEqual(true,PRMPTF_FringeDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_FringeDetailLink);
PRMPTF_FringeDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExists on FrigeDetailForm...", Logger.MessageType.INF);
			Control PRMPTF_FrigeDetailForm = new Control("FrigeDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_FRNG_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPTF_FrigeDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPTF] Perfoming VerifyExist on FrigeDetailTable...", Logger.MessageType.INF);
			Control PRMPTF_FrigeDetailTable = new Control("FrigeDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPTF_FRNG_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPTF_FrigeDetailTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_FrigeDetailForm);
formBttn = PRMPTF_FrigeDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("");


												
				CPCommon.CurrentComponent = "PRMPTF";
							CPCommon.WaitControlDisplayed(PRMPTF_MainForm);
formBttn = PRMPTF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

