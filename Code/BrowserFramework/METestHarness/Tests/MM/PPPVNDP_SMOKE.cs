 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPPVNDP_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Vendors", "xpath","//div[@class='navItem'][.='Vendors']").Click();
new Control("Compute Vendor Performance", "xpath","//div[@class='navItem'][.='Compute Vendor Performance']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPPVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPVNDP] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PPPVNDP_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PPPVNDP_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PPPVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPVNDP] Perfoming VerifyExists on SelectionRanges_Option_VendorName...", Logger.MessageType.INF);
			Control PPPVNDP_SelectionRanges_Option_VendorName = new Control("SelectionRanges_Option_VendorName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_RANGE_CD']");
			CPCommon.AssertEqual(true,PPPVNDP_SelectionRanges_Option_VendorName.Exists());

												
				CPCommon.CurrentComponent = "PPPVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPVNDP] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PPPVNDP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PPPVNDP_MainForm);
IWebElement formBttn = PPPVNDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPPVNDP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPPVNDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "PPPVNDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPVNDP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPPVNDP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPPVNDP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPPVNDP";
							CPCommon.WaitControlDisplayed(PPPVNDP_MainForm);
formBttn = PPPVNDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPPVNDP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPPVNDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPPVNDP";
							CPCommon.WaitControlDisplayed(PPPVNDP_MainForm);
formBttn = PPPVNDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

