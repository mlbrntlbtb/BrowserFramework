 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPPDCLS_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Processing", "xpath","//div[@class='navItem'][.='Payroll Processing']").Click();
new Control("Close Pay Period", "xpath","//div[@class='navItem'][.='Close Pay Period']").Click();


											Driver.SessionLogger.WriteLine("Close Pay Period");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MainForm Table");


												
				CPCommon.CurrentComponent = "PRPPDCLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPPDCLS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRPPDCLS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPPDCLS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRPPDCLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPPDCLS] Perfoming VerifyExist on PayCycleScheduleTable...", Logger.MessageType.INF);
			Control PRPPDCLS_PayCycleScheduleTable = new Control("PayCycleScheduleTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPPDCLS_PAYPDSCH_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPPDCLS_PayCycleScheduleTable.Exists());

												
				CPCommon.CurrentComponent = "PRPPDCLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPPDCLS] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PRPPDCLS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PRPPDCLS_MainForm);
IWebElement formBttn = PRPPDCLS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRPPDCLS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRPPDCLS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PRPPDCLS";
							CPCommon.AssertEqual(true,PRPPDCLS_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PRPPDCLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPPDCLS] Perfoming VerifyExists on PayCycleText...", Logger.MessageType.INF);
			Control PRPPDCLS_PayCycleText = new Control("PayCycleText", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,PRPPDCLS_PayCycleText.Exists());

												
				CPCommon.CurrentComponent = "PRPPDCLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPPDCLS] Perfoming VerifyExists on PayCycleScheduleForm...", Logger.MessageType.INF);
			Control PRPPDCLS_PayCycleScheduleForm = new Control("PayCycleScheduleForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPPDCLS_PAYPDSCH_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPPDCLS_PayCycleScheduleForm.Exists());

												
				CPCommon.CurrentComponent = "PRPPDCLS";
							CPCommon.WaitControlDisplayed(PRPPDCLS_MainForm);
formBttn = PRPPDCLS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

