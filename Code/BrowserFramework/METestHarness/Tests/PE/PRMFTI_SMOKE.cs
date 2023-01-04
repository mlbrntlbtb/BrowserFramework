 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMFTI_SMOKE : TestScript
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
new Control("Federal Taxes", "xpath","//div[@class='navItem'][.='Federal Taxes']").Click();
new Control("Manage Federal Taxes", "xpath","//div[@class='navItem'][.='Manage Federal Taxes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMFTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMFTI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMFTI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMFTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTI] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMFTI_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMFTI_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PRMFTI";
							CPCommon.WaitControlDisplayed(PRMFTI_MainForm);
IWebElement formBttn = PRMFTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRMFTI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRMFTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRMFTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTI] Perfoming VerifyExists on PayrollYear...", Logger.MessageType.INF);
			Control PRMFTI_PayrollYear = new Control("PayrollYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PR_YR_NO']");
			CPCommon.AssertEqual(true,PRMFTI_PayrollYear.Exists());

											Driver.SessionLogger.WriteLine("FUTA Credit Reduction States From");


												
				CPCommon.CurrentComponent = "PRMFTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTI] Perfoming VerifyExists on FUTACreditReductionStatesLink...", Logger.MessageType.INF);
			Control PRMFTI_FUTACreditReductionStatesLink = new Control("FUTACreditReductionStatesLink", "ID", "lnk_15756_PRMFTI_FEDTAXINFO_HDR");
			CPCommon.AssertEqual(true,PRMFTI_FUTACreditReductionStatesLink.Exists());

												
				CPCommon.CurrentComponent = "PRMFTI";
							CPCommon.WaitControlDisplayed(PRMFTI_FUTACreditReductionStatesLink);
PRMFTI_FUTACreditReductionStatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMFTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTI] Perfoming VerifyExists on FUTACreditReductionStatesForm...", Logger.MessageType.INF);
			Control PRMFTI_FUTACreditReductionStatesForm = new Control("FUTACreditReductionStatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFTI_FUTA_CREDIT_REDUC_ST_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMFTI_FUTACreditReductionStatesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMFTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTI] Perfoming VerifyExist on FUTACreditReductionStatesTable...", Logger.MessageType.INF);
			Control PRMFTI_FUTACreditReductionStatesTable = new Control("FUTACreditReductionStatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFTI_FUTA_CREDIT_REDUC_ST_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMFTI_FUTACreditReductionStatesTable.Exists());

												
				CPCommon.CurrentComponent = "PRMFTI";
							CPCommon.WaitControlDisplayed(PRMFTI_FUTACreditReductionStatesForm);
formBttn = PRMFTI_FUTACreditReductionStatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMFTI";
							CPCommon.WaitControlDisplayed(PRMFTI_MainForm);
formBttn = PRMFTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

