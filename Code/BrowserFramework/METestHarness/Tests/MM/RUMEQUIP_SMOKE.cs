 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMEQUIP_SMOKE : TestScript
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
new Control("Routings Controls", "xpath","//div[@class='navItem'][.='Routings Controls']").Click();
new Control("Manage Equipment", "xpath","//div[@class='navItem'][.='Manage Equipment']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUMEQUIP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUMEQUIP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on EquipmentID...", Logger.MessageType.INF);
			Control RUMEQUIP_EquipmentID = new Control("EquipmentID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EQUIP_ID']");
			CPCommon.AssertEqual(true,RUMEQUIP_EquipmentID.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control RUMEQUIP_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RUMEQUIP_MainTab);
IWebElement mTab = RUMEQUIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Equipment Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on EquipmentDetail_EquipmentType...", Logger.MessageType.INF);
			Control RUMEQUIP_EquipmentDetail_EquipmentType = new Control("EquipmentDetail_EquipmentType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_EQUIP_TYPE_CD']");
			CPCommon.AssertEqual(true,RUMEQUIP_EquipmentDetail_EquipmentType.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MainTab);
mTab = RUMEQUIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Source Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on SourceInfo_Source_Manufacturer...", Logger.MessageType.INF);
			Control RUMEQUIP_SourceInfo_Source_Manufacturer = new Control("SourceInfo_Source_Manufacturer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,RUMEQUIP_SourceInfo_Source_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MainTab);
mTab = RUMEQUIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on Notes_Notes...", Logger.MessageType.INF);
			Control RUMEQUIP_Notes_Notes = new Control("Notes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,RUMEQUIP_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MainForm);
IWebElement formBttn = RUMEQUIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RUMEQUIP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RUMEQUIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RUMEQUIP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMEQUIP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MainForm);
formBttn = RUMEQUIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMEQUIP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMEQUIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming Click on MaintenanceLink...", Logger.MessageType.INF);
			Control RUMEQUIP_MaintenanceLink = new Control("MaintenanceLink", "ID", "lnk_1721_RUMEQUIP_EQUIP");
			CPCommon.WaitControlDisplayed(RUMEQUIP_MaintenanceLink);
RUMEQUIP_MaintenanceLink.Click(1.5);


											Driver.SessionLogger.WriteLine("MaintenanceForm");


												
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on MaintenanceForm...", Logger.MessageType.INF);
			Control RUMEQUIP_MaintenanceForm = new Control("MaintenanceForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMEQUIP_EQUIP_MAINT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUMEQUIP_MaintenanceForm.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExist on MaintenanceFormTable...", Logger.MessageType.INF);
			Control RUMEQUIP_MaintenanceFormTable = new Control("MaintenanceFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMEQUIP_EQUIP_MAINT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMEQUIP_MaintenanceFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MaintenanceForm);
formBttn = RUMEQUIP_MaintenanceForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMEQUIP_MaintenanceForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMEQUIP_MaintenanceForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUMEQUIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMEQUIP] Perfoming VerifyExists on Maintenance_MaintenanceType...", Logger.MessageType.INF);
			Control RUMEQUIP_Maintenance_MaintenanceType = new Control("Maintenance_MaintenanceType", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMEQUIP_EQUIP_MAINT_']/ancestor::form[1]/descendant::*[@id='MAINT_TYPE_CD']");
			CPCommon.AssertEqual(true,RUMEQUIP_Maintenance_MaintenanceType.Exists());

												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MaintenanceForm);
formBttn = RUMEQUIP_MaintenanceForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "RUMEQUIP";
							CPCommon.WaitControlDisplayed(RUMEQUIP_MainForm);
formBttn = RUMEQUIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

