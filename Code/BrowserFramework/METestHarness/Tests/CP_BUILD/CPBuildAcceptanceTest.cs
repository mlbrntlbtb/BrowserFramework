 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CPBuildAcceptanceTest : TestScript
    {
        public override bool TestExecute(out string ErrorMessage, string TestEnvironment)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
                /* Log-in */
                CPCommon.Login(TestEnvironment, out ErrorMessage);

                /* 1 - Comment */
                Driver.SessionLogger.WriteLine("Process progress");

                /* 2 - Navigate to Re build Global Settings */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
                    Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed)
                        new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Admin", "xpath", "//div[@class='busItem'][.='Admin']").Click();
                    new Control("System Administration", "xpath", "//div[@class='deptItem'][.='System Administration']").Click();
                    new Control("System Administration Utilities", "xpath", "//div[@class='navItem'][.='System Administration Utilities']").Click();
                    new Control("Rebuild Global Settings", "xpath", "//div[@class='navItem'][.='Rebuild Global Settings']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error navigating to Rebuild Global Settings app ", ex.Message));
                }

                /* 3 - Click Re-load Settings */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Action Menu')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Action Menu~Reload Settings.");
                    tlbrBtn.Click();
                    CP7Main_MainToolBar.mElement.FindElements(By.XPath("//*[@class = 'tlbrDDItem' and contains(text(),'Reload Settings')]")).FirstOrDefault().Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Re-load Settings. ", ex.Message));
                }

                /* ADDTIONAL STEP - Need to wait */
                try
                {
                    Driver.SessionLogger.WriteLine("[ProcessProgress] Waiting for process to finish...", Logger.MessageType.INF);
                    CPCommon.WaitProcessProgressFinished(6000);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error waiting for process to finish. ", ex.Message));
                }

                /* 4 - Verify ProcessProgress Header */
                try
                {
                    CPCommon.CurrentComponent = "ProcessProgress";
                    CPCommon.WaitLoadingFinished(true);
                    Driver.SessionLogger.WriteLine("[ProcessProgress] Verifying Header...", Logger.MessageType.INF);
                    Control ProcessProgress_Header = new Control("Header", "ID", "progMtrSysTxt");
                    ProcessProgress_Header.FindElement();
                    CPCommon.AssertEqual(CPCommon.ReplaceCarriageReturn("Process complete.", ". "), CPCommon.ReplaceCarriageReturn(ProcessProgress_Header.mElement.Text.Trim(), ". "));
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying Process Progress header ", ex.Message));
                }

                /* 5 - Click ProcessProgress OK */
                try
                {
                    CPCommon.CurrentComponent = "ProcessProgress";
                    CPCommon.WaitLoadingFinished(true);
                    Driver.SessionLogger.WriteLine("[ProcessProgress] Perfoming Click on OK...", Logger.MessageType.INF);
                    Control ProcessProgress_OK = new Control("OK", "ID", "progMtrBtn");
                    CPCommon.WaitControlDisplayed(ProcessProgress_OK);
                    if (ProcessProgress_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        ProcessProgress_OK.Click(5, 5);
                    else
                        ProcessProgress_OK.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Process Progress OK button ", ex.Message));
                }

                /* 6 - Close Main form */
                try
                {
                    CPCommon.CurrentComponent = "SYPSTNG";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYPSTNG] Perfoming Close on MainForm...", Logger.MessageType.INF);
                    Control SYPSTNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(SYPSTNG_MainForm);
                    IWebElement formBttn = SYPSTNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x => x.Displayed).FirstOrDefault();
                    if (formBttn != null)
                        formBttn.Click();
                    else
                        throw new Exception("Close Button not found ");
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error closing SYPSTNG main form ", ex.Message));
                }

                try
                {
                    /* 7 - Click OK on Message Box */
                    CPCommon.CurrentComponent = "Dialog";
                    CPCommon.WaitLoadingFinished(true);
                    CPCommon.ClickOkDialogWithMessage("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking message box OK button ", ex.Message));
                }

                /* 8 - Comment */
                Driver.SessionLogger.WriteLine("Save");

                /* 9 - Navigate to Manage Users */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed)
                        new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Admin", "xpath", "//div[@class='busItem'][.='Admin']").Click();
                    new Control("Security", "xpath", "//div[@class='deptItem'][.='Security']").Click();
                    new Control("System Security", "xpath", "//div[@class='navItem'][.='System Security']").Click();
                    new Control("Manage Users", "xpath", "//div[@class='navItem'][.='Manage Users']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error navigating to Manage Users app ", ex.Message));
                }

                /* 10 - Set UserID */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Identification_UserID...", Logger.MessageType.INF);
                    Control SYMUSR_Identification_UserID = new Control("Identification_UserID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEC_OBJ_ID']");
                    SYMUSR_Identification_UserID.Click();
                    SYMUSR_Identification_UserID.SendKeys("CPBUILDACCEPTANCE_1", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Identification_UserID.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting UserId in SYMUSR app ", ex.Message));
                }

                /* 11 - Set UserName */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Identification_UserName...", Logger.MessageType.INF);
                    Control SYMUSR_Identification_UserName = new Control("Identification_UserName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='U___NAME']");
                    SYMUSR_Identification_UserName.Click();
                    SYMUSR_Identification_UserName.SendKeys("Asaka C.P.A., Leslie k", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Identification_UserName.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting UserName in SYMUSR app ", ex.Message));
                }

                /* 12 - Set DefaultCompany */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Identification_Information_PreferencesUserCanChange_DefaultCompany...", Logger.MessageType.INF);
                    Control SYMUSR_Identification_Information_PreferencesUserCanChange_DefaultCompany = new Control("Identification_Information_PreferencesUserCanChange_DefaultCompany", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='U___DFLT_COMPANY_ID']");
                    SYMUSR_Identification_Information_PreferencesUserCanChange_DefaultCompany.Click();
                    SYMUSR_Identification_Information_PreferencesUserCanChange_DefaultCompany.SendKeys("1", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Identification_Information_PreferencesUserCanChange_DefaultCompany.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Deafult Company in SYMUSR app ", ex.Message));
                }

                /* 13 - Click New in Company Access Form */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming ClickButtonIfExists on CompanyAccessForm...", Logger.MessageType.INF);
                    Control SYMUSR_CompanyAccessForm = new Control("CompanyAccessForm", "xpath", "//div[translate(@id,'0123456789','')='pr__SYMUSR_WUSERCOMPANY_']/ancestor::form[1]");
                    CPCommon.WaitControlDisplayed(SYMUSR_CompanyAccessForm);
                    IWebElement formBttn = SYMUSR_CompanyAccessForm.mElement.FindElements(By.CssSelector("*[title*='New']")).Count <= 0 ? SYMUSR_CompanyAccessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'New')]")).FirstOrDefault()
                        : SYMUSR_CompanyAccessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'New')]")).FirstOrDefault();
                    if (formBttn != null && formBttn.Displayed)
                    {
                        new Control("FormButton", formBttn).MouseOver(); formBttn.Click();
                        Driver.SessionLogger.WriteLine("Button [New] found and clicked.", Logger.MessageType.INF);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking New button in SYMUSR Company Access form ", ex.Message));
                }

                /* 14 - Click Form button */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    Control SYMUSR_CompanyAccessForm = new Control("CompanyAccessForm", "xpath", "//div[translate(@id,'0123456789','')='pr__SYMUSR_WUSERCOMPANY_']/ancestor::form[1]");
                    CPCommon.WaitControlDisplayed(SYMUSR_CompanyAccessForm);
                    IWebElement formBttn = SYMUSR_CompanyAccessForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? SYMUSR_CompanyAccessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault()
                        : SYMUSR_CompanyAccessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
                    if (formBttn != null && formBttn.Displayed)
                    {
                        new Control("FormButton", formBttn).MouseOver(); formBttn.Click();
                        Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Form button in SYMUSR Company Access Form ", ex.Message));
                }

                /* 15 - Set Company ID */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Company_CompanyID...", Logger.MessageType.INF);
                    Control SYMUSR_Company_CompanyID = new Control("Company_CompanyID", "xpath", "//div[translate(@id,'0123456789','')='pr__SYMUSR_WUSERCOMPANY_']/ancestor::form[1]/descendant::*[@id='COMPANY_ID']");
                    SYMUSR_Company_CompanyID.Click();
                    SYMUSR_Company_CompanyID.SendKeys("1", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Company_CompanyID.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Company ID in SYMUSR app ", ex.Message));
                }

                /* 16 - Set Default Tax Entity ID */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Company_DefaultTaxableEntityID...", Logger.MessageType.INF);
                    Control SYMUSR_Company_DefaultTaxableEntityID = new Control("Company_DefaultTaxableEntityID", "xpath", "//div[translate(@id,'0123456789','')='pr__SYMUSR_WUSERCOMPANY_']/ancestor::form[1]/descendant::*[@id='UC___DFLT_TAXBLE_ENT_ID']");
                    SYMUSR_Company_DefaultTaxableEntityID.Click();
                    SYMUSR_Company_DefaultTaxableEntityID.SendKeys("1", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Company_DefaultTaxableEntityID.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Default Taxable Entity ID in SYMUSER app ", ex.Message));
                }

                /* 17 - Set Org Sec Grp ID */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Company_OrgSecurityGroupID...", Logger.MessageType.INF);
                    Control SYMUSR_Company_OrgSecurityGroupID = new Control("Company_OrgSecurityGroupID", "xpath", "//div[translate(@id,'0123456789','')='pr__SYMUSR_WUSERCOMPANY_']/ancestor::form[1]/descendant::*[@id='UC___ORG_SEC_GRP_CD']");
                    SYMUSR_Company_OrgSecurityGroupID.Click();
                    SYMUSR_Company_OrgSecurityGroupID.SendKeys("ALL", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Company_OrgSecurityGroupID.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Org Sec ID in SYMUSER app ", ex.Message));
                }

                /* 18 - Select Authentication tab */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Select on IdentificationTab...", Logger.MessageType.INF);
                    Control SYMUSR_IdentificationTab = new Control("IdentificationTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
                    CPCommon.WaitControlDisplayed(SYMUSR_IdentificationTab);
                    IWebElement mTab = SYMUSR_IdentificationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Authentication").FirstOrDefault();
                    if (Driver.BrowserType.ToLower() != "ie")
                        new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
                    else
                        new Control("Tab", mTab).ScrollTab(mTab);
                    mTab.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Authentication tab in SYMUSER app ", ex.Message));
                }

                /* 19 - Set CP password */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Identification_Authentication_AuthenticationSettings_CostpointPassword...", Logger.MessageType.INF);
                    Control SYMUSR_Identification_Authentication_AuthenticationSettings_CostpointPassword = new Control("Identification_Authentication_AuthenticationSettings_CostpointPassword", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NEW_PASSWORD']");
                    SYMUSR_Identification_Authentication_AuthenticationSettings_CostpointPassword.Click();
                    SYMUSR_Identification_Authentication_AuthenticationSettings_CostpointPassword.SendKeys("PASSWORD", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Identification_Authentication_AuthenticationSettings_CostpointPassword.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting CP password in SYMUSER app ", ex.Message));
                }

                /* 20 - Set CP Verify password */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Set on Identification_Authentication_AuthenticationSettings_VerifyPassword...", Logger.MessageType.INF);
                    Control SYMUSR_Identification_Authentication_AuthenticationSettings_VerifyPassword = new Control("Identification_Authentication_AuthenticationSettings_VerifyPassword", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VERIFY_PASSWORD']");
                    SYMUSR_Identification_Authentication_AuthenticationSettings_VerifyPassword.Click();
                    SYMUSR_Identification_Authentication_AuthenticationSettings_VerifyPassword.SendKeys("PASSWORD", true);
                    CPCommon.WaitLoadingFinished();
                    SYMUSR_Identification_Authentication_AuthenticationSettings_VerifyPassword.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                    Thread.Sleep(3);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Verify Password in SYMUSER app ", ex.Message));
                }

                /* 21 - Click Save and Continue */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save & Continue (F6)')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Save & Continue (F6).");
                    Thread.Sleep(800);
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Save and Continue ", ex.Message));
                }

                /* 22 - Verify Save Message */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished(true);
                    IList<IWebElement> msgElements = Driver.Instance.FindElements(By.XPath("//span[contains(@class,'msgTextHdr')]/following-sibling::span[contains(@class,'msgText')]"));
                    CPCommon.AssertEqual(true, msgElements.ToList().Select(x => new Control("x", x).GetValue().Trim())
                        .Any(x => x == "Record modifications successfully completed."));
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying save message ", ex.Message));
                }

                /* 23 - Click Save and Continue */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save & Continue')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Save & Continue.");
                    Thread.Sleep(800);
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Save and Continue ", ex.Message));
                }

                /* 24 - Verify Save Message */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished(true);
                    IReadOnlyCollection<IWebElement> msgElements = Driver.Instance.FindElements(By.XPath("//span[contains(@class,'msgTextHdr')]/following-sibling::span[contains(@class,'msgText')]"));
                    CPCommon.AssertEqual(true, msgElements.ToList().Select(x => new Control("x", x).GetValue().Trim())
                        .Any(x => x == "Record modifications successfully completed."));
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying save message ", ex.Message));
                }

                /* 25 - Comment */
                Driver.SessionLogger.WriteLine("Print");

                /* 26 - Click Horizontal Layout */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Print Menu')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Print Menu~Current Record Information - Horizontal Layout.");
                    tlbrBtn.Click();
                    CP7Main_MainToolBar.mElement.FindElements(By.XPath("//*[@class = 'tlbrDDItem' and contains(text(),'Current Record Information - Horizontal Layout')]")).FirstOrDefault().Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Horizontal Layout ", ex.Message));
                }

                /* 27 - Wait process finished */
                try
                {
                    Driver.SessionLogger.WriteLine("[ProcessProgress] Waiting for process to finish...", Logger.MessageType.INF);
                    CPCommon.WaitProcessProgressFinished(6000);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error waiting for process to finish ", ex.Message));
                }

                /* 28 - Verify process progress header */
                try
                {
                    CPCommon.CurrentComponent = "ProcessProgress";
                    CPCommon.WaitLoadingFinished(true);
                    Driver.SessionLogger.WriteLine("[ProcessProgress] Verifying Header...", Logger.MessageType.INF);
                    Control ProcessProgress_Header1 = new Control("Header", "ID", "progMtrSysTxt");
                    ProcessProgress_Header1.FindElement();
                    CPCommon.AssertEqual(CPCommon.ReplaceCarriageReturn("Process complete.", ". "), CPCommon.ReplaceCarriageReturn(ProcessProgress_Header1.mElement.Text.Trim(), ". "));
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying process progress header ", ex.Message));
                }

                /* 29 - Click process progress OK */
                try
                {
                    CPCommon.CurrentComponent = "ProcessProgress";
                    Control ProcessProgress_OK = new Control("OK", "ID", "progMtrBtn");
                    CPCommon.WaitControlDisplayed(ProcessProgress_OK);
                    if (ProcessProgress_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
                        ProcessProgress_OK.Click(5, 5);
                    else
                        ProcessProgress_OK.Click(4.5);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Process Progress OK button ", ex.Message));
                }

                /* 30 - Download file */
                try
                {
                    Driver.SessionLogger.WriteLine("[Dialog] File Download...", Logger.MessageType.INF);
                    MSUIAutomationHelper.FileDownload(Driver.Instance.Title, "symusr.pdf", 6000);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error downloading file ", ex.Message));
                }

                /* 31 - Comment */
                Driver.SessionLogger.WriteLine("Delete");

                /* 32 - Click Delete in Manage Users mainform */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
                    Control SYMUSR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(SYMUSR_MainForm);
                    IWebElement formBttn = SYMUSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? SYMUSR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
                        SYMUSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
                    if (formBttn != null)
                    {
                        new Control("FormButton", formBttn).MouseOver();
                        formBttn.Click();
                    }
                    else
                        throw new Exception(" Delete not found ");
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Delete in SYMUSR app ", ex.Message));
                }

                /* 33 - Click Save */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save (F5)')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Save (F5).");
                    Thread.Sleep(800);
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Save ", ex.Message));
                }

                /* 34 - Verify Save message */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished(true);
                    IReadOnlyCollection<IWebElement> msgElements = Driver.Instance.FindElements(By.XPath("//span[contains(@class,'msgTextHdr')]/following-sibling::span[contains(@class,'msgText')]"));
                    CPCommon.AssertEqual(true, msgElements.ToList().Select(x => new Control("x", x).GetValue().Trim())
                        .Any(x => x == "Record modifications successfully completed."));
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying save message ", ex.Message));
                }

                /* 35 - Close main form */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    Control SYMUSR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(SYMUSR_MainForm);
                    IWebElement formBttn = SYMUSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x => x.Displayed).FirstOrDefault();
                    if (formBttn != null)
                        formBttn.Click();
                    else throw
                        new Exception("Close Button not found ");
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error closing SYMUSR main form ", ex.Message));
                }

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

