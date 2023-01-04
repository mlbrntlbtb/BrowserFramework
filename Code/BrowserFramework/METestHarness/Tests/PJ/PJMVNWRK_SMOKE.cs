 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMVNWRK_SMOKE : TestScript
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
new Control("Manage Vendor Work Force", "xpath","//div[@class='navItem'][.='Manage Vendor Work Force']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMVNWRK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMVNWRK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMVNWRK_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMVNWRK_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMVNWRK";
							CPCommon.WaitControlDisplayed(PJMVNWRK_MainForm);
IWebElement formBttn = PJMVNWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMVNWRK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMVNWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMVNWRK_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVNWRK_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PJMVNWRK";
							CPCommon.WaitControlDisplayed(PJMVNWRK_MainForm);
formBttn = PJMVNWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJMVNWRK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJMVNWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming VerifyExist on VendorsTable...", Logger.MessageType.INF);
			Control PJMVNWRK_VendorsTable = new Control("VendorsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_VENDWRK_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVNWRK_VendorsTable.Exists());

												
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming VerifyExist on VendWorkForceTable...", Logger.MessageType.INF);
			Control PJMVNWRK_VendWorkForceTable = new Control("VendWorkForceTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJVEND_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVNWRK_VendWorkForceTable.Exists());

											Driver.SessionLogger.WriteLine("Assign PLC to Vendor Employee Work Force");


												
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming Click on VendWorkForce_AssignPLCToVendorWorkForceLink...", Logger.MessageType.INF);
			Control PJMVNWRK_VendWorkForce_AssignPLCToVendorWorkForceLink = new Control("VendWorkForce_AssignPLCToVendorWorkForceLink", "ID", "lnk_17574_PJM_PROJVEND_HDR");
			CPCommon.WaitControlDisplayed(PJMVNWRK_VendWorkForce_AssignPLCToVendorWorkForceLink);
PJMVNWRK_VendWorkForce_AssignPLCToVendorWorkForceLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PJMVNWRK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMVNWRK] Perfoming VerifyExist on PLCsTable...", Logger.MessageType.INF);
			Control PJMVNWRK_PLCsTable = new Control("PLCsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCAT_PLC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMVNWRK_PLCsTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMVNWRK";
							CPCommon.WaitControlDisplayed(PJMVNWRK_MainForm);
formBttn = PJMVNWRK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

