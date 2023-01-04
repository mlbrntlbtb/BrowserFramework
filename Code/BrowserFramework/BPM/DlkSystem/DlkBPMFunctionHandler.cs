using System;
using CommonLib.DlkSystem;
using BPMLib.DlkControls;
using BPMLib.DlkUtility;

namespace BPMLib.DlkSystem
{
    [ControlType("Function")]
    public class DlkBPMFunctionHandler
    {
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
                        WaitForLoadingDialogFinished(Parameters[0]);
                        break;
                    case "GenerateRandomNumber":
                        GenerateRandomNumber(Parameters[0], Parameters[1], Parameters[2]);
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

        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            DlkButton loginBtn = new DlkButton("loginButton", "IFRAME_XPATH", @"servletBridgeIframe_//*[@id='_id0:logon:logonButton']");
            int waitTime = 0;
            /* wait for login screen to be ready */
            while (waitTime++ <= WAIT_LIMIT && !loginBtn.Exists(WAIT_TO_EXIST))
            {
                continue; /* Do nothing. Call to Exist() takes 1s */
            }

            new DlkTextBox("username", "IFRAME_XPATH", @"servletBridgeIframe_//*[@id='_id0:logon:USERNAME']").Set(User);
            new DlkTextBox("password", "IFRAME_XPATH", @"servletBridgeIframe_//*[@id='_id0:logon:PASSWORD']").Set(Password);
            loginBtn.Click();
            DlkBPMCommon.WaitForSpinner();
        }

        [Keyword("WaitForLoadingDialogFinished", new String[] { "" })]
        public static void WaitForLoadingDialogFinished(string TimeOut)
        {
            try
            {
                int t;
                if (!int.TryParse(TimeOut, out t))
                {
                    throw new Exception("Invalid Timeout value: '" + t + "'");
                }
                DlkBPMCommon.WaitForLoadingDialogFinished(t);
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
    }
}
