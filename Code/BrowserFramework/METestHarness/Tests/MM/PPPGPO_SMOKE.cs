 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPPGPO_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Purchase Requisitions", "xpath","//div[@class='navItem'][.='Purchase Requisitions']").Click();
new Control("Create Purchase Orders", "xpath","//div[@class='navItem'][.='Create Purchase Orders']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPPGPO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPPGPO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_MainForm);
IWebElement formBttn = PPPGPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPPGPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPPGPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPPGPO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPPGPO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_MainForm);
formBttn = PPPGPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPPGPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPPGPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PPPGPO_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PPPGPO_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("Non-Contiguous Buyer Ranges");


												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousBuyerLink...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousBuyerLink = new Control("NonContiguousBuyerLink", "ID", "lnk_2356_PPPGPO_PARAM");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousBuyerLink.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousBuyerLink);
PPPGPO_NonContiguousBuyerLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousBuyerForm...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousBuyerForm = new Control("NonContiguousBuyerForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_BUYER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousBuyerForm.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExist on NonContiguousBuyerLinkTable...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousBuyerLinkTable = new Control("NonContiguousBuyerLinkTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_BUYER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousBuyerLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousBuyerForm);
formBttn = PPPGPO_NonContiguousBuyerForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Non-Contiguous Vendor Ranges");


												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousVendorLink...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousVendorLink = new Control("NonContiguousVendorLink", "ID", "lnk_2357_PPPGPO_PARAM");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousVendorLink.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousVendorLink);
PPPGPO_NonContiguousVendorLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousVendorForm...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousVendorForm = new Control("NonContiguousVendorForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_VEND_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousVendorForm.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExist on NonContiguousVendorTable...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousVendorTable = new Control("NonContiguousVendorTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_VEND_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousVendorTable.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousVendorForm);
formBttn = PPPGPO_NonContiguousVendorForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Non-Contiguous Requisition Ranges");


												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousRequisitionLink...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousRequisitionLink = new Control("NonContiguousRequisitionLink", "ID", "lnk_2359_PPPGPO_PARAM");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousRequisitionLink.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousRequisitionLink);
PPPGPO_NonContiguousRequisitionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousRequisitionForm...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousRequisitionForm = new Control("NonContiguousRequisitionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_RQ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousRequisitionForm.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExist on NonContiguousRequisitionTable...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousRequisitionTable = new Control("NonContiguousRequisitionTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_RQ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousRequisitionTable.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousRequisitionForm);
formBttn = PPPGPO_NonContiguousRequisitionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Non-Contiguous Purchase Order Ranges");


												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousPurchaseOrderLink...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousPurchaseOrderLink = new Control("NonContiguousPurchaseOrderLink", "ID", "lnk_2360_PPPGPO_PARAM");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousPurchaseOrderLink.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousPurchaseOrderLink);
PPPGPO_NonContiguousPurchaseOrderLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExists on NonContiguousPurchaseOrderForm...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousPurchaseOrderForm = new Control("NonContiguousPurchaseOrderForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_PO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousPurchaseOrderForm.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPPGPO] Perfoming VerifyExist on NonContiguousPurchaseOrderTable...", Logger.MessageType.INF);
			Control PPPGPO_NonContiguousPurchaseOrderTable = new Control("NonContiguousPurchaseOrderTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPPGPO_NCR_PO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPPGPO_NonContiguousPurchaseOrderTable.Exists());

												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_NonContiguousPurchaseOrderForm);
formBttn = PPPGPO_NonContiguousPurchaseOrderForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "PPPGPO";
							CPCommon.WaitControlDisplayed(PPPGPO_MainForm);
formBttn = PPPGPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

