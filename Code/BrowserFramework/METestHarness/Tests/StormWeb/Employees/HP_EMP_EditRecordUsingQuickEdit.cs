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
    public class HP_EMP_EditRecordUsingQuickEdit : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {

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
                //1. Navigate to Employee
                Driver.SessionLogger.WriteLine("Navigate to Employee");
                Control Nav = new Control("SideBar", "XPATH", "//div[@class='app-list']//*[@data-app-id='Employee']");
                Nav.Click();
                StormCommon.WaitScreenGetsReady();

                //2 Navigate to Employees
                Driver.SessionLogger.WriteLine("Navigate to Employees");
                Nav = new Control("SideBar", "XPATH", "//div[@class='app-list']//*[@data-app-id='Employees']");
                Nav.Click();
                StormCommon.WaitScreenGetsReady();

                //3 Click Search Filter
                Driver.SessionLogger.WriteLine("Click Search Filter");
                Control Search = new Control("ComboBox", "XPATH", "//*[@title='View Filters']");
                Search.Click();

                //4 Click All from Search Filter
                Driver.SessionLogger.WriteLine("Click All from Search Filter");
                Search = new Control("ComboBox", "XPATH", "//ul[@id='employeeFilterDdwn']//span[@class='popupLabel'][contains(text(),'All')]");
                Search.Click();

                //5 Set Created Employee Record
                Driver.SessionLogger.WriteLine("Set Employee Record to SearchBox");
                Control Set = new Control("ComboBox", "XPATH", "//*[contains(@class,'section-search')]//input[@class='dropdown-field']");
                StormCommon.WaitScreenGetsReady();
                Set.SendKeys("Bill");

                //6 Select Item From Search
                Driver.SessionLogger.WriteLine("Select Item From Search");
                Control Set2 = new Control("ComboBox", "XPATH", "//ul[@class='results']/div[@class='result-items']/li[2][contains(.,'Bill Apple, Jr.')]");
                StormCommon.WaitControlDisplayed(Set2);
                Set2.Click();

                //7 Verify Employee Name Title
                Driver.SessionLogger.WriteLine("Verify Employee Name Title");
                Control Emp = new Control("Label", "XPATH", "//*[@data-id='EM.TitleName']//*[contains(@class,'text-field')]");
                StormCommon.WaitScreenGetsReady();
                if (Emp.GetValue().ToString() == "Bill Apple, Jr.")
                    Driver.SessionLogger.WriteLine("Label: [" + Emp.GetValue().ToString() + "] is equal to [Bill Apple, Jr.]");
                else
                    throw new Exception("Label: [" + Emp.GetValue().ToString() + "] is not equal to [Bill Apple, Jr.]");

                //8 Select Time & Expense from Tablist
                Driver.SessionLogger.WriteLine("Select Time & Expense from Tablist");
                Control Tab = new Control("Tab", "XPATH", "//*[@class='ngcrm-tabs']//*[@data-tab-id='timeAndExpenseTab']");
                Tab.FindElement();
                Thread.Sleep(1000);
                Tab.Click();

                //9 Click Pencil Icon QuickEdit Labor Code Level 2
                Driver.SessionLogger.WriteLine("Click Pencil Icon QuickEdit Labor Code Level 2");
                Control Edit = new Control("QuickEdit", "XPATH", "//*[@data-id='EM.DefaultLC2']");
                Edit.FindElement();
                Edit.ScrollIntoViewUsingJavaScript();

                Control Pen1 = new Control("QuickEdit", "XPATH", "//*[@data-id='EM.DefaultLC2']//*[@title='Edit']");
                Pen1.MouseOver();
                Pen1.Click();

                //10 Set Value to Labor Code Level 2
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 2");
                Control LaborCode = new Control("QuickEdit", "XPATH", "//*[@data-id='EM.DefaultLC2']//input");
                LaborCode.SendKeys("g");

                //11 Click Labor Code Level 2 Item
                Driver.SessionLogger.WriteLine("Click Labor Code Level 2 Item");
                LaborCode = new Control("QuickEdit", "XPATH", "//div[@class='search-info']/div[@class='search-name'][text()='000/General-Overhead']");
                LaborCode.Click();

                //Click Pencil Icon QuickEdit Labor Code Level 3
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 3");
                Control Pen2 = new Control("QuickEdit", "XPATH", "//*[@data-id='EM.DefaultLC3']//*[@title='Edit']");
                Pen2.MouseOver();
                Pen2.Click();

                //Set Value to Labor Code Level 3
                Driver.SessionLogger.WriteLine("Set Value to Labor Code Level 3");
                Control LaborCode3 = new Control("ComboBox", "XPATH", "//*[@data-id='EM.DefaultLC3']//input");
                LaborCode3.SendKeys("g");

                //Click Labor Code Level 1 Item
                Driver.SessionLogger.WriteLine("Click Labor Code Level 3 Item");
                LaborCode3 = new Control("ComboBox", "XPATH", "//div[@class='search-info']/div[@class='search-name'][text()='00/General']");
                StormCommon.WaitControlDisplayed(LaborCode3);
                LaborCode3.Click();
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
