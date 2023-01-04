 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPRPJEV_SMOKE : TestScript
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
new Control("Project Budget Reports/Inquiries", "xpath","//div[@class='navItem'][.='Project Budget Reports/Inquiries']").Click();
new Control("Print Earned Value Report", "xpath","//div[@class='navItem'][.='Print Earned Value Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BPRPJEV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJEV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPRPJEV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPRPJEV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPRPJEV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJEV] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control BPRPJEV_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BPRPJEV_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BPRPJEV";
							CPCommon.WaitControlDisplayed(BPRPJEV_MainForm);
IWebElement formBttn = BPRPJEV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BPRPJEV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BPRPJEV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BPRPJEV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJEV] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BPRPJEV_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPRPJEV_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BPRPJEV";
							CPCommon.WaitControlDisplayed(BPRPJEV_MainForm);
formBttn = BPRPJEV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

