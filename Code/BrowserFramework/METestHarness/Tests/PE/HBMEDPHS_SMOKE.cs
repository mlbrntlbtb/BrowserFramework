 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMEDPHS_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee FSA/HSA Information", "xpath","//div[@class='navItem'][.='Employee FSA/HSA Information']").Click();
new Control("Manage Medical Care FSA History", "xpath","//div[@class='navItem'][.='Manage Medical Care FSA History']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "HBMDEPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMDEPHS] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control HBMDEPHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(HBMDEPHS_MainForm);
IWebElement formBttn = HBMDEPHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMDEPHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMDEPHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HBMEDPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDPHS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMEDPHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMEDPHS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEDPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDPHS] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HBMEDPHS_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HBMEDPHS_Employee.Exists());

												
				CPCommon.CurrentComponent = "HBMEDPHS";
							CPCommon.WaitControlDisplayed(HBMEDPHS_MainForm);
formBttn = HBMEDPHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMEDPHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMEDPHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMEDPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDPHS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMEDPHS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEDPHS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Details Form");


												
				CPCommon.CurrentComponent = "HBMEDPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDPHS] Perfoming VerifyExists on DetailsForm...", Logger.MessageType.INF);
			Control HBMEDPHS_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEDPHS_HBMEDFSAHS_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMEDPHS_DetailsForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEDPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDPHS] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control HBMEDPHS_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEDPHS_HBMEDFSAHS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEDPHS_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMEDPHS";
							CPCommon.WaitControlDisplayed(HBMEDPHS_DetailsForm);
formBttn = HBMEDPHS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMEDPHS_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMEDPHS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMEDPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDPHS] Perfoming VerifyExists on DetailsForm_TransactionDate...", Logger.MessageType.INF);
			Control HBMEDPHS_DetailsForm_TransactionDate = new Control("DetailsForm_TransactionDate", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEDPHS_HBMEDFSAHS_CHLD_']/ancestor::form[1]/descendant::*[@id='TRANS_DT']");
			CPCommon.AssertEqual(true,HBMEDPHS_DetailsForm_TransactionDate.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HBMEDPHS";
							CPCommon.WaitControlDisplayed(HBMEDPHS_MainForm);
formBttn = HBMEDPHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

