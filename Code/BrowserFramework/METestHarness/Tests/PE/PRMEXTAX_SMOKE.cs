 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMEXTAX_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Interfaces", "xpath","//div[@class='navItem'][.='Payroll Interfaces']").Click();
new Control("Configure Payroll Tax Export Settings", "xpath","//div[@class='navItem'][.='Configure Payroll Tax Export Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMEXTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEXTAX] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMEXTAX_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMEXTAX_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMEXTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEXTAX] Perfoming VerifyExists on EnablePayrollTaxInterface...", Logger.MessageType.INF);
			Control PRMEXTAX_EnablePayrollTaxInterface = new Control("EnablePayrollTaxInterface", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REQUIRE_TAX_CD_FL']");
			CPCommon.AssertEqual(true,PRMEXTAX_EnablePayrollTaxInterface.Exists());

											Driver.SessionLogger.WriteLine("TAX SERVICE GROUP IDS LINK");


												
				CPCommon.CurrentComponent = "PRMEXTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEXTAX] Perfoming Click on TaxServiceGroupIDsLink...", Logger.MessageType.INF);
			Control PRMEXTAX_TaxServiceGroupIDsLink = new Control("TaxServiceGroupIDsLink", "ID", "lnk_16518_PRMEXTAX_HDR");
			CPCommon.WaitControlDisplayed(PRMEXTAX_TaxServiceGroupIDsLink);
PRMEXTAX_TaxServiceGroupIDsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PRMEXTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEXTAX] Perfoming VerifyExist on TaxServiceGroupIDsFormTable...", Logger.MessageType.INF);
			Control PRMEXTAX_TaxServiceGroupIDsFormTable = new Control("TaxServiceGroupIDsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMEXTAX_TAX_SVC_GRP_IDS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMEXTAX_TaxServiceGroupIDsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMEXTAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEXTAX] Perfoming VerifyExists on TaxServiceGroupIDsForm...", Logger.MessageType.INF);
			Control PRMEXTAX_TaxServiceGroupIDsForm = new Control("TaxServiceGroupIDsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMEXTAX_TAX_SVC_GRP_IDS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMEXTAX_TaxServiceGroupIDsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMEXTAX";
							CPCommon.WaitControlDisplayed(PRMEXTAX_TaxServiceGroupIDsForm);
IWebElement formBttn = PRMEXTAX_TaxServiceGroupIDsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMEXTAX";
							CPCommon.WaitControlDisplayed(PRMEXTAX_MainForm);
formBttn = PRMEXTAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

