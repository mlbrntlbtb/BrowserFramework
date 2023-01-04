 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLRACRN_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Standard Bills Processing", "xpath","//div[@class='navItem'][.='Standard Bills Processing']").Click();
new Control("Print ACRN Billing Edit Report", "xpath","//div[@class='navItem'][.='Print ACRN Billing Edit Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLRACRN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLRACRN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLRACRN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLRACRN_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLRACRN";
							CPCommon.WaitControlDisplayed(BLRACRN_MainForm);
IWebElement formBttn = BLRACRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLRACRN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLRACRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLRACRN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRACRN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT NON-CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExists on ProjectACRNNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLRACRN_ProjectACRNNonContiguousRangesLink = new Control("ProjectACRNNonContiguousRangesLink", "ID", "lnk_3193_BLRACRN_PARAM");
			CPCommon.AssertEqual(true,BLRACRN_ProjectACRNNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLRACRN";
							CPCommon.WaitControlDisplayed(BLRACRN_ProjectACRNNonContiguousRangesLink);
BLRACRN_ProjectACRNNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExist on ProjectACRNNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLRACRN_ProjectACRNNonContiguousRangesFormTable = new Control("ProjectACRNNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRACRN_NONCONTIGUOUS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRACRN_ProjectACRNNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExists on ProjectACRNNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLRACRN_ProjectACRNNonContiguousRangesForm = new Control("ProjectACRNNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRACRN_NONCONTIGUOUS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLRACRN_ProjectACRNNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLRACRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRACRN] Perfoming VerifyExists on ProjectACRNNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLRACRN_ProjectACRNNonContiguousRanges_Ok = new Control("ProjectACRNNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRACRN_NONCONTIGUOUS_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLRACRN_ProjectACRNNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLRACRN";
							CPCommon.WaitControlDisplayed(BLRACRN_ProjectACRNNonContiguousRangesForm);
formBttn = BLRACRN_ProjectACRNNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLRACRN";
							CPCommon.WaitControlDisplayed(BLRACRN_MainForm);
formBttn = BLRACRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

