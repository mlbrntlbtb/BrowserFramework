 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMETAX_SMOKE : TestScript
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
new Control("Employee Payroll Information", "xpath","//div[@class='navItem'][.='Employee Payroll Information']").Click();
new Control("Manage Employee Taxes", "xpath","//div[@class='navItem'][.='Manage Employee Taxes']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMETAX_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMETAX_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control PRMETAX_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRMETAX_Employee.Exists());

											Driver.SessionLogger.WriteLine("Taxes Details");


												
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming Select on TaxDetails_Tab...", Logger.MessageType.INF);
			Control PRMETAX_TaxDetails_Tab = new Control("TaxDetails_Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PRMETAX_TaxDetails_Tab);
IWebElement mTab = PRMETAX_TaxDetails_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Taxes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExists on TaxDetails_Taxes_PayCycle...", Logger.MessageType.INF);
			Control PRMETAX_TaxDetails_Taxes_PayCycle = new Control("TaxDetails_Taxes_PayCycle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,PRMETAX_TaxDetails_Taxes_PayCycle.Exists());

												
				CPCommon.CurrentComponent = "PRMETAX";
							CPCommon.WaitControlDisplayed(PRMETAX_TaxDetails_Tab);
mTab = PRMETAX_TaxDetails_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Tax Reporting Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExists on TaxDetails_TaxReportingInformation_GeographicCode...", Logger.MessageType.INF);
			Control PRMETAX_TaxDetails_TaxReportingInformation_GeographicCode = new Control("TaxDetails_TaxReportingInformation_GeographicCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_AK_GEO_CD']");
			CPCommon.AssertEqual(true,PRMETAX_TaxDetails_TaxReportingInformation_GeographicCode.Exists());

											Driver.SessionLogger.WriteLine("Table");


												
				CPCommon.CurrentComponent = "PRMETAX";
							CPCommon.WaitControlDisplayed(PRMETAX_MainForm);
IWebElement formBttn = PRMETAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMETAX_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMETAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMETAX_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMETAX_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Multi- State Taxes");


												
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExists on TaxDetails_MultiStateTaxes...", Logger.MessageType.INF);
			Control PRMETAX_TaxDetails_MultiStateTaxes = new Control("TaxDetails_MultiStateTaxes", "ID", "lnk_16474_PEMETAX_EMPLTAX_INFO");
			CPCommon.AssertEqual(true,PRMETAX_TaxDetails_MultiStateTaxes.Exists());

												
				CPCommon.CurrentComponent = "PRMETAX";
							CPCommon.WaitControlDisplayed(PRMETAX_TaxDetails_MultiStateTaxes);
PRMETAX_TaxDetails_MultiStateTaxes.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExists on MultiStateTaxesForm...", Logger.MessageType.INF);
			Control PRMETAX_MultiStateTaxesForm = new Control("MultiStateTaxesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PEMETAX_EMPLTAXMULTI_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMETAX_MultiStateTaxesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMETAX] Perfoming VerifyExist on MultiStateTaxesTable...", Logger.MessageType.INF);
			Control PRMETAX_MultiStateTaxesTable = new Control("MultiStateTaxesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PEMETAX_EMPLTAXMULTI_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMETAX_MultiStateTaxesTable.Exists());

											Driver.SessionLogger.WriteLine("Multi- State Taxes Form");


												
				CPCommon.CurrentComponent = "PRMETAX";
							CPCommon.WaitControlDisplayed(PRMETAX_MultiStateTaxesForm);
formBttn = PRMETAX_MultiStateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRMETAX_MultiStateTaxesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRMETAX_MultiStateTaxesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRMETAX";
							CPCommon.AssertEqual(true,PRMETAX_MultiStateTaxesForm.Exists());

												Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "PRMETAX";
							CPCommon.WaitControlDisplayed(PRMETAX_MainForm);
formBttn = PRMETAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

