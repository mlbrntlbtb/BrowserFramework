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
using MaconomyNavigatorLib.DlkControls;

namespace MaconomyNavigatorLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkMaconomyNavigatorKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkGrid mGrid;
            DlkSideBar mSideBar;
            DlkTable mTable;
            DlkDatePicker mDatePicker;
            DlkTextBox mTextBox;
            DlkLabel mLabel;
            DlkToolbar mToolBar;
            DlkLink mLink;
            DlkDropDownList mDropDownList;
            DlkList mList;
            DlkImage mImage;
            DlkCheckBox mCheckBox;
            DlkPeriodPicker mPeriodPicker;

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
                case "dropdownlist":
                    mDropDownList = new DlkDropDownList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDropDownList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDropDownList, null);
                                }
                                catch
                                {
                                    mi = mDropDownList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDropDownList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDropDownList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDropDownList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDropDownList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDropDownList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mDropDownList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDropDownList, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 3:
                            mi = mLabel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mLabel, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 3:
                            mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mList, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                case "toolbar":
                    mToolBar = new DlkToolbar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mToolBar.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mToolBar, null);
                                }
                                catch
                                {
                                    mi = mToolBar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mToolBar, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mToolBar.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mToolBar, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mToolBar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mToolBar, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mToolBar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mToolBar, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "image":
                    mImage = new DlkImage(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
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
                case "periodpicker":
                    mPeriodPicker = new DlkPeriodPicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mPeriodPicker.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mPeriodPicker, null);
                                }
                                catch
                                {
                                    mi = mPeriodPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mPeriodPicker, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mPeriodPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mPeriodPicker, new[] { Parameters[0] });
                            }
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
