using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using HRSmartLib.System;
using HRSmartLib.PreviousVersion.DlkFunctions;

namespace HRSmartLib.PreviousVersion.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public class DlkHRSmartFunctionHandler : HRSmartLib.DlkSystem.IFunctionHandler
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
        public void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
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
                    case "Dialog":
                        switch(Keyword)
                        {
                            case "FileDownload":
                                DlkDialog.FileDownload(Parameters[0], Parameters[1]);
                                break;
                            case "FileUpload":
                                DlkDialog.FileUpload(Parameters[0], Parameters[1]);
                                break;
                            case "ClickOkDialogWithMessage":
                                DlkDialog.ClickOkDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickCancelDialogWithMessage":
                                DlkDialog.ClickCancelDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickOkDialogIfExists":
                                DlkDialog.ClickOkDialogIfExists(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);                                
                        }
                        break;
                    case "Browser" :
                        {
                            switch (Keyword)
                            {
                                case "ScrollToElement" :
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.ScrollToElement(Parameters[0]);
                                    break;
                                }
                                case "GoToUrl" :
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.GoToUrl(Parameters[0]);
                                    break;
                                }
                                case "DragAndDrop" :
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.DragAndDrop(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "PerformActionWithAlertMessage":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.PerformActionWithAlertMessage(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "VerifyActionResult":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.VerifyActionResult(Parameters[0]);
                                    break;
                                }
                                case "VerifyActionMessage":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.VerifyActionMessage(Parameters[0]);
                                    break;
                                }
                                case "CloseAlertMessage":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.CloseAlertMessage();
                                    break;
                                }
                                case "FocusBrowserWithTitle":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.FocusBrowserWithTitle(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "FocusBrowserWithUrl":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.FocusBrowserWithUrl(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "HasPartialTextContent":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.HasPartialTextContent(Parameters[0]);
                                    break;
                                }
                                case "HasTextContent":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.HasTextContent(Parameters[0]);
                                    break;
                                }
                                case "Close":
                                {
                                    HRSmartLib.PreviousVersion.DlkFunctions.DlkBrowser.Close();
                                    break;
                                }
                            }
                            break;
                        }
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
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
                    DlkHRSmartTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    
                    DlkHRSmartTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }
    }
}
