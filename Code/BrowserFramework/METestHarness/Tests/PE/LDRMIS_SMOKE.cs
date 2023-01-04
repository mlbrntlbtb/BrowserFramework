 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDRMIS_SMOKE : TestScript
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
new Control("Timesheet Reporting", "xpath","//div[@class='navItem'][.='Timesheet Reporting']").Click();
new Control("Print Missing Timesheet Report", "xpath","//div[@class='navItem'][.='Print Missing Timesheet Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDRMIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRMIS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDRMIS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDRMIS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDRMIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRMIS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDRMIS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDRMIS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDRMIS";
							CPCommon.WaitControlDisplayed(LDRMIS_MainForm);
IWebElement formBttn = LDRMIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDRMIS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDRMIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDRMIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRMIS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDRMIS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDRMIS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDRMIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRMIS] Perfoming VerifyExists on OrganizationNonContiguousRangesLink...", Logger.MessageType.INF);
			Control LDRMIS_OrganizationNonContiguousRangesLink = new Control("OrganizationNonContiguousRangesLink", "ID", "lnk_1006034_LDRMIS_PARAM");
			CPCommon.AssertEqual(true,LDRMIS_OrganizationNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "LDRMIS";
							CPCommon.WaitControlDisplayed(LDRMIS_OrganizationNonContiguousRangesLink);
LDRMIS_OrganizationNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDRMIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRMIS] Perfoming VerifyExists on OrganizationNonContiguousRangesForm...", Logger.MessageType.INF);
			Control LDRMIS_OrganizationNonContiguousRangesForm = new Control("OrganizationNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDRMIS_OrganizationNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "LDRMIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRMIS] Perfoming VerifyExist on OrganizationNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control LDRMIS_OrganizationNonContiguousRangesFormTable = new Control("OrganizationNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDRMIS_OrganizationNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDRMIS";
							CPCommon.WaitControlDisplayed(LDRMIS_OrganizationNonContiguousRangesForm);
formBttn = LDRMIS_OrganizationNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDRMIS";
							CPCommon.WaitControlDisplayed(LDRMIS_MainForm);
formBttn = LDRMIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

