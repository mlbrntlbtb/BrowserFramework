 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PORPPO_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchase Orders", "xpath","//div[@class='navItem'][.='Purchase Orders']").Click();
new Control("Print Purchase Orders", "xpath","//div[@class='navItem'][.='Print Purchase Orders']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PORPPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PORPPO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PORPPO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PORPPO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PORPPO";
							CPCommon.WaitControlDisplayed(PORPPO_MainForm);
IWebElement formBttn = PORPPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PORPPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PORPPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PORPPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PORPPO] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control PORPPO_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PORPPO_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "PORPPO";
							CPCommon.WaitControlDisplayed(PORPPO_MainForm);
formBttn = PORPPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PORPPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PORPPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PORPPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PORPPO] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PORPPO_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PORPPO_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("Purchase Order Non-Contiguous Ranges");


												
				CPCommon.CurrentComponent = "PORPPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PORPPO] Perfoming VerifyExists on PurchaseOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PORPPO_PurchaseOrderNonContiguousRangesLink = new Control("PurchaseOrderNonContiguousRangesLink", "ID", "lnk_2728_PORPPO_PARAM");
			CPCommon.AssertEqual(true,PORPPO_PurchaseOrderNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PORPPO";
							CPCommon.WaitControlDisplayed(PORPPO_PurchaseOrderNonContiguousRangesLink);
PORPPO_PurchaseOrderNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PORPPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PORPPO] Perfoming VerifyExists on PurchaseOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PORPPO_PurchaseOrderNonContiguousRangesForm = new Control("PurchaseOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PORPPO_NCR_POID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PORPPO_PurchaseOrderNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PORPPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PORPPO] Perfoming VerifyExist on PurchaseOrderNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control PORPPO_PurchaseOrderNonContiguousRanges_Table = new Control("PurchaseOrderNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PORPPO_NCR_POID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PORPPO_PurchaseOrderNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "PORPPO";
							CPCommon.WaitControlDisplayed(PORPPO_PurchaseOrderNonContiguousRangesForm);
formBttn = PORPPO_PurchaseOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PORPPO";
							CPCommon.WaitControlDisplayed(PORPPO_MainForm);
formBttn = PORPPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

