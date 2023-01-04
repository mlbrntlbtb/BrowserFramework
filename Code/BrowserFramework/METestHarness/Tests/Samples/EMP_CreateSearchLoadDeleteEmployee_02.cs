using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;

namespace METestHarness.Tests.Samples
{
    public class EMP_CreateSearchLoadDeleteEmployee_02 : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            
            try
            {
                StormCommon.Login("SAMPLE2.0", out ErrorMessage);

                Control menuToggle = new Control("menuToggle", "xpath", "//span[contains(@class, 'menu-toggle')]");
                StormCommon.WaitControlDisplayed(menuToggle);

                //STEP 1
                Driver.SessionLogger.WriteLine("Navigate to Employees", Logger.MessageType.INF);

                //STEP 3
                Control navEmployee = new Control("NavigateEmployee", "xpath", "//div[contains(@data-app-id, 'Employees')]");
                navEmployee.Click();
                Thread.Sleep(3000);

                //STEP 5
                Control lnkNewEmp = new Control("NewEmployee", "css", "div[title='New Employee']");
                StormCommon.WaitControlDisplayed(lnkNewEmp);
                Thread.Sleep(3000);
                lnkNewEmp.Click();

                Thread.Sleep(2000);
                //STEP 7
                Driver.SessionLogger.WriteLine("TAB: Overview", Logger.MessageType.INF);

                //STEP 8
                Control tabOverview = new Control("OverviewTab", "xpath", "//a[@data-tab-id='overview']");
                Thread.Sleep(1000);
                tabOverview.Click();

                //STEP 9
                Control txtBEmpNo = new Control("EmployeeNumber", "xpath", "//div[@data-id='EM.Employee']//input");
                txtBEmpNo.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                Thread.Sleep(3000);
                txtBEmpNo.SendKeys("AU00003");

                //STEP 10
                Control txtFName = new Control("FirstName", "xpath", "//div[@data-id='EM.FullName_FirstName']//input");
                txtFName.SendKeys("Cassandra");

                //STEP 11
                Control txtMName = new Control("LastName", "xpath", "//div[@data-id='EM.FullName_MiddleName']//input");
                txtMName.SendKeys("Grace");

                //STEP 12
                Control txtLName = new Control("LastName", "xpath", "//div[@data-id='EM.FullName_LastName']//input");
                txtLName.SendKeys("Hari");

                //STEP 13
                Control txtPrefName = new Control("LastName", "xpath", "//div[@data-id='EM.FullName_PreferredName']//input");
                txtPrefName.SendKeys("CassieHari");

                //STEP 14
                Control cmbOrg = new Control("Organization", "xpath", "//div[@data-id='EM.Org']//input");
                cmbOrg.SendKeys("US / Engineering / Boston");

                //STEP 15
                Driver.SessionLogger.WriteLine("TAB: Accounting", Logger.MessageType.INF);

                //STEP 16
                Control tabAccounting = new Control("AccountingTab", "xpath", "//a[@data-tab-id='accounting']");
                tabAccounting.Click();

                Thread.Sleep(1000);

                //STEP 17
                Control cmbLPType = new Control("LaborPostingType", "xpath", "//div[@data-id='EM.Type']//input");
                cmbLPType.SendKeys("Employee");

                Thread.Sleep(1000);

                Control menuEmployee = new Control("EmployeeMenu", "xpath", "//div[@class='search-name'][.='Employee']");
                menuEmployee.Click();

                // Switch Tab: Time & Expense
                Driver.SessionLogger.WriteLine("TAB: Time & Expense", Logger.MessageType.INF);

                //STEP 18
                Control tabTimeExpense = new Control("TimeExpenseTab", "xpath", "//a[@data-tab-id='timeAndExpenseTab']");
                tabTimeExpense.Click();

                Thread.Sleep(1000);

                //STEP 19
                Control cmbLCLevel1 = new Control("LaborCodeLevel1", "xpath", "//div[@data-id='EM.DefaultLC1']//span[contains(@class, 'tap-target')]");
                cmbLCLevel1.Click();
                Thread.Sleep(1000);
                Control menuLaborCode1 = new Control("LC1Menu", "xpath", "//div[@class='search-name'][.='00/General']");
                menuLaborCode1.Click();

                //STEP 20
                Control cmbLCLevel2 = new Control("LaborCodeLevel2", "xpath", "//div[@data-id='EM.DefaultLC2']//span[contains(@class, 'tap-target')]");
                cmbLCLevel2.Click();
                Thread.Sleep(1000);
                Control menuLaborCode2 = new Control("LC2Menu", "xpath", "//div[@class='search-name'][.='000/General-Overhead']");
                menuLaborCode2.Click();

                //STEP 21
                Control cmbLCLevel3 = new Control("LaborCodeLevel3", "xpath", "//div[@data-id='EM.DefaultLC3']//span[contains(@class, 'tap-target')]");
                cmbLCLevel3.Click();
                Thread.Sleep(1000);
                Control menuLaborCode3 = new Control("LC3Menu", "xpath_display", "//div[@class='search-name'][.='00/General']");
                menuLaborCode3.Click();

                //STEP 22
                Control cmbLCLevel4 = new Control("LaborCodeLevel4", "xpath", "//div[@data-id='EM.DefaultLC4']//span[contains(@class, 'tap-target')]");
                cmbLCLevel4.Click();
                Thread.Sleep(1000);
                Control menuLaborCode4 = new Control("LC4Menu", "xpath", "//div[@class='search-name'][.='4/gail level4']");
                menuLaborCode4.Click();

                //STEP 23
                Control cmbLCLevel5 = new Control("LaborCodeLevel5", "xpath", "//div[@data-id='EM.DefaultLC5']//span[contains(@class, 'tap-target')]");
                cmbLCLevel5.Click();
                Thread.Sleep(1000);
                Control menuLaborCode5 = new Control("LC5Menu", "xpath", "//div[@class='search-name'][.='5/gail level5']");
                menuLaborCode5.Click();

                //STEP 24
                Control btnSave = new Control("SaveBttn", "css", "button.btn.primary.save-btn");
                btnSave.Click();
                Thread.Sleep(5000);

                //STEP 26
                Driver.SessionLogger.WriteLine("Search and Load Existing Employee", Logger.MessageType.INF);

                //STEP 27
                Control navProjects = new Control("NavigateProjects", "xpath", "//div[contains(@data-app-id, 'Projects')]");
                navProjects.Click();
                Thread.Sleep(5000);

                //STEP 29
                Driver.SessionLogger.WriteLine("Navigate to Employees", Logger.MessageType.INF);
                navEmployee.Click();
                Thread.Sleep(7000);
                
                //STEP 31
                Control btnEmpFilter = new Control("SearchFilter", "id", "employeeFilter");
                btnEmpFilter.Click();

                Control menuAll = new Control("AllMenu", "css", "li[data-key='ALL']");
                menuAll.Click();
                
                //STEP 32
                Control txtBFindEmp = new Control("FindEmployee", "css", "input[placeholder='Find employee']");
                txtBFindEmp.SendKeys("Cassandra");
                //STEP 33
                Control menuEmp = new Control("EmpMenu", "css", "li[data-id='0AU00003|0A']");
                menuEmp.Click();

                Thread.Sleep(5000);

                //STEP 34
                Control btnNavLeft = new Control("AllMenu", "xpath", "//span[contains(@class, 'nav-left')]");
                if (!btnNavLeft.Exists())
                {
                    Driver.SessionLogger.WriteLine(String.Format("Unable to find {0}", btnNavLeft.mControlName), Logger.MessageType.ERR);
                    return false;
                }

                //STEP 35
                Control btnNavRight = new Control("AllMenu", "xpath", "//span[contains(@class, 'nav-right')]");
                if (!btnNavRight.Exists())
                {
                    Driver.SessionLogger.WriteLine(String.Format("Unable to find {0}", btnNavRight.mControlName), Logger.MessageType.ERR);
                    return false;
                }

                //STEP 36
                btnEmpFilter.Click();

                //STEP 37
                menuAll.Click();

                Driver.SessionLogger.WriteLine("Switch To List View", Logger.MessageType.INF);
                
                //STEP 38
                Control btnListView = new Control("ListViewBttn", "xpath", "//span[contains(@class, 'icon-listview')]");
                btnListView.Click();

                Thread.Sleep(7000);
                //STEP 39
                Control tblEmployee = new Control("EmployeesTable", "css", "div[data-id='ListViewGrid']");
                if (!tblEmployee.Exists())
                {
                    Driver.SessionLogger.WriteLine(String.Format("Unable to find {0}", tblEmployee.mControlName), Logger.MessageType.ERR);
                    return false;
                }

                //STEP 40
                Control btnFormView = new Control("FormViewBttn", "xpath", "//span[contains(@class, 'icon-formview')]");
                btnFormView.Click();

                Thread.Sleep(1000);

                //STEP 41
                Driver.SessionLogger.WriteLine("Clean Up Steps", Logger.MessageType.INF);

                //STEP 42
                Control btnActions = new Control("ActionsBttn", "css", "span[action-bar-data='actionsMenu']");
                StormCommon.WaitControlDisplayed(btnActions);
                Thread.Sleep(2000);
                btnActions.Click();

                Thread.Sleep(1000);

                Control menuDelete = new Control("DeleteMenu", "xpath", "//span[@class='popupLabel'][contains(.,'Delete')]");
                menuDelete.Click();
                
                //STEP 43
                Control btnDelete = new Control("DeleteBttn", "css", "button[data-id='yes']");
                StormCommon.WaitControlDisplayed(btnDelete);
                Thread.Sleep(1000);
                btnDelete.Click();

                //STEP 45
                Thread.Sleep(5000);
                Control btnSettings = new Control("SettingBttn", "id", "stormBannerSettingsMenu");
                btnSettings.Click();
                Thread.Sleep(1000);

                //STEP 46
                Control menuLogout = new Control("LogOutMenu", "id", "logout");
                menuLogout.Click();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                ret = false;
            }

            return ret;
        }
    }
}
