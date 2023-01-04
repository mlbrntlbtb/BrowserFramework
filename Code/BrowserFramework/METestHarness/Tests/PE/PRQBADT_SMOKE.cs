 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQBADT_SMOKE : TestScript
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
new Control("Payroll Reports/Inquiries", "xpath","//div[@class='navItem'][.='Payroll Reports/Inquiries']").Click();
new Control("View Bond Information", "xpath","//div[@class='navItem'][.='View Bond Information']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQBADT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQBADT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQBADT_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPLOYEE']");
			CPCommon.AssertEqual(true,PRQBADT_Identification_Employee.Exists());

											Driver.SessionLogger.WriteLine("Inquiry Details Form");


												
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQBADT_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming ClickButton on InquiryDetailsForm...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PRQBADT_InquiryDetailsForm);
IWebElement formBttn = PRQBADT_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQBADT_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQBADT_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PRQBADT";
							CPCommon.AssertEqual(true,PRQBADT_InquiryDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on InquiryDetails_TransactionType...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetails_TransactionType = new Control("InquiryDetails_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='TRANS_TYPE']");
			CPCommon.AssertEqual(true,PRQBADT_InquiryDetails_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on InquiryDetails_InquiryDetailsTab...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetails_InquiryDetailsTab = new Control("InquiryDetails_InquiryDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PRQBADT_InquiryDetails_InquiryDetailsTab.Exists());

												
				CPCommon.CurrentComponent = "PRQBADT";
							CPCommon.WaitControlDisplayed(PRQBADT_InquiryDetails_InquiryDetailsTab);
IWebElement mTab = PRQBADT_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Bond Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on InquiryDetails_BondDetails_BondDetails_NextPurchase...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetails_BondDetails_BondDetails_NextPurchase = new Control("InquiryDetails_BondDetails_BondDetails_NextPurchase", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='NXT_PURCHASE']");
			CPCommon.AssertEqual(true,PRQBADT_InquiryDetails_BondDetails_BondDetails_NextPurchase.Exists());

												
				CPCommon.CurrentComponent = "PRQBADT";
							CPCommon.WaitControlDisplayed(PRQBADT_InquiryDetails_InquiryDetailsTab);
mTab = PRQBADT_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Mailing Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on InquiryDetails_MailingAddress_UseEmployeeAddress...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetails_MailingAddress_UseEmployeeAddress = new Control("InquiryDetails_MailingAddress_UseEmployeeAddress", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='USE_EMPL_ADD']");
			CPCommon.AssertEqual(true,PRQBADT_InquiryDetails_MailingAddress_UseEmployeeAddress.Exists());

												
				CPCommon.CurrentComponent = "PRQBADT";
							CPCommon.WaitControlDisplayed(PRQBADT_InquiryDetails_InquiryDetailsTab);
mTab = PRQBADT_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Owner/Beneficiary").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQBADT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBADT] Perfoming VerifyExists on InquiryDetails_OwnerBeneficiary_Owner_EmployeeIsOwner...", Logger.MessageType.INF);
			Control PRQBADT_InquiryDetails_OwnerBeneficiary_Owner_EmployeeIsOwner = new Control("InquiryDetails_OwnerBeneficiary_Owner_EmployeeIsOwner", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBADT_EMPLBONDINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='EMPL_OWNER']");
			CPCommon.AssertEqual(true,PRQBADT_InquiryDetails_OwnerBeneficiary_Owner_EmployeeIsOwner.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQBADT";
							CPCommon.WaitControlDisplayed(PRQBADT_MainForm);
formBttn = PRQBADT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

