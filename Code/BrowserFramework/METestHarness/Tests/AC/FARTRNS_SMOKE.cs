 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FARTRNS_SMOKE : TestScript
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
new Control("Transfer Records", "xpath","//div[@class='navItem'][.='Transfer Records']").Click();
new Control("Print Asset Transfer Information Report", "xpath","//div[@class='navItem'][.='Print Asset Transfer Information Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FARTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARTRNS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FARTRNS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FARTRNS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FARTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARTRNS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control FARTRNS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,FARTRNS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "FARTRNS";
							CPCommon.WaitControlDisplayed(FARTRNS_MainForm);
IWebElement formBttn = FARTRNS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FARTRNS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FARTRNS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "FARTRNS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARTRNS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FARTRNS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FARTRNS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FARTRNS";
							CPCommon.WaitControlDisplayed(FARTRNS_MainForm);
formBttn = FARTRNS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

