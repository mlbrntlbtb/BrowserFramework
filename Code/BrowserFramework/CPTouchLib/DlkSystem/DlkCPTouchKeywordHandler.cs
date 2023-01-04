using System;
using System.Reflection;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CPTouchLib.DlkControls;

namespace CPTouchLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkCPTouchKeywordHandler
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
            DlkTextView mTextView;
            DlkToggle mToggle;
            DlkSlidingMenu mSlidingMenu;
            DlkPicker mPicker;
            DlkCarousel mCarousel;
            DlkTimesheetLinesList mTimesheetLinesList;
            DlkDialogBox mDialogBox;
            DlkCalendar mCalendar;
            DlkMenu mMenu;
            DlkTaskItem mTaskItem;
            DlkApproveTimesheetsList mApproveTimesheetList;
            DlkHoursPicker mHoursPicker;
            DlkSummaryTable mSummaryTable;
            DlkLookupList mLookupList;
            DlkCheckBox mCheckbox;
            DlkMultiSelectList mMultiSelecList;
            DlkList mList;
            DlkTable mTable;
            DlkLeaveTable mLeaveTable;
            DlkAuditList mAuditList;
            DlkUDTLookupList mUDTLookupList;
            DlkExpenseReportList mExpenseReportsList; 
            DlkDatePicker mDatePicker;
            DlkLabel mLabel;
            DlkErrorWarningList mErrorWarningList;
            DlkRadioButton mRadioButton;

            // needed for the dynamic method execution
            MethodInfo mi;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                case "textview":
                    mTextView = new DlkTextView(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTextView.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTextView, null);
                                }
                                catch
                                {
                                    mi = mTextView.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTextView, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTextView.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTextView, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTextView.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTextView, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTextView.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTextView, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "toggle":
                    mToggle = new DlkToggle(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mToggle.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mToggle, null);
                                }
                                catch
                                {
                                    mi = mToggle.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mToggle, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mToggle.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mToggle, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mToggle.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mToggle, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mToggle.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mToggle, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mToggle.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mToggle, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "slidingmenu":
                    mSlidingMenu = new DlkSlidingMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSlidingMenu.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSlidingMenu, null);
                                }
                                catch
                                {
                                    mi = mSlidingMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSlidingMenu, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSlidingMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSlidingMenu, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSlidingMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSlidingMenu, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mSlidingMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mSlidingMenu, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mSlidingMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mSlidingMenu, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "picker":
                    mPicker = new DlkPicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mPicker.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mPicker, null);
                                }
                                catch
                                {
                                    mi = mPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mPicker, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mPicker, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mPicker, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mPicker, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mPicker, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "carousel":
                    mCarousel = new DlkCarousel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mCarousel.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mCarousel, null);
                                }
                                catch
                                {
                                    mi = mCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mCarousel, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mCarousel, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mCarousel, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCarousel, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mCarousel, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "timesheetlineslist":
                    mTimesheetLinesList = new DlkTimesheetLinesList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTimesheetLinesList, null);
                                }
                                catch
                                {
                                    mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTimesheetLinesList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTimesheetLinesList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTimesheetLinesList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTimesheetLinesList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mTimesheetLinesList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mTimesheetLinesList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mTimesheetLinesList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "dialogbox":
                    mDialogBox = new DlkDialogBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDialogBox, null);
                                }
                                catch
                                {
                                    mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDialogBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDialogBox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDialogBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDialogBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mDialogBox, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mDialogBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mDialogBox, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "calendar":
                    mCalendar = new DlkCalendar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                            mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mCalendar, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mCalendar.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mCalendar, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "menu":
                    mMenu = new DlkMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                        case 4:
                            mi = mMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mMenu, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mMenu, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "taskitem":
                    mTaskItem = new DlkTaskItem(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTaskItem, null);
                                }
                                catch
                                {
                                    mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTaskItem, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTaskItem, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTaskItem, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTaskItem, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mTaskItem, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mTaskItem.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mTaskItem, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "approvetimesheetslist":
                    mApproveTimesheetList = new DlkApproveTimesheetsList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mApproveTimesheetList, null);
                                }
                                catch
                                {
                                    mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mApproveTimesheetList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mApproveTimesheetList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mApproveTimesheetList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mApproveTimesheetList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mApproveTimesheetList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mApproveTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mApproveTimesheetList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "hourspicker":
                    mHoursPicker = new DlkHoursPicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mHoursPicker, null);
                                }
                                catch
                                {
                                    mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mHoursPicker, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mHoursPicker, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mHoursPicker, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mHoursPicker, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mHoursPicker, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mHoursPicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mHoursPicker, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "summarytable":
                    mSummaryTable = new DlkSummaryTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSummaryTable, null);
                                }
                                catch
                                {
                                    mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSummaryTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSummaryTable, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSummaryTable, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mSummaryTable, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mSummaryTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mSummaryTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mSummaryTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "lookuplist":
                    mLookupList = new DlkLookupList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mLookupList, null);
                                }
                                catch
                                {
                                    mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mLookupList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mLookupList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mLookupList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mLookupList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mLookupList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mLookupList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "checkbox":
                    mCheckbox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mCheckbox, null);
                                }
                                catch
                                {
                                    mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mCheckbox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mCheckbox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mCheckbox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCheckbox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mCheckbox, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mCheckbox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mCheckbox, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "multiselectlist":
                    mMultiSelecList = new DlkMultiSelectList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMultiSelecList, null);
                                }
                                catch
                                {
                                    mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMultiSelecList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMultiSelecList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMultiSelecList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mMultiSelecList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mMultiSelecList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mMultiSelecList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mMultiSelecList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "list":
                    mList = new DlkList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                        case 4:
                            mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "leavetable":
                    mLeaveTable = new DlkLeaveTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mLeaveTable, null);
                                }
                                catch
                                {
                                    mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mLeaveTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mLeaveTable, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mLeaveTable, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mLeaveTable, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mLeaveTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mLeaveTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mLeaveTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "auditlist":
                    mAuditList = new DlkAuditList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mAuditList, null);
                                }
                                catch
                                {
                                    mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mAuditList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mAuditList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mAuditList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mAuditList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mAuditList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mAuditList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mAuditList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "udtlookuplist":
                    mUDTLookupList = new DlkUDTLookupList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mUDTLookupList, null);
                                }
                                catch
                                {
                                    mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mUDTLookupList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mUDTLookupList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mUDTLookupList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mUDTLookupList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mUDTLookupList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mUDTLookupList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mUDTLookupList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "expensereportslist":
                    mExpenseReportsList = new DlkExpenseReportList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mExpenseReportsList, null);
                                }
                                catch
                                {
                                    mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mExpenseReportsList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mExpenseReportsList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mExpenseReportsList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mExpenseReportsList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mExpenseReportsList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mExpenseReportsList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mExpenseReportsList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "errorwarninglist":
                    mErrorWarningList = new DlkErrorWarningList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mErrorWarningList, null);
                                }
                                catch
                                {
                                    mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mErrorWarningList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mErrorWarningList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mErrorWarningList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mErrorWarningList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mErrorWarningList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mErrorWarningList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mErrorWarningList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "datepicker":
                    mDatePicker = new DlkDatePicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                        case 2:
                            mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDatePicker, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDatePicker, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string) });
                            mi.Invoke(mDatePicker, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        case 5:
                            mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(string), typeof(string) });
                            mi.Invoke(mDatePicker, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
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
