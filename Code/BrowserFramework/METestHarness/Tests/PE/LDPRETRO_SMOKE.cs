 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPRETRO_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Entry/Creation", "xpath","//div[@class='navItem'][.='Timesheet Entry/Creation']").Click();
new Control("Create Retroactive Timesheet Adjustments", "xpath","//div[@class='navItem'][.='Create Retroactive Timesheet Adjustments']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "LDPRETRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRETRO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPRETRO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPRETRO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPRETRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRETRO] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPRETRO_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPRETRO_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPRETRO";
							CPCommon.WaitControlDisplayed(LDPRETRO_MainForm);
IWebElement formBttn = LDPRETRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPRETRO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPRETRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPRETRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRETRO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDPRETRO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPRETRO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDPRETRO";
							CPCommon.WaitControlDisplayed(LDPRETRO_MainForm);
formBttn = LDPRETRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDPRETRO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDPRETRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Employee Non-Contiguous Ranges Link");


												
				CPCommon.CurrentComponent = "LDPRETRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRETRO] Perfoming Click on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control LDPRETRO_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_1004627_LDPRETRO_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(LDPRETRO_EmployeeNonContiguousRangesLink);
LDPRETRO_EmployeeNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "LDPRETRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRETRO] Perfoming VerifyExist on EmployeeNonContiguousRangesTable...", Logger.MessageType.INF);
			Control LDPRETRO_EmployeeNonContiguousRangesTable = new Control("EmployeeNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPRETRO_EmployeeNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "LDPRETRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRETRO] Perfoming Click on EmployeeNonContiguousRanges_OK...", Logger.MessageType.INF);
			Control LDPRETRO_EmployeeNonContiguousRanges_OK = new Control("EmployeeNonContiguousRanges_OK", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(LDPRETRO_EmployeeNonContiguousRanges_OK);
if (LDPRETRO_EmployeeNonContiguousRanges_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
LDPRETRO_EmployeeNonContiguousRanges_OK.Click(5,5);
else LDPRETRO_EmployeeNonContiguousRanges_OK.Click(4.5);


											Driver.SessionLogger.WriteLine("Close Main Form");


												
				CPCommon.CurrentComponent = "LDPRETRO";
							CPCommon.WaitControlDisplayed(LDPRETRO_MainForm);
formBttn = LDPRETRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

