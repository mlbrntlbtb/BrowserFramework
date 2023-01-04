 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLRRBIL_SMOKE : TestScript
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
new Control("Print Retroactive Billing Worksheet", "xpath","//div[@class='navItem'][.='Print Retroactive Billing Worksheet']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLRRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRRBIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLRRBIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLRRBIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLRRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRRBIL] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLRRBIL_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLRRBIL_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLRRBIL";
							CPCommon.WaitControlDisplayed(BLRRBIL_MainForm);
IWebElement formBttn = BLRRBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLRRBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLRRBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLRRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRRBIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLRRBIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRRBIL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT NON-CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLRRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRRBIL] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLRRBIL_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2716_BLRRBIL_WFUNCPARMCATLG_HDR");
			CPCommon.AssertEqual(true,BLRRBIL_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLRRBIL";
							CPCommon.WaitControlDisplayed(BLRRBIL_ProjectNonContiguousRangesLink);
BLRRBIL_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLRRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRRBIL] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLRRBIL_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRRBIL_NONCONTIGUOUS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRRBIL_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLRRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRRBIL] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLRRBIL_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRRBIL_NONCONTIGUOUS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLRRBIL_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLRRBIL";
							CPCommon.WaitControlDisplayed(BLRRBIL_ProjectNonContiguousRangesForm);
formBttn = BLRRBIL_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLRRBIL";
							CPCommon.WaitControlDisplayed(BLRRBIL_MainForm);
formBttn = BLRRBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

