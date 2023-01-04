 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests.PJ
{
    public class IWPUPLD_SMOKE : TestScript
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
new Control("Validate IWO Allocations", "xpath","//div[@class='navItem'][.='Validate IWO Allocations']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "IWPUPLD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWPUPLD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control IWPUPLD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,IWPUPLD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "IWPUPLD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWPUPLD] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control IWPUPLD_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,IWPUPLD_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "IWPUPLD";
							CPCommon.WaitControlDisplayed(IWPUPLD_MainForm);
IWebElement formBttn = IWPUPLD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWPUPLD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWPUPLD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "IWPUPLD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWPUPLD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control IWPUPLD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWPUPLD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWPUPLD";
							CPCommon.WaitControlDisplayed(IWPUPLD_MainForm);
formBttn = IWPUPLD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWPUPLD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWPUPLD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "IWPUPLD";
							CPCommon.WaitControlDisplayed(IWPUPLD_MainForm);
formBttn = IWPUPLD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

