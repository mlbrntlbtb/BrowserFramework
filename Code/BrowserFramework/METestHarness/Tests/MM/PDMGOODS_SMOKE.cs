 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMGOODS_SMOKE : TestScript
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
new Control("Manage Goods", "xpath","//div[@class='navItem'][.='Manage Goods']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMGOODS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on GoodsID...", Logger.MessageType.INF);
			Control PDMGOODS_GoodsID = new Control("GoodsID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDMGOODS_GoodsID.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on Characteristics_UM...", Logger.MessageType.INF);
			Control PDMGOODS_Characteristics_UM = new Control("Characteristics_UM", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_UM_CD']");
			CPCommon.AssertEqual(true,PDMGOODS_Characteristics_UM.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PDMGOODS_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMGOODS_MainFormTab);
IWebElement mTab = PDMGOODS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_MainForm);
IWebElement formBttn = PDMGOODS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMGOODS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMGOODS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Vendors");


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Click on VendorsLink...", Logger.MessageType.INF);
			Control PDMGOODS_VendorsLink = new Control("VendorsLink", "ID", "lnk_1007195_PDMGOODS_ITEM_HDR");
			CPCommon.WaitControlDisplayed(PDMGOODS_VendorsLink);
PDMGOODS_VendorsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on VendorAssignmentForm...", Logger.MessageType.INF);
			Control PDMGOODS_VendorAssignmentForm = new Control("VendorAssignmentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_VendorAssignmentForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on VendorAssignmentFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_VendorAssignmentFormTable = new Control("VendorAssignmentFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_VendorAssignmentFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_VendorAssignmentForm);
formBttn = PDMGOODS_VendorAssignmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMGOODS_VendorAssignmentForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMGOODS_VendorAssignmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on VendorAssignment_Vendor...", Logger.MessageType.INF);
			Control PDMGOODS_VendorAssignment_Vendor = new Control("VendorAssignment_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PDMGOODS_VendorAssignment_Vendor.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_VendorAssignmentForm);
formBttn = PDMGOODS_VendorAssignmentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Item Billings");


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Click on ItemBillingsLink...", Logger.MessageType.INF);
			Control PDMGOODS_ItemBillingsLink = new Control("ItemBillingsLink", "ID", "lnk_1007194_PDMGOODS_ITEM_HDR");
			CPCommon.WaitControlDisplayed(PDMGOODS_ItemBillingsLink);
PDMGOODS_ItemBillingsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on ItemBillingsForm...", Logger.MessageType.INF);
			Control PDMGOODS_ItemBillingsForm = new Control("ItemBillingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_ItemBillingsForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on ItemBillings_BasicInformation_SellingDescription...", Logger.MessageType.INF);
			Control PDMGOODS_ItemBillings_BasicInformation_SellingDescription = new Control("ItemBillings_BasicInformation_SellingDescription", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='SALES_ITEM_DESC']");
			CPCommon.AssertEqual(true,PDMGOODS_ItemBillings_BasicInformation_SellingDescription.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Select on ItemBillings_Tab...", Logger.MessageType.INF);
			Control PDMGOODS_ItemBillings_Tab = new Control("ItemBillings_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMGOODS_ItemBillings_Tab);
mTab = PDMGOODS_ItemBillings_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on ItemBillings_ShippingInformation_Weight...", Logger.MessageType.INF);
			Control PDMGOODS_ItemBillings_ShippingInformation_Weight = new Control("ItemBillings_ShippingInformation_Weight", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='SHIP_WGT_QTY']");
			CPCommon.AssertEqual(true,PDMGOODS_ItemBillings_ShippingInformation_Weight.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_ItemBillingsForm);
formBttn = PDMGOODS_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMGOODS_ItemBillingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMGOODS_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on ItemBillingsFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_ItemBillingsFormTable = new Control("ItemBillingsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_ItemBillingsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_ItemBillingsForm);
formBttn = PDMGOODS_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Costs");


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Click on CostsLink...", Logger.MessageType.INF);
			Control PDMGOODS_CostsLink = new Control("CostsLink", "ID", "lnk_1007196_PDMGOODS_ITEM_HDR");
			CPCommon.WaitControlDisplayed(PDMGOODS_CostsLink);
PDMGOODS_CostsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on CostsForm...", Logger.MessageType.INF);
			Control PDMGOODS_CostsForm = new Control("CostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_CostsForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on CostsFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_CostsFormTable = new Control("CostsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_CostsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_CostsForm);
formBttn = PDMGOODS_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMGOODS_CostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMGOODS_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on Costs_TotalUnitCost...", Logger.MessageType.INF);
			Control PDMGOODS_Costs_TotalUnitCost = new Control("Costs_TotalUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]/descendant::*[@id='TOTAL']");
			CPCommon.AssertEqual(true,PDMGOODS_Costs_TotalUnitCost.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_CostsForm);
formBttn = PDMGOODS_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assigned Standard txt");


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Click on AssignedStandardTextLink...", Logger.MessageType.INF);
			Control PDMGOODS_AssignedStandardTextLink = new Control("AssignedStandardTextLink", "ID", "lnk_1007197_PDMGOODS_ITEM_HDR");
			CPCommon.WaitControlDisplayed(PDMGOODS_AssignedStandardTextLink);
PDMGOODS_AssignedStandardTextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on AssignedStandardTextForm...", Logger.MessageType.INF);
			Control PDMGOODS_AssignedStandardTextForm = new Control("AssignedStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_ITEMTEXT_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_AssignedStandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on StandardTextForm...", Logger.MessageType.INF);
			Control PDMGOODS_StandardTextForm = new Control("StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_TEXTCODES_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_StandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on AssignedStandardTextFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_AssignedStandardTextFormTable = new Control("AssignedStandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_ITEMTEXT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_AssignedStandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_TEXTCODES_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_AssignedStandardTextForm);
formBttn = PDMGOODS_AssignedStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("User Defined Info");


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming Click on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control PDMGOODS_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_1001128_PDMGOODS_ITEM_HDR");
			CPCommon.WaitControlDisplayed(PDMGOODS_UserDefinedInfoLink);
PDMGOODS_UserDefinedInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control PDMGOODS_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMGOODS_UserDefinedInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExist on UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control PDMGOODS_UserDefinedInfoFormTable = new Control("UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMGOODS_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_UserDefinedInfoForm);
formBttn = PDMGOODS_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMGOODS_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMGOODS_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDMGOODS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMGOODS] Perfoming VerifyExists on UserDefinedInfo_Labels...", Logger.MessageType.INF);
			Control PDMGOODS_UserDefinedInfo_Labels = new Control("UserDefinedInfo_Labels", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='UDEF_LBL']");
			CPCommon.AssertEqual(true,PDMGOODS_UserDefinedInfo_Labels.Exists());

												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_UserDefinedInfoForm);
formBttn = PDMGOODS_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDMGOODS";
							CPCommon.WaitControlDisplayed(PDMGOODS_MainForm);
formBttn = PDMGOODS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

