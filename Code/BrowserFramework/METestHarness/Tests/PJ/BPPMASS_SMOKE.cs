 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPPMASS_SMOKE : TestScript
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
new Control("Advanced Project Budgeting", "xpath","//div[@class='deptItem'][.='Advanced Project Budgeting']").Click();
new Control("Project Budget and Estimate To Complete", "xpath","//div[@class='navItem'][.='Project Budget and Estimate To Complete']").Click();
new Control("Mass Add Project Budgets", "xpath","//div[@class='navItem'][.='Mass Add Project Budgets']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BPPMASS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPPMASS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPPMASS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPPMASS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPPMASS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPPMASS] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control BPPMASS_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BPPMASS_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BPPMASS";
							CPCommon.WaitControlDisplayed(BPPMASS_MainForm);
IWebElement formBttn = BPPMASS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BPPMASS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BPPMASS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BPPMASS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPPMASS] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BPPMASS_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPPMASS_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "BPPMASS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPPMASS] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control BPPMASS_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BPPMASS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPPMASS_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "BPPMASS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPPMASS] Perfoming ClickButton on BudgetDetailsForm...", Logger.MessageType.INF);
			Control BPPMASS_BudgetDetailsForm = new Control("BudgetDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPPMASS_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BPPMASS_BudgetDetailsForm);
formBttn = BPPMASS_BudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BPPMASS_BudgetDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BPPMASS_BudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BPPMASS";
							CPCommon.AssertEqual(true,BPPMASS_BudgetDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BPPMASS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPPMASS] Perfoming VerifyExists on BudgetDetails_Template_Project...", Logger.MessageType.INF);
			Control BPPMASS_BudgetDetails_Template_Project = new Control("BudgetDetails_Template_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__BPPMASS_CHILD_']/ancestor::form[1]/descendant::*[@id='TEMPLATE']");
			CPCommon.AssertEqual(true,BPPMASS_BudgetDetails_Template_Project.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BPPMASS";
							CPCommon.WaitControlDisplayed(BPPMASS_MainForm);
formBttn = BPPMASS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

