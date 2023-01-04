using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using System.Threading;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using Recorder.Model;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using OpenQA.Selenium;
using Recorder.View;
using TestRunner.Common;

namespace Recorder.Presenter
{
    public class CP_MainPresenter : MainPresenter
    {
        #region PRIVATE MEMBERS
        private List<DlkObjectStoreFileControlRecord> mMainControls;
        private List<DlkObjectStoreFileControlRecord> mQueryControls;
        private List<DlkObjectStoreFileControlRecord> mPageSetUpControls;
        private List<DlkObjectStoreFileControlRecord> mPrintOptionsControls;
        private List<DlkObjectStoreFileControlRecord> mBrowseFilePopupControls;
        private List<DlkObjectStoreFileControlRecord> mFileUploadMgrControls;
        private List<DlkObjectStoreFileControlRecord> mProcessProgControls;
        private List<DlkObjectStoreFileControlRecord> mSelectCompanyControls;
        private List<DlkObjectStoreFileControlRecord> mUserPreferencesControls;
        private List<DlkObjectStoreFileControlRecord> mSubmitBatchJobControls;
        private List<DlkObjectStoreFileControlRecord> mReportControls;
        private List<DlkObjectStoreFileControlRecord> mDatePickerControls;
        private string mLastTableColumnHeaderAccessed = string.Empty;
        private string mLastTableRowAccessed = string.Empty;
        private string mLastTableControlNameValue = string.Empty;
        private CP_Inspector mCPInspector = null;
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Sets verify keyword
        /// </summary>
        /// <param name="control">The control to be verified</param>
        /// <returns>Returns a keyword value depending on control type</returns>
        private string SetVerifyKeyword(Enumerations.ControlType control)
        {
            switch (mView.VerifyType)
            {
                case Model.Enumerations.VerifyType.VerifyContent:
                    switch (control)
                    {
                        case Enumerations.ControlType.Button:
                        case Enumerations.ControlType.TextBox:
                        case Enumerations.ControlType.TextArea:
                        case Enumerations.ControlType.Link:
                        case Enumerations.ControlType.ConditionListBox:
                            return Constants.CP_KEYWORD_VERIFYTEXT;
                        case Enumerations.ControlType.Form:
                            return Constants.CP_KEYWORD_VERIFYTITLE;
                        case Enumerations.ControlType.Table:
                            return Constants.CP_KEYWORD_VERIFYTABLECELLVALUE;
                        case Enumerations.ControlType.MessageArea:
                            return Constants.CP_KEYWORD_VERIFYMESSAGEEXISTS;
                        default:
                            return Constants.CP_KEYWORD_VERIFYVALUE;
                    }
                case Model.Enumerations.VerifyType.VerifyExists:
                    return control == Enumerations.ControlType.Table ? Constants.CP_KEYWORD_VERIFYEXIST : Constants.CP_KEYWORD_VERIFYEXISTS;
                case Model.Enumerations.VerifyType.VerifyReadOnly:
                    if (control == Enumerations.ControlType.Table)
                    {
                        return Constants.CP_KEYWORD_VERIFYTABLECELLREADONLY;
                    }
                    else
                    {
                        return Constants.CP_KEYWORD_VERIFYREADONLY;
                    }
                default:
                    return Constants.CP_KEYWORD_VERIFYEXISTS;
            }
        }

        /// <summary>
        /// Get table column name
        /// </summary>
        /// <param name="act">The action object being selected</param>
        /// <param name="Output">The header of the column</param>
        private bool GetTableControlColumnName(Model.Action act, out string Output)
        {
            bool ret = mCPInspector.GetTableColumnHeader(act.Target.Base, act.Target.Class, out Output);
            Output = ret ? DlkString.RemoveCarriageReturn(Output).Replace("/ ","/") : string.Empty;
            return ret;
        }

        /// <summary>
        /// Get table row number
        /// </summary>
        /// <param name="act">The action to be performed.</param>
        private bool GetTableRowNumber(Model.Action act, out string Output)
        {
            int targetRow;
            if (int.TryParse(act.Target.Id, out targetRow) && targetRow < 100) // row identifier 100 above is new row
            {
                Output = act.Target.Id;
                return true;
            }
            else
            {
                return mCPInspector.GetTableRowNumber(act.Target.Base, out Output);
            }
        }

        /// <summary>
        /// Get lookup button
        /// </summary>
        private void GetLookUpButton()
        {
            try
            {
                Model.Action srcAction = null;
                try
                {
                    srcAction = mView.Actions.FindLast(x => x.Target.Type == Enumerations.ControlType.TextBox);
                }
                catch
                {
                    srcAction = null;
                }

                Screen lookUpScreen = new Screen()
                {
                    Name = srcAction.Screen.Name
                };

                mAction = new Model.Action
                {
                    Type = Enumerations.ActionType.Click,
                    Screen = lookUpScreen,
                    Target = new Model.Control
                    {
                        Descriptor = new Descriptor
                        {
                            Type = srcAction.Target.Descriptor.Type,
                            Value = srcAction.Target.Descriptor.Value
                        },
                        AncestorFormIndex = srcAction.Target.AncestorFormIndex,
                        Type = srcAction.Target.Type,
                        Value = Constants.CP_CUSTOM_VALUE_LOOKUP,
                        Base = srcAction.Target.Base
                    },
                };
                mAction.Block = GetBlock(mAction);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles alert dialog
        /// </summary>
        /// <param name="alert">The content of the alert dialog</param>
        private bool HandleAlert(string alert)
        {

            bool alertResponse = false;
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (System.Action)(() =>
                {
                    System.Drawing.Point newPoint = new System.Drawing.Point(browserPt.X + browserSize.Width / 2, browserPt.Y + browserSize.Height / 2);
                    UnexpectedModalDialog dlg = new UnexpectedModalDialog(alert, "Test Capture", newPoint, true);
                    alertResponse = (bool)dlg.ShowDialog();
                }));
                if (alertResponse)
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                }
                else
                {
                    do
                    {
                        DlkEnvironment.AutoDriver.SwitchTo().Alert().Dismiss();
                    } while (DlkAlert.DoesAlertExist());
                }
            }
            catch
            {
                // ignored
            }

            return alertResponse;
        }

        /// <summary>
        /// Gets additional information from the control needed to construct the step
        /// </summary>
        /// <param name="TargetControl">The target control</param>
        /// <param name="ClassName">The class name of the control</param>
        /// <returns>Returns the information needed to construct the step</returns>
        private string GetAdditionalInfo(DlkBaseControl TargetControl, string ClassName)
        {
            string ret = "";
            if (ClassName.ToLower().Contains("fcb") || ClassName.ToLower().Contains("uiradio"))
            {
                ret = TargetControl.GetAttributeValue(Constants.ATTRIB_TYPE);
            }
            else if (ClassName.ToLower().Contains("tlbrddactiondiv"))
            {
                ret = TargetControl.GetParent().GetAttribute(Constants.ATTRIB_ID);
            }
            return ret;
        }

        /// <summary>
        /// Gets control type from classname
        /// </summary>
        /// <param name="ClassName">The class name of the control</param>
        /// <param name="AddInfo">Optional parameter to discern if button is radio or checkbox if applicable</param>
        /// <returns>Returns the control type of the identified control</returns>
        private Enumerations.ControlType GetControlType(string ClassName, string AddInfo = "")
        {
            /* support for 701 tabs */
            ClassName = ClassName.ToLower().Contains("tbbtn") ? ClassName : ClassName.Replace("Inactive", string.Empty).Replace("Active", string.Empty);

            Enumerations.ControlType ret;
            switch (ClassName.ToLower())
            {
                case "fdfrq":
                case "fdf":
                case "fdfro":
                case "fdfrqnum":
                case "fdfronum":
                case "fdfnum":
                case "fdexpandosmall":
                case "inputfld":
                case "querybasicfld":
                case "querydatafield":
                case "query":
                case "appfltr":
                case "popupdatafield":
                case "cust_lu":
                    ret = Enumerations.ControlType.TextBox;
                    break;
                case "bok":
                case "imgbtn":
                case "actbtn":
                case "fcalbtn":
                case "fcalbtn20":
                case "fexpandobtn":
                case "fbrowsebtn":
                case "flaunchbtn":
                case "submitbtn":
                case "submitbtnhover":
                case "submitbtnactive":
                case "querypopupbtn":
                case "imgandtextbtn":
                case "showcriteriadiv":
                case "hidecriteriadiv":
                case "hidecriteriaimg":
                case "showcriteriaimg":
                case "navareabtn":
                case "gotolbl":
                case "lkpglyph":
                case "popupbtn":
                case "selcomwarnbtn":
                case "mymnubottomimg":
                case "loginbtn":
                case "closelegal":
                case "hideadditionalcriteria":
                case "additionalcriteria":
                case "pushsubbtn":
                    ret = Enumerations.ControlType.Button;
                    break;
                case "querycb":
                case "popupfcb":                  
                case "fcb":
                case "testautoposition":
                case "uiradio":
                case "settings":
                    ret = AddInfo.ToLower() == "radio" ? Enumerations.ControlType.RadioButton : Enumerations.ControlType.CheckBox;
                    break;
                case "tccbf":
                case "tccb":
                case "tccbimg":
                case "tccbt":
                    ret = Enumerations.ControlType.ComboBox;
                    break;
                case "cust_tccbv":
                    ret = Enumerations.ControlType.ComboBoxItem;
                    break;
                case "sbtsklnk":
                case "drilldownpathtext":
                case "mymnubottomlbl":
                case "helplink":
                case "legallink":
                case "reset":
                case "resetgoodbye":
                case "qrlink":
                case "legal":
                    ret = Enumerations.ControlType.Link;
                    break;
                case "wmnupick":
                case "wmnuhead":
                    ret = Enumerations.ControlType.Menu;
                    break;
                case "navitem":
                case "busitem":
                case "deptitem":
                case "navicon":
                case "busicon":
                case "depticon":
                case "navlbl":
                case "buslbl":
                case "deptlbl":
                    ret = Enumerations.ControlType.NavigationMenu;
                    break;
                case "modaltablbl":
                case "tabrightbtn":
                case "tableftbtn":
                    ret = Enumerations.ControlType.Tab;
                    break;
                case "rstbbtn":
                case "rstbbtnactive":
                case "rstbbtnnormal":
                case "rstbbtnhover":
                case "rstbbtndisabled":
                case "rsdragger":
                case "rstbbtnimg":
                case "fcba":
                    ret = Enumerations.ControlType.Form;
                    break;
                case "tbbtn":
                case "tbbtnimg":
                case "tbbtnsplitleft":
                case "tbbtnsplitright":
                case "tbbtn_active":
                case "tbbtn_normal":
                case "tbbtn_disabled":
                case "tbbtn_hover":
                case "tbbtnsplitleft_normal":
                case "tbbtnsplitright_normal":
                case "tbbtnsplitleft_hover":
                case "tbbtnsplitright_hover":
                case "tbbtnhrpt":
                case "tbbtnhrpt nav":
                    ret = Enumerations.ControlType.Toolbar;
                    break;
                case "fdfnh":
                case "fdfronh":
                case "fdfrqnh":
                    ret = Enumerations.ControlType.TextArea;
                    break;
                case "tlbrddactiondiv":
                    if (AddInfo.ToLower() == "acttbl")
                    {
                        ret = Enumerations.ControlType.ContextMenu;
                    }
                    else
                    {
                        ret = mView.Actions.Last().Target.Type == Enumerations.ControlType.Menu ?
                        Enumerations.ControlType.Menu : Enumerations.ControlType.Toolbar;
                    }
                    break;
                case "popupclose":
                case "wok":
                case "msgtext":
                case "msgtextold":
                case "elnk":
                case "closem":
                    ret = Enumerations.ControlType.MessageArea;
                    break;
                case "tccbtb":
                case "tdf":
                case "tdfrq":
                case "tdfro":
                case "tcb":
                case "tdfrqnum":
                case "tdfronum":
                case "cllfrst":
                case "tdfnum": 
                case "tbledpic":
                case "drilldownplus":
                case "selall":
                case "cust_lu_table":
                case "hddiv":
                case "hddivreq":
                    ret = Enumerations.ControlType.Table;
                    break;
                case "autocitem":
                    ret = Enumerations.ControlType.SearchAppResultList;
                    break;
                case "popupcaltdybtn":
                case "popupcaldate":
                    ret = Enumerations.ControlType.Calendar;
                    break;
                case "smpllstbxitm":
                case "fclb":
                    ret = Enumerations.ControlType.ConditionListBox;
                    break;
                case "mymnuviewport":             
                    ret = Enumerations.ControlType.AppMenuView;
                    break;
                default:
                    ret = Enumerations.ControlType.Unknown; // temp
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Gets screen
        /// </summary>
        /// <param name="ControlType">Type of the control</param>
        /// <param name="Value">Value of the control</param>
        /// <returns>Returns the screen of the identified controls</returns>
        private string GetScreen(Enumerations.ControlType ControlType, string Value, DlkCapturedStep CurrentStep=null)
        {
            string ret = string.Empty;

            if (IsMain(ControlType, Value))
            {
                ret = Constants.CP_SCREEN_MAIN;
            }
            else if (IsQuery(Value))
            {
                ret = Constants.CP_SCREEN_QUERY;
            }
            else if(IsPageSetUp(ControlType, Value, CurrentStep.PopRefId))
            {
                ret = Constants.CP_SCREEN_PAGESETUP;
            }
            else if (IsFileUploadManager(Value))
            {
                ret = Constants.CP_SCREEN_FILEUPLOADMANAGER;
            }
            else if (IsPrintOptions(Value))
            {
                ret = Constants.CP_SCREEN_PRINTOPTIONS;
            }
            else if (IsProcessProgress(Value))
            {
                ret = Constants.CP_SCREEN_PROCESSPROGRESS;
            }
            else if (IsSelectCompany(Value))
            {
                ret = Constants.CP_SCREEN_SELECTCOMPANY;
            }
            else if (IsUserPreference(Value))
            {
                ret = Constants.CP_SCREEN_USERPREFERENCES;
            }
            else if (IsSubmitBatchJob(Value))
            {
                ret = Constants.CP_SCREEN_SUBMITBATCHJOB;
            }
            else if (IsAppMenuView(ControlType, Value))
            {
                ret = Constants.CP_SCREEN_APPLICATIONVIEW;
            }
            else if (IsBrowseFilePopup(Value,CurrentStep.ElementClass))
            {
                ret = Constants.CP_SCREEN_BROWSEFILEPOPUP;
            }
            else if (IsReport(Value))
            {
                ret = Constants.CP_SCREEN_REPORT;
            }
            else if (IsDatePicker(Value))
            {
                ret = Constants.CP_SCREEN_DATEPICKER;
            }
            else
            {
                if (CurrentStep != null)
                {
                    if (!string.IsNullOrEmpty(CurrentStep.PopRefId) && (CurrentStep.PopRefId.Contains("LKP") || CurrentStep.PopRefId.Contains("LOOKUP")))
                    {
                        ret = Constants.CP_SCREEN_LOOKUP;
                    }
                    else if (GetControlsFromFiles(CurrentStep.InferredScreen ?? string.Empty).Any())
                    {
                        ret = CurrentStep.InferredScreen;
                    }
                    else
                    {
                        ret = mCPInspector.GetScreen(Value);
                    }
                }
                else
                {
                    ret = mCPInspector.GetScreen(Value);
                }
            }

            return ret;
        }

        /// <summary>
        /// Checks if search string is a control from the Query screen
        /// </summary>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        private bool IsQuery(string searchstring)
        {
            bool ret = false;
            try
            {
                if (searchstring.Contains("__img")) //to truncate __img and for it to return True.
                {
                    searchstring = searchstring.Replace("__img", "");
                }
                ret = searchstring.Contains("tabQLabel") || mQueryControls.FindAll(x => x.mSearchParameters == searchstring).Count > 0;
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Checks if control type is a control from the Main screen
        /// </summary>
        /// <param name="ControlType">Type of the control</param>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        private bool IsMain(Enumerations.ControlType ControlType, string searchstring)
        {
            bool ret = false;
            try
            {
                switch (ControlType)
                {
                    case Enumerations.ControlType.NavigationMenu:
                    case Enumerations.ControlType.Menu:
                    case Enumerations.ControlType.Toolbar:
                    case Enumerations.ControlType.MessageArea:                    
                        ret = true;
                        break;
                    case Enumerations.ControlType.AppMenuView:
                        ret = searchstring.ToLower() != "appmnuview";
                        break;
                    default:
                        ret = mMainControls.FindAll(x => x.mSearchParameters == searchstring).Count > 0;
                        break;
                }
            }
            catch
            {

            }
            return ret;
        }

        /// <summary>
        /// Checks if control type is a control from App Menu View
        /// </summary>
        /// <param name="ControlType"></param>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        /// <returns></returns>
        private bool IsAppMenuView(Enumerations.ControlType ControlType, string searchstring)
        {
            bool ret = false;
            if (ControlType == Enumerations.ControlType.AppMenuView || searchstring.Contains("myMnu"))
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Checks if control type is a control from Page Setup screen
        /// </summary>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        /// <returns></returns>
        private bool IsPageSetUp(Enumerations.ControlType ControlType, string searchstring, string poprefid)
        {
            bool ret = false;
            try
            {
                ret = searchstring.Contains("rptInclCompLogo") || searchstring.Contains("rptPageOrientation") || mPageSetUpControls.FindAll(x => x.mSearchParameters == searchstring).Count > 0;   
                if (ret && !String.IsNullOrEmpty(poprefid) && ControlType != Enumerations.ControlType.Button) //for PageSetup controls placed in SYMSETNG. Button controls (OK & Cancel) are exceptions
                {
                    ret = false;
                }
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Checks if control is a control from Page Setup screen
        /// </summary>
        /// <param name="SearchString">Descriptor to find in object store file</param>
        /// <returns></returns>
        private bool IsPrintOptions(string SearchString)
        {
            bool ret = false;
            try
            {
                ret = SearchString.Contains("rptArchRelativeAbsDt") || new Regex(@"^(printSetup)\w*(Tab)$").IsMatch(SearchString)
                    || mPrintOptionsControls.FindAll(x => x.mSearchParameters == SearchString).Count > 0;
            }
            catch
            {
                // return false on exception
            }
            return ret;
        }

        /// <summary>
        /// Checks if search string is a control from the File Upload Manager screen
        /// </summary>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        private bool IsFileUploadManager(string searchstring)
        {
            bool ret = false;
            try
            {
                ret = searchstring.Contains("flblSys") || mFileUploadMgrControls.FindAll(x => x.mSearchParameters == searchstring).Count > 0;
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Check if control is from the Browse File Popup screen
        /// </summary>
        /// <param name="searchstring">search parameter</param>
        /// <param name="poprefid">classname</param>
        /// <returns></returns>
        private bool IsBrowseFilePopup(string searchstring, string poprefid)
        {
            bool ret = false;

            try
            {
                ret = poprefid.Contains("popup") && mBrowseFilePopupControls.FindAll(x => x.mSearchParameters == searchstring).Count() > 0;
            }
            catch
            {
                // ignored
            }
            return ret;
        }

        /// <summary>
        /// Checks if control is a control from Process Progress screen
        /// </summary>
        /// <param name="SearchString">Descriptor to find in object store file</param>
        /// <returns>False if condition is not met or when exception is encountered</returns>
        private bool IsProcessProgress(string SearchString)
        {
            bool ret = false;
            try
            {
                ret = SearchString.Contains("progMtr") || mProcessProgControls.FindAll(x => x.mSearchParameters == SearchString).Count > 0; ;
            }
            catch
            {
                // return false on exception
            }
            return ret;
        }

        /// <summary>
        /// Checks if control is a control from Select Company screen
        /// </summary>
        /// <param name="SearchString">Descriptor to find in object store file</param>
        /// <returns>False if condition is not met or when exception is encountered</returns>
        private bool IsSelectCompany(string SearchString)
        {
            bool ret = false;
            try
            {
                ret = mSelectCompanyControls.FindAll(x => x.mSearchParameters == SearchString).Count > 0; ;
            }
            catch
            {
                // return false on exception
            }
            return ret;
        }

        /// <summary>
        /// Checks if control is a control from User Preferences screen
        /// </summary>
        /// <param name="SearchString">Descriptor to find in object store file</param>
        /// <returns>False if condition is not met or when exception is encountered</returns>
        private bool IsUserPreference(string SearchString)
        {
            bool ret = false;
            try
            {
                ret = mUserPreferencesControls.FindAll(x => x.mSearchParameters == SearchString).Count > 0; ;
            }
            catch
            {
                // return false on exception
            }
            return ret;
        }

        /// <summary>
        /// Checks if control is a control from Submit Batch Job screen
        /// </summary>
        /// <param name="SearchString">Descriptor to find in object store file</param>
        /// <returns>False if condition is not met or when exception is encountered</returns>
        private bool IsSubmitBatchJob(string SearchString)
        {
            bool ret = false;
            try
            {
                ret = mSubmitBatchJobControls.FindAll(x => x.mSearchParameters == SearchString).Count > 0;
            }
            catch
            {
                // return false on exception
            }
            return ret;
        }

        /// <summary>
        /// Checks if search string is a control from the Report dialog
        /// </summary>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        private bool IsReport(string searchstring)
        {
            bool ret = false;
            try
            {
                ret = searchstring.Contains("Report") || mReportControls.FindAll(x => x.mSearchParameters == searchstring).Count > 0;
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Checks if search string is a control from the DatePicker screen (Calendar/DatePicker dialog)
        /// </summary>
        /// <param name="searchstring">The search parameter or class name of the control</param>
        private bool IsDatePicker(string searchstring)
        {
            bool ret = false;
            try
            {
                if (searchstring.Contains("__img")) //to truncate __img and for it to return True.
                {
                    searchstring = searchstring.Replace("__img", "");
                }
                ret = searchstring.Contains("Cal") || searchstring.StartsWith("cal") || mDatePickerControls.FindAll(x => x.mSearchParameters == searchstring).Count > 0;
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Get name of the control using its screen and control type
        /// </summary>
        /// <param name="screen">The screen where the control belongs</param>
        /// <param name="control">The control object</param>
        /// <returns>Returns the name, type and parameters of the clicked control</returns>
        private string GetControlName(string screen, Recorder.Model.Control control)
        {
            string ret = string.Empty;
            try
            {
                /* special case lookup OS */
                if (screen.ToLower() == "lookup")
                {
                    ret = Constants.CP_CONTROL_UNKNOWN;
                    if (control.Descriptor.Value.Contains("~"))
                    {
                        ret = "LookUpTable";
                    }
                    else
                    {
                        switch (control.Descriptor.Value.ToLower())
                        {
                            case "rscls":
                            case "rsmax":
                            case "qrybttn":
                            case "savedquerymnu":
                                ret = "LookUpForm";
                                break;
                            case "bcan":
                                ret = "LookUpTable_Cancel";
                                break;
                            case "bok":
                            case "bapply":
                                ret = "LookUpTable_Select";
                                break;
                            case "drilldownpathtext":
                                ret = "LookUpTable_DrillDownPath";
                                break;
                        }
                    }
                    return ret;
                }
                /* special case report OS */
                if (screen.ToLower() == "report")
                {
                    ret = Constants.CP_CONTROL_UNKNOWN;
                    switch (control.Descriptor.Value.ToLower())
                    {
                        case "closereport":
                        case "maximizereport":
                            ret = "ReportForm";
                            break;
                    }
                    return ret;
                }
                List<DlkObjectStoreFileControlRecord> lst = GetControlsFromFiles(screen);

                // check for all with ID descriptors
                List<DlkObjectStoreFileControlRecord> lstAllIDs = new List<DlkObjectStoreFileControlRecord>();
                if (control.Descriptor.Type == Enumerations.DescriptorType.Id)
                {
                    lstAllIDs = lst.FindAll(y => y.mSearchParameters == control.Descriptor.Value);
                    ret = lstAllIDs.First().mKey;
                }
                else /* xpath */
                {
                    // check xpath
                    switch (control.Type)
                    {
                        case Enumerations.ControlType.TextBox:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_TEXTBOX || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetControl(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.TextArea:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_TEXTAREA || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetControl(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.CheckBox:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_CHECKBOX || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetControl(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.ComboBox:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_COMBOBOX || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetControl(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.Tab:
                            //case Enumerations.ControlType.Table
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_TAB || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetControl(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.RadioButton:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_RADIOBUTTON || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetRadioButton(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.Button:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_BUTTON || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetButton(lstAllIDs, control.Descriptor.Value, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.Link:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_LINK || y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetLink(lstAllIDs, control.Descriptor.Value, control.Base.GetValue(),control.PopRefId);
                            break;
                        case Enumerations.ControlType.Form:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_FORM);
                            ret = mCPInspector.GetTargetForm(lstAllIDs, control.AncestorFormIndex, control.PopRefId);
                            break;
                        case Enumerations.ControlType.Table:
                            lstAllIDs = lst.FindAll(y => y.mControlType == Constants.CP_CONTROL_TYPE_TABLE || y.mControlType == Constants.CP_CONTROL_TYPE_FORM || y.mControlType == Constants.CP_CONTROL_TYPE_MULTIPARTTABLE);
                            ret = mCPInspector.GetTargetTable(lstAllIDs, control.AncestorFormIndex, control.Base, control.PopRefId);
                            break;
                        default:
                            ret = Constants.CP_CONTROL_TYPE_UNKNOWN;
                            break;
                    }
                }
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Get descriptor of control/screen tandem
        /// </summary>
        /// <param name="Source">The control being clicked</param>
        /// <param name="Screen">The object store which contains the list of controls being referenced</param>
        /// <returns>The descriptor or search parameters of the control</returns>
        private Descriptor GetDescriptor(DlkBaseControl Source, string Screen, string Id = "", string Class ="")
        {
            KeyValuePair<string, string> srcRaw = mCPInspector.GetDescriptor(Source, Id, Class);

            Descriptor ret = new Descriptor
            {
                Type = GetDescriptorTypeFromString(srcRaw.Key),
                Value = srcRaw.Value
            };
            ret.Type = IsID(Screen, ret.Value) ? GetDescriptorTypeFromString(Constants.DESCRIPTOR_TYPE_ID)
                : GetDescriptorTypeFromString(Constants.DESCRIPTOR_TYPE_XPATH);
            return ret;
        }

        /// <summary>
        /// Get ancestor form index of control
        /// </summary>
        /// <param name="Source">The control being clicked</param>
        /// <returns>Returns the index of the clicked control</returns>
        private int GetAncestorFormIndex(DlkBaseControl Source, string className = "", string InferredIndex = "")
        {
            int ret = -1;
            try
            {
                ret = mCPInspector.GetAncestorFormIndex(Source, className, InferredIndex);
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        /// <summary>
        /// Get value of Costpoint breadcrumb
        /// </summary>
        /// <returns>Returns the breadcrumbs trail of the menu</returns>
        private string GetBreadCrumbMenuValue()
        {
            string bus = mCPInspector.GetAttributeValue(Constants.DESCRIPTOR_TYPE_ID, "busPick", Constants.ATTRIB_TITLE);
            string dept = mCPInspector.GetAttributeValue(Constants.DESCRIPTOR_TYPE_ID, "deptPick", Constants.ATTRIB_TITLE);
            string work = mCPInspector.GetAttributeValue(Constants.DESCRIPTOR_TYPE_ID, "workPick", Constants.ATTRIB_TITLE);
            string act = mCPInspector.GetAttributeValue(Constants.DESCRIPTOR_TYPE_ID, "activityPick", Constants.ATTRIB_TITLE);

            return bus.Trim() + "~" + dept.Trim() + "~" + work.Trim() + "~" + act.Trim();
        }

        /// <summary>
        /// Get Menu value
        /// </summary>
        /// <param name="Actions">A list of actions</param>
        /// <returns>Returns the parameter of the menu control at point of click</returns>
        private string GetMenuValue(List<Model.Action> Actions)
        {
            string ret = string.Empty;
            foreach (Recorder.Model.Action act in Actions)
            {
                string val = act.Target.Value;

                /* Special case, click speed too fast to evaluate actual value of menu at point of click */
                switch (val)
                {
                    case "Delete Record":
                        val = "Undelete Record";
                        break;
                    case "Undelete Record":
                        val = "Delete Record";
                        break;
                    default:
                        break;
                }
                if (ret.Split('~').First() == val)
                {
                    continue;
                }
                ret += val + "~";
            }
            return ret.Trim('~');
        }

        /// <summary>
        /// Get Toolbar value
        /// </summary>
        /// <param name="Actions">A list of actions</param>
        /// <returns>Returns the parameter of the toolbar control at point of click</returns>
        private string GetToolbarValue(List<Model.Action> Actions)
        {
            string ret = string.Empty;

            foreach (Model.Action act in Actions)
            {
                string val = act.Target.Descriptor.Value == "tlbrDDActionDiv" ? act.Target.Value
                    : GetControlAttributeValue(act.Target, "title");

                if (ret.Split('~').First() == val)
                {
                    continue;
                }

                ret += val + "~";
            }
            return ret.Trim('~');
        }

        /// <summary>
        /// Compares controls if they are the same
        /// </summary>
        /// <param name="ctl1">The first control to be compared</param>
        /// <param name="ctl2">The second control to be compared</param>
        private bool CompareControls(Model.Control ctl1, Model.Control ctl2)
        {
            return ctl1.Descriptor.Value == ctl2.Descriptor.Value
                && ctl1.Descriptor.Type == ctl2.Descriptor.Type
                && ctl1.Type == ctl2.Type
                && ctl1.AncestorFormIndex == ctl2.AncestorFormIndex;
        }

        /// <summary>
        /// Check if target type changed
        /// Note: This function is intended for action types only
        /// </summary>
        /// <param name="lastActionType">The last action type</param>
        /// <param name="targetActionType">The target action type</param>
        /// <returns></returns>
        private bool TargetTypeChanged(Enumerations.ActionType lastActionType, Enumerations.ActionType targetActionType)
        {
            //Separate block if target type changed to AssignValue or AssignPartialValue
            if ((targetActionType == Enumerations.ActionType.AssignValue || lastActionType == Enumerations.ActionType.AssignValue)
                && targetActionType != lastActionType)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if controls are NavMenus
        /// </summary>
        /// <param name="ctl1">The first control to be compared</param>
        /// <param name="ctl2">The second control to be compared</param>
        private bool AreNavigationMenus(Model.Control ctl1, Model.Control ctl2)
        {
            return ctl1.Type == Enumerations.ControlType.NavigationMenu && ctl2.Type == Enumerations.ControlType.NavigationMenu;
        }

        /// <summary>
        /// Checks if controls are of the same menu group
        /// </summary>
        /// <param name="ctl1">The first control to be compared</param>
        /// <param name="ctl2">The second control to be compared</param>
        private bool AreSameMenuGroup(Model.Control ctl1, Model.Control ctl2)
        {
            bool ret = false;
            bool correctType = (ctl1.Type == Enumerations.ControlType.Menu && ctl2.Type == Enumerations.ControlType.Menu)
                || (ctl1.Type == Enumerations.ControlType.Toolbar && ctl2.Type == Enumerations.ControlType.Toolbar);
            if (correctType)
            {
                if (ctl2.Descriptor.Value == "tlbrDDActionDiv")
                {
                    Thread.Sleep(20); // to handle Stale Element exception
                    string name = GetControlAttributeValue(ctl2, Model.Constants.ATTRIB_NAME);
                    if (!string.IsNullOrEmpty(name))
                    {
                        ret = name.Contains("launch");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ctl1.Value) && !string.IsNullOrEmpty(ctl2.Descriptor.Value))
                    {
                        ret = ctl1.Value[0].ToString() == ctl2.Descriptor.Value[0].ToString();
                    }
                }

            }
            return ret;
        }
        #endregion

        #region PROTECTED OVERRIDE METHODS
        /// <summary>
        /// Get block of control
        /// </summary>
        /// <param name="TargetAction">The control to be identified.</param>
        /// <returns>Returns the block number of this step</returns>
        protected override int GetBlock(Model.Action TargetAction)
        {
            // Block
            int ret = 0;
            Model.Action lastAction = null;

            if (mView.Actions.Count > 0)
            { 
                lastAction = mView.Actions.Last();
            }            

            if (mView.Actions.Count == 0)
            {
                mView.CurrentBlock = 1;
                ret = 1;
            }
            else if (TargetAction.Target.Type == Enumerations.ControlType.Form
                || TargetAction.Target.Type == Enumerations.ControlType.Dialog)
            {
                ret = ++(mView.CurrentBlock);
            }
            else if (TargetAction.Target.Value == Constants.CP_CUSTOM_VALUE_LOOKUP
                || TargetAction.Target.Value == Constants.CP_CUSTOM_VALUE_LKP)
            {
                ret = ++(mView.CurrentBlock);
            }
            else if (TargetAction.Target.Type == Enumerations.ControlType.Table
                && (lastAction.Type == Enumerations.ActionType.AssignValue
                || TargetAction.Type == Enumerations.ActionType.AssignValue))
            {
                ret = ++(mView.CurrentBlock);
            }
            else if (TargetAction.Target.Type == Enumerations.ControlType.Table
                && lastAction.Target.Descriptor.Value != TargetAction.Target.Descriptor.Value)
            {
                ret = ++(mView.CurrentBlock);
            }
            else if (mView.KeywordType == Enumerations.KeywordType.Verify || mView.KeywordType == Enumerations.KeywordType.GetValue)
            {
                return ++(mView.CurrentBlock);
            }
            else if ((CompareControls(lastAction.Target, TargetAction.Target) && !TargetTypeChanged(lastAction.Type, TargetAction.Type)) // Same control
                || AreNavigationMenus(lastAction.Target, TargetAction.Target) // NavMenu controls
                || AreSameMenuGroup(lastAction.Target, TargetAction.Target) // Same menu group
                || TargetAction.Target.Type == Enumerations.ControlType.ComboBoxItem
                && (mView.Actions.FindAll(x => x.Block == mView.CurrentBlock).FindAll(
                y => y.Target.Value == TargetAction.Target.Value).Count == 0))
            {
                mView.CurrentBlock = mView.CurrentBlock;
                ret = mView.CurrentBlock;
            }
            else
            {
                ret = ++(mView.CurrentBlock);
            }
            return ret;
        }

        /// <summary>
        /// Handles alert response during pause and resume
        /// </summary>
        /// <param name="alert">The content of the alert dialog.</param>
        /// <param name="alertResponse">Sets if the dialog should appear or not</param>
        /// <param name="myControl">Provide a basic interface for the alert dialog</param>
        protected override void HandleAlertResponse(string alert, bool alertResponse, DlkBaseControl myControl)
        {
            try
            {
                //Handle Alert Response
                Screen alrtScreen = new Screen
                {
                    Name = Constants.CP_SCREEN_DIALOG
                };
                mAction = new Model.Action
                {
                    Type = Enumerations.ActionType.Click,
                    Screen = alrtScreen,
                    Target = new Model.Control
                    {
                        Descriptor = new Descriptor
                        {
                            Type = Enumerations.DescriptorType.Class,
                            Value = Constants.CP_SCREEN_DIALOG
                        },
                        AncestorFormIndex = -1,
                        Type = Enumerations.ControlType.Dialog,
                        Value = alert,
                        Base = myControl
                    },
                    AlertResponse = alertResponse
                };
                mAction.Block = GetBlock(mAction);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Convert list of actions into step
        /// </summary>
        /// <param name="Actions">The list of actions to be converted into step</param>
        /// <returns>Returns a step supplied with the list of actions</returns>
        protected override Model.Step PrimaryActionBlock(List<Model.Action> Actions)
        {
            Model.Step ret = new Step();
            ret.Execute = Constants.EXECUTE_DFLT_VAL;
            ret.Delay = Constants.DELAY_DFLT_VAL;
            ret.Block = Actions.Last().Block;
            // for actions other than Verify
            switch (Actions.First().Target.Type)
            {
                case Enumerations.ControlType.Dialog:
                    ret.Screen = Constants.CP_SCREEN_DIALOG;
                    ret.Control = string.Empty;
                    ret.Keyword = Actions.Last().AlertResponse ? Constants.CP_KEYWORD_CLICKOKDIALOG
                        : Constants.CP_KEYWORD_CLICKCANCELDIALOG;
                    ret.Parameters = Actions.Last().Target.Value;
                    break;
                case Enumerations.ControlType.NavigationMenu:
                    string attribStyle = mCPInspector.GetAttributeValue(Constants.DESCRIPTOR_TYPE_ID, "navPickCnt", "style");

                    if (attribStyle.Contains("visible")) // Only add a row in TC when the app is visible
                    {
                        ret.Screen = Constants.CP_SCREEN_MAIN;
                        ret.Control = Constants.CP_CONTROL_NAVMENU;
                        ret.Keyword = Constants.CP_KEYWORD_SELECTMENU;
                        int retryCount = 0;
                        string val = GetBreadCrumbMenuValue();
                        while (++retryCount < 3 && (val.Contains("~~") || val.StartsWith("~") || val.EndsWith("~")))
                        {
                            Thread.Sleep(1000);
                            val = GetBreadCrumbMenuValue();
                        }
                        ret.Parameters = val;
                    }
                    break;
                case Enumerations.ControlType.Menu:
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Control = Constants.CP_CONTROL_MAINMENU;
                    ret.Keyword = Constants.CP_KEYWORD_SELECTMENU;
                    ret.Parameters = GetMenuValue(Actions);
                    break;
                case Enumerations.ControlType.Toolbar:
                    if (Actions.Last().Target.Class.Contains("tbBtnHRpt"))
                    {
                        ret.Control = Constants.CP_CONTROL_REPORTMAINTOOLBAR;
                    }
                    else
                    {
                        ret.Control = Constants.CP_CONTROL_MAINTOOLBAR;
                    }
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Keyword = Constants.CP_KEYWORD_CLICKTOOLBARBUTTON;
                    ret.Parameters = GetToolbarValue(Actions);
                    break;
                case Enumerations.ControlType.Button:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_CLICK;
                    ret.Parameters = string.Empty;
                    break;
                case Enumerations.ControlType.TextBox:
                case Enumerations.ControlType.TextArea:
                    try
                    {
                        ret.Screen = Actions.Last().Screen.Name;
                    }
                    catch
                    {
                        ret.Screen = Constants.CP_SCREEN_UNKNOWN;
                    }
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    if (Actions.Last().Target.Value == Constants.CP_CUSTOM_VALUE_LOOKUP
                        || Actions.Last().Target.Value == Constants.CP_CUSTOM_VALUE_LKP)
                    {
                        ret.Keyword = Constants.CP_KEYWORD_CLICKTEXTBOXBUTTON;
                        ret.Parameters = string.Empty;
                    }
                    else
                    {
                        ret.Keyword = Constants.CP_KEYWORD_SET;
                        try
                        {
                            ret.Parameters = mView.Variables.FindAll(x => x.InUse).Any() ?
                                "O{" + mView.Variables.FindAll(x => x.InUse).First().Name + "}" : Actions.Last().Target.Value;
                        }
                        catch
                        {
                            ret.Parameters = string.Empty;
                        }
                    }
                    break;
                case Enumerations.ControlType.ComboBox:
                    ret.Screen = Actions.First().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.First().Target);
                    ret.Keyword = Constants.CP_KEYWORD_SELECT;
                    try
                    {
                        string parameters = Actions.Last().Target.Value;

                        if (ret.Screen == "Query" && parameters.Contains("&nbsp;"))
                        {
                            ret.Parameters = parameters.Split(new string[] { "&nbsp;" }, StringSplitOptions.None).First();
                        }
                        else if (ret.Screen == "DatePicker")
                        {
                            string value = mCPInspector.GetCalendarMonthValue(Actions.Last().Target.Base);
                            ret.Parameters = value;
                        }
                        else
                        {
                            ret.Parameters = parameters;
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.Tab:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);

                    ret.Keyword = Constants.CP_KEYWORD_SELECT;
                    try
                    {
                        ret.Parameters = Actions.Last().Target.Value;
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.CheckBox:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_SET;
                    try
                    {
                        ret.Parameters = Actions.Last().Target.Value;
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.Link:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_CLICK;
                    ret.Parameters = string.Empty;
                    break;
                case Enumerations.ControlType.Form:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_CLICKBUTTON;
                    try
                    {
                        ret.Parameters = Actions.Last().Target.Value;
                        switch (ret.Parameters)
                        {
                            case "Form":
                                ret.Parameters = "Table";
                                break;
                            case "Table":
                                ret.Parameters = "Form";
                                break;
                            case "Delete":
                                ret.Parameters = "Undelete";
                                break;
                            case "Undelete":
                                ret.Parameters = "Delete";
                                break;
                            case "Previous":
                                ret.Parameters = "Previous";
                                break;
                            case "First":
                                ret.Parameters = "First";
                                break;
                            case "Next":
                                ret.Parameters = "Next";
                                break;
                            case "Last":
                                ret.Parameters = "Last";
                                break;
                            case "Close":
                                ret.Keyword = Constants.CP_KEYWORD_CLOSE;
                                ret.Parameters = string.Empty;
                                break;
                            case "Restore":
                                ret.Parameters = "Maximize";
                                break;
                            case "Maximize":
                                ret.Parameters = "Restore";
                                break;
                            default:
                                break;
                        }

                        if (Actions.Last().Target.Id == "savedQueryMnu") /* set the param for Query dropdown in the Form */
                        {
                            ret.Parameters = "Query~";
                        }

                        if (Actions.Last().Target.Id == "cpyRwMnuBttn") /* set the param for Copy dropdown in the Form */
                        {
                            ret.Parameters = "Copy~";
                        }

                        if (Actions.Last().Target.Descriptor.Value.Contains("Report")) /* set the kw and param for Report Form */
                        {
                            if (Actions.Last().Target.Descriptor.Value.Contains("close"))
                            {
                                ret.Keyword = Constants.CP_KEYWORD_CLOSE;
                            }
                            else
                            {
                                if (ret.Parameters.Contains("norm"))
                                {
                                    ret.Parameters = "Maximize";
                                }
                                else
                                {
                                    ret.Parameters = "Restore";
                                }
                                ret.Keyword = Constants.CP_KEYWORD_CLICKBUTTON;
                            }
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.RadioButton:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_SELECT;
                    try
                    {
                        ret.Parameters = Actions.Last().Target.Value;
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.SearchAppResultList:
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Control = Constants.CP_CONTROL_SEARCHAPPRESULTLIST;
                    ret.Keyword = Constants.CP_KEYWORD_SELECT;
                    try
                    {
                        ret.Parameters = Actions.Last().Target.Value;
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.Calendar:
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Control = Constants.CP_CONTROL_CALENDAR;
                    if (Actions.Last().Target.Descriptor.Value.ToLower() == "caltdybtn")
                    {
                        ret.Keyword = Constants.CP_KEYWORD_CLICKTODAY;
                        ret.Parameters = string.Empty;
                    }
                    else if (Actions.Last().Target.Descriptor.Value.ToLower() == "sqlcalclose")
                    {
                        ret.Keyword = Constants.CP_KEYWORD_CLOSE;
                        ret.Parameters = string.Empty;
                    }
                    else
                    {
                        ret.Keyword = Constants.CP_KEYWORD_SELECTDATE;
                        int retry = 0;
                        string value = mCPInspector.GetCalendarDateValue(Actions.Last().Target.Base);
                        while (++retry < 5 && value == "")
                        {
                            Thread.Sleep(1000);
                            value = mCPInspector.GetCalendarDateValue(Actions.Last().Target.Base);
                        }
                        ret.Parameters = value;
                    }
                    break;
                case Enumerations.ControlType.MessageArea:
                    if (Actions.Last().Target.Descriptor.Value.ToLower() == "popupclose")
                    {
                        ret.Screen = Constants.CP_SCREEN_MAIN;
                        ret.Control = Constants.CP_CONTROL_MESSAGESAREA;
                        ret.Keyword = Constants.CP_KEYWORD_CLOSE;
                        ret.Parameters = string.Empty;
                    }
                    else if (Actions.Last().Target.Descriptor.Value.ToLower() == "closem")
                    {
                        ret.Screen = Constants.CP_SCREEN_MAIN;
                        ret.Control = Constants.CP_CONTROL_MESSAGESAREA;
                        ret.Keyword = Constants.CP_KEYWORD_CLICKMESSAGESAREABUTTON;
                        ret.Parameters = "Close";
                    }
                    else if (Actions.Last().Target.Descriptor.Value.ToLower() == "wok")
                    {
                        ret.Screen = Constants.CP_SCREEN_MAIN;
                        ret.Control = Constants.CP_CONTROL_MESSAGESAREA;
                        ret.Keyword = Constants.CP_KEYWORD_CLICKMESSAGESAREABUTTON;
                        ret.Parameters = "OK";
                    }
                    else if (Actions.Last().Target.Descriptor.Value.ToLower() == "woc")
                    {
                        ret.Screen = Constants.CP_SCREEN_MAIN;
                        ret.Control = Constants.CP_CONTROL_MESSAGESAREA;
                        ret.Keyword = Constants.CP_KEYWORD_CLICKMESSAGESAREABUTTON;
                        ret.Parameters = "Cancel";
                    }
                    else
                    {
                        return null;
                    }

                    break;
                case Enumerations.ControlType.Table:
                case Enumerations.ControlType.MultiPartTable:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    string tableID = Actions.Last().Target.Base.mElement.GetAttribute(Constants.ATTRIB_ID);
                    string columnHeader = string.Empty;
                    string row = string.Empty;
                    string actualRow = string.Empty;
                    if (!GetTableControlColumnName(Actions.Last(), out columnHeader))
                    {
                        columnHeader = mLastTableColumnHeaderAccessed;
                    }
                    if (!GetTableRowNumber(Actions.Last(), out row))
                    {
                        row = mLastTableRowAccessed;
                    }
                    actualRow = row;
                    if (mView.Variables.FindAll(x => x.InUse == true).Count > 0)
                    {
                        row = "O{" + mView.Variables.Find(x => x.InUse == true).Name + "}";
                    }

                    string className = Actions.Last().Target.Class.ToLower();

                    if (className == "cust_lu_table")
                    {
                        ret.Keyword = Constants.CP_KEYWORD_CLICKTABLECELLBUTTONBYROWCOLUMN;
                        ret.Parameters = row + DlkTestStepRecord.globalParamDelimiter + columnHeader + DlkTestStepRecord.globalParamDelimiter + "lookup";
                    }
                    /* special case for lookup table */
                    else if (Actions.Last().Screen.Name == Constants.CP_SCREEN_LOOKUP)
                    {
                        ret.Keyword = Constants.CP_KEYWORD_CLICKTABLEROWHEADER;
                        ret.Parameters = row;
                    }
                    else // regular table control
                    {
                        switch (className)
                        {
                            case "cllfrst":
                                ret.Keyword = Actions.Last().Type == Enumerations.ActionType.RightClick ? Constants.CP_KEYWORD_RIGHTCLICKTABLEROWHEADER : Constants.CP_KEYWORD_CLICKTABLEROWHEADER;
                                ret.Parameters = row;
                                break;
                            case "selall":
                                ret.Keyword = Constants.CP_KEYWORD_CLICKTABLECHECKBOX;
                                ret.Parameters = string.Empty;
                                break;
                            case "cust_tccbv":
                                ret.Control = mLastTableControlNameValue;
                                ret.Keyword = Constants.CP_KEYWORD_SETTABLECELLVALUE;
                                ret.Parameters = mLastTableRowAccessed + DlkTestStepRecord.globalParamDelimiter + mLastTableColumnHeaderAccessed + DlkTestStepRecord.globalParamDelimiter + Actions.Last().Target.Value;
                                break;
                            case "hddiv":
                            case "hddivreq":
                                if (Actions.Last().Type == Enumerations.ActionType.Click)
                                    return null;
                                ret.Keyword = Constants.CP_KEYWORD_RIGHTCLICKCOLUMNHEADER;
                                ret.Parameters = columnHeader;
                                break;
                            case "cll":
                            case "tcb":
                            case "tccbtb":
                            case "tdf":
                            case "tdfrq":
                            case "tdfro":
                            case "tdfrqnum":
                            case "tdfronum":
                            case "tdfnum":
                                ret.Keyword = Constants.CP_KEYWORD_SETTABLECELLVALUE;
                                string sLastValue = string.Empty;
                                if (className == "tcb") // Handle recorded value for checkbox in a table cell
                                {
                                    if (Actions.Last().Target.Value == "true")
                                    {
                                        sLastValue = "false";
                                    }
                                    else
                                    {
                                        sLastValue = "true";
                                    }
                                    ret.Parameters = row + DlkTestStepRecord.globalParamDelimiter + columnHeader + DlkTestStepRecord.globalParamDelimiter + sLastValue;
                                }
                                else
                                {
                                    ret.Parameters = row + DlkTestStepRecord.globalParamDelimiter + columnHeader + DlkTestStepRecord.globalParamDelimiter + Actions.Last().Target.Value;
                                }
                                break;
                        }
                    }

                    /* Cache these values specifically for multi action steps -> Combobox [click and select] */
                    mLastTableColumnHeaderAccessed = columnHeader; // cache the value
                    mLastTableRowAccessed = row; // cache the value
                    mLastTableControlNameValue = ret.Control; // cache the value
                    break;
                case Enumerations.ControlType.ConditionListBox:
                    ret.Screen = Constants.CP_SCREEN_QUERY;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_SELECT;
                    ret.Parameters = Actions.Last().Target.Value.Trim();
                    break;
                case Enumerations.ControlType.ContextMenu:
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Control = Constants.CP_CONTROL_TABLECONTEXTMENU;
                    ret.Keyword = Constants.CP_KEYWORD_SELECT;
                    ret.Parameters = Actions.Last().Target.Value.Trim();
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Verify actions in a step
        /// </summary>
        /// <param name="Actions">The list of actions to be converted into step</param>
        /// <returns>Returns a step where keyword is set to verify the identified control</returns>
        protected override Model.Step VerifyActionBlock(List<Model.Action> Actions)
        {
            Model.Step ret = new Step();
            ret.Execute = Constants.EXECUTE_DFLT_VAL;
            ret.Delay = Constants.DELAY_DFLT_VAL;
            ret.Block = Actions.Last().Block;

            // for Verify statements
            switch (Actions.First().Target.Type)
            {
                case Enumerations.ControlType.Button:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    switch (mView.VerifyType)
                    {
                        case Enumerations.VerifyType.VerifyContent:
                            ret.Parameters = Actions.Last().Target.Value;
                            break;
                        case Enumerations.VerifyType.VerifyExists:
                            ret.Parameters = "TRUE";
                            break;
                        case Enumerations.VerifyType.VerifyReadOnly:
                            ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                            break;
                        default:
                            ret.Parameters = string.Empty;
                            this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                            break;
                    }
                    break;
                case Enumerations.ControlType.TextBox:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    switch (mView.VerifyType)
                    {
                        case Enumerations.VerifyType.VerifyContent:
                            ret.Parameters = Actions.Last().Target.Value;
                            break;
                        case Enumerations.VerifyType.VerifyExists:
                            ret.Parameters = "TRUE";
                            break;
                        case Enumerations.VerifyType.VerifyReadOnly:
                            ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                            break;
                        default:
                            ret.Parameters = string.Empty;
                            this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                            break;
                    }
                    break;
                case Enumerations.ControlType.TextArea:
                    try
                    {
                        ret.Screen = Actions.Last().Screen.Name;
                    }
                    catch
                    {
                        ret.Screen = Constants.CP_SCREEN_UNKNOWN;
                    }
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    try
                    {
                        switch (mView.VerifyType)
                        {
                            case Enumerations.VerifyType.VerifyContent:
                                ret.Parameters = Actions.Last().Target.Value;
                                break;
                            case Enumerations.VerifyType.VerifyExists:
                                ret.Parameters = "TRUE";
                                break;
                            case Enumerations.VerifyType.VerifyReadOnly:
                                ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                                break;
                            default:
                                ret.Parameters = string.Empty;
                                this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                                break;
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.ComboBox:
                    ret.Screen = Actions.First().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.First().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    try
                    {
                        switch (mView.VerifyType)
                        {
                            case Enumerations.VerifyType.VerifyContent:
                                ret.Parameters = Actions.Last().Target.Value;
                                break;
                            case Enumerations.VerifyType.VerifyExists:
                                ret.Parameters = "TRUE";
                                break;
                            case Enumerations.VerifyType.VerifyReadOnly:
                                ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                                break;
                            default:
                                ret.Parameters = string.Empty;
                                this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                                break;
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.ComboBoxItem:
                    ret.Screen = Actions.First().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.First().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    try
                    {
                        switch (mView.VerifyType)
                        {
                            case Enumerations.VerifyType.VerifyContent:
                                ret.Parameters = Actions.Last().Target.Value;
                                break;
                            case Enumerations.VerifyType.VerifyExists:
                                ret.Parameters = "TRUE";
                                break;
                            case Enumerations.VerifyType.VerifyReadOnly:
                                ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                                break;
                            default:
                                ret.Parameters = string.Empty;
                                this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                                break;
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.CheckBox:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    try
                    {
                        switch (mView.VerifyType)
                        {
                            case Enumerations.VerifyType.VerifyContent:
                                ret.Parameters = Actions.Last().Target.Value;
                                break;
                            case Enumerations.VerifyType.VerifyExists:
                                ret.Parameters = "TRUE";
                                break;
                            case Enumerations.VerifyType.VerifyReadOnly:
                                ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                                break;
                            default:
                                ret.Parameters = string.Empty;
                                this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                                break;
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.Link:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    switch (mView.VerifyType)
                    {
                        case Enumerations.VerifyType.VerifyContent:
                            ret.Parameters = Actions.Last().Target.Value;
                            break;
                        case Enumerations.VerifyType.VerifyExists:
                            ret.Parameters = "TRUE";
                            break;
                        case Enumerations.VerifyType.VerifyReadOnly:
                            ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                            break;
                        default:
                            ret.Parameters = string.Empty;
                            this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                            break;
                    }
                    break;
                case Enumerations.ControlType.RadioButton:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    try
                    {
                        switch (mView.VerifyType)
                        {
                            case Enumerations.VerifyType.VerifyContent:
                                ret.Parameters = Actions.Last().Target.Value;
                                break;
                            case Enumerations.VerifyType.VerifyExists:
                                ret.Parameters = "TRUE";
                                break;
                            case Enumerations.VerifyType.VerifyReadOnly:
                                ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                                break;
                            default:
                                ret.Parameters = string.Empty;
                                this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                                break;
                        }
                    }
                    catch
                    {
                        ret.Parameters = string.Empty;
                    }
                    break;
                case Enumerations.ControlType.MessageArea:
                    if ((!Actions.Last().Target.Descriptor.Value.ToLower().Contains("msg") && !Actions.Last().Target.Descriptor.Value.ToLower().Contains("elnk"))
                        || mView.VerifyType == Model.Enumerations.VerifyType.VerifyReadOnly)
                    {
                        return null;
                    }
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Control = Constants.CP_CONTROL_MESSAGESAREA;
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    switch (mView.VerifyType)
                    {
                        case Enumerations.VerifyType.VerifyContent:
                            ret.Parameters = Actions.Last().Target.Value.Trim() + DlkTestStepRecord.globalParamDelimiter + "TRUE";
                            break;
                        case Enumerations.VerifyType.VerifyExists:
                            ret.Parameters = "TRUE";
                            break;
                        case Enumerations.VerifyType.VerifyReadOnly:
                            ret.Parameters = GetVerifyReadOnlyParameter(Actions.Last().Target);
                            break;
                        default:
                            ret.Parameters = string.Empty;
                            this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                            break;
                    }
                    break;
                case Enumerations.ControlType.Form:
                    if (mView.VerifyType == Model.Enumerations.VerifyType.VerifyReadOnly && !Actions.Last().Target.Descriptor.Value.Contains("Bttn")
                           && !Actions.Last().Target.Descriptor.Value.StartsWith("rs"))
                    {
                        return null;
                    }
                    else if (Actions.Last().Target.Descriptor.Value.Contains("Bttn")  || Actions.Last().Target.Descriptor.Value.StartsWith("rs")) // To handle Form buttons read only state
                    {
                        if (mView.VerifyType == Enumerations.VerifyType.VerifyExists)
                        {
                            ret.Screen = Actions.Last().Screen.Name;
                            ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                            ret.Keyword = "VerifyButtonExists";
                            ret.Parameters = Actions.Last().Target.Value + "|" + "TRUE";
                            break;
                        }
                        else if (mView.VerifyType == Enumerations.VerifyType.VerifyReadOnly)
                        {
                            ret.Screen = Actions.Last().Screen.Name;
                            ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                            ret.Keyword = "VerifyButtonReadOnly";
                            string ctrlID = Actions.Last().Target.Id;
                            string readOnly = GetControlAttributeValue(Actions.Last().Target, "dis");
                            if (readOnly == null)
                            {
                                string isDisabled = GetControlAttributeValue(Actions.Last().Target, "class"); // For CP 701 controls
                                if (isDisabled.Contains("Disabled"))
                                {
                                    readOnly = "1";
                                }
                                else
                                {
                                    readOnly = "0";
                                }
                            }
                            if (readOnly == "1")
                            {
                                ret.Parameters = Actions.Last().Target.Value + "|" + "TRUE";
                            }
                            else
                            {
                                ret.Parameters = Actions.Last().Target.Value + "|" + "FALSE";
                            }
                            break;
                        }
                    }
                    else if (Actions.Last().Target.Base.mElement.TagName.Equals("input") || Actions.Last().Target.Base.mElement.FindElements(By.TagName("input")).Count > 0) // To handle Form checkbox read only state
                    {
                        //Check if target base input element is a checkbox
                        bool isInputCheckBox = false;
                        IWebElement targetInput = null;
                        string targetParamValue = "";
                        string inputType = GetControlAttributeValue(Actions.Last().Target, "type");
                        if (inputType == null) //If current target base is not a checkbox, check child input
                        {
                            if (Actions.Last().Target.Base.mElement.FindElements(By.TagName("input")).Count > 0)
                            {
                                targetInput = Actions.Last().Target.Base.mElement.FindElement(By.TagName("input"));
                                inputType = targetInput.GetAttribute("type") != null ? targetInput.GetAttribute("type") : null;

                                if (inputType.ToLower().Contains("checkbox"))
                                {
                                    isInputCheckBox = true;
                                    targetParamValue = targetInput.GetAttribute("title") != null ? targetInput.GetAttribute("title") : Actions.Last().Target.Value; //Get value title of child input checkbox
                                }
                            }
                        }
                        else if (inputType.ToLower().Contains("checkbox")) //Verify if current target base is a checkbox
                        {
                            isInputCheckBox = true;
                            targetParamValue = Actions.Last().Target.Value;
                        }

                        if (isInputCheckBox)
                        {
                            if(targetInput == null)
                            {
                                targetInput = Actions.Last().Target.Base.mElement;
                            }

                            ret.Screen = Actions.Last().Screen.Name;
                            ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                            ret.Keyword = "VerifyCheckBoxValue";

                            bool checkBoxState = Convert.ToBoolean(targetInput.GetAttribute("checked")); //Get checkbox state by retrieving checked attribute
                            if (checkBoxState)
                            {
                                ret.Parameters = targetParamValue + "|" + "TRUE";
                            }
                            else
                            {
                                ret.Parameters = targetParamValue + "|" + "FALSE";
                            }
                            break;
                        }
                    }

                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    ret.Parameters = mView.VerifyType == Model.Enumerations.VerifyType.VerifyContent ? Actions.First().Target.Value.Trim() : "TRUE";
                    break;
                case Enumerations.ControlType.Tab:
                    if (mView.VerifyType == Model.Enumerations.VerifyType.VerifyExists)
                    {
                        ret.Screen = Actions.Last().Screen.Name;
                        ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                        ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                        ret.Parameters = "TRUE";
                        break;
                    }
                    else
                    {
                        return null;
                    }
                case Enumerations.ControlType.Table:
                case Enumerations.ControlType.MultiPartTable:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    string columnHeader = string.Empty;
                    string row = string.Empty;
                    string actualRow = string.Empty;
                    if (!GetTableControlColumnName(Actions.Last(), out columnHeader))
                    {
                        columnHeader = mLastTableColumnHeaderAccessed;
                    }
                    if (!GetTableRowNumber(Actions.Last(), out row))
                    {
                        row = mLastTableRowAccessed;
                    }
                    actualRow = row;
                    if (mView.Variables.FindAll(x => x.InUse == true).Count > 0)
                    {
                        row = "O{" + mView.Variables.Find(x => x.InUse == true).Name + "}";
                    }
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    switch (mView.VerifyType)
                    {
                        case Enumerations.VerifyType.VerifyContent:
                            ret.Parameters = row + DlkTestStepRecord.globalParamDelimiter + columnHeader + DlkTestStepRecord.globalParamDelimiter + Actions.Last().Target.Value;
                            break;
                        case Enumerations.VerifyType.VerifyExists:
                            ret.Parameters = "TRUE";
                            break;
                        case Enumerations.VerifyType.VerifyReadOnly:
                            string isReadOnly = GetVerifyReadOnlyParameter(Actions.First().Target);
                            ret.Parameters = row + DlkTestStepRecord.globalParamDelimiter + columnHeader + DlkTestStepRecord.globalParamDelimiter + isReadOnly;
                            break;
                        default:
                            ret.Parameters = string.Empty;
                            this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                            break;
                    }
                    break;
                case Enumerations.ControlType.ConditionListBox:
                    ret.Screen = Constants.CP_SCREEN_QUERY;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = SetVerifyKeyword(Actions.First().Target.Type);
                    switch (mView.VerifyType)
                    {
                        case Enumerations.VerifyType.VerifyContent:
                            ret.Parameters = Actions.Last().Target.Value.Trim();
                            break;
                        case Enumerations.VerifyType.VerifyExists:
                            ret.Parameters = "TRUE";
                            break;
                        default:
                            ret.Parameters = string.Empty;
                            this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                            break;
                    }
                    break;
                default:
                    ret.Screen = Actions.Last().Screen.Name;
                    ret.Control = GetControlName(ret.Screen, Actions.Last().Target);
                    ret.Keyword = Constants.CP_KEYWORD_VERIFYTEXT;
                    ret.Parameters = Actions.Last().Target.Value;
                    this.mView.ViewStatus = Enumerations.ViewStatus.VerifyNotSupported;
                    break;
            }
            // Reset the verify mode every time.
            this.mView.ResetVerifyMode();
            return ret;
        }

        /// <summary>
        /// Assign values to actions in a step
        /// </summary>
        /// <param name="Actions">The list of actions in the step</param>
        /// <returns>Returns a step where keyword is set to assign values to identified control</returns>
        protected override Model.Step AssignValueActionBlock(List<Model.Action> Actions)
        {
            Model.Step ret = new Step();
            Model.Action lastAction = Actions.Last();
            bool assignPartial = mView.GetValueType == Enumerations.GetValueType.AssignPartialValue;
            ret.Execute = Constants.EXECUTE_DFLT_VAL;
            ret.Delay = Constants.DELAY_DFLT_VAL;
            ret.Block = lastAction.Block;

            // for Assign Value statements
            switch (Actions.First().Target.Type)
            {
                case Enumerations.ControlType.MessageArea:
                    if (!lastAction.Target.Descriptor.Value.ToLower().Contains("msg"))
                    {
                        return null;
                    }
                    string msgs = mCPInspector.GetMessages(lastAction.Target.Base);
                    ret.Screen = Constants.CP_SCREEN_MAIN;
                    ret.Control = Constants.CP_CONTROL_MESSAGESAREA;
                    ret.Keyword = assignPartial ? Constants.CP_KEYWORD_ASSIGNPARTIALVALUE : Constants.CP_KEYWORD_ASSIGNVALUETOVARIABLE;
                    AddVariable av2 = new AddVariable(msgs, Enumerations.ControlType.MessageArea, (Window)this.mView);
                    if (!((bool)av2.ShowDialog()))
                    {
                        return null;
                    }
                    mView.VariablesUpdated();
                    Variable myVar2 = mView.Variables.Find(x => x.InUse == true);
                    ret.Parameters = assignPartial ? (myVar2.Name + DlkTestStepRecord.globalParamDelimiter + myVar2.StartIndex + DlkTestStepRecord.globalParamDelimiter + myVar2.Length) : myVar2.Name;
                    myVar2.InUse = false;
                    break;
                case Enumerations.ControlType.Table:
                case Enumerations.ControlType.MultiPartTable:
                    ret.Screen = lastAction.Screen.Name;
                    ret.Control = GetControlName(ret.Screen, lastAction.Target);
                    string columnHeader = string.Empty;
                    string row = string.Empty;
                    string actualRow = string.Empty;
                    if (!GetTableControlColumnName(lastAction, out columnHeader))
                    {
                        columnHeader = mLastTableColumnHeaderAccessed;
                    }
                    if (!GetTableRowNumber(lastAction, out row))
                    {
                        row = mLastTableRowAccessed;
                    }
                    actualRow = row;
                    if (mView.Variables.FindAll(x => x.InUse == true).Count > 0)
                    {
                        row = "O{" + mView.Variables.Find(x => x.InUse == true).Name + "}";
                    }

                    AddVariable av = new AddVariable(actualRow, Enumerations.ControlType.Table, (Window)this.mView);
                    if (!((bool)av.ShowDialog()))
                    {
                        return null;
                    }
                    mView.VariablesUpdated();
                    ret.Keyword = Constants.CP_KEYWORD_GETTABLEROWWITHCOLUMNVALUE;
                    if (mView.Variables.FindAll(x => x.InUse == true).Count > 0)
                    {
                        Variable myVarTable = mView.Variables.Find(x => x.InUse == true);
                        row = myVarTable.Name;
                        myVarTable.InUse = false;
                    }
                    ret.Parameters = columnHeader + DlkTestStepRecord.globalParamDelimiter + lastAction.Target.Value + DlkTestStepRecord.globalParamDelimiter + row;

                    /* Cache these values specifically for multi action steps -> Combobox [click and select] */
                    mLastTableColumnHeaderAccessed = columnHeader; // cache the value
                    mLastTableRowAccessed = row; // cache the value
                    mLastTableControlNameValue = ret.Control; // cache the value
                    break;
                default:
                    ret.Screen = lastAction.Screen.Name;
                    ret.Control = GetControlName(ret.Screen, lastAction.Target);

                    AddVariable av1 = new AddVariable(lastAction.Target.Value, Enumerations.ControlType.TextBox, (Window)this.mView);
                    if (!((bool)av1.ShowDialog()))
                    {
                        return null;
                    }
                    mView.VariablesUpdated();
                    ret.Keyword = assignPartial ? Constants.CP_KEYWORD_ASSIGNPARTIALVALUE : Constants.CP_KEYWORD_ASSIGNVALUETOVARIABLE;
                    Variable myVar1 = mView.Variables.Find(x => x.InUse == true);
                    ret.Parameters = assignPartial ? (myVar1.Name + DlkTestStepRecord.globalParamDelimiter + myVar1.StartIndex + DlkTestStepRecord.globalParamDelimiter + myVar1.Length) : myVar1.Name;

                    myVar1.InUse = false;
                    break;
            }
            return ret;
        }
        protected override void InterpretAction(DlkBaseControl control, DlkCapturedStep step, Enumerations.ActionType actionType)
        {
            string className = string.IsNullOrEmpty(step.ElementClass) ? "" : step.ElementClass;
            string id = string.IsNullOrEmpty(step.ElementId) ? "" : step.ElementId;

            // special case in loginpage, elements have no class
            if (className.Equals(""))
            {
                switch (id)
                {
                    case "settings":
                    case "additionalCriteria":
                        className = id;
                        break;
                }
            }

            mAction = new Model.Action
            {
                Type = actionType,
                Target = new Model.Control
                {
                    Type = GetControlType(className, GetAdditionalInfo(control, className)),
                    AncestorFormIndex = GetAncestorFormIndex(control, className, step.AncestorFormIndex),
                    Base = control,
                    PopRefId = step.PopRefId
                },
                AlertResponse = false
            };
            //mAction.Target.Value = bNotActual ? GetValue(myControl, mAction.Target.Type) : String.IsNullOrEmpty(mCurrentStep.Item2) ? GetValue(myControl, mAction.Target.Type) : mCurrentStep.Item2;
            if (className == "cust_lu_table" || (className == "drillDownPathText" && step.PopRefId != string.Empty))
            {
                mAction.Screen = new Screen
                {
                    Name = GetScreen(mAction.Target.Type, className, step) ?? string.Empty
                };
            }
            if (className == "popupClose" && id != string.Empty)
            {
                mAction.Screen = new Screen
                {
                    Name = GetScreen(mAction.Target.Type = Enumerations.ControlType.Button, id, step) ?? string.Empty
                };
            }
            else
            {
                mAction.Screen = new Screen
                {
                    Name = GetScreen(mAction.Target.Type, mAction.Target.Type == Enumerations.ControlType.Table
                    ? Constants.CP_CONTROL_TYPE_TABLE : id, step) ?? string.Empty
                };
            }
            if ((mAction.Screen.Name == "Query" && id.Contains("__img")) || (mAction.Screen.Name == "DatePicker" && id.Contains("__img"))) /* truncate __img so that correct control can be found */
            {
                id = id.Replace("__img", "");
            }
            if (mAction.Screen.Name == "FileUploadManager" && id == "fileUpldFile") /* set the control type for FileUploadManager File Name ctrl */
            {
                mAction.Target.Type = Enumerations.ControlType.Button;
            }
            else if (mAction.Screen.Name == Constants.CP_SCREEN_BROWSEFILEPOPUP && id == "browseEdit") /* set the control type for BrowseFilePopup */
            {
                mAction.Target.Type = Enumerations.ControlType.Button;
            }

            mAction.Target.Descriptor = GetDescriptor(control, mAction.Screen.Name, id, className);
            mAction.Target.Value = className.Contains("cust_lu") ? Constants.CP_CUSTOM_VALUE_LOOKUP : step.ElementTxt;
            mAction.Target.Id = id;
            mAction.Target.Class = className;

            if (mAction.Target.Type == Enumerations.ControlType.Table)
            {
                mAction.Target.Descriptor.Type = Enumerations.DescriptorType.Class;
                mAction.Target.Descriptor.Value = className + "~" + mAction.Target.Descriptor.Value;
            }

            mAction.Block = GetBlock(mAction);
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="View"></param>
        public CP_MainPresenter(IMainView View, bool LoadInitialObjects=true)
        {
            this.mView = View;
            mCPInspector = mInspectorService as CP_Inspector;
            if (LoadInitialObjects)
            {
                mMainControls = GetControlsFromFiles(Constants.CP_SCREEN_MAIN);
                mQueryControls = GetControlsFromFiles(Constants.CP_SCREEN_QUERY);
                mPageSetUpControls = GetControlsFromFiles(Constants.CP_SCREEN_PAGESETUP);
                mFileUploadMgrControls = GetControlsFromFiles(Constants.CP_SCREEN_FILEUPLOADMANAGER);
                mPrintOptionsControls = GetControlsFromFiles(Constants.CP_SCREEN_PRINTOPTIONS);
                mProcessProgControls = GetControlsFromFiles(Constants.CP_SCREEN_PROCESSPROGRESS);
                mSelectCompanyControls = GetControlsFromFiles(Constants.CP_SCREEN_SELECTCOMPANY);
                mUserPreferencesControls = GetControlsFromFiles(Constants.CP_SCREEN_USERPREFERENCES);
                mSubmitBatchJobControls = GetControlsFromFiles(Constants.CP_SCREEN_SUBMITBATCHJOB);
                mBrowseFilePopupControls = GetControlsFromFiles(Constants.CP_SCREEN_BROWSEFILEPOPUP);
                mReportControls = GetControlsFromFiles(Constants.CP_SCREEN_REPORT);
            }
        }
        public override bool IgnoreNewStep(Step NewStep)
        {
            return (NewStep == null || NewStep.Screen == Model.Constants.CP_SCREEN_UNKNOWN || string.IsNullOrEmpty(NewStep.Screen)
                    || ((string.IsNullOrEmpty(NewStep.Control) || NewStep.Control == Model.Constants.CP_CONTROL_UNKNOWN)
                    && NewStep.Screen != Model.Constants.CP_SCREEN_DIALOG));
        }
        /// <summary>
        /// Get control value
        /// </summary>
        /// <param name="control">Name of the control</param>
        /// <param name="type">Type of the control</param>
        /// <returns>Returns the value of the specified control name and type</returns>
        public override string GetValue(DlkBaseControl control, Enumerations.ControlType type, string jsId = "", string jsClass = "", string jsValue = "")
        {
            string ret = string.Empty;
            try
            {
                switch (type)
                {
                    case Enumerations.ControlType.CheckBox:
                    case Enumerations.ControlType.RadioButton:
                        Thread.Sleep(100);
                        string chked = control.GetAttributeValue(Constants.ATTRIB_CHECKED);
                        ret = chked ?? "false";
                        break;
                    case Enumerations.ControlType.ContextMenu:
                    case Enumerations.ControlType.Menu:
                        string cls = string.IsNullOrEmpty(jsClass) ? control.GetAttributeValue(Constants.ATTRIB_CLASS) : jsClass;
                        switch (cls)
                        {
                            case "wMnuPick":
                                ret = mCPInspector.GetControlViaXPath("./*[@class='wMnuPickLbl']", control).GetValue();
                                break;
                            case "tlbrDDActionDiv":
                                ret = mCPInspector.GetControlViaXPath("./*[@class='tlbrDDItem']", control).GetValue();
                                break;
                            default:
                                ret = control.GetValue();
                                break;
                        }
                        break;
                    case Enumerations.ControlType.Form:
                        string id = string.IsNullOrEmpty(jsId) ? control.GetAttributeValue(Constants.ATTRIB_ID) : jsId;
                        if (id.Contains("rsDragger"))
                        {
                            if (mIsVerify)
                            {
                                ret = mCPInspector.GetControlViaXPath("./..//*[contains(@class,'HdrLabel')]", control).GetValue().Replace("&nbsp;", " ");
                            }
                        }
                        else if (id.Contains("Bttn"))
                        {
                            Thread.Sleep(500);
                            switch (id)
                            {
                                case "frstBttn":
                                    ret = "First";
                                    break;
                                case "prvsBttn":
                                    ret = "Previous";
                                    break;
                                case "nxtBttn":
                                    ret = "Next";
                                    break;
                                case "lstBttn":
                                    ret = "Last";
                                    break;
                                case "newExistBttn":
                                    ret = "Toggle View";
                                    break;
                                default:
                                    ret = control.GetValue();
                                    break;
                            }
                        }
                        else if (id == "savedQueryMnu")
                        {
                            ret = "Query";
                        }
                        else
                        {
                            switch (id)
                            {
                                case "rsCls":
                                case "rsClsImg":
                                    ret = "Close";
                                    break;
                                case "rsMax":
                                case "rsMin":
                                case "rsMaxImg":
                                case "SEL_CD":
                                case "rsDock":
                                case "rsDockImg":
                                case "rsOnTop":
                                    ret = control.GetAttributeValue(Constants.ATTRIB_TITLE);
                                    break;
                                default:
                                    ret = control.GetValue();
                                    break;
                            }
                        }
                        break;
                    case Enumerations.ControlType.Toolbar:
                        string clsTb = string.IsNullOrEmpty(jsClass) ? control.GetAttributeValue(Constants.ATTRIB_CLASS) : jsClass;
                        ret = clsTb == "tlbrDDActionDiv" ? mCPInspector.GetControlViaXPath("./*[@class='tlbrDDItem']", control).GetValue() : control.GetValue();
                        break;
                    case Enumerations.ControlType.ComboBoxItem:
                        // for language comboboxitem that gets stale after login page is reloaded
                        if (control.IsElementStale()
                            && DlkEnvironment.AutoDriver.FindElements(By.XPath("//input[@id='loginBtn']")).Count > 0
                            && (jsValue.Equals("EN") || jsValue.Equals("FR") || jsValue.Equals("ES") || jsValue.Equals("DE") || jsValue.Equals("NL")))
                        {
                            ret = jsValue.Equals("FR") ? "Français" :
                                jsValue.Equals("ES") ? "Español" :
                                jsValue.Equals("DE") ? "Deutsch" :
                                jsValue.Equals("NL") ? "Dutch" :
                                jsValue.Equals("EN") ? "English" :
                                string.Empty;
                        }
                        else
                        {
                            ret = control.GetAttributeValue("text").Split('<').First();
                        }
                        break;
                    case Enumerations.ControlType.Table:
                        string clsTable = string.IsNullOrEmpty(jsClass) ? control.GetAttributeValue(Constants.ATTRIB_CLASS) : jsClass;
                        string tableID = control.GetAttributeValue(Constants.ATTRIB_ID);
                        switch (clsTable.ToLower())
                        {
                            case "tcb":
                                string chkedTable = control.GetAttributeValue(Constants.ATTRIB_CHECKED);
                                ret = chkedTable ?? "false";
                                break;
                            default:
                                ret = string.IsNullOrEmpty(jsValue) ? control.GetValue() : jsValue;
                                break;
                        }
                        break;
                    default:
                        ret = control.GetValue();
                        break;
                }
            }
            catch
            {
                ret = string.Empty;
            }
            return ret;
        }
        public override void AutoLogin(DlkLoginConfigHandler loginInfo)
        {
            CostpointLib.System.DlkCostpointFunctionHandler.ExecuteFunction(
                Model.Constants.CP_SCREEN_CP7LOGIN, string.Empty, Model.Constants.CP_FUNCTION_LOGIN, 
                new String[] { loginInfo.mUser, loginInfo.mPassword, loginInfo.mDatabase });
        }
        #endregion
    }
}
