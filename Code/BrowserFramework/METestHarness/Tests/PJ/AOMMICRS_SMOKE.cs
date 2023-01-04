 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMMICRS_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost and Revenue Processing Interfaces", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Interfaces']").Click();
new Control("Manage Microframe Resource Mappings", "xpath","//div[@class='navItem'][.='Manage Microframe Resource Mappings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "AOMMICRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICRS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMMICRS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMMICRS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMMICRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICRS] Perfoming VerifyExists on MappingID...", Logger.MessageType.INF);
			Control AOMMICRS_MappingID = new Control("MappingID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='X_MAP_CD']");
			CPCommon.AssertEqual(true,AOMMICRS_MappingID.Exists());

												
				CPCommon.CurrentComponent = "AOMMICRS";
							CPCommon.WaitControlDisplayed(AOMMICRS_MainForm);
IWebElement formBttn = AOMMICRS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMMICRS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMMICRS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMMICRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICRS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMMICRS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMMICRS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("mapping Table Form");


												
				CPCommon.CurrentComponent = "AOMMICRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICRS] Perfoming VerifyExists on MappingTableLink...", Logger.MessageType.INF);
			Control AOMMICRS_MappingTableLink = new Control("MappingTableLink", "ID", "lnk_5436_AOMMICRS_XMICMAP_HDR");
			CPCommon.AssertEqual(true,AOMMICRS_MappingTableLink.Exists());

												
				CPCommon.CurrentComponent = "AOMMICRS";
							CPCommon.WaitControlDisplayed(AOMMICRS_MappingTableLink);
AOMMICRS_MappingTableLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMMICRS";
							CPCommon.AssertEqual(true,AOMMICRS_MainFormTable.Exists());

													
				CPCommon.CurrentComponent = "AOMMICRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICRS] Perfoming ClickButton on MappingTableForm...", Logger.MessageType.INF);
			Control AOMMICRS_MappingTableForm = new Control("MappingTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICRS_XMICRESMAP_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOMMICRS_MappingTableForm);
formBttn = AOMMICRS_MappingTableForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMMICRS_MappingTableForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMMICRS_MappingTableForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMMICRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICRS] Perfoming VerifyExists on MappingTable_LineType...", Logger.MessageType.INF);
			Control AOMMICRS_MappingTable_LineType = new Control("MappingTable_LineType", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICRS_XMICRESMAP_CTW_']/ancestor::form[1]/descendant::*[@id='X_LINE_TYPE_CD']");
			CPCommon.AssertEqual(true,AOMMICRS_MappingTable_LineType.Exists());

												
				CPCommon.CurrentComponent = "AOMMICRS";
							CPCommon.WaitControlDisplayed(AOMMICRS_MainForm);
formBttn = AOMMICRS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? AOMMICRS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
AOMMICRS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "AOMMICRS";
							CPCommon.WaitControlDisplayed(AOMMICRS_MainForm);
formBttn = AOMMICRS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

