 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMSUEBP_SMOKE : TestScript
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
new Control("Employee Interfaces", "xpath","//div[@class='navItem'][.='Employee Interfaces']").Click();
new Control("Manage Employee Import User-Defined Format", "xpath","//div[@class='navItem'][.='Manage Employee Import User-Defined Format']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "AOMSUEBP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUEBP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMSUEBP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMSUEBP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMSUEBP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUEBP] Perfoming VerifyExists on Record...", Logger.MessageType.INF);
			Control AOMSUEBP_Record = new Control("Record", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ONECOL']");
			CPCommon.AssertEqual(true,AOMSUEBP_Record.Exists());

												
				CPCommon.CurrentComponent = "AOMSUEBP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUEBP] Perfoming VerifyExist on FieldsFormTable...", Logger.MessageType.INF);
			Control AOMSUEBP_FieldsFormTable = new Control("FieldsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMSUEBP_SEMPLPREP_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMSUEBP_FieldsFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMSUEBP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUEBP] Perfoming VerifyExist on SelectedfieldsFormTable...", Logger.MessageType.INF);
			Control AOMSUEBP_SelectedfieldsFormTable = new Control("SelectedfieldsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMSUEBP_EMPLPREPSETUP_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMSUEBP_SelectedfieldsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "AOMSUEBP";
							CPCommon.WaitControlDisplayed(AOMSUEBP_MainForm);
IWebElement formBttn = AOMSUEBP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

