 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMVEWRK_SMOKE : TestScript
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
new Control("Project Labor", "xpath","//div[@class='navItem'][.='Project Labor']").Click();
new Control("Manage Vendor Employee Work Force", "xpath","//div[@class='navItem'][.='Manage Vendor Employee Work Force']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMVEWRK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMVEWRK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control PJMVEWRK_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMVEWRK_Identification_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMVEWRK";
							CPCommon.WaitControlDisplayed(PJMVEWRK_MainForm);
IWebElement formBttn = PJMVEWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMVEWRK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMVEWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMVEWRK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVEWRK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMVEWRK";
							CPCommon.WaitControlDisplayed(PJMVEWRK_MainForm);
formBttn = PJMVEWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJMVEWRK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJMVEWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("Vendornmn EMPloyeesnmn");


												
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming VerifyExist on VendorEmployees_Table...", Logger.MessageType.INF);
			Control PJMVEWRK_VendorEmployees_Table = new Control("VendorEmployees_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_VENDEMPL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVEWRK_VendorEmployees_Table.Exists());

												
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming VerifyExist on ProjectVendorEmployees_Table...", Logger.MessageType.INF);
			Control PJMVEWRK_ProjectVendorEmployees_Table = new Control("ProjectVendorEmployees_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_VENDEMPL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVEWRK_ProjectVendorEmployees_Table.Exists());

											Driver.SessionLogger.WriteLine("Assign PLC to Vendor Employee Work Force");


												
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming Click on AssignPLCToVendorEmployeeWorkForceLink...", Logger.MessageType.INF);
			Control PJMVEWRK_AssignPLCToVendorEmployeeWorkForceLink = new Control("AssignPLCToVendorEmployeeWorkForceLink", "ID", "lnk_17594_PJM_PROJVENDEMPL_HDR");
			CPCommon.WaitControlDisplayed(PJMVEWRK_AssignPLCToVendorEmployeeWorkForceLink);
PJMVEWRK_AssignPLCToVendorEmployeeWorkForceLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PJMVEWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVEWRK] Perfoming VerifyExist on AssignPLCToVendorEmployeeWorkForce_PLCsAssignedtoVendorEmployeeWorkForceTable...", Logger.MessageType.INF);
			Control PJMVEWRK_AssignPLCToVendorEmployeeWorkForce_PLCsAssignedtoVendorEmployeeWorkForceTable = new Control("AssignPLCToVendorEmployeeWorkForce_PLCsAssignedtoVendorEmployeeWorkForceTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJVENDEMPLLABCAT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVEWRK_AssignPLCToVendorEmployeeWorkForce_PLCsAssignedtoVendorEmployeeWorkForceTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMVEWRK";
							CPCommon.WaitControlDisplayed(PJMVEWRK_MainForm);
formBttn = PJMVEWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

