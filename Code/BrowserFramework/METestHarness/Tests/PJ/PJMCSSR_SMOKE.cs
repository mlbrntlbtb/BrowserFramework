 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMCSSR_SMOKE : TestScript
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
new Control("Cost Schedule Status Report", "xpath","//div[@class='navItem'][.='Cost Schedule Status Report']").Click();
new Control("Manage CSSR Information", "xpath","//div[@class='navItem'][.='Manage CSSR Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMCSSR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMCSSR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming VerifyExists on SelectionRanges_Category...", Logger.MessageType.INF);
			Control PJMCSSR_SelectionRanges_Category = new Control("SelectionRanges_Category", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SELECT_BY_CD']");
			CPCommon.AssertEqual(true,PJMCSSR_SelectionRanges_Category.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSR";
							CPCommon.WaitControlDisplayed(PJMCSSR_MainForm);
IWebElement formBttn = PJMCSSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMCSSR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMCSSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMCSSR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCSSR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CSSR Project Detail");


												
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming VerifyExists on CSSRProjectDetailLink...", Logger.MessageType.INF);
			Control PJMCSSR_CSSRProjectDetailLink = new Control("CSSRProjectDetailLink", "ID", "lnk_3811_PJMCSSR_PROJCSSRINFO_HEADER");
			CPCommon.AssertEqual(true,PJMCSSR_CSSRProjectDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSR";
							CPCommon.WaitControlDisplayed(PJMCSSR_CSSRProjectDetailLink);
PJMCSSR_CSSRProjectDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming VerifyExist on CSSRProjectDetailsFormTable...", Logger.MessageType.INF);
			Control PJMCSSR_CSSRProjectDetailsFormTable = new Control("CSSRProjectDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSSR_PROJCSSRINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCSSR_CSSRProjectDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming ClickButton on CSSRProjectDetailsForm...", Logger.MessageType.INF);
			Control PJMCSSR_CSSRProjectDetailsForm = new Control("CSSRProjectDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSSR_PROJCSSRINFO_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMCSSR_CSSRProjectDetailsForm);
formBttn = PJMCSSR_CSSRProjectDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCSSR_CSSRProjectDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCSSR_CSSRProjectDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMCSSR";
							CPCommon.AssertEqual(true,PJMCSSR_CSSRProjectDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMCSSR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSR] Perfoming VerifyExists on CSSRProjectDetails_Project...", Logger.MessageType.INF);
			Control PJMCSSR_CSSRProjectDetails_Project = new Control("CSSRProjectDetails_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSSR_PROJCSSRINFO_CHILD_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMCSSR_CSSRProjectDetails_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSR";
							CPCommon.WaitControlDisplayed(PJMCSSR_CSSRProjectDetailsForm);
formBttn = PJMCSSR_CSSRProjectDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMCSSR";
							CPCommon.WaitControlDisplayed(PJMCSSR_MainForm);
formBttn = PJMCSSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

