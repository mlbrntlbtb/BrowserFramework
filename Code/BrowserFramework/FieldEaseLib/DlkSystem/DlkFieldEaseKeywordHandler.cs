using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using FieldEaseLib.DlkControls;
using System.Reflection;

namespace FieldEaseLib.DlkSystem
{
    public static class DlkFieldEaseKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkLabel mLabel;
            DlkTextBox mTextBox;
            DlkTable mTable;
            DlkLink mLink;
            DlkTab mTab;
            DlkSideBar mSideBar;
            DlkCheckBox mCheckBox;
            DlkList mList;
            DlkComboBox mComboBox;
            DlkDatePicker mDatePicker;
            DlkRadioButton mRadioButton;
            DlkTextArea mTextArea;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mButton, Parameters, Keyword);
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLabel, Parameters, Keyword);
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextBox, Parameters, Keyword);
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTable, Parameters, Keyword);
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLink, Parameters, Keyword);
                    break;
                case "tab":
                    mTab = new DlkTab (mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTab, Parameters, Keyword);
                    break;
                case "sidebar":
                    mSideBar = new DlkSideBar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mSideBar, Parameters, Keyword);
                    break;
                case "checkbox":
                    mCheckBox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckBox, Parameters, Keyword);
                    break;
                case "list":
                    mList = new DlkList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mList, Parameters, Keyword);
                    break;
                case "combobox":
                    mComboBox = new DlkComboBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mComboBox, Parameters, Keyword);
                    break;
                case "datepicker":
                    mDatePicker = new DlkDatePicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDatePicker, Parameters, Keyword);
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButton, Parameters, Keyword);
                    break;
                case "textarea":
                    mTextArea = new DlkTextArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextArea, Parameters, Keyword);
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
