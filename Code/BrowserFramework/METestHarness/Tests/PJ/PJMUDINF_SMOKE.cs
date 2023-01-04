 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMUDINF_SMOKE : TestScript
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
new Control("Project setup", "xpath","//div[@class='deptItem'][.='Project setup']").Click();
new Control("Project Master", "xpath","//div[@class='navItem'][.='Project Master']").Click();
new Control("Manage User-Defined Information", "xpath","//div[@class='navItem'][.='Manage User-Defined Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("User-Defined Information", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "PJMUDINF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDINF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMUDINF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMUDINF_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMUDINF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDINF] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMUDINF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMUDINF_MainForm);
IWebElement formBttn = PJMUDINF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMUDINF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMUDINF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMUDINF";
							CPCommon.AssertEqual(true,PJMUDINF_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMUDINF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDINF] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMUDINF_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ID']");
			CPCommon.AssertEqual(true,PJMUDINF_Project.Exists());

											Driver.SessionLogger.WriteLine("USE-DEFINED INFO");


												
				CPCommon.CurrentComponent = "PJMUDINF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDINF] Perfoming VerifyExist on UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control PJMUDINF_UserDefinedInfoFormTable = new Control("UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMUDINF_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMUDINF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDINF] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control PJMUDINF_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMUDINF_UserDefinedInfoForm);
formBttn = PJMUDINF_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMUDINF_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMUDINF_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMUDINF";
							CPCommon.AssertEqual(true,PJMUDINF_UserDefinedInfoForm.Exists());

													
				CPCommon.CurrentComponent = "PJMUDINF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDINF] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control PJMUDINF_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,PJMUDINF_UserDefinedInfo_DataType.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMUDINF";
							CPCommon.WaitControlDisplayed(PJMUDINF_MainForm);
formBttn = PJMUDINF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

