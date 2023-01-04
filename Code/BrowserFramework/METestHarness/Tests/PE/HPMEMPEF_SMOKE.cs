 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMEMPEF_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Termination Processing", "xpath","//div[@class='navItem'][.='Termination Processing']").Click();
new Control("Manage Employee Exit Interviews", "xpath","//div[@class='navItem'][.='Manage Employee Exit Interviews']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMEMPEF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMEMPEF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HPMEMPEF_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HPMEMPEF_Employee.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_MainForm);
IWebElement formBttn = HPMEMPEF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMEMPEF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMEMPEF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMEMPEF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEMPEF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("EMPLOYEE INFORMATION LINK");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming Click on EmployeeInformationLink...", Logger.MessageType.INF);
			Control HPMEMPEF_EmployeeInformationLink = new Control("EmployeeInformationLink", "ID", "lnk_1003341_HPMEMPEF_HEMPLEXITINTV_HDR");
			CPCommon.WaitControlDisplayed(HPMEMPEF_EmployeeInformationLink);
HPMEMPEF_EmployeeInformationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on EmployeeInformationForm...", Logger.MessageType.INF);
			Control HPMEMPEF_EmployeeInformationForm = new Control("EmployeeInformationForm", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HSECT1EMPLEXIT_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEMPEF_EmployeeInformationForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on EmployeeInformation_ForwardingAddress_Line1...", Logger.MessageType.INF);
			Control HPMEMPEF_EmployeeInformation_ForwardingAddress_Line1 = new Control("EmployeeInformation_ForwardingAddress_Line1", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HSECT1EMPLEXIT_')]/ancestor::form[1]/descendant::*[@id='LN_1_ADR']");
			CPCommon.AssertEqual(true,HPMEMPEF_EmployeeInformation_ForwardingAddress_Line1.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_EmployeeInformationForm);
formBttn = HPMEMPEF_EmployeeInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SECURITY & PROPERTY LINK");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming Click on SecurityPropertyLink...", Logger.MessageType.INF);
			Control HPMEMPEF_SecurityPropertyLink = new Control("SecurityPropertyLink", "ID", "lnk_1003342_HPMEMPEF_HEMPLEXITINTV_HDR");
			CPCommon.WaitControlDisplayed(HPMEMPEF_SecurityPropertyLink);
HPMEMPEF_SecurityPropertyLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExist on SecurityPropertyFormTable...", Logger.MessageType.INF);
			Control HPMEMPEF_SecurityPropertyFormTable = new Control("SecurityPropertyFormTable", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS2LNSEC_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEMPEF_SecurityPropertyFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming ClickButton on SecurityPropertyForm...", Logger.MessageType.INF);
			Control HPMEMPEF_SecurityPropertyForm = new Control("SecurityPropertyForm", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS2LNSEC_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMEMPEF_SecurityPropertyForm);
formBttn = HPMEMPEF_SecurityPropertyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEMPEF_SecurityPropertyForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEMPEF_SecurityPropertyForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.AssertEqual(true,HPMEMPEF_SecurityPropertyForm.Exists());

													
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on SecurityProperty_OrderNumber...", Logger.MessageType.INF);
			Control HPMEMPEF_SecurityProperty_OrderNumber = new Control("SecurityProperty_OrderNumber", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS2LNSEC_')]/ancestor::form[1]/descendant::*[@id='ORDER_NO']");
			CPCommon.AssertEqual(true,HPMEMPEF_SecurityProperty_OrderNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_SecurityPropertyForm);
formBttn = HPMEMPEF_SecurityPropertyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("COMPENSATION & BENEFIT LINK");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming Click on CompensationBenefitsLink...", Logger.MessageType.INF);
			Control HPMEMPEF_CompensationBenefitsLink = new Control("CompensationBenefitsLink", "ID", "lnk_1003343_HPMEMPEF_HEMPLEXITINTV_HDR");
			CPCommon.WaitControlDisplayed(HPMEMPEF_CompensationBenefitsLink);
HPMEMPEF_CompensationBenefitsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExist on CompensationBenefitsLinkFormTable...", Logger.MessageType.INF);
			Control HPMEMPEF_CompensationBenefitsLinkFormTable = new Control("CompensationBenefitsLinkFormTable", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS3LNCOMP_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEMPEF_CompensationBenefitsLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming ClickButton on CompensationBenefitsLinkForm...", Logger.MessageType.INF);
			Control HPMEMPEF_CompensationBenefitsLinkForm = new Control("CompensationBenefitsLinkForm", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS3LNCOMP_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMEMPEF_CompensationBenefitsLinkForm);
formBttn = HPMEMPEF_CompensationBenefitsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEMPEF_CompensationBenefitsLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEMPEF_CompensationBenefitsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on CompensationBenefits_OrderNumber...", Logger.MessageType.INF);
			Control HPMEMPEF_CompensationBenefits_OrderNumber = new Control("CompensationBenefits_OrderNumber", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS3LNCOMP_')]/ancestor::form[1]/descendant::*[@id='ORDER_NO']");
			CPCommon.AssertEqual(true,HPMEMPEF_CompensationBenefits_OrderNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_CompensationBenefitsLinkForm);
formBttn = HPMEMPEF_CompensationBenefitsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LEAVE LINK");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming Click on LeaveLink...", Logger.MessageType.INF);
			Control HPMEMPEF_LeaveLink = new Control("LeaveLink", "ID", "lnk_1003344_HPMEMPEF_HEMPLEXITINTV_HDR");
			CPCommon.WaitControlDisplayed(HPMEMPEF_LeaveLink);
HPMEMPEF_LeaveLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on LeaveForm...", Logger.MessageType.INF);
			Control HPMEMPEF_LeaveForm = new Control("LeaveForm", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS4LNLV_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEMPEF_LeaveForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExist on LeaveFormTable...", Logger.MessageType.INF);
			Control HPMEMPEF_LeaveFormTable = new Control("LeaveFormTable", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS4LNLV_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEMPEF_LeaveFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_LeaveForm);
formBttn = HPMEMPEF_LeaveForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEMPEF_LeaveForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEMPEF_LeaveForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on Leave_OrderNumber...", Logger.MessageType.INF);
			Control HPMEMPEF_Leave_OrderNumber = new Control("Leave_OrderNumber", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS4LNLV_')]/ancestor::form[1]/descendant::*[@id='ORDER_NO']");
			CPCommon.AssertEqual(true,HPMEMPEF_Leave_OrderNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_LeaveForm);
formBttn = HPMEMPEF_LeaveForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("AMOUNTS OWED LINK");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming Click on AmountsOwedLink...", Logger.MessageType.INF);
			Control HPMEMPEF_AmountsOwedLink = new Control("AmountsOwedLink", "ID", "lnk_1003345_HPMEMPEF_HEMPLEXITINTV_HDR");
			CPCommon.WaitControlDisplayed(HPMEMPEF_AmountsOwedLink);
HPMEMPEF_AmountsOwedLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExist on AmountsOwedLinkFormTable...", Logger.MessageType.INF);
			Control HPMEMPEF_AmountsOwedLinkFormTable = new Control("AmountsOwedLinkFormTable", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS5LNAMT_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEMPEF_AmountsOwedLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming ClickButton on AmountsOwedLinkForm...", Logger.MessageType.INF);
			Control HPMEMPEF_AmountsOwedLinkForm = new Control("AmountsOwedLinkForm", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS5LNAMT_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMEMPEF_AmountsOwedLinkForm);
formBttn = HPMEMPEF_AmountsOwedLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEMPEF_AmountsOwedLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEMPEF_AmountsOwedLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.AssertEqual(true,HPMEMPEF_AmountsOwedLinkForm.Exists());

													
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on AmountsOwed_OrderNumber...", Logger.MessageType.INF);
			Control HPMEMPEF_AmountsOwed_OrderNumber = new Control("AmountsOwed_OrderNumber", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS5LNAMT_')]/ancestor::form[1]/descendant::*[@id='ORDER_NO']");
			CPCommon.AssertEqual(true,HPMEMPEF_AmountsOwed_OrderNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_AmountsOwedLinkForm);
formBttn = HPMEMPEF_AmountsOwedLinkForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("COMMENTS LINK");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming Click on CommentsLink...", Logger.MessageType.INF);
			Control HPMEMPEF_CommentsLink = new Control("CommentsLink", "ID", "lnk_1003347_HPMEMPEF_HEMPLEXITINTV_HDR");
			CPCommon.WaitControlDisplayed(HPMEMPEF_CommentsLink);
HPMEMPEF_CommentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on CommentsLinkForm...", Logger.MessageType.INF);
			Control HPMEMPEF_CommentsLinkForm = new Control("CommentsLinkForm", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS6COMMENTS_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEMPEF_CommentsLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEMPEF] Perfoming VerifyExists on Comments_Comments...", Logger.MessageType.INF);
			Control HPMEMPEF_Comments_Comments = new Control("Comments_Comments", "xpath", "//div[starts-with(@id,'pr__HPMEMPEF_HEMPLS6COMMENTS_')]/ancestor::form[1]/descendant::*[@id='COMMENTS']");
			CPCommon.AssertEqual(true,HPMEMPEF_Comments_Comments.Exists());

												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_CommentsLinkForm);
formBttn = HPMEMPEF_CommentsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPMEMPEF";
							CPCommon.WaitControlDisplayed(HPMEMPEF_MainForm);
formBttn = HPMEMPEF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

