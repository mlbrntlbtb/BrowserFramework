 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJQPROJP_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Project Inquiry and Reporting", "xpath","//div[@class='deptItem'][.='Project Inquiry and Reporting']").Click();
new Control("Project Reports/Inquiries", "xpath","//div[@class='navItem'][.='Project Reports/Inquiries']").Click();
new Control("View Project Activity by Level", "xpath","//div[@class='navItem'][.='View Project Activity by Level']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJQPROJP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJQPROJP_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJQPROJP_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Budget Line Item Revisions");


												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTREVSUM_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm);
IWebElement formBttn = PJQPROJP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJQPROJP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJQPROJP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Set on Find_CriteriaValue1...", Logger.MessageType.INF);
			Control Query_Find_CriteriaValue1 = new Control("Find_CriteriaValue1", "ID", "basicField0");
			Query_Find_CriteriaValue1.Click();
Query_Find_CriteriaValue1.SendKeys("RTR4", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
Query_Find_CriteriaValue1.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PJQPROJP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTREVSUM_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatusLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatusLink = new Control("ChildForm_ProjectStatusLink", "ID", "lnk_1006686_PJQPROJP_RPTREVSUM_CHLD");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatusLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummaryLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummaryLink = new Control("ChildForm_GLCLaborSummaryLink", "ID", "lnk_1006705_PJQPROJP_RPTREVSUM_CHLD");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummaryLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummaryLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummaryLink = new Control("ChildForm_PLCLaborSummaryLink", "ID", "lnk_1006709_PJQPROJP_RPTREVSUM_CHLD");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummaryLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_EmployeeLaborSummaryLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_EmployeeLaborSummaryLink = new Control("ChildForm_EmployeeLaborSummaryLink", "ID", "lnk_1006713_PJQPROJP_RPTREVSUM_CHLD");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_EmployeeLaborSummaryLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_ProjectStatusLink);
PJQPROJP_ChildForm_ProjectStatusLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Project Status");


												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatusForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatusForm = new Control("ChildForm_ProjectStatusForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatusForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_PeriodOfPerformance...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_PeriodOfPerformance = new Control("ChildForm_ProjectStatus_PeriodOfPerformance", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRHDR_']/ancestor::form[1]/descendant::*[@id='PROJ_START_DT']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_PeriodOfPerformance.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_ProjectStatusChildForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildForm = new Control("ChildForm_ProjectStatus_ProjectStatusChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRFINALDATA_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExist on ChildForm_ProjectStatus_ProjectStatusChildFormTable...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildFormTable = new Control("ChildForm_ProjectStatus_ProjectStatusChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRFINALDATA_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildForm);
formBttn = PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_ProjectStatusChild_Description...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_Description = new Control("ChildForm_ProjectStatus_ProjectStatusChild_Description", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRFINALDATA_']/ancestor::form[1]/descendant::*[@id='DESCRIPTION']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_Description.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_ProjectStatusChild_LaborDetailLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_LaborDetailLink = new Control("ChildForm_ProjectStatus_ProjectStatusChild_LaborDetailLink", "ID", "lnk_1006688_PJQPROJP_PSRFINALDATA");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_LaborDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_ProjectStatusChild_TransactionDetailLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_TransactionDetailLink = new Control("ChildForm_ProjectStatus_ProjectStatusChild_TransactionDetailLink", "ID", "lnk_1006690_PJQPROJP_PSRFINALDATA");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_TransactionDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_ProjectStatusChild_PoolDetailLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_PoolDetailLink = new Control("ChildForm_ProjectStatus_ProjectStatusChild_PoolDetailLink", "ID", "lnk_1006694_PJQPROJP_PSRFINALDATA");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_PoolDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_ProjectStatus_ProjectStatusChild_ProfitsLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_ProfitsLink = new Control("ChildForm_ProjectStatus_ProjectStatusChild_ProfitsLink", "ID", "lnk_1007284_PJQPROJP_PSRFINALDATA");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_ProjectStatus_ProjectStatusChild_ProfitsLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_ProjectStatusForm);
formBttn = PJQPROJP_ChildForm_ProjectStatusForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("GLC Labor Summary");


												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_GLCLaborSummaryLink);
PJQPROJP_ChildForm_GLCLaborSummaryLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummaryForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummaryForm = new Control("ChildForm_GLCLaborSummaryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRHDR_GLCLABSUM_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummaryForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummary_PeriodOfPerformance...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_PeriodOfPerformance = new Control("ChildForm_GLCLaborSummary_PeriodOfPerformance", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRHDR_GLCLABSUM_']/ancestor::form[1]/descendant::*[@id='PROJ_START_DT']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummary_PeriodOfPerformance.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummaryForm.Exists());

													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExist on ChildForm_GLCLaborSummary_GLCLaborSummaryChildFormTable...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildFormTable = new Control("ChildForm_GLCLaborSummary_GLCLaborSummaryChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming ClickButton on ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm = new Control("ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm);
formBttn = PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLC...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLC = new Control("ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_']/ancestor::form[1]/descendant::*[@id='GEN_LAB_CAT']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLC.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLCLaborSummaryChildTab...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLCLaborSummaryChildTab = new Control("ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLCLaborSummaryChildTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLCLaborSummaryChildTab.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLCLaborSummaryChildTab);
IWebElement mTab = PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_GLCLaborSummaryChildTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Hours").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummary_GLCLaborSummaryChild_Hours_SubpdHrs...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_Hours_SubpdHrs = new Control("ChildForm_GLCLaborSummary_GLCLaborSummaryChild_Hours_SubpdHrs", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_']/ancestor::form[1]/descendant::*[@id='SUB_PD_ACT_HRS']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_Hours_SubpdHrs.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_GLCLaborSummary_GLCLaborSummaryChild_EmployeeDetailLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_EmployeeDetailLink = new Control("ChildForm_GLCLaborSummary_GLCLaborSummaryChild_EmployeeDetailLink", "ID", "lnk_1006707_PJQPROJP_RPTPROJLABSUM");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_GLCLaborSummary_GLCLaborSummaryChild_EmployeeDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_GLCLaborSummaryForm);
formBttn = PJQPROJP_ChildForm_GLCLaborSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PLC Labor Summary");


												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_PLCLaborSummaryLink);
PJQPROJP_ChildForm_PLCLaborSummaryLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummaryForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummaryForm = new Control("ChildForm_PLCLaborSummaryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRHDR_PLCLABSUM_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummaryForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummary_PeriodOfPerformance...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PeriodOfPerformance = new Control("ChildForm_PLCLaborSummary_PeriodOfPerformance", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_PSRHDR_PLCLABSUM_']/ancestor::form[1]/descendant::*[@id='PROJ_START_DT']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PeriodOfPerformance.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummaryForm.Exists());

													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExist on ChildForm_PLCLaborSummary_PLCLaborSummaryChildFormTable...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildFormTable = new Control("ChildForm_PLCLaborSummary_PLCLaborSummaryChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_PLCLAB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming ClickButton on ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm = new Control("ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_PLCLAB_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm);
formBttn = PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLC...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLC = new Control("ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_PLCLAB_']/ancestor::form[1]/descendant::*[@id='LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLC.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab = new Control("ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_PLCLAB_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab);
mTab = PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Hours").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummary_PLCLaborSummaryChild_Hours_SubpdHrs...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_Hours_SubpdHrs = new Control("ChildForm_PLCLaborSummary_PLCLaborSummaryChild_Hours_SubpdHrs", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_PLCLAB_']/ancestor::form[1]/descendant::*[@id='SUB_PD_ACT_HRS']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_Hours_SubpdHrs.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab);
mTab = PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_PLCLaborSummaryChildTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Hours").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_Hours_SubpdHrs.Exists());

													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_PLCLaborSummary_PLCLaborSummaryChild_EmployeeDetailLink...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_EmployeeDetailLink = new Control("ChildForm_PLCLaborSummary_PLCLaborSummaryChild_EmployeeDetailLink", "ID", "lnk_1006711_PJQPROJP_RPTPROJLABSUM_PLCLAB");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_PLCLaborSummary_PLCLaborSummaryChild_EmployeeDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_PLCLaborSummaryForm);
formBttn = PJQPROJP_ChildForm_PLCLaborSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Employee Labor Summary");


												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_EmployeeLaborSummaryLink);
PJQPROJP_ChildForm_EmployeeLaborSummaryLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_EmployeeLaborSummaryForm...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_EmployeeLaborSummaryForm = new Control("ChildForm_EmployeeLaborSummaryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_EMPLABR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_EmployeeLaborSummaryForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExist on ChildForm_EmployeeLaborSummaryFormTable...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_EmployeeLaborSummaryFormTable = new Control("ChildForm_EmployeeLaborSummaryFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_EMPLABR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_EmployeeLaborSummaryFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_EmployeeLaborSummaryForm);
formBttn = PJQPROJP_ChildForm_EmployeeLaborSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQPROJP_ChildForm_EmployeeLaborSummaryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQPROJP_ChildForm_EmployeeLaborSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJQPROJP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJP] Perfoming VerifyExists on ChildForm_EmployeeLaborSummary_Employee...", Logger.MessageType.INF);
			Control PJQPROJP_ChildForm_EmployeeLaborSummary_Employee = new Control("ChildForm_EmployeeLaborSummary_Employee", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJP_RPTPROJLABSUM_EMPLABR_']/ancestor::form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PJQPROJP_ChildForm_EmployeeLaborSummary_Employee.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_ChildForm_EmployeeLaborSummaryForm);
formBttn = PJQPROJP_ChildForm_EmployeeLaborSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJQPROJP";
							CPCommon.WaitControlDisplayed(PJQPROJP_MainForm);
formBttn = PJQPROJP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

