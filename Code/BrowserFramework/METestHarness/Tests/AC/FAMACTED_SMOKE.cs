 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMACTED_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Asset Master Records", "xpath","//div[@class='navItem'][.='Asset Master Records']").Click();
new Control("Manage Autocreation Transactions", "xpath","//div[@class='navItem'][.='Manage Autocreation Transactions']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMACTED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMACTED_MainForm);
IWebElement formBttn = FAMACTED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMACTED_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMACTED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on Identification_TemporaryAssetNo...", Logger.MessageType.INF);
			Control FAMACTED_Identification_TemporaryAssetNo = new Control("Identification_TemporaryAssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TEMP_ASSET_ID']");
			CPCommon.AssertEqual(true,FAMACTED_Identification_TemporaryAssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.AssertEqual(true,FAMACTED_MainForm.Exists());

													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming Select on MainForm_Tab...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_Tab = new Control("MainForm_Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
IWebElement mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_DescInfo_RecordStatus_RecordStatus...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_DescInfo_RecordStatus_RecordStatus = new Control("MainForm_DescInfo_RecordStatus_RecordStatus", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_STATUS_CD']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_DescInfo_RecordStatus_RecordStatus.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_PurchInfo_PurchaseOrder_PODate...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_PurchInfo_PurchaseOrder_PODate = new Control("MainForm_PurchInfo_PurchaseOrder_PODate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_DT']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_PurchInfo_PurchaseOrder_PODate.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Loc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_LocInfo_GeneralLocationInfo_City...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_LocInfo_GeneralLocationInfo_City = new Control("MainForm_LocInfo_GeneralLocationInfo_City", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CITY_NAME']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_LocInfo_GeneralLocationInfo_City.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_CostInfo_Units_Quantity...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_CostInfo_Units_Quantity = new Control("MainForm_CostInfo_Units_Quantity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UNITS_QTY_NO']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_CostInfo_Units_Quantity.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Acct Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_AcctInfo_AssetAccount_AssetAccount...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_AcctInfo_AssetAccount_AssetAccount = new Control("MainForm_AcctInfo_AssetAccount_AssetAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_AcctInfo_AssetAccount_AssetAccount.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "G/L Book Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_GLBookInfo_DepreciationInfo_DeprMethodCode...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_GLBookInfo_DepreciationInfo_DeprMethodCode = new Control("MainForm_GLBookInfo_DepreciationInfo_DeprMethodCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B1_DEPR_MTHD_CD']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_GLBookInfo_DepreciationInfo_DeprMethodCode.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Books Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_OtherBooksInfo_Tax_DeprMethodCode...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_OtherBooksInfo_Tax_DeprMethodCode = new Control("MainForm_OtherBooksInfo_Tax_DeprMethodCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B2_DEPR_MTHD_CD']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_OtherBooksInfo_Tax_DeprMethodCode.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_Tab);
mTab = FAMACTED_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_GovtInfo_NatStockNo...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_GovtInfo_NatStockNo = new Control("MainForm_GovtInfo_NatStockNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NSN_ID']");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_GovtInfo_NatStockNo.Exists());

											Driver.SessionLogger.WriteLine("Link");


												
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on MainForm_TemplateImportParametersLink...", Logger.MessageType.INF);
			Control FAMACTED_MainForm_TemplateImportParametersLink = new Control("MainForm_TemplateImportParametersLink", "ID", "lnk_1003373_FAMACTED_AUTOCRASSETEDIT_HDR");
			CPCommon.AssertEqual(true,FAMACTED_MainForm_TemplateImportParametersLink.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm_TemplateImportParametersLink);
FAMACTED_MainForm_TemplateImportParametersLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMACTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMACTED] Perfoming VerifyExists on TemplateImportParametersForm...", Logger.MessageType.INF);
			Control FAMACTED_TemplateImportParametersForm = new Control("TemplateImportParametersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMACTED_TEMPLATEIMPORT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMACTED_TemplateImportParametersForm.Exists());

												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_TemplateImportParametersForm);
formBttn = FAMACTED_TemplateImportParametersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "FAMACTED";
							CPCommon.WaitControlDisplayed(FAMACTED_MainForm);
formBttn = FAMACTED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

