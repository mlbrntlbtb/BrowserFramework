 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMBRNCH_SMOKE : TestScript
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
new Control("Purchasing Codes", "xpath","//div[@class='navItem'][.='Purchasing Codes']").Click();
new Control("Manage Branch Locations", "xpath","//div[@class='navItem'][.='Manage Branch Locations']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMBRNCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMBRNCH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExists on Branch...", Logger.MessageType.INF);
			Control POMBRNCH_Branch = new Control("Branch", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BRNCH_LOC_ID']");
			CPCommon.AssertEqual(true,POMBRNCH_Branch.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_MainForm);
IWebElement formBttn = POMBRNCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMBRNCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMBRNCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMBRNCH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBRNCH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_MainForm);
formBttn = POMBRNCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMBRNCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMBRNCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("BranchAddressForm");


												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExists on BranchAddressForm...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddressForm = new Control("BranchAddressForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBRNCH_BRNCHADDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBRNCH_BranchAddressForm.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExist on BranchAddressFormTable...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddressFormTable = new Control("BranchAddressFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBRNCH_BRNCHADDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBRNCH_BranchAddressFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_BranchAddressForm);
formBttn = POMBRNCH_BranchAddressForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMBRNCH_BranchAddressForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMBRNCH_BranchAddressForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExists on BranchAddress_AddressCode...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddress_AddressCode = new Control("BranchAddress_AddressCode", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBRNCH_BRNCHADDR_']/ancestor::form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,POMBRNCH_BranchAddress_AddressCode.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming Click on BranchAddress_ContactsLink...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddress_ContactsLink = new Control("BranchAddress_ContactsLink", "ID", "lnk_1007446_POMBRNCH_BRNCHADDR");
			CPCommon.WaitControlDisplayed(POMBRNCH_BranchAddress_ContactsLink);
POMBRNCH_BranchAddress_ContactsLink.Click(1.5);


											Driver.SessionLogger.WriteLine("BranchAddress_ContactsForm");


												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExists on BranchAddress_ContactsForm...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddress_ContactsForm = new Control("BranchAddress_ContactsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBRNCH_BRNCHADDRCNTACT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMBRNCH_BranchAddress_ContactsForm.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExists on BranchAddress_Contacts_Sequence...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddress_Contacts_Sequence = new Control("BranchAddress_Contacts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBRNCH_BRNCHADDRCNTACT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,POMBRNCH_BranchAddress_Contacts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_BranchAddress_ContactsForm);
formBttn = POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POMBRNCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMBRNCH] Perfoming VerifyExist on BranchAddress_ContactsFormTable...", Logger.MessageType.INF);
			Control POMBRNCH_BranchAddress_ContactsFormTable = new Control("BranchAddress_ContactsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMBRNCH_BRNCHADDRCNTACT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMBRNCH_BranchAddress_ContactsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_BranchAddress_ContactsForm);
formBttn = POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_BranchAddress_ContactsForm);
formBttn = POMBRNCH_BranchAddress_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "POMBRNCH";
							CPCommon.WaitControlDisplayed(POMBRNCH_MainForm);
formBttn = POMBRNCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

