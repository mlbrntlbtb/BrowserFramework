 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMWHSE_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Controls", "xpath","//div[@class='navItem'][.='Inventory Controls']").Click();
new Control("Manage WAREHOUSES", "xpath","//div[@class='navItem'][.='Manage WAREHOUSES']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMWHSE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMWHSE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on Identification_WAREHOUSEMID...", Logger.MessageType.INF);
			Control INMWHSE_Identification_WAREHOUSEMID = new Control("Identification_WAREHOUSEMID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,INMWHSE_Identification_WAREHOUSEMID.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
							CPCommon.WaitControlDisplayed(INMWHSE_MainForm);
IWebElement formBttn = INMWHSE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INMWHSE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INMWHSE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INMWHSE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMWHSE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Transaction IDs");


												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on Identification_TransactionIDsLink...", Logger.MessageType.INF);
			Control INMWHSE_Identification_TransactionIDsLink = new Control("Identification_TransactionIDsLink", "ID", "lnk_1002203_INMWHSE_WHSE_WAREHOUSES");
			CPCommon.AssertEqual(true,INMWHSE_Identification_TransactionIDsLink.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
							CPCommon.WaitControlDisplayed(INMWHSE_Identification_TransactionIDsLink);
INMWHSE_Identification_TransactionIDsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExist on LeftTransactionIDTable...", Logger.MessageType.INF);
			Control INMWHSE_LeftTransactionIDTable = new Control("LeftTransactionIDTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INMWHSE_SINVTTRNTYPE_TRANSID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMWHSE_LeftTransactionIDTable.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on LeftTransactionIDForm...", Logger.MessageType.INF);
			Control INMWHSE_LeftTransactionIDForm = new Control("LeftTransactionIDForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMWHSE_SINVTTRNTYPE_TRANSID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMWHSE_LeftTransactionIDForm.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExist on RightTransactionIDTable...", Logger.MessageType.INF);
			Control INMWHSE_RightTransactionIDTable = new Control("RightTransactionIDTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INMWHSE_WHSELASTTRANS_SELTRANS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMWHSE_RightTransactionIDTable.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on RightTransactionIDForm...", Logger.MessageType.INF);
			Control INMWHSE_RightTransactionIDForm = new Control("RightTransactionIDForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMWHSE_WHSELASTTRANS_SELTRANS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMWHSE_RightTransactionIDForm.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
							CPCommon.WaitControlDisplayed(INMWHSE_RightTransactionIDForm);
formBttn = INMWHSE_RightTransactionIDForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Location Structure");


												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on Identification_LocationStructureLink...", Logger.MessageType.INF);
			Control INMWHSE_Identification_LocationStructureLink = new Control("Identification_LocationStructureLink", "ID", "lnk_1007598_INMWHSE_WHSE_WAREHOUSES");
			CPCommon.AssertEqual(true,INMWHSE_Identification_LocationStructureLink.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
							CPCommon.WaitControlDisplayed(INMWHSE_Identification_LocationStructureLink);
INMWHSE_Identification_LocationStructureLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExist on LocationStructureTable...", Logger.MessageType.INF);
			Control INMWHSE_LocationStructureTable = new Control("LocationStructureTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INMWHSE_WHSELOCSTRUC_LOCSTRUC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMWHSE_LocationStructureTable.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWHSE] Perfoming VerifyExists on LocationStructureForm...", Logger.MessageType.INF);
			Control INMWHSE_LocationStructureForm = new Control("LocationStructureForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMWHSE_WHSELOCSTRUC_LOCSTRUC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMWHSE_LocationStructureForm.Exists());

												
				CPCommon.CurrentComponent = "INMWHSE";
							CPCommon.WaitControlDisplayed(INMWHSE_LocationStructureForm);
formBttn = INMWHSE_LocationStructureForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMWHSE";
							CPCommon.WaitControlDisplayed(INMWHSE_MainForm);
formBttn = INMWHSE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

