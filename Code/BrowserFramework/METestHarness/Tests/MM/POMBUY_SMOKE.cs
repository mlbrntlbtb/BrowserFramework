 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMBUY_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchasing Codes", "xpath","//div[@class='navItem'][.='Purchasing Codes']").Click();
new Control("Manage Buyers", "xpath","//div[@class='navItem'][.='Manage Buyers']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMBUY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMBUY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on BuyerID...", Logger.MessageType.INF);
			Control POMBUY_BuyerID = new Control("BuyerID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BUYER_ID']");
			CPCommon.AssertEqual(true,POMBUY_BuyerID.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
							CPCommon.WaitControlDisplayed(POMBUY_MainForm);
IWebElement formBttn = POMBUY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMBUY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMBUY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMBUY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBUY_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("link acct/org");


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming Click on LinkAcctOrgLink...", Logger.MessageType.INF);
			Control POMBUY_LinkAcctOrgLink = new Control("LinkAcctOrgLink", "ID", "lnk_1002208_POMBUY_BUYER");
			CPCommon.WaitControlDisplayed(POMBUY_LinkAcctOrgLink);
POMBUY_LinkAcctOrgLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on LinkAcctOrg_AcctOrgTable1Form...", Logger.MessageType.INF);
			Control POMBUY_LinkAcctOrg_AcctOrgTable1Form = new Control("LinkAcctOrg_AcctOrgTable1Form", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_VPOORGACCT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBUY_LinkAcctOrg_AcctOrgTable1Form.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on LinkAcctOrg_AcctOrgTable2Form...", Logger.MessageType.INF);
			Control POMBUY_LinkAcctOrg_AcctOrgTable2Form = new Control("LinkAcctOrg_AcctOrgTable2Form", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_BUYERORGACCT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBUY_LinkAcctOrg_AcctOrgTable2Form.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExist on LinkAcctOrg_AcctOrgTable2...", Logger.MessageType.INF);
			Control POMBUY_LinkAcctOrg_AcctOrgTable2 = new Control("LinkAcctOrg_AcctOrgTable2", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_BUYERORGACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBUY_LinkAcctOrg_AcctOrgTable2.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExist on LinkAcctOrg_AcctOrgTable1...", Logger.MessageType.INF);
			Control POMBUY_LinkAcctOrg_AcctOrgTable1 = new Control("LinkAcctOrg_AcctOrgTable1", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_VPOORGACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBUY_LinkAcctOrg_AcctOrgTable1.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
							CPCommon.WaitControlDisplayed(POMBUY_LinkAcctOrg_AcctOrgTable2Form);
formBttn = POMBUY_LinkAcctOrg_AcctOrgTable2Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Link Projects");


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming Click on LinkProjectsLink...", Logger.MessageType.INF);
			Control POMBUY_LinkProjectsLink = new Control("LinkProjectsLink", "ID", "lnk_1002211_POMBUY_BUYER");
			CPCommon.WaitControlDisplayed(POMBUY_LinkProjectsLink);
POMBUY_LinkProjectsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on LinkProjects_ProjectsTable1Form...", Logger.MessageType.INF);
			Control POMBUY_LinkProjects_ProjectsTable1Form = new Control("LinkProjects_ProjectsTable1Form", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_PROJ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBUY_LinkProjects_ProjectsTable1Form.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on LinkProjects_ProjectsTable2Form...", Logger.MessageType.INF);
			Control POMBUY_LinkProjects_ProjectsTable2Form = new Control("LinkProjects_ProjectsTable2Form", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_BUYERPROJ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBUY_LinkProjects_ProjectsTable2Form.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExist on LinkProjects_ProjectsTable1...", Logger.MessageType.INF);
			Control POMBUY_LinkProjects_ProjectsTable1 = new Control("LinkProjects_ProjectsTable1", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_PROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBUY_LinkProjects_ProjectsTable1.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExist on LinkProjects_ProjectsTable2...", Logger.MessageType.INF);
			Control POMBUY_LinkProjects_ProjectsTable2 = new Control("LinkProjects_ProjectsTable2", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_BUYERPROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBUY_LinkProjects_ProjectsTable2.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
							CPCommon.WaitControlDisplayed(POMBUY_LinkProjects_ProjectsTable2Form);
formBttn = POMBUY_LinkProjects_ProjectsTable2Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Link Vendors");


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming Click on LinkVendorsLink...", Logger.MessageType.INF);
			Control POMBUY_LinkVendorsLink = new Control("LinkVendorsLink", "ID", "lnk_1002061_POMBUY_BUYER");
			CPCommon.WaitControlDisplayed(POMBUY_LinkVendorsLink);
POMBUY_LinkVendorsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExists on LinkVendors_VendorsTableForm...", Logger.MessageType.INF);
			Control POMBUY_LinkVendors_VendorsTableForm = new Control("LinkVendors_VendorsTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_VEND_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBUY_LinkVendors_VendorsTableForm.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBUY] Perfoming VerifyExist on LinkVendors_VendorsTable...", Logger.MessageType.INF);
			Control POMBUY_LinkVendors_VendorsTable = new Control("LinkVendors_VendorsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBUY_VEND_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBUY_LinkVendors_VendorsTable.Exists());

												
				CPCommon.CurrentComponent = "POMBUY";
							CPCommon.WaitControlDisplayed(POMBUY_LinkVendors_VendorsTableForm);
formBttn = POMBUY_LinkVendors_VendorsTableForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "POMBUY";
							CPCommon.WaitControlDisplayed(POMBUY_MainForm);
formBttn = POMBUY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

