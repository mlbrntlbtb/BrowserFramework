 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMPROGP_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Progress Payment Bills Processing", "xpath","//div[@class='navItem'][.='Progress Payment Bills Processing']").Click();
new Control("Manage Progress Payment Bills", "xpath","//div[@class='navItem'][.='Manage Progress Payment Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Progress Payment Bills", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMPROGP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMPROGP_MainForm);
IWebElement formBttn = BLMPROGP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMPROGP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMPROGP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.AssertEqual(true,BLMPROGP_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on ProgressPaymentProject_Project...", Logger.MessageType.INF);
			Control BLMPROGP_ProgressPaymentProject_Project = new Control("ProgressPaymentProject_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMPROGP_ProgressPaymentProject_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BLMPROGP_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
IWebElement mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on BillingDetails_Customer...", Logger.MessageType.INF);
			Control BLMPROGP_BillingDetails_Customer = new Control("BillingDetails_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLMPROGP_BillingDetails_Customer.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Section 1; Box 1").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Section1Box1_ContractingOffice_Address...", Logger.MessageType.INF);
			Control BLMPROGP_Section1Box1_ContractingOffice_Address = new Control("Section1Box1_ContractingOffice_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_ADDR_CD']");
			CPCommon.AssertEqual(true,BLMPROGP_Section1Box1_ContractingOffice_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Section 1; Box 2").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Section1Box2_Contractor_Address...", Logger.MessageType.INF);
			Control BLMPROGP_Section1Box2_Contractor_Address = new Control("Section1Box2_Contractor_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_RMT_ADDR_CD']");
			CPCommon.AssertEqual(true,BLMPROGP_Section1Box2_Contractor_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Section 1; Box 3-8").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Section1Box38_ContractInformation_3SmallBus...", Logger.MessageType.INF);
			Control BLMPROGP_Section1Box38_ContractInformation_3SmallBus = new Control("Section1Box38_ContractInformation_3SmallBus", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SMALL_BUS_FL']");
			CPCommon.AssertEqual(true,BLMPROGP_Section1Box38_ContractInformation_3SmallBus.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Info 9-19").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on LineInfo919_Amount_Line9RESERVED...", Logger.MessageType.INF);
			Control BLMPROGP_LineInfo919_Amount_Line9RESERVED = new Control("LineInfo919_Amount_Line9RESERVED", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LINE_9_AMT1']");
			CPCommon.AssertEqual(true,BLMPROGP_LineInfo919_Amount_Line9RESERVED.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Info 20-26").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on LineInfo2026_Amount_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED...", Logger.MessageType.INF);
			Control BLMPROGP_LineInfo2026_Amount_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED = new Control("LineInfo2026_Amount_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LINE_20A_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_LineInfo2026_Amount_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line 13 Costs").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line13Costs_EstimatedTotalCostsLine12aPlus12b...", Logger.MessageType.INF);
			Control BLMPROGP_Line13Costs_EstimatedTotalCostsLine12aPlus12b = new Control("Line13Costs_EstimatedTotalCostsLine12aPlus12b", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DISPLAY_ETC_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_Line13Costs_EstimatedTotalCostsLine12aPlus12b.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainTab);
mTab = BLMPROGP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Certification Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on CertificationInfo_Certification_RepName...", Logger.MessageType.INF);
			Control BLMPROGP_CertificationInfo_Certification_RepName = new Control("CertificationInfo_Certification_RepName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CONTR_REP_NAME']");
			CPCommon.AssertEqual(true,BLMPROGP_CertificationInfo_Certification_RepName.Exists());

											Driver.SessionLogger.WriteLine("LINE 9 DETAIL");


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line9DetailLink...", Logger.MessageType.INF);
			Control BLMPROGP_Line9DetailLink = new Control("Line9DetailLink", "ID", "lnk_1003732_BLMPROGP_PRGPMTEDITHDR");
			CPCommon.AssertEqual(true,BLMPROGP_Line9DetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line9DetailLink);
BLMPROGP_Line9DetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line9DetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line9DetailForm = new Control("Line9DetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE9_LINECOSTS_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line9DetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line9Detail_BillableLineCosts_Current...", Logger.MessageType.INF);
			Control BLMPROGP_Line9Detail_BillableLineCosts_Current = new Control("Line9Detail_BillableLineCosts_Current", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE9_LINECOSTS_')]/ancestor::form[1]/descendant::*[@id='DISPLAY_CURR_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_Line9Detail_BillableLineCosts_Current.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line9Detail_Line9DetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line9Detail_Line9DetailForm = new Control("Line9Detail_Line9DetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPROGPDETAIL_PRGPMTEDITDETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line9Detail_Line9DetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line9Detail_Line9Detail_Ok...", Logger.MessageType.INF);
			Control BLMPROGP_Line9Detail_Line9Detail_Ok = new Control("Line9Detail_Line9Detail_Ok", "xpath", "//div[@class='app']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPROGP_Line9Detail_Line9Detail_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExist on Line9DetailLine9DetailFormTable...", Logger.MessageType.INF);
			Control BLMPROGP_Line9DetailLine9DetailFormTable = new Control("Line9DetailLine9DetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPROGPDETAIL_PRGPMTEDITDETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPROGP_Line9DetailLine9DetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line9DetailForm);
formBttn = BLMPROGP_Line9DetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LINE 10 DETAIL");


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line10DetailLink...", Logger.MessageType.INF);
			Control BLMPROGP_Line10DetailLink = new Control("Line10DetailLink", "ID", "lnk_1003729_BLMPROGP_PRGPMTEDITHDR");
			CPCommon.AssertEqual(true,BLMPROGP_Line10DetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line10DetailLink);
BLMPROGP_Line10DetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line10DetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line10DetailForm = new Control("Line10DetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE10_LINECOSTS_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line10DetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line10Detail_BillableLineCosts_Current...", Logger.MessageType.INF);
			Control BLMPROGP_Line10Detail_BillableLineCosts_Current = new Control("Line10Detail_BillableLineCosts_Current", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE10_LINECOSTS_')]/ancestor::form[1]/descendant::*[@id='DISPLAY_CURR_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_Line10Detail_BillableLineCosts_Current.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line10Detail_Line10DetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line10Detail_Line10DetailForm = new Control("Line10Detail_Line10DetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGPDETAIL_PRGPMTEDITDETL1_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line10Detail_Line10DetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line10Detail_Line10Detail_Ok...", Logger.MessageType.INF);
			Control BLMPROGP_Line10Detail_Line10Detail_Ok = new Control("Line10Detail_Line10Detail_Ok", "xpath", "//div[@class='app']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPROGP_Line10Detail_Line10Detail_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExist on Line10Detail_Line10DetailFormTable...", Logger.MessageType.INF);
			Control BLMPROGP_Line10Detail_Line10DetailFormTable = new Control("Line10Detail_Line10DetailFormTable", "xpath", "//div[starts-with(@id,'pr__BLMPROGPDETAIL_PRGPMTEDITDETL1_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPROGP_Line10Detail_Line10DetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line10DetailForm);
formBttn = BLMPROGP_Line10DetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LINE 14a DETAIL");


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14aDetailLink...", Logger.MessageType.INF);
			Control BLMPROGP_Line14aDetailLink = new Control("Line14aDetailLink", "ID", "lnk_1003734_BLMPROGP_PRGPMTEDITHDR");
			CPCommon.AssertEqual(true,BLMPROGP_Line14aDetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line14aDetailLink);
BLMPROGP_Line14aDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14aDetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line14aDetailForm = new Control("Line14aDetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE14A_TOTAL_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line14aDetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14aDetail_LineTotal...", Logger.MessageType.INF);
			Control BLMPROGP_Line14aDetail_LineTotal = new Control("Line14aDetail_LineTotal", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE14A_TOTAL_')]/ancestor::form[1]/descendant::*[@id='DISPLAY_CURR_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14aDetail_LineTotal.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14aDetail_Line14aDetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line14aDetail_Line14aDetailForm = new Control("Line14aDetail_Line14aDetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_PRGPMTSUBCDETL_14A_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line14aDetail_Line14aDetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14aDetail_Line14aDetail_Ok...", Logger.MessageType.INF);
			Control BLMPROGP_Line14aDetail_Line14aDetail_Ok = new Control("Line14aDetail_Line14aDetail_Ok", "xpath", "//div[@class='app']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14aDetail_Line14aDetail_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExist on Line14aDetail_Line14aDetailFormTable...", Logger.MessageType.INF);
			Control BLMPROGP_Line14aDetail_Line14aDetailFormTable = new Control("Line14aDetail_Line14aDetailFormTable", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_PRGPMTSUBCDETL_14A_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14aDetail_Line14aDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line14aDetailForm);
formBttn = BLMPROGP_Line14aDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LINE 14b DETAIL");


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14bDetailLink...", Logger.MessageType.INF);
			Control BLMPROGP_Line14bDetailLink = new Control("Line14bDetailLink", "ID", "lnk_1003736_BLMPROGP_PRGPMTEDITHDR");
			CPCommon.AssertEqual(true,BLMPROGP_Line14bDetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line14bDetailLink);
BLMPROGP_Line14bDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14bDetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line14bDetailForm = new Control("Line14bDetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE14B_TOTAL_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line14bDetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14bDetail_LineTotal...", Logger.MessageType.INF);
			Control BLMPROGP_Line14bDetail_LineTotal = new Control("Line14bDetail_LineTotal", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE14B_TOTAL_')]/ancestor::form[1]/descendant::*[@id='DISPLAY_CURR_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14bDetail_LineTotal.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14bDetail_Line14bDetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line14bDetail_Line14bDetailForm = new Control("Line14bDetail_Line14bDetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_PRGPMTSUBCDETL_14B_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line14bDetail_Line14bDetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14bDetail_Line14bDetail_Ok...", Logger.MessageType.INF);
			Control BLMPROGP_Line14bDetail_Line14bDetail_Ok = new Control("Line14bDetail_Line14bDetail_Ok", "xpath", "//div[@class='app']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14bDetail_Line14bDetail_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExist on Line14bDetail_Line14bDetailFormTable...", Logger.MessageType.INF);
			Control BLMPROGP_Line14bDetail_Line14bDetailFormTable = new Control("Line14bDetail_Line14bDetailFormTable", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_PRGPMTSUBCDETL_14B_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14bDetail_Line14bDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line14bDetailForm);
formBttn = BLMPROGP_Line14bDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LINE 14d DETAIL");


												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14dDetailLink...", Logger.MessageType.INF);
			Control BLMPROGP_Line14dDetailLink = new Control("Line14dDetailLink", "ID", "lnk_1003738_BLMPROGP_PRGPMTEDITHDR");
			CPCommon.AssertEqual(true,BLMPROGP_Line14dDetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line14dDetailLink);
BLMPROGP_Line14dDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14dDetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line14dDetailForm = new Control("Line14dDetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE14D_TOTAL_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line14dDetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14dDetail_LineTotal...", Logger.MessageType.INF);
			Control BLMPROGP_Line14dDetail_LineTotal = new Control("Line14dDetail_LineTotal", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_LINE14D_TOTAL_')]/ancestor::form[1]/descendant::*[@id='DISPLAY_CURR_AMT']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14dDetail_LineTotal.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14dDetail_Line14dDetailForm...", Logger.MessageType.INF);
			Control BLMPROGP_Line14dDetail_Line14dDetailForm = new Control("Line14dDetail_Line14dDetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_PRGPMTSUBCDETL_14D_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPROGP_Line14dDetail_Line14dDetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExists on Line14dDetail_Line14dDetail_Ok...", Logger.MessageType.INF);
			Control BLMPROGP_Line14dDetail_Line14dDetail_Ok = new Control("Line14dDetail_Line14dDetail_Ok", "xpath", "//div[@class='app']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14dDetail_Line14dDetail_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPROGP] Perfoming VerifyExist on Line14dDetail_Line14dDetailFormTable...", Logger.MessageType.INF);
			Control BLMPROGP_Line14dDetail_Line14dDetailFormTable = new Control("Line14dDetail_Line14dDetailFormTable", "xpath", "//div[starts-with(@id,'pr__BLMPROGP_PRGPMTSUBCDETL_14D_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPROGP_Line14dDetail_Line14dDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_Line14dDetailForm);
formBttn = BLMPROGP_Line14dDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMPROGP";
							CPCommon.WaitControlDisplayed(BLMPROGP_MainForm);
formBttn = BLMPROGP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

