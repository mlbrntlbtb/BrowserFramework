using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using ProjectInformationManagementLib.System;
using ProjectInformationManagementLib.DlkControls;
using ProjectInformationManagementLib.DlkFunctions;

namespace ProjectInformationManagementLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkProjectInformationManagementFunctionHandler
    {
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
                    case "MaximizeScreen":
                        MaximizeScreen();
                        break;
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "VerifyTitle":
                        VerifyTitle(Parameters[0]);
                        break;
                    case "SwitchToWindow":
                        SwitchToWindow(Convert.ToInt16(Parameters[0]));
                        break;
                    case "CloseWindowIndex":
                        CloseWindowIndex(Convert.ToInt16(Parameters[0]));
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
                    case "Login":
                        switch (Keyword)
                        {
                            case "Login":
                                DlkLogin.Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "CommonDialog":
                        switch(Keyword)
                        {
                            case "FileDownload":
                                DlkDialog.FileDownload(Parameters[0], Parameters[1]);
                                break;
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);                                
                        }
                        break;
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }

        [Keyword("MaximizeScreen")]
        public static void MaximizeScreen()
        {
            try
            {
                DlkEnvironment.AutoDriver.Manage().Window.Maximize();
                DlkLogger.LogInfo("MaximizeScreen() passed : ");
            }
            catch (Exception e)
            {
                throw new Exception("MaximizeScreen() failed : " + e.Message, e);
            }
        }

        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
           
            switch (Operator)
            {
                case "=":
                case ">=":
                    int iValueToTest = -1, iVariableValue = -1;
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
                    DlkProjectInformationManagementTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                case "contains":
                    if (VariableValue.Contains(ValueToTest))
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    
                    DlkProjectInformationManagementTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|title|sample title" })]
        public static void VerifyTitle(string Title)
        {
            try
            {
                if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
                }
                //Initialize();
                string actualTitle = DlkEnvironment.AutoDriver.Title;
                bool isEqual = DlkAssert.Equals(Title, actualTitle);

                if (isEqual)
                {
                    DlkLogger.LogInfo("VerifyTitle() passed");
                }
                else
                {
                    DlkLogger.LogInfo(string.Format("VerifyTitle() failed actual:{0} compare:{1}", actualTitle, Title));
                }

                if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
                {
                    DlkEnvironment.AutoDriver.Close();
                    DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[0]);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
            //finally
            //{
            //    Terminate();
            //}
        }

        [Keyword("SwitchToWindow", new String[] { "1|int|index|1" })]
        public static void SwitchToWindow(int Index)
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[Index]);
                DlkEnvironment.mWindowIndex = Index;
            }
            catch (Exception e)
            {
                throw new Exception("SwitchToWindow() failed : " + e.Message, e);
            }
        }

        [Keyword("CloseWindowIndex", new String[] { "1|int|index|1" })]
        public static void CloseWindowIndex(int Index)
        {
            try
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[Index]);
                DlkEnvironment.AutoDriver.Close();
                DlkEnvironment.mWindowIndex = Index - 1;
            }
            catch (Exception e)
            {
                throw new Exception("CloseWindowIndex() failed : " + e.Message, e);
            }
        }
    }
}
