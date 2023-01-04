 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMRROUT_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='navItem'][.='Routings']").Click();
new Control("Release Routings", "xpath","//div[@class='navItem'][.='Release Routings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUMRROUT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUMRROUT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control RUMRROUT_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,RUMRROUT_Part.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_MainForm);
IWebElement formBttn = RUMRROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? RUMRROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
RUMRROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Query] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_MainForm);
formBttn = RUMRROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_MainForm);
formBttn = RUMRROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Next']")).Count <= 0 ? RUMRROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Next')]")).FirstOrDefault() :
RUMRROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Next']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Next not found ");


												Driver.SessionLogger.WriteLine("MAIN FORM LINKS");


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on RoutingLineFilterLink...", Logger.MessageType.INF);
			Control RUMRROUT_RoutingLineFilterLink = new Control("RoutingLineFilterLink", "ID", "lnk_4700_RUMROUT_HEADER");
			CPCommon.AssertEqual(true,RUMRROUT_RoutingLineFilterLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_RoutingLineFilterLink);
RUMRROUT_RoutingLineFilterLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on RoutingLineFilterForm...", Logger.MessageType.INF);
			Control RUMRROUT_RoutingLineFilterForm = new Control("RoutingLineFilterForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_FILTER_ROUTLINE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUMRROUT_RoutingLineFilterForm.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on RoutingLineFilter_AsofDate...", Logger.MessageType.INF);
			Control RUMRROUT_RoutingLineFilter_AsofDate = new Control("RoutingLineFilter_AsofDate", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_FILTER_ROUTLINE_']/ancestor::form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,RUMRROUT_RoutingLineFilter_AsofDate.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_RoutingLineFilterForm);
formBttn = RUMRROUT_RoutingLineFilterForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on LinkConfigurationsLink...", Logger.MessageType.INF);
			Control RUMRROUT_LinkConfigurationsLink = new Control("LinkConfigurationsLink", "ID", "lnk_1006462_RUMROUT_HEADER");
			CPCommon.AssertEqual(true,RUMRROUT_LinkConfigurationsLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_LinkConfigurationsLink);
RUMRROUT_LinkConfigurationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on LinkConfigurationTable...", Logger.MessageType.INF);
			Control RUMRROUT_LinkConfigurationTable = new Control("LinkConfigurationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGCONFIG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_LinkConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming Click on LinkConfiguration_Ok...", Logger.MessageType.INF);
			Control RUMRROUT_LinkConfiguration_Ok = new Control("LinkConfiguration_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGCONFIG_CTW_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(RUMRROUT_LinkConfiguration_Ok);
if (RUMRROUT_LinkConfiguration_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
RUMRROUT_LinkConfiguration_Ok.Click(5,5);
else RUMRROUT_LinkConfiguration_Ok.Click(4.5);


											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming ClickButtonIfExists on ChildForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm);
formBttn = RUMRROUT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_OperationSequence...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_OperationSequence = new Control("ChildForm_OperationSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ROUT_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_OperationSequence.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM TAB");


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_Operation_OperationType...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_Operation_OperationType = new Control("ChildForm_Operation_OperationType", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='OP_TYPE_CD']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_Operation_OperationType.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming Select on ChildForm_Tab...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_Tab = new Control("ChildForm_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_Tab);
IWebElement mTab = RUMRROUT_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Lead Time/Capacity").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_LeadTimeCapacity_LeadTimeCapacity_FixedRunHours...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_LeadTimeCapacity_LeadTimeCapacity_FixedRunHours = new Control("ChildForm_LeadTimeCapacity_LeadTimeCapacity_FixedRunHours", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='UNIT_RUN_HOURS']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_LeadTimeCapacity_LeadTimeCapacity_FixedRunHours.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_Tab);
mTab = RUMRROUT_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_Cost_Currency...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_Cost_Currency = new Control("ChildForm_Cost_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='CURRENCY']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_Cost_Currency.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_Tab);
mTab = RUMRROUT_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Additional Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_AdditionalInformation_Organization...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_AdditionalInformation_Organization = new Control("ChildForm_AdditionalInformation_Organization", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_AdditionalInformation_Organization.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_Tab);
mTab = RUMRROUT_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "User-Defined Fields").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_UserDefinedFields_Supervisor...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_UserDefinedFields_Supervisor = new Control("ChildForm_UserDefinedFields_Supervisor", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='UDEF_DESC_1']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_UserDefinedFields_Supervisor.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_Tab);
mTab = RUMRROUT_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_LineNotes_LineNotes...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_LineNotes_LineNotes = new Control("ChildForm_LineNotes_LineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ROUT_LN_TX']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_LineNotes_LineNotes.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM LINK");


												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_RoutingLineComponentsLink...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_RoutingLineComponentsLink = new Control("ChildForm_RoutingLineComponentsLink", "ID", "lnk_16004_RUMROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_RoutingLineComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_RoutingLineComponentsLink);
RUMRROUT_ChildForm_RoutingLineComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildForm_RoutingLineComponentsFormTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_RoutingLineComponentsFormTable = new Control("ChildForm_RoutingLineComponentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNCOMP_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_RoutingLineComponentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming ClickButton on ChildForm_RoutingLineComponentsForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_RoutingLineComponentsForm = new Control("ChildForm_RoutingLineComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNCOMP_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_RoutingLineComponentsForm);
formBttn = RUMRROUT_ChildForm_RoutingLineComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_ChildForm_RoutingLineComponentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_ChildForm_RoutingLineComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.AssertEqual(true,RUMRROUT_ChildForm_RoutingLineComponentsForm.Exists());

													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_RoutingLineComponents_ComponentPart...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_RoutingLineComponents_ComponentPart = new Control("ChildForm_RoutingLineComponents_ComponentPart", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNCOMP_CTW_']/ancestor::form[1]/descendant::*[@id='COMP_PART_ID']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_RoutingLineComponents_ComponentPart.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_RoutingLineComponentsForm);
formBttn = RUMRROUT_ChildForm_RoutingLineComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_StandardTextLink...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_StandardTextLink = new Control("ChildForm_StandardTextLink", "ID", "lnk_1006466_RUMROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_StandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_StandardTextLink);
RUMRROUT_ChildForm_StandardTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildForm_StandardTextFormFormTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_StandardTextFormFormTable = new Control("ChildForm_StandardTextFormFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_StandardTextFormFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming ClickButton on ChildForm_StandardTextForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_StandardTextForm = new Control("ChildForm_StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_StandardTextForm);
formBttn = RUMRROUT_ChildForm_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_ChildForm_StandardTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_ChildForm_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.AssertEqual(true,RUMRROUT_ChildForm_StandardTextForm.Exists());

													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_StandardText_Sequence...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_StandardText_Sequence = new Control("ChildForm_StandardText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_StandardText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_StandardTextForm);
formBttn = RUMRROUT_ChildForm_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_DocumentsLink...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_DocumentsLink = new Control("ChildForm_DocumentsLink", "ID", "lnk_1006467_RUMROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_DocumentsLink);
RUMRROUT_ChildForm_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildForm_DocumentsFormTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_DocumentsFormTable = new Control("ChildForm_DocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNDOC_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_DocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming ClickButton on ChildForm_DocumentsForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_DocumentsForm = new Control("ChildForm_DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNDOC_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_DocumentsForm);
formBttn = RUMRROUT_ChildForm_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_ChildForm_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_ChildForm_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.AssertEqual(true,RUMRROUT_ChildForm_DocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_Documents_Document...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_Documents_Document = new Control("ChildForm_Documents_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNDOC_CTW_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_Documents_Document.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_DocumentsForm);
formBttn = RUMRROUT_ChildForm_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_EquipmentLink...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_EquipmentLink = new Control("ChildForm_EquipmentLink", "ID", "lnk_15787_RUMROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_EquipmentLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_EquipmentLink);
RUMRROUT_ChildForm_EquipmentLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildForm_EquipmentFormTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_EquipmentFormTable = new Control("ChildForm_EquipmentFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNEQUIP_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_EquipmentFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming ClickButton on ChildForm_EquipmentForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_EquipmentForm = new Control("ChildForm_EquipmentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNEQUIP_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_EquipmentForm);
formBttn = RUMRROUT_ChildForm_EquipmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_ChildForm_EquipmentForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_ChildForm_EquipmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.AssertEqual(true,RUMRROUT_ChildForm_EquipmentForm.Exists());

													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_Equipment_Equipment...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_Equipment_Equipment = new Control("ChildForm_Equipment_Equipment", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNEQUIP_CTW_']/ancestor::form[1]/descendant::*[@id='EQUIP_ID']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_Equipment_Equipment.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_EquipmentForm);
formBttn = RUMRROUT_ChildForm_EquipmentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_LaborClassificationsLink...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_LaborClassificationsLink = new Control("ChildForm_LaborClassificationsLink", "ID", "lnk_15788_RUMROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_LaborClassificationsLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_LaborClassificationsLink);
RUMRROUT_ChildForm_LaborClassificationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildForm_LaborClassificationsFormTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_LaborClassificationsFormTable = new Control("ChildForm_LaborClassificationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNLAB_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_LaborClassificationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming ClickButton on ChildForm_LaborClassificationsForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_LaborClassificationsForm = new Control("ChildForm_LaborClassificationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNLAB_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_LaborClassificationsForm);
formBttn = RUMRROUT_ChildForm_LaborClassificationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMRROUT_ChildForm_LaborClassificationsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMRROUT_ChildForm_LaborClassificationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.AssertEqual(true,RUMRROUT_ChildForm_LaborClassificationsForm.Exists());

													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_LaborClassifications_LaborClassification...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_LaborClassifications_LaborClassification = new Control("ChildForm_LaborClassifications_LaborClassification", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLNLAB_CTW_']/ancestor::form[1]/descendant::*[@id='RU_LAB_CLASS_CD']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_LaborClassifications_LaborClassification.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_LaborClassificationsForm);
formBttn = RUMRROUT_ChildForm_LaborClassificationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExists on ChildForm_AlternateWorkCenterLink...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_AlternateWorkCenterLink = new Control("ChildForm_AlternateWorkCenterLink", "ID", "lnk_4586_RUMROUT_ROUTINGLN_CTW");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_AlternateWorkCenterLink.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_AlternateWorkCenterLink);
RUMRROUT_ChildForm_AlternateWorkCenterLink.Click(1.5);


													
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming VerifyExist on ChildForm_AlternateWorkCenterTable...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_AlternateWorkCenterTable = new Control("ChildForm_AlternateWorkCenterTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ALTWORKCENTER_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMRROUT_ChildForm_AlternateWorkCenterTable.Exists());

												
				CPCommon.CurrentComponent = "RUMRROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRROUT] Perfoming Close on ChildForm_AlternateWorkCenterForm...", Logger.MessageType.INF);
			Control RUMRROUT_ChildForm_AlternateWorkCenterForm = new Control("ChildForm_AlternateWorkCenterForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ALTWORKCENTER_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMRROUT_ChildForm_AlternateWorkCenterForm);
formBttn = RUMRROUT_ChildForm_AlternateWorkCenterForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "RUMRROUT";
							CPCommon.WaitControlDisplayed(RUMRROUT_MainForm);
formBttn = RUMRROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

