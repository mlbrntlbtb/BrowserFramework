 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OERBLOG_SMOKE : TestScript
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
new Control("Sales Order Entry Reports/Inquiries", "xpath","//div[@class='navItem'][.='Sales Order Entry Reports/Inquiries']").Click();
new Control("Print Sales Order Backlog Report", "xpath","//div[@class='navItem'][.='Print Sales Order Backlog Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OERBLOG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OERBLOG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OERBLOG_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OERBLOG_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("CATALOG NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERBLOG_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_1007361_OERBLOG_PARAM");
			CPCommon.AssertEqual(true,OERBLOG_CatalogNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_CatalogNonContiguousRangesLink);
OERBLOG_CatalogNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExist on CatalogNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OERBLOG_CatalogNonContiguousRangersFormTable = new Control("CatalogNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERBLOG_CatalogNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming Close on CatalogNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OERBLOG_CatalogNonContiguousRangersForm = new Control("CatalogNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERBLOG_CatalogNonContiguousRangersForm);
IWebElement formBttn = OERBLOG_CatalogNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERBLOG_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_1007362_OERBLOG_PARAM");
			CPCommon.AssertEqual(true,OERBLOG_CustomerNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_CustomerNonContiguousRangesLink);
OERBLOG_CustomerNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExist on CustomerNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OERBLOG_CustomerNonContiguousRangersFormTable = new Control("CustomerNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERBLOG_CustomerNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming Close on CustomerNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OERBLOG_CustomerNonContiguousRangersForm = new Control("CustomerNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERBLOG_CustomerNonContiguousRangersForm);
formBttn = OERBLOG_CustomerNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PROJECT NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERBLOG_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1007364_OERBLOG_PARAM");
			CPCommon.AssertEqual(true,OERBLOG_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_ProjectNonContiguousRangesLink);
OERBLOG_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExist on ProjectNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OERBLOG_ProjectNonContiguousRangersFormTable = new Control("ProjectNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERBLOG_ProjectNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming Close on ProjectNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OERBLOG_ProjectNonContiguousRangersForm = new Control("ProjectNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERBLOG_ProjectNonContiguousRangersForm);
formBttn = OERBLOG_ProjectNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("SALES ORDER NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERBLOG_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_1007365_OERBLOG_PARAM");
			CPCommon.AssertEqual(true,OERBLOG_SalesOrderNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_SalesOrderNonContiguousRangesLink);
OERBLOG_SalesOrderNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExist on SalesOrderNonContiguousRangersFormTable...", Logger.MessageType.INF);
			Control OERBLOG_SalesOrderNonContiguousRangersFormTable = new Control("SalesOrderNonContiguousRangersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERBLOG_SalesOrderNonContiguousRangersFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming Close on SalesOrderNonContiguousRangersForm...", Logger.MessageType.INF);
			Control OERBLOG_SalesOrderNonContiguousRangersForm = new Control("SalesOrderNonContiguousRangersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERBLOG_SalesOrderNonContiguousRangersForm);
formBttn = OERBLOG_SalesOrderNonContiguousRangersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("WAREHOUSE NON CONTIGUOUS LINK");


												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExists on WarehouseNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERBLOG_WarehouseNonContiguousRangesLink = new Control("WarehouseNonContiguousRangesLink", "ID", "lnk_1007366_OERBLOG_PARAM");
			CPCommon.AssertEqual(true,OERBLOG_WarehouseNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_WarehouseNonContiguousRangesLink);
OERBLOG_WarehouseNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExist on WarehouseNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OERBLOG_WarehouseNonContiguousRangesFormTable = new Control("WarehouseNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRWHSEID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERBLOG_WarehouseNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming Close on WarehouseNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERBLOG_WarehouseNonContiguousRangesForm = new Control("WarehouseNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRWHSEID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERBLOG_WarehouseNonContiguousRangesForm);
formBttn = OERBLOG_WarehouseNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_MainForm);
formBttn = OERBLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OERBLOG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OERBLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OERBLOG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERBLOG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OERBLOG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERBLOG_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "OERBLOG";
							CPCommon.WaitControlDisplayed(OERBLOG_MainForm);
formBttn = OERBLOG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

