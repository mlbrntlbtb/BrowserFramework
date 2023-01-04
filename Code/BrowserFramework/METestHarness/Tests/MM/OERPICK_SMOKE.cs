 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OERPICK_SMOKE : TestScript
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
new Control("Print Sales Order Pick Lists", "xpath","//div[@class='navItem'][.='Print Sales Order Pick Lists']").Click();


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OERPICK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OERPICK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_MainForm);
IWebElement formBttn = OERPICK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OERPICK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OERPICK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OERPICK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPICK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_MainForm);
formBttn = OERPICK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OERPICK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OERPICK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OERPICK_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OERPICK_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("CATALOG NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPICK_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_1004708_OERPICK_FUNCPARMCATLG_HDR");
			CPCommon.WaitControlDisplayed(OERPICK_CatalogNonContiguousRangesLink);
OERPICK_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPICK_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPICK_CatalogNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_CatalogNonContiguousRangesForm);
formBttn = OERPICK_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming Click on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPICK_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_1004709_OERPICK_FUNCPARMCATLG_HDR");
			CPCommon.WaitControlDisplayed(OERPICK_CustomerNonContiguousRangesLink);
OERPICK_CustomerNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPICK_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPICK_CustomerNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_CustomerNonContiguousRangesForm);
formBttn = OERPICK_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PROJECT NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPICK_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_1004710_OERPICK_FUNCPARMCATLG_HDR");
			CPCommon.WaitControlDisplayed(OERPICK_ProjectNonContiguousRangesLink);
OERPICK_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPICK_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPICK_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_ProjectNonContiguousRangesForm);
formBttn = OERPICK_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("WAREHOUSE NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming Click on WarehouseNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPICK_WarehouseNonContiguousRangesLink = new Control("WarehouseNonContiguousRangesLink", "ID", "lnk_1004712_OERPICK_FUNCPARMCATLG_HDR");
			CPCommon.WaitControlDisplayed(OERPICK_WarehouseNonContiguousRangesLink);
OERPICK_WarehouseNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on WarehouseNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPICK_WarehouseNonContiguousRangesForm = new Control("WarehouseNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRWHSEID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPICK_WarehouseNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_WarehouseNonContiguousRangesForm);
formBttn = OERPICK_WarehouseNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SALES NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPICK_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_1004711_OERPICK_FUNCPARMCATLG_HDR");
			CPCommon.WaitControlDisplayed(OERPICK_SalesOrderNonContiguousRangesLink);
OERPICK_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPICK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPICK] Perfoming VerifyExists on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPICK_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPICK_SalesOrderNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_SalesOrderNonContiguousRangesForm);
formBttn = OERPICK_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSING MAIN");


												
				CPCommon.CurrentComponent = "OERPICK";
							CPCommon.WaitControlDisplayed(OERPICK_MainForm);
formBttn = OERPICK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

