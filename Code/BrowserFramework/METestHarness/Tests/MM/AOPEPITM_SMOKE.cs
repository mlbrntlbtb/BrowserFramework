 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPEPITM_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchasing Interfaces", "xpath","//div[@class='navItem'][.='Purchasing Interfaces']").Click();
new Control("Export eProcurement Items and Line Charge Types", "xpath","//div[@class='navItem'][.='Export eProcurement Items and Line Charge Types']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPEPITM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPITM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPEPITM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPEPITM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPEPITM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPITM] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPEPITM_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPEPITM_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPEPITM";
							CPCommon.WaitControlDisplayed(AOPEPITM_MainForm);
IWebElement formBttn = AOPEPITM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPEPITM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPEPITM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPEPITM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPITM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPEPITM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPITM_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPEPITM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPITM] Perfoming Click on ItemNonContiguousRangesLink...", Logger.MessageType.INF);
			Control AOPEPITM_ItemNonContiguousRangesLink = new Control("ItemNonContiguousRangesLink", "ID", "lnk_1006067_AOPEPITM_PARAM");
			CPCommon.WaitControlDisplayed(AOPEPITM_ItemNonContiguousRangesLink);
AOPEPITM_ItemNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOPEPITM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPITM] Perfoming VerifyExist on ItemNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control AOPEPITM_ItemNonContiguousRangesFormTable = new Control("ItemNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRITEMID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPITM_ItemNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPEPITM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPITM] Perfoming Close on ItemNonContiguousRangesForm...", Logger.MessageType.INF);
			Control AOPEPITM_ItemNonContiguousRangesForm = new Control("ItemNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRITEMID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOPEPITM_ItemNonContiguousRangesForm);
formBttn = AOPEPITM_ItemNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "AOPEPITM";
							CPCommon.WaitControlDisplayed(AOPEPITM_MainForm);
formBttn = AOPEPITM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

