using CommonLib.DlkSystem;
using MaconomyTouchLib.System;
using System;
using System.Linq;
using System.Threading;
using TestIT.Envs;
using TestIT.Sys;

namespace TestIT.TestsScripts.MaconomyTouch
{
    public class SanityAbsenceTest : TestScript
    {
        public override bool TestExecute(out string ErrorMessage, string envID, string testPath)
        {
            ErrorMessage = string.Empty;
            bool ret = true;

            DlkEnvironment.SetContext("WEBVIEW");

            try
            {
                Envs.Environment myEnv = EnvMaconomyTouch.Environments.First(x => x.Id == envID);

                //STEP 1 - ***LOGIN***
                DlkMaconomyTouchFunctionHandler.Login(myEnv.Url, myEnv.User, myEnv.Password, myEnv.Database, myEnv.PIN);
                Thread.Sleep(5000);

                //STEP 2
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "TimeToday", new String[] { "TT" });
                Thread.Sleep(1000);

                //STEP 3
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "DateToday", new String[] { "DT" });
                Thread.Sleep(1000);

                //STEP 4
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Wait", new String[] { "10" });
                Thread.Sleep(1000);

                //STEP 5
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Dialog", "", "ClickDialogButtonIfExists", new String[] { "OK" });
                Thread.Sleep(1000);

                //STEP 6
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Main", "ShowMenu", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 7
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("SlidingMenu", "Settings", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 8
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Settings", "ShowAbsence", "Set", new String[] { "ON" });
                Thread.Sleep(1000);

                //STEP 9
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Main", "ShowMenu", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 10
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("SlidingMenu", "AbsenceRequests", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 11
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absece Requests", "Add", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 12
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absece Requests", "AbsenceType", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 13
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "AssignToVariable", new String[] { "SABS", "1" });
                Thread.Sleep(1000);

                //STEP 14
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("AbsenceType", "TypeList", "SelectByRow", new String[] { "O{SABS}" });
                Thread.Sleep(1000);

                //STEP 15
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "FirstDay", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 16
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Date Picker", "Date_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 17
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "LastDay", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 18
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Date Picker", "Date_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 19
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "Remark", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 20
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description", "Set", new String[] { "Sanity Absence O{DT} O{TT}" });
                Thread.Sleep(1000);

                //STEP 21
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 22
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "Save", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 23
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "ARMenuDropdown", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 24
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "ARSubmit", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 25
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "Submitted", "VerifyTextContains", new String[] { "Yes", "True" });
                Thread.Sleep(1000);

                //STEP 26
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Absence Requests", "Delete", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 27
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PopupMessage", "Message", "VerifyTextContains", new String[] { "Delete Absence Request?" });
                Thread.Sleep(1000);

                //STEP 28
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PopupMessage", "Yes", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 29
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Main", "ShowMenu", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 30
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("SlidingMenu", "Settings", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 31
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Settings", "ForgetMe", "Click", new String[] { "" });
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
