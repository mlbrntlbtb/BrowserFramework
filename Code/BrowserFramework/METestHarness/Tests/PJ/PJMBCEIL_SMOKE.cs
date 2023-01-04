 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBCEIL_SMOKE : TestScript
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
new Control("Project Ceilings", "xpath","//div[@class='navItem'][.='Project Ceilings']").Click();
new Control("Manage Burden Cost Ceilings", "xpath","//div[@class='navItem'][.='Manage Burden Cost Ceilings']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "PJMBCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBCEIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBCEIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBCEIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBCEIL] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMBCEIL_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMBCEIL_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMBCEIL";
							CPCommon.WaitControlDisplayed(PJMBCEIL_MainForm);
IWebElement formBttn = PJMBCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMBCEIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMBCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMBCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBCEIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMBCEIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBCEIL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBCEIL] Perfoming VerifyExist on BurdenCostCeilingDetailsFormTable...", Logger.MessageType.INF);
			Control PJMBCEIL_BurdenCostCeilingDetailsFormTable = new Control("BurdenCostCeilingDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBCEIL_CEILBURDENCST_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBCEIL_BurdenCostCeilingDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBCEIL] Perfoming ClickButton on frmBurdenCostCeilingDetailsForm...", Logger.MessageType.INF);
			Control PJMBCEIL_frmBurdenCostCeilingDetailsForm = new Control("frmBurdenCostCeilingDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBCEIL_CEILBURDENCST_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBCEIL_frmBurdenCostCeilingDetailsForm);
formBttn = PJMBCEIL_frmBurdenCostCeilingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBCEIL_frmBurdenCostCeilingDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBCEIL_frmBurdenCostCeilingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMBCEIL";
							CPCommon.AssertEqual(true,PJMBCEIL_frmBurdenCostCeilingDetailsForm.Exists());

												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMBCEIL";
							CPCommon.WaitControlDisplayed(PJMBCEIL_MainForm);
formBttn = PJMBCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

