 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMBPSET_SMOKE : TestScript
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
new Control("Benefits", "xpath","//div[@class='deptItem'][.='Benefits']").Click();
new Control("Benefit Entry and Creation", "xpath","//div[@class='navItem'][.='Benefit Entry and Creation']").Click();
new Control("Manage Benefit Plans", "xpath","//div[@class='navItem'][.='Manage Benefit Plans']").Click();


												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMBPSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on BenefitPlanCode...", Logger.MessageType.INF);
			Control HBMBPSET_BenefitPlanCode = new Control("BenefitPlanCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BEN_PLAN_CD']");
			CPCommon.AssertEqual(true,HBMBPSET_BenefitPlanCode.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control HBMBPSET_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,HBMBPSET_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_MainFormTab);
IWebElement mTab = HBMBPSET_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Benefit Plan Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on BenefitPlanDetails_Provider...", Logger.MessageType.INF);
			Control HBMBPSET_BenefitPlanDetails_Provider = new Control("BenefitPlanDetails_Provider", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROVIDER_CD']");
			CPCommon.AssertEqual(true,HBMBPSET_BenefitPlanDetails_Provider.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_MainFormTab);
mTab = HBMBPSET_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Eligibility Rules").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on EligibilityRules_MinimumAge...", Logger.MessageType.INF);
			Control HBMBPSET_EligibilityRules_MinimumAge = new Control("EligibilityRules_MinimumAge", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MIN_AGE_NO']");
			CPCommon.AssertEqual(true,HBMBPSET_EligibilityRules_MinimumAge.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_MainFormTab);
mTab = HBMBPSET_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Enrollment/Coverage Rules").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on EnrollmentCoverageRules_CoverageRules_AnyTime...", Logger.MessageType.INF);
			Control HBMBPSET_EnrollmentCoverageRules_CoverageRules_AnyTime = new Control("EnrollmentCoverageRules_CoverageRules_AnyTime", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ENRLL_ANY_FL']");
			CPCommon.AssertEqual(true,HBMBPSET_EnrollmentCoverageRules_CoverageRules_AnyTime.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_MainForm);
IWebElement formBttn = HBMBPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMBPSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMBPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMBPSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPSET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_MainForm);
formBttn = HBMBPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMBPSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMBPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CoverageDetailLink...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageDetailLink = new Control("CoverageDetailLink", "ID", "lnk_4071_HBMBPSET_HBBENPLANHDR_HDR");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageDetailLink.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CoverageOptionsLink...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOptionsLink = new Control("CoverageOptionsLink", "ID", "lnk_4073_HBMBPSET_HBBENPLANHDR_HDR");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageOptionsLink.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on ValidPostalCodesLink...", Logger.MessageType.INF);
			Control HBMBPSET_ValidPostalCodesLink = new Control("ValidPostalCodesLink", "ID", "lnk_4075_HBMBPSET_HBBENPLANHDR_HDR");
			CPCommon.AssertEqual(true,HBMBPSET_ValidPostalCodesLink.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on ValidStatesLink...", Logger.MessageType.INF);
			Control HBMBPSET_ValidStatesLink = new Control("ValidStatesLink", "ID", "lnk_4077_HBMBPSET_HBBENPLANHDR_HDR");
			CPCommon.AssertEqual(true,HBMBPSET_ValidStatesLink.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_CoverageDetailLink);
HBMBPSET_CoverageDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CalculationRulesForm...", Logger.MessageType.INF);
			Control HBMBPSET_CalculationRulesForm = new Control("CalculationRulesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_DETAILS_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_CalculationRulesForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CalculationRules_AgeCalculationMethod...", Logger.MessageType.INF);
			Control HBMBPSET_CalculationRules_AgeCalculationMethod = new Control("CalculationRules_AgeCalculationMethod", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_DETAILS_HDR_']/ancestor::form[1]/descendant::*[@id='S_AGE_CALC_MTHD_CD']");
			CPCommon.AssertEqual(true,HBMBPSET_CalculationRules_AgeCalculationMethod.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CalculationRules_CopaymentsDeductiblesLink...", Logger.MessageType.INF);
			Control HBMBPSET_CalculationRules_CopaymentsDeductiblesLink = new Control("CalculationRules_CopaymentsDeductiblesLink", "ID", "lnk_4072_HBMBPSET_DETAILS_HDR");
			CPCommon.AssertEqual(true,HBMBPSET_CalculationRules_CopaymentsDeductiblesLink.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CalculationRules_CopaymentsDeductiblesForm...", Logger.MessageType.INF);
			Control HBMBPSET_CalculationRules_CopaymentsDeductiblesForm = new Control("CalculationRules_CopaymentsDeductiblesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANCOPAY_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_CalculationRules_CopaymentsDeductiblesForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExist on CalculationRules_CopaymentsDeductiblesTable...", Logger.MessageType.INF);
			Control HBMBPSET_CalculationRules_CopaymentsDeductiblesTable = new Control("CalculationRules_CopaymentsDeductiblesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANCOPAY_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPSET_CalculationRules_CopaymentsDeductiblesTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_CalculationRules_CopaymentsDeductiblesForm);
formBttn = HBMBPSET_CalculationRules_CopaymentsDeductiblesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMBPSET_CalculationRules_CopaymentsDeductiblesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMBPSET_CalculationRules_CopaymentsDeductiblesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CalculationRules_CopaymentsDeductibles_Type...", Logger.MessageType.INF);
			Control HBMBPSET_CalculationRules_CopaymentsDeductibles_Type = new Control("CalculationRules_CopaymentsDeductibles_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANCOPAY_CTW_']/ancestor::form[1]/descendant::*[@id='COPAY_TYPE_DC']");
			CPCommon.AssertEqual(true,HBMBPSET_CalculationRules_CopaymentsDeductibles_Type.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_CalculationRulesForm);
formBttn = HBMBPSET_CalculationRulesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_CoverageOptionsLink);
HBMBPSET_CoverageOptionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CoverageOptionForm...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOptionForm = new Control("CoverageOptionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_OPTIONS_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageOptionForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CoverageOptions_CashOutOption_PayType...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOptions_CashOutOption_PayType = new Control("CoverageOptions_CashOutOption_PayType", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_OPTIONS_HDR_']/ancestor::form[1]/descendant::*[@id='PAY_TYPE']");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageOptions_CashOutOption_PayType.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming Click on CoverageOptions_CoverageOptionsDetailsLink...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOptions_CoverageOptionsDetailsLink = new Control("CoverageOptions_CoverageOptionsDetailsLink", "ID", "lnk_4074_HBMBPSET_OPTIONS_HDR");
			CPCommon.WaitControlDisplayed(HBMBPSET_CoverageOptions_CoverageOptionsDetailsLink);
HBMBPSET_CoverageOptions_CoverageOptionsDetailsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CoverageOption_CoverageOptionDetailsForm...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOption_CoverageOptionDetailsForm = new Control("CoverageOption_CoverageOptionDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageOption_CoverageOptionDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExist on CoverageOptions_CoverageOptionDetailsTable...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOptions_CoverageOptionDetailsTable = new Control("CoverageOptions_CoverageOptionDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageOptions_CoverageOptionDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_CoverageOption_CoverageOptionDetailsForm);
formBttn = HBMBPSET_CoverageOption_CoverageOptionDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMBPSET_CoverageOption_CoverageOptionDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMBPSET_CoverageOption_CoverageOptionDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on CoverageOptions_CoverageOptionsDetails_StartDate...", Logger.MessageType.INF);
			Control HBMBPSET_CoverageOptions_CoverageOptionsDetails_StartDate = new Control("CoverageOptions_CoverageOptionsDetails_StartDate", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANLN_CTW_']/ancestor::form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,HBMBPSET_CoverageOptions_CoverageOptionsDetails_StartDate.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_CoverageOptionForm);
formBttn = HBMBPSET_CoverageOptionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_ValidPostalCodesLink);
HBMBPSET_ValidPostalCodesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on ValidPostalCodesForm...", Logger.MessageType.INF);
			Control HBMBPSET_ValidPostalCodesForm = new Control("ValidPostalCodesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANPOSTAL_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_ValidPostalCodesForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExist on ValidPostalCodesTable...", Logger.MessageType.INF);
			Control HBMBPSET_ValidPostalCodesTable = new Control("ValidPostalCodesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANPOSTAL_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPSET_ValidPostalCodesTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_ValidPostalCodesForm);
formBttn = HBMBPSET_ValidPostalCodesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_ValidStatesLink);
HBMBPSET_ValidStatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExists on ValidStatesForm...", Logger.MessageType.INF);
			Control HBMBPSET_ValidStatesForm = new Control("ValidStatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANSTATE_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPSET_ValidStatesForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPSET] Perfoming VerifyExist on ValidStatesTable...", Logger.MessageType.INF);
			Control HBMBPSET_ValidStatesTable = new Control("ValidStatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPSET_HBBENPLANSTATE_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPSET_ValidStatesTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_ValidStatesForm);
formBttn = HBMBPSET_ValidStatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HBMBPSET";
							CPCommon.WaitControlDisplayed(HBMBPSET_MainForm);
formBttn = HBMBPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");


												
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

