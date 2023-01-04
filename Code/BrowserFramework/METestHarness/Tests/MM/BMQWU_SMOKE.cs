 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMQWU_SMOKE : TestScript
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
new Control("View Where-Used Bills of Material", "xpath","//div[@class='navItem'][.='View Where-Used Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMQWU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMQWU_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on Component_Part...", Logger.MessageType.INF);
			Control BMQWU_Component_Part = new Control("Component_Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMPPARTID']");
			CPCommon.AssertEqual(true,BMQWU_Component_Part.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							BMQWU_Component_Part.Click();
BMQWU_Component_Part.SendKeys("#16 GA MTW RED", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
BMQWU_Component_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Component Part Information");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ComponentPartInformationLink...", Logger.MessageType.INF);
			Control BMQWU_ComponentPartInformationLink = new Control("ComponentPartInformationLink", "ID", "lnk_1005593_BMQWU_HEADER");
			CPCommon.AssertEqual(true,BMQWU_ComponentPartInformationLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ComponentPartInformationLink);
BMQWU_ComponentPartInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ComponentPartInformationForm...", Logger.MessageType.INF);
			Control BMQWU_ComponentPartInformationForm = new Control("ComponentPartInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_COMPONENTINFOHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMQWU_ComponentPartInformationForm.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ComponentPartInformationForm);
IWebElement formBttn = BMQWU_ComponentPartInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_ComponentPartInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_ComponentPartInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ComponentPartInformation_Component_MakeBuy...", Logger.MessageType.INF);
			Control BMQWU_ComponentPartInformation_Component_MakeBuy = new Control("ComponentPartInformation_Component_MakeBuy", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_COMPONENTINFOHDR_']/ancestor::form[1]/descendant::*[@id='S_MAKE_BUY_CD']");
			CPCommon.AssertEqual(true,BMQWU_ComponentPartInformation_Component_MakeBuy.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ComponentPartInformationForm);
formBttn = BMQWU_ComponentPartInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on PartDocumentsLink...", Logger.MessageType.INF);
			Control BMQWU_PartDocumentsLink = new Control("PartDocumentsLink", "ID", "lnk_3852_BMQWU_HEADER");
			CPCommon.AssertEqual(true,BMQWU_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_PartDocumentsLink);
BMQWU_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on PartDocumentsFormTable...", Logger.MessageType.INF);
			Control BMQWU_PartDocumentsFormTable = new Control("PartDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_PartDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on PartDocumentsForm...", Logger.MessageType.INF);
			Control BMQWU_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_PartDocumentsForm);
formBttn = BMQWU_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control BMQWU_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMQWU_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_PartDocumentsForm);
formBttn = BMQWU_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitute Parts");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on SubstitutePartsLink...", Logger.MessageType.INF);
			Control BMQWU_SubstitutePartsLink = new Control("SubstitutePartsLink", "ID", "lnk_3896_BMQWU_HEADER");
			CPCommon.AssertEqual(true,BMQWU_SubstitutePartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_SubstitutePartsLink);
BMQWU_SubstitutePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on SubtitutePartsFormTable...", Logger.MessageType.INF);
			Control BMQWU_SubtitutePartsFormTable = new Control("SubtitutePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_COMPONENTINFODTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_SubtitutePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on SubtitutePartsForm...", Logger.MessageType.INF);
			Control BMQWU_SubtitutePartsForm = new Control("SubtitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_COMPONENTINFODTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_SubtitutePartsForm);
formBttn = BMQWU_SubtitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_SubtitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_SubtitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_SubtitutePartsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on SubtituteParts_SubstitueParts_Sequence...", Logger.MessageType.INF);
			Control BMQWU_SubtituteParts_SubstitueParts_Sequence = new Control("SubtituteParts_SubstitueParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_COMPONENTINFODTL_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,BMQWU_SubtituteParts_SubstitueParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_SubtitutePartsForm);
formBttn = BMQWU_SubtitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control BMQWU_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control BMQWU_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_ChildForm);
formBttn = BMQWU_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_AssemblyPart...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_AssemblyPart = new Control("ChildForm_AssemblyPart", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_AssemblyPart.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMQWU_ChildForm_ChildFormTab);
IWebElement mTab = BMQWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Assembly Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_AssemblyInformation_PartStatus...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_AssemblyInformation_PartStatus = new Control("ChildForm_AssemblyInformation_PartStatus", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]/descendant::*[@id='ASY_STATUS_DESC']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_AssemblyInformation_PartStatus.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_ChildFormTab);
mTab = BMQWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "BOM Line Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_BOMLineInformation_ComponentType...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_BOMLineInformation_ComponentType = new Control("ChildForm_BOMLineInformation_ComponentType", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]/descendant::*[@id='S_COMP_TYP_CD']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_BOMLineInformation_ComponentType.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_ChildFormTab);
mTab = BMQWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_Notes_ReferenceDesignators...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_Notes_ReferenceDesignators = new Control("ChildForm_Notes_ReferenceDesignators", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ZBMQWU_DETAIL_']/ancestor::form[1]/descendant::*[@id='REF_DESIGNATOR_NT']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_Notes_ReferenceDesignators.Exists());

											Driver.SessionLogger.WriteLine("Line Text");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_LineTextLink...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_LineTextLink = new Control("ChildForm_LineTextLink", "ID", "lnk_3858_BMQWU_ZBMQWU_DETAIL");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_LineTextLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_LineTextLink);
BMQWU_ChildForm_LineTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on ChildForm_LineTextFormTable...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_LineTextFormTable = new Control("ChildForm_LineTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_COMPTEXTCODES_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_LineTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on ChildForm_LineTextForm...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_LineTextForm = new Control("ChildForm_LineTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_COMPTEXTCODES_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_ChildForm_LineTextForm);
formBttn = BMQWU_ChildForm_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_ChildForm_LineTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_ChildForm_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_ChildForm_LineTextForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_LineText_Sequence...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_LineText_Sequence = new Control("ChildForm_LineText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_COMPTEXTCODES_HDR_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_LineText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_LineTextForm);
formBttn = BMQWU_ChildForm_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Configuration Projects");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_ConfigurationPROJECTSLink...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_ConfigurationPROJECTSLink = new Control("ChildForm_ConfigurationPROJECTSLink", "ID", "lnk_1005459_BMQWU_ZBMQWU_DETAIL");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_ConfigurationPROJECTSLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_ConfigurationPROJECTSLink);
BMQWU_ChildForm_ConfigurationPROJECTSLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on ChildForm_ConfigurationPROJECTSFormTable...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_ConfigurationPROJECTSFormTable = new Control("ChildForm_ConfigurationPROJECTSFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_BOMCONFIGPROJ_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_ConfigurationPROJECTSFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on ChildForm_ConfigurationPROJECTSForm...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_ConfigurationPROJECTSForm = new Control("ChildForm_ConfigurationPROJECTSForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_BOMCONFIGPROJ_DETAIL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_ChildForm_ConfigurationPROJECTSForm);
formBttn = BMQWU_ChildForm_ConfigurationPROJECTSForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_ChildForm_ConfigurationPROJECTSForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_ChildForm_ConfigurationPROJECTSForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_ChildForm_ConfigurationPROJECTSForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_ConfigurationPROJECTS_Configuration...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_ConfigurationPROJECTS_Configuration = new Control("ChildForm_ConfigurationPROJECTS_Configuration", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_BOMCONFIGPROJ_DETAIL_']/ancestor::form[1]/descendant::*[@id='BOM_CONFIG_ID']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_ConfigurationPROJECTS_Configuration.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_ConfigurationPROJECTSForm);
formBttn = BMQWU_ChildForm_ConfigurationPROJECTSForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("End Part Configurations");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_EndPartConfigurationsLink...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_EndPartConfigurationsLink = new Control("ChildForm_EndPartConfigurationsLink", "ID", "lnk_1005470_BMQWU_ZBMQWU_DETAIL");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_EndPartConfigurationsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_EndPartConfigurationsLink);
BMQWU_ChildForm_EndPartConfigurationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on ChildForm_EndPartConfigurationsFormTable...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_EndPartConfigurationsFormTable = new Control("ChildForm_EndPartConfigurationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ENDPARTCONFIG_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_EndPartConfigurationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on ChildForm_EndPartConfigurationsForm...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_EndPartConfigurationsForm = new Control("ChildForm_EndPartConfigurationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ENDPARTCONFIG_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_ChildForm_EndPartConfigurationsForm);
formBttn = BMQWU_ChildForm_EndPartConfigurationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_ChildForm_EndPartConfigurationsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_ChildForm_EndPartConfigurationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_ChildForm_EndPartConfigurationsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_EndPartConfigurations_Configuration...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_EndPartConfigurations_Configuration = new Control("ChildForm_EndPartConfigurations_Configuration", "xpath", "//div[translate(@id,'0123456789','')='pr__BMQWU_ENDPARTCONFIG_DTL_']/ancestor::form[1]/descendant::*[@id='BOM_CONFIG_ID']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_EndPartConfigurations_Configuration.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_EndPartConfigurationsForm);
formBttn = BMQWU_ChildForm_EndPartConfigurationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_PartDocumentsLink...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_PartDocumentsLink = new Control("ChildForm_PartDocumentsLink", "ID", "lnk_1005573_BMQWU_ZBMQWU_DETAIL");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_PartDocumentsLink);
BMQWU_ChildForm_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExist on ChildForm_PartDocumentsFormTable...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_PartDocumentsFormTable = new Control("ChildForm_PartDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_PartDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming ClickButton on ChildForm_PartDocumentsForm...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_PartDocumentsForm = new Control("ChildForm_PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMQWU_ChildForm_PartDocumentsForm);
formBttn = BMQWU_ChildForm_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMQWU_ChildForm_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMQWU_ChildForm_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.AssertEqual(true,BMQWU_ChildForm_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMQWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMQWU] Perfoming VerifyExists on ChildForm_PartDocuments_Type...", Logger.MessageType.INF);
			Control BMQWU_ChildForm_PartDocuments_Type = new Control("ChildForm_PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMQWU_ChildForm_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_ChildForm_PartDocumentsForm);
formBttn = BMQWU_ChildForm_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BMQWU";
							CPCommon.WaitControlDisplayed(BMQWU_MainForm);
formBttn = BMQWU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

