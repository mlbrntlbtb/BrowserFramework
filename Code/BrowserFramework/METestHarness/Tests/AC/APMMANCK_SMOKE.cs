 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMMANCK_SMOKE : TestScript
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
new Control("Manage Manual Checks", "xpath","//div[@class='navItem'][.='Manage Manual Checks']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMMANCK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMMANCK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APMMANCK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APMMANCK_MainForm);
IWebElement formBttn = APMMANCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMMANCK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMMANCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMMANCK";
							CPCommon.AssertEqual(true,APMMANCK_MainForm.Exists());

													
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming VerifyExists on Identification_PayVendor...", Logger.MessageType.INF);
			Control APMMANCK_Identification_PayVendor = new Control("Identification_PayVendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,APMMANCK_Identification_PayVendor.Exists());

											Driver.SessionLogger.WriteLine("OPEN VOUCHERS");


												
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming VerifyExists on OpenVouchersForm...", Logger.MessageType.INF);
			Control APMMANCK_OpenVouchersForm = new Control("OpenVouchersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMMANCK_OPENAP_OPENVCHRS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMMANCK_OpenVouchersForm.Exists());

												
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming VerifyExist on OpenVouchersFormTable...", Logger.MessageType.INF);
			Control APMMANCK_OpenVouchersFormTable = new Control("OpenVouchersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMMANCK_OPENAP_OPENVCHRS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMMANCK_OpenVouchersFormTable.Exists());

											Driver.SessionLogger.WriteLine("SELECTED VOUCHERS");


												
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming VerifyExists on SelectedVouchersForm...", Logger.MessageType.INF);
			Control APMMANCK_SelectedVouchersForm = new Control("SelectedVouchersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMMANCK_OPENAP_SELECTEDVCHRS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMMANCK_SelectedVouchersForm.Exists());

												
				CPCommon.CurrentComponent = "APMMANCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMMANCK] Perfoming VerifyExist on SelectedVouchersFormTable...", Logger.MessageType.INF);
			Control APMMANCK_SelectedVouchersFormTable = new Control("SelectedVouchersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMMANCK_OPENAP_SELECTEDVCHRS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMMANCK_SelectedVouchersFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APMMANCK";
							CPCommon.WaitControlDisplayed(APMMANCK_MainForm);
formBttn = APMMANCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

