 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMVEH_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Security and Company Property", "xpath","//div[@class='navItem'][.='Security and Company Property']").Click();
new Control("Manage Company Vehicles", "xpath","//div[@class='navItem'][.='Manage Company Vehicles']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMVEH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMVEH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMVEH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMVEH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMVEH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMVEH] Perfoming VerifyExists on VehicleID...", Logger.MessageType.INF);
			Control HPMVEH_VehicleID = new Control("VehicleID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEHICLE_ID']");
			CPCommon.AssertEqual(true,HPMVEH_VehicleID.Exists());

												
				CPCommon.CurrentComponent = "HPMVEH";
							CPCommon.WaitControlDisplayed(HPMVEH_MainForm);
IWebElement formBttn = HPMVEH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMVEH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMVEH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMVEH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMVEH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMVEH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMVEH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMVEH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMVEH] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control HPMVEH_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMVEH_HVEHICLESTATUS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMVEH_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMVEH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMVEH] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control HPMVEH_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMVEH_HVEHICLESTATUS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMVEH_ChildForm);
formBttn = HPMVEH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMVEH_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMVEH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMVEH";
							CPCommon.AssertEqual(true,HPMVEH_ChildForm.Exists());

												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HPMVEH";
							CPCommon.WaitControlDisplayed(HPMVEH_MainForm);
formBttn = HPMVEH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

