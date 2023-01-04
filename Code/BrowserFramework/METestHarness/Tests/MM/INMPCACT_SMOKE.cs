 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMPCACT_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Physical Counts", "xpath","//div[@class='navItem'][.='Physical Counts']").Click();
new Control("Manage Actual Counts", "xpath","//div[@class='navItem'][.='Manage Actual Counts']").Click();


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Enter Actual Counts", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "INMPCACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCACT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMPCACT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMPCACT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMPCACT";
							CPCommon.WaitControlDisplayed(INMPCACT_MainForm);
IWebElement formBttn = INMPCACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPCACT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPCACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMPCACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCACT] Perfoming VerifyExists on Identification_CountID...", Logger.MessageType.INF);
			Control INMPCACT_Identification_CountID = new Control("Identification_CountID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COUNT_ID']");
			CPCommon.AssertEqual(true,INMPCACT_Identification_CountID.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORM");


												
				CPCommon.CurrentComponent = "INMPCACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCACT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control INMPCACT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPCACT_PHYSCOUNTDETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPCACT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPCACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCACT] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control INMPCACT_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPCACT_PHYSCOUNTDETL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INMPCACT_ChildForm);
formBttn = INMPCACT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPCACT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPCACT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INMPCACT";
							CPCommon.AssertEqual(true,INMPCACT_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "INMPCACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPCACT] Perfoming VerifyExists on ChildForm_LineDetails_ControlNo...", Logger.MessageType.INF);
			Control INMPCACT_ChildForm_LineDetails_ControlNo = new Control("ChildForm_LineDetails_ControlNo", "xpath", "//div[translate(@id,'0123456789','')='pr__INMPCACT_PHYSCOUNTDETL_']/ancestor::form[1]/descendant::*[@id='CONTROL_NO']");
			CPCommon.AssertEqual(true,INMPCACT_ChildForm_LineDetails_ControlNo.Exists());

											Driver.SessionLogger.WriteLine("SERIALLOT/INFO");


												
				CPCommon.CurrentComponent = "INMPCACT";
							CPCommon.WaitControlDisplayed(INMPCACT_MainForm);
formBttn = INMPCACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

