 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMLABOP_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Operations", "xpath","//div[@class='navItem'][.='Operations']").Click();
new Control("Manage Labor Operations", "xpath","//div[@class='navItem'][.='Manage Labor Operations']").Click();


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUMLABOP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUMLABOP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_MainForm);
IWebElement formBttn = RUMLABOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RUMLABOP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RUMLABOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RUMLABOP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_MainForm);
formBttn = RUMLABOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMLABOP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMLABOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("ACCOUNTDEFAULTS");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on AccountDefaultsLink...", Logger.MessageType.INF);
			Control RUMLABOP_AccountDefaultsLink = new Control("AccountDefaultsLink", "ID", "lnk_16770_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_AccountDefaultsLink);
RUMLABOP_AccountDefaultsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on AccountDefaults_Table...", Logger.MessageType.INF);
			Control RUMLABOP_AccountDefaults_Table = new Control("AccountDefaults_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_ACCTDEFAULTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_AccountDefaults_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Close on AccountDefaultsForm...", Logger.MessageType.INF);
			Control RUMLABOP_AccountDefaultsForm = new Control("AccountDefaultsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_ACCTDEFAULTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMLABOP_AccountDefaultsForm);
formBttn = RUMLABOP_AccountDefaultsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PLCDEFAULTS");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on PLCDefaultsLink...", Logger.MessageType.INF);
			Control RUMLABOP_PLCDefaultsLink = new Control("PLCDefaultsLink", "ID", "lnk_16088_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_PLCDefaultsLink);
RUMLABOP_PLCDefaultsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on PLCDefaults_Table...", Logger.MessageType.INF);
			Control RUMLABOP_PLCDefaults_Table = new Control("PLCDefaults_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONPLCDEFLTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_PLCDefaults_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Close on PLCDefaultsForm...", Logger.MessageType.INF);
			Control RUMLABOP_PLCDefaultsForm = new Control("PLCDefaultsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONPLCDEFLTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMLABOP_PLCDefaultsForm);
formBttn = RUMLABOP_PLCDefaultsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("LABORRATE");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on LaborRateLink...", Logger.MessageType.INF);
			Control RUMLABOP_LaborRateLink = new Control("LaborRateLink", "ID", "lnk_1001251_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_LaborRateLink);
RUMLABOP_LaborRateLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on LaborRate_Table...", Logger.MessageType.INF);
			Control RUMLABOP_LaborRate_Table = new Control("LaborRate_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONRATES_MNLBOP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_LaborRate_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Close on LaborRateForm...", Logger.MessageType.INF);
			Control RUMLABOP_LaborRateForm = new Control("LaborRateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONRATES_MNLBOP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMLABOP_LaborRateForm);
formBttn = RUMLABOP_LaborRateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("DETAIL");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on DetailLink...", Logger.MessageType.INF);
			Control RUMLABOP_DetailLink = new Control("DetailLink", "ID", "lnk_1001252_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_DetailLink);
RUMLABOP_DetailLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExists on LaborOperationDetailForm...", Logger.MessageType.INF);
			Control RUMLABOP_LaborOperationDetailForm = new Control("LaborOperationDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONNOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUMLABOP_LaborOperationDetailForm.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_LaborOperationDetailForm);
formBttn = RUMLABOP_LaborOperationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RUMLABOP_LaborOperationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RUMLABOP_LaborOperationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on LaborOperationDetail_Table...", Logger.MessageType.INF);
			Control RUMLABOP_LaborOperationDetail_Table = new Control("LaborOperationDetail_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONNOTES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_LaborOperationDetail_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_LaborOperationDetailForm);
formBttn = RUMLABOP_LaborOperationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LABOR");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on LaborLink...", Logger.MessageType.INF);
			Control RUMLABOP_LaborLink = new Control("LaborLink", "ID", "lnk_1001253_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_LaborLink);
RUMLABOP_LaborLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on LaborClassification_Table...", Logger.MessageType.INF);
			Control RUMLABOP_LaborClassification_Table = new Control("LaborClassification_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONLAB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_LaborClassification_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming ClickButton on LaborClassificationForm...", Logger.MessageType.INF);
			Control RUMLABOP_LaborClassificationForm = new Control("LaborClassificationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONLAB_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMLABOP_LaborClassificationForm);
formBttn = RUMLABOP_LaborClassificationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMLABOP_LaborClassificationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMLABOP_LaborClassificationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.AssertEqual(true,RUMLABOP_LaborClassificationForm.Exists());

													
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_LaborClassificationForm);
formBttn = RUMLABOP_LaborClassificationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EQUIPMENT");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on EquipmentLink...", Logger.MessageType.INF);
			Control RUMLABOP_EquipmentLink = new Control("EquipmentLink", "ID", "lnk_1001254_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_EquipmentLink);
RUMLABOP_EquipmentLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on Equipment_Table...", Logger.MessageType.INF);
			Control RUMLABOP_Equipment_Table = new Control("Equipment_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONEQUIP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_Equipment_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming ClickButton on EquipmentForm...", Logger.MessageType.INF);
			Control RUMLABOP_EquipmentForm = new Control("EquipmentForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONEQUIP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMLABOP_EquipmentForm);
formBttn = RUMLABOP_EquipmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMLABOP_EquipmentForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMLABOP_EquipmentForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.AssertEqual(true,RUMLABOP_EquipmentForm.Exists());

													
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_EquipmentForm);
formBttn = RUMLABOP_EquipmentForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TEXT");


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming Click on TextLink...", Logger.MessageType.INF);
			Control RUMLABOP_TextLink = new Control("TextLink", "ID", "lnk_5115_RUMLABOP_OPERATION_MAINTLABOPR");
			CPCommon.WaitControlDisplayed(RUMLABOP_TextLink);
RUMLABOP_TextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming VerifyExist on Text_Table...", Logger.MessageType.INF);
			Control RUMLABOP_Text_Table = new Control("Text_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABOP_Text_Table.Exists());

												
				CPCommon.CurrentComponent = "RUMLABOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABOP] Perfoming ClickButton on TextForm...", Logger.MessageType.INF);
			Control RUMLABOP_TextForm = new Control("TextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMLABOP_OPERATIONTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RUMLABOP_TextForm);
formBttn = RUMLABOP_TextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMLABOP_TextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMLABOP_TextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.AssertEqual(true,RUMLABOP_TextForm.Exists());

													
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_TextForm);
formBttn = RUMLABOP_TextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RUMLABOP";
							CPCommon.WaitControlDisplayed(RUMLABOP_MainForm);
formBttn = RUMLABOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

