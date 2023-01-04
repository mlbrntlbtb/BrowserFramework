 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMW2_SMOKE : TestScript
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
new Control("Year-End Processing", "xpath","//div[@class='navItem'][.='Year-End Processing']").Click();
new Control("Manage W-2s", "xpath","//div[@class='navItem'][.='Manage W-2s']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMW2_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMW2_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on PayrollYear...", Logger.MessageType.INF);
			Control PRMW2_PayrollYear = new Control("PayrollYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PR_YR_NO']");
			CPCommon.AssertEqual(true,PRMW2_PayrollYear.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_MainForm);
IWebElement formBttn = PRMW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMW2_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMW2_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMW2_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on Box12Link...", Logger.MessageType.INF);
			Control PRMW2_Box12Link = new Control("Box12Link", "ID", "lnk_1002800_PRMW2_FEDW2FILE_HDR");
			CPCommon.AssertEqual(true,PRMW2_Box12Link.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_Box12Link);
PRMW2_Box12Link.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on Box12Form...", Logger.MessageType.INF);
			Control PRMW2_Box12Form = new Control("Box12Form", "xpath", "//div[starts-with(@id,'pr__PRMW2_BOX12W2FILE_CHILD1_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMW2_Box12Form.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExist on Box12_Box12LinkTable...", Logger.MessageType.INF);
			Control PRMW2_Box12_Box12LinkTable = new Control("Box12_Box12LinkTable", "xpath", "//div[starts-with(@id,'pr__PRMW2_BOX12W2FILE_CHILD1_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMW2_Box12_Box12LinkTable.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_Box12Form);
formBttn = PRMW2_Box12Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("State");


												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on StateLink...", Logger.MessageType.INF);
			Control PRMW2_StateLink = new Control("StateLink", "ID", "lnk_1002802_PRMW2_FEDW2FILE_HDR");
			CPCommon.AssertEqual(true,PRMW2_StateLink.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_StateLink);
PRMW2_StateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on StateForm...", Logger.MessageType.INF);
			Control PRMW2_StateForm = new Control("StateForm", "xpath", "//div[starts-with(@id,'pr__PRMW2_STATEW2FILE_CHILD2_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMW2_StateForm.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExist on State_StateLinkTable...", Logger.MessageType.INF);
			Control PRMW2_State_StateLinkTable = new Control("State_StateLinkTable", "xpath", "//div[starts-with(@id,'pr__PRMW2_STATEW2FILE_CHILD2_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMW2_State_StateLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_StateForm);
formBttn = PRMW2_StateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Local");


												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on LocalLink...", Logger.MessageType.INF);
			Control PRMW2_LocalLink = new Control("LocalLink", "ID", "lnk_1002803_PRMW2_FEDW2FILE_HDR");
			CPCommon.AssertEqual(true,PRMW2_LocalLink.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_LocalLink);
PRMW2_LocalLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExists on LocalForm...", Logger.MessageType.INF);
			Control PRMW2_LocalForm = new Control("LocalForm", "xpath", "//div[starts-with(@id,'pr__PRMW2_LOCALW2FILE_CHILD3_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMW2_LocalForm.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMW2] Perfoming VerifyExist on Local_LocalLinkTable...", Logger.MessageType.INF);
			Control PRMW2_Local_LocalLinkTable = new Control("Local_LocalLinkTable", "xpath", "//div[starts-with(@id,'pr__PRMW2_LOCALW2FILE_CHILD3_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMW2_Local_LocalLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_LocalForm);
formBttn = PRMW2_LocalForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMW2";
							CPCommon.WaitControlDisplayed(PRMW2_MainForm);
formBttn = PRMW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

