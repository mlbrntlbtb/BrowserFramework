 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMUDAST_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Asset Master Records", "xpath","//div[@class='navItem'][.='Asset Master Records']").Click();
new Control("Manage Asset User-Defined Information", "xpath","//div[@class='navItem'][.='Manage Asset User-Defined Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "FAMUDAST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDAST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMUDAST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMUDAST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMUDAST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDAST] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMUDAST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMUDAST_MainForm);
IWebElement formBttn = FAMUDAST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMUDAST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMUDAST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMUDAST";
							CPCommon.AssertEqual(true,FAMUDAST_MainForm.Exists());

													
				CPCommon.CurrentComponent = "FAMUDAST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDAST] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMUDAST_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ID']");
			CPCommon.AssertEqual(true,FAMUDAST_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMUDAST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDAST] Perfoming VerifyExist on UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control FAMUDAST_UserDefinedInfoFormTable = new Control("UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMUDAST_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMUDAST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDAST] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control FAMUDAST_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMUDAST_UserDefinedInfoForm);
formBttn = FAMUDAST_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMUDAST_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMUDAST_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMUDAST";
							CPCommon.AssertEqual(true,FAMUDAST_UserDefinedInfoForm.Exists());

													
				CPCommon.CurrentComponent = "FAMUDAST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDAST] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control FAMUDAST_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,FAMUDAST_UserDefinedInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "FAMUDAST";
							CPCommon.WaitControlDisplayed(FAMUDAST_MainForm);
formBttn = FAMUDAST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

