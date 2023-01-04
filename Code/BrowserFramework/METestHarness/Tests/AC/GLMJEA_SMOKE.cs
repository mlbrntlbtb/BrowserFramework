 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMJEA_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Journal Entry Processing", "xpath","//div[@class='navItem'][.='Journal Entry Processing']").Click();
new Control("Approve Journal Entries", "xpath","//div[@class='navItem'][.='Approve Journal Entries']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMJEA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMJEA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on Approver...", Logger.MessageType.INF);
			Control GLMJEA_Approver = new Control("Approver", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='USER_ID']");
			CPCommon.AssertEqual(true,GLMJEA_Approver.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLMJEA_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMJEA_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLMJEA_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMJEA_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_ChildForm);
IWebElement formBttn = GLMJEA_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMJEA_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMJEA_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on ChildForm_ApproveJEDetails_JENo...", Logger.MessageType.INF);
			Control GLMJEA_ChildForm_ApproveJEDetails_JENo = new Control("ChildForm_ApproveJEDetails_JENo", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_CHILD_']/ancestor::form[1]/descendant::*[@id='JE_NO']");
			CPCommon.AssertEqual(true,GLMJEA_ChildForm_ApproveJEDetails_JENo.Exists());

											Driver.SessionLogger.WriteLine("JE Det");


												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on JEDetailsLink...", Logger.MessageType.INF);
			Control GLMJEA_JEDetailsLink = new Control("JEDetailsLink", "ID", "lnk_3541_GLMJEA_JEHDR_CHILD");
			CPCommon.AssertEqual(true,GLMJEA_JEDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_JEDetailsLink);
GLMJEA_JEDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on JEDetailsForm...", Logger.MessageType.INF);
			Control GLMJEA_JEDetailsForm = new Control("JEDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_JEDETAIL_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMJEA_JEDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on JEDetails_JEID_Type...", Logger.MessageType.INF);
			Control GLMJEA_JEDetails_JEID_Type = new Control("JEDetails_JEID_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_JEDETAIL_HDR_']/ancestor::form[1]/descendant::*[@id='S_JNL_CD']");
			CPCommon.AssertEqual(true,GLMJEA_JEDetails_JEID_Type.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_JEDetailsForm);
formBttn = GLMJEA_JEDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMJEA_JEDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMJEA_JEDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExist on JEDetailsTable...", Logger.MessageType.INF);
			Control GLMJEA_JEDetailsTable = new Control("JEDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_JEDETAIL_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMJEA_JEDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on JEDetail_DetailForm...", Logger.MessageType.INF);
			Control GLMJEA_JEDetail_DetailForm = new Control("JEDetail_DetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_JEDETAIL_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMJEA_JEDetail_DetailForm.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExist on JEDetail_DetailTable...", Logger.MessageType.INF);
			Control GLMJEA_JEDetail_DetailTable = new Control("JEDetail_DetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_JEDETAIL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMJEA_JEDetail_DetailTable.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_JEDetail_DetailForm);
formBttn = GLMJEA_JEDetail_DetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMJEA_JEDetail_DetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMJEA_JEDetail_DetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on JEDetail_Detail_Line...", Logger.MessageType.INF);
			Control GLMJEA_JEDetail_Detail_Line = new Control("JEDetail_Detail_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMJEA_JEHDR_JEDETAIL_CHILD_']/ancestor::form[1]/descendant::*[@id='JE_LN_NO']");
			CPCommon.AssertEqual(true,GLMJEA_JEDetail_Detail_Line.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_JEDetailsForm);
formBttn = GLMJEA_JEDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ExchangeRate");


												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on ExchangeRateLink...", Logger.MessageType.INF);
			Control GLMJEA_ExchangeRateLink = new Control("ExchangeRateLink", "ID", "lnk_3540_GLMJEA_JEHDR_CHILD");
			CPCommon.AssertEqual(true,GLMJEA_ExchangeRateLink.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_ExchangeRateLink);
GLMJEA_ExchangeRateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on ExchangeRateForm...", Logger.MessageType.INF);
			Control GLMJEA_ExchangeRateForm = new Control("ExchangeRateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMJEA_ExchangeRateForm.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMJEA] Perfoming VerifyExists on ExchangeRate_TransCurrency...", Logger.MessageType.INF);
			Control GLMJEA_ExchangeRate_TransCurrency = new Control("ExchangeRate_TransCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,GLMJEA_ExchangeRate_TransCurrency.Exists());

												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_ExchangeRateForm);
formBttn = GLMJEA_ExchangeRateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLMJEA";
							CPCommon.WaitControlDisplayed(GLMJEA_MainForm);
formBttn = GLMJEA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

