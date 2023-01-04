 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLRUNASN_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Financial Statement Configuration", "xpath","//div[@class='navItem'][.='Financial Statement Configuration']").Click();
new Control("Print Unassigned/Duplicate Account Report", "xpath","//div[@class='navItem'][.='Print Unassigned/Duplicate Account Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLRUNASN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLRUNASN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control GLRUNASN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,GLRUNASN_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "GLRUNASN";
							CPCommon.WaitControlDisplayed(GLRUNASN_MainForm);
IWebElement formBttn = GLRUNASN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLRUNASN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLRUNASN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLRUNASN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLRUNASN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Fscode");


												
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExists on FSCodeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control GLRUNASN_FSCodeNonContiguousRangesLink = new Control("FSCodeNonContiguousRangesLink", "ID", "lnk_1007471_GLRUNASN_PARAM");
			CPCommon.AssertEqual(true,GLRUNASN_FSCodeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "GLRUNASN";
							CPCommon.WaitControlDisplayed(GLRUNASN_FSCodeNonContiguousRangesLink);
GLRUNASN_FSCodeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExists on FSCodeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control GLRUNASN_FSCodeNonContiguousRangesForm = new Control("FSCodeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRFSCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLRUNASN_FSCodeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExist on FSCodeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control GLRUNASN_FSCodeNonContiguousRangesFormTable = new Control("FSCodeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRFSCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLRUNASN_FSCodeNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLRUNASN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRUNASN] Perfoming VerifyExists on FSCodeNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control GLRUNASN_FSCodeNonContiguousRanges_Ok = new Control("FSCodeNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRFSCD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,GLRUNASN_FSCodeNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "GLRUNASN";
							CPCommon.WaitControlDisplayed(GLRUNASN_FSCodeNonContiguousRangesForm);
formBttn = GLRUNASN_FSCodeNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLRUNASN";
							CPCommon.WaitControlDisplayed(GLRUNASN_MainForm);
formBttn = GLRUNASN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

