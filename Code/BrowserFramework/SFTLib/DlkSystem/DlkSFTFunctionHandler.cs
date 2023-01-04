using System;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;
using SFTLib.DlkControls;
using SFTLib.DlkUtility;
using System.Threading;
using System.Windows.Forms;
using SFTLib.AutoMapper;
using System.Linq;

namespace SFTLib.DlkSystem
{
    [ControlType("Function")]
    public class DlkSFTFunctionHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }
        private const int WAIT_LIMIT = 60;
        private const int WAIT_TO_EXIST = 1;

        private static void Scroll(int up, int down)
        {
            try
            {
                var body = DlkEnvironment.AutoDriver.FindElement(By.XPath(".//*[contains(@id, 'dashboard-body')]"));
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("arguments[0].scroll(" + up + ", " + down + ")", body);
                DlkLogger.LogInfo("Successfully executed ScrollDown()");
            }
            catch
            {
                //continue
            }
        }

        /// <summary>
        /// this logic handles functions which are to be used when keywords do not provide the required flexibility
        /// </summary>
        /// <param name="Screen"></param>
        /// <param name="ControlName"></param>
        /// <param name="Keyword"></param>
        /// <param name="Parameters"></param>
        public static void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "WaitForLoadingDialogFinished":
                        WaitForLoadingDialogFinished();
                        break;
                    case "GenerateRandomNumber":
                        GenerateRandomNumber(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "ClickOkOnAlert":
                        ClickOKOnAlert();
                        break;
                    case "GetFormattedDateToday":
                        GetFormattedDateToday(Parameters[0], Parameters[1]);
                        break;
                    case "ClickButtonIfExists":
                        ClickButtonIfExists(Parameters[0]);
                        break;
                    case "ScrollDown":
                        ScrollDown();
                        break;
                    case "ScrollUp":
                        ScrollUp();
                        break;
                    case "AutoMapper":
                        AutoMapper(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "FirstWeekdayOfTheMonth":
                        FirstWeekdayOfTheMonth(Parameters[0], Parameters[1]);
                        break;
                    case "ClickOkOnMessageBox":
                        ClickOkOnMessageBox();
                        break;
                    default:
                        DlkFunctionHandler.ExecuteFunction(Keyword, Parameters);
                        break;
                }
            }
            else
            {
                switch (Screen)
                {
                    case "Login":
                        switch (Keyword)
                        {
                            case "Login":
                                Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }
        [Keyword("GetFormattedDateToday", new String[] { "" })]
        public static void GetFormattedDateToday(String VariableName, String DateFormat)
        {
            DlkVariable.SetVariable(VariableName, DateTime.Now.ToString(DateFormat));
        }

        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            if (!String.IsNullOrEmpty(User))
            {
                new DlkButton("Control", "XPATH", "//*[text()='Acknowledge']/parent::*").ClickButtonIfExists();
                DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "YourLogin_Login");
                DlkButton loginBtn = new DlkButton(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
                int waitTime = 0;
                /* wait for login screen to be ready */
                while (waitTime++ <= WAIT_LIMIT && !loginBtn.Exists(WAIT_TO_EXIST))
                {
                    continue; /* Do nothing. Call to Exist() takes 1s */
                }
                /*  
                 *  Reuse additional settings > environment > database field for SFT's login dictionary
                 *  Dictionary to be set first before username and password due to page reload on dictionary change
                */
                new DlkComboBox("database", "XPATH_DISPLAY", DlkDynamicObjectStoreHandler.GetControlRecord("Login", "YourLogin_Dictionary").mSearchParameters).Select(Database);
                new DlkTextBox("username", "XPATH_DISPLAY", DlkDynamicObjectStoreHandler.GetControlRecord("Login", "YourLogin_Username").mSearchParameters).Set(User);
                new DlkTextBox("password", "XPATH_DISPLAY", DlkDynamicObjectStoreHandler.GetControlRecord("Login", "YourLogin_Password").mSearchParameters).Set(Password);
                loginBtn.Click();
            }
        }

        [Keyword("WaitForLoadingDialogFinished", new String[] { "" })]
        public static void WaitForLoadingDialogFinished()
        {
            try
            {
                DlkSFTCommon.WaitForSpinner();
            }
            catch (Exception ex)
            {
                throw new Exception("WaitForLoadingDialogFinished() failed: " + ex.Message);
            }
        }

        [Keyword("GenerateRandomNumber", new String[] { "1|text|Min|1",
                                                           "2|text|Max|5",
                                                           "3|text|Variable|var1"})]
        public static void GenerateRandomNumber(String Min, String Max, String Variable)
        {
            try
            {
                int min = Convert.ToInt32(Min);
                int max = Convert.ToInt32(Max);

                Random rndm = new Random();
                int result = rndm.Next(min, max + 1);

                DlkVariable.SetVariable(Variable, result.ToString());
                DlkLogger.LogInfo(string.Format("Variable: {0} value is {1}", Variable, result));
                DlkLogger.LogInfo("Successfully executed GenerateRandomNumber()");
            }
            catch (Exception ex)
            {
                throw new Exception("GenerateRandomNumber() failed: " + ex.Message);
            }
        }
        [Keyword("ClickOkOnAlert")]
        public static void ClickOKOnAlert()
        {
            try
            {
                Thread.Sleep(1000);//pause before clicking on alert

                if(DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    ClickOkOnMessageBox();
                    return;
                }

                if (DlkAlert.DoesAlertExist(2))
                    DlkAlert.Accept();
                else
                    throw new Exception("No alert window found.");

                DlkSFTCommon.WaitForScreenToLoad();
                DlkSFTCommon.WaitForSpinner();
            }
            catch (Exception ex)
            {
                throw new Exception("ClickOkOnAlert() failed: " + ex.Message);
            }
        }
        [Keyword("ClickButtonIfExists")]
        public static void ClickButtonIfExists(string ButtonText)
        {
            try
            {
                DlkButton button = new DlkButton("Control", "IFRAME_XPATH", $"contentFrame_//*[contains(@id,'btnWrap') and .//*[text()='" + ButtonText + "']]");
                
                button.Initialize();
                button.Click();

                DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");

            }
            catch (Exception)
            {
                DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");
                throw;
            }
        }

        [Keyword("ScrollDown")]
        public static void ScrollDown()
        {
            try
            {
                Scroll(0, 2000);
            }
            catch (Exception e)
            {
                throw new Exception("ScrollDown() failed : " + e.Message);
            }
        }

        [Keyword("ScrollUp")]
        public static void ScrollUp()
        {
            try
            {
                Scroll(2000, 0);
            }
            catch (Exception e)
            {
                throw new Exception("ScrollUp() failed : " + e.Message);
            }
        }

        [Keyword("AutoMapper")]
        public static void AutoMapper(string ObjectStorePath, string ParentTabName, string ScreenFileName, string IsDialogBox, string IsLookup) {
            DialogResult dialogResult = MessageBox.Show($"Auto Map? {ParentTabName}", "Auto Mapper", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
                return;
            new DlkAutoMapper(ObjectStorePath, ParentTabName, ScreenFileName, IsDialogBox, IsLookup).Start();
        }

        [Keyword("FirstWeekdayOfTheMonth", new String[] { "1|text|VariableName|varname",
                                                           "2|text|Date|mm/dd/yy"})]
        public static void FirstWeekdayOfTheMonth(string VariableName, string Date)
        {
            try
            {
                DateTime date;
                DateTime.TryParse(Date, out date);
                var first = new DateTime(date.Year, date.Month, 1);

                if(first.DayOfWeek == DayOfWeek.Sunday)
                    first = first.AddDays(1);
                else if(first.DayOfWeek == DayOfWeek.Saturday)
                    first = first.AddDays(2);

                DlkVariable.SetVariable(VariableName, first.ToString("MM/dd/yy"));
                DlkLogger.LogInfo($"Successfully set the value ({first.ToString("MM/dd/yy")}) to variable \"{VariableName}\" ");
            }
            catch (Exception e)
            {
                throw new Exception("FirstWeekdayOfTheMonth() failed : " + e.Message);
            }
        }

        [Keyword("ClickOkOnMessageBox")]
        public static void ClickOkOnMessageBox()
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.AutoDriver.SwitchTo().Frame("contentFrame");
                var messageBoxButtonOK = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[contains(@id, 'messagebox')]//*[contains(@id, 'button') and not(contains(@style, 'display: none'))]//*[contains(@id, 'btnWrap')]")).FirstOrDefault();
                if (messageBoxButtonOK != null)
                    messageBoxButtonOK.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickOkOnMessageBox() failed : " + e.Message);
            }
        }
    }
}
