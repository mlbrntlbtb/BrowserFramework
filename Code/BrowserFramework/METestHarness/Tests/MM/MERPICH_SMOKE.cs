 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MERPICH_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Materials Estimating Reports/Inquiries", "xpath","//div[@class='navItem'][.='Materials Estimating Reports/Inquiries']").Click();
new Control("Print Proposal Item Cost History Report", "xpath","//div[@class='navItem'][.='Print Proposal Item Cost History Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MERPICH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MERPICH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MERPICH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MERPICH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MERPICH";
							CPCommon.WaitControlDisplayed(MERPICH_MainForm);
IWebElement formBttn = MERPICH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MERPICH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MERPICH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MERPICH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MERPICH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MERPICH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MERPICH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MERPICH";
							CPCommon.WaitControlDisplayed(MERPICH_MainForm);
formBttn = MERPICH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MERPICH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MERPICH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MERPICH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MERPICH] Perfoming VerifyExists on MainForm_Description...", Logger.MessageType.INF);
			Control MERPICH_MainForm_Description = new Control("MainForm_Description", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_DESC']");
			CPCommon.AssertEqual(true,MERPICH_MainForm_Description.Exists());

											Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "MERPICH";
							CPCommon.WaitControlDisplayed(MERPICH_MainForm);
formBttn = MERPICH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

