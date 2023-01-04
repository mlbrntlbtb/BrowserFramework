 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMVCEIL_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Ceilings", "xpath","//div[@class='navItem'][.='Project Ceilings']").Click();
new Control("Manage Vendor Hour Ceilings", "xpath","//div[@class='navItem'][.='Manage Vendor Hour Ceilings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMVCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVCEIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMVCEIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMVCEIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMVCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVCEIL] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMVCEIL_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMVCEIL_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMVCEIL";
							CPCommon.WaitControlDisplayed(PJMVCEIL_MainForm);
IWebElement formBttn = PJMVCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMVCEIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMVCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMVCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVCEIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMVCEIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVCEIL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("VENDOR HOURS CEILINGS");


												
				CPCommon.CurrentComponent = "PJMVCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVCEIL] Perfoming VerifyExist on VendorHoursCeilingsFormTable...", Logger.MessageType.INF);
			Control PJMVCEIL_VendorHoursCeilingsFormTable = new Control("VendorHoursCeilingsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMVCEIL_VENDCEIL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVCEIL_VendorHoursCeilingsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMVCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVCEIL] Perfoming VerifyExists on VendorHoursCeilingsForm...", Logger.MessageType.INF);
			Control PJMVCEIL_VendorHoursCeilingsForm = new Control("VendorHoursCeilingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMVCEIL_VENDCEIL_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMVCEIL_VendorHoursCeilingsForm.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMVCEIL";
							CPCommon.WaitControlDisplayed(PJMVCEIL_MainForm);
formBttn = PJMVCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

