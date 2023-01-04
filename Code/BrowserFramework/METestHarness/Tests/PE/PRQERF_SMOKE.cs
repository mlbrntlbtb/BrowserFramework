 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQERF_SMOKE : TestScript
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
new Control("View Employee Earnings", "xpath","//div[@class='navItem'][.='View Employee Earnings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQERF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQERF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQERF_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQERF_Identification_Employee.Exists());

												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("FEDERAL/STATE WAGES AND TAXES");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxesForm...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxesForm = new Control("FederalStateWagesandTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEADT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on FederalStateWagesandTaxesFormTable...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxesFormTable = new Control("FederalStateWagesandTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEADT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxesForm);
IWebElement formBttn = PRQERF_FederalStateWagesandTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_FederalStateWagesandTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_FederalStateWagesandTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_TransactionType = new Control("FederalStateWagesandTaxes_EmployeeEarnings_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEADT_CHLD_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_TransactionType.Exists());

											Driver.SessionLogger.WriteLine("EMPLOYEE EARNINGS TAB");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control PRQERF_MainFormTab = new Control("MainFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEADT_CHLD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PRQERF_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_MainFormTab);
IWebElement mTab = PRQERF_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Employee Earnings").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_FederalTaxes_ExemptPayTypes_Federal...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_FederalTaxes_ExemptPayTypes_Federal = new Control("FederalStateWagesandTaxes_EmployeeEarnings_FederalTaxes_ExemptPayTypes_Federal", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEADT_CHLD_']/ancestor::form[1]/descendant::*[@id='FED_EXMPT_PT_AMT']");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_FederalTaxes_ExemptPayTypes_Federal.Exists());

											Driver.SessionLogger.WriteLine("EMPLYEE TAX SETUP TAB");


												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.AssertEqual(true,PRQERF_MainFormTab.Exists());

													
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_MainFormTab);
mTab = PRQERF_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Employee Tax Setup").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeTaxSetup_FederalAndSUTA_Exemptions...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeTaxSetup_FederalAndSUTA_Exemptions = new Control("FederalStateWagesandTaxes_EmployeeTaxSetup_FederalAndSUTA_Exemptions", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEADT_CHLD_']/ancestor::form[1]/descendant::*[@id='FED_EXMPT_NO']");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeTaxSetup_FederalAndSUTA_Exemptions.Exists());

											Driver.SessionLogger.WriteLine("STATE PAY TYPES LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_StatePayTypesLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StatePayTypesLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_StatePayTypesLink", "ID", "lnk_16809_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StatePayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StatePayTypesLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StatePayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on StatePayTypesForm...", Logger.MessageType.INF);
			Control PRQERF_StatePayTypesForm = new Control("StatePayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLSTATEPAYADT_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_StatePayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on StatePayTypes_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_StatePayTypes_TransactionType = new Control("StatePayTypes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLSTATEPAYADT_DETL_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_StatePayTypes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_StatePayTypesForm);
formBttn = PRQERF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRQERF_StatePayTypesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRQERF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on StatePayTypesFormTable...", Logger.MessageType.INF);
			Control PRQERF_StatePayTypesFormTable = new Control("StatePayTypesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLSTATEPAYADT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_StatePayTypesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_StatePayTypesForm);
formBttn = PRQERF_StatePayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PAY TYPES LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_PayTypesLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_PayTypesLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_PayTypesLink", "ID", "lnk_1005257_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_PayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_PayTypesLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_PayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on PayTypesFormTable...", Logger.MessageType.INF);
			Control PRQERF_PayTypesFormTable = new Control("PayTypesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEPAYTPADT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_PayTypesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on PayTypesForm...", Logger.MessageType.INF);
			Control PRQERF_PayTypesForm = new Control("PayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEPAYTPADT_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_PayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_PayTypesForm);
formBttn = PRQERF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_PayTypesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on PayTypes_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_PayTypes_TransactionType = new Control("PayTypes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEPAYTPADT_DETL_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_PayTypes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_PayTypesForm);
formBttn = PRQERF_PayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("STATE TAXES LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_StateTaxesLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StateTaxesLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_StateTaxesLink", "ID", "lnk_5727_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StateTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StateTaxesLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_StateTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on StateTaxesFormTable...", Logger.MessageType.INF);
			Control PRQERF_StateTaxesFormTable = new Control("StateTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLESTATEADT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_StateTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on StateTaxesForm...", Logger.MessageType.INF);
			Control PRQERF_StateTaxesForm = new Control("StateTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLESTATEADT_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_StateTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_StateTaxesForm);
formBttn = PRQERF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_StateTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on StateTaxes_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_StateTaxes_TransactionType = new Control("StateTaxes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLESTATEADT_DETL_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_StateTaxes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_StateTaxesForm);
formBttn = PRQERF_StateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LOCAL TAXES LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_LocalTaxesLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_LocalTaxesLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_LocalTaxesLink", "ID", "lnk_1005264_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_LocalTaxesLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_LocalTaxesLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_LocalTaxesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on LocalTaxesFormTable...", Logger.MessageType.INF);
			Control PRQERF_LocalTaxesFormTable = new Control("LocalTaxesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLELOCALADT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_LocalTaxesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on LocalTaxesForm...", Logger.MessageType.INF);
			Control PRQERF_LocalTaxesForm = new Control("LocalTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLELOCALADT_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_LocalTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_LocalTaxesForm);
formBttn = PRQERF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_LocalTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on LocalTaxes_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_LocalTaxes_TransactionType = new Control("LocalTaxes_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLELOCALADT_DETL_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_LocalTaxes_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_LocalTaxesForm);
formBttn = PRQERF_LocalTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DEDUCTION LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_DeductionsLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_DeductionsLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_DeductionsLink", "ID", "lnk_1005260_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_DeductionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_DeductionsLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_DeductionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on DeductionsFormTable...", Logger.MessageType.INF);
			Control PRQERF_DeductionsFormTable = new Control("DeductionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEDEDADT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_DeductionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on DeductionsForm...", Logger.MessageType.INF);
			Control PRQERF_DeductionsForm = new Control("DeductionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEDEDADT_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_DeductionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_DeductionsForm);
formBttn = PRQERF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_DeductionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on Deductions_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_Deductions_TransactionType = new Control("Deductions_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEDEDADT_DETL_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_Deductions_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_DeductionsForm);
formBttn = PRQERF_DeductionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CONTRIBUTIONS LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_ContributionsLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_ContributionsLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_ContributionsLink", "ID", "lnk_1005263_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_ContributionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_ContributionsLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_ContributionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on ContributionsFormTable...", Logger.MessageType.INF);
			Control PRQERF_ContributionsFormTable = new Control("ContributionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLECNTRBADT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_ContributionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on ContributionsForm...", Logger.MessageType.INF);
			Control PRQERF_ContributionsForm = new Control("ContributionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLECNTRBADT_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_ContributionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_ContributionsForm);
formBttn = PRQERF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_ContributionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on Contributions_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_Contributions_TransactionType = new Control("Contributions_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLECNTRBADT_DETL_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_Contributions_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_ContributionsForm);
formBttn = PRQERF_ContributionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("WORKERS' COMP LINK");


												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on FederalStateWagesandTaxes_EmployeeEarnings_WorkersCompLink...", Logger.MessageType.INF);
			Control PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_WorkersCompLink = new Control("FederalStateWagesandTaxes_EmployeeEarnings_WorkersCompLink", "ID", "lnk_1005258_PRQERF_EMPLEADT_CHLD");
			CPCommon.AssertEqual(true,PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_WorkersCompLink.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_WorkersCompLink);
PRQERF_FederalStateWagesandTaxes_EmployeeEarnings_WorkersCompLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExist on WorkersCompFormTable...", Logger.MessageType.INF);
			Control PRQERF_WorkersCompFormTable = new Control("WorkersCompFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEWCADT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQERF_WorkersCompFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on WorkersCompForm...", Logger.MessageType.INF);
			Control PRQERF_WorkersCompForm = new Control("WorkersCompForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEWCADT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQERF_WorkersCompForm.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_WorkersCompForm);
formBttn = PRQERF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQERF_WorkersCompForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQERF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQERF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQERF] Perfoming VerifyExists on WorkersComp_TransactionType...", Logger.MessageType.INF);
			Control PRQERF_WorkersComp_TransactionType = new Control("WorkersComp_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQERF_EMPLEWCADT_CHLD_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQERF_WorkersComp_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_WorkersCompForm);
formBttn = PRQERF_WorkersCompForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQERF";
							CPCommon.WaitControlDisplayed(PRQERF_MainForm);
formBttn = PRQERF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

