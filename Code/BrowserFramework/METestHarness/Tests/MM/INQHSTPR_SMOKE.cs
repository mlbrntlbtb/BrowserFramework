 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INQHSTPR_SMOKE : TestScript
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
new Control("Inventory Reports/Inquiries", "xpath","//div[@class='navItem'][.='Inventory Reports/Inquiries']").Click();
new Control("View Inventory Transaction History", "xpath","//div[@class='navItem'][.='View Inventory Transaction History']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INQHSTPR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INQHSTPR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on MainForm_Part...", Logger.MessageType.INF);
			Control INQHSTPR_MainForm_Part = new Control("MainForm_Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,INQHSTPR_MainForm_Part.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							INQHSTPR_MainForm_Part.Click();
INQHSTPR_MainForm_Part.SendKeys("1260", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
INQHSTPR_MainForm_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control INQHSTPR_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INQHSTPR_INVTTRANLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INQHSTPR_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control INQHSTPR_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INQHSTPR_INVTTRANLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INQHSTPR_ChildForm);
IWebElement formBttn = INQHSTPR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INQHSTPR_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INQHSTPR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.AssertEqual(true,INQHSTPR_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control INQHSTPR_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__INQHSTPR_INVTTRANLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INQHSTPR_ChildForm_ChildFormTab);
IWebElement mTab = INQHSTPR_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Transaction Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on ChildForm_TransactionDetail_Transaction...", Logger.MessageType.INF);
			Control INQHSTPR_ChildForm_TransactionDetail_Transaction = new Control("ChildForm_TransactionDetail_Transaction", "xpath", "//div[translate(@id,'0123456789','')='pr__INQHSTPR_INVTTRANLN_CTW_']/ancestor::form[1]/descendant::*[@id='INVT_TRN_ID']");
			CPCommon.AssertEqual(true,INQHSTPR_ChildForm_TransactionDetail_Transaction.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_ChildForm_ChildFormTab);
mTab = INQHSTPR_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Elements").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on ChildForm_CostElements_DirectCostElements_Material...", Logger.MessageType.INF);
			Control INQHSTPR_ChildForm_CostElements_DirectCostElements_Material = new Control("ChildForm_CostElements_DirectCostElements_Material", "xpath", "//div[translate(@id,'0123456789','')='pr__INQHSTPR_INVTTRANLN_CTW_']/ancestor::form[1]/descendant::*[@id='MATL_CST_AMT']");
			CPCommon.AssertEqual(true,INQHSTPR_ChildForm_CostElements_DirectCostElements_Material.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_ChildForm_ChildFormTab);
mTab = INQHSTPR_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Usage Summary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on ChildForm_UsageSummary_YearToDateUsageQty...", Logger.MessageType.INF);
			Control INQHSTPR_ChildForm_UsageSummary_YearToDateUsageQty = new Control("ChildForm_UsageSummary_YearToDateUsageQty", "xpath", "//div[translate(@id,'0123456789','')='pr__INQHSTPR_INVTTRANLN_CTW_']/ancestor::form[1]/descendant::*[@id='USG_SMRY_QTY']");
			CPCommon.AssertEqual(true,INQHSTPR_ChildForm_UsageSummary_YearToDateUsageQty.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_ChildForm_ChildFormTab);
mTab = INQHSTPR_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("Serial Lot Info");


												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on ChildForm_SerialLotInformationLink...", Logger.MessageType.INF);
			Control INQHSTPR_ChildForm_SerialLotInformationLink = new Control("ChildForm_SerialLotInformationLink", "ID", "lnk_1007369_INQHSTPR_INVTTRANLN_CTW");
			CPCommon.AssertEqual(true,INQHSTPR_ChildForm_SerialLotInformationLink.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_ChildForm_SerialLotInformationLink);
INQHSTPR_ChildForm_SerialLotInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExist on SerialLotInfoFormTable...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInfoFormTable = new Control("SerialLotInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INQHSTPR_SerialLotInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming ClickButton on SerialLotInfoForm...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInfoForm = new Control("SerialLotInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INQHSTPR_SerialLotInfoForm);
formBttn = INQHSTPR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INQHSTPR_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INQHSTPR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.AssertEqual(true,INQHSTPR_SerialLotInfoForm.Exists());

													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on SerialLotInformation_SerialNumber...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInformation_SerialNumber = new Control("SerialLotInformation_SerialNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='SERIAL_ID']");
			CPCommon.AssertEqual(true,INQHSTPR_SerialLotInformation_SerialNumber.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming Select on SerialLotInformation_SerialLotInformationTab...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInformation_SerialLotInformationTab = new Control("SerialLotInformation_SerialLotInformationTab", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INQHSTPR_SerialLotInformation_SerialLotInformationTab);
mTab = INQHSTPR_SerialLotInformation_SerialLotInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Serial/Lot Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on SerialLotInformation_SerialLotDetails_Quantity...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInformation_SerialLotDetails_Quantity = new Control("SerialLotInformation_SerialLotDetails_Quantity", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='TOTAL_QTY']");
			CPCommon.AssertEqual(true,INQHSTPR_SerialLotInformation_SerialLotDetails_Quantity.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_SerialLotInformation_SerialLotInformationTab);
mTab = INQHSTPR_SerialLotInformation_SerialLotInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manufacturer/Vendor Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on SerialLotInformation_ManufacturerVendorInformation_ManufacturerVendorSerialNumber...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInformation_ManufacturerVendorInformation_ManufacturerVendorSerialNumber = new Control("SerialLotInformation_ManufacturerVendorInformation_ManufacturerVendorSerialNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='MAN_VEND_SERIAL_ID']");
			CPCommon.AssertEqual(true,INQHSTPR_SerialLotInformation_ManufacturerVendorInformation_ManufacturerVendorSerialNumber.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_SerialLotInformation_SerialLotInformationTab);
mTab = INQHSTPR_SerialLotInformation_SerialLotInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Maintenance/Warranty Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on SerialLotInformation_MaintenanceWarrantyInformation_Maintenance_SalesOrder...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInformation_MaintenanceWarrantyInformation_Maintenance_SalesOrder = new Control("SerialLotInformation_MaintenanceWarrantyInformation_Maintenance_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='MAINT_SO_ID']");
			CPCommon.AssertEqual(true,INQHSTPR_SerialLotInformation_MaintenanceWarrantyInformation_Maintenance_SalesOrder.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_SerialLotInformation_SerialLotInformationTab);
mTab = INQHSTPR_SerialLotInformation_SerialLotInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQHSTPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQHSTPR] Perfoming VerifyExists on SerialLotInformation_Notes_Notes...", Logger.MessageType.INF);
			Control INQHSTPR_SerialLotInformation_Notes_Notes = new Control("SerialLotInformation_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_SERIALLOT_SRLT_']/ancestor::form[1]/descendant::*[@id='NOTES_NT']");
			CPCommon.AssertEqual(true,INQHSTPR_SerialLotInformation_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_SerialLotInfoForm);
formBttn = INQHSTPR_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "INQHSTPR";
							CPCommon.WaitControlDisplayed(INQHSTPR_MainForm);
formBttn = INQHSTPR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

