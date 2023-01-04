 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAPTOOL7_SMOKE : TestScript
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
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("Copy Transfer Data to Asset Audit Log", "xpath","//div[@class='navItem'][.='Copy Transfer Data to Asset Audit Log']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAPTOOL7";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL7] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAPTOOL7_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAPTOOL7_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAPTOOL7";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL7] Perfoming VerifyExists on Start_AssetItemNumbers_AssetNo...", Logger.MessageType.INF);
			Control FAPTOOL7_Start_AssetItemNumbers_AssetNo = new Control("Start_AssetItemNumbers_AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_NO_FR']");
			CPCommon.AssertEqual(true,FAPTOOL7_Start_AssetItemNumbers_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAPTOOL7";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL7] Perfoming VerifyExists on SelectTransferDataToCopyBasedOnMappingOfDataElementsForm...", Logger.MessageType.INF);
			Control FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElementsForm = new Control("SelectTransferDataToCopyBasedOnMappingOfDataElementsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPTOOL_XFERLOG_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElementsForm.Exists());

												
				CPCommon.CurrentComponent = "FAPTOOL7";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL7] Perfoming VerifyExists on SelectTransferDataToCopyBasedOnMappingOfDataElements_SelectTransferDataColumnContainingTransferDataColumnName...", Logger.MessageType.INF);
			Control FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElements_SelectTransferDataColumnContainingTransferDataColumnName = new Control("SelectTransferDataToCopyBasedOnMappingOfDataElements_SelectTransferDataColumnContainingTransferDataColumnName", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPTOOL_XFERLOG_CHLD_']/ancestor::form[1]/descendant::*[@id='MAPPING_DATA']");
			CPCommon.AssertEqual(true,FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElements_SelectTransferDataColumnContainingTransferDataColumnName.Exists());

												
				CPCommon.CurrentComponent = "FAPTOOL7";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL7] Perfoming VerifyExists on SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingForm...", Logger.MessageType.INF);
			Control FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingForm = new Control("SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPTOOL_XFERLOG_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingForm.Exists());

												
				CPCommon.CurrentComponent = "FAPTOOL7";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL7] Perfoming VerifyExist on SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingFormTable...", Logger.MessageType.INF);
			Control FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingFormTable = new Control("SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPTOOL_XFERLOG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPTOOL7_SelectTransferDataToCopyBasedOnMappingOfDataElements_DataElementsMappingFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "FAPTOOL7";
							CPCommon.WaitControlDisplayed(FAPTOOL7_MainForm);
IWebElement formBttn = FAPTOOL7_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

