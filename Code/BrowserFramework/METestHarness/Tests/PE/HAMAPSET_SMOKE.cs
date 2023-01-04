 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HAMAPSET_SMOKE : TestScript
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
new Control("Affirmative Action", "xpath","//div[@class='deptItem'][.='Affirmative Action']").Click();
new Control("Affirmative Action Plan Information", "xpath","//div[@class='navItem'][.='Affirmative Action Plan Information']").Click();
new Control("Manage Affirmative Action Plans", "xpath","//div[@class='navItem'][.='Manage Affirmative Action Plans']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HAMAPSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HAMAPSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExists on AffirmativeActionPlanCode...", Logger.MessageType.INF);
			Control HAMAPSET_AffirmativeActionPlanCode = new Control("AffirmativeActionPlanCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='AFF_PLAN_CD']");
			CPCommon.AssertEqual(true,HAMAPSET_AffirmativeActionPlanCode.Exists());

											Driver.SessionLogger.WriteLine("Main Table");


												
				CPCommon.CurrentComponent = "HAMAPSET";
							CPCommon.WaitControlDisplayed(HAMAPSET_MainForm);
IWebElement formBttn = HAMAPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HAMAPSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HAMAPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HAMAPSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAMAPSET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExists on AssignLaborLocationsLink...", Logger.MessageType.INF);
			Control HAMAPSET_AssignLaborLocationsLink = new Control("AssignLaborLocationsLink", "ID", "lnk_1002360_HAMAPSET_HAFFPLANSETUP_MAIN");
			CPCommon.AssertEqual(true,HAMAPSET_AssignLaborLocationsLink.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
							CPCommon.WaitControlDisplayed(HAMAPSET_AssignLaborLocationsLink);
HAMAPSET_AssignLaborLocationsLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Assign Labor Locations");


												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExist on AssignLaborLocationsTable...", Logger.MessageType.INF);
			Control HAMAPSET_AssignLaborLocationsTable = new Control("AssignLaborLocationsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMAPSET_LABLOCATION_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAMAPSET_AssignLaborLocationsTable.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExists on WeightedPercentagesLink...", Logger.MessageType.INF);
			Control HAMAPSET_WeightedPercentagesLink = new Control("WeightedPercentagesLink", "ID", "lnk_1002363_HAMAPSET_HAFFPLANSETUP_MAIN");
			CPCommon.AssertEqual(true,HAMAPSET_WeightedPercentagesLink.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
							CPCommon.WaitControlDisplayed(HAMAPSET_WeightedPercentagesLink);
HAMAPSET_WeightedPercentagesLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Weighted Percentages");


												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExist on WeightedPercentages_Table...", Logger.MessageType.INF);
			Control HAMAPSET_WeightedPercentages_Table = new Control("WeightedPercentages_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMAPSET_HAFFWTPCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAMAPSET_WeightedPercentages_Table.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExists on ApplicantDataLink...", Logger.MessageType.INF);
			Control HAMAPSET_ApplicantDataLink = new Control("ApplicantDataLink", "ID", "lnk_1002364_HAMAPSET_HAFFPLANSETUP_MAIN");
			CPCommon.AssertEqual(true,HAMAPSET_ApplicantDataLink.Exists());

												
				CPCommon.CurrentComponent = "HAMAPSET";
							CPCommon.WaitControlDisplayed(HAMAPSET_ApplicantDataLink);
HAMAPSET_ApplicantDataLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Application Data");


												
				CPCommon.CurrentComponent = "HAMAPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMAPSET] Perfoming VerifyExist on ApplicantData_Table...", Logger.MessageType.INF);
			Control HAMAPSET_ApplicantData_Table = new Control("ApplicantData_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMAPSET_HAPPLICNTDATA_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAMAPSET_ApplicantData_Table.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "HAMAPSET";
							CPCommon.WaitControlDisplayed(HAMAPSET_MainForm);
formBttn = HAMAPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

