 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMTAXAL_SMOKE : TestScript
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
new Control("Billing Controls", "xpath","//div[@class='navItem'][.='Billing Controls']").Click();
new Control("Manage Taxable Sales Accounts", "xpath","//div[@class='navItem'][.='Manage Taxable Sales Accounts']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Taxable Sales Accounts", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMTAXAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXAL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMTAXAL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMTAXAL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMTAXAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXAL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMTAXAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMTAXAL_MainForm);
IWebElement formBttn = BLMTAXAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMTAXAL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMTAXAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMTAXAL";
							CPCommon.AssertEqual(true,BLMTAXAL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMTAXAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXAL] Perfoming VerifyExists on SalesTaxCode...", Logger.MessageType.INF);
			Control BLMTAXAL_SalesTaxCode = new Control("SalesTaxCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SALES_TAX_CD']");
			CPCommon.AssertEqual(true,BLMTAXAL_SalesTaxCode.Exists());

											Driver.SessionLogger.WriteLine("USE-DEFINED INFO");


												
				CPCommon.CurrentComponent = "BLMTAXAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXAL] Perfoming VerifyExist on TaxableAccountsFormTable...", Logger.MessageType.INF);
			Control BLMTAXAL_TaxableAccountsFormTable = new Control("TaxableAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMTAXAL_BLSALESTAXACCT_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMTAXAL_TaxableAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMTAXAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXAL] Perfoming ClickButton on TaxableAccountsForm...", Logger.MessageType.INF);
			Control BLMTAXAL_TaxableAccountsForm = new Control("TaxableAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMTAXAL_BLSALESTAXACCT_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMTAXAL_TaxableAccountsForm);
formBttn = BLMTAXAL_TaxableAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMTAXAL_TaxableAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMTAXAL_TaxableAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMTAXAL";
							CPCommon.AssertEqual(true,BLMTAXAL_TaxableAccountsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMTAXAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXAL] Perfoming VerifyExists on TaxableAccounts_Account...", Logger.MessageType.INF);
			Control BLMTAXAL_TaxableAccounts_Account = new Control("TaxableAccounts_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMTAXAL_BLSALESTAXACCT_TBL_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,BLMTAXAL_TaxableAccounts_Account.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMTAXAL";
							CPCommon.WaitControlDisplayed(BLMTAXAL_MainForm);
formBttn = BLMTAXAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

