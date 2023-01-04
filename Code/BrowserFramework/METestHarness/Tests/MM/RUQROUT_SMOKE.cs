 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUQROUT_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Routings Reports/Inquiries", "xpath","//div[@class='navItem'][.='Routings Reports/Inquiries']").Click();
new Control("View Routings", "xpath","//div[@class='navItem'][.='View Routings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUQROUT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUQROUT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control RUQROUT_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,RUQROUT_Part.Exists());

												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Select End Item Conf.");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming Click on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control RUQROUT_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_4685_RUQROUT_HDR");
			CPCommon.WaitControlDisplayed(RUQROUT_SelectEndItemConfigurationLink);
RUQROUT_SelectEndItemConfigurationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control RUQROUT_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUQROUT_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExist on SelectEndItemConfigurationFormTable...", Logger.MessageType.INF);
			Control RUQROUT_SelectEndItemConfigurationFormTable = new Control("SelectEndItemConfigurationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUQROUT_SelectEndItemConfigurationFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_SelectEndItemConfigurationForm);
IWebElement formBttn = RUQROUT_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routing Details");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetailsForm...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetailsForm = new Control("RoutingDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGHDR_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExist on RoutingDetailsFormTable...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetailsFormTable = new Control("RoutingDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGHDR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetailsForm);
formBttn = RUQROUT_RoutingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUQROUT_RoutingDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUQROUT_RoutingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_Company...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_Company = new Control("RoutingDetails_Company", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGHDR_CTW_']/ancestor::form[1]/descendant::*[@id='COMPANY_ID']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_Company.Exists());

											Driver.SessionLogger.WriteLine("Routing Lines");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming Click on RoutingDetails_RoutingLinesLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLinesLink = new Control("RoutingDetails_RoutingLinesLink", "ID", "lnk_4465_RUQROUT_ROUTINGHDR_CTW");
			CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_RoutingLinesLink);
RUQROUT_RoutingDetails_RoutingLinesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLinesForm...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLinesForm = new Control("RoutingDetails_RoutingLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLinesForm.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExist on RoutingDetails_RoutingLinesFormTable...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLinesFormTable = new Control("RoutingDetails_RoutingLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_RoutingLinesForm);
formBttn = RUQROUT_RoutingDetails_RoutingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUQROUT_RoutingDetails_RoutingLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUQROUT_RoutingDetails_RoutingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_OperationSequence...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_OperationSequence = new Control("RoutingDetails_RoutingLines_OperationSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ROUT_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_OperationSequence.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_Operation_RunType...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_Operation_RunType = new Control("RoutingDetails_RoutingLines_Operation_RunType", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='S_RUN_TYPE']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_Operation_RunType.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming Select on RoutingDetails_RoutingLinesTab...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLinesTab = new Control("RoutingDetails_RoutingLinesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_RoutingLinesTab);
IWebElement mTab = RUQROUT_RoutingDetails_RoutingLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Lead Time/Capacity").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_LeadTimeCapacity_KeyResource_FixedUsage...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_LeadTimeCapacity_KeyResource_FixedUsage = new Control("RoutingDetails_RoutingLines_LeadTimeCapacity_KeyResource_FixedUsage", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='KEY_RSRCE_FIXD_QTY']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_LeadTimeCapacity_KeyResource_FixedUsage.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_RoutingLinesTab);
mTab = RUQROUT_RoutingDetails_RoutingLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_Cost_CostsRates_CurrentOperation_BurdenRate...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_Cost_CostsRates_CurrentOperation_BurdenRate = new Control("RoutingDetails_RoutingLines_Cost_CostsRates_CurrentOperation_BurdenRate", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='OP_BURDEN_RT']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_Cost_CostsRates_CurrentOperation_BurdenRate.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_RoutingLinesTab);
mTab = RUQROUT_RoutingDetails_RoutingLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Additional Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_AdditionalInformation_Organization...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_AdditionalInformation_Organization = new Control("RoutingDetails_RoutingLines_AdditionalInformation_Organization", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_AdditionalInformation_Organization.Exists());

											Driver.SessionLogger.WriteLine("Links");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_LineNotesLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_LineNotesLink = new Control("RoutingDetails_RoutingLines_LineNotesLink", "ID", "lnk_4556_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_LineNotesLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_KeyResourcesLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_KeyResourcesLink = new Control("RoutingDetails_RoutingLines_KeyResourcesLink", "ID", "lnk_4576_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_KeyResourcesLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_LaborOperationRatesLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_LaborOperationRatesLink = new Control("RoutingDetails_RoutingLines_LaborOperationRatesLink", "ID", "lnk_4525_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_LaborOperationRatesLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_SubcontractorOperationsLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_SubcontractorOperationsLink = new Control("RoutingDetails_RoutingLines_SubcontractorOperationsLink", "ID", "lnk_4526_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_SubcontractorOperationsLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_StandardTextLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_StandardTextLink = new Control("RoutingDetails_RoutingLines_StandardTextLink", "ID", "lnk_4481_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_StandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_WorkCentersLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_WorkCentersLink = new Control("RoutingDetails_RoutingLines_WorkCentersLink", "ID", "lnk_4473_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_WorkCentersLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_LaborClassificationsLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_LaborClassificationsLink = new Control("RoutingDetails_RoutingLines_LaborClassificationsLink", "ID", "lnk_4557_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_LaborClassificationsLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_EquipmentLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_EquipmentLink = new Control("RoutingDetails_RoutingLines_EquipmentLink", "ID", "lnk_4530_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_EquipmentLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_RoutingDocumentsLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_RoutingDocumentsLink = new Control("RoutingDetails_RoutingLines_RoutingDocumentsLink", "ID", "lnk_4811_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_RoutingDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_RoutingLines_UserDefinedFieldsLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_RoutingLines_UserDefinedFieldsLink = new Control("RoutingDetails_RoutingLines_UserDefinedFieldsLink", "ID", "lnk_5049_RUQROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_RoutingLines_UserDefinedFieldsLink.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_RoutingLinesForm);
formBttn = RUQROUT_RoutingDetails_RoutingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Configurations");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming Click on RoutingDetails_ConfigurationsLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_ConfigurationsLink = new Control("RoutingDetails_ConfigurationsLink", "ID", "lnk_4489_RUQROUT_ROUTINGHDR_CTW");
			CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_ConfigurationsLink);
RUQROUT_RoutingDetails_ConfigurationsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_ConfigurationsForm...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_ConfigurationsForm = new Control("RoutingDetails_ConfigurationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGCONFIG_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_ConfigurationsForm.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExist on RoutingDetails_ConfigurationsFormTable...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_ConfigurationsFormTable = new Control("RoutingDetails_ConfigurationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUQROUT_ROUTINGCONFIG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_ConfigurationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_ConfigurationsForm);
formBttn = RUQROUT_RoutingDetails_ConfigurationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming Click on RoutingDetails_PartDocumentsLink...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_PartDocumentsLink = new Control("RoutingDetails_PartDocumentsLink", "ID", "lnk_1006243_RUQROUT_ROUTINGHDR_CTW");
			CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_PartDocumentsLink);
RUQROUT_RoutingDetails_PartDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_PartDocumentsForm...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_PartDocumentsForm = new Control("RoutingDetails_PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExist on RoutingDetails_PartDocumentsFormTable...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_PartDocumentsFormTable = new Control("RoutingDetails_PartDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_PartDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_PartDocumentsForm);
formBttn = RUQROUT_RoutingDetails_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUQROUT_RoutingDetails_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUQROUT_RoutingDetails_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUQROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUQROUT] Perfoming VerifyExists on RoutingDetails_PartDocuments_Type...", Logger.MessageType.INF);
			Control RUQROUT_RoutingDetails_PartDocuments_Type = new Control("RoutingDetails_PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,RUQROUT_RoutingDetails_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_RoutingDetails_PartDocumentsForm);
formBttn = RUQROUT_RoutingDetails_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "RUQROUT";
							CPCommon.WaitControlDisplayed(RUQROUT_MainForm);
formBttn = RUQROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

