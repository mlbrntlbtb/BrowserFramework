 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMPRPT_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Provisional Parts", "xpath","//div[@class='navItem'][.='Provisional Parts']").Click();
new Control("Manage Provisional Parts", "xpath","//div[@class='navItem'][.='Manage Provisional Parts']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMPRPT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMPRPT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on ProvisionalPartID...", Logger.MessageType.INF);
			Control PDMPRPT_ProvisionalPartID = new Control("ProvisionalPartID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROV_PART_ID']");
			CPCommon.AssertEqual(true,PDMPRPT_ProvisionalPartID.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PDMPRPT_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMPRPT_MainFormTab);
IWebElement mTab = PDMPRPT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Characteristics").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on BasicCharacteristics_ProvisionalPartType...", Logger.MessageType.INF);
			Control PDMPRPT_BasicCharacteristics_ProvisionalPartType = new Control("BasicCharacteristics_ProvisionalPartType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROV_PART_TYPE_CD']");
			CPCommon.AssertEqual(true,PDMPRPT_BasicCharacteristics_ProvisionalPartType.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_MainFormTab);
mTab = PDMPRPT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Order Policy").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on OrderPolicy_OrderPolicy_PolicyType...", Logger.MessageType.INF);
			Control PDMPRPT_OrderPolicy_OrderPolicy_PolicyType = new Control("OrderPolicy_OrderPolicy_PolicyType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_ORD_POLICY_TYPE']");
			CPCommon.AssertEqual(true,PDMPRPT_OrderPolicy_OrderPolicy_PolicyType.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_MainFormTab);
mTab = PDMPRPT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on Comments...", Logger.MessageType.INF);
			Control PDMPRPT_Comments = new Control("Comments", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROV_NT']");
			CPCommon.AssertEqual(true,PDMPRPT_Comments.Exists());

											Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on DocumentsLink...", Logger.MessageType.INF);
			Control PDMPRPT_DocumentsLink = new Control("DocumentsLink", "ID", "lnk_1002039_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_DocumentsLink);
PDMPRPT_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on DocumentsFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_DocumentsFormTable = new Control("DocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_DocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming ClickButton on DocumentsForm...", Logger.MessageType.INF);
			Control PDMPRPT_DocumentsForm = new Control("DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_DocumentsForm);
IWebElement formBttn = PDMPRPT_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRPT_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRPT_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.AssertEqual(true,PDMPRPT_DocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on Documents_Document...", Logger.MessageType.INF);
			Control PDMPRPT_Documents_Document = new Control("Documents_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PDMPRPT_Documents_Document.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_DocumentsForm);
formBttn = PDMPRPT_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Alternate");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on AlternateLink...", Logger.MessageType.INF);
			Control PDMPRPT_AlternateLink = new Control("AlternateLink", "ID", "lnk_1002040_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_AlternateLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_AlternateLink);
PDMPRPT_AlternateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on AlternateFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_AlternateFormTable = new Control("AlternateFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVALTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_AlternateFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming ClickButton on AlternateForm...", Logger.MessageType.INF);
			Control PDMPRPT_AlternateForm = new Control("AlternateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVALTPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_AlternateForm);
formBttn = PDMPRPT_AlternateForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRPT_AlternateForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRPT_AlternateForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.AssertEqual(true,PDMPRPT_AlternateForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on Alternate_Sequence...", Logger.MessageType.INF);
			Control PDMPRPT_Alternate_Sequence = new Control("Alternate_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVALTPART_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,PDMPRPT_Alternate_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_AlternateForm);
formBttn = PDMPRPT_AlternateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Lead Time");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on LeadTimeLink...", Logger.MessageType.INF);
			Control PDMPRPT_LeadTimeLink = new Control("LeadTimeLink", "ID", "lnk_1002197_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_LeadTimeLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_LeadTimeLink);
PDMPRPT_LeadTimeLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on LeadTime1FormTable...", Logger.MessageType.INF);
			Control PDMPRPT_LeadTime1FormTable = new Control("LeadTime1FormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_SLTTYPE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_LeadTime1FormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on LeadTime2FormTable...", Logger.MessageType.INF);
			Control PDMPRPT_LeadTime2FormTable = new Control("LeadTime2FormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTLT_MTNPROVPT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_LeadTime2FormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming Close on LeadTime2Form...", Logger.MessageType.INF);
			Control PDMPRPT_LeadTime2Form = new Control("LeadTime2Form", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTLT_MTNPROVPT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_LeadTime2Form);
formBttn = PDMPRPT_LeadTime2Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Assigned Standard text");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on AssignedStandardTextLink...", Logger.MessageType.INF);
			Control PDMPRPT_AssignedStandardTextLink = new Control("AssignedStandardTextLink", "ID", "lnk_1002198_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_AssignedStandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_AssignedStandardTextLink);
PDMPRPT_AssignedStandardTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_STDTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on AssignedStandardTextFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_AssignedStandardTextFormTable = new Control("AssignedStandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_AssignedStandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming Close on AssignedStandardTextForm...", Logger.MessageType.INF);
			Control PDMPRPT_AssignedStandardTextForm = new Control("AssignedStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PROVPARTTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_AssignedStandardTextForm);
formBttn = PDMPRPT_AssignedStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("EBOM Assy");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on EBOMAssyLink...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMAssyLink = new Control("EBOMAssyLink", "ID", "lnk_1002071_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_EBOMAssyLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_EBOMAssyLink);
PDMPRPT_EBOMAssyLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on EBOMAssyFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMAssyFormTable = new Control("EBOMAssyFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_ENGBOM_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_EBOMAssyFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming ClickButton on EBOMAssyForm...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMAssyForm = new Control("EBOMAssyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_ENGBOM_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_EBOMAssyForm);
formBttn = PDMPRPT_EBOMAssyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRPT_EBOMAssyForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRPT_EBOMAssyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.AssertEqual(true,PDMPRPT_EBOMAssyForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on EBOMAssy_AssemblyPart...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMAssy_AssemblyPart = new Control("EBOMAssy_AssemblyPart", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_ENGBOM_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,PDMPRPT_EBOMAssy_AssemblyPart.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_EBOMAssyForm);
formBttn = PDMPRPT_EBOMAssyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Comp");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on EBOMCompLink...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMCompLink = new Control("EBOMCompLink", "ID", "lnk_1002072_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_EBOMCompLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_EBOMCompLink);
PDMPRPT_EBOMCompLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on EBOMCompFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMCompFormTable = new Control("EBOMCompFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_ENGBOM_MTNPROVPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_EBOMCompFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming ClickButton on EBOMCompForm...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMCompForm = new Control("EBOMCompForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_ENGBOM_MTNPROVPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_EBOMCompForm);
formBttn = PDMPRPT_EBOMCompForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRPT_EBOMCompForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRPT_EBOMCompForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.AssertEqual(true,PDMPRPT_EBOMCompForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on EBOMComp_CompLine...", Logger.MessageType.INF);
			Control PDMPRPT_EBOMComp_CompLine = new Control("EBOMComp_CompLine", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_ENGBOM_MTNPROVPART_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,PDMPRPT_EBOMComp_CompLine.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_EBOMCompForm);
formBttn = PDMPRPT_EBOMCompForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PBOM");


												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on PBOMLink...", Logger.MessageType.INF);
			Control PDMPRPT_PBOMLink = new Control("PBOMLink", "ID", "lnk_1002073_PDMPRPT_PROVPART_MATNPROVPARTS");
			CPCommon.AssertEqual(true,PDMPRPT_PBOMLink.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_PBOMLink);
PDMPRPT_PBOMLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExist on PBOMFormTable...", Logger.MessageType.INF);
			Control PDMPRPT_PBOMFormTable = new Control("PBOMFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PBOMLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRPT_PBOMFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming ClickButton on PBOMForm...", Logger.MessageType.INF);
			Control PDMPRPT_PBOMForm = new Control("PBOMForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PBOMLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRPT_PBOMForm);
formBttn = PDMPRPT_PBOMForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRPT_PBOMForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRPT_PBOMForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.AssertEqual(true,PDMPRPT_PBOMForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRPT] Perfoming VerifyExists on PBOM_Proposal...", Logger.MessageType.INF);
			Control PDMPRPT_PBOM_Proposal = new Control("PBOM_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRPT_PBOMLN_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,PDMPRPT_PBOM_Proposal.Exists());

												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_PBOMForm);
formBttn = PDMPRPT_PBOMForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PDMPRPT";
							CPCommon.WaitControlDisplayed(PDMPRPT_MainForm);
formBttn = PDMPRPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

