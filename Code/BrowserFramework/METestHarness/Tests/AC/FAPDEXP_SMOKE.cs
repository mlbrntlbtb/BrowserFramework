 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAPDEXP_SMOKE : TestScript
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
new Control("Fixed Assets Interfaces", "xpath","//div[@class='navItem'][.='Fixed Assets Interfaces']").Click();
new Control("Export Asset Disposals", "xpath","//div[@class='navItem'][.='Export Asset Disposals']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAPDEXP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAPDEXP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control FAPDEXP_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,FAPDEXP_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
							CPCommon.WaitControlDisplayed(FAPDEXP_MainForm);
IWebElement formBttn = FAPDEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAPDEXP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAPDEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAPDEXP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPDEXP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
							CPCommon.WaitControlDisplayed(FAPDEXP_MainForm);
formBttn = FAPDEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAPDEXP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAPDEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExists on AssetItemNumberNonContiguousLink...", Logger.MessageType.INF);
			Control FAPDEXP_AssetItemNumberNonContiguousLink = new Control("AssetItemNumberNonContiguousLink", "ID", "lnk_5606_FAPDEXP_PARAM");
			CPCommon.AssertEqual(true,FAPDEXP_AssetItemNumberNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecordsLink...", Logger.MessageType.INF);
			Control FAPDEXP_AssetMasterAndDisposalDataRecordsLink = new Control("AssetMasterAndDisposalDataRecordsLink", "ID", "lnk_5574_FAPDEXP_PARAM");
			CPCommon.AssertEqual(true,FAPDEXP_AssetMasterAndDisposalDataRecordsLink.Exists());

											Driver.SessionLogger.WriteLine("Asset/Item Number Non-Contiguous");


												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExists on AssetItemNumberNonContiguousForm...", Logger.MessageType.INF);
			Control FAPDEXP_AssetItemNumberNonContiguousForm = new Control("AssetItemNumberNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPDEXP_NCR_ASSETID_ITEMNO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPDEXP_AssetItemNumberNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExist on AssetItemNumberNonContiguousFormTable...", Logger.MessageType.INF);
			Control FAPDEXP_AssetItemNumberNonContiguousFormTable = new Control("AssetItemNumberNonContiguousFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPDEXP_NCR_ASSETID_ITEMNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPDEXP_AssetItemNumberNonContiguousFormTable.Exists());

											Driver.SessionLogger.WriteLine("Asset Master and Disposal Data Records");


												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExists on AssetMasterAndDisposalDataRecordsForm...", Logger.MessageType.INF);
			Control FAPDEXP_AssetMasterAndDisposalDataRecordsForm = new Control("AssetMasterAndDisposalDataRecordsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPDEXP_LN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAPDEXP_AssetMasterAndDisposalDataRecordsForm.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPDEXP] Perfoming VerifyExist on AssetMasterAndDisposalDataRecordsFormTable...", Logger.MessageType.INF);
			Control FAPDEXP_AssetMasterAndDisposalDataRecordsFormTable = new Control("AssetMasterAndDisposalDataRecordsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAPDEXP_LN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPDEXP_AssetMasterAndDisposalDataRecordsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPDEXP";
							CPCommon.WaitControlDisplayed(FAPDEXP_MainForm);
formBttn = FAPDEXP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

