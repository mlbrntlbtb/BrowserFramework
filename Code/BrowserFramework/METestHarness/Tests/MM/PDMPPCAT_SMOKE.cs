 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMPPCAT_SMOKE : TestScript
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
new Control("Manage Product Price Catalogs", "xpath","//div[@class='navItem'][.='Manage Product Price Catalogs']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMPPCAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMPPCAT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on Catalog...", Logger.MessageType.INF);
			Control PDMPPCAT_Catalog = new Control("Catalog", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PRICE_CATLG_CD']");
			CPCommon.AssertEqual(true,PDMPPCAT_Catalog.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
							CPCommon.WaitControlDisplayed(PDMPPCAT_MainForm);
IWebElement formBttn = PDMPPCAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMPPCAT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMPPCAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMPPCAT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPPCAT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Price Info");


												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on PriceInformationForm...", Logger.MessageType.INF);
			Control PDMPPCAT_PriceInformationForm = new Control("PriceInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICESCH_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMPPCAT_PriceInformationForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExist on PriceInformationTable...", Logger.MessageType.INF);
			Control PDMPPCAT_PriceInformationTable = new Control("PriceInformationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICESCH_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPPCAT_PriceInformationTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming Click on PriceInformation_OK...", Logger.MessageType.INF);
			Control PDMPPCAT_PriceInformation_OK = new Control("PriceInformation_OK", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICESCH_CHILD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PDMPPCAT_PriceInformation_OK);
if (PDMPPCAT_PriceInformation_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PDMPPCAT_PriceInformation_OK.Click(5,5);
else PDMPPCAT_PriceInformation_OK.Click(4.5);


											Driver.SessionLogger.WriteLine("Cost Info");


												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming Click on CostInformationLink...", Logger.MessageType.INF);
			Control PDMPPCAT_CostInformationLink = new Control("CostInformationLink", "ID", "lnk_1003300_PDMPPCAT_PRODPRICECATLG_HDR");
			CPCommon.WaitControlDisplayed(PDMPPCAT_CostInformationLink);
PDMPPCAT_CostInformationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on CalculateCostOfGoodsSoldBasedOnForm...", Logger.MessageType.INF);
			Control PDMPPCAT_CalculateCostOfGoodsSoldBasedOnForm = new Control("CalculateCostOfGoodsSoldBasedOnForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICECATLG_CI_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMPPCAT_CalculateCostOfGoodsSoldBasedOnForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on CalculateCostOfGoodsSoldBasedOn_PercentageOfSales...", Logger.MessageType.INF);
			Control PDMPPCAT_CalculateCostOfGoodsSoldBasedOn_PercentageOfSales = new Control("CalculateCostOfGoodsSoldBasedOn_PercentageOfSales", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICECATLG_CI_']/ancestor::form[1]/descendant::*[@id='S_COGS_CALC_CD' and @value='P']");
			CPCommon.AssertEqual(true,PDMPPCAT_CalculateCostOfGoodsSoldBasedOn_PercentageOfSales.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on CostInformationForm...", Logger.MessageType.INF);
			Control PDMPPCAT_CostInformationForm = new Control("CostInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODCSTSCH_CISUB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMPPCAT_CostInformationForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExist on CostInformationTable...", Logger.MessageType.INF);
			Control PDMPPCAT_CostInformationTable = new Control("CostInformationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODCSTSCH_CISUB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPPCAT_CostInformationTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
							CPCommon.WaitControlDisplayed(PDMPPCAT_CalculateCostOfGoodsSoldBasedOnForm);
formBttn = PDMPPCAT_CalculateCostOfGoodsSoldBasedOnForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Warranty Info");


												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming Click on WarrantyInformationLink...", Logger.MessageType.INF);
			Control PDMPPCAT_WarrantyInformationLink = new Control("WarrantyInformationLink", "ID", "lnk_1003302_PDMPPCAT_PRODPRICECATLG_HDR");
			CPCommon.WaitControlDisplayed(PDMPPCAT_WarrantyInformationLink);
PDMPPCAT_WarrantyInformationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on WarrantyInformationUpperForm...", Logger.MessageType.INF);
			Control PDMPPCAT_WarrantyInformationUpperForm = new Control("WarrantyInformationUpperForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICECATLG_WI_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMPPCAT_WarrantyInformationUpperForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on WarrantyInformationUpperForm_CalculateWarrantyCostsBasedOn_EnterPercentageOfSalesToCalculateWarrantyCosts...", Logger.MessageType.INF);
			Control PDMPPCAT_WarrantyInformationUpperForm_CalculateWarrantyCostsBasedOn_EnterPercentageOfSalesToCalculateWarrantyCosts = new Control("WarrantyInformationUpperForm_CalculateWarrantyCostsBasedOn_EnterPercentageOfSalesToCalculateWarrantyCosts", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODPRICECATLG_WI_']/ancestor::form[1]/descendant::*[@id='WARR_PCT_RT']");
			CPCommon.AssertEqual(true,PDMPPCAT_WarrantyInformationUpperForm_CalculateWarrantyCostsBasedOn_EnterPercentageOfSalesToCalculateWarrantyCosts.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExists on WarrantyInformationLowerForm...", Logger.MessageType.INF);
			Control PDMPPCAT_WarrantyInformationLowerForm = new Control("WarrantyInformationLowerForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODWARRCSTSCH_WISUB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMPPCAT_WarrantyInformationLowerForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPPCAT] Perfoming VerifyExist on WarrantyInformationLowerFormTable...", Logger.MessageType.INF);
			Control PDMPPCAT_WarrantyInformationLowerFormTable = new Control("WarrantyInformationLowerFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPPCAT_PRODWARRCSTSCH_WISUB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPPCAT_WarrantyInformationLowerFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPPCAT";
							CPCommon.AssertEqual(true,PDMPPCAT_WarrantyInformationUpperForm.Exists());

												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDMPPCAT";
							CPCommon.WaitControlDisplayed(PDMPPCAT_MainForm);
formBttn = PDMPPCAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

