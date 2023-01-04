 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests.AC
{
    public class BLMPCLOS_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing History", "xpath","//div[@class='navItem'][.='Billing History']").Click();
new Control("Manage Closed Progress Billing Detail", "xpath","//div[@class='navItem'][.='Manage Closed Progress Billing Detail']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMPCLOS_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMPCLOS_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control BLMPCLOS_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
IWebElement mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on BillingDetails_Customer...", Logger.MessageType.INF);
			Control BLMPCLOS_BillingDetails_Customer = new Control("BillingDetails_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLMPCLOS_BillingDetails_Customer.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
							CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Addresses").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on Adresses_ContractingOffice_Address...", Logger.MessageType.INF);
			Control BLMPCLOS_Adresses_ContractingOffice_Address = new Control("Adresses_ContractingOffice_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_ADDR_CD']");
			CPCommon.AssertEqual(true,BLMPCLOS_Adresses_ContractingOffice_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
							CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Remit To").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on RemitTo_ContractingOffice_Address...", Logger.MessageType.INF);
			Control BLMPCLOS_RemitTo_ContractingOffice_Address = new Control("RemitTo_ContractingOffice_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_RMT_ADDR_CD']");
			CPCommon.AssertEqual(true,BLMPCLOS_RemitTo_ContractingOffice_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
							CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Section 1").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on Section1_ContractInformation_3SmallBus...", Logger.MessageType.INF);
			Control BLMPCLOS_Section1_ContractInformation_3SmallBus = new Control("Section1_ContractInformation_3SmallBus", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DISPLAY_SM_BUS']");
			CPCommon.AssertEqual(true,BLMPCLOS_Section1_ContractInformation_3SmallBus.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
							CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line info 9-19").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on LineInfo919_Line9PAIDCOSTSELIGIBLEUNDERPROGRESSPAYMENTCLAUSE...", Logger.MessageType.INF);
			Control BLMPCLOS_LineInfo919_Line9PAIDCOSTSELIGIBLEUNDERPROGRESSPAYMENTCLAUSE = new Control("LineInfo919_Line9PAIDCOSTSELIGIBLEUNDERPROGRESSPAYMENTCLAUSE", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LINE_9_AMT']");
			CPCommon.AssertEqual(true,BLMPCLOS_LineInfo919_Line9PAIDCOSTSELIGIBLEUNDERPROGRESSPAYMENTCLAUSE.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
							CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line info 20-26").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on LineInfo2026_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED...", Logger.MessageType.INF);
			Control BLMPCLOS_LineInfo2026_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED = new Control("LineInfo2026_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LINE_20A_AMT']");
			CPCommon.AssertEqual(true,BLMPCLOS_LineInfo2026_Line20aCOSTSAPPLICABLETOITEMSDELIVEREDINVOICEDANDACCEPTED.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
							CPCommon.WaitControlDisplayed(BLMPCLOS_MainFormTab);
mTab = BLMPCLOS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line 13 Costs").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExists on Line13Costs_EstimatedTotalCostsLine12aPlus12b...", Logger.MessageType.INF);
			Control BLMPCLOS_Line13Costs_EstimatedTotalCostsLine12aPlus12b = new Control("Line13Costs_EstimatedTotalCostsLine12aPlus12b", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DISPLAY_ETC_AMT']");
			CPCommon.AssertEqual(true,BLMPCLOS_Line13Costs_EstimatedTotalCostsLine12aPlus12b.Exists());

											Driver.SessionLogger.WriteLine("Line 9 Detail");


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming Click on Line9DetailLink...", Logger.MessageType.INF);
			Control BLMPCLOS_Line9DetailLink = new Control("Line9DetailLink", "ID", "lnk_3504_BLMPCLOS_PRGPMTHDRHS_HDR");
			CPCommon.WaitControlDisplayed(BLMPCLOS_Line9DetailLink);
BLMPCLOS_Line9DetailLink.Click(1.5);


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExist on Line9DetailFormTable...", Logger.MessageType.INF);
			Control BLMPCLOS_Line9DetailFormTable = new Control("Line9DetailFormTable", "xpath", "//div[starts-with(@id,'pr__BLMPCLOS_PRGPMTDETLHS_LNDET09_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPCLOS_Line9DetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming Close on Line9DetailForm...", Logger.MessageType.INF);
			Control BLMPCLOS_Line9DetailForm = new Control("Line9DetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPCLOS_PRGPMTDETLHS_LNDET09_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMPCLOS_Line9DetailForm);
IWebElement formBttn = BLMPCLOS_Line9DetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Line 10 Detail");


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming Click on Line10DetailLink...", Logger.MessageType.INF);
			Control BLMPCLOS_Line10DetailLink = new Control("Line10DetailLink", "ID", "lnk_3505_BLMPCLOS_PRGPMTHDRHS_HDR");
			CPCommon.WaitControlDisplayed(BLMPCLOS_Line10DetailLink);
BLMPCLOS_Line10DetailLink.Click(1.5);


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming VerifyExist on Line10DetailFormTable1...", Logger.MessageType.INF);
			Control BLMPCLOS_Line10DetailFormTable1 = new Control("Line10DetailFormTable1", "xpath", "//div[starts-with(@id,'pr__BLMPCLOS_PRGPMTDETLHS_LNDET10_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPCLOS_Line10DetailFormTable1.Exists());

												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming Close on Line10DetailForm...", Logger.MessageType.INF);
			Control BLMPCLOS_Line10DetailForm = new Control("Line10DetailForm", "xpath", "//div[starts-with(@id,'pr__BLMPCLOS_PRGPMTDETLHS_LNDET10_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMPCLOS_Line10DetailForm);
formBttn = BLMPCLOS_Line10DetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLMPCLOS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPCLOS] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control BLMPCLOS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMPCLOS_MainForm);
formBttn = BLMPCLOS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

