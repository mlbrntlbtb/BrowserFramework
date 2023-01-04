using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using AjeraTimeLib.DlkControls;

namespace AjeraTimeLib.DlkSystem
{
    public static class DlkAjeraTimeKeywordHandler
    {
        #region DECLARATIONS

        #endregion
        
        #region PROPERTIES
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }
        #endregion

        #region METHODS

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkTextBox mTextBox;
            DlkLabel mLabel;
            DlkDropDown mDropDown;
            DlkCheckBox mcheckBox;
            DlkMenu mMenu;
            DlkImageButton mImageButton;
            DlkCalendarCarousel mCalendarCarousel;
            DlkListbox mListbox;
            DlkKeypad mKeypad;

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
                case "dropdown":
                    mDropDown = new DlkDropDown(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDropDown.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDropDown, null);
                                }
                                catch
                                {
                                    mi = mDropDown.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDropDown, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDropDown.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDropDown, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDropDown.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDropDown, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mDropDown.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mDropDown, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "checkbox":
                    mcheckBox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mcheckBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mcheckBox, null);
                                }
                                catch
                                {
                                    mi = mcheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mcheckBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mcheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mcheckBox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mcheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mcheckBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mcheckBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mcheckBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                case "imagebutton":
                    mImageButton = new DlkImageButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mImageButton.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mImageButton, null);
                                }
                                catch
                                {
                                    mi = mImageButton.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mImageButton, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mImageButton.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mImageButton, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mImageButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mImageButton, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mImageButton.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mImageButton, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                case "listbox":
                    mListbox = new DlkListbox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mListbox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mListbox, null);
                                }
                                catch
                                {
                                    mi = mListbox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mListbox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mListbox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mListbox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mListbox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mListbox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mListbox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mListbox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "keypad":
                    mKeypad = new DlkKeypad(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mKeypad.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mKeypad, null);
                                }
                                catch
                                {
                                    mi = mKeypad.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mKeypad, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mKeypad.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mKeypad, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mKeypad.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mKeypad, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mKeypad.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mKeypad, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }

        #endregion

    }
}
