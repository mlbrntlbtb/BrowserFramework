 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POQCOMM_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchasing Reports/Inquiries", "xpath","//div[@class='navItem'][.='Purchasing Reports/Inquiries']").Click();
new Control("View Purchase Order Commitments Summary", "xpath","//div[@class='navItem'][.='View Purchase Order Commitments Summary']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "POQCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCOMM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POQCOMM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POQCOMM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POQCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCOMM] Perfoming VerifyExists on AccountingPeriod_FiscalYear...", Logger.MessageType.INF);
			Control POQCOMM_AccountingPeriod_FiscalYear = new Control("AccountingPeriod_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,POQCOMM_AccountingPeriod_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "POQCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCOMM] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control POQCOMM_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCOMM_POCOMMITSUM_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCOMM_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "POQCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCOMM] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control POQCOMM_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCOMM_POCOMMITSUM_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCOMM_ChildForm);
IWebElement formBttn = POQCOMM_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCOMM_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCOMM_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCOMM";
							CPCommon.AssertEqual(true,POQCOMM_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "POQCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCOMM] Perfoming VerifyExists on ChildForm_Project...", Logger.MessageType.INF);
			Control POQCOMM_ChildForm_Project = new Control("ChildForm_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCOMM_POCOMMITSUM_DTL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,POQCOMM_ChildForm_Project.Exists());

											Driver.SessionLogger.WriteLine("MainForm Close");


												
				CPCommon.CurrentComponent = "POQCOMM";
							CPCommon.WaitControlDisplayed(POQCOMM_MainForm);
formBttn = POQCOMM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

