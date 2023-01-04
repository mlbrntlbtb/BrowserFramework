 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPQVNDP_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("VENDORS", "xpath","//div[@class='navItem'][.='VENDORS']").Click();
new Control("View VENDOR Performance", "xpath","//div[@class='navItem'][.='View VENDOR Performance']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPQVNDP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPQVNDP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on MainForm_LineCharges_Vendor...", Logger.MessageType.INF);
			Control PPQVNDP_MainForm_LineCharges_Vendor = new Control("MainForm_LineCharges_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PPQVNDP_MainForm_LineCharges_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming ClickButton on VendorDetailsForm...", Logger.MessageType.INF);
			Control PPQVNDP_VendorDetailsForm = new Control("VendorDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPQVNDP_VendorDetailsForm);
IWebElement formBttn = PPQVNDP_VendorDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PPQVNDP_VendorDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PPQVNDP_VendorDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExist on VendorDetailsFormTable...", Logger.MessageType.INF);
			Control PPQVNDP_VendorDetailsFormTable = new Control("VendorDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPQVNDP_VendorDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.WaitControlDisplayed(PPQVNDP_VendorDetailsForm);
formBttn = PPQVNDP_VendorDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPQVNDP_VendorDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPQVNDP_VendorDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.AssertEqual(true,PPQVNDP_VendorDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on VendorDetails_Vendor...", Logger.MessageType.INF);
			Control PPQVNDP_VendorDetails_Vendor = new Control("VendorDetails_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_DTL_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PPQVNDP_VendorDetails_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Assigned Items");


												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on VendorDetails_AssignedItemsLink...", Logger.MessageType.INF);
			Control PPQVNDP_VendorDetails_AssignedItemsLink = new Control("VendorDetails_AssignedItemsLink", "ID", "lnk_1006636_PPQVNDP_DTL");
			CPCommon.AssertEqual(true,PPQVNDP_VendorDetails_AssignedItemsLink.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.WaitControlDisplayed(PPQVNDP_VendorDetails_AssignedItemsLink);
PPQVNDP_VendorDetails_AssignedItemsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExist on AssignedItemsFormTable...", Logger.MessageType.INF);
			Control PPQVNDP_AssignedItemsFormTable = new Control("AssignedItemsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_ASSIGNEDITEMS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPQVNDP_AssignedItemsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming ClickButton on AssignedItemsForm...", Logger.MessageType.INF);
			Control PPQVNDP_AssignedItemsForm = new Control("AssignedItemsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_ASSIGNEDITEMS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPQVNDP_AssignedItemsForm);
formBttn = PPQVNDP_AssignedItemsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPQVNDP_AssignedItemsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPQVNDP_AssignedItemsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.AssertEqual(true,PPQVNDP_AssignedItemsForm.Exists());

													
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on AssignedItems_Item...", Logger.MessageType.INF);
			Control PPQVNDP_AssignedItems_Item = new Control("AssignedItems_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_ASSIGNEDITEMS_DTL_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PPQVNDP_AssignedItems_Item.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.WaitControlDisplayed(PPQVNDP_AssignedItemsForm);
formBttn = PPQVNDP_AssignedItemsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Addresses");


												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on VendorDetails_AddressesLink...", Logger.MessageType.INF);
			Control PPQVNDP_VendorDetails_AddressesLink = new Control("VendorDetails_AddressesLink", "ID", "lnk_1006635_PPQVNDP_DTL");
			CPCommon.AssertEqual(true,PPQVNDP_VendorDetails_AddressesLink.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.WaitControlDisplayed(PPQVNDP_VendorDetails_AddressesLink);
PPQVNDP_VendorDetails_AddressesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExist on AddressesFormTable...", Logger.MessageType.INF);
			Control PPQVNDP_AddressesFormTable = new Control("AddressesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_ADDRESSES_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPQVNDP_AddressesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming ClickButton on AddressesForm...", Logger.MessageType.INF);
			Control PPQVNDP_AddressesForm = new Control("AddressesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_ADDRESSES_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPQVNDP_AddressesForm);
formBttn = PPQVNDP_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPQVNDP_AddressesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPQVNDP_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.AssertEqual(true,PPQVNDP_AddressesForm.Exists());

													
				CPCommon.CurrentComponent = "PPQVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVNDP] Perfoming VerifyExists on Addresses_AddressCode...", Logger.MessageType.INF);
			Control PPQVNDP_Addresses_AddressCode = new Control("Addresses_AddressCode", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVNDP_ADDRESSES_DTL_']/ancestor::form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,PPQVNDP_Addresses_AddressCode.Exists());

												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.WaitControlDisplayed(PPQVNDP_AddressesForm);
formBttn = PPQVNDP_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PPQVNDP";
							CPCommon.WaitControlDisplayed(PPQVNDP_MainForm);
formBttn = PPQVNDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

