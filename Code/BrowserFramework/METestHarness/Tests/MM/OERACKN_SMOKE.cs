 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OERACKN_SMOKE : TestScript
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
new Control("Sales Orders", "xpath","//div[@class='navItem'][.='Sales Orders']").Click();
new Control("Print Sales Order Acknowledgements", "xpath","//div[@class='navItem'][.='Print Sales Order Acknowledgements']").Click();


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OERACKN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OERACKN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OERACKN";
							CPCommon.WaitControlDisplayed(OERACKN_MainForm);
IWebElement formBttn = OERACKN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OERACKN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OERACKN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OERACKN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERACKN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERACKN";
							CPCommon.WaitControlDisplayed(OERACKN_MainForm);
formBttn = OERACKN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OERACKN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OERACKN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OERACKN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OERACKN_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("CATALOG NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERACKN_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_2843_OERACKN_PARAM");
			CPCommon.WaitControlDisplayed(OERACKN_CatalogNonContiguousRangesLink);
OERACKN_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExist on CatalogNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERACKN_CatalogNonContiguousRangesTable = new Control("CatalogNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERACKN_CatalogNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Close on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERACKN_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERACKN_CatalogNonContiguousRangesForm);
formBttn = OERACKN_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Click on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERACKN_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_2844_OERACKN_PARAM");
			CPCommon.WaitControlDisplayed(OERACKN_CustomerNonContiguousRangesLink);
OERACKN_CustomerNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExist on CustomerNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERACKN_CustomerNonContiguousRangesTable = new Control("CustomerNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERACKN_CustomerNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Close on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERACKN_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERACKN_CustomerNonContiguousRangesForm);
formBttn = OERACKN_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PROJECT NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERACKN_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2845_OERACKN_PARAM");
			CPCommon.WaitControlDisplayed(OERACKN_ProjectNonContiguousRangesLink);
OERACKN_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExist on ProjectNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERACKN_ProjectNonContiguousRangesTable = new Control("ProjectNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERACKN_ProjectNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERACKN_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERACKN_ProjectNonContiguousRangesForm);
formBttn = OERACKN_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("SALES NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERACKN_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_2846_OERACKN_PARAM");
			CPCommon.WaitControlDisplayed(OERACKN_SalesOrderNonContiguousRangesLink);
OERACKN_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming VerifyExist on SalesOrderNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERACKN_SalesOrderNonContiguousRangesTable = new Control("SalesOrderNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERACKN_SalesOrderNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERACKN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERACKN] Perfoming Close on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERACKN_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OERACKN_SalesOrderNonContiguousRangesForm);
formBttn = OERACKN_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "OERACKN";
							CPCommon.WaitControlDisplayed(OERACKN_MainForm);
formBttn = OERACKN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

