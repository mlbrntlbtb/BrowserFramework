 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMALGRP_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost Pool Controls", "xpath","//div[@class='navItem'][.='Cost Pool Controls']").Click();
new Control("Manage Allocation Groups", "xpath","//div[@class='navItem'][.='Manage Allocation Groups']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMALGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALGRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMALGRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMALGRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMALGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALGRP] Perfoming VerifyExists on AllocationGroupNumber...", Logger.MessageType.INF);
			Control PJMALGRP_AllocationGroupNumber = new Control("AllocationGroupNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALLOC_GRP_NO']");
			CPCommon.AssertEqual(true,PJMALGRP_AllocationGroupNumber.Exists());

												
				CPCommon.CurrentComponent = "PJMALGRP";
							CPCommon.WaitControlDisplayed(PJMALGRP_MainForm);
IWebElement formBttn = PJMALGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMALGRP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMALGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMALGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALGRP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMALGRP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMALGRP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("ASSIGN POOL TYPE");


												
				CPCommon.CurrentComponent = "PJMALGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALGRP] Perfoming VerifyExists on AssignPoolTypeForm...", Logger.MessageType.INF);
			Control PJMALGRP_AssignPoolTypeForm = new Control("AssignPoolTypeForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALGRP_POOLTYPE_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMALGRP_AssignPoolTypeForm.Exists());

												
				CPCommon.CurrentComponent = "PJMALGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALGRP] Perfoming VerifyExist on AssignPoolTypeFormTable...", Logger.MessageType.INF);
			Control PJMALGRP_AssignPoolTypeFormTable = new Control("AssignPoolTypeFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALGRP_POOLTYPE_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMALGRP_AssignPoolTypeFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMALGRP";
							CPCommon.WaitControlDisplayed(PJMALGRP_MainForm);
formBttn = PJMALGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

