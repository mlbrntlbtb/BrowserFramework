 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEPGRQ_SMOKE : TestScript
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
new Control("Sales Order Material Processing", "xpath","//div[@class='navItem'][.='Sales Order Material Processing']").Click();
new Control("Create Purchase Requisitions from Sales Orders", "xpath","//div[@class='navItem'][.='Create Purchase Requisitions from Sales Orders']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEPGRQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEPGRQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OEPGRQ_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OEPGRQ_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
							CPCommon.WaitControlDisplayed(OEPGRQ_MainForm);
IWebElement formBttn = OEPGRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEPGRQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEPGRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPGRQ_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_2437_OEPGRQ_PARAM");
			CPCommon.WaitControlDisplayed(OEPGRQ_CatalogNonContiguousRangesLink);
OEPGRQ_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExist on CatalogNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OEPGRQ_CatalogNonContiguousRangesTable = new Control("CatalogNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPGRQ_CatalogNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Close on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPGRQ_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPGRQ_CatalogNonContiguousRangesForm);
formBttn = OEPGRQ_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Customer");


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Click on CustXNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPGRQ_CustXNonContiguousRangesLink = new Control("CustXNonContiguousRangesLink", "ID", "lnk_2436_OEPGRQ_PARAM");
			CPCommon.WaitControlDisplayed(OEPGRQ_CustXNonContiguousRangesLink);
OEPGRQ_CustXNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExist on CustXNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OEPGRQ_CustXNonContiguousRangesTable = new Control("CustXNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPGRQ_CustXNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Close on CustXNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPGRQ_CustXNonContiguousRangesForm = new Control("CustXNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPGRQ_CustXNonContiguousRangesForm);
formBttn = OEPGRQ_CustXNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPGRQ_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_2440_OEPGRQ_PARAM");
			CPCommon.WaitControlDisplayed(OEPGRQ_SalesOrderNonContiguousRangesLink);
OEPGRQ_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExist on SalesOrderNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OEPGRQ_SalesOrderNonContiguousRangesTable = new Control("SalesOrderNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPGRQ_SalesOrderNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Close on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPGRQ_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPGRQ_SalesOrderNonContiguousRangesForm);
formBttn = OEPGRQ_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPGRQ_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2438_OEPGRQ_PARAM");
			CPCommon.WaitControlDisplayed(OEPGRQ_ProjectNonContiguousRangesLink);
OEPGRQ_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExist on ProjectNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OEPGRQ_ProjectNonContiguousRangesTable = new Control("ProjectNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPGRQ_ProjectNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPGRQ_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPGRQ_ProjectNonContiguousRangesForm);
formBttn = OEPGRQ_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Catalog Non-Conti.");


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Click on SOLineNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPGRQ_SOLineNonContiguousRangesLink = new Control("SOLineNonContiguousRangesLink", "ID", "lnk_2441_OEPGRQ_PARAM");
			CPCommon.WaitControlDisplayed(OEPGRQ_SOLineNonContiguousRangesLink);
OEPGRQ_SOLineNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming VerifyExist on SOLineNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OEPGRQ_SOLineNonContiguousRangesTable = new Control("SOLineNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPGRQ_SOLN_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPGRQ_SOLineNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OEPGRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPGRQ] Perfoming Close on SOLineNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPGRQ_SOLineNonContiguousRangesForm = new Control("SOLineNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPGRQ_SOLN_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEPGRQ_SOLineNonContiguousRangesForm);
formBttn = OEPGRQ_SOLineNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OEPGRQ";
							CPCommon.WaitControlDisplayed(OEPGRQ_MainForm);
formBttn = OEPGRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

