 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OER250I_SMOKE : TestScript
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
new Control("Sales Order Invoices", "xpath","//div[@class='navItem'][.='Sales Order Invoices']").Click();
new Control("Print DD250 Invoices", "xpath","//div[@class='navItem'][.='Print DD250 Invoices']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OER250I_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OER250I_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OER250I_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OER250I_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_MainForm);
IWebElement formBttn = OER250I_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OER250I_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OER250I_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OER250I_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250I_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Catalog Non-Contiguous Ranges");


												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250I_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_1004944_OER250I_PARAM");
			CPCommon.AssertEqual(true,OER250I_CatalogNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_CatalogNonContiguousRangesLink);
OER250I_CatalogNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExist on CatalogNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250I_CatalogNonContiguousRangesFormTable = new Control("CatalogNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250I_CatalogNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on CatalogNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OER250I_CatalogNonContiguousRanges_Ok = new Control("CatalogNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OER250I_CatalogNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming Close on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250I_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OER250I_CatalogNonContiguousRangesForm);
formBttn = OER250I_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Customer Non-Contiguous Ranges");


												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250I_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_1004945_OER250I_PARAM");
			CPCommon.AssertEqual(true,OER250I_CustomerNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_CustomerNonContiguousRangesLink);
OER250I_CustomerNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExist on CustomerNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250I_CustomerNonContiguousRangesFormTable = new Control("CustomerNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250I_CustomerNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on CustomerNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OER250I_CustomerNonContiguousRanges_Ok = new Control("CustomerNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OER250I_CustomerNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming Close on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250I_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OER250I_CustomerNonContiguousRangesForm);
formBttn = OER250I_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Invoice Non-Contiguous Ranges");


												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on InvoiceNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250I_InvoiceNonContiguousRangesLink = new Control("InvoiceNonContiguousRangesLink", "ID", "lnk_1004946_OER250I_PARAM");
			CPCommon.AssertEqual(true,OER250I_InvoiceNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_InvoiceNonContiguousRangesLink);
OER250I_InvoiceNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExist on InvoiceNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250I_InvoiceNonContiguousRangesFormTable = new Control("InvoiceNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250I_InvoiceNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on InvoiceNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OER250I_InvoiceNonContiguousRanges_Ok = new Control("InvoiceNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OER250I_InvoiceNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming Close on InvoiceNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250I_InvoiceNonContiguousRangesForm = new Control("InvoiceNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OER250I_InvoiceNonContiguousRangesForm);
formBttn = OER250I_InvoiceNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Project Non-Contiguous Ranges");


												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250I_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1004947_OER250I_PARAM");
			CPCommon.AssertEqual(true,OER250I_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_ProjectNonContiguousRangesLink);
OER250I_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250I_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250I_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on ProjectNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OER250I_ProjectNonContiguousRanges_Ok = new Control("ProjectNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OER250I_ProjectNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250I_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OER250I_ProjectNonContiguousRangesForm);
formBttn = OER250I_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Sales Order Non-Contiguous Ranges");


												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250I_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_1004948_OER250I_PARAM");
			CPCommon.AssertEqual(true,OER250I_SalesOrderNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_SalesOrderNonContiguousRangesLink);
OER250I_SalesOrderNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExist on SalesOrderNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250I_SalesOrderNonContiguousRangesFormTable = new Control("SalesOrderNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250I_SalesOrderNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming VerifyExists on SalesOrderNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OER250I_SalesOrderNonContiguousRanges_Ok = new Control("SalesOrderNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OER250I_SalesOrderNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OER250I";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250I] Perfoming Close on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250I_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OER250I_SalesOrderNonContiguousRangesForm);
formBttn = OER250I_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "OER250I";
							CPCommon.WaitControlDisplayed(OER250I_MainForm);
formBttn = OER250I_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

