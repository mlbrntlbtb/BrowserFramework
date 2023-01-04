 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INPUID_SMOKE : TestScript
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
new Control("Inventory Utilities", "xpath","//div[@class='navItem'][.='Inventory Utilities']").Click();
new Control("Create UID Print File", "xpath","//div[@class='navItem'][.='Create UID Print File']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "INPUID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPUID] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INPUID_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INPUID_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INPUID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPUID] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control INPUID_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,INPUID_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "INPUID";
							CPCommon.WaitControlDisplayed(INPUID_MainForm);
IWebElement formBttn = INPUID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INPUID_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INPUID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INPUID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPUID] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INPUID_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INPUID_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("UID Non Contiguous Ranges");


												
				CPCommon.CurrentComponent = "INPUID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPUID] Perfoming VerifyExists on UIDNonContiguousRangesLink...", Logger.MessageType.INF);
			Control INPUID_UIDNonContiguousRangesLink = new Control("UIDNonContiguousRangesLink", "ID", "lnk_5465_INPUID_PARAM");
			CPCommon.AssertEqual(true,INPUID_UIDNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "INPUID";
							CPCommon.WaitControlDisplayed(INPUID_UIDNonContiguousRangesLink);
INPUID_UIDNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INPUID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPUID] Perfoming VerifyExist on UIDNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control INPUID_UIDNonContiguousRangesFormTable = new Control("UIDNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INPUID_NCR_UID_CD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INPUID_UIDNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "INPUID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPUID] Perfoming Close on UIDNonContiguousRangesForm...", Logger.MessageType.INF);
			Control INPUID_UIDNonContiguousRangesForm = new Control("UIDNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INPUID_NCR_UID_CD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INPUID_UIDNonContiguousRangesForm);
formBttn = INPUID_UIDNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "INPUID";
							CPCommon.WaitControlDisplayed(INPUID_MainForm);
formBttn = INPUID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

