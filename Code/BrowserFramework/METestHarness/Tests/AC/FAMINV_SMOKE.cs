 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMINV_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Inventory Records", "xpath","//div[@class='navItem'][.='Inventory Records']").Click();
new Control("Manage Asset Inventory Information", "xpath","//div[@class='navItem'][.='Manage Asset Inventory Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMINV] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMINV_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMINV_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMINV] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMINV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMINV_MainForm);
IWebElement formBttn = FAMINV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMINV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMINV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "FAMINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMINV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMINV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMINV_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Inventory Details");


												
				CPCommon.CurrentComponent = "FAMINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMINV] Perfoming VerifyExist on InventoryDetailsFormTable...", Logger.MessageType.INF);
			Control FAMINV_InventoryDetailsFormTable = new Control("InventoryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMINV_FATRACKING_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMINV_InventoryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMINV] Perfoming ClickButton on InventoryDetailsForm...", Logger.MessageType.INF);
			Control FAMINV_InventoryDetailsForm = new Control("InventoryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMINV_FATRACKING_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMINV_InventoryDetailsForm);
formBttn = FAMINV_InventoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMINV_InventoryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMINV_InventoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMINV] Perfoming VerifyExists on InventoryDetails_Date...", Logger.MessageType.INF);
			Control FAMINV_InventoryDetails_Date = new Control("InventoryDetails_Date", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMINV_FATRACKING_DTL_']/ancestor::form[1]/descendant::*[@id='TRN_DT']");
			CPCommon.AssertEqual(true,FAMINV_InventoryDetails_Date.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMINV";
							CPCommon.WaitControlDisplayed(FAMINV_MainForm);
formBttn = FAMINV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

