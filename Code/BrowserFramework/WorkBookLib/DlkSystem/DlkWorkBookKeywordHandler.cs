using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using WorkBookLib.DlkControls;
using System.Reflection;

namespace WorkBookLib.DlkSystem
{
    public static class DlkWorkBookKeywordHandler
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
            DlkTable mTable;
            DlkTab mTab;
            DlkComboBox mComboBox;
            DlkForm mForm;
            DlkLabel mLabel;
            DlkToolbar mToolbar;
            DlkCheckBox mCheckBox;
            DlkMultiselect mMultiselect;
            DlkSideBar mSideBar;
            DlkTextArea mTextArea;
            DlkRadioButton mRadioButton;
            DlkLink mLink;
            DlkProgressBar mProgressBar;
            DlkCheckBoxTree mCheckBoxTree;
            DlkGraph mGraph;
            DlkColorPicker mColorPicker;
            DlkCalendarTimeline mCalendarTimeline;
            DlkGantt mGantt;
            DlkTreeView mTreeView;
            DlkAccordion mAccordion;
            DlkCalendarList mCalendarList;
            DlkContainer mContainer;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mButton, Parameters, Keyword);
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextBox, Parameters, Keyword);
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTable, Parameters, Keyword);
                    break;
                case "combobox":
                    mComboBox = new DlkComboBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mComboBox, Parameters, Keyword);
                    break;
                case "tab":
                    mTab = new DlkTab(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTab, Parameters, Keyword);
                    break;
                case "form":
                    mForm = new DlkForm(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mForm, Parameters, Keyword);
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLabel, Parameters, Keyword);
                    break;
                case "toolbar":
                    mToolbar = new DlkToolbar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mToolbar, Parameters, Keyword);
                    break;
                case "checkbox":
                    mCheckBox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckBox, Parameters, Keyword);
                    break;
                case "multiselect":
                    mMultiselect = new DlkMultiselect(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mMultiselect, Parameters, Keyword);
                    break;
                case "sidebar":
                    mSideBar = new DlkSideBar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mSideBar, Parameters, Keyword);
                    break;
                case "textarea":
                    mTextArea = new DlkTextArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextArea, Parameters, Keyword);
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButton, Parameters, Keyword);
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLink, Parameters, Keyword);
                    break;
                case "progressbar":
                    mProgressBar = new DlkProgressBar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mProgressBar, Parameters, Keyword);
                    break;
                case "checkboxtree":
                    mCheckBoxTree = new DlkCheckBoxTree(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckBoxTree, Parameters, Keyword);
                    break;
                case "calendartimeline":
                    mCalendarTimeline = new DlkCalendarTimeline(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCalendarTimeline, Parameters, Keyword);
                    break;
                case "colorpicker":
                    mColorPicker = new DlkColorPicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mColorPicker, Parameters, Keyword);
                    break;
                case "gantt":
                    mGantt = new DlkGantt(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mGantt, Parameters, Keyword);
                    break;
                case "graph":
                    mGraph = new DlkGraph(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mGraph, Parameters, Keyword);
                    break;
                case "treeview":
                    mTreeView = new DlkTreeView(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTreeView, Parameters, Keyword);
                    break;
                case "accordion":
                    mAccordion = new DlkAccordion(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mAccordion, Parameters, Keyword);
                    break;
                case "calendarlist":
                    mCalendarList = new DlkCalendarList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCalendarList, Parameters, Keyword);
                    break;
                case "container":
                    mContainer = new DlkContainer(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mContainer, Parameters, Keyword);
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
