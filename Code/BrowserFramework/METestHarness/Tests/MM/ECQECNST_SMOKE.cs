 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECQECNST_SMOKE : TestScript
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
new Control("ECN Reports/Inquiries", "xpath","//div[@class='navItem'][.='ECN Reports/Inquiries']").Click();
new Control("View Engineering Change Notice Status", "xpath","//div[@class='navItem'][.='View Engineering Change Notice Status']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECQECNST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECQECNST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on MainForm_ECN...", Logger.MessageType.INF);
			Control ECQECNST_MainForm_ECN = new Control("MainForm_ECN", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ECN_ID']");
			CPCommon.AssertEqual(true,ECQECNST_MainForm_ECN.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm);
IWebElement formBttn = ECQECNST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? ECQECNST_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
ECQECNST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm);
formBttn = ECQECNST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ECN...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ECN = new Control("ChildForm_ECN", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='ECN_ID']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ECN.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_BasicECNInfo_Class...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_BasicECNInfo_Class = new Control("ChildForm_BasicECNInfo_Class", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='S_ECN_CLASS_CD']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_BasicECNInfo_Class.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Select on ChildForm_tab1...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_tab1 = new Control("ChildForm_tab1", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_tab1);
IWebElement mTab = ECQECNST_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "ECN Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ECNDetails_EntryLastChangeInfo_LastChangeUser...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ECNDetails_EntryLastChangeInfo_LastChangeUser = new Control("ChildForm_ECNDetails_EntryLastChangeInfo_LastChangeUser", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='CHNG_USER_ID']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ECNDetails_EntryLastChangeInfo_LastChangeUser.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_tab1);
mTab = ECQECNST_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_CustomerInfo_Customer...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_CustomerInfo_Customer = new Control("ChildForm_CustomerInfo_Customer", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_CustomerInfo_Customer.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_tab1);
mTab = ECQECNST_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_CustomerAddress_Address_AddressLine1...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_CustomerAddress_Address_AddressLine1 = new Control("ChildForm_CustomerAddress_Address_AddressLine1", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_DTL_']/ancestor::form[1]/descendant::*[@id='LN_1_ADDR']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_CustomerAddress_Address_AddressLine1.Exists());

											Driver.SessionLogger.WriteLine("Impacted Projects");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_ImpactedProjectsLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImpactedProjectsLink = new Control("ChildForm_ImpactedProjectsLink", "ID", "lnk_1006333_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ImpactedProjectsLink);
ECQECNST_ChildForm_ImpactedProjectsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ImpactedProjectsForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImpactedProjectsForm = new Control("ChildForm_ImpactedProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNDETAIL_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ImpactedProjectsForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_ImpactedProjectsFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImpactedProjectsFormTable = new Control("ChildForm_ImpactedProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNDETAIL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ImpactedProjectsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ImpactedProjectsForm);
formBttn = ECQECNST_ChildForm_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_ImpactedProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ImpactedProjects_Project...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImpactedProjects_Project = new Control("ChildForm_ImpactedProjects_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNDETAIL_DTL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ImpactedProjects_Project.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ImpactedProjectsForm);
formBttn = ECQECNST_ChildForm_ImpactedProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Approval");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_ApprovalLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ApprovalLink = new Control("ChildForm_ApprovalLink", "ID", "lnk_4484_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ApprovalLink);
ECQECNST_ChildForm_ApprovalLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ApprovalForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ApprovalForm = new Control("ChildForm_ApprovalForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNAPPRVL_APPROVDTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ApprovalForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_ApprovalFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ApprovalFormTable = new Control("ChildForm_ApprovalFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNAPPRVL_APPROVDTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ApprovalFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ApprovalForm);
formBttn = ECQECNST_ChildForm_ApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_ApprovalForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_ApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_Approval_Revision...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_Approval_Revision = new Control("ChildForm_Approval_Revision", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNAPPRVL_APPROVDTL_']/ancestor::form[1]/descendant::*[@id='ECN_RVSN_NO']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_Approval_Revision.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ApprovalForm);
formBttn = ECQECNST_ChildForm_ApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Groups");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_GroupsLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_GroupsLink = new Control("ChildForm_GroupsLink", "ID", "lnk_4486_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_GroupsLink);
ECQECNST_ChildForm_GroupsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_GroupsForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_GroupsForm = new Control("ChildForm_GroupsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_INPACTGRP_IMPACTEDGRP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_GroupsForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_GroupsFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_GroupsFormTable = new Control("ChildForm_GroupsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_INPACTGRP_IMPACTEDGRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_GroupsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_GroupsForm);
formBttn = ECQECNST_ChildForm_GroupsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_GroupsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_GroupsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_Groups_ImpactedGroup...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_Groups_ImpactedGroup = new Control("ChildForm_Groups_ImpactedGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_INPACTGRP_IMPACTEDGRP_']/ancestor::form[1]/descendant::*[@id='IMPACT_GRP_CD']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_Groups_ImpactedGroup.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_GroupsForm);
formBttn = ECQECNST_ChildForm_GroupsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Parts");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_PartsLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_PartsLink = new Control("ChildForm_PartsLink", "ID", "lnk_4499_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_PartsLink);
ECQECNST_ChildForm_PartsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_PartsForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_PartsForm = new Control("ChildForm_PartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNPART_PARTSIMPACTED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_PartsForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_PartsFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_PartsFormTable = new Control("ChildForm_PartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNPART_PARTSIMPACTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_PartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_PartsForm);
formBttn = ECQECNST_ChildForm_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_PartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_Parts_OriginalPart_Active...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_Parts_OriginalPart_Active = new Control("ChildForm_Parts_OriginalPart_Active", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNPART_PARTSIMPACTED_']/ancestor::form[1]/descendant::*[@id='ORIG_PART_ACT_FL']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_Parts_OriginalPart_Active.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_PartsForm);
formBttn = ECQECNST_ChildForm_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_DocumentsLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_DocumentsLink = new Control("ChildForm_DocumentsLink", "ID", "lnk_4516_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_DocumentsLink);
ECQECNST_ChildForm_DocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_DocumentsForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_DocumentsForm = new Control("ChildForm_DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_DocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_DocumentsFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_DocumentsFormTable = new Control("ChildForm_DocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_DocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_DocumentsForm);
formBttn = ECQECNST_ChildForm_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_Documents_ChangeToDocument_CAGE...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_Documents_ChangeToDocument_CAGE = new Control("ChildForm_Documents_ChangeToDocument_CAGE", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNDOCUMENT_ECNDOC_']/ancestor::form[1]/descendant::*[@id='CAGE_ID_FLD']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_Documents_ChangeToDocument_CAGE.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_DocumentsForm);
formBttn = ECQECNST_ChildForm_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("User-Defined Info");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_UserDefinedInfoLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_UserDefinedInfoLink = new Control("ChildForm_UserDefinedInfoLink", "ID", "lnk_4779_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_UserDefinedInfoLink);
ECQECNST_ChildForm_UserDefinedInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_UserDefinedInfoForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_UserDefinedInfoForm = new Control("ChildForm_UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_UserDefinedInfoForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_UserDefinedInfoFormTable = new Control("ChildForm_UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_UserDefinedInfoForm);
formBttn = ECQECNST_ChildForm_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Notes");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_NotesLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_NotesLink = new Control("ChildForm_NotesLink", "ID", "lnk_4575_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_NotesLink);
ECQECNST_ChildForm_NotesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_NotesForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_NotesForm = new Control("ChildForm_NotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNTX_NOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_NotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_Notes_Notes...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_Notes_Notes = new Control("ChildForm_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNTX_NOTES_']/ancestor::form[1]/descendant::*[@id='ECN_TX']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_NotesForm);
formBttn = ECQECNST_ChildForm_NotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Implementation Notes");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_ImplmentationNotesLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImplmentationNotesLink = new Control("ChildForm_ImplmentationNotesLink", "ID", "lnk_4568_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ImplmentationNotesLink);
ECQECNST_ChildForm_ImplmentationNotesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ImplementationNotesForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImplementationNotesForm = new Control("ChildForm_ImplementationNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNIMPLTX_NOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ImplementationNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ImplementationNotes_Notes...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ImplementationNotes_Notes = new Control("ChildForm_ImplementationNotes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNIMPLTX_NOTES_']/ancestor::form[1]/descendant::*[@id='ECN_IMPL_TX']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ImplementationNotes_Notes.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ImplementationNotesForm);
formBttn = ECQECNST_ChildForm_ImplementationNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Technical Notes");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_TechnicalNotesLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_TechnicalNotesLink = new Control("ChildForm_TechnicalNotesLink", "ID", "lnk_4569_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_TechnicalNotesLink);
ECQECNST_ChildForm_TechnicalNotesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_TechnicalNotesForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_TechnicalNotesForm = new Control("ChildForm_TechnicalNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNTECHTX_NOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_TechnicalNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_TechnicalNotes_Notes...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_TechnicalNotes_Notes = new Control("ChildForm_TechnicalNotes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNTECHTX_NOTES_']/ancestor::form[1]/descendant::*[@id='ECN_TECH_TX']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_TechnicalNotes_Notes.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_TechnicalNotesForm);
formBttn = ECQECNST_ChildForm_TechnicalNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Customer Details");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_CustomerDetailsLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_CustomerDetailsLink = new Control("ChildForm_CustomerDetailsLink", "ID", "lnk_4864_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_CustomerDetailsLink);
ECQECNST_ChildForm_CustomerDetailsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_CustomerDetailsForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_CustomerDetailsForm = new Control("ChildForm_CustomerDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_CUSTADDRCNTACT_DETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_CustomerDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_CustomerDetailsFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_CustomerDetailsFormTable = new Control("ChildForm_CustomerDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_CUSTADDRCNTACT_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_CustomerDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_CustomerDetailsForm);
formBttn = ECQECNST_ChildForm_CustomerDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_CustomerDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_CustomerDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_CustomerDetails_ContactID...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_CustomerDetails_ContactID = new Control("ChildForm_CustomerDetails_ContactID", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_CUSTADDRCNTACT_DETAIL_']/ancestor::form[1]/descendant::*[@id='CNTACT_ID']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_CustomerDetails_ContactID.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_CustomerDetailsForm);
formBttn = ECQECNST_ChildForm_CustomerDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Standard Text");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_StandardTextLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_StandardTextLink = new Control("ChildForm_StandardTextLink", "ID", "lnk_5371_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_StandardTextLink);
ECQECNST_ChildForm_StandardTextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_StandardTextForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_StandardTextForm = new Control("ChildForm_StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_TEXTCODES_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_StandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_StandardTextFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_StandardTextFormTable = new Control("ChildForm_StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_TEXTCODES_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_StandardTextForm);
formBttn = ECQECNST_ChildForm_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_StandardTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_StandardText_Sequence...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_StandardText_Sequence = new Control("ChildForm_StandardText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_TEXTCODES_DTL_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_StandardText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_StandardTextForm);
formBttn = ECQECNST_ChildForm_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ECN Workflow");


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming Click on ChildForm_ECNWorkFlowLink...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ECNWorkFlowLink = new Control("ChildForm_ECNWorkFlowLink", "ID", "lnk_5372_ECQECNST_DTL");
			CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ECNWorkFlowLink);
ECQECNST_ChildForm_ECNWorkFlowLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ECNWorkFlowForm...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ECNWorkFlowForm = new Control("ChildForm_ECNWorkFlowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNWORKFLOW_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ECNWorkFlowForm.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExist on ChildForm_ECNWorkFlowFormTable...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ECNWorkFlowFormTable = new Control("ChildForm_ECNWorkFlowFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNWORKFLOW_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ECNWorkFlowFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ECNWorkFlowForm);
formBttn = ECQECNST_ChildForm_ECNWorkFlowForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECQECNST_ChildForm_ECNWorkFlowForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECQECNST_ChildForm_ECNWorkFlowForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECQECNST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECQECNST] Perfoming VerifyExists on ChildForm_ECNWorkFlow_Activity...", Logger.MessageType.INF);
			Control ECQECNST_ChildForm_ECNWorkFlow_Activity = new Control("ChildForm_ECNWorkFlow_Activity", "xpath", "//div[translate(@id,'0123456789','')='pr__ECQECNST_ECNWORKFLOW_DTL_']/ancestor::form[1]/descendant::*[@id='ACTVTY_NAME']");
			CPCommon.AssertEqual(true,ECQECNST_ChildForm_ECNWorkFlow_Activity.Exists());

												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_ChildForm_ECNWorkFlowForm);
formBttn = ECQECNST_ChildForm_ECNWorkFlowForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "ECQECNST";
							CPCommon.WaitControlDisplayed(ECQECNST_MainForm);
formBttn = ECQECNST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

