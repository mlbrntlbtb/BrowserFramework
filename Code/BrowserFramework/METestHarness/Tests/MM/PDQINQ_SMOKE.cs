 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDQINQ_SMOKE : TestScript
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
new Control("Product Definition Reports/Inquiries", "xpath","//div[@class='navItem'][.='Product Definition Reports/Inquiries']").Click();
new Control("View Items", "xpath","//div[@class='navItem'][.='View Items']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDQINQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDQINQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on MainForm_Item...", Logger.MessageType.INF);
			Control PDQINQ_MainForm_Item = new Control("MainForm_Item", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDQINQ_MainForm_Item.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							PDQINQ_MainForm_Item.Click();
PDQINQ_MainForm_Item.SendKeys("0212PC500", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PDQINQ_MainForm_Item.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Item Details");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on ItemDetailsTable...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetailsTable = new Control("ItemDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails = new Control("ItemDetails", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEM_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails);
IWebElement formBttn = PDQINQ_ItemDetails.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_ItemDetails.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_ItemDetails.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_Item...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_Item = new Control("ItemDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEM_CTW_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_Item.Exists());

											Driver.SessionLogger.WriteLine("Std Text");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_StdTextLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_StdTextLink = new Control("ItemDetails_StdTextLink", "ID", "lnk_1006229_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_StdTextLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_StdTextLink);
PDQINQ_ItemDetails_StdTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on StdText_StdTextTable...", Logger.MessageType.INF);
			Control PDQINQ_StdText_StdTextTable = new Control("StdText_StdTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ITEMTEXT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_StdText_StdTextTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on StdText...", Logger.MessageType.INF);
			Control PDQINQ_StdText = new Control("StdText", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ITEMTEXT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_StdText.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_StdText);
formBttn = PDQINQ_StdText.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Vendors");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_VendorsLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_VendorsLink = new Control("ItemDetails_VendorsLink", "ID", "lnk_1006230_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_VendorsLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_VendorsLink);
PDQINQ_ItemDetails_VendorsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on VendorsTable...", Logger.MessageType.INF);
			Control PDQINQ_VendorsTable = new Control("VendorsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMVEND_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_VendorsTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on Vendors...", Logger.MessageType.INF);
			Control PDQINQ_Vendors = new Control("Vendors", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMVEND_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_Vendors.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_Vendors);
formBttn = PDQINQ_Vendors.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_Vendors.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_Vendors.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on Vendors_Vendor...", Logger.MessageType.INF);
			Control PDQINQ_Vendors_Vendor = new Control("Vendors_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMVEND_CTW_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PDQINQ_Vendors_Vendor.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_Vendors);
formBttn = PDQINQ_Vendors.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Item Cost");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_ItemCostLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_ItemCostLink = new Control("ItemDetails_ItemCostLink", "ID", "lnk_1006231_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_ItemCostLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_ItemCostLink);
PDQINQ_ItemDetails_ItemCostLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on ItemCostFormTable...", Logger.MessageType.INF);
			Control PDQINQ_ItemCostFormTable = new Control("ItemCostFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_ItemCostFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemCostForm...", Logger.MessageType.INF);
			Control PDQINQ_ItemCostForm = new Control("ItemCostForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMCST_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_ItemCostForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemCostForm);
formBttn = PDQINQ_ItemCostForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_ItemCostForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_ItemCostForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemCost_Type...", Logger.MessageType.INF);
			Control PDQINQ_ItemCost_Type = new Control("ItemCost_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='S_ITEM_CST_TYPE']");
			CPCommon.AssertEqual(true,PDQINQ_ItemCost_Type.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemCostForm);
formBttn = PDQINQ_ItemCostForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Item Billing");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_ItemBillingLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_ItemBillingLink = new Control("ItemDetails_ItemBillingLink", "ID", "lnk_1006232_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_ItemBillingLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_ItemBillingLink);
PDQINQ_ItemDetails_ItemBillingLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemBillingForm...", Logger.MessageType.INF);
			Control PDQINQ_ItemBillingForm = new Control("ItemBillingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_PRODPRICECATLG_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_ItemBillingForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemBilling_Catalog...", Logger.MessageType.INF);
			Control PDQINQ_ItemBilling_Catalog = new Control("ItemBilling_Catalog", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_PRODPRICECATLG_CTW_']/ancestor::form[1]/descendant::*[@id='PRICE_CATLG_CD']");
			CPCommon.AssertEqual(true,PDQINQ_ItemBilling_Catalog.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemBillingForm);
formBttn = PDQINQ_ItemBillingForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDQINQ_ItemBillingForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDQINQ_ItemBillingForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on ItemBillingTable...", Logger.MessageType.INF);
			Control PDQINQ_ItemBillingTable = new Control("ItemBillingTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_PRODPRICECATLG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_ItemBillingTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemBillingForm);
formBttn = PDQINQ_ItemBillingForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Alternate Parts");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_AlternatePartsLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_AlternatePartsLink = new Control("ItemDetails_AlternatePartsLink", "ID", "lnk_5290_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_AlternatePartsLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_AlternatePartsLink);
PDQINQ_ItemDetails_AlternatePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on AlternatePartsFormTable...", Logger.MessageType.INF);
			Control PDQINQ_AlternatePartsFormTable = new Control("AlternatePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ALTPART_MEALTPARTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_AlternatePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on AlternatePartsForm...", Logger.MessageType.INF);
			Control PDQINQ_AlternatePartsForm = new Control("AlternatePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ALTPART_MEALTPARTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_AlternatePartsForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_AlternatePartsForm);
formBttn = PDQINQ_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_AlternatePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_AlternatePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on Preferred...", Logger.MessageType.INF);
			Control PDQINQ_Preferred = new Control("Preferred", "xpath", "//input[@id='VEND_PART_RVSN_ID']/ancestor::form[1]/descendant::*[@id='PREF_FL']");
			CPCommon.AssertEqual(true,PDQINQ_Preferred.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_AlternatePartsForm);
formBttn = PDQINQ_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitute Parts");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_SubstitutePartsLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_SubstitutePartsLink = new Control("ItemDetails_SubstitutePartsLink", "ID", "lnk_1007294_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_SubstitutePartsLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_SubstitutePartsLink);
PDQINQ_ItemDetails_SubstitutePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on SubstitutePartsFormTable...", Logger.MessageType.INF);
			Control PDQINQ_SubstitutePartsFormTable = new Control("SubstitutePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_SUBSTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_SubstitutePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on SubstitutePartsForm...", Logger.MessageType.INF);
			Control PDQINQ_SubstitutePartsForm = new Control("SubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_SUBSTPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_SubstitutePartsForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_SubstitutePartsForm);
formBttn = PDQINQ_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_SubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_SubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on SubstituteParts_Sequence...", Logger.MessageType.INF);
			Control PDQINQ_SubstituteParts_Sequence = new Control("SubstituteParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_SUBSTPART_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,PDQINQ_SubstituteParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_SubstitutePartsForm);
formBttn = PDQINQ_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Projects");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_ProjectsLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_ProjectsLink = new Control("ItemDetails_ProjectsLink", "ID", "lnk_1006244_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_ProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_ProjectsLink);
PDQINQ_ItemDetails_ProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on ProjectsFormTable...", Logger.MessageType.INF);
			Control PDQINQ_ProjectsFormTable = new Control("ProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_PARTPROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_ProjectsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ProjectsForm...", Logger.MessageType.INF);
			Control PDQINQ_ProjectsForm = new Control("ProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_PARTPROJ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_ProjectsForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ProjectsForm);
formBttn = PDQINQ_ProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_ProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_ProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on Projects_Project...", Logger.MessageType.INF);
			Control PDQINQ_Projects_Project = new Control("Projects_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_PARTPROJ_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PDQINQ_Projects_Project.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ProjectsForm);
formBttn = PDQINQ_ProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Project Item Cost");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_ProjectItemCostLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_ProjectItemCostLink = new Control("ItemDetails_ProjectItemCostLink", "ID", "lnk_1006245_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_ProjectItemCostLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_ProjectItemCostLink);
PDQINQ_ItemDetails_ProjectItemCostLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on ProjectItemCostFormTable...", Logger.MessageType.INF);
			Control PDQINQ_ProjectItemCostFormTable = new Control("ProjectItemCostFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMPROJCST_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_ProjectItemCostFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ProjectItemCostForm...", Logger.MessageType.INF);
			Control PDQINQ_ProjectItemCostForm = new Control("ProjectItemCostForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMPROJCST_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_ProjectItemCostForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ProjectItemCostForm);
formBttn = PDQINQ_ProjectItemCostForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_ProjectItemCostForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_ProjectItemCostForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ProjectItemCost_ProjectID...", Logger.MessageType.INF);
			Control PDQINQ_ProjectItemCost_ProjectID = new Control("ProjectItemCost_ProjectID", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQINQ_ITEMPROJCST_CTW_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PDQINQ_ProjectItemCost_ProjectID.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ProjectItemCostForm);
formBttn = PDQINQ_ProjectItemCostForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ECNs");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_ECNsLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_ECNsLink = new Control("ItemDetails_ECNsLink", "ID", "lnk_1006300_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_ECNsLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_ECNsLink);
PDQINQ_ItemDetails_ECNsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on ECNFormTable...", Logger.MessageType.INF);
			Control PDQINQ_ECNFormTable = new Control("ECNFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_ECNFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ECNForm...", Logger.MessageType.INF);
			Control PDQINQ_ECNForm = new Control("ECNForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_ECNForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ECNForm);
formBttn = PDQINQ_ECNForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_ECNForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_ECNForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ECNs_ECNId...", Logger.MessageType.INF);
			Control PDQINQ_ECNs_ECNId = new Control("ECNs_ECNId", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='ECN_ID']");
			CPCommon.AssertEqual(true,PDQINQ_ECNs_ECNId.Exists());

											Driver.SessionLogger.WriteLine("ECNs");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming Select on ECNs_ECNsTab...", Logger.MessageType.INF);
			Control PDQINQ_ECNs_ECNsTab = new Control("ECNs_ECNsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDQINQ_ECNs_ECNsTab);
IWebElement mTab = PDQINQ_ECNs_ECNsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "ECN Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ECNs_ECNDetails_Status...", Logger.MessageType.INF);
			Control PDQINQ_ECNs_ECNDetails_Status = new Control("ECNs_ECNDetails_Status", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='S_ECN_STATUS_CD']");
			CPCommon.AssertEqual(true,PDQINQ_ECNs_ECNDetails_Status.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ECNForm);
formBttn = PDQINQ_ECNForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on ItemDetails_PartDocumentsLink...", Logger.MessageType.INF);
			Control PDQINQ_ItemDetails_PartDocumentsLink = new Control("ItemDetails_PartDocumentsLink", "ID", "lnk_1006247_PDQINQ_ITEM_CTW");
			CPCommon.AssertEqual(true,PDQINQ_ItemDetails_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_ItemDetails_PartDocumentsLink);
PDQINQ_ItemDetails_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExist on PartDocumentsFormTable...", Logger.MessageType.INF);
			Control PDQINQ_PartDocumentsFormTable = new Control("PartDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQINQ_PartDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on PartDocumentsForm...", Logger.MessageType.INF);
			Control PDQINQ_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDQINQ_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_PartDocumentsForm);
formBttn = PDQINQ_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQINQ_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQINQ_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDQINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQINQ] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control PDQINQ_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,PDQINQ_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_PartDocumentsForm);
formBttn = PDQINQ_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDQINQ";
							CPCommon.WaitControlDisplayed(PDQINQ_MainForm);
formBttn = PDQINQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

