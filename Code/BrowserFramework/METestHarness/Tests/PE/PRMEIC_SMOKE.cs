 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMEIC_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Federal Taxes", "xpath","//div[@class='navItem'][.='Federal Taxes']").Click();
new Control("Manage Advance Earned Income Credit Tables", "xpath","//div[@class='navItem'][.='Manage Advance Earned Income Credit Tables']").Click();


												
				CPCommon.CurrentComponent = "PRMEIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEIC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMEIC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMEIC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMEIC";
							CPCommon.WaitControlDisplayed(PRMEIC_MainForm);
IWebElement formBttn = PRMEIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMEIC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMEIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMEIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEIC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMEIC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMEIC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMEIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEIC] Perfoming Click on AdvanceEarnedIncomeCreditAndNonresidentAlienTableLink...", Logger.MessageType.INF);
			Control PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableLink = new Control("AdvanceEarnedIncomeCreditAndNonresidentAlienTableLink", "ID", "lnk_1001147_PRMEIC_EICTAXTBL_HDR");
			CPCommon.WaitControlDisplayed(PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableLink);
PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PRMEIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEIC] Perfoming VerifyExists on AdvanceEarnedIncomeCreditAndNonresidentAlienTableForm...", Logger.MessageType.INF);
			Control PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableForm = new Control("AdvanceEarnedIncomeCreditAndNonresidentAlienTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMEIC_EICTAXTBL_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableForm.Exists());

												
				CPCommon.CurrentComponent = "PRMEIC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMEIC] Perfoming VerifyExist on AdvanceEarnedIncomeCreditAndNonresidentAlienTableFormTable...", Logger.MessageType.INF);
			Control PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableFormTable = new Control("AdvanceEarnedIncomeCreditAndNonresidentAlienTableFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMEIC_EICTAXTBL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMEIC_AdvanceEarnedIncomeCreditAndNonresidentAlienTableFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMEIC";
							CPCommon.WaitControlDisplayed(PRMEIC_MainForm);
formBttn = PRMEIC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

