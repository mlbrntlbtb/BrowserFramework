 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMERF_SMOKE : TestScript
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
new Control("Payroll History", "xpath","//div[@class='navItem'][.='Payroll History']").Click();
new Control("Manage Employee Earnings History", "xpath","//div[@class='navItem'][.='Manage Employee Earnings History']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMERF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMERF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on EMPLOYEE...", Logger.MessageType.INF);
			Control PRMERF_EMPLOYEE = new Control("EMPLOYEE", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRMERF_EMPLOYEE.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_MainForm);
IWebElement formBttn = PRMERF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMERF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMERF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMERF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("STATE PAY TYPES LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on StatePayTypesLink...", Logger.MessageType.INF);
			Control PRMERF_StatePayTypesLink = new Control("StatePayTypesLink", "ID", "lnk_15363_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_StatePayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_StatePayTypesLink);
PRMERF_StatePayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on StatePayTypesForm...", Logger.MessageType.INF);
			Control PRMERF_StatePayTypesForm = new Control("StatePayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_PAYTYPE_STATE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_StatePayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on StatePayTypesTable...", Logger.MessageType.INF);
			Control PRMERF_StatePayTypesTable = new Control("StatePayTypesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_PAYTYPE_STATE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_StatePayTypesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on StatePayType_YTDInfoLink...", Logger.MessageType.INF);
			Control PRMERF_StatePayType_YTDInfoLink = new Control("StatePayType_YTDInfoLink", "ID", "lnk_15365_PRMERF_PAYTYPE_STATE");
			CPCommon.AssertEqual(true,PRMERF_StatePayType_YTDInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_StatePayTypesForm);
formBttn = PRMERF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PAY TYPES LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on PayTypesLink...", Logger.MessageType.INF);
			Control PRMERF_PayTypesLink = new Control("PayTypesLink", "ID", "lnk_15358_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_PayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_PayTypesLink);
PRMERF_PayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on PayTypesForm...", Logger.MessageType.INF);
			Control PRMERF_PayTypesForm = new Control("PayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_PAYTYPE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_PayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on PayTypesTable...", Logger.MessageType.INF);
			Control PRMERF_PayTypesTable = new Control("PayTypesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_PAYTYPE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_PayTypesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on PayTypeProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMERF_PayTypeProjectDistributionLink = new Control("PayTypeProjectDistributionLink", "ID", "lnk_15360_PRMERF_PAYTYPE");
			CPCommon.AssertEqual(true,PRMERF_PayTypeProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on PayType_YTDInfoLink...", Logger.MessageType.INF);
			Control PRMERF_PayType_YTDInfoLink = new Control("PayType_YTDInfoLink", "ID", "lnk_15361_PRMERF_PAYTYPE");
			CPCommon.AssertEqual(true,PRMERF_PayType_YTDInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_PayTypesForm);
formBttn = PRMERF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("STATE TAXES LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on StateTaxesLink...", Logger.MessageType.INF);
			Control PRMERF_StateTaxesLink = new Control("StateTaxesLink", "ID", "lnk_15351_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_StateTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_StateTaxesLink);
PRMERF_StateTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on StateTaxesForm...", Logger.MessageType.INF);
			Control PRMERF_StateTaxesForm = new Control("StateTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_TAXES_STATE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_StateTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on StateTaxesFormTable...", Logger.MessageType.INF);
			Control PRMERF_StateTaxesFormTable = new Control("StateTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_TAXES_STATE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_StateTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_StateTaxesForm);
formBttn = PRMERF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LOCAL TAXES LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on LocalTaxesLink...", Logger.MessageType.INF);
			Control PRMERF_LocalTaxesLink = new Control("LocalTaxesLink", "ID", "lnk_15352_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_LocalTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_LocalTaxesLink);
PRMERF_LocalTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on LocalTaxesForm...", Logger.MessageType.INF);
			Control PRMERF_LocalTaxesForm = new Control("LocalTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_TAXES_LOCAL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_LocalTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on LocalTaxesFormTable...", Logger.MessageType.INF);
			Control PRMERF_LocalTaxesFormTable = new Control("LocalTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_TAXES_LOCAL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_LocalTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_LocalTaxesForm);
formBttn = PRMERF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DEDUCTION LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on DeductionsLink...", Logger.MessageType.INF);
			Control PRMERF_DeductionsLink = new Control("DeductionsLink", "ID", "lnk_15371_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_DeductionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_DeductionsLink);
PRMERF_DeductionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on DeductionsForm...", Logger.MessageType.INF);
			Control PRMERF_DeductionsForm = new Control("DeductionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_DED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_DeductionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on DeductionsTable...", Logger.MessageType.INF);
			Control PRMERF_DeductionsTable = new Control("DeductionsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_DED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_DeductionsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on Deductions_UnionDeductionDetailLink...", Logger.MessageType.INF);
			Control PRMERF_Deductions_UnionDeductionDetailLink = new Control("Deductions_UnionDeductionDetailLink", "ID", "lnk_15372_PRMERF_DED");
			CPCommon.AssertEqual(true,PRMERF_Deductions_UnionDeductionDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on Deductions_GarnishmentDetailLink...", Logger.MessageType.INF);
			Control PRMERF_Deductions_GarnishmentDetailLink = new Control("Deductions_GarnishmentDetailLink", "ID", "lnk_15373_PRMERF_DED");
			CPCommon.AssertEqual(true,PRMERF_Deductions_GarnishmentDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on Deductions_YTDInfoLink...", Logger.MessageType.INF);
			Control PRMERF_Deductions_YTDInfoLink = new Control("Deductions_YTDInfoLink", "ID", "lnk_15376_PRMERF_DED");
			CPCommon.AssertEqual(true,PRMERF_Deductions_YTDInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_DeductionsForm);
formBttn = PRMERF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("WORKERS' COMP");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on WorkersCompLink...", Logger.MessageType.INF);
			Control PRMERF_WorkersCompLink = new Control("WorkersCompLink", "ID", "lnk_15367_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_WorkersCompLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_WorkersCompLink);
PRMERF_WorkersCompLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on WorkersCompForm...", Logger.MessageType.INF);
			Control PRMERF_WorkersCompForm = new Control("WorkersCompForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_WC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_WorkersCompForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on WorkersCompTable...", Logger.MessageType.INF);
			Control PRMERF_WorkersCompTable = new Control("WorkersCompTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_WC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_WorkersCompTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on WorkersComProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMERF_WorkersComProjectDistributionLink = new Control("WorkersComProjectDistributionLink", "ID", "lnk_15368_PRMERF_WC");
			CPCommon.AssertEqual(true,PRMERF_WorkersComProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on WorkersComp_YTDInfoLink...", Logger.MessageType.INF);
			Control PRMERF_WorkersComp_YTDInfoLink = new Control("WorkersComp_YTDInfoLink", "ID", "lnk_15369_PRMERF_WC");
			CPCommon.AssertEqual(true,PRMERF_WorkersComp_YTDInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_WorkersCompForm);
formBttn = PRMERF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TAX WITHHOLDING PROJECT DISTRIBUTION LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on TaxWithholdingProjectDistributionLink...", Logger.MessageType.INF);
			Control PRMERF_TaxWithholdingProjectDistributionLink = new Control("TaxWithholdingProjectDistributionLink", "ID", "lnk_15353_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_TaxWithholdingProjectDistributionLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_TaxWithholdingProjectDistributionLink);
PRMERF_TaxWithholdingProjectDistributionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on TaxesWithheldProjectDistributionForm...", Logger.MessageType.INF);
			Control PRMERF_TaxesWithheldProjectDistributionForm = new Control("TaxesWithheldProjectDistributionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_PROJDIST_TAXES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_TaxesWithheldProjectDistributionForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on TaxesWithheldProjectDistributionTable...", Logger.MessageType.INF);
			Control PRMERF_TaxesWithheldProjectDistributionTable = new Control("TaxesWithheldProjectDistributionTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_PROJDIST_TAXES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_TaxesWithheldProjectDistributionTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_TaxesWithheldProjectDistributionForm);
formBttn = PRMERF_TaxesWithheldProjectDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("YTD TAXES LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on YTDTaxesLink...", Logger.MessageType.INF);
			Control PRMERF_YTDTaxesLink = new Control("YTDTaxesLink", "ID", "lnk_15354_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_YTDTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_YTDTaxesLink);
PRMERF_YTDTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on TaxesYTDInfoForm...", Logger.MessageType.INF);
			Control PRMERF_TaxesYTDInfoForm = new Control("TaxesYTDInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_YTD_TAXES_WH_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_TaxesYTDInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on TaxesYTDInfo_YTDTaxes_Taxable_Federal...", Logger.MessageType.INF);
			Control PRMERF_TaxesYTDInfo_YTDTaxes_Taxable_Federal = new Control("TaxesYTDInfo_YTDTaxes_Taxable_Federal", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_YTD_TAXES_WH_']/ancestor::form[1]/descendant::*[@id='FED_TXBL_AMT']");
			CPCommon.AssertEqual(true,PRMERF_TaxesYTDInfo_YTDTaxes_Taxable_Federal.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_TaxesYTDInfoForm);
formBttn = PRMERF_TaxesYTDInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TIMESHEETS LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on TimesheetsLink...", Logger.MessageType.INF);
			Control PRMERF_TimesheetsLink = new Control("TimesheetsLink", "ID", "lnk_15385_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_TimesheetsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_TimesheetsLink);
PRMERF_TimesheetsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on TimesheetsForm...", Logger.MessageType.INF);
			Control PRMERF_TimesheetsForm = new Control("TimesheetsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_TIMESHEET_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_TimesheetsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on TimesheetsTable...", Logger.MessageType.INF);
			Control PRMERF_TimesheetsTable = new Control("TimesheetsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_TIMESHEET_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_TimesheetsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_TimesheetsForm);
formBttn = PRMERF_TimesheetsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DIRECT DEPOSIT INFORMATION LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on DirectDepositInformationLink...", Logger.MessageType.INF);
			Control PRMERF_DirectDepositInformationLink = new Control("DirectDepositInformationLink", "ID", "lnk_15386_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_DirectDepositInformationLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_DirectDepositInformationLink);
PRMERF_DirectDepositInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on DirectDepositInfoForm...", Logger.MessageType.INF);
			Control PRMERF_DirectDepositInfoForm = new Control("DirectDepositInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_DIRDEPOSIT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_DirectDepositInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on DirectDepositInfoTable...", Logger.MessageType.INF);
			Control PRMERF_DirectDepositInfoTable = new Control("DirectDepositInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_DIRDEPOSIT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_DirectDepositInfoTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_DirectDepositInfoForm);
formBttn = PRMERF_DirectDepositInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("FRINGE DETAIL LINK");


												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on FringeDetailLink...", Logger.MessageType.INF);
			Control PRMERF_FringeDetailLink = new Control("FringeDetailLink", "ID", "lnk_15384_PRMERF_EARNINGS");
			CPCommon.AssertEqual(true,PRMERF_FringeDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_FringeDetailLink);
PRMERF_FringeDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExists on FringeDetailForm...", Logger.MessageType.INF);
			Control PRMERF_FringeDetailForm = new Control("FringeDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_FRNG_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMERF_FringeDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMERF] Perfoming VerifyExist on FringeDetailTable...", Logger.MessageType.INF);
			Control PRMERF_FringeDetailTable = new Control("FringeDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMERF_FRNG_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMERF_FringeDetailTable.Exists());

												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_FringeDetailForm);
formBttn = PRMERF_FringeDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMERF";
							CPCommon.WaitControlDisplayed(PRMERF_MainForm);
formBttn = PRMERF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

