 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMPCLST_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Physical Counts", "xpath","//div[@class='navItem'][.='Physical Counts']").Click();
new Control("Manage Physical Counts", "xpath","//div[@class='navItem'][.='Manage Physical Counts']").Click();


												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMPCLST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMPCLST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExists on MainForm_Identification_WAREHOUSEM...", Logger.MessageType.INF);
			Control INMPCLST_MainForm_Identification_WAREHOUSEM = new Control("MainForm_Identification_WAREHOUSEM", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,INMPCLST_MainForm_Identification_WAREHOUSEM.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.WaitControlDisplayed(INMPCLST_MainForm);
IWebElement formBttn = INMPCLST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? INMPCLST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
INMPCLST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Maintain Physical Count List", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.AssertEqual(true,INMPCLST_MainForm.Exists());

													
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.WaitControlDisplayed(INMPCLST_MainForm);
formBttn = INMPCLST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPCLST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPCLST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExists on PhysicalCountDetailsForm...", Logger.MessageType.INF);
			Control INMPCLST_PhysicalCountDetailsForm = new Control("PhysicalCountDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPCLST_PHYSCOUNTDETL_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMPCLST_PhysicalCountDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.WaitControlDisplayed(INMPCLST_PhysicalCountDetailsForm);
formBttn = INMPCLST_PhysicalCountDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPCLST_PhysicalCountDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPCLST_PhysicalCountDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("SERIAL/LOTINFO");


												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming Click on PhysicalCountDetails_SerialLotInfoLink...", Logger.MessageType.INF);
			Control INMPCLST_PhysicalCountDetails_SerialLotInfoLink = new Control("PhysicalCountDetails_SerialLotInfoLink", "ID", "lnk_1008011_INMPCLST_PHYSCOUNTDETL_DETL");
			CPCommon.WaitControlDisplayed(INMPCLST_PhysicalCountDetails_SerialLotInfoLink);
INMPCLST_PhysicalCountDetails_SerialLotInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExists on SerialLotInfoMainForm...", Logger.MessageType.INF);
			Control INMPCLST_SerialLotInfoMainForm = new Control("SerialLotInfoMainForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_PHYSCOUNTDETL_SUM_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMPCLST_SerialLotInfoMainForm.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExists on SerialLotInfoMainForm_ActualCountQty...", Logger.MessageType.INF);
			Control INMPCLST_SerialLotInfoMainForm_ActualCountQty = new Control("SerialLotInfoMainForm_ActualCountQty", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_PHYSCOUNTDETL_SUM_']/ancestor::form[1]/descendant::*[@id='ACTUAL_QTY']");
			CPCommon.AssertEqual(true,INMPCLST_SerialLotInfoMainForm_ActualCountQty.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExist on SerialLotInfoChildFormTable...", Logger.MessageType.INF);
			Control INMPCLST_SerialLotInfoChildFormTable = new Control("SerialLotInfoChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLTP_PHYSCNTDTLSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPCLST_SerialLotInfoChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming ClickButton on SerialLotInfoChildForm...", Logger.MessageType.INF);
			Control INMPCLST_SerialLotInfoChildForm = new Control("SerialLotInfoChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLTP_PHYSCNTDTLSRLT_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INMPCLST_SerialLotInfoChildForm);
formBttn = INMPCLST_SerialLotInfoChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPCLST_SerialLotInfoChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPCLST_SerialLotInfoChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.AssertEqual(true,INMPCLST_SerialLotInfoChildForm.Exists());

													
				CPCommon.CurrentComponent = "INMPCLST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCLST] Perfoming VerifyExists on SerialLotInfoChildForm_BasicInformation_ActualCountQuantity...", Logger.MessageType.INF);
			Control INMPCLST_SerialLotInfoChildForm_BasicInformation_ActualCountQuantity = new Control("SerialLotInfoChildForm_BasicInformation_ActualCountQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLTP_PHYSCNTDTLSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='ACTUAL_QTY']");
			CPCommon.AssertEqual(true,INMPCLST_SerialLotInfoChildForm_BasicInformation_ActualCountQuantity.Exists());

												
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.WaitControlDisplayed(INMPCLST_SerialLotInfoMainForm);
formBttn = INMPCLST_SerialLotInfoMainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "INMPCLST";
							CPCommon.WaitControlDisplayed(INMPCLST_MainForm);
formBttn = INMPCLST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

