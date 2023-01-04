 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMVENDA_SMOKE : TestScript
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
new Control("Vendors", "xpath","//div[@class='navItem'][.='Vendors']").Click();
new Control("Approve Vendors", "xpath","//div[@class='navItem'][.='Approve Vendors']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "APMVENDA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMVENDA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMVENDA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDA] Perfoming VerifyExists on ApproveAll...", Logger.MessageType.INF);
			Control APMVENDA_ApproveAll = new Control("ApproveAll", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'APPRV_ALL') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,APMVENDA_ApproveAll.Exists());

												
				CPCommon.CurrentComponent = "APMVENDA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDA] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control APMVENDA_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVENDA_MainTable.Exists());

											Driver.SessionLogger.WriteLine("PAY VENDOR");


												
				CPCommon.CurrentComponent = "APMVENDA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDA] Perfoming VerifyExists on PayVendorForm...", Logger.MessageType.INF);
			Control APMVENDA_PayVendorForm = new Control("PayVendorForm", "xpath", "//div[translate(@id,'0123456789','')='ph__APMVENDA_PAYVEND_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDA_PayVendorForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDA] Perfoming VerifyExists on ChildForm_PayVendor_PayVendor...", Logger.MessageType.INF);
			Control APMVENDA_ChildForm_PayVendor_PayVendor = new Control("ChildForm_PayVendor_PayVendor", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDA_PAYVEND_CTW_']/ancestor::form[1]/descendant::*[@id='PAY_VEND']");
			CPCommon.AssertEqual(true,APMVENDA_ChildForm_PayVendor_PayVendor.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APMVENDA";
							CPCommon.WaitControlDisplayed(APMVENDA_MainForm);
IWebElement formBttn = APMVENDA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

