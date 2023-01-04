 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPPLNCP_SMOKE : TestScript
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
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Budget Interfaces", "xpath","//div[@class='navItem'][.='Budget Interfaces']").Click();
new Control("Import Project Budgets", "xpath","//div[@class='navItem'][.='Import Project Budgets']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPPLNCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPPLNCP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPPLNCP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPPLNCP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPPLNCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPPLNCP] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPPLNCP_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPPLNCP_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPPLNCP";
							CPCommon.WaitControlDisplayed(AOPPLNCP_MainForm);
IWebElement formBttn = AOPPLNCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPPLNCP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPPLNCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPPLNCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPPLNCP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPPLNCP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPPLNCP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORMTABLE");


												
				CPCommon.CurrentComponent = "AOPPLNCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPPLNCP] Perfoming VerifyExist on RuntimeInputValuesFormTable...", Logger.MessageType.INF);
			Control AOPPLNCP_RuntimeInputValuesFormTable = new Control("RuntimeInputValuesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOPPLNCP_PARAM_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPPLNCP_RuntimeInputValuesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOPPLNCP";
							CPCommon.WaitControlDisplayed(AOPPLNCP_MainForm);
formBttn = AOPPLNCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

