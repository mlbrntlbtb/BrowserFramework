 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POPSCVCH_SMOKE : TestScript
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
new Control("Voucher Processing", "xpath","//div[@class='navItem'][.='Voucher Processing']").Click();
new Control("Create Subcontract Purchase Order Vouchers", "xpath","//div[@class='navItem'][.='Create Subcontract Purchase Order Vouchers']").Click();


												
				CPCommon.CurrentComponent = "POPSCVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPSCVCH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POPSCVCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POPSCVCH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POPSCVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPSCVCH] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control POPSCVCH_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,POPSCVCH_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "POPSCVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPSCVCH] Perfoming VerifyExists on SelectionRanges_Option_Vendor...", Logger.MessageType.INF);
			Control POPSCVCH_SelectionRanges_Option_Vendor = new Control("SelectionRanges_Option_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEL_VEND']");
			CPCommon.AssertEqual(true,POPSCVCH_SelectionRanges_Option_Vendor.Exists());

												
				CPCommon.CurrentComponent = "POPSCVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPSCVCH] Perfoming VerifyExists on Options_IncludeNegativeVoucherLineAmounts...", Logger.MessageType.INF);
			Control POPSCVCH_Options_IncludeNegativeVoucherLineAmounts = new Control("Options_IncludeNegativeVoucherLineAmounts", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INC_NEG']");
			CPCommon.AssertEqual(true,POPSCVCH_Options_IncludeNegativeVoucherLineAmounts.Exists());

												
				CPCommon.CurrentComponent = "POPSCVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPSCVCH] Perfoming VerifyExists on Generate_InvoiceNumber_InvoiceNumber...", Logger.MessageType.INF);
			Control POPSCVCH_Generate_InvoiceNumber_InvoiceNumber = new Control("Generate_InvoiceNumber_InvoiceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_NO']");
			CPCommon.AssertEqual(true,POPSCVCH_Generate_InvoiceNumber_InvoiceNumber.Exists());

												
				CPCommon.CurrentComponent = "POPSCVCH";
							CPCommon.WaitControlDisplayed(POPSCVCH_MainForm);
IWebElement formBttn = POPSCVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

