 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OERINVC_SMOKE : TestScript
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
new Control("Print Invoices", "xpath","//div[@class='navItem'][.='Print Invoices']").Click();


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OERINVC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OERINVC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OERINVC_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OERINVC_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
							CPCommon.WaitControlDisplayed(OERINVC_MainForm);
IWebElement formBttn = OERINVC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OERINVC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OERINVC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OERINVC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERINVC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CATALOG NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERINVC_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_2920_OERINVC_PARAM");
			CPCommon.WaitControlDisplayed(OERINVC_CatalogNonContiguousRangesLink);
OERINVC_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExist on CatalogNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERINVC_CatalogNonContiguousRangesTable = new Control("CatalogNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERINVC_CatalogNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Close on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERINVC_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERINVC_CatalogNonContiguousRangesForm);
formBttn = OERINVC_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Click on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERINVC_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_2918_OERINVC_PARAM");
			CPCommon.WaitControlDisplayed(OERINVC_CustomerNonContiguousRangesLink);
OERINVC_CustomerNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExist on CustomerNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERINVC_CustomerNonContiguousRangesTable = new Control("CustomerNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERINVC_CustomerNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Close on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERINVC_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERINVC_CustomerNonContiguousRangesForm);
formBttn = OERINVC_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("INVOICE NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Click on InvoiceNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERINVC_InvoiceNonContiguousRangesLink = new Control("InvoiceNonContiguousRangesLink", "ID", "lnk_2927_OERINVC_PARAM");
			CPCommon.WaitControlDisplayed(OERINVC_InvoiceNonContiguousRangesLink);
OERINVC_InvoiceNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExist on InvoiceNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERINVC_InvoiceNonContiguousRangesTable = new Control("InvoiceNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERINVC_InvoiceNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Close on InvoiceNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERINVC_InvoiceNonContiguousRangesForm = new Control("InvoiceNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERINVC_InvoiceNonContiguousRangesForm);
formBttn = OERINVC_InvoiceNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PROJECT NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERINVC_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2921_OERINVC_PARAM");
			CPCommon.WaitControlDisplayed(OERINVC_ProjectNonContiguousRangesLink);
OERINVC_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExist on ProjectNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERINVC_ProjectNonContiguousRangesTable = new Control("ProjectNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERINVC_ProjectNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERINVC_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERINVC_ProjectNonContiguousRangesForm);
formBttn = OERINVC_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("SALES NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERINVC_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_2922_OERINVC_PARAM");
			CPCommon.WaitControlDisplayed(OERINVC_SalesOrderNonContiguousRangesLink);
OERINVC_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming VerifyExist on SalesOrderNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERINVC_SalesOrderNonContiguousRangesTable = new Control("SalesOrderNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERINVC_SalesOrderNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERINVC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERINVC] Perfoming Close on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERINVC_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERINVC_SalesOrderNonContiguousRangesForm);
formBttn = OERINVC_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "OERINVC";
							CPCommon.WaitControlDisplayed(OERINVC_MainForm);
formBttn = OERINVC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

