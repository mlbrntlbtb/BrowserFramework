 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLQSTDHS_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing Reports/Inquiries", "xpath","//div[@class='navItem'][.='Billing Reports/Inquiries']").Click();
new Control("View Standard Billing History", "xpath","//div[@class='navItem'][.='View Standard Billing History']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLQSTDHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLQSTDHS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on SelectionCriteria_FiscalYear...", Logger.MessageType.INF);
			Control BLQSTDHS_SelectionCriteria_FiscalYear = new Control("SelectionCriteria_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,BLQSTDHS_SelectionCriteria_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Inquiry Details Form");


												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control BLQSTDHS_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLQSTDHS_ChildForm);
IWebElement formBttn = BLQSTDHS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? BLQSTDHS_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
BLQSTDHS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control BLQSTDHS_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLQSTDHS_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.WaitControlDisplayed(BLQSTDHS_ChildForm);
formBttn = BLQSTDHS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLQSTDHS_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLQSTDHS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.AssertEqual(true,BLQSTDHS_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on InquiryDetails_Customer...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_Customer = new Control("InquiryDetails_Customer", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLQSTDHS_InquiryDetails_Customer.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming Select on InquiryDetails_ChildFormTab...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_ChildFormTab = new Control("InquiryDetails_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLQSTDHS_InquiryDetails_ChildFormTab);
IWebElement mTab = BLQSTDHS_InquiryDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing History").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on InquiryDetails_BillingHistory_FiscalYearLabel...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_BillingHistory_FiscalYearLabel = new Control("InquiryDetails_BillingHistory_FiscalYearLabel", "xpath", "//input[@id='ITD_BILLED_AMT']/ancestor::form[1]/descendant::*[@id='FY_CD']/preceding-sibling::span[1]");
			CPCommon.AssertEqual(true,BLQSTDHS_InquiryDetails_BillingHistory_FiscalYearLabel.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.WaitControlDisplayed(BLQSTDHS_InquiryDetails_ChildFormTab);
mTab = BLQSTDHS_InquiryDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Totals").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on InquiryDetails_Totals_OtherCharges_Code...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_Totals_OtherCharges_Code = new Control("InquiryDetails_Totals_OtherCharges_Code", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='OTH_CHG_CD1']");
			CPCommon.AssertEqual(true,BLQSTDHS_InquiryDetails_Totals_OtherCharges_Code.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.WaitControlDisplayed(BLQSTDHS_InquiryDetails_ChildFormTab);
mTab = BLQSTDHS_InquiryDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on InquiryDetails_Header_ReceivableAccountInformation_Account...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_Header_ReceivableAccountInformation_Account = new Control("InquiryDetails_Header_ReceivableAccountInformation_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='AR_ACCT_ID']");
			CPCommon.AssertEqual(true,BLQSTDHS_InquiryDetails_Header_ReceivableAccountInformation_Account.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.WaitControlDisplayed(BLQSTDHS_InquiryDetails_ChildFormTab);
mTab = BLQSTDHS_InquiryDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on InquiryDetails_Address_BillTo_Address...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_Address_BillTo_Address = new Control("InquiryDetails_Address_BillTo_Address", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_BILLINVCHDRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='CUST_ADDR_DC']");
			CPCommon.AssertEqual(true,BLQSTDHS_InquiryDetails_Address_BillTo_Address.Exists());

											Driver.SessionLogger.WriteLine("Detail Form");


												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on InquiryDetails_DetailLink...", Logger.MessageType.INF);
			Control BLQSTDHS_InquiryDetails_DetailLink = new Control("InquiryDetails_DetailLink", "ID", "lnk_1007424_BLQSTDHS_BILLINVCHDRHS_CHILD");
			CPCommon.AssertEqual(true,BLQSTDHS_InquiryDetails_DetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.WaitControlDisplayed(BLQSTDHS_InquiryDetails_DetailLink);
BLQSTDHS_InquiryDetails_DetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on DetailForm...", Logger.MessageType.INF);
			Control BLQSTDHS_DetailForm = new Control("DetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_TOTAL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLQSTDHS_DetailForm.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on Detail_TotalAmount...", Logger.MessageType.INF);
			Control BLQSTDHS_Detail_TotalAmount = new Control("Detail_TotalAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_TOTAL_']/ancestor::form[1]/descendant::*[@id='TOTAL_AMOUNT']");
			CPCommon.AssertEqual(true,BLQSTDHS_Detail_TotalAmount.Exists());

											Driver.SessionLogger.WriteLine("Billing Inquiry Detail");


												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExist on BillingInquiryDetailTable...", Logger.MessageType.INF);
			Control BLQSTDHS_BillingInquiryDetailTable = new Control("BillingInquiryDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLQSTDHS_BillingInquiryDetailTable.Exists());

												
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming ClickButton on BillingInquiryDetailForm...", Logger.MessageType.INF);
			Control BLQSTDHS_BillingInquiryDetailForm = new Control("BillingInquiryDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_DETAIL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLQSTDHS_BillingInquiryDetailForm);
formBttn = BLQSTDHS_BillingInquiryDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLQSTDHS_BillingInquiryDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLQSTDHS_BillingInquiryDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.AssertEqual(true,BLQSTDHS_BillingInquiryDetailForm.Exists());

													
				CPCommon.CurrentComponent = "BLQSTDHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLQSTDHS] Perfoming VerifyExists on BillingInquiryDetail_GroupDescription...", Logger.MessageType.INF);
			Control BLQSTDHS_BillingInquiryDetail_GroupDescription = new Control("BillingInquiryDetail_GroupDescription", "xpath", "//div[translate(@id,'0123456789','')='pr__BLQSTDHS_DETAIL_']/ancestor::form[1]/descendant::*[@id='BILL_FM_GRP_LBL']");
			CPCommon.AssertEqual(true,BLQSTDHS_BillingInquiryDetail_GroupDescription.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLQSTDHS";
							CPCommon.WaitControlDisplayed(BLQSTDHS_MainForm);
formBttn = BLQSTDHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

