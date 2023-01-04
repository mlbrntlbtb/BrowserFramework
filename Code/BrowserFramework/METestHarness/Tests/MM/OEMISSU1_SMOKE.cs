 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMISSU1_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Issues", "xpath","//div[@class='navItem'][.='Issues']").Click();
new Control("Manage Sales Order Inventory Issues", "xpath","//div[@class='navItem'][.='Manage Sales Order Inventory Issues']").Click();


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMISSU1_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMISSU1_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on Warehouse...", Logger.MessageType.INF);
			Control OEMISSU1_Warehouse = new Control("Warehouse", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,OEMISSU1_Warehouse.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_MainForm);
IWebElement formBttn = OEMISSU1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMISSU1_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMISSU1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control OEMISSU1_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU1_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control OEMISSU1_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU1_ChildForm);
formBttn = OEMISSU1_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU1_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU1_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.AssertEqual(true,OEMISSU1_ChildForm.Exists());

												Driver.SessionLogger.WriteLine("ACCOUNTINGPERIOD");


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming Click on AccountingPeriodLink...", Logger.MessageType.INF);
			Control OEMISSU1_AccountingPeriodLink = new Control("AccountingPeriodLink", "ID", "lnk_3243_OEMISSU_SOISSUEHDR_SALESISSUE");
			CPCommon.WaitControlDisplayed(OEMISSU1_AccountingPeriodLink);
OEMISSU1_AccountingPeriodLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on AccountingPeriodForm...", Logger.MessageType.INF);
			Control OEMISSU1_AccountingPeriodForm = new Control("AccountingPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMISSU1_AccountingPeriodForm.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on AccountingPeriod_FiscalYear...", Logger.MessageType.INF);
			Control OEMISSU1_AccountingPeriod_FiscalYear = new Control("AccountingPeriod_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='DFS_FYCD_ST']");
			CPCommon.AssertEqual(true,OEMISSU1_AccountingPeriod_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_AccountingPeriodForm);
formBttn = OEMISSU1_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMISSU1_AccountingPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMISSU1_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_AccountingPeriodForm);
formBttn = OEMISSU1_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("HEADERDOCUMENTS");


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming Click on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control OEMISSU1_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007711_OEMISSU_SOISSUEHDR_SALESISSUE");
			CPCommon.WaitControlDisplayed(OEMISSU1_HeaderDocumentsLink);
OEMISSU1_HeaderDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExist on HeaderDocumentsTable...", Logger.MessageType.INF);
			Control OEMISSU1_HeaderDocumentsTable = new Control("HeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU1_HeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming ClickButton on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control OEMISSU1_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU1_HeaderDocumentsForm);
formBttn = OEMISSU1_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU1_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU1_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.AssertEqual(true,OEMISSU1_HeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on HeaderDocuments_Document...", Logger.MessageType.INF);
			Control OEMISSU1_HeaderDocuments_Document = new Control("HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMISSU1_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_HeaderDocumentsForm);
formBttn = OEMISSU1_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SERIAL/LOTINFO");


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming Click on SerialLotInfoLink...", Logger.MessageType.INF);
			Control OEMISSU1_SerialLotInfoLink = new Control("SerialLotInfoLink", "ID", "lnk_1007510_OEMISSU_WORKTABLE_TEMP");
			CPCommon.WaitControlDisplayed(OEMISSU1_SerialLotInfoLink);
OEMISSU1_SerialLotInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExist on SerialLotInfoTable...", Logger.MessageType.INF);
			Control OEMISSU1_SerialLotInfoTable = new Control("SerialLotInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU1_SerialLotInfoTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming ClickButton on SerialLotInfoForm...", Logger.MessageType.INF);
			Control OEMISSU1_SerialLotInfoForm = new Control("SerialLotInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU1_SerialLotInfoForm);
formBttn = OEMISSU1_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU1_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU1_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.AssertEqual(true,OEMISSU1_SerialLotInfoForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on SerialLotInfo_LotNumber...", Logger.MessageType.INF);
			Control OEMISSU1_SerialLotInfo_LotNumber = new Control("SerialLotInfo_LotNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='LOT_ID']");
			CPCommon.AssertEqual(true,OEMISSU1_SerialLotInfo_LotNumber.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming Select on SerialLotInfoTab...", Logger.MessageType.INF);
			Control OEMISSU1_SerialLotInfoTab = new Control("SerialLotInfoTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMISSU1_SerialLotInfoTab);
IWebElement mTab = OEMISSU1_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shelf Life").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType...", Logger.MessageType.INF);
			Control OEMISSU1_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType = new Control("ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='S_SHELF_LIFE_TYPE']");
			CPCommon.AssertEqual(true,OEMISSU1_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_SerialLotInfoForm);
formBttn = OEMISSU1_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LINEDOCUMENTS");


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming Click on LineDocumentsLink...", Logger.MessageType.INF);
			Control OEMISSU1_LineDocumentsLink = new Control("LineDocumentsLink", "ID", "lnk_1007712_OEMISSU_WORKTABLE_TEMP");
			CPCommon.WaitControlDisplayed(OEMISSU1_LineDocumentsLink);
OEMISSU1_LineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExist on LineDocumentsTable...", Logger.MessageType.INF);
			Control OEMISSU1_LineDocumentsTable = new Control("LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU1_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming ClickButton on LineDocumentsForm...", Logger.MessageType.INF);
			Control OEMISSU1_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU1_LineDocumentsForm);
formBttn = OEMISSU1_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU1_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU1_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.AssertEqual(true,OEMISSU1_LineDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU1] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control OEMISSU1_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMISSU1_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_LineDocumentsForm);
formBttn = OEMISSU1_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "OEMISSU1";
							CPCommon.WaitControlDisplayed(OEMISSU1_MainForm);
formBttn = OEMISSU1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

