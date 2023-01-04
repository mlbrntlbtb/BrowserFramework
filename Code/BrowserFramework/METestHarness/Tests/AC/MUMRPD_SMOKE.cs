 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUMRPD_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Multicurrency", "xpath","//div[@class='deptItem'][.='Multicurrency']").Click();
new Control("Exchange Rates", "xpath","//div[@class='navItem'][.='Exchange Rates']").Click();
new Control("Manage Exchange Rates by Period", "xpath","//div[@class='navItem'][.='Manage Exchange Rates by Period']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUMRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUMRPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUMRPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUMRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRPD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MUMRPD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMRPD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMRPD";
							CPCommon.WaitControlDisplayed(MUMRPD_MainForm);
IWebElement formBttn = MUMRPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMRPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMRPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRPD] Perfoming VerifyExists on RateGroup...", Logger.MessageType.INF);
			Control MUMRPD_RateGroup = new Control("RateGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RT_GRP_ID']");
			CPCommon.AssertEqual(true,MUMRPD_RateGroup.Exists());

											Driver.SessionLogger.WriteLine("Period Information Form");


												
				CPCommon.CurrentComponent = "MUMRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRPD] Perfoming VerifyExists on PeriodInformationForm...", Logger.MessageType.INF);
			Control MUMRPD_PeriodInformationForm = new Control("PeriodInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRPD_RTBYPD_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MUMRPD_PeriodInformationForm.Exists());

												
				CPCommon.CurrentComponent = "MUMRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRPD] Perfoming VerifyExist on PeriodInformationFormTable...", Logger.MessageType.INF);
			Control MUMRPD_PeriodInformationFormTable = new Control("PeriodInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRPD_RTBYPD_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMRPD_PeriodInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMRPD";
							CPCommon.WaitControlDisplayed(MUMRPD_PeriodInformationForm);
formBttn = MUMRPD_PeriodInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMRPD_PeriodInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMRPD_PeriodInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRPD] Perfoming VerifyExists on PeriodInformation_FiscalYear...", Logger.MessageType.INF);
			Control MUMRPD_PeriodInformation_FiscalYear = new Control("PeriodInformation_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRPD_RTBYPD_CTW_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,MUMRPD_PeriodInformation_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUMRPD";
							CPCommon.WaitControlDisplayed(MUMRPD_MainForm);
formBttn = MUMRPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

