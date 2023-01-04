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
using MaconomyTouchLib.DlkControls;

namespace MaconomyTouchLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkMaconomyTouchKeywordHandler
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
            DlkLabel mLabel;
            DlkList mList;
            DlkPicker mPicker;
            DlkTimesheetList mTimesheetList;
            DlkToggle mToggle;
            DlkCalendarCarousel mCalendarCarousel;
            DlkTextArea mTextArea;
            DlkDatePicker mDatePicker;
            DlkContextMenu mContextMenu;
            DlkImage mImage;
            DlkMaskedTextBox mMaskedTextBox;
            DlkCalendar mCalendar;

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
                        case 2:
                            mi = mButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mButton, new[] { Parameters[0], Parameters[1] });
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
                case "picker":
                    mPicker = new DlkPicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
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
                        case 2:
                            mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDatePicker, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mDatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDatePicker, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 4:
                            mi = mList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "timesheetlist":
                    mTimesheetList = new DlkTimesheetList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTimesheetList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTimesheetList, null);
                                }
                                catch
                                {
                                    mi = mTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTimesheetList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTimesheetList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTimesheetList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTimesheetList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mTimesheetList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTimesheetList, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "toggle":
                    mToggle = new DlkToggle(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
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
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "calendarcarousel":
                    mCalendarCarousel = new DlkCalendarCarousel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mCalendarCarousel.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mCalendarCarousel, null);
                                }
                                catch
                                {
                                    mi = mCalendarCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mCalendarCarousel, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mCalendarCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mCalendarCarousel, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mCalendarCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mCalendarCarousel, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mCalendarCarousel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mCalendarCarousel, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                case "maskedtextbox":
                    mMaskedTextBox = new DlkMaskedTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMaskedTextBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMaskedTextBox, null);
                                }
                                catch
                                {
                                    mi = mMaskedTextBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMaskedTextBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMaskedTextBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMaskedTextBox, new[] { Parameters[0] });
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
