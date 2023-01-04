using System;
using System.Reflection;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using TrafficLiveLib.DlkControls;

namespace TrafficLiveLib.DlkSystem
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkTrafficLiveKeywordHandler
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
            DlkToggle mToggle;
            DlkList mList;
            DlkPicker mPicker;
            DlkTimePicker mTimePicker;
            DlkDatePicker mDatePicker;
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
                case "timepicker":
                    mTimePicker = new DlkTimePicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTimePicker.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTimePicker, null);
                                }
                                catch
                                {
                                    mi = mTimePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTimePicker, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTimePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTimePicker, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTimePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTimePicker, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTimePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTimePicker, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);

            }
        }
    }
}
