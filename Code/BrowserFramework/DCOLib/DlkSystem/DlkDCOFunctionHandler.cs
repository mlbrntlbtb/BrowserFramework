using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using DCOLib.DlkControls;

namespace DCOLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]    


    public class DlkDCOFunctionHandler
    {
        private const int LOGIN_WAIT_TIME = 30;
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
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
                    case "Login":
                        Login(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "PerformMathOperation":
                        PerformMathOperation(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
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
                    case "Database":
                        DlkDatabaseHandler.ExecuteDatabase(Keyword, Parameters);
                        break;
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }


        [Keyword("Login", new string[] {"1|text|URL|DCO URL",
                                        "2|text|User|YourUsername",
                                        "3|text|Password|YourPassword"})]
        public static void Login(string Url, string User, string Password)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("DCOLogin", "SignIn");
            DlkButton signInBtn = new DlkButton(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
            int waitTime = 0;
            /* wait for login page */
            while (waitTime++ <= LOGIN_WAIT_TIME && !signInBtn.Exists())
            {
                continue; /* Do nothing. */
            }

            try
            {
                new DlkTextBox("username", "ID", DlkDynamicObjectStoreHandler.GetControlRecord("DCOLogin", "Username").mSearchParameters).Set(User);
                new DlkTextBox("password", "ID", DlkDynamicObjectStoreHandler.GetControlRecord("DCOLogin", "Password").mSearchParameters).Set(Password);
                signInBtn.Click();

                if (String.IsNullOrEmpty(User) || String.IsNullOrEmpty(Password))
                {
                    throw new Exception("Login failed due to missing credentials.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Login cannot proceed. " + e.Message);
            }
        }

        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
            switch (Operator)
            {
                case "=":
                    int iValueToTest = -1, iVariableValue = -1;
                    bool bValueToTest, bVariableValue;
                    if (int.TryParse(VariableValue, out iVariableValue))
                    {
                        if (int.TryParse(ValueToTest, out iValueToTest)) // both are numbers, so compare the numbers
                        {
                            if (iVariableValue == iValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else // both are not numbers, so compare the values
                        {
                            if (VariableValue == ValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                    }
                    else if (bool.TryParse(VariableValue, out bVariableValue))
                    {
                        if (bool.TryParse(ValueToTest, out bValueToTest)) // both are boolean, so compare the boolean values
                        {
                            if (bVariableValue == bValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else // both are not numbers, so compare the values
                        {
                            if (VariableValue == ValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                    }
                    else // both are not numbers, so compare the values
                    {
                        if (VariableValue == ValueToTest)
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    DlkDCOTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }
        [Keyword("PerformMathOperation", new String[] { @"100|5|+|Sum" })]
        public static void PerformMathOperation(String FirstOperand, String SecondOperand, String Operator, String VariableName)
        {
            try
            {
                Decimal res = 0;
                Decimal num1 = 0;
                Decimal num2 = 0;

                if (!Decimal.TryParse(FirstOperand, out num1))
                {
                    throw new Exception("PerformMathOperation(): Invalid Input1 Parameter: " + FirstOperand);
                }
                if (!Decimal.TryParse(SecondOperand, out num2))
                {
                    throw new Exception("PerformMathOperation(): Invalid Input2 Parameter: " + SecondOperand);
                }

                /* Determine number of decimal places of output by getting the max decimal places between Operand1 and Operand2 */
                int firstOperandNumDecimal = FirstOperand.Contains('.') ? (FirstOperand.Length - 1) - FirstOperand.IndexOf('.') : 0;
                int secondOperandNumDecimal = SecondOperand.Contains('.') ? (SecondOperand.Length - 1) - SecondOperand.IndexOf('.') : 0;
                int numDecimalPlaces = Math.Max(firstOperandNumDecimal, secondOperandNumDecimal);

                int firstOperandWholeNumString = FirstOperand.Contains('.') ? FirstOperand.IndexOf('.') : FirstOperand.Length;
                int secondOperandWholeNumString = SecondOperand.Contains('.') ? SecondOperand.IndexOf('.') : SecondOperand.Length;
                int numberWholeNumber = Math.Max(firstOperandWholeNumString, secondOperandWholeNumString);

                string fmtWhole = string.Empty;
                string fmtDecimal = string.Empty;
                string fmt = string.Empty;

                if (FirstOperand.StartsWith("0") && !(FirstOperand.Contains('.'))) // To handle special cases like for serial numbers (e.g. 0000012345)
                {
                    for (int i = 0; i < numberWholeNumber; i++)
                    {
                        fmtWhole += "0";
                    }

                    for (int i = 0; i < numDecimalPlaces; i++)
                    {
                        fmtDecimal += "0";
                    }
                    fmt = !fmtDecimal.Equals("") ? string.Format("{0}.{1}", fmtWhole, fmtDecimal) : fmtWhole;
                }

                switch (Operator)
                {
                    case "+":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] + [" + SecondOperand + "].");
                        res = num1 + num2;
                        break;
                    case "-":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] - [" + SecondOperand + "].");
                        res = num1 - num2;
                        break;
                    case "*":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] * [" + SecondOperand + "].");
                        res = num1 * num2;
                        break;
                    case "/":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] / [" + SecondOperand + "].");
                        res = num1 / num2;
                        break;
                    default:
                        throw new Exception("PerformMathOperation(): Unsupported operator " + Operator);
                }
                /* Round UP the output to numDecimalPlaces */
                decimal resDec = decimal.Round(res, numDecimalPlaces, MidpointRounding.AwayFromZero);
                String resString = resDec.ToString(fmt);

                DlkVariable.SetVariable(VariableName, resString);
                DlkLogger.LogInfo("Successfully executed PerformMathOperation(). Variable:[" + VariableName + "], Value:[" + resString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMathOperation() failed: " + ex.Message);
            }
        }
    }
}
