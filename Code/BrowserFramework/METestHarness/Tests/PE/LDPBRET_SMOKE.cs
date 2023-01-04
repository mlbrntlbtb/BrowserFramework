 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPBRET_SMOKE : TestScript
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
new Control("Delete Retroactive Timesheet Adjustments", "xpath","//div[@class='navItem'][.='Delete Retroactive Timesheet Adjustments']").Click();


												
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPBRET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPBRET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPBRET_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPBRET_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPBRET";
							CPCommon.WaitControlDisplayed(LDPBRET_MainForm);
IWebElement formBttn = LDPBRET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPBRET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPBRET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDPBRET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPBRET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming Click on NonContiguousRangeSelectionsLink...", Logger.MessageType.INF);
			Control LDPBRET_NonContiguousRangeSelectionsLink = new Control("NonContiguousRangeSelectionsLink", "ID", "lnk_2365_LDPBRET_PROCESS");
			CPCommon.WaitControlDisplayed(LDPBRET_NonContiguousRangeSelectionsLink);
LDPBRET_NonContiguousRangeSelectionsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming VerifyExists on EmployeeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control LDPBRET_EmployeeNonContiguousRangesForm = new Control("EmployeeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPBRET_NON_CONTIG_RANGE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDPBRET_EmployeeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming VerifyExist on EmployeeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control LDPBRET_EmployeeNonContiguousRangesFormTable = new Control("EmployeeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPBRET_NON_CONTIG_RANGE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPBRET_EmployeeNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDPBRET";
							CPCommon.WaitControlDisplayed(LDPBRET_EmployeeNonContiguousRangesForm);
formBttn = LDPBRET_EmployeeNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDPBRET_EmployeeNonContiguousRangesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDPBRET_EmployeeNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDPBRET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPBRET] Perfoming VerifyExists on EmployeeNonContiguousRanges_Start...", Logger.MessageType.INF);
			Control LDPBRET_EmployeeNonContiguousRanges_Start = new Control("EmployeeNonContiguousRanges_Start", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPBRET_NON_CONTIG_RANGE_']/ancestor::form[1]/descendant::*[@id='EMPL_ID_FROM']");
			CPCommon.AssertEqual(true,LDPBRET_EmployeeNonContiguousRanges_Start.Exists());

												
				CPCommon.CurrentComponent = "LDPBRET";
							CPCommon.WaitControlDisplayed(LDPBRET_MainForm);
formBttn = LDPBRET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

