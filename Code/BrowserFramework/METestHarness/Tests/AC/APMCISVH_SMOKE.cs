 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMCISVH_SMOKE : TestScript
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
new Control("Payment Processing", "xpath","//div[@class='navItem'][.='Payment Processing']").Click();
new Control("Manage CIS Voucher History", "xpath","//div[@class='navItem'][.='Manage CIS Voucher History']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "APMCISVH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCISVH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMCISVH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMCISVH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMCISVH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCISVH] Perfoming VerifyExists on SelectEntityID_TaxEntityID...", Logger.MessageType.INF);
			Control APMCISVH_SelectEntityID_TaxEntityID = new Control("SelectEntityID_TaxEntityID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY_ID']");
			CPCommon.AssertEqual(true,APMCISVH_SelectEntityID_TaxEntityID.Exists());

												
				CPCommon.CurrentComponent = "APMCISVH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCISVH] Perfoming VerifyExist on CISVendorInformationFormTable...", Logger.MessageType.INF);
			Control APMCISVH_CISVendorInformationFormTable = new Control("CISVendorInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCISVH_VENDCISHS_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMCISVH_CISVendorInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMCISVH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCISVH] Perfoming ClickButton on CISVendorInformationForm...", Logger.MessageType.INF);
			Control APMCISVH_CISVendorInformationForm = new Control("CISVendorInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCISVH_VENDCISHS_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMCISVH_CISVendorInformationForm);
IWebElement formBttn = APMCISVH_CISVendorInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMCISVH_CISVendorInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMCISVH_CISVendorInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMCISVH";
							CPCommon.AssertEqual(true,APMCISVH_CISVendorInformationForm.Exists());

													
				CPCommon.CurrentComponent = "APMCISVH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCISVH] Perfoming VerifyExists on CISVendorInformtaion_CISDetails_CISVoucher...", Logger.MessageType.INF);
			Control APMCISVH_CISVendorInformtaion_CISDetails_CISVoucher = new Control("CISVendorInformtaion_CISDetails_CISVoucher", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCISVH_VENDCISHS_CTW_']/ancestor::form[1]/descendant::*[@id='CIS_VCHR_NO']");
			CPCommon.AssertEqual(true,APMCISVH_CISVendorInformtaion_CISDetails_CISVoucher.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APMCISVH";
							CPCommon.WaitControlDisplayed(APMCISVH_MainForm);
formBttn = APMCISVH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

