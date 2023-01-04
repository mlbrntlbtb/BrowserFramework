 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OER250PS_SMOKE : TestScript
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
new Control("Print DD250 Packing Slips", "xpath","//div[@class='navItem'][.='Print DD250 Packing Slips']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OER250PS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OER250PS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OER250PS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OER250PS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
							CPCommon.WaitControlDisplayed(OER250PS_MainForm);
IWebElement formBttn = OER250PS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OER250PS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OER250PS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OER250PS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250PS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CATALOG FORM");


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250PS_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_1004974_OER250PS_PARAM");
			CPCommon.WaitControlDisplayed(OER250PS_CatalogNonContiguousRangesLink);
OER250PS_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExist on CatalogNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250PS_CatalogNonContiguousRangesFormTable = new Control("CatalogNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250PS_CatalogNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming Close on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250PS_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OER250PS_CatalogNonContiguousRangesForm);
formBttn = OER250PS_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CUSTOMER FORM");


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming Click on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250PS_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_1004976_OER250PS_PARAM");
			CPCommon.WaitControlDisplayed(OER250PS_CustomerNonContiguousRangesLink);
OER250PS_CustomerNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExists on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250PS_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OER250PS_CustomerNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExist on CustomerNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250PS_CustomerNonContiguousRangesFormTable = new Control("CustomerNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250PS_CustomerNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
							CPCommon.WaitControlDisplayed(OER250PS_CustomerNonContiguousRangesForm);
formBttn = OER250PS_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PACKING SLIP");


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming Click on PackingSlipNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250PS_PackingSlipNonContiguousRangesLink = new Control("PackingSlipNonContiguousRangesLink", "ID", "lnk_1004977_OER250PS_PARAM");
			CPCommon.WaitControlDisplayed(OER250PS_PackingSlipNonContiguousRangesLink);
OER250PS_PackingSlipNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExists on PackingSlipNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250PS_PackingSlipNonContiguousRangesForm = new Control("PackingSlipNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OER250PS_PackingSlipNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExist on PackingSlipNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250PS_PackingSlipNonContiguousRangesFormTable = new Control("PackingSlipNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250PS_PackingSlipNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
							CPCommon.WaitControlDisplayed(OER250PS_PackingSlipNonContiguousRangesForm);
formBttn = OER250PS_PackingSlipNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PROJECT FORM");


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250PS_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1004978_OER250PS_PARAM");
			CPCommon.WaitControlDisplayed(OER250PS_ProjectNonContiguousRangesLink);
OER250PS_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250PS_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OER250PS_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250PS_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250PS_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
							CPCommon.WaitControlDisplayed(OER250PS_ProjectNonContiguousRangesForm);
formBttn = OER250PS_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SALES ORDER FORM");


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OER250PS_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_1004979_OER250PS_PARAM");
			CPCommon.WaitControlDisplayed(OER250PS_SalesOrderNonContiguousRangesLink);
OER250PS_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExists on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OER250PS_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OER250PS_SalesOrderNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OER250PS] Perfoming VerifyExist on SalesOrderNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OER250PS_SalesOrderNonContiguousRangesFormTable = new Control("SalesOrderNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OER250PS_SalesOrderNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OER250PS";
							CPCommon.WaitControlDisplayed(OER250PS_SalesOrderNonContiguousRangesForm);
formBttn = OER250PS_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OER250PS";
							CPCommon.WaitControlDisplayed(OER250PS_MainForm);
formBttn = OER250PS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

