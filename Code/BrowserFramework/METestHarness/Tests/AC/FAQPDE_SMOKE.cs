 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAQPDE_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Projections", "xpath","//div[@class='navItem'][.='Projections']").Click();
new Control("View Projected Depreciation Expense", "xpath","//div[@class='navItem'][.='View Projected Depreciation Expense']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAQPDE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQPDE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAQPDE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAQPDE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAQPDE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQPDE] Perfoming VerifyExists on BookNumber...", Logger.MessageType.INF);
			Control FAQPDE_BookNumber = new Control("BookNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BOOK_NO']");
			CPCommon.AssertEqual(true,FAQPDE_BookNumber.Exists());

												
				CPCommon.CurrentComponent = "FAQPDE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQPDE] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control FAQPDE_ChildForm = new Control("ChildForm", "xpath", "//div[starts-with(@id,'pr__FAQPDE_VFAFAQPDE3_CTW_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAQPDE_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "FAQPDE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQPDE] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control FAQPDE_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[starts-with(@id,'pr__FAQPDE_VFAFAQPDE3_CTW_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAQPDE_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAQPDE";
							CPCommon.AssertEqual(true,FAQPDE_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "FAQPDE";
							CPCommon.WaitControlDisplayed(FAQPDE_ChildForm);
IWebElement formBttn = FAQPDE_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAQPDE_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAQPDE_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAQPDE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQPDE] Perfoming VerifyExists on ChildForm_DepreciationExpenseInformation_BookNo...", Logger.MessageType.INF);
			Control FAQPDE_ChildForm_DepreciationExpenseInformation_BookNo = new Control("ChildForm_DepreciationExpenseInformation_BookNo", "xpath", "//div[starts-with(@id,'pr__FAQPDE_VFAFAQPDE3_CTW_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAQPDE_ChildForm_DepreciationExpenseInformation_BookNo.Exists());

												
				CPCommon.CurrentComponent = "FAQPDE";
							CPCommon.WaitControlDisplayed(FAQPDE_MainForm);
formBttn = FAQPDE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

