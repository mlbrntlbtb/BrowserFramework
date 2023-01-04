using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using AcumenTouchStoneLib.DlkControls;
using System.Reflection;

namespace AcumenTouchStoneLib.DlkSystem
{
    public static class DlkAcumenTouchStoneKeywordHandler
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
            DlkSideBar mSideBar;
            DlkLink mLink;
            DlkTab mTab;
            DlkTable mTable;
            DlkRadioButton mRadioButton;
            DlkComboBox mComboBox;
            DlkCheckBox mCheckBox;
            DlkLabel mLabel;
            DlkMenu mMenu;
            DlkCalendar mCalendar;
            DlkDatePicker mDatePicker;
            DlkMultiSelect mMultiSelect;
            DlkQuickEdit mQuickEdit;
            DlkUIDialog mUIDialog;
            DlkInfoBubble mInfoBubble;
            DlkSlider mSlider;
            DlkTextArea mTextArea;
            DlkToolTip mToolTip;
            MethodInfo mi;
            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                mi = mButton.GetType().GetMethod(Keyword, new Type[] { });
                                mi.Invoke(mButton, null);
                            }
                            else
                            {
                                mi = mButton.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mButton, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTextBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTextBox, null);
                                }
                                catch
                                {
                                    mi = mTextBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTextBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTextBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTextBox, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "multiselect":
                    mMultiSelect = new DlkMultiSelect(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMultiSelect.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMultiSelect, null);
                                }
                                catch
                                {
                                    mi = mMultiSelect.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMultiSelect, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMultiSelect.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMultiSelect, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMultiSelect.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMultiSelect, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mMultiSelect.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMultiSelect, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "menu":
                    mMenu = new DlkMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMenu.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMenu, null);
                                }
                                catch
                                {
                                    mi = mMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMenu, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMenu, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMenu, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mRadioButton.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mRadioButton, null);
                                }
                                catch
                                {
                                    mi = mRadioButton.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mRadioButton, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mRadioButton.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mRadioButton, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTable, null);
                                }
                                catch
                                {
                                    mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTable, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "checkbox":
                    mCheckBox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mCheckBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mCheckBox, null);
                                }
                                catch
                                {
                                    mi = mCheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mCheckBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mCheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mCheckBox, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "sidebar":
                    mSideBar = new DlkSideBar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSideBar.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSideBar, null);
                                }
                                catch
                                {
                                    mi = mSideBar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSideBar, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSideBar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSideBar, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSideBar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSideBar, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mLink.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mLink, null);
                                }
                                catch
                                {
                                    mi = mLink.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mLink, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mLink.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mLink, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "tab":
                    mTab = new DlkTab(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTab.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTab, null);
                                }
                                catch
                                {
                                    mi = mTab.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTab, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTab.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTab, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTab.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTab, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "combobox":
                    mComboBox = new DlkComboBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mComboBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mComboBox, null);
                                }
                                catch
                                {
                                    mi = mComboBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mComboBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mComboBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mComboBox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mComboBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mComboBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mComboBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mComboBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mComboBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mComboBox, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mLabel.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mLabel, null);
                                }
                                catch
                                {
                                    mi = mLabel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mLabel, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mLabel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mLabel, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mLabel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mLabel, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "datepicker":
                    mDatePicker = new DlkDatePicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDatePicker, null);
                                }
                                catch
                                {
                                    mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDatePicker, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDatePicker, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "calendar":
                    mCalendar = new DlkCalendar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mCalendar, null);
                                }
                                catch
                                {
                                    mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mCalendar, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mCalendar, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mCalendar, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCalendar, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCalendar, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "quickedit":
                    mQuickEdit = new DlkQuickEdit(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mQuickEdit.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mQuickEdit, null);
                                }
                                catch
                                {
                                    mi = mQuickEdit.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mQuickEdit, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mQuickEdit.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mQuickEdit, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mQuickEdit.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mQuickEdit, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mQuickEdit.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mQuickEdit, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "uidialog":
                    mUIDialog = new DlkUIDialog(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mUIDialog.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mUIDialog, null);
                                }
                                catch
                                {
                                    mi = mUIDialog.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mUIDialog, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mUIDialog.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mUIDialog, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "infobubble":
                    mInfoBubble = new DlkInfoBubble(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mInfoBubble.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mInfoBubble, null);
                                }
                                catch
                                {
                                    mi = mInfoBubble.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mInfoBubble, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mInfoBubble.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mInfoBubble, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mInfoBubble.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mInfoBubble, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mInfoBubble.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mInfoBubble, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mInfoBubble.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mInfoBubble, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "slider":
                    mSlider = new DlkSlider(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSlider.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSlider, null);
                                }
                                catch
                                {
                                    mi = mSlider.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSlider, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSlider.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSlider, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSlider.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSlider, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mSlider.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mSlider, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mSlider.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mSlider, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "textarea":
                    mTextArea = new DlkTextArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTextArea, null);
                                }
                                catch
                                {
                                    mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTextArea, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTextArea, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTextArea, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTextArea, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTextArea, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "tooltip":
                    mToolTip = new DlkToolTip(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mToolTip.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mToolTip, null);
                                }
                                catch
                                {
                                    mi = mToolTip.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mToolTip, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mToolTip.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mToolTip, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mToolTip.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mToolTip, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }
    }
}
