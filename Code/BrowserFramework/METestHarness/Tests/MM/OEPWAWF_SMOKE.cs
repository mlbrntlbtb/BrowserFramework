 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEPWAWF_SMOKE : TestScript
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
new Control("Create iRAPT Files", "xpath","//div[@class='navItem'][.='Create iRAPT Files']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEPWAWF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEPWAWF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OEPWAWF_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OEPWAWF_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_MainForm);
IWebElement formBttn = OEPWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEPWAWF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEPWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEPWAWF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPWAWF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPWAWF_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_1008070_OEPWAWF_PARAM");
			CPCommon.WaitControlDisplayed(OEPWAWF_CatalogNonContiguousRangesLink);
OEPWAWF_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPWAWF_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEPWAWF_CatalogNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExist on CatalogNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OEPWAWF_CatalogNonContiguousRangesTable = new Control("CatalogNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPWAWF_CatalogNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_CatalogNonContiguousRangesForm);
formBttn = OEPWAWF_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Customer");


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming Click on ClientNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPWAWF_ClientNonContiguousRangesLink = new Control("ClientNonContiguousRangesLink", "ID", "lnk_1008074_OEPWAWF_PARAM");
			CPCommon.WaitControlDisplayed(OEPWAWF_ClientNonContiguousRangesLink);
OEPWAWF_ClientNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPWAWF_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEPWAWF_CustomerNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExist on CustomerNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OEPWAWF_CustomerNonContiguousRangesFormTable = new Control("CustomerNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPWAWF_CustomerNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_CustomerNonContiguousRangesForm);
formBttn = OEPWAWF_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming Click on InvoiceNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPWAWF_InvoiceNonContiguousRangesLink = new Control("InvoiceNonContiguousRangesLink", "ID", "lnk_1008075_OEPWAWF_PARAM");
			CPCommon.WaitControlDisplayed(OEPWAWF_InvoiceNonContiguousRangesLink);
OEPWAWF_InvoiceNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on InvoiceNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPWAWF_InvoiceNonContiguousRangesForm = new Control("InvoiceNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEPWAWF_InvoiceNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExist on InvoiceNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OEPWAWF_InvoiceNonContiguousRangesFormTable = new Control("InvoiceNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVCID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPWAWF_InvoiceNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_InvoiceNonContiguousRangesForm);
formBttn = OEPWAWF_InvoiceNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPWAWF_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1008076_OEPWAWF_PARAM");
			CPCommon.WaitControlDisplayed(OEPWAWF_ProjectNonContiguousRangesLink);
OEPWAWF_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPWAWF_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEPWAWF_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OEPWAWF_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPWAWF_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_ProjectNonContiguousRangesForm);
formBttn = OEPWAWF_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPWAWF_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_1008077_OEPWAWF_PARAM");
			CPCommon.WaitControlDisplayed(OEPWAWF_SalesOrderNonContiguousRangesLink);
OEPWAWF_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExists on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPWAWF_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEPWAWF_SalesOrderNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPWAWF] Perfoming VerifyExist on SalesOrderNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OEPWAWF_SalesOrderNonContiguousRangesFormTable = new Control("SalesOrderNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPWAWF_SalesOrderNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_SalesOrderNonContiguousRangesForm);
formBttn = OEPWAWF_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OEPWAWF";
							CPCommon.WaitControlDisplayed(OEPWAWF_MainForm);
formBttn = OEPWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

