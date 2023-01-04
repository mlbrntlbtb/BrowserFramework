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
using TM1WebLib.DlkControls;

namespace TM1WebLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkTM1WebKeywordHandler
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
            DlkCheckBox mCheckBox;
            DlkDropdownList mDropdownList;
            DlkDropdownTree mDropdownTree;
            DlkNavigationTree mNavigationTree;
            DlkLink mLink;
            DlkScrollableGrid mScrollableGrid;
            DlkLabel mLabel;
            DlkToolbar mToolbar;
            DlkTab mTab;
            DlkContextMenu mContextMenu;

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
                case "dropdownlist":
                    mDropdownList = new DlkDropdownList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDropdownList.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDropdownList, null);
                                }
                                catch
                                {
                                    mi = mDropdownList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDropdownList, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDropdownList.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDropdownList, new[] { Parameters[0] });
                            }
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "dropdowntree":
                    mDropdownTree = new DlkDropdownTree(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mDropdownTree.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mDropdownTree, null);
                                }
                                catch
                                {
                                    mi = mDropdownTree.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mDropdownTree, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mDropdownTree.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mDropdownTree, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mDropdownTree.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mDropdownTree, new[] { Parameters[0], Parameters[1] });
                            break;
                        default:
                            throw new Exception("Unsupported keyword: " + Keyword);
                    }
                    break;
                case "navigationtree":
                    mNavigationTree = new DlkNavigationTree(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mNavigationTree.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mNavigationTree, null);
                                }
                                catch
                                {
                                    mi = mNavigationTree.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mNavigationTree, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mNavigationTree.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mNavigationTree, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mNavigationTree.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mNavigationTree, new[] { Parameters[0], Parameters[1] });
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
                case "scrollablegrid":
                    mScrollableGrid = new DlkScrollableGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                try
                                {
                                    mi = mScrollableGrid.GetType().GetMethod(Keyword, new Type[] { });
                                    mi.Invoke(mScrollableGrid, null);
                                }
                                catch
                                {
                                    mi = mScrollableGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                    mi.Invoke(mScrollableGrid, new[] { Parameters[0] });
                                }
                            }
                            else
                            {
                                mi = mScrollableGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                                mi.Invoke(mScrollableGrid, new[] { Parameters[0] });
                            }
                            break;
                        case 2:
                            mi = mScrollableGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mScrollableGrid, new[] { Parameters[0], Parameters[1] });
                            break;
                        case 3:
                            mi = mScrollableGrid.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mScrollableGrid, new[] { Parameters[0], Parameters[1], Parameters[2] });
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
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }
    }
}
