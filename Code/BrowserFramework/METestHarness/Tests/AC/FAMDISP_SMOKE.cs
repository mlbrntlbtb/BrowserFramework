 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMDISP_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Processing", "xpath","//div[@class='navItem'][.='Fixed Assets Processing']").Click();
new Control("Manage Disposal Transactions", "xpath","//div[@class='navItem'][.='Manage Disposal Transactions']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMDISP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDISP] Perfoming VerifyExists on DisposalDetails_Identification_AssetNo...", Logger.MessageType.INF);
			Control FAMDISP_DisposalDetails_Identification_AssetNo = new Control("DisposalDetails_Identification_AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMDISP_DisposalDetails_Identification_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMDISP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDISP] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMDISP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMDISP_MainForm);
IWebElement formBttn = FAMDISP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMDISP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMDISP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "FAMDISP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDISP] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control FAMDISP_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMDISP_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "FAMDISP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDISP] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control FAMDISP_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDISP_FADISPEDIT_FTR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMDISP_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "FAMDISP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDISP] Perfoming ClickButton on DisposalDetailsByBookForm...", Logger.MessageType.INF);
			Control FAMDISP_DisposalDetailsByBookForm = new Control("DisposalDetailsByBookForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDISP_FADISPEDIT_FTR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMDISP_DisposalDetailsByBookForm);
formBttn = FAMDISP_DisposalDetailsByBookForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMDISP_DisposalDetailsByBookForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMDISP_DisposalDetailsByBookForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMDISP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDISP] Perfoming VerifyExists on DisposalDetailsByBook_BookNo...", Logger.MessageType.INF);
			Control FAMDISP_DisposalDetailsByBook_BookNo = new Control("DisposalDetailsByBook_BookNo", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDISP_FADISPEDIT_FTR_']/ancestor::form[1]/descendant::*[@id='BOOK_NO']");
			CPCommon.AssertEqual(true,FAMDISP_DisposalDetailsByBook_BookNo.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMDISP";
							CPCommon.WaitControlDisplayed(FAMDISP_MainForm);
formBttn = FAMDISP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

