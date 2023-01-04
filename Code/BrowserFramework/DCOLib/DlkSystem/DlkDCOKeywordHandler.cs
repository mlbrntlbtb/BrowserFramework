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
using DCOLib.DlkControls;

namespace DCOLib.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public static class DlkDCOKeywordHandler
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
            DlkLink mLink;
            DlkForm mForm;
            DlkCheckBox mCheckBox;
            DlkComboBox mComboBox;
            DlkTable mTable;
            DlkTextArea mTextArea;

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

                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }
    }
}
