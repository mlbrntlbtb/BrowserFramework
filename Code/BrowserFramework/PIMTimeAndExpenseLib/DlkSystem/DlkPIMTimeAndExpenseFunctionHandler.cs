using System;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using PIMTimeAndExpenseLib.DlkFunctions;

namespace PIMTimeAndExpenseLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkPIMTimeAndExpenseFunctionHandler
    {
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
                    default:
                        DlkFunctionHandler.ExecuteFunction(Keyword, Parameters);
                        break;
                }
            }
            else
            {
                switch (Screen)
                {
                    case "Database":
                        DlkDatabaseHandler.ExecuteDatabase(Keyword, Parameters);
                        break;
                    case "Login":
                        switch (Keyword)
                        {
                            case "Login":
                                Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Dialog":
                        switch (Keyword)
                        {
                            case "ClickDialogButton":
                                DlkDialog.ClickDialogButton(Parameters[0]);
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


        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        private static void Login(String Url, String User, String Password, String Database, String Pin)
        {
            int iSleep = 1000;
            
            // use the object store definitions 
            if (!string.IsNullOrEmpty(Database))
            {
                DlkPIMTimeAndExpenseKeywordHandler.ExecuteKeyword("Login", "ClientCode", "Set", new String[] { Database });
                DlkPIMTimeAndExpenseKeywordHandler.ExecuteKeyword("Login", "CompleteSetup_btn", "Tap", new String[] { "" });
                Thread.Sleep(iSleep);


                DlkPIMTimeAndExpenseKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkPIMTimeAndExpenseKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                DlkPIMTimeAndExpenseKeywordHandler.ExecuteKeyword("Login", "Login_btn", "Tap", new String[] { "" });
                Thread.Sleep(iSleep);

                WaitForSpinnerToFinishLoading(iSleep);
            }
        }

        #region METHODS

        private static void WaitForSpinnerToFinishLoading(int iSleep)
        {
            for (int sleep = 0; sleep < 30; sleep++)
            {
                DlkBaseControl spin = new DlkBaseControl("Spinner", "XPATH", "//*[@class='android.widget.ProgressBar']");
                if (spin.Exists())
                {
                    //if current spinner is visible, sleep 1 sec                                    
                    DlkLogger.LogInfo("Waiting for page to load...");
                    Thread.Sleep(iSleep);
                }
                else
                {
                    break;
                }

            }
        }
        #endregion
    }
}
