 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMAMAIN_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("Engineering Change Processing", "xpath","//div[@class='navItem'][.='Engineering Change Processing']").Click();
new Control("Update Approved Engineering Change Notices", "xpath","//div[@class='navItem'][.='Update Approved Engineering Change Notices']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ECNID...", Logger.MessageType.INF);
			Control ECMAMAIN_ECNID = new Control("ECNID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ECN_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_ECNID.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MainForm);
IWebElement formBttn = ECMAMAIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? ECMAMAIN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
ECMAMAIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECMAMAIN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MainForm);
formBttn = ECMAMAIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming Select on Tab...", Logger.MessageType.INF);
			Control ECMAMAIN_Tab = new Control("Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ECMAMAIN_Tab);
IWebElement mTab = ECMAMAIN_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on BasicInfo_Type...", Logger.MessageType.INF);
			Control ECMAMAIN_BasicInfo_Type = new Control("BasicInfo_Type", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EC_TYPE_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_BasicInfo_Type.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_Tab);
mTab = ECMAMAIN_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Details_Project...", Logger.MessageType.INF);
			Control ECMAMAIN_Details_Project = new Control("Details_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CHG_PROJ_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_Details_Project.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_Tab);
mTab = ECMAMAIN_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on CustomerInformation_ECP...", Logger.MessageType.INF);
			Control ECMAMAIN_CustomerInformation_ECP = new Control("CustomerInformation_ECP", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ECP_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_CustomerInformation_ECP.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_Tab);
mTab = ECMAMAIN_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on CustomerNotes_Notes...", Logger.MessageType.INF);
			Control ECMAMAIN_CustomerNotes_Notes = new Control("CustomerNotes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_NOTES']");
			CPCommon.AssertEqual(true,ECMAMAIN_CustomerNotes_Notes.Exists());

											Driver.SessionLogger.WriteLine("Impacted Projects");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ImpactedProjectsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_ImpactedProjectsLink = new Control("ImpactedProjectsLink", "ID", "lnk_15616_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_ImpactedProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ImpactedProjectsLink);
ECMAMAIN_ImpactedProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on ImpactedProjectsTable...", Logger.MessageType.INF);
			Control ECMAMAIN_ImpactedProjectsTable = new Control("ImpactedProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPROJ_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_ImpactedProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on ImpactedProjectsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_ImpactedProjectsForm = new Control("ImpactedProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPROJ_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_ImpactedProjectsForm);
formBttn = ECMAMAIN_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_ImpactedProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_ImpactedProjectsForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ImpactedProjects_Project...", Logger.MessageType.INF);
			Control ECMAMAIN_ImpactedProjects_Project = new Control("ImpactedProjects_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPROJ_DTL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_ImpactedProjects_Project.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ImpactedProjectsForm);
formBttn = ECMAMAIN_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Approvals");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ApprovalsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_ApprovalsLink = new Control("ApprovalsLink", "ID", "lnk_15617_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_ApprovalsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ApprovalsLink);
ECMAMAIN_ApprovalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on ApprovalsTable...", Logger.MessageType.INF);
			Control ECMAMAIN_ApprovalsTable = new Control("ApprovalsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNAPPRVL_APPROVDETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_ApprovalsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on ApprovalsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_ApprovalsForm = new Control("ApprovalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNAPPRVL_APPROVDETAIL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_ApprovalsForm);
formBttn = ECMAMAIN_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_ApprovalsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_ApprovalsForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Approvals_Revision...", Logger.MessageType.INF);
			Control ECMAMAIN_Approvals_Revision = new Control("Approvals_Revision", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNAPPRVL_APPROVDETAIL_']/ancestor::form[1]/descendant::*[@id='ECN_RVSN_NO']");
			CPCommon.AssertEqual(true,ECMAMAIN_Approvals_Revision.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ApprovalsForm);
formBttn = ECMAMAIN_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("General Notes");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on GeneralNotesLink...", Logger.MessageType.INF);
			Control ECMAMAIN_GeneralNotesLink = new Control("GeneralNotesLink", "ID", "lnk_15618_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_GeneralNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_GeneralNotesLink);
ECMAMAIN_GeneralNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on GeneralNotesForm...", Logger.MessageType.INF);
			Control ECMAMAIN_GeneralNotesForm = new Control("GeneralNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNNOTES_ECNNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_GeneralNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_GeneralNotesForm);
formBttn = ECMAMAIN_GeneralNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Technical Notes");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on TechnicalNotesLink...", Logger.MessageType.INF);
			Control ECMAMAIN_TechnicalNotesLink = new Control("TechnicalNotesLink", "ID", "lnk_15619_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_TechnicalNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_TechnicalNotesLink);
ECMAMAIN_TechnicalNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on TechnicalNotesForm...", Logger.MessageType.INF);
			Control ECMAMAIN_TechnicalNotesForm = new Control("TechnicalNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTECHNOTES_ECNNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_TechnicalNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_TechnicalNotesForm);
formBttn = ECMAMAIN_TechnicalNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Implementation Notes");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ImplementationNotesLink...", Logger.MessageType.INF);
			Control ECMAMAIN_ImplementationNotesLink = new Control("ImplementationNotesLink", "ID", "lnk_15620_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_ImplementationNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ImplementationNotesLink);
ECMAMAIN_ImplementationNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ImplementationNotesForm...", Logger.MessageType.INF);
			Control ECMAMAIN_ImplementationNotesForm = new Control("ImplementationNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNIMPLNOTES_ECNNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_ImplementationNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ImplementationNotesForm);
formBttn = ECMAMAIN_ImplementationNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Text");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on TextLink...", Logger.MessageType.INF);
			Control ECMAMAIN_TextLink = new Control("TextLink", "ID", "lnk_15621_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_TextLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_TextLink);
ECMAMAIN_TextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on TextTable...", Logger.MessageType.INF);
			Control ECMAMAIN_TextTable = new Control("TextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTEXT_STDTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_TextTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on TextForm...", Logger.MessageType.INF);
			Control ECMAMAIN_TextForm = new Control("TextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTEXT_STDTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_TextForm);
formBttn = ECMAMAIN_TextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_TextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_TextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_TextForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Text_Sequence...", Logger.MessageType.INF);
			Control ECMAMAIN_Text_Sequence = new Control("Text_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTEXT_STDTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,ECMAMAIN_Text_Sequence.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_TextForm);
formBttn = ECMAMAIN_TextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on DocumentsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_DocumentsLink = new Control("DocumentsLink", "ID", "lnk_15622_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_DocumentsLink);
ECMAMAIN_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on DocumentsTable...", Logger.MessageType.INF);
			Control ECMAMAIN_DocumentsTable = new Control("DocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_DocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on DocumentsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_DocumentsForm = new Control("DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_DocumentsForm);
formBttn = ECMAMAIN_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_DocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Documents_Line...", Logger.MessageType.INF);
			Control ECMAMAIN_Documents_Line = new Control("Documents_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]/descendant::*[@id='LINE']");
			CPCommon.AssertEqual(true,ECMAMAIN_Documents_Line.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_DocumentsForm);
formBttn = ECMAMAIN_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EC Impacted Groups");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on ECImpactedGroupsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_ECImpactedGroupsLink = new Control("ECImpactedGroupsLink", "ID", "lnk_15623_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_ECImpactedGroupsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_ECImpactedGroupsLink);
ECMAMAIN_ECImpactedGroupsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on ECImpactedGroups_ImpactedGroupTable...", Logger.MessageType.INF);
			Control ECMAMAIN_ECImpactedGroups_ImpactedGroupTable = new Control("ECImpactedGroups_ImpactedGroupTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_INPACTGRP_IMPACTEDGRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_ECImpactedGroups_ImpactedGroupTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on ECImpactedGroups_SelectedGroupTable...", Logger.MessageType.INF);
			Control ECMAMAIN_ECImpactedGroups_SelectedGroupTable = new Control("ECImpactedGroups_SelectedGroupTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNIMPACTGRP_SELIMPGRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_ECImpactedGroups_SelectedGroupTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming Close on ECImpactedGroups_SelectedGroupForm...", Logger.MessageType.INF);
			Control ECMAMAIN_ECImpactedGroups_SelectedGroupForm = new Control("ECImpactedGroups_SelectedGroupForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNIMPACTGRP_SELIMPGRP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_ECImpactedGroups_SelectedGroupForm);
formBttn = ECMAMAIN_ECImpactedGroups_SelectedGroupForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("User Defined Info");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control ECMAMAIN_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_15625_ECMMAIN_ECN_MAINTAPPROVEDECN");
			CPCommon.AssertEqual(true,ECMAMAIN_UserDefinedInfoLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_UserDefinedInfoLink);
ECMAMAIN_UserDefinedInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on UserDefinedInfoTable...", Logger.MessageType.INF);
			Control ECMAMAIN_UserDefinedInfoTable = new Control("UserDefinedInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_UserDefinedInfoTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control ECMAMAIN_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_UserDefinedInfoForm);
formBttn = ECMAMAIN_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_UserDefinedInfoForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control ECMAMAIN_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='DATA_TYPE']");
			CPCommon.AssertEqual(true,ECMAMAIN_UserDefinedInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_UserDefinedInfoForm);
formBttn = ECMAMAIN_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on PartsImpactedTable...", Logger.MessageType.INF);
			Control ECMAMAIN_PartsImpactedTable = new Control("PartsImpactedTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPART_PARTSIMPACTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_PartsImpactedTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on PartsImpactedForm...", Logger.MessageType.INF);
			Control ECMAMAIN_PartsImpactedForm = new Control("PartsImpactedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPART_PARTSIMPACTED_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_PartsImpactedForm);
formBttn = ECMAMAIN_PartsImpactedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_PartsImpactedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_PartsImpactedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_PartsImpactedForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on PartsImpacted_Line...", Logger.MessageType.INF);
			Control ECMAMAIN_PartsImpacted_Line = new Control("PartsImpacted_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPART_PARTSIMPACTED_']/ancestor::form[1]/descendant::*[@id='ECN_PART_LN_NO']");
			CPCommon.AssertEqual(true,ECMAMAIN_PartsImpacted_Line.Exists());

											Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on PartDocumentsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_PartDocumentsLink = new Control("PartDocumentsLink", "ID", "lnk_15590_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_PartDocumentsLink);
ECMAMAIN_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on PartsImpacted_PartDocumentsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_PartsImpacted_PartDocumentsForm = new Control("PartsImpacted_PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_PARTDOC_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_PartsImpacted_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on PartsImpacted_PartDocuments_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_PartsImpacted_PartDocuments_Part = new Control("PartsImpacted_PartDocuments_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_PARTDOC_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_PartsImpacted_PartDocuments_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on PartDocuments_DocumentsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_PartDocuments_DocumentsForm = new Control("PartDocuments_DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTDOCUMENT_PARTDO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_PartDocuments_DocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on PartDocuments_DocumentsTable...", Logger.MessageType.INF);
			Control ECMAMAIN_PartDocuments_DocumentsTable = new Control("PartDocuments_DocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTDOCUMENT_PARTDO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_PartDocuments_DocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_PartDocuments_DocumentsForm);
formBttn = ECMAMAIN_PartDocuments_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_PartDocuments_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_PartDocuments_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on PartsImpacted_PartDocuments_Line...", Logger.MessageType.INF);
			Control ECMAMAIN_PartsImpacted_PartDocuments_Line = new Control("PartsImpacted_PartDocuments_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTDOCUMENT_PARTDO_']/ancestor::form[1]/descendant::*[@id='LINE_NO']");
			CPCommon.AssertEqual(true,ECMAMAIN_PartsImpacted_PartDocuments_Line.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_PartsImpacted_PartDocumentsForm);
formBttn = ECMAMAIN_PartsImpacted_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM Components");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMComponentsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMComponentsLink = new Control("MBOMComponentsLink", "ID", "lnk_15593_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMComponentsLink);
ECMAMAIN_MBOMComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMAssemblyForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMAssemblyForm = new Control("MBOMAssemblyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCOMP_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMAssemblyForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMComponents_MBOMAssembly_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMComponents_MBOMAssembly_Part = new Control("MBOMComponents_MBOMAssembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCOMP_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMComponents_MBOMAssembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on MBOMComponents_MBOMAssembly_ComponentTable...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentTable = new Control("MBOMComponents_MBOMAssembly_ComponentTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMCOMP_MBOMCOMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on MBOMComponents_MBOMAssembly_ComponentForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentForm = new Control("MBOMComponents_MBOMAssembly_ComponentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMCOMP_MBOMCOMP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentForm);
formBttn = ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_MBOMComponents_MBOMAssembly_ComponentForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMComponents_Component_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMComponents_Component_ActionCode = new Control("MBOMComponents_Component_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMCOMP_MBOMCOMP_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMComponents_Component_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMAssemblyForm);
formBttn = ECMAMAIN_MBOMAssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM Orig Assy");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMOrigAssyLink...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMOrigAssyLink = new Control("MBOMOrigAssyLink", "ID", "lnk_15597_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMOrigAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMOrigAssyLink);
ECMAMAIN_MBOMOrigAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMOrigAssy_ComponentForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMOrigAssy_ComponentForm = new Control("MBOMOrigAssy_ComponentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMORIG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMOrigAssy_ComponentForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMOrigAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMOrigAssy_Component_Part = new Control("MBOMOrigAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMORIG_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMOrigAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on MBOMOrigAssy_AssemblyTable...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMOrigAssy_AssemblyTable = new Control("MBOMOrigAssy_AssemblyTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMORPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMOrigAssy_AssemblyTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on MBOMOrigAssy_AssemblyForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMOrigAssy_AssemblyForm = new Control("MBOMOrigAssy_AssemblyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMORPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMOrigAssy_AssemblyForm);
formBttn = ECMAMAIN_MBOMOrigAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_MBOMOrigAssy_AssemblyForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_MBOMOrigAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMOrigAssy_Assembly_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMOrigAssy_Assembly_ActionCode = new Control("MBOMOrigAssy_Assembly_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMORPART_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMOrigAssy_Assembly_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMOrigAssy_ComponentForm);
formBttn = ECMAMAIN_MBOMOrigAssy_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM Chng Assy");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMChngAssyLink...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMChngAssyLink = new Control("MBOMChngAssyLink", "ID", "lnk_15599_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMChngAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMChngAssyLink);
ECMAMAIN_MBOMChngAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMChngAssy_ComponentForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMChngAssy_ComponentForm = new Control("MBOMChngAssy_ComponentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCHNG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMChngAssy_ComponentForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMChngAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMChngAssy_Component_Part = new Control("MBOMChngAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCHNG_HDR_']/ancestor::form[1]/descendant::*[@id='CHNG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMChngAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on MBOMChngAssy_AssemblyTable...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMChngAssy_AssemblyTable = new Control("MBOMChngAssy_AssemblyTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMChngAssy_AssemblyTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on MBOMChngAssy_AssemblyForm...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMChngAssy_AssemblyForm = new Control("MBOMChngAssy_AssemblyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMCHGPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMChngAssy_AssemblyForm);
formBttn = ECMAMAIN_MBOMChngAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_MBOMChngAssy_AssemblyForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_MBOMChngAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_MBOMChngAssy_AssemblyForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on MBOMChngAssy_Assembly_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_MBOMChngAssy_Assembly_ActionCode = new Control("MBOMChngAssy_Assembly_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_MBOMChngAssy_Assembly_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MBOMChngAssy_ComponentForm);
formBttn = ECMAMAIN_MBOMChngAssy_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Components");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMComponentsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMComponentsLink = new Control("EBOMComponentsLink", "ID", "lnk_15602_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMComponentsLink);
ECMAMAIN_EBOMComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMComponentsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMComponentsForm = new Control("EBOMComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCOMP_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMComponentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMComponents_Assembly_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMComponents_Assembly_Part = new Control("EBOMComponents_Assembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCOMP_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMComponents_Assembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on EBOMComponents_ComponentTable...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMComponents_ComponentTable = new Control("EBOMComponents_ComponentTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMCOMP_EBOMCOMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMComponents_ComponentTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on EBOMComponents_ComponentForm...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMComponents_ComponentForm = new Control("EBOMComponents_ComponentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMCOMP_EBOMCOMP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMComponents_ComponentForm);
formBttn = ECMAMAIN_EBOMComponents_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_EBOMComponents_ComponentForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_EBOMComponents_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_EBOMComponents_ComponentForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMComponents_Component_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMComponents_Component_ActionCode = new Control("EBOMComponents_Component_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMCOMP_EBOMCOMP_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMComponents_Component_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMComponentsForm);
formBttn = ECMAMAIN_EBOMComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Orig Assy");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMOrigAssyLink...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMOrigAssyLink = new Control("EBOMOrigAssyLink", "ID", "lnk_15606_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMOrigAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMOrigAssyLink);
ECMAMAIN_EBOMOrigAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMOrigAssy_ComponentForm...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMOrigAssy_ComponentForm = new Control("EBOMOrigAssy_ComponentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMORIG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMOrigAssy_ComponentForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMOrigAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMOrigAssy_Component_Part = new Control("EBOMOrigAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMORIG_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMOrigAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on EBOMOrigAssy_AssemblyTable...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMOrigAssy_AssemblyTable = new Control("EBOMOrigAssy_AssemblyTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMORPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMOrigAssy_AssemblyTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on EBOMOrigAssy_AssemblyForm...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMOrigAssy_AssemblyForm = new Control("EBOMOrigAssy_AssemblyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMORPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMOrigAssy_AssemblyForm);
formBttn = ECMAMAIN_EBOMOrigAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_EBOMOrigAssy_AssemblyForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_EBOMOrigAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_EBOMOrigAssy_AssemblyForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMOrigAssy_Assembly_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMOrigAssy_Assembly_ActionCode = new Control("EBOMOrigAssy_Assembly_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMORPART_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMOrigAssy_Assembly_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMOrigAssy_ComponentForm);
formBttn = ECMAMAIN_EBOMOrigAssy_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Chng Assy");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMChngAssyLink...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMChngAssyLink = new Control("EBOMChngAssyLink", "ID", "lnk_15609_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMChngAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMChngAssyLink);
ECMAMAIN_EBOMChngAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMChngAssy_ComponentForm...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMChngAssy_ComponentForm = new Control("EBOMChngAssy_ComponentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCHNG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMChngAssy_ComponentForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMChngAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMChngAssy_Component_Part = new Control("EBOMChngAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCHNG_HDR_']/ancestor::form[1]/descendant::*[@id='CHNG_PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMChngAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on EBOMChngAssy_AssemblyTable...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMChngAssy_AssemblyTable = new Control("EBOMChngAssy_AssemblyTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMChngAssy_AssemblyTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on EBOMChngAssy_AssemblyForm...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMChngAssy_AssemblyForm = new Control("EBOMChngAssy_AssemblyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMCHGPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMChngAssy_AssemblyForm);
formBttn = ECMAMAIN_EBOMChngAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_EBOMChngAssy_AssemblyForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_EBOMChngAssy_AssemblyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_EBOMChngAssy_AssemblyForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on EBOMChngAssy_Assembly_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_EBOMChngAssy_Assembly_ActionCode = new Control("EBOMChngAssy_Assembly_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_EBOMChngAssy_Assembly_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_EBOMChngAssy_ComponentForm);
formBttn = ECMAMAIN_EBOMChngAssy_ComponentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routings");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on RoutingsLink...", Logger.MessageType.INF);
			Control ECMAMAIN_RoutingsLink = new Control("RoutingsLink", "ID", "lnk_15612_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMAMAIN_RoutingsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_RoutingsLink);
ECMAMAIN_RoutingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Routings_RoutingStepsForm...", Logger.MessageType.INF);
			Control ECMAMAIN_Routings_RoutingStepsForm = new Control("Routings_RoutingStepsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ROUTING_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMAMAIN_Routings_RoutingStepsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Routings_RoutingStep_Part...", Logger.MessageType.INF);
			Control ECMAMAIN_Routings_RoutingStep_Part = new Control("Routings_RoutingStep_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ROUTING_HDR_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,ECMAMAIN_Routings_RoutingStep_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExist on Routings_RoutingLineTable...", Logger.MessageType.INF);
			Control ECMAMAIN_Routings_RoutingLineTable = new Control("Routings_RoutingLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTROUTING_ROUTING_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMAMAIN_Routings_RoutingLineTable.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming ClickButton on Routings_RoutingLineForm...", Logger.MessageType.INF);
			Control ECMAMAIN_Routings_RoutingLineForm = new Control("Routings_RoutingLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTROUTING_ROUTING_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMAMAIN_Routings_RoutingLineForm);
formBttn = ECMAMAIN_Routings_RoutingLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMAMAIN_Routings_RoutingLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMAMAIN_Routings_RoutingLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.AssertEqual(true,ECMAMAIN_Routings_RoutingLineForm.Exists());

													
				CPCommon.CurrentComponent = "ECMAMAIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMAMAIN] Perfoming VerifyExists on Routings_RoutingLine_ActionCode...", Logger.MessageType.INF);
			Control ECMAMAIN_Routings_RoutingLine_ActionCode = new Control("Routings_RoutingLine_ActionCode", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTROUTING_ROUTING_']/ancestor::form[1]/descendant::*[@id='S_ECN_ACTION_CD']");
			CPCommon.AssertEqual(true,ECMAMAIN_Routings_RoutingLine_ActionCode.Exists());

												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_Routings_RoutingStepsForm);
formBttn = ECMAMAIN_Routings_RoutingStepsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ECMAMAIN";
							CPCommon.WaitControlDisplayed(ECMAMAIN_MainForm);
formBttn = ECMAMAIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

