 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPRPRRQ_SMOKE : TestScript
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
new Control("Purchase Requisitions", "xpath","//div[@class='navItem'][.='Purchase Requisitions']").Click();
new Control("Print Purchase Requisitions", "xpath","//div[@class='navItem'][.='Print Purchase Requisitions']").Click();


											Driver.SessionLogger.WriteLine("Main");


												
				CPCommon.CurrentComponent = "PPRPRRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPRPRRQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPRPRRQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPRPRRQ";
							CPCommon.WaitControlDisplayed(PPRPRRQ_MainForm);
IWebElement formBttn = PPRPRRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPRPRRQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPRPRRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPRPRRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRQ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPRPRRQ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPRPRRQ_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPRPRRQ";
							CPCommon.WaitControlDisplayed(PPRPRRQ_MainForm);
formBttn = PPRPRRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPRPRRQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPRPRRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPRPRRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRQ] Perfoming VerifyExists on SelectionRanges_Option_Requisition...", Logger.MessageType.INF);
			Control PPRPRRQ_SelectionRanges_Option_Requisition = new Control("SelectionRanges_Option_Requisition", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_ID_RANGE_CD']");
			CPCommon.AssertEqual(true,PPRPRRQ_SelectionRanges_Option_Requisition.Exists());

												
				CPCommon.CurrentComponent = "PPRPRRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPRPRRQ] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PPRPRRQ_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PPRPRRQ_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("Main");


												
				CPCommon.CurrentComponent = "PPRPRRQ";
							CPCommon.WaitControlDisplayed(PPRPRRQ_MainForm);
formBttn = PPRPRRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

