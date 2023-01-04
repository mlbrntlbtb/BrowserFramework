 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPEPVN_SMOKE : TestScript
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
new Control("Export eProcurement Vendors", "xpath","//div[@class='navItem'][.='Export eProcurement Vendors']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPEPVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPVN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPEPVN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPEPVN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPEPVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPVN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPEPVN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPEPVN_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPEPVN";
							CPCommon.WaitControlDisplayed(AOPEPVN_MainForm);
IWebElement formBttn = AOPEPVN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPEPVN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPEPVN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPEPVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPVN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPEPVN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPVN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("VENDORNONCONTIGUOUSRANGES");


												
				CPCommon.CurrentComponent = "AOPEPVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPVN] Perfoming Click on VendorNonContiguousRangesLink...", Logger.MessageType.INF);
			Control AOPEPVN_VendorNonContiguousRangesLink = new Control("VendorNonContiguousRangesLink", "ID", "lnk_4988_AOPEPVN_PARAM");
			CPCommon.WaitControlDisplayed(AOPEPVN_VendorNonContiguousRangesLink);
AOPEPVN_VendorNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOPEPVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPVN] Perfoming VerifyExist on VendorNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control AOPEPVN_VendorNonContiguousRangesFormTable = new Control("VendorNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRVENDID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPVN_VendorNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPEPVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPVN] Perfoming Close on VendorNonContiguousRangesForm...", Logger.MessageType.INF);
			Control AOPEPVN_VendorNonContiguousRangesForm = new Control("VendorNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRVENDID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOPEPVN_VendorNonContiguousRangesForm);
formBttn = AOPEPVN_VendorNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "AOPEPVN";
							CPCommon.WaitControlDisplayed(AOPEPVN_MainForm);
formBttn = AOPEPVN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

