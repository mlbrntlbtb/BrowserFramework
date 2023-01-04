 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPSYNCH_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Administrative Utilities", "xpath","//div[@class='navItem'][.='Administrative Utilities']").Click();
new Control("Synchronize Project Master Data and Project Edit Data", "xpath","//div[@class='navItem'][.='Synchronize Project Master Data and Project Edit Data']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPSYNCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPSYNCH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on SelectionRanges_Option_Projects...", Logger.MessageType.INF);
			Control PJPSYNCH_SelectionRanges_Option_Projects = new Control("SelectionRanges_Option_Projects", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_RANGE']");
			CPCommon.AssertEqual(true,PJPSYNCH_SelectionRanges_Option_Projects.Exists());

											Driver.SessionLogger.WriteLine("Discrepancies");


												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExist on DiscrepanciesFormTable...", Logger.MessageType.INF);
			Control PJPSYNCH_DiscrepanciesFormTable = new Control("DiscrepanciesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_ZPJPTOOLPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPSYNCH_DiscrepanciesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming ClickButton on DiscrepanciesForm...", Logger.MessageType.INF);
			Control PJPSYNCH_DiscrepanciesForm = new Control("DiscrepanciesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_ZPJPTOOLPROJ_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJPSYNCH_DiscrepanciesForm);
IWebElement formBttn = PJPSYNCH_DiscrepanciesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJPSYNCH_DiscrepanciesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJPSYNCH_DiscrepanciesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJPSYNCH";
							CPCommon.AssertEqual(true,PJPSYNCH_DiscrepanciesForm.Exists());

													
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on Discrepancies_Project...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_Project = new Control("Discrepancies_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_ZPJPTOOLPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJPSYNCH_Discrepancies_Project.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming Select on Discrepancies_DiscrepanciesTab...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_DiscrepanciesTab = new Control("Discrepancies_DiscrepanciesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_ZPJPTOOLPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJPSYNCH_Discrepancies_DiscrepanciesTab);
IWebElement mTab = PJPSYNCH_Discrepancies_DiscrepanciesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Project Master").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on Discrepancies_ProjectMaster_ProjectName...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_ProjectMaster_ProjectName = new Control("Discrepancies_ProjectMaster_ProjectName", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_ZPJPTOOLPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='P_PROJ_NAME']");
			CPCommon.AssertEqual(true,PJPSYNCH_Discrepancies_ProjectMaster_ProjectName.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
							CPCommon.WaitControlDisplayed(PJPSYNCH_Discrepancies_DiscrepanciesTab);
mTab = PJPSYNCH_Discrepancies_DiscrepanciesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Project Edit").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on Discrepancies_ProjectEdit_ProjectName...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_ProjectEdit_ProjectName = new Control("Discrepancies_ProjectEdit_ProjectName", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_ZPJPTOOLPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='PE_PROJ_NAME']");
			CPCommon.AssertEqual(true,PJPSYNCH_Discrepancies_ProjectEdit_ProjectName.Exists());

											Driver.SessionLogger.WriteLine("Enter Synchrnization Password");


												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on Discrepancies_EnterSynchronizationPasswordLink...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_EnterSynchronizationPasswordLink = new Control("Discrepancies_EnterSynchronizationPasswordLink", "ID", "lnk_3493_PJPSYNCH_ZPJPTOOLPROJ_CTW");
			CPCommon.AssertEqual(true,PJPSYNCH_Discrepancies_EnterSynchronizationPasswordLink.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
							CPCommon.WaitControlDisplayed(PJPSYNCH_Discrepancies_EnterSynchronizationPasswordLink);
PJPSYNCH_Discrepancies_EnterSynchronizationPasswordLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on Discrepancies_EnterSynchronizationPasswordForm...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_EnterSynchronizationPasswordForm = new Control("Discrepancies_EnterSynchronizationPasswordForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_SYNCH_PW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJPSYNCH_Discrepancies_EnterSynchronizationPasswordForm.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPSYNCH] Perfoming VerifyExists on Discrepancies_EnterSynchronizationPassword_FromProjectEditToProjectMasterPassword...", Logger.MessageType.INF);
			Control PJPSYNCH_Discrepancies_EnterSynchronizationPassword_FromProjectEditToProjectMasterPassword = new Control("Discrepancies_EnterSynchronizationPassword_FromProjectEditToProjectMasterPassword", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPSYNCH_SYNCH_PW_']/ancestor::form[1]/descendant::*[@id='PE_TO_PM_PW']");
			CPCommon.AssertEqual(true,PJPSYNCH_Discrepancies_EnterSynchronizationPassword_FromProjectEditToProjectMasterPassword.Exists());

												
				CPCommon.CurrentComponent = "PJPSYNCH";
							CPCommon.WaitControlDisplayed(PJPSYNCH_Discrepancies_EnterSynchronizationPasswordForm);
formBttn = PJPSYNCH_Discrepancies_EnterSynchronizationPasswordForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJPSYNCH";
							CPCommon.WaitControlDisplayed(PJPSYNCH_MainForm);
formBttn = PJPSYNCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

