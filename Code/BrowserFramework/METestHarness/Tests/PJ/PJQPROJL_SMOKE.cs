 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJQPROJL_SMOKE : TestScript
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
new Control("View Project Ledger Activity", "xpath","//div[@class='navItem'][.='View Project Ledger Activity']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJQPROJL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJQPROJL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJQPROJL_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJQPROJL_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Inquiry Summaries");


												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummariesForm...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummariesForm = new Control("InquirySummariesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummariesForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExist on InquirySummariesFormTable...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummariesFormTable = new Control("InquirySummariesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummariesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesForm);
IWebElement formBttn = PJQPROJL_InquirySummariesForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJQPROJL_InquirySummariesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJQPROJL_InquirySummariesForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesForm);
formBttn = PJQPROJL_InquirySummariesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQPROJL_InquirySummariesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQPROJL_InquirySummariesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming Select on InquirySummariesTab...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummariesTab = new Control("InquirySummariesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
IWebElement mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "POA").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_POA_PROJ_ID...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_POA_PROJ_ID = new Control("InquirySummaries_POA_PROJ_ID", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_POA_PROJ_ID.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Summary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_OtherSummary_FeeSummary_FEE_BURD_ACT_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_OtherSummary_FeeSummary_FEE_BURD_ACT_AMT = new Control("InquirySummaries_OtherSummary_FeeSummary_FEE_BURD_ACT_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='FEE_BURD_ACT_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_OtherSummary_FeeSummary_FEE_BURD_ACT_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "YTD Summary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_YTDSummary_YTD_DIR_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_YTDSummary_YTD_DIR_AMT = new Control("InquirySummaries_YTDSummary_YTD_DIR_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='YTD_DIR_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_YTDSummary_YTD_DIR_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PTD Summary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_PTDSummary_PTD_DIR_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_PTDSummary_PTD_DIR_AMT = new Control("InquirySummaries_PTDSummary_PTD_DIR_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='PTD_DIR_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_PTDSummary_PTD_DIR_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Subperiod Summary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_SubperiodSummary_SUB_DIR_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_SubperiodSummary_SUB_DIR_AMT = new Control("InquirySummaries_SubperiodSummary_SUB_DIR_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='SUB_DIR_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_SubperiodSummary_SUB_DIR_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Award Fee Summary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_AwardFeeSummary_AWARD_FEE_ACT_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_AwardFeeSummary_AWARD_FEE_ACT_AMT = new Control("InquirySummaries_AwardFeeSummary_AWARD_FEE_ACT_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='AWARD_FEE_ACT_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_AwardFeeSummary_AWARD_FEE_ACT_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummariesTab);
mTab = PJQPROJL_InquirySummariesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Totals").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Totals_TOT_ACT_AL_EXP_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Totals_TOT_ACT_AL_EXP_AMT = new Control("InquirySummaries_Totals_TOT_ACT_AL_EXP_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_CTW_']/ancestor::form[1]/descendant::*[@id='TOT_ACT_AL_EXP_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Totals_TOT_ACT_AL_EXP_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_DetailLink...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_DetailLink = new Control("InquirySummaries_DetailLink", "ID", "lnk_1004998_PJQPROJL_PROJSUM_CTW");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_DetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailLink);
PJQPROJL_InquirySummaries_DetailLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Detail");


												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_DetailForm...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_DetailForm = new Control("InquirySummaries_DetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_DetailForm.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExist on InquirySummaries_DetailFormTable...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_DetailFormTable = new Control("InquirySummaries_DetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_DetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailForm);
formBttn = PJQPROJL_InquirySummaries_DetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQPROJL_InquirySummaries_DetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQPROJL_InquirySummaries_DetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming Select on InquirySummaries_DetailTab...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_DetailTab = new Control("InquirySummaries_DetailTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailTab);
mTab = PJQPROJL_InquirySummaries_DetailTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "POA Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Detail_POADetails_Totals_PROJ_ID...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Detail_POADetails_Totals_PROJ_ID = new Control("InquirySummaries_Detail_POADetails_Totals_PROJ_ID", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Detail_POADetails_Totals_PROJ_ID.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailTab);
mTab = PJQPROJL_InquirySummaries_DetailTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Detail_OtherDetails_FeeDetails_FEE_BURD_ACT_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Detail_OtherDetails_FeeDetails_FEE_BURD_ACT_AMT = new Control("InquirySummaries_Detail_OtherDetails_FeeDetails_FEE_BURD_ACT_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='FEE_BURD_ACT_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Detail_OtherDetails_FeeDetails_FEE_BURD_ACT_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailTab);
mTab = PJQPROJL_InquirySummaries_DetailTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "YTD Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Detail_YTDDetails_YTD_DIR_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Detail_YTDDetails_YTD_DIR_AMT = new Control("InquirySummaries_Detail_YTDDetails_YTD_DIR_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='YTD_DIR_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Detail_YTDDetails_YTD_DIR_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailTab);
mTab = PJQPROJL_InquirySummaries_DetailTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PTD Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Detail_PTDDetails_PTD_DIR_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Detail_PTDDetails_PTD_DIR_AMT = new Control("InquirySummaries_Detail_PTDDetails_PTD_DIR_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='PTD_DIR_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Detail_PTDDetails_PTD_DIR_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailTab);
mTab = PJQPROJL_InquirySummaries_DetailTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Subperiod Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Detail_SubperiodDetails_SUB_DIR_AMT...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Detail_SubperiodDetails_SUB_DIR_AMT = new Control("InquirySummaries_Detail_SubperiodDetails_SUB_DIR_AMT", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQPROJL_PROJSUM_']/ancestor::form[1]/descendant::*[@id='SUB_DIR_AMT']");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Detail_SubperiodDetails_SUB_DIR_AMT.Exists());

												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_InquirySummaries_DetailTab);
mTab = PJQPROJL_InquirySummaries_DetailTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Totals").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJQPROJL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQPROJL] Perfoming VerifyExists on InquirySummaries_Detail_Totals_TOT_ACT_AL_EXP_AMTLabel...", Logger.MessageType.INF);
			Control PJQPROJL_InquirySummaries_Detail_Totals_TOT_ACT_AL_EXP_AMTLabel = new Control("InquirySummaries_Detail_Totals_TOT_ACT_AL_EXP_AMTLabel", "xpath", "//input[@id='POST_SEQ_NO']/ancestor::form[1]/descendant::*[@id='TOT_ACT_AL_EXP_AMT']/preceding-sibling::span[1]");
			CPCommon.AssertEqual(true,PJQPROJL_InquirySummaries_Detail_Totals_TOT_ACT_AL_EXP_AMTLabel.Exists());

											Driver.SessionLogger.WriteLine("Close Main Form");


												
				CPCommon.CurrentComponent = "PJQPROJL";
							CPCommon.WaitControlDisplayed(PJQPROJL_MainForm);
formBttn = PJQPROJL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

