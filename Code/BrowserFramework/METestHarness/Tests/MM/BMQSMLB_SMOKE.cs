 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMQSMLB_SMOKE : TestScript
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
new Control("Bills of Material Reports/Inquiries", "xpath","//div[@class='navItem'][.='Bills of Material Reports/Inquiries']").Click();
new Control("View Bills of Material", "xpath","//div[@class='navItem'][.='View Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMQSMLB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMQSMLB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on Assembly_Part...", Logger.MessageType.INF);
			Control BMQSMLB_Assembly_Part = new Control("Assembly_Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMQSMLB_Assembly_Part.Exists());

											Driver.SessionLogger.WriteLine("SelectEndItemConfiguration");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control BMQSMLB_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_4674_BMQSMLB_HDR");
			CPCommon.AssertEqual(true,BMQSMLB_SelectEndItemConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_SelectEndItemConfigurationLink);
BMQSMLB_SelectEndItemConfigurationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on SelectEndItemConfigurationFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_SelectEndItemConfigurationFormTable = new Control("SelectEndItemConfigurationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_SelectEndItemConfigurationFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control BMQSMLB_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMQSMLB_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_SelectEndItemConfigurationForm);
IWebElement formBttn = BMQSMLB_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Execute");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							BMQSMLB_Assembly_Part.Click();
BMQSMLB_Assembly_Part.SendKeys("SOC1-MAKEWMBOM002", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
BMQSMLB_Assembly_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("ComponentLines");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on ComponentLinesFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLinesFormTable = new Control("ComponentLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming ClickButton on ComponentLinesForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLinesForm = new Control("ComponentLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLinesForm);
formBttn = BMQSMLB_ComponentLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQSMLB_ComponentLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQSMLB_ComponentLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.AssertEqual(true,BMQSMLB_ComponentLinesForm.Exists());

													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_Level...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_Level = new Control("ComponentLines_Level", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]/descendant::*[@id='INDENTED_LVL']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_Level.Exists());

											Driver.SessionLogger.WriteLine("ComponentLinesTab");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming Select on ComponentLinesTab...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLinesTab = new Control("ComponentLinesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLinesTab);
IWebElement mTab = BMQSMLB_ComponentLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentInformation_Component_PartStatus...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentInformation_Component_PartStatus = new Control("ComponentLines_ComponentInformation_Component_PartStatus", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]/descendant::*[@id='S_STATUS_DESC']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentInformation_Component_PartStatus.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLinesTab);
mTab = BMQSMLB_ComponentLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Planning Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_PlanningDetails_Backflush_Warehouse...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_PlanningDetails_Backflush_Warehouse = new Control("ComponentLines_PlanningDetails_Backflush_Warehouse", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]/descendant::*[@id='BKFLSH_WHSE_ID']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_PlanningDetails_Backflush_Warehouse.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLinesTab);
mTab = BMQSMLB_ComponentLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_Notes_ReferenceDesignator...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_Notes_ReferenceDesignator = new Control("ComponentLines_Notes_ReferenceDesignator", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ZBMQSMLBEXPL_DTL_']/ancestor::form[1]/descendant::*[@id='REF_DESIGNATOR_NT']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_Notes_ReferenceDesignator.Exists());

											Driver.SessionLogger.WriteLine("Assembly Information Link");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_AssemblyInformationLink...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyInformationLink = new Control("ComponentLines_AssemblyInformationLink", "ID", "lnk_3958_BMQSMLB_ZBMQSMLBEXPL_DTL");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyInformationLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_AssemblyInformationLink);
BMQSMLB_ComponentLines_AssemblyInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_AssemblyInformationForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyInformationForm = new Control("ComponentLines_AssemblyInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ASSEMBLY_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyInformationForm.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_AssemblyInformation_Assembly_Part...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyInformation_Assembly_Part = new Control("ComponentLines_AssemblyInformation_Assembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ASSEMBLY_HDR_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyInformation_Assembly_Part.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_AssemblyInformationForm);
formBttn = BMQSMLB_ComponentLines_AssemblyInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assembly Documents Link");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_AssemblyDocumentsLink...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyDocumentsLink = new Control("ComponentLines_AssemblyDocumentsLink", "ID", "lnk_3970_BMQSMLB_ZBMQSMLBEXPL_DTL");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_AssemblyDocumentsLink);
BMQSMLB_ComponentLines_AssemblyDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on ComponentLines_AssemblyDocumentsFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyDocumentsFormTable = new Control("ComponentLines_AssemblyDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ASSEMBLY_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming ClickButton on ComponentLines_AssemblyDocumentsForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyDocumentsForm = new Control("ComponentLines_AssemblyDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ASSEMBLY_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_AssemblyDocumentsForm);
formBttn = BMQSMLB_ComponentLines_AssemblyDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQSMLB_ComponentLines_AssemblyDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQSMLB_ComponentLines_AssemblyDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_AssemblyDocuments_AssyPart...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_AssemblyDocuments_AssyPart = new Control("ComponentLines_AssemblyDocuments_AssyPart", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ASSEMBLY_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='ASM_PART_ID']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_AssemblyDocuments_AssyPart.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_AssemblyDocumentsForm);
formBttn = BMQSMLB_ComponentLines_AssemblyDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("component Documents Link");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentDocumentsLink...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentDocumentsLink = new Control("ComponentLines_ComponentDocumentsLink", "ID", "lnk_3971_BMQSMLB_ZBMQSMLBEXPL_DTL");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentDocumentsLink);
BMQSMLB_ComponentLines_ComponentDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on ComponentLines_ComponentDocumentsFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentDocumentsFormTable = new Control("ComponentLines_ComponentDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_COMPONENT_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming ClickButton on ComponentLines_ComponentDocumentsForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentDocumentsForm = new Control("ComponentLines_ComponentDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_COMPONENT_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentDocumentsForm);
formBttn = BMQSMLB_ComponentLines_ComponentDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQSMLB_ComponentLines_ComponentDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQSMLB_ComponentLines_ComponentDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentDocuments_Type...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentDocuments_Type = new Control("ComponentLines_ComponentDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_COMPONENT_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentDocumentsForm);
formBttn = BMQSMLB_ComponentLines_ComponentDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Text Link");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLinesForm);
formBttn = BMQSMLB_ComponentLinesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMQSMLB_ComponentLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMQSMLB_ComponentLinesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_LineTextLink...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_LineTextLink = new Control("ComponentLines_LineTextLink", "ID", "lnk_3972_BMQSMLB_ZBMQSMLBEXPL_DTL");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_LineTextLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_LineTextLink);
BMQSMLB_ComponentLines_LineTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on ComponentLines_LineTextFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_LineTextFormTable = new Control("ComponentLines_LineTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_COMPTEXTCODES_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_LineTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming ClickButtonIfExists on ComponentLines_LineTextForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_LineTextForm = new Control("ComponentLines_LineTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_COMPTEXTCODES_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_LineTextForm);
formBttn = BMQSMLB_ComponentLines_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQSMLB_ComponentLines_LineTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQSMLB_ComponentLines_LineTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_LineTextForm.Exists());

													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_LineText_Sequence...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_LineText_Sequence = new Control("ComponentLines_LineText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_COMPTEXTCODES_HDR_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_LineText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_LineTextForm);
formBttn = BMQSMLB_ComponentLines_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Component Alternate Parts Link");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentAlternatePartsLink...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentAlternatePartsLink = new Control("ComponentLines_ComponentAlternatePartsLink", "ID", "lnk_3973_BMQSMLB_ZBMQSMLBEXPL_DTL");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentAlternatePartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentAlternatePartsLink);
BMQSMLB_ComponentLines_ComponentAlternatePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on ComponentLines_ComponentAlternatePartsFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentAlternatePartsFormTable = new Control("ComponentLines_ComponentAlternatePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ALTERNATEPARTS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentAlternatePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming ClickButton on ComponentLines_ComponentAlternatePartsForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentAlternatePartsForm = new Control("ComponentLines_ComponentAlternatePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ALTERNATEPARTS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentAlternatePartsForm);
formBttn = BMQSMLB_ComponentLines_ComponentAlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQSMLB_ComponentLines_ComponentAlternatePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQSMLB_ComponentLines_ComponentAlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentAlternatePartsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentAlternateParts_Sequence...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentAlternateParts_Sequence = new Control("ComponentLines_ComponentAlternateParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_ALTERNATEPARTS_DTL_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentAlternateParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentAlternatePartsForm);
formBttn = BMQSMLB_ComponentLines_ComponentAlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Component Substitute Parts Link");


												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentSubstitutePartsLink...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentSubstitutePartsLink = new Control("ComponentLines_ComponentSubstitutePartsLink", "ID", "lnk_3974_BMQSMLB_ZBMQSMLBEXPL_DTL");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentSubstitutePartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentSubstitutePartsLink);
BMQSMLB_ComponentLines_ComponentSubstitutePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExist on ComponentLines_ComponentSubstitutePartsFormTable...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentSubstitutePartsFormTable = new Control("ComponentLines_ComponentSubstitutePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_SUBSTITUTEPART_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentSubstitutePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming ClickButton on ComponentLines_ComponentSubstitutePartsForm...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentSubstitutePartsForm = new Control("ComponentLines_ComponentSubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_SUBSTITUTEPART_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentSubstitutePartsForm);
formBttn = BMQSMLB_ComponentLines_ComponentSubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQSMLB_ComponentLines_ComponentSubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQSMLB_ComponentLines_ComponentSubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentSubstitutePartsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQSMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQSMLB] Perfoming VerifyExists on ComponentLines_ComponentSubstituteParts_Sequence...", Logger.MessageType.INF);
			Control BMQSMLB_ComponentLines_ComponentSubstituteParts_Sequence = new Control("ComponentLines_ComponentSubstituteParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQSMLB_SUBSTITUTEPART_DTL_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,BMQSMLB_ComponentLines_ComponentSubstituteParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_ComponentLines_ComponentSubstitutePartsForm);
formBttn = BMQSMLB_ComponentLines_ComponentSubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "BMQSMLB";
							CPCommon.WaitControlDisplayed(BMQSMLB_MainForm);
formBttn = BMQSMLB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

