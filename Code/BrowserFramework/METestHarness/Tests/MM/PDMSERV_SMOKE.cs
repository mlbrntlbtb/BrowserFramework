 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMSERV_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Items", "xpath","//div[@class='navItem'][.='Items']").Click();
new Control("Manage Services", "xpath","//div[@class='navItem'][.='Manage Services']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMSERV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMSERV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on ServiceID...", Logger.MessageType.INF);
			Control PDMSERV_ServiceID = new Control("ServiceID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDMSERV_ServiceID.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_MainForm);
IWebElement formBttn = PDMSERV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMSERV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMSERV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMSERV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_MainForm);
formBttn = PDMSERV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMSERV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMSERV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PDMSERV_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMSERV_MainFormTab);
IWebElement mTab = PDMSERV_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Characteristics").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on Characteristics_UM...", Logger.MessageType.INF);
			Control PDMSERV_Characteristics_UM = new Control("Characteristics_UM", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_UM_CD']");
			CPCommon.AssertEqual(true,PDMSERV_Characteristics_UM.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_MainFormTab);
mTab = PDMSERV_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on Comments_Text...", Logger.MessageType.INF);
			Control PDMSERV_Comments_Text = new Control("Comments_Text", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_NT']");
			CPCommon.AssertEqual(true,PDMSERV_Comments_Text.Exists());

											Driver.SessionLogger.WriteLine("Vendors");


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on VendorsLink...", Logger.MessageType.INF);
			Control PDMSERV_VendorsLink = new Control("VendorsLink", "ID", "lnk_1007235_PDMSERV_ITEM_SERVICES");
			CPCommon.AssertEqual(true,PDMSERV_VendorsLink.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_VendorsLink);
PDMSERV_VendorsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on VendorAssignmentFormTable...", Logger.MessageType.INF);
			Control PDMSERV_VendorAssignmentFormTable = new Control("VendorAssignmentFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_VendorAssignmentFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming ClickButton on VendorAssignmentForm...", Logger.MessageType.INF);
			Control PDMSERV_VendorAssignmentForm = new Control("VendorAssignmentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMSERV_VendorAssignmentForm);
formBttn = PDMSERV_VendorAssignmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMSERV_VendorAssignmentForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMSERV_VendorAssignmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.AssertEqual(true,PDMSERV_VendorAssignmentForm.Exists());

													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on VendorAssignment_Vendor...", Logger.MessageType.INF);
			Control PDMSERV_VendorAssignment_Vendor = new Control("VendorAssignment_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PDMSERV_VendorAssignment_Vendor.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_VendorAssignmentForm);
formBttn = PDMSERV_VendorAssignmentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Item Billings");


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on ItemBillingsLink...", Logger.MessageType.INF);
			Control PDMSERV_ItemBillingsLink = new Control("ItemBillingsLink", "ID", "lnk_1007236_PDMSERV_ITEM_SERVICES");
			CPCommon.AssertEqual(true,PDMSERV_ItemBillingsLink.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_ItemBillingsLink);
PDMSERV_ItemBillingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on ItemBillingsForm...", Logger.MessageType.INF);
			Control PDMSERV_ItemBillingsForm = new Control("ItemBillingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMSERV_ItemBillingsForm.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming Select on ItemBillings_ShippingInformation_Tab...", Logger.MessageType.INF);
			Control PDMSERV_ItemBillings_ShippingInformation_Tab = new Control("ItemBillings_ShippingInformation_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMSERV_ItemBillings_ShippingInformation_Tab);
mTab = PDMSERV_ItemBillings_ShippingInformation_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on ItemBillings_BasicInformation_SellingDescription...", Logger.MessageType.INF);
			Control PDMSERV_ItemBillings_BasicInformation_SellingDescription = new Control("ItemBillings_BasicInformation_SellingDescription", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='SALES_ITEM_DESC']");
			CPCommon.AssertEqual(true,PDMSERV_ItemBillings_BasicInformation_SellingDescription.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_ItemBillings_ShippingInformation_Tab);
mTab = PDMSERV_ItemBillings_ShippingInformation_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on ItemBillings_ShippingInformation_Weight...", Logger.MessageType.INF);
			Control PDMSERV_ItemBillings_ShippingInformation_Weight = new Control("ItemBillings_ShippingInformation_Weight", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='SHIP_WGT_QTY']");
			CPCommon.AssertEqual(true,PDMSERV_ItemBillings_ShippingInformation_Weight.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_ItemBillingsForm);
formBttn = PDMSERV_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMSERV_ItemBillingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMSERV_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on ItemBillingsFormTable...", Logger.MessageType.INF);
			Control PDMSERV_ItemBillingsFormTable = new Control("ItemBillingsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_ItemBillingsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_ItemBillingsForm);
formBttn = PDMSERV_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Costs");


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on CostsLink...", Logger.MessageType.INF);
			Control PDMSERV_CostsLink = new Control("CostsLink", "ID", "lnk_1007237_PDMSERV_ITEM_SERVICES");
			CPCommon.AssertEqual(true,PDMSERV_CostsLink.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_CostsLink);
PDMSERV_CostsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on CostsFormTable...", Logger.MessageType.INF);
			Control PDMSERV_CostsFormTable = new Control("CostsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_CostsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming ClickButton on CostsForm...", Logger.MessageType.INF);
			Control PDMSERV_CostsForm = new Control("CostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMSERV_CostsForm);
formBttn = PDMSERV_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMSERV_CostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMSERV_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.AssertEqual(true,PDMSERV_CostsForm.Exists());

													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on Costs_CostType...", Logger.MessageType.INF);
			Control PDMSERV_Costs_CostType = new Control("Costs_CostType", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]/descendant::*[@id='S_ITEM_CST_TYPE']");
			CPCommon.AssertEqual(true,PDMSERV_Costs_CostType.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_CostsForm);
formBttn = PDMSERV_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assigned Standard Text");


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on AssignedStandardTextLink...", Logger.MessageType.INF);
			Control PDMSERV_AssignedStandardTextLink = new Control("AssignedStandardTextLink", "ID", "lnk_1007238_PDMSERV_ITEM_SERVICES");
			CPCommon.AssertEqual(true,PDMSERV_AssignedStandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_AssignedStandardTextLink);
PDMSERV_AssignedStandardTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control PDMSERV_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_TEXTCODES_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on AssignedStandardTextFormTable...", Logger.MessageType.INF);
			Control PDMSERV_AssignedStandardTextFormTable = new Control("AssignedStandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_ITEMTEXT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_AssignedStandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming Close on AssignedStandardTextForm...", Logger.MessageType.INF);
			Control PDMSERV_AssignedStandardTextForm = new Control("AssignedStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_ITEMTEXT_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMSERV_AssignedStandardTextForm);
formBttn = PDMSERV_AssignedStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("User Defined Info");


												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control PDMSERV_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_1001130_PDMSERV_ITEM_SERVICES");
			CPCommon.AssertEqual(true,PDMSERV_UserDefinedInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_UserDefinedInfoLink);
PDMSERV_UserDefinedInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExist on UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control PDMSERV_UserDefinedInfoFormTable = new Control("UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSERV_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control PDMSERV_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMSERV_UserDefinedInfoForm);
formBttn = PDMSERV_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMSERV_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMSERV_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.AssertEqual(true,PDMSERV_UserDefinedInfoForm.Exists());

													
				CPCommon.CurrentComponent = "PDMSERV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSERV] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control PDMSERV_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='DATA_TYPE']");
			CPCommon.AssertEqual(true,PDMSERV_UserDefinedInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_UserDefinedInfoForm);
formBttn = PDMSERV_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PDMSERV";
							CPCommon.WaitControlDisplayed(PDMSERV_MainForm);
formBttn = PDMSERV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

