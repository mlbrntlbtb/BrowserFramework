 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMADD_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Labor Rate Controls", "xpath","//div[@class='navItem'][.='Labor Rate Controls']").Click();
new Control("Manage Allowance Codes", "xpath","//div[@class='navItem'][.='Manage Allowance Codes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMADD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMADD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMADD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMADD] Perfoming VerifyExists on AllowanceCode...", Logger.MessageType.INF);
			Control LDMADD_AllowanceCode = new Control("AllowanceCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALLOWANCE_CD']");
			CPCommon.AssertEqual(true,LDMADD_AllowanceCode.Exists());

												
				CPCommon.CurrentComponent = "LDMADD";
							CPCommon.WaitControlDisplayed(LDMADD_MainForm);
IWebElement formBttn = LDMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMADD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMADD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMADD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMADD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMADD";
							CPCommon.WaitControlDisplayed(LDMADD_MainForm);
formBttn = LDMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

