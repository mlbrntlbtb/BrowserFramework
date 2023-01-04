 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLR1034_SMOKE : TestScript
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
new Control("Print Form 1034", "xpath","//div[@class='navItem'][.='Print Form 1034']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLR1034_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLR1034_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLR1034_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLR1034_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLR1034";
							CPCommon.WaitControlDisplayed(BLR1034_MainForm);
IWebElement formBttn = BLR1034_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLR1034_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLR1034_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLR1034_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLR1034_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT NON-CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLR1034_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1004580_BLR1034_PARAM");
			CPCommon.AssertEqual(true,BLR1034_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLR1034";
							CPCommon.WaitControlDisplayed(BLR1034_ProjectNonContiguousRangesLink);
BLR1034_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLR1034_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLR1034_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLR1034_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLR1034_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLR1034";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLR1034] Perfoming VerifyExists on ProjectNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLR1034_ProjectNonContiguousRanges_Ok = new Control("ProjectNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLR1034_ProjectNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLR1034";
							CPCommon.WaitControlDisplayed(BLR1034_ProjectNonContiguousRangesForm);
formBttn = BLR1034_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLR1034";
							CPCommon.WaitControlDisplayed(BLR1034_MainForm);
formBttn = BLR1034_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

