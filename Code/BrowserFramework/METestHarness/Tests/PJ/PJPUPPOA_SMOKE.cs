 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPUPPOA_SMOKE : TestScript
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
new Control("Administrative Utilities", "xpath","//div[@class='navItem'][.='Administrative Utilities']").Click();
new Control("Update POA Table with Valid Links/Reference Numbers", "xpath","//div[@class='navItem'][.='Update POA Table with Valid Links/Reference Numbers']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPUPPOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPUPPOA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPUPPOA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPUPPOA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPUPPOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPUPPOA] Perfoming VerifyExists on Autoload...", Logger.MessageType.INF);
			Control PJPUPPOA_Autoload = new Control("Autoload", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'AUTOFILL') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,PJPUPPOA_Autoload.Exists());

												
				CPCommon.CurrentComponent = "PJPUPPOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPUPPOA] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PJPUPPOA_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPUPPOA_UPD_POA_LNKS_REFNOS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJPUPPOA_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJPUPPOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPUPPOA] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PJPUPPOA_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPUPPOA_UPD_POA_LNKS_REFNOS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPUPPOA_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJPUPPOA";
							CPCommon.WaitControlDisplayed(PJPUPPOA_ChildForm);
IWebElement formBttn = PJPUPPOA_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJPUPPOA_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJPUPPOA_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJPUPPOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPUPPOA] Perfoming VerifyExists on ChildForm_Project...", Logger.MessageType.INF);
			Control PJPUPPOA_ChildForm_Project = new Control("ChildForm_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPUPPOA_UPD_POA_LNKS_REFNOS_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJPUPPOA_ChildForm_Project.Exists());

												
				CPCommon.CurrentComponent = "PJPUPPOA";
							CPCommon.WaitControlDisplayed(PJPUPPOA_MainForm);
formBttn = PJPUPPOA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

