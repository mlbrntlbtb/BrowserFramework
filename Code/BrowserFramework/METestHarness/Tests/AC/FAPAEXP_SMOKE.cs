 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAPAEXP_SMOKE : TestScript
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
new Control("Fixed Assets Interfaces", "xpath","//div[@class='navItem'][.='Fixed Assets Interfaces']").Click();
new Control("Export Asset Records", "xpath","//div[@class='navItem'][.='Export Asset Records']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAPAEXP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_MainForm);
IWebElement formBttn = FAPAEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAPAEXP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAPAEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAPAEXP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_MainForm);
formBttn = FAPAEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAPAEXP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAPAEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control FAPAEXP_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_MainFormTab);
IWebElement mTab = FAPAEXP_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Asset Selection").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetSelection_AssetSelection_IncludeAssetChangesFor_Additions...", Logger.MessageType.INF);
			Control FAPAEXP_AssetSelection_AssetSelection_IncludeAssetChangesFor_Additions = new Control("AssetSelection_AssetSelection_IncludeAssetChangesFor_Additions", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INCL_ASSET_ADD_FL']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetSelection_AssetSelection_IncludeAssetChangesFor_Additions.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_MainFormTab);
mTab = FAPAEXP_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Export File Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on ExportFileDetails_Generate_GenerateDisposalRecords_GenerateDisposalRecords...", Logger.MessageType.INF);
			Control FAPAEXP_ExportFileDetails_Generate_GenerateDisposalRecords_GenerateDisposalRecords = new Control("ExportFileDetails_Generate_GenerateDisposalRecords_GenerateDisposalRecords", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='GEN_DISP_FL']");
			CPCommon.AssertEqual(true,FAPAEXP_ExportFileDetails_Generate_GenerateDisposalRecords_GenerateDisposalRecords.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetSelection_SelectedAssetMasterFieldsLink...", Logger.MessageType.INF);
			Control FAPAEXP_AssetSelection_SelectedAssetMasterFieldsLink = new Control("AssetSelection_SelectedAssetMasterFieldsLink", "ID", "lnk_15115_FAPAEXP_PARAM");
			CPCommon.AssertEqual(true,FAPAEXP_AssetSelection_SelectedAssetMasterFieldsLink.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetSelection_NonContiguousAssetListLink...", Logger.MessageType.INF);
			Control FAPAEXP_AssetSelection_NonContiguousAssetListLink = new Control("AssetSelection_NonContiguousAssetListLink", "ID", "lnk_15075_FAPAEXP_PARAM");
			CPCommon.AssertEqual(true,FAPAEXP_AssetSelection_NonContiguousAssetListLink.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetSelection_AssetMasterAndDisposalDataRecordsLink...", Logger.MessageType.INF);
			Control FAPAEXP_AssetSelection_AssetMasterAndDisposalDataRecordsLink = new Control("AssetSelection_AssetMasterAndDisposalDataRecordsLink", "ID", "lnk_15073_FAPAEXP_PARAM");
			CPCommon.AssertEqual(true,FAPAEXP_AssetSelection_AssetMasterAndDisposalDataRecordsLink.Exists());

											Driver.SessionLogger.WriteLine("Selected Asset Master Fields");


												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetSelection_SelectedAssetMasterFieldsLink);
FAPAEXP_AssetSelection_SelectedAssetMasterFieldsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on SelectedAssetMasterFields_AssetMasterFieldsForm...", Logger.MessageType.INF);
			Control FAPAEXP_SelectedAssetMasterFields_AssetMasterFieldsForm = new Control("SelectedAssetMasterFields_AssetMasterFieldsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_SFADBCOLDEF_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_SelectedAssetMasterFields_AssetMasterFieldsForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExist on SelectedAssetMasterFields_AssetMasterFieldsFormTable...", Logger.MessageType.INF);
			Control FAPAEXP_SelectedAssetMasterFields_AssetMasterFieldsFormTable = new Control("SelectedAssetMasterFields_AssetMasterFieldsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_SFADBCOLDEF_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_SelectedAssetMasterFields_AssetMasterFieldsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on SelectedAssetMasterFields_SelectedAssetMasterFieldsForm...", Logger.MessageType.INF);
			Control FAPAEXP_SelectedAssetMasterFields_SelectedAssetMasterFieldsForm = new Control("SelectedAssetMasterFields_SelectedAssetMasterFieldsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_XZFAPAEXPPARM_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_SelectedAssetMasterFields_SelectedAssetMasterFieldsForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExist on SelectedAssetMasterFields_SelectedAssetMasterFieldsFormTable...", Logger.MessageType.INF);
			Control FAPAEXP_SelectedAssetMasterFields_SelectedAssetMasterFieldsFormTable = new Control("SelectedAssetMasterFields_SelectedAssetMasterFieldsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_XZFAPAEXPPARM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_SelectedAssetMasterFields_SelectedAssetMasterFieldsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_SelectedAssetMasterFields_SelectedAssetMasterFieldsForm);
formBttn = FAPAEXP_SelectedAssetMasterFields_SelectedAssetMasterFieldsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Non-Contiguous Asset List");


												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_MainFormTab);
mTab = FAPAEXP_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Asset Selection").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetSelection_NonContiguousAssetListLink);
FAPAEXP_AssetSelection_NonContiguousAssetListLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on NonContiguousAssetList_NonContiguousAssetListForm...", Logger.MessageType.INF);
			Control FAPAEXP_NonContiguousAssetList_NonContiguousAssetListForm = new Control("NonContiguousAssetList_NonContiguousAssetListForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_NCR_ASSETID_ITEMNO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_NonContiguousAssetList_NonContiguousAssetListForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExist on NonContiguousAssetList_NonContiguousAssetListFormTable...", Logger.MessageType.INF);
			Control FAPAEXP_NonContiguousAssetList_NonContiguousAssetListFormTable = new Control("NonContiguousAssetList_NonContiguousAssetListFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_NCR_ASSETID_ITEMNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_NonContiguousAssetList_NonContiguousAssetListFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_NonContiguousAssetList_NonContiguousAssetListForm);
formBttn = FAPAEXP_NonContiguousAssetList_NonContiguousAssetListForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Asset Master and Disposal Data Records");


												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetSelection_AssetMasterAndDisposalDataRecordsLink);
FAPAEXP_AssetSelection_AssetMasterAndDisposalDataRecordsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecordsForm...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecordsForm = new Control("AssetMasterAndDisposalDataRecordsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_LN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecordsForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecords_BatchID...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_BatchID = new Control("AssetMasterAndDisposalDataRecords_BatchID", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_LN_']/ancestor::form[1]/descendant::*[@id='BATCH_ID']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_BatchID.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming Click on AssetMasterAndDisposalDataRecords_ModifyViewDataForExport...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_ModifyViewDataForExport = new Control("AssetMasterAndDisposalDataRecords_ModifyViewDataForExport", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPAEXP_LN_']/ancestor::form[1]/descendant::*[contains(@id,'AB_MODIFY') and contains(@style,'visible')]");
			CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecords_ModifyViewDataForExport);
if (FAPAEXP_AssetMasterAndDisposalDataRecords_ModifyViewDataForExport.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
FAPAEXP_AssetMasterAndDisposalDataRecords_ModifyViewDataForExport.Click(5,5);
else FAPAEXP_AssetMasterAndDisposalDataRecords_ModifyViewDataForExport.Click(4.5);


												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecords_AssetMasterDataForm...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataForm = new Control("AssetMasterAndDisposalDataRecords_AssetMasterDataForm", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK1_CTW_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExist on AssetMasterAndDisposalDataRecords_AssetMasterDataFormTable...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataFormTable = new Control("AssetMasterAndDisposalDataRecords_AssetMasterDataFormTable", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK1_CTW_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataForm);
formBttn = FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterDataForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecords_AssetMasterData...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData = new Control("AssetMasterAndDisposalDataRecords_AssetMasterData", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK1_CTW_')]/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData);
mTab = FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Asset Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecords_AssetMasterData_AssetInfo_AssetNo...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_AssetInfo_AssetNo = new Control("AssetMasterAndDisposalDataRecords_AssetMasterData_AssetInfo_AssetNo", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK1_CTW_')]/ancestor::form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_AssetInfo_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData);
mTab = FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "G/L Book Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData);
mTab = FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("Disposal Data");


												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm = new Control("AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK2_CTW_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExist on AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataFormTable...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataFormTable = new Control("AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataFormTable", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK2_CTW_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm);
formBttn = FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalDataForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAPAEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPAEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalData_AssetNo...", Logger.MessageType.INF);
			Control FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalData_AssetNo = new Control("AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalData_AssetNo", "xpath", "//div[starts-with(@id,'pr__FAPAEXP_XZFAPAEXPWRK2_CTW_')]/ancestor::form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAPAEXP_AssetMasterAndDisposalDataRecords_AssetMasterData_DisposalData_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_AssetMasterAndDisposalDataRecordsForm);
formBttn = FAPAEXP_AssetMasterAndDisposalDataRecordsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "FAPAEXP";
							CPCommon.WaitControlDisplayed(FAPAEXP_MainForm);
formBttn = FAPAEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

