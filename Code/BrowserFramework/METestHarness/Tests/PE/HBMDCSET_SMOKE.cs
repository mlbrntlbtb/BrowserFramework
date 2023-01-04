 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMDCSET_SMOKE : TestScript
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
new Control("Manage Dependent Care FSA Plan Year", "xpath","//div[@class='navItem'][.='Manage Dependent Care FSA Plan Year']").Click();


												
				CPCommon.CurrentComponent = "HBMDCSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMDCSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMDCSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMDCSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMDCSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMDCSET] Perfoming VerifyExists on PlanYear...", Logger.MessageType.INF);
			Control HBMDCSET_PlanYear = new Control("PlanYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DEP_FSA_YR_NO']");
			CPCommon.AssertEqual(true,HBMDCSET_PlanYear.Exists());

												
				CPCommon.CurrentComponent = "HBMDCSET";
							CPCommon.WaitControlDisplayed(HBMDCSET_MainForm);
IWebElement formBttn = HBMDCSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMDCSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMDCSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMDCSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMDCSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMDCSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMDCSET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMDCSET";
							CPCommon.WaitControlDisplayed(HBMDCSET_MainForm);
formBttn = HBMDCSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

