 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMPAYTR_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Labor Rate Controls", "xpath","//div[@class='navItem'][.='Labor Rate Controls']").Click();
new Control("Manage Pay Type Restrictions", "xpath","//div[@class='navItem'][.='Manage Pay Type Restrictions']").Click();


												
				CPCommon.CurrentComponent = "LDMPAYTR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMPAYTR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMPAYTR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMPAYTR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMPAYTR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTR";
							CPCommon.WaitControlDisplayed(LDMPAYTR_MainForm);
IWebElement formBttn = LDMPAYTR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMPAYTR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMPAYTR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMPAYTR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTR] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control LDMPAYTR_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,LDMPAYTR_Project.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTR";
							CPCommon.WaitControlDisplayed(LDMPAYTR_MainForm);
formBttn = LDMPAYTR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

