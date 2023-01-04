 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCRMOCST_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Reports/Inquiries", "xpath","//div[@class='navItem'][.='Production Control Reports/Inquiries']").Click();
new Control("Print Manufacturing Order Cost Report", "xpath","//div[@class='navItem'][.='Print Manufacturing Order Cost Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCRMOCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCRMOCST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCRMOCST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCRMOCST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCRMOCST";
							CPCommon.WaitControlDisplayed(PCRMOCST_MainForm);
IWebElement formBttn = PCRMOCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCRMOCST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCRMOCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCRMOCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCRMOCST] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control PCRMOCST_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCRMOCST_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "PCRMOCST";
							CPCommon.WaitControlDisplayed(PCRMOCST_MainForm);
formBttn = PCRMOCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCRMOCST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCRMOCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCRMOCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCRMOCST] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PCRMOCST_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PCRMOCST_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "PCRMOCST";
							CPCommon.WaitControlDisplayed(PCRMOCST_MainForm);
formBttn = PCRMOCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

