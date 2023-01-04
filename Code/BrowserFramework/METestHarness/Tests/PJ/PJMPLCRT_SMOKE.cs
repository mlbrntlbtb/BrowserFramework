 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPLCRT_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Labor", "xpath","//div[@class='navItem'][.='Project Labor']").Click();
new Control("Link Project Labor Category Rates to Projects", "xpath","//div[@class='navItem'][.='Link Project Labor Category Rates to Projects']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPLCRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCRT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPLCRT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPLCRT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCRT] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMPLCRT_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPLCRT_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCRT] Perfoming VerifyExist on ProjectPLCRatesTable...", Logger.MessageType.INF);
			Control PJMPLCRT_ProjectPLCRatesTable = new Control("ProjectPLCRatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCATRTSC_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCRT_ProjectPLCRatesTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCRT] Perfoming ClickButton on ProjectPLCRatesForm...", Logger.MessageType.INF);
			Control PJMPLCRT_ProjectPLCRatesForm = new Control("ProjectPLCRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCATRTSC_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPLCRT_ProjectPLCRatesForm);
IWebElement formBttn = PJMPLCRT_ProjectPLCRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPLCRT_ProjectPLCRatesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPLCRT_ProjectPLCRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPLCRT";
							CPCommon.AssertEqual(true,PJMPLCRT_ProjectPLCRatesForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPLCRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCRT] Perfoming VerifyExists on ProjectPLCRates_PLC...", Logger.MessageType.INF);
			Control PJMPLCRT_ProjectPLCRates_PLC = new Control("ProjectPLCRates_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCATRTSC_CTW_']/ancestor::form[1]/descendant::*[@id='BILL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMPLCRT_ProjectPLCRates_PLC.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMPLCRT";
							CPCommon.WaitControlDisplayed(PJMPLCRT_MainForm);
formBttn = PJMPLCRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

