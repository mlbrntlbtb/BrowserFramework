using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Common;
using System.Threading;

namespace METestHarness.Tests.Samples
{
    public class EMP_CreateCopyDeleteEmployee_03 : TestScript
    {
        private Control btnActions;

        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            
            try
            {
                StormCommon.Login("SAMPLE2.0", out ErrorMessage);

                Control menuToggle = new Control("menuToggle", "xpath", "//span[contains(@class, 'menu-toggle')]");
                Control navEmployee = new Control("NavigateEmployee", "xpath", "//div[contains(@data-app-id, 'Employees')]");
                Control lnkNewEmp = new Control("NewEmployee", "css", "div[title='New Employee']");
                btnActions = new Control("ActionsBttn", "css", "span[action-bar-data='actionsMenu']");

                //STEP 1
                Driver.SessionLogger.WriteLine("Navigate to Employees", Logger.MessageType.INF);

                //STEP 2
                StormCommon.WaitControlDisplayed(menuToggle);
                
                //STEP 3
                navEmployee.Click();
                Thread.Sleep(3000);

                //STEP 4
                StormCommon.WaitControlDisplayed(lnkNewEmp);
                Thread.Sleep(5000);
                
                //STEP 5
                lnkNewEmp.Click();
                Thread.Sleep(3000);

                Employee emp1 = new Employee("AU00003", "Cassandra", "Hari", "Grace", "CassieHari");
                
                // STEP 6
                Driver.SessionLogger.WriteLine("TAB: Overview", Logger.MessageType.INF);

                Control tabOverview = new Control("OverviewTab", "xpath", "//a[@data-tab-id='overview']");
                Control txtBEmpNo = new Control("EmployeeNumber", "xpath", "//div[@data-id='EM.Employee']//input");
                Control txtFName = new Control("FirstName", "xpath", "//div[@data-id='EM.FullName_FirstName']//input");
                Control txtMName = new Control("LastName", "xpath", "//div[@data-id='EM.FullName_MiddleName']//input");
                Control txtLName = new Control("LastName", "xpath", "//div[@data-id='EM.FullName_LastName']//input");
                Control txtPrefName = new Control("LastName", "xpath", "//div[@data-id='EM.FullName_PreferredName']//input");
                Control cmbOrg = new Control("Organization", "xpath", "//div[@data-id='EM.Org']//input");
                Control tabAccounting = new Control("AccountingTab", "xpath", "//a[@data-tab-id='accounting']");
                Control cmbLPType = new Control("LaborPostingType", "xpath", "//div[@data-id='EM.Type']//input");
                Control menuEmployee = new Control("EmployeeMenu", "xpath", "//div[@class='search-name'][.='Employee']");

                //STEP 7-8
                Thread.Sleep(1000);
                tabOverview.Click();
                
                //STEP 9
                txtBEmpNo.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                Thread.Sleep(3000);
                txtBEmpNo.SendKeys(emp1.EmployeeID);

                //STEP 10
                cmbOrg.SendKeys("US / Engineering / Boston");

                //STEP 11
                txtFName.SendKeys(emp1.FirstName);
                
                //STEP 12
                txtMName.SendKeys(emp1.MiddleName);
                
                //STEP 13
                txtLName.SendKeys(emp1.LastName);
                
                //STEP  14
                txtPrefName.SendKeys(emp1.PreferredName);
                Thread.Sleep(5000);

                // STEP 15
                Driver.SessionLogger.WriteLine("TAB: Accounting", Logger.MessageType.INF);
                
                //STEP 16
                tabAccounting.Click();

                Thread.Sleep(1000);
                
                //STEP 17
                cmbLPType.SendKeys("Employee");
                Thread.Sleep(1000);
                menuEmployee.Click();

                // Switch Tab: Time & Expense
                Driver.SessionLogger.WriteLine("TAB: Time & Expense", Logger.MessageType.INF);

                Control tabTimeExpense = new Control("TimeExpenseTab", "xpath", "//a[@data-tab-id='timeAndExpenseTab']");
                Control cmbLCLevel1 = new Control("LaborCodeLevel1", "xpath", "//div[@data-id='EM.DefaultLC1']//span[contains(@class, 'tap-target')]");
                Control menuLaborCode1 = new Control("LC1Menu", "xpath", "//div[@class='search-name'][.='00/General']");
                Control cmbLCLevel2 = new Control("LaborCodeLevel2", "xpath", "//div[@data-id='EM.DefaultLC2']//span[contains(@class, 'tap-target')]");
                Control menuLaborCode2 = new Control("LC2Menu", "xpath", "//div[@class='search-name'][.='000/General-Overhead']");
                Control cmbLCLevel3 = new Control("LaborCodeLevel3", "xpath", "//div[@data-id='EM.DefaultLC3']//span[contains(@class, 'tap-target')]");
                Control menuLaborCode3 = new Control("LC3Menu", "xpath_display", "//div[@class='search-name'][.='00/General']");
                Control cmbLCLevel4 = new Control("LaborCodeLevel4", "xpath", "//div[@data-id='EM.DefaultLC4']//span[contains(@class, 'tap-target')]");
                Control menuLaborCode4 = new Control("LC4Menu", "xpath", "//div[@class='search-name'][.='4/gail level4']");
                Control cmbLCLevel5 = new Control("LaborCodeLevel5", "xpath", "//div[@data-id='EM.DefaultLC5']//span[contains(@class, 'tap-target')]");
                Control menuLaborCode5 = new Control("LC5Menu", "xpath", "//div[@class='search-name'][.='5/gail level5']");

                //STEP 18
                tabTimeExpense.Click();

                Thread.Sleep(1000);

                //STEP 19
                cmbLCLevel1.Click();
                Thread.Sleep(1000);
                menuLaborCode1.Click();

                //STEP 20
                cmbLCLevel2.Click();
                Thread.Sleep(1000);
                menuLaborCode2.Click();
                
                //STEP 21
                cmbLCLevel3.Click();
                Thread.Sleep(1000);
                menuLaborCode3.Click();

                //STEP 22
                cmbLCLevel4.Click();
                Thread.Sleep(1000);
                menuLaborCode4.Click();
                
                //STEP 23
                cmbLCLevel5.Click();
                Thread.Sleep(1000);
                menuLaborCode5.Click();

                //STEP 24
                Control btnSave = new Control("SaveBttn", "css", "button.btn.primary.save-btn");
                btnSave.Click();

                //STEP 26
                Driver.SessionLogger.WriteLine("Copy newly created Employee", Logger.MessageType.INF);
                
                //STEP 27
                Driver.SessionLogger.WriteLine("TAB: Overview", Logger.MessageType.INF);

                StormCommon.WaitControlDisplayed(btnActions);
                Thread.Sleep(7000);
                
                //STEP 28 -29
                btnActions.Click();
                Control menuCopy = new Control("CopyMenu", "xpath_display", "//span[@class='popupLabel'][contains(.,'Copy')]");
                Thread.Sleep(5000);
                menuCopy.Click();
                Thread.Sleep(5000);

                //STEP 30
                tabOverview.Click();
                Thread.Sleep(1000);

                //STEP 31
                txtFName.FindElement();
                if(!txtFName.GetValue().Equals(emp1.FirstName))
                {
                    ErrorMessage = String.Format("Verify [{0}] failed : {1} is not equal to {2}", txtFName.mControlName, txtFName.GetValue(), emp1.FirstName);
                    return false;
                }

                //STEP 32
                txtMName.FindElement();
                if(!txtMName.GetValue().Equals(emp1.MiddleName))
                {
                    ErrorMessage = String.Format("Verify [{0}] failed : {1} is not equal to {2}", txtMName.mControlName, txtMName.GetValue(), emp1.MiddleName);
                    return false;
                }

                //STEP 33
                txtLName.FindElement();
                if(!txtLName.GetValue().Equals(emp1.LastName))
                {
                    ErrorMessage = String.Format("Verify [{0}] failed : {1} is not equal to {2}", txtLName.mControlName, txtLName.GetValue(), emp1.LastName);
                    return false;
                }

                //STEP 34
                txtPrefName.FindElement();
                if(!txtPrefName.GetValue().Equals(emp1.PreferredName))
                {
                    ErrorMessage = String.Format("Verify [{0}] failed : {1} is not equal to {2}", txtPrefName.mControlName, txtPrefName.GetValue(), emp1.PreferredName);
                    return false;
                }

                //STEP 35
                txtBEmpNo.FindElement();
                if (!txtBEmpNo.GetValue().Equals(string.Empty))
                {
                    ErrorMessage = String.Format("Verify [{0}] failed : {1} is not equal to {2}", txtBEmpNo.mControlName, txtBEmpNo.GetValue(), string.Empty);
                    return false;
                }

                //STEP 36
                Driver.SessionLogger.WriteLine("Set new values", Logger.MessageType.INF);
                Employee emp2 = new Employee("AU00004", "Antonio", "Hari", "Lester", "AliHari");

                //STEP 37
                txtBEmpNo.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                txtBEmpNo.SendKeys(emp2.EmployeeID);

                //STEP 38
                txtFName.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                txtFName.SendKeys(emp2.FirstName);
                
                //STEP 39
                txtMName.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                txtMName.SendKeys(emp2.MiddleName);

                //STEP 40
                txtLName.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                txtLName.SendKeys(emp2.LastName);
                
                //STEP 41
                txtPrefName.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                txtPrefName.SendKeys(emp2.PreferredName);

                //STEP 42
                btnSave.Click();
                
                //STEP 43 - 44
                Thread.Sleep(5000);
                Driver.SessionLogger.WriteLine("Search and Load Existing Employee", Logger.MessageType.INF);

                Control navProjects = new Control("NavigateProjects", "xpath", "//div[contains(@data-app-id, 'Projects')]");
                Control btnEmpFilter = new Control("SearchFilter", "id", "employeeFilter");
                Control menuAll = new Control("AllMenu", "css", "li[data-key='ALL']");
                Control txtBFindEmp = new Control("FindEmployee", "css", "input[placeholder='Find employee']");
                Control menuEmp = new Control("EmpMenu", "css", "li[data-id='0AU00003|0A']");
                Control btnNavLeft = new Control("AllMenu", "xpath", "//span[contains(@class, 'nav-left')]");
                Control btnNavRight = new Control("AllMenu", "xpath", "//span[contains(@class, 'nav-right')]");

                //STEP 45 - 46
                navProjects.Click();
                Thread.Sleep(5000);

                //STEP 47 - 48
                Driver.SessionLogger.WriteLine("Navigate to Employees", Logger.MessageType.INF);
                navEmployee.Click();
                Thread.Sleep(7000);
               
                //STEP 49
                btnEmpFilter.Click();
                menuAll.Click();

                //STEP 50 - 51
                txtBFindEmp.SendKeys("Cassandra");
                
                menuEmp.Click();

                Thread.Sleep(5000);
                
                //STEP 52
                if (!btnNavLeft.Exists())
                {
                    Driver.SessionLogger.WriteLine(String.Format("Unable to find {0}", btnNavLeft.mControlName), Logger.MessageType.ERR);
                    return false;
                }

                //STEP 53
                if (!btnNavRight.Exists())
                {
                    Driver.SessionLogger.WriteLine(String.Format("Unable to find {0}", btnNavRight.mControlName), Logger.MessageType.ERR);
                    return false;
                }

                //STEP 54 - 59
                CleanUpStepsAndLogout();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }

        private void CleanUpStepsAndLogout()
        {
            Driver.SessionLogger.WriteLine("Clean Up Steps", Logger.MessageType.INF);
            
            StormCommon.WaitControlDisplayed(btnActions);
            Thread.Sleep(2000);
            btnActions.Click();

            Thread.Sleep(1000);

            Control menuDelete = new Control("DeleteMenu", "xpath", "//span[@class='popupLabel'][contains(.,'Delete')]");
            menuDelete.Click();

            Control btnDelete = new Control("DeleteBttn", "css", "button[data-id='yes']");
            StormCommon.WaitControlDisplayed(btnDelete);
            Thread.Sleep(1000);
            btnDelete.Click();

            Thread.Sleep(5000);
            Control btnSettings = new Control("SettingBttn", "id", "stormBannerSettingsMenu");
            btnSettings.Click();
            Thread.Sleep(1000);
            Control menuLogout = new Control("LogOutMenu", "id", "logout");
            menuLogout.Click();
        }

    }

    public class Employee
    {
        public Employee(string ID, string FirstName, string LastName, string MiddleName, string PreferredName)
        {
            this.employeeID = ID;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.MiddleName = MiddleName;
            this.preferredName = PreferredName;
        }

        private string employeeID;
        public string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        private string preferredName;
        public string PreferredName
        {
            get { return preferredName; }
            set { preferredName = value; }
        }
        
    }
}
