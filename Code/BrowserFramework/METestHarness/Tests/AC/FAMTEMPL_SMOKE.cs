 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMTEMPL_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Asset Master Records", "xpath","//div[@class='navItem'][.='Asset Master Records']").Click();
new Control("Manage Asset Template Information", "xpath","//div[@class='navItem'][.='Manage Asset Template Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMTEMPL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMTEMPL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on TemplateNo...", Logger.MessageType.INF);
			Control FAMTEMPL_TemplateNo = new Control("TemplateNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_TMPLT_ID']");
			CPCommon.AssertEqual(true,FAMTEMPL_TemplateNo.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control FAMTEMPL_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,FAMTEMPL_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
IWebElement mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on DescInfo_Description_ShortDesc...", Logger.MessageType.INF);
			Control FAMTEMPL_DescInfo_Description_ShortDesc = new Control("DescInfo_Description_ShortDesc", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SHORT_DESC']");
			CPCommon.AssertEqual(true,FAMTEMPL_DescInfo_Description_ShortDesc.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on PurchInfo_VendorInfo_Vendor...", Logger.MessageType.INF);
			Control FAMTEMPL_PurchInfo_VendorInfo_Vendor = new Control("PurchInfo_VendorInfo_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,FAMTEMPL_PurchInfo_VendorInfo_Vendor.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Loc Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on LocInfo_LocationGroupInfo_LocationGroup...", Logger.MessageType.INF);
			Control FAMTEMPL_LocInfo_LocationGroupInfo_LocationGroup = new Control("LocInfo_LocationGroupInfo_LocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_LOC_GRP_CD']");
			CPCommon.AssertEqual(true,FAMTEMPL_LocInfo_LocationGroupInfo_LocationGroup.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Acct Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on AcctInfo_AssetAccount_Project...", Logger.MessageType.INF);
			Control FAMTEMPL_AcctInfo_AssetAccount_Project = new Control("AcctInfo_AssetAccount_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,FAMTEMPL_AcctInfo_AssetAccount_Project.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "G/L Book Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on GLBookInfo_Salvage_Percent...", Logger.MessageType.INF);
			Control FAMTEMPL_GLBookInfo_Salvage_Percent = new Control("GLBookInfo_Salvage_Percent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B1_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMTEMPL_GLBookInfo_Salvage_Percent.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on GovtInfo_NatStockNo...", Logger.MessageType.INF);
			Control FAMTEMPL_GovtInfo_NatStockNo = new Control("GovtInfo_NatStockNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NSN_ID']");
			CPCommon.AssertEqual(true,FAMTEMPL_GovtInfo_NatStockNo.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainFormTab);
mTab = FAMTEMPL_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on Notes_Notes...", Logger.MessageType.INF);
			Control FAMTEMPL_Notes_Notes = new Control("Notes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,FAMTEMPL_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on OtherBooksInfoLink...", Logger.MessageType.INF);
			Control FAMTEMPL_OtherBooksInfoLink = new Control("OtherBooksInfoLink", "ID", "lnk_1007454_FAMTEMPL_FATMPLTKEY_HDR");
			CPCommon.AssertEqual(true,FAMTEMPL_OtherBooksInfoLink.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_OtherBooksInfoLink);
FAMTEMPL_OtherBooksInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExist on OtherBooksInfoFormTable...", Logger.MessageType.INF);
			Control FAMTEMPL_OtherBooksInfoFormTable = new Control("OtherBooksInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMTEMPL_FATEMPLATEOTHBK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMTEMPL_OtherBooksInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming ClickButton on OtherBooksInfoForm...", Logger.MessageType.INF);
			Control FAMTEMPL_OtherBooksInfoForm = new Control("OtherBooksInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMTEMPL_FATEMPLATEOTHBK_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMTEMPL_OtherBooksInfoForm);
IWebElement formBttn = FAMTEMPL_OtherBooksInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMTEMPL_OtherBooksInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMTEMPL_OtherBooksInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.AssertEqual(true,FAMTEMPL_OtherBooksInfoForm.Exists());

													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on OtherBooksInfo_Tax_DeprMethodCode...", Logger.MessageType.INF);
			Control FAMTEMPL_OtherBooksInfo_Tax_DeprMethodCode = new Control("OtherBooksInfo_Tax_DeprMethodCode", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMTEMPL_FATEMPLATEOTHBK_']/ancestor::form[1]/descendant::*[@id='B2_DEPR_MTHD_CD']");
			CPCommon.AssertEqual(true,FAMTEMPL_OtherBooksInfo_Tax_DeprMethodCode.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_OtherBooksInfoForm);
formBttn = FAMTEMPL_OtherBooksInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming Click on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control FAMTEMPL_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_1007524_FAMTEMPL_FATMPLTKEY_HDR");
			CPCommon.WaitControlDisplayed(FAMTEMPL_UserDefinedInfoLink);
FAMTEMPL_UserDefinedInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExist on UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control FAMTEMPL_UserDefinedInfoFormTable = new Control("UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMTEMPL_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control FAMTEMPL_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMTEMPL_UserDefinedInfoForm);
formBttn = FAMTEMPL_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMTEMPL_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMTEMPL_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMTEMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTEMPL] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control FAMTEMPL_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,FAMTEMPL_UserDefinedInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_UserDefinedInfoForm);
formBttn = FAMTEMPL_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "FAMTEMPL";
							CPCommon.WaitControlDisplayed(FAMTEMPL_MainForm);
formBttn = FAMTEMPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

