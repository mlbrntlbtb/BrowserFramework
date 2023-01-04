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
using ngCRMLib.DlkControls;

namespace ngCRMLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkNgCRMKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkCheckBox mCheckBox;
            DlkComboBox mComboBox;
            DlkLabel mLabel;
            DlkLink mLink;
            DlkList mList;
            DlkMenu mMenu;
            DlkQuickEdit mQuickEdit;
            DlkRadioButton mRadioButton;
            DlkSideBar mSideBar;
            DlkScrollingList mScrollingList;
            DlkTab mTab;
            DlkTable mTable;
            DlkTextArea mTextArea;
            DlkTextBox mTextBox;
            DlkToolbar mToolbar;
            DlkUIDialog mUIDialog;
            ngCRMLib.DlkControls.DlkImage mImage;
            DlkToolTip mToolTip;
            DlkSelectionList mSelectionList;
            DlkInfoBubbleList mInfoBubbleList;
            DlkDatePicker mDatePicker;
            DlkRichTextEditor mRichTextEditor;
            DlkGrid mGrid;
            DlkChart mChart;
            DlkMultiselect mMultiselect;

            // needed for the dynamic method execution
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
                                try
                                {
                                    mi = mButton.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mButton, null);
                                }
                                catch
                                {
                                    mi = mButton.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mButton, new[] { Parameters[0] });
                                }
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
                case "list":                
                    mList = new DlkList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mList, null);
                                }
                                catch
                                {
                                    mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mList, new[] { Parameters[0], Parameters[1] });
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
                case "scrollinglist":
                    mScrollingList = new DlkScrollingList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mScrollingList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mScrollingList, null);
                                }
                                catch
                                {
                                    mi = mScrollingList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mScrollingList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mScrollingList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mScrollingList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mScrollingList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mScrollingList, new[] { Parameters[0], Parameters[1] });
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
                case "toolbar":
                    mToolbar = new DlkToolbar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mToolbar.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mToolbar, null);
                                }
                                catch
                                {
                                    mi = mToolbar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mToolbar, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mToolbar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mToolbar, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mToolbar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mToolbar, new[] { Parameters[0], Parameters[1] });
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
                case "image":
                    mImage = new ngCRMLib.DlkControls.DlkImage(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mImage.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mImage, null);
                                }
                                catch
                                {
                                    mi = mImage.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mImage, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mImage.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mImage, new[] { Parameters[0] });
                            }
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
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "selectionlist":
                    mSelectionList = new DlkSelectionList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSelectionList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSelectionList, null);
                                }
                                catch
                                {
                                    mi = mSelectionList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSelectionList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSelectionList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSelectionList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSelectionList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSelectionList, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "infobubblelist":
                    mInfoBubbleList = new DlkInfoBubbleList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mInfoBubbleList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mInfoBubbleList, null);
                                }
                                catch
                                {
                                    mi = mInfoBubbleList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mInfoBubbleList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mInfoBubbleList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mInfoBubbleList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mInfoBubbleList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mInfoBubbleList, new[] { Parameters[0], Parameters[1] });
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
                case "richtexteditor":
                    mRichTextEditor = new DlkRichTextEditor(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mRichTextEditor.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mRichTextEditor, null);
                                }
                                catch
                                {
                                    mi = mRichTextEditor.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mRichTextEditor, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mRichTextEditor.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mRichTextEditor, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "grid":
                    mGrid = new DlkGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mGrid.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mGrid, null);
                                }
                                catch
                                {
                                    mi = mGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mGrid, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mGrid, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mGrid, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mGrid, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "chart":
                    mChart = new DlkChart(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mChart.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mChart, null);
                                }
                                catch
                                {
                                    mi = mChart.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mChart, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mChart.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mChart, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mChart.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mChart, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mChart.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mChart, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "multiselect":
                    mMultiselect = new DlkMultiselect(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMultiselect.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMultiselect, null);
                                }
                                catch
                                {
                                    mi = mMultiselect.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMultiselect, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMultiselect.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMultiselect, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMultiselect.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMultiselect, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mMultiselect.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMultiselect, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
