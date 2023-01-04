 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMPAXFR_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("check main form ");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Transfers", "xpath","//div[@class='navItem'][.='Transfers']").Click();
new Control("Enter Inventory Transfers", "xpath","//div[@class='navItem'][.='Enter Inventory Transfers']").Click();


												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMPAXFR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMPAXFR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on Identification_TransferID...", Logger.MessageType.INF);
			Control INMPAXFR_Identification_TransferID = new Control("Identification_TransferID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVT_TRN_ID_LNK']");
			CPCommon.AssertEqual(true,INMPAXFR_Identification_TransferID.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_MainForm);
IWebElement formBttn = INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INMPAXFR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INMPAXFR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPAXFR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_MainForm);
formBttn = INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPAXFR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_MainForm.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_Identification_TransferID.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_MainForm);
formBttn = INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? INMPAXFR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("check accounting period");


												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming Click on AccountingPeriodLink...", Logger.MessageType.INF);
			Control INMPAXFR_AccountingPeriodLink = new Control("AccountingPeriodLink", "ID", "lnk_1007990_INMPAXFR_INVTTRN_HDR");
			CPCommon.WaitControlDisplayed(INMPAXFR_AccountingPeriodLink);
INMPAXFR_AccountingPeriodLink.Click(1.5);


												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on AccountingPeriodForm...", Logger.MessageType.INF);
			Control INMPAXFR_AccountingPeriodForm = new Control("AccountingPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMPAXFR_AccountingPeriodForm.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on AccountingPeriod_FiscalYear...", Logger.MessageType.INF);
			Control INMPAXFR_AccountingPeriod_FiscalYear = new Control("AccountingPeriod_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='DFS_FYCD_ST']");
			CPCommon.AssertEqual(true,INMPAXFR_AccountingPeriod_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_AccountingPeriodForm);
formBttn = INMPAXFR_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INMPAXFR_AccountingPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INMPAXFR_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExist on AccountingPeriodFormTable...", Logger.MessageType.INF);
			Control INMPAXFR_AccountingPeriodFormTable = new Control("AccountingPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPAXFR_AccountingPeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_AccountingPeriodForm);
formBttn = INMPAXFR_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPAXFR_AccountingPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPAXFR_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_AccountingPeriodForm.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_AccountingPeriod_FiscalYear.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming Click on AccountingPeriod_OK...", Logger.MessageType.INF);
			Control INMPAXFR_AccountingPeriod_OK = new Control("AccountingPeriod_OK", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(INMPAXFR_AccountingPeriod_OK);
if (INMPAXFR_AccountingPeriod_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
INMPAXFR_AccountingPeriod_OK.Click(5,5);
else INMPAXFR_AccountingPeriod_OK.Click(4.5);


											Driver.SessionLogger.WriteLine("check child form");


												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control INMPAXFR_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPAXFR_INVTTRNLN_CHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPAXFR_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control INMPAXFR_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPAXFR_INVTTRNLN_CHD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INMPAXFR_ChildForm);
formBttn = INMPAXFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPAXFR_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPAXFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on ChildForm_Line...", Logger.MessageType.INF);
			Control INMPAXFR_ChildForm_Line = new Control("ChildForm_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPAXFR_INVTTRNLN_CHD_']/ancestor::form[1]/descendant::*[@id='INVT_TRN_LN_NO']");
			CPCommon.AssertEqual(true,INMPAXFR_ChildForm_Line.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_ChildForm);
formBttn = INMPAXFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INMPAXFR_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INMPAXFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_ChildFormTable.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_ChildForm);
formBttn = INMPAXFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPAXFR_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPAXFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Check serial lot information");


												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming Click on ChildForm_SerialLotInfoLink...", Logger.MessageType.INF);
			Control INMPAXFR_ChildForm_SerialLotInfoLink = new Control("ChildForm_SerialLotInfoLink", "ID", "lnk_1008078_INMPAXFR_INVTTRNLN_CHD");
			CPCommon.WaitControlDisplayed(INMPAXFR_ChildForm_SerialLotInfoLink);
INMPAXFR_ChildForm_SerialLotInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExist on SerialLotInfoFormTable...", Logger.MessageType.INF);
			Control INMPAXFR_SerialLotInfoFormTable = new Control("SerialLotInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPAXFR_SerialLotInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming ClickButton on SerialLotInfoForm...", Logger.MessageType.INF);
			Control INMPAXFR_SerialLotInfoForm = new Control("SerialLotInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INMPAXFR_SerialLotInfoForm);
formBttn = INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_SerialLotInfoForm.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on SerialLotInfo_LotNumber...", Logger.MessageType.INF);
			Control INMPAXFR_SerialLotInfo_LotNumber = new Control("SerialLotInfo_LotNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_']/ancestor::form[1]/descendant::*[@id='LOT_ID']");
			CPCommon.AssertEqual(true,INMPAXFR_SerialLotInfo_LotNumber.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPAXFR] Perfoming VerifyExists on SerialLotInfo_BasicInformation_Origin_Order...", Logger.MessageType.INF);
			Control INMPAXFR_SerialLotInfo_BasicInformation_Origin_Order = new Control("SerialLotInfo_BasicInformation_Origin_Order", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_']/ancestor::form[1]/descendant::*[@id='ORIG_ORD_ID']");
			CPCommon.AssertEqual(true,INMPAXFR_SerialLotInfo_BasicInformation_Origin_Order.Exists());

												
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_SerialLotInfoForm);
formBttn = INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.AssertEqual(true,INMPAXFR_SerialLotInfoFormTable.Exists());

													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_SerialLotInfoForm);
formBttn = INMPAXFR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "INMPAXFR";
							CPCommon.WaitControlDisplayed(INMPAXFR_MainForm);
formBttn = INMPAXFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

