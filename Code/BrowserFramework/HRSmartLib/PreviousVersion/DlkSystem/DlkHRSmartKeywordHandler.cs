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
using HRSmartLib.PreviousVersion.DlkControls;

namespace HRSmartLib.PreviousVersion.System
{
    /// <summary>
    /// Keyword handlers are product specific
    /// They allow us to map object store definitions to real objects
    /// </summary>
    public class DlkHRSmartKeywordHandler : HRSmartLib.DlkSystem.IKeywordHandler
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            object control = null;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    {
                        control = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "textbox":
                    {
                        control = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "label":
                    {
                        control = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "link":
                    {
                        control = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "checkbox" :
                    {
                        control = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "dropdownmenu" :
                    {
                        control = new DlkDropDownMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "table" :
                    {
                        control = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "pagination" :
                    {
                        control = new DlkPagination(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "multiselect" :
                    {
                        control = new DlkMultiSelect(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "combobox" :
                    {
                        control = new DlkComboBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "radiobutton" :
                    {
                        control = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "richtexteditor":
                    {
                        control = new DlkRichTextEditor(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "autosearchdropdown":
                    {
                        control = new DlkAutoSearchDropDown(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "buttongroup":
                    {
                        control = new DlkButtonGroup(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "duallist":
                    {
                        control = new DlkDualList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "dynamicform":
                    {
                        control = new DlkDynamicForm(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "slider":
                    {
                        control = new DlkSlider(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "treeview":
                    {
                        control = new DlkTreeView(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "tab":
                    {
                        control = new DlkTab(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "rowlist":
                    {
                        control = new DlkRowList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "employeelist" :
                    {
                        control = new DlkEmployeeList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "containergroup":
                    {
                        control = new DlkContainerGroup(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "wizard":
                    {
                        control = new DlkWizard(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "list":
                    {
                        control = new DlkList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "progressbar":
                    {
                        control = new DlkProgressBar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "calendar":
                    {
                        control = new DlkCalendar(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "widget":
                    {
                        control = new DlkWidget(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "image":
                    {
                        control = new DlkImage(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                case "messagemodal":
                    {
                        control = new DlkMessageModal(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters.Split('~'));
                        break;
                    }
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }

            invokeControlKeyword(control, Keyword, Parameters);
        }

        private static void invokeControlKeyword(object control, string keyWord, string[] Parameters)
        {
            MethodInfo mi;
            object[] oParameters = new object[Parameters.Length];
            Type[] tParameters = new Type[Parameters.Length];

            //Initialize parameters.
            for (int i = 0; i < Parameters.Length; i++)
            {
                oParameters[i] = Parameters[i];
                tParameters[i] = typeof(String);
            }

            if (Parameters[0] == "")
            {
                try
                {
                    mi = control.GetType().GetMethod(keyWord, new Type[] { });
                    mi.Invoke(control, null);
                }
                catch
                {
                    mi = control.GetType().GetMethod(keyWord, new Type[] { typeof(String) });
                    mi.Invoke(control, oParameters);
                }
            }
            else
            {
                mi = control.GetType().GetMethod(keyWord, tParameters);
                mi.Invoke(control, oParameters);
            }
        }
    }
}
