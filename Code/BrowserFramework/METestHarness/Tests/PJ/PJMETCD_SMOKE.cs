 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMETCD_SMOKE : TestScript
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
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Estimate To Complete", "xpath","//div[@class='navItem'][.='Estimate To Complete']").Click();
new Control("Manage Estimate To Complete Amounts", "xpath","//div[@class='navItem'][.='Manage Estimate To Complete Amounts']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMETCD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMETCD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMETCD_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMETCD_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_MainForm);
IWebElement formBttn = PJMETCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMETCD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMETCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMETCD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMETCD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DIRECT COSTS");


												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on DirectCostsDetailLink...", Logger.MessageType.INF);
			Control PJMETCD_DirectCostsDetailLink = new Control("DirectCostsDetailLink", "ID", "lnk_3824_PJMETCD_ETCDIRCST_HDR");
			CPCommon.AssertEqual(true,PJMETCD_DirectCostsDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming ClickButton on DirectCostsForm...", Logger.MessageType.INF);
			Control PJMETCD_DirectCostsForm = new Control("DirectCostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCDIRCOST_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMETCD_DirectCostsForm);
formBttn = PJMETCD_DirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMETCD_DirectCostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMETCD_DirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.AssertEqual(true,PJMETCD_DirectCostsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on DirectCosts_AccountInformation_Account...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_AccountInformation_Account = new Control("DirectCosts_AccountInformation_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCDIRCOST_CHILD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_AccountInformation_Account.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_DirectCostsForm);
formBttn = PJMETCD_DirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMETCD_DirectCostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMETCD_DirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExist on DirectCostsFormTable...", Logger.MessageType.INF);
			Control PJMETCD_DirectCostsFormTable = new Control("DirectCostsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCDIRCOST_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMETCD_DirectCostsFormTable.Exists());

											Driver.SessionLogger.WriteLine("INDIRECT COSTS");


												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on DirectCosts_IndirectCostsLink...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_IndirectCostsLink = new Control("DirectCosts_IndirectCostsLink", "ID", "lnk_3826_PJMETCD_ETCDIRCOST_CHILD");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_IndirectCostsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_IndirectCostsLink);
PJMETCD_DirectCosts_IndirectCostsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming ClickButton on DirectCosts_IndirectCostsForm...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_IndirectCostsForm = new Control("DirectCosts_IndirectCostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCINDCST_CHILDINDIREC_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_IndirectCostsForm);
formBttn = PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.AssertEqual(true,PJMETCD_DirectCosts_IndirectCostsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on DirectCosts_IndirectCosts_FiscalYear...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_IndirectCosts_FiscalYear = new Control("DirectCosts_IndirectCosts_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCINDCST_CHILDINDIREC_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_IndirectCosts_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_IndirectCostsForm);
formBttn = PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExist on DirectCosts_IndirectCostsFormTable...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_IndirectCostsFormTable = new Control("DirectCosts_IndirectCostsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCINDCST_CHILDINDIREC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_IndirectCostsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_IndirectCostsForm);
formBttn = PJMETCD_DirectCosts_IndirectCostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("INDIRECT COSTS");


												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on DirectCosts_ProjectFeeLink...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_ProjectFeeLink = new Control("DirectCosts_ProjectFeeLink", "ID", "lnk_3827_PJMETCD_ETCDIRCOST_CHILD");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_ProjectFeeLink.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_ProjectFeeLink);
PJMETCD_DirectCosts_ProjectFeeLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExist on DirectCosts_ProjectFeeFormTable...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_ProjectFeeFormTable = new Control("DirectCosts_ProjectFeeFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCFEE_CHILDFEE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_ProjectFeeFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming ClickButton on DirectCosts_ProjectFeeForm...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_ProjectFeeForm = new Control("DirectCosts_ProjectFeeForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCFEE_CHILDFEE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_ProjectFeeForm);
formBttn = PJMETCD_DirectCosts_ProjectFeeForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMETCD_DirectCosts_ProjectFeeForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMETCD_DirectCosts_ProjectFeeForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMETCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMETCD] Perfoming VerifyExists on DirectCosts_ProjectFee_FeeType...", Logger.MessageType.INF);
			Control PJMETCD_DirectCosts_ProjectFee_FeeType = new Control("DirectCosts_ProjectFee_FeeType", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMETCD_ETCFEE_CHILDFEE_']/ancestor::form[1]/descendant::*[@id='S_FEE_TYPE']");
			CPCommon.AssertEqual(true,PJMETCD_DirectCosts_ProjectFee_FeeType.Exists());

												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.AssertEqual(true,PJMETCD_DirectCosts_ProjectFeeForm.Exists());

													
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_DirectCosts_ProjectFeeForm);
formBttn = PJMETCD_DirectCosts_ProjectFeeForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMETCD";
							CPCommon.WaitControlDisplayed(PJMETCD_MainForm);
formBttn = PJMETCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

