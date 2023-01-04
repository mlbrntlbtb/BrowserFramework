 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMLTI_SMOKE : TestScript
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
new Control("Local Taxes", "xpath","//div[@class='navItem'][.='Local Taxes']").Click();
new Control("Manage Local Taxes", "xpath","//div[@class='navItem'][.='Manage Local Taxes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMLTI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMLTI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on Locality...", Logger.MessageType.INF);
			Control PRMLTI_Locality = new Control("Locality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LOCAL_CD']");
			CPCommon.AssertEqual(true,PRMLTI_Locality.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
							CPCommon.WaitControlDisplayed(PRMLTI_MainForm);
IWebElement formBttn = PRMLTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMLTI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMLTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMLTI_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMLTI_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Local Tax Details Form");


												
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on LocalTaxDetailsForm...", Logger.MessageType.INF);
			Control PRMLTI_LocalTaxDetailsForm = new Control("LocalTaxDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTI_LOCALTAXHS_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMLTI_LocalTaxDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExist on LocalTaxDetailsTable...", Logger.MessageType.INF);
			Control PRMLTI_LocalTaxDetailsTable = new Control("LocalTaxDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTI_LOCALTAXHS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMLTI_LocalTaxDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
							CPCommon.WaitControlDisplayed(PRMLTI_LocalTaxDetailsForm);
formBttn = PRMLTI_LocalTaxDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRMLTI_LocalTaxDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRMLTI_LocalTaxDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on LocalTaxDetails_EffectiveDate...", Logger.MessageType.INF);
			Control PRMLTI_LocalTaxDetails_EffectiveDate = new Control("LocalTaxDetails_EffectiveDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTI_LOCALTAXHS_CHILD_']/ancestor::form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,PRMLTI_LocalTaxDetails_EffectiveDate.Exists());

											Driver.SessionLogger.WriteLine("Tax ID / Reference Numbers / Vendor Information Form");


												
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on TaxIDRefNoVendorInfo...", Logger.MessageType.INF);
			Control PRMLTI_TaxIDRefNoVendorInfo = new Control("TaxIDRefNoVendorInfo", "ID", "lnk_1002383_PRMLTI_LOCALITY_HDR");
			CPCommon.AssertEqual(true,PRMLTI_TaxIDRefNoVendorInfo.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
							CPCommon.WaitControlDisplayed(PRMLTI_TaxIDRefNoVendorInfo);
PRMLTI_TaxIDRefNoVendorInfo.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on TaxIDRefNoVendorInfoForm...", Logger.MessageType.INF);
			Control PRMLTI_TaxIDRefNoVendorInfoForm = new Control("TaxIDRefNoVendorInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTI_LOCALITYCO_SUB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMLTI_TaxIDRefNoVendorInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTI] Perfoming VerifyExists on TaxIDRefNoVendorInfo_TaxID...", Logger.MessageType.INF);
			Control PRMLTI_TaxIDRefNoVendorInfo_TaxID = new Control("TaxIDRefNoVendorInfo_TaxID", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTI_LOCALITYCO_SUB_']/ancestor::form[1]/descendant::*[@id='LOCAL_TAX_ID']");
			CPCommon.AssertEqual(true,PRMLTI_TaxIDRefNoVendorInfo_TaxID.Exists());

												
				CPCommon.CurrentComponent = "PRMLTI";
							CPCommon.WaitControlDisplayed(PRMLTI_TaxIDRefNoVendorInfoForm);
formBttn = PRMLTI_TaxIDRefNoVendorInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMLTI";
							CPCommon.WaitControlDisplayed(PRMLTI_MainForm);
formBttn = PRMLTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

