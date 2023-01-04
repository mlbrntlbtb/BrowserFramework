 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPLPTS_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Entry/Creation", "xpath","//div[@class='navItem'][.='Timesheet Entry/Creation']").Click();
new Control("Create Leave Payout Timesheets", "xpath","//div[@class='navItem'][.='Create Leave Payout Timesheets']").Click();


												
				CPCommon.CurrentComponent = "LDPLPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPLPTS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPLPTS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPLPTS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPLPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPLPTS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPLPTS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPLPTS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPLPTS";
							CPCommon.WaitControlDisplayed(LDPLPTS_MainForm);
IWebElement formBttn = LDPLPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPLPTS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPLPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPLPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPLPTS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDPLPTS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPLPTS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDPLPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPLPTS] Perfoming Click on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control LDPLPTS_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_2806_LDPLPTS_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(LDPLPTS_EmployeeNonContiguousRangesLink);
LDPLPTS_EmployeeNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "LDPLPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPLPTS] Perfoming VerifyExists on EmployeeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control LDPLPTS_EmployeeNonContiguousRangesForm = new Control("EmployeeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDPLPTS_EmployeeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "LDPLPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPLPTS] Perfoming VerifyExist on EmployeeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control LDPLPTS_EmployeeNonContiguousRangesFormTable = new Control("EmployeeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPLPTS_EmployeeNonContiguousRangesFormTable.Exists());

												
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

