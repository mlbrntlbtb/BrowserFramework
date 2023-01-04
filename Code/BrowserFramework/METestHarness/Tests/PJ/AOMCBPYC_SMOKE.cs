 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMCBPYC_SMOKE : TestScript
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
new Control("Project History", "xpath","//div[@class='navItem'][.='Project History']").Click();
new Control("Manage Prior Year Cobra Costs", "xpath","//div[@class='navItem'][.='Manage Prior Year Cobra Costs']").Click();


											Driver.SessionLogger.WriteLine("MAIN");


												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMCBPYC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMCBPYC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control AOMCBPYC_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,AOMCBPYC_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "AOMCBPYC";
							CPCommon.WaitControlDisplayed(AOMCBPYC_MainForm);
IWebElement formBttn = AOMCBPYC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMCBPYC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMCBPYC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMCBPYC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMCBPYC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PRIOR YEAR COST LINK");


												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExists on PriorYearCostsLink...", Logger.MessageType.INF);
			Control AOMCBPYC_PriorYearCostsLink = new Control("PriorYearCostsLink", "ID", "lnk_5779_AOMCBPYC_PYCBSUM_MAINHDR");
			CPCommon.AssertEqual(true,AOMCBPYC_PriorYearCostsLink.Exists());

												
				CPCommon.CurrentComponent = "AOMCBPYC";
							CPCommon.WaitControlDisplayed(AOMCBPYC_PriorYearCostsLink);
AOMCBPYC_PriorYearCostsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExist on PriorYearCostsFormTable...", Logger.MessageType.INF);
			Control AOMCBPYC_PriorYearCostsFormTable = new Control("PriorYearCostsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCBPYC_PYCBSUM_MAINCHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMCBPYC_PriorYearCostsFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming ClickButton on PriorYearCostsForm...", Logger.MessageType.INF);
			Control AOMCBPYC_PriorYearCostsForm = new Control("PriorYearCostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCBPYC_PYCBSUM_MAINCHD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOMCBPYC_PriorYearCostsForm);
formBttn = AOMCBPYC_PriorYearCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMCBPYC_PriorYearCostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMCBPYC_PriorYearCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExists on PriorYearCosts_Account...", Logger.MessageType.INF);
			Control AOMCBPYC_PriorYearCosts_Account = new Control("PriorYearCosts_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCBPYC_PYCBSUM_MAINCHD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,AOMCBPYC_PriorYearCosts_Account.Exists());

											Driver.SessionLogger.WriteLine("POOL AMOUNTS LINK");


												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExists on PriorYearCosts_PoolAmountsLink...", Logger.MessageType.INF);
			Control AOMCBPYC_PriorYearCosts_PoolAmountsLink = new Control("PriorYearCosts_PoolAmountsLink", "ID", "lnk_5780_AOMCBPYC_PYCBSUM_MAINCHD");
			CPCommon.AssertEqual(true,AOMCBPYC_PriorYearCosts_PoolAmountsLink.Exists());

												
				CPCommon.CurrentComponent = "AOMCBPYC";
							CPCommon.WaitControlDisplayed(AOMCBPYC_PriorYearCosts_PoolAmountsLink);
AOMCBPYC_PriorYearCosts_PoolAmountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming VerifyExist on PoolAmountsFormTable...", Logger.MessageType.INF);
			Control AOMCBPYC_PoolAmountsFormTable = new Control("PoolAmountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCBPYC_PYCBBURDSUM_GRANDCHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMCBPYC_PoolAmountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMCBPYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCBPYC] Perfoming Close on PoolAmountsForm...", Logger.MessageType.INF);
			Control AOMCBPYC_PoolAmountsForm = new Control("PoolAmountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCBPYC_PYCBBURDSUM_GRANDCHD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOMCBPYC_PoolAmountsForm);
formBttn = AOMCBPYC_PoolAmountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PRIOR YEAR COST LINK CLOSE");


												
				CPCommon.CurrentComponent = "AOMCBPYC";
							CPCommon.WaitControlDisplayed(AOMCBPYC_PriorYearCostsForm);
formBttn = AOMCBPYC_PriorYearCostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMCBPYC";
							CPCommon.WaitControlDisplayed(AOMCBPYC_MainForm);
formBttn = AOMCBPYC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

