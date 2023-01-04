 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class IWPIMPRT_SMOKE : TestScript
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
new Control("Import IWO Allocations", "xpath","//div[@class='navItem'][.='Import IWO Allocations']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "IWPIMPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWPIMPRT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control IWPIMPRT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,IWPIMPRT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "IWPIMPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWPIMPRT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control IWPIMPRT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,IWPIMPRT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "IWPIMPRT";
							CPCommon.WaitControlDisplayed(IWPIMPRT_MainForm);
IWebElement formBttn = IWPIMPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWPIMPRT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWPIMPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "IWPIMPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWPIMPRT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control IWPIMPRT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWPIMPRT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWPIMPRT";
							CPCommon.WaitControlDisplayed(IWPIMPRT_MainForm);
formBttn = IWPIMPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWPIMPRT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWPIMPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "IWPIMPRT";
							CPCommon.WaitControlDisplayed(IWPIMPRT_MainForm);
formBttn = IWPIMPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

