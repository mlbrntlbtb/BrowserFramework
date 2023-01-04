 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPTOOL4_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Utilities", "xpath","//div[@class='navItem'][.='Accounts Payable Utilities']").Click();
new Control("Update Vendor Default Account Descriptions", "xpath","//div[@class='navItem'][.='Update Vendor Default Account Descriptions']").Click();


												
				CPCommon.CurrentComponent = "APPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL4] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPTOOL4_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPTOOL4_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL4] Perfoming VerifyExists on Option_Vendor...", Logger.MessageType.INF);
			Control APPTOOL4_Option_Vendor = new Control("Option_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VENDOR_RANGE']");
			CPCommon.AssertEqual(true,APPTOOL4_Option_Vendor.Exists());

											Driver.SessionLogger.WriteLine("VENDOR NON-CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "APPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL4] Perfoming Click on VendorNonContiguousRangesLink...", Logger.MessageType.INF);
			Control APPTOOL4_VendorNonContiguousRangesLink = new Control("VendorNonContiguousRangesLink", "ID", "lnk_3683_APPTOOL4_VENDDFLTACC_HDR");
			CPCommon.WaitControlDisplayed(APPTOOL4_VendorNonContiguousRangesLink);
APPTOOL4_VendorNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "APPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL4] Perfoming VerifyExists on VendorNonContiguousRangesForm...", Logger.MessageType.INF);
			Control APPTOOL4_VendorNonContiguousRangesForm = new Control("VendorNonContiguousRangesForm", "xpath", "//div[starts-with(@id,'pr__APPTOOL4_NCRVENDID_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,APPTOOL4_VendorNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL4] Perfoming VerifyExist on VendorNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control APPTOOL4_VendorNonContiguousRangesFormTable = new Control("VendorNonContiguousRangesFormTable", "xpath", "//div[starts-with(@id,'pr__APPTOOL4_NCRVENDID_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPTOOL4_VendorNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL4";
							CPCommon.WaitControlDisplayed(APPTOOL4_VendorNonContiguousRangesForm);
IWebElement formBttn = APPTOOL4_VendorNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APPTOOL4";
							CPCommon.WaitControlDisplayed(APPTOOL4_MainForm);
formBttn = APPTOOL4_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

