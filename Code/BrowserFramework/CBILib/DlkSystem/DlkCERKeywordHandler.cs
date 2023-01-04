using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CBILib.DlkControls;
using System.Reflection;

namespace CBILib.DlkSystem
{
    public static class DlkCERKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkCheckbox mCheckbox;
            DlkCheckboxList mCheckboxList;
            DlkCheckboxSearch mCheckboxSearch;
            DlkDropdown mDropdown;
            DlkLabel mLabel;
            DlkLink mLink;
            DlkMenu mMenu;
            DlkRadioButton mRadioButton;
            DlkRadioButtonSearch mRadioButtonSearch;
            DlkReport mReport;
            DlkTextBox mTextBox;
            DlkListView mListView;
            DlkListBoxSearch mListBoxSearch;
            DlkRadioButtonGroup mRadioButtonGroup;
            DlkCalendar mCalendar;
            DlkReportTable mReportTable;
            DlkListBox mListBox;
            DlkDashboardFilter mDashboardFilter;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mButton, Parameters, Keyword);
                    break;
                case "checkbox":
                    mCheckbox = new DlkCheckbox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckbox, Parameters, Keyword);
                    break;
                case "checkboxlist":
                    mCheckboxList = new DlkCheckboxList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckboxList, Parameters, Keyword);
                    break;
                case "checkboxsearch":
                    mCheckboxSearch = new DlkCheckboxSearch(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckboxSearch, Parameters, Keyword);
                    break;
                case "dropdown":
                    mDropdown = new DlkDropdown(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDropdown, Parameters, Keyword);
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLabel, Parameters, Keyword);
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLink, Parameters, Keyword);
                    break;
                case "menu":
                    mMenu = new DlkMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mMenu, Parameters, Keyword);
                    break;
                case "report":
                    mReport = new DlkReport(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mReport, Parameters, Keyword);
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButton, Parameters, Keyword);
                    break;
                case "radiobuttongroup":
                    mRadioButtonGroup = new DlkRadioButtonGroup(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButtonGroup, Parameters, Keyword);
                    break;
                case "radiobuttonsearch":
                    mRadioButtonSearch = new DlkRadioButtonSearch(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButtonSearch, Parameters, Keyword);
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextBox, Parameters, Keyword);
                    break;
                case "listview":
                    mListView = new DlkListView(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mListView, Parameters, Keyword);
                    break;
                case "listboxsearch":
                    mListBoxSearch = new DlkListBoxSearch(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mListBoxSearch, Parameters, Keyword);
                    break;
                case "calendar":
                    mCalendar = new DlkCalendar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCalendar, Parameters, Keyword);
                    break;
                case "reporttable":
                    mReportTable = new DlkReportTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mReportTable, Parameters, Keyword);
                    break;
                case "listbox":
                    mListBox = new DlkListBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mListBox, Parameters, Keyword);
                    break;
                case "dashboardfilter":
                    mDashboardFilter = new DlkDashboardFilter(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDashboardFilter, Parameters, Keyword);
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
                case 5:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                    break;
                default:
                    throw new Exception("Unsupported keyword: " + Keyword);
            }
        }
    }
}
