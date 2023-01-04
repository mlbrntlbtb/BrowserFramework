 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMTAXCD_SMOKE : TestScript
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
new Control("Billing Master", "xpath","//div[@class='navItem'][.='Billing Master']").Click();
new Control("Manage Project Sales Tax Setups", "xpath","//div[@class='navItem'][.='Manage Project Sales Tax Setups']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Project Sales Tax Setups", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMTAXCD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMTAXCD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMTAXCD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMTAXCD_MainForm);
IWebElement formBttn = BLMTAXCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMTAXCD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMTAXCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMTAXCD";
							CPCommon.AssertEqual(true,BLMTAXCD_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming VerifyExists on InvoiceProject...", Logger.MessageType.INF);
			Control BLMTAXCD_InvoiceProject = new Control("InvoiceProject", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_PROJ_ID']");
			CPCommon.AssertEqual(true,BLMTAXCD_InvoiceProject.Exists());

											Driver.SessionLogger.WriteLine("SALES TAX DETAILS");


												
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming VerifyExists on SalesTaxDetailsLink...", Logger.MessageType.INF);
			Control BLMTAXCD_SalesTaxDetailsLink = new Control("SalesTaxDetailsLink", "ID", "lnk_1000665_BLMTAXCD_PROJBILLINFO_HDR");
			CPCommon.AssertEqual(true,BLMTAXCD_SalesTaxDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming VerifyExist on SalesTaxDetailsFormTable...", Logger.MessageType.INF);
			Control BLMTAXCD_SalesTaxDetailsFormTable = new Control("SalesTaxDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMTAXCD_BLPROJTAXCD_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMTAXCD_SalesTaxDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming ClickButton on SalesTaxDetailsForm...", Logger.MessageType.INF);
			Control BLMTAXCD_SalesTaxDetailsForm = new Control("SalesTaxDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMTAXCD_BLPROJTAXCD_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMTAXCD_SalesTaxDetailsForm);
formBttn = BLMTAXCD_SalesTaxDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMTAXCD_SalesTaxDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMTAXCD_SalesTaxDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMTAXCD";
							CPCommon.AssertEqual(true,BLMTAXCD_SalesTaxDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMTAXCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMTAXCD] Perfoming VerifyExists on SalesTaxDetails_SalesTaxCode...", Logger.MessageType.INF);
			Control BLMTAXCD_SalesTaxDetails_SalesTaxCode = new Control("SalesTaxDetails_SalesTaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMTAXCD_BLPROJTAXCD_TBL_']/ancestor::form[1]/descendant::*[@id='SALES_TAX_CD']");
			CPCommon.AssertEqual(true,BLMTAXCD_SalesTaxDetails_SalesTaxCode.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMTAXCD";
							CPCommon.WaitControlDisplayed(BLMTAXCD_MainForm);
formBttn = BLMTAXCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

