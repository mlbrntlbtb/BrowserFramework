 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMPART_SMOKE : TestScript
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
new Control("Manage Parts", "xpath","//div[@class='navItem'][.='Manage Parts']").Click();


											Driver.SessionLogger.WriteLine("Basic Information");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Select on BasicInformationTab...", Logger.MessageType.INF);
			Control PDMPART_BasicInformationTab = new Control("BasicInformationTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMPART_BasicInformationTab);
IWebElement mTab = PDMPART_BasicInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Characteristics").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on Characteristics_BasicCharacteristics_UM...", Logger.MessageType.INF);
			Control PDMPART_Characteristics_BasicCharacteristics_UM = new Control("Characteristics_BasicCharacteristics_UM", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_UM_CD']");
			CPCommon.AssertEqual(true,PDMPART_Characteristics_BasicCharacteristics_UM.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_BasicInformationTab);
mTab = PDMPART_BasicInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Serial Lot Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on SerialLotInformation_InventoryTrackingRequired_Serial...", Logger.MessageType.INF);
			Control PDMPART_SerialLotInformation_InventoryTrackingRequired_Serial = new Control("SerialLotInformation_InventoryTrackingRequired_Serial", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SERIAL_REQD_FL']");
			CPCommon.AssertEqual(true,PDMPART_SerialLotInformation_InventoryTrackingRequired_Serial.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_BasicInformationTab);
mTab = PDMPART_BasicInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on Comments...", Logger.MessageType.INF);
			Control PDMPART_Comments = new Control("Comments", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_NT']");
			CPCommon.AssertEqual(true,PDMPART_Comments.Exists());

											Driver.SessionLogger.WriteLine("Units of Measure");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on UnitsOfMeasureLink...", Logger.MessageType.INF);
			Control PDMPART_UnitsOfMeasureLink = new Control("UnitsOfMeasureLink", "ID", "lnk_1006518_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_UnitsOfMeasureLink);
PDMPART_UnitsOfMeasureLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on UnitsOfMeasureTable...", Logger.MessageType.INF);
			Control PDMPART_UnitsOfMeasureTable = new Control("UnitsOfMeasureTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDUM_ITEMUM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_UnitsOfMeasureTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Close on UnitsOfMeasureForm...", Logger.MessageType.INF);
			Control PDMPART_UnitsOfMeasureForm = new Control("UnitsOfMeasureForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDUM_ITEMUM_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_UnitsOfMeasureForm);
IWebElement formBttn = PDMPART_UnitsOfMeasureForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("U/M Conversion");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on UMConversionsLink...", Logger.MessageType.INF);
			Control PDMPART_UMConversionsLink = new Control("UMConversionsLink", "ID", "lnk_1006519_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_UMConversionsLink);
PDMPART_UMConversionsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on UMConversionsTable...", Logger.MessageType.INF);
			Control PDMPART_UMConversionsTable = new Control("UMConversionsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDUM_UMCONV_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_UMConversionsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on UMConversionsForm...", Logger.MessageType.INF);
			Control PDMPART_UMConversionsForm = new Control("UMConversionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDUM_UMCONV_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_UMConversionsForm);
formBttn = PDMPART_UMConversionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_UMConversionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_UMConversionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on UMConversions_UMFrom...", Logger.MessageType.INF);
			Control PDMPART_UMConversions_UMFrom = new Control("UMConversions_UMFrom", "xpath", "//div[translate(@id,'0123456789','')='pr__PDUM_UMCONV_CTW_']/ancestor::form[1]/descendant::*[@id='UM_CD_FR']");
			CPCommon.AssertEqual(true,PDMPART_UMConversions_UMFrom.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_UMConversionsForm);
formBttn = PDMPART_UMConversionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Planning");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on PlanningLink...", Logger.MessageType.INF);
			Control PDMPART_PlanningLink = new Control("PlanningLink", "ID", "lnk_1006526_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_PlanningLink);
PDMPART_PlanningLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on PlanningDetails_BasicCharacteristics_Commodity...", Logger.MessageType.INF);
			Control PDMPART_PlanningDetails_BasicCharacteristics_Commodity = new Control("PlanningDetails_BasicCharacteristics_Commodity", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPD_PART_']/ancestor::form[1]/descendant::*[@id='COMM_CD']");
			CPCommon.AssertEqual(true,PDMPART_PlanningDetails_BasicCharacteristics_Commodity.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Close on PlanningDetailsForm...", Logger.MessageType.INF);
			Control PDMPART_PlanningDetailsForm = new Control("PlanningDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPD_PART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_PlanningDetailsForm);
formBttn = PDMPART_PlanningDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Alternate Parts");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on AlternatePartsLink...", Logger.MessageType.INF);
			Control PDMPART_AlternatePartsLink = new Control("AlternatePartsLink", "ID", "lnk_1006622_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_AlternatePartsLink);
PDMPART_AlternatePartsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on AlternatePartsTable...", Logger.MessageType.INF);
			Control PDMPART_AlternatePartsTable = new Control("AlternatePartsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMALT_ALTPART_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_AlternatePartsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on AlternatePartsForm...", Logger.MessageType.INF);
			Control PDMPART_AlternatePartsForm = new Control("AlternatePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMALT_ALTPART_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_AlternatePartsForm);
formBttn = PDMPART_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_AlternatePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on AlternateParts_Manufacturer_Manufacturer...", Logger.MessageType.INF);
			Control PDMPART_AlternateParts_Manufacturer_Manufacturer = new Control("AlternateParts_Manufacturer_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMALT_ALTPART_DTL_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,PDMPART_AlternateParts_Manufacturer_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_AlternatePartsForm);
formBttn = PDMPART_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitute Parts");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on SubstitutePartsLink...", Logger.MessageType.INF);
			Control PDMPART_SubstitutePartsLink = new Control("SubstitutePartsLink", "ID", "lnk_1006562_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_SubstitutePartsLink);
PDMPART_SubstitutePartsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on SubstitutePartsTable...", Logger.MessageType.INF);
			Control PDMPART_SubstitutePartsTable = new Control("SubstitutePartsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSUBST_SUBSTPART_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_SubstitutePartsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on SubstitutePartsForm...", Logger.MessageType.INF);
			Control PDMPART_SubstitutePartsForm = new Control("SubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSUBST_SUBSTPART_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_SubstitutePartsForm);
formBttn = PDMPART_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_SubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on SubstituteParts_Sequence...", Logger.MessageType.INF);
			Control PDMPART_SubstituteParts_Sequence = new Control("SubstituteParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSUBST_SUBSTPART_HDR_']/ancestor::form[1]/descendant::*[@id='USA_SEQ_NO']");
			CPCommon.AssertEqual(true,PDMPART_SubstituteParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_SubstitutePartsForm);
formBttn = PDMPART_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Project Requirements");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on ProjectRequirementsLink...", Logger.MessageType.INF);
			Control PDMPART_ProjectRequirementsLink = new Control("ProjectRequirementsLink", "ID", "lnk_1006563_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_ProjectRequirementsLink);
PDMPART_ProjectRequirementsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on ProjectRequirementsTable...", Logger.MessageType.INF);
			Control PDMPART_ProjectRequirementsTable = new Control("ProjectRequirementsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJPD_PARTPROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_ProjectRequirementsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on ProjectRequirementsForm...", Logger.MessageType.INF);
			Control PDMPART_ProjectRequirementsForm = new Control("ProjectRequirementsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJPD_PARTPROJ_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_ProjectRequirementsForm);
formBttn = PDMPART_ProjectRequirementsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_ProjectRequirementsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_ProjectRequirementsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on ProjectRequirements_Project...", Logger.MessageType.INF);
			Control PDMPART_ProjectRequirements_Project = new Control("ProjectRequirements_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJPD_PARTPROJ_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PDMPART_ProjectRequirements_Project.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_ProjectRequirementsForm);
formBttn = PDMPART_ProjectRequirementsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Vendors");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on VendorsLink...", Logger.MessageType.INF);
			Control PDMPART_VendorsLink = new Control("VendorsLink", "ID", "lnk_1007285_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_VendorsLink);
PDMPART_VendorsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on VendorsTable...", Logger.MessageType.INF);
			Control PDMPART_VendorsTable = new Control("VendorsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_VendorsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on VendorsForm...", Logger.MessageType.INF);
			Control PDMPART_VendorsForm = new Control("VendorsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_VendorsForm);
formBttn = PDMPART_VendorsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_VendorsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_VendorsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on Vendors_Vendor...", Logger.MessageType.INF);
			Control PDMPART_Vendors_Vendor = new Control("Vendors_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMVEND_ITEMVEND_DTL_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PDMPART_Vendors_Vendor.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_VendorsForm);
formBttn = PDMPART_VendorsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Item Billings");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on ItemBillingsLink...", Logger.MessageType.INF);
			Control PDMPART_ItemBillingsLink = new Control("ItemBillingsLink", "ID", "lnk_1007282_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_ItemBillingsLink);
PDMPART_ItemBillingsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Select on ItemBillingsTab...", Logger.MessageType.INF);
			Control PDMPART_ItemBillingsTab = new Control("ItemBillingsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMPART_ItemBillingsTab);
mTab = PDMPART_ItemBillingsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on ItemBillings_BasicInformation_SellingDescription...", Logger.MessageType.INF);
			Control PDMPART_ItemBillings_BasicInformation_SellingDescription = new Control("ItemBillings_BasicInformation_SellingDescription", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='SALES_ITEM_DESC']");
			CPCommon.AssertEqual(true,PDMPART_ItemBillings_BasicInformation_SellingDescription.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_ItemBillingsTab);
mTab = PDMPART_ItemBillingsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on ItemBillings_ShippingInformation_Weight...", Logger.MessageType.INF);
			Control PDMPART_ItemBillings_ShippingInformation_Weight = new Control("ItemBillings_ShippingInformation_Weight", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]/descendant::*[@id='SHIP_WGT_QTY']");
			CPCommon.AssertEqual(true,PDMPART_ItemBillings_ShippingInformation_Weight.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButtonIfExists on ItemBillingsForm...", Logger.MessageType.INF);
			Control PDMPART_ItemBillingsForm = new Control("ItemBillingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIBILL_ITEMPRODUCT_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_ItemBillingsForm);
formBttn = PDMPART_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMPART_ItemBillingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMPART_ItemBillingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_ItemBillingsForm);
formBttn = PDMPART_ItemBillingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Costs");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on CostsLink...", Logger.MessageType.INF);
			Control PDMPART_CostsLink = new Control("CostsLink", "ID", "lnk_1007286_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_CostsLink);
PDMPART_CostsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on CostsTable...", Logger.MessageType.INF);
			Control PDMPART_CostsTable = new Control("CostsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_CostsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on CostsForm...", Logger.MessageType.INF);
			Control PDMPART_CostsForm = new Control("CostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_CostsForm);
formBttn = PDMPART_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_CostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on Costs_CostType...", Logger.MessageType.INF);
			Control PDMPART_Costs_CostType = new Control("Costs_CostType", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMCOST_ITEMCST_DTL_']/ancestor::form[1]/descendant::*[@id='S_ITEM_CST_TYPE']");
			CPCommon.AssertEqual(true,PDMPART_Costs_CostType.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_CostsForm);
formBttn = PDMPART_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Project Item Costs");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on ProjectItemCostsLink...", Logger.MessageType.INF);
			Control PDMPART_ProjectItemCostsLink = new Control("ProjectItemCostsLink", "ID", "lnk_1006949_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_ProjectItemCostsLink);
PDMPART_ProjectItemCostsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on ProjectItemCostsTable...", Logger.MessageType.INF);
			Control PDMPART_ProjectItemCostsTable = new Control("ProjectItemCostsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJCS_ITEMPROJCST_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_ProjectItemCostsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on ProjectItemCostsForm...", Logger.MessageType.INF);
			Control PDMPART_ProjectItemCostsForm = new Control("ProjectItemCostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJCS_ITEMPROJCST_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_ProjectItemCostsForm);
formBttn = PDMPART_ProjectItemCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_ProjectItemCostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_ProjectItemCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on ProjectItemCosts_CostType...", Logger.MessageType.INF);
			Control PDMPART_ProjectItemCosts_CostType = new Control("ProjectItemCosts_CostType", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJCS_ITEMPROJCST_DTL_']/ancestor::form[1]/descendant::*[@id='S_ITEM_CST_TYPE']");
			CPCommon.AssertEqual(true,PDMPART_ProjectItemCosts_CostType.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_ProjectItemCostsForm);
formBttn = PDMPART_ProjectItemCostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assigned Standard Text");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on AssignedStandardTextLink...", Logger.MessageType.INF);
			Control PDMPART_AssignedStandardTextLink = new Control("AssignedStandardTextLink", "ID", "lnk_1007278_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_AssignedStandardTextLink);
PDMPART_AssignedStandardTextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on StandardTextTable...", Logger.MessageType.INF);
			Control PDMPART_StandardTextTable = new Control("StandardTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_TEXTCODES_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_StandardTextTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on AssignedStandardTextTable...", Logger.MessageType.INF);
			Control PDMPART_AssignedStandardTextTable = new Control("AssignedStandardTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_ITEMTEXT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_AssignedStandardTextTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Close on AssignedStandardTextForm...", Logger.MessageType.INF);
			Control PDMPART_AssignedStandardTextForm = new Control("AssignedStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSTDTX_ITEMTEXT_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_AssignedStandardTextForm);
formBttn = PDMPART_AssignedStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on DocumentsLink...", Logger.MessageType.INF);
			Control PDMPART_DocumentsLink = new Control("DocumentsLink", "ID", "lnk_1006520_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_DocumentsLink);
PDMPART_DocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on DocumentsTable...", Logger.MessageType.INF);
			Control PDMPART_DocumentsTable = new Control("DocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMEPD_PARTDOCUMENT_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_DocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on DocumentsForm...", Logger.MessageType.INF);
			Control PDMPART_DocumentsForm = new Control("DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMEPD_PARTDOCUMENT_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_DocumentsForm);
formBttn = PDMPART_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on Documents_Document_Document...", Logger.MessageType.INF);
			Control PDMPART_Documents_Document_Document = new Control("Documents_Document_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMEPD_PARTDOCUMENT_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PDMPART_Documents_Document_Document.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_DocumentsForm);
formBttn = PDMPART_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("User Defined Info");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control PDMPART_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_1006521_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_UserDefinedInfoLink);
PDMPART_UserDefinedInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on UserDefinedInfoTable...", Logger.MessageType.INF);
			Control PDMPART_UserDefinedInfoTable = new Control("UserDefinedInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_UserDefinedInfoTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control PDMPART_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_UserDefinedInfoForm);
formBttn = PDMPART_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on UserDefinedInfo_Labels...", Logger.MessageType.INF);
			Control PDMPART_UserDefinedInfo_Labels = new Control("UserDefinedInfo_Labels", "xpath", "//div[translate(@id,'0123456789','')='pr__MMUDINF_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='UDEF_LBL']");
			CPCommon.AssertEqual(true,PDMPART_UserDefinedInfo_Labels.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
							CPCommon.WaitControlDisplayed(PDMPART_UserDefinedInfoForm);
formBttn = PDMPART_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MBOM");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on ManufacturingBOMLink...", Logger.MessageType.INF);
			Control PDMPART_ManufacturingBOMLink = new Control("ManufacturingBOMLink", "ID", "lnk_4341_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_ManufacturingBOMLink);
PDMPART_ManufacturingBOMLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on ManufacturingBOM_AssemblyDetails_MakeBuy...", Logger.MessageType.INF);
			Control PDMPART_ManufacturingBOM_AssemblyDetails_MakeBuy = new Control("ManufacturingBOM_AssemblyDetails_MakeBuy", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMBOM_PART_HDR_']/ancestor::form[1]/descendant::*[@id='S_MAKE_BUY_CD']");
			CPCommon.AssertEqual(true,PDMPART_ManufacturingBOM_AssemblyDetails_MakeBuy.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on ManufacturingBOM_AssemblyDetailsForm...", Logger.MessageType.INF);
			Control PDMPART_ManufacturingBOM_AssemblyDetailsForm = new Control("ManufacturingBOM_AssemblyDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMBOM_PART_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_ManufacturingBOM_AssemblyDetailsForm);
formBttn = PDMPART_ManufacturingBOM_AssemblyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMPART_ManufacturingBOM_AssemblyDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMPART_ManufacturingBOM_AssemblyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on ManufacturingBOM_AssemblyDetailsTable...", Logger.MessageType.INF);
			Control PDMPART_ManufacturingBOM_AssemblyDetailsTable = new Control("ManufacturingBOM_AssemblyDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMBOM_PART_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_ManufacturingBOM_AssemblyDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on MBOMLineTable...", Logger.MessageType.INF);
			Control PDMPART_MBOMLineTable = new Control("MBOMLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMBOM_MFGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_MBOMLineTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on MBOMLine_OK...", Logger.MessageType.INF);
			Control PDMPART_MBOMLine_OK = new Control("MBOMLine_OK", "xpath", "//div[@id='PDMPART']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.WaitControlDisplayed(PDMPART_MBOMLine_OK);
if (PDMPART_MBOMLine_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PDMPART_MBOMLine_OK.Click(5,5);
else PDMPART_MBOMLine_OK.Click(4.5);


											Driver.SessionLogger.WriteLine("EBOM");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on EngineeringBOMsLink...", Logger.MessageType.INF);
			Control PDMPART_EngineeringBOMsLink = new Control("EngineeringBOMsLink", "ID", "lnk_4313_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_EngineeringBOMsLink);
PDMPART_EngineeringBOMsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on EngineeringBOMs_AssemblyDetails_MakeBuy...", Logger.MessageType.INF);
			Control PDMPART_EngineeringBOMs_AssemblyDetails_MakeBuy = new Control("EngineeringBOMs_AssemblyDetails_MakeBuy", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_PART_HDR_']/ancestor::form[1]/descendant::*[@id='S_MAKE_BUY_CD']");
			CPCommon.AssertEqual(true,PDMPART_EngineeringBOMs_AssemblyDetails_MakeBuy.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on EngineeringBOMs_AssemblyDetailsForm...", Logger.MessageType.INF);
			Control PDMPART_EngineeringBOMs_AssemblyDetailsForm = new Control("EngineeringBOMs_AssemblyDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_PART_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_EngineeringBOMs_AssemblyDetailsForm);
formBttn = PDMPART_EngineeringBOMs_AssemblyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMPART_EngineeringBOMs_AssemblyDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMPART_EngineeringBOMs_AssemblyDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on EngineeringBOMs_AssemblyDetailsTable...", Logger.MessageType.INF);
			Control PDMPART_EngineeringBOMs_AssemblyDetailsTable = new Control("EngineeringBOMs_AssemblyDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_PART_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_EngineeringBOMs_AssemblyDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on EBOMLineTable...", Logger.MessageType.INF);
			Control PDMPART_EBOMLineTable = new Control("EBOMLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_EBOMLineTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on EBOMLineForm...", Logger.MessageType.INF);
			Control PDMPART_EBOMLineForm = new Control("EBOMLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_EBOMLineForm);
formBttn = PDMPART_EBOMLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_EBOMLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_EBOMLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on EBOMLine_LineNo...", Logger.MessageType.INF);
			Control PDMPART_EBOMLine_LineNo = new Control("EBOMLine_LineNo", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEBOM_ENGBOM_CTW_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,PDMPART_EBOMLine_LineNo.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on EBOMLine_OK...", Logger.MessageType.INF);
			Control PDMPART_EBOMLine_OK = new Control("EBOMLine_OK", "xpath", "//div[@id='PDMPART']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.WaitControlDisplayed(PDMPART_EBOMLine_OK);
if (PDMPART_EBOMLine_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PDMPART_EBOMLine_OK.Click(5,5);
else PDMPART_EBOMLine_OK.Click(4.5);


											Driver.SessionLogger.WriteLine("Maintain Routings");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on MaintainRoutingsLink...", Logger.MessageType.INF);
			Control PDMPART_MaintainRoutingsLink = new Control("MaintainRoutingsLink", "ID", "lnk_4879_PDMEPD_PART_PARTINFO");
			CPCommon.WaitControlDisplayed(PDMPART_MaintainRoutingsLink);
PDMPART_MaintainRoutingsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on RoutingHeaderMainForm_RoutingNumber...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderMainForm_RoutingNumber = new Control("RoutingHeaderMainForm_RoutingNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_HEADER_']/ancestor::form[1]/descendant::*[@id='ROUT_NO']");
			CPCommon.AssertEqual(true,PDMPART_RoutingHeaderMainForm_RoutingNumber.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on RoutingHeaderMainForm...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderMainForm = new Control("RoutingHeaderMainForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_HEADER_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_RoutingHeaderMainForm);
formBttn = PDMPART_RoutingHeaderMainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMPART_RoutingHeaderMainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMPART_RoutingHeaderMainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on RoutingHeaderMainTable...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderMainTable = new Control("RoutingHeaderMainTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_HEADER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_RoutingHeaderMainTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExist on RoutingHeaderChildTable...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderChildTable = new Control("RoutingHeaderChildTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPART_RoutingHeaderChildTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming ClickButton on RoutingHeaderChildForm...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderChildForm = new Control("RoutingHeaderChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_RoutingHeaderChildForm);
formBttn = PDMPART_RoutingHeaderChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPART_RoutingHeaderChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPART_RoutingHeaderChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming VerifyExists on RoutingHeaderChildForm_OperationSequence...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderChildForm_OperationSequence = new Control("RoutingHeaderChildForm_OperationSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMROUT_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ROUT_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,PDMPART_RoutingHeaderChildForm_OperationSequence.Exists());

												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Click on RoutingHeaderChildForm_OK...", Logger.MessageType.INF);
			Control PDMPART_RoutingHeaderChildForm_OK = new Control("RoutingHeaderChildForm_OK", "xpath", "//div[@id='PDMPART']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.WaitControlDisplayed(PDMPART_RoutingHeaderChildForm_OK);
if (PDMPART_RoutingHeaderChildForm_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PDMPART_RoutingHeaderChildForm_OK.Click(5,5);
else PDMPART_RoutingHeaderChildForm_OK.Click(4.5);


											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDMPART";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPART] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control PDMPART_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PDMPART_MainForm);
formBttn = PDMPART_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

