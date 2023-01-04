 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMASSET_SMOKE : TestScript
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
new Control("Manage Asset Master Information", "xpath","//div[@class='navItem'][.='Manage Asset Master Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMASSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMASSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_AssetNo...", Logger.MessageType.INF);
			Control FAMASSET_Identification_AssetNo = new Control("Identification_AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm);
IWebElement formBttn = FAMASSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMASSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMASSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control FAMASSET_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMASSET_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm);
formBttn = FAMASSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMASSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMASSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming Select on MainForm_Tab...", Logger.MessageType.INF);
			Control FAMASSET_MainForm_Tab = new Control("MainForm_Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
IWebElement mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_DescInfo_Template_TemplateNo...", Logger.MessageType.INF);
			Control FAMASSET_Identification_DescInfo_Template_TemplateNo = new Control("Identification_DescInfo_Template_TemplateNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_TMPLT_ID']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_DescInfo_Template_TemplateNo.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_PurchInfo_PurchaseOrder_PODate...", Logger.MessageType.INF);
			Control FAMASSET_Identification_PurchInfo_PurchaseOrder_PODate = new Control("Identification_PurchInfo_PurchaseOrder_PODate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_DT']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_PurchInfo_PurchaseOrder_PODate.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_CostInfo_Units_Quantity...", Logger.MessageType.INF);
			Control FAMASSET_Identification_CostInfo_Units_Quantity = new Control("Identification_CostInfo_Units_Quantity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UNITS_QTY_NO']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_CostInfo_Units_Quantity.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Loc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_LocInfo_LocationGroupInfo_LocationGroup...", Logger.MessageType.INF);
			Control FAMASSET_Identification_LocInfo_LocationGroupInfo_LocationGroup = new Control("Identification_LocInfo_LocationGroupInfo_LocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_LOC_GRP_CD']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_LocInfo_LocationGroupInfo_LocationGroup.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Acct Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_AcctInfo_AssetAccount_AssetAccount...", Logger.MessageType.INF);
			Control FAMASSET_Identification_AcctInfo_AssetAccount_AssetAccount = new Control("Identification_AcctInfo_AssetAccount_AssetAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_AcctInfo_AssetAccount_AssetAccount.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "G/L Book Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_GLBookInfo_DepreciationInformation_DeprMethodCode...", Logger.MessageType.INF);
			Control FAMASSET_Identification_GLBookInfo_DepreciationInformation_DeprMethodCode = new Control("Identification_GLBookInfo_DepreciationInformation_DeprMethodCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B1_DEPR_MTHD_CD']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_GLBookInfo_DepreciationInformation_DeprMethodCode.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_GovtInfo_NatStockNo...", Logger.MessageType.INF);
			Control FAMASSET_Identification_GovtInfo_NatStockNo = new Control("Identification_GovtInfo_NatStockNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NSN_ID']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_GovtInfo_NatStockNo.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Disp Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_DispInfo_DisposalInfo_Date...", Logger.MessageType.INF);
			Control FAMASSET_Identification_DispInfo_DisposalInfo_Date = new Control("Identification_DispInfo_DisposalInfo_Date", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DISP_DT']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_DispInfo_DisposalInfo_Date.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm_Tab);
mTab = FAMASSET_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_Notes_Notes...", Logger.MessageType.INF);
			Control FAMASSET_Identification_Notes_Notes = new Control("Identification_Notes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,FAMASSET_Identification_Notes_Notes.Exists());

											Driver.SessionLogger.WriteLine("Link");


												
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_OtherBooksInfoLink...", Logger.MessageType.INF);
			Control FAMASSET_Identification_OtherBooksInfoLink = new Control("Identification_OtherBooksInfoLink", "ID", "lnk_1004505_FAMASSET_ASSET_HDR");
			CPCommon.AssertEqual(true,FAMASSET_Identification_OtherBooksInfoLink.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_Identification_OtherBooksInfoLink);
FAMASSET_Identification_OtherBooksInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on OtherBooksInfoForm...", Logger.MessageType.INF);
			Control FAMASSET_OtherBooksInfoForm = new Control("OtherBooksInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMASSET_ASSETOTHBKDEPR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMASSET_OtherBooksInfoForm.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_OtherBooksInfoForm);
formBttn = FAMASSET_OtherBooksInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_UserDefinedInfoLink...", Logger.MessageType.INF);
			Control FAMASSET_Identification_UserDefinedInfoLink = new Control("Identification_UserDefinedInfoLink", "ID", "lnk_1007547_FAMASSET_ASSET_HDR");
			CPCommon.AssertEqual(true,FAMASSET_Identification_UserDefinedInfoLink.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_Identification_UserDefinedInfoLink);
FAMASSET_Identification_UserDefinedInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control FAMASSET_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMASSET_UserDefinedInfoForm.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_UserDefinedInfoForm);
formBttn = FAMASSET_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_DisposalDetailsByBookLink...", Logger.MessageType.INF);
			Control FAMASSET_Identification_DisposalDetailsByBookLink = new Control("Identification_DisposalDetailsByBookLink", "ID", "lnk_1003781_FAMASSET_ASSET_HDR");
			CPCommon.AssertEqual(true,FAMASSET_Identification_DisposalDetailsByBookLink.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_Identification_DisposalDetailsByBookLink);
FAMASSET_Identification_DisposalDetailsByBookLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on DisposalDetailsByBookForm...", Logger.MessageType.INF);
			Control FAMASSET_DisposalDetailsByBookForm = new Control("DisposalDetailsByBookForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMASSET_ASSETBOOKSDISP_OTBK_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMASSET_DisposalDetailsByBookForm.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_DisposalDetailsByBookForm);
formBttn = FAMASSET_DisposalDetailsByBookForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on Identification_TemplateImportParametersLink...", Logger.MessageType.INF);
			Control FAMASSET_Identification_TemplateImportParametersLink = new Control("Identification_TemplateImportParametersLink", "ID", "lnk_1003774_FAMASSET_ASSET_HDR");
			CPCommon.AssertEqual(true,FAMASSET_Identification_TemplateImportParametersLink.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_Identification_TemplateImportParametersLink);
FAMASSET_Identification_TemplateImportParametersLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMASSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMASSET] Perfoming VerifyExists on TemplateImportParametersForm...", Logger.MessageType.INF);
			Control FAMASSET_TemplateImportParametersForm = new Control("TemplateImportParametersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMASSET_TEMPTINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMASSET_TemplateImportParametersForm.Exists());

												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_TemplateImportParametersForm);
formBttn = FAMASSET_TemplateImportParametersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "FAMASSET";
							CPCommon.WaitControlDisplayed(FAMASSET_MainForm);
formBttn = FAMASSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

