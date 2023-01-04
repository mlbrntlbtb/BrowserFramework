 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OERPCKSL_SMOKE : TestScript
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
new Control("Print Packing Slips", "xpath","//div[@class='navItem'][.='Print Packing Slips']").Click();


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OERPCKSL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OERPCKSL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_MainForm);
IWebElement formBttn = OERPCKSL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OERPCKSL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OERPCKSL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OERPCKSL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPCKSL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_MainForm);
formBttn = OERPCKSL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OERPCKSL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OERPCKSL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OERPCKSL_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OERPCKSL_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("CATALOG NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming Click on CatalogNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPCKSL_CatalogNonContiguousRangesLink = new Control("CatalogNonContiguousRangesLink", "ID", "lnk_2826_OERPCKSL_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(OERPCKSL_CatalogNonContiguousRangesLink);
OERPCKSL_CatalogNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on CatalogNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPCKSL_CatalogNonContiguousRangesForm = new Control("CatalogNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPCKSL_CatalogNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExist on CatalogNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERPCKSL_CatalogNonContiguousRangesTable = new Control("CatalogNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPRICECATLGCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPCKSL_CatalogNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_CatalogNonContiguousRangesForm);
formBttn = OERPCKSL_CatalogNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming Click on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPCKSL_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_2827_OERPCKSL_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(OERPCKSL_CustomerNonContiguousRangesLink);
OERPCKSL_CustomerNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPCKSL_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPCKSL_CustomerNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExist on CustomerNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERPCKSL_CustomerNonContiguousRangesTable = new Control("CustomerNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPCKSL_CustomerNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_CustomerNonContiguousRangesForm);
formBttn = OERPCKSL_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PROJECT NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming Click on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPCKSL_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2829_OERPCKSL_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(OERPCKSL_ProjectNonContiguousRangesLink);
OERPCKSL_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPCKSL_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPCKSL_ProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExist on ProjectNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERPCKSL_ProjectNonContiguousRangesTable = new Control("ProjectNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPCKSL_ProjectNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_ProjectNonContiguousRangesForm);
formBttn = OERPCKSL_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PACKING SLIP NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming Click on PackingSlipNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPCKSL_PackingSlipNonContiguousRangesLink = new Control("PackingSlipNonContiguousRangesLink", "ID", "lnk_2828_OERPCKSL_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(OERPCKSL_PackingSlipNonContiguousRangesLink);
OERPCKSL_PackingSlipNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on PackingSlipNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPCKSL_PackingSlipNonContiguousRangesForm = new Control("PackingSlipNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPCKSL_PackingSlipNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExist on PackingSlipNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERPCKSL_PackingSlipNonContiguousRangesTable = new Control("PackingSlipNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPSID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPCKSL_PackingSlipNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_PackingSlipNonContiguousRangesForm);
formBttn = OERPCKSL_PackingSlipNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SALES NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming Click on SalesOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OERPCKSL_SalesOrderNonContiguousRangesLink = new Control("SalesOrderNonContiguousRangesLink", "ID", "lnk_2830_OERPCKSL_FUNCPARMCATLG");
			CPCommon.WaitControlDisplayed(OERPCKSL_SalesOrderNonContiguousRangesLink);
OERPCKSL_SalesOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExists on SalesOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OERPCKSL_SalesOrderNonContiguousRangesForm = new Control("SalesOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OERPCKSL_SalesOrderNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OERPCKSL] Perfoming VerifyExist on SalesOrderNonContiguousRangesTable...", Logger.MessageType.INF);
			Control OERPCKSL_SalesOrderNonContiguousRangesTable = new Control("SalesOrderNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OERPCKSL_SalesOrderNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_SalesOrderNonContiguousRangesForm);
formBttn = OERPCKSL_SalesOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "OERPCKSL";
							CPCommon.WaitControlDisplayed(OERPCKSL_MainForm);
formBttn = OERPCKSL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

