using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using KnowledgePointLib.DlkControls;

namespace KnowledgePointLib.DlkSystem
{
    public static class DlkKPKeywordHandler
    {

        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkBreadcrumb mBreadcrumb;
            DlkButton mButton;
            DlkCard mCard;
            DlkCheckbox mCheckbox;
            DlkDatePicker mDatePicker;
            DlkDropdownMenu mDropdownMenu;
            DlkDropdownMultiSelect mDropdownMultiSelect;
            DlkExpansionPanel mExpansionPanel;
            DlkGrid mGrid;
            DlkImage mImage;
            DlkLabel mLabel;
            DlkMap mMap;
            DlkMenu mMenu;
            DlkLink mLink;
            DlkSnackBar mSnackBar;
            DlkTab mTab;
            DlkTable mTable ;
            DlkTextArea mTextArea;
            DlkTextBox mTextBox;
            DlkTile mTile;
            DlkToggle mToggle;
            DlkTreeView mTreeView;
            DlkWidget mWidget;
            DlkRadioButton mRadioButton;
            DlkList mList;
            DlkRecord mRecord;


            switch (mControl.mControlType.ToLower())
            {
                case "breadcrumb":
                    mBreadcrumb = new DlkBreadcrumb(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mBreadcrumb, Parameters, Keyword);
                    break;
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mButton, Parameters, Keyword);
                    break;
                case "checkbox":
                    mCheckbox = new DlkCheckbox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckbox, Parameters, Keyword);
                    break;
                case "card":
                    mCard = new DlkCard(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCard, Parameters, Keyword);
                    break;
                case "datepicker":
                    mDatePicker = new DlkDatePicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDatePicker, Parameters, Keyword);
                    break;
                case "dropdownmenu":
                    mDropdownMenu = new DlkDropdownMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDropdownMenu, Parameters, Keyword);
                    break;
                case "dropdownmultiselect":
                    mDropdownMultiSelect = new DlkDropdownMultiSelect(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDropdownMultiSelect, Parameters, Keyword);
                    break;
                case "expansionpanel":
                    mExpansionPanel = new DlkExpansionPanel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mExpansionPanel, Parameters, Keyword);
                    break;
                case "grid":
                    mGrid = new DlkGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mGrid, Parameters, Keyword);
                    break;
                case "image":
                    mImage = new DlkImage(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mImage, Parameters, Keyword);
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLabel, Parameters, Keyword);
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextBox, Parameters, Keyword);
                    break;
                case "treeview":
                    mTreeView = new DlkTreeView(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTreeView, Parameters, Keyword);
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLink, Parameters, Keyword);
                    break;
                case "menu":
                    mMenu = new DlkMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mMenu, Parameters, Keyword);
                    break;
                case "map":
                    mMap = new DlkMap(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mMap, Parameters, Keyword);
                    break;
                case "snackbar":
                    mSnackBar = new DlkSnackBar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mSnackBar, Parameters, Keyword);
                    break;
                case "tab":
                    mTab = new DlkTab(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTab, Parameters, Keyword);
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTable, Parameters, Keyword);
                    break;
                case "tile":
                    mTile = new DlkTile(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTile, Parameters, Keyword);
                    break;
                case "textarea":
                    mTextArea = new DlkTextArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextArea, Parameters, Keyword);
                    break;
                case "toggle":
                    mToggle = new DlkToggle(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mToggle, Parameters, Keyword);
                    break;
                case "widget":
                     mWidget = new DlkWidget(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mWidget, Parameters, Keyword);
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButton, Parameters, Keyword);
                    break;
                case "list":
                    mList = new DlkList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mList, Parameters, Keyword);
                    break;
                case "record":
                    mRecord = new DlkRecord(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRecord, Parameters, Keyword);
                    break;
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }
        private static void ExecuteKeyword(object mObject, String[] Parameters, String Keyword)
        {
            MethodInfo mi;

            switch (Parameters.Length)
            {
                case 1:
                    if (Parameters[0] == "")
                    {
                        try
                        {
                            mi = mObject.GetType().GetMethod(Keyword, new Type[] { });
                            mi.Invoke(mObject, null);
                        }
                        catch
                        {
                            mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                            mi.Invoke(mObject, new[] { Parameters[0] });
                        }
                    }
                    else
                    {
                        mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                        mi.Invoke(mObject, new[] { Parameters[0] });
                    }
                    break;
                case 2:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1] });
                    break;
                case 3:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1], Parameters[2] });
                    break;
                case 4:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                    break;
                default:
                    throw new Exception("Unsupported keyword: " + Keyword);
            }
        }
    }
}
