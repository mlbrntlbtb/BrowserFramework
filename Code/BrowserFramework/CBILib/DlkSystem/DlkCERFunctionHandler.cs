using System;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CBILib.DlkControls;
using CBILib.DlkUtility;

namespace CBILib.DlkSystem
{
    [ControlType("Function")]
    public class DlkCERFunctionHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }
        private const int WAIT_LIMIT = 60;
        private const int WAIT_TO_EXIST = 1;

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
                    case "SwitchToWindow":
                        SwitchToWindow(Parameters[0]);
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

        [Keyword("SwitchToWindow", new String[] { "1|int|index|1" })]
        public static void SwitchToWindow(string Index)
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[int.Parse(Index)]);
                DlkLogger.LogInfo("Successfully executed SwitchToWindow().");
            }
            catch (Exception e)
            {
                throw new Exception("SwitchToWindow() failed : " + e.Message, e);
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
                DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "YourLogin_Login");
                DlkButton loginBtn = new DlkButton(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
                int waitTime = 0;
                /* wait for login screen to be ready */
                while (waitTime++ <= WAIT_LIMIT && !loginBtn.Exists(WAIT_TO_EXIST))
                {
                    continue; /* Do nothing. Call to Exist() takes 1s */
                }

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
                DlkCERCommon.WaitForSpinner();
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
            DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
        }
    }
}
