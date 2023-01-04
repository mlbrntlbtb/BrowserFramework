using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using OpenQA.Selenium;

namespace METestHarness.Tests.StormWeb.Employees
{
    public class HP_EMP_CreateSearchAndDeleteEmployee : TestScript
    {
        public override bool TestExecute(out string ErrorMessage){

            bool ret = true;
            ErrorMessage = string.Empty;
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            try
            {
                //Environment Login
                //StormCommon.Login("Mel_2.0", out ErrorMessage);
                Driver.Instance.Url = "http://makapt618vs/DeltekPS/app/#!";

                //Set UserID
                Driver.SessionLogger.WriteLine("Set UserID");
                Control User = new Control("TextBox", "XPATH", "//input[@id='userID']");
                User.SendKeys("ADMIN");

                //Set Password
                Driver.SessionLogger.WriteLine("Set Password");
                Control Pass = new Control("TextBox", "XPATH", "//input[@id='password']");
                Pass.SendKeys("");

                //Set Database
                Driver.SessionLogger.WriteLine("Set Database");
                Control DB = new Control("ComboBox", "XPATH", "//input[@name='dbDdwn']");
                DB.SendKeys("AutoDB_FWDev_03 (MAKAPT676VS)");

                //Click Database Item
                Driver.SessionLogger.WriteLine("Click Database Item");
                Control DBItem = new Control("ComboBox", "XPATH", "//div[@class='ddwnTableDiv']//div[@class='string truncate_text'][contains(text(),'AutoDB_FWDev_03')]");
                DBItem.Click();

                //Click Login
                Driver.SessionLogger.WriteLine("Click Login");
                Control Login = new Control("Button", "XPATH", "//button[@data-id='login']");
                Login.Click();
                StormCommon.WaitScreenGetsReady();

                StormCommon.WaitScreenGetsReady();
                //Navigate to Employee
                Driver.SessionLogger.WriteLine("Navigate to Employee");
                Control Nav = new Control("SideBar", "XPATH", "//div[@class='app-list']//*[@data-app-id='Employee']");
                Nav.Click();
                StormCommon.WaitScreenGetsReady();

                //Navigate to Employees
                Driver.SessionLogger.WriteLine("Navigate to Employees");
                Nav = new Control("SideBar", "XPATH", "//div[@class='app-list']//*[@data-app-id='Employees']");
                Nav.Click();
                StormCommon.WaitScreenGetsReady();

                //Click Search Filter
                Driver.SessionLogger.WriteLine("Click Search Filter");
                Control Search = new Control("ComboBox", "XPATH", "//*[@title='View Filters']");
                Search.Click();

                //Click All from Search Filter
                Driver.SessionLogger.WriteLine("Click All from Search Filter");
                Search = new Control("ComboBox", "XPATH", "//ul[@id='employeeFilterDdwn']//span[@class='popupLabel'][contains(text(),'All')]");
                Search.Click();

                //Click New Employee Link
                Driver.SessionLogger.WriteLine("Click New Employee Link");
                Control Link = new Control("Link", "XPATH", "//div[@class='section-add safari-clickable']//*[@class='linked-text res-desktop']");
                Link.Click();

                //Set Value to FirstName
                Driver.SessionLogger.WriteLine("Set Value to FirstName");
                Control TextBox = new Control("TextBox", "XPATH", "//*[@data-id='EM.FullName_FirstName']//input");
                TextBox.SendKeys("Mel");

                //Set Value to MiddleName
                Driver.SessionLogger.WriteLine("Set Value to MiddleName");
                TextBox = new Control("TextBox", "XPATH", "//*[@data-id='EM.FullName_MiddleName']//input");
                TextBox.SendKeys("Bourne");

                //Set Value to LastName
                Driver.SessionLogger.WriteLine("Set Value to LastName");
                TextBox = new Control("TextBox", "XPATH", "//*[@data-id='EM.FullName_LastName']//input");
                TextBox.SendKeys("Legacy");

                //Set Value to PreferredName
                Driver.SessionLogger.WriteLine("Set Value to PreferredName");
                TextBox = new Control("TextBox", "XPATH", "//*[@data-id='EM.FullName_PreferredName']//input");
                TextBox.SendKeys("Damien");

                //Set Value to EmployeeNumber
                Driver.SessionLogger.WriteLine("Set Value to EmployeeNumber");
                TextBox = new Control("TextBox", "XPATH", "//*[@data-id='EM.Employee']//input");
                TextBox.SendKeys("12345");

                //Set Value to Organization
                Driver.SessionLogger.WriteLine("Set Value to Organization");
                TextBox = new Control("ComboBox", "XPATH", "//*[@data-id='EM.Org']//input");
                TextBox.SendKeys("AA:BO:AA:BO");

                //Select Accounting from Tablist
                Driver.SessionLogger.WriteLine("Select Accounting from Tablist");
                Control Tab = new Control("Tab", "XPATH", "//*[@class='ngcrm-tabs']//*[@data-tab-id='accounting']");
                Tab.Click();

                //Set Value to Labor Type
                Driver.SessionLogger.WriteLine("Set Value to Labor Type");
                Control Labor = new Control("ComboBox", "XPATH", "//*[@data-id='EM.Type']//input");
                Labor.SendKeys("Employee");

                //Click Labor Type Item
                Driver.SessionLogger.WriteLine("Click Labor Type Item");
                Labor = new Control("ComboBox", "XPATH", "//div[@class='search-info']/div[@class='search-name'][text()='Employee']");
                Labor.Click();

                //Select Time & Expense from Tablist
                Driver.SessionLogger.WriteLine("Select Time & Expense from Tablist");
                Control Tab2 = new Control("Tab", "XPATH", "//*[@class='ngcrm-tabs']//*[@data-tab-id='timeAndExpenseTab']");
                Tab2.FindElement();
                Thread.Sleep(1000);
                Tab2.Click();

                //Set Value to Labor Code Level 1
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 1");
                Control LaborCode = new Control("ComboBox", "XPATH", "//*[@data-id='EM.DefaultLC1']//input");
                LaborCode.SendKeys("p");

                //Click Labor Code Level 1 Item
                Driver.SessionLogger.WriteLine("Click Labor Code Level 1 Item");
                LaborCode = new Control("ComboBox", "XPATH", "//div[@class='search-info']/div[@class='search-name'][text()='01/Predesign']");
                StormCommon.WaitControlDisplayed(LaborCode);
                LaborCode.Click();

                //Set Value to Labor Code Level 2
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 2");
                LaborCode = new Control("ComboBox", "XPATH", "//*[@data-id='EM.DefaultLC2']//input");
                LaborCode.SendKeys("g");

                //Click Labor Code Level 2 Item
                Driver.SessionLogger.WriteLine("Click Labor Code Level 2 Item");
                LaborCode = new Control("ComboBox", "XPATH", "//div[@class='search-info']/div[@class='search-name'][text()='000/General-Overhead']");
                StormCommon.WaitControlDisplayed(LaborCode);
                LaborCode.Click();

                //Set Value to Labor Code Level 3
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 3");
                LaborCode = new Control("ComboBox", "XPATH", "//*[@data-id='EM.DefaultLC3']//input");
                LaborCode.SendKeys("s");

                //Click Labor Code Level 3 Item
                Driver.SessionLogger.WriteLine("Click Labor Code Level 3 Item");
                LaborCode = new Control("ComboBox", "XPATH", "//div[@class='search-info']/div[@class='search-name'][text()='02/Sr. Architect']");
                StormCommon.WaitControlDisplayed(LaborCode);
                LaborCode.Click();

                //Set Value to Labor Code Level 5
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 5");
                LaborCode = new Control("ComboBox", "XPATH", "//*[@data-id='EM.DefaultLC5']//input");
                LaborCode.SendKeys("g");

                //Set Value to Labor Code Level 4
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 4");
                LaborCode = new Control("ComboBox", "XPATH", "//*[@data-id='EM.DefaultLC4']//input");
                LaborCode.SendKeys("g");

                //Click Previous Button for Set of Tabs
                Driver.SessionLogger.WriteLine("Click Previous Button for Set of Tabs");
                Control Prev = new Control("ComboBox", "XPATH", "//*[contains(@class,'nav-scroll-left-btn')]");
                Prev.Click();

                //Select Overview from Tablist
                Driver.SessionLogger.WriteLine("Select Overview from Tablist");
                Control Tab3 = new Control("Tab", "XPATH", "//*[@class='ngcrm-tabs']//*[@data-tab-id='overview']");
                Tab3.FindElement();
                Tab3.ScrollIntoViewUsingJavaScript();
                StormCommon.WaitControlDisplayed(Tab3);
                Thread.Sleep(1000);
                Tab3.Click();

                //Click Save
                Driver.SessionLogger.WriteLine("Click Save");
                Control Save = new Control("ComboBox", "XPATH", "//button[contains(@class,'save-btn')]");
                Save.Click();
                StormCommon.WaitScreenGetsReady();

                //Set Created Employee Record
                Driver.SessionLogger.WriteLine("Set Created Employee Record");
                Control Set = new Control("ComboBox", "XPATH", "//*[contains(@class,'section-search')]//input[@class='dropdown-field']");
                StormCommon.WaitScreenGetsReady();
                Set.SendKeys("Dam");

                //Select Item From Search
                Driver.SessionLogger.WriteLine("Select Item From Search");
                Set = new Control("ComboBox", "XPATH", "//ul[@class='results']/div[@class='result-items']/li[3]");
                StormCommon.WaitControlDisplayed(Set);
                Set.Click();

                //Verify Employee Name Title
                Driver.SessionLogger.WriteLine("Verify Employee Name Title");
                Control Emp = new Control("Label", "XPATH", "//*[@data-id='EM.TitleName']//*[contains(@class,'text-field')]");
                if (Emp.GetValue().ToString() == "Damien Legacy")
                    Driver.SessionLogger.WriteLine("Label: [" + Emp.GetValue().ToString() + "] is equal to [Damien Legacy]");
                else
                    throw new Exception("Label: [" + Emp.GetValue().ToString() + "] is not equal to [Damien Legacy]");

                //Verify FullName QuickEdit
                Driver.SessionLogger.WriteLine("Verify FullName QuickEdit");
                Emp = new Control("QuickEdit", "XPATH", "//*[@data-id='EM.FullName']//*[contains(@class,'name-field')]");
                if (Emp.GetValue().ToString() == "Mel (Damien) Bourne Legacy")
                    Driver.SessionLogger.WriteLine("QuickEdit: [" + Emp.GetValue().ToString() + "] is equal to [Mel (Damien) Bourne Legacy]");
                else
                    throw new Exception("QuickEdit: [" + Emp.GetValue().ToString() + "] is not equal to [Mel (Damien) Bourne Legacy]");

                //Verify Employee Number
                Driver.SessionLogger.WriteLine("Verify Employee Number");
                Emp = new Control("Label", "XPATH", "//*[@data-id='EM.Employee']//*[contains(@class,'text-field')]");
                if (Emp.GetValue().ToString() == "00012345")
                    Driver.SessionLogger.WriteLine("Label: [" + Emp.GetValue().ToString() + "] is equal to [00012345]");
                else
                    throw new Exception("Label: [" + Emp.GetValue().ToString() + "] is not equal to [00012345]");

                //Verify Organization
                Driver.SessionLogger.WriteLine("Verify Organization");
                Emp = new Control("QuickEdit", "XPATH", "//*[@data-id='EM.Org']//*[contains(@class,'dropdown-field')]");
                if (Emp.GetValue().ToString() == "AA:BO:AA:BO")
                    Driver.SessionLogger.WriteLine("QuickEdit: [" + Emp.GetValue().ToString() + "] is equal to [AA:BO:AA:BO]");
                else
                    throw new Exception("QuickEdit: [" + Emp.GetValue().ToString() + "] is not equal to [AA:BO:AA:BO]");

                //Set Different Employee Record
                Driver.SessionLogger.WriteLine("Set Different Employee Record");
                Set = new Control("ComboBox", "XPATH", "//*[contains(@class,'section-search')]//input[@class='dropdown-field']");
                StormCommon.WaitScreenGetsReady();
                Set.SendKeys(Keys.Control + "a");
                Set.SendKeys(Keys.Delete);
                Set.SendKeys("Bill");

                //Select Item From Search
                Driver.SessionLogger.WriteLine("Select Item From Search");
                Set = new Control("ComboBox", "XPATH", "//ul[@class='results']/div[@class='result-items']/li[2]");
                StormCommon.WaitControlDisplayed(Set);
                Set.Click();

                //Set Created Employee Record
                Driver.SessionLogger.WriteLine("Set Created Employee Record");
                Set = new Control("ComboBox", "XPATH", "//*[contains(@class,'section-search')]//input[@class='dropdown-field']");
                StormCommon.WaitScreenGetsReady();
                Set.SendKeys(Keys.Control + "a");
                Set.SendKeys(Keys.Delete);
                Set.SendKeys("Dam");

                //Select Item From Search
                Driver.SessionLogger.WriteLine("Select Item From Search");
                Set = new Control("ComboBox", "XPATH", "//ul[@class='results']/div[@class='result-items']/li[3]");
                StormCommon.WaitControlDisplayed(Set);
                Set.Click();
                StormCommon.WaitScreenGetsReady();
                
                //Click Other Actions Menu
                Driver.SessionLogger.WriteLine("Click Other Actions Menu");
                Control AcMenu = new Control("ComboBox", "XPATH", "//*[@action-bar-data='actionsMenu']");
                StormCommon.WaitControlDisplayed(AcMenu);
                AcMenu.Click();

                //Click Delete
                Driver.SessionLogger.WriteLine("Click Delete");
                Control Delete = new Control("ComboBox", "XPATH", "//ul[contains(@id,'actionsMenu')]//li[4]");
                StormCommon.WaitControlDisplayed(Delete);
                Delete.Click();
                StormCommon.WaitScreenGetsReady();

                //Click Delete Dialog
                Driver.SessionLogger.WriteLine("Click Delete Dialog");
                Delete = new Control("ComboBox", "XPATH", "//button[@data-id='yes']");
                StormCommon.WaitControlDisplayed(Delete);
                Delete.Click();
            }
            catch(Exception ex)
            {
                ret = false;
                ErrorMessage = ex.Message;
                throw new Exception(ex.Message);
            }

            return ret;
        }
    }
}
