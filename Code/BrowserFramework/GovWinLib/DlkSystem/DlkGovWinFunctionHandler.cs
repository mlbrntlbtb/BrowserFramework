using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CommonLib.DlkHandlers;
//using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using GovWinLib.System;
using GovWinLib.DlkControls;
using GovWinLib.DlkFunctions;

namespace GovWinLib.System
{
    /// <summary>
    /// The function handler executes functions; when keywords do not provide the required flexibility
    /// Functions can be tied to screens or be top level
    /// </summary>
    [ControlType("Function")]
    public static class DlkGovWinFunctionHandler
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
                            case "ClickOkDialogIfExists":
                                DlkDialog.ClickOkDialogIfExists(Parameters[0]);
                                break;
                            case "ClickCancelDialogWithMessage":
                                DlkDialog.ClickCancelDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickOkDialogWithMessage":
                                DlkDialog.ClickOkDialogWithMessage(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);                                
                        }
                        break;
                    case "Browser":
                        switch(Keyword.ToLower())
                        {
                            case "back":
                                DlkBrowser.Back();
                                break;
                            case "gotourl":
                                DlkBrowser.GoToUrl(Parameters[0]);
                                break;
                            case "geturl":
                                DlkBrowser.GetUrl(Parameters[0]);
                                break;
                            case "getbrowsertitle":
                                DlkBrowser.GetBrowserTitle(Parameters[0]);
                                break;
                            case "verifyurl":
                                DlkBrowser.VerifyUrl(Parameters[0]);
                                break;
                            case "gotopartialurl":
                                DlkBrowser.GoToPartialUrl(Parameters[0]);
                                break;
                            case "verifypartialurl":
                                DlkBrowser.VerifyPartialUrl(Parameters[0]);
                                break;
                            case "focusbrowserwithtitle":
                                DlkBrowser.FocusBrowserWithTitle(Parameters[0], Parameters[1]);
                                break;
                            case "focusbrowserwithurl":
                                DlkBrowser.FocusBrowserWithUrl(Parameters[0], Parameters[1]);
                                break;
                            case "forward":
                                DlkBrowser.Forward();
                                break;
                            case "refresh":
                                DlkBrowser.Refresh();
                                break;
                            case "close":
                                DlkBrowser.Close();
                                break;
                            default:
                                throw new Exception("Unkown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;        
                    case "Page":
                        switch(Keyword.ToLower())
                        {
                            case "getlistcount":
                                DlkPage.GetListCount(Parameters[0],Parameters[1]);
                                break;
                            case "clickwhitespaceonpage":
                                DlkPage.ClickWhiteSpaceOnPage();
                                break;
                            case "clickverifymodalheader":
                                DlkPage.ClickVerifyModalHeader(Parameters[0], Parameters[1]);
                                break;
                            case "clickverifynewtabpartialurl":
                                DlkPage.ClickVerifyNewTabPartialUrl(Parameters[0], Parameters[1]);
                                break;
                            case "clickverifynextpageheader":
                                DlkPage.ClickVerifyNextPageHeader(Parameters[0], Parameters[1]);
                                break;
                            case "clickverifynextpagepartialurl":
                                DlkPage.ClickVerifyNextPagePartialUrl(Parameters[0], Parameters[1]);
                                break;
                            case "clickverifynextpagesubheader":
                                DlkPage.ClickVerifyNextPageSubHeader(Parameters[0], Parameters[1]);
                                break;
                            case "clickverifynextpageurl":
                                DlkPage.ClickVerifyNextPageUrl(Parameters[0], Parameters[1]);
                                break;
                            case "expandlinkverifytext":
                                DlkPage.ExpandLinkVerifyText(Parameters[0], Parameters[1]);
                                break;
                            case "verifylinkbulletlistsamemodalheadername":
                                DlkPage.VerifyLinkBulletListSameModalHeaderName(Parameters[0]);
                                break;
                            case "verifymegamenulinklistsameheadername":
                                DlkPage.VerifyMegaMenuLinkListSameHeaderName(Parameters[0], Parameters[1]);
                                break;
                            case "clicklink":
                                DlkPage.ClickLink(Parameters[0]);
                                break;
                            case "verifyheader":
                                DlkPage.VerifyHeader(Parameters[0]);
                                break;
                            case "verifysubheader":
                                DlkPage.VerifySubHeader(Parameters[0]);
                                break;
                            case "verifyheadercontains":
                                DlkPage.VerifyHeaderContains(Parameters[0]);
                                break;
                            case "verifysubheadercontains":
                                DlkPage.VerifySubHeaderContains(Parameters[0]);
                                break;
                            case "verifymodalheadercontains":
                                DlkPage.VerifyModalHeaderContains(Parameters[0]);
                                break;
                            case "parselistitemstringtoint":
                                DlkPage.ParseListItemStringToInt(Parameters[0], Parameters[1]);
                                break;
                            case "getsubdatatablecount":
                                DlkPage.GetSubDataTableCount(Parameters[0]);
                                break;
                            case "getsubresultstablecount":
                                DlkPage.GetSubResultsTableCount(Parameters[0]);
                                break;
                            case "getdatatablecount":
                                DlkPage.GetDataTableCount(Parameters[0]);
                                break;
                            case "getresultstablecount":
                                DlkPage.GetResultsTableCount(Parameters[0]);
                                break;
                            case "clickverifynextpagepartialurlnoback":
                                DlkPage.ClickVerifyNextPagePartialUrlNoBack(Parameters[0], Parameters[1]);
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
        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
            int iValueToTest = -1, iVariableValue = -1;
            switch (Operator)
            {
                case "notequal":
                    if (int.TryParse(VariableValue, out iVariableValue))
                    {
                        if (int.TryParse(ValueToTest, out iValueToTest)) // both are numbers, so compare the numbers
                        {
                            if (iVariableValue != iValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] != [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] = [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else // both are not numbers, so compare the values
                        {
                            throw new Exception("IfThenElse(): Unsupported Operator for Non-numeric Values: " + Operator);
                        }
                    }
                    else // both are not numbers, so compare the values
                    {
                        throw new Exception("IfThenElse(): Unsupported Operator for Non-numeric Values: " + Operator);
                    }
                    DlkGovWinTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                case "=":
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
                    DlkGovWinTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                case ">=":
                    if (int.TryParse(VariableValue, out iVariableValue))
                    {
                        if (int.TryParse(ValueToTest, out iValueToTest)) // both are numbers, so compare the numbers
                        {
                            if (iVariableValue >= iValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] >= [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] !>= [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else // both are not numbers, so compare the values
                        {
                            throw new Exception("IfThenElse(): Unsupported Operator for Non-numeric Values: " + Operator);
                        }
                    }
                    else // both are not numbers, so compare the values
                    {
                        throw new Exception("IfThenElse(): Unsupported Operator for Non-numeric Values: " + Operator);
                    }
                    DlkGovWinTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
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
                    
                    DlkGovWinTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }
    }
}
