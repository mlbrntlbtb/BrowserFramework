 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMCTADR_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Manage Contractor Addresses", "xpath","//div[@class='navItem'][.='Manage Contractor Addresses']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEMCTADR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMCTADR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMCTADR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMCTADR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMCTADR";
							CPCommon.WaitControlDisplayed(OEMCTADR_MainForm);
IWebElement formBttn = OEMCTADR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMCTADR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMCTADR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMCTADR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMCTADR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMCTADR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMCTADR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMCTADR";
							CPCommon.WaitControlDisplayed(OEMCTADR_MainForm);
formBttn = OEMCTADR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMCTADR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMCTADR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMCTADR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMCTADR] Perfoming VerifyExists on Contractor...", Logger.MessageType.INF);
			Control OEMCTADR_Contractor = new Control("Contractor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CONTR_ADDR_CD']");
			CPCommon.AssertEqual(true,OEMCTADR_Contractor.Exists());

												
				CPCommon.CurrentComponent = "OEMCTADR";
							CPCommon.WaitControlDisplayed(OEMCTADR_MainForm);
formBttn = OEMCTADR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

