 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OERSHLAB_SMOKE : TestScript
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
new Control("Sales Order Shipping", "xpath","//div[@class='navItem'][.='Sales Order Shipping']").Click();
new Control("Print Shipping Labels", "xpath","//div[@class='navItem'][.='Print Shipping Labels']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


											Driver.SessionLogger.WriteLine("Catalog");


												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERSHLAB_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_1004725_OERSHLAB_PARAM");
			CPCommon.AssertEqual(true,OERSHLAB_CatalogNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
							CPCommon.WaitControlDisplayed(OERSHLAB_CatalogNonContiguousRangesLink);
OERSHLAB_CatalogNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExist on CatalogNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OERSHLAB_CatalogNonContiguousRangesFormTable = new Control("CatalogNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERSHLAB_CatalogNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on CatalogNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OERSHLAB_CatalogNonContiguousRanges_Ok = new Control("CatalogNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OERSHLAB_CatalogNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming Close on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERSHLAB_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERSHLAB_CatalogNonContiguousRangesForm);
IWebElement formBttn = OERSHLAB_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Customer");


												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERSHLAB_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_1004726_OERSHLAB_PARAM");
			CPCommon.AssertEqual(true,OERSHLAB_CustomerNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
							CPCommon.WaitControlDisplayed(OERSHLAB_CustomerNonContiguousRangesLink);
OERSHLAB_CustomerNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExist on CustomerNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OERSHLAB_CustomerNonContiguousRangesFormTable = new Control("CustomerNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERSHLAB_CustomerNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on CustomerNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OERSHLAB_CustomerNonContiguousRanges_Ok = new Control("CustomerNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OERSHLAB_CustomerNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming Close on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERSHLAB_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERSHLAB_CustomerNonContiguousRangesForm);
formBttn = OERSHLAB_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Packing Slip");


												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on PackingSlipNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERSHLAB_PackingSlipNonContiguousRangesLink = new Control("PackingSlipNonContiguousRangesLink", "ID", "lnk_1004727_OERSHLAB_PARAM");
			CPCommon.AssertEqual(true,OERSHLAB_PackingSlipNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
							CPCommon.WaitControlDisplayed(OERSHLAB_PackingSlipNonContiguousRangesLink);
OERSHLAB_PackingSlipNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExist on PackingSlipNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OERSHLAB_PackingSlipNonContiguousRangesFormTable = new Control("PackingSlipNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERSHLAB_PackingSlipNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on PackingSlipNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OERSHLAB_PackingSlipNonContiguousRanges_Ok = new Control("PackingSlipNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OERSHLAB_PackingSlipNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming Close on PackingSlipNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERSHLAB_PackingSlipNonContiguousRangesForm = new Control("PackingSlipNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERSHLAB_PackingSlipNonContiguousRangesForm);
formBttn = OERSHLAB_PackingSlipNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Project");


												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERSHLAB_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1004728_OERSHLAB_PARAM");
			CPCommon.AssertEqual(true,OERSHLAB_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
							CPCommon.WaitControlDisplayed(OERSHLAB_ProjectNonContiguousRangesLink);
OERSHLAB_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OERSHLAB_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERSHLAB_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on ProjectNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OERSHLAB_ProjectNonContiguousRanges_Ok = new Control("ProjectNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OERSHLAB_ProjectNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERSHLAB_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERSHLAB_ProjectNonContiguousRangesForm);
formBttn = OERSHLAB_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Sales Order ");


												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERSHLAB_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_1004729_OERSHLAB_PARAM");
			CPCommon.AssertEqual(true,OERSHLAB_SalesOrderNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
							CPCommon.WaitControlDisplayed(OERSHLAB_SalesOrderNonContiguousRangesLink);
OERSHLAB_SalesOrderNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExist on SalesOrderNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OERSHLAB_SalesOrderNonContiguousRangesFormTable = new Control("SalesOrderNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERSHLAB_SalesOrderNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming VerifyExists on SalesOrderNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control OERSHLAB_SalesOrderNonContiguousRanges_Ok = new Control("SalesOrderNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,OERSHLAB_SalesOrderNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming Close on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERSHLAB_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERSHLAB_SalesOrderNonContiguousRangesForm);
formBttn = OERSHLAB_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "OERSHLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERSHLAB] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control OERSHLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(OERSHLAB_MainForm);
formBttn = OERSHLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

