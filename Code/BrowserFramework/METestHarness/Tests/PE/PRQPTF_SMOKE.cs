 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQPTF_SMOKE : TestScript
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
new Control("Payroll Reports/Inquiries", "xpath","//div[@class='navItem'][.='Payroll Reports/Inquiries']").Click();
new Control("View Payroll Edit Table", "xpath","//div[@class='navItem'][.='View Payroll Edit Table']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQPTF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQPTF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming ClickButton on FederalStateWagesandTaxesForm...", Logger.MessageType.INF);
			Control PRQPTF_FederalStateWagesandTaxesForm = new Control("FederalStateWagesandTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPAYROLLADT_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PRQPTF_FederalStateWagesandTaxesForm);
IWebElement formBttn = PRQPTF_FederalStateWagesandTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PRQPTF_FederalStateWagesandTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PRQPTF_FederalStateWagesandTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on TransactionType_Add...", Logger.MessageType.INF);
			Control PRQPTF_TransactionType_Add = new Control("TransactionType_Add", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ADD']");
			CPCommon.AssertEqual(true,PRQPTF_TransactionType_Add.Exists());

											Driver.SessionLogger.WriteLine("Federal State Wages and Taxes");


												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.AssertEqual(true,PRQPTF_FederalStateWagesandTaxesForm.Exists());

													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on FederalStateWagesandTaxesFormTable...", Logger.MessageType.INF);
			Control PRQPTF_FederalStateWagesandTaxesFormTable = new Control("FederalStateWagesandTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPAYROLLADT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_FederalStateWagesandTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_FederalStateWagesandTaxesForm);
formBttn = PRQPTF_FederalStateWagesandTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_FederalStateWagesandTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_FederalStateWagesandTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on FederalStateWagesAndTaxes_Details_Employee...", Logger.MessageType.INF);
			Control PRQPTF_FederalStateWagesAndTaxes_Details_Employee = new Control("FederalStateWagesAndTaxes_Details_Employee", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPAYROLLADT_CHLD_']/ancestor::form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQPTF_FederalStateWagesAndTaxes_Details_Employee.Exists());

											Driver.SessionLogger.WriteLine("Sate Pay Types");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on StatePayTypesLink...", Logger.MessageType.INF);
			Control PRQPTF_StatePayTypesLink = new Control("StatePayTypesLink", "ID", "lnk_16808_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_StatePayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_StatePayTypesLink);
PRQPTF_StatePayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on StatePayTypesForm...", Logger.MessageType.INF);
			Control PRQPTF_StatePayTypesForm = new Control("StatePayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRSTCDADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_StatePayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on StatePayTypesFormTable...", Logger.MessageType.INF);
			Control PRQPTF_StatePayTypesFormTable = new Control("StatePayTypesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRSTCDADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_StatePayTypesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_StatePayTypesForm);
formBttn = PRQPTF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_StatePayTypesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on StatePayTypes_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_StatePayTypes_TransactionType = new Control("StatePayTypes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRSTCDADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_StatePayTypes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_StatePayTypesForm);
formBttn = PRQPTF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Pay Types");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on PayTypesLink...", Logger.MessageType.INF);
			Control PRQPTF_PayTypesLink = new Control("PayTypesLink", "ID", "lnk_5589_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_PayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_PayTypesLink);
PRQPTF_PayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on PayTypesForm...", Logger.MessageType.INF);
			Control PRQPTF_PayTypesForm = new Control("PayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRPAYTPADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_PayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on PayTypesFormTable...", Logger.MessageType.INF);
			Control PRQPTF_PayTypesFormTable = new Control("PayTypesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRPAYTPADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_PayTypesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_PayTypesForm);
formBttn = PRQPTF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_PayTypesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on PayTypes_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_PayTypes_TransactionType = new Control("PayTypes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRPAYTPADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_PayTypes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_PayTypesForm);
formBttn = PRQPTF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("State Taxes");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on StateTaxesLink...", Logger.MessageType.INF);
			Control PRQPTF_StateTaxesLink = new Control("StateTaxesLink", "ID", "lnk_5593_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_StateTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_StateTaxesLink);
PRQPTF_StateTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on StateTaxesForm...", Logger.MessageType.INF);
			Control PRQPTF_StateTaxesForm = new Control("StateTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRSTATEADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_StateTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on StateTaxesFormTable...", Logger.MessageType.INF);
			Control PRQPTF_StateTaxesFormTable = new Control("StateTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRSTATEADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_StateTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_StateTaxesForm);
formBttn = PRQPTF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_StateTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on StateTaxes_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_StateTaxes_TransactionType = new Control("StateTaxes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRSTATEADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_StateTaxes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_StateTaxesForm);
formBttn = PRQPTF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Local Taxes");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on LocalTaxesLink...", Logger.MessageType.INF);
			Control PRQPTF_LocalTaxesLink = new Control("LocalTaxesLink", "ID", "lnk_5597_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_LocalTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_LocalTaxesLink);
PRQPTF_LocalTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on LocalTaxesForm...", Logger.MessageType.INF);
			Control PRQPTF_LocalTaxesForm = new Control("LocalTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRLOCALADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_LocalTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on LocalTaxesFormTable...", Logger.MessageType.INF);
			Control PRQPTF_LocalTaxesFormTable = new Control("LocalTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRLOCALADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_LocalTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_LocalTaxesForm);
formBttn = PRQPTF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_LocalTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on LocalTaxes_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_LocalTaxes_TransactionType = new Control("LocalTaxes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRLOCALADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_LocalTaxes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_LocalTaxesForm);
formBttn = PRQPTF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Deduction");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on DeductionsLink...", Logger.MessageType.INF);
			Control PRQPTF_DeductionsLink = new Control("DeductionsLink", "ID", "lnk_5594_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_DeductionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_DeductionsLink);
PRQPTF_DeductionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on DeductionsForm...", Logger.MessageType.INF);
			Control PRQPTF_DeductionsForm = new Control("DeductionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRDEDADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_DeductionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on DeductionsFormTable...", Logger.MessageType.INF);
			Control PRQPTF_DeductionsFormTable = new Control("DeductionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRDEDADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_DeductionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on Deductions_GarnishmentDetailLink...", Logger.MessageType.INF);
			Control PRQPTF_Deductions_GarnishmentDetailLink = new Control("Deductions_GarnishmentDetailLink", "ID", "lnk_5595_PRQPTF_EMPLPRDEDADT_CTW");
			CPCommon.AssertEqual(true,PRQPTF_Deductions_GarnishmentDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_DeductionsForm);
formBttn = PRQPTF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_DeductionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on Deductions_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_Deductions_TransactionType = new Control("Deductions_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRDEDADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_Deductions_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_DeductionsForm);
formBttn = PRQPTF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Contributions");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on ContributionsLink...", Logger.MessageType.INF);
			Control PRQPTF_ContributionsLink = new Control("ContributionsLink", "ID", "lnk_5596_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_ContributionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_ContributionsLink);
PRQPTF_ContributionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on ContributionsForm...", Logger.MessageType.INF);
			Control PRQPTF_ContributionsForm = new Control("ContributionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRCNTRBADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_ContributionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on ContributionsFormTable...", Logger.MessageType.INF);
			Control PRQPTF_ContributionsFormTable = new Control("ContributionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRCNTRBADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_ContributionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_ContributionsForm);
formBttn = PRQPTF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_ContributionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on Contributions_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_Contributions_TransactionType = new Control("Contributions_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRCNTRBADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_Contributions_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_ContributionsForm);
formBttn = PRQPTF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Workers' Comp");


												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on WorkersCompLink...", Logger.MessageType.INF);
			Control PRQPTF_WorkersCompLink = new Control("WorkersCompLink", "ID", "lnk_5591_PRQPTF_EMPLPAYROLLADT_CHLD");
			CPCommon.AssertEqual(true,PRQPTF_WorkersCompLink.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_WorkersCompLink);
PRQPTF_WorkersCompLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on WorkersCompForm...", Logger.MessageType.INF);
			Control PRQPTF_WorkersCompForm = new Control("WorkersCompForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRWCADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQPTF_WorkersCompForm.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExist on WorkersCompFormTable...", Logger.MessageType.INF);
			Control PRQPTF_WorkersCompFormTable = new Control("WorkersCompFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRWCADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQPTF_WorkersCompFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_WorkersCompForm);
formBttn = PRQPTF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQPTF_WorkersCompForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQPTF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQPTF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQPTF] Perfoming VerifyExists on WorkersComp_TransactionType...", Logger.MessageType.INF);
			Control PRQPTF_WorkersComp_TransactionType = new Control("WorkersComp_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQPTF_EMPLPRWCADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQPTF_WorkersComp_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_WorkersCompForm);
formBttn = PRQPTF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "PRQPTF";
							CPCommon.WaitControlDisplayed(PRQPTF_MainForm);
formBttn = PRQPTF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

