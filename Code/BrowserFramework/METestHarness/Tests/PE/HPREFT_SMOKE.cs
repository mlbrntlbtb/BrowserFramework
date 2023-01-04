 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPREFT_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Termination Processing", "xpath","//div[@class='navItem'][.='Termination Processing']").Click();
new Control("Print Exit Interview Form Template", "xpath","//div[@class='navItem'][.='Print Exit Interview Form Template']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPREFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREFT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPREFT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPREFT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPREFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREFT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HPREFT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HPREFT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HPREFT";
							CPCommon.WaitControlDisplayed(HPREFT_MainForm);
IWebElement formBttn = HPREFT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPREFT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPREFT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPREFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREFT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPREFT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPREFT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("EMPLOYEE NON-CONTIGUOUS RANGES LINK");


												
				CPCommon.CurrentComponent = "HPREFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREFT] Perfoming Click on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control HPREFT_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_5355_HPREFT_PARAM");
			CPCommon.WaitControlDisplayed(HPREFT_EmployeeNonContiguousRangesLink);
HPREFT_EmployeeNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPREFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREFT] Perfoming VerifyExist on EmployeeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control HPREFT_EmployeeNonContiguousRangesFormTable = new Control("EmployeeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPREFT_EmployeeNonContiguousRangesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPREFT";
							CPCommon.WaitControlDisplayed(HPREFT_MainForm);
formBttn = HPREFT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

