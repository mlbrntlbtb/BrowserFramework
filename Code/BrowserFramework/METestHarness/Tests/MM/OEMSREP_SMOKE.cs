 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMSREP_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Manage Sales Representatives", "xpath","//div[@class='navItem'][.='Manage Sales Representatives']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "OEMSREP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSREP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMSREP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMSREP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSREP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSREP] Perfoming VerifyExists on SalesRepresentativeCode...", Logger.MessageType.INF);
			Control OEMSREP_SalesRepresentativeCode = new Control("SalesRepresentativeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SALES_REP_ID']");
			CPCommon.AssertEqual(true,OEMSREP_SalesRepresentativeCode.Exists());

												
				CPCommon.CurrentComponent = "OEMSREP";
							CPCommon.WaitControlDisplayed(OEMSREP_MainForm);
IWebElement formBttn = OEMSREP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMSREP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMSREP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMSREP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSREP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMSREP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSREP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("MainForm Close");


												
				CPCommon.CurrentComponent = "OEMSREP";
							CPCommon.WaitControlDisplayed(OEMSREP_MainForm);
formBttn = OEMSREP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

