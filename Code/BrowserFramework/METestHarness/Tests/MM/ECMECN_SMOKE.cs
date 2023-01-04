 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMECN_SMOKE : TestScript
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
new Control("Manage Engineering Change Notices", "xpath","//div[@class='navItem'][.='Manage Engineering Change Notices']").Click();


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control ECMECN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ECMECN_MainForm);
IWebElement formBttn = ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? ECMECN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainForm);
formBttn = ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on Identification_ECNID...", Logger.MessageType.INF);
			Control ECMECN_Identification_ECNID = new Control("Identification_ECNID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ECN_ID']");
			CPCommon.AssertEqual(true,ECMECN_Identification_ECNID.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control ECMECN_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ECMECN_MainFormTab);
IWebElement mTab = ECMECN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on BasicInfo_Type...", Logger.MessageType.INF);
			Control ECMECN_BasicInfo_Type = new Control("BasicInfo_Type", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EC_TYPE_CD']");
			CPCommon.AssertEqual(true,ECMECN_BasicInfo_Type.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainFormTab);
mTab = ECMECN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on Details_ECNRelatedCharges_Project...", Logger.MessageType.INF);
			Control ECMECN_Details_ECNRelatedCharges_Project = new Control("Details_ECNRelatedCharges_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CHG_PROJ_ID']");
			CPCommon.AssertEqual(true,ECMECN_Details_ECNRelatedCharges_Project.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainFormTab);
mTab = ECMECN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ClientInformation_ECP...", Logger.MessageType.INF);
			Control ECMECN_ClientInformation_ECP = new Control("ClientInformation_ECP", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ECP_ID']");
			CPCommon.AssertEqual(true,ECMECN_ClientInformation_ECP.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainFormTab);
mTab = ECMECN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainForm);
formBttn = ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ECMECN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECMECN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainForm);
formBttn = ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Impacted Projects");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ImpactedProjectsLink...", Logger.MessageType.INF);
			Control ECMECN_ImpactedProjectsLink = new Control("ImpactedProjectsLink", "ID", "lnk_4610_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_ImpactedProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ImpactedProjectsLink);
ECMECN_ImpactedProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on ImpactedProjectsTable...", Logger.MessageType.INF);
			Control ECMECN_ImpactedProjectsTable = new Control("ImpactedProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPROJ_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_ImpactedProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ImpactedProjectsForm...", Logger.MessageType.INF);
			Control ECMECN_ImpactedProjectsForm = new Control("ImpactedProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPROJ_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_ImpactedProjectsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ImpactedProjectsForm);
formBttn = ECMECN_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_ImpactedProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ImpactedProjects_Project...", Logger.MessageType.INF);
			Control ECMECN_ImpactedProjects_Project = new Control("ImpactedProjects_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPROJ_DTL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,ECMECN_ImpactedProjects_Project.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ImpactedProjectsForm);
formBttn = ECMECN_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Approvals");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ApprovalsLink...", Logger.MessageType.INF);
			Control ECMECN_ApprovalsLink = new Control("ApprovalsLink", "ID", "lnk_1003515_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_ApprovalsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ApprovalsLink);
ECMECN_ApprovalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on ApprovalsTable...", Logger.MessageType.INF);
			Control ECMECN_ApprovalsTable = new Control("ApprovalsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNAPPRVL_APPROVDETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_ApprovalsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ApprovalsForm...", Logger.MessageType.INF);
			Control ECMECN_ApprovalsForm = new Control("ApprovalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNAPPRVL_APPROVDETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_ApprovalsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ApprovalsForm);
formBttn = ECMECN_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_ApprovalsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on Approvals_Revision...", Logger.MessageType.INF);
			Control ECMECN_Approvals_Revision = new Control("Approvals_Revision", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNAPPRVL_APPROVDETAIL_']/ancestor::form[1]/descendant::*[@id='ECN_RVSN_NO']");
			CPCommon.AssertEqual(true,ECMECN_Approvals_Revision.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ApprovalsForm);
formBttn = ECMECN_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("General Notes");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on GeneralNotesLink...", Logger.MessageType.INF);
			Control ECMECN_GeneralNotesLink = new Control("GeneralNotesLink", "ID", "lnk_1003514_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_GeneralNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_GeneralNotesLink);
ECMECN_GeneralNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on GeneralNotesForm...", Logger.MessageType.INF);
			Control ECMECN_GeneralNotesForm = new Control("GeneralNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNNOTES_ECNNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_GeneralNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_GeneralNotesForm);
formBttn = ECMECN_GeneralNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Technical Notes");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on TechnicalNotesLink...", Logger.MessageType.INF);
			Control ECMECN_TechnicalNotesLink = new Control("TechnicalNotesLink", "ID", "lnk_4607_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_TechnicalNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_TechnicalNotesLink);
ECMECN_TechnicalNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on TechnicalNotesForm...", Logger.MessageType.INF);
			Control ECMECN_TechnicalNotesForm = new Control("TechnicalNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTECHNOTES_ECNNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_TechnicalNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_TechnicalNotesForm);
formBttn = ECMECN_TechnicalNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Implementation Notes");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ImplementationNotesLink...", Logger.MessageType.INF);
			Control ECMECN_ImplementationNotesLink = new Control("ImplementationNotesLink", "ID", "lnk_4606_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_ImplementationNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ImplementationNotesLink);
ECMECN_ImplementationNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ImplementationNotesForm...", Logger.MessageType.INF);
			Control ECMECN_ImplementationNotesForm = new Control("ImplementationNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNIMPLNOTES_ECNNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_ImplementationNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ImplementationNotesForm);
formBttn = ECMECN_ImplementationNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Text");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on TextLink...", Logger.MessageType.INF);
			Control ECMECN_TextLink = new Control("TextLink", "ID", "lnk_1003511_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_TextLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_TextLink);
ECMECN_TextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on TextTable...", Logger.MessageType.INF);
			Control ECMECN_TextTable = new Control("TextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTEXT_STDTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_TextTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on TextForm...", Logger.MessageType.INF);
			Control ECMECN_TextForm = new Control("TextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTEXT_STDTEXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_TextForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_TextForm);
formBttn = ECMECN_TextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_TextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_TextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on Text_Sequence...", Logger.MessageType.INF);
			Control ECMECN_Text_Sequence = new Control("Text_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNTEXT_STDTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,ECMECN_Text_Sequence.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_TextForm);
formBttn = ECMECN_TextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on DocumentsLink...", Logger.MessageType.INF);
			Control ECMECN_DocumentsLink = new Control("DocumentsLink", "ID", "lnk_1003512_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_DocumentsLink);
ECMECN_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on DocumentsTable...", Logger.MessageType.INF);
			Control ECMECN_DocumentsTable = new Control("DocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_DocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on DocumentsForm...", Logger.MessageType.INF);
			Control ECMECN_DocumentsForm = new Control("DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_DocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_DocumentsForm);
formBttn = ECMECN_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on Documents_Line...", Logger.MessageType.INF);
			Control ECMECN_Documents_Line = new Control("Documents_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]/descendant::*[@id='LINE']");
			CPCommon.AssertEqual(true,ECMECN_Documents_Line.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_DocumentsForm);
formBttn = ECMECN_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EC Impacted Groups");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ECImpactedGroupsLink...", Logger.MessageType.INF);
			Control ECMECN_ECImpactedGroupsLink = new Control("ECImpactedGroupsLink", "ID", "lnk_1003516_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_ECImpactedGroupsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ECImpactedGroupsLink);
ECMECN_ECImpactedGroupsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on ECImpactedGroupsLookupTable...", Logger.MessageType.INF);
			Control ECMECN_ECImpactedGroupsLookupTable = new Control("ECImpactedGroupsLookupTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_INPACTGRP_IMPACTEDGRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_ECImpactedGroupsLookupTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on ECImpactedGroupsTable...", Logger.MessageType.INF);
			Control ECMECN_ECImpactedGroupsTable = new Control("ECImpactedGroupsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNIMPACTGRP_SELIMPGRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_ECImpactedGroupsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ECImpactedGroupsForm...", Logger.MessageType.INF);
			Control ECMECN_ECImpactedGroupsForm = new Control("ECImpactedGroupsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNIMPACTGRP_SELIMPGRP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_ECImpactedGroupsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ECImpactedGroupsLookupForm...", Logger.MessageType.INF);
			Control ECMECN_ECImpactedGroupsLookupForm = new Control("ECImpactedGroupsLookupForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_INPACTGRP_IMPACTEDGRP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_ECImpactedGroupsLookupForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ECImpactedGroupsForm);
formBttn = ECMECN_ECImpactedGroupsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("User-Defined Info");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control ECMECN_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_1003520_ECMMAIN_ECN_MAINTECN");
			CPCommon.AssertEqual(true,ECMECN_UserDefinedInfoLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_UserDefinedInfoLink);
ECMECN_UserDefinedInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on UserDefinedInfoTable...", Logger.MessageType.INF);
			Control ECMECN_UserDefinedInfoTable = new Control("UserDefinedInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_UserDefinedInfoTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control ECMECN_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_UserDefinedInfoForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_UserDefinedInfoForm);
formBttn = ECMECN_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on UserDefinedInfo_Value...", Logger.MessageType.INF);
			Control ECMECN_UserDefinedInfo_Value = new Control("UserDefinedInfo_Value", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='UDEF_DT']");
			CPCommon.AssertEqual(true,ECMECN_UserDefinedInfo_Value.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_UserDefinedInfoForm);
formBttn = ECMECN_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control ECMECN_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPART_PARTSIMPACTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control ECMECN_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPART_PARTSIMPACTED_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMECN_ChildForm);
formBttn = ECMECN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.AssertEqual(true,ECMECN_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_Line...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_Line = new Control("ChildForm_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPART_PARTSIMPACTED_']/ancestor::form[1]/descendant::*[@id='ECN_PART_LN_NO']");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_Line.Exists());

											Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_PartDocumentsLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_PartDocumentsLink = new Control("ChildForm_PartDocumentsLink", "ID", "lnk_1004987_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_PartDocumentsLink);
ECMECN_ChildForm_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on PartDocumentsDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_PartDocumentsDetailsTable = new Control("PartDocumentsDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTDOCUMENT_PARTDO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_PartDocumentsDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on PartDocumentsForm...", Logger.MessageType.INF);
			Control ECMECN_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_PARTDOC_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on PartDocumentsDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_PartDocumentsDetailsForm = new Control("PartDocumentsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTDOCUMENT_PARTDO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_PartDocumentsDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_PartDocumentsDetailsForm);
formBttn = ECMECN_PartDocumentsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_PartDocumentsDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_PartDocumentsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on PartDocuments_Part...", Logger.MessageType.INF);
			Control ECMECN_PartDocuments_Part = new Control("PartDocuments_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_PARTDOC_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_PartDocuments_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on PartDocumentsDetails_Line...", Logger.MessageType.INF);
			Control ECMECN_PartDocumentsDetails_Line = new Control("PartDocumentsDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTDOCUMENT_PARTDO_']/ancestor::form[1]/descendant::*[@id='LINE_NO']");
			CPCommon.AssertEqual(true,ECMECN_PartDocumentsDetails_Line.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on PartDocumentsDetails_CopyDocumentsLink...", Logger.MessageType.INF);
			Control ECMECN_PartDocumentsDetails_CopyDocumentsLink = new Control("PartDocumentsDetails_CopyDocumentsLink", "ID", "lnk_18583_ECMMAIN_PARTDOC_HDR");
			CPCommon.AssertEqual(true,ECMECN_PartDocumentsDetails_CopyDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_PartDocumentsDetails_CopyDocumentsLink);
ECMECN_PartDocumentsDetails_CopyDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_PartDocumentsForm);
formBttn = ECMECN_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM Components");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_MBOMComponentsLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_MBOMComponentsLink = new Control("ChildForm_MBOMComponentsLink", "ID", "lnk_1004992_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_MBOMComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_MBOMComponentsLink);
ECMECN_ChildForm_MBOMComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on MBOMComponentsDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_MBOMComponentsDetailsTable = new Control("MBOMComponentsDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMCOMP_MBOMCOMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_MBOMComponentsDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMComponentsForm...", Logger.MessageType.INF);
			Control ECMECN_MBOMComponentsForm = new Control("MBOMComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCOMP_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_MBOMComponentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMComponentsDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_MBOMComponentsDetailsForm = new Control("MBOMComponentsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMCOMP_MBOMCOMP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_MBOMComponentsDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MBOMComponentsDetailsForm);
formBttn = ECMECN_MBOMComponentsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_MBOMComponentsDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_MBOMComponentsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMComponents_MBOMAssembly_Part...", Logger.MessageType.INF);
			Control ECMECN_MBOMComponents_MBOMAssembly_Part = new Control("MBOMComponents_MBOMAssembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCOMP_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_MBOMComponents_MBOMAssembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMComponentsDetails_FindNo...", Logger.MessageType.INF);
			Control ECMECN_MBOMComponentsDetails_FindNo = new Control("MBOMComponentsDetails_FindNo", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMCOMP_MBOMCOMP_']/ancestor::form[1]/descendant::*[@id='COMP_FIND_NO']");
			CPCommon.AssertEqual(true,ECMECN_MBOMComponentsDetails_FindNo.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MBOMComponentsForm);
formBttn = ECMECN_MBOMComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM Orig Assy");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_MBOMOrigAssyLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_MBOMOrigAssyLink = new Control("ChildForm_MBOMOrigAssyLink", "ID", "lnk_1004999_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_MBOMOrigAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_MBOMOrigAssyLink);
ECMECN_ChildForm_MBOMOrigAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on MBOMOrigAssyDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_MBOMOrigAssyDetailsTable = new Control("MBOMOrigAssyDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMORPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_MBOMOrigAssyDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMOrigAssyForm...", Logger.MessageType.INF);
			Control ECMECN_MBOMOrigAssyForm = new Control("MBOMOrigAssyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMORIG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_MBOMOrigAssyForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMOrigAssyDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_MBOMOrigAssyDetailsForm = new Control("MBOMOrigAssyDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMORPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_MBOMOrigAssyDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MBOMOrigAssyDetailsForm);
formBttn = ECMECN_MBOMOrigAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_MBOMOrigAssyDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_MBOMOrigAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMOrigAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMECN_MBOMOrigAssy_Component_Part = new Control("MBOMOrigAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMORIG_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_MBOMOrigAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMOrigAssyDetails_Assembly_Part...", Logger.MessageType.INF);
			Control ECMECN_MBOMOrigAssyDetails_Assembly_Part = new Control("MBOMOrigAssyDetails_Assembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMORPART_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_MBOMOrigAssyDetails_Assembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MBOMOrigAssyForm);
formBttn = ECMECN_MBOMOrigAssyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM Chng Assy");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_MBOMChngAssyLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_MBOMChngAssyLink = new Control("ChildForm_MBOMChngAssyLink", "ID", "lnk_1005001_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_MBOMChngAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_MBOMChngAssyLink);
ECMECN_ChildForm_MBOMChngAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on MBOMChngAssyDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_MBOMChngAssyDetailsTable = new Control("MBOMChngAssyDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_MBOMChngAssyDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMChngAssyForm...", Logger.MessageType.INF);
			Control ECMECN_MBOMChngAssyForm = new Control("MBOMChngAssyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCHNG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_MBOMChngAssyForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMChngAssyDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_MBOMChngAssyDetailsForm = new Control("MBOMChngAssyDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMCHGPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_MBOMChngAssyDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MBOMChngAssyDetailsForm);
formBttn = ECMECN_MBOMChngAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_MBOMChngAssyDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_MBOMChngAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMChngAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMECN_MBOMChngAssy_Component_Part = new Control("MBOMChngAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_MBOMCHNG_HDR_']/ancestor::form[1]/descendant::*[@id='CHNG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_MBOMChngAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on MBOMChngAssyDetails_Assembly_Part...", Logger.MessageType.INF);
			Control ECMECN_MBOMChngAssyDetails_Assembly_Part = new Control("MBOMChngAssyDetails_Assembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNMBOMASY_MBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_MBOMChngAssyDetails_Assembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MBOMChngAssyForm);
formBttn = ECMECN_MBOMChngAssyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Components");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_EBOMComponentsLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_EBOMComponentsLink = new Control("ChildForm_EBOMComponentsLink", "ID", "lnk_1005007_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_EBOMComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_EBOMComponentsLink);
ECMECN_ChildForm_EBOMComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on EBOMComponentsDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_EBOMComponentsDetailsTable = new Control("EBOMComponentsDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMCOMP_EBOMCOMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_EBOMComponentsDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMComponentsForm...", Logger.MessageType.INF);
			Control ECMECN_EBOMComponentsForm = new Control("EBOMComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCOMP_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_EBOMComponentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMComponentsDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_EBOMComponentsDetailsForm = new Control("EBOMComponentsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMCOMP_EBOMCOMP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_EBOMComponentsDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_EBOMComponentsDetailsForm);
formBttn = ECMECN_EBOMComponentsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_EBOMComponentsDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_EBOMComponentsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMComponents_EBOMAssembly_Part...", Logger.MessageType.INF);
			Control ECMECN_EBOMComponents_EBOMAssembly_Part = new Control("EBOMComponents_EBOMAssembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCOMP_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_EBOMComponents_EBOMAssembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMComponentsDetails_Line...", Logger.MessageType.INF);
			Control ECMECN_EBOMComponentsDetails_Line = new Control("EBOMComponentsDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMCOMP_EBOMCOMP_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,ECMECN_EBOMComponentsDetails_Line.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_EBOMComponentsForm);
formBttn = ECMECN_EBOMComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Orig Assy");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_EBOMOrigAssyLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_EBOMOrigAssyLink = new Control("ChildForm_EBOMOrigAssyLink", "ID", "lnk_1005011_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_EBOMOrigAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_EBOMOrigAssyLink);
ECMECN_ChildForm_EBOMOrigAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on EBOMOrigAssyDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_EBOMOrigAssyDetailsTable = new Control("EBOMOrigAssyDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMORPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_EBOMOrigAssyDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMOrigAssyForm...", Logger.MessageType.INF);
			Control ECMECN_EBOMOrigAssyForm = new Control("EBOMOrigAssyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMORIG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_EBOMOrigAssyForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMOrigAssyDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_EBOMOrigAssyDetailsForm = new Control("EBOMOrigAssyDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMORPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_EBOMOrigAssyDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_EBOMOrigAssyDetailsForm);
formBttn = ECMECN_EBOMOrigAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_EBOMOrigAssyDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_EBOMOrigAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMOrigAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMECN_EBOMOrigAssy_Component_Part = new Control("EBOMOrigAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMORIG_HDR_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_EBOMOrigAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMOrigAssyDetails_EBOMLine_Line...", Logger.MessageType.INF);
			Control ECMECN_EBOMOrigAssyDetails_EBOMLine_Line = new Control("EBOMOrigAssyDetails_EBOMLine_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMORPART_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,ECMECN_EBOMOrigAssyDetails_EBOMLine_Line.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_EBOMOrigAssyForm);
formBttn = ECMECN_EBOMOrigAssyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Chng Assy");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_EBOMChngAssyLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_EBOMChngAssyLink = new Control("ChildForm_EBOMChngAssyLink", "ID", "lnk_1005013_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_EBOMChngAssyLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_EBOMChngAssyLink);
ECMECN_ChildForm_EBOMChngAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on EBOMChngAssyDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_EBOMChngAssyDetailsTable = new Control("EBOMChngAssyDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_EBOMChngAssyDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMChngAssyForm...", Logger.MessageType.INF);
			Control ECMECN_EBOMChngAssyForm = new Control("EBOMChngAssyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCHNG_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_EBOMChngAssyForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMChngAssyDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_EBOMChngAssyDetailsForm = new Control("EBOMChngAssyDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMCHGPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_EBOMChngAssyDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_EBOMChngAssyDetailsForm);
formBttn = ECMECN_EBOMChngAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_EBOMChngAssyDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_EBOMChngAssyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMChngAssy_Component_Part...", Logger.MessageType.INF);
			Control ECMECN_EBOMChngAssy_Component_Part = new Control("EBOMChngAssy_Component_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_EBOMCHNG_HDR_']/ancestor::form[1]/descendant::*[@id='CHNG_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_EBOMChngAssy_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on EBOMChngAssyDetails_Assembly_Part...", Logger.MessageType.INF);
			Control ECMECN_EBOMChngAssyDetails_Assembly_Part = new Control("EBOMChngAssyDetails_Assembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNEBOMASY_EBOMCHGPART_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_EBOMChngAssyDetails_Assembly_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_EBOMChngAssyForm);
formBttn = ECMECN_EBOMChngAssyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routings");


												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on ChildForm_RoutingsLink...", Logger.MessageType.INF);
			Control ECMECN_ChildForm_RoutingsLink = new Control("ChildForm_RoutingsLink", "ID", "lnk_1003563_ECMMAIN_ECNPART_PARTSIMPACTED");
			CPCommon.AssertEqual(true,ECMECN_ChildForm_RoutingsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_ChildForm_RoutingsLink);
ECMECN_ChildForm_RoutingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExist on RoutingsDetailsTable...", Logger.MessageType.INF);
			Control ECMECN_RoutingsDetailsTable = new Control("RoutingsDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTROUTING_ROUTING_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECN_RoutingsDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on RoutingsForm...", Logger.MessageType.INF);
			Control ECMECN_RoutingsForm = new Control("RoutingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ROUTING_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_RoutingsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on RoutingsDetailsForm...", Logger.MessageType.INF);
			Control ECMECN_RoutingsDetailsForm = new Control("RoutingsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTROUTING_ROUTING_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECN_RoutingsDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_RoutingsDetailsForm);
formBttn = ECMECN_RoutingsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECN_RoutingsDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECN_RoutingsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on Routings_Part...", Logger.MessageType.INF);
			Control ECMECN_Routings_Part = new Control("Routings_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ROUTING_HDR_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,ECMECN_Routings_Part.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECN] Perfoming VerifyExists on RoutingsDetails_RoutingLineDetails_OperationSequence...", Logger.MessageType.INF);
			Control ECMECN_RoutingsDetails_RoutingLineDetails_OperationSequence = new Control("RoutingsDetails_RoutingLineDetails_OperationSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMMAIN_ECNPARTROUTING_ROUTING_']/ancestor::form[1]/descendant::*[@id='ROUT_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,ECMECN_RoutingsDetails_RoutingLineDetails_OperationSequence.Exists());

												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_RoutingsForm);
formBttn = ECMECN_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "ECMECN";
							CPCommon.WaitControlDisplayed(ECMECN_MainForm);
formBttn = ECMECN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

