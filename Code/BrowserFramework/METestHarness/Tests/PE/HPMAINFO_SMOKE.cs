 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMAINFO_SMOKE : TestScript
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
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Work-Related Injuries/Illnesses", "xpath","//div[@class='navItem'][.='Work-Related Injuries/Illnesses']").Click();
new Control("Manage Accident Information", "xpath","//div[@class='navItem'][.='Manage Accident Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMAINFO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMAINFO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HPMAINFO_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HPMAINFO_Employee.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control HPMAINFO_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(HPMAINFO_MainFormTab);
IWebElement mTab = HPMAINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Employer Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on EmployerInfo_TaxableEntity...", Logger.MessageType.INF);
			Control HPMAINFO_EmployerInfo_TaxableEntity = new Control("EmployerInfo_TaxableEntity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY_ID']");
			CPCommon.AssertEqual(true,HPMAINFO_EmployerInfo_TaxableEntity.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_MainFormTab);
mTab = HPMAINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Accident Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on AccidentDetails_Location_StreetAddressOfAccident...", Logger.MessageType.INF);
			Control HPMAINFO_AccidentDetails_Location_StreetAddressOfAccident = new Control("AccidentDetails_Location_StreetAddressOfAccident", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCDNT_ADR_LN1']");
			CPCommon.AssertEqual(true,HPMAINFO_AccidentDetails_Location_StreetAddressOfAccident.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_MainFormTab);
mTab = HPMAINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Employee Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on EmployeeDetails_PersonalDetails_Phone...", Logger.MessageType.INF);
			Control HPMAINFO_EmployeeDetails_PersonalDetails_Phone = new Control("EmployeeDetails_PersonalDetails_Phone", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PHONE_ID']");
			CPCommon.AssertEqual(true,HPMAINFO_EmployeeDetails_PersonalDetails_Phone.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_MainFormTab);
mTab = HPMAINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Workers Compensation").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on WorkersCompensation_WorkersComp...", Logger.MessageType.INF);
			Control HPMAINFO_WorkersCompensation_WorkersComp = new Control("WorkersCompensation_WorkersComp", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_WORK_COMP_CD']");
			CPCommon.AssertEqual(true,HPMAINFO_WorkersCompensation_WorkersComp.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_MainFormTab);
mTab = HPMAINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Physician/Health Care").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on PhysicianHealthCare_Physician_Name...", Logger.MessageType.INF);
			Control HPMAINFO_PhysicianHealthCare_Physician_Name = new Control("PhysicianHealthCare_Physician_Name", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PHYS_NAME']");
			CPCommon.AssertEqual(true,HPMAINFO_PhysicianHealthCare_Physician_Name.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_MainForm);
IWebElement formBttn = HPMAINFO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMAINFO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMAINFO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMAINFO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAINFO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA300Link...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300Link = new Control("OSHA300Link", "ID", "lnk_1000707_HPMAINFO_ACCIDENTINFO_HDR");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA300Link.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA200Link...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200Link = new Control("OSHA200Link", "ID", "lnk_1000710_HPMAINFO_ACCIDENTINFO_HDR");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA200Link.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_OSHA300Link);
HPMAINFO_OSHA300Link.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA300Form...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300Form = new Control("OSHA300Form", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA300_HDR_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA300Form.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA300_Summary_OSHACaseNumber...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300_Summary_OSHACaseNumber = new Control("OSHA300_Summary_OSHACaseNumber", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA300_HDR_')]/ancestor::form[1]/descendant::*[@id='OSHA_CASE_ID']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA300_Summary_OSHACaseNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_OSHA300Form);
formBttn = HPMAINFO_OSHA300Form.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMAINFO_OSHA300Form.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMAINFO_OSHA300Form.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExist on OSHA300FormTable...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300FormTable = new Control("OSHA300FormTable", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA300_HDR_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA300FormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExist on OSHA300_InjuryAnatomyInformationFormTable...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300_InjuryAnatomyInformationFormTable = new Control("OSHA300_InjuryAnatomyInformationFormTable", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA300_CHILD_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA300_InjuryAnatomyInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming ClickButton on OSHA300_InjuryAnatomyInformationForm...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300_InjuryAnatomyInformationForm = new Control("OSHA300_InjuryAnatomyInformationForm", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA300_CHILD_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMAINFO_OSHA300_InjuryAnatomyInformationForm);
formBttn = HPMAINFO_OSHA300_InjuryAnatomyInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMAINFO_OSHA300_InjuryAnatomyInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMAINFO_OSHA300_InjuryAnatomyInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.AssertEqual(true,HPMAINFO_OSHA300_InjuryAnatomyInformationForm.Exists());

													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA300_InjuryAnatomyInformation_InjuryIllness...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA300_InjuryAnatomyInformation_InjuryIllness = new Control("OSHA300_InjuryAnatomyInformation_InjuryIllness", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA300_CHILD_')]/ancestor::form[1]/descendant::*[@id='INJURY_CD']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA300_InjuryAnatomyInformation_InjuryIllness.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_OSHA300Form);
formBttn = HPMAINFO_OSHA300Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_OSHA200Link);
HPMAINFO_OSHA200Link.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA200Form...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200Form = new Control("OSHA200Form", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA200_HDR_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA200Form.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA200_DateOfInjury...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200_DateOfInjury = new Control("OSHA200_DateOfInjury", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA200_HDR_')]/ancestor::form[1]/descendant::*[@id='DATE_OF_INJURY']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA200_DateOfInjury.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_OSHA200Form);
formBttn = HPMAINFO_OSHA200Form.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMAINFO_OSHA200Form.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMAINFO_OSHA200Form.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExist on OSHA200FormTable...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200FormTable = new Control("OSHA200FormTable", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA200_HDR_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA200FormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExist on OSHA200_InjuryAnatomyInformationFormTable...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200_InjuryAnatomyInformationFormTable = new Control("OSHA200_InjuryAnatomyInformationFormTable", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA200_CHILD_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA200_InjuryAnatomyInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming ClickButton on OSHA200_InjuryAnatomyInformationForm...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200_InjuryAnatomyInformationForm = new Control("OSHA200_InjuryAnatomyInformationForm", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA200_CHILD_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMAINFO_OSHA200_InjuryAnatomyInformationForm);
formBttn = HPMAINFO_OSHA200_InjuryAnatomyInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMAINFO_OSHA200_InjuryAnatomyInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMAINFO_OSHA200_InjuryAnatomyInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.AssertEqual(true,HPMAINFO_OSHA200_InjuryAnatomyInformationForm.Exists());

													
				CPCommon.CurrentComponent = "HPMAINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAINFO] Perfoming VerifyExists on OSHA200_InjuryAnatomyInformation_LineNo...", Logger.MessageType.INF);
			Control HPMAINFO_OSHA200_InjuryAnatomyInformation_LineNo = new Control("OSHA200_InjuryAnatomyInformation_LineNo", "xpath", "//div[starts-with(@id,'pr__HPMAINFO_OSHA200_CHILD_')]/ancestor::form[1]/descendant::*[@id='LN_NO']");
			CPCommon.AssertEqual(true,HPMAINFO_OSHA200_InjuryAnatomyInformation_LineNo.Exists());

												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_OSHA200Form);
formBttn = HPMAINFO_OSHA200Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "HPMAINFO";
							CPCommon.WaitControlDisplayed(HPMAINFO_MainForm);
formBttn = HPMAINFO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

