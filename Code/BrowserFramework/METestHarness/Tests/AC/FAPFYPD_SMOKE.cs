 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAPFYPD_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Calendar", "xpath","//div[@class='navItem'][.='Fixed Assets Calendar']").Click();
new Control("Update FA FY/Pd Information from GL FY/Pd Information", "xpath","//div[@class='navItem'][.='Update FA FY/Pd Information from GL FY/Pd Information']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAPFYPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPFYPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAPFYPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAPFYPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAPFYPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPFYPD] Perfoming VerifyExists on SelectionRanges_FiscalYearOption...", Logger.MessageType.INF);
			Control FAPFYPD_SelectionRanges_FiscalYearOption = new Control("SelectionRanges_FiscalYearOption", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_RANGE_CD']");
			CPCommon.AssertEqual(true,FAPFYPD_SelectionRanges_FiscalYearOption.Exists());

												
				CPCommon.CurrentComponent = "FAPFYPD";
							CPCommon.WaitControlDisplayed(FAPFYPD_MainForm);
IWebElement formBttn = FAPFYPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAPFYPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAPFYPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FAPFYPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPFYPD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAPFYPD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAPFYPD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAPFYPD";
							CPCommon.WaitControlDisplayed(FAPFYPD_MainForm);
formBttn = FAPFYPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

