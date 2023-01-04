 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLPWHCD_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing Master", "xpath","//div[@class='navItem'][.='Billing Master']").Click();
new Control("Assign Billing Withholding Codes", "xpath","//div[@class='navItem'][.='Assign Billing Withholding Codes']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLPWHCD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLPWHCD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLPWHCD_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLPWHCD_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("BILLING PROJECT NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLPWHCD_BillingProjectNonContiguousRangesLink = new Control("BillingProjectNonContiguousRangesLink", "ID", "lnk_16037_BLPWHCD_PARAM");
			CPCommon.AssertEqual(true,BLPWHCD_BillingProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_BillingProjectNonContiguousRangesLink);
BLPWHCD_BillingProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExist on BillingProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLPWHCD_BillingProjectNonContiguousRangesFormTable = new Control("BillingProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_PROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWHCD_BillingProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLPWHCD_BillingProjectNonContiguousRangesForm = new Control("BillingProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_PROJ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLPWHCD_BillingProjectNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingProjectNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLPWHCD_BillingProjectNonContiguousRanges_Ok = new Control("BillingProjectNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_PROJ_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLPWHCD_BillingProjectNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_BillingProjectNonContiguousRangesForm);
IWebElement formBttn = BLPWHCD_BillingProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CUSTOMER NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLPWHCD_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_16038_BLPWHCD_PARAM");
			CPCommon.AssertEqual(true,BLPWHCD_CustomerNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_CustomerNonContiguousRangesLink);
BLPWHCD_CustomerNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExist on CustomerNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLPWHCD_CustomerNonContiguousRangesFormTable = new Control("CustomerNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_CUST_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWHCD_CustomerNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLPWHCD_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_CUST_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLPWHCD_CustomerNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on CustomerNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLPWHCD_CustomerNonContiguousRanges_Ok = new Control("CustomerNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_CUST_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLPWHCD_CustomerNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_CustomerNonContiguousRangesForm);
formBttn = BLPWHCD_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PROJECT TYPE NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on ProjectTypeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLPWHCD_ProjectTypeNonContiguousRangesLink = new Control("ProjectTypeNonContiguousRangesLink", "ID", "lnk_16039_BLPWHCD_PARAM");
			CPCommon.AssertEqual(true,BLPWHCD_ProjectTypeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_ProjectTypeNonContiguousRangesLink);
BLPWHCD_ProjectTypeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExist on ProjectTypeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLPWHCD_ProjectTypeNonContiguousRangesFormTable = new Control("ProjectTypeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_PROJTYPEDC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWHCD_ProjectTypeNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on ProjectTypeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLPWHCD_ProjectTypeNonContiguousRangesForm = new Control("ProjectTypeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_PROJTYPEDC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLPWHCD_ProjectTypeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on ProjectTypeNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLPWHCD_ProjectTypeNonContiguousRanges_Ok = new Control("ProjectTypeNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_PROJTYPEDC_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLPWHCD_ProjectTypeNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_ProjectTypeNonContiguousRangesForm);
formBttn = BLPWHCD_ProjectTypeNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("BILLING USER GROUP NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingUserGroupNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLPWHCD_BillingUserGroupNonContiguousRangesLink = new Control("BillingUserGroupNonContiguousRangesLink", "ID", "lnk_16044_BLPWHCD_PARAM");
			CPCommon.AssertEqual(true,BLPWHCD_BillingUserGroupNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_BillingUserGroupNonContiguousRangesLink);
BLPWHCD_BillingUserGroupNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExist on BillingUserGroupNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLPWHCD_BillingUserGroupNonContiguousRangesFormTable = new Control("BillingUserGroupNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_BILL_USER_GRP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWHCD_BillingUserGroupNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingUserGroupNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLPWHCD_BillingUserGroupNonContiguousRangesForm = new Control("BillingUserGroupNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_BILL_USER_GRP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLPWHCD_BillingUserGroupNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingUserGroupNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLPWHCD_BillingUserGroupNonContiguousRanges_Ok = new Control("BillingUserGroupNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_BILL_USER_GRP_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLPWHCD_BillingUserGroupNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_BillingUserGroupNonContiguousRangesForm);
formBttn = BLPWHCD_BillingUserGroupNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("BILLING FORMULA NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingFormulaNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLPWHCD_BillingFormulaNonContiguousRangesLink = new Control("BillingFormulaNonContiguousRangesLink", "ID", "lnk_16045_BLPWHCD_PARAM");
			CPCommon.AssertEqual(true,BLPWHCD_BillingFormulaNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_BillingFormulaNonContiguousRangesLink);
BLPWHCD_BillingFormulaNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExist on BillingFormulaNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLPWHCD_BillingFormulaNonContiguousRangesFormTable = new Control("BillingFormulaNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_BILLFRMLADESC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWHCD_BillingFormulaNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingFormulaNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLPWHCD_BillingFormulaNonContiguousRangesForm = new Control("BillingFormulaNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_BILLFRMLADESC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLPWHCD_BillingFormulaNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWHCD] Perfoming VerifyExists on BillingFormulaNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control BLPWHCD_BillingFormulaNonContiguousRanges_Ok = new Control("BillingFormulaNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPWHCD_NCR_BILLFRMLADESC_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLPWHCD_BillingFormulaNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_BillingFormulaNonContiguousRangesForm);
formBttn = BLPWHCD_BillingFormulaNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "BLPWHCD";
							CPCommon.WaitControlDisplayed(BLPWHCD_MainForm);
formBttn = BLPWHCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

