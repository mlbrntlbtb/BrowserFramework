 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLRPBIL_SMOKE : TestScript
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
new Control("Progress Payment Bills Processing", "xpath","//div[@class='navItem'][.='Progress Payment Bills Processing']").Click();
new Control("Print Progress Payment Bills", "xpath","//div[@class='navItem'][.='Print Progress Payment Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLRPBIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLRPBIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLRPBIL_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLRPBIL_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLRPBIL";
							CPCommon.WaitControlDisplayed(BLRPBIL_MainForm);
IWebElement formBttn = BLRPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLRPBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLRPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLRPBIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRPBIL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT NON-CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLRPBIL_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1004581_BLRPBIL_PARAM");
			CPCommon.AssertEqual(true,BLRPBIL_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLRPBIL";
							CPCommon.WaitControlDisplayed(BLRPBIL_ProjectNonContiguousRangesLink);
BLRPBIL_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLRPBIL_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRPBIL_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLRPBIL_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLRPBIL_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLRPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRPBIL] Perfoming VerifyExists on ProjectNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLRPBIL_ProjectNonContiguousRanges_Ok = new Control("ProjectNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLRPBIL_ProjectNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLRPBIL";
							CPCommon.WaitControlDisplayed(BLRPBIL_ProjectNonContiguousRangesForm);
formBttn = BLRPBIL_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLRPBIL";
							CPCommon.WaitControlDisplayed(BLRPBIL_MainForm);
formBttn = BLRPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

