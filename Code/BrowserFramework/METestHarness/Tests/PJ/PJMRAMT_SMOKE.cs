 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMRAMT_SMOKE : TestScript
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
new Control("Revenue", "xpath","//div[@class='navItem'][.='Revenue']").Click();
new Control("Manage Revenue Formulas", "xpath","//div[@class='navItem'][.='Manage Revenue Formulas']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Revenue Formulas", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "PJMRAMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMRAMT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMRAMT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMRAMT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMRAMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMRAMT] Perfoming VerifyExists on RevenueFormula...", Logger.MessageType.INF);
			Control PJMRAMT_RevenueFormula = new Control("RevenueFormula", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_REV_FORMULA_CD']");
			CPCommon.AssertEqual(true,PJMRAMT_RevenueFormula.Exists());

												
				CPCommon.CurrentComponent = "PJMRAMT";
							CPCommon.WaitControlDisplayed(PJMRAMT_MainForm);
IWebElement formBttn = PJMRAMT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMRAMT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMRAMT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMRAMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMRAMT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMRAMT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMRAMT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMRAMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMRAMT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PJMRAMT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMRAMT_PROJREVSETUP_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMRAMT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMRAMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMRAMT] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PJMRAMT_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMRAMT_PROJREVSETUP_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMRAMT_ChildForm.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMRAMT";
							CPCommon.WaitControlDisplayed(PJMRAMT_MainForm);
formBttn = PJMRAMT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

