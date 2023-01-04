using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using DeltekProjectsToolLib.DlkControls;
using System.Reflection;

namespace DeltekProjectsToolLib.DlkSystem
{
    public static class DlkDeltekProjectsToolKeywordHandler
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
            DlkTable mTable;
            DlkComboBox mComboBox;
            DlkLabel mLabel;
            DlkUIDialog mUIDialog;
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
                                mi = mButton.GetType().GetMethod(Keyword, new Type[] { });
                                mi.Invoke(mButton, null);
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
                case "uidialog":
                    mUIDialog = new DlkUIDialog(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                    switch (Parameters.Length)
                    {
                        case 1:
                            if (Parameters[0] == "")
                            {
                                mi = mUIDialog.GetType().GetMethod(Keyword, new Type[] { });
                                mi.Invoke(mUIDialog, null);
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
                        case 5:
                            mi = mTable.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String), typeof(String) });
                            mi.Invoke(mTable, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4] });
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
                        case 2:
                            mi = mLabel.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                            mi.Invoke(mLabel, new[] { Parameters[0], Parameters[1] });
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
