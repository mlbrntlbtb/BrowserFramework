 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HAREEO1_SMOKE : TestScript
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
new Control("Print EEO-1 Report", "xpath","//div[@class='navItem'][.='Print EEO-1 Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HAREEO1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAREEO1] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HAREEO1_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HAREEO1_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HAREEO1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAREEO1] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HAREEO1_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HAREEO1_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HAREEO1";
							CPCommon.WaitControlDisplayed(HAREEO1_MainForm);
IWebElement formBttn = HAREEO1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HAREEO1_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HAREEO1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HAREEO1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAREEO1] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HAREEO1_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAREEO1_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HAREEO1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAREEO1] Perfoming VerifyExists on NonContiguousLaborLocationsLink...", Logger.MessageType.INF);
			Control HAREEO1_NonContiguousLaborLocationsLink = new Control("NonContiguousLaborLocationsLink", "ID", "lnk_4118_HAREEO1_PARAM");
			CPCommon.AssertEqual(true,HAREEO1_NonContiguousLaborLocationsLink.Exists());

												
				CPCommon.CurrentComponent = "HAREEO1";
							CPCommon.WaitControlDisplayed(HAREEO1_NonContiguousLaborLocationsLink);
HAREEO1_NonContiguousLaborLocationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HAREEO1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAREEO1] Perfoming VerifyExist on NonContiguousLaborLocationsFormTable...", Logger.MessageType.INF);
			Control HAREEO1_NonContiguousLaborLocationsFormTable = new Control("NonContiguousLaborLocationsFormTable", "xpath", "//div[starts-with(@id,'pr__HAREEO1_NCRLAGLOCCD_NCR_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAREEO1_NonContiguousLaborLocationsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HAREEO1";
							CPCommon.WaitControlDisplayed(HAREEO1_MainForm);
formBttn = HAREEO1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

