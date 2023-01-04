 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEMRFQS_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Request for Quotes", "xpath","//div[@class='navItem'][.='Request for Quotes']").Click();
new Control("Select Request for Quotes from Proposals", "xpath","//div[@class='navItem'][.='Select Request for Quotes from Proposals']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEMRFQS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on Proposal...", Logger.MessageType.INF);
			Control MEMRFQS_Proposal = new Control("Proposal", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMRFQS_Proposal.Exists());

											Driver.SessionLogger.WriteLine("Query");


												
				CPCommon.CurrentComponent = "MEMRFQS";
							MEMRFQS_Proposal.Click();
MEMRFQS_Proposal.SendKeys("10000000000000000001", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
MEMRFQS_Proposal.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Select Items");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExist on SelectItems_SelectItemsTable...", Logger.MessageType.INF);
			Control MEMRFQS_SelectItems_SelectItemsTable = new Control("SelectItems_SelectItemsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_PROPITEMDTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMRFQS_SelectItems_SelectItemsTable.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on SelectItemsform...", Logger.MessageType.INF);
			Control MEMRFQS_SelectItemsform = new Control("SelectItemsform", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_PROPITEMDTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_SelectItemsform.Exists());

											Driver.SessionLogger.WriteLine("Item Info");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on SelectItems_ItemInfoLink...", Logger.MessageType.INF);
			Control MEMRFQS_SelectItems_ItemInfoLink = new Control("SelectItems_ItemInfoLink", "ID", "lnk_5340_MEMRFQS_PROPITEMDTL");
			CPCommon.AssertEqual(true,MEMRFQS_SelectItems_ItemInfoLink.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_SelectItems_ItemInfoLink);
MEMRFQS_SelectItems_ItemInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on ItemInfoform...", Logger.MessageType.INF);
			Control MEMRFQS_ItemInfoform = new Control("ItemInfoform", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_ITEMINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_ItemInfoform.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on ItemInfo_Item...", Logger.MessageType.INF);
			Control MEMRFQS_ItemInfo_Item = new Control("ItemInfo_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_ITEMINFO_']/ancestor::form[1]/descendant::*[@id='PROP_ITEM_ID']");
			CPCommon.AssertEqual(true,MEMRFQS_ItemInfo_Item.Exists());

											Driver.SessionLogger.WriteLine("Assigned Vendors");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExist on ItemInfo_AssignedVendorsFormTable...", Logger.MessageType.INF);
			Control MEMRFQS_ItemInfo_AssignedVendorsFormTable = new Control("ItemInfo_AssignedVendorsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_ITEMVEND_ASSIGNVEND_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMRFQS_ItemInfo_AssignedVendorsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on ItemInfo_AssignedVendorsform...", Logger.MessageType.INF);
			Control MEMRFQS_ItemInfo_AssignedVendorsform = new Control("ItemInfo_AssignedVendorsform", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_ITEMVEND_ASSIGNVEND_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_ItemInfo_AssignedVendorsform.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_ItemInfo_AssignedVendorsform);
IWebElement formBttn = MEMRFQS_ItemInfo_AssignedVendorsform.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMRFQS_ItemInfo_AssignedVendorsform.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMRFQS_ItemInfo_AssignedVendorsform.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on ItemInfo_AssignedVendors_Vendor...", Logger.MessageType.INF);
			Control MEMRFQS_ItemInfo_AssignedVendors_Vendor = new Control("ItemInfo_AssignedVendors_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_ITEMVEND_ASSIGNVEND_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,MEMRFQS_ItemInfo_AssignedVendors_Vendor.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_ItemInfoform);
formBttn = MEMRFQS_ItemInfoform.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SelectVendors");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExist on SelectVendorsFormTable...", Logger.MessageType.INF);
			Control MEMRFQS_SelectVendorsFormTable = new Control("SelectVendorsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VEND_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMRFQS_SelectVendorsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on SelectVendorsForm...", Logger.MessageType.INF);
			Control MEMRFQS_SelectVendorsForm = new Control("SelectVendorsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VEND_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_SelectVendorsForm.Exists());

											Driver.SessionLogger.WriteLine("VendorInfo");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on SelectVendors_VendorInfoLink...", Logger.MessageType.INF);
			Control MEMRFQS_SelectVendors_VendorInfoLink = new Control("SelectVendors_VendorInfoLink", "ID", "lnk_5361_MEMRFQS_VEND");
			CPCommon.AssertEqual(true,MEMRFQS_SelectVendors_VendorInfoLink.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_SelectVendors_VendorInfoLink);
MEMRFQS_SelectVendors_VendorInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on VendorInfoForm...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfoForm = new Control("VendorInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfoForm.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on VendorInfo_Vendor...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfo_Vendor = new Control("VendorInfo_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDINFO_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfo_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Order Addresses");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExist on VendorInfo_OrderAddressesFormTable...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfo_OrderAddressesFormTable = new Control("VendorInfo_OrderAddressesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDADDR_ORDADDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfo_OrderAddressesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on VendorInfo_OrderAddressesForm...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfo_OrderAddressesForm = new Control("VendorInfo_OrderAddressesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDADDR_ORDADDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfo_OrderAddressesForm.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_VendorInfo_OrderAddressesForm);
formBttn = MEMRFQS_VendorInfo_OrderAddressesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMRFQS_VendorInfo_OrderAddressesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMRFQS_VendorInfo_OrderAddressesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on VendorInfo_OrderAddresses_AddressCode...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfo_OrderAddresses_AddressCode = new Control("VendorInfo_OrderAddresses_AddressCode", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDADDR_ORDADDR_']/ancestor::form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfo_OrderAddresses_AddressCode.Exists());

											Driver.SessionLogger.WriteLine("Industry Classification");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExist on VendorInfo_IndustryClassificationsFormTable...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfo_IndustryClassificationsFormTable = new Control("VendorInfo_IndustryClassificationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDINDCLASS_INDCLASS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfo_IndustryClassificationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on VendorInfo_IndustryClassificationsForm...", Logger.MessageType.INF);
			Control MEMRFQS_VendorInfo_IndustryClassificationsForm = new Control("VendorInfo_IndustryClassificationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_VENDINDCLASS_INDCLASS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_VendorInfo_IndustryClassificationsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_VendorInfoForm);
formBttn = MEMRFQS_VendorInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assigned Vendors");


												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExist on RFQLineSelectionsFormTable...", Logger.MessageType.INF);
			Control MEMRFQS_RFQLineSelectionsFormTable = new Control("RFQLineSelectionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_RFQSELECTIONS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMRFQS_RFQLineSelectionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on RFQLineSelectionsForm...", Logger.MessageType.INF);
			Control MEMRFQS_RFQLineSelectionsForm = new Control("RFQLineSelectionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_RFQSELECTIONS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMRFQS_RFQLineSelectionsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_RFQLineSelectionsForm);
formBttn = MEMRFQS_RFQLineSelectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMRFQS_RFQLineSelectionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMRFQS_RFQLineSelectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MEMRFQS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMRFQS] Perfoming VerifyExists on RFQLineSelections_Item...", Logger.MessageType.INF);
			Control MEMRFQS_RFQLineSelections_Item = new Control("RFQLineSelections_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMRFQS_RFQSELECTIONS_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,MEMRFQS_RFQLineSelections_Item.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "MEMRFQS";
							CPCommon.WaitControlDisplayed(MEMRFQS_MainForm);
formBttn = MEMRFQS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

