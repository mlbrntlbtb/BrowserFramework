 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RCRLOG_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Receiving", "xpath","//div[@class='deptItem'][.='Receiving']").Click();
new Control("Receiving Reports/Inquiries", "xpath","//div[@class='navItem'][.='Receiving Reports/Inquiries']").Click();
new Control("Print Receiving Log", "xpath","//div[@class='navItem'][.='Print Receiving Log']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "RCRLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCRLOG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RCRLOG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RCRLOG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RCRLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCRLOG] Perfoming VerifyExists on SelectionRanges_Option_ReceiptDate...", Logger.MessageType.INF);
			Control RCRLOG_SelectionRanges_Option_ReceiptDate = new Control("SelectionRanges_Option_ReceiptDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RECPT_DT_RANGE_CD']");
			CPCommon.AssertEqual(true,RCRLOG_SelectionRanges_Option_ReceiptDate.Exists());

												
				CPCommon.CurrentComponent = "RCRLOG";
							CPCommon.WaitControlDisplayed(RCRLOG_MainForm);
IWebElement formBttn = RCRLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RCRLOG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RCRLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RCRLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCRLOG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RCRLOG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCRLOG_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RCRLOG";
							CPCommon.WaitControlDisplayed(RCRLOG_MainForm);
formBttn = RCRLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCRLOG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCRLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "RCRLOG";
							CPCommon.WaitControlDisplayed(RCRLOG_MainForm);
formBttn = RCRLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

