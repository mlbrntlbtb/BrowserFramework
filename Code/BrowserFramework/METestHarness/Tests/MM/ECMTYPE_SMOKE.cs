 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMTYPE_SMOKE : TestScript
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
new Control("Manage Engineering Change Types", "xpath","//div[@class='navItem'][.='Manage Engineering Change Types']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "ECMTYPE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMTYPE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECMTYPE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMTYPE_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMTYPE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMTYPE] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control ECMTYPE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ECMTYPE_MainForm);
IWebElement formBttn = ECMTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMTYPE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMTYPE";
							CPCommon.AssertEqual(true,ECMTYPE_MainForm.Exists());

													
				CPCommon.CurrentComponent = "ECMTYPE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMTYPE] Perfoming VerifyExists on EcType_EngineeringChangeType...", Logger.MessageType.INF);
			Control ECMTYPE_EcType_EngineeringChangeType = new Control("EcType_EngineeringChangeType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EC_TYPE_CD']");
			CPCommon.AssertEqual(true,ECMTYPE_EcType_EngineeringChangeType.Exists());

											Driver.SessionLogger.WriteLine("SELECTED IMPACT GROUP LINK");


												
				CPCommon.CurrentComponent = "ECMTYPE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMTYPE] Perfoming Click on EcType_ECTypeDetails_Defaults_SelectedImpactedGroupLink...", Logger.MessageType.INF);
			Control ECMTYPE_EcType_ECTypeDetails_Defaults_SelectedImpactedGroupLink = new Control("EcType_ECTypeDetails_Defaults_SelectedImpactedGroupLink", "ID", "lnk_1002473_ECMTYPE_ECTYPE");
			CPCommon.WaitControlDisplayed(ECMTYPE_EcType_ECTypeDetails_Defaults_SelectedImpactedGroupLink);
ECMTYPE_EcType_ECTypeDetails_Defaults_SelectedImpactedGroupLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ECMTYPE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMTYPE] Perfoming VerifyExist on SelectedImpactedGroupFormTable...", Logger.MessageType.INF);
			Control ECMTYPE_SelectedImpactedGroupFormTable = new Control("SelectedImpactedGroupFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMTYPE_ECTYPEIMPACTGRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMTYPE_SelectedImpactedGroupFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ECMTYPE";
							CPCommon.WaitControlDisplayed(ECMTYPE_MainForm);
formBttn = ECMTYPE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

