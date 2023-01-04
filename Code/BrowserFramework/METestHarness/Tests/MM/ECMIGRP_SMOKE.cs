 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMIGRP_SMOKE : TestScript
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
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("Engineering Change Controls", "xpath","//div[@class='navItem'][.='Engineering Change Controls']").Click();
new Control("Manage EC Impacted Functional Groups", "xpath","//div[@class='navItem'][.='Manage EC Impacted Functional Groups']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "ECMIGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMIGRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMIGRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMIGRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMIGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMIGRP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECMIGRP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMIGRP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMIGRP";
							CPCommon.WaitControlDisplayed(ECMIGRP_MainForm);
IWebElement formBttn = ECMIGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMIGRP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMIGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMIGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMIGRP] Perfoming VerifyExists on ImpactedGroup...", Logger.MessageType.INF);
			Control ECMIGRP_ImpactedGroup = new Control("ImpactedGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='IMPACT_GRP_CD']");
			CPCommon.AssertEqual(true,ECMIGRP_ImpactedGroup.Exists());

												
				CPCommon.CurrentComponent = "ECMIGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMIGRP] Perfoming VerifyExists on ImpactedGroupUsersForm...", Logger.MessageType.INF);
			Control ECMIGRP_ImpactedGroupUsersForm = new Control("ImpactedGroupUsersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMIGRP_IMPACTGRPUSER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMIGRP_ImpactedGroupUsersForm.Exists());

												
				CPCommon.CurrentComponent = "ECMIGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMIGRP] Perfoming VerifyExist on ImpactedGroupUsersTable...", Logger.MessageType.INF);
			Control ECMIGRP_ImpactedGroupUsersTable = new Control("ImpactedGroupUsersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMIGRP_IMPACTGRPUSER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMIGRP_ImpactedGroupUsersTable.Exists());

												
				CPCommon.CurrentComponent = "ECMIGRP";
							CPCommon.WaitControlDisplayed(ECMIGRP_ImpactedGroupUsersForm);
formBttn = ECMIGRP_ImpactedGroupUsersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMIGRP_ImpactedGroupUsersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMIGRP_ImpactedGroupUsersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMIGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMIGRP] Perfoming VerifyExists on ImpactedGroupUsers_UserDetails_User...", Logger.MessageType.INF);
			Control ECMIGRP_ImpactedGroupUsers_UserDetails_User = new Control("ImpactedGroupUsers_UserDetails_User", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMIGRP_IMPACTGRPUSER_']/ancestor::form[1]/descendant::*[@id='USER_ID']");
			CPCommon.AssertEqual(true,ECMIGRP_ImpactedGroupUsers_UserDetails_User.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "ECMIGRP";
							CPCommon.WaitControlDisplayed(ECMIGRP_MainForm);
formBttn = ECMIGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

