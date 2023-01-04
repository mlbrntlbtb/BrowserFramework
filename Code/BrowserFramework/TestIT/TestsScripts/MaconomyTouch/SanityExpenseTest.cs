using CommonLib.DlkSystem;
using MaconomyTouchLib.System;
using System;
using System.Linq;
using System.Threading;
using TestIT.Envs;
using TestIT.Sys;

namespace TestIT.TestsScripts.MaconomyTouch
{
    public class SanityExpenseTest : TestScript
    {
        public override bool TestExecute(out string ErrorMessage, string envID, string TestPath)
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
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("SlidingMenu", "Expense Sheet", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 8
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Add", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 9
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Description", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 10
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description", "Set", new String[] { "Sanity Expense O{DT} O{TT}" });
                Thread.Sleep(1000);

                //STEP 11
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 12
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Period", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 13
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Period Picker", "PeriodFrom", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 14
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Period Picker", "PeriodPick_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 15
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Period Picker", "PeriodTo", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 16
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Period Picker", "PeriodPick_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 17
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Job", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 18
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "Find", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 19
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "JobList", "VerifyExists", new String[] { "True" });
                Thread.Sleep(1000);

                //STEP 20
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "JobList", "GetRowCount", new String[] { "JL" });
                Thread.Sleep(1000);

                //STEP 21
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "1", "O{JL}", "SJOB" });
                Thread.Sleep(1000);

                //STEP 22
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "JobList", "SelectByRow", new String[] { "O{SJOB}" });
                Thread.Sleep(1000);

                //STEP 23
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Currency", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 24
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "1", "27", "SC" });
                Thread.Sleep(1000);

                //STEP 25
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Currency Picker", "CurrencyPicker", "SelectByIndex", new String[] { "O{SC}" });
                Thread.Sleep(1000);

                //STEP 26 
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Currency Picker", "Currency_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 27
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Save", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 28
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "ExpenseSheetDropdown", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 29
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Dropdown_NewLine", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 30
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Description", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 31
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description", "Set", new String[] { "Sanity Expense O{DT} O{TT}" });
                Thread.Sleep(1000);

                //STEP 32
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 33
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Job", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 34
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "Find", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 35
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "JobList", "GetRowCount", new String[] { "JL" });
                Thread.Sleep(1000);

                //STEP 36
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "1", "O{JL}", "SJOB" });
                Thread.Sleep(1000);

                //STEP 37
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("FindJob", "JobList", "SelectByRow", new String[] { "O{SJOB}" });
                Thread.Sleep(1000);

                //STEP 38
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Task", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 39
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("TaskList", "TaskList", "GetRowCount", new String[] { "TL" });
                Thread.Sleep(1000);

                //STEP 40
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "1", "O{TL}", "STASK" });
                Thread.Sleep(1000);

                //STEP 41
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("TaskList", "TaskList", "SelectByRow", new String[] { "O{STASK}" });
                Thread.Sleep(1000);

                //STEP 42
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "ExpenseAmtAct", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 43
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("ActivityList", "ActivityList", "GetRowCount", new String[] { "AL" });
                Thread.Sleep(1000);

                //STEP 44
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "1", "O{AL}", "SACT" });
                Thread.Sleep(1000);

                //STEP 45
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("ActivityList", "ActivityList", "SelectByRow", new String[] { "O{SACT}" });
                Thread.Sleep(1000);

                //STEP 46
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "2", "30", "RANDQ" });
                Thread.Sleep(1000);

                //STEP 47
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Quantity", "Set", new String[] { "O{RANDQ}" });
                Thread.Sleep(1000);

                //STEP 48
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "2", "30", "RANDU" });
                Thread.Sleep(1000);

                //STEP 49
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "UnitPrice", "Set", new String[] { "O{RANDU}" });
                Thread.Sleep(1000);

                //STEP 50
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Currency", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 51
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "Randomizer", new String[] { "1", "6", "SC" });
                Thread.Sleep(1000);

                //STEP 52
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Currency Picker", "CurrencyPicker", "SelectByIndex", new String[] { "O{SC}" });
                Thread.Sleep(1000);

                //STEP 53
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Currency Picker", "Currency_Done", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 54
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Save", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 55
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Quantity", "AssignValueToVariable", new String[] { "QUANTITY" });
                Thread.Sleep(1000);

                //STEP 56
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "UnitPrice", "AssignValueToVariable", new String[] { "UNITPRICE" });
                Thread.Sleep(1000);

                //STEP 57
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Function", "Function", "PerformMathOperation", new String[] { "O{QUANTITY}", "O{UNITPRICE}" , "*" , "AMNT" });
                Thread.Sleep(1000);

                //STEP 58
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Amount", "VerifyTextContains", new String[] { "O{AMNT}", "True" });
                Thread.Sleep(1000);

                //STEP 59
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Description", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 60
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description", "Set", new String[] { "Sanity Expense O{DT} O{TT} EDIT" });
                Thread.Sleep(1000);

                //STEP 61
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Description", "Description", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 62
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Save", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 63
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet Line", "Delete", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 64
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PopupMessage", "Message", "VerifyTextContains", new String[] { "Delete Expense Sheet Line?" });
                Thread.Sleep(1000);

                //STEP 65
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PopupMessage", "Yes", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 66
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "ExpenseSheetDropdown", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 67
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Dropdown_Submit", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 68
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "ExpenseSheetDropdown", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 69
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Dropdown_Reopen", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 70
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Expense Sheet", "Delete", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 71
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PopupMessage", "Message", "VerifyTextContains", new String[] { "Delete Expense Sheet?" });
                Thread.Sleep(1000);

                //STEP 72
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("PopupMessage", "Yes", "Clicks", new String[] { "" });
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
