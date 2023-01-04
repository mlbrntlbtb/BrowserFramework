 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMEBOM1_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='deptItem'][.='Bills of Material']").Click();
new Control("Bills of Material", "xpath","//div[@class='navItem'][.='Bills of Material']").Click();
new Control("Manage Engineering Bills of Material", "xpath","//div[@class='navItem'][.='Manage Engineering Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BMMEBOM1_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_MainForm);
IWebElement formBttn = BMMEBOM1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? BMMEBOM1_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
BMMEBOM1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BMMEBOM1_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_MainForm);
formBttn = BMMEBOM1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on Identification_AssemblyPart...", Logger.MessageType.INF);
			Control BMMEBOM1_Identification_AssemblyPart = new Control("Identification_AssemblyPart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMMEBOM1_Identification_AssemblyPart.Exists());

											Driver.SessionLogger.WriteLine("Configuration");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on Identification_ConfigurationLink...", Logger.MessageType.INF);
			Control BMMEBOM1_Identification_ConfigurationLink = new Control("Identification_ConfigurationLink", "ID", "lnk_4024_BMMEBOM_PART_HDR");
			CPCommon.AssertEqual(true,BMMEBOM1_Identification_ConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_Identification_ConfigurationLink);
BMMEBOM1_Identification_ConfigurationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on ConfigurationForm...", Logger.MessageType.INF);
			Control BMMEBOM1_ConfigurationForm = new Control("ConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BM_CONFIG_IDENTIFIERS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMEBOM1_ConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on Configuration_ShowAllConfigurations...", Logger.MessageType.INF);
			Control BMMEBOM1_Configuration_ShowAllConfigurations = new Control("Configuration_ShowAllConfigurations", "xpath", "//div[translate(@id,'0123456789','')='pr__BM_CONFIG_IDENTIFIERS_']/ancestor::form[1]/descendant::*[@id='SHOW_ALL_CONFIG']");
			CPCommon.AssertEqual(true,BMMEBOM1_Configuration_ShowAllConfigurations.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_ConfigurationForm);
formBttn = BMMEBOM1_ConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assembly Serial Numbers");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on Identification_AssemblySerialNumbersLink...", Logger.MessageType.INF);
			Control BMMEBOM1_Identification_AssemblySerialNumbersLink = new Control("Identification_AssemblySerialNumbersLink", "ID", "lnk_4035_BMMEBOM_PART_HDR");
			CPCommon.AssertEqual(true,BMMEBOM1_Identification_AssemblySerialNumbersLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_Identification_AssemblySerialNumbersLink);
BMMEBOM1_Identification_AssemblySerialNumbersLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on AssemblySerialNumbersTable...", Logger.MessageType.INF);
			Control BMMEBOM1_AssemblySerialNumbersTable = new Control("AssemblySerialNumbersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BM_ENDPARTCONFIG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_AssemblySerialNumbersTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on AssemblySerialNumbers_Ok...", Logger.MessageType.INF);
			Control BMMEBOM1_AssemblySerialNumbers_Ok = new Control("AssemblySerialNumbers_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BM_ENDPARTCONFIG_CTW_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BMMEBOM1_AssemblySerialNumbers_Ok.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming Close on AssemblySerialNumbersForm...", Logger.MessageType.INF);
			Control BMMEBOM1_AssemblySerialNumbersForm = new Control("AssemblySerialNumbersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BM_ENDPARTCONFIG_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_AssemblySerialNumbersForm);
formBttn = BMMEBOM1_AssemblySerialNumbersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on Identification_PartDocumentsLink...", Logger.MessageType.INF);
			Control BMMEBOM1_Identification_PartDocumentsLink = new Control("Identification_PartDocumentsLink", "ID", "lnk_4027_BMMEBOM_PART_HDR");
			CPCommon.AssertEqual(true,BMMEBOM1_Identification_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_Identification_PartDocumentsLink);
BMMEBOM1_Identification_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on PartDocumentsTable...", Logger.MessageType.INF);
			Control BMMEBOM1_PartDocumentsTable = new Control("PartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_PartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on PartDocumentsForm...", Logger.MessageType.INF);
			Control BMMEBOM1_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_PartDocumentsForm);
formBttn = BMMEBOM1_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control BMMEBOM1_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMMEBOM1_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_PartDocumentsForm);
formBttn = BMMEBOM1_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Clone EBOM Lines");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on Identification_CloneEBOMLinesLink...", Logger.MessageType.INF);
			Control BMMEBOM1_Identification_CloneEBOMLinesLink = new Control("Identification_CloneEBOMLinesLink", "ID", "lnk_4083_BMMEBOM_PART_HDR");
			CPCommon.AssertEqual(true,BMMEBOM1_Identification_CloneEBOMLinesLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_Identification_CloneEBOMLinesLink);
BMMEBOM1_Identification_CloneEBOMLinesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on CloneEBOMLinesForm...", Logger.MessageType.INF);
			Control BMMEBOM1_CloneEBOMLinesForm = new Control("CloneEBOMLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_EBOM_CLONE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMEBOM1_CloneEBOMLinesForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on CloneEBOMLines_FromAssemblyPart...", Logger.MessageType.INF);
			Control BMMEBOM1_CloneEBOMLines_FromAssemblyPart = new Control("CloneEBOMLines_FromAssemblyPart", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_EBOM_CLONE_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMMEBOM1_CloneEBOMLines_FromAssemblyPart.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CloneEBOMLinesForm);
formBttn = BMMEBOM1_CloneEBOMLinesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMEBOM1_CloneEBOMLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMEBOM1_CloneEBOMLinesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on CloneEBOMLinesTable...", Logger.MessageType.INF);
			Control BMMEBOM1_CloneEBOMLinesTable = new Control("CloneEBOMLinesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_EBOM_CLONE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_CloneEBOMLinesTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CloneEBOMLinesForm);
formBttn = BMMEBOM1_CloneEBOMLinesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Create Provisional Part");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on CreateProvisionalPartLink...", Logger.MessageType.INF);
			Control BMMEBOM1_CreateProvisionalPartLink = new Control("CreateProvisionalPartLink", "ID", "lnk_16811_BMMEBOM_PART_HDR");
			CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPartLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CreateProvisionalPartLink);
BMMEBOM1_CreateProvisionalPartLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on CreateProvisionalPartForm...", Logger.MessageType.INF);
			Control BMMEBOM1_CreateProvisionalPartForm = new Control("CreateProvisionalPartForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_CREATE_PROVPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPartForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on CreateProvisionalPart_PartID...", Logger.MessageType.INF);
			Control BMMEBOM1_CreateProvisionalPart_PartID = new Control("CreateProvisionalPart_PartID", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_CREATE_PROVPART_']/ancestor::form[1]/descendant::*[@id='PROV_PART_ID']");
			CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPart_PartID.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CreateProvisionalPartForm);
formBttn = BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on CreateProvisionalPartFormTable...", Logger.MessageType.INF);
			Control BMMEBOM1_CreateProvisionalPartFormTable = new Control("CreateProvisionalPartFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_CREATE_PROVPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPartFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CreateProvisionalPartForm);
formBttn = BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EBOM Line");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on EBOMLineTable...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLineTable = new Control("EBOMLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLineTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on EBOMLineForm...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLineForm = new Control("EBOMLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLineForm);
formBttn = BMMEBOM1_EBOMLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_EBOMLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_EBOMLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_EBOMLineForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_LineNo...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_LineNo = new Control("EBOMLine_LineNo", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_LineNo.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming Select on EBOMLineTab...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLineTab = new Control("EBOMLineTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLineTab);
IWebElement mTab = BMMEBOM1_EBOMLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_ComponentInformation_Component_PartStatus...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_ComponentInformation_Component_PartStatus = new Control("EBOMLine_ComponentInformation_Component_PartStatus", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='S_STATUS_TYPE']");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_ComponentInformation_Component_PartStatus.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLineTab);
mTab = BMMEBOM1_EBOMLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on ComponentComments_Comments...", Logger.MessageType.INF);
			Control BMMEBOM1_ComponentComments_Comments = new Control("ComponentComments_Comments", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NT']");
			CPCommon.AssertEqual(true,BMMEBOM1_ComponentComments_Comments.Exists());

											Driver.SessionLogger.WriteLine("Reference Designators");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_ReferenceDesignatorsLink...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_ReferenceDesignatorsLink = new Control("EBOMLine_ReferenceDesignatorsLink", "ID", "lnk_4036_BMMEBOM_ENGBOM_CTW");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_ReferenceDesignatorsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLine_ReferenceDesignatorsLink);
BMMEBOM1_EBOMLine_ReferenceDesignatorsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on ReferenceDesignatorsForm...", Logger.MessageType.INF);
			Control BMMEBOM1_ReferenceDesignatorsForm = new Control("ReferenceDesignatorsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOMREF_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMEBOM1_ReferenceDesignatorsForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_ReferenceDesignatorsForm);
formBttn = BMMEBOM1_ReferenceDesignatorsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Component Text");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_ComponentTextLink...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_ComponentTextLink = new Control("EBOMLine_ComponentTextLink", "ID", "lnk_4030_BMMEBOM_ENGBOM_CTW");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_ComponentTextLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLine_ComponentTextLink);
BMMEBOM1_EBOMLine_ComponentTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on ComponentTextTable...", Logger.MessageType.INF);
			Control BMMEBOM1_ComponentTextTable = new Control("ComponentTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOMTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_ComponentTextTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on ComponentTextForm...", Logger.MessageType.INF);
			Control BMMEBOM1_ComponentTextForm = new Control("ComponentTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOMTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_ComponentTextForm);
formBttn = BMMEBOM1_ComponentTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_ComponentTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_ComponentTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_ComponentTextForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on ComponentText_Sequence...", Logger.MessageType.INF);
			Control BMMEBOM1_ComponentText_Sequence = new Control("ComponentText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOMTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,BMMEBOM1_ComponentText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_ComponentTextForm);
formBttn = BMMEBOM1_ComponentTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Alternate Parts");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_AlternatePartsLink...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_AlternatePartsLink = new Control("EBOMLine_AlternatePartsLink", "ID", "lnk_4232_BMMEBOM_ENGBOM_CTW");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_AlternatePartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLine_AlternatePartsLink);
BMMEBOM1_EBOMLine_AlternatePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on AlternatePartsTable...", Logger.MessageType.INF);
			Control BMMEBOM1_AlternatePartsTable = new Control("AlternatePartsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ALTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_AlternatePartsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on AlternatePartsForm...", Logger.MessageType.INF);
			Control BMMEBOM1_AlternatePartsForm = new Control("AlternatePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ALTPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_AlternatePartsForm);
formBttn = BMMEBOM1_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_AlternatePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_AlternatePartsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on AlternateParts_Manufacturer_Manufacturer...", Logger.MessageType.INF);
			Control BMMEBOM1_AlternateParts_Manufacturer_Manufacturer = new Control("AlternateParts_Manufacturer_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ALTPART_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,BMMEBOM1_AlternateParts_Manufacturer_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_AlternatePartsForm);
formBttn = BMMEBOM1_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitute Parts");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_SubstitutePartsLink...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_SubstitutePartsLink = new Control("EBOMLine_SubstitutePartsLink", "ID", "lnk_4032_BMMEBOM_ENGBOM_CTW");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_SubstitutePartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLine_SubstitutePartsLink);
BMMEBOM1_EBOMLine_SubstitutePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on SubstitutePartsTable...", Logger.MessageType.INF);
			Control BMMEBOM1_SubstitutePartsTable = new Control("SubstitutePartsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_SUBSTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_SubstitutePartsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on SubstitutePartsForm...", Logger.MessageType.INF);
			Control BMMEBOM1_SubstitutePartsForm = new Control("SubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_SUBSTPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_SubstitutePartsForm);
formBttn = BMMEBOM1_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_SubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_SubstitutePartsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on SubstituteParts_Sequence...", Logger.MessageType.INF);
			Control BMMEBOM1_SubstituteParts_Sequence = new Control("SubstituteParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_SUBSTPART_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,BMMEBOM1_SubstituteParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_SubstitutePartsForm);
formBttn = BMMEBOM1_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_PartDocumentsLink...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_PartDocumentsLink = new Control("EBOMLine_PartDocumentsLink", "ID", "lnk_4033_BMMEBOM_ENGBOM_CTW");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLine_PartDocumentsLink);
BMMEBOM1_EBOMLine_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExist on EBOMLinePartDocumentsTable...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLinePartDocumentsTable = new Control("EBOMLinePartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLinePartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming ClickButton on EBOMLinePartDocumentsForm...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLinePartDocumentsForm = new Control("EBOMLinePartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLinePartDocumentsForm);
formBttn = BMMEBOM1_EBOMLinePartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEBOM1_EBOMLinePartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEBOM1_EBOMLinePartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_EBOMLinePartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLinePartDocuments_Type...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLinePartDocuments_Type = new Control("EBOMLinePartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLinePartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLinePartDocumentsForm);
formBttn = BMMEBOM1_EBOMLinePartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Create Provisional Part");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEBOM1] Perfoming VerifyExists on EBOMLine_CreateProvisionalPartLink...", Logger.MessageType.INF);
			Control BMMEBOM1_EBOMLine_CreateProvisionalPartLink = new Control("EBOMLine_CreateProvisionalPartLink", "ID", "lnk_4034_BMMEBOM_ENGBOM_CTW");
			CPCommon.AssertEqual(true,BMMEBOM1_EBOMLine_CreateProvisionalPartLink.Exists());

												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_EBOMLine_CreateProvisionalPartLink);
BMMEBOM1_EBOMLine_CreateProvisionalPartLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPartForm.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPart_PartID.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CreateProvisionalPartForm);
formBttn = BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.AssertEqual(true,BMMEBOM1_CreateProvisionalPartFormTable.Exists());

													
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_CreateProvisionalPartForm);
formBttn = BMMEBOM1_CreateProvisionalPartForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BMMEBOM1";
							CPCommon.WaitControlDisplayed(BMMEBOM1_MainForm);
formBttn = BMMEBOM1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");


												
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

