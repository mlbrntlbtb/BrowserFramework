 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMPERR_SMOKE : TestScript
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
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Compensation Budgeting", "xpath","//div[@class='navItem'][.='Compensation Budgeting']").Click();
new Control("Manage Projected Performance Ratings", "xpath","//div[@class='navItem'][.='Manage Projected Performance Ratings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMPERR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMPERR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control HSMPERR_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,HSMPERR_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "HSMPERR";
							CPCommon.WaitControlDisplayed(HSMPERR_MainForm);
IWebElement formBttn = HSMPERR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSMPERR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSMPERR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMPERR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMPERR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming VerifyExists on ProjectedPerformanceRatingPercentDetailLink...", Logger.MessageType.INF);
			Control HSMPERR_ProjectedPerformanceRatingPercentDetailLink = new Control("ProjectedPerformanceRatingPercentDetailLink", "ID", "lnk_1001778_HSMPERR_PLANRATINGBYGRD_HDR");
			CPCommon.AssertEqual(true,HSMPERR_ProjectedPerformanceRatingPercentDetailLink.Exists());

												
				CPCommon.CurrentComponent = "HSMPERR";
							CPCommon.WaitControlDisplayed(HSMPERR_ProjectedPerformanceRatingPercentDetailLink);
HSMPERR_ProjectedPerformanceRatingPercentDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming VerifyExist on ProjectedPerformanceRatingPercentDetailFormTable...", Logger.MessageType.INF);
			Control HSMPERR_ProjectedPerformanceRatingPercentDetailFormTable = new Control("ProjectedPerformanceRatingPercentDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMPERR_PLANRATINGBYGRD_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMPERR_ProjectedPerformanceRatingPercentDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming ClickButton on ProjectedPerformanceRatingPercentDetailForm...", Logger.MessageType.INF);
			Control HSMPERR_ProjectedPerformanceRatingPercentDetailForm = new Control("ProjectedPerformanceRatingPercentDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMPERR_PLANRATINGBYGRD_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HSMPERR_ProjectedPerformanceRatingPercentDetailForm);
formBttn = HSMPERR_ProjectedPerformanceRatingPercentDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMPERR_ProjectedPerformanceRatingPercentDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMPERR_ProjectedPerformanceRatingPercentDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMPERR";
							CPCommon.AssertEqual(true,HSMPERR_ProjectedPerformanceRatingPercentDetailForm.Exists());

													
				CPCommon.CurrentComponent = "HSMPERR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMPERR] Perfoming VerifyExists on ProjectedPerformanceRatingPercentDetail_CompensationPlan...", Logger.MessageType.INF);
			Control HSMPERR_ProjectedPerformanceRatingPercentDetail_CompensationPlan = new Control("ProjectedPerformanceRatingPercentDetail_CompensationPlan", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMPERR_PLANRATINGBYGRD_DTL_']/ancestor::form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,HSMPERR_ProjectedPerformanceRatingPercentDetail_CompensationPlan.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMPERR";
							CPCommon.WaitControlDisplayed(HSMPERR_MainForm);
formBttn = HSMPERR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

