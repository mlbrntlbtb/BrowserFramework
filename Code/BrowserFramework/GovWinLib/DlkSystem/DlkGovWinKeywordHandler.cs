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
using GovWinLib.DlkControls;

namespace GovWinLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkGovWinKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkTextBox mTextBox;
            DlkTextArea mTextArea;
            DlkButton mButton;
            DlkCheckBox mCheckBox;
            DlkComboBox mComboBox;
            DlkRadioButton mRadioButton;
            DlkTab mTab;
            DlkToolbar mToolbar;
            DlkLink mLink;
            DlkLabel mLabel;
            DlkQuickLinks mQuickLinks;
            DlkMegaMenu mMegaMenu;
            DlkTable mTable;
            DlkResultList mResultList;
            DlkDetailsTables mDetailsTables;
            DlkList mList;
            DlkFilters mFilters;
            DlkFilterSelection mFilterSelection;
            DlkMark mMark;
            DlkPagination mPagination;
            DlkPanel mPanel;
            DlkImage mImage;
            DlkModalWindow mModalWindow;
            DlkTreeList mTreeList;
            DlkTimelineChart mTimeLineChart;
            DlkSuggestions mSuggestions;
            DlkEditor mEditor;
            DlkDocSearchResultTable mDocSearchResultTable;
            DlkColorPalette mColorPalette;
            DlkContainer mContainer;
            DlkToolTip mTooltip;
            DlkClassicTable mClassicTable;
            DlkResultItem mResultItem;
            DlkTabClassic mTabClassic;

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
                case "classictable":
                    mClassicTable = new DlkClassicTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mClassicTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mClassicTable, null);
                                }
                                catch
                                {
                                    mi = mClassicTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mClassicTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mClassicTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mClassicTable, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "colorpalette":
                    mColorPalette = new DlkColorPalette(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mColorPalette.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mColorPalette, null);
                                }
                                catch
                                {
                                    mi = mColorPalette.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mColorPalette, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mColorPalette.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mColorPalette, new[] { Parameters[0] });
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
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "container":
                    mContainer = new DlkContainer(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mContainer.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mContainer, null);
                                }
                                catch
                                {
                                    mi = mContainer.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mContainer, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mContainer.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mContainer, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mContainer.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mContainer, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "detailstables":
                    mDetailsTables = new DlkDetailsTables(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDetailsTables.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDetailsTables, null);
                                }
                                catch
                                {
                                    mi = mDetailsTables.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDetailsTables, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDetailsTables.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDetailsTables, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDetailsTables.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDetailsTables, new[] { Parameters[0], Parameters[1] });
                            break;                        
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "docsearchresulttable":
                    mDocSearchResultTable = new DlkDocSearchResultTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDocSearchResultTable.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDocSearchResultTable, null);
                                }
                                catch
                                {
                                    mi = mDocSearchResultTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDocSearchResultTable, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDocSearchResultTable.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDocSearchResultTable, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDocSearchResultTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDocSearchResultTable, new[] { Parameters[0], Parameters[1] });
                            break;                        
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "editor":
                    mEditor = new DlkEditor(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mEditor.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mEditor, null);
                                }
                                catch
                                {
                                    mi = mEditor.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mEditor, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mEditor.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mEditor, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mEditor.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mEditor, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "filters":
                    mFilters = new DlkFilters(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mFilters.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mFilters, null);
                                }
                                catch
                                {
                                    mi = mFilters.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mFilters, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mFilters.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mFilters, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mFilters.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mFilters, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "filterselection":
                    mFilterSelection = new DlkFilterSelection(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mFilterSelection.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mFilterSelection, null);
                                }
                                catch
                                {
                                    mi = mFilterSelection.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mFilterSelection, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mFilterSelection.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mFilterSelection, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mFilterSelection.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mFilterSelection, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mFilterSelection.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mFilterSelection, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                        case 2:
                            mi = mImage.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mImage, new[] { Parameters[0], Parameters[1] });
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
                case "mark":
                    mMark = new DlkMark(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMark.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMark, null);
                                }
                                catch
                                {
                                    mi = mMark.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMark, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMark.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMark, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mMark.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mMark, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "megamenu":
                    mMegaMenu = new DlkMegaMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mMegaMenu.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mMegaMenu, null);
                                }
                                catch
                                {
                                    mi = mMegaMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mMegaMenu, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mMegaMenu.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mMegaMenu, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "modalwindow":
                    mModalWindow = new DlkModalWindow(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mModalWindow.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mModalWindow, null);
                                }
                                catch
                                {
                                    mi = mModalWindow.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mModalWindow, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mModalWindow.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mModalWindow, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "pagination":
                    mPagination = new DlkPagination(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mPagination.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mPagination, null);
                                }
                                catch
                                {
                                    mi = mPagination.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mPagination, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mPagination.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mPagination, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mPagination.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mPagination, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "panel":
                    mPanel = new DlkPanel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mPanel.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mPanel, null);
                                }
                                catch
                                {
                                    mi = mPanel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mPanel, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mPanel.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mPanel, new[] { Parameters[0] });
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
                case "quicklinks":
                    mQuickLinks = new DlkQuickLinks(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mQuickLinks.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mQuickLinks, null);
                                }
                                catch
                                {
                                    mi = mQuickLinks.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mQuickLinks, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mQuickLinks.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mQuickLinks, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mQuickLinks.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mQuickLinks, new[] { Parameters[0], Parameters[1] });
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
                case "resultitem":
                    mResultItem = new DlkResultItem(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mResultItem.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mResultItem, null);
                                }
                                catch
                                {
                                    mi = mResultItem.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mResultItem, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mResultItem.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mResultItem, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "resultlist":
                    mResultList = new DlkResultList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mResultList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mResultList, null);
                                }
                                catch
                                {
                                    mi = mResultList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mResultList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mResultList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mResultList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mResultList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mResultList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mResultList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mResultList, new[] { Parameters[0], Parameters[1], Parameters[2] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "suggestions":
                    mSuggestions = new DlkSuggestions(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mSuggestions.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mSuggestions, null);
                                }
                                catch
                                {
                                    mi = mSuggestions.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mSuggestions, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mSuggestions.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mSuggestions, new[] { Parameters[0] });
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
                case "tabclassic":
                    mTabClassic = new DlkTabClassic(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTabClassic.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTabClassic, null);
                                }
                                catch
                                {
                                    mi = mTabClassic.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTabClassic, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTabClassic.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTabClassic, new[] { Parameters[0] });
                            }
                            break;
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
                case "timelinechart":
                    mTimeLineChart = new DlkTimelineChart(mControl.mKey.ToString(), mControl.mSearchMethod.ToString(), mControl.mSearchParameters.Split('~')[0].ToString());
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTimeLineChart.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTimeLineChart, null);
                                }
                                catch
                                {
                                    mi = mTimeLineChart.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTimeLineChart, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTimeLineChart.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTimeLineChart, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTimeLineChart.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTimeLineChart, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "toolbar":
                    mToolbar = new DlkToolbar(mControl.mKey.ToString(), mControl.mSearchMethod.ToString(), mControl.mSearchParameters.Split('~')[0].ToString());
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
                case "tooltip":
                    mTooltip = new DlkToolTip(mControl.mKey.ToString(), mControl.mSearchMethod.ToString(), mControl.mSearchParameters.Split('~')[0].ToString());
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTooltip.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTooltip, null);
                                }
                                catch
                                {
                                    mi = mTooltip.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTooltip, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTooltip.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTooltip, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTooltip.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTooltip, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "treelist":
                    mTreeList = new DlkTreeList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mTreeList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mTreeList, null);
                                }
                                catch
                                {
                                    mi = mTreeList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mTreeList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mTreeList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mTreeList, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mTreeList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mTreeList, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mTreeList.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTreeList, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
