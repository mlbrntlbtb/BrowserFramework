using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using SBCLib.DlkControls;

namespace SBCLib.DlkSystem
{
    public static class DlkSBCKeywordHandler
    {

        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public static void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, ControlName);
            DlkButton mButton;
            DlkLabel mLabel;
            DlkTextBox mTextBox;
            DlkMenu mMenu;
            DlkLink mLink;
            DlkDropdown mDropdown;
            DlkRadioButton mRadioButton;
            DlkCheckBox mCheckBox;
            DlkCheckBoxList mCheckBoxList;
            DlkTable mTable;
            DlkTableTree mTableTree;
            DlkAccordion mAccordion;
            DlkTreeList mTreeList;
            DlkRichTextEditor mRichTextEditor;
            DlkComboBox mComboBox;
            DlkDivisionAccordion mDivisionAccordion;
            DlkOutputDocument mOutputDocument;
            DlkDataContainer mDataContainer;
            DlkQuestionList mQuestionList;
            DlkList mList;
            DlkManufacturerGrid mManufacturerGrid;
            DlkSustainabilityGrid mSustainabilityGrid;
            DlkNavList mNavList;
            DlkSelectionContainer mSelectionContainer;
            DlkGrid mGrid;

            switch (mControl.mControlType.ToLower())
            {
                case "button":
                    mButton = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mButton, Parameters, Keyword);
                    break;
                case "label":
                    mLabel = new DlkLabel(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLabel, Parameters, Keyword);
                    break;
                case "textbox":
                    mTextBox = new DlkTextBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTextBox, Parameters, Keyword);
                    break;
                case "menu":
                    mMenu = new DlkMenu(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mMenu, Parameters, Keyword);
                    break;
                case "link":
                    mLink = new DlkLink(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mLink, Parameters, Keyword);
                    break;
                case "list":
                    mList = new DlkList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mList, Parameters, Keyword);
                    break;
                case "dropdown":
                    mDropdown = new DlkDropdown(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDropdown, Parameters, Keyword);
                    break;
                case "radiobutton":
                    mRadioButton = new DlkRadioButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRadioButton, Parameters, Keyword);
                    break;
                case "checkbox":
                    mCheckBox = new DlkCheckBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckBox, Parameters, Keyword);
                    break;
                case "table":
                    mTable = new DlkTable(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTable, Parameters, Keyword);
                    break;
               case "tabletree":
                    mTableTree = new DlkTableTree(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTableTree, Parameters, Keyword);
                    break;
                case "accordion":
                    mAccordion = new DlkAccordion(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mAccordion, Parameters, Keyword);
                    break;
                case "treelist":
                    mTreeList = new DlkTreeList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mTreeList, Parameters, Keyword);
                    break;
                case "richtexteditor":
                    mRichTextEditor = new DlkRichTextEditor(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mRichTextEditor, Parameters, Keyword);
                    break;
                case "combobox":
                    mComboBox = new DlkComboBox(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mComboBox, Parameters, Keyword);
                    break;
                case "divisionaccordion":
                    mDivisionAccordion = new DlkDivisionAccordion(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDivisionAccordion, Parameters, Keyword);
                    break;
                case "outputdocument":
                    mOutputDocument = new DlkOutputDocument(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mOutputDocument, Parameters, Keyword);
                    break;
                case "datacontainer":
                    mDataContainer = new DlkDataContainer(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mDataContainer, Parameters, Keyword);
                    break;
                case "questionlist":
                    mQuestionList = new DlkQuestionList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mQuestionList, Parameters, Keyword);
                    break;
                case "manufacturergrid":
                    mManufacturerGrid = new DlkManufacturerGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mManufacturerGrid, Parameters, Keyword);
                    break;
                case "sustainabilitygrid":
                    mSustainabilityGrid = new DlkSustainabilityGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mSustainabilityGrid, Parameters, Keyword);
                    break;
                case "navlist":
                    mNavList = new DlkNavList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mNavList, Parameters, Keyword);
                    break;
                case "selectioncontainer":
                    mSelectionContainer = new DlkSelectionContainer(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mSelectionContainer, Parameters, Keyword);
                    break;
                case "checkboxlist":
                    mCheckBoxList = new DlkCheckBoxList(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mCheckBoxList, Parameters, Keyword);
                    break;
                case "grid":
                    mGrid = new DlkGrid(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
                    ExecuteKeyword(mGrid, Parameters, Keyword);
                    break;
                default:
                    throw new Exception("Unsupported control type: " + mControl.mControlType);
            }
        }
        private static void ExecuteKeyword(object mObject, String[] Parameters, String Keyword)
        {
            MethodInfo mi;

            switch (Parameters.Length)
            {
                case 1:
                    if (Parameters[0] == "")
                    {
                        try
                        {
                            mi = mObject.GetType().GetMethod(Keyword, new Type[] { });
                            mi.Invoke(mObject, null);
                        }
                        catch
                        {
                            mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                            mi.Invoke(mObject, new[] { Parameters[0] });
                        }
                    }
                    else
                    {
                        mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String) });
                        mi.Invoke(mObject, new[] { Parameters[0] });
                    }
                    break;
                case 2:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1] });
                    break;
                case 3:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1], Parameters[2] });
                    break;
                case 4:
                    mi = mObject.GetType().GetMethod(Keyword, new Type[] { typeof(String), typeof(String), typeof(String), typeof(String) });
                    mi.Invoke(mObject, new[] { Parameters[0], Parameters[1], Parameters[2], Parameters[3] });
                    break;
                default:
                    throw new Exception("Unsupported keyword: " + Keyword);
            }
        }
    }
}
