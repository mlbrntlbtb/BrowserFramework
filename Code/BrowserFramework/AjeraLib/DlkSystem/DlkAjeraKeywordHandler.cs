using System;
using System.Reflection;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using AjeraLib.DlkControls;

namespace AjeraLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkAjeraKeywordHandler
    {
        #region DECLARATIONS

        #endregion

        #region PROPERTIES

        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        #endregion

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkImageButton mImageButton;
            DlkTextBox mTextBox;
            DlkTextArea mTextArea;
            DlkLabel mLabel;
            DlkTab mTab;
            DlkDropDown mDropDown;
            DlkExpander mExpander;
            DlkCheckBox mcheckBox;
            DlkLink mLink;
            DlkWidget mWidget;
            DlkDatePicker mdatePicker;
            DlkColorPalette mcolorPalette;
            DlkList mList;
            DlkIndicator mIndicator;
            DlkMenu mMenu;
            DlkTable mTable;
            DlkSearchBox mSearchBox;
            DlkPopup mPopUp;

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
                        case 3:
                            mi = mTab.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTab, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                case "expander":
                    mExpander = new DlkExpander(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mExpander.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mExpander, null);
                                }
                                catch
                                {
                                    mi = mExpander.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mExpander, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mExpander.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mExpander, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mExpander.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mExpander, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mExpander.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mExpander, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mExpander.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mExpander, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
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
                case "datepicker":
                    mdatePicker = new DlkDatePicker(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mdatePicker.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mdatePicker, null);
                                }
                                catch
                                {
                                    mi = mdatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mdatePicker, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mdatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mdatePicker, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mdatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mdatePicker, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mdatePicker.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mdatePicker, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "widget":
                    mWidget = new DlkWidget(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {

                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mWidget.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mWidget, null);
                                }
                                catch
                                {
                                    mi = mWidget.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mWidget, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mWidget.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mWidget, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mWidget.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mWidget, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mWidget.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mWidget, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        case 4:
                            mi = mWidget.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mWidget, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "colorpalette":
                    mcolorPalette = new DlkColorPalette(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mcolorPalette.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mcolorPalette, null);
                                }
                                catch
                                {
                                    mi = mcolorPalette.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mcolorPalette, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mcolorPalette.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mcolorPalette, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mcolorPalette.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mcolorPalette, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mcolorPalette.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mcolorPalette, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                
                case "indicator":
                    mIndicator = new DlkIndicator(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mIndicator.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mIndicator, null);
                                }
                                catch
                                {
                                    mi = mIndicator.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mIndicator, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mIndicator.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mIndicator, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mIndicator.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mIndicator, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mIndicator.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mIndicator, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                case "searchbox":
                    mSearchBox = new DlkSearchBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSearchBox.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSearchBox, null);
                                }
                                catch
                                {
                                    mi = mSearchBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSearchBox, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSearchBox.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSearchBox, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mSearchBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mSearchBox, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mSearchBox.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mSearchBox, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "popup":
                    mPopUp = new DlkPopup(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mPopUp.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mPopUp, null);
                                }
                                catch
                                {
                                    mi = mPopUp.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mPopUp, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mPopUp.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mPopUp, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mPopUp.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mPopUp, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mPopUp.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mPopUp, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
