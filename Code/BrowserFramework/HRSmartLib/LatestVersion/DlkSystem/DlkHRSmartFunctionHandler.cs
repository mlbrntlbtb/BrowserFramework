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
using HRSmartLib.LatestVersion.DlkFunctions;

namespace HRSmartLib.LatestVersion.System
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
                            case "AssignExistStatus":
                                DlkDialog.AssignExistStatus(Parameters[0], Parameters[1]);
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
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.ScrollToElement(Parameters[0]);
                                    break;
                                }
                                case "GoToUrl" :
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.GoToUrl(Parameters[0]);
                                    break;
                                }
                                case "ReplaceURL":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.ReplaceURL(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "DragAndDrop" :
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.DragAndDrop(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "PerformActionWithAlertMessage":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.PerformActionWithAlertMessage(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "PerformActionWithPopUpMessage":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.PerformActionWithPopUpMessage(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "VerifyActionResult":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyActionResult(Parameters[0]);
                                    break;
                                }
                                case "VerifyActionResultModal":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyActionResultModal(Parameters[0]);
                                    break;
                                }
                                case "VerifyActionMessage":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyActionMessage(Parameters[0]);
                                    break;
                                }
                                case "CloseAlertMessage":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.CloseAlertMessage();
                                    break;
                                }
                                case "FocusBrowserWithTitle":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.FocusBrowserWithTitle(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "FocusBrowserWithUrl":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.FocusBrowserWithUrl(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "HasPartialTextContent":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.HasPartialTextContent(Parameters[0]);
                                    break;
                                }
                                case "VerifyPartialTextContent":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyPartialTextContent(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "HasTextContent":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.HasTextContent(Parameters[0]);
                                    break;
                                }
                                case "Close":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Close();
                                    break;
                                }
                                case "FocusBrowserToMainWindow":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.FocusBrowserToMainWindow();
                                    break;
                                }
                                case "VerifyURL":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyURL(Parameters[0]);
                                    break;
                                }
                                case "Back":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Back();
                                    break;
                                }
                                case "Refresh":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Refresh();
                                    break;
                                }
                                case "Tab":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Tab();
                                    break;
                                }
                                case "VerifyPageTitleBar":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyPageTitleBar(Parameters[0]);
                                    break;
                                }
                                case "Hint":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Hint(Parameters[0]);
                                    break;
                                }
                                case "Note":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Note(Parameters[0]);
                                    break;
                                }
                                case "ExpectedResult":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.ExpectedResult(Parameters[0]);
                                    break;
                                }
                                case "Step":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Step(Parameters[0]);
                                    break;
                                }
                                case "Warning":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Warning(Parameters[0]);
                                    break;
                                }
                                case "VerifyCaptionToolTip":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyCaptionToolTip(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "VerifyPageOptionCaptionExists":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyPageOptionCaptionExists(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "ExecuteKeyword":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.ExecuteKeyword(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                    break;
                                }
                                case "VerifyScriptContent":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyScriptContent(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "Delay":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.Delay(Parameters[0]);
                                    break;
                                }
                                case "VerifyURLAccessibility":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyURLAccessibility(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "ControlsCount":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.ControlsCount(Parameters[0]);
                                    break;
                                }
                                case "GenerateScript":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.GenerateScript(Parameters[0], Parameters[1]);
                                    break;
                                }
                                case "CheckJavascriptError":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.CheckJavascriptError();
                                    break;
                                }
                                case "VerifyJavascriptError":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyJavascriptError();
                                    break;
                                }
                                case "GenerateTestCase":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.GenerateTestCase(Parameters[0], Parameters[1]);
                                    break;    
                                }
                                case "VerifyHighlightedTextInPage":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkBrowser.VerifyHighlightedTextInPage(Parameters[0], Parameters[1]);
                                    break;  
                                }
                            }
                            break;
                        }
                    case "Core_Menu":
                        switch (Keyword)
                        {
                            case "NavigateToCCCompetencies":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkCoreMenu.NavigateToCCCompetencies();
                                    break;
                                }
                            case "Select":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkCoreMenu.Select(Parameters[0], Parameters[1], Parameters[2]);
                                    break;
                                }
                            case "MenuClick":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkCoreMenu.MenuClick(Parameters[0]);
                                    break;
                                }
                            case "VerifyMenuExists":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkCoreMenu.VerifyMenuExists(Parameters[0], Parameters[1]);
                                    break;
                                }
                            case "VerifySubMenuExists":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkCoreMenu.VerifySubMenuExists(Parameters[0], Parameters[1], Parameters[2]);
                                    break;
                                }
                            case "VerifySubMenuToggleExists":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkCoreMenu.VerifySubMenuToggleExists(Parameters[0], Parameters[1], Parameters[2]);
                                    break;
                                }
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Administration":
                        switch (Keyword)
                        {
                            case "GoTo":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkAdministration.GoTo(Parameters[0], Parameters[1]);
                                    break;
                                }
                            case "VerifyContentExists":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkAdministration.VerifyContentExists(Parameters[0], Parameters[1], Parameters[2]);
                                    break;
                                }
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Table":
                        switch (Keyword)
                        {
                            case "GetTableRowWithColumnValue":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkTable.GetTableRowWithColumnValue(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                                    break;
                                }
                            case "PerformRowActionByColumn":
                                {
                                    HRSmartLib.LatestVersion.DlkFunctions.DlkTable.PerformRowActionByColumn(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                                    break;
                                }
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Dynamic_Forms":
                        switch (Keyword)
                        {
                            case "AddFieldToForm":
                            {
                                DlkDynamicForm.AddFieldToForm(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                break;
                            }
                            case "AddFieldToFormByInnerDetails":
                            {
                                DlkDynamicForm.AddFieldToFormByInnerDetails(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                                break;
                            }
                            case "RemoveFieldFromFormByDrag":
                            {
                                DlkDynamicForm.RemoveFieldFromFormByDrag(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                break;
                            }
                            case "RemoveFieldFromForm":
                            {
                                DlkDynamicForm.RemoveFieldFromForm(Parameters[0], Parameters[1]);
                                break;
                            }
                            case "RemoveFormRow":
                            {
                                DlkDynamicForm.RemoveFormRow(Parameters[0]);
                                break;
                            }
                        }
                        break;
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
