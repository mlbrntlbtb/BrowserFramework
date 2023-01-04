 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEPCPIC_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Proposal Item Costing", "xpath","//div[@class='navItem'][.='Proposal Item Costing']").Click();
new Control("Compute Proposal Item Costs", "xpath","//div[@class='navItem'][.='Compute Proposal Item Costs']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MEPCPIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPCPIC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEPCPIC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEPCPIC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEPCPIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPCPIC] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MEPCPIC_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MEPCPIC_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MEPCPIC";
							CPCommon.WaitControlDisplayed(MEPCPIC_MainForm);
IWebElement formBttn = MEPCPIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MEPCPIC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MEPCPIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MEPCPIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPCPIC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MEPCPIC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEPCPIC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEPCPIC";
							CPCommon.WaitControlDisplayed(MEPCPIC_MainForm);
formBttn = MEPCPIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEPCPIC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEPCPIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MEPCPIC";
							CPCommon.AssertEqual(true,MEPCPIC_MainForm_ParameterID.Exists());

												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "MEPCPIC";
							CPCommon.WaitControlDisplayed(MEPCPIC_MainForm);
formBttn = MEPCPIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

