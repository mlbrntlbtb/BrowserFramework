 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMACGRP_SMOKE : TestScript
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
new Control("Project Setup Controls", "xpath","//div[@class='navItem'][.='Project Setup Controls']").Click();
new Control("Manage Project Account Groups", "xpath","//div[@class='navItem'][.='Manage Project Account Groups']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMACGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMACGRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMACGRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMACGRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMACGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMACGRP] Perfoming VerifyExists on SetupInformation_ProjectAccountGroup...", Logger.MessageType.INF);
			Control PJMACGRP_SetupInformation_ProjectAccountGroup = new Control("SetupInformation_ProjectAccountGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_GRP_CD']");
			CPCommon.AssertEqual(true,PJMACGRP_SetupInformation_ProjectAccountGroup.Exists());

												
				CPCommon.CurrentComponent = "PJMACGRP";
							CPCommon.WaitControlDisplayed(PJMACGRP_MainForm);
IWebElement formBttn = PJMACGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMACGRP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMACGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMACGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMACGRP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMACGRP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMACGRP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMACGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMACGRP] Perfoming VerifyExist on ProjectAccountsTable...", Logger.MessageType.INF);
			Control PJMACGRP_ProjectAccountsTable = new Control("ProjectAccountsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMACGRP_ACCT_PROJCHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMACGRP_ProjectAccountsTable.Exists());

												
				CPCommon.CurrentComponent = "PJMACGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMACGRP] Perfoming VerifyExist on AccountFunctions_Table...", Logger.MessageType.INF);
			Control PJMACGRP_AccountFunctions_Table = new Control("AccountFunctions_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMACGRP_SACCTFUNC_ACCTCHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMACGRP_AccountFunctions_Table.Exists());

												
				CPCommon.CurrentComponent = "PJMACGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMACGRP] Perfoming VerifyExist on SelectedProjectAccounts_Table...", Logger.MessageType.INF);
			Control PJMACGRP_SelectedProjectAccounts_Table = new Control("SelectedProjectAccounts_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMACGRP_ACCTGRPSETUP_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMACGRP_SelectedProjectAccounts_Table.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMACGRP";
							CPCommon.WaitControlDisplayed(PJMACGRP_MainForm);
formBttn = PJMACGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

