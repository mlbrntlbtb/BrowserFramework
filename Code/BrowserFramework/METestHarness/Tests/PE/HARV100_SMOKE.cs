 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HARV100_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Affirmative Action", "xpath","//div[@class='deptItem'][.='Affirmative Action']").Click();
new Control("Affirmative Action Reports", "xpath","//div[@class='navItem'][.='Affirmative Action Reports']").Click();
new Control("Print VETS-4212 Report", "xpath","//div[@class='navItem'][.='Print VETS-4212 Report']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HARV100";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARV100] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HARV100_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HARV100_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HARV100";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARV100] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HARV100_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HARV100_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HARV100";
							CPCommon.WaitControlDisplayed(HARV100_MainForm);
IWebElement formBttn = HARV100_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HARV100_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HARV100_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HARV100";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARV100] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HARV100_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HARV100_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Non-Contiguous Labor Locations Form");


												
				CPCommon.CurrentComponent = "HARV100";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARV100] Perfoming VerifyExists on NonContiguousLaborLocationsLink...", Logger.MessageType.INF);
			Control HARV100_NonContiguousLaborLocationsLink = new Control("NonContiguousLaborLocationsLink", "ID", "lnk_4251_HARV100_PARAM");
			CPCommon.AssertEqual(true,HARV100_NonContiguousLaborLocationsLink.Exists());

												
				CPCommon.CurrentComponent = "HARV100";
							CPCommon.WaitControlDisplayed(HARV100_NonContiguousLaborLocationsLink);
HARV100_NonContiguousLaborLocationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HARV100";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARV100] Perfoming VerifyExists on NonContiguousLaborLocationsForm...", Logger.MessageType.INF);
			Control HARV100_NonContiguousLaborLocationsForm = new Control("NonContiguousLaborLocationsForm", "xpath", "//div[starts-with(@id,'pr__HARV100_NCR_NCRLABLOCCD_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HARV100_NonContiguousLaborLocationsForm.Exists());

												
				CPCommon.CurrentComponent = "HARV100";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARV100] Perfoming VerifyExist on NonContiguousLaborLocationsFormTable...", Logger.MessageType.INF);
			Control HARV100_NonContiguousLaborLocationsFormTable = new Control("NonContiguousLaborLocationsFormTable", "xpath", "//div[starts-with(@id,'pr__HARV100_NCR_NCRLABLOCCD_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HARV100_NonContiguousLaborLocationsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HARV100";
							CPCommon.WaitControlDisplayed(HARV100_MainForm);
formBttn = HARV100_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

