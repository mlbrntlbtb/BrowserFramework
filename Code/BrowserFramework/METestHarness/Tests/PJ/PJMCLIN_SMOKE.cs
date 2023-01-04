 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMCLIN_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Unit Pricing Information", "xpath","//div[@class='navItem'][.='Unit Pricing Information']").Click();
new Control("Manage CLIN Information", "xpath","//div[@class='navItem'][.='Manage CLIN Information']").Click();


											Driver.SessionLogger.WriteLine("QUERY");


												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMCLIN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMCLIN_MainForm);
IWebElement formBttn = PJMCLIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJMCLIN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJMCLIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMCLIN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCLIN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.WaitControlDisplayed(PJMCLIN_MainForm);
formBttn = PJMCLIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCLIN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCLIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.AssertEqual(true,PJMCLIN_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control PJMCLIN_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMCLIN_Identification_Project.Exists());

											Driver.SessionLogger.WriteLine("CLIN FORM");


												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PJMCLIN_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJCLIN_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCLIN_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control PJMCLIN_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJCLIN_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMCLIN_ChildForm);
formBttn = PJMCLIN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCLIN_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCLIN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.AssertEqual(true,PJMCLIN_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on CLINDetails_CLIN...", Logger.MessageType.INF);
			Control PJMCLIN_CLINDetails_CLIN = new Control("CLINDetails_CLIN", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJCLIN_CHILD_']/ancestor::form[1]/descendant::*[@id='CLIN_ID']");
			CPCommon.AssertEqual(true,PJMCLIN_CLINDetails_CLIN.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on CLINDetails_ItemLinkageLink...", Logger.MessageType.INF);
			Control PJMCLIN_CLINDetails_ItemLinkageLink = new Control("CLINDetails_ItemLinkageLink", "ID", "lnk_1004317_PJM_PROJCLIN_CHILD");
			CPCommon.AssertEqual(true,PJMCLIN_CLINDetails_ItemLinkageLink.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.WaitControlDisplayed(PJMCLIN_CLINDetails_ItemLinkageLink);
PJMCLIN_CLINDetails_ItemLinkageLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExist on ItemLinkage_Table...", Logger.MessageType.INF);
			Control PJMCLIN_ItemLinkage_Table = new Control("ItemLinkage_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJCLINPROD_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCLIN_ItemLinkage_Table.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on ItemLinkageForm...", Logger.MessageType.INF);
			Control PJMCLIN_ItemLinkageForm = new Control("ItemLinkageForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJCLINPROD_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMCLIN_ItemLinkageForm.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.WaitControlDisplayed(PJMCLIN_ItemLinkageForm);
formBttn = PJMCLIN_ItemLinkageForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCLIN_ItemLinkageForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCLIN_ItemLinkageForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on ItemLinkage_Item...", Logger.MessageType.INF);
			Control PJMCLIN_ItemLinkage_Item = new Control("ItemLinkage_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJCLINPROD_CHILD_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PJMCLIN_ItemLinkage_Item.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.WaitControlDisplayed(PJMCLIN_ItemLinkageForm);
formBttn = PJMCLIN_ItemLinkageForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on CLINDetails_ProjectUnitPricingLink...", Logger.MessageType.INF);
			Control PJMCLIN_CLINDetails_ProjectUnitPricingLink = new Control("CLINDetails_ProjectUnitPricingLink", "ID", "lnk_3283_PJM_PROJCLIN_CHILD");
			CPCommon.AssertEqual(true,PJMCLIN_CLINDetails_ProjectUnitPricingLink.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.WaitControlDisplayed(PJMCLIN_CLINDetails_ProjectUnitPricingLink);
PJMCLIN_CLINDetails_ProjectUnitPricingLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on ItemDetailsForm...", Logger.MessageType.INF);
			Control PJMCLIN_ItemDetailsForm = new Control("ItemDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASIC_PROJCLINPROD_ITPRCHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMCLIN_ItemDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on ItemDetails_Item...", Logger.MessageType.INF);
			Control PJMCLIN_ItemDetails_Item = new Control("ItemDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASIC_PROJCLINPROD_ITPRCHDR_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PJMCLIN_ItemDetails_Item.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExist on PricingDetailsTable...", Logger.MessageType.INF);
			Control PJMCLIN_PricingDetailsTable = new Control("PricingDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASIC_CLINPRICESCH_ITPRC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCLIN_PricingDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming ClickButton on PricingDetailsForm...", Logger.MessageType.INF);
			Control PJMCLIN_PricingDetailsForm = new Control("PricingDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASIC_CLINPRICESCH_ITPRC_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMCLIN_PricingDetailsForm);
formBttn = PJMCLIN_PricingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCLIN_PricingDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCLIN_PricingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.AssertEqual(true,PJMCLIN_PricingDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMCLIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCLIN] Perfoming VerifyExists on PricingDetails_FromQuantity...", Logger.MessageType.INF);
			Control PJMCLIN_PricingDetails_FromQuantity = new Control("PricingDetails_FromQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASIC_CLINPRICESCH_ITPRC_']/ancestor::form[1]/descendant::*[@id='FROM_QTY']");
			CPCommon.AssertEqual(true,PJMCLIN_PricingDetails_FromQuantity.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMCLIN";
							CPCommon.WaitControlDisplayed(PJMCLIN_MainForm);
formBttn = PJMCLIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

