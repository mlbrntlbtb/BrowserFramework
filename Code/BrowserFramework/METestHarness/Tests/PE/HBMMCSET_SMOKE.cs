 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMMCSET_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Benefits", "xpath","//div[@class='deptItem'][.='Benefits']").Click();
new Control("Benefit Entry and Creation", "xpath","//div[@class='navItem'][.='Benefit Entry and Creation']").Click();
new Control("Manage Medical Care FSA Plan Year", "xpath","//div[@class='navItem'][.='Manage Medical Care FSA Plan Year']").Click();


												
				CPCommon.CurrentComponent = "HBMMCSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMCSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMMCSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMMCSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMMCSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMCSET] Perfoming VerifyExists on PlanYear...", Logger.MessageType.INF);
			Control HBMMCSET_PlanYear = new Control("PlanYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MED_FSA_YR_NO']");
			CPCommon.AssertEqual(true,HBMMCSET_PlanYear.Exists());

												
				CPCommon.CurrentComponent = "HBMMCSET";
							CPCommon.WaitControlDisplayed(HBMMCSET_MainForm);
IWebElement formBttn = HBMMCSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMMCSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMMCSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMMCSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMMCSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMMCSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMMCSET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMMCSET";
							CPCommon.WaitControlDisplayed(HBMMCSET_MainForm);
formBttn = HBMMCSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

