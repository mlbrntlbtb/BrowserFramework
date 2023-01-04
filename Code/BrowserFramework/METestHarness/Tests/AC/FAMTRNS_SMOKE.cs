 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMTRNS_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Transfer Records", "xpath","//div[@class='navItem'][.='Transfer Records']").Click();
new Control("Manage Asset Transfer Information", "xpath","//div[@class='navItem'][.='Manage Asset Transfer Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAMTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTRNS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMTRNS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMTRNS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTRNS] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMTRNS_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMTRNS_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMTRNS";
							CPCommon.WaitControlDisplayed(FAMTRNS_MainForm);
IWebElement formBttn = FAMTRNS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMTRNS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMTRNS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FAMTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTRNS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMTRNS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMTRNS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("TRANSFER DETAILS FORM");


												
				CPCommon.CurrentComponent = "FAMTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTRNS] Perfoming VerifyExists on TransferDetailsForm...", Logger.MessageType.INF);
			Control FAMTRNS_TransferDetailsForm = new Control("TransferDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMTRNS_FATRACKING_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMTRNS_TransferDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "FAMTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTRNS] Perfoming VerifyExist on TransferDetailsFormTable...", Logger.MessageType.INF);
			Control FAMTRNS_TransferDetailsFormTable = new Control("TransferDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMTRNS_FATRACKING_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMTRNS_TransferDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMTRNS";
							CPCommon.WaitControlDisplayed(FAMTRNS_TransferDetailsForm);
formBttn = FAMTRNS_TransferDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMTRNS_TransferDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMTRNS_TransferDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAMTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMTRNS] Perfoming VerifyExists on TransferDetails_Date...", Logger.MessageType.INF);
			Control FAMTRNS_TransferDetails_Date = new Control("TransferDetails_Date", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMTRNS_FATRACKING_DTL_']/ancestor::form[1]/descendant::*[@id='TRN_DT']");
			CPCommon.AssertEqual(true,FAMTRNS_TransferDetails_Date.Exists());

												
				CPCommon.CurrentComponent = "FAMTRNS";
							CPCommon.WaitControlDisplayed(FAMTRNS_MainForm);
formBttn = FAMTRNS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

