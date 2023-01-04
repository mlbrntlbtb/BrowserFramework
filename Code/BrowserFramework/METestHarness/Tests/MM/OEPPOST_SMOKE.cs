 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEPPOST_SMOKE : TestScript
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
new Control("Sales Order Posting", "xpath","//div[@class='navItem'][.='Sales Order Posting']").Click();
new Control("Post Sales Order Journal", "xpath","//div[@class='navItem'][.='Post Sales Order Journal']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "OEPPOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPPOST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEPPOST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEPPOST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEPPOST";
							CPCommon.WaitControlDisplayed(OEPPOST_MainForm);
IWebElement formBttn = OEPPOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEPPOST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEPPOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEPPOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPPOST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEPPOST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPPOST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPPOST";
							CPCommon.WaitControlDisplayed(OEPPOST_MainForm);
formBttn = OEPPOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEPPOST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEPPOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEPPOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPPOST] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OEPPOST_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OEPPOST_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OEPPOST";
							CPCommon.WaitControlDisplayed(OEPPOST_MainForm);
formBttn = OEPPOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

