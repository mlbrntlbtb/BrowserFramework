 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMCDSC_SMOKE : TestScript
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
new Control("Manage Customer Volume Discounts", "xpath","//div[@class='navItem'][.='Manage Customer Volume Discounts']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Customer Volume Discounts", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMCDSC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCDSC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMCDSC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMCDSC_MainForm);
IWebElement formBttn = BLMCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMCDSC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMCDSC";
							CPCommon.AssertEqual(true,BLMCDSC_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExists on Customer...", Logger.MessageType.INF);
			Control BLMCDSC_Customer = new Control("Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLMCDSC_Customer.Exists());

											Driver.SessionLogger.WriteLine("CUSTOMER VOLUME DETAILS");


												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExist on CustomerVolumeDetailsFormTable...", Logger.MessageType.INF);
			Control BLMCDSC_CustomerVolumeDetailsFormTable = new Control("CustomerVolumeDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCDSC_CUSTVOLUMEDISC_CHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCDSC_CustomerVolumeDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming ClickButton on CustomerVolumeDetailsForm...", Logger.MessageType.INF);
			Control BLMCDSC_CustomerVolumeDetailsForm = new Control("CustomerVolumeDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCDSC_CUSTVOLUMEDISC_CHD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMCDSC_CustomerVolumeDetailsForm);
formBttn = BLMCDSC_CustomerVolumeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMCDSC_CustomerVolumeDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMCDSC_CustomerVolumeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMCDSC";
							CPCommon.AssertEqual(true,BLMCDSC_CustomerVolumeDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExists on CustomerVolumeDiscount_StartingSalesVolume...", Logger.MessageType.INF);
			Control BLMCDSC_CustomerVolumeDiscount_StartingSalesVolume = new Control("CustomerVolumeDiscount_StartingSalesVolume", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCDSC_CUSTVOLUMEDISC_CHD_']/ancestor::form[1]/descendant::*[@id='FROM_QTY']");
			CPCommon.AssertEqual(true,BLMCDSC_CustomerVolumeDiscount_StartingSalesVolume.Exists());

											Driver.SessionLogger.WriteLine("ACCOUNTS");


												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExists on AccountsLink...", Logger.MessageType.INF);
			Control BLMCDSC_AccountsLink = new Control("AccountsLink", "ID", "lnk_1007588_BLMCDSC_CUSTVOLUMEDISC_HDR");
			CPCommon.AssertEqual(true,BLMCDSC_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCDSC";
							CPCommon.WaitControlDisplayed(BLMCDSC_AccountsLink);
BLMCDSC_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExists on AccountsForm...", Logger.MessageType.INF);
			Control BLMCDSC_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCDSC_CUSTACCTDISC_CHD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCDSC_AccountsForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExist on AccountsFormTable...", Logger.MessageType.INF);
			Control BLMCDSC_AccountsFormTable = new Control("AccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCDSC_CUSTACCTDISC_CHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCDSC_AccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCDSC] Perfoming VerifyExists on Accounts_Ok...", Logger.MessageType.INF);
			Control BLMCDSC_Accounts_Ok = new Control("Accounts_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCDSC_CUSTACCTDISC_CHD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLMCDSC_Accounts_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMCDSC";
							CPCommon.WaitControlDisplayed(BLMCDSC_AccountsForm);
formBttn = BLMCDSC_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMCDSC";
							CPCommon.WaitControlDisplayed(BLMCDSC_MainForm);
formBttn = BLMCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

