 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMPLMAP_SMOKE : TestScript
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
new Control("Budget Interfaces", "xpath","//div[@class='navItem'][.='Budget Interfaces']").Click();
new Control("Configure Project Planner Mapping Definitions", "xpath","//div[@class='navItem'][.='Configure Project Planner Mapping Definitions']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "AOMPLMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMPLMAP] Perfoming VerifyExist on RowDefinitionFormTable...", Logger.MessageType.INF);
			Control AOMPLMAP_RowDefinitionFormTable = new Control("RowDefinitionFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMPLMAP_RowDefinitionFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMPLMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMPLMAP] Perfoming ClickButton on RowDefinitionForm...", Logger.MessageType.INF);
			Control AOMPLMAP_RowDefinitionForm = new Control("RowDefinitionForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(AOMPLMAP_RowDefinitionForm);
IWebElement formBttn = AOMPLMAP_RowDefinitionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMPLMAP_RowDefinitionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMPLMAP_RowDefinitionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMPLMAP";
							CPCommon.AssertEqual(true,AOMPLMAP_RowDefinitionForm.Exists());

													
				CPCommon.CurrentComponent = "AOMPLMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMPLMAP] Perfoming VerifyExists on RowDefinition_SourceName...", Logger.MessageType.INF);
			Control AOMPLMAP_RowDefinition_SourceName = new Control("RowDefinition_SourceName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SOURCE_ID']");
			CPCommon.AssertEqual(true,AOMPLMAP_RowDefinition_SourceName.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORM TABLE");


												
				CPCommon.CurrentComponent = "AOMPLMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMPLMAP] Perfoming VerifyExist on ColumnDefinitionFormTable...", Logger.MessageType.INF);
			Control AOMPLMAP_ColumnDefinitionFormTable = new Control("ColumnDefinitionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMPLMAP_SFCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMPLMAP_ColumnDefinitionFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMPLMAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMPLMAP] Perfoming VerifyExist on MappingDefinitionFormTable...", Logger.MessageType.INF);
			Control AOMPLMAP_MappingDefinitionFormTable = new Control("MappingDefinitionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMPLMAP_MAPDEF_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMPLMAP_MappingDefinitionFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMPLMAP";
							CPCommon.WaitControlDisplayed(AOMPLMAP_RowDefinitionForm);
formBttn = AOMPLMAP_RowDefinitionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

