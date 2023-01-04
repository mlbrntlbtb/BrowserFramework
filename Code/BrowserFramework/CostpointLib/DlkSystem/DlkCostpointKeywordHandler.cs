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
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using CostpointLib.DlkControls;
using CostpointLib.DlkRecords;

namespace CostpointLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkCostpointKeywordHandler
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
            DlkNavigationPath mNavPath;
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
            DlkDashpart mDashpart;
            DlkConditionListBox mConditionListBox;
            DlkAppMenuView mAppMenuView;
            DlkSetupGrid mSetupGrid;
            DlkCustomTable mCustomTable;

            // needed for the dynamic method execution
            MethodInfo mi;
            DlkPreviousControlRecord.SetCurrentControl(Screen, Keyword, ControlName, mControl.mControlType);

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
                        case 2:
                            mi = mButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mButton, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mButton, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 2:
                            mi = mTextBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTextBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTextBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTextBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "navigationmenu":
                    mNavMenu = new DlkNavigationMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mNavMenu.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mNavMenu, null);
                                }
                                catch
                                {
                                    mi = mNavMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mNavMenu, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mNavMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mNavMenu, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mNavMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mNavMenu, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mNavMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mNavMenu, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 2:
                            mi = mLink.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mLink, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mLink.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mLink, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "tab":
                    mTab = new DlkTab (mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
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
                        case 3:
                            mi = mTab.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTab, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "form":
                    mForm = new DlkForm(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mForm.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mForm, null);
                                }
                                catch
                                {
                                    mi = mForm.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mForm, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mForm.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mForm, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mForm.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mForm, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mForm.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mForm, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 3:
                            mi = mToolbar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mToolbar, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 2:
                            mi = mCheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mCheckBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mCheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCheckBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "messagearea":
                    mMessageArea = new DlkMessageArea(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMessageArea.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMessageArea, null);
                                }
                                catch
                                {
                                    mi = mMessageArea.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMessageArea, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMessageArea.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMessageArea, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMessageArea.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMessageArea, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mMessageArea.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMessageArea, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    if (!DlkEnvironment.AutoDriver.FindElements(By.XPath(mLabel.mSearchValues[0])).Any())
                    {
                        mLabel.mSearchValues[0] = mLabel.mSearchValues[0].Replace("]/preceding-sibling", "]/../preceding-sibling");
                    }
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
                        case 3:
                            mi = mLabel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mLabel, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 3:
                            mi = mMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMenu, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "multiparttable":
                    mMultiPartTable = new DlkMultiPartTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMultiPartTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMultiPartTable, null);
                                }
                                catch
                                {
                                    mi = mMultiPartTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMultiPartTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMultiPartTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMultiPartTable, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMultiPartTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMultiPartTable, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mMultiPartTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMultiPartTable, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mMultiPartTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMultiPartTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "navigationpath":
                    mNavPath = new DlkNavigationPath(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mNavPath.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mNavPath, null);
                                }
                                catch
                                {
                                    mi = mNavPath.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mNavPath, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mNavPath.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mNavPath, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mNavPath.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mNavPath, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mNavPath.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mNavPath, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 2:
                            mi = mRadioButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mRadioButton, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mRadioButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mRadioButton, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "radiobuttongroup":
                    mRadioButtonGroup = new DlkRadioButtonGroup(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mRadioButtonGroup.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mRadioButtonGroup, null);
                                }
                                catch
                                {
                                    mi = mRadioButtonGroup.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mRadioButtonGroup, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mRadioButtonGroup.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mRadioButtonGroup, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mRadioButtonGroup.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mRadioButtonGroup, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mRadioButtonGroup.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mRadioButtonGroup, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "reportbody":
                    mReportBody = new DlkReportBody(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mReportBody.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mReportBody, null);
                                }
                                catch
                                {
                                    mi = mReportBody.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mReportBody, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mReportBody.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mReportBody, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mReportBody.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mReportBody, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mReportBody.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mReportBody, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 3:
                            mi = mTextArea.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTextArea, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "contextmenu":
                    mContextMenu = new DlkContextMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mContextMenu.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mContextMenu, null);
                                }
                                catch
                                {
                                    mi = mContextMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mContextMenu, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mContextMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mContextMenu, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mContextMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mContextMenu, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mContextMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mContextMenu, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "foldertree":
                    mFolderTree = new DlkFolderTree(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mFolderTree.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mFolderTree, null);
                                }
                                catch
                                {
                                    mi = mFolderTree.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mFolderTree, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mFolderTree.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mFolderTree, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mFolderTree.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mFolderTree, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mFolderTree.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mFolderTree, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "searchappresultlist":
                    mAppSearchList = new DlkSearchAppResultList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mAppSearchList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mAppSearchList, null);
                                }
                                catch
                                {
                                    mi = mAppSearchList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mAppSearchList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mAppSearchList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mAppSearchList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mAppSearchList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mAppSearchList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mAppSearchList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mAppSearchList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "listbox":
                    mList = new DlkListBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
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
                        case 3:
                            mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "infotip":
                    mInfoTip = new DlkInfoTip(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mInfoTip.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mInfoTip, null);
                                }
                                catch
                                {
                                    mi = mInfoTip.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mInfoTip, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mInfoTip.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mInfoTip, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mInfoTip.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mInfoTip, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mInfoTip.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mInfoTip, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "webgrid":
                    mWebGrid = new DlkWebGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mWebGrid.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mWebGrid, null);
                                }
                                catch
                                {
                                    mi = mWebGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mWebGrid, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mWebGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mWebGrid, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mWebGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mWebGrid, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mWebGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mWebGrid, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "dashpart":
                    mDashpart = new DlkDashpart(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDashpart.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDashpart, null);
                                }
                                catch
                                {
                                    mi = mDashpart.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDashpart, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDashpart.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDashpart, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDashpart.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDashpart, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mDashpart.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDashpart, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mDashpart.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDashpart, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "conditionlistbox":
                    mConditionListBox = new DlkConditionListBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mConditionListBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mConditionListBox, null);
                                }
                                catch
                                {
                                    mi = mConditionListBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mConditionListBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mConditionListBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mConditionListBox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mConditionListBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mConditionListBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mConditionListBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mConditionListBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mConditionListBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mConditionListBox, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "appmenuview":
                    mAppMenuView = new DlkAppMenuView(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mAppMenuView.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mAppMenuView, null);
                                }
                                catch
                                {
                                    mi = mAppMenuView.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mAppMenuView, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mAppMenuView.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mAppMenuView, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mAppMenuView.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mAppMenuView, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mAppMenuView.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mAppMenuView, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mAppMenuView.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mAppMenuView, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "setupgrid":
                    mSetupGrid = new DlkSetupGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSetupGrid.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSetupGrid, null);
                                }
                                catch
                                {
                                    mi = mSetupGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSetupGrid, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSetupGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSetupGrid, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSetupGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSetupGrid, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mSetupGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mSetupGrid, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "customtable":
                    mCustomTable = new DlkCustomTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mCustomTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mCustomTable, null);
                                }
                                catch
                                {
                                    mi = mCustomTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mCustomTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mCustomTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mCustomTable, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mCustomTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mCustomTable, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mCustomTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCustomTable, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
