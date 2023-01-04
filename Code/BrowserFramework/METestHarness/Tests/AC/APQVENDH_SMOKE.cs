 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APQVENDH_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Reports/Inquiries", "xpath","//div[@class='navItem'][.='Accounts Payable Reports/Inquiries']").Click();
new Control("View Vendor History Inquiry", "xpath","//div[@class='navItem'][.='View Vendor History Inquiry']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APQVENDH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APQVENDH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on Vendor...", Logger.MessageType.INF);
			Control APQVENDH_Vendor = new Control("Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND']");
			CPCommon.AssertEqual(true,APQVENDH_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control APQVENDH_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VEND_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APQVENDH_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VEND_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APQVENDH_ChildForm);
IWebElement formBttn = APQVENDH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? APQVENDH_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
APQVENDH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


											Driver.SessionLogger.WriteLine("Vouchers");


												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_VouchersLink...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_VouchersLink = new Control("ChildForm_VouchersLink", "ID", "lnk_1005417_APQVENDH_VEND_CHILD");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_VouchersLink.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_VouchersLink);
APQVENDH_ChildForm_VouchersLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExist on ChildForm_VouchersFormTable...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_VouchersFormTable = new Control("ChildForm_VouchersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VCHRHDRHS_VENDVCHR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_VouchersFormTable.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming ClickButton on ChildForm_VouchersForm...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_VouchersForm = new Control("ChildForm_VouchersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VCHRHDRHS_VENDVCHR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_VouchersForm);
formBttn = APQVENDH_ChildForm_VouchersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APQVENDH_ChildForm_VouchersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APQVENDH_ChildForm_VouchersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.AssertEqual(true,APQVENDH_ChildForm_VouchersForm.Exists());

													
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_Vouchers_Vchr...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_Vouchers_Vchr = new Control("ChildForm_Vouchers_Vchr", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VCHRHDRHS_VENDVCHR_']/ancestor::form[1]/descendant::*[@id='VCHR_NO']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_Vouchers_Vchr.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_VouchersForm);
formBttn = APQVENDH_ChildForm_VouchersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Checks Disbursed");


												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_ChecksDisbursedLink...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_ChecksDisbursedLink = new Control("ChildForm_ChecksDisbursedLink", "ID", "lnk_1005414_APQVENDH_VEND_CHILD");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_ChecksDisbursedLink.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_ChecksDisbursedLink);
APQVENDH_ChildForm_ChecksDisbursedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_ChecksDisbursedForm...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_ChecksDisbursedForm = new Control("ChildForm_ChecksDisbursedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_CHECKDISBURSEDTOTAL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_ChecksDisbursedForm.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_ChecksDisbursed_TotalAllChecks...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_ChecksDisbursed_TotalAllChecks = new Control("ChildForm_ChecksDisbursed_TotalAllChecks", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_CHECKDISBURSEDTOTAL_']/ancestor::form[1]/descendant::*[@id='TOT_CHKS_PAID']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_ChecksDisbursed_TotalAllChecks.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExist on ChildForm_ChecksDisbursed_ChildFormTable...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_ChecksDisbursed_ChildFormTable = new Control("ChildForm_ChecksDisbursed_ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VENDCHK_CHECKDISBRSD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_ChecksDisbursed_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming ClickButton on ChildForm_ChecksDisbursed_ChildForm...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_ChecksDisbursed_ChildForm = new Control("ChildForm_ChecksDisbursed_ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VENDCHK_CHECKDISBRSD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_ChecksDisbursed_ChildForm);
formBttn = APQVENDH_ChildForm_ChecksDisbursed_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APQVENDH_ChildForm_ChecksDisbursed_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APQVENDH_ChildForm_ChecksDisbursed_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.AssertEqual(true,APQVENDH_ChildForm_ChecksDisbursed_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_ChecksDisbursed_ChildForm_CashAccount...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_ChecksDisbursed_ChildForm_CashAccount = new Control("ChildForm_ChecksDisbursed_ChildForm_CashAccount", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VENDCHK_CHECKDISBRSD_']/ancestor::form[1]/descendant::*[@id='CASH_ACCT_ID']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_ChecksDisbursed_ChildForm_CashAccount.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_ChecksDisbursedForm);
formBttn = APQVENDH_ChildForm_ChecksDisbursedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Address");


												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_AddressLink...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_AddressLink = new Control("ChildForm_AddressLink", "ID", "lnk_1005409_APQVENDH_VEND_CHILD");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_AddressLink.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_AddressLink);
APQVENDH_ChildForm_AddressLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExist on ChildForm_AddressFormTable...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_AddressFormTable = new Control("ChildForm_AddressFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VENDADDR_ADDRESS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_AddressFormTable.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming ClickButton on ChildForm_AddressForm...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_AddressForm = new Control("ChildForm_AddressForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VENDADDR_ADDRESS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_AddressForm);
formBttn = APQVENDH_ChildForm_AddressForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APQVENDH_ChildForm_AddressForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APQVENDH_ChildForm_AddressForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.AssertEqual(true,APQVENDH_ChildForm_AddressForm.Exists());

													
				CPCommon.CurrentComponent = "APQVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APQVENDH] Perfoming VerifyExists on ChildForm_Address_AddressCode...", Logger.MessageType.INF);
			Control APQVENDH_ChildForm_Address_AddressCode = new Control("ChildForm_Address_AddressCode", "xpath", "//div[translate(@id,'0123456789','')='pr__APQVENDH_VENDADDR_ADDRESS_']/ancestor::form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,APQVENDH_ChildForm_Address_AddressCode.Exists());

												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_ChildForm_AddressForm);
formBttn = APQVENDH_ChildForm_AddressForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "APQVENDH";
							CPCommon.WaitControlDisplayed(APQVENDH_MainForm);
formBttn = APQVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

