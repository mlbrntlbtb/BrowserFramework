 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INRHSTPA_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Reports/Inquiries", "xpath","//div[@class='navItem'][.='Inventory Reports/Inquiries']").Click();
new Control("Print Inventory Transaction History", "xpath","//div[@class='navItem'][.='Print Inventory Transaction History']").Click();


												
				CPCommon.CurrentComponent = "INRHSTPA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INRHSTPA] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control INRHSTPA_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,INRHSTPA_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "INRHSTPA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INRHSTPA] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control INRHSTPA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(INRHSTPA_MainForm);
IWebElement formBttn = INRHSTPA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INRHSTPA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INRHSTPA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "INRHSTPA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INRHSTPA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INRHSTPA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INRHSTPA_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "INRHSTPA";
							CPCommon.WaitControlDisplayed(INRHSTPA_MainForm);
formBttn = INRHSTPA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INRHSTPA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INRHSTPA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INRHSTPA";
							CPCommon.WaitControlDisplayed(INRHSTPA_MainForm);
formBttn = INRHSTPA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

