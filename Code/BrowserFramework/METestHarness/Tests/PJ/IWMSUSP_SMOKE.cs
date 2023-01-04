 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class IWMSUSP_SMOKE : TestScript
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
new Control("Inter-Company Work Orders", "xpath","//div[@class='deptItem'][.='Inter-Company Work Orders']").Click();
new Control("Inter-Company Work Orders Interfaces", "xpath","//div[@class='navItem'][.='Inter-Company Work Orders Interfaces']").Click();
new Control("Validate IWO Suspense Transactions", "xpath","//div[@class='navItem'][.='Validate IWO Suspense Transactions']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control IWMSUSP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,IWMSUSP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming VerifyExists on IWONumber...", Logger.MessageType.INF);
			Control IWMSUSP_IWONumber = new Control("IWONumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='IWO_NO']");
			CPCommon.AssertEqual(true,IWMSUSP_IWONumber.Exists());

												
				CPCommon.CurrentComponent = "IWMSUSP";
							CPCommon.WaitControlDisplayed(IWMSUSP_MainForm);
IWebElement formBttn = IWMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWMSUSP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control IWMSUSP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMSUSP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("IWO Suspense Details");


												
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming VerifyExists on IWOSuspenseDetailsLink...", Logger.MessageType.INF);
			Control IWMSUSP_IWOSuspenseDetailsLink = new Control("IWOSuspenseDetailsLink", "ID", "lnk_1002457_IWMSUSP_IWOALLOCHDRSUSP_HDR");
			CPCommon.AssertEqual(true,IWMSUSP_IWOSuspenseDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming VerifyExist on IWOSuspenseDetailsFormTable...", Logger.MessageType.INF);
			Control IWMSUSP_IWOSuspenseDetailsFormTable = new Control("IWOSuspenseDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMSUSP_IWOALLOCTRNSUSP_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMSUSP_IWOSuspenseDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming ClickButton on IWOSuspenseDetailsForm...", Logger.MessageType.INF);
			Control IWMSUSP_IWOSuspenseDetailsForm = new Control("IWOSuspenseDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMSUSP_IWOALLOCTRNSUSP_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(IWMSUSP_IWOSuspenseDetailsForm);
formBttn = IWMSUSP_IWOSuspenseDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWMSUSP_IWOSuspenseDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWMSUSP_IWOSuspenseDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "IWMSUSP";
							CPCommon.AssertEqual(true,IWMSUSP_IWOSuspenseDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "IWMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMSUSP] Perfoming VerifyExists on IWOSuspenseDetails_Line...", Logger.MessageType.INF);
			Control IWMSUSP_IWOSuspenseDetails_Line = new Control("IWOSuspenseDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMSUSP_IWOALLOCTRNSUSP_CHILD_']/ancestor::form[1]/descendant::*[@id='IWO_LN_NO']");
			CPCommon.AssertEqual(true,IWMSUSP_IWOSuspenseDetails_Line.Exists());

												
				CPCommon.CurrentComponent = "IWMSUSP";
							CPCommon.WaitControlDisplayed(IWMSUSP_IWOSuspenseDetailsForm);
formBttn = IWMSUSP_IWOSuspenseDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "IWMSUSP";
							CPCommon.WaitControlDisplayed(IWMSUSP_MainForm);
formBttn = IWMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

