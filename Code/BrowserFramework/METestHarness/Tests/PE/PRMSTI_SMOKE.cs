 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMSTI_SMOKE : TestScript
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
new Control("State Taxes", "xpath","//div[@class='navItem'][.='State Taxes']").Click();
new Control("Manage State Taxes", "xpath","//div[@class='navItem'][.='Manage State Taxes']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMSTI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMSTI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control PRMSTI_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,PRMSTI_State.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
							CPCommon.WaitControlDisplayed(PRMSTI_MainForm);
IWebElement formBttn = PRMSTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMSTI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMSTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMSTI_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTI_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on TaxIDReferenceNumbersVendorInformationLink...", Logger.MessageType.INF);
			Control PRMSTI_TaxIDReferenceNumbersVendorInformationLink = new Control("TaxIDReferenceNumbersVendorInformationLink", "ID", "lnk_1002323_PRMSTI_STATETAXINFO_HDR");
			CPCommon.AssertEqual(true,PRMSTI_TaxIDReferenceNumbersVendorInformationLink.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on StateTaxDetailsForm...", Logger.MessageType.INF);
			Control PRMSTI_StateTaxDetailsForm = new Control("StateTaxDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTI_STATETAXHS_CHLTBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMSTI_StateTaxDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExist on StateTaxDetailsFormTable...", Logger.MessageType.INF);
			Control PRMSTI_StateTaxDetailsFormTable = new Control("StateTaxDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTI_STATETAXHS_CHLTBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTI_StateTaxDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
							CPCommon.WaitControlDisplayed(PRMSTI_StateTaxDetailsForm);
formBttn = PRMSTI_StateTaxDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRMSTI_StateTaxDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRMSTI_StateTaxDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on StateTaxDetails_EffectiveDate...", Logger.MessageType.INF);
			Control PRMSTI_StateTaxDetails_EffectiveDate = new Control("StateTaxDetails_EffectiveDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTI_STATETAXHS_CHLTBL_']/ancestor::form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,PRMSTI_StateTaxDetails_EffectiveDate.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
							CPCommon.WaitControlDisplayed(PRMSTI_TaxIDReferenceNumbersVendorInformationLink);
PRMSTI_TaxIDReferenceNumbersVendorInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on TaxIDReferenceNumbersVendorInformationForm...", Logger.MessageType.INF);
			Control PRMSTI_TaxIDReferenceNumbersVendorInformationForm = new Control("TaxIDReferenceNumbersVendorInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTI_STATETAXCO_SUB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMSTI_TaxIDReferenceNumbersVendorInformationForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExists on TaxIDReferenceNumbersVendorInformation_TaxID...", Logger.MessageType.INF);
			Control PRMSTI_TaxIDReferenceNumbersVendorInformation_TaxID = new Control("TaxIDReferenceNumbersVendorInformation_TaxID", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTI_STATETAXCO_SUB_']/ancestor::form[1]/descendant::*[@id='STATE_TAX_ID']");
			CPCommon.AssertEqual(true,PRMSTI_TaxIDReferenceNumbersVendorInformation_TaxID.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
							CPCommon.WaitControlDisplayed(PRMSTI_TaxIDReferenceNumbersVendorInformationForm);
formBttn = PRMSTI_TaxIDReferenceNumbersVendorInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMSTI_TaxIDReferenceNumbersVendorInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMSTI_TaxIDReferenceNumbersVendorInformationForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMSTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTI] Perfoming VerifyExist on TaxIDReferenceNumbersVendorInformationFormTable...", Logger.MessageType.INF);
			Control PRMSTI_TaxIDReferenceNumbersVendorInformationFormTable = new Control("TaxIDReferenceNumbersVendorInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTI_STATETAXCO_SUB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTI_TaxIDReferenceNumbersVendorInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMSTI";
							CPCommon.WaitControlDisplayed(PRMSTI_TaxIDReferenceNumbersVendorInformationForm);
formBttn = PRMSTI_TaxIDReferenceNumbersVendorInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRMSTI";
							CPCommon.WaitControlDisplayed(PRMSTI_MainForm);
formBttn = PRMSTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

