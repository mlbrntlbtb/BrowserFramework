 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPLDRAT_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Revenue Processing", "xpath","//div[@class='navItem'][.='Revenue Processing']").Click();
new Control("Load Labor Rates", "xpath","//div[@class='navItem'][.='Load Labor Rates']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJPLDRAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPLDRAT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPLDRAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPLDRAT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPLDRAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPLDRAT] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control PJPLDRAT_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJPLDRAT_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJPLDRAT";
							CPCommon.WaitControlDisplayed(PJPLDRAT_MainForm);
IWebElement formBttn = PJPLDRAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJPLDRAT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJPLDRAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJPLDRAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPLDRAT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJPLDRAT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPLDRAT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJPLDRAT";
							CPCommon.WaitControlDisplayed(PJPLDRAT_MainForm);
formBttn = PJPLDRAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

