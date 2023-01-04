 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRRW2_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Year-End Processing", "xpath","//div[@class='navItem'][.='Year-End Processing']").Click();
new Control("Print W-2s", "xpath","//div[@class='navItem'][.='Print W-2s']").Click();


												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRRW2_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRRW2_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRRW2_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRRW2_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_MainForm);
IWebElement formBttn = PRRW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRRW2_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRRW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRRW2_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_MainForm);
formBttn = PRRW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRRW2_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRRW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on EmployeeNameNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNameNonContiguousRangesLink = new Control("EmployeeNameNonContiguousRangesLink", "ID", "lnk_5484_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_EmployeeNameNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_1006134_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_EmployeeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on LocatorCodeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_LocatorCodeNonContiguousRangesLink = new Control("LocatorCodeNonContiguousRangesLink", "ID", "lnk_1006141_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_LocatorCodeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on OrganizationNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_OrganizationNonContiguousRangesLink = new Control("OrganizationNonContiguousRangesLink", "ID", "lnk_1006140_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_OrganizationNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on PostalCodeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_PostalCodeNonContiguousRangesLink = new Control("PostalCodeNonContiguousRangesLink", "ID", "lnk_1006136_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_PostalCodeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on SocialSecurityNumberNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_SocialSecurityNumberNonContiguousRangesLink = new Control("SocialSecurityNumberNonContiguousRangesLink", "ID", "lnk_1006135_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_SocialSecurityNumberNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on StateNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRRW2_StateNonContiguousRangesLink = new Control("StateNonContiguousRangesLink", "ID", "lnk_1006137_PRRW2_PARAM");
			CPCommon.AssertEqual(true,PRRW2_StateNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_EmployeeNonContiguousRangesLink);
PRRW2_EmployeeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on EmployeeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNonContiguousRangesForm = new Control("EmployeeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_EmployeeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on EmployeeNonContiguousRanges_EmployeeNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNonContiguousRanges_EmployeeNonContiguousRangesTable = new Control("EmployeeNonContiguousRanges_EmployeeNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_EmployeeNonContiguousRanges_EmployeeNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on EmployeeNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNonContiguousRanges_Ok = new Control("EmployeeNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_EmployeeNonContiguousRanges_Ok);
if (PRRW2_EmployeeNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_EmployeeNonContiguousRanges_Ok.Click(5,5);
else PRRW2_EmployeeNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_SocialSecurityNumberNonContiguousRangesLink);
PRRW2_SocialSecurityNumberNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on SocialSecurityNumberNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_SocialSecurityNumberNonContiguousRangesForm = new Control("SocialSecurityNumberNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRSSNID_NCR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_SocialSecurityNumberNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on SocialSecurityNumberNonContiguousRanges_SocialSecurityNumberNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_SocialSecurityNumberNonContiguousRanges_SocialSecurityNumberNonContiguousRangesTable = new Control("SocialSecurityNumberNonContiguousRanges_SocialSecurityNumberNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRSSNID_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_SocialSecurityNumberNonContiguousRanges_SocialSecurityNumberNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on SocialSecurityNumberNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_SocialSecurityNumberNonContiguousRanges_Ok = new Control("SocialSecurityNumberNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRSSNID_NCR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_SocialSecurityNumberNonContiguousRanges_Ok);
if (PRRW2_SocialSecurityNumberNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_SocialSecurityNumberNonContiguousRanges_Ok.Click(5,5);
else PRRW2_SocialSecurityNumberNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_PostalCodeNonContiguousRangesLink);
PRRW2_PostalCodeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on PostalCodeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_PostalCodeNonContiguousRangesForm = new Control("PostalCodeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRPOSTALCD_NCR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_PostalCodeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on PostalCodeNonContiguousRanges_PostalCodeNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_PostalCodeNonContiguousRanges_PostalCodeNonContiguousRangesTable = new Control("PostalCodeNonContiguousRanges_PostalCodeNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRPOSTALCD_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_PostalCodeNonContiguousRanges_PostalCodeNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on PostalCodeNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_PostalCodeNonContiguousRanges_Ok = new Control("PostalCodeNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRPOSTALCD_NCR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_PostalCodeNonContiguousRanges_Ok);
if (PRRW2_PostalCodeNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_PostalCodeNonContiguousRanges_Ok.Click(5,5);
else PRRW2_PostalCodeNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_StateNonContiguousRangesLink);
PRRW2_StateNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on StateNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_StateNonContiguousRangesForm = new Control("StateNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRSTATECD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_StateNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on StateNonContiguousRanges_StateNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_StateNonContiguousRanges_StateNonContiguousRangesTable = new Control("StateNonContiguousRanges_StateNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRSTATECD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_StateNonContiguousRanges_StateNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on StateNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_StateNonContiguousRanges_Ok = new Control("StateNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRSTATECD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_StateNonContiguousRanges_Ok);
if (PRRW2_StateNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_StateNonContiguousRanges_Ok.Click(5,5);
else PRRW2_StateNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_OrganizationNonContiguousRangesLink);
PRRW2_OrganizationNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on OrganizationNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_OrganizationNonContiguousRangesForm = new Control("OrganizationNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_OrganizationNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on OrganizationNonContiguousRanges_OrganizationNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_OrganizationNonContiguousRanges_OrganizationNonContiguousRangesTable = new Control("OrganizationNonContiguousRanges_OrganizationNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_OrganizationNonContiguousRanges_OrganizationNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on OrganizationNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_OrganizationNonContiguousRanges_Ok = new Control("OrganizationNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_OrganizationNonContiguousRanges_Ok);
if (PRRW2_OrganizationNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_OrganizationNonContiguousRanges_Ok.Click(5,5);
else PRRW2_OrganizationNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_LocatorCodeNonContiguousRangesLink);
PRRW2_LocatorCodeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on LocatorCodeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_LocatorCodeNonContiguousRangesForm = new Control("LocatorCodeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRLOCATORCD_NCR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_LocatorCodeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on LocatorCodeNonContiguousRanges_LocatorCodeNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_LocatorCodeNonContiguousRanges_LocatorCodeNonContiguousRangesTable = new Control("LocatorCodeNonContiguousRanges_LocatorCodeNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRLOCATORCD_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_LocatorCodeNonContiguousRanges_LocatorCodeNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on LocatorCodeNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_LocatorCodeNonContiguousRanges_Ok = new Control("LocatorCodeNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRLOCATORCD_NCR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_LocatorCodeNonContiguousRanges_Ok);
if (PRRW2_LocatorCodeNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_LocatorCodeNonContiguousRanges_Ok.Click(5,5);
else PRRW2_LocatorCodeNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_EmployeeNameNonContiguousRangesLink);
PRRW2_EmployeeNameNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExists on EmployeeNameNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNameNonContiguousRangesForm = new Control("EmployeeNameNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRLASTFIRSTNAME_NCR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRRW2_EmployeeNameNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming VerifyExist on EmployeeNameNonContiguousRanges_EmployeeNameNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNameNonContiguousRanges_EmployeeNameNonContiguousRangesTable = new Control("EmployeeNameNonContiguousRanges_EmployeeNameNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRLASTFIRSTNAME_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRW2_EmployeeNameNonContiguousRanges_EmployeeNameNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRRW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRW2] Perfoming Click on EmployeeNameNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control PRRW2_EmployeeNameNonContiguousRanges_Ok = new Control("EmployeeNameNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRRW_NCRLASTFIRSTNAME_NCR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRRW2_EmployeeNameNonContiguousRanges_Ok);
if (PRRW2_EmployeeNameNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRRW2_EmployeeNameNonContiguousRanges_Ok.Click(5,5);
else PRRW2_EmployeeNameNonContiguousRanges_Ok.Click(4.5);


												
				CPCommon.CurrentComponent = "PRRW2";
							CPCommon.WaitControlDisplayed(PRRW2_MainForm);
formBttn = PRRW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

