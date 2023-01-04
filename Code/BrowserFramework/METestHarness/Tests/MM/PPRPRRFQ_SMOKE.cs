 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPRPRRFQ_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Vendor Quotes", "xpath","//div[@class='navItem'][.='Vendor Quotes']").Click();
new Control("Print Request for Quotes", "xpath","//div[@class='navItem'][.='Print Request for Quotes']").Click();


												
				CPCommon.CurrentComponent = "PPRPRRFQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRFQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPRPRRFQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPRPRRFQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPRPRRFQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRFQ] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PPRPRRFQ_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PPRPRRFQ_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PPRPRRFQ";
							CPCommon.WaitControlDisplayed(PPRPRRFQ_MainForm);
IWebElement formBttn = PPRPRRFQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPRPRRFQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPRPRRFQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPRPRRFQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRFQ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPRPRRFQ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPRPRRFQ_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPRPRRFQ";
							CPCommon.WaitControlDisplayed(PPRPRRFQ_MainForm);
formBttn = PPRPRRFQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

