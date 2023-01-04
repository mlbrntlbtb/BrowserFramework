using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using CommonLib.DlkSystem;
//using NavigatorLib.DlkControls;

namespace NavigatorLib.DlkUtility
{
    public class DlkNavigatorControlHelper : DlkControlHelper
    {
        public override string DetectControlType(DlkBaseControl control)
        {
            //DlkButton mButton;
            //DlkCheckBox mCheckBox;
            //DlkComboBox mComboBox;
            //DlkLabel mLabel;
            //DlkLink mLink;
            //DlkQuickEdit mQuickEdit;
            //DlkSideBar mSideBar;
            //DlkTab mTab;
            //DlkTable mTable;
            //DlkTextArea mTextArea;
            //DlkTextBox mTextBox;
            //DlkToolbar mToolbar;
            //DlkUIDialog mUIDialog;
            //DlkRadioButton mRadioButton;

            //mTable = new DlkTable("Table", control.mElement);
            //if (mTable.VerifyControlType())
            //    return "Table";
            //mQuickEdit = new DlkQuickEdit("QuickEdit", control.mElement);
            //if (mQuickEdit.VerifyControlType())
            //    return "QuickEdit";
            //mButton = new DlkButton("Button", control.mElement);
            //if (mButton.VerifyControlType())
            //    return "Button";
            //mComboBox = new DlkComboBox("ComboBox", control.mElement);
            //if (mComboBox.VerifyControlType())
            //    return "ComboBox";
            //mRadioButton = new DlkRadioButton("TextBox", control.mElement);
            //if (mRadioButton.VerifyControlType())
            //    return "RadioButton";
            //mCheckBox = new DlkCheckBox("CheckBox", control.mElement);
            //if (mCheckBox.VerifyControlType())
            //    return "CheckBox";
            //mTextBox = new DlkTextBox("TextBox", control.mElement);
            //if (mTextBox.VerifyControlType())
            //    return "TextBox";
            //mTextArea = new DlkTextArea("TextArea", control.mElement);
            //if (mTextArea.VerifyControlType())
            //    return "TextArea";
            //mSideBar = new DlkSideBar("SideBar", control.mElement);
            //if (mSideBar.VerifyControlType())
            //    return "SideBar";
            //mLabel = new DlkLabel("Label", control.mElement);
            //if (mLabel.VerifyControlType())
            //    return "Label";
            //mLink = new DlkLink("Link", control.mElement);
            //if (mLink.VerifyControlType())
            //    return "Link";
            //mUIDialog = new DlkUIDialog("UIDialog", control.mElement);
            //if (mUIDialog.VerifyControlType())
            //    return "UIDialog";
            //mTab = new DlkTab("Tab", control.mElement);
            //if (mTab.VerifyControlType())
            //    return "Tab";

            return "";

        }

        public override void AutoCorrectSearchMethod(DlkBaseControl control, string controlType, ref string SearchType, ref string SearchValue)
        {
            //DlkButton mButton;
            //DlkCheckBox mCheckBox;
            //DlkComboBox mComboBox;
            //DlkLabel mLabel;
            //DlkLink mLink;
            //DlkQuickEdit mQuickEdit;
            //DlkSideBar mSideBar;
            //DlkTab mTab;
            //DlkTable mTable;
            //DlkTextArea mTextArea;
            //DlkTextBox mTextBox;
            //DlkToolbar mToolbar;
            //DlkUIDialog mUIDialog;

            //switch (controlType.ToLower())
            //{
            //    case "button":
            //        mButton = new DlkButton("Button", control.mElement);
            //        mButton.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "combobox":
            //        mComboBox = new DlkComboBox("ComboBox", control.mElement);
            //        mComboBox.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "label":
            //        mLabel = new DlkLabel("Label", control.mElement);
            //        mLabel.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "link":
            //        mLink = new DlkLink("Link", control.mElement);
            //        mLink.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "quickedit":
            //        mQuickEdit = new DlkQuickEdit("QuickEdit", control.mElement);
            //        mQuickEdit.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "sidebar":
            //        mSideBar = new DlkSideBar("SideBar", control.mElement);
            //        mSideBar.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "tab":
            //        mTab = new DlkTab("Tab", control.mElement);
            //        mTab.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "textarea":
            //        mTextArea = new DlkTextArea("Tab", control.mElement);
            //        mTextArea.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "textbox":
            //        mTextBox = new DlkTextBox("TextBox", control.mElement);
            //        mTextBox.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "uidialog":
            //        mUIDialog = new DlkUIDialog("UIDialog", control.mElement);
            //        mUIDialog.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;
            //    case "table":
            //        mTable = new DlkTable("Table", control.mElement);
            //        mTable.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;

            //}
        }

        public override string[] GetControlTypes()
        {
            Assembly assy = Assembly.GetExecutingAssembly();
            List<string> ret = new List<string>();
            foreach (Type aType in assy.GetTypes())
            {
                foreach (Attribute attribControlType in aType.GetCustomAttributes(typeof(ControlType), true))
                {
                    string controlType = ((ControlType)attribControlType).controltype;
                    ret.Add(controlType);
                }
            }
            ret.Sort();
            return ret.ToArray();
        }
    }
}
