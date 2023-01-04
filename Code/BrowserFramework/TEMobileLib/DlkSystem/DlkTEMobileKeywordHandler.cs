using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using TEMobileLib.DlkControls;

namespace TEMobileLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkTEMobileKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkTextBox mTextBox;
            DlkNavigationMenu mNavMenu;
            DlkLink mLink;
            DlkTab mTab;
            DlkForm mForm;
            DlkToolbar mToolbar;
            DlkCheckBox mCheckBox;
            DlkMessageArea mMessageArea;
            DlkComboBox mComboBox;
            DlkCalendar mCalendar;
            DlkLabel mLabel;
            DlkMenu mMenu;
            DlkRadioButton mRadioButton;
            DlkRadioButtonGroup mRadioButtonGroup;
            DlkReportBody mReportBody;
            DlkTable mTable;
            DlkMultiPartTable mMultiPartTable;
            DlkTextArea mTextArea;
            DlkContextMenu mContextMenu;
            DlkFolderTree mFolderTree;
            DlkSearchAppResultList mAppSearchList;
            DlkListBox mList;
            DlkInfoTip mInfoTip;
            DlkWebGrid mWebGrid;
            DlkSection mSection;
            DlkTimesheetList mTimesheetList;
            DlkCardList mCardList;
            DlkDashpart mDashpart;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mButton, Parameters, Keyword);
                    break;                
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mTextBox, Parameters, Keyword);
                    break;
                case "navigationmenu":
                    mNavMenu = new DlkNavigationMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mNavMenu, Parameters, Keyword);
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mLink, Parameters, Keyword);
                    break;
                case "tab":
                    mTab = new DlkTab (mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mTab, Parameters, Keyword);
                    break;
                case "form":
                    mForm = new DlkForm(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mForm, Parameters, Keyword);
                    break;
                case "toolbar":
                    mToolbar = new DlkToolbar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mToolbar, Parameters, Keyword);
                    break;
                case "checkbox":
                    mCheckBox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mCheckBox, Parameters, Keyword);
                    break;
                case "messagearea":
                    mMessageArea = new DlkMessageArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mMessageArea, Parameters, Keyword);
                    break;
                case "combobox":
                    mComboBox = new DlkComboBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mComboBox, Parameters, Keyword);
                    break;
                case "calendar":
                    mCalendar = new DlkCalendar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mCalendar, Parameters, Keyword);
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mLabel, Parameters, Keyword);
                    break;
                case "menu":
                    mMenu = new DlkMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mMenu, Parameters, Keyword);
                    break;
                case "multiparttable":
                    mMultiPartTable = new DlkMultiPartTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mMultiPartTable, Parameters, Keyword);
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mRadioButton, Parameters, Keyword);
                    break;
                case "radiobuttongroup":
                    mRadioButtonGroup = new DlkRadioButtonGroup(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mRadioButtonGroup, Parameters, Keyword);
                    break;
                case "reportbody":
                    mReportBody = new DlkReportBody(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mReportBody, Parameters, Keyword);
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mTable, Parameters, Keyword);
                    break;
                case "textarea":
                    mTextArea = new DlkTextArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mTextArea, Parameters, Keyword);
                    break;
                case "contextmenu":
                    mContextMenu = new DlkContextMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mContextMenu, Parameters, Keyword);
                    break;
                case "foldertree":
                    mFolderTree = new DlkFolderTree(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mFolderTree, Parameters, Keyword);
                    break;
                case "searchappresultlist":
                    mAppSearchList = new DlkSearchAppResultList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mAppSearchList, Parameters, Keyword);
                    break;
                case "listbox":
                    mList = new DlkListBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mList, Parameters, Keyword);
                    break;
                case "infotip":
                    mInfoTip = new DlkInfoTip(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mInfoTip, Parameters, Keyword);
                    break;
                case "webgrid":
                    mWebGrid = new DlkWebGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mWebGrid, Parameters, Keyword);
                    break;
                case "section":
                    mSection = new DlkSection(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mSection, Parameters, Keyword);
                    break;
                case "timesheetlineslist":
                    mTimesheetList = new DlkTimesheetList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mTimesheetList, Parameters, Keyword);
                    break;
                case "cardlist":
                    mCardList = new DlkCardList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mCardList, Parameters, Keyword);
                    break;
                case "dashpart":
                    mDashpart = new DlkDashpart(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    InvokeKeyword(mDashpart, Parameters, Keyword);
                    break;
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }

        private static void InvokeKeyword(object mObject, String[] Parameters, String Keyword)
        {
            MethodInfo method;

            int paramCount = Parameters.Length;

            if(paramCount < 1) throw new Exception("Unsupported keyword: " + Keyword);

            else if(paramCount == 1)
            {
                if (Parameters[0] == "")
                {
                    try
                    {
                        method = mObject.GetType().GetMethod(Keyword, new Type[] { });
                        method.Invoke(mObject, null);
                    }
                    catch
                    {
                        method = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                        method.Invoke(mObject, new[] { Parameters[0] });
                    }
                }
                else
                {
                    method = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                    method.Invoke(mObject, new[] { Parameters[0] });
                }
            }
            else
            {
                var types = Enumerable.Repeat(typeof(String), paramCount).ToArray();
                method = mObject.GetType().GetMethod(Keyword, types);
                method.Invoke(mObject, Parameters);
            }
        }
    }
}
