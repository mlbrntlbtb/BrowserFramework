 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMGCTU_SMOKE : TestScript
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
new Control("Manage Template User-Defined Global Changes", "xpath","//div[@class='navItem'][.='Manage Template User-Defined Global Changes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMGCTU_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMGCTU_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMGCTU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMGCTU_MainForm);
IWebElement formBttn = FAMGCTU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMGCTU_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMGCTU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on TemplateNo...", Logger.MessageType.INF);
			Control FAMGCTU_TemplateNo = new Control("TemplateNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_TMPLT_ID']");
			CPCommon.AssertEqual(true,FAMGCTU_TemplateNo.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control FAMGCTU_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
IWebElement mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "UDF Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on UDFInfo_DataType...", Logger.MessageType.INF);
			Control FAMGCTU_UDFInfo_DataType = new Control("UDFInfo_DataType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,FAMGCTU_UDFInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on DescInfo_Description_ShortDesc...", Logger.MessageType.INF);
			Control FAMGCTU_DescInfo_Description_ShortDesc = new Control("DescInfo_Description_ShortDesc", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SHORT_DESC']");
			CPCommon.AssertEqual(true,FAMGCTU_DescInfo_Description_ShortDesc.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on PurchInfo_VendorInfo_VendorID...", Logger.MessageType.INF);
			Control FAMGCTU_PurchInfo_VendorInfo_VendorID = new Control("PurchInfo_VendorInfo_VendorID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,FAMGCTU_PurchInfo_VendorInfo_VendorID.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Loc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on LocInfo_LocationGroupInfo_LocationGroup...", Logger.MessageType.INF);
			Control FAMGCTU_LocInfo_LocationGroupInfo_LocationGroup = new Control("LocInfo_LocationGroupInfo_LocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_LOC_GRP_CD']");
			CPCommon.AssertEqual(true,FAMGCTU_LocInfo_LocationGroupInfo_LocationGroup.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Acct Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on AcctInfo_AssetAccount_AssetAccount...", Logger.MessageType.INF);
			Control FAMGCTU_AcctInfo_AssetAccount_AssetAccount = new Control("AcctInfo_AssetAccount_AssetAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ACCT_ID']");
			CPCommon.AssertEqual(true,FAMGCTU_AcctInfo_AssetAccount_AssetAccount.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "G/L Book Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on GLBookInfo_SystemCalculations_AutoCalculate...", Logger.MessageType.INF);
			Control FAMGCTU_GLBookInfo_SystemCalculations_AutoCalculate = new Control("GLBookInfo_SystemCalculations_AutoCalculate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B1_AUTO_CALC_FL']");
			CPCommon.AssertEqual(true,FAMGCTU_GLBookInfo_SystemCalculations_AutoCalculate.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on GovtInfo_NatStockNo...", Logger.MessageType.INF);
			Control FAMGCTU_GovtInfo_NatStockNo = new Control("GovtInfo_NatStockNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NSN_ID']");
			CPCommon.AssertEqual(true,FAMGCTU_GovtInfo_NatStockNo.Exists());

												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainFormTab);
mTab = FAMGCTU_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMGCTU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMGCTU] Perfoming VerifyExists on Notes_Notes...", Logger.MessageType.INF);
			Control FAMGCTU_Notes_Notes = new Control("Notes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,FAMGCTU_Notes_Notes.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMGCTU";
							CPCommon.WaitControlDisplayed(FAMGCTU_MainForm);
formBttn = FAMGCTU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

