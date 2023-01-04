 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMPMCC_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='deptItem'][.='Bills of Material']").Click();
new Control("Bills of Material", "xpath","//div[@class='navItem'][.='Bills of Material']").Click();
new Control("Apply Mass Component Changes to Bills of Material", "xpath","//div[@class='navItem'][.='Apply Mass Component Changes to Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMPMCC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMPMCC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BMPMCC_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BMPMCC_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.WaitControlDisplayed(BMPMCC_MainForm);
IWebElement formBttn = BMPMCC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMPMCC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMPMCC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMPMCC_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMPMCC_MainTable.Exists());

											Driver.SessionLogger.WriteLine("COMPONENT MBOMS WHERE USED");


												
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExists on ComponentMBOMsWhereUsedLink...", Logger.MessageType.INF);
			Control BMPMCC_ComponentMBOMsWhereUsedLink = new Control("ComponentMBOMsWhereUsedLink", "ID", "lnk_1004513_BMPMCC_PARAM");
			CPCommon.AssertEqual(true,BMPMCC_ComponentMBOMsWhereUsedLink.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.WaitControlDisplayed(BMPMCC_ComponentMBOMsWhereUsedLink);
BMPMCC_ComponentMBOMsWhereUsedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExist on ComponentMBOMSWhereUsedTable...", Logger.MessageType.INF);
			Control BMPMCC_ComponentMBOMSWhereUsedTable = new Control("ComponentMBOMSWhereUsedTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMPMCC_MFGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMPMCC_ComponentMBOMSWhereUsedTable.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming ClickButton on ComponentMBOMSWhereUsedForm...", Logger.MessageType.INF);
			Control BMPMCC_ComponentMBOMSWhereUsedForm = new Control("ComponentMBOMSWhereUsedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMPMCC_MFGBOM_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMPMCC_ComponentMBOMSWhereUsedForm);
formBttn = BMPMCC_ComponentMBOMSWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMPMCC_ComponentMBOMSWhereUsedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMPMCC_ComponentMBOMSWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.AssertEqual(true,BMPMCC_ComponentMBOMSWhereUsedForm.Exists());

													
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExists on ComponentMBOMsWhereUsed_AssemblyBOMLine_Part...", Logger.MessageType.INF);
			Control BMPMCC_ComponentMBOMsWhereUsed_AssemblyBOMLine_Part = new Control("ComponentMBOMsWhereUsed_AssemblyBOMLine_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__BMPMCC_MFGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,BMPMCC_ComponentMBOMsWhereUsed_AssemblyBOMLine_Part.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.WaitControlDisplayed(BMPMCC_ComponentMBOMSWhereUsedForm);
formBttn = BMPMCC_ComponentMBOMSWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("COMPONENT EBOMS WHERE USED");


												
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExists on ComponentEBOMsWhereUsedLink...", Logger.MessageType.INF);
			Control BMPMCC_ComponentEBOMsWhereUsedLink = new Control("ComponentEBOMsWhereUsedLink", "ID", "lnk_1004511_BMPMCC_PARAM");
			CPCommon.AssertEqual(true,BMPMCC_ComponentEBOMsWhereUsedLink.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.WaitControlDisplayed(BMPMCC_ComponentEBOMsWhereUsedLink);
BMPMCC_ComponentEBOMsWhereUsedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExist on ComponentEBOMSWhereUsedTable...", Logger.MessageType.INF);
			Control BMPMCC_ComponentEBOMSWhereUsedTable = new Control("ComponentEBOMSWhereUsedTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMPMCC_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMPMCC_ComponentEBOMSWhereUsedTable.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming ClickButton on ComponentEBOMSWhereUsedForm...", Logger.MessageType.INF);
			Control BMPMCC_ComponentEBOMSWhereUsedForm = new Control("ComponentEBOMSWhereUsedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMPMCC_ENGBOM_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMPMCC_ComponentEBOMSWhereUsedForm);
formBttn = BMPMCC_ComponentEBOMSWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMPMCC_ComponentEBOMSWhereUsedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMPMCC_ComponentEBOMSWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.AssertEqual(true,BMPMCC_ComponentEBOMSWhereUsedForm.Exists());

													
				CPCommon.CurrentComponent = "BMPMCC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPMCC] Perfoming VerifyExists on ComponentEBOMsWhereUsed_AssemblyBOMLine_Part...", Logger.MessageType.INF);
			Control BMPMCC_ComponentEBOMsWhereUsed_AssemblyBOMLine_Part = new Control("ComponentEBOMsWhereUsed_AssemblyBOMLine_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__BMPMCC_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,BMPMCC_ComponentEBOMsWhereUsed_AssemblyBOMLine_Part.Exists());

												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.WaitControlDisplayed(BMPMCC_ComponentEBOMSWhereUsedForm);
formBttn = BMPMCC_ComponentEBOMSWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BMPMCC";
							CPCommon.WaitControlDisplayed(BMPMCC_MainForm);
formBttn = BMPMCC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

