 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMLWMNT_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Lien Waiver Controls", "xpath","//div[@class='navItem'][.='Lien Waiver Controls']").Click();
new Control("Manage Lien Waiver Information", "xpath","//div[@class='navItem'][.='Manage Lien Waiver Information']").Click();


												
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APMLWMNT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APMLWMNT_MainForm);
IWebElement formBttn = APMLWMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
APMLWMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.AssertEqual(true,APMLWMNT_MainForm.Exists());

													
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_MainForm);
formBttn = APMLWMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_MainForm);
formBttn = APMLWMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_MainForm);
formBttn = APMLWMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMLWMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control APMLWMNT_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,APMLWMNT_Project.Exists());

												
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming VerifyExist on LienWaiverInformationFormTable...", Logger.MessageType.INF);
			Control APMLWMNT_LienWaiverInformationFormTable = new Control("LienWaiverInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMLWMNT_LIENWAIVERHDR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMLWMNT_LienWaiverInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming ClickButtonIfExists on LienWaiverInformationForm...", Logger.MessageType.INF);
			Control APMLWMNT_LienWaiverInformationForm = new Control("LienWaiverInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMLWMNT_LIENWAIVERHDR_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMLWMNT_LienWaiverInformationForm);
formBttn = APMLWMNT_LienWaiverInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMLWMNT_LienWaiverInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMLWMNT_LienWaiverInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.AssertEqual(true,APMLWMNT_LienWaiverInformationForm.Exists());

													
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming VerifyExists on LienWaiverInformation_LienNumber...", Logger.MessageType.INF);
			Control APMLWMNT_LienWaiverInformation_LienNumber = new Control("LienWaiverInformation_LienNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__APMLWMNT_LIENWAIVERHDR_CTW_']/ancestor::form[1]/descendant::*[@id='LIEN_NO']");
			CPCommon.AssertEqual(true,APMLWMNT_LienWaiverInformation_LienNumber.Exists());

												
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming VerifyExists on LienWaiverInformation_LienWaiverDetailLink...", Logger.MessageType.INF);
			Control APMLWMNT_LienWaiverInformation_LienWaiverDetailLink = new Control("LienWaiverInformation_LienWaiverDetailLink", "ID", "lnk_1002125_APMLWMNT_LIENWAIVERHDR_CTW");
			CPCommon.AssertEqual(true,APMLWMNT_LienWaiverInformation_LienWaiverDetailLink.Exists());

												
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_LienWaiverInformation_LienWaiverDetailLink);
APMLWMNT_LienWaiverInformation_LienWaiverDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming VerifyExist on LienLienWaiverDetailFormTable...", Logger.MessageType.INF);
			Control APMLWMNT_LienLienWaiverDetailFormTable = new Control("LienLienWaiverDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMLWMNT_LIENWVRVCHRDETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMLWMNT_LienLienWaiverDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming ClickButtonIfExists on LienWaiverDetailForm...", Logger.MessageType.INF);
			Control APMLWMNT_LienWaiverDetailForm = new Control("LienWaiverDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMLWMNT_LIENWVRVCHRDETL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMLWMNT_LienWaiverDetailForm);
formBttn = APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_LienWaiverDetailForm);
formBttn = APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "APMLWMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMLWMNT] Perfoming VerifyExists on LienWaiverDetail_CheckDetails_CheckNumber...", Logger.MessageType.INF);
			Control APMLWMNT_LienWaiverDetail_CheckDetails_CheckNumber = new Control("LienWaiverDetail_CheckDetails_CheckNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__APMLWMNT_LIENWVRVCHRDETL_']/ancestor::form[1]/descendant::*[@id='CHK_NO']");
			CPCommon.AssertEqual(true,APMLWMNT_LienWaiverDetail_CheckDetails_CheckNumber.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_LienWaiverDetailForm);
formBttn = APMLWMNT_LienWaiverDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMLWMNT";
							CPCommon.WaitControlDisplayed(APMLWMNT_MainForm);
formBttn = APMLWMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

