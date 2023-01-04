 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEPEDIIN_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Utilities", "xpath","//div[@class='navItem'][.='Sales Order Entry Utilities']").Click();
new Control("Create EDI Invoice File", "xpath","//div[@class='navItem'][.='Create EDI Invoice File']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEPEDIIN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEPEDIIN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OEPEDIIN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OEPEDIIN_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("CATALOG NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPEDIIN_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_2381_OEPEDIIN_PARAM");
			CPCommon.AssertEqual(true,OEPEDIIN_CatalogNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_CatalogNonContiguousRangesLink);
OEPEDIIN_CatalogNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExist on CatalogNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OEPEDIIN_CatalogNonContiguousRangersFormTable = new Control("CatalogNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_CATALOG_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPEDIIN_CatalogNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming Close on CatalogNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OEPEDIIN_CatalogNonContiguousRangersForm = new Control("CatalogNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_CATALOG_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPEDIIN_CatalogNonContiguousRangersForm);
IWebElement formBttn = OEPEDIIN_CatalogNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPEDIIN_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_2383_OEPEDIIN_PARAM");
			CPCommon.AssertEqual(true,OEPEDIIN_CustomerNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_CustomerNonContiguousRangesLink);
OEPEDIIN_CustomerNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExist on CustomerNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OEPEDIIN_CustomerNonContiguousRangersFormTable = new Control("CustomerNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_CUSTOMER_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPEDIIN_CustomerNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming Close on CustomerNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OEPEDIIN_CustomerNonContiguousRangersForm = new Control("CustomerNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_CUSTOMER_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPEDIIN_CustomerNonContiguousRangersForm);
formBttn = OEPEDIIN_CustomerNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("INVOICE NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on InvoiceNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPEDIIN_InvoiceNonContiguousRangesLink = new Control("InvoiceNonContiguousRangesLink", "ID", "lnk_2384_OEPEDIIN_PARAM");
			CPCommon.AssertEqual(true,OEPEDIIN_InvoiceNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_InvoiceNonContiguousRangesLink);
OEPEDIIN_InvoiceNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExist on InvoiceNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OEPEDIIN_InvoiceNonContiguousRangersFormTable = new Control("InvoiceNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_INVOICE_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPEDIIN_InvoiceNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming Close on InvoiceNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OEPEDIIN_InvoiceNonContiguousRangersForm = new Control("InvoiceNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_INVOICE_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPEDIIN_InvoiceNonContiguousRangersForm);
formBttn = OEPEDIIN_InvoiceNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PROJECT NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPEDIIN_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2385_OEPEDIIN_PARAM");
			CPCommon.AssertEqual(true,OEPEDIIN_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_ProjectNonContiguousRangesLink);
OEPEDIIN_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExist on ProjectNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OEPEDIIN_ProjectNonContiguousRangersFormTable = new Control("ProjectNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_PROJECT_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPEDIIN_ProjectNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming Close on ProjectNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OEPEDIIN_ProjectNonContiguousRangersForm = new Control("ProjectNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_PROJECT_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPEDIIN_ProjectNonContiguousRangersForm);
formBttn = OEPEDIIN_ProjectNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("SALES ORDER NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExists on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPEDIIN_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_2386_OEPEDIIN_PARAM");
			CPCommon.AssertEqual(true,OEPEDIIN_SalesOrderNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_SalesOrderNonContiguousRangesLink);
OEPEDIIN_SalesOrderNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExist on SalesOrderNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OEPEDIIN_SalesOrderNonContiguousRangersFormTable = new Control("SalesOrderNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_SALES_ORDER_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPEDIIN_SalesOrderNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming Close on SalesOrderNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OEPEDIIN_SalesOrderNonContiguousRangersForm = new Control("SalesOrderNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPEDIIN_SALES_ORDER_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPEDIIN_SalesOrderNonContiguousRangersForm);
formBttn = OEPEDIIN_SalesOrderNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_MainForm);
formBttn = OEPEDIIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEPEDIIN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEPEDIIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEPEDIIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPEDIIN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEPEDIIN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPEDIIN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "OEPEDIIN";
							CPCommon.WaitControlDisplayed(OEPEDIIN_MainForm);
formBttn = OEPEDIIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

