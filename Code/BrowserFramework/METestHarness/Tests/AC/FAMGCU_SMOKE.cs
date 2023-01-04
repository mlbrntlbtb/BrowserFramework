 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMGCU_SMOKE : TestScript
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
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("Manage Asset Master User-Defined Global Changes", "xpath","//div[@class='navItem'][.='Manage Asset Master User-Defined Global Changes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMGCU_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMGCU_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMGCU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMGCU_MainForm);
IWebElement formBttn = FAMGCU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMGCU_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMGCU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMGCU_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMGCU_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control FAMGCU_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
IWebElement mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "UDF Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on UDFInfo_DataType...", Logger.MessageType.INF);
			Control FAMGCU_UDFInfo_DataType = new Control("UDFInfo_DataType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,FAMGCU_UDFInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on DescInfo_Description_ShortDesc...", Logger.MessageType.INF);
			Control FAMGCU_DescInfo_Description_ShortDesc = new Control("DescInfo_Description_ShortDesc", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SHORT_DESC']");
			CPCommon.AssertEqual(true,FAMGCU_DescInfo_Description_ShortDesc.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on PurchInfo_VendorInfo_VendorID...", Logger.MessageType.INF);
			Control FAMGCU_PurchInfo_VendorInfo_VendorID = new Control("PurchInfo_VendorInfo_VendorID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,FAMGCU_PurchInfo_VendorInfo_VendorID.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Loc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on LocInfo_LocationGroupInfo_LocationGroup...", Logger.MessageType.INF);
			Control FAMGCU_LocInfo_LocationGroupInfo_LocationGroup = new Control("LocInfo_LocationGroupInfo_LocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_LOC_GRP_CD']");
			CPCommon.AssertEqual(true,FAMGCU_LocInfo_LocationGroupInfo_LocationGroup.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Acct Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on AcctInfo_AccountInformation_AssetAccount...", Logger.MessageType.INF);
			Control FAMGCU_AcctInfo_AccountInformation_AssetAccount = new Control("AcctInfo_AccountInformation_AssetAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ACCT_ID']");
			CPCommon.AssertEqual(true,FAMGCU_AcctInfo_AccountInformation_AssetAccount.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on CostInfo_Units_Quantity...", Logger.MessageType.INF);
			Control FAMGCU_CostInfo_Units_Quantity = new Control("CostInfo_Units_Quantity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UNITS_QTY_NO']");
			CPCommon.AssertEqual(true,FAMGCU_CostInfo_Units_Quantity.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "G/L Book Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on GLBookInfo_SystemCalculations_AutoCalculateDepreciation...", Logger.MessageType.INF);
			Control FAMGCU_GLBookInfo_SystemCalculations_AutoCalculateDepreciation = new Control("GLBookInfo_SystemCalculations_AutoCalculateDepreciation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B1_AUTO_CALC_FL']");
			CPCommon.AssertEqual(true,FAMGCU_GLBookInfo_SystemCalculations_AutoCalculateDepreciation.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Disp Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on DispInfo_Disposal_Date...", Logger.MessageType.INF);
			Control FAMGCU_DispInfo_Disposal_Date = new Control("DispInfo_Disposal_Date", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DISP_DT']");
			CPCommon.AssertEqual(true,FAMGCU_DispInfo_Disposal_Date.Exists());

												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainFormTab);
mTab = FAMGCU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCU] Perfoming VerifyExists on GovtInfo_NatStockNo...", Logger.MessageType.INF);
			Control FAMGCU_GovtInfo_NatStockNo = new Control("GovtInfo_NatStockNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NSN_ID']");
			CPCommon.AssertEqual(true,FAMGCU_GovtInfo_NatStockNo.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMGCU";
							CPCommon.WaitControlDisplayed(FAMGCU_MainForm);
formBttn = FAMGCU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

