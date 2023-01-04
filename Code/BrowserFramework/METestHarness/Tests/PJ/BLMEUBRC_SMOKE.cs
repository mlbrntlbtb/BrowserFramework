 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMEUBRC_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Unbilled Receivables", "xpath","//div[@class='navItem'][.='Unbilled Receivables']").Click();
new Control("Edit Unbilled Reason Codes", "xpath","//div[@class='navItem'][.='Edit Unbilled Reason Codes']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Edit Unbilled Reason Codes", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMEUBRC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEUBRC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMEUBRC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMEUBRC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMEUBRC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEUBRC] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMEUBRC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMEUBRC_MainForm);
IWebElement formBttn = BLMEUBRC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMEUBRC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMEUBRC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMEUBRC";
							CPCommon.AssertEqual(true,BLMEUBRC_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMEUBRC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEUBRC] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMEUBRC_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMEUBRC_Project.Exists());

											Driver.SessionLogger.WriteLine("UNBILLED AMOUNT");


												
				CPCommon.CurrentComponent = "BLMEUBRC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEUBRC] Perfoming VerifyExist on UnbilledAmountFormTable...", Logger.MessageType.INF);
			Control BLMEUBRC_UnbilledAmountFormTable = new Control("UnbilledAmountFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMEUBRC_RPTUNBILLEDRSN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMEUBRC_UnbilledAmountFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMEUBRC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEUBRC] Perfoming ClickButton on UnbilledAmountForm...", Logger.MessageType.INF);
			Control BLMEUBRC_UnbilledAmountForm = new Control("UnbilledAmountForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMEUBRC_RPTUNBILLEDRSN_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMEUBRC_UnbilledAmountForm);
formBttn = BLMEUBRC_UnbilledAmountForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMEUBRC_UnbilledAmountForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMEUBRC_UnbilledAmountForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMEUBRC";
							CPCommon.AssertEqual(true,BLMEUBRC_UnbilledAmountForm.Exists());

													
				CPCommon.CurrentComponent = "BLMEUBRC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEUBRC] Perfoming VerifyExists on UnbilledAmount_Project...", Logger.MessageType.INF);
			Control BLMEUBRC_UnbilledAmount_Project = new Control("UnbilledAmount_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMEUBRC_RPTUNBILLEDRSN_CHLD_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMEUBRC_UnbilledAmount_Project.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMEUBRC";
							CPCommon.WaitControlDisplayed(BLMEUBRC_MainForm);
formBttn = BLMEUBRC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

