 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMSRLT_SMOKE : TestScript
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
new Control("Inventory Utilities", "xpath","//div[@class='navItem'][.='Inventory Utilities']").Click();
new Control("Manage Serial/Lot Information", "xpath","//div[@class='navItem'][.='Manage Serial/Lot Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMSRLT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMSRLT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control INMSRLT_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,INMSRLT_Part.Exists());

											Driver.SessionLogger.WriteLine("SET DATA");


												
				CPCommon.CurrentComponent = "INMSRLT";
							INMSRLT_Part.Click();
INMSRLT_Part.SendKeys("047569071256", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
INMSRLT_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("CHILD FORM TABLE");


												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExist on SerialLotMaintenanceDetailsTable...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetailsTable = new Control("SerialLotMaintenanceDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming ClickButton on SerialLotMaintenanceDetailsForm...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetailsForm = new Control("SerialLotMaintenanceDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsForm);
IWebElement formBttn = INMSRLT_SerialLotMaintenanceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMSRLT_SerialLotMaintenanceDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMSRLT_SerialLotMaintenanceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetailsForm.Exists());

												Driver.SessionLogger.WriteLine("TAB");


												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_BasicInformation_SerialNumber...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_BasicInformation_SerialNumber = new Control("SerialLotMaintenanceDetails_BasicInformation_SerialNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='SERIAL_ID']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_BasicInformation_SerialNumber.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming Select on SerialLotMaintenanceDetailsTab...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetailsTab = new Control("SerialLotMaintenanceDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsTab);
IWebElement mTab = INMSRLT_SerialLotMaintenanceDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Serial/Lot Origin").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_SerialLotOrigin_OriginalOrder...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_SerialLotOrigin_OriginalOrder = new Control("SerialLotMaintenanceDetails_SerialLotOrigin_OriginalOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='ORIG_ORD_ID']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_SerialLotOrigin_OriginalOrder.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsTab);
mTab = INMSRLT_SerialLotMaintenanceDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manuf/Vend Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_ManufVendInformation_Manufacturer...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_ManufVendInformation_Manufacturer = new Control("SerialLotMaintenanceDetails_ManufVendInformation_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_ManufVendInformation_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsTab);
mTab = INMSRLT_SerialLotMaintenanceDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Warranties").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_Warranties_VendorWarranty...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_Warranties_VendorWarranty = new Control("SerialLotMaintenanceDetails_Warranties_VendorWarranty", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='VEND_WARR_CD']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_Warranties_VendorWarranty.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsTab);
mTab = INMSRLT_SerialLotMaintenanceDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "UID").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_UID_UIDDetails_UID...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_UID_UIDDetails_UID = new Control("SerialLotMaintenanceDetails_UID_UIDDetails_UID", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='UID_CD']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_UID_UIDDetails_UID.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsTab);
mTab = INMSRLT_SerialLotMaintenanceDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_Notes_Notes...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_Notes_Notes = new Control("SerialLotMaintenanceDetails_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='NOTES_NT']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.WaitControlDisplayed(INMSRLT_SerialLotMaintenanceDetailsTab);
mTab = INMSRLT_SerialLotMaintenanceDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shelf Life").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INMSRLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSRLT] Perfoming VerifyExists on SerialLotMaintenanceDetails_ShelfLife_ShelfLife_LastExtensionDate...", Logger.MessageType.INF);
			Control INMSRLT_SerialLotMaintenanceDetails_ShelfLife_ShelfLife_LastExtensionDate = new Control("SerialLotMaintenanceDetails_ShelfLife_ShelfLife_LastExtensionDate", "xpath", "//div[translate(@id,'0123456789','')='pr__INMSRLT_SERIALLOT_SERILOTMAINT_']/ancestor::form[1]/descendant::*[@id='LAST_EXT_DT']");
			CPCommon.AssertEqual(true,INMSRLT_SerialLotMaintenanceDetails_ShelfLife_ShelfLife_LastExtensionDate.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "INMSRLT";
							CPCommon.WaitControlDisplayed(INMSRLT_MainForm);
formBttn = INMSRLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

