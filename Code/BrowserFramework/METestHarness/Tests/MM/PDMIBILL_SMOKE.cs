 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMIBILL_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Product Billing", "xpath","//div[@class='navItem'][.='Product Billing']").Click();
new Control("Manage Item Billings", "xpath","//div[@class='navItem'][.='Manage Item Billings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PDMIBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMIBILL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMIBILL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMIBILL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMIBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMIBILL] Perfoming VerifyExists on Item...", Logger.MessageType.INF);
			Control PDMIBILL_Item = new Control("Item", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDMIBILL_Item.Exists());

											Driver.SessionLogger.WriteLine("TAB");


												
				CPCommon.CurrentComponent = "PDMIBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMIBILL] Perfoming VerifyExists on ItemBillings_BasicInformation_SellingDescription...", Logger.MessageType.INF);
			Control PDMIBILL_ItemBillings_BasicInformation_SellingDescription = new Control("ItemBillings_BasicInformation_SellingDescription", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SALES_ITEM_DESC']");
			CPCommon.AssertEqual(true,PDMIBILL_ItemBillings_BasicInformation_SellingDescription.Exists());

												
				CPCommon.CurrentComponent = "PDMIBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMIBILL] Perfoming Select on ItemBillings_MainTab...", Logger.MessageType.INF);
			Control PDMIBILL_ItemBillings_MainTab = new Control("ItemBillings_MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PDMIBILL_ItemBillings_MainTab);
IWebElement mTab = PDMIBILL_ItemBillings_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PDMIBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMIBILL] Perfoming VerifyExists on ItemBillings_ShippingInformation_Weight...", Logger.MessageType.INF);
			Control PDMIBILL_ItemBillings_ShippingInformation_Weight = new Control("ItemBillings_ShippingInformation_Weight", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SHIP_WGT_QTY']");
			CPCommon.AssertEqual(true,PDMIBILL_ItemBillings_ShippingInformation_Weight.Exists());

											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "PDMIBILL";
							CPCommon.WaitControlDisplayed(PDMIBILL_MainForm);
IWebElement formBttn = PDMIBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMIBILL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMIBILL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PDMIBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMIBILL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMIBILL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMIBILL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PDMIBILL";
							CPCommon.WaitControlDisplayed(PDMIBILL_MainForm);
formBttn = PDMIBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

